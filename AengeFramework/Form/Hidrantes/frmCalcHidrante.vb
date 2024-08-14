Imports System.Data.OleDb
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmCalcHidrante

#Region "----- Atributes and Declarations -----"

    Dim DtTIncFatorC As System.Data.DataTable, DtEstado As System.Data.DataTable

    Private Shared mVar_NameClass As String = "frmCalcHidrante"

#End Region

#Region "----- Events -----"

#Region "----- ComboBox -----"

    Private Sub cboTipoSistema_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboTipoSistema.KeyPress
        AutoComplete(cboTipoSistema, e, True)
    End Sub

    Private Sub cboTipoSistema_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipoSistema.SelectedIndexChanged

    End Sub

    Private Sub cboEstado_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedValueChanged
        FillTreeView_Ocupacao(trvOcupacao, "")
    End Sub

    Private Sub cboTipoSistema_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoSistema.SelectedValueChanged
        If Not cboTipoSistema.SelectedValue.ToString.Contains("System.Data.DataRowView") Then
            FillTreeView_Ocupacao(trvOcupacao, "")
            Dim LibHidrante As New LibraryHidrante
            With LibHidrante
                .FillcboHidrante(cbohidrante1, cboTipoSistema.SelectedValue)
                .FillcboHidrante(cbohidrante2, cboTipoSistema.SelectedValue)
            End With
            LibHidrante = Nothing
        End If
    End Sub

    Private Sub cboEstado_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboEstado.KeyPress
        AutoComplete(cboEstado, e, True)
    End Sub

    Private Sub cboEstado_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedIndexChanged

    End Sub

    Private Sub cboFatorC_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboFatorC.KeyPress
        AutoComplete(cboFatorC, e, True)
    End Sub

    Private Sub cboFatorC_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFatorC.SelectedIndexChanged

    End Sub

    Private Sub cboFatorC_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboFatorC.SelectedValueChanged

        lblFatorC.Text = "-"
        If IsNumeric(cboFatorC.SelectedValue) Then
            If DtTIncFatorC.Select("CodFatorC = " & cboFatorC.SelectedValue).Length > 0 Then
                lblFatorC.Text = DtTIncFatorC.Select("CodFatorC = " & cboFatorC.SelectedValue)(0)("NumFatorC").ToString
            End If
        End If

    End Sub

    Private Sub cboEsguincho_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboEsguincho.KeyPress
        AutoComplete(cboEsguincho, e, True)
    End Sub

    Private Sub cboEsguincho_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEsguincho.SelectedIndexChanged

    End Sub

#End Region

#Region "----- Forms -----"

    Private Sub frmCalcHidrante_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        DtTIncFatorC = New System.Data.DataTable : DtEstado = New System.Data.DataTable

        Dim LibraryHidrante As New LibraryHidrante
        With LibraryHidrante
            .FillEstado(cboEstado, True)
            .FillTipoSistema(cboTipoSistema)
        End With

        FillFatorC() : FillEsguincho()

        LibraryHidrante = Nothing
    End Sub

#End Region

#Region "----- CheckBox -----"

    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        If chkAll.Checked Then
            cboEstado.Enabled = False
        Else
            cboEstado.Enabled = True
        End If
    End Sub

#End Region

#Region "----- CommandButton -----"

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose(True)
    End Sub

#End Region

#Region "----- Treeview -----"

    Private Sub trvOcupacao_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvOcupacao.AfterSelect

    End Sub

    Private Sub trvOcupacao_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvOcupacao.NodeMouseClick

        With trvOcupacao

            If e.Node.Tag.ToString.Contains("NODE1") Then
                lblCargaIncendio.Text = e.Node.Tag.ToString.Split("|")(3)
                lblSistema.Text = e.Node.Tag.ToString.Split("|")(4)
            Else
                lblCargaIncendio.Text = "-"
                lblSistema.Text = "-"
            End If

        End With

    End Sub

#End Region

#End Region

#Region "----- Functions for fill components -----"

    'Combo com todos os esguinchos 
    Function FillEsguincho() As Object

        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
        Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent
        Dim DtEsguincho As New DataTable

        Try

            If ConnAhid Is Nothing Then
                With LibraryConnection
                    .TypeDb = "AHID_"
                    ConnAhid = .Aenge_OpenConnectionDB
                End With
            End If

            Dim Da As New OleDbDataAdapter("Select * From TINCOBJ Where IdtTipoObj = 'ESG' Order By DesIncObj ASC", ConnAhid)
            Da.Fill(DtEsguincho)

            With LibraryComponent
                .FillCombo(cboEsguincho, DtEsguincho, "DesIncObj", "CodIncObj")
            End With

            Da = Nothing
            Return True

        Catch ex As Exception
            MsgBox("Error fill component - Esguincho - " & ex.Message, MsgBoxStyle.Information, "Error Esguincho")
            Return False
        Finally
            LibraryConnection = Nothing
            LibraryComponent = Nothing
        End Try

    End Function

    Function FillFatorC() As Object

        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
        Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent

        Try

            If ConnAhid Is Nothing Then
                With LibraryConnection
                    .TypeDb = "AHID_"
                    ConnAhid = .Aenge_OpenConnectionDB
                End With
            End If

            Dim Da As New OleDbDataAdapter("Select * From TINCFATORC", ConnAhid)
            Da.Fill(DtTIncFatorC)

            With LibraryComponent
                .FillCombo(cboFatorC, DtTIncFatorC, "DesFatorC", "CodFatorC")
            End With

            Return True

        Catch ex As Exception
            MsgBox("Error fill component - FatorC - " & ex.Message, MsgBoxStyle.Information, "Error CalcHidrante")
            Return False
        Finally
            LibraryConnection = Nothing
            LibraryComponent = Nothing
        End Try

    End Function

#End Region

#Region "----- Functions Fill Treeview -----"

    'Treeview para descrições e nomes de dispositivos de proteção
    Function FillTreeView_Ocupacao(ByVal Trv As TreeView, ByVal sSql As String, Optional ByVal ClearTView As Boolean = True, Optional ByVal Dtbl As System.Data.DataTable = Nothing, Optional ByVal TypeData As String = "", Optional ByVal ArrayColumns() As String = Nothing, _
    Optional ByVal FormatoDt As String = "dd/mm/yyyy", Optional ByVal LstImg As System.Windows.Forms.ImageList = Nothing) As Boolean

        If ClearTView = True Then
            Trv.ResetText()
            Trv.Nodes.Clear()
        End If

        Dim Resultado As String = "Não informado", AtualizouNodeProtocolo As Boolean = False, TempDt_Nasc As String = ""
        Dim LimpaDrR As Boolean = False, Texto As String = "", Cor As New Color, Column0 As String = "", Column1 As String = ""
        Dtbl = Nothing

        Try

            Dim StrSql As String = "SELECT TINCOCUPACAODESC.CODDESC, TINCOCUPACAODESC.DESOCUPDESC, TINCOCUPACAODESC.DESDIVISAO, TINCOCUPACAODESC.NUMCARGA, TINCOCUPACAODESC.IDTDIVISAO,"
            StrSql += " TINCOCUPACAODESC.NUMDIVISAO, TINCOCUPACAODESC.CODOCUP, TINCOCUPACAO.DESOCUP, TINCOCUPACAO.IDTESTADO, TINCOCUPACAO.CODTIPO, TINCTIPO.DESCTIPO"
            StrSql += " FROM TINCTIPO INNER JOIN (TINCOCUPACAO INNER JOIN TINCOCUPACAODESC ON TINCOCUPACAO.CODOCUP = TINCOCUPACAODESC.CODOCUP) ON TINCTIPO.CODTIPO = TINCOCUPACAO.CODTIPO"
            StrSql += " WHERE TINCOCUPACAO.CODTIPO = " & cboTipoSistema.SelectedValue.ToString

            If cboEstado.Text <> "TODOS" And cboEstado.Text <> "" Then StrSql += " AND TINCOCUPACAO.IDTESTADO = '" & cboEstado.Text & "'"
            StrSql += " ORDER BY TINCOCUPACAO.DESOCUP ASC, TINCOCUPACAODESC.DESOCUPDESC ASC"

            'Povoa com os dados de tipos de ocupacao 
            If Dtbl Is Nothing Then
                Dim Da As OleDbDataAdapter = New OleDbDataAdapter(StrSql, ConnAhid)
                Dtbl = New DataTable
                Da.Fill(Dtbl)
            End If

            'Cria um datareader para manipular as informações mais facilmente
            Dim DrR As DataTableReader
            DrR = Dtbl.CreateDataReader

            'Declarações para o Treeview
            Dim Node0 As TreeNode = Nothing, i As Integer = 0
            Dim Node1 As TreeNode = Nothing, Node2 As TreeNode = Nothing, Column2 As String = ""

            Do While DrR.Read

                'Povoa o list com as informações agrupadas
                With Trv

                    .ShowNodeToolTips = True

                    If Column0 <> DrR("DESOCUP").ToString Then
                        'Monta a treeview com a sequencia --> Column0, Column1 e Column2
                        'Nó principal - Column0 - Primeira Coluna 
                        Node0 = New TreeNode
                        Node0.Text = DrR("DESOCUP").ToString
                        'Armazena somente id para a tabela de notas fiscais
                        Node0.Tag = "NODE0|PRJ"
                        .ShowNodeToolTips = True
                        'Node0.ImageIndex = 3
                        'Node0.SelectedImageIndex = 3
                        .Nodes.Add(Node0)
                        Column0 = DrR("DESOCUP").ToString
                        i += 1

                        'Informações referentes ao nó com o nome do cliente 
                        'NodeProtocolo.ImageIndex = 15
                        Node1 = New TreeNode
                        With Node1
                            'Será utilizado para consultas para não precisar reconsultar no banco de dados 
                            'Codigo / Tipo de sistema / Carga
                            .Tag = "NODE1|" & DrR("CODDESC").ToString & "|" & DrR("NUMDIVISAO") & "|" & DrR("NUMCARGA") & "|" & DrR("DESCTIPO")
                            .ToolTipText = "Divisão : " & DrR("DESDIVISAO").ToString & Chr(13) & "Carga de incêndio : " & DrR("NUMCARGA").ToString
                            .Text = DrR("DESOCUPDESC").ToString
                            '.ImageIndex = 1
                            '.SelectedImageIndex = 1
                        End With

                        Column1 = DrR("DESOCUPDESC").ToString
                        'Neste caso adicionou o node0, node1
                        Node0.Nodes.Add(Node1)

                    Else

                        If Column1 <> DrR("DESOCUP").ToString Then
                            Node1 = New TreeNode
                            With Node1
                                'Será utilizado para consultas para não precisar reconsultar no banco de dados 
                                .Tag = "NODE1|" & DrR("CODDESC").ToString & "|" & DrR("NUMDIVISAO") & "|" & DrR("NUMCARGA") & "|" & DrR("DESCTIPO")
                                .ToolTipText = "Divisão : " & DrR("DESDIVISAO").ToString & Chr(13) & "Carga de incêndio : " & DrR("NUMCARGA").ToString
                                .Text = DrR("DESOCUPDESC").ToString
                                '.ImageIndex = 1
                                '.SelectedImageIndex = 1
                            End With
                            Column1 = DrR("DESOCUPDESC").ToString
                            Node0.Nodes.Add(Node1)
                            AtualizouNodeProtocolo = True
                        End If
                    End If
                End With
            Loop

            If LimpaDrR = True Then DrR.Close()
            'Trv.ExpandAll()
            Return True

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao povoar as informações do componente Treeview - Erro de validação e consulta de ocupação/uso !", , _
                                          mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "Application_Library003")
            Return False
        Finally

        End Try

    End Function

#End Region

End Class
Imports System.Data.OleDb
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmCalcHidranteTypes

#Region "----- Atributes and declarations -----"

    Private Shared mVar_ListObj As Object, mVar_DtGrid As System.Windows.Forms.DataGridView, LoadAllInfo As Boolean = False
    Private Shared mVar_NameClass As String = "frmCalcHidranteTypes", mVar_IdFormat As String = ""
    Private Shared msgErrorTit As String = "Error aenge", msgErrorIni As String = "Ocorrência de erro - "

#End Region

#Region "----- Get and Set -----"

    'List with all objects selected by user
    Property DataObj() As Object
        Get
            DataObj = mVar_ListObj
        End Get
        Set(ByVal value As Object)
            mVar_ListObj = value
        End Set
    End Property

    Property IdFormat() As String
        Get
            IdFormat = mVar_IdFormat
        End Get
        Set(ByVal value As String)
            mVar_IdFormat = value
        End Set
    End Property

#End Region

#Region "----- Events -----"

#Region "----- Forms -----"

    Private Sub frmCalcHidranteTypes_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
        If ConnAhid Is Nothing Then
            With LibraryConnection
                .TypeDb = "AHID_"
                ConnAhid = .Aenge_OpenConnectionDB
            End With
        End If
        LibraryConnection = Nothing

        Dim LibraryHidrante As New LibraryHidrante
        cboEstado.Visible = True : lblEstado1.Visible = True
        With LibraryHidrante
            .FillTipoSistema(cboTipo)
            .FillTipoSistema(cboTipo_E)
            .FillTipoSistema(cboTipo_V)
            .FillTipoSistema(cboTipo_M)
            '.FillEstado(cboEstado, False)
            .FillEsguicho(cboDescEsg)
            .FillMangueira(cboDescMang)
        End With

        LibraryHidrante = Nothing
        FillGrid_Ocupacao()
        FillGrid_FatorC()
        FillGrid_VazaoMinima()
        FillGrid_Esguicho()
        FillGrid_Mangueira()

    End Sub

#End Region

#Region "----- ComboBox -----"

    Private Sub cboTipo_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipo.SelectedValueChanged

        cboOcupacao.DataSource = Nothing
        If Not IsNumeric(cboTipo.SelectedValue) Then Exit Sub

        Dim LIbraryHidrante As New LibraryHidrante
        With LIbraryHidrante
            .FillOcupacao(cboOcupacao, cboTipo.SelectedValue)
        End With
        LIbraryHidrante = Nothing

        FillGrid_Ocupacao()

    End Sub

    Private Sub cboTipo_E_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipo_E.SelectedValueChanged
        FillGrid_Esguicho()
    End Sub

    Private Sub cboTipo_M_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipo_M.SelectedValueChanged
        FillGrid_Mangueira()
    End Sub

    Private Sub cboOcupacao_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboOcupacao.KeyPress
        AutoComplete(cboOcupacao, e, True)
    End Sub

    Private Sub cboOcupacao_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboOcupacao.SelectedIndexChanged

    End Sub

    Private Sub cboDescEsg_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboDescEsg.KeyPress
        AutoComplete(cboDescEsg, e, False)
    End Sub

    Private Sub cboDescEsg_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDescEsg.SelectedIndexChanged

    End Sub

    Private Sub cboDescMang_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboDescMang.KeyPress
        AutoComplete(cboDescMang, e, False)
    End Sub

    Private Sub cboDescMang_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDescMang.SelectedIndexChanged

    End Sub

    Private Sub tbControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbControl.SelectedIndexChanged

    End Sub

    Private Sub cboTipo_E_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboTipo_E.KeyPress
        AutoComplete(cboTipo_E, e, True)
    End Sub

    Private Sub cboTipo_E_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipo_E.SelectedIndexChanged

    End Sub

    Private Sub cboTipo_M_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboTipo_M.KeyPress
        AutoComplete(cboTipo_M, e, True)
    End Sub

    Private Sub cboTipo_M_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipo_M.SelectedIndexChanged

    End Sub

    Private Sub cboTipo_V_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboTipo_V.KeyPress
        AutoComplete(cboTipo_V, e, True)
    End Sub

    Private Sub cboTipo_V_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipo_V.SelectedIndexChanged

    End Sub

    Private Sub cboEstado_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboEstado.KeyPress
        AutoComplete(cboEstado, e, True)
    End Sub

    Private Sub cboEstado_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedIndexChanged

    End Sub

    Private Sub cboTipo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboTipo.KeyPress
        AutoComplete(cboTipo, e, True)
    End Sub

    Private Sub cboTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTipo.SelectedIndexChanged

    End Sub

    Private Sub cboEstado_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEstado.SelectedValueChanged
        FillGrid_Ocupacao()
    End Sub

#End Region

#Region "----- CommandButton -----"

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CallHelp()
    End Sub

    Private Sub btnNewGeneral_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewGeneral.Click
        newGeneral()
    End Sub

    Private Sub btnDeleteGeneral_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteGeneral.Click
        DeleteGeneral()
    End Sub

    Private Sub btnSaveGeneral_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveGeneral.Click
        SaveGeneral()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose(True)
    End Sub

#End Region

#Region "----- ContextMenu -----"

    Private Sub tbtnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbtnnew.Click
        Select Case mVar_IdFormat.ToLower
            Case "ocupacao".ToLower
                txtDescOcup.Text = "" : txtCarga.Text = "" : txtDivisao.Text = "" : txtCodDesc.Text = "" : cboTipo.SelectedValue = 1
                txtDescOcup.ReadOnly = False : txtCarga.ReadOnly = False : txtDivisao.ReadOnly = False : cboTipo.Enabled = True

            Case "fatorc".ToLower
                txtDescFator.Text = "" : txtFatorC.Text = "" : txtCodDescFatorC.Text = ""
                txtDescFator.ReadOnly = False : txtFatorC.ReadOnly = False

        End Select

    End Sub

    Private Sub tbtnRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbtnRename.Click
        Select mVar_IdFormat.ToLower
            Case "ocupacao".ToLower
                If SelectRegOcupacao() = False Then Exit Sub
                txtDescOcup.ReadOnly = False : txtCarga.ReadOnly = False : txtDivisao.ReadOnly = False : cboTipo.Enabled = False

            Case "fatorc".ToLower
                If SelectRegFatorC() = False Then Exit Sub
                txtDescFator.ReadOnly = False : txtFatorC.ReadOnly = False

        End Select
    End Sub

    Private Sub tbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbtndelete.Click
        Select Case mVar_IdFormat.ToLower
            Case "ocupacao".ToLower
                If SelectRegOcupacao() = False Then Exit Sub
                txtDescOcup.ReadOnly = True : txtCarga.ReadOnly = True : txtDivisao.ReadOnly = True : cboTipo.Enabled = False

            Case "fatorc".ToLower
                If SelectRegFatorC() = False Then Exit Sub
                txtDescFator.ReadOnly = True : txtFatorC.ReadOnly = True

        End Select
    End Sub

#End Region

#Region "----- Treeview -----"

    Private Sub trvRegister_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvRegister.AfterSelect

    End Sub

    Private Sub trvRegister_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvRegister.NodeMouseClick

        With trvRegister

            Select Case mVar_IdFormat
                Case "ocupacao"

            End Select

        End With

    End Sub

#End Region

#Region "----- DataGridView -----"

    Private Sub GridMangueira_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridMangueira.CellClick
        If e.RowIndex < 0 Then Exit Sub
        With GridMangueira
            txtDiamMang.Text = .Rows(e.RowIndex).Cells(5).Value.ToString
            txtComprMang.Text = .Rows(e.RowIndex).Cells(6).Value.ToString
            cboDescMang.Text = .Rows(e.RowIndex).Cells(2).Value.ToString
            txtCodMang.Text = .Rows(e.RowIndex).Cells(0).Value.ToString
        End With
    End Sub

    Private Sub GridMangueira_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridMangueira.CellContentClick

    End Sub

    Private Sub gridEsguicho_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridEsguicho.CellClick

        If e.RowIndex < 0 Then Exit Sub
        With gridEsguicho
            txtDiametroEsg.Text = .Rows(e.RowIndex).Cells(5).Value.ToString
            cboDescEsg.Text = .Rows(e.RowIndex).Cells(2).Value.ToString
            txtCodEsg.Text = .Rows(e.RowIndex).Cells(0).Value.ToString
        End With

    End Sub

    Private Sub gridEsguicho_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridEsguicho.CellContentClick

    End Sub

    Private Sub gridVazao_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridVazao.CellClick
        With gridVazao
            If e.RowIndex >= 0 Then
                txtVazao.Text = .Rows(e.RowIndex).Cells(1).Value.ToString
                txtPressao.Text = .Rows(e.RowIndex).Cells(2).Value.ToString
                cboTipo_V.SelectedValue = .Rows(e.RowIndex).Cells(0).Value.ToString
            End If
        End With
    End Sub

    Private Sub gridVazao_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles gridVazao.CellContentClick

    End Sub

    Private Sub GridOcup_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridOcup.CellClick

        With GridOcup
            If e.RowIndex >= 0 Then
                cboEstado.Text = .Rows(e.RowIndex).Cells(8).Value.ToString
                cboTipo.SelectedValue = .Rows(e.RowIndex).Cells(9).Value.ToString
                txtDescOcup.Text = .Rows(e.RowIndex).Cells(1).Value.ToString
                txtDivisao.Text = .Rows(e.RowIndex).Cells(2).Value.ToString
                txtCarga.Text = .Rows(e.RowIndex).Cells(3).Value.ToString
                txtCodDesc.Text = .Rows(e.RowIndex).Cells(0).Value.ToString
            End If
        End With

    End Sub

    Private Sub GridOcup_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridOcup.CellContentClick

    End Sub

    Private Sub GridFatorC_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridFatorC.CellClick
        If e.RowIndex >= 0 Then
            txtDescFator.Text = GridFatorC.Rows(e.RowIndex).Cells(1).Value.ToString
            txtFatorC.Text = GridFatorC.Rows(e.RowIndex).Cells(2).Value.ToString
            txtCodDescFatorC.Text = GridFatorC.Rows(e.RowIndex).Cells(0).Value.ToString
        End If
    End Sub

    Private Sub GridFatorC_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles GridFatorC.CellContentClick

    End Sub

#End Region

#End Region

#Region "----- Constructors -----"

#End Region

#Region "----- Functions Fill Treeview -----"

    Function FillGrid_Ocupacao() As Object

        Try

            Dim StrSql As String = "SELECT TINCOCUPACAODESC.CODDESC, TINCOCUPACAODESC.DESOCUPDESC, TINCOCUPACAODESC.DESDIVISAO, TINCOCUPACAODESC.NUMCARGA, TINCOCUPACAODESC.IDTDIVISAO," & _
           " TINCOCUPACAODESC.NUMDIVISAO, TINCOCUPACAODESC.CODOCUP, TINCOCUPACAO.DESOCUP, TINCOCUPACAO.IDTESTADO, TINCOCUPACAO.CODTIPO" & _
           " FROM TINCOCUPACAO INNER JOIN TINCOCUPACAODESC ON TINCOCUPACAO.CODOCUP = TINCOCUPACAODESC.CODOCUP WHERE TINCOCUPACAO.CODTIPO = " & cboTipo.SelectedValue
            If cboEstado.Text <> "" And cboEstado.Text <> "TODOS" Then StrSql += " AND TINCOCUPACAO.IDTESTADO = '" & cboEstado.Text & "'"
            StrSql += " ORDER BY TINCOCUPACAO.DESOCUP ASC, TINCOCUPACAODESC.DESOCUPDESC ASC"
            Dim Dt As New System.Data.DataTable
            Dim Da As New OleDbDataAdapter(StrSql, ConnAhid)

            Da.Fill(Dt)
            With GridOcup
                .DataSource = Dt
                .Columns(0).Visible = False
                .Columns(1).HeaderText = "Descrição" : .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns(2).HeaderText = "Divisão" : .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns(3).HeaderText = "Carga de incêndio" : .Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns(4).Visible = False
                .Columns(5).Visible = False
                .Columns(6).Visible = False
                .Columns(7).Visible = False
                .Columns(8).Visible = False
                .Columns(9).Visible = False
            End With

            Da = Nothing

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    'Treeview para descrições de ocupacoes 
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

            Dim StrSql As String = "SELECT TINCOCUPACAODESC.CODDESC, TINCOCUPACAODESC.DESOCUPDESC, TINCOCUPACAODESC.DESDIVISAO, TINCOCUPACAODESC.NUMCARGA, TINCOCUPACAODESC.IDTDIVISAO," & _
            " TINCOCUPACAODESC.NUMDIVISAO, TINCOCUPACAODESC.CODOCUP, TINCOCUPACAO.DESOCUP, TINCOCUPACAO.IDTESTADO, TINCOCUPACAO.CODTIPO" & _
            " FROM TINCOCUPACAO INNER JOIN TINCOCUPACAODESC ON TINCOCUPACAO.CODOCUP = TINCOCUPACAODESC.CODOCUP WHERE "
            If cboEstado.Text <> "" Then StrSql += " AND TINCOCUPACAO.IDTESTADO = '" & cboEstado.Text & "'"
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
                        'Node0.NodeFont = New Font(Node0.NodeFont, FontStyle.Bold)
                        Node0.ForeColor = Color.Blue
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
                            .Tag = "NODE1|" & DrR("CODDESC").ToString & "|" & DrR("NUMDIVISAO") & "|" & DrR("NUMCARGA") & "|" & DrR("CODTIPO") & "|" & DrR("IDTESTADO")
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
                                .Tag = "NODE1|" & DrR("CODDESC").ToString & "|" & DrR("NUMDIVISAO") & "|" & DrR("NUMCARGA") & "|" & DrR("CODTIPO") & "|" & DrR("IDTESTADO")
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
            Trv.ExpandAll()
            Return True

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao povoar as informações do componente Treeview - Erro de validação e consulta de ocupação/uso !", , _
                                          mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "Application_Library003")
            Return False
        Finally

        End Try

    End Function

    Function FillGrid_VazaoMinima() As Object

        Try

            Dim StrSql As String = "SELECT * FROM TINCVAZAOHID ORDER BY NUMVAZAO ASC"
            Dim Dt As New System.Data.DataTable
            Dim Da As New OleDbDataAdapter(StrSql, ConnAhid)

            Da.Fill(Dt)

            With gridVazao
                .DataSource = Dt
                .Columns(0).HeaderText = "Tipo de sistema"
                .Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns(1).HeaderText = "Vazão mínima"
                .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns(2).HeaderText = "Pressão mínima"
                .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            End With

            Da = Nothing

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Function FillGrid_Mangueira() As Object

        Try

            'Limpa o banco de dados 
            GridMangueira.DataSource = Nothing
            txtCodMang.Text = "" : txtDiamMang.Text = "" : txtComprMang.Text = "" : cboDescMang.Text = ""

            Dim StrSql As String = "SELECT TINCOBJDIAM.CODINCOBJDIAM, TINCOBJDIAM.CODINCOBJ, TINCOBJ.DESINCOBJ, TINCOBJ.IDTTIPOOBJ, TINCOBJDIAM.CODVAZAO, TINCOBJDIAM.NUMDIAM, TINCOBJDIAM.NUMCOMP," & _
            " TINCOBJDIAM.CODTIPO, TINCTIPO.DESCTIPO "
            StrSql += " FROM TINCOBJ INNER JOIN (TINCTIPO INNER JOIN TINCOBJDIAM ON TINCTIPO.CODTIPO = TINCOBJDIAM.CODTIPO) ON TINCOBJ.CODINCOBJ = TINCOBJDIAM.CODINCOBJ"
            StrSql += " WHERE TINCOBJ.IDTTIPOOBJ = 'MAN'"
            If IsNumeric(cboTipo_M.SelectedValue) Then StrSql += " AND TINCOBJDIAM.CODTIPO = " & cboTipo_M.SelectedValue
            StrSql += " ORDER BY TINCOBJ.DesIncObj ASC, TINCOBJDIAM.NUMDIAM ASC, TINCOBJDIAM.NUMCOMP ASC"

            Dim Dt As New System.Data.DataTable
            Dim Da As New OleDbDataAdapter(StrSql, ConnAhid)

            Da.Fill(Dt)
            With GridMangueira
                .DataSource = Dt
                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(2).HeaderText = "Descrição" : .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns(3).Visible = False
                .Columns(4).Visible = False
                .Columns(5).HeaderText = "Diametro" : .Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns(6).HeaderText = "Comprimento" : .Columns(6).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns(7).Visible = False
                .Columns(8).Visible = False
            End With
            Da = Nothing

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Function FillGrid_Esguicho() As Object

        Try

            'Limpa o banco de dados 
            gridEsguicho.DataSource = Nothing
            txtDiametroEsg.Text = "" : cboDescEsg.Text = "" : txtCodEsg.Text = ""

            Dim StrSql As String = "SELECT TINCOBJDIAM.CODINCOBJDIAM, TINCOBJDIAM.CODINCOBJ, TINCOBJ.DESINCOBJ, TINCOBJ.IDTTIPOOBJ, TINCOBJDIAM.CODVAZAO, TINCOBJDIAM.NUMDIAM, TINCOBJDIAM.NUMCOMP," & _
            " TINCOBJDIAM.CODTIPO, TINCTIPO.DESCTIPO "
            StrSql += " FROM TINCOBJ INNER JOIN (TINCTIPO INNER JOIN TINCOBJDIAM ON TINCTIPO.CODTIPO = TINCOBJDIAM.CODTIPO) ON TINCOBJ.CODINCOBJ = TINCOBJDIAM.CODINCOBJ"
            StrSql += " WHERE TINCOBJ.IDTTIPOOBJ = 'ESG'"
            If IsNumeric(cboTipo_E.SelectedValue) Then StrSql += " AND TINCOBJDIAM.CODTIPO = " & cboTipo_E.SelectedValue
            StrSql += " ORDER BY TINCOBJ.DesIncObj ASC, TINCOBJDIAM.NUMDIAM ASC, TINCOBJDIAM.NUMCOMP ASC"

            Dim Dt As New System.Data.DataTable
            Dim Da As New OleDbDataAdapter(StrSql, ConnAhid)

            Da.Fill(Dt)
            With gridEsguicho
                .DataSource = Dt
                .Columns(0).Visible = False
                .Columns(1).Visible = False
                .Columns(2).HeaderText = "Descrição" : .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns(3).Visible = False
                .Columns(4).Visible = False
                .Columns(5).HeaderText = "Diametro" : .Columns(5).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns(6).Visible = False
                .Columns(7).Visible = False
                .Columns(8).Visible = False
            End With
            Da = Nothing

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Function FillGrid_FatorC() As Object

        Try

            Dim StrSql As String = "SELECT * FROM TINCFATORC ORDER BY DESFATORC ASC"
            Dim Dt As New System.Data.DataTable
            Dim Da As New OleDbDataAdapter(StrSql, ConnAhid)

            Da.Fill(Dt)

            With GridFatorC
                .DataSource = Dt
                .Columns(0).Visible = False
                .Columns(1).HeaderText = "Descrição"
                .Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns(2).HeaderText = "Fator C"
                .Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            End With

            Da = Nothing

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    'Treeview para descrições de fator C 
    Function FillTreeView_FatorC(ByVal Trv As TreeView, ByVal sSql As String, Optional ByVal ClearTView As Boolean = True, Optional ByVal Dtbl As System.Data.DataTable = Nothing, Optional ByVal TypeData As String = "", Optional ByVal ArrayColumns() As String = Nothing, _
    Optional ByVal FormatoDt As String = "dd/mm/yyyy", Optional ByVal LstImg As System.Windows.Forms.ImageList = Nothing) As Boolean

        If ClearTView = True Then
            Trv.ResetText()
            Trv.Nodes.Clear()
        End If

        Dim Resultado As String = "Não informado", AtualizouNodeProtocolo As Boolean = False, TempDt_Nasc As String = ""
        Dim LimpaDrR As Boolean = False, Texto As String = "", Cor As New Color, Column0 As String = "", Column1 As String = ""
        Dtbl = Nothing

        Try

            Dim StrSql As String = "SELECT * FROM TINCFATORC ORDER BY DESFATORC ASC"

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

                    If Column0 <> "Fator C de Hanzen Willians".ToString Then
                        'Monta a treeview com a sequencia --> Column0, Column1 e Column2
                        'Nó principal - Column0 - Primeira Coluna 
                        Node0 = New TreeNode
                        Node0.Text = "Fator C de Hanzen Willians"
                        'Armazena somente id para a tabela de notas fiscais
                        Node0.Tag = "NODE0|PRJ"
                        'Node0.NodeFont = New Font(Node0.NodeFont, FontStyle.Bold)
                        Node0.ForeColor = Color.Blue
                        .ShowNodeToolTips = True
                        'Node0.ImageIndex = 3
                        'Node0.SelectedImageIndex = 3
                        .Nodes.Add(Node0)
                        Column0 = "Fator C de Hanzen Willians"
                        i += 1

                        'Informações referentes ao nó com o nome do cliente 
                        'NodeProtocolo.ImageIndex = 15
                        Node1 = New TreeNode
                        With Node1
                            'Será utilizado para consultas para não precisar reconsultar no banco de dados 
                            'Codigo / Tipo de sistema / Carga
                            .Tag = "NODE1|" & DrR("CODFATORC").ToString & "|" & DrR("NUMFATORC")
                            .ToolTipText = "Fator C : " & DrR("NUMFATORC").ToString
                            .Text = DrR("DESFATORC").ToString
                            '.ImageIndex = 1
                            '.SelectedImageIndex = 1
                        End With

                        Column1 = DrR("DESFATORC").ToString
                        'Neste caso adicionou o node0, node1
                        Node0.Nodes.Add(Node1)

                    Else

                        If Column1 <> DrR("DESFATORC").ToString Then
                            Node1 = New TreeNode
                            With Node1
                                'Será utilizado para consultas para não precisar reconsultar no banco de dados 
                                'Codigo / Tipo de sistema / Carga
                                .Tag = "NODE1|" & DrR("CODFATORC").ToString & "|" & DrR("NUMFATORC")
                                .ToolTipText = "Fator C : " & DrR("NUMFATORC").ToString
                                .Text = DrR("DESFATORC").ToString
                                '.ImageIndex = 1
                                '.SelectedImageIndex = 1
                            End With
                            Column1 = DrR("DESFATORC").ToString
                            Node0.Nodes.Add(Node1)
                            AtualizouNodeProtocolo = True
                        End If
                    End If
                End With
            Loop

            If LimpaDrR = True Then DrR.Close()
            Trv.ExpandAll()
            Return True

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao povoar as informações do componente Treeview - Erro de validação e consulta de fatorC !", , _
                                          mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "Application_Library003")
            Return False
        Finally

        End Try

    End Function

#End Region

#Region "----- functions for select and search register -----"

    Function SelectRegFatorC() As Boolean

        Try

            'Verifica se tem no selecionado 
            If Not trvRegister.SelectedNode.Tag.ToString.Contains("NODE1") Then
                MsgBox("Informe uma descrição válida primeiro !", MsgBoxStyle.Information, "Registro inválido")
                trvRegister.Focus()
                Return False
            End If

            Dim ArrayInfo As Object = trvRegister.SelectedNode.Tag.ToString.Split("|")
            Dim CodDesc As Object, NumFator As Object
            CodDesc = ArrayInfo(1)
            NumFator = ArrayInfo(2)

            txtDescFator.Text = trvRegister.SelectedNode.Text
            txtFatorC.Text = NumFator
            txtCodDescFatorC.Text = CodDesc

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    Function SelectRegOcupacao() As Boolean

        Try

            'Verifica se tem no selecionado 
            If Not trvRegister.SelectedNode.Tag.ToString.Contains("NODE1") Then
                MsgBox("Informe uma descrição válida primeiro !", MsgBoxStyle.Information, "Registro inválido")
                trvRegister.Focus()
                Return False
            End If

            Dim ArrayInfo As Object = trvRegister.SelectedNode.Tag.ToString.Split("|")
            Dim CodDesc As Object, NumDivisao As Object, NumCarga As Object, CodTipo As Object, IdtEstado As String
            CodDesc = ArrayInfo(1)
            NumDivisao = ArrayInfo(2)
            NumCarga = ArrayInfo(3)
            CodTipo = ArrayInfo(4)
            IdtEstado = ArrayInfo(5)

            txtDescOcup.Text = trvRegister.SelectedNode.Text
            txtCarga.Text = NumCarga
            txtCodDesc.Text = CodDesc
            txtDivisao.Text = NumDivisao
            cboEstado.Text = IdtEstado
            If IsNumeric(CodTipo) Then cboTipo.SelectedValue = CodTipo

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "----- Functions for general procedures -----"

    Function ClearFields() As Boolean

        cboTipo.SelectedValue = 1
        txtCarga.Text = ""
        txtDescOcup.Text = ""
        txtCodDesc.Text = ""
        txtDivisao.Text = ""
        Return True

    End Function

#End Region

#Region "----- Functions for save, delete e rename objects -----"

    Function DeleteOcupacao() As Object

        Dim Cmd As New OleDbCommand

        Try

            If txtCodDesc.Text = "" Then
                MsgBox("Informe um registro de ocupação válida primeiro a ser excluida !", MsgBoxStyle.Information, "Registro inválido")
                txtDescOcup.Focus()
                Return False
            End If

            If MsgBox("Deseja realmente EXCLUIR as informações do registro ?" & Chr(13) & _
                      "Atenção : a exclusão do registro não poderá ser revertida pelo usuário, se existir em outros projetos ou desenhos informações relacionadas a este registro, elas poderão ser perdidas !" & _
                      " Em caso de dúvidas, entre em contato com nosso departamento de suporte técnico !", MsgBoxStyle.YesNo, "Excluir dados") = MsgBoxResult.No Then Return False

            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
                .CommandText = "Delete TINCOcupacao Where CodOcup = " & txtCodDesc.Text
                Dim RegAfec As Int16 = .ExecuteNonQuery
            End With

            ClearFields()

            MsgBox("Dados atualizados com sucesso !", MsgBoxStyle.Information, "Dados atualizados")
            FillGrid_Ocupacao()

            Return True

        Catch ex As Exception
            MsgBox(msgErrorIni & ex.Message, MsgBoxStyle.Information, msgErrorTit)
            Return False
        End Try

    End Function

    Function SaveOcupacao() As Object

        Dim Cmd As New OleDbCommand

        Try

            If cboEstado.Text = "" Then
                MsgBox("Informe o estado relacionado ao novo registro !", MsgBoxStyle.Information, "Campo inválido")
                cboEstado.Focus()
                Return False
            End If

            If Not IsNumeric(cboOcupacao.SelectedValue) Or cboOcupacao.Text = "" Then
                MsgBox("Informe uma ocupação/uso válido primeiro !", MsgBoxStyle.Information, "Campo inválido")
                cboOcupacao.Focus()
                Return False
            End If

            If txtDescOcup.Text = "" Then
                MsgBox("Informe uma descrição de ocupação válida primeiro !", MsgBoxStyle.Information, "Campo inválido")
                txtDescOcup.Focus()
                Return False
            End If

            If txtCarga.Text = "" Or Not IsNumeric(txtCarga.Text) Then
                MsgBox("Informe a carga relacionada primeiro (INFORME NÚMEROS) !", MsgBoxStyle.Information, "Campo inválido")
                txtCarga.Focus()
                Return False
            End If

            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
            End With

            If txtCodDesc.Text = "" Then
                If MsgBox("Deseja realmente SALVAR as informações do novo registro ?", MsgBoxStyle.YesNo, "Salvar dados") = MsgBoxResult.No Then Return False
                With Cmd
                    .CommandText = "Insert Into TINCOcupacaoDesc(DESOCUPDESC, DESDIVISAO, NUMCARGA, CODOCUP) Values('" & txtDescOcup.Text.Replace(",", ".") & "', '" & txtDivisao.Text & "', " & txtCarga.Text & "," & _
                    cboOcupacao.SelectedValue & ")"
                    Dim RegAfec As Int16 = .ExecuteNonQuery
                End With
            Else
                If MsgBox("Deseja realmente ALTERAR as informações do registro ?", MsgBoxStyle.YesNo, "Alterar dados") = MsgBoxResult.No Then Return False
                With Cmd
                    .CommandText = "Update TINCOcupacaoDesc Set DESOCUPDESC = '" & txtDescOcup.Text.Replace(",", ".") & _
                    "', CODOCUP = " & cboOcupacao.SelectedValue & ", DESDIVISAO = '" & txtDivisao.Text & "', NUMCARGA = " & txtCarga.Text & " Where CodDesc = " & txtCodDesc.Text
                    Dim RegAfec As Int16 = .ExecuteNonQuery
                End With
            End If

            MsgBox("Dados salvos com sucesso !", MsgBoxStyle.Information, "Dados salvos")
            System.Windows.Forms.Application.DoEvents()
            FillGrid_Ocupacao()
            Return True

        Catch ex As Exception
            MsgBox(msgErrorIni & ex.Message, MsgBoxStyle.Information, msgErrorTit)
            Return False
        End Try

    End Function

    Function SaveFatorC() As Object

        Dim Cmd As New OleDbCommand

        Try

            If txtDescFator.Text = "" Then
                MsgBox("Informe uma descrição do fator C válido primeiro !", MsgBoxStyle.Information, "Campo inválido")
                txtDescFator.Focus()
                Return False
            End If

            If txtFatorC.Text = "" Then
                MsgBox("Informe o fator C relacionado ao novo registro !", MsgBoxStyle.Information, "Campo inválido")
                txtFatorC.Focus()
                Return False
            End If

            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
            End With

            If txtCodDescFatorC.Text = "" Then
                If MsgBox("Deseja realmente SALVAR as informações do novo registro ?", MsgBoxStyle.YesNo, "Salvar dados") = MsgBoxResult.No Then Return False
                With Cmd
                    .CommandText = "Insert Into TINCFatorC(DesFatorC, NumFatorC) Values('" & txtDescFator.Text.Replace(",", ".") & "', " & txtFatorC.Text & ")"
                    Dim RegAfec As Int16 = .ExecuteNonQuery
                End With
            Else
                If MsgBox("Deseja realmente ALTERAR as informações do registro ?", MsgBoxStyle.YesNo, "Alterar dados") = MsgBoxResult.No Then Return False
                With Cmd
                    .CommandText = "Update TINCFatorC Set DesFatorC = '" & txtDescFator.Text.Replace(",", ".") & "', NumFatorC = " & txtFatorC.Text & " Where CODFATORC = " & txtCodDescFatorC.Text
                    Dim RegAfec As Int16 = .ExecuteNonQuery
                End With
            End If

            MsgBox("Dados atualizados com sucesso !", MsgBoxStyle.Information, "Dados atualizados")
            FillGrid_FatorC()
            Return True

        Catch ex As Exception
            MsgBox(msgErrorIni & ex.Message, MsgBoxStyle.Information, msgErrorTit)
            Return False
        Finally
            Cmd = Nothing
        End Try

    End Function

    Function SaveEsguicho() As Object

        Dim Cmd As New OleDbCommand, NewOcupCad As Boolean = False

        Try

            If cboDescEsg.Text = "" Then
                MsgBox("Informe uma descrição de esguicho válida primeiro !", MsgBoxStyle.Information, "Campo inválido")
                cboDescEsg.Focus()
                Return False
            End If

            If txtDiametroEsg.Text = "" Then
                MsgBox("Informe o diâmetro !", MsgBoxStyle.Information, "Campo inválido")
                txtDiametroEsg.Focus()
                Return False
            End If

            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
            End With

            Dim CodIncObj As Object = Nothing
            Dim DescEsgTemp As String = cboDescEsg.Text

            'Antes fazemos a validacao da descricao do esguicho... senao existir iremos cadastrar na tabela tincobj
            If cboDescEsg.Text <> "" And cboDescEsg.SelectedValue Is Nothing Then
                With Cmd
                    .CommandText = "Insert Into TIncObj(DesIncObj, IdtTipoObj) Values ('" & DescEsgTemp & "', 'ESG')"
                    .ExecuteNonQuery()
                End With
                System.Windows.Forms.Application.DoEvents()
                newOcupCad = True
            End If

            Dim Da As New OleDbDataAdapter("Select * From TIncObj Where DesIncObj = '" & DescEsgTemp & "'", ConnAhid)
            Dim DtTIncObj As New System.Data.DataTable
            Da.Fill(DtTIncObj)
            CodIncObj = DtTIncObj.Rows(0)("CODINCOBJ")

            If txtCodEsg.Text = "" Then
                If MsgBox("Deseja realmente SALVAR as informações do novo registro ?", MsgBoxStyle.YesNo, "Salvar dados") = MsgBoxResult.No Then Return False
                With Cmd
                    .CommandText = "Insert Into TINCObjDiam(CODINCOBJ, CODVAZAO, NUMDIAM,  NUMCOMP, CODTIPO) Values(" & CodIncObj & ", " & cboTipo_E.SelectedValue & _
                    " , " & txtDiametroEsg.Text & ",0, " & cboTipo_E.SelectedValue & ")"
                    Dim RegAfec As Int16 = .ExecuteNonQuery
                End With
            Else
                If MsgBox("Deseja realmente ALTERAR as informações do registro ?", MsgBoxStyle.YesNo, "Alterar dados") = MsgBoxResult.No Then Return False
                With Cmd
                    .CommandText = "Update TINCObjDiam Set CODINCOBJ = " & CodIncObj & ", CODVAZAO = " & cboTipo_E.SelectedValue & _
                    ", NUMDIAM = " & txtDiametroEsg.Text & ", CODTIPO = " & cboTipo_E.SelectedValue & " Where CodIncObj = " & txtCodEsg.Text
                    Dim RegAfec As Int16 = .ExecuteNonQuery
                End With
            End If

            MsgBox("Dados atualizados com sucesso !", MsgBoxStyle.Information, "Dados atualizados")
            FillGrid_Esguicho()

            If NewOcupCad Then
                Dim LibraryHidrante As New LibraryHidrante
                With LibraryHidrante
                    cboDescEsg.DataSource = Nothing
                    .FillEsguicho(cboDescEsg)
                End With
                LibraryHidrante = Nothing
                cboDescEsg.Text = DescEsgTemp
            End If
            Return True

        Catch ex As Exception
            MsgBox(msgErrorIni & ex.Message, MsgBoxStyle.Information, msgErrorTit)
            Return False
        Finally
            Cmd = Nothing
        End Try

    End Function

    Function SaveMangueira() As Object

        Dim Cmd As New OleDbCommand

        Try

            If cboDescMang.Text = "" Then
                MsgBox("Informe uma descrição de mangueira válida primeiro !", MsgBoxStyle.Information, "Campo inválido")
                cboDescMang.Focus()
                Return False
            End If

            If txtDiamMang.Text = "" Then
                MsgBox("Informe o diâmetro !", MsgBoxStyle.Information, "Campo inválido")
                txtDiamMang.Focus()
                Return False
            End If

            If txtComprMang.Text = "" Then
                MsgBox("Informe o comprimento !", MsgBoxStyle.Information, "Campo inválido")
                txtComprMang.Focus()
                Return False
            End If

            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
            End With

            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
            End With

            Dim CodIncObj As Object = Nothing, NewOcupCad As Boolean = False
            Dim DescMangTemp As String = cboDescMang.Text

            'Antes fazemos a validacao da descricao da mangueira.. senao existir iremos cadastrar na tabela tincobj
            If cboDescMang.Text <> "" And cboDescMang.SelectedValue Is Nothing Then
                With Cmd
                    .CommandText = "Insert Into TIncObj(DesIncObj, IdtTipoObj) Values ('" & DescMangTemp & "', 'MAN')"
                    .ExecuteNonQuery()
                End With
                System.Windows.Forms.Application.DoEvents()
                NewOcupCad = True
            End If

            Dim Da As New OleDbDataAdapter("Select * From TIncObj Where DesIncObj = '" & DescMangTemp & "'", ConnAhid)
            Dim DtTIncObj As New System.Data.DataTable
            Da.Fill(DtTIncObj)
            CodIncObj = DtTIncObj.Rows(0)("CODINCOBJ")

            If txtCodMang.Text = "" Then
                If MsgBox("Deseja realmente SALVAR as informações do novo registro ?", MsgBoxStyle.YesNo, "Salvar dados") = MsgBoxResult.No Then Return False
                With Cmd
                    .CommandText = "Insert Into TINCObjDiam(CODINCOBJ, CODVAZAO, NUMDIAM,  NUMCOMP, CODTIPO) Values(" & CodIncObj & ", " & cboTipo_M.SelectedValue & _
                    " , 0" & txtDiamMang.Text & ", 0" & txtComprMang.Text & ", " & cboTipo_E.SelectedValue & ")"
                    Dim RegAfec As Int16 = .ExecuteNonQuery
                End With
            Else
                If MsgBox("Deseja realmente ALTERAR as informações do registro ?", MsgBoxStyle.YesNo, "Alterar dados") = MsgBoxResult.No Then Return False
                With Cmd
                    .CommandText = "Update TINCObjDiam Set CODINCOBJ = " & CodIncObj & ", CODVAZAO = " & cboTipo_M.SelectedValue & _
                    ", NUMDIAM = " & txtDiamMang.Text & ", NUMCOMP = " & txtComprMang.Text & ", CODTIPO = " & cboTipo_M.SelectedValue & " Where CodIncObj = " & txtCodMang.Text
                    Dim RegAfec As Int16 = .ExecuteNonQuery
                End With
            End If

            MsgBox("Dados atualizados com sucesso !", MsgBoxStyle.Information, "Dados atualizados")
            FillGrid_Mangueira()

            If NewOcupCad Then
                Dim LibraryHidrante As New LibraryHidrante
                With LibraryHidrante
                    cboDescMang.DataSource = Nothing
                    .FillMangueira(cboDescMang)
                End With
                LibraryHidrante = Nothing
                cboDescMang.Text = DescMangTemp
            End If
            Return True

        Catch ex As Exception
            MsgBox(msgErrorIni & ex.Message, MsgBoxStyle.Information, msgErrorTit)
            Return False
        Finally
            Cmd = Nothing
        End Try

    End Function

    Function SaveVazao() As Object

        Dim Cmd As New OleDbCommand

        Try

            If txtVazao.Text = "" Then
                MsgBox("Informe uma vazão válida primeiro !", MsgBoxStyle.Information, "Campo inválido")
                txtVazao.Focus()
                Return False
            End If

            If txtPressao.Text = "" Then
                MsgBox("Informe a pressão mínima primeiro !", MsgBoxStyle.Information, "Campo inválido")
                txtPressao.Focus()
                Return False
            End If

            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
            End With

            Dim CodTipo As Integer = cboTipo_V.SelectedValue, NewReg As Boolean = False
            Dim StrCon As String = "Select * From TINCVazaoHid Where CodTipo = " & CodTipo & " And NumVazao = " & txtVazao.Text & " And NumPressaoMin = " & txtPressao.Text
            Dim Dt As New System.Data.DataTable
            Dim Da As New OleDbDataAdapter(StrCon, ConnAhid)
            Da.Fill(Dt)

            If Dt.Rows.Count <= 0 Then NewReg = True

            If NewReg Then
                If MsgBox("Deseja realmente SALVAR as informações do novo registro ?", MsgBoxStyle.YesNo, "Salvar dados") = MsgBoxResult.No Then Return False
                With Cmd
                    .CommandText = "Insert Into TINCVazaoHid(CodTipo,NumVazao, NumPressaoMin) Values(" & CodTipo & ", " & txtVazao.Text & ", " & txtPressao.Text & ")"
                    Dim RegAfec As Int16 = .ExecuteNonQuery
                End With
            Else
                Return False
            End If

            MsgBox("Dados atualizados com sucesso !", MsgBoxStyle.Information, "Dados atualizados")
            FillGrid_VazaoMinima()
            Return True

        Catch ex As Exception
            MsgBox(msgErrorIni & ex.Message, MsgBoxStyle.Information, msgErrorTit)
            Return False
        Finally
            Cmd = Nothing
        End Try

    End Function

    Function DeleteVazaoMinima() As Object

        Dim Cmd As New OleDbCommand

        Try

            If txtVazao.Text = "" Or txtPressao.Text = "" Or cboTipo_V.Text = "" Then
                MsgBox("Informe um registro válido primeiro a ser excluido !", MsgBoxStyle.Information, "Registro inválido")
                txtDescOcup.Focus()
                Return False
            End If

            If MsgBox("Deseja realmente EXCLUIR as informações do registro ?" & Chr(13) & _
                      "Atenção : a exclusão do registro não poderá ser revertida pelo usuário, se existir em outros projetos ou desenhos informações relacionadas a este registro, elas poderão ser perdidas !" & _
                      " Em caso de dúvidas, entre em contato com nosso departamento de suporte técnico !", MsgBoxStyle.YesNo, "Excluir dados") = MsgBoxResult.No Then Return False

            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
                .CommandText = "Delete From TINCVazaoHid Where CodTipo = " & cboTipo_V.SelectedValue & " And NumVazao = " & txtVazao.Text & " And NumPressaoMin = " & txtPressao.Text
                Dim RegAfec As Int16 = .ExecuteNonQuery
            End With

            txtVazao.Text = "" : txtPressao.Text = ""
            MsgBox("Dados atualizados com sucesso !", MsgBoxStyle.Information, "Dados atualizados")
            FillGrid_VazaoMinima()

            Return True

        Catch ex As Exception
            MsgBox(msgErrorIni & ex.Message, MsgBoxStyle.Information, msgErrorTit)
            Return False
        End Try

    End Function

    Function DeleteFatorC() As Object

        Dim Cmd As New OleDbCommand

        Try

            If txtCodDescFatorC.Text = "" Then
                MsgBox("Informe um registro de fator C válido primeiro a ser excluido !", MsgBoxStyle.Information, "Registro inválido")
                GridFatorC.Focus()
                Return False
            End If

            If MsgBox("Deseja realmente EXCLUIR as informações do registro ?" & Chr(13) & _
                      "Atenção : a exclusão do registro não poderá ser revertida pelo usuário, se existir em outros projetos ou desenhos informações relacionadas a este registro, elas poderão ser perdidas !" & _
                      " Em caso de dúvidas, entre em contato com nosso departamento de suporte técnico !", MsgBoxStyle.YesNo, "Excluir dados") = MsgBoxResult.No Then Return False

            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
                .CommandText = "Delete From TINCFatorC Where CodFatorC = " & txtCodDescFatorC.Text
                Dim RegAfec As Int16 = .ExecuteNonQuery
            End With

            txtCodDescFatorC.Text = "" : txtDescFator.Text = "" : txtFatorC.Text = ""

            MsgBox("Dados atualizados com sucesso !", MsgBoxStyle.Information, "Dados atualizados")
            FillGrid_FatorC()

            Return True

        Catch ex As Exception
            MsgBox(msgErrorIni & ex.Message, MsgBoxStyle.Information, msgErrorTit)
            Return False
        End Try

    End Function

    Function DeleteMangueira() As Object

        Dim Cmd As New OleDbCommand

        Try

            If txtCodMang.Text = "" Then
                MsgBox("Informe um registro de mangueira válido primeiro a ser excluido !", MsgBoxStyle.Information, "Registro inválido")
                txtDescOcup.Focus()
                Return False
            End If

            If MsgBox("Deseja realmente EXCLUIR as informações do registro ?" & Chr(13) & _
                      "Atenção : a exclusão do registro não poderá ser revertida pelo usuário, se existir em outros projetos ou desenhos informações relacionadas a este registro, elas poderão ser perdidas !" & _
                      " Em caso de dúvidas, entre em contato com nosso departamento de suporte técnico !", MsgBoxStyle.YesNo, "Excluir dados") = MsgBoxResult.No Then Return False

            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
                .CommandText = "Delete From TINCObj Where CodIncObj = " & txtCodMang.Text
                Dim RegAfec As Int16 = .ExecuteNonQuery
            End With

            txtCodMang.Text = "" : cboDescMang.Text = "" : txtDiamMang.Text = "" : txtComprMang.Text = ""
            MsgBox("Dados atualizados com sucesso !", MsgBoxStyle.Information, "Dados atualizados")
            FillGrid_Mangueira()

            Return True

        Catch ex As Exception
            MsgBox(msgErrorIni & ex.Message, MsgBoxStyle.Information, msgErrorTit)
            Return False
        End Try

    End Function

    Function DeleteEsguicho() As Object

        Dim Cmd As New OleDbCommand

        Try

            If txtCodEsg.Text = "" Then
                MsgBox("Informe um registro de esguicho válido primeiro a ser excluido !", MsgBoxStyle.Information, "Registro inválido")
                gridEsguicho.Focus()
                Return False
            End If

            If MsgBox("Deseja realmente EXCLUIR as informações do registro ?" & Chr(13) & _
                      "Atenção : a exclusão do registro não poderá ser revertida pelo usuário, se existir em outros projetos ou desenhos informações relacionadas a este registro, elas poderão ser perdidas !" & _
                      " Em caso de dúvidas, entre em contato com nosso departamento de suporte técnico !", MsgBoxStyle.YesNo, "Excluir dados") = MsgBoxResult.No Then Return False

            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
                .CommandText = "Delete FROM TINCObjDiam Where CODINCOBJDIAM = " & txtCodEsg.Text
                Dim RegAfec As Int16 = .ExecuteNonQuery
            End With

            txtCodEsg.Text = "" : cboDescEsg.Text = "" : txtDiametroEsg.Text = ""

            MsgBox("Dados atualizados com sucesso !", MsgBoxStyle.Information, "Dados atualizados")
            System.Windows.Forms.Application.DoEvents()
            FillGrid_Esguicho()

            Return True

        Catch ex As Exception
            MsgBox(msgErrorIni & ex.Message, MsgBoxStyle.Information, msgErrorTit)
            Return False
        End Try

    End Function

    Private Sub DeleteGeneral()

        Select Case tbControl.SelectedTab.Name.ToLower
            Case "tabocup".ToLower
                DeleteOcupacao()

            Case "tabfatorc".ToLower
                DeleteFatorC()

            Case "tabpressao".ToLower
                DeleteVazaoMinima()

            Case "tabEsguicho".ToLower
                DeleteEsguicho()

            Case "tabmangueira".ToLower
                DeleteMangueira()

        End Select

    End Sub

    Private Sub NewGeneral()

        Select Case tbControl.SelectedTab.Name.ToLower
            Case "tabocup".ToLower
                NewOcupacao()

            Case "tabfatorc".ToLower
                NewFatorC()

            Case "tabpressao".ToLower
                NewVazao()

            Case "tabEsguicho".ToLower
                NewEsguicho()

            Case "tabmangueira".ToLower
                newMangueira()

        End Select
    End Sub

    Private Sub SaveGeneral()

        Select Case tbControl.SelectedTab.Name.ToLower
            Case "tabocup".ToLower
                SaveOcupacao()

            Case "tabfatorc".ToLower
                SaveFatorC()

            Case "tabpressao".ToLower
                SaveVazao()

            Case "tabEsguicho".ToLower
                SaveEsguicho()

            Case "tabMangueira".ToLower
                SaveMangueira()

        End Select

    End Sub

#End Region

#Region "----- Functions general -----"

    Private Sub NewEsguicho()
        txtDiametroEsg.Text = "" : cboDescEsg.Text = "" : txtCodEsg.Text = ""
    End Sub

    Private Sub NewVazao()
        txtVazao.Text = "" : txtPressao.Text = ""
    End Sub

    Private Sub NewOcupacao()
        txtDescOcup.Text = "" : txtDivisao.Text = "" : txtCodDesc.Text = "" : txtCarga.Text = ""
    End Sub

    Private Sub NewFatorC()
        txtDescFator.Text = "" : txtCodDescFatorC.Text = "" : txtFatorC.Text = ""
    End Sub

    Private Sub NewMangueira()
        txtCodMang.Text = "" : txtDiamMang.Text = "" : txtComprMang.Text = "" : cboDescMang.Text = ""
    End Sub

#End Region

End Class
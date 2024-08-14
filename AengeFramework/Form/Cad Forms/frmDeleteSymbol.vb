Imports System.Windows.Forms
Imports System.Data.OleDb
Imports System.Drawing

Public Class frmDeleteSymbol

#Region "----- Documentation ----"

    '============================================================================================================
    'Módulo : frmDeleteSymbol
    'Empresa : Autoenge Brasil Ltda
    'Data da criação : Quarta-Feira, 18 de julho de 2012
    'Analista responsável : Raul Antonio Fernandes Junior
    'Descrição : Exclusao de objetos do banco de dados 
    '
    '
    'Id de tratamento de Erros : 
    'frmDeleteSymbol001 - FillTreeView_Objects
    'frmDeleteSymbol002 - 
    'frmDeleteSymbol003 - 
    'frmDeleteSymbol004 - 
    'frmDeleteSymbol005 - 
    'frmDeleteSymbol006 - 
    'frmDeleteSymbol007 - 
    'frmDeleteSymbol008 - 
    'frmDeleteSymbol009 - 
    'frmDeleteSymbol0010 - 
    '
    'Modificações
    '
    '============================================================================================================

#End Region

#Region "----- Atributes and Declarations -----"

    Private Shared mVar_NameClass As String = "frmDeleteSymbol"

    Dim Dtbl As System.Data.DataTable, DtDwg As New System.Data.DataTable
    Dim DtObjClas As New System.Data.DataTable

#End Region

#Region "----- Constructors -----"

#End Region

#Region "----- Functions - Events ------"

#Region "----- CommandButton -----"

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        DeleteSymbolDB()
    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        CallHelp()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Dispose(True)
    End Sub

#End Region

#Region "----- Form -----"

    Private Sub frmDeleteSymbol_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frm_DeleteSymbol = Nothing
    End Sub

    Private Sub frmDeleteSymbol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If ConnAhid Is Nothing Then
            Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
            With LibraryConnection
                .TypeDb = "AHID_"
                ConnAhid = .Aenge_OpenConnectionDB
            End With
            LibraryConnection = Nothing
        End If

        Dim Da As New OleDbDataAdapter("Select * From TObjClas Order By ClasDes Asc", ConnAhid)
        Da.Fill(DtObjClas)

        Dim LibrayComponent As New Aenge.Library.Component.LibraryComponent
        With LibrayComponent
            .FillCombo(cboClas, DtObjClas, "ClasDes", "CodClas", True)
        End With

        FillTreeView_Objects(TrvObjects, "", True)
    End Sub

#End Region

#Region "----- Treeview -----"

    Private Sub TrvObjects_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TrvObjects.AfterSelect

        lblDwgName.Text = "-"
        lblClass.Text = "-"
        lblObject.Text = "-"
        txtCodObj.Text = ""

        If e.Node.Tag.ToString.Contains("NODE0") Then lblClass.Text = "-"

        'Verifica se eh um objeto valido
        If e.Node.Tag.ToString.Contains("NODE1") Then
            lblClass.Text = e.Node.Parent.Text
            lblObject.Text = e.Node.Text
            txtCodObj.Text = e.Node.Tag.ToString.Split("|")(1).ToString

            If DtDwg.Select("CodObj = '" & txtCodObj.Text & "'").Length > 0 Then
                lblDwgName.Text = DtDwg.Select("CodObj = '" & txtCodObj.Text & "'")(0)("DwgName").ToString & ".dwg"
            End If

        End If

    End Sub

#End Region

#Region "----- ComboBox -----"

    Private Sub cboClas_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboClas.SelectedValueChanged
        FillTreeView_Objects(TrvObjects, "", True)
    End Sub

    Private Sub cboClas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboClas.KeyPress
        AutoComplete(cboClas, e, True)
    End Sub

    Private Sub cboClas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboClas.SelectedIndexChanged

    End Sub

#End Region

#End Region

#Region "----- Functions - Fill components -----"

    'Treeview para descrições e nomes de dispositivos de proteção
    Function FillTreeView_Objects(ByVal Trv As TreeView, ByVal sSql As String, Optional ByVal ClearTView As Boolean = True, Optional ByVal TypeData As String = "", Optional ByVal ArrayColumns() As String = Nothing, _
    Optional ByVal FormatoDt As String = "dd/mm/yyyy", Optional ByVal LstImg As System.Windows.Forms.ImageList = Nothing) As Boolean

        If cboClas.Text.ToString.ToLower.Contains("datarow".ToLower) Then Return False

        If ClearTView = True Then
            Trv.ResetText()
            Trv.Nodes.Clear()
        End If

        Dtbl = Nothing

        Dim Resultado As String = "Não informado", AtualizouNodeProtocolo As Boolean = False, TempDt_Nasc As String = ""
        Dim LimpaDrR As Boolean = False, Texto As String = "", Cor As New Color, Column0 As String = "", Column1 As String = ""
        Dim StrSelect As String = ""

        Try

            'Povoa com os dados do banco de dados de projetos 
            If Dtbl Is Nothing Then

                If cboClas.Text <> "" Then
                    StrSelect = "SELECT TObjBase.*, TObjClas.ClasDes FROM TObjBase, TObjClas Where TObjBase.CodClas = TObjClas.CodClas And TObjClas.CodClas = '" & cboClas.SelectedValue & "' ORDER BY ClasDes ASC, ObjLeg ASC"
                Else
                    StrSelect = "SELECT TObjBase.*, TObjClas.ClasDes FROM TObjBase, TObjClas Where TObjBase.CodClas = TObjClas.CodClas ORDER BY ClasDes ASC, ObjLeg ASC"
                End If

                Dim Da As OleDbDataAdapter = New OleDbDataAdapter(StrSelect, ConnAhid)
                Dtbl = New DataTable
                Da.Fill(Dtbl)

                DtDwg = Nothing : DtDwg = New System.Data.DataTable
                Da = Nothing : Da = New OleDbDataAdapter("Select * From TObjDwg", ConnAhid)
                Da.Fill(DtDwg)
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
                    If Column0 <> DrR("ClasDes").ToString Then
                        'Monta a treeview com a sequencia --> Column0, Column1 e Column2
                        'Nó principal - Column0 - Primeira Coluna 
                        Node0 = New TreeNode
                        Node0.Text = DrR("ClasDes").ToString
                        'Armazena somente id para a tabela de notas fiscais
                        Node0.Tag = "NODE0|CODCLAS"
                        .ShowNodeToolTips = True
                        Node0.ImageIndex = 3
                        Node0.SelectedImageIndex = 3
                        .Nodes.Add(Node0)
                        Column0 = DrR("ClasDes").ToString
                        i += 1

                        'Informações referentes ao nó com o nome do cliente 
                        'NodeProtocolo.ImageIndex = 15
                        Node1 = New TreeNode

                        With Node1
                            'Será utilizado para consultas para não precisar reconsultar no banco de dados 
                            .Tag = "NODE1|" & DrR("CodObj").ToString
                            .ToolTipText = DrR("ObjLeg").ToString & Chr(13)
                            .Text = DrR("ObjLeg").ToString
                            .ForeColor = Color.Gray
                            If IsNumeric(DrR("CodObj")) Then If Double.Parse(DrR("CodObj")) > My.Settings.CodObj_LastSymbolDefault Then .ForeColor = Color.Blue
                            '.ImageIndex = 1
                            '.SelectedImageIndex = 1
                        End With
                        Column1 = DrR("ObjLeg").ToString
                        'Neste caso adicionou o node0, node1
                        Node0.Nodes.Add(Node1)

                    Else
                        If Column1 <> DrR("ObjLeg").ToString Then
                            Node1 = New TreeNode
                            With Node1
                                'Será utilizado para consultas para não precisar reconsultar no banco de dados 
                                .Tag = "NODE1|" & DrR("CodObj").ToString
                                .ToolTipText = DrR("ObjLeg").ToString & Chr(13)
                                .Text = DrR("ObjLeg").ToString
                                .ForeColor = Color.Gray
                                If IsNumeric(DrR("CodObj")) Then If Double.Parse(DrR("CodObj")) > My.Settings.CodObj_LastSymbolDefault Then .ForeColor = Color.Blue
                                '.ImageIndex = 1
                                '.SelectedImageIndex = 1
                            End With
                            Column1 = DrR("ObjLeg").ToString
                            Node0.Nodes.Add(Node1)
                            AtualizouNodeProtocolo = True
                        End If
                    End If

                End With

            Loop

            If LimpaDrR = True Then DrR.Close()
            If cboClas.Text <> "" Then Trv.ExpandAll()
            Return True

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao povoar as informações do componente Treeview - Erro de validação e consulta de objetos cadastrados !", , _
                                          mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmDeleteSymbol001")
            Return False
        Finally

        End Try

    End Function

#End Region

#Region "----- Functions - Save, Edit and Delete -----"

    'Exclui o simbolo de todas as tabelas relacionadas no banco de dados 
    Function DeleteSymbolDB() As Object

        If txtCodObj.Text = "" Then
            MsgBox("Informe um objeto válido primeiro !", MsgBoxStyle.Information, "Objeto inválido")
            Return False
        End If

        If IsNumeric(txtCodObj.Text) Then
            If Double.Parse(txtCodObj.Text) <= 4003 Then
                MsgBox("Você está tentando excluir um objeto padrão do aplicativo. Por favor selecione apenas objetos criados pelo usuário !", MsgBoxStyle.Information, "Exclusão negada")
                Return False
            End If
        Else
            MsgBox("Informe um objeto válido primeiro !", MsgBoxStyle.Information, "Objeto inválido")
            Return False
        End If

        If txtCodObj.Text <> "" Then
            If MsgBox("Deseja realmente excluir o objeto do banco de dados ?" & Chr(13) & "Atenção : Ao excluir o objeto, todos os dados relacionados ao objeto serão excluidos. Por padrão o sistema támbem exclui o bloco criado." & _
                      " Caso queira manter o bloco relacionado ao objeto, desmarque a opção 'Excluir bloco relacionado ao objeto (DWG)'", MsgBoxStyle.YesNo, "Atenção - Exclusão") = MsgBoxResult.No Then Return False
        End If

        'Comeca a exclusao de todos os dados das tabelas relacionadas
        Dim StrDelete As String = "Delete From TObjDwg Where CodObj = '" & txtCodObj.Text & "'"
        Dim Cmd As New OleDbCommand
        With Cmd
            .Connection = ConnAhid
            .CommandType = CommandType.Text

            If chkBlock.Checked Then
                .CommandText = StrDelete
                .ExecuteNonQuery()
                'Depois excluimos o bloco relacionado ao objeto 
                With My.Computer.FileSystem
                    If .FileExists(GetAppInstall() & "Dwg\" & lblDwgName.Text) Then .DeleteFile(GetAppInstall() & "Dwg\" & lblDwgName.Text, FileIO.UIOption.AllDialogs, FileIO.RecycleOption.SendToRecycleBin)
                End With
            End If

            StrDelete = "Delete From TObjBase Where CodObj = '" & txtCodObj.Text & "'"
            .CommandText = StrDelete
            .ExecuteNonQuery()

            StrDelete = "Delete From TObj Where CodObj = '" & txtCodObj.Text & "'"
            .CommandText = StrDelete
            .ExecuteNonQuery()

            StrDelete = "Delete From TObjPVal Where CodObj = '" & txtCodObj.Text & "'"
            .CommandText = StrDelete
            .ExecuteNonQuery()

        End With

        System.Windows.Forms.Application.DoEvents()
        FillTreeView_Objects(TrvObjects, "", True)

        Return True

    End Function

#End Region

End Class
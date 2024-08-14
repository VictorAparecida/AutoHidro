Imports System.Windows.Forms
Imports Autodesk.AutoCAD.ApplicationServices
Imports System.Drawing

Public Class frmNovoDesenho

#Region "----- Documentação ----"

    '============================================================================================================
    'Módulo : frmNovoDesenho
    'Empresa : Autoenge Brasil Ltda
    'Data da criação : Quarta-Feira, 18 de julho de 2012
    'Analista responsável : Raul Antonio Fernandes Junior
    'Descrição : Novo desenho a ser configurado pela aplicação
    '
    '
    'Id de tratamento de Erros : 
    'frmNovoDesenho001 - trvProjeto_NodeMouseClick
    'frmNovoDesenho002 - SaveDwgDataBase
    'frmNovoDesenho003 - AHID_CreateFileDraw
    'frmNovoDesenho004 - AHID_SetNewFileDraw
    'frmNovoDesenho005 - AHID_CreateFolderProject
    'frmNovoDesenho006 - ValidaParametros
    'frmNovoDesenho007 - InsertNewDraw
    'frmNovoDesenho008 - 
    'frmNovoDesenho009 - 
    'frmNovoDesenho0010 - 
    '
    'Modificações
    '
    '============================================================================================================

#End Region

#Region "----- Atributos e Declarações -----"

    Dim LibraryError As LibraryError

    Private Shared mVar_NameClass As String = "frmNovoDesenho", PathDiretorio As String = "", mVar_TipoForm As String

#End Region

#Region "----- Construtores -----"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LibraryError = New LibraryError
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        LibraryError = Nothing
    End Sub

#End Region

#Region "----- Get e Set -----"

    Property TipoForm() As String
        Get
            TipoForm = mVar_TipoForm
        End Get
        Set(ByVal value As String)
            mVar_TipoForm = value
        End Set
    End Property

#End Region

#Region "----- Eventos -----"

#Region "----- Form -----"

    Private Sub frmNovoDesenho_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

    End Sub

    Private Sub frmNovoDesenho_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frm_NovoDesenho.Dispose(True)
        frm_NovoDesenho = Nothing
    End Sub

    Private Sub frmNovoDesenho_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        PathDiretorio = GetAppInstall()
        'Me.lblPath.Text = ConnAenge.ConnectionString.ToString

        ListFormato()
        ListMedida()
        ListEscala()
        'Carrega o treevie de projetos 
        FillTreeView_Projeto(trvProjeto, "", True)

        'Busca qual foi o ultimo projeto e desenho para ja carregar o treeview com as informações 
        Dim ProjTactual As String = ""
        ProjTactual = Aenge_GetCfg("AppData", "AENGEPROJ", PathDiretorio & AutoengeCfgFile)

        If ProjTactual <> "" Then
            'Busca as informações no treeviw
            With trvProjeto
                If .Nodes.Count <= 0 Then Exit Sub
                For Each No As TreeNode In .Nodes
                    'validou se o node é de projetos e expandiu se for da mesma letra 
                    If No.Text.ToString.ToLower = ProjTactual.ToLower.ToString Then
                        No.Expand()
                        'Pega os filhos dos no para busca 
                        For Each NoFilho As TreeNode In No.Nodes
                            If NoFilho.Text.ToString.ToLower = ProjTactual.ToLower Then
                                NoFilho.EnsureVisible()
                                txtProjetoSel.Text = NoFilho.Text
                                txtProjeto.Text = NoFilho.Text
                                txtQtdDesenho.Text = NoFilho.Nodes.Count
                                txtLocalizacao.Text = PathDiretorio & NoFilho.Text
                                trvProjeto.SelectedNode = NoFilho
                            End If
                        Next
                    End If
                Next
            End With

        End If

        SetCurrentNode(ProjTactual, "", trvProjeto)

        Dim LibraryComponet As New Aenge.Library.Component.LibraryComponent
        LibraryComponet.ConfigFont_Component(frm_NovoDesenho)
        LibraryComponet = Nothing

    End Sub

#End Region

#Region "----- ComboBox -----"

    Private Sub cboFormato_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboFormato.KeyPress
        AutoComplete(cboFormato, e, True)
    End Sub

    Private Sub cboFormato_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboFormato.SelectedIndexChanged

    End Sub

    Private Sub cboEscala_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboEscala.KeyPress
        AutoComplete(cboEscala, e, True)
    End Sub

    Private Sub cboEscala_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboEscala.SelectedIndexChanged

    End Sub

    Private Sub cboMedida_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboMedida.KeyPress
        AutoComplete(cboMedida, e, True)
    End Sub

    Private Sub cboMedida_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMedida.SelectedIndexChanged

    End Sub

#End Region

#Region "----- TextBox -----"

    Private Sub txtDesenho_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDesenho.KeyPress
        If e.KeyChar.ToString.Contains("\") Or e.KeyChar.ToString.Contains("/") Then e.KeyChar = Nothing : Exit Sub
        e.KeyChar = e.KeyChar.ToString.ToUpper
        Trata_Keypress(e)
    End Sub

    Private Sub txtDesenho_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDesenho.TextChanged

    End Sub

    Private Sub txtProjeto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtProjeto.KeyPress
        If e.KeyChar.ToString.Contains("\") Or e.KeyChar.ToString.Contains("/") Then e.KeyChar = Nothing : Exit Sub
        e.KeyChar = e.KeyChar.ToString.ToUpper
        Trata_Keypress(e)
    End Sub

    Private Sub txtProjeto_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtProjeto.KeyUp

    End Sub

    Private Sub txtProjeto_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProjeto.Leave
        If txtProjeto.Text = "" Then
            txtProjetoSel.Text = "-"
        Else
            txtProjetoSel.Text = txtProjeto.Text
        End If
    End Sub

    Private Sub txtProjeto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtProjeto.TextChanged
        txtQtdDesenho.Text = "0" : txtProjetoSel.Text = "-"
        If txtProjeto.Text = "" Then Exit Sub
        txtLocalizacao.Text = PathDiretorio & txtProjeto.Text.ToString
        txtProjetoSel.Text = txtProjeto.Text

        For Each NodeT As TreeNode In trvProjeto.Nodes
            If NodeT.Tag.ToString.Contains("NODE0") Then
                If NodeT.Text = txtProjeto.Text Then txtQtdDesenho.Text = NodeT.GetNodeCount(True)
            End If
        Next
    End Sub

#End Region

#Region "----- CommandButton -----"

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        CallHelp()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If InsertNewDraw() = False Then Exit Sub 'Not close in case false return
        IsSaveImgThumbnail = True
        giReturnDoModal_Class = -1
        Me.Dispose(True)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If giReturnDoModal_Class = 98 Then Main("inicial")
        IsSaveImgThumbnail = True
        Me.Dispose(True)
    End Sub

#End Region

#Region "----- Treeview -----"

    Private Sub trvProjeto_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvProjeto.AfterSelect
        For Each NodeT1 As TreeNode In trvProjeto.Nodes
            NodeT1.ForeColor = Color.Black
            For Each NodeT2 As TreeNode In NodeT1.Nodes
                NodeT2.ForeColor = Color.Black
            Next
        Next

        e.Node.ForeColor = Color.Red
        If e.Node.Tag.ToString.Contains("NODE1") Then e.Node.Parent.ForeColor = Color.Red
    End Sub

    Private Sub trvProjeto_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvProjeto.NodeMouseClick

        Dim NodeAnterior As Object = Nothing
        txtProjetoSel.Text = "-" : txtQtdDesenho.Text = "0"

        Try

            Dim Codigo As Object = Nothing, Tabela As String = ""

            'Seleciona os dados dom protocolo 
            With e

                Select Case Mid(e.Node.Tag, 1, 6).ToString
                    'Configura o grid destacando as linhas do protocolo que foi selecionado
                    Case "NODE0|"
                        'txtProjeto.Text = ""
                        'txtLocalizacao.Text = ""
                        Tabela = e.Node.Tag.ToString.Replace("NODE0|", "")
                        txtProjeto.Text = e.Node.Text.ToString
                        txtProjetoSel.Text = txtProjeto.Text
                        txtQtdDesenho.Text = e.Node.GetNodeCount(True)

                        'Caso tenha selecionado um registro. Neste caso aparece a lista com os registros e a cada doubleclick carrega uma funcionalidade
                        'Case "NODE1|"
                        '    Tabela = e.Node.Parent.Tag.ToString.Replace("NODE0|", "")
                        '    txtProjeto.Text = e.Node.Tag.ToString.Replace("NODE1|", "")
                        '    txtProjetoSel.Text = txtProjeto.Text
                        '    txtQtdDesenho.Text = e.Node.GetNodeCount(True)

                    Case "NODE1|"
                        txtProjeto.Text = e.Node.Parent.Text
                        txtProjetoSel.Text = txtProjeto.Text
                        txtQtdDesenho.Text = e.Node.Parent.GetNodeCount(True)

                End Select

            End With

            txtLocalizacao.Text = PathDiretorio & txtProjeto.Text.ToString

            'Obtem o nó selecionado para que após uma reconsulta, o sistema seta o nó que estava anteriormente sendo utilizado 
            'NodeAnterior = e.Node.FullPath  'e.Node

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao selecionar o registro da lista de informações gerais do projeto e dos circuitos - TrvSelect !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmNovoDesenho001")
        End Try


    End Sub

    Private Sub trvProjeto_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvProjeto.NodeMouseDoubleClick

    End Sub

#End Region

#End Region

#Region "----- Funcionalidades de consultas e fill de componentes -----"

    'Função que retorna o respectivo nome do selo selecionado pelo usuário, baseando-se no
    'listIndex do combo e relacionado com os respectivos arquivos encontrados na pasta
    '...\AHID\DWG
    Function ReturnNameSelo() As String
        On Error Resume Next
        Select Case cboFormato.SelectedIndex
            Case 0
                ReturnNameSelo = "AHID_SeloA0"

            Case 1
                ReturnNameSelo = "AHID_SeloA1"

            Case 2
                ReturnNameSelo = "AHID_SeloA2"

            Case 3
                ReturnNameSelo = "AHID_SeloA3"

            Case 4
                ReturnNameSelo = "AHID_SeloA4"

            Case 5
                ReturnNameSelo = "AHID_SeloA4V"

                'Para o caso de assumir um valor padrão
            Case Else
                ReturnNameSelo = "AHID_SeloA4V"

        End Select
    End Function

    'Função que retorna a unidade de medida desejada pelo usuário
    Function ReturnUniMedida() As String
        On Error Resume Next
        Select Case cboMedida.SelectedIndex
            Case 0
                ReturnUniMedida = "M"

            Case 1
                ReturnUniMedida = "CM"

            Case 2
                ReturnUniMedida = "MM"

                'Para o caso de assumir um valor padrão
            Case Else
                ReturnUniMedida = "M"

        End Select
    End Function

    'Formatos
    Function ListFormato() As Boolean
        With cboFormato.Items
            .Add("Selo A0")
            .Add("Selo A1")
            .Add("Selo A2")
            .Add("Selo A3")
            .Add("Selo A4")
            cboFormato.SelectedIndex = 0
        End With
        Return True
    End Function

    Private Sub ListEscala()
        With cboEscala.Items
            .Add("1")
            .Add("10")
            .Add("25")
            .Add("50")
            .Add("75")
            .Add("100")
            .Add("125")
            .Add("150")
            .Add("200")
            .Add("250")
            .Add("500")
            .Add("1000")
            .Add("2000")
            .Add("5000")
            cboEscala.SelectedIndex = 3
        End With
    End Sub

    Private Sub ListMedida()
        With cboMedida.Items
            .Add("Metro (m)")
            .Add("Centímetro (cm)")
            .Add("Milímetro (mm)")
            cboMedida.SelectedIndex = 0
        End With
    End Sub

#End Region

#Region "---- Funcionalidades de Inserção e Novo desenho -----"

    'Inserção de selos e de outras inforações de criação do novo desenho
    Private Sub InsertSelo()

        Dim DocCurrent As Document
        Dim AppPath As String, TactualDwg As String = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Name
        Dim ValorUnidade As Double

        Try

            'Seta como o inicial single mode
            '.Preferences.System.SingleDocumentMode = False
            'Fecha todas as janelas que estiverem abertas]
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("SDI", 0)

            ''Verifica se quer salvar os dados do desenho atual 
            'If DirectCast(DirectCast(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument, Autodesk.AutoCAD.ApplicationServices.Document).Database, Autodesk.AutoCAD.DatabaseServices.Database).NumberOfSaves <= 0 Then
            '    Autodesk.AutoCAD.ApplicationServices.DocumentExtension.CloseAndDiscard(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument) ''(TactualDwg)
            'Else
            '    Autodesk.AutoCAD.ApplicationServices.DocumentExtension.CloseAndSave(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument, TactualDwg)
            'End If

            'Adiciona um novo documento
            DocCurrent = Autodesk.AutoCAD.ApplicationServices.DocumentCollectionExtension.Add(Nothing, My.Settings.AcadTemplate)
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument = DocCurrent

            Using DocCurrent.LockDocument

                'In case of acadtemplate is inchees
                DocCurrent.Database.Insunits = Autodesk.AutoCAD.DatabaseServices.UnitsValue.Millimeters

                AppPath = Replace(GetAppInstall, "\", "\\")
                Select Case cboMedida.Text
                    Case "Metro (m)"
                        ValorUnidade = 0.001

                    Case "Centímetro (cm)"
                        ValorUnidade = 0.1

                    Case "Milímetro (mm)"
                        ValorUnidade = 1

                    Case Else
                        ValorUnidade = 0

                End Select

                Dim sStrInsercaoSelo As String, TipoSelo As String = ""

                Select Case cboFormato.Text
                    Case "Selo A0"
                        TipoSelo = "A0"

                    Case "Selo A1"
                        TipoSelo = "A1"

                    Case "Selo A2"
                        TipoSelo = "A2"

                    Case "Selo A3"
                        TipoSelo = "A3"

                    Case "Selo A4"
                        TipoSelo = "A4"

                End Select

                Dim Cam As String, ocmd As String
                sStrInsercaoSelo = "(INSERE_SELO " & Chr(34) & TipoSelo & Chr(34) & ")"
                Cam = (GetAppInstall() & txtProjetoSel.Text & "\" & txtDesenho.Text & ".dwg").ToString.Replace("\", "\\")
                Dim CommandExecute As String = ""
                CommandExecute = "(SetVar " & Chr(34) & "USERS1" & Chr(34) & " " & Chr(34) & txtProjetoSel.Text & Chr(34) & ")"
                mVar_NameProjTactual = txtProjetoSel.Text
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(CommandExecute & " ", True, False, False)

                CommandExecute = "(SetVar " & Chr(34) & "USERS2" & Chr(34) & " " & Chr(34) & PathDiretorio.Replace("\", "\\") & Chr(34) & ")"
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(CommandExecute & " ", True, False, False)

                CommandExecute = "(SetVar " & Chr(34) & "USERS3" & Chr(34) & " " & Chr(34) & "1000" & Chr(34) & ")"
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(CommandExecute & " ", True, False, False)

                CommandExecute = "(SetVar " & Chr(34) & "USERR3" & Chr(34) & " " & CDbl(cboEscala.Text) & ")"
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(CommandExecute & " ", True, False, False)

                CommandExecute = "(SetVar " & Chr(34) & "USERR2" & Chr(34) & " " & ValorUnidade.ToString.Replace(",", ".") & ")".Replace(",", ".")
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(CommandExecute & " ", True, False, False)
                ocmd = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("CMDECHO").ToString()
                'Após setar as users, salva para ter certeza
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute("(setvar ""CMDECHO"" 0)" & "(command ""_.SAVEAS"" """" """ & Cam & """)" & "(setvar ""CMDECHO"" " & ocmd.ToString() & ")" & "(princ) ", False, False, False)

                'Carrega o lisp com as configurações
                Dim PathLisp As String, EstiloMenu As String
                PathLisp = "(LOAD " & Chr(34) & Replace(GetAppInstall, "\", "\\") & "AHID.vlx" & Chr(34) & ")" & Chr(13)

                'Verifica qual o tipo de menu a ser carregado
                EstiloMenu = Aenge_GetCfg("GENERAL", "MENU", GetAppInstall() & "autoenge.cfg")
                If EstiloMenu = "" Then EstiloMenu = "0"
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS5", EstiloMenu)

                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(PathLisp, True, False, False)
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute("HIDInicial" & Chr(13), True, False, False)
                'Após carregar os arquivos, roda o comando para inserção de símbolos
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(sStrInsercaoSelo & Chr(13), True, False, False)
                'Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("SDI", 1)
                ValidaParametros()
            End Using

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro de inserção de selo.", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "")
        End Try

    End Sub

    Function InsertNewDraw() As Object

        Try

            'Valida os campos antes de inserir o novo desenho no projeto selecionado 
            If ValidateFields() = False Then Return False

            'If exists this name of dwg, then bloked creation
            Dim SqlStr As String = "SELECT * FROM AENGEDES WHERE PRJDWGNOME = '" & txtDesenho.Text & "' AND PrjTipo = '" & My.Settings.FolderApp_Name.ToString & "' AND PrjNome = '" & txtProjetoSel.Text & "'"
            Dim Da As New OleDb.OleDbDataAdapter(SqlStr, ConnAenge)
            Dim Dt As New System.Data.DataTable
            Da.Fill(Dt)
            If Dt.Rows.Count > 0 Then
                MsgBox("Desenho já cadastrado no banco de dados. Escolha outro nome por favor !", MsgBoxStyle.Information, "Desenho já existente")
                txtDesenho.Focus()
                Return False
            End If

            'Para não ficar visivel o formulário enquanto carrega as informações no cad
            Me.Hide()

            'If AppCad Is Nothing Then AppCad = Load_AcadApp()
            Dim Doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("SDI", 0)

            'Hide na tela para que efetue as operações em paralelo
            Me.Hide()
            SaveDwgDataBase()
            'Cria e valida as pastas do novo projeto 
            AHID_CreateFolderProject()
            'O insertselo se encontrava após a função AHID_CREATEFOLDERPROJECT
            SetArquivoCFG()

            'Atualiza as informações do projeto atual     'Seta o id do projeto no cfg geral do sistema
            Dim bRet As Boolean
            bRet = Aenge_SetCfg("AppData", "AENGEPROJID", IDProject(txtProjetoSel.Text), GetAppInstall() & "Autoenge.cfg")
            'Insere o selo e os parâmetros a serem utilizados no CAD
            InsertSelo()

            'Salva o Id do projeto em questão
            SetIdTactual(txtProjetoSel.Text) 'Combo1.Text

            Me.Hide()
            Initialize()

            'Verifica se o projeto possui algum desenho cadastrado. Se não possuir, chama a configuração de potencias e conscer.
            If Not ConnAenge Is Nothing Then
                If ConnAenge.State <> ConnectionState.Open Then ConnAenge.Open()
                Dim RsValida As OleDb.OleDbDataAdapter, DtValida As New DataTable
                RsValida = New OleDb.OleDbDataAdapter("SELECT * FROM AengeDes WHERE PrjNome = '" & txtProjetoSel.Text & "'", ConnAenge)
                RsValida.Fill(DtValida)
                RsValida = Nothing
            End If

            'Sincroniza os bancos de dados accdb e mdb 97 - Neste caso não é necessário se o banco de dados estiver sendo atualizado normalmente
            'SincDatabase(List1.Text)

            Return True

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error InsertNewDraw - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmNovoDesenho007")
            Return False
        Finally
            IsSaveImgThumbnail = True
        End Try

    End Function

    'Salva as informações no banco de dados e valida a pasta a ser criada de acordo com o projeto selecionado pelo usuário
    Private Sub SaveDwgDataBase()

        Dim AppDWG As String, Cmd, CmdProj As New OleDb.OleDbCommand, Regafect As Integer, ProxId As Int16 = 1
        AppDWG = GetAppInstall() & txtProjetoSel.Text & "\" & txtDesenho.Text & ".DWG"

        If ConnAenge Is Nothing Then Exit Sub
        If ConnAenge.State <> ConnectionState.Open Then ConnAenge.Open()

        Try

            'Salva o novo projeto com seu respectivo Id 
            Dim Da_AengeDef As New OleDb.OleDbDataAdapter("SELECT MAX(Id) AS Id FROM AengeDef", ConnAenge)
            Dim Dt_AengeDef As New System.Data.DataTable
            Da_AengeDef.Fill(Dt_AengeDef)

            If Dt_AengeDef.Rows.Count > 0 Then If Dt_AengeDef.Rows(0)("Id").ToString.Trim <> "" Then ProxId = Int16.Parse(Dt_AengeDef.Rows(0)("Id")) + 1
            'Verificamos se ja existe o projeto no aengedef, se nao existir teremos que cadastra-lo
            Da_AengeDef = Nothing
            Da_AengeDef = New OleDb.OleDbDataAdapter("SELECT * FROM AengeDef WHERE PrjNome = '" & txtProjetoSel.Text & "'", ConnAenge)
            Dt_AengeDef = Nothing : Dt_AengeDef = New System.Data.DataTable
            Da_AengeDef.Fill(Dt_AengeDef)

            If Dt_AengeDef.Rows.Count <= 0 Then
                With CmdProj
                    .Connection = ConnAenge
                    .CommandType = CommandType.Text
                    .CommandText = "INSERT INTO AENGEDEF(PRJTIPO, PRJNOME, ID, VERSION, CLOSED)" & _
                    " VALUES('" & My.Settings.FolderApp_Name.ToString & "','" & txtProjetoSel.Text & "'," & ProxId & ",'1.0',0)"
                    Regafect = .ExecuteNonQuery
                End With
            End If

            With Cmd
                .Connection = ConnAenge
                .CommandType = CommandType.Text
                .CommandText = "INSERT INTO AENGEDES(PRJNOME, PRJTIPO, PRJDWG, PRJDWGNOME)" & _
                " VALUES('" & txtProjetoSel.Text & "','AHID','" & AppDWG & "','" & txtDesenho.Text & "')"
                Regafect = .ExecuteNonQuery
            End With

            If AHID_CreateFileDraw() = True Then
                AHID_SetNewFileDraw()
            Else
                MsgBox("Não foi possivel criar o arquivo de paramêtros !", vbCritical, "Erro")
            End If

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao salvar as informações do projeto no banco de dados !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmNovoDesenho002")
        End Try

    End Sub

#End Region

#Region "----- Funcionalidades de tratamento de diretorios e arquivos -----"

    'Valida as pastas e arquivos a serem copiados
    Function ValidaParametros() As Boolean

        'CAminho aonde a aplicação está configurada
        Dim UserS2 As String, UserS1 As String, path As String

        Try

            UserS1 = txtProjetoSel.Text
            UserS2 = GetAppInstall() & UserS1 & "\"
            path = GetAppInstall()

            With My.Computer.FileSystem
                'Caso não exista a pasta, cria uma nova
                If Not .DirectoryExists(UserS2) Then .CreateDirectory(UserS2)
            End With

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao salvar as informações do projeto - Validação de parâmetros !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmNovoDesenho006")
            Return False
        End Try

        Return True

    End Function

    'Função que faz a copia do arquivo que será necessário para as configurações de um deter-
    'minado projeto e de um novo desenho quando este é criado (Novo projeto por exemplo)
    Function AHID_CreateFolderProject()

        Dim DirInstall As String, DirNewProject As String
        Dim FileProject As String, CfgDesenho As String, CfgProjetoDesenho As String

        Try

            DirInstall = GetAppInstall()
            FileProject = DirInstall & "Projeto.cfg"
            CfgProjetoDesenho = DirInstall & txtProjetoSel.Text & "\Projeto.cfg"
            CfgDesenho = DirInstall & txtProjetoSel.Text & "\" & txtDesenho.Text & ".cfg"

            With My.Computer.FileSystem

                If Not .DirectoryExists(DirInstall & txtProjetoSel.Text) Then
                    .CreateDirectory(DirInstall & txtProjetoSel.Text)
                    '.CreateFolder DirInstall & List1.Text & "\" & "AHID"
                    DirNewProject = DirInstall & txtProjetoSel.Text
                    'DirNewProjectAHID = DirInstall & List1.Text & "\" & "AHID"
                    'Copia os arquivos para a pasta
                End If

                If Not .FileExists(CfgProjetoDesenho) Then .CopyFile(FileProject, CfgProjetoDesenho, True)
                'If .FileExists(FileProject) Then .CopyFile(FileProject, CfgDesenho, True)

            End With

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao salvar as informações do projeto - criação de pastas e arquivos !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmNovoDesenho005")
        End Try

        Return True

    End Function

    'Passa os novos parametros para os arquivos CFG dos desenhos e dos projetos
    Private Sub SetArquivoCFG()
        On Error GoTo Erro
        Dim ApCaminho As String, ApCaminhoCFG As String, NameSelo As String
        Dim UniMedida As String, EscalaSelo As String
        Dim bRet As Boolean

        ApCaminho = GetAppInstall()

        bRet = Aenge_SetCfg("AppData", "AENGEPROJ", txtProjetoSel.Text, ApCaminho & "Autoenge.cfg")
        bRet = Aenge_SetCfg("AppData", "AENGEDWG", txtDesenho.Text, ApCaminho & "Autoenge.cfg")
        'bRet = AENGE_bSetCfg("AppData", "AENGEPROJID", s, sProjectDir & "\" & gsProjetoCFG)

        'Para os dados do desenho a ser inserido
        ApCaminhoCFG = ApCaminho & txtProjetoSel.Text & "\" & txtDesenho.Text & ".cfg" 'AppCFGDraw
        NameSelo = ReturnNameSelo()
        UniMedida = ReturnUniMedida()
        EscalaSelo = cboEscala.Text

        bRet = Aenge_SetCfg("DRAWING", "SELO", NameSelo, ApCaminhoCFG)
        'Para o selo do Autoenge.cfg
        bRet = Aenge_SetCfg("DRAWING", "SELO", NameSelo, ApCaminho & "Autoenge.cfg")

        bRet = Aenge_SetCfg("DRAWING", "UNIDADE_MEDIDA", UniMedida, ApCaminhoCFG)
        bRet = Aenge_SetCfg("DRAWING", "ESCALA", EscalaSelo, ApCaminhoCFG)
        'bRet = AENGE_bSetCfg("DRAWING", "AREA_TRABALHO", Combo1.Text, ApCaminhoCFG)

        Exit Sub
Erro:
        MsgBox("Ocorrência de erro : " & Err.Number & " - " & Err.Description, vbInformation, "Erro")
        Resume Next
    End Sub

    'Função que cria um novo desenho,considerando-se que já exista o projeto no qual o
    'mesmo irá ser criado.No caso desta função será passado como paramêtros :
    '   DirFileDraw    ==> Diretório aonde será salvo o novo arquivo cfg do desenho que
    '                      está sendo criado.Caso o mesmo já exista, não haverá a necessidade
    '                      de se criar um novo arquivo.
    '   DirFileProject ==> Diretório aonde se encontra o arquivo padrão Projeto.cfg, que será
    '                      copiado para a pasta do novo desenho para que se possa setar os va-
    '                      lores para o projeto e desenho.
    '
    'Neste caso, a função retornará um valor Booleano para que se possa ter a confirmação de
    'de que o arquivo foi criado com sucesso
    Function AHID_CreateFileDraw(Optional ByVal DirFileProject As String = "") As Boolean
        Dim PathFolder As String
        Dim DirNewDraw As String, DirInstall As String, DirFileDraw As String

        Try

            DirInstall = GetAppInstall()                                                              'DiretorioAENGE 'AENGE_sGetInstallationDirectory()
            DirFileDraw = DirInstall & "Projeto.cfg"          'DirInstall & "AHID\" & "Projeto.cfg"
            PathFolder = DirInstall & txtProjeto.Text
            DirNewDraw = DirInstall & txtProjeto.Text & "\" & txtDesenho.Text & ".cfg"   'DirInstall & List1.Text & "\AHID\" & Text2.Text

            With My.Computer.FileSystem
                'Valida a pasta antes da cópia
                If Not .DirectoryExists(PathFolder) Then .CreateDirectory(PathFolder)

                If .FileExists(DirFileDraw) Then
                    .CopyFile(DirFileDraw, DirNewDraw, True)
                Else
                    'CreateFileProject(DirInstall)
                    MsgBox("Arquivo de configuração CFG da aplicação não encontrado. Ele pode estar corrompido ou não acessível. Caso persista esta mensagem, entre em contato com nosso suporte técnico !", MsgBoxStyle.Information, "Aenge Error")
                    Return False
                End If
            End With

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao criar as validações do desenho !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmNovoDesenho003")
        End Try

        Return True

    End Function

    'Função que seta os valores do cfg criado para o caso de um novo desenho.Passa-se como pa-
    'ramêtro neste caso os dados de:
    '   AppFileDraw ==> Caminho de onde se encontra o arquivo cfg a ser setado.No caso desta
    '                   função, será inicialmente utilizado o caminho já informado pelo aplic.
    '                   no label7.
    Function AHID_SetNewFileDraw(Optional ByVal AppFileDraw As String = "")

        Dim bRet As Long, AppFile As String, SeloName As String, Medida As String, Escala As String
        Dim DirInstall As String = GetAppInstall()

        Try

            AppFile = DirInstall & txtProjetoSel.Text & "\" & txtDesenho.Text & ".cfg"

            bRet = Aenge_SetCfg("GENERAL", "APPNAME", "AHID", AppFile)
            bRet = Aenge_SetCfg("GENERAL", "PROJETO", txtProjetoSel.Text, AppFile)
            bRet = Aenge_SetCfg("GENERAL", "DWGNAME", txtDesenho.Text, AppFile)

            SeloName = ReturnNameSelo()
            Medida = ReturnUniMedida()

            If cboEscala.Text = "" Then
                Escala = "50"
            Else
                Escala = cboEscala.Text
            End If

            bRet = Aenge_SetCfg("DRAWING", "SELO", SeloName, AppFile)
            bRet = Aenge_SetCfg("DRAWING", "ESCALA", Escala, AppFile)
            bRet = Aenge_SetCfg("DRAWING", "UNIDADE_MEDIDA", Medida, AppFile)

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao criar as validações do desenho - SetValues Cfg !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmNovoDesenho004")
            Return False
        End Try

        Return True

    End Function

#End Region

#Region "----- Funcionalidades gerais da aplicação -----"

    'Valida os campos a serem inseridos 
    Function ValidateFields() As Boolean

        If txtProjeto.Text = "" Then
            MsgBox("Informe o nome do projeto primeiro !", MsgBoxStyle.Information, "Campo inválido")
            txtProjeto.Focus()
            Return False
        End If

        If txtDesenho.Text = "" Then
            MsgBox("Informe o nome do desenho primeiro !", MsgBoxStyle.Information, "Campo inválido")
            txtDesenho.Focus()
            Return False
        End If

        If cboFormato.Text = "" Then
            MsgBox("Informe o formato primeiro !", MsgBoxStyle.Information, "Campo inválido")
            cboFormato.Focus()
            Return False
        End If

        If cboEscala.Text = "" Then
            MsgBox("Informe a escala primeiro !", MsgBoxStyle.Information, "Campo inválido")
            cboEscala.Focus()
            Return False
        End If

        If cboMedida.Text = "" Then
            MsgBox("Informe a medida primeiro !", MsgBoxStyle.Information, "Campo inválido")
            cboMedida.Focus()
            Return False
        End If

        Return True

    End Function

#End Region

End Class
Imports System.Drawing
Imports System.Windows.Forms
Imports Autodesk.AutoCAD.Interop
Imports Autodesk.AutoCAD.ApplicationServices
Imports System.IO

Public Class frmExisteDesenho

#Region "----- Documentação ----"

    '============================================================================================================
    'Módulo : frmExisteDesenho
    'Empresa : Autoenge Brasil Ltda
    'Data da criação : Quarta-Feira, 18 de julho de 2012
    'Analista responsável : Raul Antonio Fernandes Junior
    'Descrição : Novo desenho a ser configurado pela aplicação
    '
    '
    'Id de tratamento de Erros : 
    'frmExisteDesenho001 - trvProjeto_NodeMouseClick
    'frmExisteDesenho002 - LoadImg_Dwg
    'frmExisteDesenho003 - ValidateOpen_Dwg
    'frmExisteDesenho004 - OpenDWG
    'frmExisteDesenho005 - SetArquivoCFGDados
    'frmExisteDesenho006 - 
    'frmExisteDesenho007 - 
    'frmExisteDesenho008 - 
    'frmExisteDesenho009 - 
    'frmExisteDesenho0010 - 
    '
    'Modificações
    '
    '============================================================================================================

#End Region

#Region "----- Construtores -----"

    Public Sub New()

        'IdAction pega a acao desejada pelo usuario e retorna a desejada pelo usuario

        ' This call is required by the designer.
        InitializeComponent()
        LibraryError = New LibraryError
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Protected Overrides Sub Finalize()
        LibraryError = Nothing
        MyBase.Finalize()
    End Sub

#End Region

#Region "----- Atributos e Declarações -----"

    Dim LibraryError As LibraryError ', AcThumbnailReader As acThumbnailReade
    Private Shared PathDiretorio As String = GetAppInstall(), mVar_NameClass As String = "frmExisteDesenho", PathNoDwg As String, SearchInfo As Boolean = False

#End Region

#Region "----- Eventos -----"

#Region "----- CommandButton -----"

    Private Sub btnConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfig.Click
        frm_ConfigAbert = New frmConfigAbert
        frm_ConfigAbert.TopMost = True
        frm_ConfigAbert.ShowDialog()
        frm_ConfigAbert = Nothing
    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        CallHelp()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        'giReturnDoModal_Class = 20
        ''povoamos o arrayinfo_project de acordo com as informacoes selecionadas pelo usuario 
        'ArrayInfo_Project.Add(txtProjetoSel.Text)
        'ArrayInfo_Project.Add(txtDesenhoSel.Text)
        'ArrayInfo_Project.Add(GetAppInstall() & txtProjetoSel.Text & "\" & txtDesenhoSel.Text & ".dwg")
        'Me.Dispose(True)

        'Anteriormente este funcao fazia toda a validacao internamente neste form 
        ValidateOpen_Dwg()
        giReturnDoModal_Class = -1

        'Valida se existe agendamentos e tarefas para o projeto em questao
        Dim LibraryVBA As New LibraryVBA
        LibraryVBA.Load_AhidAgendamento(Nothing)
        LibraryVBA = Nothing
        'Fecha o formulário após todas as informações terem sido carregadas 
        Me.Dispose(True)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If giReturnDoModal_Class = 98 Then Main("inicial")
        Me.Dispose(True)
    End Sub

#End Region

#Region "----- Treeview -----"

    Private Sub trvProjeto_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TrvProjeto.AfterSelect

        For Each NodeT1 As TreeNode In TrvProjeto.Nodes
            NodeT1.ForeColor = Color.Black
            For Each NodeT2 As TreeNode In NodeT1.Nodes
                NodeT2.ForeColor = Color.Black
            Next
        Next

        e.Node.ForeColor = Color.Red
        If e.Node.Tag.ToString.Contains("NODE1") Then e.Node.Parent.ForeColor = Color.Red

    End Sub

    Private Sub trvProjeto_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TrvProjeto.NodeMouseClick

        Dim NodeAnterior As Object = Nothing
        Dim FileImg As String, PathProject As String, FileImg_NoPhoto As String = ""
        txtProjetoSel.Text = "-" : txtQtdDesenho.Text = "0" : txtDesenhoSel.Text = "-"
        btnOk.Enabled = True
        lblInfo.Visible = False : lblInfo.Refresh()
        PicNoImg.Visible = False : PicNoImg.Refresh() : Dwg.Image = Nothing

        Try

            Dim Codigo As Object = Nothing, Tabela As String = ""

            'Seleciona os dados dom protocolo 
            With e

                Select Case Mid(e.Node.Tag, 1, 6).ToString
                    'Configura o grid destacando as linhas do protocolo que foi selecionado
                    Case "NODE0|"
                        'txtProjetoSel.Text = "-" : txtDesenhoSel.Text = "-"
                        'txtLocalizacao.Text = "-"
                        Tabela = e.Node.Tag.ToString.Replace("NODE0|", "")
                        txtProjetoSel.Text = e.Node.Text.ToString.Replace("NODE1|", "")
                        txtQtdDesenho.Text = e.Node.GetNodeCount(True)
                        txtDesenhoSel.Text = "-"
                        txtLocalizacao.Text = PathDiretorio & txtProjetoSel.Text.ToString

                        lblInfo.BackColor = Color.Gray
                        lblInfo.Visible = True : lblInfo.Refresh()
                        lblInfo.Text = "Selecione um desenho do projeto !" : lblInfo.Refresh()
                        PicNoImg.Visible = True : PicNoImg.Refresh()
                        btnOk.Enabled = False
                        Exit Sub

                    Case "NODE1|"
                        txtProjetoSel.Text = e.Node.Parent.Text
                        txtDesenhoSel.Text = e.Node.Text
                        txtQtdDesenho.Text = e.Node.Parent.GetNodeCount(True)
                        txtLocalizacao.Text = PathDiretorio & txtProjetoSel.Text.ToString & "\" & txtDesenhoSel.Text & ".dwg"

                End Select

            End With

            PathProject = PathDiretorio & txtProjetoSel.Text.ToString & "\"
            FileImg = PathProject & txtDesenhoSel.Text & My.Settings.ExtensionCompl_Img & My.Settings.Extension_Img
            FileImg_NoPhoto = PathDiretorio & "NoDwg" & My.Settings.Extension_Img

            'Valida se o desenho é valido
            If My.Computer.FileSystem.FileExists(txtLocalizacao.Text) Then
                If My.Computer.FileSystem.FileExists(FileImg) Then
                    Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent
                    Dim FileBmp As Bitmap
                    FileBmp = LibraryComponent.LoadImage_InPictureBox(FileImg)
                    Dwg.Image = FileBmp
                Else
                    lblInfo.BackColor = Color.Gray : lblInfo.ForeColor = Color.White
                    lblInfo.Visible = True
                    lblInfo.Text = My.Settings.frmExisteDesenho_MsgNoPreview : lblInfo.Refresh()
                    PicNoImg.Visible = True : PicNoImg.Refresh()
                End If
            Else
                lblInfo.BackColor = Color.Maroon
                lblInfo.Visible = True : lblInfo.Refresh()
                lblInfo.Text = "Desenho não encontrado ou removido !" : lblInfo.Refresh()
                PicNoImg.Visible = True : PicNoImg.Refresh()
                btnOk.Enabled = False
            End If

            'Obtem o nó selecionado para que após uma reconsulta, o sistema seta o nó que estava anteriormente sendo utilizado 
            'NodeAnterior = e.Node.FullPath  'e.Node

        Catch ex As Exception
            'LibraryError.CreateErrorAenge(Err, "Erro ao selecionar o registro da lista de informações gerais do projeto e dos circuitos - TrvSelect !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmExisteDesenho001")
            lblInfo.BackColor = Color.Gray : lblInfo.ForeColor = Color.White
            lblInfo.Visible = True
            lblInfo.Text = "Formato inválido. Visualização não disponível !" : lblInfo.Refresh()
            PicNoImg.Visible = True : PicNoImg.Refresh()
        End Try

    End Sub

    Private Sub trvProjeto_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TrvProjeto.NodeMouseDoubleClick

    End Sub

#End Region

#Region "----- Form -----"

    Private Sub frmExisteDesenho_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Disposed
        IsSaveImgThumbnail = True
        frm_ExisteDesenho.Dispose(True)
        frm_ExisteDesenho = Nothing
    End Sub

    Private Sub frmExisteDesenho_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim FileImg As String = ""
        IsSaveImgThumbnail = False

        Try

            PathNoDwg = PathDiretorio & "NoDwg" & My.Settings.Extension_Img

            'Carrega o treevie de projetos 
            FillTreeView_Projeto(TrvProjeto, "", True)
            'Valida a consulta de novas informações sobre desenhos e outros 
            SearchInfo = True

            'Busca qual foi o ultimo projeto e desenho para ja carregar o treeview com as informações 
            Dim ProjTactual As String = "", DwgTactual As String
            ProjTactual = Aenge_GetCfg("AppData", "AENGEPROJ", PathDiretorio & AutoengeCfgFile)
            DwgTactual = Aenge_GetCfg("AppData", "AENGEDWG", PathDiretorio & AutoengeCfgFile)

            If ProjTactual <> "" Then
                'Busca as informações no treeviw
                With TrvProjeto
                    If .Nodes.Count <= 0 Then Exit Sub
                    For Each No As TreeNode In .Nodes
                        'validou se o node é de projetos e expandiu se for da mesma letra 
                        If No.Text.ToString.ToLower = ProjTactual.ToLower.ToString Then
                            No.Expand()
                            'Pega os filhos dos no para busca 
                            'Informações sobre o projeto
                            txtProjetoSel.Text = No.Text
                            txtQtdDesenho.Text = No.Nodes.Count

                            For Each NoFilho As TreeNode In No.Nodes
                                If NoFilho.Text.ToString.ToLower = DwgTactual.ToLower Then
                                    NoFilho.EnsureVisible()
                                    TrvProjeto.SelectedNode = NoFilho : TrvProjeto.SelectedNode.Checked = True
                                    txtDesenhoSel.Text = NoFilho.Text
                                    'txtLocalizacao.Text = PathDiretorio & NoFilho.Text & "\" & NoFilho.Text & ".dwg"
                                    txtLocalizacao.Text = PathDiretorio & txtProjetoSel.Text & "\" & NoFilho.Text & ".dwg"
                                    'Carrega as informações e visualização do ultimo desenho selecionado
                                    If My.Computer.FileSystem.FileExists(txtLocalizacao.Text) Then
                                        'Dwg.DwgFileName = txtLocalizacao.Text : Dwg.Refresh()
                                        FileImg = PathDiretorio & txtProjetoSel.Text & "\" & NoFilho.Text & My.Settings.ExtensionCompl_Img & My.Settings.Extension_Img

                                        If My.Computer.FileSystem.FileExists(FileImg) Then
                                            Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent
                                            Dim FileBmp As Bitmap
                                            FileBmp = LibraryComponent.LoadImage_InPictureBox(FileImg)
                                            Dwg.Image = FileBmp
                                        Else
                                            lblInfo.BackColor = Color.Gray : lblInfo.ForeColor = Color.White
                                            lblInfo.Visible = True
                                            lblInfo.Text = My.Settings.frmExisteDesenho_MsgNoPreview : lblInfo.Refresh()
                                            PicNoImg.Visible = True : PicNoImg.Refresh()
                                        End If

                                    Else
                                        lblInfo.BackColor = Color.DarkSalmon
                                        lblInfo.Visible = True : lblInfo.Refresh()
                                        lblInfo.Text = "Desenho selecionado não encontrado !" : lblInfo.Refresh()
                                        btnOk.Enabled = False
                                    End If

                                    Exit For
                                End If
                            Next
                        End If
                    Next
                End With
            End If

            SetCurrentNode(ProjTactual, DwgTactual, TrvProjeto)

            Dim LibraryComponet As New Aenge.Library.Component.LibraryComponent
            LibraryComponet.ConfigFont_Component(frm_ExisteDesenho)
            LibraryComponet = Nothing

        Catch ex As Exception
            lblInfo.BackColor = Color.Khaki
            lblInfo.Visible = True
            lblInfo.Text = "Formato inválido. Visualização não disponível !" : lblInfo.Refresh()
        End Try

    End Sub

#End Region

#Region "----- Label -----"

    Private Sub txtDesenhoSel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDesenhoSel.Click

    End Sub

    Private Sub txtDesenhoSel_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDesenhoSel.TextChanged
        'LoadImg_Dwg()
        btnOk.Enabled = False
        If txtDesenhoSel.Text.ToString.Trim <> "-" Then btnOk.Enabled = True
    End Sub

#End Region

#End Region

#Region "----- Funcionalidades de consulta e busca de informações -----"

    'Carrega os previews dos dwgs selecionados pelo usuário 
    Function LoadImg_Dwg() As Object

        If SearchInfo = False Then Return False

        Dim PathDes As String = "", FileImg As String = ""
        lblInfo.Visible = False : lblInfo.Refresh()

        Try

            'Caso possua desenho valido selecionado, ai carrega o dwg com a visualização do dwg, senão limpa o dwg 
            If txtDesenhoSel.Text <> "" And txtDesenhoSel.Text <> "-" Then
                PathDes = PathDiretorio & txtProjetoSel.Text & "\" & txtDesenhoSel.Text & My.Settings.ExtensionCompl_Img & My.Settings.Extension_Img
                If My.Computer.FileSystem.FileExists(PathDes) Then
                    'Dwg.DwgFileName = txtLocalizacao.Text
                    FileImg = PathDes
                    Dwg.Image = Image.FromFile(FileImg) : Dwg.Refresh()
                    Dwg.Refresh()
                Else
                    'Limpa o desenho e visualização dos dados 
                    'Dwg.DwgFileName = PathNoDwg
                    Dwg.Image = Image.FromFile(PathNoDwg) : Dwg.Refresh()
                    'lblInfo.Visible = True : lblInfo.Refresh()
                End If
            Else
                'Limpa o desenho e visualização dos dados 
                'Dwg.DwgFileName = PathNoDwg
                Dwg.Image = Image.FromFile(PathNoDwg) : Dwg.Refresh()
                'lblInfo.Visible = True : lblInfo.Refresh()
            End If

        Catch ex As Exception
            'If My.Computer.FileSystem.FileExists(PathDes) Then Dwg.DwgFileName = txtLocalizacao.Text : Dwg.Refresh() : Return True
            'LibraryError.CreateErrorAenge(Err, "Erro de visualização do Dwg !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmExisteDesenho002")
            Return False
        End Try

        Return True

    End Function

    'Validate information about open projects, info about open drawings 
    Function GetConfig_Open() As Object

        Dim StyleMenu, IniMenu, Menubar As Object
        Dim PathDir As String = GetAppInstall()

        StyleMenu = Aenge_GetCfg("Configuration", "StyleMenu", PathDir & My.Settings.File_Autoenge)
        IniMenu = Aenge_GetCfg("Configuration", "IniMenu", PathDir & My.Settings.File_Autoenge)
        Menubar = Aenge_GetCfg("Configuration", "Menubar", PathDir & My.Settings.File_Autoenge)

        If Not IsNumeric(Menubar) Then Menubar = 1
        'Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("MENUBAR", Menubar)
        Return True

    End Function

    'Valida a abertura do desenho selecionado pelo usuário, assim como parâmetros e outras funcionalidades 
    Function ValidateOpen_Dwg() As Object

        Try

            If Not My.Computer.FileSystem.FileExists(txtLocalizacao.Text) Then
                MsgBox("O desenho selecionado não está disponível para visualização e acesso. Verifique se o desenho foi excluido ou removido da pasta de origem. Em caso de dúvidas, entre em contato com nosso suporte técnico!", MsgBoxStyle.Information, "Desenho não encontrado")
                Return False
            Else
                'Verifica se o arquivo eh somente para visualizacao, isto eh, outra instancia ja esta com o arquivo aberto, pois assim nao eh possivel acessar o banco de dados do dwg
                ' Create a new FileInfo object. 
                Dim fInfo As New FileInfo(txtLocalizacao.Text)
                ' Return the IsReadOnly property value. 
                If fInfo.IsReadOnly Then
                    MsgBox("O Desenho selecionado está sendo utilizado por outra instância do Cad ou por outra aplicação, não será possível acessar os seus dados. Caso queira abrir o desenho selecionado," & _
                           " favor fechar qualquer outra aplicação que esteja usando o mesmo.", MsgBoxStyle.Information, "Desenho bloqueado")
                    Return False
                End If
            End If

            'Para não ficar visivel o formulário enquanto carrega as informações no cad
            Me.Hide()
            'Seta o nome do projeto atual 
            mVar_NameProjTactual = txtProjetoSel.Text

            'Valida a abertura e parametros selecionados 
            OpenDWG(txtLocalizacao.Text)
            SetArquivoCFGDados()
            System.Windows.Forms.Application.DoEvents()
            SetIdTactual(txtProjetoSel.Text)
            Initialize()
            GetConfig_Open()

            Dwg.Image = Nothing : Dwg.Dispose()

        Catch ex As Exception
            If LibraryError Is Nothing Then LibraryError = New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao validar a abertura do desenho selecionado !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmExisteDesenho003")
            Return False
        End Try

        Return True

    End Function

    'Abre um determinado DWG para sua manipulação dentro do ambiente do Intellicad
    Function OpenDWG(ByVal DrawApp As String) As Boolean

        Try

            Dim PathF As String, DocTactual As Document, NameDocTactual As String = ""
            PathF = Replace(GetAppInstall, "\", "\\")
            DrawApp = Replace(DrawApp, "\\", "\")

            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("SDI", 0)
            'Autodesk.AutoCAD.ApplicationServices.DocumentExtension.CloseAndSave(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument, NameDocTactual)
            System.Windows.Forms.Application.DoEvents()

            Dim AcDocMgr As DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager
            DocTactual = DocumentCollectionExtension.Open(AcDocMgr, DrawApp, False)
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument = DocTactual
            'DocTactual = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
            System.Windows.Forms.Application.DoEvents()
            NameDocTactual = DocTactual.Name

            'Using DocTactual.LockDocument

            'Atualiza as Userr1,2 e 3
            Dim PathCfgDraw As String, UnidadeCfg As String, EscalaCfg As String, ValorUnidade As Single
            PathCfgDraw = PathDiretorio & txtProjetoSel.Text & "\" & txtDesenhoSel.Text & ".cfg"    'GetAppInstall() & Combo1.Text & "\" & List1.Text & ".cfg"
            UnidadeCfg = Aenge_GetCfg("DRAWING", "UNIDADE_MEDIDA", PathCfgDraw)
            EscalaCfg = Aenge_GetCfg("DRAWING", "ESCALA", PathCfgDraw)

            Select Case LCase(UnidadeCfg)
                Case "m"
                    ValorUnidade = 0.001

                Case "cm"
                    ValorUnidade = 0.1

                Case "mm"
                    ValorUnidade = 1

                Case Else
                    ValorUnidade = 0

            End Select

            'Atualiza as references para o cad
            '.Preferences.System.SingleDocumentMode = False
            'Seta as variaveis locais para a aplicação

            Dim CommandExecute As String = ""
            CommandExecute = "(SetVar " & Chr(34) & "USERS1" & Chr(34) & " " & Chr(34) & txtProjetoSel.Text & Chr(34) & ")"
            mVar_NameProjTactual = txtProjetoSel.Text
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(CommandExecute & " ", True, False, False)

            CommandExecute = "(SetVar " & Chr(34) & "USERS2" & Chr(34) & " " & Chr(34) & PathDiretorio.Replace("\", "\\") & Chr(34) & ")"
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(CommandExecute & " ", True, False, False)

            CommandExecute = "(SetVar " & Chr(34) & "USERS3" & Chr(34) & " " & Chr(34) & "1000" & Chr(34) & ")"
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(CommandExecute & " ", True, False, False)

            CommandExecute = "(SetVar " & Chr(34) & "USERR3" & Chr(34) & " " & CDbl(EscalaCfg) & ")"
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(CommandExecute & " ", True, False, False)

            CommandExecute = "(SetVar " & Chr(34) & "USERR2" & Chr(34) & " " & ValorUnidade.ToString.Replace(",", ".") & ")".Replace(",", ".")
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(CommandExecute & " ", True, False, False)
            System.Windows.Forms.Application.DoEvents()

            'Verifica qual o tipo de menu a ser carregado
            Dim EstiloMenu As String
            EstiloMenu = Aenge_GetCfg("GENERAL", "MENU", PathDiretorio & "autoenge.cfg")
            If EstiloMenu = "" Then EstiloMenu = "0"
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS5", EstiloMenu)

            Dim PathLisp As String
            PathLisp = "(LOAD " & Chr(34) & Replace(PathDiretorio, "\", "\\") & "AHID.vlx" & Chr(34) & ")"
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(PathLisp & Chr(13), True, False, False)
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute("HIDInicial" & Chr(13), True, False, False)
            System.Windows.Forms.Application.DoEvents()
            ' ''Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("SDI", 1)

            'End Using

            Return True

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao validar parâmetros e funções de abertura do desenho selecionado !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmExisteDesenho004")
            Return False
        Finally

        End Try

    End Function

    'Passa os novos parametros para os arquivos CFG dos desenhos e dos projetos
    Private Sub SetArquivoCFGDados()

        Dim ApCaminho As String, IDP As String
        Dim bRet As Boolean

        Try

            ApCaminho = PathDiretorio
            IDP = CStr(IDProject(txtProjetoSel.Text))
            bRet = Aenge_SetCfg("AppData", "AENGEPROJ", txtProjetoSel.Text, ApCaminho & "Autoenge.cfg")
            bRet = Aenge_SetCfg("AppData", "AENGEDWG", txtDesenhoSel.Text, ApCaminho & "Autoenge.cfg")
            bRet = Aenge_SetCfg("AppData", "AENGEPROJID", IDP, ApCaminho & "Autoenge.cfg")

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao salvar as configurações do desenho que esta sendo carregado !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmExisteDesenho005")
        End Try
    End Sub

#End Region

End Class
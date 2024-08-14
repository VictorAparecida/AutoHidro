Imports Autodesk.AutoCAD.Runtime
Imports System.Windows.Forms
Imports Autodesk.AutoCAD.EditorInput
Imports System.Drawing

'<Assembly: CommandClass(GetType(UsrC_Project))> 
Public Class UsrC_Project

#Region "----- Documentação -----"

    '==============================================================================================================================================
    'Projeto : CadSolution 1.0
    'Empresa : Thorx Sistemas e Consultoria Ltda
    'Reponsável criação : Raul Antonio Fernandes Junior    
    'Data de criação : 13/03/2009
    'Objetivo : Formulário responsável pela visualização das informações através do usercontrol
    '
    'Informações adicionais - Tratamento de Erros :
    'UsrC_Project001 - 
    'UsrC_Project002 - 
    'UsrC_Project003 - 
    'UsrC_Project004 - 
    'UsrC_Project005 - 
    'UsrC_Project006 - 
    'UsrC_Project007 - 
    'UsrC_Project008 - 
    'UsrC_Project009 - 
    'UsrC_Project010 - 
    'UsrC_Project011 - 
    'UsrC_Project012 - 
    'UsrC_Project013 - 
    'UsrC_Project014 - 
    'UsrC_Project015 - 
    '==============================================================================================================================================

#End Region

#Region "----- Atribute and Declare -----"

    Private Shared LibraryReference As LibraryReference, GetAppInstall As String, LibraryRegister As LibraryRegister, LibraryCad As LibraryCad
    Private Shared mVar_NameClass As String = "UsrC_Project", _TableName As String = "", mVar_AllProjects As Boolean

#End Region

#Region "----- Constructors -----"

    Public Sub New(ByVal AllProject As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        mVar_AllProjects = AllProject
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        LibraryReference = Nothing : LibraryRegister = Nothing : LibraryCad = Nothing
    End Sub

#End Region

#Region "----- Events -----"

#Region "----- Form -----"

    Private Sub UsrC_Project_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed

    End Sub

    Private Sub UsrC_Project_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
        GetAppInstall = LibraryReference.ReturnPathApplication
        FillTreeView_DrawProject(trvDraw, "", mVar_AllProjects)

        'Format fonts and labels 
        Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent
        LibraryComponent.ConfigFont_Component(_UsrC_Project)
        LibraryComponent = Nothing
    End Sub

#End Region

#Region "----- Treeview -----"

    Private Sub trvDraw_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvDraw.AfterSelect

    End Sub

    Private Sub trvDraw_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvDraw.NodeMouseClick
        GetInfoDrawing(e)
    End Sub

    Function OpenDrawing() As Object
        Dim NameDwg As String = "", ProjectSelected As String = "", Id_Project As Integer, ProjectOld As String = "", DrawSelected As String = ""
        Dim PathCfgProj As String = ""
        Dim NameProj As String = LibraryReference.Return_TactualProject.ToString
        Dim FullPath As String = LibraryReference.ReturnPathApplication & NameProj & "\" & trvDraw.SelectedNode.Text.ToString & ".dwg"

        If trvDraw.SelectedNode.Tag.ToString.Contains("NODE0") Then
            MsgBox("Selecione um desenho válido primeiro !", MsgBoxStyle.Information, "Desenho inválido")
        Else

            If LibraryCad Is Nothing Then LibraryCad = New LibraryCad

            ProjectSelected = trvDraw.SelectedNode.Parent.Text
            'Get Id project 
            Id_Project = trvDraw.SelectedNode.Parent.Tag.ToString.Split("|")(1)
            'Get a old project and compare with a selected by user
            ProjectOld = LibraryReference.Return_TactualProject

            'Validate a change of project 
            If ProjectOld <> ProjectSelected Then
                frm_Confirm = New frmConfirm
                With frm_Confirm
                    .NameProjNew = ProjectSelected.ToString
                    .NameDwgNew = trvDraw.SelectedNode.Text.ToString
                    .ShowDialog()
                End With

                If ReturnFormOption = False Then Return False
            End If

            IsSaveImgThumbnail = False
            Dwg.Image = Nothing
            frmExisteDesenho_ValidateOpen_Dwg(GetAppInstall & ProjectSelected & "\" & trvDraw.SelectedNode.Text & ".dwg", ProjectSelected, lblNameDwg.Text.ToUpper.Replace(".DWG", ""))
            IsSaveImgThumbnail = True

            'Valida se existe agendamentos e tarefas para o projeto em questao
            Dim LibraryVBA As New LibraryVBA
            LibraryVBA.Load_AhidAgendamento(Nothing)
            LibraryVBA = Nothing


            'After all functions, validate if a project tactual is equal a old project 
            If ProjectOld <> ProjectSelected Then
                For Each Docs As Autodesk.AutoCAD.ApplicationServices.Document In Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager
                    If Not Docs.Name.ToString.ToLower.EndsWith(lblNameDwg.Text.ToLower) Then
                        'Dim AcDocMgr As DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager
                        Autodesk.AutoCAD.ApplicationServices.DocumentExtension.CloseAndSave(Docs, Docs.Name)
                        'Docs.CloseAndSave(Docs.Name) 'DrawSelected
                    End If

                Next
            End If

        End If

        Return True

    End Function

    Private Sub trvDraw_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvDraw.NodeMouseDoubleClick
        OpenDrawing()
    End Sub

#End Region

#Region "----- CommandButton -----"

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        OpenDrawing()
    End Sub

    Private Sub btnConfig_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfig.Click
        frm_ConfigAbert = New frmConfigAbert
        frm_ConfigAbert.ShowDialog()
        frm_ConfigAbert = Nothing
    End Sub

#End Region

#End Region

#Region "----- Load in treeview all information about projects and drawings -----"

    Function FillTreeView_DrawProject(ByVal Trv As TreeView, ByVal sSql As String, Optional ByVal ClearTView As Boolean = True, _
    Optional ByVal TypeData As String = "", Optional ByVal ArrayColumns() As String = Nothing) As Boolean

        Dim TactualProj As String, Dt_DrawProject As New DataTable

        If ClearTView = True Then
            Trv.ResetText()
            Trv.Nodes.Clear()
        End If

        'Get a tactual project 
        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
        If LibraryRegister Is Nothing Then LibraryRegister = New LibraryRegister

        TactualProj = LibraryReference.Return_TactualProject
        Dt_DrawProject = LibraryRegister.RetunAllDraw_Project("datatable", , True)

        If Dt_DrawProject Is Nothing Then Return False
        If Dt_DrawProject.Rows.Count <= 0 Then Return False

        Dim Resultado As String = "Não informado", AtualizouNodeProtocolo As Boolean = False
        Dim LimpaDrR As Boolean = False, Texto As String = "", Cor As New System.Drawing.Color, Column0 As String = "", Column1 As String = ""

        Try

            'Datareader for povoate a treeview with a drawings of project 
            Dim DrR As DataTableReader
            DrR = Dt_DrawProject.CreateDataReader

            Dim Node0 As TreeNode = Nothing, i As Integer = 0
            Dim Node1 As TreeNode = Nothing

            Do While DrR.Read

                'Povoa o list com as informações agrupadas
                With Trv

                    .ShowNodeToolTips = True

                    'For the project 
                    If Column0 <> DrR("AengeDes.PRJNOME").ToString Then
                        Node0 = New TreeNode
                        Node0.Text = DrR("AengeDes.PRJNOME").ToString
                        Node0.Tag = "NODE0|" & DrR("Id").ToString
                        Node0.ImageIndex = My.Settings.Index_IconFolder
                        Node0.ForeColor = Drawing.Color.Black
                        .ShowNodeToolTips = True
                        .Nodes.Add(Node0)

                        Column0 = DrR("AengeDes.PRJNOME").ToString
                        i += 1

                        Node1 = New TreeNode
                        With Node1
                            .Tag = "NODE1|" & DrR("PrjDwgNome").ToString
                            .ToolTipText = "Desenho : " & DrR("PrjDwgNome").ToString & Chr(13) & "Localização : " & DrR("PrjDwg").ToString
                            .Text = DrR("PrjDwgNome").ToString
                            .ImageIndex = My.Settings.Index_IconDwg
                        End With

                        Column1 = "Objeto : " & DrR("PrjDwgNome").ToString
                        Node0.Nodes.Add(Node1)

                    Else

                        If Column1 <> "Objeto : " & DrR("PrjDwgNome").ToString Then
                            'Informações referentes ao nó de protocolo
                            'NodeProtocolo.ImageIndex = 15
                            Node1 = New TreeNode
                            With Node1
                                .Tag = "NODE1|" & DrR("PrjDwgNome").ToString
                                .ToolTipText = "Desenho : " & DrR("PrjDwgNome").ToString & Chr(13) & "Localização : " & DrR("PrjDwg").ToString
                                .Text = DrR("PrjDwgNome").ToString
                                .ImageIndex = My.Settings.Index_IconDwg
                            End With
                            AtualizouNodeProtocolo = True
                        End If

                        If AtualizouNodeProtocolo = True Then
                            Node0.Nodes.Add(Node1)
                            AtualizouNodeProtocolo = False
                        End If

                    End If

                End With

            Loop

            'Expand only project tactual
            For Each NodeT As TreeNode In Trv.Nodes

                If NodeT.Tag.ToString.Contains("NODE0") Then
                    If TactualProj = NodeT.Text Then NodeT.Expand()
                Else
                    NodeT.Collapse()
                End If

            Next


            If LimpaDrR = True Then DrR.Close()
            Return True

        Catch ex As Exception
            MsgBox("Erro ao povoar as informações do componente Treeview - Objetos para leitura e comparação !", MsgBoxStyle.Information, "Error Aenge")
            Return False
        Finally

        End Try

    End Function

#End Region

#Region "----- Functions for consulting data ----"

    Function GetInfoDrawing(ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) As Boolean

        Dim PathProject As String = "", FileImg As String = "", FileImg_NoPhoto As String = ""

        'clear all fields before
        lblProject.Text = "-"
        lblNameDwg.Text = "-"
        lblScale.Text = "-"
        lblMed.Text = "-"
        lblDtCreate.Text = "Não disponível"
        lblLen.Text = "-"
        lblInfo.Visible = False : lblInfo.Refresh()
        PicNoImg.Visible = False : PicNoImg.Refresh()
        btnOpen.Enabled = True

        Try

            If Not e.Node.Tag.ToString.Contains("NODE0") Then
                If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
                If LibraryCad Is Nothing Then LibraryCad = New LibraryCad

                Dim NameProjTactual As String = e.Node.Parent.Text.ToString
                Dim NameDwgSelect As String = e.Node.Text.ToString
                Dim FullPath As String = LibraryReference.ReturnPathApplication & NameProjTactual & "\"

                lblProject.Text = NameProjTactual
                lblNameDwg.Text = NameDwgSelect & ".Dwg"
                lblScale.Text = Aenge_GetCfg("DRAWING", "ESCALA", FullPath & NameDwgSelect & ".cfg")
                lblMed.Text = Aenge_GetCfg("DRAWING", "UNIDADE_MEDIDA", FullPath & NameDwgSelect & ".cfg")

                PathProject = FullPath
                FileImg = PathProject & NameDwgSelect & My.Settings.ExtensionCompl_Img & My.Settings.Extension_Img
                FileImg_NoPhoto = LibraryReference.ReturnPathApplication & "NoDwg" & My.Settings.Extension_Img

                With My.Computer.FileSystem
                    'Valida se o desenho é valido
                    If My.Computer.FileSystem.FileExists(FullPath & NameDwgSelect & ".dwg") Then
                        lblDtCreate.Text = .GetFileInfo(FullPath & NameDwgSelect & ".dwg").CreationTime.Date
                        lblLen.Text = .GetFileInfo(FullPath & NameDwgSelect & ".dwg").Length
                        'Valida as thumbnails do desenho em questao para visualizacao do usuario 
                        If My.Computer.FileSystem.FileExists(FileImg) Then
                            'Dwg.Image = Image.FromFile(FileImg) : Dwg.Refresh()
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
                        btnOpen.Enabled = False
                        lblDtCreate.Text = "Não disponível"
                        lblLen.Text = "-"
                    End If

                End With

            End If

        Catch ex As Exception
            lblDtCreate.Text = "Não disponível"
            lblLen.Text = "-"
            lblInfo.BackColor = Color.DarkSalmon
            lblInfo.Visible = True : lblInfo.Refresh()
            lblInfo.Text = "Desenho selecionado não encontrado !" : lblInfo.Refresh()
            PicNoImg.Visible = True : PicNoImg.Refresh()
        End Try

        Return True

    End Function

#End Region

End Class

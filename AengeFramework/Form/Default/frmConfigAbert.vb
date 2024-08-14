Public Class frmConfigAbert

#Region "----- Documentation -----"

    '==============================================================================================================================================
    'Projeto :    Autohidro
    'Empresa :  Autoenge
    'Reponsável criação : Raul Antonio Fernandes Junior    
    'Data de criação : 04/03/2013
    'Objetivo : Form for configuration open drawings in application 
    '
    'Info Add - Tratament errors :
    'frmConfigAbert001 - GetConfig_Application
    'frmConfigAbert002 - SetConfig_Application
    'frmConfigAbert003 - 
    'frmConfigAbert004 - 
    'frmConfigAbert005 - 
    'frmConfigAbert006 - 
    'frmConfigAbert007 - 
    'frmConfigAbert008 - 
    'frmConfigAbert009 - 
    'frmConfigAbert010 - 
    'frmConfigAbert011 - 
    'frmConfigAbert012 - 
    'frmConfigAbert013 - 
    'frmConfigAbert014 - 
    'frmConfigAbert015 - 
    '==============================================================================================================================================

#End Region

#Region "----- Atributes and declare -----"

    Dim LibraryReference As LibraryReference, LibraryError As New LibraryError

    Private Shared mVar_InfoText As Object, mVar_Titulo As String = "", Mvar_NameProjNew As String, mVar_NameDwgNew As String
    Private Shared PathDir As String = GetAppInstall(), mVar_NameClass As String = "frmConfigAbert"

#End Region

#Region "----- Constructors -----"

    Protected Overrides Sub Finalize()
        LibraryError = Nothing
        MyBase.Finalize()
    End Sub

#End Region

#Region "----- Get e Set -----"

    'Obtem a referência da instancia do autocad a ser utilizada
    Property InfoText() As Object
        Get
            InfoText = mVar_InfoText
        End Get
        Set(ByVal value As Object)
            mVar_InfoText = value
        End Set
    End Property

    Property NameProjNew() As Object
        Get
            NameProjNew = Mvar_NameProjNew
        End Get
        Set(ByVal value As Object)
            Mvar_NameProjNew = value
        End Set
    End Property

    Property NameDwgNew() As Object
        Get
            NameDwgNew = mVar_NameDwgNew
        End Get
        Set(ByVal value As Object)
            mVar_NameDwgNew = value
        End Set
    End Property

    Property TituloForm() As Object
        Get
            TituloForm = mVar_Titulo
        End Get
        Set(ByVal value As Object)
            mVar_Titulo = value
        End Set
    End Property

#End Region

#Region "----- Events -----"

#Region "----- Form -----"

    Private Sub frmConfigAbert_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frm_ConfigAbert = Nothing
    End Sub

    Private Sub frmConfigAbert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Carrega as informaçoes do titulo de acordo com o padrao e local desejado pelo usuario 
        If mVar_Titulo = "" Then mVar_Titulo = My.Settings.frmLoading_Default.ToString
        lblTitulo.Text = My.Settings.AhidName & " " & My.Settings.Version & " - " & mVar_Titulo

        GetConfig_Application()

        Dim LibraryComponet As New Aenge.Library.Component.LibraryComponent
        ' LibraryComponet.ConfigFont_Component(frm_ConfigAbert)
        LibraryComponet = Nothing

    End Sub

#End Region

#Region "----- CommandButton -----"

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If SetConfig_Application() Then Me.Dispose(True)
    End Sub

    Private Sub btnFechar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFechar.Click
        Me.Dispose(True)
    End Sub

#End Region

#End Region

#Region "----- Funcionalidades de configurações de dados -----"

    'Save all data in form
    Function SetConfig_Application() As Object

        Dim Menubar As Object = 1, StyleMenu As Object = 0, IniMenu As Object = 1

        Try

            StyleMenu = cboStyleMenu.SelectedIndex
            IniMenu = cboIniMenu.SelectedIndex
            Menubar = cboMenuBar.SelectedIndex

            Aenge_SetCfg("Configuration", "StyleMenu", StyleMenu, PathDir & My.Settings.File_Autoenge)
            Aenge_SetCfg("Configuration", "IniMenu", IniMenu, PathDir & My.Settings.File_Autoenge)
            Aenge_SetCfg("Configuration", "Menubar", Menubar, PathDir & My.Settings.File_Autoenge)
            Return True

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error saving data - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmConfigAbert002")
            Return False
        End Try

    End Function

    'Get all data config before for user
    Function GetConfig_Application() As Object

        Dim Menubar As Object, StyleMenu As Object, IniMenu As Object

        Try

            StyleMenu = Aenge_GetCfg("Configuration", "StyleMenu", PathDir & My.Settings.File_Autoenge)
            IniMenu = Aenge_GetCfg("Configuration", "IniMenu", PathDir & My.Settings.File_Autoenge)
            Menubar = Aenge_GetCfg("Configuration", "Menubar", PathDir & My.Settings.File_Autoenge)

            If IsNumeric(StyleMenu) Then
                cboStyleMenu.SelectedIndex = Int16.Parse(StyleMenu)
            Else
                cboStyleMenu.SelectedIndex = 1
            End If

            If IsNumeric(IniMenu) Then
                cboIniMenu.SelectedIndex = Int16.Parse(IniMenu)
            Else
                cboIniMenu.SelectedIndex = 0
            End If

            If IsNumeric(Menubar) Then
                cboMenuBar.SelectedIndex = Int16.Parse(Menubar)
            Else
                cboMenuBar.SelectedIndex = 1
            End If

            Return True

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error getting data - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmConfigAbert001")
            Return False
        End Try

    End Function

#End Region

End Class
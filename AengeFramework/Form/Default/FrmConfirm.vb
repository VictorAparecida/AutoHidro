Public Class frmConfirm

#Region "----- Atributes and declare -----"

    Dim LibraryReference As LibraryReference

    Private Shared mVar_InfoText As Object, mVar_Titulo As String = "", Mvar_NameProjNew As String, mVar_NameDwgNew As String

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

#Region "----- Constructors -----"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        LibraryReference = Nothing
    End Sub

#End Region

#Region "----- Events -----"

#Region "-----Form -----"

    Private Sub frmConfirm_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frm_Confirm = Nothing
    End Sub

    Private Sub frmConfirm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Carrega as informaçoes do titulo de acordo com o padrao e local desejado pelo usuario 
        If mVar_Titulo = "" Then mVar_Titulo = My.Settings.frmLoading_Default.ToString
        lblTitulo.Text = My.Settings.AhidName & " " & My.Settings.Version & " - " & mVar_Titulo

        ReturnFormOption = False
        GetAllInfo_TactualProject()

        Dim LibraryComponet As New Aenge.Library.Component.LibraryComponent
        LibraryComponet.ConfigFont_Component(frm_Confirm)
        LibraryComponet = Nothing

    End Sub

#End Region

#Region "----- CommandButton -----"

    Private Sub btnFechar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFechar.Click
        Me.Dispose(True)
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        ReturnFormOption = True
        Me.Dispose(True)
    End Sub

#End Region

#End Region

#Region "----- Info Project -----"

    Function GetAllInfo_TactualProject() As Boolean

        Dim FullPath As String, NameProj As String, NameDwg As String

        Try

            If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
            With LibraryReference
                FullPath = .ReturnPathApplication
                NameProj = .Return_TactualProject
                NameDwg = .Return_TactualDrawing
            End With

            lblNameDwg.Text = NameDwg.ToString
            lblProject.Text = NameProj.ToString
            lblNameDwgNew.Text = mVar_NameDwgNew.ToString
            lblProjectNew.Text = Mvar_NameProjNew.ToString
            Return True

        Catch ex As Exception
            Return False
        Finally
            LibraryReference = Nothing
        End Try

    End Function

#End Region

End Class
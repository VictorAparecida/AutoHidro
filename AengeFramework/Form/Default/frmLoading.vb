Public Class frmLoading

#Region "----- Atributos e Declaracoes -----"

    Private Shared mVar_InfoText As Object, mVar_Titulo As String = "", mVar_IDForm As String
    'Contador de integridade
    Dim CountScreen As Int16 = 0

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

    Property IDForm() As Object
        Get
            IDForm = mVar_IDForm
        End Get
        Set(ByVal value As Object)
            mVar_IDForm = value
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

#Region "----- Eventos -----"

#Region "----- Command Button -----"

#End Region

#Region "----- Timer -----"

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'lblDestaque.Visible = Not lblDestaque.Visible
        If mVar_IDForm Is Nothing Then mVar_IDForm = ""
        Select Case mVar_IDForm.ToLower
            Case "apwrdim".ToLower
                If CountScreen >= My.Settings.frmLoading_TimerDimens Then Me.Dispose(True)

            Case "apwrvis".ToLower
                If CountScreen >= My.Settings.frmLoading_TimerDimens Then Me.Dispose(True)

            Case Else
                If CountScreen >= My.Settings.frmLoading_Timer Then Me.Dispose(True)

        End Select
        CountScreen += 1
    End Sub

#End Region

#Region "----- Form -----"

    Private Sub frmLoading_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Carrega as informaçoes do titulo de acordo com o padrao e local desejado pelo usuario 
        If mVar_Titulo = "" Then mVar_Titulo = My.Settings.frmLoading_Default.ToString
        lblTitulo.Text = My.Settings.AhidName & " " & My.Settings.Version & " - " & mVar_Titulo

        'If mVar_InfoText = "" Then mVar_InfoText = My.Settings.frmLoading_DefaultNull.ToString
        txtInfo.Text = mVar_InfoText
        Timer1.Enabled = True

        Dim LibraryComponet As New Aenge.Library.Component.LibraryComponent
        LibraryComponet.ConfigFont_Component(frm_Loading)
        LibraryComponet = Nothing

    End Sub

#End Region

#End Region

End Class
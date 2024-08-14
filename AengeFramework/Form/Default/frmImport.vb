Public Class frmImport

#Region "----- Atributos e Declaracoes -----"

    Private Shared mVar_Titulo As String
    Dim CountTimer As Int16 = 5

#End Region

#Region "----- Events -----"

#Region "----- CommandButton -----"

    Private Sub btnVisualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVisualizar.Click
        RetornoFrmImport = True
        Me.Dispose(True)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose(True)
    End Sub

#End Region

#Region "----- Form -----"

    Private Sub frmImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Carrega as informaçoes do titulo de acordo com o padrao e local desejado pelo usuario 
        If mVar_Titulo = "" Then mVar_Titulo = My.Settings.frmLoading_Default.ToString
        lblTitulo.Text = My.Settings.AhidName & " " & My.Settings.Version

        Dim LibraryComponet As New Aenge.Library.Component.LibraryComponent
        LibraryComponet.ConfigFont_Component(frm_Import)
        LibraryComponet = Nothing

    End Sub

#End Region

#Region "----- Timer -----"

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If CountTimer <= 0 Then Me.Dispose(True)
        CountTimer -= 1
    End Sub

#End Region

#End Region

End Class
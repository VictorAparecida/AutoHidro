Public Class frmHomologation

    Public Dt, Dt2 As System.Data.DataTable

    Private Sub frmHomologation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        DataGridView1.DataSource = Dt
        DataGridView2.DataSource = Dt2

    End Sub
End Class
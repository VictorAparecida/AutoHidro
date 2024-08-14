<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoading
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.txtInfo = New System.Windows.Forms.RichTextBox
        Me.lblRodape = New System.Windows.Forms.Label
        Me.lblDestaque = New System.Windows.Forms.Label
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LinkAutoenge = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.Color.White
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblTitulo.ForeColor = System.Drawing.Color.SteelBlue
        Me.lblTitulo.Location = New System.Drawing.Point(7, 1)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(11, 13)
        Me.lblTitulo.TabIndex = 0
        Me.lblTitulo.Text = "-"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Silver
        Me.Panel1.Location = New System.Drawing.Point(146, 34)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(288, 1)
        Me.Panel1.TabIndex = 2
        '
        'txtInfo
        '
        Me.txtInfo.BackColor = System.Drawing.Color.White
        Me.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInfo.ForeColor = System.Drawing.Color.SteelBlue
        Me.txtInfo.Location = New System.Drawing.Point(18, 39)
        Me.txtInfo.Name = "txtInfo"
        Me.txtInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.txtInfo.Size = New System.Drawing.Size(384, 83)
        Me.txtInfo.TabIndex = 3
        Me.txtInfo.Text = ""
        '
        'lblRodape
        '
        Me.lblRodape.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblRodape.Location = New System.Drawing.Point(13, 130)
        Me.lblRodape.Name = "lblRodape"
        Me.lblRodape.Size = New System.Drawing.Size(383, 30)
        Me.lblRodape.TabIndex = 4
        Me.lblRodape.Text = "Softwares para engenharia e customização de ferramentas CAD. Entre em contato com" & _
            " um dos nossos consultores..."
        '
        'lblDestaque
        '
        Me.lblDestaque.BackColor = System.Drawing.Color.Silver
        Me.lblDestaque.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblDestaque.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDestaque.ForeColor = System.Drawing.Color.Black
        Me.lblDestaque.Location = New System.Drawing.Point(15, 19)
        Me.lblDestaque.Name = "lblDestaque"
        Me.lblDestaque.Size = New System.Drawing.Size(142, 16)
        Me.lblDestaque.TabIndex = 5
        Me.lblDestaque.Text = "AGUARDE POR FAVOR !"
        Me.lblDestaque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'LinkAutoenge
        '
        Me.LinkAutoenge.AutoSize = True
        Me.LinkAutoenge.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.LinkAutoenge.ForeColor = System.Drawing.Color.SteelBlue
        Me.LinkAutoenge.LinkColor = System.Drawing.Color.SteelBlue
        Me.LinkAutoenge.Location = New System.Drawing.Point(15, 159)
        Me.LinkAutoenge.Name = "LinkAutoenge"
        Me.LinkAutoenge.Size = New System.Drawing.Size(125, 13)
        Me.LinkAutoenge.TabIndex = 7
        Me.LinkAutoenge.TabStop = True
        Me.LinkAutoenge.Text = "www.autoenge.com.br"
        '
        'frmLoading
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(404, 178)
        Me.Controls.Add(Me.LinkAutoenge)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblDestaque)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.lblRodape)
        Me.Controls.Add(Me.txtInfo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmLoading"
        Me.Opacity = 0.9
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Carregando, aguarde..."
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtInfo As System.Windows.Forms.RichTextBox
    Friend WithEvents lblRodape As System.Windows.Forms.Label
    Friend WithEvents lblDestaque As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents LinkAutoenge As System.Windows.Forms.LinkLabel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLineType
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblDestaque = New System.Windows.Forms.Label
        Me.Linetype1 = New System.Windows.Forms.ComboBox
        Me.btnOk = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.lblSubTitulo = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Location = New System.Drawing.Point(142, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(426, 1)
        Me.Panel1.TabIndex = 33
        '
        'lblDestaque
        '
        Me.lblDestaque.BackColor = System.Drawing.Color.DimGray
        Me.lblDestaque.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblDestaque.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDestaque.ForeColor = System.Drawing.Color.White
        Me.lblDestaque.Location = New System.Drawing.Point(12, 9)
        Me.lblDestaque.Name = "lblDestaque"
        Me.lblDestaque.Size = New System.Drawing.Size(141, 18)
        Me.lblDestaque.TabIndex = 32
        Me.lblDestaque.Text = "LineTypes"
        Me.lblDestaque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Linetype1
        '
        Me.Linetype1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Linetype1.FormattingEnabled = True
        Me.Linetype1.Location = New System.Drawing.Point(34, 69)
        Me.Linetype1.Name = "Linetype1"
        Me.Linetype1.Size = New System.Drawing.Size(246, 21)
        Me.Linetype1.TabIndex = 34
        '
        'btnOk
        '
        Me.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnOk.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnOk.ForeColor = System.Drawing.Color.Black
        Me.btnOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOk.Location = New System.Drawing.Point(135, 102)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(70, 24)
        Me.btnOk.TabIndex = 35
        Me.btnOk.Text = "&Ok"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(211, 102)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(70, 24)
        Me.btnClose.TabIndex = 36
        Me.btnClose.Text = "&Cancelar"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblSubTitulo
        '
        Me.lblSubTitulo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblSubTitulo.Location = New System.Drawing.Point(31, 33)
        Me.lblSubTitulo.Name = "lblSubTitulo"
        Me.lblSubTitulo.Size = New System.Drawing.Size(249, 33)
        Me.lblSubTitulo.TabIndex = 37
        Me.lblSubTitulo.Text = "Selecione o tipo de linha desejado e clique em OK para selecionar o tipo desejado" & _
            "..."
        '
        'frmLineType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(292, 133)
        Me.Controls.Add(Me.lblSubTitulo)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Linetype1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblDestaque)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmLineType"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LineType - Seleção"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblDestaque As System.Windows.Forms.Label
    Friend WithEvents Linetype1 As System.Windows.Forms.ComboBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblSubTitulo As System.Windows.Forms.Label
End Class

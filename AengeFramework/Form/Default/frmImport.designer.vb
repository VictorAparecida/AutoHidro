<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
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
        Me.btnVisualizar = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblDestaque = New System.Windows.Forms.Label
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.pnlDimens = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.pBar1 = New System.Windows.Forms.ProgressBar
        Me.pnlDimens.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnVisualizar
        '
        Me.btnVisualizar.BackColor = System.Drawing.SystemColors.Control
        Me.btnVisualizar.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnVisualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnVisualizar.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnVisualizar.Location = New System.Drawing.Point(81, 104)
        Me.btnVisualizar.Name = "btnVisualizar"
        Me.btnVisualizar.Size = New System.Drawing.Size(137, 24)
        Me.btnVisualizar.TabIndex = 0
        Me.btnVisualizar.Text = "Visualizar Quadro"
        Me.btnVisualizar.UseVisualStyleBackColor = False
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.SystemColors.Control
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnClose.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnClose.Location = New System.Drawing.Point(224, 104)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(69, 24)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Fechar"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label1.Location = New System.Drawing.Point(40, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(253, 47)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Importação de dados efetuada com sucesso. Clique em ""Ok"" para continuar ou ""Fecha" & _
            "r""  para cancelar a visualização..."
        '
        'lblDestaque
        '
        Me.lblDestaque.BackColor = System.Drawing.Color.DimGray
        Me.lblDestaque.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblDestaque.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDestaque.ForeColor = System.Drawing.Color.White
        Me.lblDestaque.Location = New System.Drawing.Point(12, 21)
        Me.lblDestaque.Name = "lblDestaque"
        Me.lblDestaque.Size = New System.Drawing.Size(54, 18)
        Me.lblDestaque.TabIndex = 10
        Me.lblDestaque.Text = "Cálculos"
        Me.lblDestaque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.Color.Transparent
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.ForeColor = System.Drawing.Color.Navy
        Me.lblTitulo.Location = New System.Drawing.Point(11, 2)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(11, 13)
        Me.lblTitulo.TabIndex = 8
        Me.lblTitulo.Text = "-"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Location = New System.Drawing.Point(61, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(426, 1)
        Me.Panel1.TabIndex = 11
        '
        'Timer1
        '
        '
        'pnlDimens
        '
        Me.pnlDimens.Controls.Add(Me.pBar1)
        Me.pnlDimens.Controls.Add(Me.Label2)
        Me.pnlDimens.Location = New System.Drawing.Point(13, 45)
        Me.pnlDimens.Name = "pnlDimens"
        Me.pnlDimens.Size = New System.Drawing.Size(281, 85)
        Me.pnlDimens.TabIndex = 12
        Me.pnlDimens.Visible = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label2.Location = New System.Drawing.Point(18, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(253, 47)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Aguarde enquanto o Autopower executa os cálculos iniciais do dimensionamento de d" & _
            "ados..."
        '
        'pBar1
        '
        Me.pBar1.Location = New System.Drawing.Point(21, 60)
        Me.pBar1.Name = "pBar1"
        Me.pBar1.Size = New System.Drawing.Size(250, 6)
        Me.pBar1.TabIndex = 4
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(296, 136)
        Me.Controls.Add(Me.pnlDimens)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblDestaque)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnVisualizar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Importação"
        Me.pnlDimens.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnVisualizar As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblDestaque As System.Windows.Forms.Label
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents pnlDimens As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents pBar1 As System.Windows.Forms.ProgressBar
End Class

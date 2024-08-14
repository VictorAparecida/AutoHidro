<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDiagnostic
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDiagnostic))
        Me.trvInfo = New System.Windows.Forms.TreeView
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnFechar = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblDestaque = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'trvInfo
        '
        Me.trvInfo.BackColor = System.Drawing.SystemColors.Control
        Me.trvInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvInfo.Location = New System.Drawing.Point(6, 49)
        Me.trvInfo.Name = "trvInfo"
        Me.trvInfo.Size = New System.Drawing.Size(595, 289)
        Me.trvInfo.TabIndex = 19
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitulo.Location = New System.Drawing.Point(7, 6)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(11, 13)
        Me.lblTitulo.TabIndex = 16
        Me.lblTitulo.Text = "-"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(3, 342)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(519, 39)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = resources.GetString("Label2.Text")
        '
        'btnFechar
        '
        Me.btnFechar.Location = New System.Drawing.Point(528, 353)
        Me.btnFechar.Name = "btnFechar"
        Me.btnFechar.Size = New System.Drawing.Size(73, 23)
        Me.btnFechar.TabIndex = 15
        Me.btnFechar.Text = "&Fechar"
        Me.btnFechar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 28)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(420, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Diagnostico - Informações importantes sobre os procedimentos do Autohidro..."
        '
        'lblDestaque
        '
        Me.lblDestaque.BackColor = System.Drawing.Color.DimGray
        Me.lblDestaque.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblDestaque.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDestaque.ForeColor = System.Drawing.Color.White
        Me.lblDestaque.Location = New System.Drawing.Point(485, 15)
        Me.lblDestaque.Name = "lblDestaque"
        Me.lblDestaque.Size = New System.Drawing.Size(127, 16)
        Me.lblDestaque.TabIndex = 10
        Me.lblDestaque.Text = "DIAGNOSTICO"
        Me.lblDestaque.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.Controls.Add(Me.lblDestaque)
        Me.Panel2.Location = New System.Drawing.Point(-7, -10)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(706, 36)
        Me.Panel2.TabIndex = 18
        '
        'frmDiagnostic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(608, 386)
        Me.Controls.Add(Me.trvInfo)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnFechar)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmDiagnostic"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Informações gerais - Diagnostico de procedimentos..."
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents trvInfo As System.Windows.Forms.TreeView
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnFechar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblDestaque As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTarefa
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
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblSubTit1 = New System.Windows.Forms.Label
        Me.trvInfo = New System.Windows.Forms.TreeView
        Me.btnClose = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label2.Location = New System.Drawing.Point(50, 293)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(368, 29)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "Caso queira visualizar mais detalhes sobre a informação, dê dois cliques na linha" & _
            " desejada..."
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Location = New System.Drawing.Point(158, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(385, 1)
        Me.Panel1.TabIndex = 19
        '
        'lblSubTit1
        '
        Me.lblSubTit1.BackColor = System.Drawing.Color.DimGray
        Me.lblSubTit1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblSubTit1.ForeColor = System.Drawing.Color.White
        Me.lblSubTit1.Location = New System.Drawing.Point(21, 5)
        Me.lblSubTit1.Name = "lblSubTit1"
        Me.lblSubTit1.Size = New System.Drawing.Size(163, 16)
        Me.lblSubTit1.TabIndex = 18
        Me.lblSubTit1.Text = "Informações "
        '
        'trvInfo
        '
        Me.trvInfo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trvInfo.BackColor = System.Drawing.Color.White
        Me.trvInfo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvInfo.Location = New System.Drawing.Point(53, 28)
        Me.trvInfo.Name = "trvInfo"
        Me.trvInfo.Size = New System.Drawing.Size(456, 262)
        Me.trvInfo.TabIndex = 17
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(434, 299)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "Fechar"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmTarefa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(521, 331)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblSubTit1)
        Me.Controls.Add(Me.trvInfo)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmTarefa"
        Me.Opacity = 0.87
        Me.Text = "Agendamento de tarefas - www.autoenge.com.br"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblSubTit1 As System.Windows.Forms.Label
    Friend WithEvents trvInfo As System.Windows.Forms.TreeView
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class

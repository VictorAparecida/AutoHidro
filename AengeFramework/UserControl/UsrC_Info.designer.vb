<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UsrC_Info
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lblTit = New System.Windows.Forms.Label
        Me.dgRegister = New System.Windows.Forms.DataGridView
        CType(Me.dgRegister, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTit
        '
        Me.lblTit.AutoSize = True
        Me.lblTit.Location = New System.Drawing.Point(6, 4)
        Me.lblTit.Name = "lblTit"
        Me.lblTit.Size = New System.Drawing.Size(213, 13)
        Me.lblTit.TabIndex = 3
        Me.lblTit.Text = "Registros cadastrados no banco de dados :"
        '
        'dgRegister
        '
        Me.dgRegister.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgRegister.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgRegister.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.dgRegister.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgRegister.Location = New System.Drawing.Point(3, 26)
        Me.dgRegister.Name = "dgRegister"
        Me.dgRegister.ReadOnly = True
        Me.dgRegister.Size = New System.Drawing.Size(238, 440)
        Me.dgRegister.TabIndex = 2
        '
        'UsrC_Info
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblTit)
        Me.Controls.Add(Me.dgRegister)
        Me.Name = "UsrC_Info"
        Me.Size = New System.Drawing.Size(244, 470)
        CType(Me.dgRegister, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblTit As System.Windows.Forms.Label
    Friend WithEvents dgRegister As System.Windows.Forms.DataGridView

End Class

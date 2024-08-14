<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLegendaNum
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSelect = New System.Windows.Forms.Button
        Me.GridRegister = New System.Windows.Forms.DataGridView
        Me.IsSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.chkMarq = New System.Windows.Forms.CheckBox
        CType(Me.GridRegister, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(8, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Inserção de legenda - "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(126, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(312, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Selecione os objetos que deverão ser inseridos na legenda."
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(422, 237)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(86, 26)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "&Cancelar"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Location = New System.Drawing.Point(330, 237)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(86, 26)
        Me.btnSelect.TabIndex = 4
        Me.btnSelect.Text = "&Ok"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'GridRegister
        '
        Me.GridRegister.AllowUserToAddRows = False
        Me.GridRegister.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.Khaki
        Me.GridRegister.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.GridRegister.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridRegister.BackgroundColor = System.Drawing.SystemColors.Control
        Me.GridRegister.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.GridRegister.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.GridRegister.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IsSelect})
        Me.GridRegister.Location = New System.Drawing.Point(11, 24)
        Me.GridRegister.Name = "GridRegister"
        Me.GridRegister.Size = New System.Drawing.Size(497, 207)
        Me.GridRegister.TabIndex = 5
        '
        'IsSelect
        '
        Me.IsSelect.HeaderText = ""
        Me.IsSelect.Name = "IsSelect"
        Me.IsSelect.Width = 30
        '
        'chkMarq
        '
        Me.chkMarq.AutoSize = True
        Me.chkMarq.Checked = True
        Me.chkMarq.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkMarq.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkMarq.Location = New System.Drawing.Point(11, 240)
        Me.chkMarq.Name = "chkMarq"
        Me.chkMarq.Size = New System.Drawing.Size(158, 17)
        Me.chkMarq.TabIndex = 6
        Me.chkMarq.Text = "Marcar todos os objetos..."
        Me.chkMarq.UseVisualStyleBackColor = True
        '
        'frmLegendaNum
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(516, 269)
        Me.Controls.Add(Me.chkMarq)
        Me.Controls.Add(Me.GridRegister)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmLegendaNum"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Seleção de objetos..."
        CType(Me.GridRegister, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents GridRegister As System.Windows.Forms.DataGridView
    Friend WithEvents IsSelect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents chkMarq As System.Windows.Forms.CheckBox
End Class

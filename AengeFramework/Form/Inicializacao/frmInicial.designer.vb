<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInicial
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInicial))
        Me.rdoClose = New System.Windows.Forms.RadioButton
        Me.rdoCad = New System.Windows.Forms.RadioButton
        Me.rdoOpen = New System.Windows.Forms.RadioButton
        Me.rdoNew = New System.Windows.Forms.RadioButton
        Me.btnHelp = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label2 = New System.Windows.Forms.Label
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'rdoClose
        '
        Me.rdoClose.AutoSize = True
        Me.rdoClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.rdoClose.Location = New System.Drawing.Point(61, 116)
        Me.rdoClose.Name = "rdoClose"
        Me.rdoClose.Size = New System.Drawing.Size(43, 17)
        Me.rdoClose.TabIndex = 4
        Me.rdoClose.TabStop = True
        Me.rdoClose.Text = "Sair"
        Me.rdoClose.UseVisualStyleBackColor = True
        '
        'rdoCad
        '
        Me.rdoCad.AutoSize = True
        Me.rdoCad.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.rdoCad.Location = New System.Drawing.Point(61, 93)
        Me.rdoCad.Name = "rdoCad"
        Me.rdoCad.Size = New System.Drawing.Size(91, 17)
        Me.rdoCad.TabIndex = 3
        Me.rdoCad.TabStop = True
        Me.rdoCad.Text = "Carregar Cad"
        Me.rdoCad.UseVisualStyleBackColor = True
        '
        'rdoOpen
        '
        Me.rdoOpen.AutoSize = True
        Me.rdoOpen.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.rdoOpen.Location = New System.Drawing.Point(61, 70)
        Me.rdoOpen.Name = "rdoOpen"
        Me.rdoOpen.Size = New System.Drawing.Size(140, 17)
        Me.rdoOpen.TabIndex = 2
        Me.rdoOpen.TabStop = True
        Me.rdoOpen.Text = "Desenho já existente..."
        Me.rdoOpen.UseVisualStyleBackColor = True
        '
        'rdoNew
        '
        Me.rdoNew.AutoSize = True
        Me.rdoNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.rdoNew.Location = New System.Drawing.Point(61, 47)
        Me.rdoNew.Name = "rdoNew"
        Me.rdoNew.Size = New System.Drawing.Size(108, 17)
        Me.rdoNew.TabIndex = 1
        Me.rdoNew.TabStop = True
        Me.rdoNew.Text = "Novo desenho..."
        Me.rdoNew.UseVisualStyleBackColor = True
        '
        'btnHelp
        '
        Me.btnHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnHelp.Location = New System.Drawing.Point(209, 154)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(57, 25)
        Me.btnHelp.TabIndex = 7
        Me.btnHelp.Text = "&Ajuda"
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnCancel.Location = New System.Drawing.Point(133, 154)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 25)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "&Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnOk.Location = New System.Drawing.Point(8, 154)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(70, 25)
        Me.btnOk.TabIndex = 5
        Me.btnOk.Text = "&Ok"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(58, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(207, 18)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Selecione a opção abaixo ..."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(58, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(208, 13)
        Me.Label4.TabIndex = 43
        Me.Label4.Text = "Autoenge - Softwares para engenharia"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(8, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(43, 43)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 42
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Gainsboro
        Me.Label2.Location = New System.Drawing.Point(16, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(250, 15)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "_________________________________________________________________________________" & _
            "_"
        '
        'frmInicial
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(278, 185)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.rdoClose)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.rdoCad)
        Me.Controls.Add(Me.rdoOpen)
        Me.Controls.Add(Me.rdoNew)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmInicial"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Iniciando os aplicativos Autoenge"
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rdoClose As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCad As System.Windows.Forms.RadioButton
    Friend WithEvents rdoOpen As System.Windows.Forms.RadioButton
    Friend WithEvents rdoNew As System.Windows.Forms.RadioButton
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class

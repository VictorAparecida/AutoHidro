<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfirm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfirm))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblNameDwg = New System.Windows.Forms.Label
        Me.lblProject = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnFechar = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblNameDwgNew = New System.Windows.Forms.Label
        Me.lblProjectNew = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblDestaque = New System.Windows.Forms.Label
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(37, 51)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 22
        Me.PictureBox1.TabStop = False
        '
        'lblNameDwg
        '
        Me.lblNameDwg.AutoSize = True
        Me.lblNameDwg.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblNameDwg.ForeColor = System.Drawing.Color.Maroon
        Me.lblNameDwg.Location = New System.Drawing.Point(132, 134)
        Me.lblNameDwg.Name = "lblNameDwg"
        Me.lblNameDwg.Size = New System.Drawing.Size(11, 13)
        Me.lblNameDwg.TabIndex = 21
        Me.lblNameDwg.Text = "-"
        '
        'lblProject
        '
        Me.lblProject.AutoSize = True
        Me.lblProject.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblProject.ForeColor = System.Drawing.Color.Maroon
        Me.lblProject.Location = New System.Drawing.Point(132, 115)
        Me.lblProject.Name = "lblProject"
        Me.lblProject.Size = New System.Drawing.Size(11, 13)
        Me.lblProject.TabIndex = 20
        Me.lblProject.Text = "-"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label4.Location = New System.Drawing.Point(80, 134)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Nome :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label3.Location = New System.Drawing.Point(80, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Projeto :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label2.Location = New System.Drawing.Point(59, 99)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(195, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Informações sobre o desenho atual :"
        '
        'btnFechar
        '
        Me.btnFechar.BackColor = System.Drawing.SystemColors.Control
        Me.btnFechar.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnFechar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnFechar.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnFechar.Location = New System.Drawing.Point(335, 215)
        Me.btnFechar.Name = "btnFechar"
        Me.btnFechar.Size = New System.Drawing.Size(79, 25)
        Me.btnFechar.TabIndex = 16
        Me.btnFechar.Text = "Cancelar"
        Me.btnFechar.UseVisualStyleBackColor = False
        '
        'btnOk
        '
        Me.btnOk.BackColor = System.Drawing.SystemColors.Control
        Me.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.Silver
        Me.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnOk.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnOk.Location = New System.Drawing.Point(196, 215)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(133, 25)
        Me.btnOk.TabIndex = 15
        Me.btnOk.Text = "Carregar projeto"
        Me.btnOk.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label1.Location = New System.Drawing.Point(59, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(356, 45)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "O desenho selecionado está relacionado a um projeto diferente do atual. Para carr" & _
            "egá-lo, será necessário fechar todos os desenhos relacionados ao projeto corrent" & _
            "e. Deseja continuar?"
        '
        'lblNameDwgNew
        '
        Me.lblNameDwgNew.AutoSize = True
        Me.lblNameDwgNew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblNameDwgNew.ForeColor = System.Drawing.Color.Navy
        Me.lblNameDwgNew.Location = New System.Drawing.Point(132, 198)
        Me.lblNameDwgNew.Name = "lblNameDwgNew"
        Me.lblNameDwgNew.Size = New System.Drawing.Size(11, 13)
        Me.lblNameDwgNew.TabIndex = 27
        Me.lblNameDwgNew.Text = "-"
        '
        'lblProjectNew
        '
        Me.lblProjectNew.AutoSize = True
        Me.lblProjectNew.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblProjectNew.ForeColor = System.Drawing.Color.Navy
        Me.lblProjectNew.Location = New System.Drawing.Point(132, 179)
        Me.lblProjectNew.Name = "lblProjectNew"
        Me.lblProjectNew.Size = New System.Drawing.Size(11, 13)
        Me.lblProjectNew.TabIndex = 26
        Me.lblProjectNew.Text = "-"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label7.Location = New System.Drawing.Point(80, 198)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(43, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Nome :"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label8.Location = New System.Drawing.Point(80, 179)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 13)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "Projeto :"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label9.Location = New System.Drawing.Point(59, 163)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(248, 13)
        Me.Label9.TabIndex = 23
        Me.Label9.Text = "Informações sobre o desenho a ser carregado :"
        '
        'lblDestaque
        '
        Me.lblDestaque.BackColor = System.Drawing.Color.DimGray
        Me.lblDestaque.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblDestaque.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDestaque.ForeColor = System.Drawing.Color.White
        Me.lblDestaque.Location = New System.Drawing.Point(8, 23)
        Me.lblDestaque.Name = "lblDestaque"
        Me.lblDestaque.Size = New System.Drawing.Size(141, 18)
        Me.lblDestaque.TabIndex = 30
        Me.lblDestaque.Text = "Seleção de desenhos"
        Me.lblDestaque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.Color.Transparent
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.ForeColor = System.Drawing.Color.Navy
        Me.lblTitulo.Location = New System.Drawing.Point(8, 4)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(11, 13)
        Me.lblTitulo.TabIndex = 28
        Me.lblTitulo.Text = "-"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Location = New System.Drawing.Point(138, 40)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(426, 1)
        Me.Panel1.TabIndex = 31
        '
        'frmConfirm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(424, 245)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblDestaque)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.lblNameDwgNew)
        Me.Controls.Add(Me.lblProjectNew)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblNameDwg)
        Me.Controls.Add(Me.lblProject)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnFechar)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmConfirm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Confirmação"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblNameDwg As System.Windows.Forms.Label
    Friend WithEvents lblProject As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnFechar As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblNameDwgNew As System.Windows.Forms.Label
    Friend WithEvents lblProjectNew As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblDestaque As System.Windows.Forms.Label
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class

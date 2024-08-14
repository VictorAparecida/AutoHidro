<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExisteDesenho
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExisteDesenho))
        Me.lblInfo = New System.Windows.Forms.Label
        Me.PicNoImg = New System.Windows.Forms.Panel
        Me.TrvProjeto = New System.Windows.Forms.TreeView
        Me.txtDesenhoSel = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtProjetoSel = New System.Windows.Forms.Label
        Me.btnHelp = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.txtQtdDesenho = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtLocalizacao = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Dwg = New System.Windows.Forms.PictureBox
        Me.btnOk = New System.Windows.Forms.Button
        Me.btnConfig = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        CType(Me.Dwg, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblInfo
        '
        Me.lblInfo.BackColor = System.Drawing.Color.Maroon
        Me.lblInfo.ForeColor = System.Drawing.Color.White
        Me.lblInfo.Location = New System.Drawing.Point(215, 139)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(301, 15)
        Me.lblInfo.TabIndex = 27
        Me.lblInfo.Text = "Dwg não pode ser visualizado !"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblInfo.Visible = False
        '
        'PicNoImg
        '
        Me.PicNoImg.BackColor = System.Drawing.Color.White
        Me.PicNoImg.Location = New System.Drawing.Point(207, 71)
        Me.PicNoImg.Name = "PicNoImg"
        Me.PicNoImg.Size = New System.Drawing.Size(316, 152)
        Me.PicNoImg.TabIndex = 29
        Me.PicNoImg.Visible = False
        '
        'TrvProjeto
        '
        Me.TrvProjeto.Location = New System.Drawing.Point(11, 71)
        Me.TrvProjeto.Name = "TrvProjeto"
        Me.TrvProjeto.Size = New System.Drawing.Size(188, 277)
        Me.TrvProjeto.TabIndex = 1
        '
        'txtDesenhoSel
        '
        Me.txtDesenhoSel.AutoSize = True
        Me.txtDesenhoSel.ForeColor = System.Drawing.Color.Navy
        Me.txtDesenhoSel.Location = New System.Drawing.Point(333, 262)
        Me.txtDesenhoSel.Name = "txtDesenhoSel"
        Me.txtDesenhoSel.Size = New System.Drawing.Size(11, 13)
        Me.txtDesenhoSel.TabIndex = 25
        Me.txtDesenhoSel.Text = "-"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(204, 262)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(124, 13)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "Desenho selecionado :"
        '
        'txtProjetoSel
        '
        Me.txtProjetoSel.AutoSize = True
        Me.txtProjetoSel.ForeColor = System.Drawing.Color.Navy
        Me.txtProjetoSel.Location = New System.Drawing.Point(333, 245)
        Me.txtProjetoSel.Name = "txtProjetoSel"
        Me.txtProjetoSel.Size = New System.Drawing.Size(11, 13)
        Me.txtProjetoSel.TabIndex = 19
        Me.txtProjetoSel.Text = "-"
        '
        'btnHelp
        '
        Me.btnHelp.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnHelp.Location = New System.Drawing.Point(449, 354)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(74, 25)
        Me.btnHelp.TabIndex = 4
        Me.btnHelp.Text = "&Ajuda"
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnCancel.Location = New System.Drawing.Point(365, 354)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(77, 25)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "&Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtQtdDesenho
        '
        Me.txtQtdDesenho.AutoSize = True
        Me.txtQtdDesenho.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtQtdDesenho.Location = New System.Drawing.Point(407, 281)
        Me.txtQtdDesenho.Name = "txtQtdDesenho"
        Me.txtQtdDesenho.Size = New System.Drawing.Size(13, 13)
        Me.txtQtdDesenho.TabIndex = 23
        Me.txtQtdDesenho.Text = "0"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(204, 280)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(204, 13)
        Me.Label10.TabIndex = 22
        Me.Label10.Text = "Quantidade de desenhos do projeto : "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Navy
        Me.Label9.Location = New System.Drawing.Point(204, 226)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(133, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Dados sobre o arquivo : "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(204, 245)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Projeto selecionado :"
        '
        'txtLocalizacao
        '
        Me.txtLocalizacao.BackColor = System.Drawing.SystemColors.Control
        Me.txtLocalizacao.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLocalizacao.ForeColor = System.Drawing.Color.Black
        Me.txtLocalizacao.Location = New System.Drawing.Point(276, 299)
        Me.txtLocalizacao.Multiline = True
        Me.txtLocalizacao.Name = "txtLocalizacao"
        Me.txtLocalizacao.Size = New System.Drawing.Size(246, 49)
        Me.txtLocalizacao.TabIndex = 20
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(204, 298)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 18
        Me.Label5.Text = "Localização :"
        '
        'Dwg
        '
        Me.Dwg.BackColor = System.Drawing.Color.White
        Me.Dwg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Dwg.Location = New System.Drawing.Point(207, 71)
        Me.Dwg.Name = "Dwg"
        Me.Dwg.Size = New System.Drawing.Size(316, 152)
        Me.Dwg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Dwg.TabIndex = 31
        Me.Dwg.TabStop = False
        '
        'btnOk
        '
        Me.btnOk.Enabled = False
        Me.btnOk.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnOk.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnOk.Location = New System.Drawing.Point(282, 354)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(77, 25)
        Me.btnOk.TabIndex = 2
        Me.btnOk.Text = "&Ok"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnConfig
        '
        Me.btnConfig.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray
        Me.btnConfig.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnConfig.Location = New System.Drawing.Point(12, 354)
        Me.btnConfig.Name = "btnConfig"
        Me.btnConfig.Size = New System.Drawing.Size(99, 25)
        Me.btnConfig.TabIndex = 17
        Me.btnConfig.Text = "C&onfigurações"
        Me.btnConfig.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(60, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(463, 28)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Abertura de projetos e desenhos já cadastrados no sistema. Selecione abaixo o reg" & _
            "istro de desenho desejado..."
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(11, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(43, 43)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 23
        Me.PictureBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(12, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 13)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Projetos cadastrados :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(60, 3)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(208, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Autoenge - Softwares para engenharia"
        '
        'frmExisteDesenho
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(534, 385)
        Me.Controls.Add(Me.TrvProjeto)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.PicNoImg)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtDesenhoSel)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.btnConfig)
        Me.Controls.Add(Me.txtProjetoSel)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.txtQtdDesenho)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtLocalizacao)
        Me.Controls.Add(Me.Dwg)
        Me.Controls.Add(Me.Label5)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmExisteDesenho"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Autoenge - Abrir um desenho existente..."
        Me.TopMost = True
        CType(Me.Dwg, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents PicNoImg As System.Windows.Forms.Panel
    Friend WithEvents TrvProjeto As System.Windows.Forms.TreeView
    Friend WithEvents txtDesenhoSel As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtProjetoSel As System.Windows.Forms.Label
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtQtdDesenho As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtLocalizacao As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Dwg As System.Windows.Forms.PictureBox
    Friend WithEvents btnConfig As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class

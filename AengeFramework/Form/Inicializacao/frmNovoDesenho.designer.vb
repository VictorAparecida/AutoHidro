<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNovoDesenho
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNovoDesenho))
        Me.btnHelp = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.txtProjeto = New System.Windows.Forms.TextBox
        Me.trvProjeto = New System.Windows.Forms.TreeView
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboMedida = New System.Windows.Forms.ComboBox
        Me.cboEscala = New System.Windows.Forms.ComboBox
        Me.txtEscala = New System.Windows.Forms.TextBox
        Me.cboFormato = New System.Windows.Forms.ComboBox
        Me.txtDesenho = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtProjetoSel = New System.Windows.Forms.Label
        Me.txtQtdDesenho = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtLocalizacao = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnHelp
        '
        Me.btnHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnHelp.Location = New System.Drawing.Point(406, 317)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(75, 25)
        Me.btnHelp.TabIndex = 10
        Me.btnHelp.Text = "&Ajuda"
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnCancel.Location = New System.Drawing.Point(322, 317)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(77, 25)
        Me.btnCancel.TabIndex = 9
        Me.btnCancel.Text = "&Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnOk.Location = New System.Drawing.Point(201, 317)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(77, 25)
        Me.btnOk.TabIndex = 8
        Me.btnOk.Text = "&Ok"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'txtProjeto
        '
        Me.txtProjeto.Location = New System.Drawing.Point(314, 77)
        Me.txtProjeto.MaxLength = 8
        Me.txtProjeto.Multiline = True
        Me.txtProjeto.Name = "txtProjeto"
        Me.txtProjeto.Size = New System.Drawing.Size(168, 20)
        Me.txtProjeto.TabIndex = 1
        '
        'trvProjeto
        '
        Me.trvProjeto.Location = New System.Drawing.Point(7, 78)
        Me.trvProjeto.Name = "trvProjeto"
        Me.trvProjeto.Size = New System.Drawing.Size(180, 264)
        Me.trvProjeto.TabIndex = 11
        Me.trvProjeto.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(198, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Novo projeto :"
        '
        'cboMedida
        '
        Me.cboMedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMedida.FormattingEnabled = True
        Me.cboMedida.Location = New System.Drawing.Point(314, 170)
        Me.cboMedida.Name = "cboMedida"
        Me.cboMedida.Size = New System.Drawing.Size(168, 21)
        Me.cboMedida.TabIndex = 7
        '
        'cboEscala
        '
        Me.cboEscala.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboEscala.FormattingEnabled = True
        Me.cboEscala.Location = New System.Drawing.Point(365, 146)
        Me.cboEscala.Name = "cboEscala"
        Me.cboEscala.Size = New System.Drawing.Size(117, 21)
        Me.cboEscala.TabIndex = 6
        '
        'txtEscala
        '
        Me.txtEscala.Enabled = False
        Me.txtEscala.Location = New System.Drawing.Point(314, 146)
        Me.txtEscala.Multiline = True
        Me.txtEscala.Name = "txtEscala"
        Me.txtEscala.Size = New System.Drawing.Size(49, 20)
        Me.txtEscala.TabIndex = 5
        Me.txtEscala.Text = "1 :"
        Me.txtEscala.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cboFormato
        '
        Me.cboFormato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFormato.FormattingEnabled = True
        Me.cboFormato.Location = New System.Drawing.Point(314, 122)
        Me.cboFormato.Name = "cboFormato"
        Me.cboFormato.Size = New System.Drawing.Size(168, 21)
        Me.cboFormato.TabIndex = 4
        '
        'txtDesenho
        '
        Me.txtDesenho.Location = New System.Drawing.Point(314, 99)
        Me.txtDesenho.Multiline = True
        Me.txtDesenho.Name = "txtDesenho"
        Me.txtDesenho.Size = New System.Drawing.Size(168, 20)
        Me.txtDesenho.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(198, 170)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(114, 13)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "Unidade de medida :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(198, 146)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(109, 13)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Escala do desenho :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(198, 123)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(105, 13)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Formato do papel :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(198, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(108, 13)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Nome do desenho :"
        '
        'txtProjetoSel
        '
        Me.txtProjetoSel.AutoSize = True
        Me.txtProjetoSel.ForeColor = System.Drawing.Color.Navy
        Me.txtProjetoSel.Location = New System.Drawing.Point(315, 217)
        Me.txtProjetoSel.Name = "txtProjetoSel"
        Me.txtProjetoSel.Size = New System.Drawing.Size(11, 13)
        Me.txtProjetoSel.TabIndex = 17
        Me.txtProjetoSel.Text = "-"
        '
        'txtQtdDesenho
        '
        Me.txtQtdDesenho.AutoSize = True
        Me.txtQtdDesenho.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txtQtdDesenho.Location = New System.Drawing.Point(403, 235)
        Me.txtQtdDesenho.Name = "txtQtdDesenho"
        Me.txtQtdDesenho.Size = New System.Drawing.Size(13, 13)
        Me.txtQtdDesenho.TabIndex = 21
        Me.txtQtdDesenho.Text = "0"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(198, 234)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(204, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Quantidade de desenhos do projeto : "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Navy
        Me.Label9.Location = New System.Drawing.Point(198, 199)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(133, 13)
        Me.Label9.TabIndex = 19
        Me.Label9.Text = "Dados sobre o arquivo : "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(198, 217)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 15
        Me.Label2.Text = "Projeto selecionado :"
        '
        'txtLocalizacao
        '
        Me.txtLocalizacao.BackColor = System.Drawing.SystemColors.Control
        Me.txtLocalizacao.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLocalizacao.Location = New System.Drawing.Point(275, 253)
        Me.txtLocalizacao.Multiline = True
        Me.txtLocalizacao.Name = "txtLocalizacao"
        Me.txtLocalizacao.Size = New System.Drawing.Size(207, 59)
        Me.txtLocalizacao.TabIndex = 18
        Me.txtLocalizacao.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(198, 252)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Localização :"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(56, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(208, 13)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Autoenge - Softwares para engenharia"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(7, 6)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(43, 43)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 35
        Me.PictureBox1.TabStop = False
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(56, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(425, 28)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "Criação de projetos e desenhos. Informe os dados abaixo para criar um novo projet" & _
            "o / desenho na aplicação..."
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.Navy
        Me.Label12.Location = New System.Drawing.Point(6, 58)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(129, 13)
        Me.Label12.TabIndex = 37
        Me.Label12.Text = "Dados sobre o projeto :"
        '
        'frmNovoDesenho
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 347)
        Me.Controls.Add(Me.trvProjeto)
        Me.Controls.Add(Me.cboMedida)
        Me.Controls.Add(Me.txtProjeto)
        Me.Controls.Add(Me.cboEscala)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtEscala)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.cboFormato)
        Me.Controls.Add(Me.txtDesenho)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.txtProjetoSel)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.txtQtdDesenho)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtLocalizacao)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmNovoDesenho"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Autoenge - Novo desenho"
        Me.TopMost = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtProjetoSel As System.Windows.Forms.Label
    Friend WithEvents txtQtdDesenho As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtLocalizacao As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtEscala As System.Windows.Forms.TextBox
    Friend WithEvents cboFormato As System.Windows.Forms.ComboBox
    Friend WithEvents txtDesenho As System.Windows.Forms.TextBox
    Friend WithEvents cboMedida As System.Windows.Forms.ComboBox
    Friend WithEvents cboEscala As System.Windows.Forms.ComboBox
    Friend WithEvents txtProjeto As System.Windows.Forms.TextBox
    Friend WithEvents trvProjeto As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
End Class

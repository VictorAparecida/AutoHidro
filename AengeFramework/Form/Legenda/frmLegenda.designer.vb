<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLegenda
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLegenda))
        Me.LblTitulo = New System.Windows.Forms.Label
        Me.lblSubTitulo = New System.Windows.Forms.Label
        Me.grpEletrical = New System.Windows.Forms.GroupBox
        Me.btnSelecao = New System.Windows.Forms.Button
        Me.listLayer = New System.Windows.Forms.ListBox
        Me.rdoTomada = New System.Windows.Forms.CheckBox
        Me.optLayer = New System.Windows.Forms.RadioButton
        Me.optSelecao = New System.Windows.Forms.RadioButton
        Me.optTelefonia = New System.Windows.Forms.RadioButton
        Me.optEletrica = New System.Windows.Forms.RadioButton
        Me.optTodos = New System.Windows.Forms.RadioButton
        Me.grpTubulacao = New System.Windows.Forms.GroupBox
        Me.Desc3 = New System.Windows.Forms.TextBox
        Me.Desc2 = New System.Windows.Forms.TextBox
        Me.Desc1 = New System.Windows.Forms.TextBox
        Me.Linetype3 = New System.Windows.Forms.ComboBox
        Me.Linetype2 = New System.Windows.Forms.ComboBox
        Me.Linetype1 = New System.Windows.Forms.ComboBox
        Me.colortub3 = New System.Windows.Forms.TextBox
        Me.colortub2 = New System.Windows.Forms.TextBox
        Me.colortub1 = New System.Windows.Forms.TextBox
        Me.btnCor3 = New System.Windows.Forms.Button
        Me.btnCor2 = New System.Windows.Forms.Button
        Me.btnCor1 = New System.Windows.Forms.Button
        Me.rdoTubulacaoin = New System.Windows.Forms.CheckBox
        Me.rdoTubulacao = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtTitulo = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtObservacao = New System.Windows.Forms.RichTextBox
        Me.cmdDesenhar = New System.Windows.Forms.Button
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdHelp = New System.Windows.Forms.Button
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.chkLegenda = New System.Windows.Forms.CheckBox
        Me.grpEletrical.SuspendLayout()
        Me.grpTubulacao.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblTitulo
        '
        Me.LblTitulo.AutoSize = True
        Me.LblTitulo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.LblTitulo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.LblTitulo.Location = New System.Drawing.Point(43, -1)
        Me.LblTitulo.Name = "LblTitulo"
        Me.LblTitulo.Size = New System.Drawing.Size(116, 13)
        Me.LblTitulo.TabIndex = 0
        Me.LblTitulo.Text = "Inserção de legendas"
        '
        'lblSubTitulo
        '
        Me.lblSubTitulo.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblSubTitulo.Location = New System.Drawing.Point(43, 14)
        Me.lblSubTitulo.Name = "lblSubTitulo"
        Me.lblSubTitulo.Size = New System.Drawing.Size(366, 42)
        Me.lblSubTitulo.TabIndex = 1
        Me.lblSubTitulo.Text = "Informe os parâmetros abaixo corretamente para que seja possível executar a inser" & _
            "ção da legenda dos objetos que se encontram cadastrados em seu projeto..."
        '
        'grpEletrical
        '
        Me.grpEletrical.Controls.Add(Me.btnSelecao)
        Me.grpEletrical.Controls.Add(Me.listLayer)
        Me.grpEletrical.Controls.Add(Me.rdoTomada)
        Me.grpEletrical.Controls.Add(Me.optLayer)
        Me.grpEletrical.Controls.Add(Me.optSelecao)
        Me.grpEletrical.Controls.Add(Me.optTelefonia)
        Me.grpEletrical.Controls.Add(Me.optEletrica)
        Me.grpEletrical.Controls.Add(Me.optTodos)
        Me.grpEletrical.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.grpEletrical.Location = New System.Drawing.Point(7, 57)
        Me.grpEletrical.Name = "grpEletrical"
        Me.grpEletrical.Size = New System.Drawing.Size(401, 104)
        Me.grpEletrical.TabIndex = 2
        Me.grpEletrical.TabStop = False
        Me.grpEletrical.Text = "Seleção de objetos"
        '
        'btnSelecao
        '
        Me.btnSelecao.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnSelecao.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnSelecao.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.btnSelecao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSelecao.Location = New System.Drawing.Point(249, 12)
        Me.btnSelecao.Name = "btnSelecao"
        Me.btnSelecao.Size = New System.Drawing.Size(144, 24)
        Me.btnSelecao.TabIndex = 9
        Me.btnSelecao.Text = ">> Selecione"
        Me.btnSelecao.UseVisualStyleBackColor = True
        '
        'listLayer
        '
        Me.listLayer.FormattingEnabled = True
        Me.listLayer.Location = New System.Drawing.Point(147, 41)
        Me.listLayer.Name = "listLayer"
        Me.listLayer.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple
        Me.listLayer.Size = New System.Drawing.Size(246, 56)
        Me.listLayer.TabIndex = 6
        '
        'rdoTomada
        '
        Me.rdoTomada.AutoSize = True
        Me.rdoTomada.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.rdoTomada.Location = New System.Drawing.Point(233, -15)
        Me.rdoTomada.Name = "rdoTomada"
        Me.rdoTomada.Size = New System.Drawing.Size(161, 17)
        Me.rdoTomada.TabIndex = 5
        Me.rdoTomada.Text = "Incluir tomadas específicas"
        Me.rdoTomada.UseVisualStyleBackColor = True
        Me.rdoTomada.Visible = False
        '
        'optLayer
        '
        Me.optLayer.AutoSize = True
        Me.optLayer.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.optLayer.Location = New System.Drawing.Point(16, 39)
        Me.optLayer.Name = "optLayer"
        Me.optLayer.Size = New System.Drawing.Size(125, 17)
        Me.optLayer.TabIndex = 4
        Me.optLayer.TabStop = True
        Me.optLayer.Text = "Somente dos Layers"
        Me.optLayer.UseVisualStyleBackColor = True
        '
        'optSelecao
        '
        Me.optSelecao.AutoSize = True
        Me.optSelecao.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.optSelecao.Location = New System.Drawing.Point(117, 16)
        Me.optSelecao.Name = "optSelecao"
        Me.optSelecao.Size = New System.Drawing.Size(127, 17)
        Me.optSelecao.TabIndex = 3
        Me.optSelecao.TabStop = True
        Me.optSelecao.Text = "Somente da Seleção"
        Me.optSelecao.UseVisualStyleBackColor = True
        '
        'optTelefonia
        '
        Me.optTelefonia.AutoSize = True
        Me.optTelefonia.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.optTelefonia.Location = New System.Drawing.Point(319, -16)
        Me.optTelefonia.Name = "optTelefonia"
        Me.optTelefonia.Size = New System.Drawing.Size(71, 17)
        Me.optTelefonia.TabIndex = 2
        Me.optTelefonia.TabStop = True
        Me.optTelefonia.Text = "Telefonia"
        Me.optTelefonia.UseVisualStyleBackColor = True
        Me.optTelefonia.Visible = False
        '
        'optEletrica
        '
        Me.optEletrica.AutoSize = True
        Me.optEletrica.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.optEletrica.Location = New System.Drawing.Point(262, -15)
        Me.optEletrica.Name = "optEletrica"
        Me.optEletrica.Size = New System.Drawing.Size(61, 17)
        Me.optEletrica.TabIndex = 1
        Me.optEletrica.TabStop = True
        Me.optEletrica.Text = "Elétrica"
        Me.optEletrica.UseVisualStyleBackColor = True
        Me.optEletrica.Visible = False
        '
        'optTodos
        '
        Me.optTodos.AutoSize = True
        Me.optTodos.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.optTodos.Location = New System.Drawing.Point(16, 16)
        Me.optTodos.Name = "optTodos"
        Me.optTodos.Size = New System.Drawing.Size(55, 17)
        Me.optTodos.TabIndex = 0
        Me.optTodos.TabStop = True
        Me.optTodos.Text = "Todos"
        Me.optTodos.UseVisualStyleBackColor = True
        '
        'grpTubulacao
        '
        Me.grpTubulacao.Controls.Add(Me.Desc3)
        Me.grpTubulacao.Controls.Add(Me.Desc2)
        Me.grpTubulacao.Controls.Add(Me.Desc1)
        Me.grpTubulacao.Controls.Add(Me.Linetype3)
        Me.grpTubulacao.Controls.Add(Me.Linetype2)
        Me.grpTubulacao.Controls.Add(Me.Linetype1)
        Me.grpTubulacao.Controls.Add(Me.colortub3)
        Me.grpTubulacao.Controls.Add(Me.colortub2)
        Me.grpTubulacao.Controls.Add(Me.colortub1)
        Me.grpTubulacao.Controls.Add(Me.btnCor3)
        Me.grpTubulacao.Controls.Add(Me.btnCor2)
        Me.grpTubulacao.Controls.Add(Me.btnCor1)
        Me.grpTubulacao.Controls.Add(Me.rdoTubulacaoin)
        Me.grpTubulacao.Controls.Add(Me.rdoTubulacao)
        Me.grpTubulacao.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.grpTubulacao.Location = New System.Drawing.Point(7, 167)
        Me.grpTubulacao.Name = "grpTubulacao"
        Me.grpTubulacao.Size = New System.Drawing.Size(401, 152)
        Me.grpTubulacao.TabIndex = 5
        Me.grpTubulacao.TabStop = False
        Me.grpTubulacao.Text = "Indicação de tubulação"
        '
        'Desc3
        '
        Me.Desc3.ForeColor = System.Drawing.Color.DarkGray
        Me.Desc3.Location = New System.Drawing.Point(16, 120)
        Me.Desc3.Name = "Desc3"
        Me.Desc3.Size = New System.Drawing.Size(161, 22)
        Me.Desc3.TabIndex = 13
        Me.Desc3.Text = "Digite a descrição para liberar..."
        '
        'Desc2
        '
        Me.Desc2.ForeColor = System.Drawing.Color.DarkGray
        Me.Desc2.Location = New System.Drawing.Point(16, 92)
        Me.Desc2.Name = "Desc2"
        Me.Desc2.Size = New System.Drawing.Size(161, 22)
        Me.Desc2.TabIndex = 10
        Me.Desc2.Text = "Digite a descrição para liberar..."
        '
        'Desc1
        '
        Me.Desc1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.Desc1.ForeColor = System.Drawing.Color.DarkGray
        Me.Desc1.Location = New System.Drawing.Point(16, 65)
        Me.Desc1.Name = "Desc1"
        Me.Desc1.Size = New System.Drawing.Size(161, 22)
        Me.Desc1.TabIndex = 7
        Me.Desc1.Text = "Digite a descrição para liberar..."
        '
        'Linetype3
        '
        Me.Linetype3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Linetype3.FormattingEnabled = True
        Me.Linetype3.Location = New System.Drawing.Point(238, 120)
        Me.Linetype3.Name = "Linetype3"
        Me.Linetype3.Size = New System.Drawing.Size(155, 21)
        Me.Linetype3.TabIndex = 15
        '
        'Linetype2
        '
        Me.Linetype2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Linetype2.FormattingEnabled = True
        Me.Linetype2.Location = New System.Drawing.Point(238, 93)
        Me.Linetype2.Name = "Linetype2"
        Me.Linetype2.Size = New System.Drawing.Size(155, 21)
        Me.Linetype2.TabIndex = 12
        '
        'Linetype1
        '
        Me.Linetype1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Linetype1.FormattingEnabled = True
        Me.Linetype1.Location = New System.Drawing.Point(238, 65)
        Me.Linetype1.Name = "Linetype1"
        Me.Linetype1.Size = New System.Drawing.Size(155, 21)
        Me.Linetype1.TabIndex = 9
        '
        'colortub3
        '
        Me.colortub3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.colortub3.Location = New System.Drawing.Point(227, 120)
        Me.colortub3.Multiline = True
        Me.colortub3.Name = "colortub3"
        Me.colortub3.ReadOnly = True
        Me.colortub3.Size = New System.Drawing.Size(6, 21)
        Me.colortub3.TabIndex = 14
        Me.colortub3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colortub3.UseSystemPasswordChar = True
        '
        'colortub2
        '
        Me.colortub2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.colortub2.Location = New System.Drawing.Point(227, 93)
        Me.colortub2.Multiline = True
        Me.colortub2.Name = "colortub2"
        Me.colortub2.ReadOnly = True
        Me.colortub2.Size = New System.Drawing.Size(6, 21)
        Me.colortub2.TabIndex = 13
        Me.colortub2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colortub2.UseSystemPasswordChar = True
        '
        'colortub1
        '
        Me.colortub1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.colortub1.Location = New System.Drawing.Point(227, 65)
        Me.colortub1.Multiline = True
        Me.colortub1.Name = "colortub1"
        Me.colortub1.ReadOnly = True
        Me.colortub1.Size = New System.Drawing.Size(6, 21)
        Me.colortub1.TabIndex = 12
        Me.colortub1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.colortub1.UseSystemPasswordChar = True
        '
        'btnCor3
        '
        Me.btnCor3.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnCor3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnCor3.ForeColor = System.Drawing.Color.Black
        Me.btnCor3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCor3.Location = New System.Drawing.Point(184, 120)
        Me.btnCor3.Name = "btnCor3"
        Me.btnCor3.Size = New System.Drawing.Size(37, 21)
        Me.btnCor3.TabIndex = 14
        Me.btnCor3.Text = "Cor"
        Me.btnCor3.UseVisualStyleBackColor = True
        '
        'btnCor2
        '
        Me.btnCor2.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnCor2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnCor2.ForeColor = System.Drawing.Color.Black
        Me.btnCor2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCor2.Location = New System.Drawing.Point(184, 93)
        Me.btnCor2.Name = "btnCor2"
        Me.btnCor2.Size = New System.Drawing.Size(37, 21)
        Me.btnCor2.TabIndex = 11
        Me.btnCor2.Text = "Cor"
        Me.btnCor2.UseVisualStyleBackColor = True
        '
        'btnCor1
        '
        Me.btnCor1.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.btnCor1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.btnCor1.ForeColor = System.Drawing.Color.Black
        Me.btnCor1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCor1.Location = New System.Drawing.Point(184, 65)
        Me.btnCor1.Name = "btnCor1"
        Me.btnCor1.Size = New System.Drawing.Size(37, 21)
        Me.btnCor1.TabIndex = 8
        Me.btnCor1.Text = "Cor"
        Me.btnCor1.UseVisualStyleBackColor = True
        '
        'rdoTubulacaoin
        '
        Me.rdoTubulacaoin.AutoSize = True
        Me.rdoTubulacaoin.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.rdoTubulacaoin.Location = New System.Drawing.Point(16, 42)
        Me.rdoTubulacaoin.Name = "rdoTubulacaoin"
        Me.rdoTubulacaoin.Size = New System.Drawing.Size(254, 17)
        Me.rdoTubulacaoin.TabIndex = 6
        Me.rdoTubulacaoin.Text = "Incluir outros tipos de tubulação na legenda"
        Me.rdoTubulacaoin.UseVisualStyleBackColor = True
        '
        'rdoTubulacao
        '
        Me.rdoTubulacao.AutoSize = True
        Me.rdoTubulacao.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.rdoTubulacao.Location = New System.Drawing.Point(16, 19)
        Me.rdoTubulacao.Name = "rdoTubulacao"
        Me.rdoTubulacao.Size = New System.Drawing.Size(177, 17)
        Me.rdoTubulacao.TabIndex = 5
        Me.rdoTubulacao.Text = "Incluir tubulações na legenda"
        Me.rdoTubulacao.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtTitulo)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 319)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(401, 40)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Título da legenda"
        '
        'txtTitulo
        '
        Me.txtTitulo.Location = New System.Drawing.Point(13, 14)
        Me.txtTitulo.Name = "txtTitulo"
        Me.txtTitulo.Size = New System.Drawing.Size(380, 22)
        Me.txtTitulo.TabIndex = 17
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtObservacao)
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 359)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(401, 97)
        Me.GroupBox2.TabIndex = 18
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Observações "
        '
        'txtObservacao
        '
        Me.txtObservacao.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtObservacao.Location = New System.Drawing.Point(13, 17)
        Me.txtObservacao.Name = "txtObservacao"
        Me.txtObservacao.Size = New System.Drawing.Size(380, 72)
        Me.txtObservacao.TabIndex = 0
        Me.txtObservacao.Text = ""
        '
        'cmdDesenhar
        '
        Me.cmdDesenhar.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.cmdDesenhar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.cmdDesenhar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.cmdDesenhar.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdDesenhar.ForeColor = System.Drawing.Color.Black
        Me.cmdDesenhar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdDesenhar.Location = New System.Drawing.Point(239, 489)
        Me.cmdDesenhar.Name = "cmdDesenhar"
        Me.cmdDesenhar.Size = New System.Drawing.Size(90, 24)
        Me.cmdDesenhar.TabIndex = 21
        Me.cmdDesenhar.Text = "&Desenhar"
        Me.cmdDesenhar.UseVisualStyleBackColor = True
        '
        'cmdClose
        '
        Me.cmdClose.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.cmdClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.cmdClose.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdClose.ForeColor = System.Drawing.Color.Black
        Me.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdClose.Location = New System.Drawing.Point(335, 489)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(70, 24)
        Me.cmdClose.TabIndex = 22
        Me.cmdClose.Text = "&Sair"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdHelp
        '
        Me.cmdHelp.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.cmdHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.cmdHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver
        Me.cmdHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.cmdHelp.ForeColor = System.Drawing.Color.Black
        Me.cmdHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.cmdHelp.Location = New System.Drawing.Point(7, 488)
        Me.cmdHelp.Name = "cmdHelp"
        Me.cmdHelp.Size = New System.Drawing.Size(64, 25)
        Me.cmdHelp.TabIndex = 20
        Me.cmdHelp.Text = "&Ajuda"
        Me.cmdHelp.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(7, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 32)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 23
        Me.PictureBox1.TabStop = False
        '
        'chkLegenda
        '
        Me.chkLegenda.AutoSize = True
        Me.chkLegenda.Checked = True
        Me.chkLegenda.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLegenda.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.chkLegenda.ForeColor = System.Drawing.Color.Maroon
        Me.chkLegenda.Location = New System.Drawing.Point(9, 462)
        Me.chkLegenda.Name = "chkLegenda"
        Me.chkLegenda.Size = New System.Drawing.Size(320, 17)
        Me.chkLegenda.TabIndex = 24
        Me.chkLegenda.Text = "Imprimir utilizando novo padrão de legenda automática..."
        Me.chkLegenda.UseVisualStyleBackColor = True
        '
        'frmLegenda
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(413, 521)
        Me.Controls.Add(Me.chkLegenda)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.cmdHelp)
        Me.Controls.Add(Me.cmdDesenhar)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grpTubulacao)
        Me.Controls.Add(Me.grpEletrical)
        Me.Controls.Add(Me.lblSubTitulo)
        Me.Controls.Add(Me.LblTitulo)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmLegenda"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Legenda Automática"
        Me.grpEletrical.ResumeLayout(False)
        Me.grpEletrical.PerformLayout()
        Me.grpTubulacao.ResumeLayout(False)
        Me.grpTubulacao.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LblTitulo As System.Windows.Forms.Label
    Friend WithEvents lblSubTitulo As System.Windows.Forms.Label
    Friend WithEvents grpEletrical As System.Windows.Forms.GroupBox
    Friend WithEvents listLayer As System.Windows.Forms.ListBox
    Friend WithEvents rdoTomada As System.Windows.Forms.CheckBox
    Friend WithEvents optLayer As System.Windows.Forms.RadioButton
    Friend WithEvents optSelecao As System.Windows.Forms.RadioButton
    Friend WithEvents optTelefonia As System.Windows.Forms.RadioButton
    Friend WithEvents optEletrica As System.Windows.Forms.RadioButton
    Friend WithEvents optTodos As System.Windows.Forms.RadioButton
    Friend WithEvents grpTubulacao As System.Windows.Forms.GroupBox
    Friend WithEvents rdoTubulacaoin As System.Windows.Forms.CheckBox
    Friend WithEvents rdoTubulacao As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTitulo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdDesenhar As System.Windows.Forms.Button
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdHelp As System.Windows.Forms.Button
    Friend WithEvents btnCor2 As System.Windows.Forms.Button
    Friend WithEvents btnCor1 As System.Windows.Forms.Button
    Friend WithEvents btnCor3 As System.Windows.Forms.Button
    Friend WithEvents colortub3 As System.Windows.Forms.TextBox
    Friend WithEvents colortub2 As System.Windows.Forms.TextBox
    Friend WithEvents colortub1 As System.Windows.Forms.TextBox
    Friend WithEvents Linetype3 As System.Windows.Forms.ComboBox
    Friend WithEvents Linetype2 As System.Windows.Forms.ComboBox
    Friend WithEvents Linetype1 As System.Windows.Forms.ComboBox
    Friend WithEvents Desc3 As System.Windows.Forms.TextBox
    Friend WithEvents Desc2 As System.Windows.Forms.TextBox
    Friend WithEvents Desc1 As System.Windows.Forms.TextBox
    Friend WithEvents btnSelecao As System.Windows.Forms.Button
    Friend WithEvents txtObservacao As System.Windows.Forms.RichTextBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents chkLegenda As System.Windows.Forms.CheckBox
End Class

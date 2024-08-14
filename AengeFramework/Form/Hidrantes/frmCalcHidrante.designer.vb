<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCalcHidrante
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.trvOcupacao = New System.Windows.Forms.TreeView
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblSistema = New System.Windows.Forms.Label
        Me.lblCargaIncendio = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cboFatorC = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.cbohidrante1 = New System.Windows.Forms.ComboBox
        Me.cbohidrante2 = New System.Windows.Forms.ComboBox
        Me.cboEsguincho = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnSelect = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.TextBox2 = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.lblFatorC = New System.Windows.Forms.Label
        Me.cboEstado = New System.Windows.Forms.ComboBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.chkAll = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboTipoSistema = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(9, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(580, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Cálculo de rede de Hidrantes. Informe os campos abaixo para que possar ser calcul" & _
            "adas todas as informações relacionadas à rede..."
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 37)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(465, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Selecione a ocupação desejada :"
        '
        'trvOcupacao
        '
        Me.trvOcupacao.Location = New System.Drawing.Point(10, 79)
        Me.trvOcupacao.Name = "trvOcupacao"
        Me.trvOcupacao.Size = New System.Drawing.Size(578, 107)
        Me.trvOcupacao.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(9, 191)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(236, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Sistema de tipo de proteção seleccionado :"
        '
        'lblSistema
        '
        Me.lblSistema.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblSistema.Location = New System.Drawing.Point(236, 191)
        Me.lblSistema.Name = "lblSistema"
        Me.lblSistema.Size = New System.Drawing.Size(236, 15)
        Me.lblSistema.TabIndex = 4
        Me.lblSistema.Text = "Não informado"
        '
        'lblCargaIncendio
        '
        Me.lblCargaIncendio.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblCargaIncendio.Location = New System.Drawing.Point(236, 209)
        Me.lblCargaIncendio.Name = "lblCargaIncendio"
        Me.lblCargaIncendio.Size = New System.Drawing.Size(236, 15)
        Me.lblCargaIncendio.TabIndex = 6
        Me.lblCargaIncendio.Text = "Não informado"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(9, 209)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(236, 15)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Carga de incêndio :"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(9, 235)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(236, 15)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Dados sobre a mangueira de incêndios"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(9, 259)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(120, 15)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Tipo de tubo :"
        '
        'cboFatorC
        '
        Me.cboFatorC.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFatorC.FormattingEnabled = True
        Me.cboFatorC.Location = New System.Drawing.Point(135, 257)
        Me.cboFatorC.Name = "cboFatorC"
        Me.cboFatorC.Size = New System.Drawing.Size(349, 21)
        Me.cboFatorC.TabIndex = 9
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(9, 283)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(236, 15)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Dados do hidrante 1 :"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(9, 308)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(236, 15)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "Dados do hidrante 2 :"
        '
        'cbohidrante1
        '
        Me.cbohidrante1.FormattingEnabled = True
        Me.cbohidrante1.Location = New System.Drawing.Point(135, 281)
        Me.cbohidrante1.Name = "cbohidrante1"
        Me.cbohidrante1.Size = New System.Drawing.Size(454, 21)
        Me.cbohidrante1.TabIndex = 12
        '
        'cbohidrante2
        '
        Me.cbohidrante2.FormattingEnabled = True
        Me.cbohidrante2.Location = New System.Drawing.Point(135, 305)
        Me.cbohidrante2.Name = "cbohidrante2"
        Me.cbohidrante2.Size = New System.Drawing.Size(454, 21)
        Me.cbohidrante2.TabIndex = 13
        '
        'cboEsguincho
        '
        Me.cboEsguincho.FormattingEnabled = True
        Me.cboEsguincho.Location = New System.Drawing.Point(135, 329)
        Me.cboEsguincho.Name = "cboEsguincho"
        Me.cboEsguincho.Size = New System.Drawing.Size(454, 21)
        Me.cboEsguincho.TabIndex = 16
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(9, 332)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(236, 15)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "Esguicho :"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(9, 365)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(236, 15)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "Cálculos :"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(9, 387)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(236, 15)
        Me.Label13.TabIndex = 18
        Me.Label13.Text = "Vazão :"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(9, 411)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(236, 15)
        Me.Label14.TabIndex = 19
        Me.Label14.Text = "Pressão :"
        '
        'btnSelect
        '
        Me.btnSelect.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSelect.Location = New System.Drawing.Point(409, 433)
        Me.btnSelect.Name = "btnSelect"
        Me.btnSelect.Size = New System.Drawing.Size(86, 26)
        Me.btnSelect.TabIndex = 21
        Me.btnSelect.Text = "&Ok"
        Me.btnSelect.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(502, 433)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(86, 26)
        Me.btnClose.TabIndex = 20
        Me.btnClose.Text = "&Cancelar"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(135, 383)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 22)
        Me.TextBox1.TabIndex = 22
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(135, 408)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(100, 22)
        Me.TextBox2.TabIndex = 23
        '
        'Label15
        '
        Me.Label15.ForeColor = System.Drawing.Color.Green
        Me.Label15.Location = New System.Drawing.Point(257, 365)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(221, 15)
        Me.Label15.TabIndex = 24
        Me.Label15.Text = "Valores padrões da norma - Consulta"
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(257, 411)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(143, 15)
        Me.Label16.TabIndex = 26
        Me.Label16.Text = "Pressão mínima exigida :"
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(257, 387)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(143, 15)
        Me.Label17.TabIndex = 25
        Me.Label17.Text = "Vazão mínima exigida : "
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(391, 386)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(73, 15)
        Me.Label18.TabIndex = 27
        Me.Label18.Text = "-"
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(391, 411)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(73, 15)
        Me.Label19.TabIndex = 28
        Me.Label19.Text = "-"
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(451, 387)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(49, 15)
        Me.Label20.TabIndex = 29
        Me.Label20.Text = "l/min"
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(451, 408)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(49, 15)
        Me.Label21.TabIndex = 30
        Me.Label21.Text = "Kpa"
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(490, 258)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(56, 15)
        Me.Label22.TabIndex = 31
        Me.Label22.Text = "Fator C :"
        '
        'lblFatorC
        '
        Me.lblFatorC.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblFatorC.Location = New System.Drawing.Point(543, 257)
        Me.lblFatorC.Name = "lblFatorC"
        Me.lblFatorC.Size = New System.Drawing.Size(49, 15)
        Me.lblFatorC.TabIndex = 32
        Me.lblFatorC.Text = "-"
        '
        'cboEstado
        '
        Me.cboEstado.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboEstado.FormattingEnabled = True
        Me.cboEstado.Location = New System.Drawing.Point(406, 55)
        Me.cboEstado.Name = "cboEstado"
        Me.cboEstado.Size = New System.Drawing.Size(56, 21)
        Me.cboEstado.TabIndex = 34
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(280, 57)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(120, 15)
        Me.Label23.TabIndex = 33
        Me.Label23.Text = "Selecione o estado :"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(472, 57)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(115, 17)
        Me.chkAll.TabIndex = 35
        Me.chkAll.Text = "Todos os estados"
        Me.chkAll.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(9, 59)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(158, 15)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "Selecione o tipo do sistema : "
        '
        'cboTipoSistema
        '
        Me.cboTipoSistema.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTipoSistema.FormattingEnabled = True
        Me.cboTipoSistema.Location = New System.Drawing.Point(168, 55)
        Me.cboTipoSistema.Name = "cboTipoSistema"
        Me.cboTipoSistema.Size = New System.Drawing.Size(93, 21)
        Me.cboTipoSistema.TabIndex = 37
        '
        'frmCalcHidrante
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(596, 466)
        Me.Controls.Add(Me.cboTipoSistema)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.chkAll)
        Me.Controls.Add(Me.cboEstado)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.lblFatorC)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.btnSelect)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.cboEsguincho)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cbohidrante2)
        Me.Controls.Add(Me.cbohidrante1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboFatorC)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblCargaIncendio)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblSistema)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.trvOcupacao)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmCalcHidrante"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cálculos de rede de hidrantes"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents trvOcupacao As System.Windows.Forms.TreeView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblSistema As System.Windows.Forms.Label
    Friend WithEvents lblCargaIncendio As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cboFatorC As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cbohidrante1 As System.Windows.Forms.ComboBox
    Friend WithEvents cbohidrante2 As System.Windows.Forms.ComboBox
    Friend WithEvents cboEsguincho As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnSelect As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents lblFatorC As System.Windows.Forms.Label
    Friend WithEvents cboEstado As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboTipoSistema As System.Windows.Forms.ComboBox
End Class

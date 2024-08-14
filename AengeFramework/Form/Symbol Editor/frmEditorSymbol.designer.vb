<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditorSymbol
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditorSymbol))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtObjLeg = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboClas = New System.Windows.Forms.ComboBox()
        Me.txtObjMenu = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnPtoInsert = New System.Windows.Forms.Button()
        Me.btnSelectBlock = New System.Windows.Forms.Button()
        Me.cboLayer = New System.Windows.Forms.ComboBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Slide = New System.Windows.Forms.PictureBox()
        Me.cboFamily = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.trvSymbol = New System.Windows.Forms.TreeView()
        Me.chkAllSymbols = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblObjMenu = New System.Windows.Forms.Label()
        Me.txtCodObj = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnSelectObj = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cboUnit = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.chkCurrentLayer = New System.Windows.Forms.CheckBox()
        Me.cboScale = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboAngle = New System.Windows.Forms.ComboBox()
        Me.lblColor = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnColor = New System.Windows.Forms.Button()
        Me.PicColor = New System.Windows.Forms.PictureBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.txtHandle = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtObjectId = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Slide, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PicColor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(263, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 15)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Símbolo :"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(33, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(705, 29)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "Criação e edição de objetos - Para criar um novo objeto será necessário informar " & _
            "os dados de layers, bloco e outras informações abaixo..."
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(263, 148)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(131, 15)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Descrição :"
        '
        'txtObjLeg
        '
        Me.txtObjLeg.Location = New System.Drawing.Point(335, 146)
        Me.txtObjLeg.Name = "txtObjLeg"
        Me.txtObjLeg.Size = New System.Drawing.Size(403, 22)
        Me.txtObjLeg.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(263, 218)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(131, 15)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Classe :"
        '
        'cboClas
        '
        Me.cboClas.FormattingEnabled = True
        Me.cboClas.Location = New System.Drawing.Point(335, 217)
        Me.cboClas.Name = "cboClas"
        Me.cboClas.Size = New System.Drawing.Size(403, 21)
        Me.cboClas.TabIndex = 3
        '
        'txtObjMenu
        '
        Me.txtObjMenu.Location = New System.Drawing.Point(335, 170)
        Me.txtObjMenu.Name = "txtObjMenu"
        Me.txtObjMenu.Size = New System.Drawing.Size(403, 22)
        Me.txtObjMenu.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(263, 172)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(131, 15)
        Me.Label6.TabIndex = 17
        Me.Label6.Text = "Nome :"
        '
        'btnSave
        '
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.Location = New System.Drawing.Point(140, 6)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(117, 23)
        Me.btnSave.TabIndex = 13
        Me.btnSave.Text = "&Salvar símbolo"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.ForeColor = System.Drawing.Color.Black
        Me.btnClose.Location = New System.Drawing.Point(663, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 15
        Me.btnClose.Text = "Fechar"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(12, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(174, 15)
        Me.Label7.TabIndex = 22
        Me.Label7.Text = "Layer :"
        '
        'btnPtoInsert
        '
        Me.btnPtoInsert.ForeColor = System.Drawing.Color.Black
        Me.btnPtoInsert.Location = New System.Drawing.Point(255, 58)
        Me.btnPtoInsert.Name = "btnPtoInsert"
        Me.btnPtoInsert.Size = New System.Drawing.Size(220, 23)
        Me.btnPtoInsert.TabIndex = 10
        Me.btnPtoInsert.Text = "Alterar ponto base do símbolo"
        Me.btnPtoInsert.UseVisualStyleBackColor = True
        '
        'btnSelectBlock
        '
        Me.btnSelectBlock.ForeColor = System.Drawing.Color.Black
        Me.btnSelectBlock.Location = New System.Drawing.Point(9, 58)
        Me.btnSelectBlock.Name = "btnSelectBlock"
        Me.btnSelectBlock.Size = New System.Drawing.Size(223, 23)
        Me.btnSelectBlock.TabIndex = 9
        Me.btnSelectBlock.Text = "Selecionar símbolo existente"
        Me.btnSelectBlock.UseVisualStyleBackColor = True
        '
        'cboLayer
        '
        Me.cboLayer.FormattingEnabled = True
        Me.cboLayer.Location = New System.Drawing.Point(70, 20)
        Me.cboLayer.Name = "cboLayer"
        Me.cboLayer.Size = New System.Drawing.Size(278, 21)
        Me.cboLayer.TabIndex = 4
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(7, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 39
        Me.PictureBox1.TabStop = False
        '
        'Slide
        '
        Me.Slide.BackColor = System.Drawing.Color.Black
        Me.Slide.Location = New System.Drawing.Point(335, 42)
        Me.Slide.Name = "Slide"
        Me.Slide.Size = New System.Drawing.Size(157, 98)
        Me.Slide.TabIndex = 40
        Me.Slide.TabStop = False
        '
        'cboFamily
        '
        Me.cboFamily.FormattingEnabled = True
        Me.cboFamily.Items.AddRange(New Object() {"Hidro-Sanitário", "Gás", "Incêndio"})
        Me.cboFamily.Location = New System.Drawing.Point(335, 194)
        Me.cboFamily.Name = "cboFamily"
        Me.cboFamily.Size = New System.Drawing.Size(403, 21)
        Me.cboFamily.TabIndex = 2
        Me.cboFamily.Text = "Hidro-Sanitário"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(263, 195)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 15)
        Me.Label3.TabIndex = 41
        Me.Label3.Text = "Família :"
        '
        'trvSymbol
        '
        Me.trvSymbol.Location = New System.Drawing.Point(7, 35)
        Me.trvSymbol.Name = "trvSymbol"
        Me.trvSymbol.Size = New System.Drawing.Size(250, 227)
        Me.trvSymbol.TabIndex = 6
        '
        'chkAllSymbols
        '
        Me.chkAllSymbols.AutoSize = True
        Me.chkAllSymbols.Checked = True
        Me.chkAllSymbols.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAllSymbols.Location = New System.Drawing.Point(7, 266)
        Me.chkAllSymbols.Name = "chkAllSymbols"
        Me.chkAllSymbols.Size = New System.Drawing.Size(179, 17)
        Me.chkAllSymbols.TabIndex = 7
        Me.chkAllSymbols.Text = "Selecionar de todas as classes"
        Me.chkAllSymbols.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(501, 42)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(131, 15)
        Me.Label8.TabIndex = 45
        Me.Label8.Text = "Nome do símbolo :"
        '
        'lblObjMenu
        '
        Me.lblObjMenu.ForeColor = System.Drawing.Color.Navy
        Me.lblObjMenu.Location = New System.Drawing.Point(505, 59)
        Me.lblObjMenu.Name = "lblObjMenu"
        Me.lblObjMenu.Size = New System.Drawing.Size(233, 15)
        Me.lblObjMenu.TabIndex = 46
        Me.lblObjMenu.Text = "-"
        '
        'txtCodObj
        '
        Me.txtCodObj.Location = New System.Drawing.Point(335, 240)
        Me.txtCodObj.Name = "txtCodObj"
        Me.txtCodObj.ReadOnly = True
        Me.txtCodObj.Size = New System.Drawing.Size(94, 22)
        Me.txtCodObj.TabIndex = 48
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(263, 241)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 15)
        Me.Label9.TabIndex = 47
        Me.Label9.Text = "Código :"
        '
        'btnDelete
        '
        Me.btnDelete.ForeColor = System.Drawing.Color.Black
        Me.btnDelete.Location = New System.Drawing.Point(262, 6)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(132, 23)
        Me.btnDelete.TabIndex = 14
        Me.btnDelete.Text = "&Excluir símbolo"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnSelectObj)
        Me.GroupBox1.Controls.Add(Me.btnSelectBlock)
        Me.GroupBox1.Controls.Add(Me.btnPtoInsert)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox1.Location = New System.Drawing.Point(7, 379)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(731, 88)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Ações relacionadas ao bloco :"
        '
        'btnSelectObj
        '
        Me.btnSelectObj.ForeColor = System.Drawing.Color.Black
        Me.btnSelectObj.Location = New System.Drawing.Point(495, 58)
        Me.btnSelectObj.Name = "btnSelectObj"
        Me.btnSelectObj.Size = New System.Drawing.Size(227, 23)
        Me.btnSelectObj.TabIndex = 11
        Me.btnSelectObj.Text = "Selecionar objetos no desenho"
        Me.btnSelectObj.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(6, 18)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(716, 32)
        Me.Label10.TabIndex = 51
        Me.Label10.Text = resources.GetString("Label10.Text")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboUnit)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.CheckBox1)
        Me.GroupBox2.Controls.Add(Me.chkCurrentLayer)
        Me.GroupBox2.Controls.Add(Me.cboScale)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.cboAngle)
        Me.GroupBox2.Controls.Add(Me.lblColor)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.btnColor)
        Me.GroupBox2.Controls.Add(Me.PicColor)
        Me.GroupBox2.Controls.Add(Me.cboLayer)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.ForeColor = System.Drawing.Color.Navy
        Me.GroupBox2.Location = New System.Drawing.Point(7, 284)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(731, 94)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Opções de inserção de símbolos :"
        '
        'cboUnit
        '
        Me.cboUnit.FormattingEnabled = True
        Me.cboUnit.Location = New System.Drawing.Point(234, 66)
        Me.cboUnit.Name = "cboUnit"
        Me.cboUnit.Size = New System.Drawing.Size(114, 21)
        Me.cboUnit.TabIndex = 60
        '
        'Label14
        '
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(12, 69)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(115, 19)
        Me.Label14.TabIndex = 61
        Me.Label14.Text = "Unidade do bloco : "
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.ForeColor = System.Drawing.Color.Black
        Me.CheckBox1.Location = New System.Drawing.Point(361, 43)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(215, 17)
        Me.CheckBox1.TabIndex = 59
        Me.CheckBox1.Text = "Informar escala na criação do Objeto"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'chkCurrentLayer
        '
        Me.chkCurrentLayer.AutoSize = True
        Me.chkCurrentLayer.ForeColor = System.Drawing.Color.Black
        Me.chkCurrentLayer.Location = New System.Drawing.Point(361, 21)
        Me.chkCurrentLayer.Name = "chkCurrentLayer"
        Me.chkCurrentLayer.Size = New System.Drawing.Size(128, 17)
        Me.chkCurrentLayer.TabIndex = 58
        Me.chkCurrentLayer.Text = "Pegar layer corrente"
        Me.chkCurrentLayer.UseVisualStyleBackColor = True
        '
        'cboScale
        '
        Me.cboScale.FormattingEnabled = True
        Me.cboScale.Location = New System.Drawing.Point(234, 43)
        Me.cboScale.Name = "cboScale"
        Me.cboScale.Size = New System.Drawing.Size(114, 21)
        Me.cboScale.TabIndex = 56
        '
        'Label13
        '
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(180, 45)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 15)
        Me.Label13.TabIndex = 57
        Me.Label13.Text = "Escala :"
        '
        'cboAngle
        '
        Me.cboAngle.FormattingEnabled = True
        Me.cboAngle.Items.AddRange(New Object() {"Dinâmico", "0", "90", "180", "270"})
        Me.cboAngle.Location = New System.Drawing.Point(70, 43)
        Me.cboAngle.Name = "cboAngle"
        Me.cboAngle.Size = New System.Drawing.Size(99, 21)
        Me.cboAngle.TabIndex = 52
        '
        'lblColor
        '
        Me.lblColor.Location = New System.Drawing.Point(602, 44)
        Me.lblColor.Name = "lblColor"
        Me.lblColor.Size = New System.Drawing.Size(62, 15)
        Me.lblColor.TabIndex = 55
        Me.lblColor.Text = "-"
        '
        'Label12
        '
        Me.Label12.ForeColor = System.Drawing.Color.Black
        Me.Label12.Location = New System.Drawing.Point(12, 45)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(62, 19)
        Me.Label12.TabIndex = 53
        Me.Label12.Text = "Ângulo :"
        '
        'btnColor
        '
        Me.btnColor.ForeColor = System.Drawing.Color.Black
        Me.btnColor.Location = New System.Drawing.Point(661, 19)
        Me.btnColor.Name = "btnColor"
        Me.btnColor.Size = New System.Drawing.Size(61, 24)
        Me.btnColor.TabIndex = 5
        Me.btnColor.Text = "&Cores"
        Me.btnColor.UseVisualStyleBackColor = True
        '
        'PicColor
        '
        Me.PicColor.BackColor = System.Drawing.Color.Transparent
        Me.PicColor.Location = New System.Drawing.Point(635, 22)
        Me.PicColor.Name = "PicColor"
        Me.PicColor.Size = New System.Drawing.Size(20, 18)
        Me.PicColor.TabIndex = 53
        Me.PicColor.TabStop = False
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(602, 21)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(62, 15)
        Me.Label11.TabIndex = 52
        Me.Label11.Text = "Cor :"
        '
        'btnNew
        '
        Me.btnNew.ForeColor = System.Drawing.Color.Black
        Me.btnNew.Location = New System.Drawing.Point(13, 6)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(121, 23)
        Me.btnNew.TabIndex = 12
        Me.btnNew.Text = "&Novo símbolo"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.btnNew)
        Me.Panel1.Controls.Add(Me.btnClose)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 474)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(750, 36)
        Me.Panel1.TabIndex = 56
        '
        'txtHandle
        '
        Me.txtHandle.Location = New System.Drawing.Point(504, 240)
        Me.txtHandle.Name = "txtHandle"
        Me.txtHandle.ReadOnly = True
        Me.txtHandle.Size = New System.Drawing.Size(94, 22)
        Me.txtHandle.TabIndex = 58
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(436, 241)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(66, 15)
        Me.Label15.TabIndex = 57
        Me.Label15.Text = "Id Block : "
        '
        'txtObjectId
        '
        Me.txtObjectId.Location = New System.Drawing.Point(604, 240)
        Me.txtObjectId.Name = "txtObjectId"
        Me.txtObjectId.ReadOnly = True
        Me.txtObjectId.Size = New System.Drawing.Size(134, 22)
        Me.txtObjectId.TabIndex = 59
        '
        'frmEditorSymbol
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(750, 510)
        Me.Controls.Add(Me.txtObjectId)
        Me.Controls.Add(Me.txtHandle)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtCodObj)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblObjMenu)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.chkAllSymbols)
        Me.Controls.Add(Me.trvSymbol)
        Me.Controls.Add(Me.cboFamily)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Slide)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.txtObjMenu)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboClas)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtObjLeg)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmEditorSymbol"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Criação e edição de símbolos"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Slide, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PicColor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtObjLeg As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cboClas As System.Windows.Forms.ComboBox
    Friend WithEvents txtObjMenu As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnPtoInsert As System.Windows.Forms.Button
    Friend WithEvents btnSelectBlock As System.Windows.Forms.Button
    Friend WithEvents cboLayer As System.Windows.Forms.ComboBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Slide As System.Windows.Forms.PictureBox
    Friend WithEvents cboFamily As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents trvSymbol As System.Windows.Forms.TreeView
    Friend WithEvents chkAllSymbols As System.Windows.Forms.CheckBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblObjMenu As System.Windows.Forms.Label
    Friend WithEvents txtCodObj As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents PicColor As System.Windows.Forms.PictureBox
    Friend WithEvents btnColor As System.Windows.Forms.Button
    Friend WithEvents lblColor As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cboAngle As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkCurrentLayer As System.Windows.Forms.CheckBox
    Friend WithEvents cboScale As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnSelectObj As System.Windows.Forms.Button
    Friend WithEvents cboUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtHandle As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtObjectId As System.Windows.Forms.TextBox
End Class

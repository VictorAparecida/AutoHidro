<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UsrC_Project
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UsrC_Project))
        Me.lblLen = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.lblDtCreate = New System.Windows.Forms.Label
        Me.lblSep = New System.Windows.Forms.Label
        Me.lblMed = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblScale = New System.Windows.Forms.Label
        Me.lblNameDwg = New System.Windows.Forms.Label
        Me.lblProject = New System.Windows.Forms.Label
        Me.ImgTreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.trvDraw = New System.Windows.Forms.TreeView
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblInfo = New System.Windows.Forms.Label
        Me.PicNoImg = New System.Windows.Forms.Panel
        Me.btnConfig = New System.Windows.Forms.Button
        Me.btnOpen = New System.Windows.Forms.Button
        Me.Dwg = New System.Windows.Forms.PictureBox
        CType(Me.Dwg, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblLen
        '
        Me.lblLen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblLen.AutoSize = True
        Me.lblLen.ForeColor = System.Drawing.Color.Navy
        Me.lblLen.Location = New System.Drawing.Point(130, 52)
        Me.lblLen.Name = "lblLen"
        Me.lblLen.Size = New System.Drawing.Size(11, 13)
        Me.lblLen.TabIndex = 29
        Me.lblLen.Text = "-"
        Me.lblLen.Visible = False
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(17, 52)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(122, 13)
        Me.Label8.TabIndex = 28
        Me.Label8.Text = "Tamanho do arquivo : "
        Me.Label8.Visible = False
        '
        'lblDtCreate
        '
        Me.lblDtCreate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDtCreate.AutoSize = True
        Me.lblDtCreate.ForeColor = System.Drawing.Color.Navy
        Me.lblDtCreate.Location = New System.Drawing.Point(109, 32)
        Me.lblDtCreate.Name = "lblDtCreate"
        Me.lblDtCreate.Size = New System.Drawing.Size(11, 13)
        Me.lblDtCreate.TabIndex = 27
        Me.lblDtCreate.Text = "-"
        Me.lblDtCreate.Visible = False
        '
        'lblSep
        '
        Me.lblSep.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSep.AutoSize = True
        Me.lblSep.Location = New System.Drawing.Point(17, 32)
        Me.lblSep.Name = "lblSep"
        Me.lblSep.Size = New System.Drawing.Size(95, 13)
        Me.lblSep.TabIndex = 26
        Me.lblSep.Text = "Data da criação : "
        Me.lblSep.Visible = False
        '
        'lblMed
        '
        Me.lblMed.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblMed.AutoSize = True
        Me.lblMed.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblMed.ForeColor = System.Drawing.Color.Navy
        Me.lblMed.Location = New System.Drawing.Point(215, 529)
        Me.lblMed.Name = "lblMed"
        Me.lblMed.Size = New System.Drawing.Size(11, 13)
        Me.lblMed.TabIndex = 25
        Me.lblMed.Text = "-"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label7.Location = New System.Drawing.Point(103, 529)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(117, 13)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Unidade de medida : "
        '
        'lblScale
        '
        Me.lblScale.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblScale.AutoSize = True
        Me.lblScale.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblScale.ForeColor = System.Drawing.Color.Navy
        Me.lblScale.Location = New System.Drawing.Point(69, 529)
        Me.lblScale.Name = "lblScale"
        Me.lblScale.Size = New System.Drawing.Size(11, 13)
        Me.lblScale.TabIndex = 23
        Me.lblScale.Text = "-"
        '
        'lblNameDwg
        '
        Me.lblNameDwg.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblNameDwg.AutoSize = True
        Me.lblNameDwg.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblNameDwg.ForeColor = System.Drawing.Color.Navy
        Me.lblNameDwg.Location = New System.Drawing.Point(69, 509)
        Me.lblNameDwg.Name = "lblNameDwg"
        Me.lblNameDwg.Size = New System.Drawing.Size(11, 13)
        Me.lblNameDwg.TabIndex = 22
        Me.lblNameDwg.Text = "-"
        '
        'lblProject
        '
        Me.lblProject.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblProject.AutoSize = True
        Me.lblProject.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblProject.ForeColor = System.Drawing.Color.Navy
        Me.lblProject.Location = New System.Drawing.Point(69, 490)
        Me.lblProject.Name = "lblProject"
        Me.lblProject.Size = New System.Drawing.Size(11, 13)
        Me.lblProject.TabIndex = 21
        Me.lblProject.Text = "-"
        '
        'ImgTreeView
        '
        Me.ImgTreeView.ImageStream = CType(resources.GetObject("ImgTreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImgTreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.ImgTreeView.Images.SetKeyName(0, "autocad_icon.PNG")
        Me.ImgTreeView.Images.SetKeyName(1, "folder_1.jpg")
        Me.ImgTreeView.Images.SetKeyName(2, "Folder_2.PNG")
        Me.ImgTreeView.Images.SetKeyName(3, "Icon_Dwg.png")
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label5.Location = New System.Drawing.Point(9, 529)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Escala :"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label4.Location = New System.Drawing.Point(9, 509)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Desenho :"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label3.Location = New System.Drawing.Point(9, 490)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Projeto :"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label2.Location = New System.Drawing.Point(9, 471)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(166, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Informações sobre o desenho :"
        '
        'trvDraw
        '
        Me.trvDraw.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trvDraw.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.trvDraw.ImageIndex = 0
        Me.trvDraw.ImageList = Me.ImgTreeView
        Me.trvDraw.Location = New System.Drawing.Point(8, 23)
        Me.trvDraw.Name = "trvDraw"
        Me.trvDraw.SelectedImageIndex = 0
        Me.trvDraw.Size = New System.Drawing.Size(231, 338)
        Me.trvDraw.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label1.Location = New System.Drawing.Point(5, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(206, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Desenhos cadastrados para o projeto :"
        '
        'lblInfo
        '
        Me.lblInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblInfo.BackColor = System.Drawing.Color.DarkGray
        Me.lblInfo.ForeColor = System.Drawing.Color.White
        Me.lblInfo.Location = New System.Drawing.Point(16, 401)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(216, 34)
        Me.lblInfo.TabIndex = 31
        Me.lblInfo.Text = "Dwg não pode ser visualizado !"
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblInfo.Visible = False
        '
        'PicNoImg
        '
        Me.PicNoImg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PicNoImg.BackColor = System.Drawing.Color.White
        Me.PicNoImg.Location = New System.Drawing.Point(8, 366)
        Me.PicNoImg.Name = "PicNoImg"
        Me.PicNoImg.Size = New System.Drawing.Size(231, 103)
        Me.PicNoImg.TabIndex = 32
        Me.PicNoImg.Visible = False
        '
        'btnConfig
        '
        Me.btnConfig.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnConfig.Location = New System.Drawing.Point(8, 551)
        Me.btnConfig.Name = "btnConfig"
        Me.btnConfig.Size = New System.Drawing.Size(121, 23)
        Me.btnConfig.TabIndex = 33
        Me.btnConfig.Text = "Configurações"
        Me.btnConfig.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOpen.Location = New System.Drawing.Point(135, 551)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(103, 23)
        Me.btnOpen.TabIndex = 34
        Me.btnOpen.Text = "Abrir"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'Dwg
        '
        Me.Dwg.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Dwg.BackColor = System.Drawing.Color.White
        Me.Dwg.Location = New System.Drawing.Point(8, 366)
        Me.Dwg.Name = "Dwg"
        Me.Dwg.Size = New System.Drawing.Size(231, 103)
        Me.Dwg.TabIndex = 35
        Me.Dwg.TabStop = False
        '
        'UsrC_Project
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.trvDraw)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.btnConfig)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.PicNoImg)
        Me.Controls.Add(Me.lblLen)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblDtCreate)
        Me.Controls.Add(Me.lblSep)
        Me.Controls.Add(Me.lblMed)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblScale)
        Me.Controls.Add(Me.lblNameDwg)
        Me.Controls.Add(Me.lblProject)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Dwg)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.MinimumSize = New System.Drawing.Size(200, 300)
        Me.Name = "UsrC_Project"
        Me.Size = New System.Drawing.Size(244, 580)
        CType(Me.Dwg, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblLen As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblDtCreate As System.Windows.Forms.Label
    Friend WithEvents lblSep As System.Windows.Forms.Label
    Friend WithEvents lblMed As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblScale As System.Windows.Forms.Label
    Friend WithEvents lblNameDwg As System.Windows.Forms.Label
    Friend WithEvents lblProject As System.Windows.Forms.Label
    Friend WithEvents ImgTreeView As System.Windows.Forms.ImageList
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents trvDraw As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents PicNoImg As System.Windows.Forms.Panel
    Friend WithEvents btnConfig As System.Windows.Forms.Button
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents Dwg As System.Windows.Forms.PictureBox

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigAbert
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmConfigAbert))
        Me.lblDestaque = New System.Windows.Forms.Label
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cboStyleMenu = New System.Windows.Forms.ComboBox
        Me.cboIniMenu = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboMenuBar = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnFechar = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDestaque
        '
        Me.lblDestaque.BackColor = System.Drawing.Color.Transparent
        Me.lblDestaque.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.lblDestaque.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.lblDestaque.ForeColor = System.Drawing.Color.Black
        Me.lblDestaque.Location = New System.Drawing.Point(9, 21)
        Me.lblDestaque.Name = "lblDestaque"
        Me.lblDestaque.Size = New System.Drawing.Size(118, 18)
        Me.lblDestaque.TabIndex = 36
        Me.lblDestaque.Text = "Abertura de projetos"
        Me.lblDestaque.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.Color.Transparent
        Me.lblTitulo.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.ForeColor = System.Drawing.Color.Navy
        Me.lblTitulo.Location = New System.Drawing.Point(11, 4)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(11, 13)
        Me.lblTitulo.TabIndex = 34
        Me.lblTitulo.Text = "-"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label1.Location = New System.Drawing.Point(31, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(428, 30)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Configuração de abertura e inicialização de projetos. Selecione as configurações " & _
            "desejadas e clique em 'Salvar'..."
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(9, 47)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(16, 16)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 33
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label2.Location = New System.Drawing.Point(6, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(129, 18)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "Abertura de desenhos :"
        '
        'cboStyleMenu
        '
        Me.cboStyleMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStyleMenu.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboStyleMenu.FormattingEnabled = True
        Me.cboStyleMenu.Items.AddRange(New Object() {"Estilo clássico (Autohidro 2012 - anteriores)", "Novo estilo - Autohidro 2014"})
        Me.cboStyleMenu.Location = New System.Drawing.Point(141, 91)
        Me.cboStyleMenu.Name = "cboStyleMenu"
        Me.cboStyleMenu.Size = New System.Drawing.Size(318, 21)
        Me.cboStyleMenu.TabIndex = 39
        '
        'cboIniMenu
        '
        Me.cboIniMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIniMenu.FormattingEnabled = True
        Me.cboIniMenu.Items.AddRange(New Object() {"Abrir tela automaticamente sempre que abrir um desenho existente", "Abrir tela automaticamente somente quando incializar o Autopower", "Não abrir a tela, irei chamá-la manualmente"})
        Me.cboIniMenu.Location = New System.Drawing.Point(141, 229)
        Me.cboIniMenu.Name = "cboIniMenu"
        Me.cboIniMenu.Size = New System.Drawing.Size(318, 21)
        Me.cboIniMenu.TabIndex = 41
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label3.Location = New System.Drawing.Point(6, 229)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(122, 18)
        Me.Label3.TabIndex = 40
        Me.Label3.Text = "Inicialização :"
        '
        'cboMenuBar
        '
        Me.cboMenuBar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMenuBar.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMenuBar.FormattingEnabled = True
        Me.cboMenuBar.Items.AddRange(New Object() {"Não carregar a barra de menu - irei chamá-la manualmente", "Sempre carregar a barra de menu ao iniciar o Autopower "})
        Me.cboMenuBar.Location = New System.Drawing.Point(141, 116)
        Me.cboMenuBar.Name = "cboMenuBar"
        Me.cboMenuBar.Size = New System.Drawing.Size(318, 21)
        Me.cboMenuBar.TabIndex = 43
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.Label4.Location = New System.Drawing.Point(6, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(122, 18)
        Me.Label4.TabIndex = 42
        Me.Label4.Text = "Barra de menu :"
        '
        'btnFechar
        '
        Me.btnFechar.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnFechar.Location = New System.Drawing.Point(358, 146)
        Me.btnFechar.Name = "btnFechar"
        Me.btnFechar.Size = New System.Drawing.Size(101, 25)
        Me.btnFechar.TabIndex = 45
        Me.btnFechar.Text = "Cancelar"
        Me.btnFechar.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnOk.Location = New System.Drawing.Point(199, 146)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(153, 25)
        Me.btnOk.TabIndex = 44
        Me.btnOk.Text = "Salvar configurações"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.DimGray
        Me.Panel1.Location = New System.Drawing.Point(127, 36)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(426, 1)
        Me.Panel1.TabIndex = 46
        '
        'frmConfigAbert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 178)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnFechar)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.cboMenuBar)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboIniMenu)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboStyleMenu)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblDestaque)
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmConfigAbert"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configurações de abertura"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDestaque As System.Windows.Forms.Label
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboStyleMenu As System.Windows.Forms.ComboBox
    Friend WithEvents cboIniMenu As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboMenuBar As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnFechar As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class

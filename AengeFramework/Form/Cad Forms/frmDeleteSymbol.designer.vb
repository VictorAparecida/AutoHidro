<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDeleteSymbol
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDeleteSymbol))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnHelp = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.TrvObjects = New System.Windows.Forms.TreeView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblObject = New System.Windows.Forms.Label()
        Me.lblClass = New System.Windows.Forms.Label()
        Me.txtCodObj = New System.Windows.Forms.TextBox()
        Me.chkBlock = New System.Windows.Forms.CheckBox()
        Me.lblDwgName = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboClas = New System.Windows.Forms.ComboBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(34, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 39
        Me.Label1.Text = "Exclusão de objetos "
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(24, 24)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox1.TabIndex = 38
        Me.PictureBox1.TabStop = False
        '
        'Label11
        '
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(34, 20)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(465, 60)
        Me.Label11.TabIndex = 37
        Me.Label11.Text = resources.GetString("Label11.Text")
        '
        'btnHelp
        '
        Me.btnHelp.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnHelp.Location = New System.Drawing.Point(5, 349)
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(75, 25)
        Me.btnHelp.TabIndex = 42
        Me.btnHelp.Text = "&Ajuda"
        Me.btnHelp.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnCancel.Location = New System.Drawing.Point(425, 349)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(77, 25)
        Me.btnCancel.TabIndex = 41
        Me.btnCancel.Text = "&Cancelar"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.btnOk.Location = New System.Drawing.Point(342, 349)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(77, 25)
        Me.btnOk.TabIndex = 40
        Me.btnOk.Text = "&Excluir"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'TrvObjects
        '
        Me.TrvObjects.Location = New System.Drawing.Point(6, 108)
        Me.TrvObjects.Name = "TrvObjects"
        Me.TrvObjects.Size = New System.Drawing.Size(498, 170)
        Me.TrvObjects.TabIndex = 43
        '
        'Label2
        '
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(5, 302)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(123, 17)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Objeto selecionado :"
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(5, 282)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(123, 17)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "Classe :"
        '
        'lblObject
        '
        Me.lblObject.ForeColor = System.Drawing.Color.Navy
        Me.lblObject.Location = New System.Drawing.Point(122, 302)
        Me.lblObject.Name = "lblObject"
        Me.lblObject.Size = New System.Drawing.Size(373, 17)
        Me.lblObject.TabIndex = 46
        Me.lblObject.Text = "-"
        '
        'lblClass
        '
        Me.lblClass.ForeColor = System.Drawing.Color.Navy
        Me.lblClass.Location = New System.Drawing.Point(122, 282)
        Me.lblClass.Name = "lblClass"
        Me.lblClass.Size = New System.Drawing.Size(373, 17)
        Me.lblClass.TabIndex = 47
        Me.lblClass.Text = "-"
        '
        'txtCodObj
        '
        Me.txtCodObj.Location = New System.Drawing.Point(86, 349)
        Me.txtCodObj.Name = "txtCodObj"
        Me.txtCodObj.Size = New System.Drawing.Size(56, 22)
        Me.txtCodObj.TabIndex = 48
        Me.txtCodObj.Visible = False
        '
        'chkBlock
        '
        Me.chkBlock.AutoSize = True
        Me.chkBlock.Checked = True
        Me.chkBlock.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkBlock.Location = New System.Drawing.Point(258, 322)
        Me.chkBlock.Name = "chkBlock"
        Me.chkBlock.Size = New System.Drawing.Size(244, 17)
        Me.chkBlock.TabIndex = 49
        Me.chkBlock.Text = "Excluir bloco relacionado ao objeto (DWG)"
        Me.chkBlock.UseVisualStyleBackColor = True
        '
        'lblDwgName
        '
        Me.lblDwgName.ForeColor = System.Drawing.Color.Navy
        Me.lblDwgName.Location = New System.Drawing.Point(122, 322)
        Me.lblDwgName.Name = "lblDwgName"
        Me.lblDwgName.Size = New System.Drawing.Size(123, 17)
        Me.lblDwgName.TabIndex = 51
        Me.lblDwgName.Text = "-"
        '
        'Label5
        '
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(5, 323)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 17)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "Bloco relacionado :"
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(5, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 17)
        Me.Label4.TabIndex = 52
        Me.Label4.Text = "Classes :"
        '
        'cboClas
        '
        Me.cboClas.FormattingEnabled = True
        Me.cboClas.Location = New System.Drawing.Point(58, 84)
        Me.cboClas.Name = "cboClas"
        Me.cboClas.Size = New System.Drawing.Size(446, 21)
        Me.cboClas.TabIndex = 53
        '
        'frmDeleteSymbol
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(514, 379)
        Me.Controls.Add(Me.cboClas)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblDwgName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.chkBlock)
        Me.Controls.Add(Me.txtCodObj)
        Me.Controls.Add(Me.lblClass)
        Me.Controls.Add(Me.lblObject)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TrvObjects)
        Me.Controls.Add(Me.btnHelp)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmDeleteSymbol"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gerenciador de símbolos - Exclusão"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnHelp As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents TrvObjects As System.Windows.Forms.TreeView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblObject As System.Windows.Forms.Label
    Friend WithEvents lblClass As System.Windows.Forms.Label
    Friend WithEvents txtCodObj As System.Windows.Forms.TextBox
    Friend WithEvents chkBlock As System.Windows.Forms.CheckBox
    Friend WithEvents lblDwgName As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboClas As System.Windows.Forms.ComboBox
End Class

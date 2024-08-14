Imports Autodesk.AutoCAD.Colors
Imports Autodesk.AutoCAD.Geometry

Public Class frmEditorSymbol

#Region "----- Documentação -----"

    '============================================================================================================

    'Empresa : Thorx Sistemas e Consultoria Ltda
    'Data da criação : domingo, 2 de março de 2014
    'Analista responsável : Raul Antonio Fernandes Junior
    'Descrição : Regras de negócios e de funcionalidades relacionadas à camada TOBJCLAS
    '
    '
    'Id de tratamento de Erros : 
    'FRMEDITORSYMBOL001 - btnSelectBlock_Click
    'FRMEDITORSYMBOL002 - trvSymbol_AfterSelect
    'FRMEDITORSYMBOL003 - SaveRegister
    'FRMEDITORSYMBOL004 - DeleteRegister
    'FRMEDITORSYMBOL005 - 
    'FRMEDITORSYMBOL006 - 
    'FRMEDITORSYMBOL007 - 
    '
    'Modificações
    '
    '============================================================================================================

#End Region

#Region "------ Atributes and declarations -----"

    Dim LibraryError As LibraryError, mVar_NameClass As String = "frmEditorSymbol", TextNotInfo As String = "Não informado"

    'Para nao ficar consultando e carregando toda vez os datatables, carregamos na memoria somente uma vez o TObjBase 
    Dim DtTObjBase As DataTable, DtObjClas As DataTable, DtObjLay As DataTable, DtObjFamily As DataTable
    Dim PathFolder As String
    'Sera utilizado para editar os dados do TObjBase
    Dim DtObjBaseUpdate As DataTable, ObjIDCurrent As Autodesk.AutoCAD.DatabaseServices.ObjectId
    Dim DataTable_Layer As New System.Data.DataTable

    'SelectionSet com os objetos selecionados pelo usuario 
    Dim SelSet_Objects As Object

#End Region

#Region "----- Constructors -----"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        LibraryError = New LibraryError
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        LibraryError = Nothing
        MyBase.Finalize()
    End Sub

#End Region

#Region "----- Events -----"

#Region "----- ComboBox -----"

    Private Sub cboAngle_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboAngle.KeyPress
        AutoComplete(cboAngle, e, True)
    End Sub

    Private Sub cboAngle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAngle.SelectedIndexChanged

    End Sub

    Private Sub cboUnit_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cboUnit.KeyPress
        AutoComplete(cboUnit, e, True)
    End Sub

    Private Sub cboUnit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboUnit.SelectedIndexChanged

    End Sub

    Private Sub cboLayer_KeyPress(ByVal sender As Object, ByVal e As Windows.Forms.KeyPressEventArgs) Handles cboLayer.KeyPress
        AutoComplete(cboLayer, e, True)
    End Sub

    Private Sub cboLayer_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboLayer.SelectedIndexChanged

    End Sub

    Private Sub cboClas_KeyPress(ByVal sender As Object, ByVal e As Windows.Forms.KeyPressEventArgs) Handles cboClas.KeyPress
        AutoComplete(cboClas, e, True)
    End Sub

    Private Sub cboClas_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboClas.SelectedIndexChanged

    End Sub

    Private Sub cboFamily_KeyPress(ByVal sender As Object, ByVal e As Windows.Forms.KeyPressEventArgs) Handles cboFamily.KeyPress
        AutoComplete(cboFamily, e, True)
    End Sub

    Private Sub cboFamily_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboFamily.SelectedIndexChanged

    End Sub

    Private Sub cboFamily_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboFamily.SelectedValueChanged

        'Limpamos o combo de classes antes 
        'cboClas.Items.Clear() : cboClas.Text = ""

        'Dim TObjClasBLL As New TObjClasBLL
        'With TObjClasBLL
        '    .CLASFAMILY = Int16.Parse(MenuItemData.Valor)
        '    .FillCbo_TOBJCLAS(cboClas)
        'End With
        'TObjClasBLL = Nothing

    End Sub

#End Region

#Region "----- CheckedBox -----"

    Private Sub chkCurrentLayer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCurrentLayer.CheckedChanged
        If chkCurrentLayer.Checked Then
            cboLayer.Enabled = False

            Dim LayerCurrent As String
            Dim LibraryCad As New LibraryCad
            LayerCurrent = LibraryCad.GetCurrentLayer
            LibraryCad = Nothing
            cboLayer.Text = LayerCurrent

        Else
            cboLayer.Enabled = True
        End If
    End Sub

#End Region

#Region "----- CommandButton -----"

    Private Sub btnSelectObj_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectObj.Click
        SelectObjectsDrawing()
    End Sub

    Private Sub btnPtoInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPtoInsert.Click
        'A alteração do ponto base esta em Lisp. Iremos repassar o comando abaixo com o handle do objeto selecionado ou criado pelo usuário 

        Dim pathtodwg As String = "C:\Program Files\AutoPOWER 2015\DWG\apwr_logcab02.dwg"
        Dim db As Autodesk.AutoCAD.DatabaseServices.Database = New Autodesk.AutoCAD.DatabaseServices.Database(False, False)
        db.ReadDwgFile(pathtodwg, System.IO.FileShare.Read, True, Nothing)
        Using Trans As Autodesk.AutoCAD.DatabaseServices.Transaction = db.TransactionManager.StartTransaction
            Dim curblock As String = "ahlbord" & Now.Minute.ToString & Now.Second.ToString
            Dim curtag As String = "REV"
            Dim bktbl As Autodesk.AutoCAD.DatabaseServices.BlockTable = db.BlockTableId.GetObject(Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead)
            Dim revblk As Autodesk.AutoCAD.DatabaseServices.BlockTableRecord = bktbl(curblock).GetObject(Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead)
            Dim refs As Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection = revblk.GetBlockReferenceIds(False, False)
            For Each ref In refs
                Dim bref As Autodesk.AutoCAD.DatabaseServices.BlockReference = Trans.GetObject(ref, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead)
                Dim atts As Autodesk.AutoCAD.DatabaseServices.AttributeCollection = bref.AttributeCollection
                For Each obj In atts
                    Dim att As Autodesk.AutoCAD.DatabaseServices.AttributeReference
                    att = Trans.GetObject(obj, Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead, False)
                    If att.Tag = curtag Then
                        Autodesk.AutoCAD.ApplicationServices.Application.ShowAlertDialog(att.TextString)
                    End If
                Next
            Next
        End Using

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        DeleteRegister()
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        SaveRegister()
    End Sub

    Private Sub btnColor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnColor.Click
        Dim LibraryCadCommands As New LibraryCommand
        Dim ColorSelected As Autodesk.AutoCAD.Colors.Color

        With LibraryCadCommands
            ColorSelected = .ShowColorDialog
            PicColor.BackColor = ColorSelected.ColorValue
            lblColor.Text = ColorSelected.ColorIndex.ToString
        End With
        LibraryCadCommands = Nothing

    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
        If MsgBox("Deseja limpar os campos para cadastro de novo símbolo ?", MsgBoxStyle.YesNo, "Novo símbolo") = MsgBoxResult.No Then Exit Sub
        ClearFields(False)
        txtObjLeg.Focus()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        Me.Dispose(True)
    End Sub

    Private Sub btnSelectBlock_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSelectBlock.Click

        ''Verifica se ja foi salvo o simbolo em tela
        'If txtCodObj.Text = "" Then
        '    MsgBox("Salve os dados primeiro do símbolo que deseja selecionar. Para relacionar um bloco à um símbolo será necessário ter o registro cadastrado primeiro !", MsgBoxStyle.Information, "Símbolo inválido")
        '    btnSelectBlock.Focus()
        '    Exit Sub
        'End If

        'Dim ReturnSelectBlock As Object = Nothing, ReturnBlockCreate As String
        'Dim LibraryCadUI As New LibraryCadUI
        'Dim LibraryComponent As New Thorx.Library.Framework.LibraryComponent

        'Try

        '    With LibraryCadUI
        '        ReturnSelectBlock = .SelectBlockDwg
        '        'Se retornou erro nao continua a funcao 
        '        If ReturnSelectBlock(0).ToString.StartsWith("@@") Then
        '            MsgBox(ReturnSelectBlock(0).ToString.Replace("@@", ""), MsgBoxStyle.Information, "Error select block")
        '            Exit Sub
        '        End If

        '        'Guardamos o objectId selecionado. Se o usuario clicar em outro simbolo ou efetuar alguma açao, o ObjectId eh llimpo
        '        ObjIDCurrent = ReturnSelectBlock(1)

        '        'Cria o bloco e retorna o selecionado com sucesso
        '        ReturnBlockCreate = .BlkPreview(ReturnSelectBlock(0))
        '        If ReturnBlockCreate.ToString.StartsWith("@@") Then
        '            MsgBox(ReturnBlockCreate.ToString.Replace("@@", ""), MsgBoxStyle.Information, "Error saving block")
        '            Exit Sub
        '        End If

        '        'Carrega a imagem dentro do formulario (PictureBox)
        '        LibraryComponent.LoadBmpToPicutreBox(ReturnBlockCreate, Slide)
        '        'Coloca no campo somente os nome do arquivo salvo 
        '        lblObjMenu.Text = Dir(ReturnBlockCreate)

        '    End With

        'Catch ex As Exception
        '    LibraryError.CreateErr(Err, True, "Erro ao selecionar o bloco para símbolos - " & ex.Message, mVar_NameClass, , , , "FRMEDITORSYMBOL001", mVar_NameApplication, mVar_NameOwner)
        'Finally
        '    LibraryCadUI = Nothing : LibraryComponent = Nothing
        'End Try

    End Sub

#End Region

#Region "----- Forms -----"

    Private Sub frmEditorSymbol_Disposed(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Disposed
        frm_EditorSymbol = Nothing
    End Sub

    Private Sub frmEditorSymbol_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        DataTable_Layer = Nothing

        FillCbo_Family()
        FillCbo_TObjClas()
        FillCbo_Layer()
        'FillTrv_TObjBase()

    End Sub

#End Region

#Region "----- TreeView -----"

    Private Sub trvSymbol_AfterSelect(ByVal sender As Object, ByVal e As Windows.Forms.TreeViewEventArgs) Handles trvSymbol.AfterSelect

        'Try

        '    ClearFields()
        '    If e.Node.Tag.ToString.Contains("NODE0") Then
        '        'Aqui temos o cabeçalho do Treeview 
        '    Else

        '        Dim CodObj As String
        '        Dim TObjBaseBLL As New TOBJBASEBLL
        '        With TObjBaseBLL

        '            CodObj = e.Node.Tag.ToString.Split("|")(1)
        '            If DtTObjBase.Select(._CODOBJ & " = '" & CodObj & "'").Length > 0 Then

        '                Dim Dr As DataRow = DtTObjBase.Select(._CODOBJ & " = '" & CodObj & "'")(0)
        '                txtObjLeg.Text = Dr(._OBJLEG).ToString
        '                lblObjMenu.Text = Dr(._OBJMENU).ToString
        '                txtCodObj.Text = Dr(._CODOBJ).ToString

        '                If Dr(._CODLAY).ToString <> "" Then
        '                    Dim DrLay As DataRow = DtObjLay.Select(._CODLAY & " = " & Dr(._CODLAY).ToString)(0)
        '                    cboLayer.Text = DrLay(DtObjLay.Columns(1).ColumnName.ToString).ToString
        '                Else
        '                    cboLayer.Text = ""
        '                End If

        '                'Consulta a Familia relacionada com a classe corrente 
        '                If Dr(._CODCLAS).ToString <> "" Then
        '                    Dim DrClas As DataRow = DtObjClas.Select(._CODCLAS & " = " & Dr(._CODCLAS))(0)
        '                    Dim DrFamily As DataRow = DtObjFamily.Select(._CLASFAMILY & " = " & DrClas(._CLASFAMILY))(0)
        '                    cboFamily.Text = DrFamily(._FAMILYDES).ToString
        '                Else
        '                    cboFamily.Text = "" : cboClas.Text = ""
        '                End If

        '                'Coloca a classe relacionado 
        '                cboClas.Text = e.Node.Parent.Text

        '                'Neste caso o símbolo não possui bloco relacionado 
        '                If lblObjMenu.Text = "" Then
        '                    lblObjMenu.Text = TextNotInfo : lblObjMenu.ForeColor = Drawing.Color.Red
        '                Else
        '                    lblObjMenu.ForeColor = Drawing.Color.Blue
        '                    'Carrega a imagem caso a mesma exista 
        '                    If My.Computer.FileSystem.FileExists(PathFolder & My.Settings.NameFolderDWG & "\" & Dr(._OBJMENU).ToString) Then
        '                        Dim LibraryComponent As New Thorx.Library.Framework.LibraryComponent
        '                        LibraryComponent.LoadBmpToPicutreBox(PathFolder & My.Settings.NameFolderDWG & "\" & Dr(._OBJMENU).ToString, Slide)
        '                    End If
        '                End If

        '                'Trata a cor relacionada ao simbolo
        '                Dim CorCad As Autodesk.AutoCAD.Colors.Color
        '                CorCad = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, Dr(._OBJCOR))
        '                PicColor.BackColor = CorCad.ColorValue
        '                lblColor.Text = CorCad.ColorIndex.ToString

        '            End If

        '        End With

        '    End If


        'Catch ex As Exception
        '    LibraryError.CreateErr(Err, True, "Error selection object - " & ex.Message, mVar_NameClass, , , , "FRMEDITORSYMBOL002", mVar_NameApplication, mVar_NameOwner)
        'End Try

    End Sub

#End Region

#End Region

#Region "----- Functions for Fill components -----"

    Function FillTrv_TObjBase() As Object

        'Dim CodClas As String = ""
        'Dim cboClasItem As Thorx.CadSolution.BLL.MeuItemData

        'Try

        '    If cboClas.Text <> "" Then cboClasItem = cboClas.SelectedItem : CodClas = cboClasItem.Valor.ToString
        '    Dim TObjBaseBLL As New TOBJBASEBLL
        '    With TObjBaseBLL
        '        If chkAllSymbols.Checked = False And CodClas <> "" Then .CODCLAS = CodClas
        '        DtTObjBase = Nothing : DtTObjBase = New DataTable
        '        DtTObjBase = .FillTreeView_TObjClas(trvSymbol, "", Nothing)
        '        'Para consulta e tratamentos de dados diretamente com a tabela. Nao podemos usar a de cima pois ela possui mais dados fora a tabela TObjBase 
        '        'DtTObjBase = .S_sp_TOBJBASE
        '    End With
        '    TObjBaseBLL = Nothing

        'Catch ex As Exception

        'End Try

        Return True

    End Function

    'Povoa os dados relacionados ao combo de familias
    Function FillCbo_Family() As Object

        Try

            Return True

        Catch ex As Exception

            Return False
        End Try

    End Function

    'Povoa os dados relacionados ao combo de layers 
    Function FillCbo_TObjClas() As Object

        Try

            Dim AhidBLL As New AhidBLL.TObjClasBLL
            AhidBLL.FillCbo_TOBJCLAS(cboClas)
            AhidBLL = Nothing

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    'Povoa os dados relacionados ao combo de layers 
    Function FillCbo_Layer() As Object

        Try

            If DataTable_Layer Is Nothing Then
                If ConnAhid Is Nothing Then
                    Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
                    If ConnAhid Is Nothing Then
                        With LibraryConnection
                            .TypeDb = "AHID_"
                            ConnAhid = .Aenge_OpenConnectionDB()
                        End With
                    End If
                    LibraryConnection = Nothing
                End If

                Dim Da As New OleDb.OleDbDataAdapter("Select * From TObjLay Order By LayNome ASC", ConnAhid)
                If DataTable_Layer Is Nothing Then DataTable_Layer = New DataTable
                Da.Fill(DataTable_Layer)
            End If

            Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent
            LibraryComponent.FillCombo(cboLayer, DataTable_Layer, "LayNome", "CodLay", True)
            LibraryComponent = Nothing
            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "----- Functions for save, edit e delete registers -----"

    'Neste caso iremos excluir a linha do datatable assim como o bmp (alterar seu nome, nao deletar por seguranca)
    Function DeleteRegister() As Object

        'Try

        '    Dim TObjBaseBLL As New TOBJBASEBLL

        '    If txtCodObj.Text = "" Then
        '        MsgBox("Selecione um símbolo válido a ser excluido !", MsgBoxStyle.Information, "Símbolo inválido")
        '        trvSymbol.Focus()
        '        Return False
        '    End If

        '    If MsgBox("Deseja realmente excluir o símbolo selecionado ?" & Chr(13) & "Atenção : Ao excluir o símbolo todos os seus dados ficarão inacessíveis. Deseja continuar ?", MsgBoxStyle.YesNo, "EXCLUSÃO de símbolos") = MsgBoxResult.No Then Return False

        '    With TObjBaseBLL

        '        If DtObjBaseUpdate.Select(._CODOBJ & " = '" & txtCodObj.Text & "'").Length <= 0 Then
        '            MsgBox("Selecione um símbolo válido a ser excluido !", MsgBoxStyle.Information, "Símbolo inválido")
        '            trvSymbol.Focus()
        '            Return False
        '        Else

        '            Dim DrDelete As DataRow, NameFile As String = ""
        '            DrDelete = DtObjBaseUpdate.Select(._CODOBJ & " = '" & txtCodObj.Text & "'")(0)
        '            NameFile = DrDelete(._OBJMENU).ToString
        '            DtObjBaseUpdate.Rows.Remove(DrDelete)
        '            'Passa o novo datatable sem a linha desejada (excluida)
        '            .IU_sp_TOBJBASE(DtObjBaseUpdate)
        '            'Exclui o arquivo de visualizacao criado
        '            If My.Computer.FileSystem.FileExists(PathFolder & My.Settings.NameFolderDWG & "" & NameFile) Then My.Computer.FileSystem.RenameFile(PathFolder & My.Settings.NameFolderDWG & "" & NameFile, NameFile & "_Deleted")

        '            ClearFields()
        '            'Temos de atualizar o treeview com as novas informacoes 
        '            FillTrv_TObjBase()
        '        End If

        '    End With

        '    TObjBaseBLL = Nothing
        Return True

        'Catch ex As Exception
        '    LibraryError.CreateErr(Err, True, "Error deleting information symbol - " & ex.Message, mVar_NameClass, , , , "FRMEDITORSYMBOL004", mVar_NameApplication, mVar_NameOwner)
        '    Return False
        'End Try

    End Function

    'Salva os dados do registro corrente em tela, ou edita seus dados 
    Function SaveRegister() As Object

        'Try

        '    If ValidateFields() = False Then Return False

        '    'Verifica se ja existe o símbolo a ser salvo
        '    Dim NewReg As Boolean = False, cboLayItem, cboClasItem As Thorx.CadSolution.BLL.MeuItemData
        '    Dim TObjBaseBLL As New TOBJBASEBLL
        '    Dim LibraryFormat As New Thorx.Library.Framework.LibraryFormat
        '    If txtCodObj.Text = "" Then NewReg = True

        '    With TObjBaseBLL
        '        If txtCodObj.Text <> "" Then If DtObjBaseUpdate.Select(._CODOBJ & " = " & txtCodObj.Text).Length <= 0 Then NewReg = True

        '        'Iremos acrescentar uma nova linha no datatable 
        '        If NewReg Then
        '            'Pega o item selecionado na classe
        '            cboLayItem = cboLayer.SelectedItem
        '            cboClasItem = cboClas.SelectedItem

        '            Dim CodObjNew As String = .S_sp_MAXCODOBJ(DtObjBaseUpdate)
        '            CodObjNew = LibraryFormat.CompleteStringWithChar((Integer.Parse(CodObjNew) + 1).ToString, 6, "0")
        '            txtCodObj.Text = CodObjNew

        '            Dim DrInsert As DataRow
        '            DrInsert = DtObjBaseUpdate.NewRow
        '            DrInsert(._CODOBJ) = CodObjNew
        '            DrInsert(._OBJLEG) = txtObjLeg.Text
        '            If lblObjMenu.Text <> TextNotInfo And lblObjMenu.Text <> "-" Then DrInsert(._OBJMENU) = lblObjMenu.Text
        '            DrInsert(._CODCLAS) = cboClasItem.Valor 'LibraryFormat.CompleteStringWithChar(cboClas.SelectedValue.ToString, "0", 4)
        '            DrInsert(._CODLAY) = cboLayItem.Valor
        '            If IsNumeric(lblColor.Text) Then DrInsert(._OBJCOR) = lblColor.Text
        '            'Os campos abaixo inicialmente serão fixos para a tabela 
        '            DrInsert("OBJESC") = "REAL"
        '            DrInsert("OBJLTYPE") = "BYLAYER"
        '            DrInsert("ERASED") = "0"
        '            DtObjBaseUpdate.Rows.Add(DrInsert)

        '            'Neste caso iremos somente editar as informações 
        '        Else
        '            'Pega o item selecionado na classe
        '            cboLayItem = cboLayer.SelectedItem
        '            cboClasItem = cboClas.SelectedItem

        '            Dim DrUpdate As DataRow
        '            DrUpdate = DtObjBaseUpdate.Select(._CODOBJ & " = " & txtCodObj.Text)(0)
        '            DrUpdate.BeginEdit()
        '            DrUpdate(._OBJLEG) = txtObjLeg.Text
        '            If lblObjMenu.Text <> TextNotInfo And lblObjMenu.Text <> "-" Then DrUpdate(._OBJMENU) = lblObjMenu.Text
        '            DrUpdate(._CODCLAS) = cboClasItem.Valor 'LibraryFormat.CompleteStringWithChar(cboClas.SelectedValue.ToString, "0", 4)
        '            DrUpdate(._CODLAY) = cboLayItem.Valor
        '            If IsNumeric(lblColor.Text) Then DrUpdate(._OBJCOR) = lblColor.Text
        '            DrUpdate.EndEdit()
        '        End If

        '        'Envia o DataTable para atualizar os dados de simbolos 
        '        .IU_sp_TOBJBASE(DtObjBaseUpdate)

        '        'Reconsulta os dados do datatable global de controle 
        '        'DtObjBaseUpdate = Nothing : DtObjBaseUpdate = New DataTable
        '        'DtObjBaseUpdate = TObjBaseBLL.S_sp_TOBJBASE

        '        'Salva agora o bloco a ser utilizado em inserções 
        '        If Not ObjIDCurrent.IsNull Then
        '            Dim LibraryCadUI As New LibraryCadUI
        '            With LibraryCadUI
        '                .SaveBlockEntity(ObjIDCurrent, PathFolder & My.Settings.NameFolderDWG & "\" & lblObjMenu.Text.ToLower.Replace(".bmp", ".dwg"))
        '            End With
        '            LibraryCadUI = Nothing
        '        End If

        '        System.Windows.Forms.Application.DoEvents()
        '        'Temos de atualizar o treeview com as novas informacoes 
        '        FillTrv_TObjBase()
        '    End With

        '    TObjBaseBLL = Nothing
        Return True

        'Catch ex As Exception
        '    LibraryError.CreateErr(Err, True, "Error saving information symbol - " & ex.Message, mVar_NameClass, , , , "FRMEDITORSYMBOL003", mVar_NameApplication, mVar_NameOwner)
        '    Return False
        'End Try

    End Function

#End Region

#Region "----- Functions for general procedures -----"

    'Fun;cao que seleciona os objetosem um desenho. Retorna uma lista no selectionSet ou lista de handles 
    Function SelectObjectsDrawing() As Object

        If ValidateFields() = False Then Return False

        'Antes de iniciar os procedimentos, verificamos se já existe o nome no desenho do bloco por questao de integridade
        Dim LibraryC As New LibraryCommand
        If LibraryC.ExistNameBlock(txtObjMenu.Text) Then
            MsgBox("O nome do símbolo que está sendo criado já se encontra sendo utilizado no desenho corrente. Caso queira criar com o nome desejado (" & txtObjMenu.Text & "), favor copie-o e cole-o em um novo arquivo DWG !", MsgBoxStyle.Information, "Nome existente")
            Return False
        End If

        Me.Hide()

        'Primeiro, fazemos a selecao do ponto, Se nao retornar um ponto valido iremos cancelar o resto da funcao
        Dim PointSelected As Object = LibraryC.SelectPointDrawing("Informe o ponto base do símbolo : ")
        If PointSelected Is Nothing Then
            MsgBox("Não foi informado um ponto válido. A função foi cancelada !", MsgBoxStyle.Information, "Ponto inválido")
            Return False
        End If

        'Obtem a lista com os objectids do desenho 
        SelSet_Objects = LibraryC.SelectObjectsDrawing("objectid")

        'Com os dados, cria-se agora o bloco com as informações selecionadas 
        LibraryC.CreateBlockReference(PointSelected, txtObjMenu.Text, 1, "0", Nothing, SelSet_Objects)

        LibraryC = Nothing

        Me.Visible = True
        Return SelSet_Objects

    End Function

    Private Sub ClearFields(Optional ByVal ClearCbo As Boolean = True)
        txtObjLeg.Text = ""
        txtObjMenu.Text = ""
        If ClearCbo Then
            cboFamily.Text = ""
            cboClas.Text = ""
            cboLayer.Text = ""
        End If
        'Caso esteja limpo o campo colocamos a cor azul normal 
        lblObjMenu.ForeColor = Drawing.Color.Blue
        lblObjMenu.Text = "-"
        txtCodObj.Text = ""
        Slide.Image = Nothing

        PicColor.BackColor = Drawing.Color.Transparent
        lblColor.Text = ""

        ObjIDCurrent = Nothing
    End Sub

    'Valida se os campos foram informados corretamente 
    Function ValidateFields() As Boolean

        If txtObjLeg.Text = "" Then
            MsgBox("Informe uma descrição válida para o símbolo primeiro !", MsgBoxStyle.Information, "Campo inválido")
            txtObjLeg.Focus()
            Return False
        End If

        If cboFamily.Text = "" Then
            MsgBox("Informe uma família válida para o símbolo primeiro !", MsgBoxStyle.Information, "Campo inválido")
            cboFamily.Focus()
            Return False
        End If

        If cboClas.Text = "" Then
            MsgBox("Informe uma classe válida para o símbolo primeiro !", MsgBoxStyle.Information, "Campo inválido")
            cboClas.Focus()
            Return False
        End If

        If cboLayer.Text = "" Then
            MsgBox("Informe um layer válido para o símbolo primeiro !", MsgBoxStyle.Information, "Campo inválido")
            cboLayer.Focus()
            Return False
        End If

        Return True

    End Function

    Function ValidateSymbolToSelect() As Boolean
        If txtCodObj.Text = "" Then

        End If

        Return True
    End Function

#End Region

End Class
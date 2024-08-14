Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.Windows
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.Colors
Imports Autodesk.AutoCAD.Interop
Imports System.IO
Imports System.Collections

Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Geometry
Imports Autodesk.AutoCAD

'Imports Excel = Microsoft.Office.Interop.Excel

'Seta a instancia do assembly a ser trabalhada
'<Assembly: CommandClass(GetType(LibraryCadCommands))> 

Public Class LibraryCommand

#Region "----- Documentação -----"

    '==============================================================================================================================================
    'Projeto : CadSolution 1.0
    'Empresa : Thorx Sistemas e Consultoria Ltda
    'Reponsável criação : Raul Antonio Fernandes Junior    
    'Data de criação : 13/03/2009
    'Objetivo : Formulário responsável pelo cadastro das informações de objetos e comandos relacionados com o Cad. Nesta libraru, temos as seguintes funcionalidades :
    ' Execução de comandos (linha de comando) para ser enviado ao Cad
    ' Exportação de dados para arquivos excel, de acordo com as células desejadas
    ' Chamada para o lisp de registros de usuários no regedit so sistema 
    ' Retorno de dados de usuários que foram cadastrados no registro e dos respectivos caminhos do sistema.
    '
    '
    'Informações adicionais - Tratamento de Erros :
    'LibraryCadCommands001 - ExportExcel_TypeFile1
    'LibraryCadCommands002 - LoadFunction_Application
    'LibraryCadCommands003 - 
    'LibraryCadCommands004 - 
    'LibraryCadCommands005 -
    'LibraryCadCommands006 - 
    'LibraryCadCommands007 - 
    'LibraryCadCommands008 -
    'LibraryCadCommands009 -
    '==============================================================================================================================================

#End Region

#Region "----- Construtores -----"

    Public Sub New()
        LibraryError = New LibraryError
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        LibraryError = Nothing
    End Sub

#End Region

#Region "----- Atributos e Declarações -----"

    Private Shared LibraryReference As LibraryReference, LibraryError As LibraryError, mVar_NameClass As String = "LibraryCadCommands", LibraryRegister As LibraryRegister
    'Var for iniapp information
    Dim Register_KeyUser As String = "", Register_KeyTypeUser As String = "", Register_KeyDepart As String = "", Register_KeyAutoCadApplication As String = "", FileName_DllMain As String = ""
    Dim CabTipo_CadastroCFG, CadastroCFG, CabFU_CadastroCFG As String

#End Region

#Region "----- Get e Set -----"

    'Property oSheetSelected() As String
    '    Get
    '        oSheetSelected = mVar_OSheet
    '    End Get
    '    Set(ByVal value As String)
    '        mVar_OSheet = value
    '    End Set
    'End Property

#End Region

#Region "----- Dialogs - Comandos de interface com o usuário LISP/VB -----"

    'Verifica se já existe um bloco com um determinado nome no desenho. Se existir, deve pedir para o usuário criar um diferente 
    Public Function ExistNameBlock(ByVal NameBlock As String) As Object

        Try

            Dim db As Database = HostApplicationServices.WorkingDatabase
            Using tr As Transaction = db.TransactionManager.StartTransaction
                Dim bt As BlockTable = db.BlockTableId.GetObject(OpenMode.ForRead)
                Dim btrMS As BlockTableRecord = bt(BlockTableRecord.ModelSpace).GetObject(OpenMode.ForRead)
                If Not bt.Has(NameBlock) Then
                    Return False
                Else
                    Return True
                End If
                tr.Abort()
            End Using

            db = Nothing

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    'Função que informa um determinado ponto no desenho. Neste caso normalmente eh utilizado para a parte de blocos 
    Public Function SelectPointDrawing(ByVal PromptText As String) As Object

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database
        Dim ed As Editor = doc.Editor

        Dim pSelOpt As PromptPointOptions = New PromptPointOptions("")
        pSelOpt.Message = "Informe o ponto no desenho :"
        If PromptText <> "" Then pSelOpt.Message = PromptText
        Dim pSelRes As PromptPointResult = ed.GetPoint(pSelOpt)

        'Basic error handling
        Try
            If (pSelRes.Status <> PromptStatus.OK) Then
                Return Nothing
            End If

            Dim PointSelected As Point3d = pSelRes.Value
            Return PointSelected

        Catch ex As Exception
            ed.WriteMessage(ex.ToString())
            Return Nothing
        End Try

    End Function

    'Função que seleciona no desenho objetos e armazena em um selectionset
    'TypeReturn : Handle or objectid
    Public Function SelectObjectsDrawing(ByVal TypeReturn As String) As Object

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database
        Dim ed As Editor = doc.Editor

        Dim pSelOpt As New PromptSelectionOptions
        pSelOpt.MessageForAdding = "Selecione os objetos :"
        Dim pSelRes As PromptSelectionResult = ed.GetSelection(pSelOpt)
        Dim dbObj As DBObject

        Using tr As Transaction = db.TransactionManager.StartTransaction

            ' Basic error handling
            Try
                If (pSelRes.Status <> PromptStatus.OK) Then
                    Return Nothing
                End If

                Dim objIdArray() As ObjectId = pSelRes.Value.GetObjectIds()
                Dim HandleArray As New ArrayList

                If TypeReturn.ToLower = "handle".ToLower Then
                    For Each objId As ObjectId In objIdArray
                        dbObj = tr.GetObject(objId, OpenMode.ForRead)
                        HandleArray.Add(dbObj.Handle.ToString)
                        'ed.WriteMessage(dbObj.GetType.ToString + vbCrLf)
                    Next
                End If

                tr.Commit()
                'Retorna os ObjectsIds selecionados pelo usuário 
                If TypeReturn.ToLower = "handle".ToLower Then
                    Return HandleArray
                Else
                    Return objIdArray
                End If

            Catch ex As Exception
                ed.WriteMessage(ex.ToString())
                tr.Abort()
                Return Nothing
            End Try

        End Using
    End Function

    'Cria um novo bloco com as propriedades de acordo com o informado pelo usuário
    Public Function CreateBlockReference(ByVal pntInsert As Geometry.Point3d, ByVal strBlockName As String, ByVal dScale As Double, ByVal strLayerName As String, ByVal arrAttrValues As ArrayList, ByVal arrObjectIdSelecteds As Object) As Object

        Dim doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        Dim dwg As Database = doc.Database

        'Dim res As PromptSelectionResult = ed.GetSelection
        'Dim SS As Autodesk.AutoCAD.EditorInput.SelectionSet = res.Value
        Dim idArray As ObjectId() = arrObjectIdSelecteds 'SS.GetObjectIds()
        Dim tm As Autodesk.AutoCAD.DatabaseServices.TransactionManager = dwg.TransactionManager

        Dim trans As Transaction = tm.StartTransaction

        Try
            ' create the new block definition              
            Dim newBlockDef As BlockTableRecord = New BlockTableRecord
            newBlockDef.Name = strBlockName

            Dim blockTable As BlockTable = trans.GetObject(dwg.BlockTableId, OpenMode.ForRead)

            If (blockTable.Has(strBlockName) = False) Then
                blockTable.UpgradeOpen()
                ' Add the BlockTableRecord
                blockTable.Add(newBlockDef)
                trans.AddNewlyCreatedDBObject(newBlockDef, True)


                For Each id In arrObjectIdSelecteds
                    Dim ent = TryCast(trans.GetObject(id, OpenMode.ForRead), Entity)
                    If ent <> Nothing Then
                        Dim newent = TryCast(ent.Clone(), Entity)
                        If newent <> Nothing Then
                            newBlockDef.AppendEntity(newent)
                        End If
                    End If
                Next

                ' ''For Each acSSobj As ObjectId In arrObjectIdSelecteds
                ' ''    If Not IsDBNull(acSSobj) Then
                ' ''        Dim acEnt As Entity = trans.GetObject(acSSobj, OpenMode.ForRead)
                ' ''        If Not IsDBNull(acEnt) Then
                ' ''            newBlockDef.AppendEntity(acEnt)
                ' ''        End If
                ' ''    End If
                ' ''Next



                'Declare a BlockReference variable
                Dim blockRef As BlockReference = New BlockReference(pntInsert, newBlockDef.ObjectId)
                Dim curSpace As BlockTableRecord = trans.GetObject(dwg.CurrentSpaceId, OpenMode.ForWrite)
                curSpace.AppendEntity(blockRef)
                ' Tell the transaction about the new block reference
                trans.AddNewlyCreatedDBObject(blockRef, True)
                ' Commit the transaction
                trans.Commit()
            End If
        Catch ex As Exception
            ' If an error occurs the details of the problem will be printed
            ' on the AutoCAD command line.
            ed.WriteMessage("a problem occured because " + ex.Message)
        Finally
            'Dispose the transaction by calling the Dispose method
            trans.Dispose()
        End Try

    End Function

    'Open a file 
    <LispFunction("openfile")> _
    Public Function OpenFile_Net(ByVal myLispArgs As ResultBuffer) As ResultBuffer
        Dim NameFile As String = "", rbfResult As ResultBuffer

        Try

            NameFile = myLispArgs.AsArray(0).Value
            With My.Computer.FileSystem
                If .FileExists(NameFile) Then

                    Dim Proc As New System.Diagnostics.Process
                    Proc.StartInfo.WorkingDirectory = .GetFileInfo(NameFile).DirectoryName   '"C:\"
                    Proc.StartInfo.FileName = Dir(NameFile)
                    Proc.Start()

                End If
                'Shell(NameFile, AppWinStyle.NormalFocus, False)
            End With

            rbfResult = New ResultBuffer(New TypedValue(LispDataType.Text, "True"))

        Catch ex As Exception
            rbfResult = New ResultBuffer(New TypedValue(LispDataType.Text, "False"))
        End Try

        Return rbfResult

    End Function

    'Get a full path of file and delete this file, return true and false for delete. Exclude one a one files 
    <LispFunction("deletefilelisp")> _
    Public Function DeleteFile_Lisp(ByVal myLispArgs As ResultBuffer) As ResultBuffer
        Dim NameFile As String = "", rbfResult As ResultBuffer

        Try

            NameFile = myLispArgs.AsArray(0).Value
            With My.Computer.FileSystem
                If .FileExists(NameFile) Then .DeleteFile(NameFile)
            End With

            rbfResult = New ResultBuffer(New TypedValue(LispDataType.Text, "True"))

        Catch ex As Exception
            rbfResult = New ResultBuffer(New TypedValue(LispDataType.Text, "False"))
        End Try

        Return rbfResult

    End Function

#End Region

#Region "----- Funcionalidades de configurações - Load e functions (Automatizados) -----"

#End Region

#Region "----- Funcionalidades relacionadas à registro de usuário -----"

#End Region

#Region "----- Funcionalidades de Dialogs e Funções do windows -----"

    'Chama a tela de cores e retorna a cor selecionada pelo usuário. Se o parametro for sim para returncadcolor, entao nao tratamos a cor e retornamos com cor de cad mesmo
    'Dim CorCad As Autodesk.AutoCAD.Colors.Color
    'CorCad = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.None, CorAdicionado)
    'pnlAdicionado.BackColor = CorCad.ColorValue
    Function ShowColorDialog(Optional ByVal ReturnCadColor As Boolean = True) As Object

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim db As Database = doc.Database
        Dim ed As Editor = doc.Editor

        Try

            Dim cd As New ColorDialog()
            Dim dr As System.Windows.Forms.DialogResult = cd.ShowDialog()

            If dr = System.Windows.Forms.DialogResult.OK Then
                If ReturnCadColor Then
                    Return cd.Color
                Else
                    Return cd.Color.ColorValue
                End If
            Else
                Return Nothing
            End If

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    'Return server path and configurate application s
    Function GetFolder_Application(Optional ByVal ExibeMensagem As Boolean = True) As String

        Dim ResultDialog As System.Windows.Forms.DialogResult, Arquivo As String = "", PathFolder As String = ""
        Dim ODialog As System.Windows.Forms.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog, PathFolder_Server As String = "", FileName_Server As String = ""

        With ODialog
            .ShowNewFolderButton = False
            .Description = "Informe a pasta aonde se encontra os arquivos DWG..."
            ResultDialog = .ShowDialog
            Arquivo = .SelectedPath
            If Arquivo.ToString = "" Then
                If ExibeMensagem = True Then MsgBox("Pasta não informada pelo usuário. Comando cancelado !", MsgBoxStyle.Information, "Diretório inválido")
                Return ""
            Else
                PathFolder = Arquivo
                If Mid(PathFolder, 1, PathFolder.ToString.Length) <> "\" Then PathFolder += "\"
            End If

        End With

        PathFolder = PathFolder.ToString.Replace("\\", "\")

        Return PathFolder

    End Function

    'Return a path of file work
    Function GetFile_Application(Optional ByVal ExibeMensagem As Boolean = True) As String

        Dim ResultDialog As System.Windows.Forms.DialogResult, Arquivo As String = "", PathFile As String = ""
        Dim ODialog As New System.Windows.Forms.OpenFileDialog, PathFolder_Server As String = "", FileName_Server As String = ""

        With ODialog
            .AddExtension = True
            .Filter = "Excel Files (*.xls)|*.*|Excel Files (*.xlsx)|*.*"
            .Title = "Informe o arquivo Excel a ser trabalhado..."
            ResultDialog = .ShowDialog
            Arquivo = .FileName

            If Arquivo.ToString = "" Then
                If ExibeMensagem = True Then MsgBox("O arquivo não foi informado corretamente. Verifique por favor !", MsgBoxStyle.Information, "Arquivo inválido")
                Return ""
            Else
                PathFile = Arquivo
            End If

        End With

        Return PathFile

    End Function

#End Region

End Class

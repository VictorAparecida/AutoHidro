#Region "----- Imports -----"

Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.DatabaseServices
Imports System.Runtime.InteropServices

Imports System.IO
Imports System.Text
Imports Autodesk.AutoCAD.Interop
Imports System.Drawing
Imports Autodesk.AutoCAD.Geometry

#End Region

'Seta a instancia do assembly a ser trabalhada
<Assembly: CommandClass(GetType(LibraryCad))> 

Public Class LibraryCad
    Implements IExtensionApplication

#Region "----- Documentation -----"

    '==============================================================================================================================================
    'Projeto : CadSolution 1.0
    'Empresa : Thorx Sistemas e Consultoria Ltda
    'Reponsável criação : Raul Antonio Fernandes Junior    
    'Data de criação : 13/03/2009
    'Objetivo : Formulário responsável pelo cadastro das informações de objetos relacionados ao dwg. Lista as diferenças dos objetos selecionados 
    '
    'Informações adicionais - Tratamento de Erros :
    'LibraryCad001 - ExtractObjectsFromFile
    'LibraryCad002 - GetUpper_NumberRetorno
    'LibraryCad003 - SetXData_Circuit
    'LibraryCad004 - AtributeVisible
    'LibraryCad005 - GetList_ClassReturn
    'LibraryCad006 - CreateDataTable
    'LibraryCad007 - SetXData
    'LibraryCad008 - CreatePathTree_CircuitoAuto
    'LibraryCad009 - CircuitoAuto_CreateCircuitOrder
    'LibraryCad010 - ObjectFromHandle_Interop
    'LibraryCad011 - OrderList_HandleCircuit
    'LibraryCad012 - ValidateR2
    'LibraryCad013 - ExplodeBlock
    'LibraryCad014 - ExistTerra
    'LibraryCad015 - CircuitoAuto_CreatePathInterruptor
    'LibraryCad016 - CircuitoAuto_TubWithReturn
    'LibraryCad017 - ExistsNeutro_Handle
    'LibraryCad018 - CircuitoAuto_CreatePathParalelo
    'LibraryCad019 - CircuitoAuto_PathParalelo_OnlyPhase
    'LibraryCad020 - CircuitoAuto_DiagnosticCircuit
    'LibraryCad021 - HighLight_Object
    'LibraryCad022 - ExtractObjectsFromFile
    'LibraryCad023 - ReturnExtractObjectsFromFile
    'LibraryCad024 - CircuitoAuto_UpdateTubCircTomada
    'LibraryCad025 - CircuitoAuto_FirstHandleQdc
    'LibraryCad026 - SprinkDrawing - Função do 2017, que faz o desenho como array dos objetos no desenho, considerando os parametros desejados.
    'LibraryCad027 - InsertBlk - Função que faz a inserção de um determinado bloco, recebendo como parametros o ponto de insercao, nome de blocos e outros 
    'LibraryCad028 - 
    'LibraryCad029 - 
    'LibraryCad030 - 
    'LibraryCad031 - 
    'LibraryCad032 - 
    'LibraryCad033 - 
    'LibraryCad034 - 
    'LibraryCad035 - 
    '==============================================================================================================================================

    'Codes for reading objects and anothers not using yet
    'For all atributos of block
    'If (Not CType(ent, BlockReference).AttributeCollection Is Nothing) Then
    '    Dim attId As ObjectId
    '    For Each attId In CType(ent, BlockReference).AttributeCollection
    '        Dim att As AttributeReference = TryCast(attId.GetObject(Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead, False), AttributeReference)
    '        att.UpgradeOpen()
    '        'att.ColorIndex = ColorSet1.ColorIndex
    '        att.DowngradeOpen()
    '    Next
    'End If

#End Region

#Region "----- Declare and Parameters -----"

    Dim CollectionArray As New Collection, LibraryError As LibraryError

    Private Shared Library_Reference As LibraryReference, Library_Register As LibraryRegister, FileVlx As String = "APWR.vlx", mVar_NameClass As String = "LibraryCad"
    Public Const IniName_FileMaterial As String = "DB_", SeparatorList As String = "|", IniName_FileMaterialSelection As String = "DBS_"

    Public ScreenTactual As Autodesk.AutoCAD.Geometry.Matrix3d
    Public DtMapeamento As System.Data.DataTable, DtParalelo As System.Data.DataTable
    Public DtTub As New System.Data.DataTable, DtCircuito As New System.Data.DataTable

    <DllImport("acad.exe", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.Cdecl)> _
    Private Shared Function Ads_Queueexpr(ByVal strExpr As String) As Integer
    End Function

    <DllImport("acad.exe", CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.Cdecl, EntryPoint:="?acedPostCommand@@YAHPB_W@Z")> _
    Private Shared Function AcedPostCommand(ByVal strExpr As String) As Integer
    End Function

#End Region

#Region "----- Constructors -----"

    Public Sub New()
        LibraryError = New LibraryError
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        Library_Reference = Nothing : LibraryError = Nothing
        DtMapeamento = Nothing : DtTub = Nothing
    End Sub

    'In this configurate, load all informations about Cad and functions of application
    Public Sub Initialize() Implements Autodesk.AutoCAD.Runtime.IExtensionApplication.Initialize
        AddHandler Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentActivated, AddressOf ActivatedAcadDocument
        AddHandler Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentCreated, AddressOf ActivatedAcadCreated
        AddHandler Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.BeginDocumentClose, AddressOf ActivatedAcadClosed

        If My.Settings.SetSecureLoad = "T" And Autodesk.AutoCAD.ApplicationServices.Application.Version.Major >= 19 And Autodesk.AutoCAD.ApplicationServices.Application.Version.Minor > 0 Then Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("SECURELOAD", 0)
        ' ''AddHandler Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentToBeDestroyed, AddressOf ActivatedAcadClosed
        '' ''AddHandler Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.CommandEnded, AddressOf OnCommandFinished

        Dim DocMan As DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager
        AddHandler DocMan.DocumentCreated, AddressOf callback_documentCreated

        Dim doc As Document
        For Each doc In DocMan
            Dim db As Database = doc.Database
            AddHandler db.BeginSave, AddressOf callback_BeginSave
            AddHandler db.SaveComplete, AddressOf callback_SaveComplete
        Next

    End Sub

    'Private Sub OnCommandFinished(ByVal sender As Object, ByVal e As CommandEventArgs)

    '    'If e.GlobalCommandName.ToString.ToLower = "INSERT".ToLower Then    'OPEN
    '    '    Dim doc As Document = DirectCast(sender, Document)
    '    'End If

    'End Sub

    Private Sub callback_documentCreated(ByVal sender As Object, ByVal e As DocumentCollectionEventArgs)
        If e.Document = Nothing Then
            Exit Sub
        End If

        Dim db As Database = e.Document.Database
        AddHandler db.BeginSave, AddressOf callback_BeginSave
        AddHandler db.SaveComplete, AddressOf callback_SaveComplete
    End Sub

    'SaveBegin handler
    Private Sub callback_BeginSave(ByVal sender As Object, ByVal e As DatabaseIOEventArgs)
        'Dim myDwg As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        'AcApp.ShowAlertDialog("BeginSave: " + e.FileName)
        'This validation is because AutoCad 2011 not save preview files and occurs errors in execution
        If IsProjectValid() = False Then Exit Sub
        If IsSaveImgThumbnail Then SavePreviewImage()
        System.Windows.Forms.Application.DoEvents()
    End Sub

    'SaveComplete handler
    Private Sub callback_SaveComplete(ByVal sender As Object, ByVal e As DatabaseIOEventArgs)

    End Sub

    Public Sub Terminate() Implements Autodesk.AutoCAD.Runtime.IExtensionApplication.Terminate

        Try

            'Dim Doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
            'Dim AcCad As AcadApplication = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication
            'AcCad.ActiveDocument.SendCommand("")

        Catch ex As Exception

        Finally

        End Try

    End Sub

#End Region

#Region "----- Functions call windows and forms -----"

    <CommandMethod("CallForm_Hom")> _
    Public Sub CallForm_Hom()
        Dim frmH As New frmHomologation
        frmH.Dt = DtMapeamento
        frmH.Dt2 = DtCircuito
        frmH.ShowDialog()
        frmH = Nothing
    End Sub

#End Region

#Region "----- AutoCad functions for reading and writing -----"

    <LispFunction("ExistGroup")> _
Function ExistGroup(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim AcadAp As Object = Application.AcadApplication, NameGroup As String   'AcadApplication
        Dim doc As Object = AcadAp.ActiveDocument, FindGroup As String = "F"   'AcadDocument
        Dim myDB As Object = doc.Database  'AcadDocument
        Dim Groups As Object = myDB.Groups 'AcadGroups
        Dim RbfResult As New ResultBuffer

        'Obtem as informações repassadas pelo lisp 
        NameGroup = rbf.AsArray(0).Value.ToString

        Try

            For Each myGroup As Object In Groups   'AcadGroup
                Dim myBlkName As [String] = myGroup.Name.ToString()
                If [String].Compare(myBlkName.ToLower(), NameGroup.ToLower) = 0 Then
                    FindGroup = "T"
                    Exit For
                End If
            Next

            'Retorna como string o maior número encontrado acrescentado de 1 
            With RbfResult
                .Add(New TypedValue(LispDataType.Text, FindGroup))
            End With
            Return RbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao validar as informações de groups de Cad.", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad006")
            Return Nothing
        End Try

    End Function

    'Transforma uma string em lista do lisp de acordo com o parametros fixo predefinido na aplicação 
    <LispFunction("StringToList")> _
    Function StringToList(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim RbfResult As New ResultBuffer, ArrayStr As Object

        Try

            'Caso não tenha sido passado nenhum parâmetro 
            If rbf.AsArray.Count <= 0 Then Return Nothing
            Dim StrRbf As String = rbf.AsArray(0).Value

            'Caso retorne Nil para o net, iremos retornar nil tb 
            If StrRbf Is Nothing Then Return Nothing

            ArrayStr = StrRbf.Split("}")

            With RbfResult
                .Add(New TypedValue(LispDataType.ListBegin))
                For Each oBj As Object In ArrayStr
                    oBj = oBj.ToString.Replace("{", "").Replace("}", "")
                    If oBj.ToString <> "" Then .Add(New TypedValue(LispDataType.Text, oBj.ToString))
                Next

                .Add(New TypedValue(LispDataType.ListEnd))
            End With

            Return RbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao validar as informações de StrTOList in Code.", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad004")
            Return Nothing
        End Try

        Return RbfResult

    End Function

    'Function for order a list of lisp and return a new list in order (numeric first and string after)
    <LispFunction("orderlist")> _
    Function OrderList(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim rbfEnd As New ResultBuffer, ArrayString As New ArrayList, ArrayNumber As New ArrayList

        'First order the numbers, after strings. In case of "10 Test...", is a string and enter in list of strings 
        Try

            For Each Obj As Object In rbf.AsArray
                'For start and end of list 
                If DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5016 And DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5017 Then
                    If Not DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value Is Nothing Then
                        If IsNumeric(DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString) Then
                            ArrayNumber.Add(Double.Parse(DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString))
                        Else
                            ArrayString.Add(DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString)
                        End If
                    End If
                End If
            Next

            ArrayString.Sort() : ArrayNumber.Sort()

            With rbfEnd
                .Add(New TypedValue(LispDataType.ListBegin))

                For Each Numero As Double In ArrayNumber
                    .Add(New TypedValue(LispDataType.Text, Numero.ToString))
                Next

                For Each Texto As String In ArrayString
                    .Add(New TypedValue(LispDataType.Text, Texto))
                Next

                .Add(New TypedValue(LispDataType.ListEnd))
            End With

        Catch ex As Exception
            MsgBox("Exception: " & Convert.ToString(ex) & " - " & ex.Message)
            rbfEnd = Nothing
        End Try

        Return rbfEnd

    End Function

    'Return a object after user pass a handle. Return a acadobject or a acadentity
    Public Shared Function ObjectFromHandle_Interop(ByVal HandleObj As String) As Object

        Return ObjectFromHandle(HandleObj)

        ''Ini and end of list. Return nothing 
        'If HandleObj = "-1" Then Return Nothing
        ''Dim LibraryReference As New LibraryReference
        'Dim AcadAp As Object = GetObject(, My.Settings.Application_AutoCad) 'LibraryReference.ReturnAcadReference    'Autodesk.AutoCAD.Interop.AcadApplication
        'Dim doc As Object = AcadAp.ActiveDocument    'Autodesk.AutoCAD.Interop.AcadDocument

        'Try

        '    Dim obj As Object = Nothing
        '    obj = doc.HandleToObject(HandleObj)
        '    Return obj

        'Catch ex As System.Exception
        '    MsgBox("Exception: " & Convert.ToString(ex) & " - " & ex.Message)
        '    Return Nothing
        'Finally
        '    'LibraryReference = Nothing
        'End Try

    End Function

    'Return a object after user pass a handle. Return a acadobject or a acadentity
    Public Shared Function ObjectFromHandle(ByVal HandleObj As String) As Object

        'Ini and end of list. Return nothing 
        If HandleObj = "-1" Then Return Nothing

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim ed As Editor = doc.Editor
        Dim db As Database = doc.Database

        Try

            ' Convert hexadecimal string to 64-bit integer
            Dim ln As Long = Convert.ToInt64(HandleObj.ToString, 16)
            ' Not create a Handle from the long integer
            Dim hn As New Handle(ln)
            ' And attempt to get an ObjectId for the Handle
            Dim id As ObjectId = db.GetObjectId(False, hn, 0)
            ' Finally let's open the object and erase it
            Dim obj As DBObject

            Dim tr As Transaction = doc.TransactionManager.StartTransaction()
            Using tr
                obj = tr.GetObject(id, OpenMode.ForRead)
                'obj.[Erase]()
                tr.Commit()
            End Using

            'Dim arrayInfo As Object = CType(obj, BlockReference).GetXDataForApplication("AHID")

            Return obj

        Catch ex As System.Exception
            MsgBox("Exception: " & Convert.ToString(ex) & " - " & ex.Message)
            Return Nothing
        End Try

    End Function

    Public Shared Function ObjectFromObjectId(ByVal ObjectId_Obj As Object) As Object

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim ed As Editor = doc.Editor
        Dim db As Database = doc.Database

        Try

            ' Finally let's open the object and erase it
            Dim obj As DBObject

            Dim tr As Transaction = doc.TransactionManager.StartTransaction()
            Using tr
                obj = tr.GetObject(ObjectId_Obj, OpenMode.ForRead)
            End Using

            Return obj

        Catch ex As System.Exception
            MsgBox("Exception: " & Convert.ToString(ex) & " - " & ex.Message)
            Return Nothing
        End Try

    End Function

    'Function for filter all information in dwg. Search and filter in selection objects for users
    <LispFunction("ReadQdcData")> _
    Function ReadQdcData(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer   'FilterSelectionSet

        If IsProjectValid() = False Then Return Nothing
        Dim CallPallete As Boolean = False
        'If lisp pass as true palette, then is activate
        If Not Rbf Is Nothing Then If Rbf.AsArray(0).Value.ToString.ToLower = "YES".ToLower Then CallPallete = True

        'If palette is loaded and if exists palette in screen for update data
        If Not MyPaletteObjectType Is Nothing Then
            If MyPaletteObjectType.Visible = True Then If ExistPaletteAutocad(NamePalettePot) = True Then CallPallete = True
        End If

        '' Get the current document editor
        Dim acDocEd As Editor = Application.DocumentManager.MdiActiveDocument.Editor, ObjRead As DBObject
        Dim doc As Document = Application.DocumentManager.MdiActiveDocument, ArrayXData As Object = Nothing, FilterString As String = ""

        Try

            If Library_Register Is Nothing Then Library_Register = New LibraryRegister
            'Dim Dt As System.Data.DataTable = Library_Register.CreateTable("quadro")
            'Clear DataTable 
            Dt_QdcDraw = Nothing
            Dt_QdcDraw = Library_Register.CreateTable("quadro")

            '' Create a TypedValue array to define the filter criteria
            Dim acTypValAr(0) As TypedValue
            acTypValAr.SetValue(New TypedValue(DxfCode.Start, "INSERT"), 0)  'BLOCK
            '' Assign the filter criteria to a SelectionFilter object
            Dim acSelFtr As SelectionFilter = New SelectionFilter(acTypValAr)

            '' Request for objects to be selected in the drawing area
            Dim acSSPrompt As PromptSelectionResult

            'For selection user
            'acSSPrompt = acDocEd.GetSelection(acSelFtr)

            'Declare all fields in qdc 
            Dim Qdc As String = "", Circuito As String = "", PotW As Double, PotVA As Double, Tensao As Double, ClassObject As String, CodObject As String, FatorPot As Double = 0

            'For all objects in dwg
            acSSPrompt = acDocEd.SelectAll(acSelFtr)

            'For progressbar Cad
            Dim pm As New ProgressMeter()

            '' If the prompt status is OK, objects were selected
            If acSSPrompt.Status = PromptStatus.OK Then
                Dim acSSet As SelectionSet = acSSPrompt.Value

                'For read all objects in selection 
                Dim tr As Transaction = doc.TransactionManager.StartTransaction()
                Using tr

                    'Define limits for progressbar
                    pm.Start(My.Settings.Application & " reading...")
                    pm.SetLimit(acSSet.GetObjectIds.Count - 1)

                    For Each Obj As Object In acSSet.GetObjectIds

                        ObjRead = tr.GetObject(Obj, OpenMode.ForRead)
                        'Validate all info in xdata 
                        Qdc = "" : Circuito = "" : PotW = 0 : PotVA = 0 : Tensao = 0 : ClassObject = "" : CodObject = ""

                        ArrayXData = ObjRead.GetXDataForApplication("AHID")
                        If Not ArrayXData Is Nothing Then

                            'Validate all class 
                            Select Case DirectCast(ArrayXData.AsArray, System.Object)(1).Value.ToString
                                Case "0001", "0003", "0004"
                                    ClassObject = DirectCast(ArrayXData.AsArray, System.Object)(1).Value
                                    CodObject = DirectCast(ArrayXData.AsArray, System.Object)(2).Value
                                    Qdc = DirectCast(ArrayXData.AsArray, System.Object)(3).Value
                                    Circuito = DirectCast(ArrayXData.AsArray, System.Object)(4).Value
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(5).Value) Then PotVA = DirectCast(ArrayXData.AsArray, System.Object)(5).Value
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(6).Value) Then FatorPot = DirectCast(ArrayXData.AsArray, System.Object)(6).Value
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(8).Value) Then Tensao = DirectCast(ArrayXData.AsArray, System.Object)(8).Value
                                    If FatorPot > 0 Then PotW = PotVA * FatorPot

                                    'Interup. com tomadas esta nesta classe
                                Case "0005", "0006", "0019", "0126"
                                    ClassObject = DirectCast(ArrayXData.AsArray, System.Object)(1).Value
                                    CodObject = DirectCast(ArrayXData.AsArray, System.Object)(2).Value
                                    Qdc = DirectCast(ArrayXData.AsArray, System.Object)(3).Value
                                    Circuito = DirectCast(ArrayXData.AsArray, System.Object)(4).Value
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(6).Value) Then PotVA = DirectCast(ArrayXData.AsArray, System.Object)(6).Value
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(7).Value) Then FatorPot = DirectCast(ArrayXData.AsArray, System.Object)(7).Value
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(9).Value) Then Tensao = DirectCast(ArrayXData.AsArray, System.Object)(9).Value
                                    If FatorPot > 0 Then PotW = PotVA * FatorPot

                                    'Motor
                                Case "0011"
                                    ClassObject = DirectCast(ArrayXData.AsArray, System.Object)(1).Value
                                    CodObject = DirectCast(ArrayXData.AsArray, System.Object)(2).Value
                                    Qdc = DirectCast(ArrayXData.AsArray, System.Object)(3).Value
                                    Circuito = DirectCast(ArrayXData.AsArray, System.Object)(4).Value
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(5).Value) Then PotVA = DirectCast(ArrayXData.AsArray, System.Object)(5).Value * 735.5
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(6).Value) Then FatorPot = DirectCast(ArrayXData.AsArray, System.Object)(6).Value
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(8).Value) Then Tensao = DirectCast(ArrayXData.AsArray, System.Object)(8).Value
                                    If FatorPot > 0 Then PotW = PotVA * FatorPot

                                    'Luminária com 3 retornos - Automação
                                Case "0127"
                                    ClassObject = DirectCast(ArrayXData.AsArray, System.Object)(1).Value
                                    CodObject = DirectCast(ArrayXData.AsArray, System.Object)(2).Value
                                    Qdc = DirectCast(ArrayXData.AsArray, System.Object)(3).Value
                                    Circuito = DirectCast(ArrayXData.AsArray, System.Object)(4).Value
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(8).Value) Then PotVA = DirectCast(ArrayXData.AsArray, System.Object)(8).Value
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(9).Value) Then FatorPot = DirectCast(ArrayXData.AsArray, System.Object)(9).Value
                                    If IsNumeric(DirectCast(ArrayXData.AsArray, System.Object)(11).Value) Then Tensao = DirectCast(ArrayXData.AsArray, System.Object)(11).Value
                                    If FatorPot > 0 Then PotW = PotVA * FatorPot

                            End Select

                            'Now, consult for qdc and circuito for update row
                            If Qdc.Trim <> "" Then
                                FilterString = "Quadro = '" & Qdc & "' AND Circuito = '" & Circuito & "'"
                                Dim drc As DataRow() = Dt_QdcDraw.Select(FilterString)
                                If drc.Length > 0 Then
                                    drc(0)("PotW") = Decimal.Parse(drc(0)("PotW").ToString) + PotW
                                    drc(0)("PotVA") = Decimal.Parse(drc(0)("PotVA").ToString) + PotVA
                                Else
                                    Dt_QdcDraw.Rows.Add(Qdc, Circuito, PotW, PotVA, Tensao)
                                End If
                            End If
                        End If

                        pm.MeterProgress()
                        ' This allows AutoCAD to repaint
                        System.Windows.Forms.Application.DoEvents()

                    Next

                End Using

                pm.[Stop]()

                'Only user request the palette
                If CallPallete = True Then
                    'Load a palette with all info about qdc. Only if palette is aticvate. Only update the palette if is loaded in screen
                    If Rbf Is Nothing Then
                        'AddPaletteQdc(Dt_QdcDraw)
                    Else
                        'If DirectCast(Rbf.AsArray(0), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value = "YES" Then AddPaletteQdc(Dt_QdcDraw)
                        AddPaletteQdc(Dt_QdcDraw)
                    End If
                End If

            Else
                'Application.ShowAlertDialog("Number of objects selected: 0")
            End If

            Return Nothing

        Catch ex As Exception
            MsgBox("Ocorrência de erro : " & ex.Message, MsgBoxStyle.Information, "Aenge Error")
            Return Nothing
        End Try

    End Function

    'Return a value in cfg file. The function need a CabFile, a Field to consult and a file.cfg for search (Dwg, project or application)
    <LispFunction("ReturnCfg_Field")> _
    Function ReturnCfg_Field(ByVal Rbf As ResultBuffer) As ResultBuffer

        If Rbf Is Nothing Then Return Nothing
        If Rbf.AsArray(0).Value.ToString = "" Then Return Nothing
        If Rbf.AsArray(1).Value.ToString = "" Then Return Nothing

        Dim rbfSearch As New ResultBuffer, LibraryReference As New LibraryReference
        Dim FieldSelected As String = Rbf.AsArray(1).Value.ToString
        Dim CabSelected As String = Rbf.AsArray(0).Value.ToString
        'dwg, project or app
        Dim FileSelected As String = ""
        Dim PathFile As String = "", Retorno As Object = Nothing, rbfResult As New ResultBuffer

        Try

            If Rbf.AsArray.Count > 2 Then FileSelected = Rbf.AsArray(2).Value.ToString

            If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
            Select Case FileSelected.ToLower
                Case "app".ToLower
                    PathFile = LibraryReference.ReturnPathApplication & "Projeto.cfg"

                Case "project".ToLower
                    PathFile = LibraryReference.ReturnPathApplication & LibraryReference.Return_TactualProject & "\Projeto.cfg"

                    'Dwg file 
                Case Else
                    PathFile = LibraryReference.ReturnPathApplication & LibraryReference.Return_TactualProject & "\" & LibraryReference.Return_TactualDrawing & ".cfg"

            End Select

            Retorno = Aenge_GetCfg(FieldSelected, CabSelected, PathFile)

            With rbfResult
                If Retorno Is Nothing Then
                    'when not exists registers
                    .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListBegin))
                    .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListEnd))
                Else
                    .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.Text, Retorno))
                End If
            End With

        Catch ex As Exception
            rbfSearch = Nothing
        Finally
            LibraryReference = Nothing
        End Try

        Return rbfResult

    End Function

    'Return in list all objects in cfg cab. 
    <LispFunction("ReturnCfg_Data")> _
    Function ReturnCfg_Data(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim rbfSearch As New ResultBuffer, LibraryReference As New LibraryReference
        Dim typeSearch As String = Rbf.AsArray(1).Value.ToString
        Dim typeFileCfg As String = Rbf.AsArray(0).Value.ToString

        Try

            rbfSearch = LibraryReference.Return_AllFieldCfg_Cab(typeFileCfg, typeSearch)

        Catch ex As Exception
            rbfSearch = Nothing
        Finally
            LibraryReference = Nothing
        End Try

        Return rbfSearch

    End Function

    'Return in list all objects in cfg cab. This is for all folders and paths of older version of autopower
    <LispFunction("ReturnCfg_Old_Data")> _
    Function ReturnCfg_Old_Data(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim rbfSearch As New ResultBuffer, LibraryReference As New LibraryReference
        Dim typeSearch As String = Rbf.AsArray(1).Value.ToString
        Dim typeFileCfg As String = Rbf.AsArray(0).Value.ToString

        Try
            rbfSearch = LibraryReference.Return_Old_AllFieldCfg_Cab(typeFileCfg, typeSearch)
        Catch ex As Exception
            rbfSearch = Nothing
        Finally
            LibraryReference = Nothing
        End Try

        Return rbfSearch

    End Function

    'Set all fields of cfg with values passed in lisp. Is a generic function, receive a select case file (dwg, project, others ... .cfg)
    <LispFunction("SetCfg_Data")> _
    Public Sub SetCfg_DataFull(ByVal Rbf As ResultBuffer)

        Dim rbfSearch As New ResultBuffer, LibraryReference As New LibraryReference
        Dim TypeFileCfg As String = Rbf.AsArray(0).Value.ToString
        Dim CabFileCfg As String = Rbf.AsArray(1).Value.ToString

        Try

            rbfSearch = LibraryReference.SetCfg_DataFull(TypeFileCfg, CabFileCfg, Rbf)

        Catch ex As Exception
            rbfSearch = Nothing
        Finally
            LibraryReference = Nothing
        End Try

        'Return Nothing
    End Sub

    'Set all fields of cfg with values passed in lisp. Is a generic function, receive a select case file (dwg, project, others ... .cfg)
    <LispFunction("SetCfg_Old_Data")> _
    Function SetCfg_Old_DataFull(ByVal Rbf As ResultBuffer) As Object

        Dim rbfSearch As New ResultBuffer, LibraryReference As New LibraryReference
        Dim TypeFileCfg As String = Rbf.AsArray(0).Value.ToString
        Dim CabFileCfg As String = Rbf.AsArray(1).Value.ToString

        Try

            rbfSearch = LibraryReference.SetCfg_Old_DataFull(TypeFileCfg, CabFileCfg, Rbf)

        Catch ex As Exception
            rbfSearch = Nothing
        Finally
            LibraryReference = Nothing
        End Try

        Return True
    End Function

    'Return all information about PotW, PotVa, Circuit and Tensao
    <LispFunction("ReturnQdc_ListPot")> _
    Function ReturnQdc_ListPot(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim rbfResult As ResultBuffer = Nothing
        Dim NameQdc As String = Rbf.AsArray(0).Value.ToString
        'In case of no records in datatable 
        If Dt_QdcDraw Is Nothing Then Return Nothing
        If Dt_QdcDraw.Rows.Count <= 0 Then Return Nothing

        'Return list
        rbfResult = New ResultBuffer()
        With rbfResult

            For Each Dr As System.Data.DataRow In Dt_QdcDraw.Rows
                .Add(New TypedValue(LispDataType.ListBegin))
                .Add(New TypedValue(LispDataType.DottedPair, Dr("Circuito")))
                .Add(New TypedValue(LispDataType.DottedPair, Dr("PotW")))
                .Add(New TypedValue(LispDataType.DottedPair, Dr("PotVA")))
                .Add(New TypedValue(LispDataType.DottedPair, Dr("Tensao")))
                .Add(New TypedValue(LispDataType.ListEnd))
            Next

        End With

        Return rbfResult
    End Function

    <LispFunction("ReturnQdc_ListPotS")> _
Function ReturnQdc_ListPotS(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim rbfResult As ResultBuffer = Nothing, DtView As System.Data.DataView, ReturnEmpty As Boolean = False
        Dim NameQdc As String

        If Rbf Is Nothing Then
            NameQdc = ""
        Else
            NameQdc = Rbf.AsArray(0).Value.ToString
        End If

        'In case of no records in datatable 
        If Dt_QdcDraw Is Nothing Then
            ReturnEmpty = True
        Else
            If Dt_QdcDraw.Rows.Count <= 0 Then ReturnEmpty = True
        End If

        'In case of 0 records 
        rbfResult = New ResultBuffer()
        If ReturnEmpty = True Then
            rbfResult.Add(New TypedValue(LispDataType.ListBegin))
            rbfResult.Add(New TypedValue(LispDataType.Nil, Nothing))
            rbfResult.Add(New TypedValue(LispDataType.ListEnd))
            Return rbfResult
        End If

        DtView = New DataView(Dt_QdcDraw, "Quadro = '" & NameQdc & "'", "Circuito ASC", DataViewRowState.CurrentRows)

        With rbfResult

            'All list is a list-list for lisp...
            .Add(New TypedValue(LispDataType.ListBegin))

            'If records = 0
            If DtView.Count <= 0 Then

                .Add(New TypedValue(LispDataType.ListBegin))
                .Add(New TypedValue(LispDataType.ListEnd))

            Else

                For Each Dr As System.Data.DataRowView In DtView
                    .Add(New TypedValue(LispDataType.ListBegin))
                    .Add(New TypedValue(LispDataType.Text, Dr("Circuito").ToString))    '.Add(New TypedValue(LispDataType.Text, Dr("Quadro").ToString & "|" & Dr("Circuito").ToString))
                    .Add(New TypedValue(LispDataType.Text, Double.Parse(Dr("PotW")).ToString("0.00")))
                    .Add(New TypedValue(LispDataType.Text, Double.Parse(Dr("PotVA")).ToString("0.00")))
                    .Add(New TypedValue(LispDataType.Text, Dr("Tensao")))
                    .Add(New TypedValue(LispDataType.ListEnd))
                Next

            End If

            .Add(New TypedValue(LispDataType.ListEnd))

        End With

        Return rbfResult
    End Function

    'Extract from dwg all information in blocks and groups. Create a material list and calc...
    <LispFunction("GetAllObjects_Aenge")> _
    Public Shared Function ExtractObjectsFromFile(ByVal RBuffer As ResultBuffer) As ResultBuffer

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim ed As Editor = doc.Editor, NameTactualProject As String, LibraryRegister As LibraryRegister
        Dim Text As String = "", ArrayList_Object As New ArrayList(), Result As String = "TRUE", UserR2 As Decimal = 0.01, PathCfg_Dwg As String
        Dim PathCad As String = "", NameObject As String = "", AttFind As String = ""

        PathCad = Mid(doc.Name, 1, doc.Name.Length - Dir(doc.Name).ToString.Length)
        NameObject = Dir(doc.Name.ToString)
        NameObject = NameObject.ToUpper
        NameObject = NameObject.Replace(".DWG", "")

        If PathCad.ToString.Trim = "" Then Return Nothing
        If Not My.Computer.FileSystem.DirectoryExists(PathCad) Then Return Nothing

        'Get a name tactual project 
        LibraryRegister = New LibraryRegister
        NameTactualProject = LibraryRegister.GetNameDWG_Tactual
        LibraryRegister = Nothing

        'Get a userr2. This is a variable for calculate the tub width
        'UserR2 = Application.GetSystemVariable("USERR2").ToString
        Dim Library_Reference As New LibraryReference
        PathCfg_Dwg = GetAppInstall() & Library_Reference.Return_TactualProject & "\" & Library_Reference.Return_TactualDrawing & ".cfg"
        If My.Computer.FileSystem.FileExists(PathCfg_Dwg) Then
            Select Case Aenge_GetCfg("DRAWING", "UNIDADE_MEDIDA", PathCfg_Dwg).ToString.Trim.ToLower
                Case "MM".ToLower
                    UserR2 = 1
                Case "CM".ToLower
                    UserR2 = 0.1
                Case Else
                    UserR2 = 0.01
            End Select
        End If

        ' Create a database and try to load the file
        Dim db As New Database(False, True)

        Using db

            Try
                'Alterar depois para receber o caminho do arquivo a ser escrito
                'If RBuffer Is Nothing Then
                db = doc.Database
                'Else
                'db.ReadDwgFile(PathCad, System.IO.FileShare.Read, False, "")
                'End If

                Dim tr As Transaction = db.TransactionManager.StartTransaction()
                Using tr
                    ' Open the blocktable, get the modelspace
                    Dim bt As BlockTable = DirectCast(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)
                    Dim btr As BlockTableRecord = DirectCast(tr.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForRead), BlockTableRecord)
                    ' Iterate through it, dumping objects
                    Dim ArrayyBlock As Array, ArrayyBlock_Group As Array, Count As Integer = 0

                    For Each objId As ObjectId In btr
                        Dim ent As Entity = DirectCast(tr.GetObject(objId, OpenMode.ForRead), Entity)

                        If TypeOf ent Is BlockReference Then
                            'Filter for xdata application created
                            Dim RSBuffer As ResultBuffer = CType(ent, BlockReference).GetXDataForApplication("AHID")

                            Text = ""
                            If Not RSBuffer Is Nothing Then
                                ArrayyBlock = RSBuffer.AsArray
                                'Clear text for new information

                                Text = "" : Count = 0
                                For Each Obj As Object In ArrayyBlock
                                    If Count > 0 Then Text += DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode.ToString & SeparatorList & DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value & SeparatorList
                                    Count += 1
                                Next
                                Text += "HANDLE" & SeparatorList & ent.Handle.ToString & "_" & NameTactualProject & SeparatorList
                                ArrayList_Object.Add(Text)

                            End If

                            'Else for others objects in dwg
                        Else
                            'Text = ent.Handle.ToString
                            'ArrayList_Object.Add(Text)
                        End If

                    Next

                    'After read all blocks objects, now read all xdata of groups 
                    Dim nod As DBDictionary = tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead) ' now get the ACAD_GROUP dictionary entry, this contains all of the Groups defined in the drawing 
                    Dim acadGroup As DBDictionary = tr.GetObject(nod("ACAD_GROUP"), OpenMode.ForRead) ' next, find the group name that was entered above 
                    Dim groupRequired As Group, LenPolyline As Object = Nothing

                    For Each testeobj As Object In acadGroup
                        groupRequired = tr.GetObject(acadGroup(DirectCast(testeobj, Autodesk.AutoCAD.DatabaseServices.DBDictionaryEntry).Key.ToString), OpenMode.ForRead) ' we now have the group required, lets find out what's inside 

                        If Not groupRequired.GetXDataForApplication("AHID") Is Nothing Then
                            ArrayyBlock_Group = groupRequired.GetXDataForApplication("AHID").AsArray

                            Dim entityIds As ObjectId() = groupRequired.GetAllEntityIds()
                            Dim id As ObjectId, HandleTub As String = ""

                            For Each id In entityIds
                                ' open the entity for read 
                                Dim entPol As Entity = tr.GetObject(id, OpenMode.ForRead)

                                If TypeOf entPol Is Polyline Then

                                    Select Case UserR2
                                        Case 0.1
                                            LenPolyline = CType(entPol, Polyline).Length / 100

                                        Case 1
                                            LenPolyline = CType(entPol, Polyline).Length / 1000

                                        Case Else
                                            LenPolyline = CType(entPol, Polyline).Length

                                    End Select

                                    HandleTub += "LEN" & SeparatorList & LenPolyline & SeparatorList & "HANDLE" & SeparatorList & entPol.Handle.ToString & "_" & NameTactualProject & SeparatorList

                                Else

                                    'Considera como polyline2d
                                    If TypeOf entPol Is Polyline2d Then

                                        Select Case UserR2
                                            Case 0.1
                                                LenPolyline = CType(entPol, Polyline2d).Length / 100

                                            Case 1
                                                LenPolyline = CType(entPol, Polyline2d).Length / 1000

                                            Case Else
                                                LenPolyline = CType(entPol, Polyline2d).Length

                                        End Select

                                        HandleTub += "LEN" & SeparatorList & LenPolyline & SeparatorList & "HANDLE" & SeparatorList & entPol.Handle.ToString & "_" & NameTactualProject & SeparatorList

                                    End If

                                End If

                                ' create the highlight path 
                                'Dim path As FullSubentityPath = New FullSubentityPath(New ObjectId(0) {id}, New SubentityId(SubentityType.Null, 0))
                                ' now highlight it 
                                'ent.Highlight(path, True)
                            Next

                            'Confirm if exists entities in group. If not exist, don't create a line of group in db.dat for the lisp
                            If groupRequired.NumEntities > 0 Then
                                Text = "" : Count = 0
                                For Each Obj As Object In ArrayyBlock_Group
                                    If Count > 0 Then Text += DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode.ToString & SeparatorList & DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value & "|" 'DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value & "|"
                                    Count += 1
                                Next
                                'For the end line (string)
                                If HandleTub.Contains("LEN") Then
                                    Text += HandleTub '"LEN" & SeparatorList & LenPolyline & "|Handle" & SeparatorList & HandleTub & "|"
                                    ArrayList_Object.Add(Text)
                                End If

                            End If

                        End If

                    Next

                    'Now, write in file all information about array
                    SaveText_To_File(ArrayList_Object, PathCad & IniName_FileMaterial & NameObject & ".dat", , True)
                End Using

                'After read all blocks and group, create a Db_All for lisp 
                If Library_Register Is Nothing Then Library_Register = New LibraryRegister
                'Append all files DB_
                Library_Register.AppendAllFilesDB_Project(True)

            Catch generatedExceptionName As System.Exception
                ed.WriteMessage(vbLf & "Unable to read drawing file. Error type : " & generatedExceptionName.Message)
                Result = "FALSE"
            Finally
                Library_Register = Nothing
            End Try

        End Using

        'After execute all functions, return to lisp a result
        Dim rbfResult As ResultBuffer
        rbfResult = New ResultBuffer(New TypedValue(LispDataType.Text, Result))
        Return rbfResult

    End Function

    'Retorna em lista todos os dados que foram adicionados antermente nos .dat do desenho, no caso a funcao precisa saber se eh o dat do desenho ou o geral 
    <LispFunction("ReturnAllObjects_Aenge")> _
    Public Shared Function ReturnExtractObjectsFromFile(ByVal rBf As ResultBuffer) As ResultBuffer

        If rBf Is Nothing Then Return Nothing

        Dim RbfResult As New ResultBuffer, ConnAHID_ As OleDb.OleDbConnection
        Dim NameDwg As String, Dt As New System.Data.DataTable
        Dim Library_Reference As New LibraryReference, Da As OleDb.OleDbDataAdapter
        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection

        Try

            NameDwg = Library_Reference.Return_TactualDrawing
            With LibraryConnection
                .TypeDb = "AHID_"
                ConnAHID_ = .Aenge_OpenConnectionDB
            End With

            Select Case rBf.AsArray(0).Value.ToString.ToLower
                Case "DWG".ToLower
                    Da = New OleDb.OleDbDataAdapter("Select * From AeleObjC Where Id = " & Library_Reference.Return_TactualID & " And Dwg = '" & IniName_FileMaterial & NameDwg & "'", ConnAHID_)

                Case "DWGS".ToLower
                    Da = New OleDb.OleDbDataAdapter("Select * From AeleObjC Where Id = " & Library_Reference.Return_TactualID & " And Dwg = '" & IniName_FileMaterial & NameDwg & "'", ConnAHID_)

                Case "ALL".ToLower
                    Da = New OleDb.OleDbDataAdapter("Select * From AeleObjC Where Id = " & Library_Reference.Return_TactualID & "", ConnAHID_)

                Case Else
                    Return Nothing

            End Select

            Da.Fill(Dt)
            Dim LineFile As Object = Nothing

            With RbfResult
                If Dt.Rows.Count <= 0 Then
                    .Add(New TypedValue(LispDataType.ListBegin))
                    .Add(New TypedValue(LispDataType.ListEnd))
                Else
                    .Add(New TypedValue(LispDataType.ListBegin))
                    For Each Dr As System.Data.DataRow In Dt.Rows
                        .Add(New TypedValue(LispDataType.ListBegin))
                        LineFile = Nothing
                        For Each Cl As System.Data.DataColumn In Dt.Columns
                            If Cl.ColumnName.ToString.ToLower <> "ID".ToLower And Cl.ColumnName.ToString.ToLower <> "DWG".ToLower Then LineFile += Dr(Cl.ColumnName.ToString).ToString & "|"
                        Next
                        .Add(New TypedValue(LispDataType.Text, LineFile))
                        .Add(New TypedValue(LispDataType.ListEnd))
                    Next
                    .Add(New TypedValue(LispDataType.ListEnd))
                End If
            End With

            Return RbfResult

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad023")
            LibraryError = Nothing
            Return Nothing
        End Try

    End Function

    <LispFunction("GetAllObjects_Aenge_New")> _
    Public Shared Function ExtractObjectsFromFile_New(ByVal RBuffer As ResultBuffer) As ResultBuffer

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim ed As Editor = doc.Editor, NameTactualProject As String, LibraryRegister As LibraryRegister
        Dim Text As String = "", ArrayList_Object As New ArrayList(), Result As String = "TRUE", UserR2 As Decimal = 0
        Dim PathCad As String = "", NameObject As String = "", AttFind As String = ""

        PathCad = Mid(doc.Name, 1, doc.Name.Length - Dir(doc.Name).ToString.Length)
        NameObject = Dir(doc.Name.ToString)
        NameObject = NameObject.ToUpper
        NameObject = NameObject.Replace(".DWG", "")

        If PathCad.ToString.Trim = "" Then Return Nothing
        If Not My.Computer.FileSystem.DirectoryExists(PathCad) Then Return Nothing

        Try

            'Get a name tactual project 
            LibraryRegister = New LibraryRegister
            NameTactualProject = LibraryRegister.GetNameDWG_Tactual
            LibraryRegister = Nothing

            'Create datatable for fill database (table aeleobjc)
            Dim DtAeleObjC As System.Data.DataTable, DrAeleObjC As System.Data.DataRow
            Dim LIbraryCad As New LibraryCad, Library_Reference As New LibraryReference
            DtAeleObjC = LIbraryCad.CreateDataTable("dtaeleobjc")

            Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
            Dim ConnApwr_ As OleDb.OleDbConnection
            With LibraryConnection
                .TypeDb = "AHID_"
                ConnApwr_ = .Aenge_OpenConnectionDB
            End With

            'Clear all register of table with Id and Dwg current 
            Dim Cmd As New OleDb.OleDbCommand
            With Cmd
                .CommandText = "Delete From AeleObjC Where Id = " & Library_Reference.Return_TactualID & " And Dwg = '" & IniName_FileMaterial & NameObject & "'"
                .CommandType = CommandType.Text
                .Connection = ConnApwr_
                .ExecuteNonQuery()
            End With
            Cmd = Nothing

            ' Create a database and try to load the file
            Dim db As New Database(False, True)

            Using db

                Try
                    'Alterar depois para receber o caminho do arquivo a ser escrito
                    db = doc.Database
                    Dim tr As Transaction = db.TransactionManager.StartTransaction()
                    Dim pm As New ProgressMeter()

                    Using tr
                        ' Open the blocktable, get the modelspace
                        Dim bt As BlockTable = DirectCast(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)
                        Dim btr As BlockTableRecord = DirectCast(tr.GetObject(bt(BlockTableRecord.ModelSpace), OpenMode.ForRead), BlockTableRecord)
                        ' Iterate through it, dumping objects
                        Dim ArrayyBlock As Array, ArrayyBlock_Group As Array, Count As Integer = 0

                        pm.Start("Aguarde. Processando...")
                        pm.SetLimit(DirectCast(btr.Database, Autodesk.AutoCAD.DatabaseServices.Database).ApproxNumObjects)

                        For Each objId As ObjectId In btr
                            Dim ent As Entity = DirectCast(tr.GetObject(objId, OpenMode.ForRead), Entity)

                            'System.Threading.Thread.Sleep(1)
                            ' Increment Progress Meter...
                            pm.MeterProgress()
                            ' This allows AutoCAD to repaint
                            System.Windows.Forms.Application.DoEvents()

                            If TypeOf ent Is BlockReference Then
                                'Filter for xdata application created
                                Dim RSBuffer As ResultBuffer = CType(ent, BlockReference).GetXDataForApplication("AHID")

                                Text = ""
                                If Not RSBuffer Is Nothing Then
                                    ArrayyBlock = RSBuffer.AsArray
                                    DrAeleObjC = Nothing : DrAeleObjC = DtAeleObjC.NewRow
                                    DrAeleObjC("ID") = Library_Reference.Return_TactualID
                                    DrAeleObjC("DWG") = IniName_FileMaterial & NameObject
                                    'Clear text for new information

                                    Text = "" : Count = 0
                                    For Each Obj As Object In ArrayyBlock
                                        'Neste primeiro caso eh pq pega o APWR, por isso pula a primeira linha 
                                        'If Count > 0 Then Text += DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode.ToString & SeparatorList & DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value & SeparatorList
                                        If Count > 0 Then
                                            DrAeleObjC("TYPE" & Count.ToString) = DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode.ToString
                                            DrAeleObjC("FIELD" & Count.ToString) = DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString
                                        End If
                                        Count += 1
                                    Next
                                    'Text += "HANDLE" & SeparatorList & ent.Handle.ToString & "_" & NameTactualProject & SeparatorList
                                    DrAeleObjC("TYPE" & Count.ToString) = "HANDLE"
                                    DrAeleObjC("FIELD" & Count.ToString) = ent.Handle.ToString & "_" & NameTactualProject & SeparatorList
                                    'ArrayList_Object.Add(Text)
                                    DtAeleObjC.Rows.Add(DrAeleObjC)
                                End If

                                'Else for others objects in dwg
                            Else
                                'Text = ent.Handle.ToString
                                'ArrayList_Object.Add(Text)
                            End If

                        Next

                        'After read all blocks objects, now read all xdata of groups 
                        Dim nod As DBDictionary = tr.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead) ' now get the ACAD_GROUP dictionary entry, this contains all of the Groups defined in the drawing 
                        Dim acadGroup As DBDictionary = tr.GetObject(nod("ACAD_GROUP"), OpenMode.ForRead) ' next, find the group name that was entered above 
                        Dim groupRequired As Group, LenPolyline As Object = Nothing

                        For Each GroupTub As Object In acadGroup
                            groupRequired = tr.GetObject(acadGroup(DirectCast(GroupTub, Autodesk.AutoCAD.DatabaseServices.DBDictionaryEntry).Key.ToString), OpenMode.ForRead) ' we now have the group required, lets find out what's inside 

                            'System.Threading.Thread.Sleep(1)
                            ' Increment Progress Meter...
                            pm.MeterProgress()
                            ' This allows AutoCAD to repaint
                            System.Windows.Forms.Application.DoEvents()

                            If Not groupRequired.GetXDataForApplication("AHID") Is Nothing Then
                                ArrayyBlock_Group = groupRequired.GetXDataForApplication("AHID").AsArray

                                Dim entityIds As ObjectId() = groupRequired.GetAllEntityIds()
                                Dim id As ObjectId, HandleTub As String = ""

                                For Each id In entityIds
                                    ' open the entity for read 
                                    Dim entPol As Entity = tr.GetObject(id, OpenMode.ForRead)

                                    If TypeOf entPol Is Polyline Then

                                        Select Case UserR2
                                            Case 0.1
                                                LenPolyline = CType(entPol, Polyline).Length / 100

                                            Case 1
                                                LenPolyline = CType(entPol, Polyline).Length / 1000

                                            Case Else
                                                LenPolyline = CType(entPol, Polyline).Length

                                        End Select

                                        HandleTub += "LEN" & SeparatorList & LenPolyline & SeparatorList & "HANDLE" & SeparatorList & entPol.Handle.ToString & "_" & NameTactualProject & SeparatorList

                                    Else

                                        'Considera como polyline2d
                                        If TypeOf entPol Is Polyline2d Then

                                            Select Case UserR2
                                                Case 0.1
                                                    LenPolyline = CType(entPol, Polyline2d).Length / 100

                                                Case 1
                                                    LenPolyline = CType(entPol, Polyline2d).Length / 1000

                                                Case Else
                                                    LenPolyline = CType(entPol, Polyline2d).Length

                                            End Select

                                            HandleTub += "LEN" & SeparatorList & LenPolyline & SeparatorList & "HANDLE" & SeparatorList & entPol.Handle.ToString & "_" & NameTactualProject & SeparatorList

                                        End If

                                    End If

                                    ' create the highlight path 
                                    'Dim path As FullSubentityPath = New FullSubentityPath(New ObjectId(0) {id}, New SubentityId(SubentityType.Null, 0))
                                    ' now highlight it 
                                    'ent.Highlight(path, True)
                                Next

                                'Confirm if exists entities in group. If not exist, don't create a line of group in db.dat for the lisp
                                If groupRequired.NumEntities > 0 Then
                                    Text = "" : Count = 0
                                    DrAeleObjC = Nothing : DrAeleObjC = DtAeleObjC.NewRow
                                    DrAeleObjC("ID") = Library_Reference.Return_TactualID
                                    DrAeleObjC("DWG") = IniName_FileMaterial & NameObject
                                    For Each Obj As Object In ArrayyBlock_Group
                                        ' Increment Progress Meter...
                                        pm.MeterProgress()
                                        ' This allows AutoCAD to repaint
                                        System.Windows.Forms.Application.DoEvents()
                                        If Count > 0 Then
                                            DrAeleObjC("TYPE" & Count.ToString) = DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode.ToString
                                            DrAeleObjC("FIELD" & Count.ToString) = DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString
                                        End If
                                        Count += 1
                                    Next

                                    'For the end line (string)
                                    If HandleTub.Contains("LEN") Then
                                        Text += HandleTub '"LEN" & SeparatorList & LenPolyline & SeparatorList & "HANDLE" & SeparatorList & entPol.Handle.ToString & "_" & NameTactualProject & SeparatorList
                                        If HandleTub.Split("|").Count >= 1 Then DrAeleObjC("TYPE" & Count.ToString) = HandleTub.Split("|")(0)
                                        If HandleTub.Split("|").Count >= 2 Then DrAeleObjC("FIELD" & Count.ToString) = HandleTub.Split("|")(1)
                                        Count += 1
                                        If HandleTub.Split("|").Count >= 3 Then DrAeleObjC("TYPE" & Count.ToString) = HandleTub.Split("|")(2)
                                        If HandleTub.Split("|").Count >= 4 Then DrAeleObjC("FIELD" & Count.ToString) = HandleTub.Split("|")(3)
                                        Count += 1
                                        'ArrayList_Object.Add(Text)
                                    End If
                                    DtAeleObjC.Rows.Add(DrAeleObjC)
                                End If

                            End If

                        Next

                        'Stop progressmeter, but initialize reading for groups 
                        pm.[Stop]()
                        doc.Editor.WriteMessage("Gravando dados, aguarde mais alguns instantes...")

                        'Now, write in file all information about array
                        'SaveText_To_File(ArrayList_Object, PathCad & IniName_FileMaterial & NameObject & ".dat", , True)
                        If Library_Register Is Nothing Then Library_Register = New LibraryRegister
                        Library_Register.UpdateTableDatabase_AeleObjC(DtAeleObjC, ConnApwr_)
                        doc.Editor.WriteMessage("Dados gravados !")
                    End Using

                Catch generatedExceptionName As System.Exception
                    ed.WriteMessage(vbLf & "Unable to read drawing file. Error type : " & generatedExceptionName.Message)
                    Result = "FALSE"
                Finally
                    If Not ConnApwr_ Is Nothing Then If ConnApwr_.State = ConnectionState.Open Then ConnApwr_.Close()
                    LibraryConnection = Nothing
                    Library_Register = Nothing
                End Try

            End Using

            'After execute all functions, return to lisp a result
            Dim rbfResult As ResultBuffer
            rbfResult = New ResultBuffer(New TypedValue(LispDataType.Text, Result))
            Return rbfResult

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Error ExtractObjectsFromF autoenge - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad022")
            Return Nothing
        End Try

    End Function

    'Function for append all files DB_ in folder of project. Used in Material list
    <LispFunction("AppendAll")> _
    Function AppendAllFilesDB_Project(ByVal Rbuffer As ResultBuffer) As Object
        If IsProjectValid() = False Then Return Nothing
        If Library_Register Is Nothing Then Library_Register = New LibraryRegister
        Library_Register.AppendAllFilesDB_Project()
        Return Nothing
    End Function

    'Get all objects in selection set for reading
    <LispFunction("SelSet_Aenge")> _
    Public Function ExtractObjectsFromFile_SelSet(ByVal rbs As ResultBuffer) As ResultBuffer

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument, Result As Boolean = True
        Dim ed As Editor = doc.Editor, Count As Double = 0, NameTactualProject As String
        Dim Text As String = "", ArrayList_Object As New ArrayList(), LibraryRegister As LibraryRegister
        Dim PathCad As String = "", NameObject As String = "", AttFind As String = ""
        'Now, read all information in groups 
        Dim LenPolyline As Decimal = 0, UserR2 As String = ""
        Dim groupRequired As Group

        'Get a name tactual project 
        LibraryRegister = New LibraryRegister
        NameTactualProject = LibraryRegister.GetNameDWG_Tactual
        LibraryRegister = Nothing

        PathCad = Mid(doc.Name, 1, doc.Name.Length - Dir(doc.Name).ToString.Length)
        NameObject = Dir(doc.Name.ToString)
        NameObject = NameObject.ToUpper
        NameObject = NameObject.Replace(".DWG", "")

        'Get a userr2. This is a variable for calculate the tub width
        'UserR2 = Application.GetSystemVariable("USERR2").ToString
        Dim Library_Reference As New LibraryReference, PathCfg_Dwg As String = ""
        PathCfg_Dwg = GetAppInstall() & Library_Reference.Return_TactualProject & "\" & Library_Reference.Return_TactualDrawing & ".cfg"
        If My.Computer.FileSystem.FileExists(PathCfg_Dwg) Then
            Select Case Aenge_GetCfg("DRAWING", "UNIDADE_MEDIDA", PathCfg_Dwg).ToString.Trim.ToLower
                Case "MM".ToLower
                    UserR2 = 1
                Case "CM".ToLower
                    UserR2 = 0.1
                Case Else
                    UserR2 = 0.01
            End Select
        End If

        Try

            ' Create a database and try to load the file
            Dim db As Database = doc.Database
            Dim tr As Transaction = db.TransactionManager.StartTransaction()

            Using db
                Using tr

                    ' Iterate through it, dumping objects
                    Dim ArrayyBlock As Array, ArrayyBlock_Group As Array = Nothing

                    For Each objArrayHandle As Object In rbs.AsArray

                        'Dim ent As Entity = ObjectFromHandle(objId.Handle.ToString)
                        Dim ent As Object = ObjectFromHandle(DirectCast(objArrayHandle, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value)

                        If TypeOf ent Is BlockReference Or TypeOf ent Is Polyline Then
                            'Filter for xdata application created
                            Dim RSBuffer As ResultBuffer = CType(ent, BlockReference).GetXDataForApplication("AHID")

                            Text = ""
                            If Not RSBuffer Is Nothing Then
                                ArrayyBlock = RSBuffer.AsArray
                                'Clear text for new information

                                Text = "" : Count = 0
                                For Each Obj As Object In ArrayyBlock
                                    If Count > 0 Then Text += DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode.ToString & SeparatorList & DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value & SeparatorList
                                    Count += 1
                                Next
                                Text += "HANDLE" & SeparatorList & ent.Handle.ToString & "_" & NameTactualProject & SeparatorList
                                ArrayList_Object.Add(Text)

                            End If

                            'Else for others objects in dwg
                        Else

                            'Only for group informations 
                            If TypeOf ent Is Group Then

                                groupRequired = CType(ent, Group)

                                If Not groupRequired.GetXDataForApplication("AHID") Is Nothing Then
                                    ArrayyBlock_Group = groupRequired.GetXDataForApplication("AHID").AsArray

                                    Dim entityIds As ObjectId() = groupRequired.GetAllEntityIds()
                                    Dim id As ObjectId, HandleTub As String = ""

                                    For Each id In entityIds
                                        ' open the entity for read 
                                        Dim entPol As Entity = tr.GetObject(id, OpenMode.ForRead)

                                        If TypeOf entPol Is Polyline Then

                                            Select Case UserR2
                                                Case 0.1
                                                    LenPolyline = CType(entPol, Polyline).Length / 100

                                                Case 1
                                                    LenPolyline = CType(entPol, Polyline).Length / 1000

                                                Case Else
                                                    LenPolyline = CType(entPol, Polyline).Length

                                            End Select

                                            HandleTub += "LEN" & SeparatorList & LenPolyline & SeparatorList & "HANDLE" & SeparatorList & entPol.Handle.ToString & "_" & NameTactualProject & SeparatorList
                                        End If

                                        ' create the highlight path 
                                        'Dim path As FullSubentityPath = New FullSubentityPath(New ObjectId(0) {id}, New SubentityId(SubentityType.Null, 0))
                                        ' now highlight it 
                                        'ent.Highlight(path, True)
                                    Next

                                    'Confirm if exists entities in group. If not exist, don't create a line of group in db.dat for the lisp
                                    If groupRequired.NumEntities > 0 Then
                                        Text = "" : Count = 0
                                        For Each Obj As Object In ArrayyBlock_Group
                                            If Count > 0 Then Text += DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode.ToString & SeparatorList & DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value & SeparatorList 'DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value & "|"
                                            Count += 1
                                        Next
                                        'For the end line (string)
                                        If HandleTub.Contains("LEN") Then
                                            Text += HandleTub '"LEN" & SeparatorList & LenPolyline & "|Handle" & SeparatorList & HandleTub & "|"
                                            ArrayList_Object.Add(Text)
                                        End If
                                    End If

                                End If

                            End If

                        End If

                    Next

                    'Now, write in file all information about array
                    SaveText_To_File(ArrayList_Object, PathCad & IniName_FileMaterialSelection & NameObject & ".dat", , True)

                End Using

            End Using

        Catch ex As Exception
            ed.WriteMessage(vbLf & "Unable to read drawing file selection. Error type : " & ex.Message)
            Result = False
        Finally

        End Try

        'After execute all functions, return to lisp a result
        Dim rbfResult As ResultBuffer
        rbfResult = New ResultBuffer(New TypedValue(LispDataType.Text, Result.ToString))
        Return rbfResult

    End Function

    <LispFunction("SelSet_Aenge_Old")> _
    Public Function ExtractObjectsFromFile_SelSet_Old(ByVal rbs As ResultBuffer) As ResultBuffer

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument, Result As Boolean = True
        Dim ed As Editor = doc.Editor, SsGet As SelectionSet, Count As Double = 0
        Dim Text As String = "", ArrayList_Object As New ArrayList()
        Dim PathCad As String = "", NameObject As String = "", AttFind As String = ""

        'For SelectionResult
        Dim rs As PromptSelectionResult, Ro As PromptSelectionOptions = New PromptSelectionOptions

        Try

            With Ro
                .AllowDuplicates = True
                .AllowSubSelections = True
                '.SingleOnly = True
                .Keywords.Add("Selecione os objetos")
            End With

            rs = ed.GetSelection(Ro)
            SsGet = rs.Value

            ' Create a database and try to load the file
            Dim db As Database = doc.Database
            Using db

                Dim tr As Transaction = db.TransactionManager.StartTransaction()

                Using tr
                    ' Iterate through it, dumping objects
                    Dim ArrayyBlock As Array, ArrayyBlock_Group As Array = Nothing

                    For Each objId As ObjectId In SsGet.GetObjectIds
                        'Dim ent As Entity = ObjectFromHandle(objId.Handle.ToString)
                        Dim ent As Entity = DirectCast(tr.GetObject(objId, OpenMode.ForRead), Entity)

                        If TypeOf ent Is BlockReference Then
                            'Filter for xdata application created
                            Dim RSBuffer As ResultBuffer = CType(ent, BlockReference).GetXDataForApplication("AHID")

                            Text = ""
                            If Not RSBuffer Is Nothing Then
                                ArrayyBlock = RSBuffer.AsArray
                                'Clear text for new information

                                Text = "" : Count = 0
                                For Each Obj As Object In ArrayyBlock
                                    If Count > 0 Then Text += DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode.ToString & SeparatorList & DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value & "|"
                                    Count += 1
                                Next
                                Text += "Handle" & SeparatorList & ent.Handle.ToString & "|"
                                ArrayList_Object.Add(Text)

                            End If

                            'Else for others objects in dwg
                        Else
                            'Text = ent.Handle.ToString
                            'ArrayList_Object.Add(Text)
                        End If

                    Next

                    'Now, read all information in groups 
                    Dim ObjTemp_Selection As Object, LenPolyline As Decimal = 0, UserR2 As String = Application.GetSystemVariable("USERR2").ToString
                    For Each ObjT As Object In rs.Value

                        ObjTemp_Selection = ObjectFromObjectId(ObjT.ObjectId)

                        If TypeOf ObjTemp_Selection Is Polyline Then

                            If Not ObjTemp_Selection.GetXDataForApplication("AHID") Is Nothing Then
                                'Get a entity for xdata reading
                                Dim ent As Entity = DirectCast(tr.GetObject(ObjT.ObjectId, OpenMode.ForRead), Entity)

                                Dim persistentReactors As ObjectIdCollection = ent.GetPersistentReactorIds()
                                For Each oid As ObjectId In persistentReactors
                                    If oid.ObjectClass = RXClass.GetClass(GetType(Group)) Then
                                        Dim group As DBObject = tr.GetObject(oid, OpenMode.ForRead)

                                        If Not group.GetXDataForApplication("AHID") Is Nothing Then
                                            ArrayyBlock_Group = group.GetXDataForApplication("AHID").AsArray

                                            Dim entityIds As ObjectId() = CType(group, Group).GetAllEntityIds()
                                            Dim id As ObjectId, HandleTub As String = ""

                                            For Each id In entityIds
                                                ' open the entity for read 
                                                Dim entPol As Entity = tr.GetObject(id, OpenMode.ForRead)

                                                If TypeOf entPol Is Polyline Then

                                                    Select Case UserR2
                                                        Case 0.1
                                                            LenPolyline = CType(entPol, Polyline).Length / 100

                                                        Case 1
                                                            LenPolyline = CType(entPol, Polyline).Length / 1000

                                                        Case Else
                                                            LenPolyline = CType(entPol, Polyline).Length

                                                    End Select

                                                    HandleTub += "LEN" & SeparatorList & LenPolyline & SeparatorList & "HANDLE" & SeparatorList & entPol.Handle.ToString & SeparatorList
                                                End If

                                                ' create the highlight path 
                                                'Dim path As FullSubentityPath = New FullSubentityPath(New ObjectId(0) {id}, New SubentityId(SubentityType.Null, 0))
                                                ' now highlight it 
                                                'ent.Highlight(path, True)
                                            Next

                                            'Confirm if exists entities in group. If not exist, don't create a line of group in db.dat for the lisp
                                            If CType(group, Group).NumEntities > 0 Then
                                                Text = "" : Count = 0
                                                For Each Obj As Object In ArrayyBlock_Group
                                                    If Count > 0 Then Text += DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode.ToString & SeparatorList & DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value & "|" 'DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value & "|"
                                                    Count += 1
                                                Next
                                                'For the end line (string)
                                                Text += HandleTub '"LEN" & SeparatorList & LenPolyline & "|Handle" & SeparatorList & HandleTub & "|"
                                                ArrayList_Object.Add(Text)
                                            End If

                                        End If

                                    End If
                                Next
                                tr.Commit()

                            End If
                        End If
                    Next

                    'Now, write in file all information about array
                    SaveText_To_File(ArrayList_Object, PathCad & IniName_FileMaterialSelection & NameObject & ".dat")

                End Using

            End Using

        Catch ex As Exception
            ed.WriteMessage(vbLf & "Unable to read drawing file selection. Error type : " & ex.Message)
            Result = False
        Finally

        End Try

        'After execute all functions, return to lisp a result
        Dim rbfResult As ResultBuffer
        rbfResult = New ResultBuffer(New TypedValue(LispDataType.Text, Result.ToString))
        Return rbfResult

    End Function

    '<CommandMethod("TestG")> _
    'Public Sub CommandMethodTest()
    '    Dim activeDoc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
    '    Dim db As Database = activeDoc.Database
    '    Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

    '    Using tr As Transaction = ed.Document.Database.TransactionManager.StartTransaction()
    '        Dim pso As New PromptSelectionOptions()
    '        pso.SingleOnly = True
    '        'pso.AllowDuplicates = True

    '        Dim psr As PromptSelectionResult = ed.GetSelection(pso)

    '        If psr.Status = PromptStatus.OK Then

    '            For Each ObjT As Object In psr.Value

    '                Dim t As Object = ObjectFromObjectId(ObjT.ObjectId)

    '                If TypeOf t Is Polyline Then

    '                    Dim ent As Entity = DirectCast(tr.GetObject(ObjT.ObjectId, OpenMode.ForRead), Entity)

    '                    Dim persistentReactors As ObjectIdCollection = ent.GetPersistentReactorIds()
    '                    For Each oid As ObjectId In persistentReactors
    '                        If oid.ObjectClass = RXClass.GetClass(GetType(Group)) Then
    '                            Dim group As DBObject = tr.GetObject(oid, OpenMode.ForRead)
    '                            ed.WriteMessage(group.ObjectId.Handle.ToString())
    '                        End If
    '                    Next
    '                    tr.Commit()

    '                End If

    '            Next

    '        End If
    '    End Using
    'End Sub

    'Read all objects exists in modelspace
    '<CommandMethod("extractgroups")> _
    'Public Sub ExtractAllGroups()

    '    Dim Text As String = "", ArrayList_Object As New ArrayList()
    '    Dim PathCad As String = ""

    '    'Get a AcadReference for objects 
    '    Dim AcadRef As Autodesk.AutoCAD.Interop.AcadApplication, DocTactual As AcadDocument
    '    Dim group As AcadGroup
    '    AcadRef = ReturnAcadReference()
    '    DocTactual = AcadRef.ActiveDocument

    '    For Each group In DocTactual.Groups
    '        Text = group.Handle
    '        ArrayList_Object.Add(Text)
    '    Next

    '    'Now, write in file all information about array
    '    SaveText_To_File(ArrayList_Object, "c:\teste.txt")

    'End Sub

#End Region

#Region "----- Functions General - AutoCad -----"

    'Retorna os dados relacionados ao layer corrente no Autocad 
    Public Function GetCurrentLayer() As Object

        Dim LayerCurrent As Object

        Try

            LayerCurrent = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable(My.Settings.VarSys_CLAYER)
            Return LayerCurrent

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    'Get all list of layers in Dwg tactual
    Public Function LayersToList() As List(Of String)

        Dim lstlay As New List(Of String)()
        Dim layer As LayerTableRecord
        Dim db As Database = HostApplicationServices.WorkingDatabase

        Using tr As Transaction = db.TransactionManager.StartOpenCloseTransaction()
            Dim lt As LayerTable = TryCast(tr.GetObject(db.LayerTableId, OpenMode.ForRead), LayerTable)
            For Each layerId As ObjectId In lt
                layer = TryCast(tr.GetObject(layerId, OpenMode.ForRead), LayerTableRecord)
                lstlay.Add(layer.Name)
            Next
        End Using

        Return lstlay

    End Function

    'Get all list of layers in Dwg tactual
    Public Function LineTypeToList() As List(Of String)

        Dim lstlay As New List(Of String)()
        Dim LineTyp As LinetypeTableRecord
        Dim db As Database = HostApplicationServices.WorkingDatabase

        Using tr As Transaction = db.TransactionManager.StartOpenCloseTransaction()
            Dim lt As LinetypeTable = TryCast(tr.GetObject(db.LinetypeTableId, OpenMode.ForRead), LinetypeTable)
            For Each layerId As ObjectId In lt
                LineTyp = TryCast(tr.GetObject(layerId, OpenMode.ForRead), LinetypeTableRecord)
                lstlay.Add(LineTyp.Name)
            Next
        End Using

        Return lstlay

    End Function

    <LispFunction("ExplodeBlock")> _
        Function ExplodeBlock(ByVal Rbf As ResultBuffer) As ResultBuffer

        'Pass the handle 
        If Rbf Is Nothing Then Return Nothing
        If Rbf.AsArray(0).Value.ToString() = "" Then Return Nothing

        Dim db As Database = HostApplicationServices.WorkingDatabase
        Dim ed As Editor = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor
        Dim ObjId As Object, HandleTemp As String = Rbf.AsArray(0).Value.ToString

        'Get objectId from handle 
        ObjId = ObjectFromHandle(HandleTemp)

        Using tr As Transaction = db.TransactionManager.StartTransaction

            Try

                Dim btr As BlockTableRecord = CType(tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite), BlockTableRecord)
                Dim coll As DBObjectCollection = New Autodesk.AutoCAD.DatabaseServices.DBObjectCollection()
                Dim ent As Entity
                Dim oDim As BlockReference  'Dimension

                ent = CType(tr.GetObject(ObjId.ObjectId, OpenMode.ForWrite), Entity)

                If TypeOf (ent) Is BlockReference Then
                    oDim = ent
                    oDim.Explode(coll)
                    Dim iter As System.Collections.IEnumerator = coll.GetEnumerator()
                    'Add the entities from exploding the dimension to model space
                    While iter.MoveNext()
                        Dim entCur As Entity = CType(iter.Current, Entity)
                        btr.AppendEntity(entCur)
                        tr.AddNewlyCreatedDBObject(entCur, True)
                    End While
                    'erase the dimension
                    'oDim.Erase()
                    'clear the collection of exploded entities
                    coll.Clear()
                End If

                tr.Commit()

            Catch ex As System.Exception
                LibraryError.CreateErrorAenge(Err, "Error explode block - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad013")
            End Try

        End Using

        Return Nothing

    End Function

#End Region

#Region "----- XData functions - Reading and Writing -----"

    'Receive a list of circuits data and get upper number of returns in wich circuito
    <LispFunction("ReturnQtdRet")> _
    Function GetUpper_NumberRetorno(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim ReturnRbf As ResultBuffer = Nothing
        Dim QtdCircuit As Integer = 0, Count As Integer = 1, ValueTactual As String, UpperValue As Integer = 0

        'In this case, ini  0 and ubound - 2 (Cab e Bottom)
        QtdCircuit = UBound(Rbf.AsArray) - 1

        Try

            For Count = 1 To QtdCircuit
                ValueTactual = Rbf.AsArray(Count).Value
                If IsNumeric(Right(ValueTactual.Split("R")(0), 1)) Then If UpperValue < Right(ValueTactual.Split("R")(0), 1) Then UpperValue = Right(ValueTactual.Split("R")(0), 1)
            Next

            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, UpperValue.ToString))

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad002")
        End Try

        Return ReturnRbf

    End Function

    'Set all values of objects - In this case of circuit and return objects 
    <LispFunction("setxdata_circuit")> _
    Function SetXData_Circuit(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim activeDoc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Dim db As Database = activeDoc.Database, RbfResult As ResultBuffer = Nothing
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor, Quant_StrNewData As Integer
        Dim StrNewData As String, NewCirc = "", NewRet(9) As String
        'Only return for lisp the result of xdata 
        Dim ReturnRbf As ResultBuffer, HandleObj As String

        Try
            StrNewData = Rbf.AsArray(0).Value
            Quant_StrNewData = StrNewData.Split("|").Count

            'Validate information about newdata 
            NewCirc = StrNewData.Split("|")(0)
            If Quant_StrNewData >= 1 Then NewRet(0) = StrNewData.Split("|")(1).ToString
            If Quant_StrNewData >= 2 Then NewRet(1) = StrNewData.Split("|")(2).ToString
            If Quant_StrNewData >= 3 Then NewRet(2) = StrNewData.Split("|")(3).ToString
            If Quant_StrNewData >= 4 Then NewRet(3) = StrNewData.Split("|")(4).ToString
            If Quant_StrNewData >= 5 Then NewRet(4) = StrNewData.Split("|")(5).ToString
            If Quant_StrNewData >= 6 Then NewRet(5) = StrNewData.Split("|")(6).ToString

            'Fase
            If Quant_StrNewData >= 7 Then NewRet(6) = StrNewData.Split("|")(7).ToString
            'Neutro
            If Quant_StrNewData >= 8 Then NewRet(7) = StrNewData.Split("|")(8).ToString
            'Terra
            If Quant_StrNewData >= 9 Then NewRet(8) = StrNewData.Split("|")(9).ToString
            'Condutor 
            If Quant_StrNewData >= 10 Then NewRet(9) = StrNewData.Split("|")(10).ToString

            'Ini and End of array with information circuits 
            Dim IniArray As Integer = 1, EndArray As Integer = Rbf.AsArray.Length - 1    'Anteriormente era 2 o numero a ser retirado do array (Raul) - Apos alteracao do lisp foi para 1 
            Dim Count_TotalXDataItem As Integer = 0

            Using tr As Transaction = ed.Document.Database.TransactionManager.StartTransaction()

                'Get data about tablerecord for xdata 
                'Dim rat As RegAppTable = DirectCast(tr.GetObject(db.RegAppTableId, OpenMode.ForWrite, False), RegAppTable)
                'Dim regAppName As String = "AHID"

                For i As Integer = IniArray To EndArray
                    HandleObj = Rbf.AsArray(i).Value.ToString
                    Dim objCirc As Object = ObjectFromHandle(HandleObj)
                    Dim ArrayXData As Object = Nothing

                    If TypeOf objCirc Is BlockReference Then

                        If CType(objCirc, BlockReference).GetXDataForApplication("AHID").AsArray.Count > 0 Then
                            ArrayXData = CType(objCirc, BlockReference).GetXDataForApplication("AHID").AsArray
                            Count_TotalXDataItem = CType(objCirc, BlockReference).GetXDataForApplication("AHID").AsArray.Count
                        End If

                        'Now, set a new values of xdata 
                        Dim id As ObjectId
                        id = objCirc.ObjectId

                        'Dim bt As BlockTable = tr.GetObject(db.BlockTableId, OpenMode.ForRead)
                        'Dim msPace As BlockTableRecord = tr.GetObject(bt.Item(BlockTableRecord.ModelSpace), OpenMode.ForRead)
                        Dim ent As DBObject = tr.GetObject(id, OpenMode.ForWrite)
                        Dim ClassTactual As String = "", FieldUpdated As Boolean = True
                        AddRegAppTableRecord("AHID")

                        RbfResult = New ResultBuffer
                        Dim CountXData_Item As Integer = 1, ExistReturn As Boolean = True, J As Integer = 0
                        For Each ObjItem As Object In ArrayXData
                            Select Case CountXData_Item
                                Case Is <= 4
                                    If CountXData_Item = 2 Then ClassTactual = DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                    RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value))

                                    'Circuit - In this case, all circuits objects (5 element of array is circuit)
                                Case 5
                                    RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value))
                                    'RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewCirc))

                                Case Else

                                    'If DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 1000 Then ExistReturn = False
                                    'If ExistReturn = True Then
                                    '    RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J)))
                                    '    'Count for array newret (Values set)
                                    '    J += 1
                                    'Else

                                    'Precisamos verificar se eh fase, neutro ou terra, para setarmos os novos valores 
                                    FieldUpdated = False
                                    Select Case ClassTactual
                                        Case "0060"
                                            'Atualizamos a fase e condutor 
                                            If CountXData_Item = 6 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 7 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0061"
                                            'Atualizamos a neutro e condutor 
                                            If CountXData_Item = 6 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 7 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0062"
                                            'Atualizamos circuito retorno
                                            If CountXData_Item = 6 Then If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 7 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0085"
                                            'Atualizamos circuito 2 retorno
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Then If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 8 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0086"
                                            'Atualizamos circuito 3 retorno
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Then If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 9 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0087"
                                            'Atualizamos circuito 4 retorno
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Or CountXData_Item = 9 Then If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 10 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0088"
                                            'Atualizamos circuito 5 retorno
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Or CountXData_Item = 9 Or CountXData_Item = 10 Then If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 11 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0089"
                                            'Atualizamos circuito 6 retorno
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Or CountXData_Item = 9 Or CountXData_Item = 10 Or CountXData_Item = 11 Then If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 12 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0063"
                                            'Atualizamos o terra e condutor 
                                            If CountXData_Item = 6 Then If NewRet(8).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(8))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 7 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0064"
                                            'Atualizamos a fase, neutro e condutor 
                                            If CountXData_Item = 6 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            If CountXData_Item = 7 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 8 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0065"
                                            'Atualizamos a fase, terra e condutor 
                                            If CountXData_Item = 6 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            If CountXData_Item = 7 Then If NewRet(8).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(8))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 8 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0066"
                                            'Atualizamos a fase, neutro, terra e condutor 
                                            If CountXData_Item = 6 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            If CountXData_Item = 7 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            If CountXData_Item = 8 Then If NewRet(8).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(8))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 9 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0067", "0079"
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If

                                            If ClassTactual = "0067" Then
                                                'Fase 
                                                If CountXData_Item = 7 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            ElseIf ClassTactual = "0079" Then
                                                'Neutro retorno 
                                                If CountXData_Item = 7 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            End If

                                            'Condutor 
                                            If CountXData_Item = 8 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0068", "0080"
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If

                                            If ClassTactual = "0068" Then
                                                'Fase 2 retorno 
                                                If CountXData_Item = 8 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            ElseIf ClassTactual = "0080" Then
                                                'Fase 2 retorno 
                                                If CountXData_Item = 8 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            End If

                                            'Condutor 
                                            If CountXData_Item = 9 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0069", "0081"
                                            'Fase 3 retorno
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If

                                            If ClassTactual = "0069" Then
                                                If CountXData_Item = 9 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            ElseIf ClassTactual = "0081" Then
                                                If CountXData_Item = 9 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            End If

                                            'Condutor 
                                            If CountXData_Item = 10 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0070", "0082"
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Or CountXData_Item = 9 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If
                                            'Fase 4 retorno 

                                            If ClassTactual = "0070" Then
                                                If CountXData_Item = 10 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            ElseIf ClassTactual = "0082" Then
                                                If CountXData_Item = 10 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            End If

                                            'Condutor 
                                            If CountXData_Item = 11 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0071", "0083"
                                            'Fase 5 retorno 
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Or CountXData_Item = 9 Or CountXData_Item = 10 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If

                                            If ClassTactual = "0071" Then
                                                If CountXData_Item = 11 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            ElseIf ClassTactual = "0083" Then
                                                If CountXData_Item = 11 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            End If

                                            'Condutor 
                                            If CountXData_Item = 12 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0072", "0084"
                                            'Fase 6 retorno 
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Or CountXData_Item = 9 Or CountXData_Item = 10 Or CountXData_Item = 11 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If

                                            If ClassTactual = "0072" Then
                                                If CountXData_Item = 12 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            ElseIf ClassTactual = "0084" Then
                                                If CountXData_Item = 12 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            End If

                                            'Condutor 
                                            If CountXData_Item = 13 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0073"
                                            'Fase neutro retorno
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If

                                            If CountXData_Item = 7 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            If CountXData_Item = 8 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 9 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0074"
                                            'Fase neutro 2 retorno
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If

                                            If CountXData_Item = 8 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            If CountXData_Item = 9 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 10 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0075"
                                            'Fase neutro 3 retorno
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If

                                            If CountXData_Item = 9 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            If CountXData_Item = 10 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 11 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0076"
                                            'Fase neutro 4 retorno
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Or CountXData_Item = 9 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If

                                            If CountXData_Item = 10 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            If CountXData_Item = 11 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 12 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0077"
                                            'Fase neutro 5 retorno
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Or CountXData_Item = 9 Or CountXData_Item = 10 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If

                                            If CountXData_Item = 11 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            If CountXData_Item = 12 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 13 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                        Case "0078"
                                            'Fase neutro 6 retorno
                                            'Retorno vem primeiro 
                                            If CountXData_Item = 6 Or CountXData_Item = 7 Or CountXData_Item = 8 Or CountXData_Item = 9 Or CountXData_Item = 10 Or CountXData_Item = 11 Then
                                                If NewRet(J).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J))) : J += 1 : FieldUpdated = True
                                                'Count for array newret (Values set)
                                                'J += 1
                                            End If

                                            If CountXData_Item = 12 Then If NewRet(6).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(6))) : FieldUpdated = True
                                            If CountXData_Item = 13 Then If NewRet(7).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(7))) : FieldUpdated = True
                                            'Condutor 
                                            If CountXData_Item = 14 Then If NewRet(9).ToString.Trim <> "" Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(9))) : FieldUpdated = True

                                    End Select

                                    'Se no select acima nao tiver sido atualizado algum campo, entao atualizamos o campo que ficou para tras 
                                    If FieldUpdated = False Then RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value))
                                    'End If

                            End Select

                            'Add 1 to count 
                            CountXData_Item += 1
                        Next

                        'Antes de atualizar o xdata, verifica se esta passando os 4 ultimos campos, no caso o secao fase, secao neutro, secai terra e o condutor 
                        ent.XData = RbfResult

                        If (Not CType(ent, BlockReference).AttributeCollection Is Nothing) Then
                            Dim attId As ObjectId
                            For Each attId In CType(ent, BlockReference).AttributeCollection
                                Dim att As AttributeReference = TryCast(attId.GetObject(Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead, False), AttributeReference)
                                att.UpgradeOpen()

                                Select Case att.Tag
                                    'Case "001"  'Circ
                                    '    att.TextString = NewCirc

                                    Case "009"  'Ret1
                                        If NewRet(0).ToString.Trim <> "" Then att.TextString = NewRet(0)

                                    Case "030"  'Ret2
                                        If NewRet(1).ToString.Trim <> "" Then att.TextString = NewRet(1)

                                    Case "031"  'Ret3
                                        If NewRet(2).ToString.Trim <> "" Then att.TextString = NewRet(2)

                                    Case "032"  'Ret4
                                        If NewRet(3).ToString.Trim <> "" Then att.TextString = NewRet(3)

                                    Case "033"  'Ret5
                                        If NewRet(4).ToString.Trim <> "" Then att.TextString = NewRet(4)

                                    Case "040"  'Ret6
                                        If NewRet(5).ToString.Trim <> "" Then att.TextString = NewRet(5)

                                End Select

                                att.DowngradeOpen()
                            Next
                        End If

                        'Dispose resultbuffer after edit a atributes 
                        RbfResult.Dispose()

                    End If

                Next

                tr.Commit()

            End Using

            'ReturnRbf = Nothing 'New ResultBuffer(New TypedValue(LispDataType.Text, "S"))

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Ocorrência de erro : " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad003")
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, "False"))
        End Try

        'Return RbfResult
        Return Nothing

    End Function

    'Validate RegApp in table record - Xdata values
    Private Shared Sub AddRegAppTableRecord(ByVal regAppName As String)

        Dim doc As Document = Application.DocumentManager.MdiActiveDocument
        Dim ed As Editor = doc.Editor
        Dim db As Database = doc.Database
        Dim tr As Transaction = doc.TransactionManager.StartTransaction()

        Using tr
            Dim rat As RegAppTable = DirectCast(tr.GetObject(db.RegAppTableId, OpenMode.ForRead, False), RegAppTable)
            If Not rat.Has(regAppName) Then

                rat.UpgradeOpen()
                Dim ratr As New RegAppTableRecord()
                ratr.Name = regAppName
                rat.Add(ratr)
                tr.AddNewlyCreatedDBObject(ratr, True)
            End If

            tr.Commit()
        End Using

    End Sub

    <LispFunction("setxdata_circuit_Vb6")> _
     Function SetXData_Circuit_Vb6(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim XDataType() As Integer, XDataValue() As Object

        'XDataType(0) = 1001
        'XDataValue(0) = "PETER"
        'XDataType(1) = 1002
        'XDataValue(1) = "{"
        'XDataType(2) = 1000
        'XDataValue(2) = "item3"
        'XDataType(3) = 1000
        'XDataValue(3) = "item2changed"
        'XDataType(4) = 1000
        'XDataValue(4) = "item"
        'XDataType(5) = 1002
        'XDataValue(5) = "}"

        Dim activeDoc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Dim db As Database = activeDoc.Database, RbfResult As ResultBuffer = Nothing
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor, Quant_StrNewData As Integer
        Dim StrNewData As String, NewCirc = "", NewRet(5) As String

        Try
            StrNewData = Rbf.AsArray(0).Value
            Quant_StrNewData = StrNewData.Split("|").Count

            'Validate information about newdata 
            NewCirc = StrNewData.Split("|")(0)
            If Quant_StrNewData >= 1 Then NewRet(0) = StrNewData.Split("|")(1).ToString
            If Quant_StrNewData >= 1 Then NewRet(1) = StrNewData.Split("|")(2).ToString
            If Quant_StrNewData >= 1 Then NewRet(2) = StrNewData.Split("|")(3).ToString
            If Quant_StrNewData >= 1 Then NewRet(3) = StrNewData.Split("|")(4).ToString
            If Quant_StrNewData >= 1 Then NewRet(4) = StrNewData.Split("|")(5).ToString
            If Quant_StrNewData >= 1 Then NewRet(5) = StrNewData.Split("|")(6).ToString

            'Ini and End of array with information circuits 
            Dim IniArray As Integer = 2, EndArray As Integer = Rbf.AsArray.Length - 2
            Dim Count_TotalXDataItem As Integer = 0

            Using tr As Transaction = ed.Document.Database.TransactionManager.StartTransaction()

                For i As Integer = IniArray To EndArray
                    Dim objCirc As Object = ObjectFromHandle(Rbf.AsArray(i).Value.ToString)
                    Dim ArrayXData As Object = Nothing

                    If TypeOf objCirc Is BlockReference Then

                        If CType(objCirc, BlockReference).GetXDataForApplication("AHID").AsArray.Count > 0 Then
                            ArrayXData = CType(objCirc, BlockReference).GetXDataForApplication("AHID").AsArray
                            Count_TotalXDataItem = CType(objCirc, BlockReference).GetXDataForApplication("AHID").AsArray.Count
                        End If

                        'Now, set a new values of xdata 
                        Dim id As ObjectId
                        id = objCirc.ObjectId
                        Dim ent As Entity = tr.GetObject(id, OpenMode.ForRead)

                        RbfResult = New ResultBuffer
                        Dim CountXData_Item As Integer = 1, ExistReturn As Boolean = True, J As Integer = 0

                        ReDim XDataType(Count_TotalXDataItem - 1)
                        ReDim XDataValue(Count_TotalXDataItem - 1)

                        For Each ObjItem As Object In ArrayXData
                            Select Case CountXData_Item
                                Case Is <= 4
                                    'RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value))
                                    XDataType(CountXData_Item - 1) = DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode
                                    XDataValue(CountXData_Item - 1) = DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value

                                    'Circuit - In this case, all circuits objects (5 element of array is circuit)
                                Case 5
                                    'RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewCirc))
                                    XDataType(CountXData_Item - 1) = DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode
                                    XDataValue(CountXData_Item - 1) = NewCirc

                                Case Else
                                    If DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 1000 Then ExistReturn = False
                                    If ExistReturn = True Then
                                        'RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewRet(J)))
                                        XDataType(CountXData_Item - 1) = DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode
                                        XDataValue(CountXData_Item - 1) = NewRet(J)
                                        'Count for array newret (Values set)
                                        J += 1
                                    Else

                                        'Now, only exists other types and finish with a quantity of circuit, fases....
                                        'If Count_TotalXDataItem > CountXData_Item Then
                                        'RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value))
                                        XDataType(CountXData_Item - 1) = DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode
                                        XDataValue(CountXData_Item - 1) = DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                        'Else
                                        'RbfResult.Add(New TypedValue(DirectCast(ObjItem, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode, NewStrFases))
                                        'End If
                                    End If

                            End Select

                            'Add 1 to count 
                            CountXData_Item += 1
                        Next

                        'Set a new xdata 
                        'Dim mApp As AcadApplication = CType(Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication, Autodesk.AutoCAD.Interop.AcadApplication)
                        'Dim mDoc As AcadDocument = mApp.ActiveDocument
                        'Dim mDocBlock As Autodesk.AutoCAD.Interop.Common.AcadEntity = CType(ent, Autodesk.AutoCAD.Interop.Common.AcadEntity) 'mDoc.HandleToObject(Rbf.AsArray(i).Value.ToString)
                        'mDocBlock.SetXData(XDataType, XDataValue)

                        ent.XData.Dispose()
                        ent.XData.Add(RbfResult)
                        tr.Commit()

                    End If

                Next

            End Using

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Ocorrência de erro : " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad003")
        End Try

        Return RbfResult

    End Function

#End Region

#Region "----- Functions for windows, files and diretories -----"

    Private Shared Sub ZoomWin(ByVal ed As Editor, ByVal min As Point3d, ByVal max As Point3d)
        Dim min2d As New Point2d(min.X, min.Y)
        Dim max2d As New Point2d(max.X, max.Y)

        Dim view As New ViewTableRecord()

        view.CenterPoint = min2d + ((max2d - min2d) / 2.0)
        view.Height = max2d.Y - min2d.Y
        view.Width = max2d.X - min2d.X
        ed.SetCurrentView(view)
    End Sub

    Function HighLight_Object(ByVal HandleObj As String) As Object

        Dim Doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Dim ent As Entity, ObjId As ObjectId
        Dim db As Database = HostApplicationServices.WorkingDatabase()
        Dim trans As Transaction, Ed As Editor
        trans = db.TransactionManager.StartTransaction

        Try

            ObjId = ObjectFromHandle(HandleObj)
            ent = trans.GetObject(ObjId, OpenMode.ForNotify)
            ent.Highlight()
            Ed = Doc.Editor

            Dim ext As Extents3d

            Using trans
                ent = DirectCast(trans.GetObject(ObjId, OpenMode.ForRead), Entity)
                ext = ent.GeometricExtents
                trans.Commit()
            End Using

            ext.TransformBy(Ed.CurrentUserCoordinateSystem.Inverse())

            ' Call our helper function
            ' [Change this to ZoomWin2 or WoomWin3 to
            ' use different zoom techniques]
            ZoomWin(Ed, ext.MinPoint, ext.MaxPoint)
            Return True

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error setting highlight object - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad021")
            Return False
        End Try

    End Function

    'Create a preview image dwg current
    Function SavePreviewImage() As Object

        Dim LibraryComponent As Aenge.Library.Component.LibraryComponent
        Dim FileBmp, FileBmpReconsult As Bitmap

        Try

            If IsSaveImgThumbnail = False Then Return False

            Dim Doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
            Dim ReconsultImg As Boolean = False

            If Doc Is Nothing Then Return False
            If Doc.CommandInProgress.ToString = "" Then Return False

            Dim AcCad As Object = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication
            Dim Bmp As System.Drawing.Bitmap, NameTactual_Dwg As String, NameTactual_Project As String
            Dim LibraryReference As New LibraryReference
            Dim NameBmp As String = ""

            LibraryComponent = New Aenge.Library.Component.LibraryComponent

            NameTactual_Project = LibraryReference.Return_TactualProject()
            NameTactual_Dwg = LibraryReference.Return_TactualDrawing()

            'If usercontrol is loaded and is a same project, then not save a preview of drawing 
            If Not _UsrC_Project Is Nothing Then
                'Load a empty image for not block tactual image 
                If _UsrC_Project.lblNameDwg.Text.Contains(NameTactual_Dwg.ToString) Then
                    If My.Computer.FileSystem.FileExists(GetAppInstall() & My.Settings.NoImage_Preview) Then
                        FileBmp = LibraryComponent.LoadImage_InPictureBox(GetAppInstall() & My.Settings.NoImage_Preview)
                        _UsrC_Project.Dwg.Image = FileBmp
                        ReconsultImg = True
                    End If
                End If
            End If

            Bmp = Autodesk.AutoCAD.ApplicationServices.DocumentExtension.CapturePreviewImage(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument, My.Settings.PreviewWidth, My.Settings.PreviewHeight)
            'Save only exists directory
            If My.Computer.FileSystem.DirectoryExists(GetAppInstall() & NameTactual_Project) Then
                NameBmp = GetAppInstall() & NameTactual_Project & "\" & NameTactual_Dwg & My.Settings.ExtensionCompl_Img & My.Settings.Extension_Img
                If My.Computer.FileSystem.FileExists(NameBmp) Then My.Computer.FileSystem.DeleteFile(NameBmp)
                Bmp.Save(NameBmp, System.Drawing.Imaging.ImageFormat.Bmp)
            End If

            System.Windows.Forms.Application.DoEvents()
            If ReconsultImg Then If My.Computer.FileSystem.FileExists(NameBmp) Then FileBmpReconsult = LibraryComponent.LoadImage_InPictureBox(NameBmp) : _UsrC_Project.Dwg.Image = FileBmpReconsult

            LibraryReference = Nothing : Bmp = Nothing
            LibraryComponent = Nothing
            Return True

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error saving preview project - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad012")
            Return False
        Finally
            LibraryComponent = Nothing
        End Try

    End Function

    'Create a datatable structure for data and search info in memory
    Function CreateDataTable(ByVal TypeTable As String) As System.Data.DataTable

        Dim dt As System.Data.DataTable = Nothing
        Dim dr As System.Data.DataRow = Nothing

        Dim Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8, Column9 As System.Data.DataColumn

        dt = New System.Data.DataTable()

        Try

            Select Case TypeTable
                'Tabela para armazenar os diagnosticos encontrados na aplicação 
                Case "diagnostico"
                    Column1 = New System.Data.DataColumn("TypeObject", Type.GetType("System.Int16"))   'Handle da tubulação 
                    Column2 = New System.Data.DataColumn("Handle", Type.GetType("System.String"))  'Circuito
                    Column3 = New System.Data.DataColumn("Descricao", Type.GetType("System.String"))  'Circuito
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)

                    'For order list os circuit auto - Get Handle and info circ, but return two lists, one with handles and another with list of circ ordered asc 
                Case "orderlistcircauto"
                    'Create a new datatable 
                    Column1 = New System.Data.DataColumn("Handle", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Circuito", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("Fase", Type.GetType("System.Int16"))
                    Column4 = New System.Data.DataColumn("Neutro", Type.GetType("System.Int16"))
                    Column5 = New System.Data.DataColumn("Terra", Type.GetType("System.Int16"))
                    Column6 = New System.Data.DataColumn("Retorno", Type.GetType("System.Int16"))
                    Column7 = New System.Data.DataColumn("Completo", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)
                    dt.Columns.Add(Column6)
                    dt.Columns.Add(Column7)

                    'This datatable is temp for order return in lisp, receive a list of handles and tratament for lisp before
                Case "order_retorno"
                    'Create a new datatable 
                    Column1 = New System.Data.DataColumn("Qdc", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Retorno", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)

                    'List of all circuit in selection (insertion auto)
                Case "listcircuit"
                    'Column1 = New System.Data.DataColumn("Flag", Type.GetType("System.String"))
                    Column1 = New System.Data.DataColumn("Circuito", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Fase", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("SecaoFase", Type.GetType("System.String"))
                    Column4 = New System.Data.DataColumn("Neutro", Type.GetType("System.String"))
                    Column5 = New System.Data.DataColumn("SecaoNeutro", Type.GetType("System.String"))
                    Column6 = New System.Data.DataColumn("Terra", Type.GetType("System.String"))
                    Column7 = New System.Data.DataColumn("SecaoTerra", Type.GetType("System.String"))
                    Column8 = New System.Data.DataColumn("CodEle", Type.GetType("System.String"))   'Condutor 
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)
                    dt.Columns.Add(Column6)
                    dt.Columns.Add(Column7)
                    dt.Columns.Add(Column8)

                Case "pathcad"
                    Column1 = New System.Data.DataColumn("PathTree", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)

                Case "handleselected"
                    'Create a new datatable 
                    Column1 = New System.Data.DataColumn("HandleTub", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Flag", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)

                    'Create a datatble for circuitauto - mapeamento 
                Case "mapeamento"
                    'Create a new datatable 
                    Column1 = New System.Data.DataColumn("Qdc", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Chave", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("HandleTub", Type.GetType("System.String"))
                    Column4 = New System.Data.DataColumn("HandleObj", Type.GetType("System.String"))
                    Column5 = New System.Data.DataColumn("CircObj", Type.GetType("System.String"))
                    Column6 = New System.Data.DataColumn("Flag", Type.GetType("System.String"))
                    Column7 = New System.Data.DataColumn("EhTomada", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)
                    dt.Columns.Add(Column6)
                    dt.Columns.Add(Column7)

                    'Create a datatble for circuitauto - mapeamento 
                Case "mapeamento_tub"
                    'Create a new datatable 
                    Column1 = New System.Data.DataColumn("HandleGroup", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("ObjectIdGroup", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("HandleTub", Type.GetType("System.String"))
                    Column4 = New System.Data.DataColumn("HandleObj1", Type.GetType("System.String"))
                    Column5 = New System.Data.DataColumn("HandleObj2", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)

                Case "apau_printambiente"
                    Column1 = New System.Data.DataColumn("Handle", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Circuito", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("Retorno", Type.GetType("System.String"))
                    Column4 = New System.Data.DataColumn("Tipo", Type.GetType("System.String"))
                    Column5 = New System.Data.DataColumn("Ambiente", Type.GetType("System.String"))
                    Column6 = New System.Data.DataColumn("TSaida", Type.GetType("System.String"))
                    Column7 = New System.Data.DataColumn("Campo1", Type.GetType("System.Int16"))
                    Column8 = New System.Data.DataColumn("Campo2", Type.GetType("System.Int16"))
                    Column9 = New System.Data.DataColumn("Campo3", Type.GetType("System.Object"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)
                    dt.Columns.Add(Column6)
                    dt.Columns.Add(Column7)
                    dt.Columns.Add(Column8)
                    dt.Columns.Add(Column9)

            End Select

            Return dt

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error in datatable info creating - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad006")
            Return Nothing
        End Try

    End Function

    'Create a datatable structure for data and search info in memory
    Function CreateDataTable_Old(ByVal TypeTable As String) As System.Data.DataTable

        Dim dt As System.Data.DataTable = Nothing
        Dim dr As System.Data.DataRow = Nothing

        Dim Column1, Column2, Column3, Column4, Column5, Column6, Column7, Column8, Column9, Column10, Column11 As System.Data.DataColumn
        Dim Column12, Column13, Column14, Column15, Column16, Column17, Column18, Column19, Column20, Column21 As System.Data.DataColumn
        Dim Column22, Column23, Column24, Column25, Column26, Column27, Column28, Column29, Column30, Column31 As System.Data.DataColumn
        Dim Column32, Column33, Column34, Column35, Column36, Column37, Column38, Column39, Column40, Column41 As System.Data.DataColumn
        Dim Column42 As System.Data.DataColumn

        dt = New System.Data.DataTable()

        Try

            Select Case TypeTable
                'Tabela temporaria para criar os dados antigos dos .dat, anteriormente eram criados nos dar, mas com desenhos muitos grandes o sistema comecava a ficar lento 
                Case "dtaeleobjc"
                    Column1 = New System.Data.DataColumn("ID", Type.GetType("System.Int16"))
                    Column2 = New System.Data.DataColumn("DWG", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("TYPE1", Type.GetType("System.String"))
                    Column4 = New System.Data.DataColumn("FIELD1", Type.GetType("System.String"))
                    Column5 = New System.Data.DataColumn("TYPE2", Type.GetType("System.String"))
                    Column6 = New System.Data.DataColumn("FIELD2", Type.GetType("System.String"))
                    Column7 = New System.Data.DataColumn("TYPE3", Type.GetType("System.String"))
                    Column8 = New System.Data.DataColumn("FIELD3", Type.GetType("System.String"))
                    Column9 = New System.Data.DataColumn("TYPE4", Type.GetType("System.String"))
                    Column10 = New System.Data.DataColumn("FIELD4", Type.GetType("System.String"))
                    Column11 = New System.Data.DataColumn("TYPE5", Type.GetType("System.String"))
                    Column12 = New System.Data.DataColumn("FIELD5", Type.GetType("System.String"))
                    Column13 = New System.Data.DataColumn("TYPE6", Type.GetType("System.String"))
                    Column14 = New System.Data.DataColumn("FIELD6", Type.GetType("System.String"))
                    Column15 = New System.Data.DataColumn("TYPE7", Type.GetType("System.String"))
                    Column16 = New System.Data.DataColumn("FIELD7", Type.GetType("System.String"))
                    Column17 = New System.Data.DataColumn("TYPE8", Type.GetType("System.String"))
                    Column18 = New System.Data.DataColumn("FIELD8", Type.GetType("System.String"))
                    Column19 = New System.Data.DataColumn("TYPE9", Type.GetType("System.String"))
                    Column20 = New System.Data.DataColumn("FIELD9", Type.GetType("System.String"))
                    Column21 = New System.Data.DataColumn("TYPE10", Type.GetType("System.String"))
                    Column22 = New System.Data.DataColumn("FIELD10", Type.GetType("System.String"))
                    Column23 = New System.Data.DataColumn("TYPE11", Type.GetType("System.String"))
                    Column24 = New System.Data.DataColumn("FIELD11", Type.GetType("System.String"))
                    Column25 = New System.Data.DataColumn("TYPE12", Type.GetType("System.String"))
                    Column26 = New System.Data.DataColumn("FIELD12", Type.GetType("System.String"))
                    Column27 = New System.Data.DataColumn("TYPE13", Type.GetType("System.String"))
                    Column28 = New System.Data.DataColumn("FIELD13", Type.GetType("System.String"))
                    Column29 = New System.Data.DataColumn("TYPE14", Type.GetType("System.String"))
                    Column30 = New System.Data.DataColumn("FIELD14", Type.GetType("System.String"))
                    Column31 = New System.Data.DataColumn("TYPE15", Type.GetType("System.String"))
                    Column32 = New System.Data.DataColumn("FIELD15", Type.GetType("System.String"))
                    Column33 = New System.Data.DataColumn("TYPE16", Type.GetType("System.String"))
                    Column34 = New System.Data.DataColumn("FIELD16", Type.GetType("System.String"))
                    Column35 = New System.Data.DataColumn("TYPE17", Type.GetType("System.String"))
                    Column36 = New System.Data.DataColumn("FIELD17", Type.GetType("System.String"))
                    Column37 = New System.Data.DataColumn("TYPE18", Type.GetType("System.String"))
                    Column38 = New System.Data.DataColumn("FIELD18", Type.GetType("System.String"))
                    Column39 = New System.Data.DataColumn("TYPE19", Type.GetType("System.String"))
                    Column40 = New System.Data.DataColumn("FIELD19", Type.GetType("System.String"))
                    Column41 = New System.Data.DataColumn("TYPE20", Type.GetType("System.String"))
                    Column42 = New System.Data.DataColumn("FIELD20", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)
                    dt.Columns.Add(Column6)
                    dt.Columns.Add(Column7)
                    dt.Columns.Add(Column8)
                    dt.Columns.Add(Column9)
                    dt.Columns.Add(Column10)
                    dt.Columns.Add(Column11)
                    dt.Columns.Add(Column12)
                    dt.Columns.Add(Column13)
                    dt.Columns.Add(Column14)
                    dt.Columns.Add(Column15)
                    dt.Columns.Add(Column16)
                    dt.Columns.Add(Column17)
                    dt.Columns.Add(Column18)
                    dt.Columns.Add(Column19)
                    dt.Columns.Add(Column20)
                    dt.Columns.Add(Column21)
                    dt.Columns.Add(Column22)
                    dt.Columns.Add(Column23)
                    dt.Columns.Add(Column24)
                    dt.Columns.Add(Column25)
                    dt.Columns.Add(Column26)
                    dt.Columns.Add(Column27)
                    dt.Columns.Add(Column28)
                    dt.Columns.Add(Column29)
                    dt.Columns.Add(Column30)
                    dt.Columns.Add(Column31)
                    dt.Columns.Add(Column32)
                    dt.Columns.Add(Column33)
                    dt.Columns.Add(Column34)
                    dt.Columns.Add(Column35)
                    dt.Columns.Add(Column36)
                    dt.Columns.Add(Column37)
                    dt.Columns.Add(Column38)
                    dt.Columns.Add(Column39)
                    dt.Columns.Add(Column40)
                    dt.Columns.Add(Column41)
                    dt.Columns.Add(Column42)

                    'Tabela para armazenar os diagnosticos encontrados na aplicação 
                Case "diagnostico"
                    Column1 = New System.Data.DataColumn("TypeObject", Type.GetType("System.Int16"))   'Handle da tubulação 
                    Column2 = New System.Data.DataColumn("Handle", Type.GetType("System.String"))  'Circuito
                    Column3 = New System.Data.DataColumn("Descricao", Type.GetType("System.String"))  'Circuito
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)

                    'Datatable com todos os handles e circuitos relacionados ao mesmo, no caso de tomadas e luminarias para fazer o ajustes de circuitos das tubulacoes 
                Case "handlecircuito"
                    Column1 = New System.Data.DataColumn("Handle", Type.GetType("System.String"))   'Handle da tubulação 
                    Column2 = New System.Data.DataColumn("Circuito", Type.GetType("System.String"))  'Circuito
                    Column3 = New System.Data.DataColumn("Retorno", Type.GetType("System.String"))  'Circuito
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)

                Case "handletub_circ"
                    Column1 = New System.Data.DataColumn("Handle", Type.GetType("System.String"))   'Handle da tubulação 
                    Column2 = New System.Data.DataColumn("Circuito", Type.GetType("System.String"))  'Circuito
                    Column3 = New System.Data.DataColumn("Campo1", Type.GetType("System.String")) 'Temporary field 1
                    Column4 = New System.Data.DataColumn("Campo2", Type.GetType("System.String")) 'Temporary field 2
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)

                    'For tree path interruptor - get all interruptor in dtmapeamento and create a datatable tree for interruptor 
                Case "treeinterruptor"
                    Column1 = New System.Data.DataColumn("Interruptor", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Chave", Type.GetType("System.String"))  'Path with handle interruptor and all handles of tub until luminiarias
                    Column3 = New System.Data.DataColumn("Campo1", Type.GetType("System.String")) 'Temporary field 1
                    Column4 = New System.Data.DataColumn("Campo2", Type.GetType("System.String")) 'Temporary field 2
                    Column5 = New System.Data.DataColumn("CircObj", Type.GetType("System.String"))
                    Column6 = New System.Data.DataColumn("Flag", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)
                    dt.Columns.Add(Column6)

                    'For order list os circuit auto - Get Handle and info circ, but return two lists, one with handles and another with list of circ ordered asc 
                Case "orderlistcircauto"
                    'Create a new datatable 
                    Column1 = New System.Data.DataColumn("Handle", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Circuito", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("Fase", Type.GetType("System.Int16"))
                    Column4 = New System.Data.DataColumn("Neutro", Type.GetType("System.Int16"))
                    Column5 = New System.Data.DataColumn("Terra", Type.GetType("System.Int16"))
                    Column6 = New System.Data.DataColumn("Retorno", Type.GetType("System.Int16"))
                    Column7 = New System.Data.DataColumn("Completo", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)
                    dt.Columns.Add(Column6)
                    dt.Columns.Add(Column7)

                    'Datatable para validar os caminhos de interruptores na geração do ciruito automatico, neste caso teremos o circuito e os handles desejados 
                Case "pathcircinterruptor"
                    'Create a new datatable 
                    Column1 = New System.Data.DataColumn("Circuito", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Handle", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("Interruptor", Type.GetType("System.String"))
                    Column4 = New System.Data.DataColumn("Retorno", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)

                    'This datatable is temp for order return in lisp, receive a list of handles and tratament for lisp before
                Case "order_retorno"
                    'Create a new datatable 
                    Column1 = New System.Data.DataColumn("Qdc", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Retorno", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)

                    'List of all circuit in selection (insertion auto)
                Case "listcircuit"
                    'Column1 = New System.Data.DataColumn("Flag", Type.GetType("System.String"))
                    Column1 = New System.Data.DataColumn("Circuito", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Fase", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("SecaoFase", Type.GetType("System.String"))
                    Column4 = New System.Data.DataColumn("Neutro", Type.GetType("System.String"))
                    Column5 = New System.Data.DataColumn("SecaoNeutro", Type.GetType("System.String"))
                    Column6 = New System.Data.DataColumn("Terra", Type.GetType("System.String"))
                    Column7 = New System.Data.DataColumn("SecaoTerra", Type.GetType("System.String"))
                    Column8 = New System.Data.DataColumn("CodEle", Type.GetType("System.String"))   'Condutor 
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)
                    dt.Columns.Add(Column6)
                    dt.Columns.Add(Column7)
                    dt.Columns.Add(Column8)

                Case "pathcad"
                    Column1 = New System.Data.DataColumn("PathTree", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)

                Case "handleselected"
                    'Create a new datatable 
                    Column1 = New System.Data.DataColumn("HandleTub", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Flag", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)

                    'Create a datatble for circuitauto - mapeamento 
                Case "mapeamento"
                    'Create a new datatable 
                    Column1 = New System.Data.DataColumn("Qdc", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Chave", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("HandleTub", Type.GetType("System.String"))
                    Column4 = New System.Data.DataColumn("HandleObj", Type.GetType("System.String"))
                    Column5 = New System.Data.DataColumn("CircObj", Type.GetType("System.String"))
                    Column6 = New System.Data.DataColumn("Flag", Type.GetType("System.String"))
                    Column7 = New System.Data.DataColumn("EhTomada", Type.GetType("System.String"))
                    Column8 = New System.Data.DataColumn("Luminaria", Type.GetType("System.String"))
                    Column9 = New System.Data.DataColumn("CircObj_Original", Type.GetType("System.String"))
                    Column10 = New System.Data.DataColumn("TamChave", System.Type.GetType("System.Int16"))
                    Column11 = New System.Data.DataColumn("Interruptor", System.Type.GetType("System.String"))
                    Column12 = New System.Data.DataColumn("EhUltimaTub", System.Type.GetType("System.String"))
                    Column13 = New System.Data.DataColumn("CircObj_Prox", System.Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)
                    dt.Columns.Add(Column6)
                    dt.Columns.Add(Column7)
                    dt.Columns.Add(Column8)
                    dt.Columns.Add(Column9)
                    dt.Columns.Add(Column10)
                    dt.Columns.Add(Column11)
                    dt.Columns.Add(Column12)
                    dt.Columns.Add(Column13)

                    'Create a datatble for circuitauto - mapeamento 
                Case "mapeamento_tub"
                    'Create a new datatable 
                    Column1 = New System.Data.DataColumn("HandleGroup", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("ObjectIdGroup", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("HandleTub", Type.GetType("System.String"))
                    Column4 = New System.Data.DataColumn("HandleObj1", Type.GetType("System.String"))
                    Column5 = New System.Data.DataColumn("HandleObj2", Type.GetType("System.String"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)

                Case "apau_printambiente"
                    Column1 = New System.Data.DataColumn("Handle", Type.GetType("System.String"))
                    Column2 = New System.Data.DataColumn("Circuito", Type.GetType("System.String"))
                    Column3 = New System.Data.DataColumn("Retorno", Type.GetType("System.String"))
                    Column4 = New System.Data.DataColumn("Tipo", Type.GetType("System.String"))
                    Column5 = New System.Data.DataColumn("Ambiente", Type.GetType("System.String"))
                    Column6 = New System.Data.DataColumn("TSaida", Type.GetType("System.String"))
                    Column7 = New System.Data.DataColumn("Campo1", Type.GetType("System.Int16"))
                    Column8 = New System.Data.DataColumn("Campo2", Type.GetType("System.Int16"))
                    Column9 = New System.Data.DataColumn("Campo3", Type.GetType("System.Object"))
                    dt.Columns.Add(Column1)
                    dt.Columns.Add(Column2)
                    dt.Columns.Add(Column3)
                    dt.Columns.Add(Column4)
                    dt.Columns.Add(Column5)
                    dt.Columns.Add(Column6)
                    dt.Columns.Add(Column7)
                    dt.Columns.Add(Column8)
                    dt.Columns.Add(Column9)

            End Select

            Return dt

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error in datatable info creating - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad006")
            Return Nothing
        End Try

    End Function

    <LispFunction("GetScreenZoom")> _
    Function GetScreenZoom(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim Rbf_End As ResultBuffer = Nothing
        Dim Doc As Autodesk.AutoCAD.ApplicationServices.Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        ScreenTactual = Doc.Editor.CurrentUserCoordinateSystem

        Return Rbf_End
    End Function

    <LispFunction("SetScreenZoom")> _
     Function SetScreenZoom(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim Rbf_End As ResultBuffer = Nothing
        Dim Doc As Autodesk.AutoCAD.ApplicationServices.Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Doc.Editor.CurrentUserCoordinateSystem = ScreenTactual

        Return Rbf_End
    End Function

    <CommandMethod("PB")> _
  Public Sub ProgressBarManaged()

        Dim pm As New ProgressMeter()
        pm.Start("Testing Progress Bar")
        pm.SetLimit(100)

        ' Now our lengthy operation
        For i As Integer = 0 To 100

            System.Threading.Thread.Sleep(5)
            ' Increment Progress Meter...
            pm.MeterProgress()
            ' This allows AutoCAD to repaint
            System.Windows.Forms.Application.DoEvents()
        Next

        pm.[Stop]()

    End Sub

    'Function for save a arraylist in .dat file ou txt file. 
    Public Shared Function SaveText_To_File(ByVal ArrayList_Object As Object, Optional ByVal FullPath As String = "", Optional ByVal ErrInfo As String = "", Optional ByVal CreateCab As Boolean = False) As Boolean

        Dim bAns As Boolean = False, NameApp As String = My.Settings.Application
        Dim objReader As StreamWriter, Count As Integer = 0
        Dim myEncoding As Encoding
        'myEncoding = System.Text.Encoding.GetEncoding(1252)
        myEncoding = System.Text.Encoding.UTF8

        Dim pm As New ProgressMeter()
        'Valida se 
        If NameApp = "" Then
            NameApp = "Processando. Aguarde..."
        Else
            NameApp = My.Settings.Application.ToString & " processando..."
        End If

        Try

            pm.Start(NameApp)
            pm.SetLimit(CType(ArrayList_Object, ArrayList).Count - 1)
            objReader = New StreamWriter(FullPath, False, myEncoding)

            With objReader

                For Each Obj As Object In ArrayList_Object
                    'Create a cab one time 
                    If CreateCab = True And Count < 1 Then
                        .Write(My.Settings.Cab_File & vbCrLf)
                        Count += 1
                    End If

                    .Write(Obj.ToString & vbCrLf)
                    'For progressbar autocad
                    'System.Threading.Thread.Sleep(5)
                    ' Increment Progress Meter...
                    pm.MeterProgress()
                    ' This allows AutoCAD to repaint
                    System.Windows.Forms.Application.DoEvents()

                Next

                .Close()
            End With

            bAns = True
            'Stop progress bar
            pm.[Stop]()

        Catch Ex As Exception
            ErrInfo = Ex.Message
        End Try

        Return bAns

    End Function

#End Region

#Region "----- Functions for references cad -----"

    'Return version language for Cad
    <LispFunction("ReturnVersionLanguage")> _
    Function ReturnVersionLanguage(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        Dim VersionLag As String = Aenge_GetCfg("Configuration", "Language", GetAppInstall() & My.Settings.FileIniApp)
        If VersionLag = "" Then VersionLag = "US" 'Seta o padrao em caso de vazio

        Dim ReturnRbf As ResultBuffer
        ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, NameVersionApp))
        Return ReturnRbf

    End Function

    'Return for lisp a variable of folder Apwr or Ahid 
    <LispFunction("ReturnVersionApp")> _
    Function ReturnVersionApp_Lisp(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        Dim ReturnRbf As ResultBuffer
        ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, NameVersionApp))
        Return ReturnRbf

    End Function

    'Return for lisp a variable of folder Apwr or Ahid 
    <LispFunction("ReturnFolderApp")> _
    Function ReturnFolderApplication_Lisp(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        Dim ReturnRbf As ResultBuffer
        ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, NameFolderApp))
        Return ReturnRbf

    End Function

    'Function for validate a path and files in lisp. Return a string 
    <LispFunction("ValidatePath_Lisp")> _
    Function ValidatePath_Lisp(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim ReturnRbf As ResultBuffer = Nothing, NameFile As String = Rbf.AsArray(1).Value.ToString
        Dim PathInstall As String, FullPathFile As String = "", FolderName As String = Rbf.AsArray(0).Value.ToString

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        PathInstall = Library_Reference.ReturnPathApplication

        Try

            If NameFile = "" And FolderName = "" Then
                'Return only path install
                FullPathFile = (PathInstall & My.Settings.Folder_Dwg)
            Else

                If FolderName <> "" Then
                    If NameFile <> "" Then
                        FullPathFile = (PathInstall & FolderName & "\" & NameFile)
                    Else
                        FullPathFile = (PathInstall & FolderName & "\")
                    End If
                Else
                    'Return only file path in fix
                    FullPathFile = (PathInstall & NameFile)
                End If

            End If

            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, FullPathFile))
            Return ReturnRbf

        Catch ex As Exception
            MsgBox("Ocorrência de erro : " & ex.Message, MsgBoxStyle.Information, "Aenge Error")
            Return Nothing
        End Try

    End Function

    Private Sub ActivatedAcadCommandEnd(ByVal sender As Object, ByVal e As CommandEventArgs)
        'MsgBox(e.GlobalCommandName)
    End Sub

    Function KillAllProcess() As Boolean

        Dim ListProcess As Object, Result As Boolean = True

        Try

            If My.Settings.NameProcess.ToString.Trim = "" Then Return True
            ListProcess = My.Settings.NameProcess.ToString.Split("|")

            For Each Obj As String In ListProcess
                With My.Computer.FileSystem
                    ' Kill all notepad process
                    'Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName(Obj)
                    Dim pProcess() As Process = System.Diagnostics.Process.GetProcesses
                    For Each p As Process In pProcess
                        If p.ProcessName.ToLower.Contains(Obj.ToLower) Then p.Kill()
                        Result = True
                    Next
                End With
            Next

            Return Result

        Catch ex As Exception
            Return False
        End Try

    End Function

    'Start events at close dwg. In this case, update all information about 
    Function ActivatedAcadClosed() As Object

        Try
            'Finaliza os processos de 64 bits 
            KillAllProcess()
            If IsProjectValid() = False Then Return False
            ExtractObjectsFromFile(Nothing)
            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    'Update list of treeview project
    Function ActivatedAcadCreated() As Object

        Return Nothing

        'For get all commands of autocad - For readqdcdata and others functions in end of command 
        'Dim dm As DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager()
        'AddHandler dm.MdiActiveDocument.CommandEnded, AddressOf ActivatedAcadCommandEnd

        'open the palette with project and drawings. If palette is not visible, then not load palette
        If Not MyPaletteObjectType Is Nothing Then
            If MyPaletteObjectType.Visible = True Then
                If ExistPaletteAutocad(NamePaletteProject) = True Then AddPaletteProject()
                If ExistPaletteAutocad(NamePalettePot) = True Then If Not _UsrC_Info Is Nothing Then _UsrC_Info.dgRegister.DataSource = Nothing
            End If
        End If
        'Validate all information in new project. If not exists circuit and qdc, then clear datagridview

        'Update info about treeview 
        ActivatedAcadDocument()
        Return True

    End Function

    'When the dwg is activate, system update info in cfg and others informations
    Function ActivatedAcadDocument() As Object

        If IsProjectValid() = False Then Return False

        Dim FullPath As String = GetAppInstall(), NameDwg As String
        'Now, set a cfg file default with info 
        Dim Doc As Autodesk.AutoCAD.ApplicationServices.Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Dim UserS1 As String = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("USERS1").ToString
        Dim IdTact As Int16 = 0

        If My.Computer.FileSystem.FileExists(Doc.Name) Then
            NameDwg = UCase(Dir(Doc.Name)).Replace(".DWG", "")
            Aenge_SetCfg("AppData", "AENGEDWG", NameDwg, FullPath & My.Settings.File_Autoenge)
            Aenge_SetCfg("AppData", "AENGEPROJ", UserS1, FullPath & My.Settings.File_Autoenge)
            'Consulta o ID no aenge.mdb 
            ValidateConnection()
            Dim Da As New OleDb.OleDbDataAdapter("Select * From AengeDef Where PrjNome = '" & UserS1 & "'", ConnAenge)
            Dim Dt As New System.Data.DataTable
            Da.Fill(Dt)
            If Dt.Rows.Count > 0 Then
                IdTact = Dt.Rows(0)("Id")
                Aenge_SetCfg("AppData", "AENGEPROJID", IdTact, FullPath & My.Settings.File_Autoenge)
            End If

            'Atualiza na tabela tactualProject o id corrent do desenho em qustao
            Dim Cmd As New OleDb.OleDbCommand
            With Cmd
                .Connection = ConnAhid
                .CommandType = CommandType.Text
                .CommandText = "Update TactualProject Set Id = " & IdTact
                .ExecuteNonQuery()
            End With
            Cmd = Nothing

        End If

        Return True

    End Function

    Function IsProjectValid() As Boolean
        Try
            If Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("USERS1").ToString = "" Then Return False
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Function DocumentInApp(ByVal NameDwg As String, ByVal ActiveDwg As Boolean) As Boolean

        Dim Result As Boolean = False

        For Each Doc As Document In Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager

            If Doc.Name.ToString.ToLower = NameDwg.ToLower Then
                If ActiveDwg = True Then Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument = Doc
                Return True
            End If

        Next

        Return Result
    End Function

    'Return for lisp all dwg in project tactual
    <LispFunction("returnalldwg_project")> _
    Public Function ReturnAllDwg_Project(ByVal RbF As ResultBuffer) As ResultBuffer

        Dim ArrayDwg As ArrayList = Nothing, StrDwg As String = ""
        Dim LibraryRegister As New LibraryRegister

        Try

            ArrayDwg = LibraryRegister.RetunAllDraw_Project("array")

            'After execute all functions, return to lisp a result
            Dim rbfResult As ResultBuffer
            'Return string 
            'rbfResult = New ResultBuffer(New TypedValue(LispDataType.Text, StrDwg))

            'Return list
            rbfResult = New ResultBuffer()
            With rbfResult
                .Add(New TypedValue(LispDataType.ListBegin))

                For Each DwgA As Object In ArrayDwg
                    .Add(New TypedValue(LispDataType.Text, DwgA.ToString))
                Next

                .Add(New TypedValue(LispDataType.ListEnd))
            End With

            Return rbfResult

        Catch ex As Exception
            Return Nothing
        Finally
            LibraryRegister = Nothing
        End Try

    End Function

    'Consulta qual a instância atual do autocad está ativa 
    'Function ReturnAcadReference(Optional ByVal CreateNewInstance As Boolean = True) As Object

    '    Dim mVar_ReferenceCad As Object

    '    Try

    '        mVar_ReferenceCad = CType(Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication, Autodesk.AutoCAD.Interop.AcadApplication)

    '        If mVar_ReferenceCad Is Nothing Then
    '            'Cria uma nova instancia do Autocad
    '            If CreateNewInstance = True Then
    '                mVar_ReferenceCad = New Autodesk.AutoCAD.Interop.AcadApplication
    '            Else
    '                Return Nothing
    '            End If

    '        End If

    '        Return mVar_ReferenceCad

    '    Catch ex As Exception
    '        Return Nothing
    '    End Try

    'End Function

#End Region

#Region "----- Function for insert objects and values -----"

    '####################################################################################################################################################
    '####################################################################################################################################################
    'Inserção de objetos atraves do .net e lisp
    '<LispFunction("inserirteste")> _
    'Function InsertObj(ByVal rbf As ResultBuffer) As ResultBuffer

    '    Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
    '    Dim Pto As AcadPoint
    '    Pto.Coordinate()

    '    Dim comando As String = "command " & Chr(34) & "INSERT" & Chr(34) & " " & Chr(34) & "C:\\Program Files\\Autopower 2012\\DWG\\Apwr_tealt031.dwg" & Chr(34) & " " & _
    '    "(rtos (* (GETVAR " & Chr(34) & "USERR2" & Chr(34) & ")(GETVAR " & Chr(34) & "USERR3" & Chr(34) & ")) 2) (rtos (* (GETVAR " & Chr(34) & "USERR2" & Chr(34) & ")(GETVAR " & Chr(34) & "USERR3" & Chr(34) & ")) 2) pause)"

    '    Doc.SendStringToExecute(comando, True, False, False)
    '
    'End Function

    'Function for getxdata application in object 
    Public Function GetXData(ByVal Id As ObjectId, ByVal adsAppName As String) As ResultBuffer

        Dim db As Database = HostApplicationServices.WorkingDatabase()
        Dim trans As Transaction
        trans = db.TransactionManager.StartTransaction

        Dim ed As Editor
        ed = Application.DocumentManager.MdiActiveDocument.Editor

        Try

            If IsDBNull(Id) Then
                Dim promptEntSel As New PromptEntityOptions("Selecione o objeto : ")
                Dim resultEntsel As PromptEntityResult
                resultEntsel = ed.GetEntity(promptEntSel)
            End If

            Dim ent As Entity, resBuf As ResultBuffer
            ent = trans.GetObject(Id, OpenMode.ForWrite)
            resBuf = ent.GetXDataForApplication(adsAppName)
            If resBuf Is Nothing Then Return Nothing

            'Dim iterator As IEnumerator
            'iterator = resBuf.GetEnumerator
            'Do While iterator.MoveNext
            '    Dim tmpVal As TypedValue
            '    tmpVal = CType(iterator.Current, TypedValue)
            '    ed.WriteMessage(tmpVal.Value.ToString)
            'Loop

            trans.Commit()
            Return resBuf

        Catch
            Return Nothing
        Finally
            trans.Dispose()
        End Try

    End Function

    'Function for setxdata information 
    Public Sub SetXData(ByVal Id As ObjectId, ByVal adsAppName As String, ByVal XdataEnt As ResultBuffer)

        Dim db As Database = HostApplicationServices.WorkingDatabase()
        Dim trans As Transaction, ed As Editor
        Dim ELock As DocumentLock

        trans = db.TransactionManager.StartTransaction
        ed = Application.DocumentManager.MdiActiveDocument.Editor

        ELock = ed.Document.LockDocument(DocumentLockMode.ProtectedAutoWrite, Nothing, Nothing, Nothing)

        Try

            Using ELock
                Using db
                    Dim ent As Entity

                    ent = trans.GetObject(Id, OpenMode.ForWrite)

                    Dim regTable As RegAppTable
                    regTable = trans.GetObject(db.RegAppTableId, OpenMode.ForWrite)
                    Dim regTableRec As New RegAppTableRecord()
                    regTableRec.Name = adsAppName
                    'First delete 

                    If regTable.Has(adsAppName) = False Then
                        Dim regAppRec As New RegAppTableRecord
                        regTable.UpgradeOpen()
                        regAppRec.Name = adsAppName
                        regTable.Add(regAppRec)
                        trans.AddNewlyCreatedDBObject(regAppRec, True)
                    Else
                        'regTable.Add(regTableRec)
                        'trans.AddNewlyCreatedDBObject(regTableRec, True)
                    End If

                    ent.XData = XdataEnt 'New ResultBuffer(New TypedValue(1001, "adsAppName"), New TypedValue(1000, "DevTech Services"))
                    trans.Commit()
                End Using
            End Using

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Erro XDT - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad007")
        Finally
            trans.Dispose()
        End Try

    End Sub

    <LispFunction("AtributeVisible")> _
    Function AtributeVisible(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim rbfResult As New ResultBuffer

        Try

            Dim HandleAtribute As String = Rbf.AsArray(0).Value
            Dim objAtt As Object = ObjectFromHandle(HandleAtribute)
            Dim ObjectIdAtt As Object = CType(objAtt, AttributeReference).ObjectId
            'Caso não tenha sido repassado o handle do atributo, retorna nothing a função
            If HandleAtribute = "" Then Return Nothing

            Dim activeDoc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
            Dim db As Database = activeDoc.Database
            Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

            Dim BT As BlockTable
            Dim blockSourceRec As AttributeReference 'BlockTableRecord
            'open blocktable for read

            Using tr As Transaction = ed.Document.Database.TransactionManager.StartTransaction()
                BT = DirectCast(tr.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)
                blockSourceRec = DirectCast(tr.GetObject(ObjectIdAtt, OpenMode.ForWrite), AttributeReference)
                'Faz o tratamento do atributo agora 
                blockSourceRec.Visible = False
                'Commit a transação 
                tr.Commit()
            End Using

            Return Nothing

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao validar as informações do atributo selecionado - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad004")
            Return Nothing
        End Try

    End Function

#End Region

#Region "----- Functions for help and chm -----"

    <CommandMethod("apwr_help", CommandFlags.Session)> _
    Public Sub Apwr_Help()
        CallHelp(Nothing)
    End Sub

    <CommandMethod("Ahid_help", CommandFlags.Session)> _
    Public Sub Ahid_Help()
        CallHelp(Nothing)
    End Sub

    <LispFunction("apwr_help")> _
    Function CallHelp(ByVal Rbf As ResultBuffer) As ResultBuffer
        Dim ArqHelp As String

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        ArqHelp = Library_Reference.ReturnPathApplication & "apwr.chm"
        If AengeFrameworkH.My.Computer.FileSystem.FileExists(ArqHelp) = True Then System.Windows.Forms.Help.ShowHelp(Nothing, ArqHelp)

        Return Nothing

    End Function
#End Region

#Region "----- Functions for Cfg and Files Application -----"

    'Set all values of autoenge.cfg and users in autocad
    <LispFunction("SetValueCfg_Apwr")> _
    Function SetValuesCFG_Apwr(ByVal RbfLisp As ResultBuffer) As ResultBuffer

        Dim Rbf As ResultBuffer = Nothing, FullPath As String = "", NameProj As String = "", NameDwg As String = ""
        Dim UniTactual As String, ScaleTactual As String, PathCfgDrawing As String, UniValue As Single
        Dim Doc As Autodesk.AutoCAD.ApplicationServices.Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument

        Try

            If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
            With Library_Reference
                PathCfgDrawing = .Return_Old_FileCfgDrawing
                FullPath = .ReturnPathApplication
                If mVar_NameProjTactual = "" Then
                    NameProj = .Return_TactualProject
                Else
                    NameProj = mVar_NameProjTactual
                End If
                UniTactual = .Return_FieldCfg("DRAWING", "UNIDADE_MEDIDA", PathCfgDrawing)
                ScaleTactual = .Return_FieldCfg("DRAWING", "ESCALA", PathCfgDrawing)

                Select Case LCase(UniTactual)
                    Case "m"
                        UniValue = 0.001

                    Case "cm"
                        UniValue = 0.1

                    Case "mm"
                        UniValue = 1

                    Case Else
                        UniValue = 0

                End Select

                NameDwg = UCase(Dir(Doc.Name)).Replace(".DWG", "")
                'Set all users
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS2", FullPath)
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS1", NameProj)
                If IsNumeric(ScaleTactual) Then Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERR3", Double.Parse(ScaleTactual))
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERR2", UniValue)
            End With

        Catch ex As Exception
            MsgBox("Ocorrência de erro : " & ex.Message & " - SetCfg File configuration", MsgBoxStyle.Information, "Error aenge")
            Return Nothing
        End Try

        Return Rbf
    End Function

#End Region

#Region "----- Palettes functions -----"

    'Return if a palette exists or not in autocad 
    Function ExistPaletteAutocad(ByVal NamePalette As String) As Boolean

        Dim IndexPalet As Integer = 0
        For Each Palet As Autodesk.AutoCAD.Windows.Palette In MyPaletteObjectType
            If Palet.Name = NamePalette Then Return True
        Next

        Return False
    End Function

    'Update a treeview with new objects
    Public Function UpdateListProject() As Boolean

        If MyPaletteObjectType Is Nothing Then Return False
        If _UsrC_Project Is Nothing Then Return False

        'Update info in palette - Only update a list with drawings and projects 
        With _UsrC_Project
            .FillTreeView_DrawProject(.trvDraw, "")
        End With
        System.Windows.Forms.Application.DoEvents()

        Return True

    End Function

    Public Sub AddPaletteTarefa()

        Dim CreatePalette As Boolean = False
        If DtAgendamento.Rows.Count <= 0 Then
            MsgBox("Não existe tarefas relacionadas ao projeto atual !", MsgBoxStyle.OkOnly, "Atenção")
            Exit Sub
        End If

        ' check to see if it is valid
        If (MyPaletteObjectType = Nothing) Then
            ' create a new palette set, with a unique guid - New Guid("CD4C4DFC-F34E-4e80-9FB5-8B63CF6D5979")
            MyPaletteObjectType = New Autodesk.AutoCAD.Windows.PaletteSet(NamePaletteAutopower, "", NewGuidePalette)
            ' now create a palette inside, this has our tree control
            If _UsrC_General Is Nothing Then
                CreatePalette = True
                _UsrC_General = New UsrCGeneral
            End If

            System.Windows.Forms.Application.DoEvents()
            'Update info in palette
            With _UsrC_General
                .Id_Form = "apwrsch"
                .FillTreeView_Agendamento(.trvInfo, "", , DtAgendamento)
                .Focus()
            End With
            System.Windows.Forms.Application.DoEvents()

            ' now add the palette to the paletteset
            With MyPaletteObjectType
                If CreatePalette = True Then .Add(NamePaletteAgenda, _UsrC_General)
                If .Dock = Autodesk.AutoCAD.Windows.DockSides.None Then .AutoRollUp = False
                .RolledUp = True
                .Visible = True
                .KeepFocus = True
                .WindowState = Autodesk.AutoCAD.Windows.Window.State.Normal
                If DirectCast(MyPaletteObjectType.Item(0), Autodesk.AutoCAD.Windows.Palette).Name.Contains(My.Settings.Text_PaletteAgenda) Then
                    .Activate(0)
                ElseIf .Count >= 2 Then
                    .Activate(1)
                End If
                '.Dock = Autodesk.AutoCAD.Windows.DockSides.Left
            End With

        Else

            If _UsrC_General Is Nothing Then
                CreatePalette = True
                _UsrC_General = New UsrCGeneral
            End If

            System.Windows.Forms.Application.DoEvents()
            'Update info in palette
            With _UsrC_General
                .Id_Form = "apwrsch"
                .FillTreeView_Agendamento(.trvInfo, "", , DtAgendamento)
                .Focus()
            End With
            System.Windows.Forms.Application.DoEvents()

            ' now add the palette to the paletteset
            With MyPaletteObjectType
                If CreatePalette = True Then .Add(NamePaletteAgenda, _UsrC_General)
                If .Dock = Autodesk.AutoCAD.Windows.DockSides.None Then .AutoRollUp = False
                .RolledUp = True
                .Visible = True
                .KeepFocus = True
                .WindowState = Autodesk.AutoCAD.Windows.Window.State.Normal
                If DirectCast(MyPaletteObjectType.Item(0), Autodesk.AutoCAD.Windows.Palette).Name.Contains(My.Settings.Text_PaletteAgenda) Then
                    .Activate(0)
                ElseIf .Count >= 2 Then
                    .Activate(1)
                End If

                '.Dock = Autodesk.AutoCAD.Windows.DockSides.Left
            End With

        End If

    End Sub

    <CommandMethod("Projetos", CommandFlags.Session)> _
    Public Sub AddPaletteProject()

        Dim CreatePalette As Boolean = False

        ' check to see if it is valid
        If (MyPaletteObjectType = Nothing) Then
            ' create a new palette set, with a unique guid - New Guid("CD4C4DFC-F34E-4e80-9FB5-8B63CF6D5979")
            MyPaletteObjectType = New Autodesk.AutoCAD.Windows.PaletteSet(NamePaletteAutopower, "", NewGuidePalette)
            ' now create a palette inside, this has our tree control
            If _UsrC_Project Is Nothing Then
                CreatePalette = True
                _UsrC_Project = New UsrC_Project(True)
            End If

            System.Windows.Forms.Application.DoEvents()
            'Update info in palette
            With _UsrC_Project
                .FillTreeView_DrawProject(.trvDraw, "")
                .Focus()
            End With
            System.Windows.Forms.Application.DoEvents()

            ' now add the palette to the paletteset
            With MyPaletteObjectType
                If CreatePalette = True Then .Add(NamePaletteProject, _UsrC_Project)
                '.DockEnabled = Autodesk.AutoCAD.Windows.DockSides.Left
                If Not .Dock = Autodesk.AutoCAD.Windows.DockSides.None Then .Dock = Autodesk.AutoCAD.Windows.DockSides.None '.AutoRollUp = False
                .KeepFocus = True
                .RolledUp = True
                .Visible = True
                .WindowState = Autodesk.AutoCAD.Windows.Window.State.Normal
                If DirectCast(MyPaletteObjectType.Item(0), Autodesk.AutoCAD.Windows.Palette).Name.Contains(My.Settings.Text_PaletteProject) Then
                    .Activate(0)
                ElseIf .Count >= 2 Then
                    .Activate(1)
                End If
                '.Dock = Autodesk.AutoCAD.Windows.DockSides.Left
            End With

        Else

            If _UsrC_Project Is Nothing Then
                CreatePalette = True
                _UsrC_Project = New UsrC_Project(True)
            End If

            System.Windows.Forms.Application.DoEvents()
            'Update info in palette
            With _UsrC_Project
                .FillTreeView_DrawProject(.trvDraw, "")
                .Focus()
                .BringToFront()
                .Visible = True
            End With
            System.Windows.Forms.Application.DoEvents()

            ' now add the palette to the paletteset
            With MyPaletteObjectType
                If CreatePalette = True Then .Add(NamePaletteProject, _UsrC_Project)
                '.DockEnabled = Autodesk.AutoCAD.Windows.DockSides.Left
                If Not .Dock = Autodesk.AutoCAD.Windows.DockSides.None Then .Dock = Autodesk.AutoCAD.Windows.DockSides.None '.AutoRollUp = False
                .KeepFocus = True
                .RolledUp = True
                .Visible = True
                .WindowState = Autodesk.AutoCAD.Windows.Window.State.Normal
                If DirectCast(MyPaletteObjectType.Item(0), Autodesk.AutoCAD.Windows.Palette).Name.Contains(My.Settings.Text_PaletteProject) Then
                    .Activate(0)
                ElseIf .Count >= 2 Then
                    .Activate(1)
                End If
                '.Dock = Autodesk.AutoCAD.Windows.DockSides.Left
            End With

        End If

    End Sub

    <CommandMethod("Quadros")> _
    Public Sub AddPaletteQdc(ByVal Dt As System.Data.DataTable)

        Dim CreatePalette As Boolean = False

        ' check to see if it is valid
        If (MyPaletteObjectType = Nothing) Then
            ' create a new palette set, with a unique guid - New Guid("CD4C4DFC-F34E-4e80-9FB5-8B63CF6D5979")
            MyPaletteObjectType = New Autodesk.AutoCAD.Windows.PaletteSet(NamePaletteAutopower, "", NewGuidePalette)
            ' now create a palette inside, this has our tree control
            If _UsrC_Info Is Nothing Then
                CreatePalette = True
                _UsrC_Info = New UsrC_Info("quadro")
            End If

            _UsrC_Info.DtRegister = Dt
            _UsrC_Info.FunctionQdc()

            ' now add the palette to the paletteset
            With MyPaletteObjectType
                If CreatePalette = True Then .Add(NamePalettePot, _UsrC_Info)
                .Visible = True
                .KeepFocus = True
            End With

        Else

            ' now create a palette inside, this has our tree control
            If _UsrC_Info Is Nothing Then
                CreatePalette = True
                _UsrC_Info = New UsrC_Info("quadro")
            End If

            _UsrC_Info.DtRegister = Dt
            _UsrC_Info.FunctionQdc()

            ' now add the palette to the paletteset
            With MyPaletteObjectType
                If CreatePalette = True Then .Add(NamePalettePot, _UsrC_Info)
                .Visible = True
                '.Activate(1)
                .KeepFocus = True
            End With

        End If

        Dim IndexPalet As Integer = 0
        For Each Palet As Autodesk.AutoCAD.Windows.Palette In MyPaletteObjectType
            If Palet.Name = NamePalettePot Then MyPaletteObjectType.Activate(IndexPalet)
            IndexPalet += 1
        Next

    End Sub

#End Region

#Region "----- Function for path and windows app - Version 5.0 or old -----"

    'Return PathApplication for versions 5.0 or old of autopower
    <LispFunction("Return_Old_FolderAutoenge")> _
    Function Return_Old_FolderAutoenge(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim PathTemp As String = Library_Reference.Return_Old_FolderAutoenge
        Dim ReturnRbf As ResultBuffer

        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, PathTemp))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try

        Return ReturnRbf

    End Function

    'Return a path of folder project 
    <LispFunction("Return_Old_FolderProject")> _
    Function Return_Old_FolderProject(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim StrPath As String = Library_Reference.Return_Old_FolderAutoenge()
        Dim ProjectTactual As String = Library_Reference.Return_Old_TactualProject()
        Dim ReturnRbf As ResultBuffer

        StrPath = StrPath & ProjectTactual & My.Settings.FolderApwr

        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, StrPath))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try

        Return ReturnRbf

    End Function

    'Return a path of folder project 
    <LispFunction("Return_Old_FolderCom")> _
    Function Return_Old_FolderCom(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim StrPath As String = Library_Reference.Return_Old_FolderAutoenge()
        Dim ReturnRbf As ResultBuffer

        StrPath = StrPath & My.Settings.FolderCom

        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, StrPath))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try

        Return ReturnRbf

    End Function

    'Return a path of cfg drawing 
    <LispFunction("Return_Old_FolderCfgDrawing")> _
    Function Return_Old_FolderCfgDrawing(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim StrPath As String = Library_Reference.Return_Old_FolderAutoenge()
        Dim ReturnRbf As ResultBuffer

        Dim ProjectTactual As String = Library_Reference.Return_Old_TactualProject()

        StrPath = StrPath & ProjectTactual & "\" & My.Settings.FolderApwr

        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, StrPath))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try

        Return ReturnRbf

    End Function

    'Return a path of file cfg drawing 
    <LispFunction("Return_Old_FileCfgDrawing")> _
    Function Return_Old_FileCfgDrawing(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim StrPath As String = Library_Reference.Return_Old_FolderAutoenge()
        Dim ProjectTactual As String = Library_Reference.Return_Old_TactualProject()
        Dim DrawingTactual As String = Library_Reference.Return_Old_TactualDrawing()
        Dim ReturnRbf As ResultBuffer

        StrPath = StrPath & ProjectTactual & "\" & My.Settings.FolderApwr & DrawingTactual & ".cfg"

        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, StrPath))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try

        Return ReturnRbf

    End Function

    'Return a path of file cfg drawing project 
    <LispFunction("Return_Old_FileCfgProject")> _
    Function Return_Old_FileCfgProject(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim StrPath As String = Library_Reference.Return_Old_FolderAutoenge()
        Dim ProjectTactual As String = Library_Reference.Return_Old_TactualProject()
        Dim DrawingTactual As String = Library_Reference.Return_Old_TactualDrawing()
        Dim ReturnRbf As ResultBuffer

        StrPath = StrPath & ProjectTactual & "\" & My.Settings.FolderApwr & My.Settings.File_ProjectCfg

        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, StrPath))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try

        Return ReturnRbf

    End Function

    'Functions for paths of application and folders Autopower - Autohidro
    <LispFunction("Return_Old_FileCfgAutoenge")> _
    Function Return_Old_FileCfgAutoenge(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim StrPath As String = Library_Reference.Return_Old_FolderAutoenge()
        Dim ReturnRbf As ResultBuffer

        StrPath = StrPath & My.Settings.File_Autoenge

        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, StrPath))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try

        Return ReturnRbf

    End Function

    'Functions for paths of application and folders Autopower - Autohidro
    <LispFunction("Return_Old_FileCfgProjectDefault")> _
    Function Return_Old_FileCfgProjectDefault(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim StrPath As String = Library_Reference.Return_Old_FolderAutoenge()
        Dim ReturnRbf As ResultBuffer

        StrPath = StrPath & My.Settings.FolderApwr & My.Settings.File_ProjectCfg

        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, StrPath))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try

        Return ReturnRbf

    End Function

    <LispFunction("Return_Old_TactualCApp")> _
    Function Return_Old_TactualCApp(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim TempReg As String = ""
        Dim ReturnRbf As ResultBuffer

        TempReg = Aenge_GetCfg("AppData", "AENGECAPP", Library_Reference.Return_Old_FolderAutoenge() & My.Settings.File_Autoenge)

        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, TempReg))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try
        Return ReturnRbf

    End Function

    <LispFunction("Return_Old_TactualProject")> _
    Function Return_Old_TactualProject(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim TempReg As String = ""
        Dim ReturnRbf As ResultBuffer

        TempReg = Aenge_GetCfg("AppData", "AENGEPROJ", Library_Reference.Return_Old_FolderAutoenge() & My.Settings.File_Autoenge)

        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, TempReg))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try

        Return ReturnRbf


    End Function

    <LispFunction("Return_Old_TactualDrawing")> _
    Function Return_Old_TactualDrawing(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim TempReg As String = ""
        Dim ReturnRbf As ResultBuffer

        TempReg = Aenge_GetCfg("AppData", "AENGEDWG", Library_Reference.Return_Old_FolderAutoenge() & My.Settings.File_Autoenge)
        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, TempReg))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try

        Return ReturnRbf

    End Function

    <LispFunction("Return_Old_TactualID")> _
    Function Return_Old_TactualID(Optional ByVal Rbf As ResultBuffer = Nothing) As ResultBuffer

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim TempReg As String = ""
        Dim ReturnRbf As ResultBuffer

        TempReg = Aenge_GetCfg("AppData", "AENGEPROJID", Library_Reference.Return_Old_FolderAutoenge() & My.Settings.File_Autoenge)
        Try
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, TempReg))
        Catch ex As Exception
            ReturnRbf = New ResultBuffer(New TypedValue(LispDataType.Text, ""))
        End Try

        Return ReturnRbf

    End Function

#End Region

#Region "----- Functions for Autopower and Autohidro - Lisp -----"

#Region "----- Indicação de legenda ----"

    'Função que procura todos os blocos com um determinado desenho para indicação de legenda - Procura depois em cada objeto as informações dentro do seu respectivo xdata 
    <LispFunction("GetMax_IndicaLeg")> _
    Function GetMax_IndicaLeg(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim RbfResult As New ResultBuffer, MaxNroLegenda As Int16 = 0
        Dim db As Database = Application.DocumentManager.MdiActiveDocument.Database
        Dim trans As Transaction = db.TransactionManager.StartTransaction()

        Try

            Dim BT As BlockTable
            Dim BTR As BlockTableRecord
            Dim editor As Autodesk.AutoCAD.EditorInput.Editor
            Dim blockSourceRec As BlockTableRecord
            Dim filterlist As TypedValue()
            Dim filter As Autodesk.AutoCAD.EditorInput.SelectionFilter
            Dim selRes As Autodesk.AutoCAD.EditorInput.PromptSelectionResult
            Dim oSS As Autodesk.AutoCAD.EditorInput.SelectionSet = Nothing

            db = HostApplicationServices.WorkingDatabase
            editor = Application.DocumentManager.MdiActiveDocument.Editor

            Using trans

                'open blocktable for read
                BT = DirectCast(trans.GetObject(db.BlockTableId, OpenMode.ForRead), BlockTable)

                'create a selectionset with filter
                filterlist = New Autodesk.AutoCAD.DatabaseServices.TypedValue(0) {}
                filterlist(0) = New Autodesk.AutoCAD.DatabaseServices.TypedValue(0, "INSERT")
                filter = New Autodesk.AutoCAD.EditorInput.SelectionFilter(filterlist)
                selRes = Application.DocumentManager.MdiActiveDocument.Editor.SelectAll(filter)

                If selRes.Status <> Autodesk.AutoCAD.EditorInput.PromptStatus.OK Then
                    'editor.WriteMessage(vbLf & "No block references in model space")
                    Return Nothing
                End If

                Try
                    blockSourceRec = DirectCast(trans.GetObject(BT("Apwr_IndicaLeg"), OpenMode.ForRead), BlockTableRecord)

                Catch ee As System.Exception
                    'editor.WriteMessage(ee.ToString())
                    'editor.WriteMessage(vbLf & "Block 'test' does not exist.")
                    'Return Nothing
                    'Caso não exista nenhuma numeração, retorna como 1
                    MaxNroLegenda = 1
                    GoTo encerra
                End Try

                'Iterate through the selectionset and replace the blockreferences.
                oSS = selRes.Value
                'Open ModelSpace for write
                BTR = DirectCast(trans.GetObject(BT(BlockTableRecord.ModelSpace), OpenMode.ForWrite), BlockTableRecord)
                For i As Integer = 0 To oSS.Count - 1
                    Dim oEnt As BlockReference

                    oEnt = DirectCast(trans.GetObject(oSS(i).ObjectId, OpenMode.ForWrite), BlockReference)
                    If oEnt.Name.ToString.ToLower = "Apwr_IndicaLeg".ToLower Then
                        If Not oEnt.GetXDataForApplication("AHID") Is Nothing Then

                            Dim XData As Object = oEnt.GetXDataForApplication("AHID")
                            'Neste caso, o array 4 é o de númerção de legenda 
                            If CType(XData, ResultBuffer).AsArray()(4).Value.ToString <> "" Then If MaxNroLegenda < CType(XData, ResultBuffer).AsArray()(4).Value Then MaxNroLegenda = CType(XData, ResultBuffer).AsArray()(4).Value

                        End If

                        'oEnt.BlockTableRecord = blockSourceRec.ObjectId
                        'If oEnt.GetXDataForApplication("AHID").AsArray.Count > 0 Then
                        '    Dim XData As Object = oEnt.GetXDataForApplication("AHID")
                        'End If
                    End If
                Next

                trans.Commit()
            End Using

            'retorna uma numeracao acima do ultimo numero de legenda encontrado
            MaxNroLegenda += 1
Encerra:
            Dim ResultMaior As String
            If MaxNroLegenda.ToString.Length = 1 Then
                ResultMaior = "0" & MaxNroLegenda.ToString
            Else
                ResultMaior = MaxNroLegenda.ToString
            End If

            'Retorna como string o maior número encontrado acrescentado de 1 
            With RbfResult
                .Add(New TypedValue(LispDataType.Text, ResultMaior))
            End With

            Return RbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao buscar dados de indicação de legenda.", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad005")
            Return Nothing
        End Try

    End Function

#End Region

#Region "----- Circuitos e Inserção de símbolos ----- "

    'Funcao que retorna uma lista oordenada com as descricoes dos layers cadastrados no banco de dados para o lisp criar a lista 
    <LispFunction("ReturnListLayer")> _
    Function ReturnListLayer(ByVal rbf As ResultBuffer) As ResultBuffer

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

        Dim RbfResult As New ResultBuffer
        Dim Da As New OleDb.OleDbDataAdapter("Select * From TObjLay Order By LayNome ASC", ConnAhid)
        Dim DtLay As New System.Data.DataTable
        Da.Fill(DtLay)

        For Each Dr As DataRow In DtLay.Rows
            With RbfResult
                .Add(New TypedValue(LispDataType.Text, Dr("LayNome").ToString))
            End With
        Next

        Return RbfResult

    End Function

    'Verifica se o desenho ja esta cadastrado como bloco no banco de dados 
    <LispFunction("ExistSymbolDwg")> _
    Function ValidateSymbolDwg(ByVal rbf As ResultBuffer) As ResultBuffer

        If rbf.AsArray.Count <= 0 Then Return Nothing
        Dim DwgName As String = rbf.AsArray(0).Value.ToString.Trim.ToLower.Replace("\\", "\").Replace("//", "/")  '.Replace(".DWG".ToLower, "")
        Dim RbfResult As New ResultBuffer, SysExist As Boolean = True
        Dim DtTemp As New System.Data.DataTable, DwgName_FileName As String = ""

        DwgName = DwgName.Replace("/", "\")
        With My.Computer.FileSystem
            If Not .FileExists(DwgName) Then
                MsgBox("O Bloco informado não se encontra no local especificado. Por favor verifique novamente !", MsgBoxStyle.Information, "Arquivo inválido")
                SysExist = False 'Neste caso falamos para o lisp que existe o bloco para ele travar o lancamento 
                GoTo finaliza
            End If

            Dim ArrayPath As Object = DwgName.Split("\")
            DwgName_FileName = ArrayPath(UBound(ArrayPath))
            'Verifica se o bloco existe na pasta Dwg 
            If .FileExists(GetAppInstall() & "DWG\" & DwgName_FileName) Then
                MsgBox("O Bloco informado já se encontra na pasta padrão de desenhos do aplicativo. Por favor renomeie antes o arquivo !", MsgBoxStyle.Information, "Arquivo existente")
                SysExist = False
                GoTo Finaliza
            End If
        End With

        'Valida a conexao com o banco de dados 
        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
        If ConnAhid Is Nothing Then
            With LibraryConnection
                .TypeDb = "AHID_"
                ConnAhid = .Aenge_OpenConnectionDB()
            End With
        End If

        DwgName_FileName = DwgName_FileName.ToUpper.Replace(".DWG".ToUpper, "")
        Dim Da As New OleDb.OleDbDataAdapter("Select * From TobjDwg Where DwgName = '" & DwgName_FileName & "'", ConnAhid)
        Da.Fill(DtTemp)
        If DtTemp.Rows.Count > 0 Then SysExist = False

Finaliza:
        With RbfResult
            If SysExist Then
                .Add(New TypedValue(LispDataType.Text, "T"))
            Else
                .Add(New TypedValue(LispDataType.Text, "F"))
            End If
        End With

        LibraryConnection = Nothing
        Da = Nothing : DtTemp = Nothing
        Return RbfResult

    End Function

    'Obtem a lista no lisp para insercao e atualizacao de simbolos 
    <LispFunction("InsertSymbol")> _
    Function InsertSymbol(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim ReturnRes As Object = Nothing
        'Caso não tenha sido passado nenhum parâmetro 
        If Rbf.AsArray.Count <= 0 Then Return Nothing
        Dim StrRbf As String = Rbf.AsArray(0).Value
        'Array e campos a serem atualizados no banco de dados 
        Dim ArrayStr As Object
        Dim CodObj As String, CodClas As String, ObjLeg As String, ClasDes As String, DesLayer As String = "", DwgName_FileName As String = ""
        'Tabela de visualizacao
        Dim CodView As Int16 = 1, DwgName As String
        Dim PathDwgBlock As String

        'Este primeiro Array separa os circuitos repassados pelo lisp, depois deveremos quebrar de acordo com o @ existente na string para retornar os dados da soma de fases 
        ArrayStr = StrRbf.Split("|")
        ClasDes = ArrayStr(0).ToString
        ObjLeg = ArrayStr(1).ToString
        DesLayer = ArrayStr(2).ToString
        DwgName = ArrayStr(3).ToString.ToUpper.Replace("\\", "\").Replace("//", "/")
        PathDwgBlock = DwgName

        Dim ArrayPath As Object = PathDwgBlock.Split("\")
        DwgName_FileName = ArrayPath(UBound(ArrayPath))

        'Antes de comecar as validacoes gerais de banco de dados, verificamos se o bloco informado existe no caminho passado, pois se nao existir ja nem daremos continuidade ao cadastro
        With My.Computer.FileSystem
            If Not .FileExists(PathDwgBlock) Then
                MsgBox("O Bloco informado não se encontra no local especificado. Por favor verifique novamente !", MsgBoxStyle.Information, "Arquivo inválido")
                GoTo finaliza
            Else
                'Faz a copia para a pasta Dwg do software
                .CopyFile(PathDwgBlock, GetAppInstall() & "DWG\" & DwgName_FileName, True)
            End If
        End With

        Dim Da As OleDb.OleDbDataAdapter, RbfResult As New ResultBuffer
        Dim DtClas As New System.Data.DataTable
        Dim Cmd As New OleDb.OleDbCommand
        'Valida a conexao com o banco de dados 
        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
        If ConnAhid Is Nothing Then
            With LibraryConnection
                .TypeDb = "AHID_"
                ConnAhid = .Aenge_OpenConnectionDB()
            End With
        End If

        With Cmd
            .Connection = ConnAhid
            .CommandType = CommandType.Text
        End With

        'Primeiro consultamos na tabela de classes 
        Dim StrSql_Class As String = "Select * From TObjClas Where ClasDes = '" & ClasDes & "'"
        Dim StrSql_Insert As String = ""
        Da = New OleDb.OleDbDataAdapter(StrSql_Class, ConnAhid)
        Da.Fill(DtClas)

        If DtClas.Rows.Count <= 0 Then
            MsgBox("A Classe informada para o objeto a ser inserido não é válida. Pode ter ocorrido alguma inconsistência na criação do objeto. Caso persista esta mensagem, por favor entre em contato com nosso departamento de suporte técnico !", _
                   MsgBoxStyle.Information, "Classe não encontrada")
            Return Nothing
        Else
            CodClas = DtClas.Rows(0)("CodClas")
        End If

        'Obtemos agora o codigo do objeto disponivel para o cadastro
        Dim StrSql_Base As String = "Select * From TObjBase Order By CodObj Desc"
        Dim DtBase As New System.Data.DataTable
        Da = Nothing : Da = New OleDb.OleDbDataAdapter(StrSql_Base, ConnAhid)
        Da.Fill(DtBase)

        Dim CodObjTemp As String = ""
        If DtBase.Rows.Count > 0 Then
            CodObj = Double.Parse(DtBase.Rows(0)("CodObj")) + 1
            CodObjTemp = CodObj
            'Formatamos a quantidade de caracteres 
            Do While CodObjTemp.Length < 6
                CodObjTemp = "0" & CodObjTemp
            Loop
        Else
            MsgBox("Ocorreu uma inconsistência no cadastro do objeto. Caso persista esta mensagem, entre em contato com nosso departamento de suporte técnico !", MsgBoxStyle.Information, "Objeto inválido")
            GoTo Finaliza
        End If

        'Obtem o codigo do layer relacionado 
        Dim CodLay As String = "0001", DtLay As New System.Data.DataTable
        Da = Nothing : Da = New OleDb.OleDbDataAdapter("Select * From TObjLay Where LayNome = '" & DesLayer & "'", ConnAhid)
        Da.Fill(DtLay)
        If DtLay.Rows.Count > 0 Then CodLay = DtLay.Rows(0)("CodLay").ToString
        DtLay = Nothing

        'Insere os dados na tabela de objetos - TOBJ
        StrSql_Insert = "Insert Into Tobj (CodObj) Values ('" & CodObjTemp & "')"
        With Cmd
            .CommandText = StrSql_Insert
            .ExecuteNonQuery()
            System.Windows.Forms.Application.DoEvents()
        End With

        'Insere os dados na tabela de objetos - tobjbase 
        StrSql_Insert = "Insert Into TobjBase (Id, CodObj, CodClas,ObjLeg, CodLay, ObjCor, ObjEsc, ObjLType) Values (0, '" & CodObjTemp & "','" & CodClas & "','" & ObjLeg & "','" & CodLay & "',256,'REAL','BYLAYER')"
        With Cmd
            .CommandText = StrSql_Insert
            .ExecuteNonQuery()
            System.Windows.Forms.Application.DoEvents()
        End With

        DwgName_FileName = DwgName_FileName.ToUpper.Replace(".DWG".ToUpper, "")
        'Por ultimo insere na tabela para visualizacao do Dwg
        StrSql_Insert = "Insert into TobjDwg (Id, CodObj, CodView, DwgName) Values (" & Id_Tactual & ", '" & CodObjTemp & "',1,'" & DwgName_FileName & "')"
        With Cmd
            .CommandText = StrSql_Insert
            .ExecuteNonQuery()
            System.Windows.Forms.Application.DoEvents()
        End With

        Cmd = Nothing
        'Get list of class return in my.config 
        With RbfResult
            .Add(New TypedValue(LispDataType.Text, "T"))
        End With

        LibraryConnection = Nothing
        Return RbfResult

finaliza:
        'Get list of class return in my.config 
        With RbfResult
            .Add(New TypedValue(LispDataType.Text, "F"))
        End With

        Return RbfResult

    End Function


    'Recebe do lisp uma string com as informações de circuitos com suas respectivas fases, neutros e retornos e retorna a soma de fases 
    <LispFunction("GetSum_FaseCircuito")> _
    Function GetSum_FaseCircuito(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim RbfResult As New ResultBuffer, ArrayStr As Object, SumFase As Int16 = 0

        Try

            'Caso não tenha sido passado nenhum parâmetro 
            If rbf.AsArray.Count <= 0 Then Return Nothing
            Dim StrRbf As String = rbf.AsArray(0).Value

            'Este primeiro Array separa os circuitos repassados pelo lisp, depois deveremos quebrar de acordo com o @ existente na string para retornar os dados da soma de fases 
            ArrayStr = StrRbf.Split("}")

            With RbfResult
                .Add(New TypedValue(LispDataType.ListBegin))

                For Each oBj As Object In ArrayStr
                    oBj = oBj.ToString.Replace("{", "").Replace("}", "")
                    SumFase = 0 'Zera para a nova soma 
                    'Após tratar cada um da lista, pega cada obj e quebra no @ para poder somar a quantidade existente de fases dentro da string 
                    If oBj.ToString <> "" Then

                        Dim ArrayInterno As Object = oBj.ToString.Split("@")
                        'é no segundo que possui as informações da fase desejada 
                        If Not ArrayInterno(1) Is Nothing Then
                            For Each ObjChar As Char In ArrayInterno(1).ToString
                                If IsNumeric(ObjChar) Then SumFase = SumFase + Int16.Parse(ObjChar)
                            Next
                        End If

                        'Adiciona no resultbuffer o resultado atual do registro repassado 
                        .Add(New TypedValue(LispDataType.Int16, SumFase))
                    End If

                Next

                .Add(New TypedValue(LispDataType.ListEnd))
            End With

            Return RbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao validar as informações de SomaF de circuitos in Code.", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad007")
            Return Nothing
        End Try

        Return RbfResult

    End Function

    'Get a higher number of return in list
    <LispFunction("ExistsNeutro")> _
    Function ExistsNeutro(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim RbfResult As New ResultBuffer, ResultNeutro As String = "T", ArrayString As New ArrayList
        Dim CircDefault As String = "", Count As Int16 = 0

        Try

            'Configure list os handles with properties 
            For Each Obj As Object In rbf.AsArray
                'For start and end of list 
                If DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5016 And DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5017 Then
                    'Get the 3 last numbers of return if exists
                    If Count = 0 Then
                        CircDefault = (DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString)
                        Count += 1
                    Else
                        'Validate if is circ_default is equals in list of circts.
                        If CircDefault <> "" Then
                            If Obj.Value.ToString.Split("@")(0) = CircDefault Then ArrayString.Add(DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString)
                        End If
                    End If
                End If
            Next

            'Validate now if exists neutro in array created
            For Each ObjArray As Object In ArrayString
                If IsNumeric(ObjArray.ToString.Split("@")(1).ToString.Split("N")(0).ToString.Split("F")(1).ToString) Then If Int16.Parse(ObjArray.ToString.Split("@")(1).ToString.Split("N")(0).ToString.Split("F")(1).ToString) > 0 Then ResultNeutro = "F" : Exit For
            Next

            'Get list of class return in my.config 
            With RbfResult
                .Add(New TypedValue(LispDataType.Text, ResultNeutro))
            End With

            Return RbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error getlist for class ExistNeutro - Lisp function - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad008")
            Return Nothing
        End Try

    End Function

    'Get a higher number of return in list
    <LispFunction("ExistsNeutro_Handle")> _
    Function ExistsNeutro_Handle(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim RbfResult As New ResultBuffer, ResultNeutro As String = "F", ArrayString As Object
        Dim CircDefault As String = "", HandleDefault As String = "", Count As Int16 = 0
        Dim ArrayHandle As Object, StringArrayHandle As String = "", StringArrayCirc As String = ""

        Try

            'Configure list os handles with properties 
            For Each Obj As Object In rbf.AsArray
                'For start and end of list 
                If DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5016 And DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5017 Then
                    'Get the 3 last numbers of return if exists
                    Select Case Count
                        Case 0
                            StringArrayHandle = (DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString)
                            Count += 1

                        Case 1
                            StringArrayCirc = (DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString)
                            Count += 1
                    End Select
                End If
            Next

            'Agora faz o tratamento relacionado aos dados de handle e objetos 
            Dim IndexCorrecao As Int16 = 2
            ArrayString = StringArrayCirc.Replace("{", "").Split("}")
            ArrayHandle = StringArrayHandle.Replace("{", "").Split("}")
            HandleDefault = ArrayHandle(0)
            CircDefault = ArrayHandle(1)
            Count = 0

            'Procura pelo mesmo registro de circuito
            For Each Obj_ArrayStringCirc As Object In ArrayString
                If Obj_ArrayStringCirc.ToString <> "" Then
                    'Neste caso encontrou um mesmo circuito 
                    If CircDefault = Obj_ArrayStringCirc.ToString.Split("@")(0).Trim Then
                        If IsNumeric(Obj_ArrayStringCirc.ToString.Split("@")(1).ToString.Split("N")(0).ToString.Split("F")(1).ToString) Then
                            If Int16.Parse(Obj_ArrayStringCirc.ToString.Split("@")(1).ToString.Split("N")(0).ToString.Split("F")(1).ToString) > 0 And ArrayHandle(Count + 2) = HandleDefault Then ResultNeutro = "T" : Exit For
                        End If
                    End If
                End If
                Count += 1
            Next

            'Get list of class return in my.config 
            With RbfResult
                .Add(New TypedValue(LispDataType.Text, ResultNeutro))
            End With

            Return RbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error getlist for class ExistNeutro with handle  - Lisp function - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad017")
            Return Nothing
        End Try

    End Function

#End Region

#Region "----- Localizador de retornos -----"

    'Localizador de retornos - Lê a lista com os handles repassados pela aplicação e retorna ordenado todos os retornos sem que estejam repetidos na lista 
    <LispFunction("GetList_RetornoCirc_Order")> _
    Function GetList_RetornoCirc_Order(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim RbfResult As New ResultBuffer
        Dim activeDoc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Dim db As Database = activeDoc.Database
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Dim ArrayString As New ArrayList, Count_TotalXDataItem As Object

        Try

            'Create a datatable for data info
            Dim Dt As System.Data.DataTable = CreateDataTable("order_retorno")

            'Configure list os handles with properties 
            For Each Obj As Object In rbf.AsArray
                'For start and end of list 
                If DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5016 And DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5017 Then ArrayString.Add(DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString)
            Next
            'Order the list of handles 
            ArrayString.Sort()

            Using tr As Transaction = ed.Document.Database.TransactionManager.StartTransaction()

                'Do for all objects (handles) order by return 
                For Each ObjHandle As String In ArrayString
                    Dim objCirc As Object = ObjectFromHandle(ObjHandle)
                    Dim ArrayXData As Object = Nothing

                    'Validate blockreference in search
                    If TypeOf objCirc Is BlockReference Then

                        If CType(objCirc, BlockReference).GetXDataForApplication("AHID").AsArray.Count > 0 Then
                            ArrayXData = CType(objCirc, BlockReference).GetXDataForApplication("AHID").AsArray
                            Count_TotalXDataItem = CType(objCirc, BlockReference).GetXDataForApplication("AHID").AsArray.Count
                        End If

                        'Now, set a new values of xdata 
                        Dim id As ObjectId
                        id = objCirc.ObjectId
                        'Dim ent As Entity = tr.GetObject(id, OpenMode.ForRead)

                        RbfResult = New ResultBuffer
                        Dim CountXData_Item As Integer = 1, ExistReturn As Boolean = True, J As Integer = 0, IndexXDataItem As Int16 = 0
                        'ReDim XDataType(Count_TotalXDataItem - 1)
                        'ReDim XDataValue(Count_TotalXDataItem - 1)

                        'Now, verify class for search - Index 1 is a class 
                        'datarow for fill datable with informations about qdc and returns  (1 - 7 ret)
                        Dim Dr As System.Data.DataRow = Nothing, Qdc As String, Ret1 As String = "", Ret2 As String = "", Ret3 As String = "", Ret4 As String = "", Ret5 As String = "", Ret6 As String = "", Ret7 As String = ""

                        'Qdc never change, its aways nro 3 
                        IndexXDataItem = 3 : Qdc = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value

                        Select Case ArrayXData(1).Value.ToString
                            Case "0006", "0005", "0015", "0019", "0122"
                                '1 return 
                                IndexXDataItem = 5 : Ret1 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value

                            Case "0016"
                                '2 return 
                                IndexXDataItem = 5 : Ret1 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                IndexXDataItem = 6 : Ret2 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value

                            Case "0017", "0120"
                                '3 return 
                                IndexXDataItem = 5 : Ret1 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                IndexXDataItem = 6 : Ret2 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                IndexXDataItem = 7 : Ret3 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value

                            Case "0018"
                                '4 return 
                                IndexXDataItem = 5 : Ret1 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                IndexXDataItem = 6 : Ret2 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                IndexXDataItem = 7 : Ret3 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                IndexXDataItem = 8 : Ret4 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value

                            Case "0121"
                                '6 return 
                                IndexXDataItem = 5 : Ret1 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                IndexXDataItem = 6 : Ret2 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                IndexXDataItem = 7 : Ret3 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                IndexXDataItem = 8 : Ret4 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                IndexXDataItem = 9 : Ret5 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value
                                IndexXDataItem = 10 : Ret6 = DirectCast(ArrayXData(IndexXDataItem), Autodesk.AutoCAD.DatabaseServices.TypedValue).Value

                            Case Else

                        End Select

                        'Its a first datarow in datatable
                        If Dt.Rows.Count <= 0 Then
                            If Ret1 <> "" Then Dt.Rows.Add(Qdc, Ret1)
                            If Ret2 <> "" Then Dt.Rows.Add(Qdc, Ret2)
                            If Ret3 <> "" Then Dt.Rows.Add(Qdc, Ret3)
                            If Ret4 <> "" Then Dt.Rows.Add(Qdc, Ret4)
                            If Ret5 <> "" Then Dt.Rows.Add(Qdc, Ret5)
                            If Ret6 <> "" Then Dt.Rows.Add(Qdc, Ret6)
                            If Ret7 <> "" Then Dt.Rows.Add(Qdc, Ret7)
                            'In this case, search for registry in datatable 
                        Else
                            'Search in datatable for the register. Validate all ret exists 
                            If Ret1 <> "" Then If Dt.Select("Qdc = '" & Qdc & "' AND Retorno = '" & Ret1 & "'").Length <= 0 Then Dt.Rows.Add(Qdc, Ret1)
                            If Ret2 <> "" Then If Dt.Select("Qdc = '" & Qdc & "' AND Retorno = '" & Ret2 & "'").Length <= 0 Then Dt.Rows.Add(Qdc, Ret2)
                            If Ret3 <> "" Then If Dt.Select("Qdc = '" & Qdc & "' AND Retorno = '" & Ret3 & "'").Length <= 0 Then Dt.Rows.Add(Qdc, Ret3)
                            If Ret4 <> "" Then If Dt.Select("Qdc = '" & Qdc & "' AND Retorno = '" & Ret4 & "'").Length <= 0 Then Dt.Rows.Add(Qdc, Ret4)
                            If Ret5 <> "" Then If Dt.Select("Qdc = '" & Qdc & "' AND Retorno = '" & Ret5 & "'").Length <= 0 Then Dt.Rows.Add(Qdc, Ret5)
                            If Ret6 <> "" Then If Dt.Select("Qdc = '" & Qdc & "' AND Retorno = '" & Ret6 & "'").Length <= 0 Then Dt.Rows.Add(Qdc, Ret6)
                            If Ret7 <> "" Then If Dt.Select("Qdc = '" & Qdc & "' AND Retorno = '" & Ret7 & "'").Length <= 0 Then Dt.Rows.Add(Qdc, Ret7)
                        End If

                    End If

                    'Add 1 to count 
                    'CountXData_Item += 1
                Next
                tr.Commit()

            End Using

            'Order datatable with a registers 
            Dim dView As New DataView(Dt), QdcTactual As String = ""
            dView.Sort = "Qdc ASC, Retorno ASC"

            'Now, using dview, pass to lisp all informations 
            'Get list of returns in datatable and create a result for lisp 
            With RbfResult
                'Create list of list wich all qdc find 
                For Each oBj As System.Data.DataRowView In dView
                    'If qdc is diferent, create a new list 
                    If QdcTactual <> oBj("Qdc").ToString Then
                        'If is not first register, then close list lisp 
                        If QdcTactual <> "" Then .Add(New TypedValue(LispDataType.ListEnd))
                        .Add(New TypedValue(LispDataType.ListBegin))
                        .Add(New TypedValue(LispDataType.Text, oBj("Qdc").ToString))
                        QdcTactual = oBj("Qdc")
                    End If
                    'Insert a return field in list lisp
                    .Add(New TypedValue(LispDataType.Text, oBj("Retorno").ToString))
                Next
                'Close the last list 
                .Add(New TypedValue(LispDataType.ListEnd))
            End With

            Return RbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error getlist for class return - Lisp function - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad005")
            Return Nothing
        End Try

    End Function

#End Region

#Region "----- Automação -----"

    'Get a higher number of return in list
    <LispFunction("GetUpper_Retorno")> _
    Function GetUpper_Retorno(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim RbfResult As New ResultBuffer, HigherNumber As Int16 = 0, ArrayString As New ArrayList, ObjTemp As String

        Try

            'Configure list os handles with properties 
            For Each Obj As Object In rbf.AsArray
                'For start and end of list 
                If DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5016 And DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5017 Then
                    'Get the 3 last numbers of return if exists
                    ObjTemp = (DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString)
                    'Now, for each obj, get a higher value for return 
                    If ObjTemp.Length >= 3 Then If IsNumeric(Mid(ObjTemp, ObjTemp.Length - 2, 3)) Then If Int16.Parse(Mid(ObjTemp, ObjTemp.Length - 2, 3)) > HigherNumber Then HigherNumber = Int16.Parse(Mid(ObjTemp, ObjTemp.Length - 2, 3))
                    If ObjTemp.Length >= 2 Then If IsNumeric(Mid(ObjTemp, ObjTemp.Length - 1, 2)) Then If Int16.Parse(Mid(ObjTemp, ObjTemp.Length - 1, 2)) > HigherNumber Then HigherNumber = Int16.Parse(Mid(ObjTemp, ObjTemp.Length - 1, 2))
                    If ObjTemp.Length >= 1 Then If IsNumeric(Mid(ObjTemp, ObjTemp.Length - 0, 1)) Then If Int16.Parse(Mid(ObjTemp, ObjTemp.Length - 0, 1)) > HigherNumber Then HigherNumber = Int16.Parse(Mid(ObjTemp, ObjTemp.Length - 0, 1))
                End If
            Next

            'Get list of class return in my.config 
            With RbfResult
                .Add(New TypedValue(LispDataType.Text, HigherNumber.ToString))
            End With

            Return RbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error getlist for class return - Lisp function - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad005")
            Return Nothing
        End Try

    End Function

#End Region

#Region "----- General - App and Functions -----"

    'Validate a userr2 and userr3 values for lisp
    <LispFunction("ValidateR2")> _
    Public Function ValidateR2(ByVal rBf As ResultBuffer) As ResultBuffer

        'Atualiza as Userr1,2 e 3
        Dim PathCfgDraw As String, UnidadeCfg As String, EscalaCfg As String, ValorUnidade As Single
        Dim ProjTactual As String, DwgTactual As String
        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference

        Try

            ProjTactual = Library_Reference.Return_TactualProject  'ReturnCurrent_Project
            DwgTactual = Library_Reference.Return_TactualDrawing  'ReturnCurrent_Dwg
            PathCfgDraw = Library_Reference.ReturnPathApplication & ProjTactual & "\" & DwgTactual & ".cfg"
            UnidadeCfg = Aenge_GetCfg("DRAWING", "UNIDADE_MEDIDA", PathCfgDraw)
            EscalaCfg = Aenge_GetCfg("DRAWING", "ESCALA", PathCfgDraw)

            Select Case LCase(UnidadeCfg)
                Case "m"
                    ValorUnidade = 0.001

                Case "cm"
                    ValorUnidade = 0.1

                Case "mm"
                    ValorUnidade = 1

                Case Else
                    ValorUnidade = 0.001

            End Select

            'Atualiza as references para o cad
            '.Preferences.System.SingleDocumentMode = False
            'Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS1", ProjTactual)
            'Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS2", GetAppInstall.Replace("\", "\\"))
            'Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS3", "1000")
            'If IsNumeric(EscalaCfg) Then Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERR3", CDbl(EscalaCfg))
            'If IsNumeric(ValorUnidade) Then Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERR2", CDbl(ValorUnidade))

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error ValidateR2 - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad012")
        End Try

        Return Nothing

    End Function

#End Region

#Region "----- Circuitos automaticos -----"

    'Get all groups in dwg for reading....
    <CommandMethod("getGroupIds")> _
    Public Sub getGroupIds() ' input the group name we want to list 

        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor

        'Dim groupName As PromptResult = ed.GetString("Enter Group name to list : ")
        ' get the working database 
        Dim db As Database = Autodesk.AutoCAD.DatabaseServices.HostApplicationServices.WorkingDatabase
        ' start a transaction 

        Dim trans As Transaction = db.TransactionManager.StartTransaction()
        ' now try the read 

        'Clear tub datatable 
        DtTub = Nothing
        DtTub = CreateDataTable("mapeamento_tub")

        Try ' get the named objects dictionary 
            Dim nod As DBDictionary = trans.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForRead) ' now get the ACAD_GROUP dictionary entry, this contains all of the Groups defined in the drawing 
            Dim acadGroup As DBDictionary = trans.GetObject(nod("ACAD_GROUP"), OpenMode.ForRead) ' next, find the group name that was entered above 
            Dim groupRequired As Group
            'Dim entityIds As ObjectId() = groupRequired.GetAllEntityIds()
            'Dim id As ObjectId

            Dim ArrayGroup As New ArrayList

            For Each testeobj As Object In acadGroup

                groupRequired = trans.GetObject(acadGroup(DirectCast(testeobj, Autodesk.AutoCAD.DatabaseServices.DBDictionaryEntry).Key.ToString), OpenMode.ForRead) ' we now have the group required, lets find out what's inside 
                ArrayGroup.Add(groupRequired.Name.ToString)

                Dim ArrayNanic As Object = groupRequired.GetXDataForApplication("AHID")
                Dim ArrayTeste As Object = DirectCast(testeobj, Autodesk.AutoCAD.DatabaseServices.DBDictionaryEntry)

                'In this case, have a xdata information 
                If Not ArrayNanic Is Nothing Then

                    Dim DrTub As System.Data.DataRow
                    DrTub = DtTub.NewRow
                    DrTub("HandleGroup") = groupRequired.Handle.ToString
                    DrTub("ObjectIdGroup") = groupRequired.ObjectId.ToString
                    DrTub("HandleTub") = ""

                    For Each Obj As Object In DirectCast(ArrayNanic, Autodesk.AutoCAD.DatabaseServices.ResultBuffer).AsArray
                        If DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode = 1005 Then
                            If DrTub("HandleObj1") Is DBNull.Value Then DrTub("HandleObj1") = DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString : GoTo Prox
                            If DrTub("HandleObj2") Is DBNull.Value Then DrTub("HandleObj2") = DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString
prox:
                        End If
                    Next
                    DtTub.Rows.Add(DrTub)
                End If
            Next

            'For Each id In entityIds ' open the entity for read 
            '    Dim ent As Entity = trans.GetObject(id, OpenMode.ForRead) ' create the highlight path 
            '    Dim path As FullSubentityPath = New FullSubentityPath(New ObjectId(0) {id}, New SubentityId(SubentityType.Null, 0))
            '    ' now highlight it 
            '    ent.Highlight(path, True)
            'Next
            trans.Commit()

        Catch ex As Exception
            MsgBox(ex.Message)
            trans.Dispose()
        Finally
        End Try
    End Sub

#End Region

#Region "----- Version Autopower / Autohidro -----"

    <CommandMethod("VerAp")> _
    Sub VerAp()
        Dim Doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Doc.SendStringToExecute("(VerApL) ", False, False, False)
    End Sub

    <LispFunction("VerApL")> _
    Function VerApL(ByVal rbF As ResultBuffer) As ResultBuffer

        Dim rbFResult As New ResultBuffer, VerApCfg As String = ""

        If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
        Dim Diretorio As String = Library_Reference.ReturnPathApplication

        VerApCfg = Aenge_GetCfg("Configuration", "VerAp", Diretorio & "IniApp.ini")
        'Get list of class return in my.config 
        With rbFResult
            .Add(New TypedValue(LispDataType.Text, VerApCfg.ToString))
        End With

        Library_Reference = Nothing
        Return rbFResult

    End Function

#End Region

#End Region

#Region "----- Homologation ------"

    '<LispFunction("Vinic")> _
    'Function Vinic(ByVal rbf As ResultBuffer) As ResultBuffer
    '    Dim frm As New Form1
    '    frm.ShowDialog()
    'End Function

#End Region

#Region "----- Functions for Autohidro 2017 Version -----"

#Region "----- Functions for insert objetcs -----"

    'Função que retorna a seleção de um determinado ponto no Dwg, para depois o usuario poder fazer todas as inserções necessárias
    'Aqui apenas selecionamos um ponto no DWG, no caso o de inserção 
    Public Function SelectPointDWG(MsgScreen As Boolean) As Object

        Try

            'Aqui apenas selecionamos, se nao for valido, retorna como nothing 
            Dim Ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
            Dim opPt As New PromptPointOptions("Selecione um ponto válido:")
            Dim resPt As PromptPointResult
            resPt = Ed.GetPoint(opPt)

            If resPt.Status <> PromptStatus.OK Then
                If MsgScreen Then
                    MsgBox("Não foi selecionado um ponto válido!", MsgBoxStyle.Information, "Ponto inválido")
                Else
                    Ed.WriteMessage("Não foi selecionado um ponto válido!")
                End If

                Return Nothing
            End If

            Dim ptInsert As Point3d
            ptInsert = resPt.Value
            Return ptInsert

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Restrição ao carregar as informações para traçados de sprinklers - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad026")
            Return Nothing
        End Try

    End Function

    'Faz a inserção de um determinado bloco no desenho, verificamos se nao possui algum parecido 
    'Aqui inserimos o bloco de acordo com o parametro a ser trabalhado. nao teremos rotação no mesmo, apenas para inserir no desenho e podermos trabalharmos com o ele depois no lançamento 
    'de arrays, ou outras funcionalidades. Retornamos o handle do objeto que acabou de ser inserido 
    Public Function InsertBlk(PtoInsert As Point3d) As Object

        Dim db As Database
        db = HostApplicationServices.WorkingDatabase()
        Dim ed As Editor, ObjIdInserted As ObjectId
        ed = Application.DocumentManager.MdiActiveDocument.Editor
        Dim trans As Transaction
        trans = db.TransactionManager.StartTransaction

        Try

            'Fatores de escala a serem tratados antes 
            '###################################################################################
            Dim Userr2 As Double = 1, Userr3 As Double = 1, FatorEscala As Double = -1
            If IsNumeric(Application.GetSystemVariable("USERR2")) Then Userr2 = Application.GetSystemVariable("USERR2")
            If IsNumeric(Application.GetSystemVariable("USERR3")) Then Userr3 = Application.GetSystemVariable("USERR3")
            'Faz a validação de escala para a inserção dos
            FatorEscala = Userr2 * Userr3
            'Se no caso nao tiver sido setado alguma das variaveis entao iremos padronizar como 0,05 o tamanho 
            If FatorEscala = 0 Then FatorEscala = 0.05
            'Caso nenhuma validação passe e tenha valor como -1 na variavel, setamos o padrao do sistema
            If FatorEscala = -1 Then FatorEscala = 0.05

            'Inserção do bloco a ser trabalhado
            '###################################################################################
            Dim bt As BlockTable = trans.GetObject(db.BlockTableId, OpenMode.ForRead)
            Dim btr As BlockTableRecord
            btr = trans.GetObject(bt.Item(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
            Dim id As ObjectId

            If bt.Has("test2") Then
                Dim btrSrc As BlockTableRecord
                btrSrc = trans.GetObject(bt.Item("test2"), OpenMode.ForRead)
                id = btrSrc.Id
            Else
                Dim dbDwg As New Database(False, True)
                dbDwg.ReadDwgFile("C:\\TEST2.dwg", IO.FileShare.Read, True, "")
                id = db.Insert("test", dbDwg, True)
                dbDwg.Dispose()
            End If

            If PtoInsert.ToString = "" Then
                Dim opPt As New PromptPointOptions("Selecione o ponto de inserção:")
                Dim resPt As PromptPointResult
                resPt = ed.GetPoint(opPt)
                If resPt.Status <> PromptStatus.OK Then
                    MsgBox("Falha ao obter o ponto de inserção...")
                    Return Nothing
                End If
                'Retornamos para a mesma variavel a selecao do ponto a ser selecionado 
                PtoInsert = resPt.Value
            End If

            Dim blkRef As New BlockReference(PtoInsert, id)
            Dim mat As Matrix3d = Matrix3d.Identity
            blkRef.TransformBy(mat)
            blkRef.ScaleFactors = New Scale3d(FatorEscala, FatorEscala, FatorEscala)

            btr.AppendEntity(blkRef)
            trans.AddNewlyCreatedDBObject(blkRef, True)
            trans.Commit()

            ObjIdInserted = blkRef.ObjectId
            Return ObjIdInserted

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Restrição na leitura das informações do objeto informado (blkFail) - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad027")
            Return Nothing
        Finally
            trans.Dispose()
        End Try

    End Function

#End Region


#Region "----- Sprinklers -----"

    'Controle de ponto de inserção dos objetos 
    Public Shared Function PolarPoints(ByVal pPt As Point2d, _
                                       ByVal dAng As Double, _
                                       ByVal dDist As Double)

        Return New Point2d(pPt.X + dDist * Math.Cos(dAng), _
                           pPt.Y + dDist * Math.Sin(dAng))
    End Function

    'Funcionalidades de lançamento de sprinklers. Aqui iremos receber um array da função, que será passada pelo formulário, sendo possível assim
    'fazer o lançamento dos dados. Para isso, teremos os seguintes parametros: 
    '0) Aqui temos qual o tipo de retorno para gerar, se é de sprinklers ou de tub   0 / Sprinklers --> 1 / Tub
    '1) Distancia X
    '2) Distancia Y
    '3) Area de cobertura
    '4) Numero de sprinklers
    '5) Espaçamento entre sprinklers
    '6) Diametro das colunas / Ramais 
    Function SprinkDrawing(ListParameters As Object) As Object

        Try

            '' Primeiro, obtemos o ponto de inserção dos sprinklers
            Dim PtoInsert As Object = SelectPointDWG(False)
            Dim DistY As Double = 0, DistX As Double = 0, Area As Double = 0, NroSprin As Integer = 0, EspSprin As Double = 0
            If ListParameters = "" Then
                MsgBox("Não será possível inserir os sprinklers informados. Verifique se todas as informações foram digitadas corretamente...", MsgBoxStyle.Information, "Informações inválidas")
                Return False
            Else
                '' Validamos as informaçoes repassadas pelo form de sprinklers
                If ListParameters.ToString.Split("|").Count > 0 Then
                    If ListParameters.ToString.Split("|").Count >= 2 Then DistY = ListParameters.ToString.Split("|")(1)
                    If ListParameters.ToString.Split("|").Count >= 3 Then DistX = ListParameters.ToString.Split("|")(2)
                    If ListParameters.ToString.Split("|").Count >= 4 Then Area = ListParameters.ToString.Split("|")(3)
                    If ListParameters.ToString.Split("|").Count >= 5 Then NroSprin = ListParameters.ToString.Split("|")(4)
                    If ListParameters.ToString.Split("|").Count >= 6 Then EspSprin = ListParameters.ToString.Split("|")(5)
                End If
            End If

            '' Get the current document and database
            Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
            Dim acCurDb As Database = acDoc.Database
            '' Start a transaction
            Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()

                ''############################################################################################
                ''Antes de fazer a distribuição, iremos ver se existe o bloco no desenho a ser aproveitado, senao iremos exportar o mesmo
                ''iremos fazer a inserção do bloco a ser utilizado
                ''Aqui iremos repassar o bloco selecionado a ser distribuido
                Dim ObjIdISample As ObjectId = InsertBlk(PtoInsert)

                '############################################################################################
                '' Open the Block table record for read
                '' Pegamos o bloco que será trabalhado nesta inserção
                Dim acBlkTbl As BlockTable
                acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead)
                '' Open the Block table record Model space for write
                Dim acBlkTblRec As BlockTableRecord
                acBlkTblRec = acTrans.GetObject(acBlkTbl(BlockTableRecord.ModelSpace), OpenMode.ForWrite)
                Dim blockInserted As BlockReference = acTrans.GetObject(ObjIdISample, OpenMode.ForRead)

                '' Create a rectangular array with 5 rows and 5 columns
                Dim nRows As Integer = 5
                Dim nColumns As Integer = 5

                '' Set the row and column offsets along with the base array angle
                Dim dRowOffset As Double = 10   'Anterior era 1
                Dim dColumnOffset As Double = 1
                Dim dArrayAng As Double = 0

                '' Get the angle from X for the current UCS 
                Dim curUCSMatrix As Matrix3d = acDoc.Editor.CurrentUserCoordinateSystem
                Dim curUCS As CoordinateSystem3d = curUCSMatrix.CoordinateSystem3d
                Dim acVec2dAng As Vector2d = New Vector2d(curUCS.Xaxis.X, curUCS.Xaxis.Y)

                '' If the UCS is rotated, adjust the array angle accordingly
                dArrayAng = dArrayAng + acVec2dAng.Angle

                '' Use the upper-left corner of the objects extents for the array base point
                Dim acExts As Extents3d = blockInserted.Bounds.GetValueOrDefault()
                Dim acPt2dArrayBase As Point2d = New Point2d(acExts.MinPoint.X, acExts.MaxPoint.Y)

                '' Track the objects created for each column
                Dim acDBObjCollCols As DBObjectCollection = New DBObjectCollection()
                acDBObjCollCols.Add(blockInserted)

                '' Create the number of objects for the first column
                Dim nColumnsCount As Integer = 1
                While (nColumns > nColumnsCount)
                    Dim acEntClone As Entity = blockInserted.Clone()
                    acDBObjCollCols.Add(acEntClone)

                    '' Caclucate the new point for the copied object (move)
                    Dim acPt2dTo As Point2d = PolarPoints(acPt2dArrayBase, dArrayAng, dColumnOffset * nColumnsCount)

                    Dim acVec2d As Vector2d = acPt2dArrayBase.GetVectorTo(acPt2dTo)
                    Dim acVec3d As Vector3d = New Vector3d(acVec2d.X, acVec2d.Y, 0)
                    acEntClone.TransformBy(Matrix3d.Displacement(acVec3d))

                    acBlkTblRec.AppendEntity(acEntClone)
                    acTrans.AddNewlyCreatedDBObject(acEntClone, True)

                    nColumnsCount = nColumnsCount + 1
                End While

                '' Set a value in radians for 90 degrees
                Dim dAng As Double = Math.PI / 2

                '' Track the objects created for each row and column
                Dim acDBObjCollLvls As DBObjectCollection = New DBObjectCollection()

                For Each acObj As DBObject In acDBObjCollCols
                    acDBObjCollLvls.Add(acObj)
                Next

                '' Create the number of objects for each row
                For Each acEnt As Entity In acDBObjCollCols
                    Dim nRowsCount As Integer = 1

                    While (nRows > nRowsCount)
                        Dim acEntClone As Entity = acEnt.Clone()
                        acDBObjCollLvls.Add(acEntClone)

                        '' Caclucate the new point for the copied object (move)
                        Dim acPt2dTo As Point2d = PolarPoints(acPt2dArrayBase, dArrayAng + dAng, dRowOffset * nRowsCount)

                        Dim acVec2d As Vector2d = acPt2dArrayBase.GetVectorTo(acPt2dTo)
                        Dim acVec3d As Vector3d = New Vector3d(acVec2d.X, acVec2d.Y, 0)
                        acEntClone.TransformBy(Matrix3d.Displacement(acVec3d))

                        acBlkTblRec.AppendEntity(acEntClone)
                        acTrans.AddNewlyCreatedDBObject(acEntClone, True)

                        nRowsCount = nRowsCount + 1
                    End While
                Next

                '' Save the new objects to the database
                acTrans.Commit()
            End Using

            Return True

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Restrição ao carregar as informações para traçados de sprinklers - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad026")
            Return False
        End Try

    End Function

    'Function SprinkDrawing_APAGAR(ListParameters As Object) As Object

    '    Try

    '        'Primeiro, obtemos o ponto de inserção dos sprinklers
    '        Dim PtoInsert As Object = SelectPointDWG(False)

    '        '' Get the current document and database
    '        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
    '        Dim acCurDb As Database = acDoc.Database

    '        '' Start a transaction
    '        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()

    '            'Antes de fazer a distribuição, iremos ver se existe o bloco no desenho a ser aproveitado, senao iremos exportar o mesmo
    '            'iremos fazer a inserção do bloco a ser utilizado
    '            'Aqui iremos repassar o bloco selecionado a ser distribuido
    '            Dim ObjIdISample As ObjectId = InsertBlk(PtoInsert)

    '            '' Open the Block table record for read
    '            Dim acBlkTbl As BlockTable
    '            acBlkTbl = acTrans.GetObject(acCurDb.BlockTableId, OpenMode.ForRead)

    '            '' Open the Block table record Model space for write
    '            Dim acBlkTblRec As BlockTableRecord
    '            acBlkTblRec = acTrans.GetObject(acBlkTbl(BlockTableRecord.ModelSpace), OpenMode.ForWrite)

    '            '' Create a circle that is at 2,2 with a radius of 0.5
    '            Using acCirc As Circle = New Circle()
    '                acCirc.Center = New Point3d(2, 2, 0)
    '                acCirc.Radius = 0.5

    '                '' Add the new object to the block table record and the transaction
    '                acBlkTblRec.AppendEntity(acCirc)
    '                acTrans.AddNewlyCreatedDBObject(acCirc, True)

    '                '' Create a rectangular array with 5 rows and 5 columns
    '                Dim nRows As Integer = 5
    '                Dim nColumns As Integer = 5

    '                '' Set the row and column offsets along with the base array angle
    '                Dim dRowOffset As Double = 10   'Anterior era 1
    '                Dim dColumnOffset As Double = 1
    '                Dim dArrayAng As Double = 0

    '                '' Get the angle from X for the current UCS 
    '                Dim curUCSMatrix As Matrix3d = acDoc.Editor.CurrentUserCoordinateSystem
    '                Dim curUCS As CoordinateSystem3d = curUCSMatrix.CoordinateSystem3d
    '                Dim acVec2dAng As Vector2d = New Vector2d(curUCS.Xaxis.X, curUCS.Xaxis.Y)

    '                '' If the UCS is rotated, adjust the array angle accordingly
    '                dArrayAng = dArrayAng + acVec2dAng.Angle

    '                '' Use the upper-left corner of the objects extents for the array base point
    '                Dim acExts As Extents3d = acCirc.Bounds.GetValueOrDefault()
    '                Dim acPt2dArrayBase As Point2d = New Point2d(acExts.MinPoint.X, acExts.MaxPoint.Y)

    '                '' Track the objects created for each column
    '                Dim acDBObjCollCols As DBObjectCollection = New DBObjectCollection()
    '                acDBObjCollCols.Add(acCirc)

    '                '' Create the number of objects for the first column
    '                Dim nColumnsCount As Integer = 1
    '                While (nColumns > nColumnsCount)
    '                    Dim acEntClone As Entity = acCirc.Clone()
    '                    acDBObjCollCols.Add(acEntClone)

    '                    '' Caclucate the new point for the copied object (move)
    '                    Dim acPt2dTo As Point2d = PolarPoints(acPt2dArrayBase, dArrayAng, dColumnOffset * nColumnsCount)

    '                    Dim acVec2d As Vector2d = acPt2dArrayBase.GetVectorTo(acPt2dTo)
    '                    Dim acVec3d As Vector3d = New Vector3d(acVec2d.X, acVec2d.Y, 0)
    '                    acEntClone.TransformBy(Matrix3d.Displacement(acVec3d))

    '                    acBlkTblRec.AppendEntity(acEntClone)
    '                    acTrans.AddNewlyCreatedDBObject(acEntClone, True)

    '                    nColumnsCount = nColumnsCount + 1
    '                End While

    '                '' Set a value in radians for 90 degrees
    '                Dim dAng As Double = Math.PI / 2

    '                '' Track the objects created for each row and column
    '                Dim acDBObjCollLvls As DBObjectCollection = New DBObjectCollection()

    '                For Each acObj As DBObject In acDBObjCollCols
    '                    acDBObjCollLvls.Add(acObj)
    '                Next

    '                '' Create the number of objects for each row
    '                For Each acEnt As Entity In acDBObjCollCols
    '                    Dim nRowsCount As Integer = 1

    '                    While (nRows > nRowsCount)
    '                        Dim acEntClone As Entity = acEnt.Clone()
    '                        acDBObjCollLvls.Add(acEntClone)

    '                        '' Caclucate the new point for the copied object (move)
    '                        Dim acPt2dTo As Point2d = PolarPoints(acPt2dArrayBase, dArrayAng + dAng, dRowOffset * nRowsCount)

    '                        Dim acVec2d As Vector2d = acPt2dArrayBase.GetVectorTo(acPt2dTo)
    '                        Dim acVec3d As Vector3d = New Vector3d(acVec2d.X, acVec2d.Y, 0)
    '                        acEntClone.TransformBy(Matrix3d.Displacement(acVec3d))

    '                        acBlkTblRec.AppendEntity(acEntClone)
    '                        acTrans.AddNewlyCreatedDBObject(acEntClone, True)

    '                        nRowsCount = nRowsCount + 1
    '                    End While
    '                Next
    '            End Using

    '            '' Save the new objects to the database
    '            acTrans.Commit()
    '        End Using

    '        Return True

    '    Catch ex As Exception
    '        LibraryError.CreateErrorAenge(ex, "Restrição ao carregar as informações para traçados de sprinklers - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad026")
    '        Return False
    '    End Try

    'End Function

#End Region

#End Region

End Class

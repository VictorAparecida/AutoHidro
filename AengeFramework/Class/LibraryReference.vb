Imports Autodesk.AutoCAD.DatabaseServices
Imports System.IO

Public Class LibraryReference

#Region "----- Documentação -----"

    '==============================================================================================================================================
    'Projeto : FrameWork 2008 - Autocad
    'Empresa : Autoenge Brasil Ltda
    'Reponsável criação : Raul Antonio Fernandes Junior    
    'Data de criação : 04/03/2009
    'Objetivo : Classe responsável pela manipulação das informações e funcionalidades das desenhos do autocad.
    '
    'Informações adicionais :
    'LIBREF001 - ReturnAcadReferenceNet
    'LIBREF002 - 
    'LIBREF003 - 
    'LIBREF004 -
    '==============================================================================================================================================

#End Region

#Region "----- Atributos e Declarações -----"

    'Aplicação do autocad a ser instanciada
    'Private Shared mVar_ReferenceCad As Autodesk.AutoCAD.Interop.AcadApplication = Nothing
    Private Shared mVar_NameClass As String = "LibraryReference", mVar_NameApplication As String = My.Application.Info.ProductName

#End Region

#Region "----- Parâmetros - Get e Set -----"

    'Obtem a referência da instancia do autocad a ser utilizada
    'Property ReferenciaAutocad() As Autodesk.AutoCAD.Interop.AcadApplication
    '    Get
    '        ReferenciaAutocad = mVar_ReferenceCad
    '    End Get
    '    Set(ByVal value As Autodesk.AutoCAD.Interop.AcadApplication)
    '        mVar_ReferenceCad = value
    '    End Set
    'End Property

#End Region

#Region "----- Function for Cad references -----"

    'Get a instance of Cad activate
    'Function ReturnAcadReference(Optional ByVal CreateNewInstance As Boolean = True) As Object

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

#Region "----- Function for Path and windows app -----"

    'Set all values in cfg with default parametrs. Receive a type of file update (dwg, project and others), cab and list of parametrs 
    Function SetCfg_DataFull(ByVal TypeFile As String, ByVal Cab As String, ByVal Lst As ResultBuffer) As ResultBuffer

        Dim RbfEnd As New ResultBuffer, PathCfg As String = "", Rbf_Value As String = "", Rbf_Key As String = ""
        Try

            'Get all informations in cfg of dwg, project ou system - This is path of file 
            Select Case TypeFile.ToLower
                Case "dwg"
                    PathCfg = ReturnPathApplication() & Return_TactualProject() & "\" & Return_TactualDrawing() & ".cfg"

                    'for all dwg in projects 
                Case "projeto"
                    PathCfg = ReturnPathApplication() & Return_TactualProject() & "\Projeto.cfg"

                    'in this case, is for all new projects and drawings created
                Case "sistema"
                    PathCfg = ReturnPathApplication() & "Projeto.cfg"

            End Select

            'Now, for all objects in list. Update info 
            Dim IndexPA As Integer = 4, IndexIni As Integer = 4, Count As Integer = 4
            Do While Count + 1 <= Lst.AsArray.Count

                Rbf_Key = "" : Rbf_Value = ""
                If Not Lst.AsArray(Count).Value Is Nothing Then Rbf_Key = Lst.AsArray(Count).Value.ToString
                If Not Lst.AsArray(Count + 1).Value Is Nothing Then Rbf_Value = Lst.AsArray(Count + 1).Value.ToString

                Aenge_SetCfg(Cab, Rbf_Key, Rbf_Value, PathCfg)
                Count += IndexPA
            Loop

            Return RbfEnd

        Catch ex As Exception
            MsgBox("Ocorrência de erro : " & ex.Message, MsgBoxStyle.Information, "Aenge Error SetCData")
            Return Nothing
        End Try

    End Function

    'Set all values in cfg with default parametrs. Receive a type of file update (dwg, project and others), cab and list of parametrs 
    Function SetCfg_Old_DataFull(ByVal TypeFile As String, ByVal Cab As String, ByVal Lst As ResultBuffer) As ResultBuffer

        Dim RbfEnd As New ResultBuffer, PathCfg As String = "", Rbf_Value As String = "", Rbf_Key As String = ""
        Try

            'Get all informations in cfg of dwg, project ou system - This is path of file 
            Select Case TypeFile.ToLower
                Case "dwg"
                    PathCfg = Return_Old_FolderAutoenge() & Return_Old_TactualProject() & "\" & My.Settings.FolderApwr & Return_Old_TactualDrawing() & ".cfg"

                    'for all dwg in projects 
                Case "projeto"
                    PathCfg = Return_Old_FolderAutoenge() & Return_Old_TactualProject() & "\" & My.Settings.FolderApwr & "Projeto.cfg"

                    'in this case, is for all new projects and drawings created
                Case "sistema"
                    PathCfg = Return_Old_FolderAutoenge() & My.Settings.FolderApwr & "Projeto.cfg"

                Case Else
                    PathCfg = Return_Old_FolderAutoenge() & Return_Old_TactualProject() & "\" & My.Settings.FolderApwr & Return_Old_TactualDrawing() & ".cfg"

            End Select

            'Now, for all objects in list. Update info 
            Dim IndexPA As Integer = 4, IndexIni As Integer = 4, Count As Integer = 4
            Do While Count + 1 <= Lst.AsArray.Count

                Rbf_Key = "" : Rbf_Value = ""
                If Not Lst.AsArray(Count).Value Is Nothing Then Rbf_Key = Lst.AsArray(Count).Value.ToString
                If Not Lst.AsArray(Count + 1).Value Is Nothing Then Rbf_Value = Lst.AsArray(Count + 1).Value.ToString

                Aenge_SetCfg(Cab, Rbf_Key, Rbf_Value, PathCfg)
                Count += IndexPA
            Loop

            Return RbfEnd

        Catch ex As Exception
            MsgBox("Ocorrência de erro : " & ex.Message, MsgBoxStyle.Information, "Aenge Error SetCData")
            Return Nothing
        End Try

    End Function

    'Return a field of cfg - generic function 
    Function Return_FieldCfg(ByVal Cab As String, ByVal Body As String, Optional ByVal PathCfg As String = "Autoenge") As String

        Dim TempReg As String = "", PathCfgTemp As String = ""
        If PathCfg = "Autoenge" Then PathCfgTemp = ReturnPathApplication() & My.Settings.File_Autoenge

        TempReg = Aenge_GetCfg(Cab, Body, PathCfg)
        Return TempReg

    End Function

    'Return all fields of file cfg
    Function Return_AllFieldCfg_Cab(ByVal AppFile As String, ByVal CabFile As String) As Object

        Dim LineIn As String = "", ArrayInfo As New ArrayList, I As Integer = 0
        'Dim oRead As System.IO.StreamReader
        Dim FindCab As Boolean = False
        Dim RbfRet As New Autodesk.AutoCAD.DatabaseServices.ResultBuffer
        Dim PathCfg As String = ""

        'Get all informations in cfg of dwg, project ou system
        Select Case AppFile.ToLower
            Case "dwg"
                PathCfg = ReturnPathApplication() & Return_TactualProject() & "\" & Return_TactualDrawing() & ".cfg"

                'for all dwg in projects 
            Case "projeto"
                PathCfg = ReturnPathApplication() & Return_TactualProject() & "\Projeto.cfg"

                'in this case, is for all new projects and drawings created
            Case "sistema"
                PathCfg = ReturnPathApplication() & "Projeto.cfg"

            Case Else
                PathCfg = ReturnPathApplication() & Return_TactualProject() & "\" & Return_TactualDrawing() & ".cfg"

        End Select

        'Set the path of file 
        AppFile = PathCfg

        If My.Computer.FileSystem.FileExists(AppFile) Then

            'oRead = IO.File.OpenText(AppFile)
            Dim objReader As StreamReader = New StreamReader(AppFile, System.Text.Encoding.Default)
            While objReader.Peek <> -1
                'While oRead.Peek <> -1
                'LineIn = oRead.ReadLine()
                LineIn = objReader.ReadLine
                'If find the cab
                If LineIn.Trim = "[" & CabFile & "]" Then FindCab = True

                If FindCab = True Then
                    If I <> 0 Then
                        If Mid(LineIn, 1, 1) = "[" Then
                            FindCab = False
                            GoTo EndRead
                        End If
                        ArrayInfo.Add(LineIn.Split("=")(0) & "³" & (Mid(LineIn, LineIn.Split("=")(0).Length + 2, LineIn.Length - (LineIn.Split("=")(0).Length))))
                    End If
                    I += 1
                End If
            End While

EndRead:
            'oRead.Close()
            objReader.Close()

            With RbfRet

                If ArrayInfo.Count <= 0 Then
                    'when not exists registers
                    .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListBegin))
                    .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListEnd))
                Else
                    For Each objStr As String In ArrayInfo
                        .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListBegin))
                        .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.Text, objStr.ToString.Split("³")(0)))
                        .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.Text, objStr.ToString.Split("³")(1)))
                        .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListEnd))
                    Next
                End If

            End With

        Else
            MsgBox("File not exists in projetc. Please contact support", MsgBoxStyle.Information, "AengeError")
        End If

        Return RbfRet

    End Function

    'Return all fields of file cfg - In this case, return all information in 2009 version of software
    Function Return_Old_AllFieldCfg_Cab(ByVal AppFile As String, ByVal CabFile As String) As Object

        Dim LineIn As String = "", ArrayInfo As New ArrayList, I As Integer = 0
        Dim oRead As System.IO.StreamReader, FindCab As Boolean = False
        Dim RbfRet As New Autodesk.AutoCAD.DatabaseServices.ResultBuffer
        Dim PathCfg As String = ""

        'Get all informations in cfg of dwg, project ou system
        Select Case AppFile.ToLower
            Case "dwg"
                PathCfg = Return_Old_FolderAutoenge() & Return_Old_TactualProject() & "\" & My.Settings.FolderApwr & Return_Old_TactualDrawing() & ".cfg"

                'for all dwg in projects 
            Case "projeto"
                PathCfg = Return_Old_FolderAutoenge() & Return_Old_TactualProject() & "\" & My.Settings.FolderApwr & "Projeto.cfg"

                'in this case, is for all new projects and drawings created
            Case "sistema"
                PathCfg = Return_Old_FolderAutoenge() & My.Settings.FolderApwr & "Projeto.cfg"

            Case Else
                PathCfg = Return_Old_FolderAutoenge() & Return_Old_TactualProject() & "\" & My.Settings.FolderApwr & Return_Old_TactualDrawing() & ".cfg"

        End Select

        'Set the path of file 
        AppFile = PathCfg

        If My.Computer.FileSystem.FileExists(AppFile) Then

            oRead = IO.File.OpenText(AppFile)

            While oRead.Peek <> -1
                LineIn = oRead.ReadLine()

                'If find the cab
                If LineIn.Trim = "[" & CabFile & "]" Then FindCab = True

                If FindCab = True Then
                    If I <> 0 Then
                        If Mid(LineIn, 1, 1) = "[" Then
                            FindCab = False
                            GoTo EndRead
                        End If

                        ArrayInfo.Add(LineIn.Split("=")(0) & "³" & (Mid(LineIn, LineIn.Split("=")(0).Length + 2, LineIn.Length - (LineIn.Split("=")(0).Length))))
                    End If

                    I += 1
                End If
            End While

EndRead:
            oRead.Close()

            With RbfRet

                If ArrayInfo.Count <= 0 Then
                    'when not exists registers
                    .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListBegin))
                    .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListEnd))
                Else
                    For Each objStr As String In ArrayInfo
                        .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListBegin))
                        .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.Text, objStr.ToString.Split("³")(0)))
                        .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.Text, objStr.ToString.Split("³")(1)))
                        .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListEnd))
                    Next
                End If

            End With

        Else
            MsgBox("File not exists in projetc. Please contact support", MsgBoxStyle.Information, "AengeError")
        End If

        Return RbfRet

    End Function

    'Return all information about Lengeds in list function 
    Function Return_FieldCfg_Legend(ByVal typeFile As String, ByVal typeSearch As String) As ResultBuffer

        Dim TempReg As String = "", CabInsercao As String = "INSERCAO DE LEGENDA", ArrayInfo(29) As String
        Dim PathCfg As String = ""

        'Get all informations in cfg of dwg, project ou system
        Select Case typeFile
            Case "dwg"
                PathCfg = ReturnPathApplication() & Return_TactualProject() & "\" & Return_TactualDrawing() & ".cfg"

                'for all dwg in projects 
            Case "projeto"
                PathCfg = ReturnPathApplication() & Return_TactualProject() & "\Projeto.cfg"

                'in this case, is for all new projects and drawings created
            Case "sistema"
                PathCfg = ReturnPathApplication() & "Projeto.cfg"

        End Select

        Return_AllFieldCfg_Cab(PathCfg, CabInsercao)

        ArrayInfo(0) = "LAYER" & "|" & Aenge_GetCfg(CabInsercao, "LAYER", PathCfg)
        ArrayInfo(1) = "COR" & "|" & Aenge_GetCfg(CabInsercao, "COR", PathCfg)
        ArrayInfo(2) = "TIPOLINHA" & "|" & Aenge_GetCfg(CabInsercao, "TIPOLINHA", PathCfg)
        ArrayInfo(3) = "TIPOLETRA" & "|" & Aenge_GetCfg(CabInsercao, "TIPOLETRA", PathCfg)
        ArrayInfo(4) = "TITULO" & "|" & Aenge_GetCfg(CabInsercao, "TITULO", PathCfg)
        ArrayInfo(5) = "OBS1" & "|" & Aenge_GetCfg(CabInsercao, "OBS1", PathCfg)
        ArrayInfo(6) = "OBS2" & "|" & Aenge_GetCfg(CabInsercao, "OBS2", PathCfg)
        ArrayInfo(7) = "OBS3" & "|" & Aenge_GetCfg(CabInsercao, "OBS3", PathCfg)
        ArrayInfo(8) = "OBS4" & "|" & Aenge_GetCfg(CabInsercao, "OBS4", PathCfg)
        ArrayInfo(9) = "OBS5" & "|" & Aenge_GetCfg(CabInsercao, "OBS5", PathCfg)
        ArrayInfo(10) = "OBS6" & "|" & Aenge_GetCfg(CabInsercao, "OBS6", PathCfg)
        ArrayInfo(11) = "OBS7" & "|" & Aenge_GetCfg(CabInsercao, "OBS7", PathCfg)
        ArrayInfo(12) = "OBS8" & "|" & Aenge_GetCfg(CabInsercao, "OBS8", PathCfg)
        ArrayInfo(14) = "SELECAO" & "|" & Aenge_GetCfg(CabInsercao, "SELECAO", PathCfg)
        ArrayInfo(15) = "INSTUB" & "|" & Aenge_GetCfg(CabInsercao, "INSTUB", PathCfg)
        ArrayInfo(16) = "INSOUTTUB" & "|" & Aenge_GetCfg(CabInsercao, "INSOUTTUB", PathCfg)
        ArrayInfo(17) = "COR1" & "|" & Aenge_GetCfg(CabInsercao, "COR1", PathCfg)
        ArrayInfo(18) = "COR2" & "|" & Aenge_GetCfg(CabInsercao, "COR2", PathCfg)
        ArrayInfo(19) = "COR3" & "|" & Aenge_GetCfg(CabInsercao, "COR3", PathCfg)
        ArrayInfo(20) = "LIN1" & "|" & Aenge_GetCfg(CabInsercao, "LIN1", PathCfg)
        ArrayInfo(21) = "LIN2" & "|" & Aenge_GetCfg(CabInsercao, "LIN2", PathCfg)
        ArrayInfo(22) = "LIN3" & "|" & Aenge_GetCfg(CabInsercao, "LIN3", PathCfg)
        ArrayInfo(23) = "DESC1" & "|" & Aenge_GetCfg(CabInsercao, "DESC1", PathCfg)
        ArrayInfo(24) = "DESC2" & "|" & Aenge_GetCfg(CabInsercao, "DESC2", PathCfg)
        ArrayInfo(25) = "DESC3" & "|" & Aenge_GetCfg(CabInsercao, "DESC3", PathCfg)
        ArrayInfo(26) = "TODOS" & "|" & Aenge_GetCfg(CabInsercao, "TODOS", PathCfg)
        ArrayInfo(27) = "TOMADA" & "|" & Aenge_GetCfg(CabInsercao, "TOMADA", PathCfg)
        ArrayInfo(28) = "HIDRAULICA" & "|" & Aenge_GetCfg(CabInsercao, "HIDRAULICA", PathCfg)
        ArrayInfo(29) = "TELEFONE" & "|" & Aenge_GetCfg(CabInsercao, "TELEFONE", PathCfg)
        ArrayInfo(13) = "LISLAYER" & "|" & Aenge_GetCfg(CabInsercao, "LISLAYER", PathCfg)

        Dim RbfRet As New Autodesk.AutoCAD.DatabaseServices.ResultBuffer
        With RbfRet
            For Each objStr As String In ArrayInfo
                .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListBegin))
                .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.Text, objStr.ToString.Split("|")(0)))
                .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.Text, objStr.ToString.Split("|")(1)))
                .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListEnd))
            Next
        End With

        Return RbfRet

    End Function

    'For list material - only default, not security and cabling
    Function Return_FieldCfg_LMat(Optional ByVal typeFile As String = "dwg") As ResultBuffer

        Dim TempReg As String = "", CabInsercao As String = "QUANTITATIVO DE MATERIAIS", ArrayInfo(26) As String
        Dim PathCfg As String = ""

        'Get all informations in cfg of dwg, project ou system
        Select Case typeFile
            Case "dwg"
                PathCfg = ReturnPathApplication() & Return_TactualProject() & "\" & Return_TactualDrawing() & ".cfg"

        End Select

        ArrayInfo(0) = "TIPO_QUANTITATIVO" & "|" & Aenge_GetCfg(CabInsercao, "TIPO_QUANTITATIVO", PathCfg)
        ArrayInfo(1) = "COEF_SIMBOLOS" & "|" & Aenge_GetCfg(CabInsercao, "COEF_SIMBOLOS", PathCfg)
        ArrayInfo(2) = "COEF_CONDUTOS" & "|" & Aenge_GetCfg(CabInsercao, "COEF_CONDUTOS", PathCfg)
        ArrayInfo(3) = "COEF_CONDUTORES" & "|" & Aenge_GetCfg(CabInsercao, "COEF_CONDUTORES", PathCfg)
        ArrayInfo(4) = "COEF_ACESSORIOS" & "|" & Aenge_GetCfg(CabInsercao, "COEF_ACESSORIOS", PathCfg)
        ArrayInfo(5) = "COEF_DISPOSITIVOS" & "|" & Aenge_GetCfg(CabInsercao, "COEF_DISPOSITIVOS", PathCfg)
        ArrayInfo(6) = "OBRAVOLARE" & "|" & Aenge_GetCfg(CabInsercao, "OBRAVOLARE", PathCfg)
        ArrayInfo(7) = "TITULO" & "|" & Aenge_GetCfg(CabInsercao, "TITULO", PathCfg)
        ArrayInfo(8) = "ORDEM_DE_IMPRESSAO_COMP" & "|" & Aenge_GetCfg(CabInsercao, "ORDEM_DE_IMPRESSAO_COMP", PathCfg)
        ArrayInfo(9) = "ORDEM_DE_IMPRESSAO_INS" & "|" & Aenge_GetCfg(CabInsercao, "ORDEM_DE_IMPRESSAO_INS", PathCfg)
        ArrayInfo(10) = "LAYER_TIT" & "|" & Aenge_GetCfg(CabInsercao, "LAYER_TIT", PathCfg)
        ArrayInfo(11) = "COR_TIT" & "|" & Aenge_GetCfg(CabInsercao, "COR_TIT", PathCfg)
        ArrayInfo(12) = "TIPOLINHA_TIT" & "|" & Aenge_GetCfg(CabInsercao, "TIPOLINHA_TIT", PathCfg)
        ArrayInfo(13) = "TIPOLETRA_TIT" & "|" & Aenge_GetCfg(CabInsercao, "TIPOLETRA_TIT", PathCfg)
        ArrayInfo(14) = "LAYER_CAB" & "|" & Aenge_GetCfg(CabInsercao, "LAYER_CAB", PathCfg)
        ArrayInfo(15) = "COR_CAB" & "|" & Aenge_GetCfg(CabInsercao, "COR_CAB", PathCfg)
        ArrayInfo(16) = "TIPOLINHA_CAB" & "|" & Aenge_GetCfg(CabInsercao, "TIPOLINHA_CAB", PathCfg)
        ArrayInfo(17) = "TIPOLETRA_CAB" & "|" & Aenge_GetCfg(CabInsercao, "TIPOLETRA_CAB", PathCfg)
        ArrayInfo(18) = "LAYER_TEXT" & "|" & Aenge_GetCfg(CabInsercao, "LAYER_TEXT", PathCfg)
        ArrayInfo(19) = "COR_TEXT" & "|" & Aenge_GetCfg(CabInsercao, "COR_TEXT", PathCfg)
        ArrayInfo(20) = "TIPOLINHA_TEXT" & "|" & Aenge_GetCfg(CabInsercao, "TIPOLINHA_TEXT", PathCfg)
        ArrayInfo(21) = "TIPOLETRA_TEXT" & "|" & Aenge_GetCfg(CabInsercao, "TIPOLETRA_TEXT", PathCfg)
        ArrayInfo(22) = "LAYER_BORDA" & "|" & Aenge_GetCfg(CabInsercao, "LAYER_BORDA", PathCfg)
        ArrayInfo(23) = "COR_BORDA" & "|" & Aenge_GetCfg(CabInsercao, "COR_BORDA", PathCfg)
        ArrayInfo(24) = "TIPOLINHA_BORDA" & "|" & Aenge_GetCfg(CabInsercao, "TIPOLINHA_BORDA", PathCfg)
        ArrayInfo(25) = "TIPOLETRA_BORDA" & "|" & Aenge_GetCfg(CabInsercao, "TIPOLETRA_BORDA", PathCfg)
        ArrayInfo(26) = "TIPOLETRA" & "|" & Aenge_GetCfg(CabInsercao, "TIPOLETRA", PathCfg)
        ArrayInfo(26) = "FATORTABELA" & "|" & Aenge_GetCfg(CabInsercao, "FATORTABELA", PathCfg)

        Dim RbfRet As New Autodesk.AutoCAD.DatabaseServices.ResultBuffer
        With RbfRet
            For Each objStr As String In ArrayInfo
                .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListBegin))
                .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.Text, objStr.ToString.Split("|")(0)))
                .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.Text, objStr.ToString.Split("|")(1)))
                .Add(New TypedValue(Autodesk.AutoCAD.Runtime.LispDataType.ListEnd))
            Next
        End With

        Return RbfRet

    End Function

    Function Return_TactualCApp() As String

        Dim TempReg As String = ""
        TempReg = Aenge_GetCfg("AppData", "AENGECAPP", ReturnPathApplication() & My.Settings.File_Autoenge)
        Return TempReg

    End Function

    Function Return_TactualProject() As String

        Dim TempReg As String = ""
        TempReg = Aenge_GetCfg("AppData", "AENGEPROJ", ReturnPathApplication() & My.Settings.File_Autoenge)
        Return TempReg

    End Function

    Function Return_TactualDrawing() As String

        Dim TempReg As String = ""
        TempReg = Aenge_GetCfg("AppData", "AENGEDWG", ReturnPathApplication() & My.Settings.File_Autoenge)
        Return TempReg

    End Function

    Function Return_TactualID() As String

        Dim TempReg As String = "", PathReg As String = ReturnPathApplication()
        TempReg = Aenge_GetCfg("AppData", "AENGEPROJID", PathReg & My.Settings.File_Autoenge)
        If TempReg = "" Then TempReg = "0"
        Return TempReg

    End Function

    'Return PathApplication
    Function ReturnPathApplication() As String

        Dim APath As String = "", FullPathStr As String = System.Reflection.Assembly.GetExecutingAssembly.Location

        Try
            APath = Mid(FullPathStr, 1, FullPathStr.Length - Dir(FullPathStr).Length)
            Return APath

        Catch ex As Exception
            Return ""
        End Try

    End Function

    'Return a name of database for consulting 
    Function ReturnDBName(ByVal TypeDB As String) As String

        Select Case LCase(TypeDB)
            Case "AHID"
                Return "AHID.mdb"

            Case "apwrnew"
                Return "AHID.accdb"

            Case "aenge"
                Return "Aenge.mdb"

            Case "aengenew"
                Return "Aenge.accdb"

            Case Else
                Return "AHID.mdb"

        End Select

    End Function

#End Region

#Region "----- Function for path and windows app - Version 5.0 or old -----"

    'Return PathApplication for versions 5.0 or old of autopower
    Function Return_Old_FolderAutoenge() As String

        Dim APath As String = "", FullPathStr As String = System.Reflection.Assembly.GetExecutingAssembly.Location

        Try
            APath = Mid(FullPathStr, 1, FullPathStr.Length - Dir(FullPathStr).Length)
            If APath.EndsWith("\") Then
                If APath.EndsWith(My.Settings.FolderApwr) Then APath = Mid(APath, 1, APath.Length - 5)
            Else
                If APath.EndsWith("AHID") Then APath = Mid(APath, 1, APath.Length - 4)
            End If

            If Not APath.EndsWith("\") Then APath += "\"
            Return APath

        Catch ex As Exception
            Return ""
        End Try

    End Function

    'Return a path of folder project 
    Function Return_Old_FolderProject() As String

        Dim StrPath As String = Return_Old_FolderAutoenge()
        Dim ProjectTactual As String = Return_Old_TactualProject()
        StrPath = StrPath & ProjectTactual & My.Settings.FolderApwr
        Return StrPath

    End Function

    'Return a path of folder project 
    Function Return_Old_FolderCom() As String

        Dim StrPath As String = Return_Old_FolderAutoenge()
        StrPath = StrPath & My.Settings.FolderCom
        Return StrPath

    End Function

    'Return a path of cfg drawing 
    Function Return_Old_FolderCfgDrawing() As String

        Dim StrPath As String = Return_Old_FolderAutoenge()
        Dim ProjectTactual As String = Return_Old_TactualProject()

        StrPath = StrPath & ProjectTactual & "\" & My.Settings.FolderApwr
        Return StrPath

    End Function

    'Return a path of file cfg drawing 
    Function Return_Old_FileCfgDrawing() As String

        Dim StrPath As String = Return_Old_FolderAutoenge()
        Dim ProjectTactual As String = Return_Old_TactualProject()
        Dim DrawingTactual As String = Return_Old_TactualDrawing()

        StrPath = StrPath & ProjectTactual & "\" & My.Settings.FolderApwr & DrawingTactual & ".cfg"
        Return StrPath

    End Function

    'Return a path of file cfg drawing project 
    Function Return_Old_FileCfgProject() As String

        Dim StrPath As String = Return_Old_FolderAutoenge()
        Dim ProjectTactual As String = Return_Old_TactualProject()
        Dim DrawingTactual As String = Return_Old_TactualDrawing()

        StrPath = StrPath & ProjectTactual & "\" & My.Settings.FolderApwr & My.Settings.File_ProjectCfg
        Return StrPath

    End Function

    'Functions for paths of application and folders Autopower - Autohidro
    Function Return_Old_FileCfgAutoenge() As String

        Dim StrPath As String = Return_Old_FolderAutoenge()
        StrPath = StrPath & My.Settings.File_Autoenge
        Return StrPath

    End Function

    'Functions for paths of application and folders Autopower - Autohidro
    Function Return_Old_FileCfgProjectDefault() As String

        Dim StrPath As String = Return_Old_FolderAutoenge()
        StrPath = StrPath & My.Settings.FolderApwr & My.Settings.File_ProjectCfg
        Return StrPath

    End Function

    Function Return_Old_TactualCApp() As String

        Dim TempReg As String = ""
        TempReg = Aenge_GetCfg("AppData", "AENGECAPP", Return_Old_FolderAutoenge() & My.Settings.File_Autoenge)
        Return TempReg

    End Function

    Function Return_Old_TactualProject() As String

        Dim TempReg As String = ""
        TempReg = Aenge_GetCfg("AppData", "AENGEPROJ", Return_Old_FolderAutoenge() & My.Settings.File_Autoenge)
        Return TempReg

    End Function

    Function Return_Old_TactualDrawing() As String

        Dim TempReg As String = ""
        TempReg = Aenge_GetCfg("AppData", "AENGEDWG", Return_Old_FolderAutoenge() & My.Settings.File_Autoenge)
        Return TempReg

    End Function

    Function Return_Old_TactualID() As String

        Dim TempReg As String = ""
        TempReg = Aenge_GetCfg("AppData", "AENGEPROJID", Return_Old_FolderAutoenge() & My.Settings.File_Autoenge)
        Return TempReg

    End Function

#End Region

End Class

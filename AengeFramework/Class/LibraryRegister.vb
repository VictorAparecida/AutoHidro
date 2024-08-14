Imports System.Data.OleDb
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text

Public Class LibraryRegister

#Region "----- Documentação -----"

    '==============================================================================================================================================
    'Projeto : FrameWork 2008 - Autocad
    'Empresa : Autoenge Brasil Ltda
    'Reponsável criação : Raul Antonio Fernandes Junior    
    'Data de criação : 04/03/2009
    'Objetivo : Class for read and write in databases of application. Return registers for all tables.
    '
    'Informações adicionais :
    'LIBREGISTER001 - RetunAllDraw_Project
    'LIBREGISTER002 - UpdateTableDatabase_AeleObjC
    'LIBREGISTER003 - 
    'LIBREGISTER004 -
    '==============================================================================================================================================

#End Region

#Region "----- Declare and Parameters -----"

    Private Shared LibraryConnection As LibraryConnection, LibraryReference As LibraryReference
    Private Const FileName_AengeCFG As String = "Autoenge.cfg"
    'For file DBALL (Material List)
    Private Const NameFile_DbAll As String = "DB_ALL.dat", mVar_NameClass As String = "LibraryRegister"

    Public IdTactual As Integer = 0

    Dim LibraryError As LibraryError

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

#Region "----- Function for DB_APwr - Return register from database -----"

    'Return a name of project tactual 
    Function GetNameProject_Tactual() As String

        Dim libraryReference As New LibraryReference, ResultCFG As String
        Dim FullPath_Install = libraryReference.ReturnPathApplication

        Try

            ResultCFG = Aenge_GetCfg("AppData", "AENGEPROJ", FullPath_Install & FileName_AengeCFG).ToString
            Return ResultCFG

        Catch ex As Exception
            libraryReference = Nothing
            Return ""
        End Try

    End Function

    'Return a name of project tactual 
    Function GetNameDWG_Tactual() As String

        Dim libraryReference As New LibraryReference, ResultCFG As String
        Dim FullPath_Install = libraryReference.ReturnPathApplication

        Try

            ResultCFG = Aenge_GetCfg("AppData", "AENGEDWG", FullPath_Install & FileName_AengeCFG).ToString
            Return ResultCFG

        Catch ex As Exception
            libraryReference = Nothing
            Return ""
        End Try

    End Function

    'Return a tactual Id os databse 
    Function GetID_Tactual(Optional ByVal Conn As Object = Nothing) As Integer

        Dim FullPath_Install As String, IdTactual As String

        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
        FullPath_Install = LibraryReference.ReturnPathApplication & "Autoenge.cfg"

        IdTactual = Aenge_GetCfg("AppData", "AENGEPROJID", FileName_AengeCFG).ToString
        If Not IsNumeric(IdTactual) Then IdTactual = "0"
        'Return idtactual, but set IdTactual variable public in class for using 
        Return Integer.Parse(IdTactual)

    End Function

    'Function for read all darwings of a project in database 
    'Function RetunAllDraw_Project(Optional ByVal TypeReturn As String = "string", Optional ByVal IsFullPath As Boolean = False, Optional ByVal AllProject As Boolean = False) As Object

    '    Dim ArrayDwg As New ArrayList, StrDWg As String = "", NameProject_Tactual As String = GetNameProject_Tactual()
    '    Dim FullPath_Install As String, StrTable As String, IdProject As Integer, NameDB As String, NameDb_Aenge As String

    '    Try

    '        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
    '        'Get a tactual Id
    '        IdProject = LibraryReference.Return_TactualID

    '        With LibraryReference
    '            NameDB = .ReturnDBName("AHID")
    '            NameDb_Aenge = .ReturnDBName("aenge")
    '            FullPath_Install = .ReturnPathApplication
    '        End With

    '        'Read all information about tactual project
    '        If AllProject = True Then
    '            StrTable = "SELECT AengeDef.Id, * FROM AengeDes INNER JOIN AengeDef ON AengeDef.PrjNome = AengeDes.PrjNome "
    '        Else
    '            StrTable = "SELECT AengeDef.Id, * FROM AengeDes INNER JOIN AengeDef ON AengeDef.PrjNome = AengeDes.PrjNome WHERE AengeDef.Id = " & IdProject & ""
    '        End If

    '        Dim ApwrCss As New APWRCSS.CDialogCSS, Dt As Object
    '        ApwrCss.RetAllDwg_Aenge(StrTable, "", FullPath_Install & NameDb_Aenge)

    '        'Now, read all register and create a string with all dwg in database of this project 
    '        If Dt.Rows.Count > 0 Then
    '            Dim Dr As DataRow

    '            For Each Dr In Dt.Rows
    '                If IsFullPath = True Then
    '                    StrDWg += Dr("PrjDwgNome").ToString & ".dwg" & "|"
    '                    ArrayDwg.Add(FullPath_Install & NameProject_Tactual & "\" & Dr("PrjDwgNome").ToString & ".dwg")
    '                Else
    '                    StrDWg += Dr("PrjDwgNome").ToString & "|"
    '                    ArrayDwg.Add(Dr("PrjDwgNome").ToString)
    '                End If

    '            Next

    '        End If

    '        Select Case TypeReturn
    '            Case "string"
    '                Return StrDWg

    '            Case "datatable"
    '                Return Dt

    '            Case Else
    '                Return ArrayDwg

    '        End Select

    '    Catch ex As Exception
    '        LibraryError.CreateErrorAenge(ex, ex.Message, , mVar_NameApplication, mVar_NameOwner, mvar_nameclass, "LIBREGISTER001")
    '        Return Nothing
    '    Finally
    '        LibraryConnection = Nothing
    '    End Try

    'End Function

    'Function for read all darwings of a project in database 
    Function RetunAllDraw_Project_COMDll(Optional ByVal TypeReturn As String = "string", Optional ByVal IsFullPath As Boolean = False, Optional ByVal AllProject As Boolean = False) As Object

        Dim ArrayDwg As New ArrayList, StrDWg As String = "", NameProject_Tactual As String = GetNameProject_Tactual()
        Dim NameDB As String, NameDb_Aenge As String, FullPath_Install As String, IdProject As Integer

        Try

            If LibraryConnection Is Nothing Then LibraryConnection = New LibraryConnection
            If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
            'Get a tactual Id
            IdProject = LibraryReference.Return_TactualID

            With LibraryReference
                NameDB = .ReturnDBName("apwrnew")
                NameDb_Aenge = .ReturnDBName("aengenew")
                FullPath_Install = .ReturnPathApplication
            End With

            With LibraryConnection
                .PathDb = ""
                .PathDb_Aenge = FullPath_Install & NameDb_Aenge
                'ConnTactual_Aenge = .ReturnConn_DB()
            End With

            'Dim ApwrCss As New APWRCSS.CDialogCSS
            'Dim ArrayAPWRCSS As Object

            ''Read all information about tactual project
            'If AllProject = True Then
            '    'StrTable = "SELECT AengeDef.Id, * FROM AengeDes INNER JOIN AengeDef ON AengeDef.PrjNome = AengeDes.PrjNome "
            '    ArrayAPWRCSS = ApwrCss.RetAllDwg_Aenge("SELECT AengeDef.Id, * FROM AengeDes INNER JOIN AengeDef ON AengeDef.PrjNome = AengeDes.PrjNome ", "aenge", FullPath_Install & "Aenge.mdb")
            'Else
            '    'StrTable = "SELECT AengeDef.Id, * FROM AengeDes INNER JOIN AengeDef ON AengeDef.PrjNome = AengeDes.PrjNome WHERE AengeDef.Id = " & IdProject & ""
            '    ArrayAPWRCSS = ApwrCss.RetAllDwg_Aenge("SELECT AengeDef.Id, * FROM AengeDes INNER JOIN AengeDef ON AengeDef.PrjNome = AengeDes.PrjNome WHERE AengeDef.Id = " & IdProject & "", "aenge", _
            '                                           FullPath_Install & "Aenge.mdb")
            'End If

            'If Not ArrayAPWRCSS Is Nothing Then
            '    'Now, read all register and create a string with all dwg in database of this project 
            '    If UBound(CType(ArrayAPWRCSS, Array)) > 0 Then

            '        For Each Dr As Object In ArrayAPWRCSS
            '            If Not Dr Is Nothing Then
            '                If IsFullPath = True Then
            '                    StrDWg += Dr.ToString & ".dwg" & "|"
            '                    ArrayDwg.Add(FullPath_Install & NameProject_Tactual & "\" & Dr.ToString & ".dwg")
            '                Else
            '                    StrDWg += Dr.ToString & "|"
            '                    ArrayDwg.Add(Dr.ToString)
            '                End If
            '            End If
            '        Next

            '    End If
            'End If

            Select Case TypeReturn
                Case "string"
                    Return StrDWg

                Case "datatable"
                    Return Nothing

                Case Else
                    Return ArrayDwg

            End Select

        Catch ex As Exception
            'LibraryError.CreateErrorAenge(ex, ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LIBREGISTER001")
            MsgBox("Ocorrência de erro - Sincronização - " & ex.Message, MsgBoxStyle.Information, "Error aenge")
            Return Nothing
        Finally
            LibraryConnection = Nothing
        End Try

    End Function

    'Maneira antiga de acesso a banco de dados diretamente pelo .net 
    Function RetunAllDraw_Project(Optional ByVal TypeReturn As String = "string", Optional ByVal IsFullPath As Boolean = False, Optional ByVal AllProject As Boolean = False) As Object

        Dim ArrayDwg As New ArrayList, StrDWg As String = "", ConnTactual_Aenge As Object = Nothing, NameProject_Tactual As String = GetNameProject_Tactual()
        Dim NameDB As String, NameDb_Aenge As String, FullPath_Install As String, StrTable As String, IdProject As Integer
        Dim objReader As StreamReader, NameFileTemp As String = "", NameFileTemp_Dat As String = "", FileDbAll As String = ""
        Dim sLine As String = "", arrText As New ArrayList(), FullPath As String = "", Library_Reference As LibraryReference = Nothing

        Try

            If LibraryConnection Is Nothing Then LibraryConnection = New LibraryConnection
            If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
            'Get a tactual Id
            IdProject = LibraryReference.Return_TactualID

            With LibraryReference
                'NameDB = .ReturnDBName("apwrnew")
                'NameDb_Aenge = .ReturnDBName("aengenew")
                NameDB = .ReturnDBName("AHID")
                NameDb_Aenge = .ReturnDBName("aenge")
                FullPath_Install = .ReturnPathApplication
            End With

            Dim Path_FileAllFiles_Reader As String

            'Read all information about tactual project
            If AllProject = True Then
                StrTable = "SELECT AengeDef.Id, * FROM AengeDes INNER JOIN AengeDef ON AengeDef.PrjNome = AengeDes.PrjNome "
                Path_FileAllFiles_Reader = FullPath_Install & "APWRDWG.dll"
            Else
                'Consulta somente de um projeto em questao...
                StrTable = "SELECT AengeDef.Id, * FROM AengeDes INNER JOIN AengeDef ON AengeDef.PrjNome = AengeDes.PrjNome WHERE AengeDef.Id = " & IdProject & ""
                Path_FileAllFiles_Reader = FullPath_Install & "APWRDWGAL.dll"
            End If

            If My.Computer.FileSystem.FileExists(Path_FileAllFiles_Reader) Then
                'Open file for reading 
                objReader = New StreamReader(Path_FileAllFiles_Reader)
                Do
                    sLine = objReader.ReadLine()
                    'Validate a Cab-File 
                    If Not sLine Is Nothing Then arrText.Add(sLine.Replace(Chr(34), ""))
                Loop Until sLine Is Nothing
                'Close the file opened for reading 
                objReader.Close()
            End If

            'With LibraryConnection
            '    .PathDb = ""
            '    .PathDb_Aenge = FullPath_Install & NameDb_Aenge
            '    ConnTactual_Aenge = .ReturnConn_DB_97
            'End With
            'Dim Da As New OleDbDataAdapter(StrTable, CType(ConnTactual_Aenge, OleDbConnection))

            Dim Da As New OleDbDataAdapter(StrTable, ConnAenge)
            Dim Dt As New DataTable
            Da.Fill(Dt)

            'Now, read all register and create a string with all dwg in database of this project 
            If Dt.Rows.Count > 0 Then
                Dim Dr As DataRow

                For Each Dr In Dt.Rows
                    If IsFullPath = True Then
                        StrDWg += Dr("PrjDwgNome").ToString & ".dwg" & "|"
                        ArrayDwg.Add(FullPath_Install & NameProject_Tactual & "\" & Dr("PrjDwgNome").ToString & ".dwg")
                    Else
                        StrDWg += Dr("PrjDwgNome").ToString & "|"
                        ArrayDwg.Add(Dr("PrjDwgNome").ToString)
                    End If
                Next
            End If

            If arrText.Count > 0 Then

                For Each Dr In arrText
                    If Not Dr Is Nothing Then
                        If IsFullPath = True Then
                            StrDWg += Dr.ToString & ".dwg" & "|"
                            ArrayDwg.Add(FullPath_Install & NameProject_Tactual & "\" & Dr.ToString & ".dwg")
                        Else
                            StrDWg += Dr.ToString & "|"
                            ArrayDwg.Add(Dr.ToString)
                        End If
                    End If
                Next
            End If

            Select Case TypeReturn
                Case "string"
                    Return StrDWg

                Case "datatable"
                    Return Dt ''arrText

                Case Else
                    Return ArrayDwg

            End Select

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LIBREGISTER001")
            Return Nothing
        Finally
            LibraryConnection = Nothing
        End Try

    End Function

#End Region

#Region "----- Functions for read and write files of windows (OS) and another similar functions -----"

    'This function append all registers in the files DB_ in project. This file DB_NameProject is used in lisp function for create a material list complete of project.
    Function AppendAllFilesDB_Project(Optional ByVal Cab_File As Boolean = False) As Object

        Dim Result As Boolean = True, Count As Integer = 0, Trecho As String = "Trecho 1", NameFileOriginal As String
        Dim ArrayDwg As ArrayList, NameProject_Tactual As String = GetNameProject_Tactual()
        Dim objReader As StreamReader, NameFileTemp As String = "", NameFileTemp_Dat As String, FileDbAll As String = ""
        Dim sLine As String = "", arrText As New ArrayList(), FullPath As String = "", Library_Reference As LibraryReference = Nothing

        Try

            ArrayDwg = RetunAllDraw_Project("array", True)

            'For desconsider a cab-file 
            Count = 0

            If Not ArrayDwg Is Nothing Then

                For Each FileObj As Object In ArrayDwg
                    'Validate file if exits

                    NameFileOriginal = FileObj.ToString.Split("\")(UBound(FileObj.ToString.Split("\")))
                    NameFileTemp = NameFileOriginal.Replace(".dwg", "")
                    NameFileTemp_Dat = "DB_" & NameFileTemp & ".dat"
                    NameFileTemp_Dat = FileObj.ToString.Replace(NameFileOriginal, NameFileTemp_Dat)

                    If FullPath = "" Then FullPath = Mid(FileObj.ToString, 1, FileObj.ToString.Length - NameFileOriginal.Length)

                    If My.Computer.FileSystem.FileExists(NameFileTemp_Dat) Then
                        'Open file for reading 
                        objReader = New StreamReader(NameFileTemp_Dat.ToString)

                        Do
                            sLine = objReader.ReadLine()
                            'Validate a Cab-File 

                            If Cab_File = True And Count < 1 Then
                                arrText.Add(My.Settings.Cab_File)
                                Count += 1
                            End If

                            'Now reading all information in file, desconsider a cab-file 
                            If Not sLine Is Nothing Then If Not sLine.Contains(My.Settings.Cab_File) Then arrText.Add(sLine)

                        Loop Until sLine Is Nothing

                        'Close the file opened for reading 
                        objReader.Close()

                    End If

                Next

                'Now write in file DB_All
                Trecho = "Trecho 2"
                If Library_Reference Is Nothing Then Library_Reference = New LibraryReference
                FullPath = Library_Reference.ReturnPathApplication

                'If exists file, then delete
                If My.Computer.FileSystem.FileExists(FullPath & NameProject_Tactual & "\" & NameFile_DbAll) Then My.Computer.FileSystem.DeleteFile(FullPath & NameProject_Tactual & "\" & NameFile_DbAll)
                Dim myEncoding As Encoding
                'myEncoding = System.Text.Encoding.GetEncoding(1252)
                myEncoding = System.Text.Encoding.UTF8

                Trecho = "Trecho 3"

                Dim objWriter As StreamWriter = New StreamWriter(FullPath & NameProject_Tactual & "\DB_" & NameProject_Tactual & ".dat", False, myEncoding)
                For Each sLine In arrText
                    objWriter.WriteLine(sLine)
                Next
                objWriter.Close()

            End If   'ArrayDwg - Is nothing 

            Return Result

        Catch ex As Exception
            MsgBox("Ocorrência de erro : Reading data of project and all info in AppendAllFilesDb - " & ex.Message, MsgBoxStyle.Information & " - " & Trecho, "Aenge Error")
            Return False
        End Try

    End Function

    'Function for read all information in cfg and return a list of list with all lines in cab
    Function ReturnAll_CfgData(ByVal Rbf As Autodesk.AutoCAD.DatabaseServices.ResultBuffer) As Autodesk.AutoCAD.DatabaseServices.ResultBuffer

        Dim RbfEnd As New Autodesk.AutoCAD.DatabaseServices.ResultBuffer




        Return RbfEnd
    End Function

#End Region

#Region "----- Functions for datatable and registers -----"

    'Function for create a table of Pot and others 
    Function CreateTable(ByVal TypeTable As String) As DataTable

        ' Create new DataTable instance.
        Dim table As New DataTable

        Select Case TypeTable
            Case "quadro"
                With table
                    ' Create four typed columns in the DataTable.
                    .Columns.Add("Quadro", GetType(String))
                    .Columns.Add("Circuito", GetType(String))
                    .Columns.Add("PotW", GetType(Decimal))
                    .Columns.Add("PotVA", GetType(Decimal))
                    .Columns.Add("Tensao", GetType(Decimal))
                    ' Add five rows with those columns filled in the DataTable.
                    'table.Rows.Add(25, "Indocin", "David", DateTime.Now)
                End With

        End Select

        Return table
    End Function

    'Update all registers in datatable
    Function UpdateTableDatabase_AeleObjC(ByVal Dt As DataTable, ByVal ConnApwr_ As OleDbConnection) As Object

        Try

            If ConnApwr_ Is Nothing Then
                Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
                With LibraryConnection
                    .TypeDb = "AHID_"
                    ConnApwr_ = .Aenge_OpenConnectionDB
                End With
            End If

            ' Create an instance of a DataAdapter.
            Dim daAeleObjC As New OleDbDataAdapter("Select * From AeleObjC", ConnApwr_)
            ' Create an instance of a DataSet, and retrieve data from the Authors table.
            Dim dsPubs As New DataSet("AENGE")
            daAeleObjC.FillSchema(dsPubs, SchemaType.Source, "AeleObjC")
            daAeleObjC.Fill(dsPubs, "AeleObjC")

            '*****************
            'BEGIN ADD CODE 
            ' Create a new instance of a DataTable
            Dim tblAeleObjC As DataTable
            tblAeleObjC = dsPubs.Tables("AeleObjC")
            tblAeleObjC.Merge(Dt)

            Dim objCommandBuilder As New OleDbCommandBuilder(daAeleObjC)
            daAeleObjC.Update(dsPubs, "AeleObjC")
            Return True

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao atualizar dados do AengeObjectDat - AeleObjC - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LIBREGISTER002")
            Return Nothing
        Finally
            If ConnApwr_.State = ConnectionState.Open Then ConnApwr_.Close()
            ConnApwr_ = Nothing
            LibraryConnection = Nothing
        End Try

    End Function

#End Region

End Class

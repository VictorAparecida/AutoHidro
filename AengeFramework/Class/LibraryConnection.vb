Imports System.Data.OleDb

Public Class LibraryConnection

#Region "----- Documentação -----"

    '==============================================================================================================================================
    'Projeto : FrameWork 2008 - Autocad
    'Empresa : Autoenge Brasil Ltda
    'Reponsável criação : Raul Antonio Fernandes Junior    
    'Data de criação : 04/03/2009
    'Objetivo : Classe responsável pela manipulação das informações e funcionalidades das desenhos do autocad.
    '
    'Informações adicionais :
    'LIBCONNECTION001 - 
    'LIBCONNECTION002 - 
    'LIBCONNECTION003 - 
    'LIBCONNECTION004 -
    '==============================================================================================================================================

#End Region

#Region "----- Declare and Parameters -----"

    Private Shared mVar_PathDb As String, mVar_PathDb_Aenge As String
    Private Shared mVar_ConnDb As OleDbConnection

#End Region

#Region "----- Get and Set -----"

    Property PathDb() As String
        Get
            PathDb = mVar_PathDb
        End Get
        Set(ByVal value As String)
            mVar_PathDb = value
        End Set
    End Property

    Property PathDb_Aenge() As String
        Get
            PathDb_Aenge = mVar_PathDb_Aenge
        End Get
        Set(ByVal value As String)
            mVar_PathDb_Aenge = value
        End Set
    End Property

#End Region

#Region "----- Coonections for databases -----"

    Function ReturnConn_DB() As Object

        Dim APWRCSS As New ApwrCss, TypeDb As String = "", Path As String = ""
        Dim ConnString As String

        If mVar_PathDb <> "" Then
            TypeDb = "AHID"
            Path = mVar_PathDb
        Else
            TypeDb = "Aenge"
            Path = mVar_PathDb_Aenge
        End If

        ConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Path & ";Persist Security Info=False;Jet OLEDB:Database Password=" & APWRCSS.RetCSS(TypeDb)

        Try

            mVar_ConnDb = New OleDbConnection(ConnString)
            With mVar_ConnDb

                'If open, then return connection
                If .State = ConnectionState.Open Then Return mVar_ConnDb

                .Open()
                If .State = ConnectionState.Open Then
                    Return mVar_ConnDb
                Else
                    Return Nothing
                End If
            End With

        Catch ex As Exception
            Return Nothing
        Finally
            APWRCSS = Nothing
        End Try

    End Function

    'Retorna a conex]ao com o access 97
    Function ReturnConn_DB_97() As Object

        Dim APWRCSS As New ApwrCss, TypeDb As String = "", Path As String = ""
        Dim ConnString As String

        If mVar_PathDb <> "" Then
            TypeDb = "AHID"
            Path = mVar_PathDb
        Else
            TypeDb = "AENGE"
            Path = mVar_PathDb_Aenge
        End If

        'Provider=Microsoft.Jet.OLEDB.4.0;Data Source="C:\Program Files\AutoPOWER 2012\AHID.mdb"
        ConnString = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
        "Data Source=" & Path & ";Persist Security Info=False;Jet OLEDB:Database Password=" & APWRCSS.RetCSS(TypeDb)

        Try

            mVar_ConnDb = New OleDbConnection(ConnString)
            With mVar_ConnDb

                'If open, then return connection
                If .State = ConnectionState.Open Then Return mVar_ConnDb

                .Open()
                If .State = ConnectionState.Open Then
                    Return mVar_ConnDb
                Else
                    Return Nothing
                End If
            End With

        Catch ex As Exception
            Return Nothing
        Finally
            APWRCSS = Nothing
        End Try

    End Function

#End Region

End Class

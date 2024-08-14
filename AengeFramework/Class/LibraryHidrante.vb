Imports System.Windows.Forms
Imports System.Data.OleDb

Public Class LibraryHidrante

#Region "------ Atributes and declarations -----"

    Private Shared msgErrorTit As String = "Error aenge", msgErrorIni As String = "Ocorrência de erro - "

#End Region

#Region "----- Functions for fill components -----"

    'Cria o combo com as informacoes de hidrantes de acordo com o tipo selecionado 
    Function FillcboHidrante(ByVal cbo As ComboBox, ByVal CodTipo As Int16, Optional ByVal AddTodos As Boolean = True) As Object

        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
        Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent
        Dim DtTipo As New System.Data.DataTable

        Try

            If ConnAhid Is Nothing Then
                With LibraryConnection
                    .TypeDb = "AHID_"
                    ConnAhid = .Aenge_OpenConnectionDB
                End With
            End If

            'Dim StrSql As String = "SELECT TINCOBJDIAM.CODINCOBJDIAM, TINCOBJDIAM.CODINCOBJ, TINCOBJ.DESINCOBJ, TINCOBJ.IDTTIPOOBJ, TINCOBJDIAM.CODVAZAO, TINCOBJDIAM.NUMDIAM, TINCOBJDIAM.NUMCOMP," & _
            '" TINCOBJDIAM.CODTIPO, TINCTIPO.DESCTIPO,  ('Diam: ' & TINCOBJDIAM.NUMDIAM & '  /  Comp: ' & TINCOBJDIAM.NUMCOMP) AS HIDRANTE"
            'StrSql += " FROM TINCOBJ INNER JOIN (TINCTIPO INNER JOIN TINCOBJDIAM ON TINCTIPO.CODTIPO = TINCOBJDIAM.CODTIPO) ON TINCOBJ.CODINCOBJ = TINCOBJDIAM.CODINCOBJ"
            'StrSql += " WHERE TINCOBJ.IDTTIPOOBJ = 'MAN'"
            'StrSql += " AND TINCOBJDIAM.CODTIPO = " & CodTipo
            'StrSql += " ORDER BY TINCOBJ.DesIncObj ASC, TINCOBJDIAM.NUMDIAM ASC, TINCOBJDIAM.NUMCOMP ASC"

            Dim StrSql As String = "SELECT DISTINCT TINCOBJDIAM.CODINCOBJDIAM,  ('Diam: ' & TINCOBJDIAM.NUMDIAM & '  /  Comp: ' & TINCOBJDIAM.NUMCOMP) AS HIDRANTE"
            StrSql += " FROM TINCOBJ INNER JOIN (TINCTIPO INNER JOIN TINCOBJDIAM ON TINCTIPO.CODTIPO = TINCOBJDIAM.CODTIPO) ON TINCOBJ.CODINCOBJ = TINCOBJDIAM.CODINCOBJ"
            StrSql += " WHERE TINCOBJ.IDTTIPOOBJ = 'MAN'"
            StrSql += " AND TINCOBJDIAM.CODTIPO = " & CodTipo
            'StrSql += " ORDER BY TINCOBJ.DesIncObj ASC, TINCOBJDIAM.NUMDIAM ASC, TINCOBJDIAM.NUMCOMP ASC"

            Dim Da As New OleDbDataAdapter(StrSql, ConnAhid)
            Da.Fill(DtTipo)

            With LibraryComponent
                .FillCombo(cbo, DtTipo, "HIDRANTE", "CODINCOBJDIAM")
            End With
            LibraryComponent = Nothing
            Return True

        Catch ex As Exception
            MsgBox("Error fill component - hidrantes - " & ex.Message, MsgBoxStyle.Information, "Error hidrantes")
            Return False
        Finally
            LibraryConnection = Nothing
            LibraryComponent = Nothing
        End Try

    End Function

    Function FillMangueira(ByVal cbo As ComboBox, Optional ByVal AddTodos As Boolean = True) As Object

        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
        Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent
        Dim DtTipo As New System.Data.DataTable

        Try

            If ConnAhid Is Nothing Then
                With LibraryConnection
                    .TypeDb = "AHID_"
                    ConnAhid = .Aenge_OpenConnectionDB
                End With
            End If

            Dim Da As New OleDbDataAdapter("Select * From TINCObj Where IdtTipoObj = 'MAN' Order By DesIncObj Asc", ConnAhid)
            Da.Fill(DtTipo)

            With LibraryComponent
                .FillCombo(cbo, DtTipo, "DesIncObj", "CodIncObj")
            End With
            LibraryComponent = Nothing
            Return True

        Catch ex As Exception
            MsgBox("Error fill component - mangueiras - " & ex.Message, MsgBoxStyle.Information, "Error mangueiras")
            Return False
        Finally
            LibraryConnection = Nothing
            LibraryComponent = Nothing
        End Try

    End Function

    Function FillEsguicho(ByVal cbo As ComboBox, Optional ByVal AddTodos As Boolean = True) As Object

        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
        Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent
        Dim DtTipo As New System.Data.DataTable

        Try

            If ConnAhid Is Nothing Then
                With LibraryConnection
                    .TypeDb = "AHID_"
                    ConnAhid = .Aenge_OpenConnectionDB
                End With
            End If

            Dim Da As New OleDbDataAdapter("Select * From TINCObj Where IdtTipoObj = 'ESG' Order By DesIncObj Asc", ConnAhid)
            Da.Fill(DtTipo)

            With LibraryComponent
                .FillCombo(cbo, DtTipo, "DesIncObj", "CodIncObj")
            End With
            LibraryComponent = Nothing
            Return True

        Catch ex As Exception
            MsgBox("Error fill component - Esguichos - " & ex.Message, MsgBoxStyle.Information, "Error esguichos")
            Return False
        Finally
            LibraryConnection = Nothing
            LibraryComponent = Nothing
        End Try

    End Function

    Function FillTipoSistema(ByVal cbo As ComboBox, Optional ByVal AddTodos As Boolean = True) As Object

        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
        Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent
        Dim DtTipo As New System.Data.DataTable

        Try

            If ConnAhid Is Nothing Then
                With LibraryConnection
                    .TypeDb = "AHID_"
                    ConnAhid = .Aenge_OpenConnectionDB
                End With
            End If

            Dim Da As New OleDbDataAdapter("Select * From TINCTipo", ConnAhid)
            Da.Fill(DtTipo)

            With LibraryComponent
                .FillCombo(cbo, DtTipo, "DescTipo", "CodTipo")
            End With
            LibraryComponent = Nothing
            Return True

        Catch ex As Exception
            MsgBox("Error fill component - Tipos de sistema - " & ex.Message, MsgBoxStyle.Information, "Error Tipos de sistema")
            Return False
        Finally
            LibraryConnection = Nothing
            LibraryComponent = Nothing
        End Try

    End Function

    Function FillOcupacao(ByVal cbo As ComboBox, ByVal CodTipo As Int16, Optional ByVal AddTodos As Boolean = True) As Object

        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
        Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent
        Dim DtTipo As New System.Data.DataTable

        Try

            If ConnAhid Is Nothing Then
                With LibraryConnection
                    .TypeDb = "AHID_"
                    ConnAhid = .Aenge_OpenConnectionDB
                End With
            End If

            Dim Da As New OleDbDataAdapter("Select * From TINCOcupacao Where CodTipo = " & CodTipo, ConnAhid)
            Da.Fill(DtTipo)

            With LibraryComponent
                .FillCombo(cbo, DtTipo, "DesOcup", "CodOcup")
            End With
            LibraryComponent = Nothing
            Return True

        Catch ex As Exception
            MsgBox("Error fill component - Tipos de ocupação - " & ex.Message, MsgBoxStyle.Information, "Error ocupação")
            Return False
        Finally
            LibraryConnection = Nothing
            LibraryComponent = Nothing
        End Try

    End Function

    Function FillEstado(ByVal Cbo As ComboBox, Optional ByVal AddTodos As Boolean = True) As Object

        Dim LibraryConnection As New Aenge.Library.Db.LibraryConnection
        Dim LibraryComponent As New Aenge.Library.Component.LibraryComponent
        Dim DtEstado As New System.Data.DataTable

        Try

            If ConnAhid Is Nothing Then
                With LibraryConnection
                    .TypeDb = "AHID_"
                    ConnAhid = .Aenge_OpenConnectionDB
                End With
            End If

            Dim Da As New OleDbDataAdapter("Select Distinct(IDTEstado) as IDTEstado From TINCOcupacao ORDER BY IDTEstado ASC", ConnAhid)
            Da.Fill(DtEstado)

            Cbo.Items.Clear()
            If AddTodos Then Cbo.Items.Add("TODOS")
            For Each Dr As DataRow In DtEstado.Rows
                Cbo.Items.Add(Dr("IDTEstado").ToString)
            Next

            Return DtEstado

        Catch ex As Exception
            MsgBox("Error fill component - Estados - " & ex.Message, MsgBoxStyle.Information, "Error estados")
            Return Nothing
        Finally
            LibraryConnection = Nothing
            LibraryComponent = Nothing
        End Try

    End Function

#End Region

End Class

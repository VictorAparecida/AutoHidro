Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Runtime

Public Class frmLineType

#Region "----- Atributes and Declarations -----"

    Dim LibraryError As LibraryError

    Private Shared mVar_NameClass As String = "frmLineType"

#End Region

#Region "----- Constructors -----"

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LibraryError = New LibraryError
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        LibraryError = Nothing
        MyBase.Finalize()
    End Sub

#End Region

#Region "----- Functions for Cad procedures and components ------"

    Function GetInfoLineType() As Object

        Dim LibraryCad As New LibraryCad, LLine As Object

        Try

            Linetype1.Items.Clear()
            LLine = LibraryCad.LineTypeToList

            For Each Lin As Object In LLine
                Linetype1.Items.Add(Lin.ToString)
            Next

            Linetype1.SelectedIndex = 0

        Catch ex As System.Exception
            LibraryError.CreateErrorAenge(Err, "Error GetInfLINT - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "Aenge_GetCfg03")
        Finally
            LibraryCad = Nothing
        End Try

        Return Nothing

    End Function

#End Region

#Region "----- Events -----"

#Region "----- CommandButton -----"

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click

        ResultBf_Default = Nothing
        ResultBf_Default = New ResultBuffer

        'Get list of class return in my.config 
        With ResultBf_Default
            .Add(New TypedValue(LispDataType.ListBegin))
            .Add(New TypedValue(LispDataType.Text, Linetype1.Text.ToString))
            .Add(New TypedValue(LispDataType.ListEnd))
        End With

        Me.Dispose(True)

    End Sub

    'Cancel parameters for resultbuffer
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        ResultBf_Default = Nothing
        ResultBf_Default = New ResultBuffer

        'Get list of class return in my.config 
        With ResultBf_Default
            .Add(New TypedValue(LispDataType.ListBegin))
            .Add(New TypedValue(LispDataType.Text, "cancel".ToString))
            .Add(New TypedValue(LispDataType.ListEnd))
        End With

        Me.Dispose(True)

    End Sub

#End Region

#Region "----- Form -----"

    Private Sub frmLineType_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frm_LineType = Nothing
    End Sub

    Private Sub frmLineType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetInfoLineType()
    End Sub

#End Region

#End Region

End Class
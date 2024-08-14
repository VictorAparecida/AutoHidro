Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.Runtime

Public Class frmLegendaNum

#Region "----- Atributes and declarations -----"

    Private Shared mVar_ListObj As Object, mVar_DtGrid As System.Windows.Forms.DataGridView, LoadAllInfo As Boolean = False

#End Region

#Region "----- Get and Set -----"

    'List with all objects selected by user
    Property DataObj() As Object
        Get
            DataObj = mVar_ListObj
        End Get
        Set(ByVal value As Object)
            mVar_ListObj = value
        End Set
    End Property

    Property DtGrid() As System.Windows.Forms.DataGridView
        Get
            DtGrid = mVar_DtGrid
        End Get
        Set(ByVal value As System.Windows.Forms.DataGridView)
            mVar_DtGrid = value
        End Set
    End Property

#End Region

#Region "----- Constructors -----"

#End Region

#Region "----- Events -----"

#Region "----- CheckBox -----"

    Private Sub chkMarq_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMarq.CheckedChanged

        If LoadAllInfo = False Then Exit Sub

        If chkMarq.Checked Then
            For i As Int16 = 0 To mVar_DtGrid.RowCount - 1
                mVar_DtGrid.Rows(i).Cells(0).Value = True
            Next

        Else
            For i As Int16 = 0 To mVar_DtGrid.RowCount - 1
                mVar_DtGrid.Rows(i).Cells(0).Value = False
            Next

        End If

    End Sub

#End Region

#Region "----- Forms -----"

    Private Sub frmLegendaNum_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frm_LegendaNum = Nothing
    End Sub

    Private Sub frmLegendaNum_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FillChkObj()
        LoadAllInfo = True
    End Sub

#End Region

#Region "----- Command Button -----"

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        ResultBf_Default = Nothing
        Me.Dispose(True)
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click


        ResultBf_Default.Add(New TypedValue(LispDataType.ListBegin))
        For i As Int16 = 0 To GridRegister.Rows.Count - 1
            'For each line in datagridview 
            Dim Dtg As System.Windows.Forms.DataGridViewRow
            Dtg = GridRegister.Rows(i)
            'Validate if circuit is selected by user 
            If Not GridRegister.Rows(i).Cells(0).Value Is Nothing Then
                If GridRegister.Rows(i).Cells(0).Value.ToString.ToLower = "True".ToLower Then
                    With ResultBf_Default
                        .Add(New TypedValue(LispDataType.ListBegin))
                        .Add(New TypedValue(LispDataType.Text, GridRegister.Rows(i).Cells(1).Value.ToString))
                        .Add(New TypedValue(LispDataType.Text, GridRegister.Rows(i).Cells(3).Value.ToString))
                        .Add(New TypedValue(LispDataType.ListEnd))
                    End With
                End If
            End If
        Next
        ResultBf_Default.Add(New TypedValue(LispDataType.ListEnd))

        Me.Dispose(True)
    End Sub

#End Region

#End Region

#Region "----- Functions for Fill objects -----"

    Function FillChkObj() As Object

        Try

            Dim Dv As New System.Data.DataView(mVar_ListObj)
            Dv.Sort = "ObjLeg ASC"

            With mVar_DtGrid
                .DataSource = mVar_ListObj
                .Columns(1).Visible = False
                .Columns(2).AutoSizeMode = Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
                .Columns(2).ReadOnly = True
                .Columns(3).Visible = False
            End With

            For i As Int16 = 0 To mVar_DtGrid.RowCount - 1
                mVar_DtGrid.Rows(i).Cells(0).Value = True
            Next

            Return True

        Catch ex As System.Exception

            Return False
        End Try

    End Function

#End Region

End Class
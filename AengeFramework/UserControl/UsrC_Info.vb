Public Class UsrC_Info

#Region "----- Documentação -----"

    '==============================================================================================================================================
    'Projeto : CadSolution 1.0
    'Empresa : Thorx Sistemas e Consultoria Ltda
    'Reponsável criação : Raul Antonio Fernandes Junior    
    'Data de criação : 09/08/2011
    'Objetivo : Formulário responsável pela visualização das informações através do usercontrol
    '
    'Informações adicionais - Tratamento de Erros :
    'UsrC_Info001 - 
    'UsrC_Info002 - 
    'UsrC_Info003 - 
    'UsrC_Info004 - 
    'UsrC_Info005 - 
    'UsrC_Info006 - 
    'UsrC_Info007 - 
    'UsrC_Info008 - 
    'UsrC_Info009 - 
    'UsrC_Info010 - 
    'UsrC_Info011 - 
    'UsrC_Info012 - 
    'UsrC_Info013 - 
    'UsrC_Info014 - 
    'UsrC_Info015 - 
    '==============================================================================================================================================

#End Region

#Region "----- Atribute and Declare -----"

    Private Shared LibraryReference As LibraryReference, GetAppInstall As String, LibraryRegister As LibraryRegister, LibraryCad As LibraryCad
    Private Shared mVar_NameClass As String = "UsrC_Info", _TableName As String = "", mVar_AllProjects As Boolean
    Private Shared mVar_DtRegister As System.Data.DataTable, mVar_TypeForm As String

#End Region

#Region "----- Constructors -----"

    Public Sub New(ByVal TypeForm As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        mVar_TypeForm = TypeForm
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        LibraryCad = Nothing : LibraryReference = Nothing : LibraryRegister = Nothing
    End Sub

#End Region

#Region "----- Get and Set -----"

    Property DtRegister() As System.Data.DataTable
        Get
            DtRegister = mVar_DtRegister
        End Get
        Set(ByVal value As System.Data.DataTable)
            mVar_DtRegister = value
        End Set
    End Property

#End Region

#Region "----- Events -----"

#Region "----- Form -----"

    Private Sub UsrC_Info_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        _UsrC_Info = Nothing
    End Sub

    Private Sub UsrC_Info_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Select Case mVar_TypeForm
            Case "quadro"
                functionQdc()

        End Select

    End Sub

#End Region

#End Region

#Region "----- Functions for consulting and register -----"

    Function FunctionQdc() As Object

        'Firts, clear a datagridview
        dgRegister.DataSource = Nothing

        Dim DtView As DataView = Nothing
        DtRegister.DefaultView.Sort = "Quadro, Circuito ASC"

        dgRegister.DataSource = DtRegister
        'Ajust all columns in datagridview
        With dgRegister
            .Columns(0).Width = 50
            .Columns(1).Width = 50
            .Columns(2).Width = 70
            .Columns(2).DefaultCellStyle.Format = "0.00"
            .Columns(3).Width = 70
            .Columns(3).DefaultCellStyle.Format = "0.00"
            .Columns(4).Width = 70
            .Columns(4).DefaultCellStyle.Format = "0.00"
        End With

        'Now, configure datagridview for colors
        Dim QdcTactual As String = ""
        Dim IndexColor As Integer = 1

        For Each Dr As System.Windows.Forms.DataGridViewRow In dgRegister.Rows

            If Not Dr.Cells("Quadro").Value Is Nothing Then
                If QdcTactual = Dr.Cells("Quadro").Value.ToString Then
                    Select Case IndexColor
                        Case 0
                            Dr.DefaultCellStyle.BackColor = Drawing.Color.White

                        Case 1
                            Dr.DefaultCellStyle.BackColor = Drawing.Color.Gainsboro
                    End Select

                Else
                    'Control color 
                    If IndexColor = 1 Then
                        IndexColor = 0
                    Else
                        IndexColor = 1
                    End If

                    QdcTactual = Dr.Cells("Quadro").Value.ToString
                    Select Case IndexColor
                        Case 0
                            Dr.DefaultCellStyle.BackColor = Drawing.Color.White

                        Case 1
                            Dr.DefaultCellStyle.BackColor = Drawing.Color.Gainsboro

                    End Select
                End If
            End If

        Next

        Return True

    End Function

#End Region

End Class

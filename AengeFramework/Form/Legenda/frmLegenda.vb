Imports System.Windows.Forms
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.Colors
Imports System.Drawing

Public Class frmLegenda

#Region "----- Documentation -----"

    '==============================================================================================================================================
    'Projeto : frmLegenda
    'Empresa : Autoenge Brasil Ltda
    'Reponsável criação : Raul Antonio Fernandes Junior    
    'Data de criação : 30/10/2012
    'Objetivo : Form for create and set all info about legend
    '
    'Informações adicionais :
    'Aenge_GetCfg01 - GetInfoLegenda
    'Aenge_GetCfg02 - GetInfoLayer 
    'Aenge_GetCfg03 - GetInfoLineType
    'Aenge_GetCfg04 - GetColorAcad
    'Aenge_GetCfg05 - frmLegenda_Load
    'Aenge_GetCfg06 - 
    'Aenge_GetCfg07 - 
    'Aenge_GetCfg08 - 
    'Aenge_GetCfg09 - 
    'Aenge_GetCfg010 - 
    '==============================================================================================================================================

#End Region

#Region "----- Atributes and Declarations -----"

    Private Shared mVar_Ok As String, mVar_Selecao As String, mVar_Layer As String, mVar_Tomada As String, mVar_Tub1 As String, mVar_Tub2 As String
    Private Shared mVar_Tub3 As String, mVar_Tub4 As String, mVar_Titulo As String, mVar_Cab_Legenda As String, TypeLegenda As String = ""
    Private Shared LibraryError As LibraryError, mVar_NameClass As String = "frmLegenda"

    Public TypeFileCfg As String, Selecao As String = "", Tomada As String = ""

    Const ByLayer As String = "ByLayer"

    'Control for status of rdoTomada
    Dim StatusTomada As Boolean, ValidateStatus As Boolean, LibraryReference As LibraryReference, Directory As String, ProjectTactual As String, DwgTactual As String
    Dim FieldTomada As String = "TOMADA", FieldInstub As String = "INSTUB", FieldInsOutTub As String = "INSOUTTUB", FieldTitulo As String = "TITULO"
    Dim FieldCor1 As String = "COR1", FieldLin1 As String = "LIN1", FieldDesc1 As String = "DESC1", FieldObs As String = "OBS", OK As String = "NAO"
    Dim FieldCor2 As String = "COR2", FieldLin2 As String = "LIN2", FieldDesc2 As String = "DESC2"
    Dim FieldCor3 As String = "COR3", FieldLin3 As String = "LIN3", FieldDesc3 As String = "DESC3", FieldObs1 As String = "OBS", FieldObs2 As String = "OBS", FieldObs3 As String = "OBS"
    Dim FieldObs4 As String = "OBS", FieldObs5 As String = "OBS", FieldObs6 As String = "OBS", FieldObs7 As String = "OBS", FieldObs8 As String = "OBS"
    Dim FieldTodos As String = "TODOS", FieldEletrica As String = "HIDRAULICA", FieldSelecao As String = "SELECAO", FieldTelefone As String = "TELEFONE", FieldLisLayer As String = "LISLAYER"

    Dim TextField_Tub As String = "Digite a descrição para liberar..."
    Dim ColorTextField_TubInactive As Drawing.Color = Drawing.Color.DarkGray, ColorTextField_TubActive As Drawing.Color = Drawing.Color.Black
    Dim IndexColorNeutro As Int16 = -1

#End Region

#Region "----- Get and Set -----"

    Property Titulo() As String
        Get
            Titulo = mVar_Titulo
        End Get
        Set(ByVal value As String)
            mVar_Titulo = value
        End Set
    End Property

    Property btn_Ok() As String
        Get
            btn_Ok = mVar_Ok
        End Get
        Set(ByVal value As String)
            mVar_Ok = value
        End Set
    End Property

    Property btn_Select() As String
        Get
            btn_Select = mVar_Selecao
        End Get
        Set(ByVal value As String)
            mVar_Selecao = value
        End Set
    End Property

    Property Cab_Legenda() As String
        Get
            Cab_Legenda = mVar_Cab_Legenda
        End Get
        Set(ByVal value As String)
            mVar_Cab_Legenda = value
        End Set
    End Property

#End Region

#Region "----- Constructor -----"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        LibraryError = New LibraryError
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Protected Overrides Sub Finalize()
        LibraryReference = Nothing : LibraryError = Nothing
        MyBase.Finalize()
    End Sub

#End Region

#Region "----- Events -----"

#Region "----- Form -----"

    Private Sub frmLegenda_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frm_Legenda = Nothing
    End Sub

    Private Sub frmLegenda_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim LibraryComponet As New Aenge.Library.Component.LibraryComponent
            LibraryComponet.ConfigFont_Component(frm_Legenda)
            LibraryComponet = Nothing

            If LibraryReference Is Nothing Then LibraryReference = New LibraryReference

            With LibraryReference
                Directory = .ReturnPathApplication
                ProjectTactual = .Return_TactualProject
                DwgTactual = .Return_TactualDrawing
            End With

            'This variable is the same of Cab, only create for set values in GetInfoLegenda 
            TypeLegenda = mVar_Cab_Legenda
            'Get all info before load legend data 
            GetInfoLayer()
            GetInfoLineType()

            'Format form for the legend type called from user
            'Select Case mVar_Cab_Legenda
            '    'Eletric 
            '    Case "INSERCAO DE LEGENDA"
            LblTitulo.Text = "Inserção de legendas"

            'Automação
            'Case "INSERCAO DE LEGENDA AUTOMACAO"
            '    optTelefonia.Text = "Automação"
            '    LblTitulo.Text = "Inserção de legendas - Automação"
            '    FieldTelefone = "AUTOMACAO"

            '    'Pararaios
            'Case "INSERCAO DE LEGENDA PARARAIO"
            '    optTodos.Text = "Para-Raios"
            '    LblTitulo.Text = "Inserção de legendas - Para-raios"
            '    rdoTomada.Visible = False : optEletrica.Visible = False : optTelefonia.Visible = False : optLayer.Visible = False
            '    listLayer.Visible = False

            '    'Set fields equals cfg
            '    FieldTomada += "PAR" : FieldInstub += "PAR" : FieldInsOutTub += "PAR" : FieldTitulo += "PAR"
            '    FieldCor1 += "PAR" : FieldLin1 += "PAR" : FieldDesc1 += "PAR" : FieldObs += "PAR" : OK += "PAR"
            '    FieldCor2 += "PAR" : FieldLin2 += "PAR" : FieldDesc2 += "PAR"
            '    FieldCor3 += "PAR" : FieldLin3 += "PAR" : FieldDesc3 += "PAR" : FieldObs1 += "PAR" : FieldObs2 += "PAR" : FieldObs3 += "PAR"
            '    FieldObs4 += "PAR" : FieldObs5 += "PAR" : FieldObs6 += "PAR" : FieldObs7 += "PAR" : FieldObs8 += "PAR"
            '    FieldTodos += "PAR" : FieldEletrica += "PAR" : FieldSelecao += "PAR" : FieldTelefone += "PAR" : FieldLisLayer += "PAR"

            '    'Cabeamento
            'Case "INSERCAO DE LEGENDA NET"
            '    LblTitulo.Text = "Inserção de legendas - Cabeamento"
            '    optEletrica.Text = "Lógica" : optLayer.Visible = False
            '    listLayer.Visible = False

            '    'Set fields equals cfg
            '    FieldTomada += "NET" : FieldInstub += "NET" : FieldInsOutTub += "NET" : FieldTitulo += "NET"
            '    FieldCor1 += "NET" : FieldLin1 += "NET" : FieldDesc1 += "NET" : FieldObs += "NET" : OK += "NET"
            '    FieldCor2 += "NET" : FieldLin2 += "NET" : FieldDesc2 += "NET"
            '    FieldCor3 += "NET" : FieldLin3 += "NET" : FieldDesc3 += "NET" : FieldObs1 += "NET" : FieldObs2 += "NET" : FieldObs3 += "NET"
            '    FieldObs4 += "NET" : FieldObs5 += "NET" : FieldObs6 += "NET" : FieldObs7 += "NET" : FieldObs8 += "NET"
            '    FieldTodos += "NET" : FieldEletrica = "LOGICANET" : FieldSelecao += "NET" : FieldTelefone += "NET" : FieldLisLayer += "NET"

            '    'Segurança
            'Case "INSERCAO DE LEGENDA SECURITY"
            '    LblTitulo.Text = "Inserção de legendas - Segurança"
            '    optEletrica.Text = "Alarme" : optTelefonia.Text = "Circuito" : optLayer.Visible = False : listLayer.Visible = False

            '    'Set fields equals cfg
            '    FieldTomada += "SEC" : FieldInstub += "SEC" : FieldInsOutTub += "SEC" : FieldTitulo += "SEC"
            '    FieldCor1 += "SEC" : FieldLin1 += "SEC" : FieldDesc1 += "SEC" : FieldObs += "SEC" : OK += "SEC"
            '    FieldCor2 += "SEC" : FieldLin2 += "SEC" : FieldDesc2 += "SEC"
            '    FieldCor3 += "SEC" : FieldLin3 += "SEC" : FieldDesc3 += "SEC" : FieldObs1 += "SEC" : FieldObs2 += "SEC" : FieldObs3 += "SEC"
            '    FieldObs4 += "SEC" : FieldObs5 += "SEC" : FieldObs6 += "SEC" : FieldObs7 += "SEC" : FieldObs8 += "SEC"
            '    FieldTodos += "SEC" : FieldEletrica = "ALARMESEC" : FieldSelecao += "SEC" : FieldTelefone = "CIRCUITOTVSEC" : FieldLisLayer += "SEC"

            'End Select

            'Now, load all information about legend selected
            GetInfoLegenda()

            'Verify if exists text for fields in tubulation 
            If Desc1.Text.Trim = "" Or Desc1.Text = TextField_Tub Then
                Desc1.ForeColor = ColorTextField_TubInactive : Desc1.Text = TextField_Tub
            Else
                Desc1.ForeColor = ColorTextField_TubActive
            End If

            If Desc2.Text.Trim = "" Or Desc2.Text = TextField_Tub Then
                Desc2.ForeColor = ColorTextField_TubInactive : Desc2.Text = TextField_Tub
            Else
                Desc2.ForeColor = ColorTextField_TubActive
            End If

            If Desc3.Text.Trim = "" Or Desc3.Text = TextField_Tub Then
                Desc3.ForeColor = ColorTextField_TubInactive : Desc3.Text = TextField_Tub
            Else
                Desc3.ForeColor = ColorTextField_TubActive
            End If

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error loading legend form - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmLegenda_Load")
        End Try

    End Sub

#End Region

#Region "----- CommandButton -----"

    Private Sub cmdHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdHelp.Click
        CallHelp()
    End Sub

    Private Sub cmdDesenhar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDesenhar.Click
        SetValues_SelectObjects("Draw")
    End Sub

    Private Sub btnSelecao_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelecao.Click

        Dim Doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        cmdDesenhar.Enabled = True
        OK = "NAO" : Selecao = "SELECAO"

        'Anteriormente era este o comando, agora é setado no cfg do desenho
        'ReadTxt "", Ok, Selecao, Layer, Tomada, Tub1, Tub2, Tub3, Tub4, Titulo
        SetValues_SelectObjects()
        Me.Hide()

        'Select Case TypeLegenda
        '    Case "INSERCAO DE LEGENDA"
        Doc.SendStringToExecute("(vba_ssget " & Chr(34) & "HIDRAULICA" & Chr(34) & ")" & Chr(13), True, False, False)

        'Case "INSERCAO DE LEGENDA NET"
        '    Doc.SendStringToExecute("(vba_ssget " & Chr(34) & "NET" & Chr(34) & ")" & Chr(13), True, False, False)

        'Case "INSERCAO DE LEGENDA SECURITY"
        '    Doc.SendStringToExecute("(vba_ssget " & Chr(34) & "SECURITY" & Chr(34) & ")" & Chr(13), True, False, False)

        'Case "INSERCAO DE LEGENDA PARARAIO"
        '    Doc.SendStringToExecute("(vba_ssget " & Chr(34) & "PARARAIO" & Chr(34) & ")" & Chr(13), True, False, False)

        'Case Else
        '    Doc.SendStringToExecute("(vba_ssget " & Chr(34) & "HIDRAULICA" & Chr(34) & ")" & Chr(13), True, False, False)

        'End Select

    End Sub

    Private Sub btnCor1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCor1.Click
        GetColorAcad(colortub1, True, IndexColorNeutro)
    End Sub

    Private Sub btnCor2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCor2.Click
        GetColorAcad(colortub2, True, IndexColorNeutro)
    End Sub

    Private Sub btnCor3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCor3.Click
        GetColorAcad(colortub3, True, IndexColorNeutro)
    End Sub

    Private Sub cmdClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClose.Click
        SetValues_SelectObjects()
        Me.Dispose(True)
    End Sub

#End Region

#Region "----- RadionButton -----"

    Private Sub rdoTubulacaoin_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoTubulacaoin.CheckedChanged
        If rdoTubulacaoin.Checked Then
            Desc1.Enabled = True : Desc2.Enabled = True : Desc3.Enabled = True
        Else
            Desc1.Enabled = False : Desc2.Enabled = False : Desc3.Enabled = False
        End If
    End Sub

    Private Sub rdoTomada_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoTomada.CheckedChanged
        If ValidateStatus = False Then Exit Sub
        If rdoTomada.Checked Then
            Tomada = "SIM" : StatusTomada = True
        Else
            Tomada = "NAO" : StatusTomada = False
        End If
    End Sub

#End Region

#Region "----- OptionButton -----"

    Private Sub optEletrica_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optEletrica.CheckedChanged
        rdoTomada.Enabled = True
        cmdDesenhar.Enabled = True
        Selecao = "HIDRAULICA"
        btnSelecao.Enabled = False : listLayer.Enabled = False
    End Sub

    Private Sub optLayer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optLayer.CheckedChanged
        rdoTomada.Enabled = True
        cmdDesenhar.Enabled = True
        Selecao = "LAYER"
        btnSelecao.Enabled = False : listLayer.Enabled = True
    End Sub

    Private Sub optSelecao_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSelecao.CheckedChanged
        rdoTomada.Enabled = True
        'cmdDesenhar.Enabled = False
        Selecao = "SELECAO"
        btnSelecao.Enabled = True : listLayer.Enabled = False
    End Sub

    Private Sub optTelefonia_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTelefonia.CheckedChanged
        ValidateStatus = False
        'StatusTomada = rdoTomada.Value
        rdoTomada.Checked = False
        cmdDesenhar.Enabled = True
        Selecao = "TELEFONIA"
        btnSelecao.Enabled = False : listLayer.Enabled = False
        ValidateStatus = True
    End Sub

    Private Sub optTodos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optTodos.CheckedChanged
        rdoTomada.Enabled = True
        cmdDesenhar.Enabled = True
        Selecao = "TODOS"
        btnSelecao.Enabled = False : listLayer.Enabled = False
    End Sub

#End Region

#Region "----- TextBox -----"

    Private Sub Desc1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Desc1.GotFocus
        If Desc1.Text = TextField_Tub Then Desc1.Text = ""
    End Sub

    Private Sub Desc1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Desc1.Leave
        If Desc1.Text = "" Then Desc1.Text = TextField_Tub
    End Sub

    Private Sub Desc1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Desc1.TextChanged

        If Desc1.Text.Trim = "" Or Desc1.Text = TextField_Tub Then
            Linetype1.Enabled = False : btnCor1.Enabled = False
            Desc1.ForeColor = ColorTextField_TubInactive

            If Not Desc1.Focused Then Desc1.Text = TextField_Tub

            'If colortub1.Text = "" Then
            '    GetColorAcad(colortub1, False, IndexColorNeutro)
            'Else
            '    If colortub1.Text < 0 Then GetColorAcad(colortub1, False, IndexColorNeutro) 'colortub1.SetSelColorIndex (0)
            'End If

            If Linetype1.Text = "" Then Linetype1.Text = ByLayer
        Else
            Linetype1.Enabled = True : btnCor1.Enabled = True
            Desc1.ForeColor = ColorTextField_TubActive
        End If

    End Sub

    Private Sub Desc2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Desc2.GotFocus
        If Desc2.Text = TextField_Tub Then Desc2.Text = ""
    End Sub

    Private Sub Desc2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Desc2.Leave
        If Desc2.Text = "" Then Desc2.Text = TextField_Tub
    End Sub

    Private Sub Desc2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Desc2.TextChanged

        If Desc2.Text.Trim = "" Or Desc2.Text = TextField_Tub Then
            Linetype2.Enabled = False : btnCor2.Enabled = False
            Desc2.ForeColor = ColorTextField_TubInactive

            If Not Desc2.Focused Then Desc2.Text = TextField_Tub

            'If colortub2.Text = "" Then
            '    GetColorAcad(colortub2, False, IndexColorNeutro)
            'Else
            '    If colortub2.Text < 0 Then GetColorAcad(colortub2, False, IndexColorNeutro) 'colortub1.SetSelColorIndex (0)
            'End If

            If Linetype2.Text = "" Then Linetype2.Text = ByLayer
        Else
            Linetype2.Enabled = True : btnCor2.Enabled = True
            Desc2.ForeColor = ColorTextField_TubActive
        End If

    End Sub

    Private Sub Desc3_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Desc3.GotFocus
        If Desc3.Text = TextField_Tub Then Desc3.Text = ""
    End Sub

    Private Sub Desc3_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Desc3.Leave
        If Desc3.Text = "" Then Desc3.Text = TextField_Tub
    End Sub

    Private Sub Desc3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Desc3.TextChanged

        If Desc3.Text.Trim = "" Or Desc3.Text = TextField_Tub Then
            Linetype3.Enabled = False : btnCor3.Enabled = False
            Desc3.ForeColor = ColorTextField_TubInactive
            colortub3.Enabled = False

            If Not Desc3.Focused Then Desc3.Text = TextField_Tub

            'If colortub3.Text = "" Then
            '    GetColorAcad(colortub3, False, IndexColorNeutro)
            'Else
            '    If colortub3.Text < 0 Then GetColorAcad(colortub3, False, IndexColorNeutro) 'colortub1.SetSelColorIndex (0)
            'End If

            If Linetype3.Text = "" Then Linetype3.Text = ByLayer
        Else
            Linetype3.Enabled = True : btnCor3.Enabled = True
            Desc3.ForeColor = ColorTextField_TubActive
        End If

    End Sub

#End Region

#End Region

#Region "----- AutoCad functions - All Cad procedures -----"

    'Get all layers
    Function GetInfoLayer() As Object

        Dim LibraryCad As New LibraryCad, LLayer As Object

        Try

            listLayer.Items.Clear()
            LLayer = LibraryCad.LayersToList

            For Each Lay As Object In LLayer
                listLayer.Items.Add(Lay.ToString)
            Next

            'Set colorindex 0 for the component color
            GetColorAcad(colortub1, False, IndexColorNeutro)
            colortub1.Text = "" '
            GetColorAcad(colortub2, False, IndexColorNeutro)
            colortub2.Text = ""
            GetColorAcad(colortub3, False, IndexColorNeutro)
            colortub3.Text = ""

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error GetInfLay - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "Aenge_GetCfg02")
        Finally
            LibraryCad = Nothing
        End Try

        Return Nothing

    End Function

    Function GetInfoLineType() As Object

        Dim LibraryCad As New LibraryCad, LLine As Object

        Try

            Linetype1.Items.Clear()
            Linetype2.Items.Clear()
            Linetype3.Items.Clear()
            LLine = LibraryCad.LineTypeToList

            For Each Lin As Object In LLine
                Linetype1.Items.Add(Lin.ToString)
                Linetype2.Items.Add(Lin.ToString)
                Linetype3.Items.Add(Lin.ToString)
            Next

            Linetype1.SelectedIndex = 0 : Linetype2.SelectedIndex = 0 : Linetype3.SelectedIndex = 0

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error GetInfLINT - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "Aenge_GetCfg03")
        Finally
            LibraryCad = Nothing
        End Try

        Return Nothing

    End Function

    Function GetColorAcad(ByVal txtColorTub As TextBox, ByVal LoadForm As Boolean, Optional ByVal ColorIndex As Long = 0, Optional ByVal ColorNameSel As String = "") As Object

        Dim Doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Dim Db As Database = Doc.Database
        Dim Ed As Editor = Doc.Editor
        'Dim Cd As ColorDialog = New ColorDialog
        Dim Cd As Autodesk.AutoCAD.Windows.ColorDialog = New Autodesk.AutoCAD.Windows.ColorDialog  'ColorDialog
        Dim Dr As System.Windows.Forms.DialogResult
        Dim ColorWin As Drawing.Color, NColor As Object, ColorCad As Autodesk.AutoCAD.Colors.Color

        Try

            If LoadForm Then
                Dr = Cd.ShowDialog
                If Dr = System.Windows.Forms.DialogResult.OK Then
                    ColorCad = Cd.Color
                    ColorWin = ColorCad.ColorValue
                    NColor = ColorWin.ToArgb
                    ColorCad = Autodesk.AutoCAD.Colors.Color.FromColor(ColorWin)
                    txtColorTub.BackColor = ColorWin : txtColorTub.Text = ColorCad.ColorIndex.ToString : txtColorTub.ForeColor = ColorWin : txtColorTub.Refresh()
                End If

                'Set colors direct info
            Else
                ColorCad = Autodesk.AutoCAD.Colors.Color.FromColorIndex(ColorMethod.ByColor, ColorIndex)
                ColorWin = ColorCad.ColorValue 'Drawing.Color.FromArgb(ColorIndex)
                txtColorTub.BackColor = ColorWin : txtColorTub.ForeColor = ColorWin
            End If

            Return True

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    'Get the color of cad and open then form for select.
    Function GetColorAcad_Old(ByVal txtColorTub As TextBox, ByVal LoadForm As Boolean, Optional ByVal ColorIndex As Long = 0) As Object

        Dim pColor As Long                  ' picked color from windows palette - long before alter for variant

        'TERMINAR RAUL - DOCUMENTADA A LINHA SOMENTE PARA TRATAMENTO DOS DADOS 
        Dim AcadColor_Selected As Object = Nothing  'New Autodesk.AutoCAD.Interop.Common.AcadAcCmColor       'AcadAcCmColor
        Dim ResultColor As Object = Nothing

        Try

            If LoadForm = True Then
                Dim Cd As ColorDialog = New ColorDialog()
                Cd.AllowFullOpen = True
                Cd.ShowDialog()
                ResultColor = Cd.Color
                pColor = DirectCast(ResultColor, System.Drawing.Color).ToArgb
            Else
                pColor = ColorIndex
                'AcadColor_Selected.ColorIndex = pColor
            End If

            'set color method to enable grabbing colour by colour index

            'TERMINAR RAUL - DOCUMENTADA A LINHA SOMENTE PARA TRATAMENTO DOS DADOS 
            'AcadColor_Selected.ColorMethod = Autodesk.AutoCAD.Interop.Common.AcColorMethod.acColorMethodByACI  'Autodesk.AutoCAD.Interop.AcadUtility.acColorMethodByACI
            'Set retcol from surface point style
            'AcadColor_Selected.ColorIndex = pColor
            'Set the windows color for component colortub

            'TERMINAR RAUL - DOCUMENTADA A LINHA SOMENTE PARA TRATAMENTO DOS DADOS 
            If ColorIndex = 0 Then
                'txtColorTub.BackColor = RGB(AcadColor_Selected.Red, AcadColor_Selected.Green, AcadColor_Selected.Blue)
            Else
                txtColorTub.BackColor = CType(ResultColor, System.Windows.Forms.ColorDialog).Color   'RGB(AcadColor_Selected.red, AcadColor_Selected.green, AcadColor_Selected.blue)
            End If

            txtColorTub.Text = pColor 'AcadColor_Selected.ColorIndex

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error GetClCad - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "")
        End Try

        Return Nothing

    End Function

#End Region

#Region "----- Functions get and set values parameters -----"

    'For form configurate to legend eletric 
    Function SetValues_SelectObjects(Optional ByVal TypeAction As String = "") As Boolean

        Dim PathCfgDwg As String, Count As Int16
        PathCfgDwg = Directory & ProjectTactual & "\" & DwgTactual & ".cfg"
        Dim Todos As String = "0", Eletrica As String = "0", Telefonia As String = "0", Selecao As String = "0", StringLayerSelected As String = ""

        'Set all fields with selection user
        If optTodos.Checked Then Todos = "1"
        If optEletrica.Checked Then Eletrica = "1"
        'Automação / Telefonia 
        If optTelefonia.Checked Then Telefonia = "1"
        If optSelecao.Checked Then Selecao = "1"

        'Validate lislayer field, if visible, then get all layers selecteds
        If listLayer.Visible And optLayer.Checked Then
            If listLayer.SelectedItems.Count <= 0 Then
                MsgBox("Informe pelo menos um layer da lista !", MsgBoxStyle.Information, "Campo inválido")
                listLayer.Focus()
                Return False
            Else
                For Count = 0 To listLayer.Items.Count - 1
                    If listLayer.GetSelected(Count) Then StringLayerSelected += listLayer.Items.Item(Count).ToString & "|"
                Next
            End If
        End If

        Dim NewLegendValue As String = "TRUE"
        If chkLegenda.Checked = False Then NewLegendValue = "FALSE"

        'First, configure form with cablegend
        'Fields not default of form 
        'Select Case mVar_Cab_Legenda
        '    'Not config fields, is default 
        '    Case "INSERCAO DE LEGENDA"
        Aenge_SetCfg(mVar_Cab_Legenda, FieldTodos, Todos, PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, FieldEletrica, Eletrica, PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, FieldSelecao, Selecao, PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, FieldTelefone, Telefonia, PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, FieldLisLayer, StringLayerSelected, PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, "DRWSIM", NewLegendValue, PathCfgDwg)

        '    Case "INSERCAO DE LEGENDA AUTOMACAO"
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldTodos, Todos, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldEletrica, Eletrica, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldSelecao, Selecao, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldTelefone, Telefonia, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldLisLayer, StringLayerSelected, PathCfgDwg)

        ''Insert for pararaios
        '    Case "INSERCAO DE LEGENDA PARARAIO"
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldTodos, Todos, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldEletrica, 0, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldSelecao, Selecao, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldTelefone, 0, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldLisLayer, StringLayerSelected, PathCfgDwg)

        ''Insert for cabling
        '    Case "INSERCAO DE LEGENDA NET"
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldTodos, Todos, PathCfgDwg)
        ''Lógica
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldEletrica, Eletrica, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldSelecao, Selecao, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldTelefone, Telefonia, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldLisLayer, StringLayerSelected, PathCfgDwg)

        'Insert for security
        '    Case "INSERCAO DE LEGENDA SECURITY"
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldTodos, Todos, PathCfgDwg)
        ''Alarme
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldEletrica, Eletrica, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldSelecao, Selecao, PathCfgDwg)
        ''Circuito
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldTelefone, Telefonia, PathCfgDwg)
        'Aenge_SetCfg(mVar_Cab_Legenda, FieldLisLayer, StringLayerSelected, PathCfgDwg)

        'End Select

        If rdoTomada.Checked = True Then
            Aenge_SetCfg(mVar_Cab_Legenda, FieldTomada, 1, PathCfgDwg)
        Else
            Aenge_SetCfg(mVar_Cab_Legenda, FieldTomada, 0, PathCfgDwg)
        End If

        If rdoTubulacao.Checked = True Then
            Aenge_SetCfg(mVar_Cab_Legenda, FieldInstub, 1, PathCfgDwg)
        Else
            Aenge_SetCfg(mVar_Cab_Legenda, FieldInstub, 0, PathCfgDwg)
        End If

        If rdoTubulacaoin.Checked = True Then
            Aenge_SetCfg(mVar_Cab_Legenda, FieldInsOutTub, 1, PathCfgDwg)
        Else
            Aenge_SetCfg(mVar_Cab_Legenda, FieldInsOutTub, 0, PathCfgDwg)
        End If

        mVar_Titulo = txtTitulo.Text
        Aenge_SetCfg(mVar_Cab_Legenda, FieldTitulo, Titulo, PathCfgDwg)

        If rdoTubulacaoin.Checked = True Then
            If Desc1.Text <> "" And Desc1.Text <> TextField_Tub Then
                Aenge_SetCfg(mVar_Cab_Legenda, FieldCor1, colortub1.Text, PathCfgDwg)
                Aenge_SetCfg(mVar_Cab_Legenda, FieldLin1, Linetype1.Text, PathCfgDwg)
                Aenge_SetCfg(mVar_Cab_Legenda, FieldDesc1, Desc1.Text, PathCfgDwg)
            Else
                Aenge_SetCfg(mVar_Cab_Legenda, FieldCor1, IndexColorNeutro, PathCfgDwg)
                Aenge_SetCfg(mVar_Cab_Legenda, FieldLin1, "", PathCfgDwg)
                Aenge_SetCfg(mVar_Cab_Legenda, FieldDesc1, "", PathCfgDwg)
            End If

            If Desc2.Text <> "" And Desc2.Text <> TextField_Tub Then
                Aenge_SetCfg(mVar_Cab_Legenda, FieldCor2, colortub2.Text, PathCfgDwg)
                Aenge_SetCfg(mVar_Cab_Legenda, FieldLin2, Linetype2.Text, PathCfgDwg)
                Aenge_SetCfg(mVar_Cab_Legenda, FieldDesc2, Desc2.Text, PathCfgDwg)
            Else
                Aenge_SetCfg(mVar_Cab_Legenda, FieldCor2, IndexColorNeutro, PathCfgDwg)
                Aenge_SetCfg(mVar_Cab_Legenda, FieldLin2, "", PathCfgDwg)
                Aenge_SetCfg(mVar_Cab_Legenda, FieldDesc2, "", PathCfgDwg)
            End If

            If Desc3.Text <> "" And Desc3.Text <> TextField_Tub Then
                Aenge_SetCfg(mVar_Cab_Legenda, FieldCor3, colortub3.Text, PathCfgDwg)   'GetSelColorIndex
                Aenge_SetCfg(mVar_Cab_Legenda, FieldLin3, Linetype3.Text, PathCfgDwg)
                Aenge_SetCfg(mVar_Cab_Legenda, FieldDesc3, Desc3.Text, PathCfgDwg)
            Else
                Aenge_SetCfg(mVar_Cab_Legenda, FieldCor3, IndexColorNeutro, PathCfgDwg)
                Aenge_SetCfg(mVar_Cab_Legenda, FieldLin3, "", PathCfgDwg)
                Aenge_SetCfg(mVar_Cab_Legenda, FieldDesc3, "", PathCfgDwg)
            End If

            'Clear all info about fields
        Else
            Aenge_SetCfg(mVar_Cab_Legenda, FieldCor1, IndexColorNeutro, PathCfgDwg)
            Aenge_SetCfg(mVar_Cab_Legenda, FieldLin1, "", PathCfgDwg)
            Aenge_SetCfg(mVar_Cab_Legenda, FieldDesc1, "", PathCfgDwg)
            Aenge_SetCfg(mVar_Cab_Legenda, FieldCor2, IndexColorNeutro, PathCfgDwg)
            Aenge_SetCfg(mVar_Cab_Legenda, FieldLin2, "", PathCfgDwg)
            Aenge_SetCfg(mVar_Cab_Legenda, FieldDesc2, "", PathCfgDwg)
            Aenge_SetCfg(mVar_Cab_Legenda, FieldCor3, IndexColorNeutro, PathCfgDwg)
            Aenge_SetCfg(mVar_Cab_Legenda, FieldLin3, "", PathCfgDwg)
            Aenge_SetCfg(mVar_Cab_Legenda, FieldDesc3, "", PathCfgDwg)
        End If

        'Clear all information in txtobs
        Aenge_SetCfg(mVar_Cab_Legenda, (FieldObs & 1).ToString, "", PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, (FieldObs & 2).ToString, "", PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, (FieldObs & 3).ToString, "", PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, (FieldObs & 4).ToString, "", PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, (FieldObs & 5).ToString, "", PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, (FieldObs & 6).ToString, "", PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, (FieldObs & 7).ToString, "", PathCfgDwg)
        Aenge_SetCfg(mVar_Cab_Legenda, (FieldObs & 8).ToString, "", PathCfgDwg)
        Dim ObjStr As Object, I As Int16
        'All info about obs
        I = 1
        For Each ObjStr In txtObservacao.Lines
            If ObjStr.ToString.Trim <> "" Then Aenge_SetCfg(mVar_Cab_Legenda, (FieldObs & I).ToString, ObjStr.ToString, PathCfgDwg)
            I = I + 1
        Next

        'In this case, print legend in dwg
        Dim doc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument

        Me.Hide()
        If TypeAction.ToLower = "draw".ToLower Then
            'Select Case mVar_Cab_Legenda
            '    Case "INSERCAO DE LEGENDA"
            doc.SendStringToExecute("(LEGENDA_OK " & Chr(34) & "HIDRAULICA" & Chr(34) & ")" & Chr(13), True, False, True)   'ELETRICA

            '    Case "INSERCAO DE LEGENDA NET"
            '        doc.SendStringToExecute("(LEGENDA_OK " & Chr(34) & "NET" & Chr(34) & ")" & Chr(13), True, False, True)

            '    Case "INSERCAO DE LEGENDA SECURITY"
            '        doc.SendStringToExecute("(LEGENDA_OK " & Chr(34) & "SECURITY" & Chr(34) & ")" & Chr(13), True, False, True)

            '    Case "INSERCAO DE LEGENDA PARARAIO"
            '        doc.SendStringToExecute("(LEGENDA_OK " & Chr(34) & "PARARAIO" & Chr(34) & ")" & Chr(13), True, False, True)

            '    Case "INSERCAO DE LEGENDA AUTOMACAO"
            '        doc.SendStringToExecute("(LEGENDA_OK " & Chr(34) & "AUTOMACAO" & Chr(34) & ")" & Chr(13), True, False, True)

            '        'So imprime a eletrica se nenhum estiver configurado acima 
            '    Case Else
            '        doc.SendStringToExecute("(LEGENDA_OK " & Chr(34) & "HIDRAULICA" & Chr(34) & ")" & Chr(13), True, False, True)

            'End Select

        End If
        Windows.Forms.Application.DoEvents()
        'End forms 
        Me.Dispose(True)
        Return Nothing
    End Function

#End Region

#Region "----- Functions for consult and return registers -----"

    'Função que obtem as informações de legenda automatica (Titulo e observações no campo para legenda de cada projeto)
    Function GetInfoLegenda()

        Dim PathApp As String, NameProject As String, PathAutoengeCfg As String, NameDgw As String, Count As Int16 = 0
        Dim Todos As String, Eletrica As String, Telefonia As String, Selecao As String, Layers As String = "", DrwSIM As String = "Nil"

        Try

            PathApp = Directory
            NameProject = ProjectTactual
            NameDgw = DwgTactual
            PathAutoengeCfg = PathApp & NameProject & "\" & NameDgw & ".cfg"

            'First, configure form with cablegend
            'Fields not default of form 
            'Select Case mVar_Cab_Legenda
            '    'Not config fields, is default 
            '    Case "INSERCAO DE LEGENDA"
            Me.Text = "Legenda automática"
            Todos = Aenge_GetCfg(TypeLegenda, FieldTodos, PathAutoengeCfg)
            Eletrica = Aenge_GetCfg(TypeLegenda, FieldEletrica, PathAutoengeCfg)
            Telefonia = Aenge_GetCfg(TypeLegenda, FieldTelefone, PathAutoengeCfg)
            Selecao = Aenge_GetCfg(TypeLegenda, FieldSelecao, PathAutoengeCfg)
            Layers = Aenge_GetCfg(TypeLegenda, FieldLisLayer, PathAutoengeCfg)
            DrwSIM = Aenge_GetCfg(TypeLegenda, "DRWSIM", PathAutoengeCfg)

            'Se nao tiver nenhuma informacao seta o padrao
            chkLegenda.Checked = True
            If DrwSIM = "FALSE" Then chkLegenda.Checked = False

            'Set all fields with selection user
            If Todos = "1" Then optTodos.Checked = True
            If Eletrica = "1" Then optEletrica.Checked = True
            If Telefonia = "1" Then optTelefonia.Checked = True
            If Selecao = "1" Then optSelecao.Checked = True
            If Layers.Trim <> "" Then optLayer.Checked = True

            'Case "INSERCAO DE LEGENDA AUTOMACAO"
            '    'Format field 
            '    optTelefonia.Text = "Automação"
            '    Me.Text = "Legenda automática - Automação"

            '    Todos = Aenge_GetCfg(TypeLegenda, FieldTodos, PathAutoengeCfg)
            '    Eletrica = Aenge_GetCfg(TypeLegenda, FieldEletrica, PathAutoengeCfg)
            '    Telefonia = Aenge_GetCfg(TypeLegenda, FieldTelefone, PathAutoengeCfg)
            '    Selecao = Aenge_GetCfg(TypeLegenda, FieldSelecao, PathAutoengeCfg)
            '    Layers = Aenge_GetCfg(TypeLegenda, FieldLisLayer, PathAutoengeCfg)
            '    'Set all fields with selection user
            '    If Todos = "1" Then optTodos.Checked = True
            '    If Eletrica = "1" Then optEletrica.Checked = True
            '    'Automação
            '    If Telefonia = "1" Then optTelefonia.Checked = True
            '    If Selecao = "1" Then optSelecao.Checked = True
            '    If Layers.Trim <> "" Then optLayer.Checked = True

            '    'Insert for pararaios
            'Case "INSERCAO DE LEGENDA PARARAIO"
            '    Me.Text = "Legenda automática - Para-Raios"
            '    optTodos.Text = "Para-Raios"
            '    FieldTodos = "TODOSPAR"
            '    rdoTomada.Visible = False : rdoTomada.Checked = False : optLayer.Checked = False
            '    optEletrica.Checked = False : optEletrica.Visible = False : optTelefonia.Checked = False : optTelefonia.Visible = False : optLayer.Visible = False
            '    listLayer.Visible = False : listLayer.DataSource = Nothing : listLayer.Items.Clear()

            '    'Equal Para-raios in form (checkbox)
            '    Todos = Aenge_GetCfg(TypeLegenda, FieldTodos, PathAutoengeCfg)
            '    Selecao = Aenge_GetCfg(TypeLegenda, FieldSelecao, PathAutoengeCfg)
            '    'Set all fields with selection user (only two)
            '    If Todos = "1" Then optTodos.Checked = True
            '    If Selecao = "1" Then optSelecao.Checked = True
            '    If Layers.Trim <> "" Then optLayer.Checked = True

            '    'Insert for cabling
            'Case "INSERCAO DE LEGENDA NET"
            '    Me.Text = "Legenda automática - Cabeamento"
            '    optEletrica.Text = "Lógica"
            '    FieldTodos = "TODOSNET"
            '    optLayer.Visible = False : listLayer.Visible = False : listLayer.DataSource = Nothing : listLayer.Items.Clear()

            '    Todos = Aenge_GetCfg(TypeLegenda, FieldTodos, PathAutoengeCfg)
            '    'Lógica
            '    Eletrica = Aenge_GetCfg(TypeLegenda, FieldEletrica, PathAutoengeCfg)
            '    Telefonia = Aenge_GetCfg(TypeLegenda, FieldTelefone, PathAutoengeCfg)
            '    Selecao = Aenge_GetCfg(TypeLegenda, FieldSelecao, PathAutoengeCfg)

            '    'Set all fields with selection user
            '    If Todos = "1" Then optTodos.Checked = True
            '    'Lógica
            '    If Eletrica = "1" Then optEletrica.Checked = True
            '    If Telefonia = "1" Then optTelefonia.Checked = True
            '    If Selecao = "1" Then optSelecao.Checked = True
            '    If Layers.Trim <> "" Then optLayer.Checked = True

            '    'Insert for security
            'Case "INSERCAO DE LEGENDA SECURITY"
            '    Me.Text = "Legenda automática - Segurança e Alarme"
            '    optEletrica.Text = "Alarme" : optTelefonia.Text = "Circuito"
            '    FieldTodos = "TODOSSEC"
            '    optLayer.Visible = False : listLayer.Visible = False : listLayer.DataSource = Nothing : listLayer.Items.Clear()

            '    Todos = Aenge_GetCfg(TypeLegenda, FieldTodos, PathAutoengeCfg)
            '    'Lógica
            '    Eletrica = Aenge_GetCfg(TypeLegenda, FieldEletrica, PathAutoengeCfg)
            '    Telefonia = Aenge_GetCfg(TypeLegenda, FieldTelefone, PathAutoengeCfg)
            '    Selecao = Aenge_GetCfg(TypeLegenda, FieldSelecao, PathAutoengeCfg)

            '    'Set all fields with selection user
            '    If Todos = "1" Then optTodos.Checked = True
            '    'Alarme
            '    If Eletrica = "1" Then optEletrica.Checked = True
            '    'Circuito
            '    If Telefonia = "1" Then optTelefonia.Checked = True
            '    If Selecao = "1" Then optSelecao.Checked = True
            '    If Layers.Trim <> "" Then optLayer.Checked = True
            'End Select

            txtTitulo.Text = Aenge_GetCfg(TypeLegenda, FieldTitulo, PathAutoengeCfg)

            If Layers <> "" Then
                For Count = 0 To listLayer.Items.Count - 1
                    If Layers.ToString.Contains(listLayer.Items.Item(Count).ToString & "|") Then listLayer.SetSelected(Count, True)
                Next
            End If

            If rdoTomada.Visible Then
                If Aenge_GetCfg(TypeLegenda, FieldTomada, PathAutoengeCfg) = "1" Then
                    rdoTomada.Checked = True
                Else
                    rdoTomada.Checked = False
                End If
            End If

            'Generic for all legends types 
            If Aenge_GetCfg(TypeLegenda, FieldInstub, PathAutoengeCfg) = "1" Then
                rdoTubulacao.Checked = True
            Else
                rdoTubulacao.Checked = False
            End If

            If Aenge_GetCfg(TypeLegenda, FieldInsOutTub, PathAutoengeCfg) = "1" Then
                rdoTubulacaoin.Checked = True
            Else
                rdoTubulacaoin.Checked = False
            End If

            Dim TextTemp As String = ""
            'Informações sobre as observações
            TextTemp = Aenge_GetCfg(TypeLegenda, (FieldObs1 & "1").ToString, PathAutoengeCfg)
            If TextTemp <> "" Then txtObservacao.Text = TextTemp & Chr(13)
            TextTemp = Aenge_GetCfg(TypeLegenda, (FieldObs1 & "2").ToString, PathAutoengeCfg)
            If TextTemp <> "" Then txtObservacao.Text = txtObservacao.Text & TextTemp & Chr(13)
            TextTemp = Aenge_GetCfg(TypeLegenda, (FieldObs1 & "3").ToString, PathAutoengeCfg)
            If TextTemp <> "" Then txtObservacao.Text = txtObservacao.Text & TextTemp & Chr(13)
            TextTemp = Aenge_GetCfg(TypeLegenda, (FieldObs1 & "4").ToString, PathAutoengeCfg)
            If TextTemp <> "" Then txtObservacao.Text = txtObservacao.Text & TextTemp & Chr(13)
            TextTemp = Aenge_GetCfg(TypeLegenda, (FieldObs1 & "5").ToString, PathAutoengeCfg)
            If TextTemp <> "" Then txtObservacao.Text = txtObservacao.Text & TextTemp & Chr(13)
            TextTemp = Aenge_GetCfg(TypeLegenda, (FieldObs1 & "6").ToString, PathAutoengeCfg)
            If TextTemp <> "" Then txtObservacao.Text = txtObservacao.Text & TextTemp & Chr(13)
            TextTemp = Aenge_GetCfg(TypeLegenda, (FieldObs1 & "7").ToString, PathAutoengeCfg)
            If TextTemp <> "" Then txtObservacao.Text = txtObservacao.Text & TextTemp & Chr(13)
            TextTemp = Aenge_GetCfg(TypeLegenda, (FieldObs1 & "8").ToString, PathAutoengeCfg)
            If TextTemp <> "" Then txtObservacao.Text = txtObservacao.Text & TextTemp & Chr(13)

            Dim Cor1 As String, Cor2 As String, Cor3 As String, Lin1 As String, Lin2 As String, Lin3 As String
            Dim texto_Desc1 As String, texto_Desc2 As String, texto_Desc3 As String

            'Clear all fields of others tub
            Desc1.Text = "" : Desc2.Text = "" : Desc3.Text = ""
            Linetype1.SelectedIndex = 0 : Linetype2.SelectedIndex = 0 : Linetype3.SelectedIndex = 0
            colortub1.Text = "" : colortub2.Text = "" : colortub3.Text = ""
            btnCor1.Enabled = False : btnCor2.Enabled = False : btnCor3.Enabled = False
            ': colortub2.SetSelColorIndex 0: colortub3.SetSelColorIndex 0

            If rdoTubulacaoin.Checked = True Then
                Cor1 = Aenge_GetCfg(TypeLegenda, FieldCor1, PathAutoengeCfg)
                Cor2 = Aenge_GetCfg(TypeLegenda, FieldCor2, PathAutoengeCfg)
                Cor3 = Aenge_GetCfg(TypeLegenda, FieldCor3, PathAutoengeCfg)
                Lin1 = Aenge_GetCfg(TypeLegenda, FieldLin1, PathAutoengeCfg)
                Lin2 = Aenge_GetCfg(TypeLegenda, FieldLin2, PathAutoengeCfg)
                Lin3 = Aenge_GetCfg(TypeLegenda, FieldLin3, PathAutoengeCfg)
                texto_Desc1 = Aenge_GetCfg(TypeLegenda, FieldDesc1, PathAutoengeCfg)
                texto_Desc2 = Aenge_GetCfg(TypeLegenda, FieldDesc2, PathAutoengeCfg)
                texto_Desc3 = Aenge_GetCfg(TypeLegenda, FieldDesc3, PathAutoengeCfg)

                If Lin1 <> "" Then Linetype1.Text = Lin1
                If Lin2 <> "" Then Linetype2.Text = Lin2
                If Lin3 <> "" Then Linetype3.Text = Lin3
                If texto_Desc1 <> "" Then Desc1.Text = texto_Desc1
                If texto_Desc2 <> "" Then Desc2.Text = texto_Desc2
                If texto_Desc3 <> "" Then Desc3.Text = texto_Desc3

                If Desc1.Text <> "" And Desc1.Text <> TextField_Tub Then
                    If Cor1 <> "" Then
                        btnCor1.Enabled = True : Linetype1.Enabled = True : Desc1.Enabled = True
                        If Not IsNumeric(Cor1) Then Cor1 = IndexColorNeutro
                        GetColorAcad(colortub1, False, Integer.Parse(Cor1), Cor1)
                        Dim ColorCad As New Autodesk.AutoCAD.Colors.Color
                        colortub1.Text = Cor1
                    Else
                        btnCor1.Enabled = False : Linetype1.Enabled = False ': Desc1.Enabled = False
                    End If
                End If

                If Desc2.Text <> "" And Desc2.Text <> TextField_Tub Then
                    If Cor2 <> "" Then
                        btnCor2.Enabled = True : Linetype2.Enabled = True : Desc2.Enabled = True
                        If Not IsNumeric(Cor2) Then Cor2 = IndexColorNeutro
                        GetColorAcad(colortub2, False, Integer.Parse(Cor2), Cor2)
                        colortub2.Text = Cor2
                    Else
                        btnCor2.Enabled = False : Linetype2.Enabled = False ': Desc2.Enabled = False
                    End If
                End If

                If Desc3.Text <> "" And Desc3.Text <> TextField_Tub Then
                    If Cor3 <> "" Then
                        btnCor3.Enabled = True : Linetype3.Enabled = True : Desc3.Enabled = True
                        If Not IsNumeric(Cor3) Then Cor3 = IndexColorNeutro
                        GetColorAcad(colortub3, False, Integer.Parse(Cor3), Cor3)
                        colortub3.Text = Cor3
                    Else
                        btnCor3.Enabled = False : Linetype3.Enabled = False ': Desc3.Enabled = False
                    End If
                End If

            End If

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error GetILeg - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "Aenge_GetCfg01")
            Return False
        End Try

        Return True

    End Function

#End Region

End Class
Imports Autodesk.AutoCAD.DatabaseServices
Imports System.Windows.Forms
Imports System.Drawing

Module ModFramework

#Region "----- Atributos e Declarações -----"

    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpSectionName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Private Declare Function LoadString Lib "user32" Alias "LoadStringA" (ByVal hInstance As Long, ByVal wID As Long, ByVal lpBuffer As String, ByVal nBufferMax As Long) As Long

    'Declarações para desabilitar o botão fechar do formulário
    Declare Function GetSystemMenu Lib "User" (ByVal hWnd%, ByVal bRevert%) As Integer
    Declare Function ModifyMenu Lib "User" (ByVal hMenu%, ByVal nPosition%, ByVal wFlags%, ByVal wIDNewItem%, ByVal lpNewItem As VariantType) As Integer

    Public mVar_NameApplication As String = "FrameworkThorx", mVar_NameOwner As String = "AENGE. R.A.F", PathIni As String = "Iniapp.Ini", FileVlx As String = "APWR.vlx", ReturnFormOption As Boolean = False, mVar_NameProjTactual As String = ""
    Private mVar_NameClass As String = "ModFrameworkThorx"

    ' declare a paletteset object, this will only be created once
    Public myPaletteSet, MyPaletteObjectType As Autodesk.AutoCAD.Windows.PaletteSet, NewGuidePalette As New Guid("AAAFFFAA-F34E-4e80-9FB5-8B63CF6D5979")
    Public _UsrC_Project As UsrC_Project, _UsrC_Info As UsrC_Info, _UsrC_General As UsrCGeneral
    Public NamePaletteProject As String = My.Settings.Application & My.Settings.Text_PaletteProject, NamePalettePot As String = My.Settings.Application & My.Settings.Text_PaletteQdc
    Public NamePaletteAgenda As String = My.Settings.Application & My.Settings.Text_PaletteAgenda
    Public NamePaletteAutopower As String = My.Settings.Application & " " & My.Settings.Version, NameFolderApp As String = My.Settings.FolderApp_Name, NameVersionApp As String = My.Settings.Version

    Public frm_Confirm As frmConfirm, frm_Import As frmImport, StringDB As String = "_AHID"
    Public frm_Legenda As frmLegenda, frm_Tarefa As frmTarefa, frm_LegendaNum As frmLegendaNum, frm_CalcHidrante As frmCalcHidrante, frm_CalcHidranteTypes As frmCalcHidranteTypes

    'Datatable for return lisp all potw,va... about qdc
    Public Dt_QdcDraw As System.Data.DataTable, RetornoFrmImport As Object, ArrayReturnGeneral As New ArrayList   'Using all code, for temp informations 
    'Datatable para armazenar os handles das tubulações com os respectivos circuitos, assim poderemos saber e validar a questão do interrup. (retornos) com as fazes e neutros que se encontram na tubulação em questão
    Public Dt_HandleTub_Circ As System.Data.DataTable, DtAgendamento As System.Data.DataTable

    'Call forms and windows 
    Public DbName As String = "AHID"
    Public frm_CircuitoInfo As frmDiagnostic, ArrayHandle_ListaParalelo As String = "", ArrayHandle_ListaRetorno As String = "|", frm_Diagnostic As frmDiagnostic

    'Gerenciamento de documentos e projetos
    'Formulários
    Public frm_Inicial As frmInicial, frm_ExisteDesenho As frmExisteDesenho, frm_NovoDesenho As frmNovoDesenho, frm_Loading As frmLoading, frm_ConfigAbert As frmConfigAbert
    Public frm_LineType As frmLineType, frm_DeleteSymbol As frmDeleteSymbol, frm_EditorSymbol As frmEditorSymbol

    'Variavel geral de retorno ao lisp, sempre deixar ela nothing após a utilização 
    Public ResultBf_Default As ResultBuffer

#End Region

#Region "----- Funcionalidades de Get e Set do arquivo de configuração -----"

    'Função que seta as informações no cfg, de acordo com os parâmetros passados. O CFG está em uma variavel global.
    Function Aenge_SetCfg(ByVal sAppName$, ByVal sKeyName$, ByVal sSetting$, ByVal sFileName$) As Boolean

        Try

            Dim iValid%

            iValid% = WritePrivateProfileString(sAppName$, sKeyName$, sSetting$, sFileName$)
            If iValid% = 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            MsgBox("Erro ao setar as informações do Cfg !", MsgBoxStyle.Information, "Aenge Error")
            Return False
        Finally

        End Try

    End Function

    Function Aenge_GetCfg(ByVal sAppName$, ByVal sKeyName$, ByVal sFileName$)

        Try

            Dim sDefault$, sReturnString$
            Dim iSize%, iValid%, sPath$
            REM
            'By Alessandro Luiz
            REM
            sDefault$ = ""
            sReturnString$ = Space$(260)
            iSize% = Len(sReturnString$)
            '* The return value (assigned to Valid%) represents the number of
            '* characters read into lpReturnString$. Note that the
            '* following assignment must be placed on one line.
            iValid% = GetPrivateProfileString(sAppName$, sKeyName$, sDefault$, sReturnString$, iSize%, sFileName$)

            '*Alteração por causa da referência do Intellicad
            '* Discard the trailing spaces and null character.
            sPath$ = Mid$(sReturnString$, 1, iValid%)

            Return sPath$

        Catch ex As Exception
            MsgBox("Erro ao consultar as informações cadastradas no Cf", MsgBoxStyle.Information, "Aenge Error")
            Return ""
        Finally

        End Try

    End Function

#End Region

#Region "----- Funcionalidades gerais da aplicação - tratamento de string -----"

    Public Function Is64Bit() As Boolean
        If My.Settings.WinVersion.ToString = "64" Then
            Return True
        Else
            Return False
        End If
    End Function

    'Complete with a CharStr a string pass for user 
    Function CompleteString(ByVal Str As String, ByVal Qtd As Integer, ByVal CharStr As String) As String

        Dim StrEnd As String = Str

        Try

            For i = 1 To Qtd - Str.Length
                StrEnd += CharStr
            Next

        Catch ex As Exception
            Return ""
        End Try

        Return StrEnd
    End Function

#End Region

#Region "----- Funcionalidades de combo e outros components -----"

    Public Function SetCurrentNode(ByVal Node0 As String, ByVal Node1 As String, ByVal Trv As TreeView) As Boolean

        If Trv.Nodes.Count <= 0 Then Return False

        'First, we get a node0 focus 
        For Each NodeCurrent As TreeNode In Trv.Nodes
            If NodeCurrent.Text = Node0 Then
                Trv.SelectedNode = NodeCurrent
                Trv.SelectedNode.Checked = True
                For Each NodeCurrentChild As TreeNode In NodeCurrent.Nodes
                    If NodeCurrentChild.Text = Node1 Then
                        Trv.SelectedNode = NodeCurrentChild : Trv.SelectedNode.Checked = True : Trv.SelectedNode.ForeColor = Color.Red
                        Trv.SelectedNode.Parent.ForeColor = Color.Red
                    End If
                Next
                Return True
            End If
        Next

        Return True
    End Function

    'Faz a validação de um campo de acordo com os calores permitidos no mesmo
    Function ConfiguraTxt_ValorValido(ByVal Txt As TextBox, ByVal IsNumber As Boolean, Optional ByVal PermiteMaior1 As Boolean = True, Optional ByVal ValMaximoPermitido As Decimal = 0, Optional ByVal PermiteZero As Boolean = False) As Boolean

        Txt.BackColor = Color.White

        With Txt

            If IsNumber Then

                If Not IsNumeric(Txt.Text) Then
                    Txt.BackColor = Color.LightCoral 'Color.Red
                Else

                    'Permite q o usuário informe o valor zero 
                    If PermiteZero = True Then If Txt.Text = "0" Then Return True

                    If Double.Parse(Txt.Text) <= 0 Then Txt.BackColor = Color.LightCoral
                    'Verifica se permite numeros maiores do que hum (1)
                    If PermiteMaior1 = False Then
                        If Decimal.Parse(Txt.Text) > 1 Then Txt.BackColor = Color.LightCoral
                    Else
                        'Neste caso permite maior que um (1), mas verifica se existe algum limite de valor a ser informado 
                        If ValMaximoPermitido <> 0 Then If Decimal.Parse(Txt.Text) > ValMaximoPermitido Then Txt.BackColor = Color.LightCoral
                    End If

                End If

            Else
                If Txt.Text.Trim = "" Then Txt.BackColor = Color.LightCoral
            End If

        End With

        Return True
    End Function

    ' AutoComplete - Função para o autocompletar do combo
    Public Function AutoComplete(ByRef cb As ComboBox, ByVal e As System.Windows.Forms.KeyPressEventArgs, Optional ByVal blnLimitToList As Boolean = False, Optional ByVal TxtC As TextBox = Nothing, _
    Optional ByVal BloqueiaSelecao As Boolean = False) As Boolean

        'Verifica se são keypress padrões do sistema, Crtl + C, Insert....
        Select Case e.KeyChar.GetHashCode
            Case 851981
                Return False

            Case 196611
                Return False

        End Select

        Dim strFindStr As String

        If e.KeyChar = Chr(8) Then  'Backspace
            If cb.SelectionStart <= 1 Then
                cb.Text = ""
                Return False
            End If

            If cb.SelectionLength = 0 Then
                strFindStr = cb.Text.Substring(0, cb.Text.Length - 1)
            Else
                strFindStr = cb.Text.Substring(0, cb.SelectionStart - 1)
            End If

        Else

            If cb.SelectionLength = 0 Then
                strFindStr = cb.Text & e.KeyChar
            Else
                strFindStr = cb.Text.Substring(0, cb.SelectionStart) & e.KeyChar
            End If

        End If

        Dim intIdx As Integer = -1

        ' Search the string in the Combo Box List.
        intIdx = cb.FindString(strFindStr)

        If intIdx <> -1 Then ' String found in the List.
            cb.SelectedText = ""
            cb.SelectedIndex = intIdx
            cb.SelectionStart = strFindStr.Length
            cb.SelectionLength = cb.Text.Length
            e.Handled = True
            Return True

        Else

            If blnLimitToList = True Then
                e.Handled = True
            Else
                e.Handled = False
            End If

            If Not TxtC Is DBNull.Value And Not TxtC Is Nothing Then TxtC.Text = ""

            Return False
        End If
    End Function

    Function Trata_KeyDown(ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
        If e.KeyCode = 46 Then e.SuppressKeyPress = True
        Return True
    End Function

    'Função que trata os eventos de keypress para validar as informações de entrada de numeros e caracteres
    Function Trata_Keypress(ByVal e As System.Windows.Forms.KeyPressEventArgs, _
                                             Optional ByVal EhMoeda As Boolean = False, _
                                             Optional ByVal PermitePonto As Boolean = True, _
                                             Optional ByVal PermiteVirgula As Boolean = True, Optional ByRef PermiteTracoBarra As Boolean = True, _
                                             Optional ByVal EhCPF As Boolean = False, Optional ByVal PermiteEnter As Boolean = False) As Boolean

        If Not e.KeyChar.GetHashCode.Equals(524296) Then
            If PermiteEnter = False Then If e.KeyChar.GetHashCode.Equals(851981) Then e.KeyChar = Nothing

            If PermitePonto = False Then
                If e.KeyChar = "." Then e.KeyChar = Nothing
            End If

            If e.KeyChar = "'" Then e.KeyChar = Nothing

            'Permite a entrada de barras e traços 
            If PermiteTracoBarra = False Then
                If e.KeyChar = "-" Or e.KeyChar = "/" Or e.KeyChar = "\" Or e.KeyChar = "_" Then e.KeyChar = Nothing
            End If

            If PermiteVirgula = False Then
                If e.KeyChar = "," Then e.KeyChar = Nothing
            End If

            'Faz a validação dos caracteres --> Função abaixo para o tratamento
            If EhMoeda = True Then If Valida_Numericos(e.KeyChar, , EhMoeda) = False Then e.KeyChar = Nothing
        End If

        Return True
    End Function

    'Função que não permite a entrada de caracteres não numéricos em um textbox
    Function Valida_Numericos(ByVal Caracter As String, Optional ByVal txt As TextBox = Nothing, _
                                                             Optional ByVal EhMoeda As Boolean = False, _
                                                             Optional ByVal ValidaPontoVirgula As Boolean = True) As Boolean

        With txt

            'Primeiro, verifica se está validando ponto e virgula
            If ValidaPontoVirgula = True Then
                'Valida os caracteres válidos somente para o tipo moeda
                If EhMoeda = True Then
                    If Caracter = "." Or Caracter = "," Then Return True
                Else
                    If Caracter = "." Or Caracter = "," Then Return True
                End If
            End If

            If Not IsNumeric(Caracter) Then Return False

            'Retorna verdadeiro em caso de numérico
            Return True

        End With

    End Function

#End Region

End Module

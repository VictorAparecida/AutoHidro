Imports Autodesk.AutoCAD.Runtime
Imports Autodesk.AutoCAD.ApplicationServices
Imports Autodesk.AutoCAD.EditorInput
Imports Autodesk.AutoCAD.DatabaseServices
Imports Autodesk.AutoCAD.ComponentModel
'Imports Autodesk.AutoCAD.Interop.Common
Imports System.Runtime.InteropServices

Imports System.IO
Imports System.Text
Imports Autodesk.AutoCAD
Imports Autodesk.AutoCAD.Interop.Common
Imports System.Data.OleDb
Imports Autodesk.AutoCAD.Interop

#Region "----- Documentation ----"

'============================================================================================================
'Módulo : frmAmbiente
'Empresa : Autoenge Brasil Ltda
'Data da criação : Quarta-Feira, 28 de Agosto de 2012
'Analista responsável : Raul Antonio Fernandes Junior
'Descrição : Information about VBA library, all class of VBA olders is here 
'
'
'Id de tratamento de Erros : 
'LibraryVBA001 - Aenge_CodAuto
'LibraryVBA002 - SaveLayerState
'LibraryVBA003 - ReturnCopyPoly
'LibraryVBA004 - ReturnLenTub
'LibraryVBA005 - EditBlock
'LibraryVBA006 - EditAtt_Ang
'LibraryVBA007 - EditColorBlock
'LibraryVBA008 - ReturnData_Circuit
'LibraryVBA009 - ReturnData_LayerObject
'LibraryVBA0010 - ReturnData_Tubulacao
'LibraryVBA0011 - ReturnData_AengeCurva
'LibraryVBA0012 - ReturnData_AengeTipoCurva
'LibraryVBA0013 - Load_AhidPacl
'LibraryVBA0014 - ReturnData_AengeDt
'LibraryVBA0015 - ReturnData_AengeDt2Full
'LibraryVBA0016 - ReturnData_AengeDt2
'LibraryVBA0017 - ReturnData_AengeDt3
'LibraryVBA0018 - Aengedt3_Full
'LibraryVBA0019 - Aengedt3_Intensidade
'LibraryVBA0020 - Aengedt3_Luminoso
'LibraryVBA0021 - Aengedt3_AeleUtil
'LibraryVBA0022 - Aengedt3_AeleReg
'LibraryVBA0023 - Aengedt3_AeleLux
'LibraryVBA0024 - LegendAuto
'LibraryVBA0025 - Initialize_Validate
'LibraryVBA0026 - 
'LibraryVBA0027 - 
'LibraryVBA0028 - 
'LibraryVBA0029 - 
'
'Modificações
'
'============================================================================================================

#End Region

'Seta a instancia do assembly a ser trabalhada
'<Assembly: ExtensionApplication(GetType(LibraryCadUI))> 
<Assembly: CommandClass(GetType(LibraryVBA))> 

Public Class LibraryVBA
    Implements IExtensionApplication

#Region "----- Atribute and declarations -----"

    Dim LibraryReference As LibraryReference, PathDir As String = "", LibraryError As LibraryError, LibraryCad As LibraryCad, LibraryConnection As Aenge.Library.Db.LibraryConnection

    Private Shared mVar_NameClass As String = "LibraryVBA", SecaoQuantitativo As String = "QUANTITATIVO DE MATERIAIS"

    Public Declare Function acedSetColorDialog Lib "acad.exe" (ByVal color As Long, ByVal bAllowMetaColor As Boolean, ByVal nCurLayerColor As Long) As Boolean

#End Region

#Region "----- Construtores -----"

    Public Sub Initialize() Implements Autodesk.AutoCAD.Runtime.IExtensionApplication.Initialize

    End Sub

    Public Sub Terminate() Implements Autodesk.AutoCAD.Runtime.IExtensionApplication.Terminate

    End Sub

    Public Sub New()
        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
        LibraryError = New LibraryError
        PathDir = LibraryReference.ReturnPathApplication
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
        LibraryReference = Nothing : LibraryError = Nothing : LibraryCad = Nothing : LibraryConnection = Nothing
    End Sub

#End Region

#Region "----- Functions for Call dlls and applications external -----"

#Region "----- AutoHidro (Hidráulica gerall) -----"

#Region "----- Menu Inicial -----"

    'Inicialização da aplicação para seleção de novo ou desenho existente 
    <CommandMethod("Load_AhidIni")> _
    Public Sub Load_AhidIni()
        Main("inicial")
    End Sub

    'Inicialização da aplicação para seleção de novo desenho
    <LispFunction("Load_AhidNew")> _
    Function Load_AhidNew(ByVal rbf As ResultBuffer) As ResultBuffer
        Main("new")
        Return Nothing
    End Function

    'Inicialização da aplicação para seleção de novo desenho
    <LispFunction("Load_AhidOpen")> _
    Function Load_AhidOpen(ByVal rbf As ResultBuffer) As ResultBuffer
        Main("open")
        Return Nothing
    End Function

#End Region

#Region "----- Menu Editor de Simbolos -----"

    <LispFunction("load_editorsymbol")> _
    Function Load_EditorSymbol(ByVal rbf As ResultBuffer) As ResultBuffer

        If frm_EditorSymbol Is Nothing Then frm_EditorSymbol = New frmEditorSymbol
        With frm_EditorSymbol
            .ShowDialog()
        End With
        frm_EditorSymbol = Nothing

        Return Nothing
    End Function

#End Region

#Region "----- Menu General -----"

    'Carrega as informacoes de menu 
    <LispFunction("MenuTeste")> _
    Function Load_Menu(ByVal rbf As ResultBuffer) As ResultBuffer

        'creates a new submenu on the Modify menu called "Mymodify"
        ' '' ''Dim oAcad As AcadApplication, IndexCurrent As Integer = 0, SubMenuCurrent As String = "", SubMenuCurrent2 As String = "", SubMenuCurrent3 As String = ""
        ' '' ''Dim oPopup As AcadPopupMenu
        '' '' ''First node of menu
        ' '' ''Dim oSubPopup As AcadPopupMenu = Nothing, oPopupMenuItem As AcadPopupMenuItem = Nothing
        ' '' ''oAcad = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication.ActiveDocument.Application

        ' '' ''For Each oPopup In oAcad.MenuBar
        ' '' ''    With oPopup

        ' '' ''        If oPopup.Name.Contains("horx") Then
        ' '' ''            'Check for item (nivel). In select case verify type of menu
        ' '' ''            'Clear string of submenu
        ' '' ''            SubMenuCurrent = "" : SubMenuCurrent2 = "" : SubMenuCurrent3 = ""
        ' '' ''            'In case of separator
        ' '' ''            'Create a menu item with command
        ' '' ''            .AddMenuItem(IndexCurrent, "Testando", "_Save")
        ' '' ''            .AddSeparator(IndexCurrent)
        ' '' ''        End If

        ' '' ''    End With

        ' '' ''Next
        Return Nothing

    End Function

    <LispFunction("deleteMenuTeste")> _
   Function DeleteMenuThorxCad(ByVal rbf As ResultBuffer) As ResultBuffer

        '' '' ''this macro removes the submenu created by the addMenuItem macro
        ' '' ''Dim oAcad As AcadApplication
        ' '' ''oAcad = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication.ActiveDocument.Application
        ' '' ''Dim oPopup As AcadPopupMenu
        ' '' ''Dim oPopupItem As AcadPopupMenuItem

        ' '' ''For Each oPopup In oAcad.MenuBar
        ' '' ''    'TagString is not localized, so use it to locate the menu
        ' '' ''    If oPopup.Name.Contains("horx") Then
        ' '' ''        'For each item in menu selection
        ' '' ''        For Each oPopupItem In oPopup

        ' '' ''            'Delete item especific
        ' '' ''            'If oPopupItem.TagString = My.Settings.Mnu_PadraoDelete Then
        ' '' ''            '    oPopupItem.Delete()
        ' '' ''            '    Exit For
        ' '' ''            'End If

        ' '' ''            'Delete all itens in menu dinamic
        ' '' ''            oPopupItem.Delete()
        ' '' ''        Next oPopupItem

        ' '' ''        Exit For
        ' '' ''    End If
        ' '' ''Next oPopup
        Return Nothing
    End Function

    'Call help file - Application - Getin settings a name of file do load
    <LispFunction("load_Ahidcfg")> _
    Function Load_AhidCfg(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AhidCfg As New AHIDCONFIG.CDialogAHIDCfg
        AhidCfg.DoModal(True, Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication)
        AhidCfg = Nothing
        Return Nothing
    End Function

    'Call help file - Application - Getin settings a name of file do load
    <LispFunction("Agenda")> _
    Function Load_AhidAgendamento(ByVal rbf As ResultBuffer) As ResultBuffer

        DtAgendamento = Nothing
        DtAgendamento = New System.Data.DataTable

        Dim LibraryReference As New LibraryReference
        Dim ProjectTactual As String = LibraryReference.Return_TactualProject, DwgTactual As String = LibraryReference.Return_TactualDrawing, DirPathIni As String = GetAppInstall() & "Iniapp.ini"
        Dim ModeAgenda As Object, ModeVis As Object, Id As Object = LibraryReference.Return_TactualID

        ModeAgenda = Aenge_GetCfg("Configuration", "Apwrsch_ModeAgend", DirPathIni)
        ModeVis = Aenge_GetCfg("Configuration", "Apwrsch_ModeVis", DirPathIni)
        If ModeAgenda.ToString = "" Then ModeAgenda = 0
        If ModeVis.ToString = "" Then ModeVis = 0
        'Not alert user 
        If Int16.Parse(ModeAgenda) = 1 Then Return Nothing

        'Verify if exists agend for the project
        If ConnAhid Is Nothing Then ValidateConnection()
        Dim Da As New OleDbDataAdapter("Select * From TTarefas Where Id = " & Id, ConnAhid)
        Da.Fill(DtAgendamento)

        'Types od agendamento 
        '0 - Alertar somente quando abrir o projeto/desenho
        '1 - Alertar durante o intervalo de tempo informado
        '2 - Não me alertar - Irei consultar manualmente meus agendamentos

        'Visualiation 
        '0 - Abrir em forma de Palettes do CAD
        '1 - Abrir tela flutuante para visualização
        '2 - Informar por mensagem em tela sobre o agendamento
        If DtAgendamento.Rows.Count > 0 Then

            'Validate how user visualizate this information
            Select Case Int16.Parse(ModeVis)
                Case 0
                    If LibraryCad Is Nothing Then LibraryCad = New LibraryCad
                    LibraryCad.AddPaletteTarefa()

                Case 1
                    If frm_Tarefa Is Nothing Then frm_Tarefa = New frmTarefa
                    With frm_Tarefa
                        .Id_Form = "apwrsch"
                        .DataTable_TreeView = DtAgendamento
                        .FillTreeView_Agendamento(.trvInfo, "", , DtAgendamento)
                        .Show()
                    End With

                Case 2
                    If MsgBox("Existe(m) agendamento(s) de tarefa(s) pendentes a serem visualizados. Deseja visualizar agora ?", MsgBoxStyle.YesNo, "Visualizar tarefas") = MsgBoxResult.No Then Return Nothing
                    Dim ApwrSch As New AHIDSCH.CDialogAgenda
                    ApwrSch.DoModal(True)
                    ApwrSch = Nothing

            End Select

        End If
        Return Nothing
    End Function

    'Inicialização da aplicação para seleção de novo ou desenho existente 
    <LispFunction("Load_AhidPluv")> _
     Function Load_AhidPluv(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AhidPluv As New AHIDPLUV.cDialogAHIDPLUV, Retorno As Object
        Retorno = AhidPluv.DoModal()
        AhidPluv = Nothing

        'Verifica se precisa imprimir
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument, Comando As String = ""
        SetCmdEcho(0)
        Comando = "(printlayout1)"
        If Retorno = 2 Then Doc.SendStringToExecute(Comando & Chr(13), False, False, False)
        Doc = Nothing
        Return Nothing

    End Function

    <LispFunction("Load_AhidBomb")> _
    Function Load_AhidBomb(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AhidBomb As New AHIDBOMB.CDialogBombaRecalque
        AhidBomb.DoModal()
        AhidBomb = Nothing
        Return Nothing
    End Function

    <LispFunction("Load_AhidRinc")> _
    Function Load_AhidRinc(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AhidRinc As New AHIDRINC.cDialogAHIDRINC
        AhidRinc.DoModal()
        AhidRinc = Nothing
        Return Nothing
    End Function

    <LispFunction("Load_AhidCRes")> _
    Function Load_AhidCRes(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AhidCRes As New AHIDCRES.CDialogCalcRes
        Dim Retorno As Object
        Retorno = AhidCRes.DoModal()
        AhidCRes = Nothing

        'Verifica se precisa imprimir
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument, Comando As String = ""
        SetCmdEcho(0)
        Comando = "(printlayout1)"
        If Retorno = 2 Then Doc.SendStringToExecute(Comando & Chr(13), False, False, False)
        Doc = Nothing
        Return Nothing

    End Function

    'Carrega os sprinklers, calculos. Aqui iremos fazer a validacao dos dados para a inserção do mesmo no desenho tb
    <LispFunction("Load_AhidSpri")> _
    Function Load_AhidSPri(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AhidSPri As New AHIDSPRI.cDialogAHIDSPRI
        Dim Retorno As Object = Nothing
        Retorno = AhidSPri.DoModal()
        AhidSPri = Nothing

        'Aqui vamos ver se temos. Precisamos pegar as informações que retornam como array 
        'Se for nothing, nao iremos retornar para a chamada 
        If Not Retorno Is Nothing Then

            If Not Retorno.ToString.Split("").Length > 0 Then Return Nothing
            'Verificamos se é desenhar e se for, iremos voltar a chamada do formulário
            'Aqui se temos as informações, vamos pegar o ponto de insercao e fazer o array do comando
            'Temos as seguintes informacoes de retorno 
            '0) Aqui temos qual o tipo de retorno para gerar, se é de sprinklers ou de tub   0 / Sprinklers --> 1 / Tub
            '1) Distancia X
            '2) Distancia Y
            '3) Area de cobertura
            '4) Numero de sprinklers
            '5) Espaçamento entre sprinklers
            '6) Diametro das colunas / Ramais 
            Dim TipoDesenho As String = "0", DistanciaX As Double = 0, DistanciaY As Double = 0, AreaCobert As Double = 0, NroSprink As Double = 0, EspacSprink As Double = 0, DiamCol As Object, DiamRamais As Object
            Dim ArrayRetorno As Object = Retorno.ToString.Split("|")
            If ArrayRetorno.length >= 1 Then TipoDesenho = ArrayRetorno(0).ToString
            If ArrayRetorno.length >= 2 Then DistanciaX = ArrayRetorno(1)
            If ArrayRetorno.length >= 3 Then DistanciaY = ArrayRetorno(2)
            If ArrayRetorno.length >= 4 Then AreaCobert = ArrayRetorno(3)
            If ArrayRetorno.length >= 5 Then NroSprink = ArrayRetorno(4)
            If ArrayRetorno.length >= 6 Then EspacSprink = ArrayRetorno(5)
            If ArrayRetorno.length >= 7 Then DiamCol = ArrayRetorno(6)
            If ArrayRetorno.length >= 8 Then DiamRamais = ArrayRetorno(7)

            'Podemos ter outros tipos de desenhos, alem de sprinklers
            Select Case TipoDesenho
                Case "0"
                    'Passamos o array inteiro, para nao ter que tratar as informações antes e aumentar código na chamada do formulário
                    If LibraryCad Is Nothing Then LibraryCad = New LibraryCad
                    LibraryCad.SprinkDrawing(Retorno)

            End Select

        End If

        Return Nothing

    End Function

    <LispFunction("Load_AhidCEsg")> _
    Function Load_AhidCEsg(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AhidCesg As New AHIDCESG.CDialogCalculoesgoto, Retorno As Object
        Retorno = AhidCesg.DoModal()
        AhidCesg = Nothing

        'Verifica se precisa imprimir
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument, Comando As String = ""
        SetCmdEcho(0)
        Comando = "(printlayout1)"
        If Retorno = 2 Then Doc.SendStringToExecute(Comando & Chr(13), False, False, False)
        Doc = Nothing

        Return Nothing
    End Function

    <LispFunction("Load_AhidGas")> _
    Function Load_AhidGas(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AHIDGAS As New AHIDGAS.cDialogAHIDGAS, Retorno As Object
        Retorno = AHIDGAS.DoModal()
        AHIDGAS = Nothing

        'Verifica se precisa imprimir
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument, Comando As String = ""
        SetCmdEcho(0)
        Comando = "(printlayout1)"
        If Retorno = 2 Then Doc.SendStringToExecute(Comando & Chr(13), False, False, False)
        Doc = Nothing

        Return Nothing
    End Function

    <LispFunction("Load_AhidCdia")> _
    Function Load_AhidCdia(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AHIDCDIA As New AHIDCDIA.CDialogConsumo
        AHIDCDIA.DoModal()
        AHIDCDIA = Nothing
        Return Nothing
    End Function

    <LispFunction("Load_AhidDimaf")> _
    Function Load_AhidDimaf(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AHIDDIMAF As New AHIDDIMAF.CDialogDimAguaFria, Retorno As Object
        Retorno = AHIDDIMAF.DoModal()
        AHIDDIMAF = Nothing

        'Verifica se precisa imprimir
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument, Comando As String = ""
        SetCmdEcho(0)
        'Comando = "(printlayout4 " & Chr(34) & "YES" & Chr(34) & " " & Chr(34) & "YES" & Chr(34) & " " & Chr(34) & "AGUA FRIA" & Chr(34) & ")"
        Comando = "(printlayout4)"
        If Retorno = 2 Then Doc.SendStringToExecute(Comando & Chr(13), False, False, False)
        Doc = Nothing

        Return Nothing
    End Function

    <LispFunction("Load_AhidDRam")> _
    Function Load_AhidDRam(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AHIDDRAM As New AHIDDRAM.CDialogDimenRamal
        AHIDDRAM.DoModal()
        AHIDDRAM = Nothing
        Return Nothing
    End Function

    <LispFunction("Load_AhidEdif")> _
    Function Load_AhidEdif(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AHIDEDIF As New AHIDEDIF.CDialogEdificacoes
        AHIDEDIF.DoModal()
        AHIDEDIF = Nothing
        Return Nothing
    End Function

    <LispFunction("Load_AhidHidm")> _
    Function Load_AhidHidm(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AHIDHIDM As New AHIDHIDM.CDialogHidrometros
        AHIDHIDM.DoModal()
        AHIDHIDM = Nothing
        Return Nothing
    End Function

    <LispFunction("Load_AhidCsel")> _
    Function Load_AhidCsel(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AHIDCSEL As New AHIDCSEL.CDialogSistElev, Retorno As Object
        Retorno = AHIDCSEL.DoModal()
        AHIDCSEL = Nothing

        'Verifica se precisa imprimir
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument, Comando As String = ""
        SetCmdEcho(0)
        Comando = "(printlayout1)"
        If Retorno = 2 Then Doc.SendStringToExecute(Comando & Chr(13), False, False, False)
        Doc = Nothing

        Return Nothing
    End Function

    <LispFunction("Load_AhidTqsep")> _
    Function Load_AhidTqsep(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AhidTqsep As New AHIDTQSEP.CDialogTqsep, Retorno As Object
        Retorno = AhidTqsep.DoModal()
        AhidTqsep = Nothing

        'Verifica se precisa imprimir
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument, Comando As String = ""
        SetCmdEcho(0)
        Comando = "(printlayout1)"
        If Retorno = 2 Then Doc.SendStringToExecute(Comando & Chr(13), False, False, False)
        Doc = Nothing

        Return Nothing
    End Function

    <LispFunction("Load_AhidThom")> _
    Function Load_AhidThom(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AhidThom As New AHIDTHOM.CDialogThomeu
        AhidThom.DoModal()
        AhidThom = Nothing
        Return Nothing
    End Function

    <LispFunction("Load_AhidSch")> _
    Function Load_AhidSch(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AhidCsh As New AHIDSCH.CDialogAgenda
        AhidCsh.DoModal(False)    'True 
        AhidCsh = Nothing
        Return Nothing
    End Function

    'Call form calc rede de hidrantes 
    <LispFunction("Load_AhidCalcHid")> _
    Function Load_AhidCalcHidr(ByVal rbf As ResultBuffer) As ResultBuffer

        frm_CalcHidrante = New frmCalcHidrante
        With frm_CalcHidrante
            .ShowDialog()
        End With
        frm_CalcHidrante = Nothing

        Return Nothing
    End Function

    'Call form calc rede de hidrantes - Cadastros diversos de informações 
    <LispFunction("Load_AhidCalcHid_Ocup")> _
    Function Load_AhidCalcHid_Ocup(ByVal rbf As ResultBuffer) As ResultBuffer

        frm_CalcHidranteTypes = New frmCalcHidranteTypes
        With frm_CalcHidranteTypes
            .IdFormat = "ocupacao"
            .ShowDialog()
        End With
        frm_CalcHidranteTypes = Nothing

        Return Nothing
    End Function

#End Region

#Region "----- Menu Arquivos -----"

    'Exclusao de simbolos
    <LispFunction("DeleteSymbol")> _
    Function DeleteSymbol(ByVal Rbf As ResultBuffer) As ResultBuffer

        frm_DeleteSymbol = New frmDeleteSymbol
        With frm_DeleteSymbol
            .ShowDialog()
        End With
        frm_DeleteSymbol = Nothing

        Return Nothing

    End Function

    'Inicialização da aplicação para seleção de novo ou desenho existente 
    <LispFunction("Load_AhidPrjMg")> _
    Function Load_AhidPrjMg(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim AhidPrjMg As New AHIDPRJMG.CDialogPrjManager
        AhidPrjMg.DoModal()
        AhidPrjMg = Nothing
        Return Nothing
    End Function

    'Call help file - Application - Getin settings a name of file do load
    <LispFunction("Chm")> _
    Function Chm(ByVal rbf As ResultBuffer) As ResultBuffer

        'Dim Diretorio As String
        'If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
        'Diretorio = LibraryReference.ReturnPathApplication.ToString
        'If My.Computer.FileSystem.FileExists(Diretorio & My.Settings.Help_FileName.ToString) Then Shell(Diretorio & My.Settings.Help_FileName.ToString, AppWinStyle.NormalFocus)

        CallHelp()
        Return Nothing
    End Function

#End Region

#Region "----- Functions for AengeDt and Similars -----"

    'Return all symbols for print in dwg 
    <LispFunction("ReturnList_LegNum")> _
    Function ReturnList_LegNum(ByVal rBf As ResultBuffer) As ResultBuffer

        Dim ArrayString As New ArrayList, ArrayStringWithProp11 As New ArrayList

        Try

            If ConnAhid Is Nothing Then
                Dim LibConn As New Aenge.Library.Db.LibraryConnection
                With LibConn
                    .TypeDb = "AHID_"
                    ConnAhid = .Aenge_OpenConnectionDB
                End With
                LibConn = Nothing
            End If

            Dim DaTobjBase As New OleDbDataAdapter("Select TObjBase.*, TObjPVal.Prop11 From TObjBase, TObjPVal Where TObjBase.CodObj = TObjPVal.CodObj Order By ObjLeg ASC", ConnAhid)
            Dim Dt As New System.Data.DataTable, DrSelect As DataRow
            Dim DtTObjBase As New System.Data.DataTable
            DaTobjBase.Fill(DtTObjBase)

            ' Create four typed columns in the DataTable.
            With Dt
                .Columns.Add("CodObj", GetType(String))
                .Columns.Add("ObjLeg", GetType(String))
                .Columns.Add("Prop11", GetType(String))
            End With

            For Each Obj As Object In rBf.AsArray
                'For start and end of list 
                If DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5016 And DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5017 Then
                    If Not DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value Is Nothing Then
                        If DtTObjBase.Select("CodObj = '" & DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString.Trim & "'").Length > 0 Then
                            Dim Dr As DataRow
                            Dr = Dt.NewRow
                            DrSelect = DtTObjBase.Select("CodObj = '" & DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString.Trim & "'")(0)
                            Dr("CodObj") = DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString.Trim
                            Dr("ObjLeg") = DrSelect("ObjLeg")
                            Dr("Prop11") = DrSelect("Prop11")
                            Dt.Rows.Add(Dr)
                        End If
                    End If
                End If
            Next

            ResultBf_Default = New ResultBuffer
            frm_LegendaNum = New frmLegendaNum
            With frm_LegendaNum
                .DtGrid = .GridRegister
                .DataObj = Dt
                .ShowDialog()
            End With
            frm_LegendaNum = Nothing

        Catch ex As Exception

            Return Nothing
        End Try

        Return ResultBf_Default

    End Function

    'Return information about prop11 in aelepval 
    <LispFunction("ReturnData_Prop11")> _
    Function ReturnData_Prop11(ByVal rBf As ResultBuffer) As ResultBuffer

        Dim ArrayString As New ArrayList, RbfResult As New ResultBuffer, ArrayStringWithProp11 As New ArrayList

        Try

            For Each Obj As Object In rBf.AsArray
                'For start and end of list 
                If DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5016 And DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5017 Then
                    If Not DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value Is Nothing Then
                        ArrayString.Add(DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString)
                    End If
                End If
            Next

            If ArrayString.Count <= 0 Then
                With RbfResult
                    .Add(New TypedValue(LispDataType.ListBegin))
                    .Add(New TypedValue(LispDataType.Text, Nothing))
                    .Add(New TypedValue(LispDataType.ListEnd))
                End With
                Return RbfResult
            End If

            If ConnAhid Is Nothing Then
                Dim LibraryDb As New Aenge.Library.Db.LibraryConnection
                With LibraryDb
                    .TypeDb = StringDB
                    ConnAhid = .Aenge_OpenConnectionDB
                End With
                LibraryDb = Nothing
            End If

            Dim Da As New OleDbDataAdapter("Select * From TObjPVal", ConnAhid)
            Dim DtObjPVal As New System.Data.DataTable, Prop11 As String = ""
            Da.Fill(DtObjPVal)

            RbfResult.Add(New TypedValue(LispDataType.ListBegin))
            For Each Obj As Object In ArrayString
                If DtObjPVal.Select("CodObj = '" & Obj.ToString.Trim & "'").Length > 0 Then

                    With RbfResult
                        .Add(New TypedValue(LispDataType.ListBegin))
                        Prop11 = "" : Prop11 = DtObjPVal.Select("CodObj = '" & Obj.ToString.Trim & "'")(0)("Prop11").ToString
                        .Add(New TypedValue(LispDataType.Text, Obj.ToString))
                        .Add(New TypedValue(LispDataType.Text, Prop11))
                        .Add(New TypedValue(LispDataType.ListEnd))
                    End With

                End If
            Next
            RbfResult.Add(New TypedValue(LispDataType.ListEnd))

            Return RbfResult

        Catch ex As Exception
            MsgBox("Ocorrência de erro - Pr11 - " & ex.Message, MsgBoxStyle.Information, "Error properties")
            Return Nothing
        End Try

    End Function

    'Return all information of aelebase e aelepval. In this case, not pass anymore in txt aengedt and aengedt2
    <LispFunction("ReturnData_AengeDt")> _
    Function ReturnData_AengeDt(ByVal rBf As ResultBuffer) As ResultBuffer

        Dim rbFResult As New ResultBuffer
        If rBf Is Nothing Then Return Nothing
        If rBf.AsArray(0).Value.ToString() = "" Then Return Nothing

        'Get parametrs for consult
        Dim ArrayInfo As Object, UserS5 As String, FilterUsers5 As String = "", ArrayClass() As String = Nothing, QuantArray As Integer, StrCodClass As String = ""
        Dim CodSymb As String = "", CodClass As String = "", TipoObjeto As String = "", Descricao As String = "", DwgFile As String = "", DwgFileCompl As String = ""
        Dim Prop2 As String = "", Prop3 As String = "", Prop4 As String = "", UserS5_Cad As String = "", DiamConsult As String = ""
        UserS5 = rBf.AsArray(0).Value.ToString()

        Try

            'ReDim ArrayInfo(10)
            ArrayInfo = Split(UserS5, "|")
            QuantArray = UBound(ArrayInfo)

            'Only for autohidro, description in aengedt is different of database, i dont know why....
            'UserS5_Cad = Application.GetSystemVariable("USERS5").ToString

            'Get all columns win select
            If QuantArray >= 0 Then CodSymb = ArrayInfo(0)
            If QuantArray >= 1 Then CodClass = ArrayInfo(1)
            If QuantArray >= 1 Then ArrayClass = Split(CodClass, ";")
            If QuantArray >= 2 Then TipoObjeto = ArrayInfo(2)
            If QuantArray >= 3 Then Descricao = ArrayInfo(3)
            If QuantArray >= 4 Then DwgFile = ArrayInfo(4)
            If QuantArray >= 5 Then DwgFileCompl = ArrayInfo(5)
            If QuantArray >= 6 Then DiamConsult = ArrayInfo(6)

            If CodSymb <> "" Then FilterUsers5 = FilterUsers5 + "AND CodObj = '" & CodSymb & "'"

            'Verify how filter is used - TipoObjeto
            'Lisp can pass more of one class
            Dim ArrayDescClass() As String, ObjDescClass As Object

            If TipoObjeto <> "" Then

                ArrayDescClass = Split(TipoObjeto, ";")
                FilterUsers5 = FilterUsers5 + "AND ( "

                Select Case Trim(Mid(TipoObjeto, 1, 3))
                    Case "*"
                        For Each ObjDescClass In ArrayDescClass
                            FilterUsers5 = FilterUsers5 + " OR ClasDes like '%" & LTrim(Mid(ObjDescClass, 4, 255)) & "'"
                        Next

                    Case "**"
                        For Each ObjDescClass In ArrayDescClass
                            FilterUsers5 = FilterUsers5 + " OR ClasDes like '*" & Mid(ObjDescClass, 4, 255) & "%'"
                        Next

                    Case "***"
                        For Each ObjDescClass In ArrayDescClass
                            FilterUsers5 = FilterUsers5 + " OR ClasDes like '" & RTrim(Mid(ObjDescClass, 4, 255)) & "%'"
                        Next

                        'Consulta de texto exato
                    Case Else
                        For Each ObjDescClass In ArrayDescClass
                            FilterUsers5 = FilterUsers5 + " OR ClasDes = '" & Mid(ObjDescClass, 4, 255) & "'"
                        Next

                End Select

                FilterUsers5 = Replace(FilterUsers5 + ")", "(  OR ", "(")
            End If

            'Verify how filter is used
            If Descricao <> "" Then
                Select Case Trim(Mid(Descricao, 1, 3))
                    Case "*"
                        FilterUsers5 = FilterUsers5 + " AND Objleg like '%" & Mid(Descricao, 4, 255) & "'"

                    Case "**"
                        FilterUsers5 = FilterUsers5 + " AND Objleg like '%" & Mid(Descricao, 4, 255) & "%'"

                    Case "***"
                        FilterUsers5 = FilterUsers5 + " AND Objleg like '" & Mid(Descricao, 4, 255) & "%'"

                End Select
            End If

            If CodClass <> "" Then
                Dim ObjCodClass As Object
                Dim CountCodClass As Integer
                CountCodClass = 1
                StrCodClass = " AND (TObjClas.CodClas = "

                'Create a string for consulting in database
                For Each ObjCodClass In ArrayClass
                    If CountCodClass <= 1 Then
                        StrCodClass = StrCodClass + Chr(34) + ObjCodClass + Chr(34)
                    Else
                        StrCodClass = StrCodClass + " OR TObjClas.CodClas = " + Chr(34) + ObjCodClass + Chr(34)
                    End If
                    CountCodClass = CountCodClass + 1
                Next
                StrCodClass = StrCodClass + ")"
            End If

            If StrCodClass <> "" Then FilterUsers5 = FilterUsers5 + StrCodClass
            If DwgFile <> "" Then FilterUsers5 = FilterUsers5 + "AND ObjLeg = '" & DwgFile & "'"

            'For dwg name consult file 
            'If DwgFileCompl <> "" Then FilterUsers5 = FilterUsers5 + "AND (ObjLeg.EleNome + '.dwg') = '" & DwgFileCompl & "'"

            'Get DescClas description
            Dim RsClass As New System.Data.DataTable, Conn As OleDbConnection, Diretorio As String
            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            If LibraryReference Is Nothing Then LibraryReference = New LibraryReference

            Dim sSql As String, Da As OleDbDataAdapter

            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB
            Diretorio = LibraryReference.ReturnPathApplication
            sSql = ""

            If DiamConsult <> "" Then

                For Each ClassTemp As Object In CodClass.Split(";")
                    'Somente tratamos neste caso diretamente pois pode se ter mais de uma consulta no aengedt para insercao de simbolos
                    Select Case ClassTemp

                        Case "2920", "2930", "2940", "2950"
                            If sSql.ToString.Trim <> "" Then sSql += " UNION "
                            sSql += "SELECT TOBJBASE.CODOBJ, TOBJBASE.CODCLAS, TOBJCLAS.CLASDES, TOBJBASE.OBJLEG, TOBJDWG.DWGNAME, TOBJDWG.DWGNAME & '.dwg' AS BlockName, TOBJDWG.CODVIEW, " & _
                            " TObjPVal.Prop2, TObjPVal.Prop3, TObjPVal.Prop4" & _
                            " FROM TOBJPVAL RIGHT JOIN (TOBJCLAS INNER JOIN (TOBJDWG INNER JOIN TOBJBASE ON TOBJDWG.CODOBJ = TOBJBASE.CODOBJ) ON TOBJCLAS.CODCLAS = TOBJBASE.CODCLAS) ON" & _
                            " TOBJPVAL.CODOBJ = TOBJBASE.CODOBJ" & _
                            " WHERE (TObjPval.Prop2 = '" & DiamConsult & "' AND TObjBase.CodClas = '" & ClassTemp & "')" & _
                            " ORDER BY TOBJBASE.OBJLEG ASC"

                        Case "2935"
                            If sSql.ToString.Trim <> "" Then sSql += " UNION "
                            sSql += "SELECT TOBJBASE.CODOBJ, TOBJBASE.CODCLAS, TOBJCLAS.CLASDES, TOBJBASE.OBJLEG, TOBJDWG.DWGNAME, TOBJDWG.DWGNAME & '.dwg' AS BlockName, TOBJDWG.CODVIEW, " & _
                            " TObjPVal.Prop2, TObjPVal.Prop3, TObjPVal.Prop4" & _
                            " FROM TOBJPVAL RIGHT JOIN (TOBJCLAS INNER JOIN (TOBJDWG INNER JOIN TOBJBASE ON TOBJDWG.CODOBJ = TOBJBASE.CODOBJ) ON TOBJCLAS.CODCLAS = TOBJBASE.CODCLAS) ON" & _
                            " TOBJPVAL.CODOBJ = TOBJBASE.CODOBJ" & _
                            " WHERE (TObjPval.Prop3 = '" & DiamConsult & "' AND TObjBase.CodClas = '" & ClassTemp & "')" & _
                            " ORDER BY TOBJBASE.OBJLEG ASC"

                        Case Else
                            If sSql.ToString.Trim <> "" Then sSql += " UNION "
                            sSql += "SELECT TOBJBASE.CODOBJ, TOBJBASE.CODCLAS, TOBJCLAS.CLASDES, TOBJBASE.OBJLEG, TOBJDWG.DWGNAME, TOBJDWG.DWGNAME & '.dwg' AS BlockName, TOBJDWG.CODVIEW, " & _
                            " TObjPVal.Prop2, TObjPVal.Prop3, TObjPVal.Prop4" & _
                            " FROM TOBJPVAL RIGHT JOIN (TOBJCLAS INNER JOIN (TOBJDWG INNER JOIN TOBJBASE ON TOBJDWG.CODOBJ = TOBJBASE.CODOBJ) ON TOBJCLAS.CODCLAS = TOBJBASE.CODCLAS) ON" & _
                            " TOBJPVAL.CODOBJ = TOBJBASE.CODOBJ" & _
                            " WHERE (TObjBase.CodClas = '" & ClassTemp & "')" & _
                            " ORDER BY TOBJBASE.OBJLEG ASC"

                    End Select
                Next

            Else
                sSql = "SELECT TOBJBASE.CODOBJ, TOBJBASE.CODCLAS, TOBJCLAS.CLASDES, TOBJBASE.OBJLEG, TOBJDWG.DWGNAME, TOBJDWG.DWGNAME & '.dwg' AS BlockName, TOBJDWG.CODVIEW, " & _
                " TObjPVal.Prop2, TObjPVal.Prop3, TObjPVal.Prop4" & _
                " FROM TOBJPVAL RIGHT JOIN (TOBJCLAS INNER JOIN (TOBJDWG INNER JOIN TOBJBASE ON TOBJDWG.CODOBJ = TOBJBASE.CODOBJ) ON TOBJCLAS.CODCLAS = TOBJBASE.CODCLAS) ON" & _
                " TOBJPVAL.CODOBJ = TOBJBASE.CODOBJ" & _
                " WHERE 1 = 1 " & FilterUsers5 & "" & _
                " ORDER BY TOBJBASE.OBJLEG ASC"

            End If

            Da = New OleDbDataAdapter(sSql, Conn)
            Da.Fill(RsClass)

            If RsClass.Rows.Count > 0 Then
                'system and and save the text box contents
                'Dim hFile As Long
                Dim sFileName As String
                'Path of file aengedt.dll
                sFileName = Diretorio & "AengeDt.dll"

                'Após gerar todos os textos relacionados, gera o txt a ser utilizado
                'Após configurar as informações, salva no arquivo txt temporário
                'Caminho do lista de material que se encontra
                'hFile = FreeFile()
                Dim objWriter As StreamWriter = New StreamWriter(sFileName, False, System.Text.Encoding.Default)
                objWriter.WriteLine("CODSYMB||CLASSE|DESCR|??????|DWGFILE")
                For Each sLine As System.Data.DataRow In RsClass.Rows

                    'Limpa as variaveis para nao jogar informacoes de outro bloco erroneamente 
                    CodSymb = "" : CodClass = "" : TipoObjeto = "" : Descricao = "" : DwgFile = "" : DwgFileCompl = "" : Prop2 = "" : Prop3 = "" : Prop4 = ""
                    If sLine(0).ToString <> "" Then CodSymb = sLine(0).ToString
                    If sLine(1).ToString <> "" Then CodClass = sLine(1).ToString
                    If sLine(2).ToString <> "" Then
                        If UserS5_Cad <> "" Then
                            TipoObjeto = UserS5_Cad
                        Else
                            TipoObjeto = sLine(2).ToString
                        End If
                    End If

                    'Column of dview of slide 
                    Select Case sLine(6).ToString
                        Case "2"
                            If sLine(3).ToString <> "" Then Descricao = sLine(3).ToString & " - frontal"
                        Case "3"
                            If sLine(3).ToString <> "" Then Descricao = sLine(3).ToString & " - lateral"
                        Case Else
                            If sLine(3).ToString <> "" Then Descricao = sLine(3).ToString
                    End Select
                    If sLine(4).ToString <> "" Then DwgFile = sLine(4).ToString
                    If sLine(5).ToString <> "" Then DwgFileCompl = sLine(5).ToString

                    If sLine(7).ToString <> "" Then
                        Prop2 = sLine(7).ToString
                    Else
                        Prop2 = "0"
                    End If

                    If sLine(8).ToString <> "" Then
                        Prop3 = sLine(8).ToString
                    Else
                        Prop3 = "0"
                    End If

                    If sLine(9).ToString <> "" Then
                        Prop4 = sLine(9).ToString
                    Else
                        Prop4 = "0"
                    End If

                    objWriter.WriteLine(CodSymb & "|" & CodClass & "|" & TipoObjeto & "|" & Descricao & "|" & DwgFile & "|" & DwgFileCompl & "|")   '& Prop2 & "|" & Prop3 & "|" & Prop4 & "|"
                Next
                objWriter.Close()

            End If

            RsClass = Nothing
            Conn.Close() : Conn = Nothing

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error settings ReturnDADt - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0014")
        Finally

        End Try

        Return Nothing

    End Function

    <LispFunction("ReturnData_AengeDt2Full")> _
    Function ReturnData_AengeDt2Full(ByVal rBf As ResultBuffer)

        Dim TextoLinha As String
        'obtain the next free file handle from the system and and save the text box contents. In this case, all code is for print in aengedt2.dll (txt file)
        Dim hFile As Long = 0, I As Integer = 0
        Dim sFileName As String, Diretorio As String
        Dim rsImp As New System.Data.DataTable, Da As OleDbDataAdapter, Conn As OleDbConnection

        Try

            If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection

            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB
            Diretorio = LibraryReference.ReturnPathApplication

            Da = New OleDbDataAdapter("SELECT * FROM Aelepval", Conn)
            Da.Fill(rsImp)

            'Caminho do lista de material que se encontra
            sFileName = Diretorio & "AengeDt2.dll"

            Dim objWriter As StreamWriter = New StreamWriter(sFileName, False, System.Text.Encoding.Default)
            objWriter.WriteLine("CODSYMB|QDC|PR01")
            For Each sLine As System.Data.DataRow In rsImp.Rows

                TextoLinha = ""
                For I = 1 To rsImp.Columns.Count - 2

                    If sLine(I).ToString = "" Then
                        TextoLinha = IIf(Len(TextoLinha) <= 0, "_", TextoLinha & "|_")
                    Else

                        If TextoLinha = "" Then
                            TextoLinha = IIf(Len(TextoLinha) <= 0, sLine(I).ToString, TextoLinha & "|_")
                        Else
                            TextoLinha = TextoLinha & "|" & sLine(I).ToString
                        End If

                    End If

                Next

                objWriter.WriteLine(TextoLinha)
            Next

            objWriter.Close()
            Conn.Close() : Conn = Nothing
            rsImp = Nothing

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error settings ReturnDADt2Full - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0015")
        End Try

        Return Nothing

    End Function

    'Return all information of aelebase e aelepval. In this case, not pass anymore in txt aengedt and aengedt2
    <LispFunction("ReturnData_AengeDt2")> _
    Function ReturnData_AengeDt2(ByVal Rbf As ResultBuffer) As ResultBuffer

        Try

            Dim rbFResult As New ResultBuffer
            If Rbf Is Nothing Then Return Nothing
            If Rbf.AsArray(0).Value.ToString() = "" Then Return Nothing

            'Get parametrs for consult
            Dim UserS5 As String, Diretorio As String = ""
            UserS5 = Rbf.AsArray(0).Value.ToString()
            Dim TextoLinha As String, Conn As OleDbConnection

            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            If LibraryReference Is Nothing Then LibraryReference = New LibraryReference

            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB
            Diretorio = LibraryReference.ReturnPathApplication

            'obtain the next free file handle from the system and and save the text box contents. In this case, all code is for print in aengedt2.dll (txt file)
            Dim I As Int16
            Dim sFileName As String
            Dim rsImp As New System.Data.DataTable, Da As OleDbDataAdapter

            'Caminho do lista de material que se encontra
            sFileName = Diretorio & "AengeDt2.dll"
            If UserS5 <> "" Then
                Da = New OleDbDataAdapter("SELECT * FROM TObjPVal WHERE CodObj = '" & UserS5 & "'", Conn)
            Else
                Da = New OleDbDataAdapter("SELECT * FROM TObjPVal", Conn)
                'If users5 is empty, then code return all register os aengedt2 for file
            End If

            Dim objWriter As StreamWriter = New StreamWriter(sFileName, False, System.Text.Encoding.Default)
            objWriter.WriteLine("CODSYMB|QDC|PR01")
            Da.Fill(rsImp)
            For Each sLine As System.Data.DataRow In rsImp.Rows
                TextoLinha = ""
                For I = 1 To rsImp.Columns.Count - 1
                    If sLine(I).ToString = "" Then
                        TextoLinha = IIf(Len(TextoLinha) <= 0, "_", TextoLinha & "|_")
                    Else
                        If TextoLinha = "" Then
                            TextoLinha = IIf(Len(TextoLinha) <= 0, sLine(I).ToString, TextoLinha & "|_")
                        Else
                            TextoLinha = TextoLinha & "|" & sLine(I).ToString
                        End If
                    End If
                Next I

                objWriter.WriteLine(TextoLinha)

                'If UserS5 <> "" Then
                '    'Retorna como string o maior número encontrado acrescentado de 1 
                '    With rbFResult
                '        .Add(New TypedValue(LispDataType.Double, TextoLinha))
                '    End With
                '    Exit For
                'End If

            Next
            objWriter.Close()
            rsImp = Nothing

            Return Nothing 'rbFResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error settings Aegdt2 - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0016")
            Return Nothing
        End Try

    End Function

    'Return all information of aelebase e aelepval. In this case, not pass anymore in txt aengedt and aengedt2
    <LispFunction("ReturnData_AengeDt3")> _
    Function ReturnData_AengeDt3(ByVal rbF As ResultBuffer) As ResultBuffer

        'Get parametrs for consult
        Dim UserS5 As String, Tabela As String, ArrayInfo As Object
        Dim rbFResult As New ResultBuffer

        If rbF Is Nothing Then Return Nothing
        If rbF.AsArray(0).Value.ToString() = "" Then Return Nothing
        UserS5 = rbF.AsArray(0).Value.ToString()

        Try

            ArrayInfo = Split(UserS5, "|")
            Tabela = ArrayInfo(0)
            'Line default of file
            Select Case Tabela.ToLower

                '#############################################
                '#############################################
                '#############################################
                'Voltar todas as funções do select case 

                Case "tab_aelelux".ToLower
                    Aengedt3_AeleLux(ArrayInfo)

                Case "tab_aelereg".ToLower
                    Aengedt3_AeleReg(ArrayInfo)

                Case "tab_aeleutil".ToLower
                    Aengedt3_AeleUtil(ArrayInfo)

                Case "tab_luminoso".ToLower
                    Aengedt3_Luminoso(ArrayInfo)

                Case "tab_intensidade".ToLower
                    Aengedt3_Intensidade(ArrayInfo)

                    'for default return only information of aelelux
                Case Else
                    Aengedt3_AeleLux(ArrayInfo)

            End Select

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error settings AengDt3 - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0017")
        End Try

        Return Nothing

    End Function

    'Generate the aengedt3.dll full for lisp consulting
    <LispFunction("Aengedt3_Full")> _
    Function Aengedt3_Full(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim RsAengedt As New System.Data.DataTable, Da As OleDbDataAdapter, Conn As OleDbConnection, TextoLinha As String = ""

        If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
        LibraryConnection.TypeDb = "AHID"
        Conn = LibraryConnection.Aenge_OpenConnectionDB

        'obtain the next free file handle from the system and and save the text box contents. In this case, all code is for print in aengedt2.dll (txt file)
        Dim I As Double, Diretorio As String = LibraryReference.ReturnPathApplication
        Dim sFileName As String

        Try

            'Caminho do lista de material que se encontra
            sFileName = Diretorio & "AengeDt3.dll"

            Da = New OleDbDataAdapter("SELECT * FROM Aelelux", Conn)
            Da.Fill(RsAengedt)

            Dim objWriter As StreamWriter = New StreamWriter(sFileName, False, System.Text.Encoding.Default)
            objWriter.WriteLine("[TAB_AELELUX]")
            For Each sLine As System.Data.DataRow In RsAengedt.Rows
                TextoLinha = ""
                TextoLinha = IIf(Not sLine("CodLux").ToString = "", sLine("CodLux").ToString, "") & "|" & IIf(Not sLine("Descricao").ToString = "", sLine("Descricao").ToString, "") & "|" & _
                IIf(Not sLine("Lux").ToString = "", sLine("Lux").ToString, "")
                objWriter.WriteLine(TextoLinha)
            Next

            'Clear datatable e adapter for new register 
            RsAengedt = Nothing : RsAengedt = New System.Data.DataTable
            Da = Nothing
            Da = New OleDbDataAdapter("SELECT * FROM Aelereg", Conn)
            Da.Fill(RsAengedt)

            objWriter.WriteLine("[TAB_AELEREG]")
            For Each sLine As System.Data.DataRow In RsAengedt.Rows
                TextoLinha = ""
                TextoLinha = IIf(Not sLine("CodReg").ToString = "", sLine("CodReg").ToString, "") & "|" & IIf(Not sLine("RegNome").ToString = "", sLine("RegNome").ToString, "") & "|" & IIf(Not sLine("RegEst").ToString = "", sLine("RegEst").ToString, "") & _
                 "|" & IIf(Not sLine("RegEstU").ToString = "", sLine("RegEstU").ToString, "") & "|" & IIf(Not sLine("RegAlt").ToString = "", sLine("RegAlt").ToString, "") & "|" & IIf(Not sLine("RegAltU").ToString = "", sLine("RegAltU").ToString, "") & _
                 "|" & IIf(Not sLine("RegRot").ToString = "", sLine("RegRot").ToString, "") & _
                 "|" & IIf(Not sLine("RegRotU").ToString = "", sLine("RegRotU").ToString, "") & "|" & IIf(Not sLine("RegCor").ToString = "", sLine("RegCor").ToString, "") & "|" & IIf(Not sLine("RegCorU").ToString = "", sLine("RegCorU").ToString, "")
                objWriter.WriteLine(TextoLinha)
            Next

            'Clear datatable e adapter for new register 
            RsAengedt = Nothing : RsAengedt = New System.Data.DataTable
            Da = Nothing
            Da = New OleDbDataAdapter("SELECT * FROM AeleUtil", Conn)
            Da.Fill(RsAengedt)

            objWriter.WriteLine("[TAB_AELEUTIL]")
            For Each sLine As System.Data.DataRow In RsAengedt.Rows
                TextoLinha = ""
                TextoLinha = IIf(Not sLine("Id").ToString = "", sLine("Id").ToString, "") & "|" & IIf(Not sLine("CodEle").ToString = "", sLine("CodEle").ToString, "") & "|" & IIf(Not sLine("FatLocal").ToString = "", sLine("FatLocal").ToString, "") & _
                 "|" & IIf(Not sLine("R853").ToString = "", sLine("R853").ToString, "") & "|" & IIf(Not sLine("R851").ToString = "", sLine("R851").ToString, "") & "|" & IIf(Not sLine("R831").ToString = "", sLine("R831").ToString, "") & _
                 "|" & IIf(Not sLine("R753").ToString = "", sLine("R753").ToString, "") & _
                 "|" & IIf(Not sLine("R751").ToString = "", sLine("R751").ToString, "") & "|" & IIf(Not sLine("R733").ToString = "", sLine("R733").ToString, "") & "|" & IIf(Not sLine("R731").ToString = "", sLine("R731").ToString, "") & _
                 "|" & IIf(Not sLine("R711").ToString = "", sLine("R711").ToString, "") & "|" & IIf(Not sLine("R551").ToString = "", sLine("R551").ToString, "") & "|" & IIf(Not sLine("R531").ToString = "", sLine("R531").ToString, "") & _
                 "|" & IIf(Not sLine("R511").ToString = "", sLine("R511").ToString, "") & "|" & IIf(Not sLine("R331").ToString = "", sLine("R331").ToString, "") & "|" & IIf(Not sLine("R311").ToString = "", sLine("R311").ToString, "")
                objWriter.WriteLine(TextoLinha)
            Next

            'Clear datatable e adapter for new register 
            RsAengedt = Nothing : RsAengedt = New System.Data.DataTable
            Da = Nothing
            Da = New OleDbDataAdapter("SELECT * FROM TFluxoLuminoso", Conn)
            Da.Fill(RsAengedt)

            objWriter.WriteLine("[TAB_LUMINOSO]")
            For Each sLine As System.Data.DataRow In RsAengedt.Rows
                TextoLinha = ""
                TextoLinha = IIf(Not sLine("CodEle").ToString = "", sLine("CodEle").ToString, "") & "|" & IIf(Not sLine("FluxoHor").ToString = "", sLine("FluxoHor").ToString, "") & "|" & IIf(Not sLine("FluxOver").ToString = "", sLine("FluxOver").ToString, "")
                objWriter.WriteLine(TextoLinha)
            Next

            'Clear datatable e adapter for new register 
            RsAengedt = Nothing : RsAengedt = New System.Data.DataTable
            Da = Nothing
            Da = New OleDbDataAdapter("SELECT * FROM TIntensidadeLuminosa", Conn)
            Da.Fill(RsAengedt)

            objWriter.WriteLine("[TAB_INTENSIDADE]")
            For Each sLine As System.Data.DataRow In RsAengedt.Rows
                TextoLinha = ""
                TextoLinha = IIf(Not sLine("CodEle").ToString = "", sLine("CodEle").ToString, "") & "|" & IIf(Not sLine("ANGINCIDENCIA").ToString = "", sLine("ANGINCIDENCIA").ToString, "") & "|" & IIf(Not sLine("INTLUMINOSA").ToString = "", sLine("INTLUMINOSA").ToString, "")
                objWriter.WriteLine(TextoLinha)
            Next

            'Get all cod-objects not in intensidade and complete with 0 value. In this case, we need 0 - 70 (10 interval)
            Dim CountA As Integer, Interval As Integer
            CountA = 7 : Interval = 10

            'Clear datatable e adapter for new register 
            RsAengedt = Nothing : RsAengedt = New System.Data.DataTable
            Da = Nothing
            Da = New OleDbDataAdapter("SELECT * FROM TFluxoLuminoso WHERE CodEle NOT IN (SELECT CodEle FROM TIntensidadeLuminosa)", Conn)
            Da.Fill(RsAengedt)

            For Each sLine As System.Data.DataRow In RsAengedt.Rows
                TextoLinha = ""
                For I = 0 To 7
                    TextoLinha = IIf(Not sLine("CodEle").ToString = "", sLine("CodEle").ToString, "") & "|" & CStr(Interval) & "|0"
                    objWriter.WriteLine(TextoLinha)
                    Interval = Interval + 10
                Next

                Interval = 0
            Next

            objWriter.WriteLine(My.Settings.CharEndFile)
            objWriter.Close()

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error executing - AengeD3Full - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0018")
        End Try

        Return Nothing

    End Function

    'Update all register in table Aengedt3_Intensidade
    Function Aengedt3_Intensidade(ByVal ArrayInfo As Object)

        'obtain the next free file handle from the system and and save the text box contents. In this case, all code is for print in aengedt2.dll (txt file)
        Dim sFileName As String, TextoLinha As String, Diretorio As String, FilterUsers5 As String = ""
        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference

        Try

            Diretorio = LibraryReference.ReturnPathApplication
            'Caminho do lista de material que se encontra
            sFileName = Diretorio & "AengeDt3.dll"

            Dim objWriter As StreamWriter = New StreamWriter(sFileName, False, System.Text.Encoding.Default)
            objWriter.WriteLine("[TAB_INTENSIDADE]")

            'For fields in filter
            Dim CodEle As String, ANGINCIDENCIA As Object, INTLUMINOSA As Object
            CodEle = ArrayInfo(1) : ANGINCIDENCIA = ArrayInfo(2) : INTLUMINOSA = ArrayInfo(3)

            'Create the filter
            If CodEle <> "" Then FilterUsers5 = FilterUsers5 + " AND CodEle = '" & CodEle & "'"
            If ANGINCIDENCIA <> "" And CStr(ANGINCIDENCIA) <> "0" Then FilterUsers5 = FilterUsers5 + " AND ANGINCIDENCIA = " & ANGINCIDENCIA & ""
            If INTLUMINOSA <> "" And CStr(INTLUMINOSA) <> "0" Then FilterUsers5 = FilterUsers5 + " AND INTLUMINOSA = " & INTLUMINOSA & ""

            'Connection with database
            Dim Conn As OleDbConnection, RsImp As New System.Data.DataTable, Da As OleDbDataAdapter
            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB

            Da = New OleDbDataAdapter("SELECT * FROM TIntensidadeLuminosa WHERE 1 = 1 " & FilterUsers5, Conn)
            Da.Fill(RsImp)

            For Each DrImp As System.Data.DataRow In RsImp.Rows
                TextoLinha = ""
                TextoLinha = IIf(Not DrImp("CodEle").ToString = "", DrImp("CodEle").ToString, "") & "|" & IIf(Not DrImp("ANGINCIDENCIA").ToString = "", DrImp("ANGINCIDENCIA").ToString, "") & "|" & IIf(Not DrImp("INTLUMINOSA").ToString = "", DrImp("INTLUMINOSA").ToString, "")
                objWriter.WriteLine(TextoLinha)
            Next

            objWriter.Close()

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error executing - AengeD3_Intensidade - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0019")
        End Try

        Return True

    End Function

    Function Aengedt3_Luminoso(ByVal ArrayInfo As Object)

        'obtain the next free file handle from the system and and save the text box contents. In this case, all code is for print in aengedt2.dll (txt file)
        Dim sFileName As String, TextoLinha As String, Diretorio As String, FilterUsers5 As String = ""
        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference

        Try

            Diretorio = LibraryReference.ReturnPathApplication
            'Caminho do lista de material que se encontra
            sFileName = Diretorio & "AengeDt3.dll"

            Dim objWriter As StreamWriter = New StreamWriter(sFileName, False, System.Text.Encoding.Default)
            objWriter.WriteLine("[TAB_LUMINOSO]")

            'For fields in filter
            Dim CodEle As String, FluxoHor As Object, FluxoOver As Object
            CodEle = ArrayInfo(1) : FluxoHor = ArrayInfo(2) : FluxoOver = ArrayInfo(3)

            'Create the filter
            If CodEle <> "" Then FilterUsers5 = FilterUsers5 + " AND CodEle = '" & CodEle & "'"
            If FluxoHor <> "" And CStr(FluxoHor) <> "0" Then FilterUsers5 = FilterUsers5 + " AND FluxoHor = " & FluxoHor & ""
            If FluxoOver <> "" And CStr(FluxoOver) <> "0" Then FilterUsers5 = FilterUsers5 + " AND FluxoOver = " & FluxoOver & ""

            'Connection with database
            Dim Conn As OleDbConnection, RsImp As New System.Data.DataTable, Da As OleDbDataAdapter
            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB

            Da = New OleDbDataAdapter("SELECT * FROM TFluxoLuminoso WHERE 1 = 1 " & FilterUsers5, Conn)
            Da.Fill(RsImp)

            For Each DrImp As System.Data.DataRow In RsImp.Rows
                TextoLinha = ""
                TextoLinha = IIf(Not DrImp("CodEle").ToString = "", DrImp("CodEle").ToString, "") & "|" & IIf(Not DrImp("FluxoHor").ToString = "", DrImp("FluxoHor").ToString, "") & "|" & IIf(Not DrImp("FluxOver").ToString = "", DrImp("FluxOver").ToString, "")
                objWriter.WriteLine(TextoLinha)
            Next
            objWriter.Close()

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error executing - AengeD3_Luminoso - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0020")
        End Try

        Return True

    End Function

    Function Aengedt3_AeleUtil(ByVal ArrayInfo As Object)

        'obtain the next free file handle from the system and and save the text box contents. In this case, all code is for print in aengedt2.dll (txt file)
        Dim sFileName As String, TextoLinha As String, Diretorio As String, FilterUsers5 As String = ""
        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference

        Try

            Diretorio = LibraryReference.ReturnPathApplication
            'Caminho do lista de material que se encontra
            sFileName = Diretorio & "AengeDt3.dll"

            Dim objWriter As StreamWriter = New StreamWriter(sFileName, False, System.Text.Encoding.Default)
            objWriter.WriteLine("[TAB_AELEUTIL]")

            'For fields in filter
            Dim CodEle As String, FatLocal As Object, Id As Object
            Id = ArrayInfo(1) : CodEle = ArrayInfo(2) : FatLocal = ArrayInfo(3)

            'Create the filter
            If Id <> "" And Id <> 0 Then FilterUsers5 = FilterUsers5 + " AND Id = " & Id & ""
            If CodEle <> "" Then FilterUsers5 = FilterUsers5 + " AND CodEle = '" & CodEle & "'"
            If FatLocal <> "" Then FilterUsers5 = FilterUsers5 + " AND FatLocal = '" & FatLocal & "'"

            'Connection with database
            Dim Conn As OleDbConnection, RsImp As New System.Data.DataTable, Da As OleDbDataAdapter
            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB

            Da = New OleDbDataAdapter("SELECT * FROM AeleUtil WHERE 1 = 1 " & FilterUsers5, Conn)
            Da.Fill(RsImp)

            For Each DrImp As System.Data.DataRow In RsImp.Rows
                TextoLinha = ""
                TextoLinha = IIf(Not DrImp("Id").ToString = "", DrImp("Id").ToString, "") & "|" & IIf(Not DrImp("CodEle").ToString = "", DrImp("CodEle").ToString, "") & "|" & IIf(Not DrImp("FatLocal").ToString = "", DrImp("FatLocal").ToString, "") & _
                 "|" & IIf(Not DrImp("R853").ToString = "", DrImp("R853").ToString, "") & "|" & IIf(Not DrImp("R851").ToString = "", DrImp("R851").ToString, "") & "|" & IIf(Not DrImp("R831").ToString = "", DrImp("R831").ToString, "") & _
                 "|" & IIf(Not DrImp("R753").ToString = "", DrImp("R753").ToString, "") & _
                 "|" & IIf(Not DrImp("R751").ToString = "", DrImp("R751").ToString, "") & "|" & IIf(Not DrImp("R733").ToString = "", DrImp("R733").ToString, "") & "|" & IIf(Not DrImp("R731").ToString = "", DrImp("R731").ToString, "") & _
                 "|" & IIf(Not DrImp("R711").ToString = "", DrImp("R711").ToString, "") & "|" & IIf(Not DrImp("R551").ToString = "", DrImp("R551").ToString, "") & "|" & IIf(Not DrImp("R531").ToString = "", DrImp("R531").ToString, "") & _
                 "|" & IIf(Not DrImp("R511").ToString = "", DrImp("R511").ToString, "") & "|" & IIf(Not DrImp("R331").ToString = "", DrImp("R331").ToString, "") & "|" & IIf(Not DrImp("R311").ToString = "", DrImp("R311").ToString, "")
                objWriter.WriteLine(TextoLinha)
            Next

            objWriter.WriteLine("[]")
            objWriter.Close()

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error executing - AengeD3_AeleUtil - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0021")
            Return False
        End Try

        Return True
    End Function

    Function Aengedt3_AeleReg(ByVal ArrayInfo As Object)

        'obtain the next free file handle from the system and and save the text box contents. In this case, all code is for print in aengedt2.dll (txt file)
        'obtain the next free file handle from the system and and save the text box contents. In this case, all code is for print in aengedt2.dll (txt file)
        Dim sFileName As String, TextoLinha As String, Diretorio As String, FilterUsers5 As String = ""
        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference

        Try

            Diretorio = LibraryReference.ReturnPathApplication
            'Caminho do lista de material que se encontra
            sFileName = Diretorio & "AengeDt3.dll"

            Dim objWriter As StreamWriter = New StreamWriter(sFileName, False, System.Text.Encoding.Default)
            objWriter.WriteLine("[TAB_AELEREG]")

            'For fields in filter
            Dim CodReg As String, RegNome As Object, RegEst As Object
            CodReg = ArrayInfo(1) : RegNome = ArrayInfo(2) : RegEst = ArrayInfo(3)

            'Create the filter
            If CodReg <> "" Then FilterUsers5 = FilterUsers5 + " AND CodReg = '" & CodReg & "'"
            If RegNome <> "" Then FilterUsers5 = FilterUsers5 + " AND RegNome = '" & RegNome & "'"

            'Verify how filter is used
            If RegNome <> "" Then
                Select Case Trim(Mid(RegNome, 1, 3))
                    Case "*"
                        FilterUsers5 = FilterUsers5 + " AND RegNome like '%" & Mid(RegNome, 4, 255) & "'"

                    Case "**"
                        FilterUsers5 = FilterUsers5 + " AND RegNome like '%" & Mid(RegNome, 4, 255) & "%'"

                    Case "***"
                        FilterUsers5 = FilterUsers5 + " AND RegNome like '" & Mid(RegNome, 4, 255) & "%'"

                End Select
            End If

            'Verify how filter is used
            If RegEst <> "" Then
                Select Case Trim(Mid(RegEst, 1, 3))
                    Case "*"
                        FilterUsers5 = FilterUsers5 + " AND RegEst like '%" & Mid(RegEst, 4, 255) & "'"

                    Case "**"
                        FilterUsers5 = FilterUsers5 + " AND RegEst like '%" & Mid(RegEst, 4, 255) & "%'"

                    Case "***"
                        FilterUsers5 = FilterUsers5 + " AND RegEst like '" & Mid(RegEst, 4, 255) & "%'"

                End Select
            End If

            'Connection with database
            Dim Conn As OleDbConnection, RsImp As New System.Data.DataTable, Da As OleDbDataAdapter
            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB

            Da = New OleDbDataAdapter("SELECT * FROM Aelereg WHERE 1 = 1 " & FilterUsers5, Conn)
            Da.Fill(RsImp)

            For Each DrImp As System.Data.DataRow In RsImp.Rows
                TextoLinha = ""
                TextoLinha = IIf(Not DrImp("CodReg").ToString = "", DrImp("CodReg").ToString, "") & "|" & IIf(Not DrImp("RegNome").ToString = "", DrImp("RegNome").ToString, "") & "|" & IIf(Not DrImp("RegEst").ToString = "", DrImp("RegEst").ToString, "") & _
                         "|" & IIf(Not DrImp("RegEstU").ToString = "", DrImp("RegEstU").ToString, "") & "|" & IIf(Not DrImp("RegAlt").ToString = "", DrImp("RegAlt").ToString, "") & "|" & IIf(Not DrImp("RegAltU").ToString = "", DrImp("RegAltU").ToString, "") & _
                         "|" & IIf(Not DrImp("RegRot").ToString = "", DrImp("RegRot").ToString, "") & _
                         "|" & IIf(Not DrImp("RegRotU").ToString = "", DrImp("RegRotU").ToString, "") & "|" & IIf(Not DrImp("RegCor").ToString = "", DrImp("RegCor").ToString, "") & "|" & IIf(Not DrImp("RegCorU").ToString = "", DrImp("RegCorU").ToString, "")
                objWriter.WriteLine(TextoLinha)
            Next

            objWriter.WriteLine("[]")
            objWriter.Close()

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error executing - AengeD3_AeleReg - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0022")
            Return False
        End Try

        Return True

    End Function

    Function Aengedt3_AeleLux(ByVal ArrayInfo As Object)

        Dim sFileName As String, TextoLinha As String, Diretorio As String, FilterUsers5 As String = ""
        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference

        Try

            Diretorio = LibraryReference.ReturnPathApplication
            'Caminho do lista de material que se encontra
            sFileName = Diretorio & "AengeDt3.dll"

            Dim objWriter As StreamWriter = New StreamWriter(sFileName, False, System.Text.Encoding.Default)
            objWriter.WriteLine("[TAB_AELELUX]")

            'For fields in filter
            Dim CodLux As String, Descricao As Object, Lux As Object
            CodLux = ArrayInfo(1) : Descricao = ArrayInfo(2) : Lux = ArrayInfo(3)

            'Create the filter
            If CodLux <> "" Then FilterUsers5 = FilterUsers5 + " AND CodLux = '" & CodLux & "'"
            If Lux <> 0 And Lux <> "" Then FilterUsers5 = FilterUsers5 + " AND Lux = " & Lux & ""

            'Verify how filter is used
            If Descricao <> "" Then
                Select Case Trim(Mid(Descricao, 1, 3))
                    Case "*"
                        FilterUsers5 = FilterUsers5 + " AND Descricao like '%" & Mid(Descricao, 4, 255) & "'"

                    Case "**"
                        FilterUsers5 = FilterUsers5 + " AND Descricao like '%" & Mid(Descricao, 4, 255) & "%'"

                    Case "***"
                        FilterUsers5 = FilterUsers5 + " AND Descricao like '" & Mid(Descricao, 4, 255) & "%'"

                End Select
            End If

            'Connection with database
            Dim Conn As OleDbConnection, RsImp As New System.Data.DataTable, Da As OleDbDataAdapter
            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB

            Da = New OleDbDataAdapter("SELECT * FROM Aelelux WHERE 1 = 1 " & FilterUsers5, Conn)
            Da.Fill(RsImp)

            For Each DrImp As System.Data.DataRow In RsImp.Rows
                TextoLinha = ""
                TextoLinha = ""
                TextoLinha = IIf(Not DrImp("CodLux").ToString = "", DrImp("CodLux").ToString, "") & "|" & IIf(Not DrImp("Descricao").ToString = "", DrImp("Descricao").ToString, "") & "|" & IIf(Not DrImp("Lux").ToString = "", DrImp("Lux").ToString, "")
                objWriter.WriteLine(TextoLinha)
            Next

            objWriter.WriteLine("[]")
            objWriter.Close()

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error executing - AengeD3_AeleLux - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0023")
            Return False
        End Try

        Return True

    End Function

#End Region

#Region "----- Lista de materiais -----"

    'Lista de materiais - Retorna o titulo da lista de material salva no arquvo cfg
    <LispFunction("TitleLstMat")> _
    Public Function TitleLstMat(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim RbfResult As New ResultBuffer
        Dim TitQuat As String, ProjTactual As String, DwgTactual As String

        ProjTactual = Aenge_GetCfg("AppData", "AENGEPROJ", GetAppInstall() & "Autoenge.cfg")
        DwgTactual = Aenge_GetCfg("AppData", "AENGEDWG", GetAppInstall() & "Autoenge.cfg")

        TitQuat = Aenge_GetCfg(SecaoQuantitativo, "TITULO", GetAppInstall() & ProjTactual & "\" & DwgTactual & ".cfg").ToString

        'Retorna como string o maior número encontrado acrescentado de 1 
        With RbfResult
            .Add(New TypedValue(LispDataType.Text, TitQuat))
        End With
        Return RbfResult

    End Function

    <LispFunction("Load_AhidLMat_Par")> _
    Sub Load_AhidLMat_Par()

        'Dim ALegN As New AhidMAT.CDialogListaMaterial, Retorno As Object
        'Dim Doc As Document = Application.DocumentManager.MdiActiveDocument, Comando As String = ""
        'Retorno = ALegN.DoModal_Other
        'ALegN = Nothing

        ''Caso seja impressão, retorna para o lisp a impressão
        'SetCmdEcho(0)
        'Comando = "(printlayout4 " & Chr(34) & "YES" & Chr(34) & " " & Chr(34) & "YES" & Chr(34) & " " & Chr(34) & "QUANTITATIVO DE MATERIAIS PARARAIOS" & Chr(34) & ")"
        'If Retorno = 2 Then Doc.SendStringToExecute(Comando & Chr(13), False, False, False)

    End Sub

    <LispFunction("Load_AhidLMat_Sec")> _
    Sub Load_AhidLMat_Sec()

        'Dim ALegN As New AhidMAT.CDialogListaMaterial, Retorno As Object
        'Dim Doc As Document = Application.DocumentManager.MdiActiveDocument, Comando As String = ""
        'Retorno = ALegN.DoModal_Other
        'ALegN = Nothing

        'SetCmdEcho(0)
        'Comando = "(printlayout4 " & Chr(34) & "YES" & Chr(34) & " " & Chr(34) & "YES" & Chr(34) & " " & Chr(34) & "QUANTITATIVO DE MATERIAIS SECURITY" & Chr(34) & ")"
        'If Retorno = 2 Then Doc.SendStringToExecute(Comando & Chr(13), False, False, False)

    End Sub

    <LispFunction("Load_AhidLMat_Net")> _
    Sub Load_AhidLMat_Net()

        'Dim ALegN As New AhidMAT.CDialogListaMaterial, Retorno As Object
        'Dim Doc As Document = Application.DocumentManager.MdiActiveDocument, Comando As String = ""
        'Retorno = ALegN.DoModal_Other
        'ALegN = Nothing

        'SetCmdEcho(0)
        'Comando = "(printlayout4 " & Chr(34) & "YES" & Chr(34) & " " & Chr(34) & "YES" & Chr(34) & " " & Chr(34) & "QUANTITATIVO DE MATERIAIS NET" & Chr(34) & ")"
        'If Retorno = 2 Then Doc.SendStringToExecute(Comando & Chr(13), False, False, False)

    End Sub

    'Lista de material para automação
    <LispFunction("Load_AhidLMat_Auto")> _
    Sub Load_AhidLMat_Auto()

        'Dim ALegN As New AhidMAT.CDialogListaMaterial, Retorno As Object
        'Dim Doc As Document = Application.DocumentManager.MdiActiveDocument, Comando As String = ""
        'Retorno = ALegN.DoModal_Other
        'ALegN = Nothing

        'SetCmdEcho(0)
        'Comando = "(printlayout4 " & Chr(34) & "YES" & Chr(34) & " " & Chr(34) & "YES" & Chr(34) & " " & Chr(34) & "QUANTITATIVO DE MATERIAIS AUTO" & Chr(34) & ")"
        'If Retorno = 2 Then Doc.SendStringToExecute(Comando & Chr(13), False, False, False)

    End Sub

#End Region

#Region "----- Information Project and Dwgs -----"

    'Return a name of project
    Public Function ReturnCurrent_Project() As String

        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
        Dim NameProject As String
        NameProject = LibraryReference.Return_TactualProject
        LibraryReference = Nothing
        Return NameProject

    End Function

    'Return a name of dwg tactual of project
    Public Function ReturnCurrent_Dwg() As String

        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
        Dim NameDwg As String
        NameDwg = LibraryReference.Return_TactualDrawing
        LibraryReference = Nothing
        Return NameDwg

    End Function

    'Return a tactual Id of project
    Public Function ReturnCurrent_Id() As String

        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
        Dim IdT As String
        IdT = LibraryReference.Return_TactualID
        LibraryReference = Nothing
        Return IdT

    End Function

#End Region

#Region "----- Eletrodutos e calculos de tubulaçao -----"

    'Get all information about dimension and tub. Calc of eletrod.
    <LispFunction("Calc_DimensionTub")> _
    Function Calc_DimensionTub() As Double

        'Receive Area_Total_Fios, Taxa_Ocupacao - Calc Area_Eletroduto_Minima for search the best result in AELEPCONCEE
        Dim Area_Eletroduto_Minima As Double
        Dim RsResult As New System.Data.DataTable, DiametroFinal As Double
        Dim Da As OleDbDataAdapter, Conn As OleDbConnection

        If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
        'Database AHID
        LibraryConnection.TypeDb = "AHID"
        Conn = LibraryConnection.Aenge_OpenConnectionDB
        'Set value default
        DiametroFinal = 0

        'For test. Delete after execute final version
        Area_Eletroduto_Minima = My.Settings.Calc_Area_Eletroduto_Minima  '44.9

        Try

            Conn = LibraryConnection.Aenge_OpenConnectionDB
            Da = New OleDbDataAdapter("SELECT * FROM AELEPCONCEE WHERE Diametro >= (DiametroExt - (2 * Espessura)) * (DiametroExt - (2 * Espessura))", Conn)
            Da.Fill(RsResult)

            If RsResult.Rows.Count > 0 Then
                Dim DrResult As System.Data.DataRow = RsResult.Rows(0)
                DiametroFinal = DrResult("Diametro")
            End If

            Conn.Close() : Conn = Nothing
            RsResult = Nothing

            Dim rbFResult As New ResultBuffer
            'Retorna como string o maior número encontrado acrescentado de 1 
            With rbFResult
                .Add(New TypedValue(LispDataType.Double, DiametroFinal))
            End With

            Return DiametroFinal
        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error settings BlockAtt - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA005")
            Return Nothing
        End Try

    End Function

    'Call compact for return diametro circuit
    <LispFunction("RetornaDiametroCircuito")> _
    Function RetornaDiametroCircuito(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim Diametro As Object
        Dim rbFResult As New ResultBuffer
        If rbf Is Nothing Then Return Nothing
        If rbf.AsArray(0).Value.ToString() = "" Then Return Nothing

        Diametro = ReturnData_Circuit("diametroext", rbf)

        'Retorna como string o maior número encontrado acrescentado de 1 
        With rbFResult
            .Add(New TypedValue(LispDataType.Text, CStr(Diametro)))
        End With

        Return rbFResult

    End Function

    'Return diam in and out - Parameters cod
    <LispFunction("ReturnInOut_CodObj")> _
    Function ReturnInOut_CodObj(ByVal Rbf As ResultBuffer)

        Dim rbFResult As New ResultBuffer, DiamIn As String = "0", DiamOut As String = "0"

        If Rbf Is Nothing Then
            With rbFResult
                .Add(New TypedValue(LispDataType.Nil))
            End With
            GoTo Finaliza
        End If

        If Rbf.AsArray(0).Value Is Nothing Then
            With rbFResult
                .Add(New TypedValue(LispDataType.Nil))
            End With
            GoTo Finaliza
        End If

        If Rbf.AsArray(0).Value.ToString() = "" Then
            With rbFResult
                .Add(New TypedValue(LispDataType.Nil))
            End With
            GoTo Finaliza
        End If

        Dim CodObj As String = Rbf.AsArray(0).Value.ToString()
        If CodObj.ToString.Trim = "" Then
            With rbFResult
                .Add(New TypedValue(LispDataType.Nil))
            End With
            GoTo Finaliza
        End If

        Dim SqlStr As String = "SELECT TOBJPVAL.CODOBJ, TOBJBASE.CODCLAS, TOBJPVAL.PROP1, TOBJPVAL.PROP2, TOBJPVAL.PROP3, TOBJPVAL.PROP4, TOBJPVAL.PROP5" & _
                                       " FROM TOBJBASE INNER JOIN TOBJPVAL ON TOBJBASE.CODOBJ = TOBJPVAL.CODOBJ WHERE TOBJPVAL.CodObj = '" & CodObj & "'"

        If ConnAhid Is Nothing Then
            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            LibraryConnection.TypeDb = "AHID"
            ConnAhid = LibraryConnection.Aenge_OpenConnectionDB()
        End If

        Dim Da As New System.Data.OleDb.OleDbDataAdapter(SqlStr, ConnAhid)
        Dim Dt As New System.Data.DataTable, CodClas As String = ""
        Da.Fill(Dt)

        If Dt.Rows.Count > 0 Then

            Dim Dr As DataRow = Dt.Rows(0)
            CodClas = Dr("CodClas").ToString

            Select Case CodClas
                Case "2935", "2235"
                    If Dr("Prop3").ToString <> "" Then DiamIn = Dr("Prop3").ToString
                    If Dr("Prop4").ToString <> "" Then DiamOut = Dr("Prop4").ToString '                   If Dr("Prop4").ToString <> "" Then DiamIn = Dr("Prop4").ToString

                Case "2930", "2230"
                    If Dr("Prop2").ToString <> "" Then DiamIn = Dr("Prop2").ToString
                    If Dr("Prop3").ToString <> "" Then DiamOut = Dr("Prop3").ToString '                    If Dr("Prop3").ToString <> "" Then DiamIn = Dr("Prop3").ToString

                    'Foi repassado depois da função estar pronta, por isso estamos separando no case abaixo, mesmo sendo igual ao o de cima 
                    'Neste caso sao para caixas e ralos unifilar somente o tratamento abaixo
                Case "2245", "2250", "2240"
                    If Dr("Prop2").ToString <> "" Then DiamIn = Dr("Prop2").ToString
                    If Dr("Prop3").ToString <> "" Then DiamOut = Dr("Prop3").ToString

                Case Else
                    If Dr("Prop2").ToString <> "" Then DiamIn = Dr("Prop2").ToString

            End Select

            'Retorna como string o maior número encontrado acrescentado de 1 
            With rbFResult
                .Add(New TypedValue(LispDataType.Text, DiamIn))
                .Add(New TypedValue(LispDataType.Text, DiamOut))
            End With

        Else
            'rbFResult = Nothing
            With rbFResult
                .Add(New TypedValue(LispDataType.Nil))
            End With
        End If

Finaliza:
        Return rbFResult

    End Function

    'Call compact for return diametro circuit
    <LispFunction("RetornaDiametroTub")> _
    Function RetornaDiametroTub()
        Dim Diametro As Object
        Dim rbFResult As New ResultBuffer

        Diametro = ReturnData_Tubulacao("diametroext")

        'Retorna como string o maior número encontrado acrescentado de 1 
        With rbFResult
            .Add(New TypedValue(LispDataType.Text, CStr(Diametro)))
        End With

        Return rbFResult

    End Function

    'Return all information about objetc circuit for lisp - database
    Function ReturnData_Circuit(Optional ByVal TReturn As String = "diametroext", Optional ByVal Rbf As ResultBuffer = Nothing) As Object

        Try

            'Receive Area_Total_Fios, Taxa_Ocupacao - Calc Area_Eletroduto_Minima for search the best result in AELEPCONCEE
            Dim CodObj As String, DiametroExt As Double, Secao As String
            Dim RsResult As New System.Data.DataTable, ArrayInfo As Object, Conn As OleDbConnection
            Dim Da As OleDbDataAdapter

            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            'Database AHID
            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB

            'Get in temp variable
            ArrayInfo = Split(Rbf.AsArray(0).Value.ToString(), "|")
            CodObj = Trim(ArrayInfo(0))
            Secao = Trim(ArrayInfo(1))
            If Secao = "" Then Secao = "0"

            Da = New OleDbDataAdapter("SELECT * FROM AELEPCONCON WHERE CodEle = '" & CodObj & "' AND Tipo = 'FASE' AND Secao = " & CDbl(Secao) & "", Conn)
            Da.Fill(RsResult)

            If RsResult.Rows.Count > 0 Then
                Dim DrResult As System.Data.DataRow = RsResult.Rows(0)
                DiametroExt = DrResult("DiametroExt")
            End If

            Conn.Close() : Conn = Nothing
            RsResult = Nothing

            Dim RbfResult As New ResultBuffer

            Select Case TReturn
                Case ""
                    Return Nothing

                    'For default, return diametro
                Case Else
                    'Return for lisp
                    'Retorna como string o maior número encontrado acrescentado de 1 
                    Return DiametroExt

            End Select

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error settings ReturnData_Circuit - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA008")
            Return Nothing
        End Try

    End Function

    'Return information about layers and colors
    <LispFunction("ReturnData_LayerObject")> _
    Function ReturnData_LayerObject(ByVal Rbf As ResultBuffer) As ResultBuffer

        If Rbf Is Nothing Then Return Nothing
        If Rbf.AsArray(0).Value.ToString() = "" Then Return Nothing

        Try

            'Receive Area_Total_Fios, Taxa_Ocupacao - Calc Area_Eletroduto_Minima for search the best result in AELEPCONCEE
            Dim CodObj As String
            Dim RsResult As New System.Data.DataTable
            Dim Da As OleDbDataAdapter, Conn As OleDbConnection, rbFResult As New ResultBuffer

            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            'Database AHID
            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB
            'Get in temp variable
            CodObj = Rbf.AsArray(0).Value.ToString()

            'Aelebase - tabela 
            Da = New OleDbDataAdapter("SELECT ObjLeg, TObjBase.CodLay, TObjLay.LayNome, TObjLay.LayCor FROM TObjBase " & _
            " INNER JOIN TObjLay ON TObjLay.CodLay = TObjBase.CodLay WHERE CodObj = '" & CodObj & "'", Conn)
            Da.Fill(RsResult)

            If RsResult.Rows.Count > 0 Then
                Dim DrResult As System.Data.DataRow = RsResult.Rows(0)
                'Retorna como string o maior número encontrado acrescentado de 1 
                With rbFResult
                    .Add(New TypedValue(LispDataType.Text, DrResult("LayNome") & "|" & DrResult("LayCor")))
                End With
            Else
                'Retorna como string o maior número encontrado acrescentado de 1 
                With rbFResult
                    .Add(New TypedValue(LispDataType.Text, ""))
                End With
            End If

            RsResult = Nothing
            Conn.Close() : Conn = Nothing
            Return rbFResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error settings RDta_LayObj - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA009")
            Return Nothing
        End Try

    End Function

    'Return all information about objetc circuit for lisp - database
    Function ReturnData_Tubulacao(Optional ByVal TReturn As String = "diametroext", Optional ByVal Rbf As ResultBuffer = Nothing) As Object

        Try
            'Receive Area_Total_Fios, Taxa_Ocupacao - Calc Area_Eletroduto_Minima for search the best result in AELEPCONCEE
            Dim CodObj As String, Diametro As Double, AreaEletrodutoMinima As String
            Dim Da As OleDbDataAdapter, RsResult As New System.Data.DataTable, Conn As OleDbConnection, ArrayInfo As Object

            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            'Database AHID
            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB

            'Get in temp variable
            ArrayInfo = Split(Rbf.AsArray(0).Value.ToString(), "|")
            CodObj = Trim(ArrayInfo(0))
            AreaEletrodutoMinima = Trim(ArrayInfo(1))
            If AreaEletrodutoMinima = "" Then AreaEletrodutoMinima = "0"

            Da = New OleDbDataAdapter("SELECT Diametro, (DiametroExt - 2 * Espessura) * (DiametroExt - 2 * Espessura) AS AreaMinima" & _
            " FROM AELEPCONCEE" & _
            " WHERE CodEle = '" & CodObj & "'" & _
            " GROUP BY Diametro, (DiametroExt - 2 * Espessura) * (DiametroExt - 2 * Espessura) " & _
            " HAVING (DiametroExt - 2 * Espessura) * (DiametroExt - 2 * Espessura) >= " & Replace(AreaEletrodutoMinima, ",", "."), Conn)
            Da.Fill(RsResult)

            If RsResult.Rows.Count > 0 Then
                Dim DrResult As DataRow = RsResult.Rows(0)
                Diametro = DrResult("Diametro")
            End If

            RsResult = Nothing
            Conn.Close() : Conn = Nothing

            Select Case TReturn
                Case ""
                    Return Nothing

                    'For default, return diametro
                Case Else
                    'Return for lisp
                    Return Diametro

            End Select

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error settings ReturnData_Tubulacao - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA010")
            Return Nothing
        End Try

    End Function

    'Return all information of aelebase e aelepval. In this case, not pass anymore in txt aengedt and aengedt2
    <LispFunction("ReturnData_AengeCurva")> _
    Function ReturnData_AengeCurva(ByVal rBf As ResultBuffer) As ResultBuffer

        If rBf Is Nothing Then Return Nothing
        If rBf.AsArray(0).Value.ToString() = "" Then Return Nothing

        Try

            'Get parametrs for consult
            Dim UserS5 As String, ArrayInfo As Object, RbfResult As New ResultBuffer
            Dim CodTub As String, Eledline As Integer
            Dim rsImp As New System.Data.DataTable, Conn As OleDbConnection, Da As OleDbDataAdapter

            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            'Database AHID
            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB

            UserS5 = rBf.AsArray(0).Value.ToString()
            ArrayInfo = Split(UserS5, "|")
            CodTub = ArrayInfo(0) : Eledline = ArrayInfo(1)

            Da = New OleDbDataAdapter("SELECT * FROM Aelebase WHERE CodTub = '" & CodTub & "' AND Eledline = " & Eledline & "", Conn)
            Da.Fill(rsImp)

            If rsImp.Rows.Count <= 0 Then
                'Retorna como string o maior número encontrado acrescentado de 1 
                With RbfResult
                    .Add(New TypedValue(LispDataType.Text, "||"))
                End With
            Else
                Dim DrResult As DataRow = rsImp.Rows(0)
                'Retorna como string o maior número encontrado acrescentado de 1 
                With RbfResult
                    .Add(New TypedValue(LispDataType.Text, DrResult("CodEle") & "|" & DrResult("CodClas")))
                End With
            End If

            Return Nothing

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error settings ReturnData_AengeCurva - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA011")
            Return Nothing
        End Try

    End Function

    'Return all information of aelebase e aelepval. In this case, not pass anymore in txt aengedt and aengedt2
    <LispFunction("ReturnData_AengeTipoCurva")> _
    Function ReturnData_AengeTipoCurva(ByVal rBf As ResultBuffer) As ResultBuffer
        If rBf Is Nothing Then Return Nothing
        If rBf.AsArray(0).Value.ToString() = "" Then Return Nothing

        Try

            'Get parametrs for consult
            Dim UserS5 As String, ArrayInfo As Object, RbfResult As New ResultBuffer
            Dim CodObj As String, da As OleDbDataAdapter
            Dim rsImp As New System.Data.DataTable, Conn As OleDbConnection

            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            'Database AHID
            LibraryConnection.TypeDb = "AHID"
            Conn = LibraryConnection.Aenge_OpenConnectionDB

            UserS5 = rBf.AsArray(0).Value.ToString()
            ArrayInfo = Split(UserS5, "|")
            CodObj = ArrayInfo(0)

            da = New OleDbDataAdapter("SELECT * FROM Aelebase WHERE CodEle = '" & CodObj & "'", Conn)
            da.Fill(rsImp)

            If rsImp.Rows.Count <= 0 Then
                With RbfResult
                    .Add(New TypedValue(LispDataType.Text, ""))
                End With
            Else
                Dim DrResult As DataRow = rsImp.Rows(0)
                With RbfResult
                    .Add(New TypedValue(LispDataType.Text, CStr(DrResult("EleDline"))))
                End With

            End If

            Conn.Close() : Conn = Nothing
            rsImp = Nothing
            Return Nothing

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error settings ReturnData_AengeTipoCurva - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA012")
            Return Nothing
        End Try
    End Function

#End Region

#Region "----- Detalhes de Esgoto -----"

    'Complete string with data information in parameters
    Function CompleteCharacteres(ByVal StrOriginal As String, ByVal QuantComp As Int16, ByVal Side As String, ByVal CharCompl As String) As String

        Dim Result As String
        Result = StrOriginal

        Try

            If QuantComp < StrOriginal.Length Then Return StrOriginal

            For i = StrOriginal.ToString.Length To QuantComp - 1
                'Left or right insertion 
                If Side = "e" Then
                    Result = CharCompl & Result
                Else
                    Result = Result & CharCompl
                End If

            Next

            Return Result

        Catch ex As Exception

            Return StrOriginal
        End Try

    End Function

    <LispFunction("DetEsgoto_ConvertUnifilar")> _
    Function DetEsgoto_ConvertUnifilar(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim rbfResult As New ResultBuffer, Array_rBf As New ArrayList
        'Fields for tratament 
        Dim CodClas As String = "", CodObj As String = "", DiamIn As String = "", DiamOut As String = "", PtoX As String = "", PtoY As String = "", PtoZ As String = "", Ang As String = ""
        Dim CodRef As String = "", DescSymb As String = "", NameDwg As String = "", DescSymb_Original As String = "", StrSql As String = "", DwgNameBlock As String = ""
        Dim QuantSplit As Int16, ArrayTemp As Object, SymbMat As String, Altura As String, CodView As Int16 = 1, StrSql_NoDiam As String = ""
        'Campos adicionais passados pelo lisp nos ultimos da lista. Apenas retornar os campos 
        Dim Field1 As Object, Field2 As Object

        Dim Da_TObjClas As OleDbDataAdapter = Nothing, Da_TObjBase As OleDbDataAdapter = Nothing, Da_TObjPVal As OleDbDataAdapter = Nothing, Da_TObjDwg As OleDbDataAdapter = Nothing
        Dim Dt_TObjClas As New System.Data.DataTable, Dt_TObjBase As New System.Data.DataTable, Dt_TObjPVal As New System.Data.DataTable, Dt_TObjDwg As New System.Data.DataTable
        Dim NameDwgOriginal As String = ""

        Try
            'Inicio da lista
            rbfResult.Add(New TypedValue(LispDataType.ListBegin))

            'Get all objects in list resultbuffer passed lisp 
            For Each Obj As Object In rbf.AsArray
                'For start and end of list 
                If DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5016 And DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5017 Then
                    Array_rBf.Add(DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value.ToString)
                End If
            Next

            'for each object read in resultbuffer, get a unifilar object and create a resultbuffer result for lisp
            Dim DtDiagnostic As System.Data.DataTable, DrDiagnostic As System.Data.DataRow, LibCad As New LibraryCad
            DtDiagnostic = LibCad.CreateDataTable("diagnostico")
            LibCad = Nothing

            For Each ObjArray As Object In Array_rBf
                If ObjArray.ToString.Trim <> "" Then
                    'Clear all fields 
                    DescSymb = "" : PtoX = "" : PtoY = "" : PtoZ = "" : Ang = "" : CodRef = "" : CodObj = "" : Altura = "" : DiamIn = "" : DiamOut = "" : SymbMat = ""
                    DiamIn = 0 : DiamOut = 0 : NameDwg = "" : DescSymb_Original = "" : CodView = 1 : DwgNameBlock = ""
                    Field1 = "" : Field2 = ""
                    Da_TObjClas = Nothing : Dt_TObjClas = Nothing : Da_TObjPVal = Nothing : Dt_TObjPVal = Nothing
                    Dt_TObjClas = New System.Data.DataTable : Dt_TObjPVal = New System.Data.DataTable

                    QuantSplit = ObjArray.ToString.Split("|").Count
                    ArrayTemp = ObjArray.ToString.Split("|")
                    If QuantSplit >= 1 Then PtoX = ArrayTemp(0)
                    If QuantSplit >= 2 Then PtoY = ArrayTemp(1)
                    If QuantSplit >= 3 Then PtoZ = ArrayTemp(2)
                    If QuantSplit >= 4 Then Ang = ArrayTemp(3)
                    If QuantSplit >= 5 Then CodRef = ArrayTemp(4) 'Equal CodClas, but for consult use this field
                    If QuantSplit >= 6 Then CodObj = CompleteCharacteres(ArrayTemp(5), 6, "e", "0")
                    If QuantSplit >= 7 Then Altura = ArrayTemp(6)
                    If QuantSplit >= 8 Then DiamIn = ArrayTemp(7)
                    If QuantSplit >= 9 Then DiamOut = ArrayTemp(8)
                    If QuantSplit >= 10 Then SymbMat = ArrayTemp(9)
                    If QuantSplit >= 11 Then DescSymb_Original = ArrayTemp(10)
                    If QuantSplit >= 12 Then NameDwgOriginal = ArrayTemp(11)
                    If QuantSplit >= 13 Then Field1 = ArrayTemp(12)
                    If QuantSplit >= 14 Then Field2 = ArrayTemp(13)

                    If DescSymb_Original.ToString.ToLower.Contains("- frontal".ToLower) Then
                        CodView = 2
                    ElseIf DescSymb_Original.ToString.ToLower.Contains("- lateral".ToLower) Then
                        CodView = 3
                    End If

                    If ConnAHID Is Nothing Then ValidateConnection("AHID")
                    'Now, create a list result for unifilar respectively
                    Da_TObjClas = New OleDbDataAdapter("SELECT CodClas, ClasDes, ClasPCon, ClasComp, ClasFamily, ClasRef, CodMat FROM TObjClas" & _
                                                                         " WHERE ClasRef = '" & CodRef & "'", ConnAHID)
                    Da_TObjClas.Fill(Dt_TObjClas)
                    If Dt_TObjClas.Rows.Count > 0 Then
                        CodClas = Dt_TObjClas.Rows(0)("CodClas") '(1)
                    Else

                        'Save in diagnostic table information  TypeObject, Handle and Description
                        DrDiagnostic = DtDiagnostic.NewRow
                        DrDiagnostic(0) = 2   'Diversos 
                        DrDiagnostic(1) = ""
                        DrDiagnostic(2) = "Classe de referência do objeto não encontrada - Não foi possível encontrar o objeto similar ou não existe !"
                        DtDiagnostic.Rows.Add(DrDiagnostic)

                        GoTo ProxObj
                    End If

                    'Get all objects in TObjPVal 
                    Select Case CodClas
                        'some cases, prop3 is null, then consult prop2 only
                        Case "2930"
                            If DiamOut.ToString <> "" And DiamOut.ToString <> "0" Then
                                StrSql = "SELECT Id, CodObj, Prop2, Prop3, Prop4 FROM TObjPVal WHERE CodObj IN (SELECT CodObj FROM TObjBase" & _
                                                                             " WHERE CodClas = '" & CodClas & "' AND CodRef = '" & CodObj & "') AND Prop2 = '" & Double.Parse(DiamIn) & "'"
                            Else
                                StrSql = "SELECT Id, CodObj, Prop2, Prop3, Prop4 FROM TObjPVal WHERE CodObj IN (SELECT CodObj FROM TObjBase" & _
                                                                             " WHERE CodClas = '" & CodClas & "' AND CodRef = '" & CodObj & "') AND Prop2 = '" & Double.Parse(DiamIn) & "' AND Prop3 = '" & Double.Parse(DiamOut) & "'"
                            End If
                            StrSql_NoDiam = "SELECT DISTINCT Prop2, Prop3 FROM TObjPVal WHERE CodObj IN (SELECT CodObj FROM TObjBase WHERE CodClas = '" & CodClas & "' AND CodRef = '" & CodObj & "')"

                            'Some cases, prop4 is null, then consult prop3 only
                        Case "2935"
                            If DiamOut.ToString <> "" And DiamOut.ToString <> "0" Then
                                StrSql = "SELECT Id, CodObj, Prop2, Prop3, Prop4 FROM TObjPVal WHERE CodObj IN (SELECT CodObj FROM TObjBase" & _
                                                                             " WHERE CodClas = '" & CodClas & "' AND CodRef = '" & CodObj & "') AND Prop3 = '" & Double.Parse(DiamIn) & "' AND Prop4 = '" & Double.Parse(DiamOut) & "'"
                            Else
                                StrSql = "SELECT Id, CodObj, Prop2, Prop3, Prop4 FROM TObjPVal WHERE CodObj IN (SELECT CodObj FROM TObjBase" & _
                                                                             " WHERE CodClas = '" & CodClas & "' AND CodRef = '" & CodObj & "') AND Prop3 = '" & Double.Parse(DiamIn) & "'"
                            End If
                            StrSql_NoDiam = "SELECT DISTINCT Prop3, Prop4 FROM TObjPVal WHERE CodObj IN (SELECT CodObj FROM TObjBase" & _
                                                                         " WHERE CodClas = '" & CodClas & "' AND CodRef = '" & CodObj & "')"

                            '2920, 2940, 2950
                        Case Else
                            StrSql = "SELECT Id, CodObj, Prop2, Prop3, Prop4 FROM TObjPVal WHERE CodObj IN (SELECT CodObj FROM TObjBase" & _
                                                                         " WHERE CodClas = '" & CodClas & "' AND CodRef = '" & CodObj & "') AND Prop2 = '" & Double.Parse(DiamIn) & "'"
                            StrSql_NoDiam = "SELECT DISTINCT Prop2 FROM TObjPVal WHERE CodObj IN (SELECT CodObj FROM TObjBase WHERE CodClas = '" & CodClas & "' AND CodRef = '" & CodObj & "')"

                    End Select

                    Da_TObjPVal = New OleDbDataAdapter(StrSql, ConnAHID)
                    Da_TObjPVal.Fill(Dt_TObjPVal)
                    'Íf exists, then we find register in tobjpval
                    If Dt_TObjPVal.Rows.Count > 0 Then

                        Dim Dr_TObjPVal As System.Data.DataRow = Dt_TObjPVal.Rows(0)
                        Select Case CodClas
                            Case "2930"
                                DiamIn = Dr_TObjPVal("Prop2")
                                If Dr_TObjPVal("Prop3").ToString <> "" Then DiamOut = Dr_TObjPVal("Prop3")

                            Case "2935"
                                DiamIn = Dr_TObjPVal("Prop3")
                                If Dr_TObjPVal("Prop4").ToString <> "" Then DiamOut = Dr_TObjPVal("Prop4")

                            Case Else
                                DiamIn = Dr_TObjPVal("Prop2")
                                DiamOut = 0

                        End Select

                        'Get a new CodObj and our CodClas
                        CodObj = Dr_TObjPVal("CodObj")
                        Da_TObjBase = Nothing : Dt_TObjBase = Nothing
                        Dt_TObjBase = New System.Data.DataTable
                        'Search in tObjbase all objects with codclas = CodClas up(1) - In this case we have a list, with this list we consult inn tobjpval and search for diamt respectively
                        Da_TObjBase = New OleDbDataAdapter("SELECT Id, CodObj, CodClas, ObjMenu,ObjLeg, CodLay, ObjCor, ObjEsc, ObjLType, ObjLtwid, ObjDLine, ObjDeclive,CodRef, Erased FROM TObjBase" & _
                                                                             " WHERE CodObj = '" & CodObj & "'", ConnAHID)
                        Da_TObjBase.Fill(Dt_TObjBase)
                        If Dt_TObjBase.Rows.Count > 0 Then
                            DescSymb = Dt_TObjBase.Rows(0)("ObjLeg") ': CodObj = Dt_TObjBase.Rows(0)("CodObj")
                            CodClas = Dt_TObjBase.Rows(0)("CodClas")
                        End If

                        'Get a dwg block for insert in drawing 
                        'Get all objects in TObjPVal 
                        Da_TObjDwg = Nothing : Dt_TObjDwg = Nothing
                        Dt_TObjDwg = New System.Data.DataTable
                        Da_TObjDwg = New OleDbDataAdapter("SELECT Id, CodObj, CodView, DwgName FROM TObjDwg WHERE CodObj = '" & CodObj & "' AND CodView = " & CodView, ConnAHID)
                        Da_TObjDwg.Fill(Dt_TObjDwg)
                        If Dt_TObjDwg.Rows.Count > 0 Then NameDwg = Dt_TObjDwg.Rows(0)("DwgName").ToString

                        'Now, we create a list return for this object consult 
                        Dim PtoInsert As New Autodesk.AutoCAD.Geometry.Point3d(PtoX, PtoY, PtoZ)

                        'Create a list in resulbuffer - With order return : pto, angle, codclas, codobj, altura, diamin, diamout, dessymb, symbmat
                        With rbfResult
                            .Add(New TypedValue(LispDataType.ListBegin))
                            .Add(New TypedValue(LispDataType.Point3d, PtoInsert))    '.Add(New TypedValue(LispDataType.Text, Dr("Quadro").ToString & "|" & Dr("Circuito").ToString))
                            .Add(New TypedValue(LispDataType.Double, Double.Parse(Ang)))
                            .Add(New TypedValue(LispDataType.Text, CodClas))
                            .Add(New TypedValue(LispDataType.Text, CodObj))
                            .Add(New TypedValue(LispDataType.Text, Altura))
                            .Add(New TypedValue(LispDataType.Text, DiamIn))
                            .Add(New TypedValue(LispDataType.Text, DiamOut))
                            .Add(New TypedValue(LispDataType.Text, DescSymb))
                            .Add(New TypedValue(LispDataType.Text, SymbMat))
                            .Add(New TypedValue(LispDataType.Text, NameDwg))
                            .Add(New TypedValue(LispDataType.Text, Field1.ToString))
                            .Add(New TypedValue(LispDataType.Text, Field2.ToString))
                            .Add(New TypedValue(LispDataType.ListEnd))
                        End With

                        'Dont find diam and objects in search -- Neste caso nao lanca de volta os dados, entao pegaremos as informacoes que foram passadas originalmente e retornaremos elas para o lisp da maneira que chegou
                        'juntamente com o nome do dwg que foi passado por ultimo
                    Else

                        'Adiciona o registro original de qq maneira, pois nao encontrou
                        'Now, we create a list return for this object consult 
                        Dim PtoInsert As New Autodesk.AutoCAD.Geometry.Point3d(PtoX, PtoY, PtoZ)
                        'Create a list in resulbuffer - With order return : pto, angle, codclas, codobj, altura, diamin, diamout, dessymb, symbmat
                        With rbfResult
                            .Add(New TypedValue(LispDataType.ListBegin))
                            .Add(New TypedValue(LispDataType.Point3d, PtoInsert))    '.Add(New TypedValue(LispDataType.Text, Dr("Quadro").ToString & "|" & Dr("Circuito").ToString))
                            .Add(New TypedValue(LispDataType.Double, Double.Parse(Ang)))
                            .Add(New TypedValue(LispDataType.Text, CodRef))
                            .Add(New TypedValue(LispDataType.Text, CodObj))
                            .Add(New TypedValue(LispDataType.Text, Altura))
                            .Add(New TypedValue(LispDataType.Text, DiamIn))
                            .Add(New TypedValue(LispDataType.Text, DiamOut))
                            .Add(New TypedValue(LispDataType.Text, DescSymb))
                            .Add(New TypedValue(LispDataType.Text, SymbMat))
                            .Add(New TypedValue(LispDataType.Text, NameDwgOriginal))
                            .Add(New TypedValue(LispDataType.Text, Field1.ToString))
                            .Add(New TypedValue(LispDataType.Text, Field2.ToString))
                            .Add(New TypedValue(LispDataType.ListEnd))
                        End With

                        Dim DaDiagnostic As New OleDbDataAdapter(StrSql_NoDiam, ConnAHID)
                        Dim DtDiagnostic_Consult As New System.Data.DataTable, StrTemp As String = ""
                        DaDiagnostic.Fill(DtDiagnostic_Consult)

                        Da_TObjBase = Nothing
                        Dt_TObjBase = Nothing : Dt_TObjBase = New System.Data.DataTable
                        'Search in tObjbase all objects with codclas = CodClas up(1) - In this case we have a list, with this list we consult inn tobjpval and search for diamt respectively
                        Da_TObjBase = New OleDbDataAdapter("SELECT Id, CodObj, CodClas, ObjMenu,ObjLeg, CodLay, ObjCor, ObjEsc, ObjLType, ObjLtwid, ObjDLine, ObjDeclive,CodRef, Erased FROM TObjBase" & _
                                                                             " WHERE CodObj = '" & CodObj & "'", ConnAHID)
                        Da_TObjBase.Fill(Dt_TObjBase)
                        If Dt_TObjBase.Rows.Count > 0 Then
                            DescSymb = Dt_TObjBase.Rows(0)("ObjLeg")
                        End If

                        'Save in diagnostic table information  TypeObject, Handle and Description
                        DrDiagnostic = DtDiagnostic.NewRow

                        'In this case, not exists objects 
                        If DtDiagnostic_Consult.Rows.Count <= 0 Then
                            DrDiagnostic(0) = 0   'Diametro
                            DrDiagnostic(1) = ""
                            DrDiagnostic(2) = "Não existe(m) objeto(s) relacionado(s) ao objeto --> '" & DescSymb & "' !"

                            'Exists objects, but diam not exists 
                        Else
                            DrDiagnostic(0) = 0   'Diametro
                            DrDiagnostic(1) = ""
                            For Each DrTemp As DataRow In DtDiagnostic_Consult.Rows
                                Select Case CodClas
                                    Case "2930"
                                        If DrTemp("Prop3").ToString <> "" Then
                                            StrTemp += "   " & DrTemp("Prop2").ToString
                                        Else
                                            StrTemp += "   (Entrada:" & DrTemp("Prop2").ToString & " Saida:" & DrTemp("Prop3").ToString & ")"
                                        End If

                                    Case "2935"
                                        If DrTemp("Prop4").ToString <> "" Then
                                            StrTemp += "   " & DrTemp("Prop3").ToString
                                        Else
                                            StrTemp += "   (Entrada:" & DrTemp("Prop3").ToString & " Saida:" & DrTemp("Prop4").ToString & ")"
                                        End If

                                    Case Else
                                        StrTemp += "   " & DrTemp("Prop2").ToString

                                End Select

                            Next
                            DrDiagnostic(2) = "Diametros disponíveis para o objeto ('" & DescSymb & "') :" & StrTemp
                        End If
                        DtDiagnostic.Rows.Add(DrDiagnostic)
                    End If

                End If
ProxObj:
            Next

            'End of default list
            rbfResult.Add(New TypedValue(LispDataType.ListEnd))

            'If exists diagnostic, then call form 
            If DtDiagnostic.Rows.Count > 0 Then
                frm_Diagnostic = New frmDiagnostic
                With frm_Diagnostic
                    .TypeFunction = "detesgoto_convertunifilar"
                    .DataTable_Diagnostic = DtDiagnostic
                    .ShowDialog()
                End With
                frm_Diagnostic = Nothing
            End If

            Return rbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Erro ao converter dados do unifilar - lsp - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryCad013")
            Return Nothing
        End Try

    End Function

    <LispFunction("Detesgoto_Convertunifilar_NewDesactivate")> _
    Function detesgoto_convertunifilar_NewDesactivate(ByVal rbf As ResultBuffer) As ResultBuffer

        Dim ArrayStr As New ArrayList

        Try

            For Each Obj As Object In rbf.AsArray
                'For start and end of list 
                If DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5016 And DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).TypeCode <> 5017 Then
                    If Not DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value Is Nothing Then
                        ArrayStr.Add(DirectCast(Obj, Autodesk.AutoCAD.DatabaseServices.TypedValue).Value)
                    End If
                End If
            Next

            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            If ConnAhid Is Nothing Then
                With LibraryConnection
                    .TypeDb = "AHID_"
                    ConnAhid = .Aenge_OpenConnectionDB
                End With
            End If
            LibraryConnection = Nothing

            Dim RbfResult As New ResultBuffer
            Dim PtoX As Double = 0, PtoY As Double = 0, PtoZ As Double = 0
            Dim ValReal_Field4 As Double = 0
            Dim CodClasse As String = "", CodSymbol As String = ""
            Dim Alt As Decimal = 0, DiamEntrada As Decimal = 0, DiamSaida As Decimal = 0
            Dim Material As String = "", Descricao As String = "", DwgName As String = "", CodView As Byte = 1
            'Para a consulta do dwg relacionado ao objeto 
            Dim Da As New OleDbDataAdapter("Select * From TObjDwg", ConnAhid)
            Dim DtConsult As New System.Data.DataTable
            Da.Fill(DtConsult)

            RbfResult.Add(New TypedValue(LispDataType.ListBegin))  '##1

            For Each ObjArray As Object In ArrayStr

                'Limpa todas as variaveis antes
                PtoX = 0
                PtoY = 0
                PtoZ = 0
                ValReal_Field4 = 0
                CodClasse = ""
                CodSymbol = ""
                Alt = 0
                DiamEntrada = 0
                DiamSaida = 0
                Material = ""
                Descricao = ""
                DwgName = ""
                CodView = 1

                Dim ArrayTemp As Object = ObjArray.ToString.Split("|")
                PtoX = ArrayTemp(0)
                PtoY = ArrayTemp(1)
                PtoZ = ArrayTemp(2)
                If IsNumeric(ArrayTemp(3)) Then ValReal_Field4 = ArrayTemp(3)
                CodClasse = ArrayTemp(4)
                CodSymbol = ArrayTemp(5)
                If IsNumeric(ArrayTemp(6)) Then Alt = ArrayTemp(6)
                If IsNumeric(ArrayTemp(7)) Then DiamEntrada = ArrayTemp(7)
                If IsNumeric(ArrayTemp(8)) Then DiamSaida = ArrayTemp(8)
                Material = ArrayTemp(9)
                Descricao = ArrayTemp(10)

                DwgName = "" : CodView = 1
                If Descricao.ToLower.Contains("- frontal".ToLower) Then
                    CodView = 2
                ElseIf Descricao.ToLower.Contains("- lateral".ToLower) Then
                    CodView = 3
                End If

                If DtConsult.Select("CodObj = '" & CodSymbol & "' and CodView = " & CodView).Length > 0 Then DwgName = DtConsult.Select("CodObj = '" & CodSymbol & "' and CodView = " & CodView)(0)("DwgName").ToString

                Dim PtoTemp As New Autodesk.AutoCAD.Geometry.Point3d(PtoX, PtoY, PtoZ)

                With RbfResult
                    .Add(New TypedValue(LispDataType.ListBegin))
                    .Add(New TypedValue(LispDataType.Point3d, PtoTemp))
                    .Add(New TypedValue(LispDataType.Double, ValReal_Field4))
                    .Add(New TypedValue(LispDataType.Text, CodClasse))
                    .Add(New TypedValue(LispDataType.Text, CodSymbol))
                    .Add(New TypedValue(LispDataType.Double, Alt))
                    .Add(New TypedValue(LispDataType.Double, DiamEntrada))
                    .Add(New TypedValue(LispDataType.Double, DiamSaida))
                    .Add(New TypedValue(LispDataType.Text, Descricao))
                    .Add(New TypedValue(LispDataType.Text, Material))
                    .Add(New TypedValue(LispDataType.Text, DwgName))
                    .Add(New TypedValue(LispDataType.ListEnd))
                End With

            Next

            RbfResult.Add(New TypedValue(LispDataType.ListEnd))  '##1

            Return RbfResult

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "----- Legend -----"

    <LispFunction("LegendaHidraulica")> _
    Function Load_LegHidraulicac(ByVal rbf As ResultBuffer) As ResultBuffer
        Load_LegHidraulica(rbf)
        Return Nothing
    End Function

    <LispFunction("Ahid_Legenda")> _
    Function Load_LegHidraulica(ByVal rbf As ResultBuffer) As ResultBuffer
        'Pass information for configurate legend eletric 
        'Executa o comando para limpar selecoes 
        Dim Comando As String = "(setq ss_vba nil) "
        Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(Comando, True, False, False)
        System.Windows.Forms.Application.DoEvents()

        If frm_Legenda Is Nothing Then frm_Legenda = New frmLegenda
        With frm_Legenda
            .Cab_Legenda = "INSERCAO DE LEGENDA"
            .ShowDialog()
        End With
        frm_Legenda = Nothing
        Return Nothing
    End Function

    <LispFunction("LegendAuto")> _
    Function LegendAuto(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim rBfResult As New ResultBuffer

        Try

            frm_Legenda = New frmLegenda
            With frm_Legenda
                .ShowDialog()
            End With

            Return rBfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error loading legenda - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0024")
            Return Nothing
        Finally
            frm_Legenda = Nothing
        End Try

    End Function

#End Region

#End Region

#End Region

#Region "----- Functions (General and Commands Cad) -----"

    'Return information about layers and colors
    <LispFunction("ReturnData_FieldAeleBase")> _
    Function ReturnData_FieldAeleBase(ByVal Rbf As ResultBuffer) As ResultBuffer

        If Rbf Is Nothing Then Return Nothing
        If Rbf.AsArray(0).Value.ToString() = "" Then Return Nothing

        Try

            'Receive Area_Total_Fios, Taxa_Ocupacao - Calc Area_Eletroduto_Minima for search the best result in AELEPCONCEE
            Dim CodObj As String, ColumnName As String = "ELEESC"
            Dim RsResult As New System.Data.DataTable
            Dim Da As OleDbDataAdapter, Conn As OleDbConnection, rbFResult As New ResultBuffer

            If LibraryConnection Is Nothing Then LibraryConnection = New Aenge.Library.Db.LibraryConnection
            'Database apower
            LibraryConnection.TypeDb = "APOWER"
            Conn = LibraryConnection.Aenge_OpenConnectionDB
            'Get in temp variable
            CodObj = Rbf.AsArray(0).Value.ToString().Split("|")(0)
            If Rbf.AsArray(0).Value.ToString().Split("|").Count >= 2 Then ColumnName = Rbf.AsArray(0).Value.ToString().Split("|")(1)

            Da = New OleDbDataAdapter("SELECT Id, CodEle, CodClas, EleMenu, EleLeg, EleNome, CodLay, EleCor, EleEsc, EleLtype, EleLTwid, EleDLine, CodTub, Erased " & _
                                                                 " FROM AeleBase" & _
                                                                 " WHERE CodEle = '" & CodObj & "'", Conn)
            Da.Fill(RsResult)

            If RsResult.Rows.Count > 0 Then
                Dim DrResult As System.Data.DataRow = RsResult.Rows(0)
                'Retorna como string o maior número encontrado acrescentado de 1 
                With rbFResult
                    .Add(New TypedValue(LispDataType.Text, DrResult(ColumnName.Trim).ToString))
                End With

            Else
                'Retorna como string o maior número encontrado acrescentado de 1 
                With rbFResult
                    .Add(New TypedValue(LispDataType.Text, ""))
                End With
            End If

            RsResult = Nothing
            Conn.Close() : Conn = Nothing
            Return rbFResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Error settings RDta_FAeleB - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0026")
            Return Nothing
        End Try

    End Function

    'Retorna a fonte padrao cadastrada no arquivo inicial
    <LispFunction("FontDefault_AengeStyle")> _
    Function FontDefault_AengeStyle(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim RbfResult As New ResultBuffer
        Dim FontIni As String = Aenge_GetCfg("Configuration", "FontDefault_AengeSytle", GetAppInstall() & "Iniapp.ini")
        Dim FontDefault As String = Aenge_GetCfg("Configuration", "FontDefault", GetAppInstall() & "Iniapp.ini")

        Try

            'Retorna como string o maior número encontrado acrescentado de 1 
            With RbfResult
                .Add(New TypedValue(LispDataType.Text, FontIni))
            End With

        Catch ex As Exception

            MsgBox("Error setting aengestyle font default - Err: " & ex.Message)
            If FontDefault = "" Then FontDefault = "Arial"
            With RbfResult
                .Add(New TypedValue(LispDataType.Text, FontDefault))
            End With

        End Try

        Return RbfResult

    End Function

    'Altera as fontes do styles do Cad
    <LispFunction("defaultstyle")> _
    Function AlterStyleFont(ByVal rbf As ResultBuffer) As ResultBuffer

        '' Get the current document and database
        Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
        Dim acCurDb As Database = acDoc.Database

        '' Start a transaction
        Using acTrans As Transaction = acCurDb.TransactionManager.StartTransaction()

            '' Open the current text style for write
            Dim acTextStyleTblRec As TextStyleTableRecord
            acTextStyleTblRec = acTrans.GetObject(acCurDb.Textstyle, OpenMode.ForWrite)
            '' Get the current font settings
            Dim acFont As Autodesk.AutoCAD.GraphicsInterface.FontDescriptor
            acFont = acTextStyleTblRec.Font
            '' Update the text style's typeface with "PlayBill"
            Dim acNewFont As Autodesk.AutoCAD.GraphicsInterface.FontDescriptor
            acNewFont = New Autodesk.AutoCAD.GraphicsInterface.FontDescriptor("Arial Unicode MS", _
                                                                acFont.Bold, _
                                                                acFont.Italic, _
                                                                acFont.CharacterSet, _
                                                                acFont.PitchAndFamily)

            acTextStyleTblRec.Font = acNewFont

            acDoc.Editor.Regen()

            '' Save the changes and dispose of the transaction
            acTrans.Commit()
        End Using

        Return Nothing
    End Function

    'Formata as informações recebidas pelo txt para fazer o array de leitura e atualização dos atributos. Utilizado pela função acima
    'EditBlock para atualização das informações de acordo com o handle na lista dos objetos
    Function FormataArrayTxt(ByVal CorpoTxt As String, ByVal ArrayFinal() As String) As Object

        Dim ArrayTexto() As String, QuantChar As Integer
        Dim I As Integer, Count As Integer, j As Integer

        I = 0 : Count = 0 : j = 0

        ArrayTexto = Split(CorpoTxt, "|")
        QuantChar = UBound(ArrayTexto) + 1

        For I = 0 To QuantChar
            'Verifica se é o primeiro ou segundo do array
            If Count = 1 Then
                ArrayFinal(j) = ArrayFinal(j) & "|" & ArrayTexto(I)
                j = j + 1 : Count = 0
            Else
                ArrayFinal(j) = ArrayTexto(I)
                Count = 1
            End If
        Next

        Return ArrayFinal

    End Function


    'Formata as informações recebidas pelo txt para fazer o array de leitura e atualização dos atributos. Utilizado pela função acima
    'EditBlock para atualização das informações de acordo com o handle na lista dos objetos
    Function FormataArrayTxt(ByVal CorpoTxt As String) As Object

        Dim ArrayTexto() As String, QuantChar As Integer, ArrayFinal As New ArrayList
        Dim I As Integer, Count As Integer, j As Integer

        I = 0 : Count = 0 : j = 0

        ArrayTexto = Split(CorpoTxt, "|")
        QuantChar = UBound(ArrayTexto) + 1

        For I = 0 To ArrayTexto.Length - 1 'QuantChar
            'Verifica se é o primeiro ou segundo do array
            If Count = 1 Then
                ArrayFinal.Add(ArrayTexto(I - 1) & "|" & ArrayTexto(I))
                j = j + 1 : Count = 0
            Else
                'ArrayFinal.Add(ArrayTexto(I))
                Count = 1
            End If
        Next

        Return ArrayFinal

    End Function

    'Edita os atributos de um determinado blockReference - Faz a edição dos atributos do blocos
    <LispFunction("EditBlock")> _
    Public Sub EditBlock(ByVal rBf As ResultBuffer)

        If rBf Is Nothing Then Exit Sub
        If rBf.AsArray(0).Value.ToString() = "" Then Exit Sub

        Dim Elem As BlockReference, LibraryCad As New LibraryCad
        Dim objDoc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Dim db As Database = objDoc.Database, RbfResult As ResultBuffer = Nothing
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        Dim NewCirc = "", NewRet(5) As String
        'Only return for lisp the result of xdata 
        Dim HandleObj As String

        Try

            'Declarações para leitura
            Dim sText, CorpoObj As String, sTextFull
            Dim ArrayFinal As New ArrayList, RotateAtt As Boolean = True

            'Teste para validação - Usado somente para testes
            'Application.ActiveDocument.SetVariable "USERS5", "USUSU¹2EA0   020|TESTE|012|200|004|600"

            'Obtem da variavel, usa o ¹ para fazer um split na variavel com as informações a serem atualizadas
            sTextFull = Split(rBf.AsArray(0).Value.ToString(), "¹")
            'Recupera as informações atraves da variavel do sistema
            sText = sTextFull(1)
            'Caso não tenha sido repassado nenhuma informação
            If sText = "" Then Exit Sub

            Dim ObjArray As Object, ObjValue As Object, j As Integer
            HandleObj = Replace(Mid(sText, 1, 7), " ", "")
            CorpoObj = Mid(sText, 8, Len(sText) - 7)
            Err.Clear()

            Using tr As Transaction = ed.Document.Database.TransactionManager.StartTransaction()

                'Aqui faz a leitura para cada objeto a ser lido
                Elem = LibraryCad.ObjectFromHandle(HandleObj)
                ArrayFinal = FormataArrayTxt(CorpoObj)
                j = 0

                If TypeOf Elem Is BlockReference Then
                    If (Not CType(Elem, BlockReference).AttributeCollection Is Nothing) Then
                        Dim attId As ObjectId
                        For Each attId In CType(Elem, BlockReference).AttributeCollection
                            Dim XdataObj As Object = Elem.GetXDataForApplication("AHID")
                            'For objects "0201" not rotate attribute...
                            'If Not XdataObj Is Nothing Then If UBound(XdataObj.asarray) >= 1 Then If DirectCast(XdataObj.asarray, System.Object)(1).Value.ToString = "0201" Then RotateAtt = False
                            Dim att As AttributeReference = TryCast(attId.GetObject(Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead, False), AttributeReference)
                            att.UpgradeOpen()

                            'Verifica se não está vazio
                            For Each ObjArray In ArrayFinal
                                If ObjArray <> "" Then
                                    'Faz a varredura e recupera o novo valor a ser setado
                                    If Mid(ObjArray, 1, 3) = att.Tag Then
                                        ObjValue = Trim(Mid(ObjArray, 5, 255))
                                        att.TextString = ObjValue
                                        If RotateAtt Then att.Rotation = 0
                                        'Update information in attributes 
                                        att.DowngradeOpen()
                                        Exit For
                                    End If
                                End If
                            Next

                        Next
                    End If

                End If
                'Commit operations in end
                tr.Commit()
            End Using

        Catch ex As Exception
            Select Case Err.Number
                Case 13

                Case Else
                    LibraryError.CreateErrorAenge(Err, "Error settings BlockAtt - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA005")

            End Select

        Finally
            LibraryReference = Nothing
        End Try

    End Sub

    'Edita os atributos de um determinado blockReference - Faz a edição dos atributos do blocos
    ' '' ''<LispFunction("EditBlock_COM")> _
    ' '' ''Public Sub EditBlock_COM(ByVal rBf As ResultBuffer)

    ' '' ''    If rBf Is Nothing Then Exit Sub
    ' '' ''    If rBf.AsArray(0).Value.ToString() = "" Then Exit Sub

    ' '' ''    If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
    ' '' ''    Dim Elem As AcadBlockReference
    ' '' ''    Dim objDoc As Autodesk.AutoCAD.Interop.AcadDocument
    ' '' ''    Dim AcadApp As AcadApplication = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication

    ' '' ''    Try
    ' '' ''        '
    ' '' ''        objDoc = AcadApp.ActiveDocument       'Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument

    ' '' ''        'Declarações para leitura
    ' '' ''        Dim sText, HandleObj As String, CorpoObj As String, sTextFull
    ' '' ''        Dim ArrayFinal() As String, AtributoObj As AcadAttributeReference
    ' '' ''        ReDim ArrayFinal(20)

    ' '' ''        'Teste para validação - Usado somente para testes
    ' '' ''        'Application.ActiveDocument.SetVariable "USERS5", "USUSU¹2EA0   020|TESTE|012|200|004|600"

    ' '' ''        'Obtem da variavel, usa o ¹ para fazer um split na variavel com as informações a serem atualizadas
    ' '' ''        sTextFull = Split(rBf.AsArray(0).Value.ToString(), "¹")
    ' '' ''        'Recupera as informações atraves da variavel do sistema
    ' '' ''        sText = sTextFull(1)
    ' '' ''        'Caso não tenha sido repassado nenhuma informação
    ' '' ''        If sText = "" Then Exit Sub

    ' '' ''        Dim ObjArray As Object, ObjValue As Object, j As Integer
    ' '' ''        HandleObj = Replace(Mid(sText, 1, 7), " ", "")
    ' '' ''        CorpoObj = Mid(sText, 8, Len(sText) - 7)

    ' '' ''        Err.Clear()

    ' '' ''        'Aqui faz a leitura para cada objeto a ser lido
    ' '' ''        Elem = objDoc.HandleToObject(HandleObj)
    ' '' ''        FormataArrayTxt(CorpoObj, ArrayFinal)
    ' '' ''        j = 0

    ' '' ''        'MsgBox Elem.Name
    ' '' ''        If Elem.HasAttributes Then

    ' '' ''            'Faz a leitura para cada um dos atributos do objeto carregado

    ' '' ''            For j = LBound(Elem.GetAttributes) To UBound(Elem.GetAttributes)
    ' '' ''                AtributoObj = Elem.GetAttributes(j)
    ' '' ''                For Each ObjArray In ArrayFinal
    ' '' ''                    'Verifica se não está vazio
    ' '' ''                    If ObjArray <> "" Then
    ' '' ''                        'Faz a varredura e recupera o novo valor a ser setado
    ' '' ''                        If Mid(ObjArray, 1, 3) = AtributoObj.TagString Then
    ' '' ''                            ObjValue = Trim(Mid(ObjArray, 5, 255))
    ' '' ''                            AtributoObj.TextString = ObjValue
    ' '' ''                            AtributoObj.Rotation = 0
    ' '' ''                        End If
    ' '' ''                    End If

    ' '' ''                Next

    ' '' ''            Next

    ' '' ''        End If

    ' '' ''        'Atualiza as informações do elemento
    ' '' ''        Elem.Update()

    ' '' ''    Catch ex As Exception
    ' '' ''        Select Case Err.Number
    ' '' ''            Case 13

    ' '' ''            Case Else
    ' '' ''                LibraryError.CreateErrorAenge(Err, "Error settings BlockAtt - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA005")

    ' '' ''        End Select

    ' '' ''    Finally
    ' '' ''        LibraryReference = Nothing
    ' '' ''    End Try

    ' '' ''End Sub

    'Edita os angulos dos atributos
    <LispFunction("EditAtt_Ang")> _
    Sub EditAtt_Ang(ByVal rBf As ResultBuffer)

        If rBf Is Nothing Then Exit Sub
        If rBf.AsArray(0).Value.ToString() = "" Then Exit Sub

        Dim Elem As BlockReference, LibraryCad As New LibraryCad
        Dim objDoc As Document = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
        Dim db As Database = objDoc.Database, RbfResult As ResultBuffer = Nothing
        Dim ed As Editor = Application.DocumentManager.MdiActiveDocument.Editor
        'Only return for lisp the result of xdata 
        Dim HandleObj As String

        Try

            'Teste para validação - Usado somente para testes
            HandleObj = rBf.AsArray(0).Value.ToString()
            'Aqui faz a leitura para cada objeto a ser lido
            Elem = LibraryCad.ObjectFromHandle(HandleObj)

            Using tr As Transaction = ed.Document.Database.TransactionManager.StartTransaction()
                'Aqui faz a leitura para cada objeto a ser lido
                Elem = LibraryCad.ObjectFromHandle(HandleObj)
                If TypeOf Elem Is BlockReference Then
                    If (Not CType(Elem, BlockReference).AttributeCollection Is Nothing) Then

                        Dim ArrayXData As Object = CType(Elem, BlockReference).GetXDataForApplication("AHID")
                        If ("|" & My.Settings.ClassExclude_EditAtt_Ang & "|").ToString.Contains(("|" & DirectCast(ArrayXData.asarray, System.Object)(1).Value.ToString & "|")) Then GoTo DontSet

                        Dim attId As ObjectId
                        For Each attId In CType(Elem, BlockReference).AttributeCollection
                            Dim att As AttributeReference = TryCast(attId.GetObject(Autodesk.AutoCAD.DatabaseServices.OpenMode.ForRead, False), AttributeReference)
                            att.UpgradeOpen()
                            att.Rotation = 0
                            'Update information in attributes 
                            att.DowngradeOpen()
                        Next
                    End If
                End If
dontSet:
                'Commit operations in end
                tr.Commit()
            End Using

        Catch ex As Exception
            Select Case Err.Number
                Case 13

                Case Else
                    LibraryError.CreateErrorAenge(Err, "Error settings BlockAttAng - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA006")

            End Select

        Finally
            LibraryReference = Nothing
        End Try
    End Sub

    'Edita os angulos dos atributos
    <LispFunction("EditAtt_Ang_COM")> _
    Sub EditAtt_Ang_COM(ByVal rBf As ResultBuffer)

        'If rBf Is Nothing Then Exit Sub
        'If rBf.AsArray(0).Value.ToString() = "" Then Exit Sub
        'If LibraryReference Is Nothing Then LibraryReference = New LibraryReference

        'Try

        '    Dim Elem As AcadBlockReference, AcadAp As Autodesk.AutoCAD.Interop.AcadApplication = LibraryReference.ReturnAcadReference
        '    Dim objDoc As Autodesk.AutoCAD.Interop.AcadDocument
        '    Dim I As Int16 = 0, J As Int16 = 0

        '    objDoc = AcadAp.ActiveDocument

        '    'Declarações para leitura
        '    Dim HandleObj As String
        '    Dim ArrayFinal() As String, AtributoObj As AcadAttributeReference
        '    ReDim ArrayFinal(20)

        '    'Teste para validação - Usado somente para testes
        '    HandleObj = rBf.AsArray(0).Value.ToString()
        '    Err.Clear()
        '    'Aqui faz a leitura para cada objeto a ser lido
        '    Elem = objDoc.HandleToObject(HandleObj)
        '    J = 0

        '    'MsgBox Elem.Name
        '    If Elem.HasAttributes Then
        '        'Faz a leitura para cada um dos atributos do objeto carregado
        '        For J = LBound(Elem.GetAttributes) To UBound(Elem.GetAttributes)
        '            AtributoObj = Elem.GetAttributes(J)
        '            AtributoObj.Rotation = 0
        '        Next
        '    End If

        '    'Atualiza as informações do elemento
        '    Elem.Update()

        'Catch ex As Exception
        '    Select Case Err.Number
        '        Case 13

        '        Case Else
        '            LibraryError.CreateErrorAenge(Err, "Error settings BlockAttAng - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA006")

        '    End Select

        'Finally
        '    LibraryReference = Nothing
        'End Try
    End Sub

    'Função que recebe uma lista com os handles para alterar as cores do bloco
    <LispFunction("EditColorBlock")> _
    Sub EditColorBlock(ByVal rBf As ResultBuffer)

        If rBf Is Nothing Then Exit Sub
        If rBf.AsArray(0).Value.ToString() = "" Then Exit Sub
        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference

        Try

            Dim StringHandle As String, ArrayHandle() As String
            Dim CorBlock As Object = Nothing, QtdBlock As Integer, I As Integer
            Dim Obj As Object, AcadAp As Object = GetObject(, My.Settings.Application_AutoCad) 'LibraryReference.ReturnAcadReference    'Autodesk.AutoCAD.Interop.AcadApplication

            If AcadAp Is Nothing Then Exit Sub
            StringHandle = AcadAp.ActiveDocument.GetVariable("USERS4")
            ArrayHandle = Split(StringHandle, "|")
            I = 0

            'Verifica se existe objetos no array
            QtdBlock = UBound(ArrayHandle)

            If QtdBlock > 0 Then

                For Each Obj In ArrayHandle
                    If I = 0 Then
                        'Get color of new block
                        CorBlock = ArrayHandle(0)
                        I = 1
                    Else

                        With AcadAp.ActiveDocument
                            Dim EntBlock As Object 'AcadBlockReference
                            EntBlock = .HandleToObject(Obj)
                            EntBlock.color = CorBlock
                        End With

                    End If
                Next

            End If

        Catch ex As Exception
            Select Case Err.Number
                Case 13

                Case Else
                    LibraryError.CreateErrorAenge(Err, "Error settings EditClBlc - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA007")

            End Select

        Finally
            LibraryReference = Nothing
        End Try

    End Sub

    'Função que recebe uma lista com os handles para alterar as cores do bloco
    <LispFunction("EditColorBlock_COM")> _
    Sub EditColorBlock_COM(ByVal rBf As ResultBuffer)

        'If rBf Is Nothing Then Exit Sub
        'If rBf.AsArray(0).Value.ToString() = "" Then Exit Sub
        'If LibraryReference Is Nothing Then LibraryReference = New LibraryReference

        'Try

        '    Dim StringHandle As String, ArrayHandle() As String
        '    Dim CorBlock As Object = Nothing, QtdBlock As Integer, I As Integer
        '    Dim Obj As Object, AcadAp As Autodesk.AutoCAD.Interop.AcadApplication = LibraryReference.ReturnAcadReference

        '    StringHandle = AcadAp.ActiveDocument.GetVariable("USERS4")
        '    ArrayHandle = Split(StringHandle, "|")
        '    I = 0

        '    'Verifica se existe objetos no array
        '    QtdBlock = UBound(ArrayHandle)

        '    If QtdBlock > 0 Then

        '        For Each Obj In ArrayHandle
        '            If I = 0 Then
        '                'Get color of new block
        '                CorBlock = ArrayHandle(0)
        '                I = 1
        '            Else

        '                With AcadAp.ActiveDocument
        '                    Dim EntBlock As AcadBlockReference
        '                    EntBlock = .HandleToObject(Obj)
        '                    EntBlock.color = CorBlock
        '                End With

        '            End If
        '        Next


        '    End If

        'Catch ex As Exception
        '    Select Case Err.Number
        '        Case 13

        '        Case Else
        '            LibraryError.CreateErrorAenge(Err, "Error settings EditClBlc - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA007")

        '    End Select

        'Finally
        '    LibraryReference = Nothing
        'End Try

    End Sub

    'Function for set variable CmdEcho in Cad 
    Private Sub SetCmdEcho(ByVal NewValue As Int16)
        Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", NewValue)
    End Sub

    'Carrega a caixa de linetype para função TUBULAÇAO ESGOTO
    <LispFunction("LType1")> _
     Function LType1(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        With Doc
            SetCmdEcho(0)
            .SendStringToExecute("'Linetype" & Chr(13), False, False, False)
            .SendStringToExecute("epdrw_tub" & Chr(13), False, False, False)
            SetCmdEcho(1)
        End With
        Doc = Nothing
        Return Nothing
    End Function

    'Carrega a caixa de linetype TUBULAÇÃO VENTILAÇÃO
    <LispFunction("LType2")> _
     Function LType2(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        With Doc
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 0)
            .SendStringToExecute("'Linetype" & Chr(13), False, False, False)
            .SendStringToExecute("(C:INDRW_TUB)" & Chr(13), False, False, False)
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 1)
        End With
        Doc = Nothing
        Return Nothing
    End Function

    'Carrega a caixa de linetype TUBULAÇÃO AGUA FRIA
    <LispFunction("LType3")> _
     Function LType3(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        With Doc
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 0)
            .SendStringToExecute("'Linetype" & Chr(13), False, False, False)
            .SendStringToExecute("(C:TVDRW_TUB)" & Chr(13), False, False, False)
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 1)
        End With
        Doc = Nothing
        Return Nothing
    End Function

    'Carrega a caixa de linetype TUBULAÇÃO AGUA QUENTE
    <LispFunction("LType4")> _
     Function LType4(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        With Doc
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 0)
            .SendStringToExecute("'Linetype" & Chr(13), False, False, False)
            .SendStringToExecute("(C:OUDRW_TUB)" & Chr(13), False, False, False)
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 1)
        End With
        Doc = Nothing
        Return Nothing
    End Function

    'Carrega a caixa de linetype TUBULAÇÃO INCENDIO
    <LispFunction("LType5")> _
     Function LType5(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        With Doc
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 0)
            .SendStringToExecute("'Linetype" & Chr(13), False, False, False)
            .SendStringToExecute("(C:INC_TUB)" & Chr(13), False, False, False)
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 1)
        End With
        Doc = Nothing
        Return Nothing
    End Function

    'Carrega a caixa de linetype TUBULAÇÃO GAS
    <LispFunction("LType6")> _
     Function LType6(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        With Doc
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 0)
            .SendStringToExecute("'Linetype" & Chr(13), False, False, False)
            .SendStringToExecute("(C:GAS_TUB)" & Chr(13), False, False, False)
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 1)
        End With
        Doc = Nothing
        Return Nothing
    End Function

    <LispFunction("Ccolor1")> _
    Function Ccolor1(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        With Doc
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 0)
            .SendStringToExecute("'DDCOLOR" & Chr(13), False, False, False)
            .SendStringToExecute("(Ahid_TUB)" & Chr(13), False, False, False)
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 1)
        End With
        Doc = Nothing
        Return Nothing
    End Function

    <LispFunction("XDrawing")> _
     Function XDrawing(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        With Doc
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 0)
            .SendStringToExecute("(Ahid_TUB)" & Chr(13), False, False, False)
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 1)
        End With
        Doc = Nothing
        Return Nothing
    End Function

    <LispFunction("ConfiTubEsp")> _
     Function ConfiTubEsp(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        With Doc
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 0)
            .SendStringToExecute("(C:CONFITUBESP4)" & Chr(13), False, False, False)
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 1)
        End With
        Doc = Nothing
        Return Nothing
    End Function

    <LispFunction("ConfiTubEsp2")> _
     Function ConfiTubEsp2(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        With Doc
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 0)
            .SendStringToExecute("'DDCOLOR" & Chr(13), False, False, False)
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 1)
        End With
        Doc = Nothing
        Return Nothing
    End Function

    <LispFunction("ConfiTubEsp3")> _
     Function ConfiTubEsp3(ByVal rbf As ResultBuffer) As ResultBuffer
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        With Doc
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 0)
            .SendStringToExecute("'Linetype" & Chr(13), False, False, False)
            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("CMDECHO", 1)
        End With
        Doc = Nothing
        Return Nothing
    End Function

    'function for copy a polyline in group - Pass the handle of poly
    <LispFunction("ReturnCopyPoly")> _
    Function ReturnCopyPoly(ByVal rBf As ResultBuffer) As Object

        If rBf Is Nothing Then Return Nothing
        If rBf.AsArray(0).Value.ToString() = "" Then Return Nothing
        If LibraryCad Is Nothing Then LibraryCad = New LibraryCad

        Try

            Dim UserS5 As String, Clone_Polyline As Object, Original_Polyline As Object, RbfResult As New ResultBuffer   'AcadPolyline
            Dim IniPoint(2) As Double, EndPoint(2) As Double, NewHandle As String
            Dim AcadLineType As Object
            Dim Doc As Document = Application.DocumentManager.MdiActiveDocument

            UserS5 = rBf.AsArray(0).Value.ToString()
            IniPoint(0) = 0 : IniPoint(1) = 0 : IniPoint(2) = 0
            EndPoint(0) = 0 : EndPoint(1) = 0 : EndPoint(2) = 0

            Original_Polyline = LibraryCad.ObjectFromHandle_Interop(UserS5)
            If Original_Polyline.ObjectName.ToString.ToLower <> "acdbpolyline".ToLower Then Return RbfResult
            Clone_Polyline = Original_Polyline.Copy

            With Clone_Polyline
                .Move(IniPoint, EndPoint)
                NewHandle = .Handle
                '.SetWidth 0, 0, 0
                .ConstantWidth = 0
                AcadLineType = .Linetype
                .Linetype = "Continuous"
                .Update()
            End With

            'Retorna como string o maior número encontrado acrescentado de 1 
            With RbfResult
                .Add(New TypedValue(LispDataType.Text, NewHandle))
            End With

            Return RbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao validar as informações de ReturnCPoly.", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA003")
            Return Nothing
        End Try

    End Function

    'Function - Get a handle of tub and return a len
    <LispFunction("ReturnLenTub")> _
    Function ReturnLenTub(ByVal Rbf As ResultBuffer) As Object

        If Rbf Is Nothing Then Return Nothing
        If Rbf.AsArray(0).Value.ToString() = "" Then Return Nothing
        If LibraryCad Is Nothing Then LibraryCad = New LibraryCad

        Dim RbfResult As New ResultBuffer

        Try

            Dim HandleTub As String, ObjEntity As Object, LenTub As String
            HandleTub = Rbf.AsArray(0).Value.ToString()
            LenTub = ""

            SetCmdEcho(0)
            ObjEntity = LibraryCad.ObjectFromHandle_Interop(HandleTub)

            'For errors only
            Select Case ObjEntity.ObjectName
                Case "AcDbPolyline"
                    LenTub = Replace(CStr(ObjEntity.Length), ",", ".")

            End Select

            'Retorna como string o maior número encontrado acrescentado de 1 
            With RbfResult
                .Add(New TypedValue(LispDataType.Text, LenTub))
            End With

            Return RbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao validar as informações de ReturnLTub.", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA004")
            Return Nothing
        Finally
            LibraryCad = Nothing
        End Try

    End Function

#End Region

#Region "----- Functions for path and my.computer.filesystem -----"

    'Return path of application for lisp - Set users2 in case of true in parameters 
    Public Function ReturnCurrentPath(ByVal Rbf As ResultBuffer) As Object

        Dim SetaUser As String = ""
        If Not Rbf Is Nothing Then Rbf.AsArray(0).Value.ToString()
        If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
        Dim Diretorio As String = LibraryReference.ReturnPathApplication

        If SetaUser.ToLower = "True".ToLower Then Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS2", Diretorio)
        Return Diretorio

    End Function

#End Region

#Region "----- Functions default and general use -----"

    <CommandMethod("ConfiguraRelatorio")> _
    Public Sub ConfigureReport()
        Dim Diretorio As String = GetAppInstall()
        System.Diagnostics.Process.Start(Diretorio & My.Settings.ReportViewer.ToString)
    End Sub

    <LispFunction("Finalize")> _
    Function Finalize_Validate(ByVal Rbf As ResultBuffer) As ResultBuffer

        Try
            Autodesk.AutoCAD.ApplicationServices.DocumentExtension.CloseAndDiscard(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument)
            Autodesk.AutoCAD.ApplicationServices.Application.Quit()
        Catch ex As Exception

        End Try

        Return Nothing

    End Function

    <LispFunction("Initialize")> _
    Function Initialize_Validate(ByVal Rbf As ResultBuffer) As ResultBuffer

        Dim LibCad As New LibraryCad
        With LibCad
            If .IsProjectValid() = False Then Return Nothing
        End With
        LibCad = Nothing

        Dim Result As String, RbfResult As New ResultBuffer, KeySoft As String = "", Resultado As String = "1201", Trech As String = "T1"
        Dim AengeLibrary As New Library_FrameworKAP.LibraryValidation

        Try

            KeySoft = Aenge_GetCfg("Configuration", "CodeApplication", GetAppInstall() & "Iniapp.ini")
            Result = AengeLibrary.KeyWindows(KeySoft)
            If Result.Trim.ToLower = "HHH211".Trim.ToLower Then
                Trech = "T2"
                'Application.SetSystemVariable("USERS1", "1202")
                Resultado = "1202"
            Else
                'Application.SetSystemVariable("USERS1", "1201")
                Resultado = "1201" : Trech = "T3"
            End If

            AengeLibrary = Nothing
            'Retorna como string o maior número encontrado acrescentado de 1 
            With RbfResult
                .Add(New TypedValue(LispDataType.Text, Resultado))
            End With

            Return RbfResult

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error Initialize_V - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA0025")
            Return Nothing
        End Try

    End Function

    'Abre a aplicação de autorização de códigos de barra
    <LispFunction("Aenge_CodAuto")> _
    Function Aenge_CodAuto(ByVal rBf As ResultBuffer) As ResultBuffer

        Try

            Dim AppPat As String
            AppPat = LibraryReference.ReturnPathApplication & "Aenge.CodeValidator.exe"
            Shell(AppPat, vbNormalFocus)
            Return Nothing

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error AengeCodAuto - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA001")
            Return Nothing
        End Try

    End Function

    'Save layerstate in dwg tactual
    <LispFunction("SaveLayerState")> _
    Function SaveLayerState(ByVal rBf As ResultBuffer) As ResultBuffer

        Try

            '' Get the current document
            Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument
            Dim acLyrStMan As LayerStateManager
            acLyrStMan = acDoc.Database.LayerStateManager

            If PathDir = "" Then
                If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
                PathDir = LibraryReference.ReturnPathApplication
            End If

            Dim sLyrStName As String = "ColorLinetype"
            'Save a state of layer 
            If acLyrStMan.HasLayerState(sLyrStName) = False Then
                acLyrStMan.SaveLayerState(sLyrStName, _
                                LayerStateMasks.Color + _
                                LayerStateMasks.LineType, _
                                ObjectId.Null)
            End If
            acLyrStMan.ExportLayerState(sLyrStName, PathDir & sLyrStName & ".las")

            Return Nothing

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error SaveLayerState - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA002")
            Return Nothing
        End Try

    End Function

    'Save layerstate in dwg tactual
    <LispFunction("RestoreLayerState")> _
    Function RestoreLayerState(ByVal rBf As ResultBuffer) As ResultBuffer

        Try
            '' Get the current document
            Dim acDoc As Document = Application.DocumentManager.MdiActiveDocument

            Dim acLyrStMan As LayerStateManager
            acLyrStMan = acDoc.Database.LayerStateManager

            If PathDir = "" Then
                If LibraryReference Is Nothing Then LibraryReference = New LibraryReference
                PathDir = LibraryReference.ReturnPathApplication
            End If

            Dim sLyrStFileName As String = "ColorLinetype"
            'sLyrStFileName = PathDir & sLyrStFileName '& ".las" '"c:\my documents\ColorLinetype.las"
            'If System.IO.File.Exists(sLyrStFileName) Then acLyrStMan.RestoreLayerState(sLyrStFileName, ObjectId.Null, 0, LayerStateMasks.Color)
            acLyrStMan.RestoreLayerState(sLyrStFileName, ObjectId.Null, 0, LayerStateMasks.Color)
            Return Nothing

        Catch ex As Exception
            LibraryError.CreateErrorAenge(ex, "Error SaveLayerState - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "LibraryVBA002")
            Return Nothing
        End Try

    End Function

    'Return a color selected by user
    <LispFunction("Color_SelectByUser")> _
    Function Color_SelectByUser(ByVal rBf As ResultBuffer) As Object

        Try

            Dim blnMetaColor As Boolean
            Dim lngCurClr As Long
            Dim lngInitClr As Long
            lngInitClr = 0

            If acedSetColorDialog(lngInitClr, blnMetaColor, lngCurClr) Then
                Return lngInitClr
            End If

            Return lngInitClr

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

End Class

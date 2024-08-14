Imports Autodesk.AutoCAD
Imports Autodesk.AutoCAD.ApplicationServices

Public Class frmInicial

#Region "----- Documentação -----"

#End Region

#Region "----- Atributos e Declarações -----"

#End Region

#Region "----- Eventos -----"

#Region "----- Label -----"

    Private Sub lblInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        HeightScreen()
    End Sub

#End Region

#Region "----- CommandButton -----"

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MsgBox("Deseja realmente fechar a aplicação ?", vbYesNo, "Finalizar") = vbNo Then Exit Sub
        'Carrega o menu novamente do AutoCad para qdo sair sempre abrir o menu original
        'LoadCad_Application()
        'Fecha a aplicação
        Autodesk.AutoCAD.ApplicationServices.Application.Quit()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click

        If rdoCad.Checked Then
            LoadCad_Application()
        ElseIf rdoClose.Checked Then
            'Carrega o menu novamente do AutoCad para qdo sair sempre abrir o menu original
            Autodesk.AutoCAD.ApplicationServices.DocumentExtension.CloseAndDiscard(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument)
            Autodesk.AutoCAD.ApplicationServices.Application.Quit()
        ElseIf rdoNew.Checked Then
            LoadNew_Draw()
            'Desenho existente 
        Else
            LoadExist_Draw()
        End If

    End Sub

    Private Sub btnHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHelp.Click
        CallHelp()
    End Sub

#End Region

#Region "----- Form -----"

    Private Sub frmInicial_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frm_Inicial = Nothing
    End Sub

    Private Sub frmInicial_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim LibraryComponet As New Aenge.Library.Component.LibraryComponent
        LibraryComponet.ConfigFont_Component(frm_Inicial)
        LibraryComponet = Nothing
        GetInfoVersion()

        Dim MenuBar As String = ""
        MenuBar = Aenge_GetCfg("Configuration", "Menubar", GetAppInstall() & My.Settings.File_Autoenge)
        If Not IsNumeric(MenuBar) Then MenuBar = "1"
        Application.SetSystemVariable("MENUBAR", Int16.Parse(MenuBar))

    End Sub

#End Region

#End Region

#Region "----- Funcionalidades relacionadas a chamadas e consultas -----"

    'Obtem as informações relacionadas ao Cad e versão do Autopower ou AutoHidro 
    Function GetInfoVersion() As Boolean

        Dim LibraryReference As New LibraryReference
        'With txtInfo
        '    .Text = "Aplicação : " & My.Settings.Aplication_AutoCadName.ToString & Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("ACADVER").ToString & vbCrLf
        '    .Text += "Version : " & DirectCast(Autodesk.AutoCAD.ApplicationServices.Application.Version, System.Version).Major.ToString & "." & DirectCast(Autodesk.AutoCAD.ApplicationServices.Application.Version, System.Version).Minor & vbCrLf
        '    .Text += "Diretório : " & LibraryReference.ReturnPathApplication.ToString
        'End With
        LibraryReference = Nothing
        Return True

    End Function

    'Carrega somente o Cad com suas informações originais e menu carregado, sem o do Autopower
    Function LoadCad_Application() As Boolean

        Dim Comando As String, MenuCad As String
        If AppCad Is Nothing Then Load_AcadApp()

        MenuCad = Aenge_GetCfg("GENERAL", "MENUCAD", GetAppInstall() & AutoengeCfgFile)
        If MenuCad = "" Then
            Aenge_SetCfg("GENERAL", "MENUCAD", "ACAD.cuix", GetAppInstall() & AutoengeCfgFile)
            MenuCad = "ACAD.cuix"
        End If

        Comando = "(COMMAND " & Chr(34) & "MENU" & Chr(34) & " " & Chr(34) & MenuCad & Chr(34) & ")" & vbCrLf
        Dim Doc As Document = Application.DocumentManager.MdiActiveDocument
        Doc.SendStringToExecute(Comando, True, False, False)
        giReturnDoModal_Class = 99   'Neste caso nao chama os formularios de inicializacao pois ja foi tratado 
        Me.Dispose(True)

        Return True

    End Function

    'Carrega a tela de cadastro de um novo desenho 
    Function LoadNew_Draw() As Object

        Me.Hide()
        If frm_NovoDesenho Is Nothing Then frm_NovoDesenho = New frmNovoDesenho
        frm_NovoDesenho.ShowDialog()

        Return True

    End Function

    Function LoadExist_Draw() As Object

        Me.Hide()
        If frm_ExisteDesenho Is Nothing Then frm_ExisteDesenho = New frmExisteDesenho
        frm_ExisteDesenho.Show()

        Return True
    End Function

#End Region

#Region "----- Funcionalidades gerais da aplicação -----"

    'Ajusta o tamanho da tela de acordo com o selecionado pelo usuário 
    Function HeightScreen() As Object

        'If lblInfo.Text.Trim = "(+) Informações sobre a aplicação".Trim Then
        '    Me.Height = 236
        '    txtInfo.Visible = True : txtInfo.Refresh()
        '    lblInfo.Text = "(-) Informações sobre a aplicação " : lblInfo.Refresh()
        'Else
        '    Me.Height = 184
        '    txtInfo.Visible = False : txtInfo.Refresh()
        '    lblInfo.Text = "(+) Informações sobre a aplicação " : lblInfo.Refresh()
        'End If

        Return True

    End Function

#End Region

End Class
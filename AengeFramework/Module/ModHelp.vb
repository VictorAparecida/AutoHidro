Module ModHelp

    'Analista responsável : Raul Antonio Fernandes Junior
    'Data de criação : 18/05/2006
    'Function call provided...
    'HelpMe(ProgWindow,HelpFile,[ContextID])    :  Displays either help index or context sensitive help.

    'Two parameters required, one optional...
    'ProgWindow                                 :  Your Program's Window Handle.
    'HelpFile                                   :  Filespec of the .CHM file to display.
    '[ContextID]                                :  If passed will open context help at that topic.

    Const HH_DISPLAY_TOPIC = &H0
    Const HH_SET_WIN_TYPE = &H4
    Const HH_GET_WIN_TYPE = &H5
    Const HH_GET_WIN_HANDLE = &H6
    Const HH_DISPLAY_TEXT_POPUP = &HE   ' Display string resource ID or
    ' text in a pop-up window.
    Const HH_HELP_CONTEXT = &HF         ' Display mapped numeric value in
    ' dwData.
    Const HH_TP_HELP_CONTEXTMENU = &H10 ' Text pop-up help, similar to
    ' WinHelp's HELP_CONTEXTMENU.
    Const HH_TP_HELP_WM_HELP = &H11     ' text pop-up help, similar to
    ' WinHelp's HELP_WM_HELP.

    Private Declare Function HtmlHelp Lib "hhctrl.ocx" Alias "HtmlHelpA" (ByVal hwndCaller As Long, _
            ByVal pszFile As String, ByVal uCommand As Long, ByVal dwData As Long) As Long

    'When ContextID is passed, routine will open a window containing the specified subject. Otherwise it
    'will open a window containing the index of the specified help file. On both counts it returns the
    'handle of the window created.
    Public Function HelpMe(ByVal ProgWindow As Long, ByVal HelpFile As String, Optional ByVal ContextID As Object = Nothing) As Long
        On Error GoTo Erro
        If ContextID Is Nothing Then                               'Display Root.
            HelpMe = HtmlHelp(ProgWindow, HelpFile, HH_DISPLAY_TOPIC, 0)
        Else                                                                'Context sensitive help.
            HelpMe = HtmlHelp(ProgWindow, HelpFile, HH_HELP_CONTEXT, ContextID)
        End If
        Exit Function
Erro:
        MsgBox("Ocorrência de erro : " & Err.Number & " - " & Err.Description, vbInformation, "Erro")
    End Function

#Region "----- Funcionalidades de segurança e validações -----"

    Function CallHelp() As Object
        Dim ArqHelp As String
        ArqHelp = GetAppInstall() & "apwr.chm"
        If My.Computer.FileSystem.FileExists(ArqHelp) = True Then System.Windows.Forms.Help.ShowHelp(Nothing, ArqHelp)
        Return Nothing
    End Function

#End Region

End Module

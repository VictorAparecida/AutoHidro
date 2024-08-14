Public Class LibraryError

    '===============================================================================================================================
    'Modulo :  LibraryLog
    'Empresa :   Autoenge Brasil Ltda
    'Data de CriaÃ§Ã£o : 12/03/2009
    'Analista ResponsÃ¡vel : Raul Antonio Fernandes Junior
    'DescriÃ§Ã£o : Classe responsÃ¡vel pelo tratamento de erros e de logs de aplicaÃ§Ãµes. Recebe informaÃ§Ãµes referentes Ã  informaÃ§Ã£o gerada pela aplicaÃ§Ã£o
    'e retorna se necessÃ¡rio emula uma tela com as informaÃ§Ãµes a serem geradas.
    '
    'Id de tratamento de Erros :
    'LIBENTI001 - Erro genÃ©rico da aplicaÃ§Ã£o responsÃ¡vel pela chamada. Obter o Id do erro para tratamento.
    'LIBENTI002 -
    'LIBENTI003 -
    'LIBENTI004 -
    'LIBENTI005 -
    '
    'ModificaÃ§Ãµes
    '
    '===============================================================================================================================

    'FunÃ§Ã£o que trata a ocorrÃªncia de erro de uma determinada aplicaÃ§Ã£o
    Function CreateErrorAenge(ByVal erro As Object, ByVal ErrorDescription As String, Optional ByVal EndApplication As Boolean = False, _
                              Optional ByVal NameApplication As String = "Aenge aplication", Optional ByVal NameOwner As String = "Autoenge Brasil Ltda", _
                              Optional ByVal NameClassRequired As String = "LibraryLog", Optional ByVal IdError As String = "LIBENTI001", _
                              Optional ByVal CreateLogError As Boolean = False, Optional ByVal PathLogError As String = "", _
                              Optional ByVal EmailRequired As String = "suportetecnico@autoenge.com.br") As Object

        If TypeOf erro Is Microsoft.VisualBasic.ErrObject Then
            MsgBox("Ocorrência de erro na aplicação - ErrDesc : " & CType(erro, Microsoft.VisualBasic.ErrObject).Description, MsgBoxStyle.Information, "Erro - Aenge ")
        Else
            MsgBox("Ocorrência de erro na aplicação - ErrDesc : " & erro.Message, MsgBoxStyle.Information, "Erro - Aenge ")
        End If

        'Verifica se o erro fez requisiÃ§Ã£o para visualizaÃ§Ã£o de formulÃ¡rio - Envio de informaÃ§Ãµes
        If CreateLogError = True Then
            'If frm_Error Is Nothing Then frm_Error = New frmError

            'With frmError
            '    .txtEmpresa.Text = NameOwner
            '    .txtAplicacao.Text = NameApplication
            '    .TxtDescricao.Text = ErrorDescription
            '    .txtEmail.Text = EmailRequired
            '    .txtIdChamada.Text = IdError
            '    .txtMotivo.Text = "Tratamento de Erro - AplicaÃ§Ã£o " & NameApplication
            '    .Show(1)
            'End With

        End If

        Return "Tratamento de erros efetuado com sucesso !"
    End Function


End Class

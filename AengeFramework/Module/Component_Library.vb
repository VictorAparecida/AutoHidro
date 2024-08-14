Imports System.Windows.Forms
Imports System.Drawing
Imports System.Data.OleDb
Imports System.Runtime.InteropServices
Imports Autodesk.AutoCAD.ApplicationServices

Module Component_Library

#Region "----- Atributos e Declarações -----"

    Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpSectionName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Private Declare Function LoadString Lib "user32" Alias "LoadStringA" (ByVal hInstance As Long, ByVal wID As Long, ByVal lpBuffer As String, ByVal nBufferMax As Long) As Long

    'Declarações para desabilitar o botão fechar do formulário
    Declare Function GetSystemMenu Lib "User" (ByVal hWnd%, ByVal bRevert%) As Integer
    Declare Function ModifyMenu Lib "User" (ByVal hMenu%, ByVal nPosition%, ByVal wFlags%, ByVal wIDNewItem%, ByVal lpNewItem As VariantType) As Integer

    Public giReturnDoModal_Class As Integer 'Retorna o botão pressionado no form que encerra o sistema
    'Valores:
    'giReturnDoModal_Class = -1   Algum problema aconteceu
    'giReturnDoModal_Class =  0   Cancel
    'giReturnDoModal_Class =  1   Ok
    'giReturnDoModal_Class =  2   Desenhar
    'giReturnDoModal_Class =  3   Abrir
    'giReturnDoModal_Class =  4   Salvar
    'giReturnDoModal_Class =  5   Help
    'giReturnDoModal_Class =  6   Sistema de Arquivo
    'giReturnDoModal_Class =  7   CloseButton (Processo finalizado pelo botão fechar da window - lado direito superior)
    'giReturnDoModal_Class =  8   Selected
    'giReturnDoModal_Class =  9   Banco de Dados Dimensionamento vazio
    'giReturnDoModal_Class =  10  Apagar
    'giReturnDoModal_Class =  11  Inserir
    'giReturnDoModal_Class =  12  Mover
    'giReturnDoModal_Class =  13  Rotacionar
    'giReturnDoModal_Class = 20   Botao ok foi selecionado para carregar um determinado desenho

    Public ArrayInfo_Project As New ArrayList, IsSaveImgThumbnail As Boolean = True
    'Este arraylist armazena de acordo com a ordem a seguir os dados sobre um desenho ou projeto carregado que estamos trabalhando
    '0 --> Nome do projeto
    '1 --> Nome do desenho
    '2 --> Caminho completo do desenho em questao 

    'Conexões de banco de dados
    Public ConnAhid As OleDbConnection, ConnAenge As OleDbConnection

    'Gerais da aplicação e de validações 
    Public Id_Tactual As Integer
    Public AutoengeCfgFile As String = "Autoenge.cfg"
    Public AppCad As Autodesk.AutoCAD.ApplicationServices.Application

    Private mVar_NameClass As String = "Application_Library", _PrjNome As String = "PrjNome", _PrjDwgNome As String = "PrjDwgNome"

#End Region

#Region "----- Funcionalidades de Path e diretorios -----"

    'Obtem o path de instalação da aplicação
    Function GetAppInstall() As String
        If Right(My.Application.Info.DirectoryPath, 1) = "\" Then
            GetAppInstall = My.Application.Info.DirectoryPath
        Else
            GetAppInstall = My.Application.Info.DirectoryPath & "\"
        End If
    End Function

#End Region

#Region "----- Funcionalidades relacionadas a componentes -----"

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

#Region "----- Funcionalidades gerais da aplicação -----"

    'Funcao que gerencia a abertura e fechamento de documentos no autocad
    Function OpenClose_AcadDocument(ByVal DwgOpen As String) As Object

        'Try

        '    Dim docs As DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager

        '    For Each doc As Document In docs
        '        ' First cancel any running command
        '        If doc.CommandInProgress <> "" AndAlso doc.CommandInProgress <> "CD" Then
        '            Dim oDoc As AcadDocument = DirectCast(doc.AcadDocument, AcadDocument)
        '            oDoc.SendCommand(ChrW(3) & ChrW(3))
        '        End If

        '        If doc.IsReadOnly Then
        '            doc.CloseAndDiscard()
        '        Else
        '            ' Activate the document, so we can check DBMOD
        '            If docs.MdiActiveDocument <> doc Then
        '                docs.MdiActiveDocument = doc
        '            End If
        '            Dim isModified As Integer = System.Convert.ToInt32(Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("DBMOD"))

        '            ' No need to save if not modified
        '            If isModified = 0 Then
        '                doc.CloseAndDiscard()
        '            Else
        '                ' This may create documents in strange places
        '                doc.CloseAndSave(doc.Name)
        '            End If
        '        End If
        '    Next
        '    'Abre o novo desenho
        '    Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.Open(DwgOpen, False)

        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'End Try

        Return Nothing
    End Function

    'Carrega a variavel com a instancia atual do AutoCad que esta sendo utilizada 
    Function Load_AcadApp() As Object

        Try

            Return Nothing
            'Neste caso obtem as referências para Cad 2013
            AppCad = Autodesk.AutoCAD.ApplicationServices.Application.AcadApplication         'GetObject("AutoCAD.Application.19")
            Return AppCad

        Catch ex As Exception
            Dim AengeError As New LibraryError
            AengeError.CreateErrorAenge(Err, "Ocorrência de erro ao executar as validações iniciais da aplicação - Referência de Cad não encontrada !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "Application_Library002")
            AengeError = Nothing
            Return Nothing
        End Try

    End Function

    'Faz a validacao do banco de dados se estao abertos ou nao
    Public Function ValidateConnection(Optional ByVal WConn As String = "all") As Object

        Try

            'Abre a conexão com o banco de dados
            Dim Aenge_Connection As New Aenge.Library.Db.LibraryConnection

            If ConnAhid Is Nothing Then
                If WConn = "all" Or WConn = "AHID" Then
                    'AHID
                    Aenge_Connection.TypeDb = "AHID_"
                    ConnAhid = Aenge_Connection.Aenge_OpenConnectionDB
                End If
            Else
                If ConnAhid.State <> ConnectionState.Open Then ConnAhid.Open()
            End If

            'Verificamos se eh 64 bits e se existe office 2013 instalado, pois o office retira o provider para uso na dll
            If Not TypeOf (ConnAhid) Is OleDbConnection Then
                If Aenge_GetCfg("Configuration", "WinVersion", GetAppInstall() & "Iniapp.ini").ToString = "64" Then
                    MsgBox("O software encontrou uma inconsistência com sua versão do Windows e Office, será necessário reconfigurar alguns arquivos." & _
                    " Reinicie o software para que os arquivos sejam restaurados. Caso persista esta mensagem após a reinstalação, por favor entre em contato" & _
                    " com nosso suporte técnico !", MsgBoxStyle.Information, "Software desconfigurado")
                    'If My.Computer.FileSystem.FileExists(GetAppInstall() & My.Settings.OfficePack2010_64.ToString) Then System.Diagnostics.Process.Start(GetAppInstall() & My.Settings.OfficePack2010_64.ToString)
                    Aenge_SetCfg("Configuration", "InstallProvider", "1", GetAppInstall() & "Iniapp.ini")
                    'Finaliza o autocad
                    Autodesk.AutoCAD.ApplicationServices.Application.Quit()
                    Return Nothing
                End If
            End If

            If ConnAenge Is Nothing Then
                If WConn = "all" Or WConn = "aenge" Then
                    'Aenge
                    Aenge_Connection.TypeDb = "AENGE"
                    ConnAenge = Aenge_Connection.Aenge_OpenConnectionDB
                End If
            Else
                If ConnAenge.State <> ConnectionState.Open Then ConnAenge.Open()
            End If

            Return True

        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    'Chamada principal da aplicação - controle 
    Sub Main(ByVal typeO As String)

        Try
            'Ja foi tratada a abertura dos dados 
            If giReturnDoModal_Class = 99 Then Exit Sub

            ValidateConnection()
            If ConnAhid Is Nothing Then
                Autodesk.AutoCAD.ApplicationServices.Application.Quit()
                Exit Sub
            End If

            Select Case typeO
                Case "inicial"
                    giReturnDoModal_Class = 98   'Neste caso consideramos o primeiro ini, entao caso o usuario cancela temos que voltar para a tela inicial 
                    If frm_Inicial Is Nothing Then frm_Inicial = New frmInicial
                    frm_Inicial.Show()
                    frm_Inicial = Nothing

                Case "open"
                    'Validate type open form 
                    Dim StyleMenu, IniMenu, Menubar As Object
                    Dim PathDir As String = GetAppInstall()

                    StyleMenu = Aenge_GetCfg("Configuration", "StyleMenu", PathDir & My.Settings.File_Autoenge)
                    IniMenu = Aenge_GetCfg("Configuration", "IniMenu", PathDir & My.Settings.File_Autoenge)
                    Menubar = Aenge_GetCfg("Configuration", "Menubar", PathDir & My.Settings.File_Autoenge)
                    'Save tactual dwg for open another drawing 
                    'If MsgBox("Deseja salvar os dados do desenho atual ?", MsgBoxStyle.YesNo, "Salvar") = MsgBoxResult.Yes Then
                    '    If Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("USERS1") <> "" Then Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute("SAVE ", True, False, False)
                    'End If

                    Select Case StyleMenu
                        Case "1"
                            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute("Projetos" & Chr(13), True, False, False)

                        Case Else
                            If frm_ExisteDesenho Is Nothing Then frm_ExisteDesenho = New frmExisteDesenho
                            'frm_ExisteDesenho.TipoForm = "open"
                            frm_ExisteDesenho.Show()

                    End Select

                Case Else
                    If frm_NovoDesenho Is Nothing Then frm_NovoDesenho = New frmNovoDesenho
                    frm_NovoDesenho.TipoForm = "new"
                    frm_NovoDesenho.Show()

            End Select

        Catch ex As Exception
            Dim AengeError As New LibraryError
            AengeError.CreateErrorAenge(Err, "Ocorrência de erro ao executar as validações iniciais da aplicação !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "Application_Library001")
            AengeError = Nothing
        Finally
            'Close all connections 
            If Not ConnAenge Is Nothing Then If ConnAenge.State = ConnectionState.Open Then ConnAenge.Close()
            If Not ConnAhid Is Nothing Then If ConnAhid.State = ConnectionState.Open Then ConnAhid.Close()
        End Try

    End Sub

#End Region

#Region "----- Funcionalidades de consulta e povoamento de componentes -----"

    'Função publica que faz a busca no banco de dados o código do projeto atual
    'os dados referentes ao mesmo.
    'Criado em 17/12/2003 por Raul Antonio Fernandes Junior
    'Alterado em 17/12/2003 por Raul Antonio Fernandes Junior
    Function IDProject(ByVal NPrj As String) As Integer

        Dim RsProjects As OleDbDataAdapter, DtTemp As New DataTable, IdTemp As Integer = 0
        ValidateConnection("aenge")

        Try

            RsProjects = New OleDbDataAdapter("SELECT * FROM AENGEDEF WHERE PRJNOME = '" & NPrj & "'", ConnAenge)
            RsProjects.Fill(DtTemp)
            If DtTemp.Rows.Count > 0 Then IdTemp = DtTemp.Rows(0)("Id")

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao consultar ID do projeto.", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "Application_Library004")
        End Try

        Return IdTemp

    End Function

    'Função que seta o código ID do projeto selecionado pelo usuário no banco de dados
    Public Function SetIdTactual(ByVal NameProject As String) As Object

        Try

            Dim Cmd2 As New OleDbCommand, RefAfect As Integer

            'Obtem o Id_Tactual do projeto a ser tratado
            Id_Tactual = IDProject(NameProject)

            'Valida se a conexao esta aberta ou nao com o banco de dados 
            ValidateConnection("AHID")
            If ConnAhid.State <> ConnectionState.Open Then ConnAhid.Open()
            With Cmd2
                .Connection = ConnAhid
                .CommandText = "UPDATE TACTUALPROJECT SET ID = " & Id_Tactual
                .CommandType = CommandType.Text
                RefAfect = .ExecuteNonQuery
            End With

            Cmd2 = Nothing

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao gravar as informações do projeto e idtactual !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "")
            LibraryError = Nothing
            Return False
        End Try

        Return True
    End Function

    'Treeview para descrições e nomes de dispositivos de proteção
    Function FillTreeView_Projeto(ByVal Trv As TreeView, ByVal sSql As String, Optional ByVal ClearTView As Boolean = True, Optional ByVal Dtbl As System.Data.DataTable = Nothing, Optional ByVal TypeData As String = "", Optional ByVal ArrayColumns() As String = Nothing, _
    Optional ByVal FormatoDt As String = "dd/mm/yyyy", Optional ByVal LstImg As System.Windows.Forms.ImageList = Nothing) As Boolean

        If ClearTView = True Then
            Trv.ResetText()
            Trv.Nodes.Clear()
        End If

        Dim Resultado As String = "Não informado", AtualizouNodeProtocolo As Boolean = False, TempDt_Nasc As String = ""
        Dim LimpaDrR As Boolean = False, Texto As String = "", Cor As New Color, Column0 As String = "", Column1 As String = ""

        Try

            If ConnAenge Is Nothing Then
                Dim LibConn As New Aenge.Library.Db.LibraryConnection
                With LibConn
                    .TypeDb = "AENGE_"
                    ConnAenge = .Aenge_OpenConnectionDB
                End With
                LibConn = Nothing
            End If

            'Povoa com os dados do banco de dados de projetos 
            If Dtbl Is Nothing Then
                Dim Da As OleDbDataAdapter = New OleDbDataAdapter("SELECT * FROM AengeDes ORDER BY PrjNome ASC, PrjDwgNome ASC", ConnAenge)
                Dtbl = New DataTable
                Da.Fill(Dtbl)
            End If

            'Cria um datareader para manipular as informações mais facilmente
            Dim DrR As DataTableReader
            DrR = Dtbl.CreateDataReader

            'Declarações para o Treeview
            Dim Node0 As TreeNode = Nothing, i As Integer = 0
            Dim Node1 As TreeNode = Nothing, Node2 As TreeNode = Nothing, Column2 As String = ""

            Do While DrR.Read

                'Povoa o list com as informações agrupadas
                With Trv

                    .ShowNodeToolTips = True

                    If Column0 <> DrR(_PrjNome).ToString Then

                        'Monta a treeview com a sequencia --> Column0, Column1 e Column2
                        'Nó principal - Column0 - Primeira Coluna 
                        Node0 = New TreeNode
                        Node0.Text = DrR(_PrjNome).ToString
                        'Armazena somente id para a tabela de notas fiscais
                        Node0.Tag = "NODE0|PRJ"
                        .ShowNodeToolTips = True
                        Node0.ImageIndex = 3
                        Node0.SelectedImageIndex = 3
                        .Nodes.Add(Node0)
                        Column0 = DrR(_PrjNome).ToString
                        i += 1

                        'Informações referentes ao nó com o nome do cliente 
                        'NodeProtocolo.ImageIndex = 15
                        Node1 = New TreeNode

                        With Node1
                            'Será utilizado para consultas para não precisar reconsultar no banco de dados 
                            .Tag = "NODE1|" & DrR(_PrjDwgNome).ToString
                            .ToolTipText = "Desenho : " & DrR(_PrjDwgNome).ToString & Chr(13)
                            .Text = DrR(_PrjDwgNome).ToString
                            .ImageIndex = 1
                            .SelectedImageIndex = 1
                        End With

                        Column1 = DrR(_PrjDwgNome).ToString
                        'Neste caso adicionou o node0, node1
                        Node0.Nodes.Add(Node1)

                    Else

                        If Column1 <> DrR(_PrjNome).ToString Then
                            Node1 = New TreeNode

                            With Node1
                                'Será utilizado para consultas para não precisar reconsultar no banco de dados 
                                .Tag = "NODE1|" & DrR(_PrjDwgNome).ToString
                                .ToolTipText = "Desenho : " & DrR(_PrjDwgNome).ToString & Chr(13)
                                .Text = DrR(_PrjDwgNome).ToString
                                .ImageIndex = 1
                                .SelectedImageIndex = 1
                            End With
                            Column1 = DrR(_PrjDwgNome).ToString
                            Node0.Nodes.Add(Node1)

                            AtualizouNodeProtocolo = True

                        End If

                    End If

                End With

            Loop

            If LimpaDrR = True Then DrR.Close()
            'Trv.ExpandAll()
            Return True

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao povoar as informações do componente Treeview - Erro de validação e consulta de projetos cadastrados !", , _
                                          mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "Application_Library003")
            Return False
        Finally

        End Try

    End Function

#End Region

#Region "----- Funcionalidades de segurança e validações -----"

    'Função que valida as informações carregadas - Segurança do sistema e suas validações de chave 
    Function Initialize() As Boolean

       Try

            Dim Result As String
            Dim AengeLibrary As New Library_FrameworKAP.LibraryValidation
            Result = AengeLibrary.KeyWindows(My.Settings.CodeApplication.ToString)

            If Result <> "HHH211" Then
                MsgBox("Arquivos não carregados corretamente ! A aplicação será finalizada !", vbCritical, "Aplicação não configurada")
                Autodesk.AutoCAD.ApplicationServices.DocumentExtension.CloseAndDiscard(Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument)
                Autodesk.AutoCAD.ApplicationServices.Application.Quit()
                Return False
            End If

            AengeLibrary = Nothing
            Return True

        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "----- Funcionalidades relacionadas a abertura e inicializacao de projetos e desenhos -----"

    'Valida a abertura do desenho selecionado pelo usuário, assim como parâmetros e outras funcionalidades 
    Function frmExisteDesenho_ValidateOpen_Dwg(ByVal PathDwg As String, ByVal NameProject As String, ByVal NameDwg As String, Optional ByVal ValidateInitialize As Boolean = False) As Object

        Try

            If Not My.Computer.FileSystem.FileExists(PathDwg) Then
                MsgBox("O desenho selecionado não está disponível para visualização e acesso. Verifique se o desenho foi excluido ou removido da pasta de origem." & _
                       " Em caso de dúvidas, entre em contato com nosso suporte técnico!", MsgBoxStyle.Information, "Desenho não encontrado")
                Return False
            End If

            'Valida a abertura e parametros selecionados 
            frmExisteDesenho_OpenDWG(PathDwg, NameProject, nameDwg)
            frmExisteDesenho_SetArquivoCFGDados(NameProject, NameDwg)
            System.Windows.Forms.Application.DoEvents()
            SetIdTactual(NameProject)
            If ValidateInitialize Then Initialize()

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao validar a abertura do desenho selecionado !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmExisteDesenho003")
            LibraryError = Nothing
            Return False
        End Try

        Return True

    End Function

    'Abre um determinado DWG para sua manipulação dentro do ambiente do Intellicad
    Function frmExisteDesenho_OpenDWG(ByVal DrawApp As String, ByVal NameProject As String, ByVal NameDwg As String) As Boolean

        Try

            Dim PathF As String, DocTactual As Document, NameDocTactual As String = "", PathDiretorio As String = GetAppInstall()
            PathF = Replace(GetAppInstall, "\", "\\")
            DrawApp = Replace(DrawApp, "\\", "\")

            Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("SDI", 0)
            DocTactual = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument
            NameDocTactual = DocTactual.Name
            'Nova maneira de fechar - Alteração Raul
            Autodesk.AutoCAD.ApplicationServices.DocumentExtension.CloseAndSave(DocTactual, NameDocTactual)
            'Anteriormente era o modo como fechava 
            'DocTactual.CloseAndSave(NameDocTactual)

            System.Windows.Forms.Application.DoEvents()

            Dim AcDocMgr As DocumentCollection = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager
            Dim This As Document = DocumentCollectionExtension.Open(AcDocMgr, DrawApp, False)
            'DocTactual = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.Open(DrawApp, False)
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument = DocTactual
            System.Windows.Forms.Application.DoEvents()

            Using DocTactual.LockDocument

                'Atualiza as Userr1,2 e 3
                Dim PathCfgDraw As String, UnidadeCfg As String, EscalaCfg As String, ValorUnidade As Single
                PathCfgDraw = PathDiretorio & NameProject & "\" & NameDwg & ".cfg"    'GetAppInstall() & Combo1.Text & "\" & List1.Text & ".cfg" 
                UnidadeCfg = Aenge_GetCfg("DRAWING", "UNIDADE_MEDIDA", PathCfgDraw)
                EscalaCfg = Aenge_GetCfg("DRAWING", "ESCALA", PathCfgDraw)

                Select Case LCase(UnidadeCfg)
                    Case "m"
                        ValorUnidade = 0.001

                    Case "cm"
                        ValorUnidade = 0.1

                    Case "mm"
                        ValorUnidade = 1

                    Case Else
                        ValorUnidade = 0

                End Select

                'Atualiza as references para o cad
                '.Preferences.System.SingleDocumentMode = False
                'Seta as variaveis locais para a aplicação
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS1", NameProject)
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS2", PathDiretorio)
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS3", "1000")
                If IsNumeric(EscalaCfg) Then Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERR3", CDbl(EscalaCfg))
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERR2", CDbl(ValorUnidade))
                System.Windows.Forms.Application.DoEvents()

                'Verifica qual o tipo de menu a ser carregado
                Dim EstiloMenu As String
                EstiloMenu = Aenge_GetCfg("GENERAL", "MENU", PathDiretorio & "autoenge.cfg")
                If EstiloMenu = "" Then EstiloMenu = "0"
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("USERS5", EstiloMenu)

                Dim PathLisp As String
                PathLisp = "(LOAD " & Chr(34) & Replace(PathDiretorio, "\", "\\") & "AHID.vlx" & Chr(34) & ")"
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute(PathLisp & Chr(13), True, False, False)
                Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.SendStringToExecute("AhidInicial" & Chr(13), True, False, False)
                System.Windows.Forms.Application.DoEvents()
                Autodesk.AutoCAD.ApplicationServices.Application.SetSystemVariable("SDI", 1)

            End Using

            Return True

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao validar parâmetros e funções de abertura do desenho selecionado !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmExisteDesenho004")
            LibraryError = Nothing
            Return False
        End Try

    End Function

    'Passa os novos parametros para os arquivos CFG dos desenhos e dos projetos
    Private Sub frmExisteDesenho_SetArquivoCFGDados(ByVal NameProject As String, ByVal NameDwg As String)

        Dim ApCaminho As String, IDP As String, PathDiretorio As String = GetAppInstall()
        Dim bRet As Boolean

        Try

            ApCaminho = PathDiretorio
            IDP = CStr(IDProject(NameProject))
            bRet = Aenge_SetCfg("AppData", "AENGEPROJ", NameProject, ApCaminho & "Autoenge.cfg")
            bRet = Aenge_SetCfg("AppData", "AENGEDWG", NameDwg, ApCaminho & "Autoenge.cfg")
            bRet = Aenge_SetCfg("AppData", "AENGEPROJID", IDP, ApCaminho & "Autoenge.cfg")

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao salvar as configurações do desenho que esta sendo carregado !", , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmExisteDesenho005")
            LibraryError = Nothing
        End Try

    End Sub

#End Region

End Module

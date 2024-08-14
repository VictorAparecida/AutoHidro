Imports System.Windows.Forms
Imports System.Drawing

Public Class frmDiagnostic

#Region "----- Documentação ----"

    '============================================================================================================
    'Módulo : frmCircuitoInfo
    'Empresa : Autoenge Brasil Ltda
    'Data da criação : Quarta-Feira, 13 de março de 2013
    'Analista responsável : Raul Antonio Fernandes Junior
    'Descrição : Informações gerais sobre os diagnosticos gerais da aplicação do autohidro. Teremos que ver de acordo com cada um do requisitante para os tipos selecionados 
    '                1) Circuitos 
    '                2) Interruptores
    '                3) Luminarias
    '                4) Tubulaçao
    '                5) Logica incorreta
    '                6) Diversos
    '   Neste caso, o lisp irá passar para o .net as informações formatadas da seguinte maneira: Código do objeto - Handle do objeto relacionado (caso exista) - Mensagem geral sobre a ocorrência do erro ou inconsistencia 
    '
    'Id de tratamento de Erros : 
    'frmDiagnostic001 - FillTreeView_DetEsgoto
    'frmDiagnostic002 - 
    'frmDiagnostic003 - 
    'frmDiagnostic004 - 
    'frmDiagnostic005 - 
    'frmDiagnostic006 - 
    'frmDiagnostic007 - 
    'frmDiagnostic008 - 
    'frmDiagnostic009 - 
    'frmDiagnostic0010 - 
    '
    'Modificações
    '
    '============================================================================================================

#End Region

#Region "----- Atributos e Declaracoes -----"

    Public mVar_InfoText As String, mVar_TypeFunction As String, mVar_DtDiagnostic As New System.Data.DataTable, mVar_NameClass As String = "frmDiagnostic"

    Private Shared ArrayDescObject As New ArrayList, IndexCorrect As Int16 = -1

    Dim LibraryError As New LibraryError

#End Region

#Region "----- Constructor -----"

    Protected Overrides Sub Finalize()
        LibraryError = Nothing
        MyBase.Finalize()
    End Sub

#End Region

#Region "----- Get e Set -----"

    'Get which form is called 
    Property TypeFunction() As String
        Get
            TypeFunction = mVar_TypeFunction
        End Get
        Set(ByVal value As String)
            mVar_TypeFunction = value
        End Set
    End Property

    Property DataTable_Diagnostic() As System.Data.DataTable

        Get
            DataTable_Diagnostic = mVar_DtDiagnostic
        End Get
        Set(ByVal value As System.Data.DataTable)
            mVar_DtDiagnostic = value
        End Set
    End Property

#End Region

#Region "------ Events -----"

#Region "----- Forms -----"

    Private Sub frmCircuitoInfo_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frm_Diagnostic = Nothing
    End Sub

    Private Sub frmCircuitoInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'For order all datatables in form 
        Dim DvDiagnostic As System.Data.DataView

        Try

            lblTitulo.Text = My.Settings.Application.ToString & " " & My.Settings.Version.ToString

            Select Case TypeFunction
                'Detalhamento de esgostos 
                Case "detesgoto_convertunifilar"
                    'Create array with descobject --> Describe in documentation
                    With ArrayDescObject
                        .Add("Diametro")
                        .Add("Objetos")
                        .Add("Diversos")
                    End With

                    If Not mVar_DtDiagnostic Is Nothing Then If mVar_DtDiagnostic.Rows.Count <= 0 Then Me.Dispose(True)
                    DvDiagnostic = New System.Data.DataView(mVar_DtDiagnostic)
                    DvDiagnostic.Sort = "TypeObject ASC"
                    FillTreeView_DetEsgoto(trvInfo, "", True, DvDiagnostic.ToTable)

                Case Else
                    'Create array with descobject --> Describe in documentation general 
                    With ArrayDescObject
                        .Add("Aplicacao")
                        .Add("Projeto")
                        .Add("Hardware")
                        .Add("Diversos")
                    End With

            End Select

        Catch ex As Exception

        End Try

    End Sub

#End Region

#Region "----- CommandButton -----"

    Private Sub btnFechar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFechar.Click
        Me.Dispose(True)
    End Sub

#End Region

#End Region

#Region "----- Funcions for fill components -----"

    'Treeview para descrições e nomes de dispositivos de proteção
    Function FillTreeView_DetEsgoto(ByVal Trv As TreeView, ByVal sSql As String, Optional ByVal ClearTView As Boolean = True, Optional ByVal Dtbl As System.Data.DataTable = Nothing, Optional ByVal TypeData As String = "", Optional ByVal ArrayColumns() As String = Nothing, _
    Optional ByVal FormatoDt As String = "dd/mm/yyyy", Optional ByVal LstImg As System.Windows.Forms.ImageList = Nothing) As Boolean

        If ClearTView = True Then
            Trv.ResetText()
            Trv.Nodes.Clear()
        End If

        Dim Resultado As String = "Não informado", AtualizouNodeProtocolo As Boolean = False, TempDt_Nasc As String = ""
        Dim LimpaDrR As Boolean = False, Texto As String = "", Cor As New Color, Column0 As String = "", Column1 As String = ""

        Try

            'Povoa com os dados do banco de dados de projetos 
            If Dtbl Is Nothing Then Return Nothing
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

                    If Column0 <> ArrayDescObject(DrR("typeobject").ToString) Then
                        'Monta a treeview com a sequencia --> Column0, Column1 e Column2
                        'Nó principal - Column0 - Primeira Coluna 
                        Node0 = New TreeNode
                        Node0.Text = ArrayDescObject(DrR("typeobject").ToString)
                        'Armazena somente id para a tabela de notas fiscais
                        Node0.Tag = "NODE0|TYP"
                        .ShowNodeToolTips = True
                        .ForeColor = Color.Red
                        'Node0.ImageIndex = 3
                        'Node0.SelectedImageIndex = 3
                        .Nodes.Add(Node0)
                        Column0 = ArrayDescObject(DrR("TypeObject").ToString)
                        i += 1

                        'Informações referentes ao nó com o nome do cliente 
                        'NodeProtocolo.ImageIndex = 15
                        Node1 = New TreeNode

                        With Node1
                            'Será utilizado para consultas para não precisar reconsultar no banco de dados   
                            .Tag = "NODE1|" & DrR("Handle").ToString
                            '.ToolTipText = " : " & DrR(_PrjDwgNome).ToString & Chr(13)
                            .Text = DrR("Handle").ToString & " - " & DrR("Descricao").ToString
                            .ForeColor = Color.Black
                            '.ImageIndex = 1
                            '.SelectedImageIndex = 1
                        End With

                        Column1 = DrR("Handle").ToString & " - " & DrR("Descricao").ToString
                        'Neste caso adicionou o node0, node1
                        Node0.Nodes.Add(Node1)

                    Else

                        If Column1 <> ArrayDescObject(DrR("TypeObject").ToString) Then
                            Node1 = New TreeNode

                            With Node1
                                'Será utilizado para consultas para não precisar reconsultar no banco de dados   
                                .Tag = "NODE1|" & DrR("Handle").ToString
                                .ForeColor = Color.Black
                                '.ToolTipText = " : " & DrR(_PrjDwgNome).ToString & Chr(13)
                                .Text = DrR("Handle").ToString & " - " & DrR("Descricao").ToString
                                '.ImageIndex = 1
                                '.SelectedImageIndex = 1
                            End With
                            Column1 = DrR("Handle").ToString & " - " & DrR("Descricao").ToString
                            Node0.Nodes.Add(Node1)

                            AtualizouNodeProtocolo = True

                        End If

                    End If

                End With

            Loop

            If LimpaDrR = True Then DrR.Close()
            Trv.ExpandAll()
            Return True

        Catch ex As Exception
            Dim LibraryError As New LibraryError
            LibraryError.CreateErrorAenge(Err, "Erro ao povoar as informações do componente Treeview - Erro de validação e consulta de projetos cadastrados !", , _
                                          mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "frmDiagnostic001")
            Return False
        Finally

        End Try

    End Function


#End Region

End Class
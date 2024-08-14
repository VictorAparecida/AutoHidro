Imports System.Windows.Forms
Imports System.Drawing

Public Class frmTarefa

#Region "----- Documentação ----"

    '============================================================================================================
    'Módulo : frmCircuitoInfo
    'Empresa : Autoenge Brasil Ltda
    'Data da criação : Quarta-Feira, 13 de março de 2013
    'Analista responsável : Raul Antonio Fernandes Junior
    'Descrição : Formulário geral de acesso a dados e informações do usuário 
    'Id de tratamento de Erros : 
    'UsrCGeneral01 - 
    'UsrCGeneral02 - trvInfo_NodeMouseDoubleClick
    'UsrCGeneral03 - 
    'UsrCGeneral04 - 
    'UsrCGeneral05 - 
    'UsrCGeneral06 - 
    'UsrCGeneral07 - 
    'UsrCGeneral08 - 
    'UsrCGeneral09 - 
    'UsrCGeneral010 - 
    '
    'Modificações
    '
    '============================================================================================================

#End Region

#Region "----- Atributos e Declaracoes -----"

    Public mVar_InfoText As String, mVar_TypeFunction As String, mVar_DtDiagnostic As New System.Data.DataTable, mVar_NameClass As String = "frmUsrcGeneral"

    Private Shared ArrayDescObject As New ArrayList, IndexCorrect As Int16 = -1, mVar_DtTreeView As DataTable, mVar_Id_Form As String

    Dim LibraryError As New LibraryError

#End Region

#Region "----- Get e Set -----"

    'Get which form is called 
    Property Id_Form() As String
        Get
            Id_Form = mVar_Id_Form
        End Get
        Set(ByVal value As String)
            mVar_Id_Form = value
        End Set
    End Property

    Property DataTable_TreeView() As System.Data.DataTable
        Get
            DataTable_TreeView = mVar_DtTreeView
        End Get
        Set(ByVal value As System.Data.DataTable)
            mVar_DtTreeView = value
        End Set
    End Property

#End Region

#Region "----- Events -----"

#Region "----- Forms -----"

    Private Sub frmTarefa_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        frm_Tarefa = Nothing
    End Sub

    Private Sub frmTarefa_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Select Case mVar_Id_Form.ToLower
            Case "apwrsch".ToLower
                lblSubTit1.Text = "Agendamento de tarefas"

        End Select
    End Sub

#End Region

#Region "----- Treeview -----"

    Private Sub trvInfo_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvInfo.AfterSelect

    End Sub

    Private Sub trvInfo_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvInfo.NodeMouseDoubleClick

        Try

            With trvInfo

                If e.Node.Tag.ToString.Contains("NODE0") Then
                    MsgBox("Informe um registro válido abaixo primeiro !", MsgBoxStyle.Information, "Registro inválido")
                    Exit Sub
                End If

                MsgBox(e.Node.ToolTipText.ToString, MsgBoxStyle.OkOnly, "Visualização")

            End With

        Catch ex As Exception
            LibraryError.CreateErrorAenge(Err, "Erro ao selecionar o registro para visualização - trvV - " & ex.Message, , mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "UsrCGeneral02")
        End Try

    End Sub

#End Region

#Region "----- CommandButton -----"

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Dispose(True)
    End Sub

#End Region

#End Region

#Region "----- Funcions for fill components -----"

    'Treeview para descrições e nomes de dispositivos de proteção
    Function FillTreeView_Agendamento(ByVal Trv As TreeView, ByVal sSql As String, Optional ByVal ClearTView As Boolean = True, Optional ByVal Dtbl As System.Data.DataTable = Nothing, Optional ByVal TypeData As String = "", Optional ByVal ArrayColumns() As String = Nothing, _
    Optional ByVal FormatoDt As String = "dd/mm/yyyy", Optional ByVal LstImg As System.Windows.Forms.ImageList = Nothing) As Boolean

        Trv = trvInfo
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
                    If Column0 <> "Agenda e informações" Then
                        'Monta a treeview com a sequencia --> Column0, Column1 e Column2
                        'Nó principal - Column0 - Primeira Coluna 
                        Node0 = New TreeNode
                        Node0.Text = "Agenda e informações"
                        Node0.ForeColor = Color.Red
                        'Armazena somente id para a tabela de notas fiscais
                        Node0.Tag = "NODE0|AGD"
                        .ShowNodeToolTips = True
                        'Node0.ImageIndex = 3
                        'Node0.SelectedImageIndex = 3
                        .Nodes.Add(Node0)
                        Column0 = "Agenda e informações"
                        i += 1

                        'Informações referentes ao nó com o nome do cliente 
                        'NodeProtocolo.ImageIndex = 15
                        Node1 = New TreeNode

                        With Node1
                            'Será utilizado para consultas para não precisar reconsultar no banco de dados   
                            .Tag = "NODE1|" & DrR("IdTarefa").ToString
                            .ToolTipText = DrR("Descricao").ToString & Chr(13)
                            .Text = CDate(DrR("Data").ToString).ToString("dd/MM/yyyy") & " " & CDate(DrR("Hora").ToString).ToString("hh:mm") & " - " & DrR("Assunto").ToString
                            Node1.ForeColor = Color.Black
                            '.ImageIndex = 1
                            '.SelectedImageIndex = 1
                        End With
                        Column1 = DrR("IdTarefa").ToString
                        'Neste caso adicionou o node0, node1
                        Node0.Nodes.Add(Node1)

                    Else
                        If Column1 <> DrR("IdTarefa").ToString Then
                            Node1 = New TreeNode
                            With Node1
                                'Será utilizado para consultas para não precisar reconsultar no banco de dados   
                                .Tag = "NODE1|" & DrR("IdTarefa").ToString
                                .ToolTipText = DrR("Descricao").ToString & Chr(13)
                                .Text = CDate(DrR("Data").ToString).ToString("dd/MM/yyyy") & " " & CDate(DrR("Hora").ToString).ToString("hh:mm") & " - " & DrR("Assunto").ToString
                                Node1.ForeColor = Color.Black
                                '.ImageIndex = 1
                                '.SelectedImageIndex = 1
                            End With
                            Column1 = DrR("IdTarefa").ToString
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
            LibraryError.CreateErrorAenge(Err, "Erro ao povoar as informações do componente Treeview - Agendamento de tarefas e informações gerais - " & ex.Message, , _
                                          mVar_NameApplication, mVar_NameOwner, mVar_NameClass, "UsrCGeneral01")
            Return False
        Finally

        End Try

    End Function

#End Region

End Class
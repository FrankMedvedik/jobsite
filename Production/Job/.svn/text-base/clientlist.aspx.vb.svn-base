Imports System.Configuration
Imports System.Data.SqlClient

Public Class clientlist
    Inherits System.Web.UI.Page
    Protected WithEvents rptClientList As System.Web.UI.WebControls.Repeater
    Protected WithEvents txtSearch As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        LookUp()
    End Sub

    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        LookUp()
    End Sub

    Private Sub LookUp()
        Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim _search As String = ""
        _search = txtSearch.Text
        Dim _sql As String = ""
        If _search = "" Then
            _sql = "SELECT clientid, clientname, city, state FROM clients WHERE clientname is not null ORDER BY clientid"
        Else
            _sql = "SELECT clientid, clientname, city, state FROM clients WHERE clientname LIKE '" + _search + "' ORDER BY clientid"
        End If
        Dim cmd As New SqlCommand(_sql, cn)
        Dim dr As SqlDataReader
        cn.Open()
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        rptClientList.DataSource = dr
        rptClientList.DataBind()
        cn.Close()
    End Sub

    Public Function Combine(ByVal city As String, ByVal state As String) As String
        Dim cs As String = String.Empty
        If city <> "" And state <> "" Then
            cs = city + ", " + state
        ElseIf city <> "" And state = "" Then
            cs = city
        ElseIf city = "" And state <> "" Then
            cs = state
        End If
        Return cs
    End Function
End Class

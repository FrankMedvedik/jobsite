Imports System.Data
Imports System.Configuration

Public Class client
    Inherits System.Web.UI.Page
    Protected WithEvents txtClientID As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClientName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClientAddress As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClientCity As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClientState As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClientZip As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClientPhone As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClientFax As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClientEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSaveClient As System.Web.UI.WebControls.LinkButton
    Protected WithEvents errMessage As System.Web.UI.WebControls.Label
    Protected WithEvents txtClientAttention As System.Web.UI.WebControls.TextBox
    Protected WithEvents emailValidator As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents idValidator As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents phoneValidator As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents faxValidator As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents txtClientCountry As System.Web.UI.WebControls.TextBox

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
        If Not Page.IsPostBack Then
            If Page.Request.QueryString("ClientID") <> "" Then
                Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                Dim cmd As New SqlCommand("sp_GetClient", cn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim prmClientId As New SqlParameter("@ClientID", SqlDbType.VarChar, 8)
                prmClientId.Value = Page.Request.QueryString("ClientID")
                cmd.Parameters.Add(prmClientId)

                cn.Open()
                Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                If dr.Read Then
                    If Not IsDBNull(dr("clientid")) Then txtClientID.Text = dr("clientid")
                    If Not IsDBNull(dr("clientname")) Then txtClientName.Text = dr("clientname")
                    If Not IsDBNull(dr("address")) Then txtClientAddress.Text = dr("address")
                    If Not IsDBNull(dr("city")) Then txtClientCity.Text = dr("city")
                    If Not IsDBNull(dr("state")) Then txtClientState.Text = dr("state")
                    If Not IsDBNull(dr("zip")) Then txtClientZip.Text = dr("zip")
                    If Not IsDBNull(dr("phone")) Then txtClientPhone.Text = dr("phone")
                    If Not IsDBNull(dr("fax")) Then txtClientFax.Text = dr("fax")
                    If Not IsDBNull(dr("email")) Then txtClientEmail.Text = dr("email")
                    If Not IsDBNull(dr("country")) Then txtClientCountry.Text = dr("country")
                End If
            End If
        End If
    End Sub

    Private Sub btnSaveClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveClient.Click
        If Page.IsValid Then
            If Page.Request.QueryString("ClientID") = "" Then
                Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                Dim cmd As New SqlCommand("CreateClient", cn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim prmClientID As New SqlParameter("@ClientID", SqlDbType.VarChar, 8)
                prmClientID.Value = txtClientID.Text
                cmd.Parameters.Add(prmClientID)

                Dim prmClientName As New SqlParameter("@ClientName", SqlDbType.VarChar, 50)
                prmClientName.Value = txtClientName.Text
                cmd.Parameters.Add(prmClientName)

                Dim prmClientAddress As New SqlParameter("@Address", SqlDbType.VarChar, 100)
                prmClientAddress.Value = txtClientAddress.Text
                cmd.Parameters.Add(prmClientAddress)

                Dim prmClientCity As New SqlParameter("@City", SqlDbType.VarChar, 30)
                prmClientCity.Value = txtClientCity.Text
                cmd.Parameters.Add(prmClientCity)

                Dim prmClientState As New SqlParameter("@State", SqlDbType.VarChar, 3)
                prmClientState.Value = txtClientState.Text
                cmd.Parameters.Add(prmClientState)

                Dim prmClientZip As New SqlParameter("@Zip", SqlDbType.VarChar, 10)
                prmClientZip.Value = txtClientZip.Text
                cmd.Parameters.Add(prmClientZip)

                Dim prmClientAttention As New SqlParameter("@Attention", SqlDbType.VarChar, 40)
                prmClientAttention.Value = txtClientAttention.Text
                cmd.Parameters.Add(prmClientAttention)

                Dim prmClientPhone As New SqlParameter("@Phone", SqlDbType.VarChar, 10)
                prmClientPhone.Value = onlyNumbers(txtClientPhone.Text)
                cmd.Parameters.Add(prmClientPhone)

                Dim prmClientFax As New SqlParameter("@Fax", SqlDbType.VarChar, 10)
                prmClientFax.Value = onlyNumbers(txtClientFax.Text)
                cmd.Parameters.Add(prmClientFax)

                Dim prmClientEmail As New SqlParameter("@Email", SqlDbType.VarChar, 50)
                prmClientEmail.Value = txtClientEmail.Text
                cmd.Parameters.Add(prmClientEmail)

                Dim prmClientCountry As New SqlParameter("@Country", SqlDbType.VarChar, 30)
                prmClientCountry.Value = txtClientCountry.Text
                cmd.Parameters.Add(prmClientCountry)

                Dim prmRetVal As New SqlParameter("@retval", SqlDbType.Int, 4)
                prmRetVal.Direction = ParameterDirection.ReturnValue
                cmd.Parameters.Add(prmRetVal)

                cn.Open()
                cmd.ExecuteNonQuery()
                cn.Close()

                If cmd.Parameters("@retval").Value = 0 Then
                    errMessage.Text = "Error Creating Client"
                Else
                    errMessage.Text = "Client Created"
                End If
            Else
                Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                Dim cmd As New SqlCommand("UpdateClient", cn)
                cmd.CommandType = CommandType.StoredProcedure

                Dim prmClientID As New SqlParameter("@ClientID", SqlDbType.VarChar, 8)
                prmClientID.Value = txtClientID.Text
                cmd.Parameters.Add(prmClientID)

                Dim prmClientName As New SqlParameter("@ClientName", SqlDbType.VarChar, 50)
                prmClientName.Value = txtClientName.Text
                cmd.Parameters.Add(prmClientName)

                Dim prmClientAddress As New SqlParameter("@Address", SqlDbType.VarChar, 100)
                prmClientAddress.Value = txtClientAddress.Text
                cmd.Parameters.Add(prmClientAddress)

                Dim prmClientCity As New SqlParameter("@City", SqlDbType.VarChar, 30)
                prmClientCity.Value = txtClientCity.Text
                cmd.Parameters.Add(prmClientCity)

                Dim prmClientState As New SqlParameter("@State", SqlDbType.VarChar, 3)
                prmClientState.Value = txtClientState.Text
                cmd.Parameters.Add(prmClientState)

                Dim prmClientZip As New SqlParameter("@Zip", SqlDbType.VarChar, 10)
                prmClientZip.Value = txtClientZip.Text
                cmd.Parameters.Add(prmClientZip)

                Dim prmClientAttention As New SqlParameter("@Attention", SqlDbType.VarChar, 40)
                prmClientAttention.Value = txtClientAttention.Text
                cmd.Parameters.Add(prmClientAttention)

                Dim prmClientPhone As New SqlParameter("@Phone", SqlDbType.VarChar, 10)
                prmClientPhone.Value = onlyNumbers(txtClientPhone.Text)
                cmd.Parameters.Add(prmClientPhone)

                Dim prmClientFax As New SqlParameter("@Fax", SqlDbType.VarChar, 10)
                prmClientFax.Value = onlyNumbers(txtClientFax.Text)
                cmd.Parameters.Add(prmClientFax)

                Dim prmClientEmail As New SqlParameter("@Email", SqlDbType.VarChar, 50)
                prmClientEmail.Value = txtClientEmail.Text
                cmd.Parameters.Add(prmClientEmail)

                Dim prmClientCountry As New SqlParameter("@Country", SqlDbType.VarChar, 30)
                prmClientCountry.Value = txtClientCountry.Text
                cmd.Parameters.Add(prmClientCountry)

                Dim prmRetVal As New SqlParameter("@retval", SqlDbType.Int, 4)
                prmRetVal.Direction = ParameterDirection.ReturnValue
                cmd.Parameters.Add(prmRetVal)

                cn.Open()
                cmd.ExecuteNonQuery()
                cn.Close()

                If cmd.Parameters("@retval").Value = 0 Then
                    errMessage.Text = "Error Updating Client"
                Else
                    errMessage.Text = "Client Updated"
                End If
            End If
        End If
    End Sub
    Private Function onlyNumbers(ByVal str As String) As String
        Dim i As Integer
        Dim rt As New System.Text.StringBuilder()
        For i = 1 To str.Length
            If IsNumeric(Mid(str, i, 1)) Then rt.Append(Mid(str, i, 1))

        Next
        Return rt.ToString
    End Function
End Class

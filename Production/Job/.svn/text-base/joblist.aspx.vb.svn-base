Public Class joblist
    Inherits System.Web.UI.Page
    Protected WithEvents rptJobList As System.Web.UI.WebControls.Repeater
    Protected WithEvents lblMore As System.Web.UI.WebControls.Label


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Dim JobNum As Integer
        If IsNothing(Request.QueryString("jobnum")) Then
            JobNum = 0
        Else
            JobNum = Request.QueryString("jobnum")
        End If

        Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim cmd As New SqlCommand("sp_Job_List", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim adpt As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()

        Dim prmStarting As New SqlParameter("@Starting", SqlDbType.Int, 4)
        prmStarting.Value = JobNum
        cmd.Parameters.Add(prmStarting)

        cn.Open()
        adpt.Fill(ds)

        rptJobList.DataSource = ds.Tables(0).DefaultView
        rptJobList.DataBind()

        cn.Close()
        Dim i As Integer = ds.Tables(0).Rows.Count() - 1
        lblMore.Text = "<a href=" & Request.ServerVariables("SCRIPT_NAME") & "?control=" & Request.QueryString("control") & "&jobnum=" & ds.Tables(0).Rows(i).Item(0) & ">more values</a>"
    End Sub

End Class



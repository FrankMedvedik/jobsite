Public Class InvoiceList
    Inherits System.Web.UI.Page
    Dim sSortOrder As String
    Dim sFindText As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            txtPageSize.Text = "10"
            BindData()
        Else
            dgInvoices.PageSize = CInt(txtPageSize.Text)
            If txtFilterValue.Text <> "" Then
                If cmbFilterColumn.SelectedItem.Value = "jra_job_num" Then
                    sFindText = cmbFilterColumn.SelectedItem.Value + " = " + txtFilterValue.Text
                Else
                    sFindText = cmbFilterColumn.SelectedItem.Value + " LIKE '" + txtFilterValue.Text + "'"
                End If
            End If
            If Not ViewState("sortfield") Is Nothing Then
                sSortOrder = ViewState("sortfield") + " " + ViewState("sortdirection")
            End If

        End If
    End Sub
    Private Sub Page_Error(ByVal sender As System.Object, ByVal e As EventArgs)
        Dim err As String = "Error in: " & Request.Url.ToString & "<p/>" & _
                            "Stack Trace Below:<br/>" & _
                            Server.GetLastError().ToString()
        Response.Write(err)
        Server.ClearError()
    End Sub
    Protected Sub dbInvoices_ItemCreated(ByVal sender As System.Object, ByVal e As DataGridItemEventArgs) Handles dgInvoices.ItemCreated
        If e.Item.DataItem Is Nothing Then Exit Sub
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem, ListItemType.EditItem
                Dim oDeleteButton As ImageButton
                oDeleteButton = e.Item.FindControl("btnDelete")
                oDeleteButton.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this invoice?');")

                ' change row style 
                Dim sApproved As String = DataBinder.Eval(e.Item.DataItem, "approved").ToString
                Dim bApproved As Boolean = Boolean.Parse(sApproved)
                Dim sPrinted As String = DataBinder.Eval(e.Item.DataItem, "printed").ToString
                Dim bPrinted As Boolean = Boolean.Parse(sPrinted)
                Dim sInvoiceType As String = DataBinder.Eval(e.Item.DataItem, "invoice_type").ToString
                Dim sStyle As String = "Normal"
                If (bApproved And bPrinted = False) Or (sInvoiceType = "Advance" And bPrinted = False) Then
                    sStyle = "NormalAttention"
                ElseIf bApproved = False And bPrinted = False Then
                    sStyle = "NormalInProgress"
                Else
                    sStyle = "Normal"
                End If
                e.Item.CssClass = sStyle

        End Select
    End Sub
    Protected Function getPath(ByVal id As Integer, ByVal approved As Boolean) As String
        Dim url As String = ""
        If approved Then
            ' url = "production_print.aspx?id=" & CStr(id) & "&mode=0"
            url = "http://sql08-02/ReportServer/Pages/ReportViewer.aspx?http%3a%2f%2fhome.reckner.com%2fAnnouncements%2fReports%2fInvoice%2fInvoice.rdl&rs:Command=Render&invoice_id=" & CStr(id)

        Else
            url = "CreateInvoice.aspx?id=" & CStr(id)
        End If
        Return url
    End Function
    Private Sub Invoices_Click(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles dgInvoices.ItemCommand
        If e.CommandName = "delete" Then
            Dim oCN As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
            Dim oCMD As New SqlCommand("deleteInvoice", oCN)
            oCMD.CommandType = CommandType.StoredProcedure

            Dim prmInvoiceID As New SqlParameter("@invoice_id", SqlDbType.Int, 4)
            prmInvoiceID.Value = dgInvoices.DataKeys.Item(e.Item.ItemIndex)
            oCMD.Parameters.Add(prmInvoiceID)

            Dim err As Exception
            Try
                oCN.Open()
                oCMD.ExecuteNonQuery()

            Finally
                oCN.Close()
                BindData()
            End Try
        End If
    End Sub
    Private Sub NewInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewInvoice.Click
        Dim oCN As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim oCMD As New SqlCommand("addInvoice", oCN)
        oCMD.CommandType = CommandType.StoredProcedure

        Dim prmJobNum As New SqlParameter("@job_num", SqlDbType.Int, 4)
        prmJobNum.Value = CInt(cmbJobNum.SelectedItem.Value)
        oCMD.Parameters.Add(prmJobNum)

        Dim prmAddExpense As New SqlParameter("@add_expense", SqlDbType.Bit)
        prmAddExpense.Value = chkInclude.Checked
        oCMD.Parameters.Add(prmAddExpense)

        Dim prmRetVal As New SqlParameter("retval", SqlDbType.Int, 4)
        prmRetVal.Direction = ParameterDirection.ReturnValue
        oCMD.Parameters.Add(prmRetVal)

        Dim err As Exception
        Try
            oCN.Open()
            oCMD.ExecuteNonQuery()
            Dim iInvoiceID As Integer = oCMD.Parameters("retval").Value
            Response.Redirect("createinvoice.aspx?id=" & iInvoiceID)
        Catch err
            errMessage.Text = err.Message
            Trace.Write("error in create invoice: " & err.Message)
        Finally
            oCN.Close()
        End Try

    End Sub
    Protected Sub ChangeInvoicePage(ByVal sender As System.Object, ByVal e As DataGridPageChangedEventArgs)
        dgInvoices.CurrentPageIndex = e.NewPageIndex
        BindData()

    End Sub
    Protected Sub FilterInvoices(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click
        ' 2003.02.04 - Duane
        ' Added check to ensure that user has entered a numeric value if filtering on job number.
        '
        If cmbFilterColumn.SelectedItem.Value = "jra_job_num" Then
            If Not IsNumeric(txtFilterValue.Text) Or Len(txtFilterValue.Text) = 0 Then
                errMessage.Text = "You did not enter a numeric job number."
            Else
                errMessage.Text = ""
                sFindText = cmbFilterColumn.SelectedItem.Value + " = " + txtFilterValue.Text
            End If
        Else
            sFindText = cmbFilterColumn.SelectedItem.Value + " LIKE '" + txtFilterValue.Text + "'"
        End If
        dgInvoices.CurrentPageIndex = 0
        BindData()
    End Sub
    Protected Sub ClearFilter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearFilter.Click
        sFindText = ""
        txtFilterValue.Text = ""
        BindData()
    End Sub
    Protected Sub Refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        BindData()
    End Sub
    Protected Sub setPageMode(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPageMode.CheckedChanged
        If chkPageMode.Checked Then
            dgInvoices.PagerStyle.Mode = PagerMode.NumericPages
        Else
            dgInvoices.PagerStyle.Mode = PagerMode.NextPrev
        End If

    End Sub

    Protected Sub SortInvoices(ByVal sender As System.Object, ByVal e As DataGridSortCommandEventArgs)

        viewstate.Add("sortfield", e.SortExpression)
        If viewstate("sortdirection") Is Nothing Then
            viewstate.Add("sortdirection", "ASC")
        Else
            If viewstate("sortdirection") = "ASC" Then
                viewstate("sortdirection") = "DESC"
            Else
                viewstate("sortdirection") = "ASC"
            End If
        End If


        sSortOrder = e.SortExpression + " " + viewstate("sortdirection")
        BindData()

    End Sub
    Private Sub BindData()
        Dim oCN As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim oCMD As New SqlCommand("PagedInvoiceList", oCN)
        oCMD.CommandType = CommandType.StoredProcedure

        Dim prmTotalRecords As New SqlParameter("@TotalRecords", SqlDbType.Int, 4)
        prmTotalRecords.Direction = ParameterDirection.Output
        oCMD.Parameters.Add(prmTotalRecords)

        Dim prmPageSize As New SqlParameter("@PageSize", SqlDbType.Int, 4)
        prmPageSize.Value = CInt(txtPageSize.Text)
        oCMD.Parameters.Add(prmPageSize)

        Dim prmCurrentPage As New SqlParameter("@CurrentPage", SqlDbType.Int, 4)
        prmCurrentPage.Value = dgInvoices.CurrentPageIndex + 1
        oCMD.Parameters.Add(prmCurrentPage)

        If sFindText <> "" Then
            Dim prmWhere As New SqlParameter("@Where", SqlDbType.VarChar, 255)
            prmWhere.Value = sFindText
            oCMD.Parameters.Add(prmWhere)
        End If

        If sSortOrder <> "" Then
            Dim prmOrder As New SqlParameter("@Order", SqlDbType.VarChar, 255)
            prmOrder.Value = sSortOrder
            oCMD.Parameters.Add(prmOrder)
        End If

        Dim ad As New SqlDataAdapter(oCMD)
        Dim ds As New DataSet()
        Dim err As Exception

        Try
            oCN.Open()
            ad.Fill(ds)
            dgInvoices.VirtualItemCount = oCMD.Parameters("@TotalRecords").Value
            dgInvoices.DataSource = ds
            dgInvoices.DataBind()


        Catch err
            Trace.Write("error in invoice list bind: " & err.Message)
        Finally
            oCN.Close()
        End Try


        Dim oJobList As New SqlCommand("SELECT Top 2000 jobnum, convert(varchar,jobnum,8) + ' ' + jobname as jobname FROM joblog ORDER BY jobnum DESC", oCN)
        cmbJobNum.DataValueField = "jobnum"
        cmbJobNum.DataTextField = "jobname"
        Try
            oCN.Open()
            cmbJobNum.DataSource = oJobList.ExecuteReader(CommandBehavior.CloseConnection)
            cmbJobNum.DataBind()
        Catch err
            Trace.Write("error in joblist data bind : " & err.Message)
        Finally
            oCN.Close()
        End Try
    End Sub
End Class
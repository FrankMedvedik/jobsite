Public Class CreateInvoice
    Inherits System.Web.UI.Page
    Protected WithEvents dsValueList As New DataSet()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadValueList()
            BindData()
        Else
            LoadValueList()
        End If
    End Sub
    Private Sub Page_Error(ByVal sender As System.Object, ByVal e As EventArgs)
        Dim err As String = "Error in: " & Request.Url.ToString & "<p/>" & _
                            "Stack Trace Below:<br/>" & _
                            Server.GetLastError().ToString()
        Response.Write(err)
        Server.ClearError()
    End Sub
    Private Sub LoadValueList()
        Dim oCN As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim oCMD As New SqlCommand("GetInvoiceValueList", oCN)
        oCMD.CommandType = CommandType.StoredProcedure
        Dim ad As New SqlDataAdapter(oCMD)
        Dim err As Exception
        Try
            ad.Fill(dsValueList)
        Catch err
            Trace.Write(err.Source & " " & err.Message)
        Finally
            oCN.Close()
        End Try

    End Sub
    Private Sub SaveInvoice()
        Dim oCN As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim oCMD As New SqlCommand("UpdateInvoice", oCN)
        oCMD.CommandType = CommandType.StoredProcedure

        Dim prmInvoiceID As New SqlParameter("@invoice_id", SqlDbType.Int, 4)
        prmInvoiceID.Value = CInt(lblInvoiceID.Text)
        oCMD.Parameters.Add(prmInvoiceID)

        Dim prmInvoiceNumber As New SqlParameter("@invoice_number", SqlDbType.VarChar, 10)
        prmInvoiceNumber.Value = lblInvoiceNumber.Text
        oCMD.Parameters.Add(prmInvoiceNumber)

        Dim prmInvoiceDate As New SqlParameter("@invoice_date", SqlDbType.SmallDateTime)
        prmInvoiceDate.Value = CDate(txtInvoiceDate.Text)
        oCMD.Parameters.Add(prmInvoiceDate)

        Dim prmInvoiceType As New SqlParameter("@invoice_type", SqlDbType.Char, 10)
        prmInvoiceType.Value = cmbInvoiceType.SelectedItem.Value
        oCMD.Parameters.Add(prmInvoiceType)

        Dim prmIsApproved As New SqlParameter("@approved", SqlDbType.Bit)
        prmIsApproved.Value = CBool(chkApproved.Checked)
        oCMD.Parameters.Add(prmIsApproved)

        Dim prmToBePrinted As New SqlParameter("@printed", SqlDbType.Bit)
        prmToBePrinted.Value = CBool(chkPrinted.Checked)
        oCMD.Parameters.Add(prmToBePrinted)

        Dim prmToBeExported As New SqlParameter("@exported", SqlDbType.Bit)
        prmToBeExported.Value = CBool(chkExported.Checked)
        oCMD.Parameters.Add(prmToBeExported)

        Dim prmCustomerId As New SqlParameter("@customer_id", SqlDbType.Char, 15)
        prmCustomerId.Value = cmbCustomer.SelectedItem.Value
        oCMD.Parameters.Add(prmCustomerId)

        Dim prmTerms As New SqlParameter("@terms", SqlDbType.VarChar, 15)
        prmTerms.Value = txtInvoiceTerms.Text
        oCMD.Parameters.Add(prmTerms)

        Dim prmJraJobNum As New SqlParameter("@jra_job_num", SqlDbType.Int, 4)
        prmJraJobNum.Value = CInt(lblInvoiceJobNum.Text)
        oCMD.Parameters.Add(prmJraJobNum)

        Dim prmClientJobNum As New SqlParameter("@client_job_num", SqlDbType.VarChar, 25)
        prmClientJobNum.Value = txtInvoiceClientNum.Text
        oCMD.Parameters.Add(prmClientJobNum)

        Dim prmPONum As New SqlParameter("@po_num", SqlDbType.VarChar, 25)
        prmPONum.Value = txtInvoicePONumber.Text
        oCMD.Parameters.Add(prmPONum)

        Dim prmAttention As New SqlParameter("@attention", SqlDbType.VarChar, 128)
        prmAttention.Value = txtAttention.Text
        oCMD.Parameters.Add(prmAttention)

        Dim prmJobDescription As New SqlParameter("@job_description", SqlDbType.VarChar, 128)
        prmJobDescription.Value = txtJobDescription.Text
        oCMD.Parameters.Add(prmJobDescription)

        Dim prmInstructions As New SqlParameter("@instructions", SqlDbType.VarChar, 500)
        prmInstructions.Value = txtInstructions.Text
        oCMD.Parameters.Add(prmInstructions)

        Dim prmStudyDates As New SqlParameter("@study_dates", SqlDbType.VarChar, 128)
        prmStudyDates.Value = txtStudyDates.Text
        oCMD.Parameters.Add(prmStudyDates)

        Dim prmRetVal As New SqlParameter("@RetVal", SqlDbType.Int, 4)
        prmRetVal.Direction = ParameterDirection.ReturnValue
        oCMD.Parameters.Add(prmRetVal)

        Dim err As Exception

        Try
            oCN.Open()
            oCMD.ExecuteNonQuery()

        Catch err
            lblErrUpdate.Text = err.Message
        Finally
            oCN.Close()

        End Try
        If oCMD.Parameters("@RetVal").Value = 0 Then
            BindData()
        Else
            lblErrUpdate.Text = "Invoice Number already exists in Accounting Database.  Please try again."
        End If
    End Sub
    Protected Sub SaveInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveInvoice()
    End Sub

    Protected Sub InvoiceDetail_Click(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles dgInvoiceDetail.ItemCommand
        SaveInvoice()
        If e.CommandName = "add" Then
            Dim sQuantity As String
            Dim sPrice As String

            sQuantity = CType(e.Item.FindControl("txtAddQuantity"), TextBox).Text
            sPrice = CType(e.Item.FindControl("txtAddPrice"), TextBox).Text

            If CType(e.Item.FindControl("txtAddDescription"), TextBox).Text = "" Then
                lblErrUpdate.Text = "Error Adding Line - Insufficient Data Supplied"
            Else
                Dim oCN As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                Dim oCMD As New SqlCommand("AddInvoiceDetail", oCN)
                oCMD.CommandType = CommandType.StoredProcedure

                Dim prmInvoiceID As New SqlParameter("@Invoice_ID", SqlDbType.Int, 4)
                prmInvoiceID.Value = CInt(lblInvoiceID.Text)
                oCMD.Parameters.Add(prmInvoiceID)

                Dim prmShowOnInvoice As New SqlParameter("@show_on_invoice", SqlDbType.Bit)
                prmShowOnInvoice.Value = CType(e.Item.FindControl("chkAddShow"), CheckBox).Checked
                ocmd.Parameters.Add(prmShowOnInvoice)

                Dim prmCategory As New SqlParameter("@category", SqlDbType.VarChar, 15)
                prmCategory.Value = CType(e.Item.FindControl("cmbAddCategory"), DropDownList).SelectedItem.Value
                oCMD.Parameters.Add(prmCategory)

                Dim prmDepartment As New SqlParameter("@department", SqlDbType.VarChar, 10)
                prmDepartment.Value = CType(e.Item.FindControl("cmbAddDepartment"), DropDownList).SelectedItem.Value
                oCMD.Parameters.Add(prmDepartment)

                Dim prmQuantity As New SqlParameter("@quantity", SqlDbType.Decimal, 18)
                Dim Quantity As String = CType(e.Item.FindControl("txtAddQuantity"), TextBox).Text
                If IsNumeric(Quantity) Then
                    prmQuantity.Value = CDec(Quantity)
                Else
                    prmQuantity.Value = DBNull.Value
                End If
                oCMD.Parameters.Add(prmQuantity)

                Dim prmLineDescription As New SqlParameter("@line_description", SqlDbType.VarChar, 128)
                prmLineDescription.Value = CType(e.Item.FindControl("txtAddDescription"), TextBox).Text
                oCMD.Parameters.Add(prmLineDescription)

                Dim prmPrice As New SqlParameter("@price", SqlDbType.Money)
                Dim Price As String = CType(e.Item.FindControl("txtAddPrice"), TextBox).Text
                If IsNumeric(Price) Then
                    prmPrice.Value = CDec(Price)
                Else
                    prmPrice.Value = DBNull.Value
                End If
                oCMD.Parameters.Add(prmPrice)

                Dim err As Exception
                Try
                    oCN.Open()
                    oCMD.ExecuteNonQuery()
                Catch err
                    Response.Write(err.Message)
                Finally
                    oCN.Close()
                End Try

                BindData()
            End If

        ElseIf e.CommandName = "cancel" Then
            dgInvoiceDetail.EditItemIndex = -1
            BindData()

        ElseIf e.CommandName = "delete" Then
            Dim oCN As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
            Dim oCMD As New SqlCommand("DeleteInvoiceDetail", ocn)
            ocmd.CommandType = CommandType.StoredProcedure

            Dim prmDetailID As New SqlParameter("@detail_id", SqlDbType.Int, 4)
            prmDetailID.Value = dgInvoiceDetail.DataKeys(e.Item.ItemIndex)
            ocmd.Parameters.Add(prmDetailID)
            Dim err As Exception
            Try
                ocn.Open()
                ocmd.ExecuteNonQuery()
            Catch err
                Trace.Write("err delete invoice detail: " & err.Message)
            Finally
                BindData()
                ocn.Close()
            End Try


        ElseIf e.CommandName = "edit" Then
            dgInvoiceDetail.EditItemIndex = e.Item.ItemIndex
            BindData()

        ElseIf e.CommandName = "move_down" Then
            Dim oCN As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
            Dim oCMD As New SqlCommand("MoveInvoiceDetailDown", ocn)
            ocmd.CommandType = CommandType.StoredProcedure

            Dim prmDetailID As New SqlParameter("@detail_id", SqlDbType.Int, 4)
            prmDetailID.Value = dgInvoiceDetail.DataKeys(e.Item.ItemIndex)
            ocmd.Parameters.Add(prmDetailID)

            Dim err As Exception
            Try
                ocn.Open()
                ocmd.ExecuteNonQuery()
            Catch err
                Trace.Write("error in move down invoice detail: " & err.Message)
            Finally
                BindData()
                ocn.Close()
            End Try

        ElseIf e.CommandName = "move_up" Then
            Dim oCN As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
            Dim oCMD As New SqlCommand("MoveInvoiceDetailUp", ocn)
            ocmd.CommandType = CommandType.StoredProcedure

            Dim prmDetailID As New SqlParameter("@detail_id", SqlDbType.Int, 4)
            prmDetailID.Value = dgInvoiceDetail.DataKeys(e.Item.ItemIndex)
            ocmd.Parameters.Add(prmDetailID)
            Dim err As Exception

            Try
                ocn.Open()
                ocmd.ExecuteNonQuery()
            Catch err
                Trace.Write("error in move up invoice detail: " & err.Message)
            Finally
                BindData()
                ocn.Close()
            End Try


        ElseIf e.CommandName = "update" Then
            Dim oCN As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
            Dim oCMD As New SqlCommand("UpdateInvoiceDetail", ocn)
            ocmd.CommandType = CommandType.StoredProcedure

            Dim prmDetailID As New SqlParameter("@detail_id", SqlDbType.Int, 4)
            prmDetailID.Value = dgInvoiceDetail.DataKeys(e.Item.ItemIndex)
            ocmd.Parameters.Add(prmDetailID)

            Dim prmShowOnInvoice As New SqlParameter("@show_on_invoice", SqlDbType.Bit)
            prmShowOnInvoice.Value = CType(e.Item.FindControl("chkUpdateShow"), CheckBox).Checked
            ocmd.Parameters.Add(prmShowOnInvoice)

            Dim prmCategory As New SqlParameter("@category", SqlDbType.VarChar, 15)
            prmCategory.Value = CType(e.Item.FindControl("cmbCategory"), DropDownList).SelectedItem.Value
            oCMD.Parameters.Add(prmCategory)

            Dim prmDepartment As New SqlParameter("@department", SqlDbType.VarChar, 10)
            prmDepartment.Value = CType(e.Item.FindControl("cmbDepartment"), DropDownList).SelectedItem.Value
            oCMD.Parameters.Add(prmDepartment)

            Dim prmQuantity As New SqlParameter("@quantity", SqlDbType.Decimal)
            prmQuantity.Value = CDec(CType(e.Item.FindControl("txtQuantity"), TextBox).Text)
            oCMD.Parameters.Add(prmQuantity)

            Dim prmLineDescription As New SqlParameter("@line_description", SqlDbType.VarChar, 128)
            prmLineDescription.Value = CType(e.Item.FindControl("txtDescription"), TextBox).Text
            oCMD.Parameters.Add(prmLineDescription)

            Dim prmPrice As New SqlParameter("@price", SqlDbType.Money)
            prmPrice.Value = CDec(CType(e.Item.FindControl("txtPrice"), TextBox).Text)
            oCMD.Parameters.Add(prmPrice)

            Dim err As Exception
            Try
                oCN.Open()
                oCMD.ExecuteNonQuery()
            Catch err
                Trace.Write("error in update invoice detail: " & err.Message)
            Finally
                dgInvoiceDetail.EditItemIndex = -1
                oCN.Close()
                BindData()
            End Try


        End If
    End Sub
    Public Function GetCategory() As DataTable
        Return dsValueList.Tables(0)
    End Function
    Protected Function GetCustomer() As DataTable
        Return dsValueList.Tables(1)
    End Function
    Protected Function GetDepartment() As DataTable
        Return dsValueList.Tables(2)
    End Function
    Protected Function GetSelectedCategory(ByVal sCategory As String) As Integer
        Dim i As Integer
        Dim s As String
        For i = 0 To dsValueList.Tables(0).Rows.Count - 1
            s = RTrim(RTrim(dsValueList.Tables(0).Rows(i).Item(0)))
            If RTrim(dsValueList.Tables(0).Rows(i).Item(0)) = RTrim(sCategory) Then
                Exit For
            End If
        Next
        Return i
    End Function
    Protected Function GetSelectedDepartment(ByVal sDepartment As String) As Integer
        Dim i As Integer
        Dim s As String
        For i = 0 To dsValueList.Tables(2).Rows.Count - 1
            s = RTrim(dsValueList.Tables(2).Rows(i).Item(0))
            If RTrim(dsValueList.Tables(2).Rows(i).Item(0)) = RTrim(sDepartment) Then
                Exit For
            End If
        Next
        Return i
    End Function
    Protected Function GetLineTotal(ByVal Quantity As Object, ByVal Price As Object) As String
        Dim sLineTotal As String = ""
        If IsNumeric(Quantity) And IsNumeric(Price) Then
            sLineTotal = Format(CDec(Quantity) * CDec(Price), "$#,###.00")
        End If
        Return sLineTotal
    End Function
    Private Sub BindData()

        Dim invoice_id As Integer = Request.Params("id")
        Dim mode As Integer = Request.Params("mode")

        Select Case mode
            Case 0
                chkApproved.Enabled = False
                chkPrinted.Enabled = False
                chkExported.Enabled = False
                btnReturn.NavigateUrl = "../DesktopDefault.aspx?tabtier=3&tabindex=1&tabid=53"
            Case 1
                chkApproved.Enabled = True
                chkPrinted.Enabled = False
                chkExported.Enabled = False
                btnReturn.NavigateUrl = "ApproveInvoice.aspx"
            Case 2
                chkApproved.Enabled = False
                chkPrinted.Enabled = True
                chkExported.Enabled = False
            Case 3
                chkApproved.Enabled = False
                chkPrinted.Enabled = False
                chkExported.Enabled = True
        End Select

        lblErrUpdate.Text = ""



        Dim oSQLXML As New SqlXmlCommand(WebConfigurationManager.AppSettings("xmlProback"))
        oSQLXML.RootTag = "invoices"
        oSQLXML.CommandText = "invoice[id='" & CStr(invoice_id) & "']"
        oSQLXML.CommandType = SqlXmlCommandType.XPath
        oSQLXML.SchemaPath = Server.MapPath("xml/invoice.xsd")
        Dim ad As New SqlXmlAdapter(oSQLXML)
        Dim ds As New DataSet()
        ds.ReadXmlSchema(Server.MapPath("xml/invoice_frag.xsd"))
        Dim err As Exception

        Try
            ad.Fill(ds)

        Catch ex As Exception
            lblErrUpdate.Text = ex.ToString
        Finally
            oSQLXML = Nothing
        End Try
        Dim s As String = ds.GetXml
        cmbCustomer.DataSource = GetCustomer()
        cmbCustomer.DataBind()
        cmbCustomer.Items.Insert(0, "")

        Try
            If ds.Tables("invoice").Columns.Contains("approved") Then
                chkApproved.Checked = CBool(ds.Tables("invoice").Rows(0).Item("approved"))
            End If
            If ds.Tables("invoice").Columns.Contains("printed") Then
                chkPrinted.Checked = CBool(ds.Tables("invoice").Rows(0).Item("printed"))
            End If
            If ds.Tables("invoice").Columns.Contains("exported") Then
                chkPrinted.Checked = CBool(ds.Tables("invoice").Rows(0).Item("exported"))
            End If

            If ds.Tables("invoice").Columns.Contains("id") Then
                lblInvoiceID.Text = ds.Tables("invoice").Rows(0).Item("id")
            End If
            If ds.Tables("invoice").Columns.Contains("date") Then
                txtInvoiceDate.Text = CDate(ds.Tables("invoice").Rows(0).Item("date"))
            End If
            If ds.Tables("invoice").Columns.Contains("number") Then
                If Not IsDBNull(ds.Tables("invoice").Rows(0).Item("number")) Then
                    lblInvoiceNumber.Text = ds.Tables("invoice").Rows(0).Item("number")
                End If
            End If
            If ds.Tables("invoice").Columns.Contains("client_id") Then
                lblClientID.Text = ds.Tables("invoice").Rows(0).Item("client_id")
            End If
            If ds.Tables("invoice").Columns.Contains("type") Then
                cmbInvoiceType.SelectedIndex = cmbInvoiceType.Items.IndexOf(cmbInvoiceType.Items.FindByValue(RTrim(ds.Tables("invoice").Rows(0).Item("type"))))
            End If
            If ds.Tables("invoice").Columns.Contains("jra_job_num") Then
                lblInvoiceJobNum.Text = ds.Tables("invoice").Rows(0).Item("jra_job_num")
                btnJobCost.NavigateUrl = "http://sql08-02/ReportServer/Pages/ReportViewer.aspx?http%3a%2f%2fhome.reckner.com%2fAnnouncements%2fReports%2fMisc%20Reports/Job_Cost_Analysis.rdl"
            End If
            If ds.Tables("invoice").Columns.Contains("terms") Then
                txtInvoiceTerms.Text = ds.Tables("invoice").Rows(0).Item("terms")
            End If
            If ds.Tables("invoice").Columns.Contains("client_job_num") Then
                txtInvoiceClientNum.Text = ds.Tables("invoice").Rows(0).Item("client_job_num")
            End If
            If ds.Tables("invoice").Columns.Contains("po_num") Then
                txtInvoicePONumber.Text = ds.Tables("invoice").Rows(0).Item("po_num")
            End If
            If ds.Tables("customer").Rows.Count > 0 Then

                If ds.Tables("customer").Columns.Contains("custid") Then
                    cmbCustomer.SelectedIndex = cmbCustomer.Items.IndexOf(cmbCustomer.Items.FindByValue(RTrim(ds.Tables("customer").Rows(0).Item("custid"))))
                End If
                If ds.Tables("customer").Columns.Contains("name") Then
                    lblCustomerName.Text = ds.Tables("customer").Rows(0).Item("name")
                End If
                If ds.Tables("customer").Columns.Contains("address_one") Then
                    If Not IsDBNull(ds.Tables("customer").Rows(0).Item("address_one")) Then
                        lblCustomerAddressOne.Text = ds.Tables("customer").Rows(0).Item("address_one")
                    End If
                End If
                If ds.Tables("customer").Columns.Contains("address_two") Then
                    lblCustomerAddressTwo.Text = ds.Tables("customer").Rows(0).Item("address_two")
                End If
                If ds.Tables("customer").Columns.Contains("city") Then
                    lblCustomerCity.Text = ds.Tables("customer").Rows(0).Item("city")
                End If
                If ds.Tables("customer").Columns.Contains("state") Then
                    lblCustomerState.Text = ds.Tables("customer").Rows(0).Item("state")
                End If
                If ds.Tables("customer").Columns.Contains("zip") Then
                    lblCustomerZip.Text = ds.Tables("customer").Rows(0).Item("zip")
                End If
                If ds.Tables("customer").Columns.Contains("country") Then
                    lblCustomerCountry.Text = ds.Tables("customer").Rows(0).Item("country")
                End If
                If ds.Tables("customer").Columns.Contains("phone") Then
                    lblCustomerPhoneNumber.Text = ds.Tables("customer").Rows(0).Item("phone")
                End If
                If ds.Tables("customer").Columns.Contains("fax") Then
                    lblCustomerFaxNumber.Text = ds.Tables("customer").Rows(0).Item("fax")
                End If
            End If
            If ds.Tables("invoice").Columns.Contains("attention") Then
                txtAttention.Text = ds.Tables("invoice").Rows(0).Item("attention")
            End If
            If ds.Tables("invoice").Columns.Contains("job_description") Then
                txtJobDescription.Text = ds.Tables("invoice").Rows(0).Item("job_description")
            End If
            'instructions added 3/21/2003
            If ds.Tables("invoice").Columns.Contains("instructions") Then
                txtInstructions.Text = ds.Tables("invoice").Rows(0).Item("instructions")
            End If
            If ds.Tables("invoice").Columns.Contains("study_dates") Then
                txtStudyDates.Text = ds.Tables("invoice").Rows(0).Item("study_dates")
            End If
            Dim i As Integer = 0
            Dim invoiceTotal As Decimal = 0
            Dim distributedTotal As Decimal = 0

            If ds.Tables.Contains("detail") Then
                For i = 0 To ds.Tables("detail").Rows.Count - 1
                    If ds.Tables("detail").Rows(i).Item("account") > 0 And _
                        ds.Tables("detail").Rows(i).Item("account") < 9999 And _
                        IsNumeric(ds.Tables("detail").Rows(i).Item("quantity")) And _
                        IsNumeric(ds.Tables("detail").Rows(i).Item("price")) Then
                        distributedTotal += CDec(ds.Tables("detail").Rows(i).Item("quantity")) * CDec(ds.Tables("detail").Rows(i).Item("price"))
                    End If
                    If ds.Tables("detail").Rows(i).Item("show") = True And _
                        IsNumeric(ds.Tables("detail").Rows(i).Item("quantity")) And _
                        IsNumeric(ds.Tables("detail").Rows(i).Item("price")) Then
                        invoiceTotal += CDec(ds.Tables("detail").Rows(i).Item("quantity")) * CDec(ds.Tables("detail").Rows(i).Item("price"))
                    End If
                Next
            End If
            If invoiceTotal <> distributedTotal Then
                lblDistributedTotal.CssClass = "normalred"
            End If
            lblDistributedTotal.Text = Format(distributedTotal, "$#,###.00")
            lblInvoiceTotal.Text = Format(invoiceTotal, "$#,###.00")
            i = 0
            Dim depositTotal As Decimal = 0
            If ds.Tables.Contains("deposit") Then
                For i = 0 To ds.Tables("deposit").Rows.Count - 1
                    depositTotal += CDec(ds.Tables("deposit").Rows(i).Item("check_amount"))
                Next
            End If

            lblDepositTotal.Text = Format(depositTotal, "$#,###.00")
            lblInvoiceBalance.Text = Format(invoiceTotal - depositTotal, "$#,###.00")

            If ds.Tables.Contains("detail") Then
                '  Dim s As String = ds.Tables("detail").Columns("order").DataType.ToString
                Dim dv As New DataView(ds.Tables("detail"))
                dv.Sort = "order"
                dgInvoiceDetail.DataSource = dv
                dgInvoiceDetail.DataBind()
            Else
                Dim oTable As New DataTable("detail")
                oTable.Columns.Add("id")
                oTable.Columns.Add("order")
                oTable.Columns.Add("category")
                oTable.Columns.Add("account")
                oTable.Columns.Add("task")
                oTable.Columns.Add("department")
                oTable.Columns.Add("quantity")
                oTable.Columns.Add("line_description")
                oTable.Columns.Add("price")
                ds.Tables.Add(oTable)
                dgInvoiceDetail.DataSource = ds.Tables("detail")
                dgInvoiceDetail.DataBind()
            End If
        Catch ex As Exception
            lblErrUpdate.Text = ex.ToString
        Finally
            ds = Nothing
        End Try
        If cmbCustomer.SelectedItem.Value = "" Or dgInvoiceDetail.Items.Count = 0 Then
            btnPrint.Visible = False
        Else
            btnPrint.Visible = True
            btnPrint.NavigateUrl = "http://sql08-02/ReportServer/Pages/ReportViewer.aspx?http%3a%2f%2fhome.reckner.com%2fAnnouncements%2fReports%2fInvoice%2fInvoice.rdl&rs:Command=Render&invoice_id=" & CStr(lblInvoiceID.Text)
        End If
        writeToBeValid()
    End Sub
    Private Sub writeToBeValid()
        Dim err As Exception
        Try
            If cmbCustomer.SelectedItem.Value = "" Then
                lblErrUpdate.Text = lblErrUpdate.Text + "<br>" + "Please select a customer."
            End If
            If cmbInvoiceType.SelectedItem.Value = "" Then
                lblErrUpdate.Text = lblErrUpdate.Text + "<br>" + "Please select an invoice type."
            End If
            If txtAttention.Text = "" Then
                lblErrUpdate.Text = lblErrUpdate.Text + "<br>" + "Please enter a attention line."
            End If
            If txtJobDescription.Text = "" Then
                lblErrUpdate.Text = lblErrUpdate.Text + "<br>" + "Please enter a job description."
            End If
            If CDec(lblDistributedTotal.Text) <> CDec(lblInvoiceTotal.Text) Then
                lblErrUpdate.Text = lblErrUpdate.Text + "<br>" + "Please make the distributed total equal the invoice total."
            End If
        Catch err
            lblErrUpdate.Text = lblErrUpdate.Text + "<br>" + err.ToString
        End Try
    End Sub
    Private Sub copy_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCopyInvoice.Click
        Response.Redirect(Request.ApplicationPath + "/production/copyInvoice.aspx?id=" + lblInvoiceID.Text + "&num=" + lblInvoiceJobNum.Text)
    End Sub
End Class
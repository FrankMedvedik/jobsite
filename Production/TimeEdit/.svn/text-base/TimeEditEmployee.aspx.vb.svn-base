Public Class TimeEditEmployee
    Inherits System.Web.UI.Page
    Private totalHours As Single
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            loadPVs()
        End If
    End Sub
    Private Sub loadPVs()
        Dim err As Exception
        Try
            Dim conSql As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
            Dim cmdSql As New SqlCommand("sp_EditTimeValues", conSql)
            cmdSql.CommandType = CommandType.StoredProcedure
            conSql.Open()
            Dim dtrEdit As SqlDataReader
            dtrEdit = cmdSql.ExecuteReader

            ddlEmployeeID.DataSource = dtrEdit
            ddlEmployeeID.DataTextField = "name"
            ddlEmployeeID.DataValueField = "empid"
            ddlEmployeeID.DataBind()

            dtrEdit.NextResult()
            ddlJobNumber.DataSource = dtrEdit
            ddlJobNumber.DataTextField = "jobnum"
            ddlJobNumber.DataValueField = "jobnum"
            ddlJobNumber.DataBind()

            dtrEdit.NextResult()
            dtrEdit.Read()
            txtStart.Text = dtrEdit(0)
            Dim strHolding As String
            Do While dtrEdit.Read
                strHolding = dtrEdit(0)
            Loop
            txtEnd.Text = strHolding
            conSql.Close()
            viewstate("PeriodStart") = CDate(txtStart.Text)
            viewstate("PeriodEnd") = CDate(txtEnd.Text)
        Catch err
            lblValidation.Text = err.ToString
        End Try

    End Sub
    Protected Function inPeriod(ByVal myDate As Date) As Boolean
        Dim isIn As Boolean = False
        Dim dPeriodStart = CDate(viewstate("PeriodStart"))
        Dim dPeriodEnd = CDate(viewstate("PeriodEnd"))

        If myDate >= dPeriodStart And myDate <= dPeriodEnd Then
            isIn = True
        End If
        Return isIn
    End Function
    Private Sub bindDataGrid()

        Dim err As Exception
        Try
            Dim dtrQA As SqlDataReader

            Dim conQA As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
            Dim cmdQA As New SqlCommand("sp_EmployeeTime2", conQA)

            cmdQA.CommandType = CommandType.StoredProcedure

            Dim prmEmpID As New SqlParameter("@EmpID", SqlDbType.Char, 8)
            prmEmpID.Value = ddlEmployeeID.SelectedItem.Value
            cmdQA.Parameters.Add(prmEmpID)

            Dim prmStartDate As New SqlParameter("@StartDate", SqlDbType.SmallDateTime)
            prmStartDate.Value = txtStart.Text
            cmdQA.Parameters.Add(prmStartDate)

            Dim prmEndDate As New SqlParameter("@EndDate", SqlDbType.SmallDateTime)
            prmEndDate.Value = txtEnd.Text
            cmdQA.Parameters.Add(prmEndDate)

            conQA.Open()

            Dim adpt As New SqlDataAdapter(cmdQA)
            Dim ds As New DataSet()
            adpt.Fill(ds)

            conQA.Close()

            Dim i As Integer
            Dim dblTotalHours As Double
            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dblTotalHours += ds.Tables(0).Rows(i).Item("Hours")
                Next
            End If
            totalHours = dblTotalHours

            grid1.DataSource = ds.Tables(0).DefaultView
            grid1.DataBind()

            conQA.Close()
            lblValidation.Text = ""
        Catch err
            lblValidation.Text = err.ToString
        End Try
    End Sub
    Protected Function getTotal() As String
        Return String.Format("Total: {0:n}", totalHours)
    End Function
    Protected Function getHrsDataSource() As DataView
        Dim err As Exception
        Try
            Dim ds As New DataSet()
            ds.ReadXml(Server.MapPath("xml/hour_type.xml"))
            Dim dv As New DataView(ds.Tables(0))
            dv.Sort = "hour_code"
            getHrsDataSource = dv
        Catch err
            lblValidation.Text = err.ToString
            getHrsDataSource = Nothing
        End Try
    End Function

    Protected Function getHrsSelectedIndex(ByVal strHrsType As String) As Integer
        Dim err As Exception
        Try
            Dim ds As New DataSet()
            ds.ReadXml(Server.MapPath("proadmin/hour_type.xml"))
            Dim dvwXML As New DataView(ds.Tables(0))
            dvwXML.Sort = "hour_code"
            getHrsSelectedIndex = dvwXML.Find(strHrsType)
        Catch err
            lblValidation.Text = err.ToString
            getHrsSelectedIndex = -1
        End Try
    End Function

    Private Sub btnGetTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTime.Click
        If Not IsDate(txtStart.Text) Or Len(txtStart.Text) = 0 Then
            lblValidation.Text = "You did not enter a valid start date."
        ElseIf Not IsDate(txtEnd.Text) Or Len(txtEnd.Text) = 0 Then
            lblValidation.Text = "You did not enter a valid end date."
        Else
            lblValidation.Text = ""
            ddlEmployeeID.Enabled = False
            txtStart.Enabled = False
            txtEnd.Enabled = False
            grid1.Visible = True
            bindDataGrid()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        lblValidation.Text = ""
        ddlEmployeeID.Enabled = True
        txtStart.Enabled = True
        txtEnd.Enabled = True
        grid1.EditItemIndex = -1
        grid1.Visible = False
        bindDataGrid()
    End Sub

    Private Function isValidRecord(ByVal sdate As String, ByVal sJobNumber As String, ByVal sHours As String) As Boolean
        Dim bValid As Boolean = True
        lblValidation.Text = ""
        If Not IsDate(sdate) Or Len(sdate) = 0 Then
            lblValidation.Text = lblValidation.Text + "You did not enter a valid date.  "
            bValid = False
        Else
            If Not inPeriod(CDate(sdate)) Then
                lblValidation.Text = lblValidation.Text + " The date you entered is not in the current payroll period."
                bValid = False
            End If
        End If
        Dim i As Integer
        Dim bJobFound As Boolean = False
        For i = 0 To ddlJobNumber.Items.Count - 1
            If sJobNumber = ddlJobNumber.Items(i).Value Then
                bJobFound = True
            End If
        Next
        If Not bJobFound Or Len(sJobNumber) = 0 Then
            lblValidation.Text = lblValidation.Text + "You did not enter a valid job number.  "
            bValid = False
        End If

        If Not IsNumeric(sHours) Then
            lblValidation.Text = lblValidation.Text + "Hours must be numeric."
            bValid = False
        Else
            If Decimal.Parse(sHours) > 16.0 Then
                lblValidation.Text = lblValidation.Text + "You can not enter more than 16 hours in any one entry."
                bValid = False
            End If
        End If

        Return bValid
    End Function

    Private Sub grid1_Click(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles grid1.ItemCommand

        Select Case UCase(e.CommandName)
            Case "ADD"
                Dim sDate As String
                Dim sJobNumber As String
                Dim sHours As String
                Dim sHrsType As String

                sDate = CType(e.Item.FindControl("txtNewDate"), TextBox).Text
                sJobNumber = CType(e.Item.FindControl("txtNewJobNumber"), TextBox).Text
                sHours = CType(e.Item.FindControl("txtNewHours"), TextBox).Text
                sHrsType = CType(e.Item.FindControl("ddlNewHrsType"), DropDownList).SelectedItem.Value

                If isValidRecord(sDate, sJobNumber, sHours) Then
                    Dim err As Exception
                    Try
                        Dim conQA As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                        Dim cmdQA As New SqlCommand("sp_AddTime", conQA)
                        cmdQA.CommandType = CommandType.StoredProcedure

                        Dim prmWorkDate As New SqlParameter("@workdate", SqlDbType.SmallDateTime)
                        prmWorkDate.Value = sDate
                        cmdQA.Parameters.Add(prmWorkDate)

                        Dim prmEmpID As New SqlParameter("@EmpID", SqlDbType.Char, 8)
                        prmEmpID.Value = ddlEmployeeID.SelectedItem.Value
                        cmdQA.Parameters.Add(prmEmpID)

                        Dim prmJobNum As New SqlParameter("@jobnum", SqlDbType.Int, 4)
                        prmJobNum.Value = sJobNumber
                        cmdQA.Parameters.Add(prmJobNum)

                        Dim prmHourNum As New SqlParameter("@hournum", SqlDbType.Decimal, 6)
                        prmHourNum.Value = sHours
                        cmdQA.Parameters.Add(prmHourNum)

                        Dim prmHourType As New SqlParameter("@hourtype", SqlDbType.Char, 15)
                        prmHourType.Value = sHrsType
                        cmdQA.Parameters.Add(prmHourType)

                        conQA.Open()
                        cmdQA.ExecuteNonQuery()
                        conQA.Close()

                        grid1.EditItemIndex = -1
                        lblValidation.Text = ""
                        bindDataGrid()
                    Catch err
                        lblValidation.Text = err.ToString
                        bindDataGrid()
                    End Try
                End If

            Case "UPDATE"
                Dim sDate As String
                Dim sJobNumber As String
                Dim sHours As String
                Dim sHrsType As String

                sDate = CType(e.Item.FindControl("txtDate"), TextBox).Text
                sJobNumber = CType(e.Item.FindControl("txtJobNumber"), TextBox).Text
                sHours = CType(e.Item.FindControl("txtHours"), TextBox).Text
                sHrsType = CType(e.Item.FindControl("ddlHrsType"), DropDownList).SelectedItem.Value

                If Decimal.Parse(sHours) < 16.25 Then
                    Dim err As Exception
                    Try
                        Dim conQA As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                        Dim cmdQA As New SqlCommand("UpdateTime", conQA)
                        cmdQA.CommandType = CommandType.StoredProcedure

                        Dim prmID As New SqlParameter("@ID", SqlDbType.Int, 4)
                        prmID.Value = grid1.DataKeys(e.Item.ItemIndex)
                        cmdqa.Parameters.Add(prmID)

                        Dim prmWorkDate As New SqlParameter("@date", SqlDbType.SmallDateTime)
                        prmWorkDate.Value = sDate
                        cmdQA.Parameters.Add(prmWorkDate)

                        Dim prmEmpID As New SqlParameter("@empid", SqlDbType.Char, 8)
                        prmEmpID.Value = ddlEmployeeID.SelectedItem.Value
                        cmdQA.Parameters.Add(prmEmpID)

                        Dim prmJobNum As New SqlParameter("@jobnum", SqlDbType.Int, 4)
                        prmJobNum.Value = sJobNumber
                        cmdQA.Parameters.Add(prmJobNum)

                        Dim prmHourNum As New SqlParameter("@hours", SqlDbType.Decimal, 6)
                        prmHourNum.Value = sHours
                        cmdQA.Parameters.Add(prmHourNum)

                        Dim prmHourType As New SqlParameter("@hrstype", SqlDbType.Char, 15)
                        prmHourType.Value = sHrsType
                        cmdQA.Parameters.Add(prmHourType)

                        conQA.Open()
                        cmdQA.ExecuteNonQuery()
                        conQA.Close()

                        grid1.EditItemIndex = -1
                        lblValidation.Text = ""
                        bindDataGrid()
                    Catch err
                        lblValidation.Text = err.ToString

                    End Try
                End If

            Case "DELETE"
                Dim err As Exception
                Try
                    Dim conQA As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                    Dim cmdQA As New SqlCommand("DeleteTime", conQA)
                    cmdQA.CommandType = CommandType.StoredProcedure

                    Dim prmID As New SqlParameter("@ID", SqlDbType.Int, 4)
                    prmID.Value = grid1.DataKeys(e.Item.ItemIndex)
                    cmdqa.Parameters.Add(prmID)

                    conQA.Open()
                    cmdQA.ExecuteNonQuery()
                    conQA.Close()

                    grid1.EditItemIndex = -1
                    bindDataGrid()
                Catch err
                    lblValidation.Text = err.tostring
                End Try

            Case "CANCEL"
                grid1.EditItemIndex = -1
                bindDataGrid()

            Case "EDIT"
                grid1.EditItemIndex = e.Item.ItemIndex
                bindDataGrid()
        End Select
    End Sub
End Class
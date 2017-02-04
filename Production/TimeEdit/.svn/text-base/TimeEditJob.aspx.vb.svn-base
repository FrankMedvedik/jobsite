Public Class TimeEditJob
    Inherits System.Web.UI.Page
    Private dblTotalHours As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            loadPVs()
        End If
    End Sub
    Private Sub loadPVs()
        '
        ' INIT grabs the values that we need to start the form
        '
        ' INIT conflicts with the component namespace - name changed to protect the innocent DR1212003
        '
        Dim err As Exception
        Try
            Dim conSql As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
            Dim cmdSql As New SqlCommand("sp_EditTimeValues", conSql)
            cmdSql.CommandType = CommandType.StoredProcedure
            conSql.Open()
            Dim dtrEdit As SqlDataReader
            dtrEdit = cmdSql.ExecuteReader

            ddlEmpIdHidden.DataSource = dtrEdit
            ddlEmpIdHidden.DataTextField = "empid"
            ddlEmpIdHidden.DataValueField = "empid"
            ddlEmpIdHidden.DataBind()

            dtrEdit.NextResult()

            Do While dtrEdit.Read
                ddlJobNumber.Items.Add(New ListItem(dtrEdit("jobnum") & " - " & dtrEdit("jobname"), dtrEdit("jobnum")))
            Loop

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
        Dim isPeriod As Boolean = False
        If myDate >= CDate(viewstate("PeriodStart")) And myDate <= CDate(viewstate("PeriodEnd")) Then
            isPeriod = True
        End If
        Return isPeriod
    End Function
    Protected Function getHrsDataSource() As DataView
        '
        ' returns a datatable to populate the Hours Type drop-down
        '
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
        '
        ' returns the selected index for the Hours Type drop-down
        '
        Dim err As Exception
        Try
            Dim ds As New DataSet()
            ds.ReadXml(Server.MapPath("proadmin/hour_type.xml"))
            Dim dvwXML As New DataView(ds.Tables(0))
            dvwXML.Sort = "hour_code"
            Return dvwXML.Find(strHrsType)
        Catch err
            lblValidation.Text = err.ToString
            getHrsSelectedIndex = -1
        End Try
    End Function
    Protected Function getTotal() As String
        Return String.Format("Total: {0:n}", dblTotalHours)
    End Function

    Private Sub bindDataGrid()
        Dim err As Exception
        Try
            Dim dtrQA As SqlDataReader
            Dim conQA As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
            Dim cmdQA As New SqlCommand("sp_JobTime2", conQA)

            cmdQA.CommandType = CommandType.StoredProcedure

            Dim prmJobNum As New SqlParameter("@JobNum", SqlDbType.Int, 4)
            prmJobNum.Value = ddlJobNumber.SelectedItem.Value
            cmdQA.Parameters.Add(prmJobNum)

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

            If ds.Tables(0).Rows.Count > 0 Then
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dblTotalHours += ds.Tables(0).Rows(i).Item("Hours")
                Next
            End If

            grid1.DataSource = ds
            grid1.DataBind()


        Catch err
            lblValidation.Text = err.ToString
        End Try
    End Sub

    Protected Function getJobNumber() As String
        getJobNumber = ddlJobNumber.SelectedItem.Value
    End Function

    Protected Function getEmpIDDataSource() As SqlDataReader
        Dim err As Exception
        Try
            Dim conSql As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
            Dim cmdSql As New SqlCommand("sp_EditTimeValues", conSql)
            cmdSql.CommandType = CommandType.StoredProcedure
            conSql.Open()
            Dim dtrEmpID As SqlDataReader
            dtrEmpID = cmdSql.ExecuteReader
            getEmpIDDataSource = dtrEmpID
        Catch err
            lblValidation.Text = err.ToString
            getEmpIDDataSource = Nothing
        End Try

    End Function

    Protected Function getEmpIDSelectedIndex(ByVal strEmpID As String) As Integer
        Dim retVal As Integer = -1
        For i = 0 To ddlEmpIdHidden.Items.Count - 1
            If ddlEmpIdHidden.Items(i).Value = strEmpID Then
                retVal = i
                Exit For
            End If
        Next
        getEmpIDSelectedIndex = retVal
    End Function

    Private Sub btnGetTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetTime.Click
        If Not IsDate(txtStart.Text) Or Len(txtStart.Text) = 0 Then
            lblValidation.Text = "You did not enter a valid start date."
        ElseIf Not IsDate(txtEnd.Text) Or Len(txtEnd.Text) = 0 Then
            lblValidation.Text = "You did not enter a valid end date."
        Else
            lblValidation.Text = ""
            ddlJobNumber.Enabled = False
            txtStart.Enabled = False
            txtEnd.Enabled = False
            btnGetTime.Enabled = False
            grid1.Visible = True
            bindDataGrid()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        lblValidation.Text = ""
        ddlJobNumber.Enabled = True
        txtStart.Enabled = True
        txtEnd.Enabled = True
        btnGetTime.Enabled = True
        grid1.EditItemIndex = -1
        grid1.Visible = False
    End Sub

    Private Function isValidRecord(ByVal sdate As String, ByVal sEmpID As String, ByVal sHours As String) As Boolean
        Dim bValid As Boolean = True

        If Not IsDate(sdate) Or Len(sdate) = 0 Then
            lblValidation.Text = lblValidation.Text + "You did not enter a valid date.  "
            bValid = False
        End If

        Dim i As Integer
        Dim bEmpFound As Boolean = False
        For i = 0 To ddlEmpIdHidden.Items.Count - 1
            If sEmpID = ddlEmpIdHidden.Items(i).Value Then
                bEmpFound = True
            End If
        Next
        If Not bEmpFound Or Len(sEmpID) = 0 Then
            lblValidation.Text = lblValidation.Text + "You did not enter a valid employee ID.  "
            bValid = False
        End If

        If Not IsNumeric(sHours) Then
            lblValidation.Text = lblValidation.Text + "You did not enter a valid number of hours.  "
            bValid = False
        Else
            If Decimal.Parse(sHours) > 16 Then
                lblValidation.Text = lblValidation.Text + " You can not enter more than 16 hours in any one entry."
                bValid = False
            End If
        End If

        Return bValid
    End Function

    Private Sub grid1_Click(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles grid1.ItemCommand

        Select Case UCase(e.CommandName)
            Case "ADD"
                Dim err As Exception
                Dim sDate As String
                Dim sEmpID As String
                Dim sHours As String
                Dim sHoursType As String

                sDate = CType(e.Item.FindControl("txtNewDate"), TextBox).Text
                sEmpID = CType(e.Item.FindControl("ddlNewEmpID"), DropDownList).SelectedItem.Value
                sHours = CType(e.Item.FindControl("txtNewHours"), TextBox).Text
                sHoursType = CType(e.Item.FindControl("ddlNewHrsType"), DropDownList).SelectedItem.Value

                If isValidRecord(sDate, sEmpID, sHours) Then
                    Try
                        Dim conSQL As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                        Dim cmdSQL As New SqlCommand("sp_AddTime", conSQL)
                        cmdSQL.CommandType = CommandType.StoredProcedure

                        Dim prmWorkDate As New SqlParameter("@workdate", SqlDbType.SmallDateTime)
                        prmWorkDate.Value = sDate
                        cmdSQL.Parameters.Add(prmWorkDate)

                        Dim prmEmpID As New SqlParameter("@empid", SqlDbType.Char, 8)
                        prmEmpID.Value = sEmpID
                        cmdSQL.Parameters.Add(prmEmpID)

                        Dim prmJobNum As New SqlParameter("@jobnum", SqlDbType.Int, 4)
                        prmJobNum.Value = ddlJobNumber.SelectedItem.Value
                        cmdSQL.Parameters.Add(prmJobNum)

                        Dim prmHourNum As New SqlParameter("@hournum", SqlDbType.Decimal, 6)
                        prmHourNum.Value = sHours
                        cmdSQL.Parameters.Add(prmHourNum)

                        Dim prmHourType As New SqlParameter("@hourtype", SqlDbType.Char, 15)
                        prmHourType.Value = sHoursType
                        cmdSQL.Parameters.Add(prmHourType)

                        conSQL.Open()
                        cmdSQL.ExecuteNonQuery()
                        conSQL.Close()

                        grid1.EditItemIndex = -1
                        lblValidation.Text = ""
                        bindDataGrid()
                    Catch err
                        lblValidation.Text = err.ToString
                    End Try
                End If

            Case "CANCEL"
                grid1.EditItemIndex = -1
                bindDataGrid()

            Case "EDIT"
                grid1.EditItemIndex = e.Item.ItemIndex
                bindDataGrid()

            Case "DELETE"
                Dim err As Exception
                Try
                    Dim conSQL As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                    Dim cmdSQL As New SqlCommand("DeleteTime", conSQL)
                    cmdSQL.CommandType = CommandType.StoredProcedure

                    Dim prmID As New SqlParameter("@ID", SqlDbType.Int, 4)
                    prmID.Value = grid1.DataKeys(e.Item.ItemIndex)
                    cmdsql.Parameters.Add(prmID)

                    conSQL.Open()
                    cmdSQL.ExecuteNonQuery()
                    conSQL.Close()

                    grid1.EditItemIndex = -1
                    bindDataGrid()
                    lblValidation.Text = ""
                Catch err
                    lblValidation.Text = err.ToString
                End Try

            Case "UPDATE"
                Dim err As Exception
                Dim sDate As String
                Dim sEmpID As String
                Dim sHours As String
                Dim sHoursType As String

                sDate = CType(e.Item.FindControl("txtDate"), TextBox).Text
                sEmpID = CType(e.Item.FindControl("txtEmpID"), TextBox).Text
                sHours = CType(e.Item.FindControl("txtHours"), TextBox).Text
                sHoursType = CType(e.Item.FindControl("ddlHrsType"), DropDownList).SelectedItem.Value
                If IsNumeric(shours) Then
                    If Decimal.Parse(shours) < 16.25 Then
                        Try
                            Dim conSQL As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                            Dim cmdSQL As New SqlCommand("UpdateTime", conSQL)
                            cmdSQL.CommandType = CommandType.StoredProcedure


                            Dim prmID As New SqlParameter("@ID", SqlDbType.Int, 4)
                            prmid.Value = grid1.DataKeys(e.Item.ItemIndex)
                            cmdSQL.Parameters.Add(prmid)

                            Dim prmWorkDate As New SqlParameter("@date", SqlDbType.SmallDateTime)
                            prmWorkDate.Value = sDate
                            cmdSQL.Parameters.Add(prmWorkDate)

                            Dim prmEmpID As New SqlParameter("@empid", SqlDbType.Char, 8)
                            prmEmpID.Value = sEmpID
                            cmdSQL.Parameters.Add(prmEmpID)

                            Dim prmJobNum As New SqlParameter("@jobnum", SqlDbType.Int, 4)
                            prmJobNum.Value = ddlJobNumber.SelectedItem.Value
                            cmdSQL.Parameters.Add(prmJobNum)

                            Dim prmHourNum As New SqlParameter("@hours", SqlDbType.Decimal, 6)
                            prmHourNum.Value = sHours
                            cmdSQL.Parameters.Add(prmHourNum)

                            Dim prmHourType As New SqlParameter("@hrstype", SqlDbType.Char, 15)
                            prmHourType.Value = sHoursType
                            cmdSQL.Parameters.Add(prmHourType)

                            conSQL.Open()
                            cmdSQL.ExecuteNonQuery()
                            conSQL.Close()

                            grid1.EditItemIndex = -1
                            lblValidation.Text = ""
                            bindDataGrid()
                        Catch err
                            lblValidation.Text = err.ToString
                        End Try
                    End If
                End If

        End Select
    End Sub
End Class
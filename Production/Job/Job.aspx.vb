﻿Imports System.Web.UI

Public Class Job
    Inherits System.Web.UI.Page


    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

      
            BindData()
            panelJob.Visible = False
            txtJobNum.Enabled = True
            txtJobNum.ReadOnly = False
            getJob.Visible = True
            lblName.Visible = False
            txtJobName.Visible = False
            lblactive.Visible = False
            chkJobactive.Visible = False
            updateJob.Visible = False
            If Not IsDBNull(Request.QueryString("jobnum")) Then
                txtJobNum.Text = Request.QueryString("jobnum")
                jobLookup()
            End If
        End If
    End Sub
    Private Sub BindData()
        'Load dropdown controls
        Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim cmd As New SqlCommand("JobValueList", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim ds As New DataSet
        Dim adpt As New SqlDataAdapter(cmd)
        cn.Open()
        adpt.Fill(ds)

        cmbProjectManager.DataSource = ds.Tables(0).DefaultView
        cmbassitantManager.DataSource = ds.Tables(0).DefaultView
        cmbScdMethod.DataSource = ds.Tables(1).DefaultView
        cmbProductCategory.DataSource = ds.Tables(2).DefaultView

        Page.DataBind()
        cmbProjectManager.Items.Insert(0, "")
        cmbassitantManager.Items.Insert(0, "")
        cmbProductCategory.Items.Insert(0, "")
        cmbScdMethod.Items.Insert(0, "")

        cn.Close()
    End Sub
    Private Function onlyNumbers(ByVal s As String) As String
        Dim i As Integer
        Dim sb As New Text.StringBuilder
        For i = 1 To Len(s)
            If IsNumeric(Mid(s, i, 1)) Then
                sb.Append(Mid(s, i, 1))
            End If
        Next
        Return sb.ToString
    End Function
    Private Sub saveXML()
        If Len(ViewState("currentJob")) > 0 Then
            errMessage.Text = ""
            Dim ds As New DataSet
            ds.ReadXmlSchema(getSchemaPath())
            Dim xml As New XmlDataDocument(ds)
            xml.LoadXml(ViewState("currentJob"))
            ds.Tables("job").Rows(0).Item("name") = txtJobName.Text
            ds.Tables("job").Rows(0).Item("clientid") = txtClientID.Text
            ds.Tables("client").Rows(0).Item("name") = txtClientName.Text
            ds.Tables("client").Rows(0).Item("address") = txtClientaddress.Text
            ds.Tables("client").Rows(0).Item("city") = txtClientCity.Text
            ds.Tables("client").Rows(0).Item("state") = txtClientState.Text
            ds.Tables("client").Rows(0).Item("zip") = txtClientZip.Text
            ds.Tables("client").Rows(0).Item("attention") = txtClientattention.Text
            ds.Tables("client").Rows(0).Item("phone") = onlyNumbers(txtClientPhoneNumber.Text)
            ds.Tables("client").Rows(0).Item("fax") = onlyNumbers(txtClientFaxNumber.Text)
            ds.Tables("client").Rows(0).Item("country") = txtClientCountry.Text

            ds.Tables("job").Rows(0).Item("active") = chkJobactive.Checked
            ds.Tables("job").Rows(0).Item("project_manager") = cmbProjectManager.SelectedItem.Value
            ds.Tables(0).Rows(0).Item("assistant_manager") = cmbassitantManager.SelectedItem.Value
            If IsDate(txtStartdate.Text) Then ds.Tables("job").Rows(0).Item("start_date") = txtStartdate.Text
            If IsDate(txtEndDate.Text) Then ds.Tables("job").Rows(0).Item("end_date") = txtEndDate.Text
            ds.Tables("job").Rows(0).Item("primary_method") = cmbPriMethod.SelectedItem.Value
            ds.Tables("job").Rows(0).Item("second_method") = cmbScdMethod.SelectedItem.Value
            If IsNumeric(txtProductionRate.Text) Then ds.Tables("job").Rows(0).Item("production_rate") = txtProductionRate.Text
            ' If IsNumeric(txtTotal.Text) Then 
            ds.Tables("job").Rows(0).Item("total_value") = 0 'txtTotal.Text remove for new job income distribution function
            ds.Tables("job").Rows(0).Item("location_code") = locationWrite()
            ds.Tables("job").Rows(0).Item("client_job_num") = txtClientJobNumber.Text
            ds.Tables("job").Rows(0).Item("po_num") = txtClientPoNumber.Text
            ds.Tables("job").Rows(0).Item("product_code") = cmbProductCategory.SelectedItem.Value
            ds.Tables(0).Rows(0).Item("master_job_num") = txtMasterJobNumber.Text
            ds.Tables(0).Rows(0).Item("invoice_num") = txtInvoiceNumber.Text
            ViewState("currentJob") = ds.GetXml
        End If
    End Sub
    Private Sub bindXML()
        Dim err As Exception
        If Len(ViewState("currentJob")) > 0 Then

            ' Code added to fail gracefully is there is now row, i.e. if the job number entered does not exist.
            ' "dim ds" statement moved out of TRY block so that ds would be visible to CATCH block
            '
            Dim ds As New DataSet
            Try
                ds.ReadXmlSchema(getSchemaPath())
                Dim xml As New XmlDataDocument(ds)
                xml.LoadXml(ViewState("currentJob"))

                If Not IsDBNull(ds.Tables("job").Rows(0).Item("name")) Then
                    txtJobName.Text = ds.Tables("job").Rows(0).Item("name")
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("clientid")) Then
                    txtClientID.Text = ds.Tables("job").Rows(0).Item("clientid")
                End If
                If Not IsDBNull(ds.Tables("client").Rows(0).Item("name")) Then
                    txtClientName.Text = ds.Tables("client").Rows(0).Item("name")
                End If
                If Not IsDBNull(ds.Tables("client").Rows(0).Item("address")) Then
                    txtClientaddress.Text = ds.Tables("client").Rows(0).Item("address")
                End If
                If Not IsDBNull(ds.Tables("client").Rows(0).Item("city")) Then
                    txtClientCity.Text = ds.Tables("client").Rows(0).Item("city")
                End If
                If Not IsDBNull(ds.Tables("client").Rows(0).Item("state")) Then
                    txtClientState.Text = ds.Tables("client").Rows(0).Item("state")
                End If
                If Not IsDBNull(ds.Tables("client").Rows(0).Item("zip")) Then
                    txtClientZip.Text = ds.Tables("client").Rows(0).Item("zip")
                End If
                If Not IsDBNull(ds.Tables("client").Rows(0).Item("attention")) Then
                    txtClientattention.Text = ds.Tables("client").Rows(0).Item("attention")
                End If
                If Not IsDBNull(ds.Tables("client").Rows(0).Item("phone")) Then
                    If Len(ds.Tables("client").Rows(0).Item("phone")) > 1 Then
                        txtClientPhoneNumber.Text = CDbl(ds.Tables("client").Rows(0).Item("phone")).ToString("(###)###-####")
                    End If
                End If
                If Not IsDBNull(ds.Tables("client").Rows(0).Item("fax")) Then
                    If Len(ds.Tables("client").Rows(0).Item("fax")) > 1 Then
                        txtClientFaxNumber.Text = CDbl(ds.Tables("client").Rows(0).Item("fax")).ToString("(###)###-####")
                    End If
                End If
                If Not IsDBNull(ds.Tables("client").Rows(0).Item("country")) Then
                    txtClientCountry.Text = ds.Tables("client").Rows(0).Item("country")
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("active")) Then
                    chkJobactive.Checked = CBool(ds.Tables("job").Rows(0).Item("active"))
                End If
                dgParts.DataSource = ds.Tables("job_detail")
                dgParts.DataBind()
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("project_manager")) Then
                    cmbProjectManager.SelectedIndex = cmbProjectManager.Items.IndexOf(cmbProjectManager.Items.FindByText(ds.Tables("job").Rows(0).Item("project_manager")))
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("assistant_manager")) Then
                    cmbassitantManager.SelectedIndex = cmbassitantManager.Items.IndexOf(cmbassitantManager.Items.FindByText(ds.Tables(0).Rows(0).Item("assistant_manager")))
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("start_date")) Then
                    txtStartdate.Text = FormatDateTime(ds.Tables("job").Rows(0).Item("start_date"), DateFormat.ShortDate)
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("end_date")) Then
                    txtEndDate.Text = FormatDateTime(ds.Tables("job").Rows(0).Item("end_date"), DateFormat.ShortDate)
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("primary_method")) Then
                    cmbPriMethod.SelectedIndex = cmbPriMethod.Items.IndexOf(cmbPriMethod.Items.FindByValue(ds.Tables("job").Rows(0).Item("primary_method")))
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("second_method")) Then
                    cmbScdMethod.SelectedIndex = cmbScdMethod.Items.IndexOf(cmbScdMethod.Items.FindByValue(ds.Tables("job").Rows(0).Item("second_method")))
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("production_rate")) Then
                    txtProductionRate.Text = ds.Tables("job").Rows(0).Item("production_rate")
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("total_value")) Then
                    txtTotal.Text = ds.Tables("job").Rows(0).Item("total_value")
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("location_code")) Then
                    ClickLocation(ds.Tables("job").Rows(0).Item("location_code"))
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("client_job_num")) Then
                    txtClientJobNumber.Text = ds.Tables("job").Rows(0).Item("client_job_num")
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("po_num")) Then
                    txtClientPoNumber.Text = ds.Tables("job").Rows(0).Item("po_num")
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("product_code")) Then
                    cmbProductCategory.SelectedIndex = cmbProductCategory.Items.IndexOf(cmbProductCategory.Items.FindByText(ds.Tables("job").Rows(0).Item("product_code")))
                End If
                If Not IsDBNull(ds.Tables("job").Rows(0).Item("master_job_num")) Then
                    txtMasterJobNumber.Text = ds.Tables(0).Rows(0).Item("master_job_num")
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("invoice_num")) Then
                    txtInvoiceNumber.Text = ds.Tables(0).Rows(0).Item("invoice_num")
                End If
                If ds.Tables.Contains("note") Then
                    dgNotes.DataSource = ds.Tables("note").DefaultView
                    dgNotes.DataBind()
                End If
                panelJob.Visible = True
                txtJobNum.Enabled = False
                txtJobNum.ReadOnly = True
                getJob.Visible = False
                lblName.Visible = True
                txtJobName.Visible = True
                lblactive.Visible = True
                chkJobactive.Visible = True
                updateJob.Visible = True
                createJob.Visible = False
                errMessage.Text = ""
            Catch err
                If ds.Tables("job").Rows.Count = 0 Then
                    errMessage.Text = "The job number you entered does not exist."
                Else
                    errMessage.Text = err.ToString
                End If
            End Try
        End If
    End Sub
    Private Sub getJob_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles getJob.Click
        jobLookup()
        jobIncomeLookup()
    End Sub
    Private Sub jobLookup()
        Dim path As String = getSchemaPath()
        If Not IsNumeric(txtJobNum.Text) Or Len(txtJobNum.Text) = 0 Then '
            errMessage.Text = "You did not enter a numeric job number."
        Else
            errMessage.Text = ""
            Dim oSQLXML As New SqlXmlCommand(WebConfigurationManager.AppSettings("xmlProback")) '"Provider=SQLOLEDB;Server=SQL08-02;database=proback;user id=sa;password=louis59")
            oSQLXML.RootTag = "jobs"
            oSQLXML.CommandText = "job[num='" & txtJobNum.Text & "']"
            oSQLXML.CommandType = SqlXmlCommandType.XPath
            oSQLXML.SchemaPath = path
            Dim ad As New SqlXmlAdapter(oSQLXML)
            Dim ds As New DataSet
            ds.ReadXmlSchema(path)
            Dim err As Exception

            Try
                ad.Fill(ds)
                ViewState("currentJob") = ds.GetXml
                bindXML()
            Catch err
                errMessage.Text = "Error Loading Data: " & err.ToString
            Finally
                oSQLXML = Nothing
            End Try
        End If
    End Sub
    Private Sub jobIncomeLookup()
        Dim cn As SqlConnection = New SqlConnection(WebConfigurationManager.AppSettings("ProbackConnectionString"))
        cn.Open()
        Dim cm As SqlCommand = cn.CreateCommand
        With cm
            .CommandText = "GetJobIncomeDistribution"
            .CommandType = CommandType.StoredProcedure
            .Parameters.Add(New SqlParameter("@JobNum", Me.txtJobNum.Text))

            Try
                Dim dr As SqlDataReader = .ExecuteReader
                While dr.Read
                    Me.txtJRaTotal.Text = dr("JRAIncome")
                    Me.txtSubcontractorTotal.Text = dr("SubcontractorIncome")
                    Me.txtHonorariaTotal.Text = dr("HonorariaIncome")
                    Me.txtJobTotal.Text = CDbl(dr("JRAIncome")) + CDbl(dr("SubcontractorIncome")) + CDbl(dr("HonorariaIncome"))
                    Me.cmbBrand.SelectedIndex = Me.cmbBrand.Items.IndexOf(Me.cmbBrand.Items.FindByText(Trim(dr("Brand"))))
                    Exit While
                End While
            Finally
                cn.Close()
            End Try
        End With
        If Me.txtJRaTotal.Text = "0" Then
            Me.txtJRaTotal.Text = Me.txtTotal.Text
        End If


    End Sub
    Private Sub ClickLocation(ByVal iLocation As Integer)

        chkMOP.Checked = False
        chkMall.Checked = False
        chk1600.Checked = False
        chkExton.Checked = False
        chkMtLaurel.Checked = False
        chkFieldManagement.Checked = False
        chkFieldaudit.Checked = False
        chkWesTest.Checked = False
        chkRacine.Checked = False
        chkOther.Checked = False
        chkDCMC.Checked = False
        chkMilwaukee.Checked = False
        chkCorporate.Checked = False
        chkGHRS.Checked = False
        chkFacilityMgmt.Checked = False
        chkConsumerInsights.Checked = False
        chkISR.Checked = False

        Do Until iLocation = 0
            If iLocation = 2 Then
                chkMOP.Checked = True
                iLocation = iLocation - 2
            ElseIf iLocation < 8 Then
                chkMall.Checked = True
                iLocation = iLocation - 4
            ElseIf iLocation < 16 Then
                chk1600.Checked = True
                iLocation = iLocation - 8
            ElseIf iLocation < 32 Then
                chkExton.Checked = True
                iLocation = iLocation - 16
            ElseIf iLocation < 64 Then
                chkMtLaurel.Checked = True
                iLocation = iLocation - 32
            ElseIf iLocation < 128 Then
                chkFieldManagement.Checked = True
                iLocation = iLocation - 64
            ElseIf iLocation < 256 Then
                chkOther.Checked = True
                iLocation = iLocation - 128
            ElseIf iLocation < 512 Then
                chkFieldaudit.Checked = True
                iLocation = iLocation - 256
            ElseIf iLocation < 1024 Then
                chkWesTest.Checked = True
                iLocation = iLocation - 512
            ElseIf iLocation < 2048 Then
                chkRacine.Checked = True
                iLocation = iLocation - 1024
            ElseIf iLocation < 4096 Then
                chkDCMC.Checked = True
                iLocation = iLocation - 2048
            ElseIf iLocation < 8192 Then
                chkMilwaukee.Checked = True
                iLocation = iLocation - 4096
            ElseIf iLocation < 16384 Then
                chkCorporate.Checked = True
                iLocation = iLocation - 8192
            ElseIf iLocation < 32768 Then
                chkGHRS.Checked = True
                iLocation = iLocation - 16384
            ElseIf iLocation < 65536 Then
                chkFacilityMgmt.Checked = True
                iLocation = iLocation - 32768
            ElseIf iLocation < 131072 Then
                chkConsumerInsights.Checked = True
                iLocation = iLocation - 65536
            ElseIf iLocation < 262144 Then
                chkISR.Checked = True
                iLocation = iLocation - 131072
            End If
        Loop
    End Sub
    Private Function locationWrite() As Integer
        Dim iLocation As Integer = 0

        If chkMOP.Checked Then iLocation = iLocation + 2
        If chkMall.Checked Then iLocation = iLocation + 4
        If chk1600.Checked Then iLocation = iLocation + 8
        If chkExton.Checked Then iLocation = iLocation + 16
        If chkMtLaurel.Checked Then iLocation = iLocation + 32
        If chkFieldManagement.Checked Then iLocation = iLocation + 64
        If chkOther.Checked Then iLocation = iLocation + 128
        If chkFieldaudit.Checked Then iLocation = iLocation + 256
        If chkWesTest.Checked Then iLocation = iLocation + 512
        If chkRacine.Checked Then iLocation = iLocation + 1024
        If chkDCMC.Checked Then iLocation = iLocation + 2048
        If chkMilwaukee.Checked Then iLocation = iLocation + 4096
        If chkCorporate.Checked Then iLocation = iLocation + 8192
        If chkGHRS.Checked Then iLocation = iLocation + 16384
        If chkFacilityMgmt.Checked Then iLocation = iLocation + 32768
        If chkConsumerInsights.Checked Then iLocation = iLocation + 65536
        If chkISR.Checked Then iLocation = iLocation + 131072
        Return iLocation
    End Function
    Private Function getSchemaPath() As String
        Dim schemaPath As String = Server.MapPath(".")
        If schemaPath.IndexOf("production") > 0 Then
            schemaPath += "\schema\job.xsd"
        Else
            schemaPath += "\schema\job.xsd"
        End If
        Return schemaPath
    End Function
    Private Sub dgNotes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgNotes.ItemCommand
        saveXML()
        Dim ds As New DataSet
        ds.ReadXmlSchema(getSchemaPath)
        Dim xml As New XmlDataDocument(ds)
        xml.LoadXml(ViewState("currentJob"))

        Select Case e.CommandName
            Case "add"
                Dim rw As DataRow
                rw = ds.Tables("note").NewRow()
                rw("note_date") = CDate(CType(e.Item.FindControl("txtAddNoteDate"), TextBox).Text)
                rw("note_text") = CStr(CType(e.Item.FindControl("txtAddNote"), TextBox).Text)
                If validNote(rw) Then
                    ds.Tables("note").Rows.Add(rw)
                    ViewState("currentJob") = ds.GetXml
                    bindXML()
                End If
            Case "cancel"
                dgNotes.EditItemIndex = -1
            Case "delete"
                Dim rw As DataRow
                rw = ds.Tables("note").Rows(e.Item.ItemIndex)
                ds.Tables("note").Rows.Remove(rw)
                ViewState("currentJob") = ds.GetXml
                bindXML()
            Case "edit"
                dgNotes.EditItemIndex = e.Item.ItemIndex
                bindXML()
            Case "update"
                ds.Tables("note").Rows(e.Item.ItemIndex).Item("note_date") = CDate(CType(e.Item.FindControl("txtEditNoteDate"), TextBox).Text)
                ds.Tables("note").Rows(e.Item.ItemIndex).Item("note_text") = CStr(CType(e.Item.FindControl("txtEditNote"), TextBox).Text)
                If validNote(ds.Tables("note").Rows(e.Item.ItemIndex)) Then
                    ViewState("currentJob") = ds.GetXml
                    dgNotes.EditItemIndex = -1
                    bindXML()
                End If
        End Select
    End Sub
    Private Sub dgParts_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgParts.ItemCommand
        saveXML()
        Dim ds As New DataSet
        ds.ReadXmlSchema(getSchemaPath())
        Dim xml As New XmlDataDocument(ds)
        xml.LoadXml(ViewState("currentJob"))

        Select Case e.CommandName
            Case "add"
                Dim rw As DataRow
                rw = ds.Tables("job_detail").NewRow
                rw("part") = CType(e.Item.FindControl("txtAddPart"), TextBox).Text
                If Len(CType(e.Item.FindControl("txtAddIncidence"), TextBox).Text) > 0 Then rw("incidence") = CType(e.Item.FindControl("txtAddIncidence"), TextBox).Text
                If Len(CType(e.Item.FindControl("txtAddQuota"), TextBox).Text) > 0 Then rw("quota") = CType(e.Item.FindControl("txtAddQuota"), TextBox).Text
                If Len(CType(e.Item.FindControl("txtAddHours"), TextBox).Text) > 0 Then rw("hours") = CType(e.Item.FindControl("txtAddHours"), TextBox).Text
                If Len(CType(e.Item.FindControl("txtAddCPI"), TextBox).Text) > 0 Then rw("cpi") = CType(e.Item.FindControl("txtAddCpi"), TextBox).Text
                If Len(CType(e.Item.FindControl("txtAddCoop"), TextBox).Text) > 0 Then rw("coop") = CType(e.Item.FindControl("txtAddCoop"), TextBox).Text
                If Len(CType(e.Item.FindControl("txtAddLength"), TextBox).Text) > 0 Then rw("length") = CType(e.Item.FindControl("txtAddLength"), TextBox).Text
                If Len(CType(e.Item.FindControl("txtAddExt"), TextBox).Text) > 0 Then rw("ext") = CType(e.Item.FindControl("txtAddExt"), TextBox).Text
                If validPart(rw) Then
                    ds.Tables("job_detail").Rows.Add(rw)
                    ViewState("currentJob") = ds.GetXml
                    bindXML()
                End If
            Case "cancel"
                dgParts.EditItemIndex = -1
            Case "delete"
                Dim rw As DataRow
                rw = ds.Tables("job_detail").Rows(e.Item.ItemIndex)
                ds.Tables("job_detail").Rows.Remove(rw)
                ViewState("currentJob") = ds.GetXml
                bindXML()
            Case "edit"
                dgParts.EditItemIndex = e.Item.ItemIndex
                bindXML()
            Case "update"
                ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("part") = CStr(CType(e.Item.FindControl("txtEditPart"), TextBox).Text)

                If IsNumeric(CType(e.Item.FindControl("txtEditIncidence"), TextBox).Text) Then
                    ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("incidence") = CDbl(CType(e.Item.FindControl("txtEditIncidence"), TextBox).Text)
                Else
                    ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("incidence") = DBNull.Value
                End If

                If IsNumeric(CType(e.Item.FindControl("txtEditQuota"), TextBox).Text) Then
                    ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("quota") = CInt(CType(e.Item.FindControl("txtEditQuota"), TextBox).Text)
                Else
                    ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("quota") = DBNull.Value
                End If

                If IsNumeric(CType(e.Item.FindControl("txtEditHours"), TextBox).Text) Then
                    ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("hours") = CDbl(CType(e.Item.FindControl("txtEditHours"), TextBox).Text)
                Else
                    ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("hours") = DBNull.Value
                End If

                If IsNumeric(CType(e.Item.FindControl("txtEditCpi"), TextBox).Text) Then
                    ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("cpi") = CDbl(CType(e.Item.FindControl("txtEditCpi"), TextBox).Text)
                Else
                    ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("cpi") = DBNull.Value
                End If

                ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("coop") = CStr(CType(e.Item.FindControl("txtEditCoop"), TextBox).Text)
                ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("length") = CStr(CType(e.Item.FindControl("txtEditLength"), TextBox).Text)
                If IsNumeric(CType(e.Item.FindControl("txtEditExt"), TextBox).Text) Then
                    ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("ext") = CInt(CType(e.Item.FindControl("txtEditExt"), TextBox).Text)
                Else
                    ds.Tables("job_detail").Rows(e.Item.ItemIndex).Item("ext") = DBNull.Value
                End If

                If validPart(ds.Tables("job_detail").Rows(e.Item.ItemIndex)) Then
                    ViewState("currentJob") = ds.GetXml
                    Dim str As String = ViewState("currentJob")
                    dgParts.EditItemIndex = -1
                    bindXML()
                End If
        End Select
    End Sub
    Private Function createJobLog(ByVal rw As DataRow, ByRef cn As SqlConnection, ByRef trans As SqlTransaction) As Integer

        'Dim cn As New SqlConnection(ConfigurationSettings.AppSettings("ProAdminConnectionString"))
        Dim cmd As New SqlCommand("sp_CreateJob", cn)
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure

        Dim prmClientID As New SqlParameter("@ClientID", SqlDbType.VarChar, 8)
        prmClientID.Value = rw.Item("clientid")
        cmd.Parameters.Add(prmClientID)

        Dim prmJobName As New SqlParameter("@JobName", SqlDbType.VarChar, 50)
        prmJobName.Value = rw.Item("name")
        cmd.Parameters.Add(prmJobName)

        Dim prmActive As New SqlParameter("@Active", SqlDbType.Bit)
        prmActive.Value = rw.Item("active")
        cmd.Parameters.Add(prmActive)

        Dim prmPriMethod As New SqlParameter("@priMethod", SqlDbType.VarChar, 25)
        prmPriMethod.Value = rw.Item("primary_method")
        cmd.Parameters.Add(prmPriMethod)

        Dim prmScdMethod As New SqlParameter("@scdMethod", SqlDbType.VarChar, 25)
        prmScdMethod.Value = rw.Item("second_method")
        cmd.Parameters.Add(prmScdMethod)

        Dim prmStarted As New SqlParameter("@Started", SqlDbType.DateTime)
        prmStarted.Value = rw.Item("start_date")
        cmd.Parameters.Add(prmStarted)

        Dim prmEnded As New SqlParameter("@Ended", SqlDbType.DateTime)
        prmEnded.Value = rw.Item("end_date")
        cmd.Parameters.Add(prmEnded)

        Dim prmProjMgr As New SqlParameter("@ProjMgr", SqlDbType.VarChar, 50)
        prmProjMgr.Value = rw.Item("project_manager")
        cmd.Parameters.Add(prmProjMgr)

        Dim prmScdPrjMgr As New SqlParameter("@scdPrjMgr", SqlDbType.VarChar, 50)
        prmScdPrjMgr.Value = rw.Item("assistant_manager")
        cmd.Parameters.Add(prmScdPrjMgr)

        Dim prmProdRate As New SqlParameter("@ProdRate", SqlDbType.Real)
        prmProdRate.Value = rw.Item("production_rate")
        cmd.Parameters.Add(prmProdRate)

        Dim prmTotal As New SqlParameter("@Total", SqlDbType.Int, 4)
        prmTotal.Value = rw.Item("total_value")
        cmd.Parameters.Add(prmTotal)

        Dim prmLocation As New SqlParameter("@Location", SqlDbType.Int, 4)
        prmLocation.Value = rw.Item("location_code")
        cmd.Parameters.Add(prmLocation)

        Dim prmClientJob As New SqlParameter("@ClientJob", SqlDbType.VarChar, 50)
        prmClientJob.Value = rw.Item("client_job_num")
        cmd.Parameters.Add(prmClientJob)

        Dim prmPO As New SqlParameter("@PO", SqlDbType.VarChar, 50)
        prmPO.Value = rw.Item("po_num")
        cmd.Parameters.Add(prmPO)

        Dim prmProduct As New SqlParameter("@Product", SqlDbType.VarChar, 50)
        prmProduct.Value = rw.Item("product_code")
        cmd.Parameters.Add(prmProduct)

        Dim prmRefJobs As New SqlParameter("@RefJobs", SqlDbType.VarChar, 50)
        prmRefJobs.Value = rw.Item("master_job_num")
        cmd.Parameters.Add(prmRefJobs)

        Dim prmInvoice As New SqlParameter("@Invoice", SqlDbType.VarChar, 50)
        prmInvoice.Value = rw.Item("invoice_num")
        cmd.Parameters.Add(prmInvoice)

        Dim prmJobNum As New SqlParameter("@JobNum", SqlDbType.Int, 4)
        prmJobNum.Direction = ParameterDirection.Output
        cmd.Parameters.Add(prmJobNum)


        Dim i As Integer = -1
        Dim err As Exception
        Try
            'cn.Open()
            cmd.ExecuteNonQuery()

            i = cmd.Parameters("@JobNum").Value
            ' 
        Catch err
            errMessage.Text = err.ToString
            i = 0
        Finally
            ' cn.Close()
        End Try
        Return i
    End Function
    Private Function createJobDetail(ByVal jobnum As Integer, ByVal rw As DataRow, ByRef cn As SqlConnection, ByRef trans As SqlTransaction) As Integer
        'Dim cn As New SqlConnection(ConfigurationSettings.AppSettings("ProAdminConnectionString"))
        Dim cmd As New SqlCommand("sp_CreatePart", cn)
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure

        Dim prmJobNum As New SqlParameter("@JobNum", SqlDbType.Int, 4)
        prmJobNum.Value = jobnum
        cmd.Parameters.Add(prmJobNum)

        Dim prmPart As New SqlParameter("@Part", SqlDbType.VarChar, 25)
        prmPart.Value = rw.Item("part")
        cmd.Parameters.Add(prmPart)

        Dim prmIncidence As New SqlParameter("@Incidence", SqlDbType.Real)
        prmIncidence.Value = rw.Item("incidence")
        cmd.Parameters.Add(prmIncidence)

        Dim prmQuota As New SqlParameter("@Quota", SqlDbType.Int, 4)
        prmQuota.Value = rw.Item("quota")
        cmd.Parameters.Add(prmQuota)

        Dim prmHours As New SqlParameter("@Hours", SqlDbType.Real)
        prmHours.Value = rw.Item("hours")
        cmd.Parameters.Add(prmHours)

        Dim prmCPI As New SqlParameter("@CPI", SqlDbType.Decimal)
        prmCPI.Value = rw.Item("cpi")

        cmd.Parameters.Add(prmCPI)

        Dim prmCoop As New SqlParameter("@Coop", SqlDbType.VarChar, 25)
        prmCoop.Value = rw.Item("coop")
        cmd.Parameters.Add(prmCoop)

        Dim prmLength As New SqlParameter("@Length", SqlDbType.VarChar, 20)
        prmLength.Value = rw.Item("length")
        cmd.Parameters.Add(prmLength)

        Dim prmExtention As New SqlParameter("@Extention", SqlDbType.SmallInt)
        prmExtention.Value = rw.Item("ext")
        cmd.Parameters.Add(prmExtention)

        Dim i As Integer = -1
        Try
            'cn.Open()
            cmd.ExecuteNonQuery()
        Catch
            i = 0
        Finally
            'cn.Close()
        End Try
        Return i
    End Function
    Private Function createNote(ByVal jobnum As Integer, ByVal rw As DataRow, ByRef cn As SqlConnection, ByRef trans As SqlTransaction) As Integer
        'Dim cn As New SqlConnection(ConfigurationSettings.AppSettings("ProAdminConnectionString"))
        Dim cmd As New SqlCommand("sp_CreateNote", cn)
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure

        Dim prmJobNum As New SqlParameter("@JobNum", SqlDbType.Int, 4)
        prmJobNum.Value = jobnum
        cmd.Parameters.Add(prmJobNum)

        Dim prmNoteDate As New SqlParameter("@NoteDate", SqlDbType.DateTime)
        prmNoteDate.Value = rw.Item("note_date")
        cmd.Parameters.Add(prmNoteDate)

        Dim prmNotes As New SqlParameter("@Notes", SqlDbType.VarChar, 255)
        prmNotes.Value = rw.Item("note_text")
        cmd.Parameters.Add(prmNotes)

        Dim i As Integer = -1
        Try

            cmd.ExecuteNonQuery()
        Catch
            i = 0
        End Try
        Return i
    End Function
    Private Sub updateJobLog(ByVal rw As DataRow, ByRef cn As SqlConnection, ByRef trans As SqlTransaction)

        'Dim cn As New SqlConnection(ConfigurationSettings.AppSettings("ProAdminConnectionString"))
        Dim cmd As New SqlCommand("sp_UpdateJob", cn)
        cmd.Transaction = trans
        cmd.CommandType = CommandType.StoredProcedure

        Dim prmJobNum As New SqlParameter("@jobnum", SqlDbType.Int, 4)
        prmJobNum.Value = rw.Item("num")
        cmd.Parameters.Add(prmJobNum)

        Dim prmClientID As New SqlParameter("@ClientID", SqlDbType.VarChar, 8)
        prmClientID.Value = rw.Item("clientid")
        cmd.Parameters.Add(prmClientID)

        Dim prmJobName As New SqlParameter("@JobName", SqlDbType.VarChar, 50)
        prmJobName.Value = rw.Item("name")
        cmd.Parameters.Add(prmJobName)

        Dim prmActive As New SqlParameter("@Active", SqlDbType.Bit)
        prmActive.Value = rw.Item("active")
        cmd.Parameters.Add(prmActive)

        Dim prmPriMethod As New SqlParameter("@priMethod", SqlDbType.VarChar, 25)
        prmPriMethod.Value = rw.Item("primary_method")
        cmd.Parameters.Add(prmPriMethod)

        Dim prmScdMethod As New SqlParameter("@scdMethod", SqlDbType.VarChar, 25)
        prmScdMethod.Value = rw.Item("second_method")
        cmd.Parameters.Add(prmScdMethod)

        Dim prmStarted As New SqlParameter("@Started", SqlDbType.DateTime)
        prmStarted.Value = rw.Item("start_date")
        cmd.Parameters.Add(prmStarted)

        Dim prmEnded As New SqlParameter("@Ended", SqlDbType.DateTime)
        prmEnded.Value = rw.Item("end_date")
        cmd.Parameters.Add(prmEnded)

        Dim prmProjMgr As New SqlParameter("@ProjMgr", SqlDbType.VarChar, 50)
        prmProjMgr.Value = rw.Item("project_manager")
        cmd.Parameters.Add(prmProjMgr)

        Dim prmScdPrjMgr As New SqlParameter("@scdPrjMgr", SqlDbType.VarChar, 50)
        prmScdPrjMgr.Value = rw.Item("assistant_manager")
        cmd.Parameters.Add(prmScdPrjMgr)

        Dim prmProdRate As New SqlParameter("@ProdRate", SqlDbType.Real)
        prmProdRate.Value = rw.Item("production_rate")
        cmd.Parameters.Add(prmProdRate)

        Dim prmTotal As New SqlParameter("@Total", SqlDbType.Int, 4)
        prmTotal.Value = rw.Item("total_value")
        cmd.Parameters.Add(prmTotal)

        Dim prmLocation As New SqlParameter("@Location", SqlDbType.Int, 4)
        prmLocation.Value = rw.Item("location_code")
        cmd.Parameters.Add(prmLocation)

        Dim prmClientJob As New SqlParameter("@ClientJob", SqlDbType.VarChar, 50)
        prmClientJob.Value = rw.Item("client_job_num")
        cmd.Parameters.Add(prmClientJob)

        Dim prmPO As New SqlParameter("@PO", SqlDbType.VarChar, 50)
        prmPO.Value = rw.Item("po_num")
        cmd.Parameters.Add(prmPO)

        Dim prmProduct As New SqlParameter("@Product", SqlDbType.VarChar, 50)
        prmProduct.Value = rw.Item("product_code")
        cmd.Parameters.Add(prmProduct)

        Dim prmRefJobs As New SqlParameter("@RefJobs", SqlDbType.VarChar, 50)
        prmRefJobs.Value = rw.Item("master_job_num")
        cmd.Parameters.Add(prmRefJobs)

        Dim prmInvoice As New SqlParameter("@Invoice", SqlDbType.VarChar, 50)
        prmInvoice.Value = rw.Item("invoice_num")
        cmd.Parameters.Add(prmInvoice)


        cmd.ExecuteNonQuery()

    End Sub
    Private Sub clearParts(ByVal jobnum As Integer, ByRef cn As SqlConnection, ByRef trans As SqlTransaction)
        Dim cmd As New SqlCommand("DELETE joblogdtl WHERE jobnum = " & jobnum, cn)
        cmd.Transaction = trans
        ' cn.Open()
        cmd.ExecuteNonQuery()
        'cn.Close()

    End Sub
    Private Sub clearNotes(ByVal jobnum As Integer, ByRef cn As SqlConnection, ByRef trans As SqlTransaction)
        Dim cmd As New SqlCommand("DELETE notes WHERE jobnum = " & jobnum, cn)
        cmd.Transaction = trans
        'cn.Open()
        cmd.ExecuteNonQuery()
        'cn.Close()

    End Sub

    Dim jobYear As String
    Dim clientID As String
    Dim primaryMethod As String
    Dim secondMethod As String
    Dim jobNumber As String
    Dim startDate As Date
    Dim endDate As Date
    Dim jobName As String


    Private Sub updateJob_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles updateJob.Click
        If Len(ViewState("currentJob")) > 0 Then
            saveXML()
            Dim currentDS As New DataSet
            currentDS.ReadXmlSchema(getSchemaPath())
            Dim currentXML As New XmlDataDocument(currentDS)
            currentXML.LoadXml(ViewState("currentJob"))
            If validJob(currentDS) Then
                Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                cn.Open()

                Dim trans As SqlTransaction = cn.BeginTransaction(IsolationLevel.ReadCommitted, "myTransaction")
                Dim i As Integer = 0
                Dim err As Exception

                Try
                    If txtJobNum.Text = "" Then
                        i = createJobLog(currentDS.Tables("job").Rows(0), cn, trans)
                        Dim rw As DataRow
                        For Each rw In currentDS.Tables("job_detail").Rows
                            createJobDetail(i, rw, cn, trans)
                        Next
                        For Each rw In currentDS.Tables("note").Rows
                            createNote(i, rw, cn, trans)
                        Next
                        txtJobNum.Text = i
                        bindXML()
                        insertJobIncome(cn, trans)
                        jobYear = i.ToString
                        jobYear = jobYear.Substring(0, 4)
                        jobNumber = i.ToString()
                        jobNumber = jobNumber.Substring(4, 4)
                        rw = currentDS.Tables("job").Rows(0)
                        jobName = rw.Item("name")
                        primaryMethod = rw.Item("primary_method")
                        secondMethod = rw.Item("second_method")
                        startDate = CDate(rw.Item("start_date"))
                        endDate = CDate(rw.Item("end_date"))

                        If chkSharePoint.Checked Then
                            primaryMethod = "Blank SharePoint JobSite"
                        End If

                        SharePointHelper.CreateJobSiteBasedOnTemplate(jobYear, jobNumber, jobName, primaryMethod, secondMethod)


                    Else

                        updateJobLog(currentDS.Tables("job").Rows(0), cn, trans)

                        clearParts(txtJobNum.Text, cn, trans)
                        Dim rw As DataRow
                        For Each rw In currentDS.Tables("job_detail").Rows
                            createJobDetail(txtJobNum.Text, rw, cn, trans)
                        Next

                        clearNotes(txtJobNum.Text, cn, trans)
                        For Each rw In currentDS.Tables("note").Rows
                            createNote(txtJobNum.Text, rw, cn, trans)
                        Next
                        updateJobIncome(cn, trans)
                        rw = currentDS.Tables("job").Rows(0)
                        jobYear = rw.Item("num")
                        jobYear = jobYear.Substring(0, 4)
                        jobNumber = rw.Item("num")
                        jobNumber = jobNumber.Substring(4, 4)
                        jobName = rw.Item("name")
                        clientID = rw.Item("clientid")
                        primaryMethod = rw.Item("primary_method")
                        startDate = CDate(rw.Item("start_date"))
                        endDate = CDate(rw.Item("end_date"))


                    End If

                    trans.Commit()
                    cn.Close()
                    jobLookup()

         

                Catch err
                    errMessage.Text = err.Message
                    trans.Rollback()
                End Try


          


                errMessage.Text = "Job #" & jobNumber & " has been created"



            End If
        End If
    End Sub
    Private Sub createJobSite(ByVal yr As Integer, ByVal num As Integer, ByVal name As String, ByVal clientID As String, ByVal methodType As String, ByVal startDate As Date, ByVal endDate As Date)
        Dim svc As SharePointServicesClient = New SharePointServicesClient

        Dim rslt As String = svc.BuildSiteBasedOnTemplate(yr, num, name, clientID, methodType, startDate, endDate)
        If Not rslt = "Success" Then
            errMessage.Text = rslt
        End If
    End Sub
    Private Sub insertJobIncome(ByVal cn As SqlConnection, ByVal trn As SqlTransaction)
        Dim _jraTotal As Decimal = 0
        Dim _subContractorTotal As Decimal = 0
        Dim _honorariaTotal As Decimal = 0
        Dim _brand As String = ""

        If IsNumeric(Me.txtJRaTotal.Text) Then
            _jraTotal = Me.txtJRaTotal.Text
        End If

        If IsNumeric(Me.txtSubcontractorTotal.Text) Then
            _subContractorTotal = Me.txtSubcontractorTotal.Text
        End If

        If IsNumeric(Me.txtHonorariaTotal.Text) Then
            _honorariaTotal = Me.txtHonorariaTotal.Text
        End If

        _brand = Me.cmbBrand.SelectedValue

        'Dim cn As SqlConnection = New SqlConnection(ConfigurationSettings.AppSettings("ProbackConnectionString"))
        'cn.Open()
        Dim cm As SqlCommand = cn.CreateCommand
        With cm
            .CommandText = "InsertJobIncomeDistribution"
            .CommandType = CommandType.StoredProcedure
            .Transaction = trn
            .Parameters.Add(New SqlParameter("@JobNum", Me.txtJobNum.Text))
            .Parameters.Add(New SqlParameter("@JRAIncome", _jraTotal))
            .Parameters.Add(New SqlParameter("@SubcontractorIncome", _subContractorTotal))
            .Parameters.Add(New SqlParameter("@HonorariaIncome", _honorariaTotal))
            .Parameters.Add(New SqlParameter("@Brand", _brand))
            .ExecuteNonQuery()
            'Try
            '    .ExecuteNonQuery()
            'Finally
            '    cn.Close()
            'End Try

        End With

    End Sub
    Private Sub updateJobIncome(ByVal cn As SqlConnection, ByVal trn As SqlTransaction)
        Dim _jraTotal As Decimal = 0
        Dim _subContractorTotal As Decimal = 0
        Dim _honorariaTotal As Decimal = 0
        Dim _brand As String = ""

        If IsNumeric(Me.txtJRaTotal.Text) Then
            _jraTotal = Me.txtJRaTotal.Text
        End If

        If IsNumeric(Me.txtSubcontractorTotal.Text) Then
            _subContractorTotal = Me.txtSubcontractorTotal.Text
        End If

        If IsNumeric(Me.txtHonorariaTotal.Text) Then
            _honorariaTotal = Me.txtHonorariaTotal.Text
        End If

        _brand = Me.cmbBrand.SelectedValue

        'Dim cn As SqlConnection = New SqlConnection(ConfigurationSettings.AppSettings("ProbackConnectionString"))
        'cn.Open()
        Dim cm As SqlCommand = cn.CreateCommand
        With cm
            .CommandText = "UpdateJobIncomeDistribution"
            .CommandType = CommandType.StoredProcedure
            .Transaction = trn
            .Parameters.Add(New SqlParameter("@JobNum", Me.txtJobNum.Text))
            .Parameters.Add(New SqlParameter("@JRAIncome", _jraTotal))
            .Parameters.Add(New SqlParameter("@SubcontractorIncome", _subContractorTotal))
            .Parameters.Add(New SqlParameter("@HonorariaIncome", _honorariaTotal))
            .Parameters.Add(New SqlParameter("@Brand", _brand))
            .ExecuteNonQuery()
            'Try
            '    .ExecuteNonQuery()
            'Finally
            '    cn.Close()
            'End Try

        End With
    End Sub
    Private Sub createJob_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles createJob.Click
        Dim ds As New DataSet
        ds.ReadXmlSchema(getSchemaPath())
        Dim xml As New XmlDataDocument(ds)
        Dim newJob As String = Replace(getSchemaPath(), "job.xsd", "newJob.xml")

        xml.Load(newJob)
        Dim rw As DataRow
        rw = ds.Tables("job_detail").Rows(0)
        ds.Tables("job_detail").Rows.Remove(rw)
        ViewState("currentJob") = ds.GetXml
        bindXML()
    End Sub
    Private Function validJob(ByRef ds As DataSet) As Boolean
        Dim isvalid As Boolean = True
        Dim errString As New Text.StringBuilder

        If Len(txtClientID.Text) = 0 And Not isClient(txtClientID.Text) Then
            isvalid = False
            errString.Append(" Invalid Client ID ")
        End If
        If Len(txtJobName.Text) = 0 Then
            isvalid = False
            errString.Append(" Invalid Job Name ")
        End If
        If cmbProjectManager.SelectedItem.Value = "" Then
            isvalid = False
            errString.Append(" Please select a project manager ")
        End If
        If cmbPriMethod.SelectedItem.Value = "" Then
            isvalid = False
            errString.Append(" Please select a primary method ")
        End If
        If Not IsDate(txtStartdate.Text) And Not Len(txtStartdate.Text) = 0 Then
            isvalid = False
            errString.Append(" Please enter a valid start date ")
        End If
        If Not IsDate(txtEndDate.Text) Then
            isvalid = False
            errString.Append(" Please enter a valid end date ")
        End If
        If ds.Tables("job").Rows(0).Item("location_code") = 0 Then
            isvalid = False
            errString.Append(" Please select a location ")
        End If
        If ds.Tables("job_detail").Rows.Count = 0 Then
            isvalid = False
            errString.Append(" Please enter at least one part ")
        End If
        If Not IsNumeric(txtProductionRate.Text) Then
            isvalid = False
            errString.Append(" Please enter a numeric production rate ")
        End If
        'If Not isInt(txtTotal.Text) Then
        '    isvalid = False
        '    errString.Append(" Please enter a numeric total without a decimal ")
        'End If
        If Not Len(txtMasterJobNumber.Text) = 0 Then
            If IsNumeric(txtMasterJobNumber.Text) Then
                If Not txtMasterJobNumber.Text > 20000000 And Not txtMasterJobNumber.Text < txtJobNum.Text Then
                    isvalid = False
                    errString.Append(" Please enter a valid master jobnumber ")
                End If
            Else
                isvalid = False
                errString.Append(" Please enter a valid master jobnumber ")
            End If
        End If
        errMessage.Text = errMessage.Text + errString.ToString
        Return isvalid
    End Function
    Private Function isInt(ByVal value As String) As Boolean
        Dim bValid = False
        If IsNumeric(value) Then
            If CInt(value) = CType(value, Double) Then
                bValid = True
            End If
        End If
        Return bValid
    End Function
    Private Function isClient(ByVal clientid As String) As Boolean
        Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim cmd As New SqlCommand("IsClient", cn)
        cmd.CommandType = CommandType.StoredProcedure

        Dim prmClientID As New SqlParameter("@ClientID", SqlDbType.VarChar, 8)
        prmClientID.Value = clientid
        cmd.Parameters.Add(prmClientID)

        Dim prmRetVal As New SqlParameter("retval", SqlDbType.Int)
        prmRetVal.Direction = ParameterDirection.ReturnValue
        cmd.Parameters.Add(prmRetVal)

        cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
        Dim rtn As Boolean
        If cmd.Parameters.Item("retval").Value = -1 Then
            rtn = True
        Else
            rtn = False
        End If
        Return rtn

    End Function
    Private Function validPart(ByRef rw As DataRow) As Boolean
        Dim isValid As Boolean = True
        Dim errString As New Text.StringBuilder

        If Len(rw("part")) < 1 Or Len(rw("part")) > 25 Then
            errString.Append("Invalid Part Description")
            isValid = False
        End If

        If Not IsDBNull(rw("incidence")) Then
            If Not IsNumeric(rw("incidence")) Then
                errString.Append(" Invalid incidence ")
                isValid = False
            End If
        End If

        If Not IsDBNull(rw("quota")) Then
            If Not IsNumeric(rw("quota")) Then
                errString.Append(" Invalid quota ")
                isValid = False
            End If
        End If

        If Not IsDBNull(rw("incidence")) Then
            If Not IsNumeric(rw("hours")) Then
                errString.Append(" Invalid hours ")
                isValid = False
            End If
        End If

        If Not IsDBNull(rw("incidence")) Then
            If Not IsNumeric(rw("ext")) Then
                errString.Append(" Invalid Ext ")
                isValid = False
            End If
        End If

        errMessage.Text = errMessage.Text + errString.ToString
        Return isValid
    End Function
    Private Function validNote(ByRef rw As DataRow) As Boolean
        Dim isValid As Boolean = True
        Dim errString As New Text.StringBuilder

        If Not IsDBNull(rw("note_date")) Then
            If Not IsDate(rw("note_date")) Then
                errString.Append(" Invalid Note Date ")
                isValid = False
            End If
        Else
            errString.Append(" Invalid Note Date ")
            isValid = False
        End If

        If Len(rw("note_text")) = 0 Then
            errString.Append(" Invalid Note Text ")
            isValid = False
        End If

        errMessage.Text = errMessage.Text + errString.ToString
        Return isValid
    End Function
    Private Sub btnGetClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetClient.Click
        Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim cmd As New SqlCommand("sp_GetClient", cn)
        cmd.CommandType = CommandType.StoredProcedure

        Dim prmClientID As New SqlParameter("@ClientID", SqlDbType.VarChar, 8)
        prmClientID.Value = txtClientID.Text
        cmd.Parameters.Add(prmClientID)
        Dim err As Exception
        Try
            cn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If reader.Read Then
                If Not IsDBNull(reader("clientname")) Then txtClientName.Text = reader("clientname")
                If Not IsDBNull(reader("address")) Then txtClientaddress.Text = reader("address")
                If Not IsDBNull(reader("city")) Then txtClientCity.Text = reader("city")
                If Not IsDBNull(reader("state")) Then txtClientState.Text = reader("state")
                If Not IsDBNull(reader("zip")) Then txtClientZip.Text = reader("zip")
                If Not IsDBNull(reader("attention")) Then txtClientattention.Text = reader("attention")
                If Not IsDBNull(reader("phone")) Then txtClientPhoneNumber.Text = reader("phone")
                If Not IsDBNull(reader("fax")) Then txtClientFaxNumber.Text = reader("fax")
                If Not IsDBNull(reader("email")) Then txtClientEmail.Text = reader("email")
                If Not IsDBNull(reader("country")) Then txtClientCountry.Text = reader("country")
                saveXML()
                bindXML()
            Else
                errMessage.Text = "Client Not Found"
            End If
        Catch err
            errMessage.Text = err.ToString
        End Try

    End Sub
    Private Sub lnkClientShowHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkClientShowHide.Click
        pnlClient.Visible = Not pnlClient.Visible
        If lnkClientShowHide.Text = "(hide)" Then
            lnkClientShowHide.Text = "(show)"
        Else
            lnkClientShowHide.Text = "(hide)"
        End If
    End Sub

End Class
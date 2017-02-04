Public Class TimeEntry
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            loadLocation()
            topPanel.Visible = True
            bottomPanel.Visible = False
        End If
    End Sub
    Private Sub loadLocation()
        Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim cmd As New SqlCommand("SELECT Location, [location description] as description FROM location WHERE active = 1", cn)
        cn.Open()
        Dim dr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        cmbLocation.DataSource = dr
        cmbLocation.DataBind()

    End Sub


    Private Sub btnFillValues_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFillValues.Click
        Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim cmd As New SqlCommand("sp_TimeValues", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim prmLocation As New SqlParameter("@Location", SqlDbType.Int, 4)
        prmLocation.Value = cmbLocation.SelectedItem.Value
        cmd.Parameters.Add(prmLocation)
        Dim adpt As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()
        adpt.Fill(ds)

        Dim i As Integer
        For i = 0 To ds.Tables(2).Rows.Count - 1
            Dim sValue As String = Day(ds.Tables(2).Rows(i).Item(0)) & " - " & Str_Month(ds.Tables(2).Rows(i).Item(0))
            Dim sText As String = ds.Tables(2).Rows(i).Item(0)

            Dim iList As New ListItem(sValue, sText)
            cmbDate.Items.Add(iList)
        Next

        cmbEmployee.DataSource = ds.Tables(0).DefaultView
        cmbEmployee.DataBind()
        cmbEmployee.Items.Insert(0, "")
        cmbJobNum.Items.Insert(0, "")
        For i = 0 To ds.Tables(1).Rows.Count - 1
            Dim sValue As String = ds.Tables(1).Rows(i).Item(0)
            Dim sText As String = ds.Tables(1).Rows(i).Item(0) & " - " & ds.Tables(1).Rows(i).Item(2)
            Dim iList As New ListItem(stext, svalue)
            cmbJobNum.Items.Add(ilist)
        Next

        cmbHourType.DataSource = ds.Tables(3).DefaultView
        cmbHourType.DataBind()
        cmbHourType.Items.Insert(0, "")

        topPanel.Visible = False
        bottomPanel.Visible = True

    End Sub
    Private Function Str_Month(ByVal dDate)
        Dim Str
        Select Case Month(CDate(dDate))
            Case 1
                Str = "JAN"
            Case 2
                Str = "FEB"
            Case 3
                Str = "MAR"
            Case 4
                Str = "APR"
            Case 5
                Str = "MAY"
            Case 6
                Str = "JUN"
            Case 7
                Str = "JUL"
            Case 8
                Str = "AUG"
            Case 9
                Str = "SEP"
            Case 10
                Str = "OCT"
            Case 11
                Str = "NOV"
            Case 12
                Str = "DEC"
        End Select
        Str_Month = Str
    End Function

    Private Sub addTime(ByVal workDate As Date, ByVal empID As String, ByVal jobNum As Integer, ByVal hourNum As Decimal, ByVal hourType As String)
        Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
        Dim cmd As New SqlCommand("sp_AddTime", cn)
        cmd.CommandType = CommandType.StoredProcedure

        Dim prmWorkDate As New SqlParameter("@workdate", SqlDbType.SmallDateTime)
        prmWorkDate.Value = workDate
        cmd.Parameters.Add(prmWorkDate)

        Dim prmEmpID As New SqlParameter("@empid", SqlDbType.Char, 8)
        prmEmpID.Value = empID
        cmd.Parameters.Add(prmEmpID)

        Dim prmJobNum As New SqlParameter("@jobnum", SqlDbType.Int, 4)
        prmJobNum.Value = jobNum
        cmd.Parameters.Add(prmJobNum)

        Dim prmHourNum As New SqlParameter("@hournum", SqlDbType.Decimal, 4)
        prmHourNum.Value = hourNum
        cmd.Parameters.Add(prmHourNum)

        Dim prmHourType As New SqlParameter("@hourtype", SqlDbType.Char, 15)
        prmHourType.Value = hourType
        cmd.Parameters.Add(prmHourType)

        Dim err As Exception
        Try
            cn.Open()
            cmd.ExecuteNonQuery()
        Catch err
            Throw err
        Finally
            cn.Close()
        End Try

    End Sub
    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim s As String = Request.Form("xmlTimeString")

        If Len(s) > 0 Then
            Dim xmlTime As New System.Xml.XmlDocument()
            Dim err As Exception
            Try

                xmlTime.LoadXml(s)
                Dim workDate As XmlNodeList = xmlTime.GetElementsByTagName("workDate")
                Dim empID As XmlNodeList = xmlTime.GetElementsByTagName("empID")
                Dim jobNum As XmlNodeList = xmlTime.GetElementsByTagName("jobNum")
                Dim hourNum As XmlNodeList = xmlTime.GetElementsByTagName("hourNum")
                Dim hourType As XmlNodeList = xmlTime.GetElementsByTagName("hourType")

                Dim i As Integer = 0

                If workDate.Count > 0 Then
                    For i = 0 To workDate.Count - 1
                        addTime(workDate.Item(i).InnerText, _
                                empID.Item(i).InnerText, _
                                jobNum.Item(i).InnerText, _
                                hourNum.Item(i).InnerText, _
                                hourType.Item(i).InnerText)

                    Next
                    errMessage.Text = "all data loaded"

                End If
            Catch err
                errMessage.Text = err.ToString
            End Try
        End If
    End Sub
End Class

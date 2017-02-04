Public Class Deposits
    Inherits System.Web.UI.Page

    Private sngTotalAmount As Single

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub bindDataGrid()
        Dim err As Exception
        Try
            Dim cn As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
            Dim cmd As New SqlCommand("sp_ViewDeposits", cn)
            cmd.CommandType = CommandType.StoredProcedure

            Dim prmJobNum As New SqlParameter("@JobNum", SqlDbType.Int, 4)
            prmJobNum.Value = txtJobNumber.Text
            cmd.Parameters.Add(prmJobNum)

            Dim adpt As New SqlDataAdapter(cmd)
            Dim ds As New DataSet()
            adpt.Fill(ds)

            Dim i As Integer
            sngTotalAmount = 0
            For i = 0 To ds.Tables(0).Rows.Count - 1
                sngTotalAmount += ds.Tables(0).Rows(i).Item("check_amount")
            Next

            grid1.DataSource = ds.Tables(0).DefaultView
            grid1.DataBind()

            cn.Close()
            lblValidation.Text = ""
        Catch err
            lblValidation.Text = err.ToString
        End Try
    End Sub

    Public Function getTotalAmount() As String
        Return String.Format("Total: {0:c}", sngTotalAmount)
    End Function

    Private Sub btnGet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGet.Click
        If Not IsNumeric(txtJobNumber.Text) Or Len(txtJobNumber.Text) = 0 Then
            lblValidation.Text = "You did not enter a numeric job number."
        Else
            lblValidation.Text = ""
            btnGet.Enabled = False
            txtJobNumber.Enabled = False
            grid1.Visible = True
            bindDataGrid()
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        lblValidation.Text = ""

        txtJobNumber.Text = ""
        txtJobNumber.Enabled = True

        btnGet.Enabled = True

        grid1.Visible = False
        grid1.EditItemIndex = -1
    End Sub

    Private Function isValidRecord(ByVal sCheckNumber As String, ByVal sCheckDate As String, ByVal sCheckAmount As String) As Boolean
        Dim bValid As Boolean = True
        lblValidation.Text = ""

        If Not IsNumeric(sCheckNumber) Or Len(sCheckNumber) = 0 Then
            lblValidation.Text = lblValidation.Text + "You did not enter a numeric check number.  "
            bValid = False
        End If

        If Not IsDate(sCheckDate) Or Len(sCheckDate) = 0 Then
            lblValidation.Text = lblValidation.Text + "You did not enter a valid check date.  "
            bValid = False
        End If

        If Not IsNumeric(sCheckAmount) Or Len(sCheckAmount) = 0 Then
            lblValidation.Text = lblValidation.Text + "You did not enter a numeric check amount.  "
            bValid = False
        End If

        Return bValid
    End Function


    Private Sub grid1_Click(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles grid1.ItemCommand

        Select Case UCase(e.CommandName)
            Case "ADD"
                Dim sJobNumber As String
                Dim sCheckNumber As String
                Dim sCheckDate As String
                Dim sCheckDescription As String
                Dim sCheckAmount As String

                sJobNumber = txtJobNumber.Text
                sCheckNumber = CType(e.Item.FindControl("txtNewCheckNumber"), TextBox).Text
                sCheckDate = CType(e.Item.FindControl("txtNewCheckDate"), TextBox).Text
                sCheckDescription = CType(e.Item.FindControl("txtNewCheckDescription"), TextBox).Text
                sCheckAmount = CType(e.Item.FindControl("txtNewAmount"), TextBox).Text

                If isValidRecord(sCheckNumber, sCheckDate, sCheckAmount) Then
                    Dim err As Exception
                    Try
                        Dim conQA As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                        Dim cmdQA As New SqlCommand("sp_AddDeposit", conQA)
                        cmdQA.CommandType = CommandType.StoredProcedure

                        Dim prmJobNumber As New SqlParameter("@JobNum", SqlDbType.Int)
                        prmJobNumber.Value = sJobNumber
                        cmdQA.Parameters.Add(prmJobNumber)

                        Dim prmCheckNumber As New SqlParameter("@Check_Num", SqlDbType.NVarChar, 50)
                        prmCheckNumber.Value = sCheckNumber
                        cmdQA.Parameters.Add(prmCheckNumber)

                        Dim prmCheckDate As New SqlParameter("@Check_Date", SqlDbType.SmallDateTime)
                        prmCheckDate.Value = sCheckDate
                        cmdQA.Parameters.Add(prmCheckDate)

                        Dim prmCheckDescription As New SqlParameter("@Description", SqlDbType.NVarChar, 50)
                        prmCheckDescription.Value = sCheckDescription
                        cmdQA.Parameters.Add(prmCheckDescription)

                        Dim prmCheckAmount As New SqlParameter("@Amount", SqlDbType.Money)
                        prmCheckAmount.Value = sCheckAmount
                        cmdQA.Parameters.Add(prmCheckAmount)

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


            Case "UPDATE"
                Dim sJobNumber As String
                Dim sCheckNumber As String
                Dim sCheckDate As String
                Dim sCheckDescription As String
                Dim sCheckAmount As String

                sJobNumber = txtJobNumber.Text
                sCheckNumber = CType(e.Item.FindControl("txtCheckNumber"), TextBox).Text
                sCheckDate = CType(e.Item.FindControl("txtCheckDate"), TextBox).Text
                sCheckDescription = CType(e.Item.FindControl("txtCheckDescription"), TextBox).Text
                sCheckAmount = CType(e.Item.FindControl("txtAmount"), TextBox).Text

                If isValidRecord(sCheckNumber, sCheckDate, sCheckAmount) Then
                    Dim err As Exception
                    Try
                        Dim conQA As New SqlConnection(WebConfigurationManager.AppSettings("ProAdminConnectionString"))
                        Dim cmdQA As New SqlCommand("sp_UpdateDeposits", conQA)
                        cmdQA.CommandType = CommandType.StoredProcedure

                        Dim prmID As New SqlParameter("@ID", SqlDbType.Int)
                        prmID.Value = grid1.DataKeys(e.Item.ItemIndex)
                        cmdQA.Parameters.Add(prmID)

                        Dim prmJobNumber As New SqlParameter("@JobNum", SqlDbType.Int)
                        prmJobNumber.Value = sJobNumber
                        cmdQA.Parameters.Add(prmJobNumber)

                        Dim prmCheckNumber As New SqlParameter("@CheckNum", SqlDbType.NVarChar, 50)
                        prmCheckNumber.Value = sCheckNumber
                        cmdQA.Parameters.Add(prmCheckNumber)

                        Dim prmCheckDate As New SqlParameter("@Date", SqlDbType.SmallDateTime)
                        prmCheckDate.Value = sCheckDate
                        cmdQA.Parameters.Add(prmCheckDate)

                        Dim prmCheckDescription As New SqlParameter("@Description", SqlDbType.NVarChar, 50)
                        prmCheckDescription.Value = sCheckDescription
                        cmdQA.Parameters.Add(prmCheckDescription)

                        Dim prmCheckAmount As New SqlParameter("@Amount", SqlDbType.Money)
                        prmCheckAmount.Value = sCheckAmount
                        cmdQA.Parameters.Add(prmCheckAmount)

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
                    Dim cmdQA As New SqlCommand("sp_DeleteDeposits", conQA)
                    cmdQA.CommandType = CommandType.StoredProcedure

                    Dim prmID As New SqlParameter("@ID", SqlDbType.Int)
                    prmID.Value = grid1.DataKeys(e.Item.ItemIndex)
                    cmdQA.Parameters.Add(prmID)

                    conQA.Open()
                    cmdQA.ExecuteNonQuery()
                    conQA.Close()

                    grid1.EditItemIndex = -1
                    lblValidation.Text = ""
                    bindDataGrid()
                Catch
                    lblValidation.Text = err.ToString
                End Try

            Case "EDIT"
                grid1.EditItemIndex = e.Item.ItemIndex
                bindDataGrid()

            Case "CANCEL"
                grid1.EditItemIndex = -1
                bindDataGrid()


        End Select
    End Sub

End Class

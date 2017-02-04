Imports System

Public Class calpop
    Inherits System.Web.UI.Page
    Protected m As Int16
    Protected y As Int16
    Private fDay As Int16
    Private numDay As Int16
    Private mPrev As Int16
    Private yPrev As Int16
    Private mNext As Int16
    Protected WithEvents cal As System.Web.UI.WebControls.Label
    Private yNext As Int16


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

        Dim work As Date
        If IsNothing(Request.QueryString("m")) Then
            m = DatePart(DateInterval.Month, Today)
        Else
            m = Request.QueryString("m")
        End If

        If IsNothing(Request.QueryString("y")) Then
            y = DatePart(DateInterval.Year, Today)
        Else
            y = Request.QueryString("y")
        End If

        work = m & "/01/" & y

        fDay = Weekday(m & "/01/" & y)
        numDay = DateDiff(DateInterval.Day, work, DateAdd(DateInterval.Month, 1, work))


        mPrev = Month(DateAdd("m", -1, m & "/01/" & y))
        yPrev = Year(DateAdd("m", -1, m & "/01/" & y))

        mNext = Month(DateAdd("m", 1, m & "/01/" & y))
        yNext = Year(DateAdd("m", 1, m & "/01/" & y))

        Dim s As New Text.StringBuilder()
        s.Append(WriteMonthNameTable)
        s.Append(WriteCalTableBegin)
        s.Append(WriteWeekLabelRow)

        ' -- Code to output the Calendar
        s.Append(GenerateCalendar)

        s.Append(WriteCalTableEnd)

        cal.Text = s.ToString

    End Sub
    Private Function WriteMonthNameTable() As String
        ' -- outputs the table that displays the month name 
        ' -- and the prev and next buttons	
        Dim s As New System.Text.StringBuilder()
        s.Append("<TABLE BORDER='0' CELLPADDING='1' CELLSPACING='0' WIDTH='200' STYLE='border : 1px solid Black;'>")
        s.Append("<TR BGCOLOR='#CCCCCC'>")
        s.Append("<TD WIDTH='12' ALIGN='CENTER' VALIGN='TOP'>")
        s.Append("<A HREF=" & Request.ServerVariables("SCRIPT_NAME") & "?tbm=" & Request.QueryString("tbm") & "&m=" & mPrev)
        s.Append("&y=" & yPrev & "><IMG SRC='images/calmLeft.gif' WIDTH='8' HEIGHT='12' ALT='Previous Month' BORDER='0'></A></TD>")
        s.Append("<TD WIDTH='176' ALIGN='CENTER' VALIGN='TOP'><STRONG>" & MonthName(m) & ", " & y & "</STRONG></TD>")
        s.Append("<TD WIDTH='12' ALIGN='CENTER' VALIGN='TOP'><A HREF=" & Request.ServerVariables("SCRIPT_NAME"))
        s.Append("?tbm=" & Request.QueryString("tbm") & "&m=" & mNext & "&y=" & yNext & ">")
        s.Append("<IMG SRC='images/calmRight.gif' WIDTH='8' HEIGHT='12' ALT='Next Month'")
        s.Append("BORDER='0'></A></TD></TR></TABLE>")

        Return s.ToString

    End Function
    Private Function WriteCalTableBegin() As String
        Return "<TABLE BORDER='0' CELLPADDING='1' CELLSPACING='0' WIDTH='200' STYLE='border: 1px solid black;'>"
    End Function
    Private Function WriteWeekLabelRow() As String
        Dim s As New System.Text.StringBuilder()
        s.Append("<TR><TD>&nbsp;</TD><TD WIDTH='20' ALIGN='CENTER' VALIGN='TOP'>S</TD>")
        s.Append("<TD WIDTH='20' ALIGN='CENTER' VALIGN='TOP'>M</TD>")
        s.Append("<TD WIDTH='20' ALIGN='CENTER' VALIGN='TOP'>T</TD>")
        s.Append("<TD WIDTH='20' ALIGN='CENTER' VALIGN='TOP'>W</TD>")
        s.Append("<TD WIDTH='20' ALIGN='CENTER' VALIGN='TOP'>T</TD>")
        s.Append("<TD WIDTH='20' ALIGN='CENTER' VALIGN='TOP'>F</TD>")
        s.Append("<TD WIDTH='20' ALIGN='CENTER' VALIGN='TOP'>S</TD>")
        s.Append("<TD>&nbsp;</TD></TR><TR><TD>&nbsp;</TD>")
        s.Append("<TD COLSPAN='7'><HR SIZE='1' COLOR='#808080' NOSHADE></TD>")
        s.Append("	<TD>&nbsp;</TD></TR>")
        Return s.ToString
    End Function
    Private Function WriteCalTableEnd() As String
        Dim s As New System.Text.StringBuilder()
        s.Append("<TR><TD>&nbsp;</A></TD><TD COLSPAN='7'><HR SIZE='1' COLOR='#808080' NOSHADE></A></TD>")
        s.Append("<TD>&nbsp;</A></TD></TR><TR><TD>&nbsp;</A></TD><TD COLSPAN='7' ALIGN='CENTER' VALIGN='TOP'>")
        s.Append("<INPUT TYPE='BUTTON' NAME='B' VALUE='Today' Style = 'font-family: Tahoma; font-size: 8pt; width: 48px; height: 20px;'")
        s.Append("ONCLICK='SetTodayValue()'></TD><TD>&nbsp;</A></TD></TR></TABLE>")
        Return s.ToString
    End Function
    Private Function GenerateCalendar() As String
        Dim i
        Dim s As New Text.StringBuilder()
        ' -- Row begin
        s.Append("<TR>" & vbCrLf)
        ' -- Left blank column
        s.Append("<TD>&nbsp;</TD>" & vbCrLf)
        ' -- Dump all the days
        For i = 1 To 42
            ' -- is it before the beginning of the month
            If i < fDay Then
                ' -- write a blank
                s.Append(WriteInActiveCalDay("&nbsp;"))
                ' -- Is it after the end of the month
            ElseIf i > numDay + fDay - 1 Then
                ' -- Write a blank
                s.Append(WriteInActiveCalDay("&nbsp;"))
            Else
                ' -- Write the day
                s.Append(WriteActiveCalDay(i - fDay + 1))
            End If
            ' -- Do we need to go to the next row?
            If i Mod 7 = 0 Then
                ' -- we are at the end of the week
                ' -- Right blank column
                s.Append("<TD>&nbsp;</TD>" & vbCrLf)
                ' -- End of the Row
                s.Append("</TR>" & vbCrLf)
                ' -- Do we need another row? 
                If i <= numDay + fDay - 1 Then
                    ' -- Row begin
                    s.Append("<TR>" & vbCrLf)
                    ' -- Left blank column
                    s.Append("<TD>&nbsp;</TD>" & vbCrLf)
                Else
                    ' -- we have exceeded the number of days, get out
                    Exit For
                End If
            End If
        Next
        ' -- End the row
        If i Mod 7 <> 0 Then
            Do While i Mod 7 > 0
                s.Append(WriteInActiveCalDay("&nbsp;"))
                i = i + 1
            Loop
            ' -- Right blank column
            s.Append("<TD>&nbsp;</TD>" & vbCrLf)
            ' -- End of the Row
            s.Append("</TR>" & vbCrLf)
        End If
        Return s.ToString
    End Function
    Private Function WriteInActiveCalDay(ByVal sLabel As String) As String
        Return "<TD WIDTH=""20"" ALIGN=""CENTER"" VALIGN=""TOP"">" & sLabel & "</TD>" & vbCrLf
    End Function
    '------------------------------------------------------
    Private Function WriteActiveCalDay(ByVal sLabel As String) As String
        Return "<TD WIDTH=""20"" ALIGN=""CENTER"" VALIGN=""TOP""><A HREF=""javascript:SetDateValue(" & CInt(sLabel) & ");"">" & sLabel & "</A></TD>" & vbCrLf
    End Function


End Class


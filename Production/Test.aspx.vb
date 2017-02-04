Imports Microsoft.SharePoint.Client
Imports System.Linq

Public Class Test
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Function SiteExist(ByVal ctx As ClientContext, ByVal JobYear As String, ByVal JobNumber As String) As Boolean
        Dim oWebSites = ctx.Web.Webs
        ctx.Load(oWebSites)
        ctx.ExecuteQuery()
        Dim s As String
        Dim result = False
        For Each item In oWebSites
            s = "/Jobs/" & JobYear & "/" & JobNumber
       

            If item.ServerRelativeUrl.ToLower() = s.ToLower() Then
                result = True


            End If

        Next

        Return result

    End Function


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ProductionWorkUrl As String = "http://work.reckner.com"



        Dim ctx As New ClientContext("http://work.reckner.com/jobs/2011")


        Label1.Text = SiteExist(ctx, 2011, "0002")





 



    End Sub
End Class
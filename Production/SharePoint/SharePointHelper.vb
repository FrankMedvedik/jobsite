Imports Microsoft.SharePoint.Client

Public Class SharePointHelper

     Public Shared ProductionWorkUrl As String = "http://work.reckner.com"


    Public Shared Function SiteExist(ByVal ctx As ClientContext, ByVal JobYear As String, ByVal JobNumber As String) As Boolean
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


    '{160C955C-79CA-4DFD-AAEF-878039D76E45}#BlankJob_V01

    Public Shared Sub CreateJobSiteBasedOnTemplate(ByVal jobYear As Integer, ByVal jobNumber As String, ByVal jobDescription As String, ByVal PrimaryMethod As String, ByVal SecondMethod As String)
        Dim jobUrl As String = jobYear & "/" & jobNumber
        Dim fullDesc As String = jobNumber & " " & jobDescription

        Using ctx As New ClientContext(ProductionWorkUrl & "/jobs/" & jobYear)

            If SiteExist(ctx, jobYear, jobNumber) = False Then

                Dim NewJob As New WebCreationInformation


                With NewJob
                    .Url = jobNumber
                    .Description = fullDesc
                    .Title = fullDesc
                    .UseSamePermissionsAsParentSite = True
                End With
                ' fjm 10/13/2016




                Select Case PrimaryMethod
                    Case Is = "Qualitative"
                        NewJob.WebTemplate = "{EC935914-9BFE-4DE7-B993-428270D010AF}#QualTemplate2014a"
                    Case Is = "Quantitative"
                        Select Case SecondMethod
                            Case Is = "Web"
                                NewJob.WebTemplate = "{13F5A851-BBE8-4175-BEFD-6E53377376EC}#QuantWeb2017b"
                                ' 2/4/2017 NewJob.WebTemplate = "{AF1F0E0E-E0CF-411A-99D1-AF475E730BAE}#QuantWeb2017a"
                                ' fjm 12/30/2016 NewJob.WebTemplate = "{F534559A-07A4-4E0B-8B78-FE13D627AF4F}#Quant2016h1"
                            Case Else
                                NewJob.WebTemplate = "{9F4E3860-E467-4B2B-8AE4-5719DEDC7CF7}#Quant2014g"
                        End Select
                    Case Is = "Blank SharePoint JobSite"
                        NewJob.WebTemplate = "{FCA7D073-5938-4077-A89A-EAD9EF990562}#BlankJobSite_v12"
                    Case Is = "Field"
                        Select Case SecondMethod
                            Case Is = "Web"
                                NewJob.WebTemplate = "{B8D46E22-7399-4DCA-A71B-06A7E5FDE8F8}#FieldMgmtWeb2017a"
                                ' fjm 2/4/2017 NewJob.WebTemplate = "{EFBD22D7-9218-4ABF-8FA7-6ECC83A3FEC5}#FieldMgmtWeb2017a"

                            Case Else
                                NewJob.WebTemplate = "{C74C372D-2E04-4AF4-818C-01B3AE4CA5D0}#FieldTemplate_V5"
                        End Select
                    Case Else
                        NewJob.WebTemplate = "{3ADD5185-6E39-4078-88BB-5528DFF7E5DF}#BlankJobSite_v11"
                End Select

                ' BEFORE fjm 10/13/2016
                'Select Case PrimaryMethod
                '    Case Is = "Qualitative"
                '        NewJob.WebTemplate = "{EC935914-9BFE-4DE7-B993-428270D010AF}#QualTemplate2014a"
                '    Case Is = "Quantitative"
                '        NewJob.WebTemplate = "{9F4E3860-E467-4B2B-8AE4-5719DEDC7CF7}#Quant2014g"
                '    Case Is = "Blank SharePoint JobSite"
                '        NewJob.WebTemplate = "{FCA7D073-5938-4077-A89A-EAD9EF990562}#BlankJobSite_v12"
                '    Case Is = "Field"
                '        NewJob.WebTemplate = "{C74C372D-2E04-4AF4-818C-01B3AE4CA5D0}#FieldTemplate_V5"
                '    Case Else
                '        NewJob.WebTemplate = "{3ADD5185-6E39-4078-88BB-5528DFF7E5DF}#BlankJobSite_v11"
                'End Select


                ctx.Web.Webs.Add(NewJob)

                ctx.ExecuteQuery()

            End If



        End Using


        Using ctx As New ClientContext(ProductionWorkUrl & "/jobs/" & jobYear & "/" & jobNumber)
            ctx.Web.Navigation.UseShared = True

            ctx.ExecuteQuery()

        End Using
    End Sub


End Class






﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.18033
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace SharePointSVC
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="SharePointSVC.ISharePointServices")>  _
    Public Interface ISharePointServices
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ISharePointServices/SiteTemplates", ReplyAction:="http://tempuri.org/ISharePointServices/SiteTemplatesResponse")>  _
        Function SiteTemplates(ByVal SharePointSite As String) As String()
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ISharePointServices/CurrentIdentity", ReplyAction:="http://tempuri.org/ISharePointServices/CurrentIdentityResponse")>  _
        Function CurrentIdentity() As String
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ISharePointServices/JobSiteUrl", ReplyAction:="http://tempuri.org/ISharePointServices/JobSiteUrlResponse")>  _
        Function JobSiteUrl() As String
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ISharePointServices/BuildSiteBasedOnTemplate", ReplyAction:="http://tempuri.org/ISharePointServices/BuildSiteBasedOnTemplateResponse")>  _
        Function BuildSiteBasedOnTemplate(ByVal Year As Integer, ByVal JobNumber As Integer, ByVal JobName As String, ByVal ClientID As String, ByVal MethodType As String, ByVal StartDate As Date, ByVal EndDate As Date) As String
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface ISharePointServicesChannel
        Inherits SharePointSVC.ISharePointServices, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class SharePointServicesClient
        Inherits System.ServiceModel.ClientBase(Of SharePointSVC.ISharePointServices)
        Implements SharePointSVC.ISharePointServices
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Function SiteTemplates(ByVal SharePointSite As String) As String() Implements SharePointSVC.ISharePointServices.SiteTemplates
            Return MyBase.Channel.SiteTemplates(SharePointSite)
        End Function
        
        Public Function CurrentIdentity() As String Implements SharePointSVC.ISharePointServices.CurrentIdentity
            Return MyBase.Channel.CurrentIdentity
        End Function
        
        Public Function JobSiteUrl() As String Implements SharePointSVC.ISharePointServices.JobSiteUrl
            Return MyBase.Channel.JobSiteUrl
        End Function
        
        Public Function BuildSiteBasedOnTemplate(ByVal Year As Integer, ByVal JobNumber As Integer, ByVal JobName As String, ByVal ClientID As String, ByVal MethodType As String, ByVal StartDate As Date, ByVal EndDate As Date) As String Implements SharePointSVC.ISharePointServices.BuildSiteBasedOnTemplate
            Return MyBase.Channel.BuildSiteBasedOnTemplate(Year, JobNumber, JobName, ClientID, MethodType, StartDate, EndDate)
        End Function
    End Class
End Namespace

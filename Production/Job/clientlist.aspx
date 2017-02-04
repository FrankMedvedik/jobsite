<%@ Page Language="vb" AutoEventWireup="false" Codebehind="clientlist.aspx.vb" Inherits="Production.clientlist"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>clientlist</title>
        <link href="css/portal.css" rel="stylesheet" type="text/css" />
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie3-2nav3-0">
		<LINK rel="stylesheet" type="text/css" href="portal.css">
		<SCRIPT LANGUAGE="JavaScript" TYPE="text/javascript">
		<!--
		function fillcontrol(ClientID){
		
			window.opener.document.getElementById('<%=Request.QueryString("control")%>').value = ClientID;
			self.close();
		
			}
		//-->
		</SCRIPT>		
	</head>
	<body>
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
				<td><asp:TextBox ID="txtSearch" Runat="server" />
				<asp:Button ID="btnFind" Text="Find" Runat="server" />
				</td>
				</tr>
				<asp:repeater ID="rptClientList" Runat="server" EnableViewState="False">
					<ItemTemplate>
						<tr>
							<td class="normal">
								<A class="Normal" href=# onclick="javascript:fillcontrol('<%# replace(Container.DataItem("ClientID"), "'", "\'")%>');"><%# Container.DataItem("ClientName") %></A>
							</td>
							<td class="normal">
								<asp:Label Runat="server" text=<%# Combine(Container.DataItem("City").ToString(), Container.DataItem("State").ToString())%> />
							</td>
						</tr>
					</ItemTemplate>
				</asp:repeater>
			</table>
		</form>
	</body>
</html>

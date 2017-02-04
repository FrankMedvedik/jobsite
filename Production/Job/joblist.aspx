<%@ Page Language="vb" AutoEventWireup="false" Codebehind="joblist.aspx.vb" Inherits="Production.joblist" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>joblist</title>
		
		<SCRIPT LANGUAGE="JavaScript" TYPE="text/javascript">
		<!--
		function fillcontrol(jobnum){
		    window.opener.document.getElementById('<%=Request.QueryString("control")%>').value = jobnum;

			self.close();
			}
		//-->
		</SCRIPT>		
		
        <link href="css/portal.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table>
				<asp:repeater ID="rptJobList" Runat="server" EnableViewState="False">
					<ItemTemplate>
						<tr>
							<td>
								<A class="Normal" href="javascript:fillcontrol(<%# Container.DataItem("Jobnum") %>);"><%# Container.DataItem("Jobnum") %></A>&nbsp;&nbsp;
							</td>
							<td class="Normal">
								&nbsp;<%# Container.DataItem("ClientID") %>
							</td>
							<td class="Normal">
								<%# Container.DataItem("JobName") %>
							</td>
						</tr>
					</ItemTemplate>
				</asp:repeater>
				<tr>
					<td colspan="3">
						<asp:Label CssClass="Normal" ID="lblMore" Runat="server"/>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

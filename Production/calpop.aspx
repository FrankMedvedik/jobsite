<%@ Page Language="vb" AutoEventWireup="false" Codebehind="calpop.aspx.vb" Inherits="Production.calpop"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>calpop</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie3-2nav3-0">
		<STYLE>
			<!-- 
	body, td {font-family: Tahoma, Verdana, Arial, Helvetica, Sans Serif;
		      font-size: 8pt;
			  }
	// --></STYLE>
	</HEAD>
	<body MS_POSITIONING="FlowLayout" BGCOLOR="#ffffff">
		<form id="Form1" method="post" runat="server">
			<script language="javascript">
<!--
			    function SetDateValue(d) {
			        document.getElementById('<%=Request("tbm")%>').value = '<%= m %>/' + d + '/<%=y %>';
	 
	self.close();
}
function SetTodayValue() {
	                document.getElementById('<%=Request("tbm")%>').value = '<%=Month(today) %>/<%= Day(today) %>/<%= Year(today) %>';
	                self.close();
}
//-->
			</script>
			<asp:Label ID="cal" Runat="server" />
		</form>
	</body>
</HTML>

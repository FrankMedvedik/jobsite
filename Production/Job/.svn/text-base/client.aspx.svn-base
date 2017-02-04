<%@ Page Language="vb" AutoEventWireup="false" Codebehind="client.aspx.vb" Inherits="Production.client"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>client</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie3-2nav3-0">
        <link href="css/portal.css" rel="stylesheet" type="text/css" />
			<script language="javascript">
<!--
function closeWindow(){
if (window.opener.document.getElementById('txtClientID') != null) {
    window.opener.document.getElementById('txtClientID').value = window.document.getElementById('txtClientID').value;
}
self.close();
}
//-->
			</script>
	</HEAD>
	<body MS_POSITIONING="FlowLayout">
		<form id="Form1" method="post" runat="server">
			<table>
				
				<tr>
					<td colspan="2"><asp:Label ID="errMessage" Runat="server" /></td>
				</tr>
				<tr>
					<td class="SubHead">client id:</td>
					<td>
						<asp:TextBox ID="txtClientID" MaxLength="8" Columns="10" CssClass="NormalTextBox" Runat="server" />
						<asp:RegularExpressionValidator id="idValidator" runat="server" ControlToValidate="txtClientID" ValidationExpression="([A-Za-z0-9_\-.]+)"
							ErrorMessage="Client ID's can contain any letter a-z, number 0-9, or the symbols _, - ." />
					</td>
				</tr>
				<tr>
					<td class="SubHead">name:</td>
					<td><asp:TextBox ID="txtClientName" MaxLength="50" Columns="50" CssClass="NormalTextBox" Runat="server" /></td>
				</tr>
				<tr>
					<td class="SubHead">address:</td>
					<td><asp:TextBox ID="txtClientAddress" TextMode="MultiLine" Columns="50" MaxLength="100" CssClass="NormalTextBox"
							Runat="server" /></td>
				</tr>
				<tr>
					<td class="SubHead">city, state zip:</td>
					<td>
						<asp:TextBox ID="txtClientCity" Columns="30" MaxLength="30" CssClass="NormalTextBox" Runat="server" />,
						<asp:TextBox ID="txtClientState" Columns="3" MaxLength="3" CssClass="NormalTextBox" Runat="server" />
						&nbsp;
						<asp:TextBox ID="txtClientZip" Columns="10" MaxLength="10" CssClass="NormalTextBox" Runat="server" />
					</td>
				</tr>
				<tr>
					<td class="SubHead">attention:</td>
					<td><asp:TextBox ID="txtClientAttention" Columns="40" MaxLength="40" CssClass="NormalTextBox" Runat="server" /></td>
				</tr>
				<tr>
					<td class="SubHead">phone:</td>
					<td>
						<asp:TextBox ID="txtClientPhone" Columns="14" MaxLength="10" CssClass="NormalTextBox" Runat="server" />
						<asp:RegularExpressionValidator id="phoneValidator" runat="server" controltovalidate="txtClientPhone" validationexpression="([0-9]+)"
							errormessage="Please enter phone number without any formating symbols" />
					</td>
				</tr>
				<tr>
					<td class="SubHead">fax:</td>
					<td>
						<asp:TextBox ID="txtClientFax" Columns="14" MaxLength="10" CssClass="NormalTextBox" Runat="server" />
						<asp:RegularExpressionValidator id="faxValidator" runat="server" controltovalidate="txtClientFax" ValidationExpression="([0-9]+)"
							errormessage="Please enter fax number without any formating symbols." />
					</td>
				</tr>
				<tr>
					<td class="SubHead">email:</td>
					<td><asp:TextBox ID="txtClientEmail" Columns="50" MaxLength="50" CssClass="NormalTextBox" Runat="server" />
						<asp:RegularExpressionValidator id="emailValidator" runat="server" ControlToValidate="txtClientEmail" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
							ErrorMessage="Please enter a valid email address" /></td>
				</tr>
				<tr>
					<td class="SubHead">country:</td>
					<td><asp:TextBox ID="txtClientCountry" Columns="35" MaxLength="35" CssClass="NormalTextBox" Runat="server" /></td>
				</tr>
				<tr>
					<td>
					<td>
						<asp:LinkButton ID="btnSaveClient" CssClass="CommandButton" Text="save client" Runat="server" />
						&nbsp; <a href="#" onclick="javascript:closeWindow();" class="CommandButton">close 
							window</a>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>

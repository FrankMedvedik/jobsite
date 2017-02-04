<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TimeEditJob.aspx.vb" Inherits="Production.TimeEditJob" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Time Edit Job</title>
    <script src="js/smartDrop.js" type="text/javascript"></script>
    <link href="css/portal.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
	<tr>
		<td CLASS="SubHead">Job</td>
		<td CLASS="SubHead">Starting Date</td>
		<td CLASS="SubHead">Ending Date</td>
		<td CLASS="SubHead"><asp:DropDownList ID="ddlEmpIdHidden" Runat="server" Visible="False" /></td>
	</tr>
	<tr>
		<td><asp:dropdownlist id="ddlJobNumber" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" Width="250" Runat="server" Font-Size="9pt"></asp:dropdownlist></td>
		<td><asp:textbox id="txtStart" CssClass="NormalTextBox" onkeydown="textbox_onkeydown()" Runat="server"></asp:textbox></td>
		<td><asp:textbox id="txtEnd" CssClass="NormalTextBox" onkeydown="textbox_onkeydown()" Runat="server"></asp:textbox></td>
		<td><asp:linkbutton id="btnGetTime" CssClass="CommandButton" Text="Get" runat="server"/>&nbsp;&nbsp;
			<asp:linkbutton id="btnClear" CssClass="CommandButton" Text="Clear" Runat="server"/></td>
	</tr>
	<tr>
		<td colspan="4">
			<asp:label id="lblValidation" Runat="server" CssClass="NormalRed"></asp:label>
		</td>
	</tr>
	<tr>
		<td colspan="4">
			<asp:datagrid id="grid1" runat="server" ShowFooter="True" ItemStyle-CssClass="GRID" AlternatingItemStyle-BackColor="lightgrey" DataKeyField="ID" AutoGenerateColumns="False">
				<Columns>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Date" FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblDate" text='<%# DataBinder.Eval(container.dataitem, "Date", "{0:d}") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtDate" onkeydown="textbox_onkeydown()" Columns="10" Text='<%# DataBinder.Eval(container.dataitem, "Date", "{0:d}") %>' Enabled="False" Runat="server" />
						</EditItemTemplate>
						<FooterTemplate>
							<asp:TextBox ID="txtNewDate" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="10" Runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Employee ID" FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblEmpID" text='<%# container.dataitem("EmpID") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtEmpID" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Text='<%# container.dataitem("EmpID") %>' Enabled=False Runat="server"/>
						</EditItemTemplate>
						<FooterTemplate>
							<asp:dropdownlist ID="ddlNewEmpID" CssClass="NormalTextBox" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" DataTextField="empid" DataValueField="empid" DataSource='<%# getEmpIDDataSource() %>' runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Job Number" FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblJobNumber" text='<%# container.dataitem("JobNum") %>' CssClass="Normal" Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtJobNumber" onkeydown="textbox_onkeydown()" text='<%# container.dataitem("JobNum") %>' enabled = "False" columns="10" CssClass="NormalNumber" runat="server"/>
						</EditItemTemplate>
						<FooterTemplate>
							<asp:TextBox ID="txtNewJobNumber" onkeydown="textbox_onkeydown()" text='<%# getJobNumber() %>' Enabled="False" CssClass="NormalTextBox" columns="10" runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Hours" ItemStyle-HorizontalAlign="Right" FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblHours" text='<%# DataBinder.Eval(container.dataitem, "Hours", "{0:n}") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtHours" onkeydown="textbox_onkeydown()" text='<%# container.dataitem("Hours") %>' columns="8" CssClass="NormalNumber" Enabled='<%# inPeriod(container.dataitem("Date")) %>' runat="server"/>
						</EditItemTemplate>
						<FooterTemplate>
							<asp:TextBox ID="txtNewHours" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" columns="8" runat="server" />
							<hr noshade>
							<asp:Label ID="totalHours" CssClass="SubHead" text='<%# getTotal()%>' Runat="server"/>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Hour Type" FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblHrsType" text='<%# container.dataitem("Hour_Type") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:dropdownlist ID="ddlHrsType" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" selectedindex='<%# getHrsSelectedIndex(container.dataitem("HrsType")) %>' DataSource='<%# getHrsDataSource() %>' DataTextField="code_description" DataValueField="hour_code" runat="server" />
						</EditItemTemplate>
						<FooterTemplate>
							<asp:dropdownlist ID="ddlNewHrsType" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" DataSource='<%# getHrsDataSource() %>' DataTextField="code_description" DataValueField="hour_code" CssClass="NormalTextBox" runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:ImageButton ID="btnEdit" ImageUrl="images/edit.gif" CommandName="Edit" AlternateText="edit" Runat="server" />
							&nbsp;
							<asp:ImageButton ID="btnDelete" ImageUrl="images/delete.gif" CommandName="Delete" AlternateText="delete" Visible='<%# inPeriod(container.dataitem("Date"))%>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="btnUpdate" CssClass="CommandButton" Text="update" CommandName="Update" Runat="server" />
							&nbsp;
							<asp:LinkButton ID="btnCancel" CssClass="CommandButton" Text="cancel" CommandName="Cancel" Runat="server" />
						</EditItemTemplate>
						<Footertemplate>
							<asp:linkButton ID="btnAdd" CssClass="CommandButton" Text="add" CommandName="Add" Runat="server" />
						</Footertemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
		</td>
	</tr>
</table>
<script language="javascript">
<!--
    var objs = document.forms[0];
    for (var i = 0; i < objs.length; i++) {
        if (objs(i).name) {
            var obj = objs(i);
            if (obj.name.indexOf("ddlJobNumber") > 0) {

                if ((!obj.readonly) && (!obj.disabled)) {
                    obj.focus();
                    break;
                }
            }
            else if (obj.name.indexOf("txtDate") > 0) {

                if ((!obj.readonly) && (!obj.disabled)) {
                    obj.focus();
                    break;
                }
            }
            else if (obj.name.indexOf("txtNewDate") > 0) {

                if ((!obj.readonly) && (!obj.disabled)) {
                    obj.focus();
                    break;
                }
            }
        }
    }
//-->
</script>
    </div>
    </form>
</body>
</html>

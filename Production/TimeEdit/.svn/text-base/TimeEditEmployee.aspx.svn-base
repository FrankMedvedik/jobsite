<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TimeEditEmployee.aspx.vb" Inherits="Production.TimeEditEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Time Edit By Employee</title>
    <script src="js/smartDrop.js" type="text/javascript"></script>
    <link href="css/portal.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
	<tr>
		<td class="SubHead">Employee Name</td>
		<td class="SubHead">&nbsp;</td>
		<td class="SubHead">Starting Date</td>
		<td class="SubHead">Ending Date</td>
	</tr>
	<tr>
		<td><asp:dropdownlist id="ddlEmployeeID" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" Runat="server" /></td>
		<td><asp:dropdownlist id="ddlJobNumber" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" Runat="server"
				Visible="False" /></td>
		<td><asp:textbox id="txtStart" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Runat="server" /></td>
		<td><asp:textbox id="txtEnd" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Runat="server" /></td>
		<td><asp:linkbutton id="btnGetTime" CssClass="CommandButton" Text="Get" Runat="server" /></td>
		<td><asp:linkbutton id="btnClear" CssClass="CommandButton" Text="Clear" Runat="server" /></td>
	</tr>
	<tr>
		<td colspan="6"><asp:label id="lblValidation" CssClass="NormalRed" Runat="server"></asp:label></td>
	</tr>
	<tr>
		<td colspan="6">
			<asp:datagrid id="grid1" runat="server" ShowFooter="True" ItemStyle-CssClass="GRID" AlternatingItemStyle-BackColor="lightgrey"
				DataKeyField="ID" AutoGenerateColumns="False">
				<Columns>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Date" FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblDate" text='<%# DataBinder.Eval(container.dataitem, "Date", "{0:d}") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtDate" onkeydown="textbox_onkeydown()" columns="8" text='<%# DataBinder.Eval(container.dataitem, "Date", "{0:d}") %>' Enabled="False" runat="server"/>
						</EditItemTemplate>
						<FooterTemplate>
							<asp:TextBox ID="txtNewDate" onkeydown="textbox_onkeydown()" columns="8" CssClass="NormalTextBox"
								runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Employee ID"
						FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblEmpID" CssClass="Normal" text='<%# container.dataitem("EmpID") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtEmpID" onkeydown="textbox_onkeydown()" columns="8" text='<%# container.dataitem("EmpID") %>' Enabled= "False" runat="server"/>
						</EditItemTemplate>
						<FooterTemplate>
							<asp:label ID="lblNewEmpID" CssClass="Normal" Text='<%# ddlEmployeeID.SelectedItem.Value %>' runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Job Number"
						FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblJobNumber" text='<%# container.dataitem("JobNum") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtJobNumber" onkeydown="textbox_onkeydown()" columns="8" text='<%# container.dataitem("JobNum") %>' Enabled="False" runat="server"/>
						</EditItemTemplate>
						<FooterTemplate>
							<asp:TextBox ID="txtNewJobNumber" onkeydown="textbox_onkeydown()" columns="8" runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Hours" ItemStyle-HorizontalAlign="Right"
						FooterStyle-HorizontalAlign="Right" FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblHours" text='<%# DataBinder.Eval(container.dataitem, "Hours", "{0:n}") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtHours" onkeydown="textbox_onkeydown()" columns="8" text='<%# container.dataitem("Hours") %>' Enabled='<%# inPeriod(container.dataitem("Date")) %>' runat="server"/>
						</EditItemTemplate>
						<FooterTemplate>
							<asp:TextBox ID="txtNewHours" onkeydown="textbox_onkeydown()" columns="8" runat="server" />
							<hr noshade>
							<asp:Label ID="lblTotalLine" CssClass="Normal" text='<%# gettotal() %>' Runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Hour Type"
						FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblHrsType" text='<%# container.dataitem("Hour_Type") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:dropdownlist ID="ddlHrsType" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" selectedindex='<%# getHrsSelectedIndex(container.dataitem("HrsType")) %>' DataSource='<%# getHrsDataSource() %>' DataTextField="code_description" DataValueField="hour_code" runat="server" />
						</EditItemTemplate>
						<FooterTemplate>
							<asp:dropdownlist ID="ddlNewHrsType" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" DataSource='<%# getHrsDataSource() %>' DataTextField="code_description" DataValueField="hour_code" runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:ImageButton ID="Imagebutton1" ImageUrl="images/edit.gif" CommandName="Edit" AlternateText="edit"
								Runat="server" />
							&nbsp;
							<asp:ImageButton ID="Imagebutton2" ImageUrl="images/delete.gif" CommandName="Delete" AlternateText="delete" Visible='<%# inPeriod(container.dataitem("Date")) %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:LinkButton ID="btnUpdate" CssClass="CommandButton" Text="update" CommandName="Update" Runat="server" />
							&nbsp;
							<asp:LinkButton ID="Linkbutton1" CssClass="CommandButton" Text="cancel" CommandName="Cancel" Runat="server" />
						</EditItemTemplate>
						<Footertemplate>
							<asp:linkButton ID="Linkbutton2" CssClass="CommandButton" Text="add" CommandName="Add" Runat="server" />
						</Footertemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
		</td>
	</tr>
	<tr>
		<td colspan="6" align="right"></td>
	</tr>
</table>
<script language="javascript">
<!--
    var objs = document.forms[0];
    for (var i = 0; i < objs.length; i++) {
        if (objs(i).name) {
            var obj = objs(i);
            if (obj.name.indexOf("ddlEmployeeID") > 0) {

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

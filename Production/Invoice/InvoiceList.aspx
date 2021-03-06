﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InvoiceList.aspx.vb" Inherits="Production.InvoiceList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Invoice List</title>
    <script src="js/smartDrop.js" type="text/javascript"></script>
    <link href="css/portal.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            color: #FF0000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="80%">
	<tr>
		<td colspan="2">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2"><asp:Label ID="errMessage" CssClass="NormalRed" Runat="server" /></td>
	</tr>
	<tr>
		<td colspan="2"><table align="center">
				<tr>
					<td class="SubHead">Filter Value</td>
					<td class="SubHead">Filter Column</td>
					<td></td>
					<td>&nbsp;</td>
					<td class="SubHead">Page Size</td>
				</tr>
				<tr>
					<td><asp:TextBox ID="txtFilterValue" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Runat="server" /></td>
					<td><asp:DropDownList ID="cmbFilterColumn" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" CssClass="Normal"
							Runat="server">
							<asp:ListItem Value="customer_name">Customer</asp:ListItem>
							<asp:ListItem Value="jra_job_num" Selected="True">Job Num</asp:ListItem>
							<asp:ListItem Value="projmgr">Project Mgr</asp:ListItem>
							<asp:ListItem Value="approved">Approved</asp:ListItem>
							<asp:ListItem Value="printed">Printed</asp:ListItem>
						</asp:DropDownList></td>
					<td>
                        <asp:linkbutton ID="btnFilter" CssClass="CommandButton" Text="filter" runat="server" />
                        &nbsp;&nbsp;&nbsp;
						<asp:LinkButton ID="btnClearFilter" CssClass="CommandButton" text="clear" Runat="server" />
					</td>
					<td>&nbsp;&nbsp;&nbsp;</td>
					<td>
						<asp:textbox id="txtPageSize" Runat="server" CssClass="Normal" Columns="3" />
						&nbsp;
						<asp:CheckBox ID="chkPageMode" Runat="server" CssClass="Normal" OnCheckedChanged="setPageMode"
							Text="Numeric Buttons" />
						&nbsp;
						<asp:LinkButton ID="btnRefresh" Runat="server" CssClass="CommandButton" text="refresh" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="2"><hr noshade>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<asp:DataGrid ID="dgInvoices" Width="100%" DataKeyField="invoice_id" AllowPaging="True" PagerStyle-HorizontalAlign="Right"
				OnPageIndexChanged="ChangeInvoicePage" PagerStyle-Mode="NextPrev" AllowSorting="True" AllowCustomPaging="True"
				OnSortCommand="SortInvoices" AutoGenerateColumns="false" Runat="server">
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:ImageButton CommandName="delete" ImageUrl="images/delete.gif" visible='<%# not cbool(container.dataitem("approved")) %>' id="btnDelete" Runat="server"/>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:boundcolumn HeaderText="Num" SortExpression="invoice_number" HeaderStyle-CssClass="SubHead"
						ItemStyle-CssClass="Normal" DataField="invoice_number" />
					<asp:BoundColumn HeaderText="Date" SortExpression="invoice_date" HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal"
						DataField="invoice_date" DataFormatString="{0:d}" />
					<asp:BoundColumn HeaderText="Customer" SortExpression="customer_name" HeaderStyle-CssClass="SubHead"
						ItemStyle-CssClass="Normal" DataField="customer_name" />
					<asp:BoundColumn HeaderText="Job Num" SortExpression="jra_job_num" HeaderStyle-CssClass="SubHead"
						ItemStyle-CssClass="Normal" DataField="jra_job_num" />
					<asp:BoundColumn Headertext="Description" HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal"
						DataField="job_description" />
					<asp:BoundColumn HeaderText="Project Mgr" SortExpression="projmgr" HeaderStyle-CssClass="SubHead"
						ItemStyle-CssClass="Normal" DataField="projmgr" />
					<asp:BoundColumn HeaderText="Type" SortExpression="invoice_type" HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal"
						DataField="invoice_type" />
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:HyperLink ID="btnNavigate" NavigateUrl='<%# getPath(container.dataitem("invoice_id"), container.dataitem("approved")) %>' ImageUrl="images/edit.gif" Runat="server"/>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</td>
	</tr>
	<tr>
		<td colspan="2"><hr noshade>
		</td>
	</tr>
	<tr>
		<td align="right">
			<asp:Label ID="lblJobNum" CssClass="SubHead" text="JobNum:" Runat="server" />
			<asp:DropDownList ID="cmbJobNum" CssClass="Normal" Width="200px" DataValueField="jobnum" DataTextField="jobname"
				onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" Runat="server" />
		</td>
		<td>
			<asp:CheckBox ID="chkInclude" CssClass="SubHead" Text="Include Request Detail" Runat="server" />
		</td>
	</tr>
	<tr>
		<td>
		<td align="right">
			<asp:LinkButton ID="btnNewInvoice" CssClass="CommandButton" Text="Create New Invoice" Runat="server" />
		</td>
	</tr>
	<tr>
		<td colspan="2"><hr noshade>
		</td>
	</tr>
</table>
    </div>
    </form>
    <p class="style1">
        Printing an invoice may take longer than expected, please click &quot;only once&quot; to 
        print button and wait for page load.</p>
</body>
</html>

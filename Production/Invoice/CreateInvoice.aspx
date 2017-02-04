<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CreateInvoice.aspx.vb" Inherits="Production.CreateInvoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Invoice</title>
    <link href="css/portal.css" rel="stylesheet" type="text/css" />
			<xml id="xmlCategories" src='xml/accounts.xml'>
			</xml>

		<script type="text/javascript" language="javascript" src="js/smartDrop.js"></script>
		<script type="text/javascript" language="javascript">
<!--
		    

		    function GetTerms() {
		        if (form1.elements("txtInvoiceTerms").value == "") {
		            if (form1.elements("cmbInvoiceType").value == "Advance")
		                form1.elements("txtInvoiceTerms").value = "Due On Receipt";
		            else
		                form1.elements("txtInvoiceTerms").value = "Net 30 days";
		        }
		        return true;
		    }

		    function InsertDefaultValues(c) {
		        var sName = c.name;
		        var sAddDescription = sName.replace("cmbAddCategory", "txtAddDescription");
		        var sAddPrice = sName.replace("cmbAddCategory", "txtAddPrice");
		        if (form1.elements(sAddDescription).value == "") {
		            var sCategory = form1.elements(sName).value;
		            var oCategories = document.all("xmlCategories").XMLDocument.getElementsByTagName("category");
		            for (var i = 0; i < oCategories.length; i++) {
		                if (sCategory == oCategories.item(i).text) {
		                    var oDescriptions = document.all("xmlCategories").XMLDocument.getElementsByTagName("description");
		                    var oPrices = document.all("xmlCategories").XMLDocument.getElementsByTagName("rate");
		                    form1.elements(sAddDescription).value = oDescriptions.item(i).text;
		                    form1.elements(sAddPrice).value = oPrices.item(i).text;
		                    oDescriptions = null;
		                    oPrices = null;
		                    oCategories = null;
		                    break
		                }
		            }

		        }
		        listbox_onblur();
		        return true;
		    }

//-->
		</script>
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
    	<table>
				
				<tr>
					<td colspan="4">
						<asp:label id="lblErrUpdate" Runat="server" CssClass="NormalRed" /><br>
						<asp:RegularExpressionValidator CssClass="NormalRed" ControlToValidate="txtInvoiceDate" text="Please enter an invoice date in the format MM/DD/YYYY"
							ValidationExpression="^(?:(?:(?:0?[13578]|1[02])(\/|-|\.)31)\1|(?:(?:0?[1,3-9]|1[0-2])(\/|-|\.)(?:29|30)\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:0?2(\/|-|\.)29\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:(?:0?[1-9])|(?:1[0-2]))(\/|-|\.)(?:0?[1-9]|1\d|2[0-8])\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$"
							runat="server" ID="Regularexpressionvalidator1" />
					</td>
				</tr>
				<tr>
					<td>
					<td align="right"><asp:CheckBox ID="chkApproved" CssClass="SubHead" Text="Approved" Runat="server" /></td>
					<td align="center"><asp:CheckBox ID="chkPrinted" CssClass="SubHead" Text="Printed" Runat="server" /></td>
					<td><asp:CheckBox ID="chkExported" CssClass="SubHead" Text="Exported" Runat="server" /></td>
				</tr>
				<tr>
					<td colspan="4"><hr noshade>
					</td>
				</tr>
				<tr>
					<td colSpan="2">&nbsp;</td>
					<td class="SubHead" align="right">Invoice ID:</td>
					<td align="right"><asp:label id="lblInvoiceID" name="lblInvoiceID" Runat="server" CssClass="Normal" /></td>
				</tr>
				<tr>
					<td class="SubHead">Customer ID:</td>
					<td><asp:DropDownList ID="cmbCustomer" DataValueField="custid" datatextfield="customer_name" onkeydown="listbox_onkeydown()"
							onblur="listbox_onblur()" Runat="server" />
					</td>
					<td class="SubHead" align="right">Client ID:</td>
					<td align="right"><asp:label id="lblClientID" Runat="server" CssClass="Normal" /></td>
				</tr>
				<tr>
					<td></td>
					<td><asp:label id="lblCustomerName" Runat="server" CssClass="Normal" /></td>
					<td class="SubHead" align="right">Invoice Date:</td>
					<td align="right">
						<asp:textbox id="txtInvoiceDate" onkeydown="textbox_onkeydown()" Runat="server" CssClass="Normal"
							Columns="10" />
					</td>
				</tr>
				<tr>
					<td></td>
					<td><asp:label id="lblCustomerAddressOne" Runat="server" CssClass="Normal" /></td>
					<td class="SubHead" align="right">Invoice #:</td>
					<td align="right"><asp:label id="lblInvoiceNumber" Runat="server" CssClass="Normal" /></td>
				</tr>
				<tr>
					<td></td>
					<td><asp:label id="lblCustomerAddressTwo" Runat="server" CssClass="Normal"></asp:label></td>
					<td class="SubHead" align="right">Invoice Type:</td>
					<td align="right"><asp:dropdownlist id="cmbInvoiceType" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" Runat="server"
							onChange="GetTerms()" CssClass="Normal">
							<asp:ListItem></asp:ListItem>
							<asp:ListItem Value="Advance">Advance</asp:ListItem>
							<asp:ListItem Value="Interim">Interim</asp:ListItem>
							<asp:ListItem Value="Final">Final</asp:ListItem>
						</asp:dropdownlist></td>
				</tr>
				<tr>
					<td></td>
					<td><asp:label id="lblCustomerCity" Runat="server" CssClass="Normal"></asp:label>,
						<asp:label id="lblCustomerState" Runat="server" CssClass="Normal"></asp:label>&nbsp;
						<asp:label id="lblCustomerZip" Runat="server" CssClass="Normal"></asp:label></td>
					<td class="SubHead" align="right">Terms:</td>
					<td align="right"><asp:textbox id="txtInvoiceTerms" onkeydown="textbox_onkeydown()" Runat="server" CssClass="Normal"
							Columns="10"></asp:textbox></td>
				</tr>
				<tr>
					<td></td>
					<td><asp:label id="lblCustomerCountry" Runat="server" CssClass="Normal"></asp:label></td>
					<td class="SubHead" align="right">JRA Job #:</td>
					<td align="right"><asp:label id="lblInvoiceJobNum" Runat="server" CssClass="Normal" /></td>
				</tr>
				<tr>
					<td colspan="2">
					<td class="SubHead" align="right">Client Job #:</td>
					<td align="right"><asp:textbox id="txtInvoiceClientNum" onkeydown="textbox_onkeydown()" Columns="10" Runat="server"
							CssClass="Normal" /></td>
				</tr>
				<tr>
					<td class="SubHead">Attention:</td>
					<td><asp:textbox id="txtAttention" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
							Columns="30" /></td>
					<td class="SubHead" align="right">PO #:</td>
					<td align="right"><asp:textbox id="txtInvoicePONumber" onkeydown="textbox_onkeydown()" Columns="10" Runat="server"
							CssClass="Normal" /></td>
				</tr>
				<tr>
					<td colSpan="2">
					<td class="SubHead" align="right">Phone #:</td>
					<td align="right"><asp:label id="lblCustomerPhoneNumber" Runat="server" CssClass="Normal"></asp:label></td>
				</tr>
				<tr>
					<td class="SubHead">Job Description:</td>
					<td><asp:textbox id="txtJobDescription" Runat="server" onkeydown="textbox_onkeydown()" CssClass="Normal"
							Columns="30"></asp:textbox></td>
					<td class="SubHead" align="right">Fax #:</td>
					<td align="right"><asp:label id="lblCustomerFaxNumber" Runat="server" CssClass="Normal"></asp:label></td>
				</tr>
				<tr>
					<td class="SubHead">Study Dates:</td>
					<td colSpan="3"><asp:textbox id="txtStudyDates" Runat="server" onkeydown="textbox_onkeydown()" CssClass="Normal"
							Columns="30"></asp:textbox></td>
				</tr>
				<tr>
					<td colSpan="4">
						<hr noShade>
					</td>
				</tr>
				<tr>
					<td colSpan="4"><asp:datagrid id="dgInvoiceDetail" CellPadding="2" Runat="server" Width="100%" ShowFooter="True"
							AutoGenerateColumns="false" OnItemCommand="InvoiceDetail_Click" DataKeyField="id" BorderWidth="0">
							<Columns>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:ImageButton ID="btnEdit" ImageUrl="images/edit.gif" CommandName="edit" Runat="server" />
										<asp:ImageButton ID="btnDelete" ImageUrl="images/delete.gif" CommandName="delete" Runat="server" />
										<asp:ImageButton ID="btnUp" ImageUrl="images/up.gif" CommandName="move_up" Runat="server" />
										<asp:ImageButton ID="btnDn" ImageUrl="images/dn.gif" CommandName="move_down" Runat="server" />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:LinkButton ID="btnUpdate" Commandname="update" CssClass="commandbutton" text="update" Runat="server" />
										<asp:LinkButton ID="btnCancel" CommandName="cancel" CssClass="commandbutton" text="cancel" Runat="server" />
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Show" HeaderStyle-CssClass="SubHead">
									<ItemTemplate>
										<asp:CheckBox ID="chkViewShow" Checked='<%# cbool(Container.DataItem("show")) %>' Enabled="False" Runat="server" />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:CheckBox ID="chkUpdateShow" Checked='<%# cbool(Container.DataItem("show"))%>' onkeydown="textbox_onkeydown()" Runat="server" />
									</EditItemTemplate>
									<FooterTemplate>
										<asp:CheckBox ID="chkAddShow" Checked="True" onkeydown="textbox_onkeydown()" Runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Category" HeaderStyle-CssClass="SubHead">
									<ItemTemplate>
										<asp:Label ID="lblCategory" Text='<%# Container.DataItem("category") %>' CssClass="Normal" Runat="server" />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList ID="cmbCategory" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" CssClass="Normal" DataValueField="category" DataTextField="category" DataSource='<%# GetCategory() %>' SelectedIndex='<%# GetSelectedCategory(Container.DataItem("category")) %>' Runat="server" />
									</EditItemTemplate>
									<FooterTemplate>
										<asp:DropDownList ID="cmbAddCategory" onkeydown="listbox_onkeydown()" onblur="InsertDefaultValues(this);" CssClass="Normal" DataValueField="category" DataTextField="category" DataSource='<%# GetCategory() %>' Runat="server"/>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Account" HeaderStyle-CssClass="SubHead">
									<ItemTemplate>
										<asp:Label ID="lblAccount" text='<%# Container.DataItem("account") %>' CssClass="Normal" Runat="server"/>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Depart" HeaderStyle-CssClass="SubHead">
									<ItemTemplate>
										<asp:Label id="lblDepartment" Text='<%# Container.DataItem("department") %>' CssClass="Normal" Runat="server" />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:DropDownList ID="cmbDepartment" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" CssClass="Normal" DataValueField="department" DataTextField="department" DataSource='<%# GetDepartment() %>' SelectedIndex='<%# GetSelectedDepartment(Container.DataItem("department")) %>' Runat="server" />
									</EditItemTemplate>
									<FooterTemplate>
										<asp:DropDownList ID="cmbAddDepartment" onkeydown="listbox_onkeydown()" onblur="listbox_onblur()" CssClass="Normal" DataValueField="department" DataTextField="department" DataSource='<%# GetDepartment() %>' Runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-HorizontalAlign="Right" HeaderText="Quantity" HeaderStyle-CssClass="SubHead">
									<ItemTemplate>
										<asp:Label ID="lblQuantity" CssClass="Normal" Text='<%# DataBinder.Eval(Container.DataItem, "quantity", "{0:n}") %>' Runat="server" />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="txtQuantity" onkeydown="textbox_onkeydown()" CssClass="Normal" text='<%# Container.DataItem("quantity") %>' Columns="6" Runat="server" />
									</EditItemTemplate>
									<FooterTemplate>
										<asp:TextBox ID="txtAddQuantity" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="6" 
 runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Description" HeaderStyle-CssClass="SubHead">
									<ItemTemplate>
										<asp:Label ID="lblDescription" CssClass="Normal" text='<%# Container.DataItem("line_description") %>' Runat="server" />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="txtDescription" onkeydown="textbox_onkeydown()" CssClass="Normal" text='<%# Container.DataItem("line_description") %>' Columns=30 Runat="server" />
									</EditItemTemplate>
									<FooterTemplate>
										<asp:TextBox ID="txtAddDescription" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" 
 Columns="30" runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-HorizontalAlign="Right" HeaderText="Price" HeaderStyle-CssClass="SubHead">
									<ItemTemplate>
										<asp:Label ID="lblPrice" CssClass="Normal" text='<%# DataBinder.Eval(Container.DataItem, "price", "{0:c}") %>' Runat="server" />
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="txtPrice" onkeydown="textbox_onkeydown()" CssClass="Normal" text='<%# Container.DataItem("price") %>' Columns="6" Runat="server" />
									</EditItemTemplate>
									<FooterTemplate>
										<asp:textbox ID="txtAddPrice" onkeydown="textbox_onkeydown()" Columns="6" CssClass="NormalTextBox" 
 runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn ItemStyle-HorizontalAlign="Right" HeaderText="Amount" HeaderStyle-CssClass="SubHead">
									<ItemTemplate>
										<asp:Label ID="lblAmount" CssClass="Normal" text='<%# GetLineTotal( Container.DataItem("quantity"), Container.DataItem("price"))%>' Runat="server" />
									</ItemTemplate>
									<EditItemTemplate>
									</EditItemTemplate>
									<FooterTemplate>
										<asp:LinkButton ID="btnAddDetail" text="add" CssClass="commandbutton" CommandName="add" Runat="server" />
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td colspan="4"><hr noshade>
					</td>
				</tr>
				<tr>
					<td></td>
					<td class="SubHead" align="right">Distributed Total:&nbsp;<asp:Label ID="lblDistributedTotal" cssclass="Normal" Runat="server" />&nbsp;&nbsp;</td>
					<td class="SubHead">Invoice Total:</td>
					<td align="right"><asp:label id="lblInvoiceTotal" Runat="server" CssClass="NormalNumber"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="2"></td>
					<td class="SubHead">Deposits:</td>
					<td align="right"><asp:label id="lblDepositTotal" Runat="server" CssClass="NormalNumber"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="2"><asp:linkbutton id="btnSave" Runat="server" CssClass="commandbutton" Text="save" />&nbsp;&nbsp;<asp:hyperlink id="btnReturn" Runat="server" CssClass="commandbutton" Text="cancel" NavigateUrl="InvoiceList.ascx" />&nbsp;&nbsp;<asp:hyperlink ID="btnPrint" CssClass="commandbutton" text="print" Runat="server" />&nbsp;&nbsp;<asp:HyperLink ID="btnJobCost" CssClass="CommandButton" Runat="server" Text="job cost" Target="_blank" />&nbsp;&nbsp;<asp:LinkButton ID="btnCopyInvoice" CssClass="CommandButton" Runat="server" Text="copy" /></td>
					<td class="SubHead">Balance:</td>
					<td align="right"><asp:Label ID="lblInvoiceBalance" CssClass="NormalNumber" Runat="server" /></td>
				</tr>
				<tr>
					<td colspan="4"><hr noshade>
					</td>
				</tr>
				<tr>
					<td class="SubHead" valign="top">instructions:</td>
					<td colspan="3">
						<asp:TextBox ID="txtInstructions" CssClass="NormalTextBox" TextMode="MultiLine" MaxLength="500"
							Columns="80" Rows="5" Runat="server" />
					</td>
				</tr>
			</table>
    </div>
    </form>
    <p class="style1">
        Printing an invoice may take longer than expected, please click &quot;only once&quot; to 
        print button and wait for the page load.</p>
</body>
</html>

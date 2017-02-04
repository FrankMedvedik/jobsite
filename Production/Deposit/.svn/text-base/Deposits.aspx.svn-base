<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Deposits.aspx.vb" Inherits="Production.Deposits" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deposit Form</title>
    <link href="css/portal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
<!--
        function JobList(c) {
            var s, t, l;
            t = event.screenY + 10;
            l = event.screenX;
            s = "../Job/joblist.aspx?control=" + c + "&jobnum=0";
            window.open(s, null, "left=" + l + ",top=" + t + ",width=450,height=200,toolbar=no,location=no,scrollbars=yes");
            return true;
        }
	//-->
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
	<tr>
		<td class="SubHead">Job Number:</td>
		<td><asp:textbox id="txtJobNumber" CssClass="NormalTextBox" Runat="server" /></td>
		<td><A class="CommandButton" onclick="javascript:JobList('txtJobNumber');" href="#">...</A></td>
		<td><asp:LinkButton id="btnGet" CssClass="CommandButton" runat="server" Text="get" /></td>
		<td><asp:LinkButton id="btnClear" CssClass="CommandButton" Runat="server" Text="clear" /></td>
	</tr>
	<tr>
		<td colspan="5"><asp:label id="lblValidation" Runat="server" CssClass="NormalRed"/></td>
	</tr>
	<tr>
		<td colspan="5">
			<asp:datagrid id="grid1" runat="server" AutoGenerateColumns="False" DataKeyField="ID" AlternatingItemStyle-BackColor="lightgrey" ItemStyle-CssClass="GRID" ShowFooter="True">
				<Columns>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Job Number" FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblJobNum" CssClass="Normal" text='<%# container.dataitem("job_num") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="lblJobNum2" CssClass="Normal" columns="8" Text='<%# container.dataitem("job_num") %>' enabled="False" Runat="server" />
						</EditItemTemplate>
						<FooterTemplate>
							<asp:Label ID="lblJobNum3" CssClass="Normal" Text='<%# txtJobNumber.text %>' Runat="server"/>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Check Number" FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblCheckNumber" CssClass="Normal" text='<%# container.dataitem("check_num") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtCheckNumber" CssClass="NormalTextbox" Text='<%# container.dataitem("check_num") %>' Runat="server" />
						</EditItemTemplate>
						<FooterTemplate>
							<asp:TextBox ID="txtNewCheckNumber" CssClass="NormalTextbox" Runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Check Date" FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblCheckDate" CssClass="Normal" text='<%# DataBinder.Eval(container.dataitem, "check_date", "{0:d}") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtCheckDate" CssClass="NormalTextbox" Text='<%# DataBinder.Eval(container.dataitem, "check_date", "{0:d}") %>' Runat="server" />
						</EditItemTemplate>
						<FooterTemplate>
							<asp:TextBox ID="txtNewCheckDate" CssClass="NormalTextbox" Runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" ItemStyle-CssClass="Normal" HeaderText="Check Description" FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:Label ID="lblCheckDescription" CssClass="Normal" text='<%# container.dataitem("check_description") %>' Runat="server" />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtCheckDescription" CssClass="NormalTextbox" Text='<%# container.dataitem("check_description") %>' Runat="server" />
						</EditItemTemplate>
						<FooterTemplate>
							<asp:TextBox ID="txtNewCheckDescription" CssClass="NormalTextbox" Runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderStyle-CssClass="SubHead" FooterStyle-VerticalAlign="Top" FooterStyle-HorizontalAlign="Right">
						<HeaderTemplate>
							Amount
						</HeaderTemplate>
						<ItemTemplate>
							<div align="right">
								<asp:Label ID="lblAmount" CssClass="Normal" text='<%# DataBinder.Eval(container.dataitem, "check_amount", "{0:c}") %>' Runat="server" />
							</div>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox ID="txtAmount" CssClass="NormalTextbox" Text='<%# container.dataitem("check_amount") %>' Runat="server" />
						</EditItemTemplate>
						<FooterTemplate>
							<asp:TextBox ID="txtNewAmount" CssClass="NormalTextbox" Runat="server" />
							<hr noshade>
							<asp:Label ID="lblTotalAmount" CssClass="SubHead" Text='<%# getTotalAmount() %>' Runat="server" /></b>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn FooterStyle-VerticalAlign="Top">
						<ItemTemplate>
							<asp:ImageButton ID="Imagebutton1" ImageUrl="images/edit.gif" CommandName="Edit" AlternateText="edit" Runat="server" />
							&nbsp;
							<asp:ImageButton ID="Imagebutton2" ImageUrl="images/delete.gif" CommandName="Delete" AlternateText="delete" Runat="server" />
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
			</asp:datagrid></td>
	</tr>
</table>
<script type="text/javascript" language="javascript">
<!--
    var objs = document.forms[0];
    for (var i = 0; i < objs.length; i++) {
        if (objs(i).name) {
            var obj = objs(i);
            if (obj.name.indexOf("txtJobNumber") > 0) {

                if ((!obj.readonly) && (!obj.disabled)) {
                    obj.focus();
                    break;
                }
            }
            else if (obj.name.indexOf("txtCheckNumber") > 0) {

                if ((!obj.readonly) && (!obj.disabled)) {
                    obj.focus();
                    break;
                }
            }
            else if (obj.name.indexOf("txtNewCheckNumber") > 0) {

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

<%@ Page Language="vb" autoEventWireup="false" CodeBehind="Job.aspx.vb" Inherits="Production.Job" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Job Log Form</title>
    <link href="portal.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/themes/cupertino/jquery-ui.css" type="text/css" />
    <script src="job_scripts.js" type="text/javascript"></script>
    <script src="smartdrop.js" type="text/javascript"></script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
   <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/jquery-ui.min.js"></script>
 
 <script type="text/javascript">
     $(document).ready(function () {
         $('#updateJob').one('click', function () {
             $(this).slideUp();
             $('#errMessage').text('Please Wait!!!!!');

         });

     });


      $(function () {
          $("#txtStartdate").datepicker();
      });


      $(function () {
          $("#txtEndDate").datepicker();
      });


      $(function () {
          $(".dateTextBox").datepicker();
      });
 

 </script>

</head>
<body>

    <form id="form1" runat="server">

    <div>
    <table width="800">
	<tr>
		<td width="100%"><asp:panel id="headerpanel" Runat="server">
				<table width="100%">
					<tr>
						<td width="100%">
							<table width="750" bgColor="#dddddd">
								<tr>
									<td class="SubHead">Job Number</td>
									<td>
										<asp:label id="lblName" runat="server" Text="Job Name *" CssClass="SubHead"></asp:label>&nbsp;</td>
									<td>
										<asp:label id="lblactive" Runat="server" Text="active" CssClass="SubHead"></asp:label></td>
								</tr>
								<tr>
									<td>
										<asp:textbox id="txtJobNum" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											MaxLength="8" Columns="10"></asp:textbox>&nbsp; <a id="getJobList" onclick='JobList("txtJobNum");' href="#">
											...</a> &nbsp;
										<asp:linkbutton id="getJob" onkeydown="textbox_onkeydown()" Runat="server" Text="get" CssClass="CommandButton"></asp:linkbutton></td>
									<td>
										<asp:textbox id="txtJobName" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											MaxLength="50" Columns="50"></asp:textbox></td>
									<td>
										<asp:checkbox id="chkJobactive" onkeydown="textbox_onkeydown()" Runat="server"></asp:checkbox></td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</asp:panel><asp:panel id="panelJob" Runat="server">
				<table width="100%">
					<tr>
						<td width="100%">
							<table width="750" bgColor="#eeeeee">
								<tr>
									<td class="SubHead" colspan="2">Client Information *&nbsp;
										<asp:LinkButton id="lnkClientShowHide" Runat="server" CssClass="CommandButton" CommandName="Hide"
											text="(show)"></asp:LinkButton></td>
								</tr>
								<tr>
									<td class="subSubHead">id:</td>
									<td>
										<asp:textbox id="txtClientID" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											MaxLength="8" Columns="8"></asp:textbox>&nbsp; <a class="CommandButton" onclick='ClientList("txtClientID");' href="#">
											...</a> &nbsp;
										<asp:linkbutton id="btnGetClient" Runat="server" CssClass="CommandButton" text="get"></asp:linkbutton></td>
								</tr>
								<tr>
									<td class="subSubHead">name:</td>
									<td>
										<asp:label id="txtClientName" Runat="server" CssClass="Normal"></asp:label></td>
								</tr>
								<asp:Panel id="pnlClient" Runat="server" visible="False">
									<tr>
										<td class="subSubHead">address:</td>
										<td>
											<asp:label id="txtClientaddress" Runat="server" CssClass="Normal"></asp:label></td>
									</tr>
									<tr>
										<td class="subSubHead">city, state zip:</td>
										<td>
											<asp:label id="txtClientCity" Runat="server" CssClass="Normal"></asp:label>,
											<asp:label id="txtClientState" Runat="server" CssClass="Normal"></asp:label>&nbsp;
											<asp:label id="txtClientZip" Runat="server" CssClass="Normal"></asp:label></td>
									</tr>
									<tr>
										<td class="subSubHead">phone:</td>
										<td>
											<asp:label id="txtClientPhoneNumber" Runat="server" CssClass="Normal"></asp:label></td>
									</tr>
									<tr>
										<td class="subSubHead">fax:</td>
										<td>
											<asp:label id="txtClientFaxNumber" Runat="server" CssClass="Normal"></asp:label></td>
									</tr>
									<tr>
										<td class="subSubHead">email:</td>
										<td>
											<asp:label id="txtClientEmail" Runat="server" CssClass="Normal"></asp:label></td>
									</tr>
									<tr>
										<td class="subSubHead">country:</td>
										<td>
											<asp:label id="txtClientCountry" Runat="server" CssClass="Normal"></asp:label></td>
									</tr>
									<tr>
										<td class="subSubHead">attention:</td>
										<td>
											<asp:label id="txtClientattention" Runat="server" CssClass="Normal"></asp:label></td>
									</tr>
									<tr>
										<td></td>
										<td align="right"><a class="CommandButton" onclick="javascript:CreateClient();" href="#">create 
												client</a> &nbsp; <a class="CommandButton" onclick="javascript:UpdateClient();" href="#">
												update client</a>
										</td>
									</tr>
								</asp:Panel></table>
						</td>
					</tr>
					<tr>
						<td width="100%">
							<table width="750" bgColor="#eeeeee">
								<tr>
									<td width="100%"><span class="SubHead">Parts Information</span>
										<asp:datagrid id="dgParts" Runat="server" autoGenerateColumns="False" ShowFooter="true" DataKeyField="id">
											<Columns>
												<asp:TemplateColumn HeaderText="name *" HeaderStyle-CssClass="subSubHead">
													<ItemTemplate>
														<asp:label ID="lblPart" CssClass="Normal" Text='<%# Container.DataItem("Part") %>' Runat="server"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditPart" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="25" MaxLength="25" Text='<%# Container.DataItem("Part") %>' Runat="server"/>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtaddPart" onkeydown="textbox_onkeydown()" Columns="25" CssClass="NormalTextBox"
															MaxLength="25" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="incidence" HeaderStyle-CssClass="subSubHead">
													<ItemTemplate>
														<asp:Label ID="lblIncidence" CssClass="Normal" Text='<%# Container.DataItem("Incidence")%>' Runat="server"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditIncidence" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="8" Text='<%# Container.DataItem("Incidence")%>' Runat="server"/>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtaddIncidence" onkeydown="textbox_onkeydown()" Columns="8" CssClass="NormalTextBox"
															Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="quota" HeaderStyle-CssClass="subSubHead">
													<ItemTemplate>
														<asp:Label ID="lblQuota" CssClass="Normal" text='<%# Container.DataItem("Quota")%>' Runat="server"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditQuota" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="8" Text='<%# Container.DataItem("Quota")%>' Runat="server"/>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtaddQuota" onkeydown="textbox_onkeydown()" Columns="8" CssClass="NormalTextBox"
															Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="hours" HeaderStyle-CssClass="subSubHead">
													<ItemTemplate>
														<asp:Label ID="lblHours" CssClass="Normal" Text='<%# Container.DataItem("Hours")%>' Runat="server"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditHours" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="8" Text='<%# Container.DataItem("Hours")%>' Runat="server"/>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtaddHours" onkeydown="textbox_onkeydown()" Columns="8" CssClass="NormalTextBox"
															Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="cpi" HeaderStyle-CssClass="subSubHead">
													<ItemTemplate>
														<asp:Label ID="lblCpi" CssClass="Normal" Text='<%# Container.DataItem("CPI")%>' Runat="server"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditCpi" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="8" Text='<%# Container.DataItem("CPI")%>' Runat="server"/>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtaddCpi" onkeydown="textbox_onkeydown()" Columns="8" CssClass="NormalTextBox"
															Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="coop" HeaderStyle-CssClass="subSubHead">
													<ItemTemplate>
														<asp:Label ID="lblCoop" CssClass="Normal" Text='<%# Container.DataItem("Coop")%>' Runat="server"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditCoop" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="8" Text='<%# Container.DataItem("Coop")%>' Runat="server"/>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtaddCoop" onkeydown="textbox_onkeydown()" Columns="8" CssClass="NormalTextBox"
															Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="length" HeaderStyle-CssClass="subSubHead">
													<ItemTemplate>
														<asp:Label ID="lblLength" cssclass="Normal" Text='<%# Container.DataItem("Length")%>' Runat="server"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditLength" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="8" Text='<%# Container.DataItem("Length")%>' Runat="server"/>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtaddLength" onkeydown="textbox_onkeydown()" Columns="8" CssClass="NormalTextBox"
															Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="ext" HeaderStyle-CssClass="subSubHead">
													<ItemTemplate>
														<asp:Label ID="lblExt" CssClass="Normal" Text='<%# Container.DataItem("Ext")%>' Runat="server"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditExt" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="8" Text='<%# Container.DataItem("Ext")%>' Runat="server"/>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtaddExt" onkeydown="textbox_onkeydown()" Columns="8" CssClass="NormalTextBox"
															Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<ItemTemplate>
														<asp:ImageButton ID="btnEditPart" ImageUrl="images/edit.gif" CommandName="edit" Runat="server" />
														&nbsp;
														<asp:ImageButton ID="btnDeletePart" ImageUrl="images/delete.gif" CommandName="delete" Runat="server" />
													</ItemTemplate>
													<EditItemTemplate>
														<asp:LinkButton ID="btnUpdatePart" CssClass="CommandButton" CommandName="update" Text="update" Runat="server" />
														&nbsp;
														<asp:LinkButton ID="btnCancelPart" CssClass="CommandButton" CommandName="cancel" Text="cancel" Runat="server" />
													</EditItemTemplate>
													<FooterTemplate>
														<asp:LinkButton ID="btnaddPart" CssClass="CommandButton" CommandName="add" Text="add" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td width="100%">
							<table width="750" bgColor="#eeeeee">
								<tr>
									<td class="SubHead" colspan="4">Organization Information</td>
								</tr>
								<tr>
									<td class="subSubHead">project manager *</td>
									<td class="subSubHead">assistant manager</td>
									<td class="subSubHead">start date *</td>
									<td class="subSubHead">end date *</td>
								</tr>
								<tr>
									<td>
										<asp:dropdownlist id="cmbProjectManager" onkeydown="listbox_onkeydown();" onblur="listbox_onblur()"
											Runat="server" CssClass="NormalTextBox" DataTextField="Proj Man" DataValueField="Proj Man"></asp:dropdownlist></td>
									<td>
										<asp:dropdownlist id="cmbassitantManager" onkeydown="listbox_onkeydown();" onblur="listbox_onblur()"
											Runat="server" CssClass="NormalTextBox" DataTextField="Proj Man" DataValueField="Proj Man"></asp:dropdownlist></td>
									<td>
										<asp:textbox id="txtStartdate" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											EnableViewState="False"></asp:textbox>
									</td>
									<td>
										<asp:textbox id="txtEndDate" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											EnableViewState="False"></asp:textbox>
									</td>
								</tr>
								<tr>
									<td class="subSubHead">method</td>
									<td class="subSubHead">method description</td>
									<td class="subSubHead">prod rate</td>
									<td class="subSubHead"></td>
								</tr>
								<tr>
									<td>
										<asp:dropdownlist id="cmbPriMethod" onkeydown="listbox_onkeydown();" onblur="listbox_onblur()" Runat="server"
											CssClass="NormalTextBox">
											<asp:ListItem Value=""></asp:ListItem>
											<asp:ListItem Value="Qualitative">Qualitative</asp:ListItem>
											<asp:ListItem Value="Quantitative">Quantitative</asp:ListItem>
											<asp:ListItem Value="Field">Field Management</asp:ListItem>

										</asp:dropdownlist>
                                        <br />
                                        <asp:CheckBox ID="chkSharePoint" runat="server" 
                                            Text="Create Blank SharePoint Job Site" />
                                    </td>
									<td>
										<asp:dropdownlist id="cmbScdMethod" onkeydown="listbox_onkeydown();" onblur="listbox_onblur()" Runat="server"
											CssClass="NormalTextBox" DataTextField="Methods" DataValueField="Methods"></asp:dropdownlist></td>
									<td>
										<asp:textbox id="txtProductionRate" onkeydown="textbox_onkeydown()" Runat="server" EnableViewState="False"></asp:textbox></td>
									<td>
										<asp:textbox id="txtTotal" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											EnableViewState="False" ReadOnly="true" Visible="False"></asp:textbox></td>
								</tr>
								<tr>
									<td class="subSubHead">client job #</td>
									<td class="subSubHead">client po #</td>
									<td class="subSubHead">discount group</td>
									<td class="subSubHead">master job number</td>
								</tr>
								<tr>
									<td height="19">
										<asp:textbox id="txtClientJobNumber" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											EnableViewState="False"></asp:textbox></td>
									<td height="19">
										<asp:textbox id="txtClientPoNumber" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											EnableViewState="False"></asp:textbox></td>
									<td height="19">
										<asp:dropdownlist id="cmbProductCategory" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											DataTextField="Product_Category" DataValueField="Product_Category"></asp:dropdownlist></td>
									<td height="19">
										<asp:textbox id="txtMasterJobNumber" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											EnableViewState="False"></asp:textbox></td>
								</tr>
								<tr>
									<td class="subSubHead">invoice #</td>
								</tr>
								<tr>
									<td>
										<asp:textbox id="txtInvoiceNumber" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											EnableViewState="False"></asp:textbox></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td width="100%">
							<table width="750" bgColor="#eeeeee">
								<tr>
									<td class="SubHead" colspan="4">income distribution</td>
								</tr>
								<tr>
									<td class="SubHead">jra total</td>
									<td class="SubHead">subcontractor total</td>
									<td class="SubHead">honoraria</td>
									<td class="SubHead">job total</td>
									<td class="SubHead">brand</td>
								</tr>
								<tr>
									<td>
										<asp:TextBox id="txtJRaTotal" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											onChange="updateTotal()"></asp:TextBox></td>
									<td>
										<asp:TextBox id="txtSubcontractorTotal" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											onChange="updateTotal()"></asp:TextBox></td>
									<td>
										<asp:TextBox id="txtHonorariaTotal" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											onChange="updateTotal()"></asp:TextBox></td>
									<td>
										<asp:TextBox id="txtJobTotal" onkeydown="textbox_onkeydown()" Runat="server" CssClass="NormalTextBox"
											ReadOnly="true"></asp:TextBox></td>
									<td>
										<asp:DropDownList id="cmbBrand" runat="server" CssClass="NormalTextBox" Enabled="False">
											<asp:ListItem></asp:ListItem>
											<asp:ListItem Value="BLUE">BLUE</asp:ListItem>
											<asp:ListItem Value="GHRS">GHRS</asp:ListItem>
											<asp:ListItem Value="ISR">ISR</asp:ListItem>
											<asp:ListItem Value="LM">LM</asp:ListItem>
										</asp:DropDownList></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td width="100%">
							<table width="750" bgColor="#eeeeee">
								<tr>
									<td class="SubHead" colspan="6">location *</td>
								</tr>
								<tr>
									<td>
										<asp:checkbox id="chkMOP" onkeydown="textbox_onkeydown()" Runat="server" Text="central ops" CssClass="Normal"
											EnableViewState="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkMall" onkeydown="textbox_onkeydown()" Runat="server" Text="mall" CssClass="Normal"
											EnableViewState="False" Enabled="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chk1600" onkeydown="textbox_onkeydown()" Runat="server" Text="1600 market street"
											CssClass="Normal" EnableViewState="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkOther" onkeydown="textbox_onkeydown()" Runat="server" Text="other" CssClass="Normal"
											EnableViewState="False" Enabled="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkExton" onkeydown="textbox_onkeydown()" Runat="server" Text="exton" CssClass="Normal"
											EnableViewState="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkMtLaurel" onkeydown="textbox_onkeydown()" Runat="server" Text="mt laurel"
											CssClass="Normal" EnableViewState="False"></asp:checkbox></td>
								</tr>
								<tr>
									<td>
										<asp:checkbox id="chkFieldManagement" onkeydown="textbox_onkeydown()" Runat="server" Text="field management"
											CssClass="Normal" EnableViewState="False" Enabled="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkDCMC" onkeydown="textbox_onkeydown()" Runat="server" Text="dcmc" CssClass="Normal"
											EnableViewState="False" Enabled="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkFieldaudit" onkeydown="textbox_onkeydown()" Runat="server" Text="field audit"
											CssClass="Normal" EnableViewState="False" Enabled="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkWesTest" onkeydown="textbox_onkeydown()" Runat="server" Text="white plains"
											CssClass="Normal" EnableViewState="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkRacine" onkeydown="textbox_onkeydown()" Runat="server" Text="racine" CssClass="Normal"
											EnableViewState="False" Enabled="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkMilwaukee" onkeydown="textbox_onkeydown()" Runat="server" Text="milwaukee"
											CssClass="Normal" EnableViewState="False"></asp:checkbox></td>
								</tr>
								<tr>
									<td>
										<asp:checkbox id="chkCorporate" onkeydown="textbox_onkeydown()" Runat="server" Text="corporate"
											CssClass="Normal" EnableViewState="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkGHRS" onkeydown="textbox_onkeydown()" Runat="server" Text="ghrs" CssClass="Normal"
											EnableViewState="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkFacilityMgmt" onkeydown="textbox_onkeydown()" Runat="server" Text="facility mgmt"
											CssClass="Normal" EnableViewState="False"></asp:checkbox></td>
									<td>
										<asp:checkbox id="chkConsumerInsights" onkeydown="textbox_onkeydown()" Runat="server" Text="consumer insights"
											CssClass="Normal" EnableViewState="False"></asp:checkbox></td>
									<td>
										<asp:CheckBox id="chkISR" onkeydown="textbox_onkeydown()" Runat="server" Text="isr" CssClass="Normal"
											EnableViewState="False"></asp:CheckBox></td>
									<td></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td width="100%">
							<table width="750" bgColor="#eeeeee">
								<tr>
									<td width="100%"><span class="SubHead">notes</span>
										<asp:datagrid id="dgNotes" Runat="server" autoGenerateColumns="False" ShowFooter="true" DataKeyField="note_id">
											<Columns>
												<asp:TemplateColumn HeaderText="date" HeaderStyle-CssClass="subSubHead">
													<ItemTemplate>
														<asp:Label ID="lblNoteDate" CssClass="Normal" Text='<%# Container.DataItem("note_date")%>' Runat="server"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditNoteDate" onkeydown="textbox_onkeydown()" CssClass="dateTextBox" Columns="10" Text='<%# Container.DataItem("note_date")%>' Runat="server"/>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtaddNoteDate" onkeydown="textbox_onkeydown()" CssClass="dateTextBox" Columns="10"
															Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn HeaderText="note" HeaderStyle-CssClass="subSubHead">
													<ItemTemplate>
														<asp:Label ID="lblNote" CssClass="Normal" Text='<%# Container.DataItem("note_text")%>' Runat="server"/>
													</ItemTemplate>
													<EditItemTemplate>
														<asp:TextBox ID="txtEditNote" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="50" TextMode="MultiLine" Text='<%# Container.DataItem("note_text")%>' Runat="server"/>
													</EditItemTemplate>
													<FooterTemplate>
														<asp:TextBox ID="txtaddNote" onkeydown="textbox_onkeydown()" CssClass="NormalTextBox" Columns="50"
															Rows="3" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
												<asp:TemplateColumn>
													<ItemTemplate>
														<asp:ImageButton ID="btnEditNote" ImageUrl="../images/edit.gif" CommandName="edit" Runat="server" />
														&nbsp;
														<asp:ImageButton ID="btnDeleteNote" ImageUrl="../images/delete.gif" CommandName="delete" Runat="server" />
													</ItemTemplate>
													<EditItemTemplate>
														<asp:LinkButton ID="btnUpdateNote" CssClass="CommandButton" CommandName="update" Text="update" Runat="server" />
														&nbsp;
														<asp:LinkButton ID="btnCancelNote" CssClass="CommandButton" CommandName="cancel" Text="cancel" Runat="server" />
													</EditItemTemplate>
													<FooterTemplate>
														<asp:LinkButton ID="btnaddNote" CssClass="CommandButton" CommandName="add" Text="add" Runat="server" />
													</FooterTemplate>
												</asp:TemplateColumn>
											</Columns>
										</asp:datagrid></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td> 
							<asp:label id="errMessage" Runat="server" cssclass="ErrorRed"></asp:label></td>
					</tr>
				</table>
			</asp:panel></td>
	</tr>
	<tr>
		<td colspan="3"><asp:linkbutton id="createJob" Runat="server" CssClass="CommandButton" Text="create job"></asp:linkbutton>&nbsp;
			<asp:linkbutton id="updateJob" Runat="server" CssClass="CommandButton" Text="update job"></asp:linkbutton>&nbsp;
			<a class="CommandButton" onclick="javascript:history.go(0);" href="#">clear page</a></td>
	</tr>
</table>
<script type="text/javascript" language="javascript">
<!--
    var objs = document.forms(0).elements;
    for (var i = 0; i < objs.length; i++) {
        if (objs[i].name.indexOf("txtJobNum") > 0) {
            if (objs[i].readOnly == true) {
                i += 1
            }
            objs[i].focus();
            break;
        }
    }
		
//-->
</script>
    </div>
    </form>
    </body>
</html>

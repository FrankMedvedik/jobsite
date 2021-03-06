﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TimeEntry.aspx.vb" Inherits="Production.TimeEntry" ValidateRequest="false"%>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 transitional//EN" "http://www.w3.org/tr/xhtml1/Dtd/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Time</title>
    <script src="js/smartDrop.js" type="text/javascript"></script>
    <link href="css/portal.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="errMessage" Runat="server" /><br>
<asp:Panel ID="topPanel" Runat="server">
	<table>
		<tr>
			<td>
				<asp:label id="lblLocation" Runat="server" CssClass="SubHead" Text="select location:"></asp:label></td>
			<td>
				<asp:dropdownlist id="cmbLocation" onkeydown="listbox_onkeydown();" onblur="listbox_onblur()" Runat="server" CssClass="Normal" DataValueField="Location" DataTextField="Description"></asp:dropdownlist></td>
			<td>
				<asp:linkbutton id="btnFillValues" Runat="server" CssClass="CommandButton" Text="get values"></asp:linkbutton></td>
		</tr>
	</table>
</asp:Panel>
<div id="timeValues" name="timeValues"></div>
<input  id="xmlTimeString" name="xmlTimeString" type="hidden" />


<asp:Panel ID="bottomPanel" Runat="server">
	<hr width="98%" noshade />
	<table>
		<tr>
			<td class="SubHead">date</td>
			<td class="SubHead">employee</td>
		</tr>
		<tr>
			<td>
				<asp:dropdownlist id="cmbDate" name="cmbDate" onkeydown="listbox_onkeydown();" onblur="listbox_onblur()" Runat="server" CssClass="Normal"></asp:dropdownlist></td>
			<td>
				<asp:dropdownlist id="cmbEmployee" name="cmbEmployee" onkeydown="listbox_onkeydown();" onblur="listbox_onblur()" Runat="server" CssClass="Normal" DataValueField="empid" DataTextField="name"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td class="SubHead" colspan="2">job num</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:dropdownlist id="cmbJobNum" name="cmbJobNum" onkeydown="listbox_onkeydown();" onblur="listbox_onblur()" Runat="server" CssClass="Normal"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td class="SubHead">hours</td>
			<td class="SubHead">type</td>
		</tr>
		<tr>
			<td>
				<asp:textbox id="txtHours" name="txtHours" onkeydown="textbox_onkeydown()" onblur="validateHours(this);" Runat="server" CssClass="NormalTextBox" Columns="4"></asp:textbox></td>
			<td>
				<asp:dropdownlist id="cmbHourType" name="cmbHourType" onkeydown="listbox_onkeydown();" onblur="listbox_onblur()" Runat="server" CssClass="Normal" DataValueField="type" DataTextField="hour_type"></asp:dropdownlist>&nbsp;
				<a class="CommandButton" onclick="addTime();" href="#">add</a>
			</td>
		</tr>
		<tr>
			<td></td>
			<td>
				<asp:LinkButton id="btnSubmit" Runat="server" CssClass="CommandButton" Text="submit values"></asp:LinkButton></td>
		</tr>
	</table>
    <script type="text/javascript">
        // Register all controls needed for time card.

        var oCount = 0;

        var objs = document.forms(0).elements;
        for (var i = 0; i < objs.length; i++) {
            var obj;
            var obj = objs(i);
            if (obj.name) {

                if (obj.name.indexOf("cmbDate") > -1) {
                    objDate = obj;
                    oCount += 1;
                }
                else if (obj.name.indexOf("cmbEmployee") > -1) {
                    objEmp = obj;
                    oCount += 1;
                }
                else if (obj.name.indexOf("cmbJobNum") > -1) {
                    objJob = obj;
                    oCount += 1;
                }
                else if (obj.name.indexOf("txtHours") > -1) {
                    objHours = obj;
                    oCount += 1;
                }
                else if (obj.name.indexOf("cmbHourType") > -1) {
                    objType = obj;
                    oCount += 1;
                }
                else if (obj.name.indexOf("cmbLocation") > -1) {
                    objLocation = obj;
                    oCount += 1;
                }
            }
        }
        if (oCount == 1) {
            objLocation.focus();
        }
        else if (oCount == 5) {
            objDate.focus();
        }
    </script>
</asp:Panel>
<script type="text/javascript" language="javascript">
<!--
    var objLocation;
    var objWorkDate;
    var objEmp;
    var objJob;
    var objHours;
    var objType;
    var objSubmit;
    var objError;

    var objXML;
    var myErr;
    //objXML = new ActiveXObject("MSXML2.DOMDocument");
    var text = "<?xml version='1.0' encoding='UTF-8' standalone='yes' ?><timeCard><timeRecord><workDate></workDate><empID></empID><empName></empName><jobNum></jobNum><jobName></jobName><hourNum></hourNum><hourType></hourType><hourDescription></hourDescription></timeRecord></timeCard>";
    if (window.DOMParser) {
        parser = new DOMParser();
        objXML = parser.parseFromString(text, "text/xml");
    }
    else // Internet Explorer
    {
        objXML = new ActiveXObject("Microsoft.XMLDOM");
        objXML.async = "false";
        objXML.loadXML(text);
    } 

    //objXML.async = false;
    // requested change to display hour description in grid
    // objXML.loadXML("<timeCard><timeRecord><workDate></workDate><empID></empID><empName></empName><jobNum></jobNum><jobName></jobName><hourNum></hourNum><hourType></hourType></timeRecord></timeCard>");
    //
    //objXML.loadXML("<timeCard><timeRecord><workDate></workDate><empID></empID><empName></empName><jobNum></jobNum><jobName></jobName><hourNum></hourNum><hourType></hourType><hourDescription></hourDescription></timeRecord></timeCard>");


//    myErr = objXML.parseError;
//    if (myErr.errorCode != 0) {
//        window.alert(objXML.parseError.reason);
//    }
    var root;
    root = objXML.documentElement;
    root.removeChild(root.childNodes.item(0));
    root = null;

    //if (document.Form1.elements['_ctl0:cmbLocation'] != null){
    //		document.Form1.elements['_ctl0:cmbLocation'].focus();
    //		}


    function addTime(c) {

        var workDate = objDate.value; //document.Form1.elements['_ctl0:cmbDate'].value;
        var empID = objEmp.value; //document.Form1.elements['_ctl0:cmbEmployee'].value;
        var empName = objEmp.options[objEmp.selectedIndex].text; //document.Form1.elements['_ctl0:cmbEmployee'].options[document.Form1.elements['_ctl0:cmbEmployee'].selectedIndex].text;
        var jobNum = objJob.value; //document.Form1.elements['_ctl0:cmbJobNum'].value;
        var jobText = objJob.options[objJob.selectedIndex].text; //document.Form1.elements['_ctl0:cmbJobNum'].options[document.Form1.elements['_ctl0:cmbJobNum'].selectedIndex].text;
        var jobArray = jobText.split(" - ");
        var jobName = jobArray[1];
        var hourNum = objHours.value; //document.Form1.elements['_ctl0:txtHours'].value;
        var hourType = objType.value; //document.Form1.elements['_ctl0:cmbHourType'].value;

        // added hourDescription to xml to display in grid
        var hourDescription = objType.options[objType.selectedIndex].text; //document.Form1.elements['_ctl0:cmbHourType'].options[document.Form1.elements['_ctl0:cmbHourType'].selectedIndex].text;

        if (validateRecord(workDate, empID, jobNum, hourNum, hourType)) {
            objXML.documentElement.appendChild(createTimeRecord(workDate, empID, empName, jobNum, jobName, hourNum, hourType, hourDescription));
            objJob.selectedIndex = 0; // document.Form1.elements['_ctl0:cmbJobNum'].selectedIndex = 0;
            objHours.value = 0; //document.Form1.elements['_ctl0:txtHours'].value = 0;
            // document.Form1.elements['_ctl0:cmbHourType'].selectedIndex = 0;
            displayTimeCard();
            objJob.focus(); //document.Form1.elements['_ctl0:cmbJobNum'].focus();
            return true;
        }
        else
            return false;
    }

    function createTimeRecord(workDate, empID, empName, jobNum, jobName, hourNum, hourType, hourDescription) {

        var timeNode = objXML.createElement("timeRecord");
        timeNode.appendChild(createElement("workDate", workDate));
        timeNode.appendChild(createElement("empID", empID));
        timeNode.appendChild(createElement("empName", empName));
        timeNode.appendChild(createElement("jobNum", jobNum));
        timeNode.appendChild(createElement("jobName", jobName));
        timeNode.appendChild(createElement("hourNum", hourNum));
        timeNode.appendChild(createElement("hourType", hourType));
        timeNode.appendChild(createElement("hourDescription", hourDescription));
        return timeNode;
    }
    function createElement(elementName, text) {
        var elementNode = objXML.createElement(elementName);
        var textNode = objXML.createTextNode(text);
        elementNode.appendChild(textNode);
        return elementNode;
    }
    function deleteRow(i) {
        var root = objXML.documentElement;
        root.removeChild(root.childNodes.item(i));
        root = null;
        displayTimeCard();
    }
    function displayTimeCard() {
        var strHTML = "<table><thead><th class='SubHead'>date</th><th class='SubHead'>emp id</th><th class='SubHead'>employee name</th>";
        strHTML += "<th class='SubHead'>jobnum</th><th class='SubHead'>job name</th><th class='SubHead'>hours</th><th class='SubHead'>type</th></thead>";

        var workDate = objXML.getElementsByTagName("workDate");
        var empID = objXML.getElementsByTagName("empID");
        var empName = objXML.getElementsByTagName("empName");
        var jobNum = objXML.getElementsByTagName("jobNum");
        var jobName = objXML.getElementsByTagName("jobName");
        var hourNum = objXML.getElementsByTagName("hourNum");
        var hourType = objXML.getElementsByTagName("hourType");
        var hourDescription = objXML.getElementsByTagName("hourDescription");
        var hourSum = 0;
        if (workDate.length > 0) {
            document.getElementById('btnSubmit').innerText = "submit values";
            document.getElementById('errMessage').innerText = "";
        }
        for (var i = 0; i < workDate.length; i++) {
            var rowColor;
            if (i / 2 == parseInt(i / 2)) {
                rowColor = "lightseagreen";
            }
            else {
                rowColor = "white";
            }
            if (workDate.item(i).text == undefined) {
                strHTML += "<tr bgColor='" + rowColor + "'><td class='normal'>" + workDate.item(i).textContent + "</td>";
                strHTML += "<td class='normal'>" + empID.item(i).textContent + "</td>";
                strHTML += "<td class='normal'>" + empName.item(i).textContent + "</td>";
                strHTML += "<td class='normal'>" + jobNum.item(i).textContent + "</td>";
                strHTML += "<td class='normal'>" + jobName.item(i).textContent + "</td>";
                strHTML += "<td class='normal' align='right'>" + hourNum.item(i).textContent + "</td>";
                strHTML += "<td class='normal'>" + hourDescription.item(i).textContent + "</td>";
                strHTML += "<td class='normal'><a href=# onclick='deleteRow(" + i + ")' >del</a>&nbsp;&nbsp;<a href=# onclick='editrow(" + i + ")' >edit</a></td></tr>";
                hourSum += Number(hourNum.item(i).textContent);
            }
            else {
                strHTML += "<tr bgColor='" + rowColor + "'><td class='normal'>" + workDate.item(i).text + "</td>";
                strHTML += "<td class='normal'>" + empID.item(i).text + "</td>";
                strHTML += "<td class='normal'>" + empName.item(i).text + "</td>";
                strHTML += "<td class='normal'>" + jobNum.item(i).text + "</td>";
                strHTML += "<td class='normal'>" + jobName.item(i).text + "</td>";
                strHTML += "<td class='normal' align='right'>" + hourNum.item(i).text + "</td>";
                strHTML += "<td class='normal'>" + hourDescription.item(i).text + "</td>";
                strHTML += "<td class='normal'><a href=# onclick='deleteRow(" + i + ")' >del</a>&nbsp;&nbsp;<a href=# onclick='editrow(" + i + ")' >edit</a></td></tr>";
                hourSum += Number(hourNum.item(i).text);
            }
        }
        strHTML += "<tr><td colspan='5'/><td><hr noshade/></td><td colspan='2'/></tr>"
        strHTML += "<tr><td class='subSubHead' colspan='5' align='right'>total hours:</td><td class='subSubHead'>" + hourSum + "</td><td/></tr>"
        strHTML += "<tr><td class='subSubHead' colspan='5' align='right'>row count:</td><td class='subSubHead'>" + i + "</td><td/></tr>"
        strHTML += "</table>";

        document.all.timeValues.innerHTML = strHTML;
        
        if (typeof window.XMLSerializer != 'undefined') {
            document.all.xmlTimeString.value = (new XMLSerializer()).serializeToString(objXML);
        }
        else {
            document.all.xmlTimeString.value = objXML.xml;
        }
        return true;
    }

    function editrow(i) {
        if (objXML.getElementsByTagName("workDate").item(i).text == undefined) {
            objDate.value = objXML.getElementsByTagName("workDate").item(i).textContent;
            objEmp.value = objXML.getElementsByTagName("empID").item(i).textContent;
            objJob.value = objXML.getElementsByTagName("jobNum").item(i).textContent;
            objHours.value = objXML.getElementsByTagName("hourNum").item(i).textContent;
            objType.value = objXML.getElementsByTagName("hourType").item(i).textContent;
        }
        else {
            objDate.value = objXML.getElementsByTagName("workDate").item(i).text;
            objEmp.value = objXML.getElementsByTagName("empID").item(i).text;
            objJob.value = objXML.getElementsByTagName("jobNum").item(i).text;
            objHours.value = objXML.getElementsByTagName("hourNum").item(i).text;
            objType.value = objXML.getElementsByTagName("hourType").item(i).text;
        }
        deleteRow(i);
    }

    function validateHours(c) {
        var h = Number(c.value);
        if (h != NaN & h > 0 & h < 16.25) {
            if (4 * h == parseInt(h * 4)) {
                return true;
            }
        }
        window.alert("Please enter a number between 0.25 and 16 in quarter hour increments.");
        return false;
    }
    function validateRecord(workDate, empID, jobNum, hourNum, hourType) {
        if (workDate != "" & empID != "" & jobNum != "" & hourNum != "" & hourType != "") {
            return true;
        }
        else {
            return false;
        }
    }

    function validateTimeCard() {
        if (document.Form1.xmlTimeString.value == null) {
            return false;
        }
        else {
            return true;
        }
    }

   

//-->
</script>

    </div>
    </form>
</body>
</html>

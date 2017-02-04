function JobList(c){
	var s, t, l;
	t = event.screenY + 10;
	l = event.screenX - 100;
	s = "joblist.aspx?control=" + c + "&jobnum=0";
	window.open(s,null, "left=" + l + ",top=" + t + ",width=450,height=200,toolbar=no,location=no,scrollbars=yes");
	return true;
	}
function ClientList(c){
	var s, t, l;
	t = event.screenY + 10;
	l = event.screenX - 100;
	
	s = "clientlist.aspx?control=" + c;
	window.open( s, null, "left=" + l + ",top=" + t + ",width=450,height=200,toolbar=no,location=no,scrollbars=yes");
	return true;
	}
function CalPop(c){
	var s, t, l;
	t = event.screenY + 10;
	l = event.screenX - 200;
	s = "../calpop.aspx?tbm=" + c;
	window.open( s, null,"left=" + l + ",top=" + t + ",width=220, height=220,toolbar=no,location=no");
	return true;
	}
function CreateClient(){
	window.open( "client.aspx", null, "width=500, height=500, scrollbars=yes, resizable=yes, toolbar=no, location=no, status=yes");
	}
function UpdateClient(){
	window.open("client.aspx?clientid=" + window.document.getElementById('txtClientID').value, null,"width=500, height=500, toolbar=no, resizable=yes, scrollbars=yes, location=no, status=yes");
	}
function registerJobTotalControl(c){
	jobTotalControl = c;
	alert("Hello!");
	}
function updateTotal(){
	var jraTotal = parseFloat(document.Form1.elements['txtJRATotal'].value);
	var subcontractorTotal = parseFloat(document.Form1.elements['txtSubcontractorTotal'].value);
	var honorariaTotal = parseFloat(document.Form1.elements['txtHonorariaTotal'].value);
	
	document.Form1.elements['txtJobTotal'].value = jraTotal + subcontractorTotal + honorariaTotal;
	}
	
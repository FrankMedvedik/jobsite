var toFind = "";              // Variable that acts as keyboard buffer 
var oControl = "";            // Maintains a global reference to the  
var foundIndex = 0;			  // keep track of where the last match was found.   
var previousIndex = 0;  
          
function textbox_onkeydown(){
	oControl = window.event.srcElement;
	var keycode = window.event.keyCode;
	if(keycode == 13){ //ENTER KEY
		tabNext(oControl);
		event.returnValue=false;
		}
	}
	
function listbox_onkeydown(){ 

   oControl = window.event.srcElement; 

   var keycode = window.event.keyCode; 
  
   if(keycode == 27){// ESC key
		toFind = "";
		window.status = "";
		foundIndex = 0;
		return false;
	}
	else if (keycode == 13){ //ENTER key
		toFind = "";
		status = "";
		foundIndex = 0;
		tabNext(oControl);
		}
    else if (keycode == 8 || keycode == 37){ // BACKSPACE key
		
		toFind = toFind.substring(0,toFind.length -1);
		if (toFind == ""){
			foundIndex = 0;
			}
		status = "looking for " + toFind;
		oControl.selectedIndex = foundIndex;
		event.returnValue=false;
		
	}
	else if (keycode == 40){ //DOWN Arrow key
		if (document.all){ 
			foundIndex = ((oControl.selectedIndex + 1) > oControl.options.length-1) ? oControl.options.length-1:oControl.selectedIndex+1;	
			oControl.selectedIndex = foundIndex;
			}
			else {
			foundIndex += 1;
			toFind = "";
			window.status="";
			}
			event.returnValue=false;
		}
	 else if (keycode == 38){ //UP Arrow key
	 	if (document.all){ 
			foundIndex = ((oControl.selectedIndex - 1) == -1) ? 0:oControl.selectedIndex-1;
			oControl.selectedIndex = foundIndex;
			}
			else {
			foundIndex = oControl.selectedIndex -1;
			toFind = "";
			window.status="";
			}
			event.returnValue=false;
		}
		
	else if(keycode >= 32){ // & keycode != 37 ){ 
       // What character did the user type? 
		var c;
		if (keycode == 96){
			c = 0;
			}
		else if (keycode == 97){
			c = 1;
			}
		else if (keycode == 98){
			c = 2;
			}
		else if (keycode == 99){
			c = 3;
			}
		else if (keycode == 100){
			c = 4;
			}
		else if (keycode == 101){
			c = 5;
			}
		else if (keycode == 102){
			c = 6;
			}
		else if (keycode == 103){
			c = 7;
			}
		else if (keycode == 104){
			c = 8;
			}
		else if (keycode == 105){
			c = 9;
			}
		else {
			c = String.fromCharCode(keycode); 
			c = c.toUpperCase();  
			}
     
       
     
       // Convert it to uppercase so that comparisons don't fail 
       toFind += c ; // Add to the keyboard buffer
       // timeoutInterval = 350; 
       window.status = "looking for " + toFind;
	   find();    // Search the listbox 
	   
	   // if the search string doesn't match warn the client
	    
	   var sVal = oControl.options[oControl.selectedIndex].text.toUpperCase();
	   var sLength = toFind.length;
	   if (toFind != sVal.substr(0, sLength)){
		status += " NOT FOUND - left arrow key or escape to continue";
		return true;
		}
    } 
} 

function listbox_onblur(){ 
   resetToFind(); 
} 

function resetToFind(){ 
   toFind = "" ;
   window.status = "";
} 

function find(){ 
    // Walk through the select list looking for a match 

    var allOptions = document.all.item(oControl.id); 
	
    for (i=0; i < allOptions.length; i++){ 
       // Gets the next item from the listbox 
       nextOptionText = allOptions(i).text.toUpperCase(); 
       lenStr = toFind.length;
		
       // Does the next item match exactly what the user typed? 
           
        if(toFind == nextOptionText.substr(0,lenStr)){    
            // OK, we can stop at this option. Set focus here 
            oControl.selectedIndex = i; 
            foundIndex = i;
            window.event.returnValue = false; 
            break; 
        } 
			oControl.selectedIndex = foundIndex;
			window.event.returnValue = false;
       
    }  // for 
} // function 

	
function getElementIndex(obj) {
	var theform = obj.form;
	for (var i=0; i<theform.elements.length; i++) {
		if (obj.name == theform.elements[i].name) {
			return i;
			}
		}
	return -1;
	}

function tabNext(obj) {
	if (navigator.platform.toUpperCase().indexOf("SUNOS") != -1) {
		obj.blur(); return; // Sun's onFocus() is messed up
		}
	var theform = obj.form;
	var i = getElementIndex(obj);
	var j=i+1;
	if (j >= theform.elements.length) { j=0; }
	if (i == -1) { return; }
	while (j != i) {
		if ((theform.elements[j].type!="hidden") && 
		    (theform.elements[j].name != theform.elements[i].name) && 
			(!theform.elements[j].disabled) &&
			(!theform.elements[j].readonly)) {
			try{
			theform.elements[j].focus();
			break;
			}
			catch(e){}
			}
		j++;
		if (j >= theform.elements.length) { j=0; }
		}
	}

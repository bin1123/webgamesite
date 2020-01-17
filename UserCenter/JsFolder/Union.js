document.domain = "dao50.com"; 
function getBoolUser() {
    var str = document.getElementById("userName").value;
    var name = document.getElementById("Name");
    if (verName(str)) {
        var xmlhttp = getHTTPObject();
        var url = "http://game.dao50.com/Services/Ajax.ashx?AjaxType=ValName&Account=" + str;
        xmlhttp.open("POST", url, true);
        xmlhttp.onreadystatechange = function() {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                var sss = xmlhttp.responseText;
                if (sss == 0) {
                    name.innerHTML = "<span class=\"zq\"></span>";
                    return false;
                }
                else
                    if (sss != 0) {
                    name.innerHTML = "<span class=\"cw\">用户已存在</span>";
                    return true;
                }
            }
        }
        xmlhttp.send(null);
    }
}

function getHTTPObject() {
    var oHttpReq = null;
    if (window.ActiveXObject)
        oHttpReq = new ActiveXObject("MSXML2.XMLHTTP");
    else if (window.createRequest)
        oHttpReq = window.createRequest();
    else
        oHttpReq = new XMLHttpRequest();
    return oHttpReq;
}
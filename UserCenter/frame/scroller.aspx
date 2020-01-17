<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="scroller.aspx.cs" Inherits="UserCenter.frame.scroller" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title></title>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            background: #000000;
            cursor: E-resize;
        }
    </style>
    <script language="JavaScript" type="text/javascript">
<!--
        var pic = new Image();
        pic.src = "<%=sWebUrl %>/wldFolder/ljzl/nav_right.jpg";

        function toggleMenu() {
            frmBody = parent.document.getElementById('frame-body');
            imgArrow = document.getElementById('img');

            if (frmBody.cols == "0, 10, *") {
                frmBody.cols = "110, 10, *";
                imgArrow.src = "<%=sWebUrl %>/wldFolder/ljzl/nav_left.jpg";
            }
            else {
                frmBody.cols = "0, 10, *";
                imgArrow.src = "<%=sWebUrl %>/wldFolder/ljzl/nav_right.jpg";
            }
        }

        var orgX = 0;
        document.onmousedown = function(e) {
            var evt = Utils.fixEvent(e);
            orgX = evt.clientX;

            if (Browser.isIE) document.getElementById('tbl').setCapture();
        }

        document.onmouseup = function(e) {
            var evt = Utils.fixEvent(e);

            frmBody = parent.document.getElementById('frame-body');
            frmWidth = frmBody.cols.substr(0, frmBody.cols.indexOf(','));
            frmWidth = (parseInt(frmWidth) + (evt.clientX - orgX));

            frmBody.cols = frmWidth + ", 10, *";

            if (Browser.isIE) document.releaseCapture();
        }

        var Browser = new Object();

        Browser.isMozilla = (typeof document.implementation != 'undefined') && (typeof document.implementation.createDocument != 'undefined') && (typeof HTMLDocument != 'undefined');
        Browser.isIE = window.ActiveXObject ? true : false;
        Browser.isFirefox = (navigator.userAgent.toLowerCase().indexOf("firefox") != -1);
        Browser.isSafari = (navigator.userAgent.toLowerCase().indexOf("safari") != -1);
        Browser.isOpera = (navigator.userAgent.toLowerCase().indexOf("opera") != -1);

        var Utils = new Object();

        Utils.fixEvent = function(e) {
            var evt = (typeof e == "undefined") ? window.event : e;
            return evt;
        }
//-->
</script>
</head>
<body onselect="return false;">
    <table height="100%" cellspacing="0" cellpadding="0" id="tbl">
        <tbody>
            <tr>
                <td>
                    <a title="打开/关闭" href="javascript:toggleMenu();">
                        <img width="10" height="30" border="0" id="img" src="<%=sWebUrl %>/wldFolder/ljzl/nav_left.jpg"></a>
                </td>
            </tr>
        </tbody>
    </table>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="noreglogin.aspx.cs" Inherits="UserCenter.Services.noreglogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type"/>
<meta http-equiv="refresh" content="<%=iSecond %>;URL=noregreg.aspx?gn=<%=sGameName %>"/>
<title>快速游戏</title>
<style type="text/css">
body, html {width:100%;height:100%;margin:0;padding:0;background-color:Black}
</style>
</head>
<body>
<div style="width:100%; height:100%; overflow:hidden;" id="center">
<iframe width="100%" scrolling="no" height="100%" frameborder="0" name="mainFrame" src="<%=sUrl %>">
</iframe>
</div>
</body>
</html>
<div style="display:none;" ><script src="http://s14.cnzz.com/stat.php?id=5365358&web_id=5365358" language="JavaScript"></script></div>
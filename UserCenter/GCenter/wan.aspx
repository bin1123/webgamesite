<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wan.aspx.cs" Inherits="UserCenter.GCenter.wan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type"/>
<title><%=sTitle %></title>
<style type="text/css">
body, html {width:100%;height:<%=sHeight%>;margin:0;padding:0;background-color:Black}
</style>
</head>
<body>
<div style="width:100%; height:100%; overflow:hidden;" id="center">
<iframe width="100%" scrolling="no" height="100%" frameborder="0" name="mainFrame" src="../frame/g_mainframe_<%=sGame %>.aspx<%=sQueryString %>">
</iframe>
</div>
</body>
</html>

﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_mainframe_noreg.aspx.cs" Inherits="UserCenter.frame.g_mainframe_noreg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html>
<frameset framespacing="0" border="0" frameborder="no" rows="27,*" name="f2">
<frame scrolling="no" noresize="noresize" name="topFrame" src="g_top_nav_l.aspx"></frame>
<frameset framespacing="0" border="0" frameborder="no" cols="140, 10, *" rows="*" name="f3" id="frame-body">
	<frame scrolling="no" noresize="noresize" name="leftFrame" src="g_left_nav_noreg.aspx?gn=<%=sGame %>"></frame>
	<frame scrolling="no" noresize="noresize" name="leftFrame" src="scroller.aspx"></frame>
	<frame scrolling="auto" noresize="noresize" id="mainFrame" name="mainFrame" src="../GCenter/ToGame.aspx?gn=<%=sGame %>"></frame>
</frameset>
</frameset>
<noframes>
<body> 
很抱歉，馈下使用的浏览器不支持框架功能，请转用其他浏览器。 
</body>
</noframes>
</html>

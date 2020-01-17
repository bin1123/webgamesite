<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_mainframe_gcld.aspx.cs" Inherits="UserCenter.frame.g_mainframe_gcld" %>
<html>
<frameset framespacing="0" border="0" frameborder="no" rows="27,*" name="f2">
<frame scrolling="no" noresize="noresize" name="topFrame" src="g_top_nav_l.aspx"></frame>
<frameset framespacing="0" border="0" frameborder="no" cols="100, 10, *" rows="*" name="f3" id="frame-body">
	<frame scrolling="no" noresize="noresize" name="leftFrame" src="g_left_nav_gcld.html"></frame>
	<frame scrolling="no" noresize="noresize" name="leftFrame" src="scroller.aspx"></frame>
	<frame scrolling="auto" noresize="noresize" id="mainFrame" name="mainFrame" src="../GCenter/ToGame.aspx?gn=<%=sGame %>"></frame>
</frameset>
</frameset>
<noframes></noframes>
</html>

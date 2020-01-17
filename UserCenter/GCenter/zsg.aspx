<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zsg.aspx.cs" Inherits="UserCenter.GCenter.zsg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>战三国登陆页面</title>
    <link href="<%=sWebUrl %>/wldFolder/zsgcl/style/master.css" rel="stylesheet" type="text/css" />
</head>
<body scroll="no" style="background-color:#000;overflow:hidden">
<div class="layout">
  <div class="menu"> 
  	<a href="http://www.dao50.com/yxzq/zsg/" target="_blank" class="m1"></a> 
    <a href="http://game.dao50.com/pay/" target="_blank" class="m2"></a> 
    <a href="http://www.dao50.com/news/zsg_yxzl/" target="_blank" class="m3"></a> 
    <a href="http://bbs.dao50.com/showforum-134.aspx" target="_blank" class="m4"></a> 
 </div>
  <div class="login_info">
    <div class="hd">
      <ul class="state_tip">
        <li> <span class="state_icon state0"></span>火爆 </li>
        <li> <span class="state_icon state1"></span>正常 </li>
        <li> <span class="state_icon state2"></span>维护 </li>
      </ul>
      <span class="user_info"><%=sAccount %></span>&nbsp;<a href="/Services/userexit.aspx" class="logout">注销</a> </div>
    <div class="bd">
    	<div class="recent_server"><span class="tit">最近登录服务器：</span><a href="#" id="zjdlurl" class="server_block"><span class="state_icon state0" id="zjdlname"></span></a><a href="#" id="start" class="btn_start"></a></div>
    </div>
  </div>
  <div class="server_list">
    <div class="hd">
      <ul class="serverTab">
        <li id="tjfwli" onmouseover="ServerTJSel()" class="on">推荐服务器</li>
        <li id="allfwli" onmouseover="ServerAllSel()">全部服务器</li>
      </ul>
    </div>
    <div class="bd">
      <ul id="tjfw" class="serverContent rec_server" >
      </ul>
      <ul id="allfw" class="serverContent all_server" style="display:block;">
      </ul>
    </div>
  </div>
</div>
</body>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/zsgcl.js"></script>
<script type="text/javascript">
    $(document).ready(function() {
        var account = '<%=sAccount %>';
        ServerLastSel(account);
        ServerSel();
    });
</script>
</html>
<%=sMsg %>
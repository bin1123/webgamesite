<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sjsgcl.aspx.cs" Inherits="UserCenter.GCenter.sjsgcl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>神将三国</title>
    <link href="http://file.dao50.com/sjsgl/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
</head>
<body bgcolor="#fefefe">
<div class="main">
  <div class="box fwq">
    <div class="nav"><a href="http://www.dao50.com/yxzq/sjsg" target="_blank">官网</a> <a href="http://bbs.dao50.com/showforum-126.aspx" target="_blank">论坛</a> <a href="/Pay/" target="_blank">充值</a> <a href="http://bbs.dao50.com/showforum-129.aspx" target="_blank">客服</a></div>
    <ul class="log_info">
      <li><a class="change" href="/Services/userexit.aspx">更换账号</a><%=sAccount %></li>
      <li><a id="zjdl" href="#" onclick="LoadGame(this.href);return false;">无登陆记录</a></li>
      <li><a id="zxkf" class="new" href="sjsg.aspx?gn=sjsg1" onclick="LoadGame(this.href);return false">1服_三国伊始</a></li>
    </ul>
    <div class="fwq_list">
      <h3><a class="on" id="tjf" href="#" onclick="ServerTJSel();return false;">推荐服</a> <a href="#" id="syf" onclick="ServerAllSel();return false;">所有服</a></h3>
      <div class="list_cont">
        <ul id="sjsgsever">
        </ul>
      </div>
    </div>
    <div class="close" onclick="location.href = 'app://exitgame';"></div>
    <div class="small" onclick="location.href = 'app://minimize';"></div>
  </div>
</div><input type="hidden" id="qbyx"/><input type="hidden" id="tjyx"/>
</body>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/sjsgcl.js"></script>
<script type="text/javascript">
    $(document).ready(function() {
        var account = '<%=sAccount %>';
        ServerLastSel(account);
        ServerSel();
    });
</script>
</html>

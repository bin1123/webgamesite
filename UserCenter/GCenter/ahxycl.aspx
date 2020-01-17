<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ahxycl.aspx.cs" Inherits="UserCenter.GCenter.ahxycl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>暗黑西游登陆器</title>
    <link href="http://file.dao50.com/ahxycl/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
</head>
<body>
  <div class="box">
    <div class="fwq">
      <ul class="log_info">
        <li><a class="change" href="/Services/userexit.aspx">更换账号</a><%=sAccount %></li>
        <li><a id="zxkf" href="ahxy.aspx?gn=ahxy1">1服_零零一服</a></li>
        <li><a id="zjdl" href="#"></a></li>
      </ul>
      <div class="fwq_list">
        <h3><a class="on" id="tjf" href="#" onclick="ServerTJSel();return false;">最新服</a> <a href="#" id="syf" onclick="ServerAllSel();return false;">所有服</a></h3>
        <div class="list_cont">
          <ul id="ahxysever">
          </ul>
        </div>
      </div>
      <div class="close" onclick="location.href = 'app://exitgame';"></div>
      <div class="small" onclick="location.href = 'app://minimize';"></div>
    </div>
  </div>
<input type="hidden" id="qbyx"/><input type="hidden" id="tjyx"/>
</body>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/ahxycl.js"></script>
<script type="text/javascript">
    $(document).ready(function() {
        var account = '<%=sAccount %>';
        ServerLastSel(account);
        ServerSel();
    });
</script>
</html>

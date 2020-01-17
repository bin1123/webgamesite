<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ahxyLogin.aspx.cs" Inherits="UserCenter.GCenter.ahxyLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>暗黑西游微端登陆</title>
    <link href="http://file.dao50.com/ahxycl/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
</head>
<body>
  <div class="box">
    <div class="login">
      <form action="ahxyLogin.aspx" method="post" onsubmit="return loginVal()" id="form1" class="log">
        <ul>
          <li>
            <label>用户名：</label>
            <input type="text" class="text1" id="username" name="username" maxlength="16">
          </li>
          <li>
            <label>密码：</label>
            <input type="password" class="text1" id="pwd" name="pwd" maxlength="16">
          </li>
         
          <li>
            <input type="submit" value=""  class="log_btn">
          </li>
          <li class="links"><a href="http://game.dao50.com/UCenter/searchPass.aspx" target="_blank">忘记密码?</a>|<a href="http://game.dao50.com/UCenter/reg.aspx" target="_blank">注册账号</a></li>
        </ul>
      </form>
    </div>
    <div class="btn"><a href="http://www.dao50.com/yxzq/ahxy/" target="_blank" class="b1">进入官网</a>
    <a href="http://bbs.dao50.com/" target="_blank" class="b2">游戏论坛</a>
    <a href="http://www.dao50.com/news/ahxy_yxzl/" target="_blank" class="b3">游戏资料</a>
    <a href="http://bbs.dao50.com/showforum-176.aspx" target="_blank" class="b4">在线客服</a>
    <a href="http://game.dao50.com/pay/" target="_blank" class="b5">充值中心</a></div>
    <div class="news">
      <ul>
        <li><span>11/29</span>·<a href="http://www.dao50.com/news/20131125/n017620870.html" target="_blank">暗黑西游震撼上线</a></li>
        <li><span>11/29</span>·<a href="http://www.dao50.com/news/ahxy_yxhd/" target="_blank">梦幻飞仙与您迎接6月激情盛夏</a></li>
        <li><span>11/29</span>·<a href="http://www.dao50.com/news/ahxy_yxgg/" target="_blank">暗黑西游最新动态</a></li>
      </ul>
    </div>
    <div class="close" onclick="location.href = 'app://exitgame';"></div>
    <div class="small" onclick="location.href = 'app://minimize';"></div>
  </div>
</body>
<script type="text/javascript">
    function loginVal() {
        var username = $("#username").val();
        var password = $("#pwd").val();
        if (username == "" || username == "请输入用户名") {
            alert("请输入用户名");
            return false;
        }
        else if (password == "") {
            alert("密码不能为空");
            return false;
        }
        else if (username.length < 4 || username.length > 16) {
            alert("用户名格式不正确!");
            return false;
        }
        else if (password.length < 6 || password.length > 16) {
            alert("密码格式不正确!");
            return false;
        }
        else {
            return true;
        }
    } 
</script>
</html>
<%=sMsg %>
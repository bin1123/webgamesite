<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sjsgLogin.aspx.cs" Inherits="UserCenter.GCenter.sjsgLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>神将三国登陆页</title>
    <link href="http://file.dao50.com/sjsgl/main.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
</head>
<body bgcolor="#fefefe">
<div class="main">
  <div class="box">
    <div class="login">
      <form action="sjsgLogin.aspx" method="post" id="form1" onsubmit="return loginVal()" class="log">
        <ul>
          <li>
            <label>用户名：</label>
            <input type="text" class="text" id="username" name="username" maxlength="16">
          </li>
          <li>
            <label>密码：</label>
            <input type="password" class="text" id="pwd" name="pwd" maxlength="16">
          </li>
          <li class="auto">
            <span class="check"></span>自动登录 | <a href="http://game.dao50.com/UCenter/searchPass.aspx" target="_blank">忘记密码？</a>
          </li>
          <li class="ce">
			<a href="http://game.dao50.com/UCenter/reg.aspx" target="_blank">注册</a>
			<input class="text3" type="submit" name="button" id="button" value=" "/>
          </li>
        </ul>
      </form>
    </div>
    <div class="nav"><a href="http://www.dao50.com/yxzq/sjsg" target="_blank">官网</a> <a href="http://bbs.dao50.com/showforum-126.aspx" target="_blank">论坛</a> <a href="/Pay/" target="_blank">充值</a> <a href="http://bbs.dao50.com/showforum-129.aspx" target="_blank">客服</a></div>    
    <div class="news">
      <a class="more" href="http://www.dao50.com/yxzq/sjsg/" target="_blank">更多</a>
      <ul>
        <li><span>03/13</span><a href="http://www.dao50.com/news/sjsg_xwgg/" target="_blank">到武林《神将三国》最新开服新闻</a></li>
        <li><span>03/13</span><a href="http://www.dao50.com/news/sjsg_hdgg/" target="_blank">到武林《神将三国》精彩活动</a></li>
        <li><span>03/13</span><a href="http://www.dao50.com/news/201338/n06228014.html" target="_blank">到武林《神将三国》火热上线</a></li>
      </ul>
    </div>
    <div class="close" onclick="location.href = 'app://exitgame';"></div>
    <div class="small" onclick="location.href = 'app://minimize';"></div>
  </div>
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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sq2cl.aspx.cs" Inherits="UserCenter.GCenter.sq2cl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>神曲II登陆页</title>
    <link rel="stylesheet" href="http://file.dao50.com/sq2cl/css/normalize.css?ver=1385716904216" type="text/css" />
    <link rel="stylesheet" href="http://file.dao50.com/sq2cl/css/sq-client.css?ver=1385716904216" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript">
        function loginVal() {
            var username = $("#account").val();
            var password = $("#pwdone").val();
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
</head>
<body>
  <div class="doc sign-in"><!-- Document start -->	
    <div class="container">
      <div class="news clearfix"><!-- News start -->
        <div class="hd"><h1 class="title">新闻公告</h1></div>
        <div class="bd">
          <ul class="news-list">
                <li class="hot">
                  <a href="http://www.dao50.com/news/sq_yxzl/index.html" title="到武林《神曲II》最新资料" target="_blank">到武林《神曲II》最新资料</a>
                  <span class="date">[2013-12-03]</span>
                </li>
                <li class="hot">
                  <a href="http://www.dao50.com/news/sq_hdzx/index.html" title="到武林《神曲II》精彩活动" target="_blank">到武林《神曲II》精彩活动</a>
                  <span class="date">[2013-12-03]</span>
                </li>
                <li class="hot">
                  <a href="http://www.dao50.com/news/2013122/n883821380.html" title="到武林《神曲II》版本内容" target="_blank">到武林《神曲II》版本内容</a>
                  <span class="date">[2013-12-03]</span>
                </li>
                <li>
                  <a href="http://www.dao50.com/news/2013122/n688821381.html" title="到武林《神曲II》新手FAQ" target="_blank">到武林《神曲II》新手FAQ</a>
                  <span class="date">[2013-12-03]</span>
                </li>
                <li>
                  <a href="http://www.dao50.com/news/sq_xwgg/index.html" title="到武林《神曲II》最新动态" target="_blank">到武林《神曲II》最新动态</a>
                  <span class="date">[2013-12-03]</span>
                </li>
          </ul>
        </div>
      </div><!-- News end -->
      <div class="signin clearfix"><!-- signin start -->
        <form id="signin" action="sq2cl.aspx" method="post" onsubmit="return loginVal()">
          <div class="signin-l">
            <div class="field username-field">
              <label class="title" for="username">用户名：</label>
              <div class="username"><input name="account" id="account" tabindex="1" type="text" maxlength="20" /></div>
            </div>
            <div class="field password-field">
              <label class="title" for="password">密码：</label>
              <div class="password"><input name="pwdone" id="pwdone" tabindex="2" type="password" maxlength="20" /></div>
            </div>
            <div class="remember-field">
            </div>    
          </div>
          <div class="signin-r">
            <div class="submit-field clearfix">
              <input type="submit" id="submit" tabindex="3" class="normal" value="" />
              <a href="http://game.dao50.com/UCenter/reg.aspx" target="_blank" class="fl">注册帐号</a>
              <a href="http://game.dao50.com/UCenter/searchPass.aspx" target="_blank" class="fr">找回密码</a>
            </div>
          </div>
        <input type="hidden" value="login" name="Type"/>
        </form>
      </div><!-- signin end -->
    </div>
  </div><!-- Document end -->
</body>
</html>
<%=sMsg %>
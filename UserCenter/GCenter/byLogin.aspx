<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="byLogin.aspx.cs" Inherits="UserCenter.GCenter.byLogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>霸域登陆页</title>
    <link href="http://file.dao50.com/byl/style/css.css" rel="stylesheet" type="text/css" />
</head>
    <!--[if IE 6]>
    <script type="text/javascript" src="http://file.dao50.com/byl/js/DD_belatedPNG_0.0.8a-min.js"></script>
    <script type="text/javascript">
    window.attachEvent("onload", function(){
       DD_belatedPNG.fix("div, a ,em");
    });
    </script>
    <![endif]-->
<body>
   <div class="c_box">
         <!--新闻-->
         <div class="cont_box">
             <div class="news fl">
               <div class="title ti_9">新闻中心</div>
               <ul>
                  <li><a href="http://www.dao50.com/news/by_xwgg/" target="_blank"><em class="ico ti_9 fl">&raquo;</em>【新闻】到武林《霸域》最新开服公告</a></li>
                  <li><a href="http://www.dao50.com/news/2013617/n739911463.html" target="_blank"><em class="ico ti_9 fl">&raquo;</em>【活动】到武林《霸域》通服礼包</a></li>
                  <li><a href="http://www.dao50.com/news/2013524/n108510581.html" target="_blank"><em class="ico ti_9 fl">&raquo;</em>【活动】到武林《霸域》开服活动</a></li>
                  <li><a href="http://www.dao50.com/news/by_hdgg/ " target="_blank"><em class="ico ti_9 fl">&raquo;</em>【活动】到武林《霸域》最新活动详情</a></li>
               </ul> 
             </div>
             <!--图片-->
             <div class="img_box fr"><a href="http://www.dao50.com/news/2013513/n56699790.html" target="_blank"><img src="http://file.dao50.com/byl/images/xc.jpg"></a></div><!--宣传图片-->
             <div class="clear"></div>
         </div>
         <form id="loginform1" action="byLogin.aspx" name="loginform" method="post">
             <table class="login">
                <tr class="use">
                  <td><span class="ti_9">用户：</span></td>
                  <td><div class="inp_bg"><input id="username" name="username" type="text" tabindex="1"  maxlength="16"/></div></td>
                  <td rowspan="2"><a href="#" onclick="submit_form();return false;" class="ti_9 start ml20">开始游戏</a></td>
                </tr>
                <tr class="pwd">
                  <td><span class="ti_9">密码：</span></td>
                  <td><div class="inp_bg"><input  name="pwd" id="pwd" type="password" tabindex="2" maxlength="16"/></div></td>
                </tr>
              </table>
          </form>
         <div class="info">
            <div class="fl" style="width:200px;"><input type="checkbox" checked="checked"/><a class="rem" href="#">记住我的状态</a></div>
            <div class="fr"><a href="byreg.aspx">注册账号</a>|<a href="http://game.dao50.com/UCenter/searchPass.aspx" target="_blank">找回密码</a></div>
            <div class="clear"></div>
          </div>
      </div>
</body>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript">
    function submit_form() {
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
            document.getElementById("loginform").submit();
            return true;
        }
    }
</script>
</html>
<%=sMsg %>
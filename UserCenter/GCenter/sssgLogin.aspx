<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/PageBase/GameLogin.cs" Inherits="UserCenter.PageBase.GameLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>盛世三国登陆页</title>
    <link href="<%=sWebUrl %>/yxzq/sssg/yxzq_sssg/css/sg.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type ="text/javascript" src="<%=sRootUrl %>/JsFolder/Game.js"></script>
    <script type="text/javascript">
        function serverShow() {
            $("#servercheck").show();
        }

        function serverHid() {
            $("#servercheck").hide();
        }

        function dataval() {
            var username = $("#account").val();
            var password = $("#password").val();
            if (username == "") {
                alert("账号不能为空");
                return false;
            }
            if (password == "") {
                alert("密码不能为空");
                return false;
            }
            if (username.length < 4 || username.length > 16) {
                alert("用户名格式不正确!");
                return false;
            }
            if (password.length < 6 || password.length > 16) {
                alert("密码格式不正确!");
                return false;
            }
            form1.submit();
        }
        
        $(document).ready(function() {
            var account = '<%=sAccount %>';
            if (account != '') {
                var gameabbre = 'sssg';
                var serverabbre = $("#gameabbre").val();
                GameOfServerSelWebSSSG(gameabbre, '5');
                ServerLastSelWeb(4, 'lastplay', account);
                //$("#login").hide();
                $("#login").remove();
                $("#logined").show();
            }
            else {
                $("#logined").remove();
                $("#login").show();
            }
        });
    </script>
</head>
<body>
    <div id="login" class="denglu0"  style="display:none">
        <form id="form1" method="post" action="../PageBase/GameLogin.aspx">
            <span>用户名<input type="text" id="account" name="account" class="tx"/>
            </span>
            <span>密<font color="#2f1e16">密</font>码<input type="password" id="password" name="pwd" class="tx" /></span>
            <span><input type="button" onclick="dataval()" class="btn" /></span>
        </form>
    </div>
    <div id="logined" class="denglu0">
        <ul>
        <li><img src="<%=sWebUrl %>/yxzq/sssg/yxzq_sssg/images/hy.jpg" /><%=sAccount %></li>
        <li>继续上次的游戏：</li>
        <li><center><a href="#" target="_blank" id="lastplay">1服_唯我独尊</a></center></li>
        <li>【<a href="http://game.dao50.com/pay/" target="_blank">充值</a>】【<a href="http://game.dao50.com/xsk" target="_blank">领取新手卡</a>】【<a href="<%=sRootUrl %>/Services/userexit.aspx">退出</a>】</li>
        <li><a href="#" onclick="serverShow()"><img src="<%=sWebUrl %>/yxzq/sssg/yxzq_sssg/images/xzfwq.jpg" alt="游戏专区服务器"/></a></li></ul>
    </div>
    <div class="kfzx">
    <span class="mr">玩家交流群：<font color="#a27028">175393719</font></span>
    <span>客服QQ：<font color="#a27028">2361245014</font></span>
    <span class="mb">客服热线：<font color="#a54613">400 008 5267</font></span>
    <span>在线客服：<font color="#a27028">2460929679</font></span>
    </div>
    <div class="yxgg"></div>
    <div id="servercheck" onmouseover="serverShow()" onmouseout="serverHid()" style="display:none" class="xzfwq">
        <a id="sssg1" href="http://game.dao50.com/GCenter/PlayGame.aspx?gn=sssg1" target="_blank">1服_唯我独尊</a>
    </div>
</body>
<%=sMsg %>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/PageBase/GameLogin.cs" Inherits="UserCenter.PageBase.GameLogin" %>

<!doctype html public "-//w3c//dtd xhtml 1.0 transitional//en" "http://www.w3.org/tr/xhtml1/dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>神仙道登陆页</title>
    <link rel="stylesheet" type="text/css" href="<%=sWebUrl %>/wldFolder/css/reset.css" />
    <link rel="stylesheet" type="text/css" href="<%=sWebUrl %>/wldFolder/css/sxd.css" />

    <script type="text/javascript" src="<%=sRootUrl %>/jsfolder/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="<%=sRootUrl %>/jsfolder/game.js"></script>

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
                var gameabbre = 'sxd';
                var serverabbre = $("#gameabbre").val();
                GameOfServerSelWebSXD(gameabbre,'4');
                ServerLastSelWeb(5, 'lastplay', account);
                ServerNewSel2(5, 'newplay')
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
<body style="background: none;">
    <!--登陆前-->
    <form id="form1" method="post" action="../PageBase/GameLogin.aspx">
    <div id="login" class="no_login">
        <a class="login_button_head" href="http://game.dao50.com/GCenter/PlayGame.aspx?gn=sxd1"
            target="_blank">开始游戏</a>
        <div class="user_name">
            <span>帐号：</span>
            <input type="text" id="account" name="account" />
        </div>
        <div class="password">
            <span>密码：</span>
            <input type="password" id="password" name="pwd" />
        </div>
        <div class="two_button">
            <div class="checkbox">
                <input type="checkbox" name="save_cookie" /><label for="save_cookie">自动登录</label>
            </div>
            <input style="padding-bottom: 5px" onclick="return dataval()" class="button_mode"
                value="登录" type="submit" />
            <div class="button_mode reg">
                <a href="http://game.dao50.com/UCenter/reg.aspx" style="color: #FFFFFF;" target="_blank">
                    注册</a></div>
        </div>
        <div class="error_paw">
            <span class="error"></span><a class="forget_paw" href="http://game.dao50.com/UCenter/searchPass.aspx"
                target="_blank">忘记密码？</a>
        </div>
    </div>
    </form>
    <div id="logined" class="yes_login" style="display: none">
        <!--登陆后-->
        <div class="welcome">
            欢迎：<span><%=sAccount %></span>
        </div>
        <div class="which_play">
            <span>服务器推荐:</span><br>
            <span><a href="http://game.dao50.com/GCenter/PlayGame.aspx?gn=sxd20" target="_blank"
                id="newplay">20服_九天十地</a></span><br />
            <span>继续上次游戏:</span><br>
            <span><a href="http://game.dao50.com/GCenter/PlayGame.aspx?gn=sxd1" target="_blank"
                id="lastplay">1服_洞天福地</a></span>
        </div>
        <div id="servercheck" class="which_play1" onmouseover="serverShow()" onmouseout="serverHid()"
            style="display: none">
            <span><a href="http://game.dao50.com/GCenter/PlayGame.aspx?gn=sxd1" target="_blank">
                1服_洞天福地</a></span>
        </div>
        <a class="start_game" href="#" onclick="serverShow()">立即开始游戏</a>
        <div class="login_option">
            <span>[<a href="http://game.dao50.com/UCenter/UPWordM.aspx" target="_blank">修改密码</a>]</span>
            <span>[<a href="http://game.dao50.com/pay/" target="_blank">充值</a>]</span> <span
                style="font-weight: bold">[<a href="<%=sRootUrl %>/Services/userexit.aspx">退出登录</a>]</span>
        </div>
    </div>
    <div class="xsk">
        <a class="code_button" href="http://game.dao50.com/xsk/" target="_blank">新手卡</a>
    </div>
    <div class="health_play">
        <p>
            抵制不良游戏&nbsp;&nbsp;拒绝盗版游戏</p>
        <p>
            注意自我保护&nbsp;&nbsp;谨防上当受骗</p>
        <p>
            适度游戏益脑&nbsp;&nbsp;沉迷游戏伤身</p>
        <p>
            合理安排时间&nbsp;&nbsp;享受健康生活</p>
    </div>
    <div class="kefu">
        <h1 class="red_title">
            <span class="left"></span><span class="middle">客服中心</span><span class="right"></span></h1>
        <div class="kefu_content">
         <strong>客服QQ</strong>
        <p>
            GM01 :<a href="tencent://message/?uin=2357752837">2357752837</a></p>
        <p>
            GM02 :<a href="tencent://message/?uin=2361245014">2361245014</a></p>
        <p>
            GM03 :<a href="tencent://message/?uin=2460929679">2460929679</a></p>
        <strong>玩家交流群</strong>
        <p>
            QQ群1 :175393719</p>
        <p>
            QQ群2 :175393858</p>   
        </div>
    </div>
</body>
<%=sMsg %>
</html>

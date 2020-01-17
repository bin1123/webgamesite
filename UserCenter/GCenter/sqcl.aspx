<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sqcl.aspx.cs" Inherits="UserCenter.GCenter.sqcl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>神曲登陆页面</title>
    <link href="<%=sWebUrl %>/wldFolder/sq/sq_login.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/UserVal.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/road.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/Game.js"></script>
    <script type="text/javascript">
        var account = '<%=sAccount %>';
        function regshow() {
            $('#reg').show();
            $('#login').hide();
        }

        function loginshow() {
            $('#reg').hide();
            $('#login').show();
        }

        function focusing(obj) {
            obj.value = ''; obj.className = 'input-on';
        }

        function bluring(obj, txt, flag) {
            var val = obj.value;
            if (val == '') {
                obj.value = txt; obj.className = 'input2';
            }
            else {
                if (val != txt && val.length > 4 && flag == "y") {
                    CheckNameIn(val);
                }
            }
        }

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

        function regVal() {
            var username = $("#accountreg").val();
            var password = $("#pwdonereg").val();
            var passwordtwo = $("#pwdtwo").val();
            if (username == "" || username == "请输入您的帐号") {
                alert("请输入帐号");
                return false;
            }
            else if (password == "") {
                alert("密码不能为空");
                return false;
            }
            else if (passwordtwo == "") {
                alert("确认密码不能为空");
                return false;
            }
            else if (username.length < 4 || username.length > 16) {
                alert("用户名格式不正确");
                return false;
            }
            else if (password.length < 6 || password.length > 16) {
                alert("密码格式不正确");
                return false;
            }
            else if (password != passwordtwo) {
                alert("密码和确认密码不一致,请核对!");
                return false;
            }
            else {
                CheckNameIn(username);
                var flag = $("#RFlag").val();
                if (flag == "f") {
                    return false;
                }
                else {
                    return true;
                }
            }

        }

        function DivShowOn(id) {
            if (id == "tjgame") {
                $('#tjgame').attr('class', 'on');
                $('#tjul').show();
                $('#allgame').attr('class', '');
                $('#allgameul').hide();
            }
            else {
                $('#tjgame').attr('class', '');
                $('#tjul').hide();
                $('#allgame').attr('class', 'on');
                $('#allgameul').show();
            }
        }

        $(document).ready(function() {
            if (account == '') {
                $('#nologin').show();
                $('#logined').hide();
            }
            else {

                $('#nologin').hide();
                $('#logined').show();
                $('#user').text(account);
                ServerSqLastSel(account);
                GameOfSqServerSel()
            }
        });

        function SetGame(game) {
            $("#gamename").val(game);
            submitGame();
        }

        function submitGame() {
            if (account == '') {
                $('#nologin').show();
                $('#logined').hide();
                return;
            }
            var game = $('#gamename').val();
            $.ajax({
                type: "POST",
                url: "/Services/Ajax.ashx",
                data: "AjaxType=sqGameCL&account=" + account + "&game=" + game,
                beforeSend: function() {
                },
                success: function(data) {
                    if (data == '') {
                        alert("登陆异常!");
                    }
                    else {
                        LoadGame(data);
                    }
                }
            });
        }
    </script>
</head>
<body>
    <div id="nologin" class="main" style="display: none">
        <div class="news">
            <div class="lbpic">
                <a href="http://www.dao50.com/news/2012621/n12482948.html" target="_blank">
                    <img alt="" src="<%=sWebUrl %>/wldFolder/sq/lb_pic.jpg" width="331" height="174" />
                </a>
            </div>
            <div class="list">
                <ul id="xwgg">
                    <li><a href="http://www.dao50.com/news/sq_xwgg/" target="_blank">到武林《神曲》新闻公告</a></li>
                    <li><a href="http://www.dao50.com/news/sq_hdzx/" target="_blank">到武林《神曲》精彩活动公告</a></li>
                    <li><a href="http://www.dao50.com/news/2012821/n58834178.html" target="_blank"> 百年恩爱双心结，千里姻缘一线牵</a></li>
                    <li><a href="http://www.dao50.com/news/2012821/n82964194.html" target="_blank"> 浓情七夕，浪漫来袭</a></li>
                    <li><a href="http://www.dao50.com/news/201287/n88523965.html" target="_blank">天降惊喜，好礼不断</a></li>
                </ul>
            </div>
        </div>
        <div id="login" class="login" style="display: none">
            <div class="login_l">
                <img src="<%=sWebUrl %>/wldFolder/sq/logo.jpg" width="152" height="67" /></div>
                <form id="form1" name="from1" onsubmit="return loginVal()" action="sqcl.aspx" method="post">
            <div class="login_c" style="padding-top:25px;">
                <p class="id">
                   <input name="account" onfocus="focusing(this)" onblur="bluring(this,'请输入用户名','n')" type="text" class="input2" id="account" value="请输入用户名"/>
                </p>
                <p class="pass">
                   <input name="pwdone" type="password" class="input2" id="pwdone"/>
                </p>
            </div>
            <div class="login_r" align="center">
                <button class="ss-pass-login" type="submit"></button>
                <a href="#" onclick="regshow()">注册账号</a> | <a href="<%=sRootUrl %>/UCenter/searchPass.aspx" target="_blank">找回密码</a></div>
                <input type="hidden" value="login" name="Type"/>
                </form>
        </div>
        <div id="reg" class="login">
            <div class="login_l">
                <img src="<%=sWebUrl %>/wldFolder/sq/logo.jpg" width="152" height="67" /></div>
                <form id="form2" name="form2" onsubmit="return regVal()" action="sqcl.aspx" method="post">
            <div class="login_c">
                <p class="id">
                    <input class="input2" maxlength="40" type="text" onfocus="focusing(this)" onblur="bluring(this,'请输入您的帐号','y')" name="accountreg" id="accountreg" value="请输入您的帐号"/></p>
                <p class="pass">
                    <input name="pwdonereg" id="pwdonereg" title="请输入密码" class="input2" type="password"/></p>
                <p class="repass">
                    <input name="pwdtwo" id="pwdtwo" title="请再次输入密码" class="input2" type="password"/></p>
            </div>
            <div class="login_r" align="center">
                    <button class="ss-pass-reg" type="submit"></button>
                <a href="#" onclick="loginshow()">用户登录</a></div>
                <input type="hidden" value="reg" name="Type"/>
                <input type="hidden" id="RFlag" value="t" />                
                </form>
        </div>
    </div>
    <div id="logined" class="xf">
        <!--服务器列表-->
        <div class="use">
            <span id="user" style="color: #FF0;"></span> 欢迎您登陆，请选择服务器！<a href="../Services/userexit.aspx">注销</a>
        </div>
        <div class="zjdl">
            <div class="list">
                <ul>
                    <li><a href="javascript:submitGame();" id="pserver" class="zc">1服_开天辟地</a></li>
                </ul>
            </div>
            <a href="javascript:submitGame();">
                <img src="<%=sWebUrl %>/wldFolder/sq/but_st.gif" width="203" height="90" border="0" class="start" /></a></div>
        <div>
            <div class="xfdh">
                <p id="tjgame" onmouseover="DivShowOn('tjgame')">
                    <a href="#">推荐服务器</a></p>
                <p id="allgame" onmouseover="DivShowOn('allgame')" class="on">
                    <a href="#">全部服务器</a></p>
            </div>
            <div class="qb_list">
                <ul id="tjul" onmouseover="DivShowOn('tjgame')" style="display: none">
                    <!--推荐-->
                    <li class="hot"><a href="javascript:submitGame();">1服_测试服</a></li>
                </ul>
                <ul id="allgameul" onmouseover="DivShowOn('allgame')">
                    <!--全部-->
                    <li class="hot"><a href="javascript:submitGame();">1服_开天辟地</a></li>
                    <%--<li class="hui"><a href="#">神曲双线一服</a></li>--%>
                </ul>
            </div>
        </div>
    </div>
    <input type="hidden" id="gamename" value="sq1"/>
</body>
</html>
<%=sMsg %>
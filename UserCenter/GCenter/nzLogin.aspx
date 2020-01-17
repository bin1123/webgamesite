<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nzLogin.aspx.cs" Inherits="UserCenter.GCenter.nzLogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>怒斩登陆页</title>
    <link href="http://file.dao50.com/nzl/css/pc_game_nz.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
</head>
<body scroll="no">
    <!--登录前-->
    <div id="login_before">
        <div class="main">
            <div class="news">
                <p class="news_tit">
                    <a href="http://www.dao50.com/news/nz_xwgg/" class="news_tit01" target="_blank" rel="nofollow">更多+</a><div class="__data__" data_id="__data__[1342]" page="article" postfix="client_nz" style="display: none;">
                        <div>
                        </div>
                        <span>怒斩客户端新闻</span></div>
                </p>
                <ul class="news_txt">
                    <li><a href="http://www.dao50.com/news/2013516/n314810138.html" target="_blank" style="color: #ff0000; font-weight: bold;" />到武林《怒斩》开服活动</a></li>
                    <li><a href="http://www.dao50.com/news/2013516/n124110110.html" target="_blank" style="color: #ff0000; font-weight: bold;" />到武林《怒斩》升级送装备</a></li>
                    <li><a href="http://www.dao50.com/news/2013516/n433010106.html" target="_blank" style="color: #FF6600; font-weight: bold;" />到武林《怒斩》充值10%返利</a></li>
                    <li><a href="http://www.dao50.com/news/2013516/n201610112.html" target="_blank" style="color: #FF6600;" />杀BOSS领大奖 你敢杀我就送</a></li>
                    <li><a href="http://www.dao50.com/news/nz_xwgg/" target="_blank" style="color: #ff0000;" />到武林《怒斩》最新开服</a></li>
                </ul>
            </div>
            <div class="banner">
                <div id="Slideshow">
                    <a href="#" target="_blank" rel="nofollow">
                        <img src="http://file.dao50.com/nzl/images/201304080757237250.jpg" width="214" height="77" /></a>
                </div>
            </div>
            <form action="nzLogin.aspx" method="post" id="login_form" onsubmit="return submit_form()">
            <div class="login">
                <div class="left">
                    <p>
                        <img src="http://file.dao50.com/nzl/images/name.jpg" /><input name="username" type="text"x
                            id="username" value="" tabindex="1" /></p>
                    <p>
                        <img src="http://file.dao50.com/nzl/images/password.jpg" /><input name="pwd" type="password"
                            id="pwd" tabindex="2" /></p>
                    <p style="text-align: right; padding-right: 4px; position: relative; margin-top: 10px;
                        *margin-top: 15px;">
                        <a href="http://game.dao50.com/UCenter/searchPass.aspx" target="_blank">忘记密码？</a><a
                            href="http://game.dao50.com/UCenter/reg.aspx" target="_blank">注册帐号</a>
                    </p>
                </div>
                <div class="right">
                    <a href="###" onclick="submit_form(); return false;" id="submit_button" class="start">
                        <img src="http://file.dao50.com/nzl/images/login_btn.jpg" /></a></div>
                <div style="width: 0px; height: 0px; overflow: hidden;">
                    <input type="submit" value="" /></div>
            </div>
            </form>
        </div>
    </div>
<div id="login_after" style="display:none">
    <div class="main">
        <div class="game_into">
        	<p><a href="/Services/userexit.aspx" style="color:#f00; text-decoration:underline; padding-left:30px;">[退出]</a></p>
            <span>最近玩过：</span>
            <p class="btn"><a id="zjdl" href="nz.aspx?gn=nz1">双线1区</a></p>
        </div>
        <div class="tips">尊敬的：<span id="user" style="color:#eace2b;"></span>欢迎您登录《怒斩》</div>
        <div class="zx tab" id="tab_list">
        	<div class="zx_tit">
            	<ul><li id="tjf" onmouseover="ServerTJSel()" class="cur">最新服</li><li id="syf" onmouseover="ServerAllSel()">所有服</li></ul>
            </div>
            <div class="zx_txt cont">
        	    <ul id="nzsever" class="cur server">							                   										
                </ul>
			    <br clear="all" />
            </div>
        </div>
    </div>
</div>
<input type="hidden" id="qbyx"/><input type="hidden" id="tjyx"/>
</body>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/nzcl.js"></script>
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
            document.getElementById("login_form").submit();
            return true;
        }
    }

    $(document).ready(function() {
        var account = '<%=sAccount %>';
        if (account == '') {
            $('#login_before').show();
            $('#login_after').hide();
        }
        else {
            ServerLastSel(account);
            ServerSel();
            $('#login_before').hide();
            $('#login_after').show();
            $('#user').text(account);
        }
    }); 
</script>
</html>
<%=sMsg %>
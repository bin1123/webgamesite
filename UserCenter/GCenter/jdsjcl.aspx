<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jdsjcl.aspx.cs" Inherits="UserCenter.GCenter.jdsjcl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>绝代双娇</title>
    <link type="text/css" rel="stylesheet" href="http://file.dao50.com/jdsjcl/css/css.css" />
</head>
<body>
    <div id="content" class="main2">
    <div id="gamelogin" class="login"><!--登陆前-->
    <form id="login_form" action="jdsjcl.aspx" name="login_form" method="post">
        <div class="login-pop">
            <ul class="clearfix">
                <li class="mod1">
                    <span class="name">用户名：</span>
                    <input class="input" type="text" id="username" name="username" maxlength="16"/>
                </li>
                <li class="mod1">
                    <span class="name">密码：</span>
                    <input class="input" type="password" id="pwd" name="pwd" maxlength="16"/>
                </li>
            </ul>
            <div class="mod3">
                <label for="check">
                    <input class="input" type="checkbox"  id="isKeepAlive"/>
                    <span>记住登录状态</span>
                </label>
            </div>
            <a href="#" class="btn_log" onclick="submit_form()"  id="loginbtn">登录</a>
            <a href="http://game.dao50.com/UCenter/searchPass.aspx" target="_blank" class="btn_reg btn_reg1" title="找回密码">找回密码</a>
        </div>
    </form>
    </div><!--登陆前结束-->
    <div id="gamelogined" class="login_Af" style="display:none"><!--登陆后-->
        <div class="login_Af-pop">
            <p class="name"><%=sAccount %></p>
            <div class="choseServe" id="played">
                <h3>最近登录服务器：</h3>
                <span id="played_item_tpl" style="">
                    <a id="loginedserver" href="#"></a>
                </span>
            </div>
            <div class="choseServe">
                <h3>推荐服务器：</h3>
                <a id="tjserver" href="#"></a>
            </div>
            <a class="choseServeBtn js_serveBoxBtn" href="javascript:XZServerS();" title="选择服务器"></a>
            <div class="choseOhterBtn">
                <a href="/Services/userexit.aspx" title="退出">退出登录</a>
                <a href="http://game.dao50.com/UCenter/UPWordM.aspx" title="修改密码" target="_blank">修改密码</a>
            </div>
        </div>
    </div><!--登陆后结束-->
	<div class="activity">
        <a href="http://www.dao50.com/news/jdsj_xwgg/" target="_blank" class="more">更多></a>
        <ul class="clearfix">
            <li class="hot"><a href="http://www.dao50.com/news/2013718/n537213079.html" target="_blank">《绝代双骄》vip特权</a><span>5-28</span></li>
            <li class="hot"><a href="http://www.dao50.com/news/jdsj_xwgg/" target="_blank">《绝代双骄》开服信息</a><span>5-28</span></li>
            <li class="hot"><a href="http://www.dao50.com/news/2013718/n034713078.html" target="_blank">登录领奖励，好礼天天有</a><span>5-28</span></li>
            <li class="hot"><a href="http://www.dao50.com/news/2013718/n684413077.html" target="_blank">极品翅膀降临</a><span>5-28</span></li>
            <li class="hot"><a href="http://www.dao50.com/news/2013718/n500913075.html" target="_blank">首充礼包，超值放送</a><span>5-28</span></li>
            <li class="hot"><a href="http://www.dao50.com/news/2013718/n417613076.html" target="_blank">累计充值连环送</a><span>5-28</span></li>
            <li class="hot"><a href="http://www.dao50.com/news/2013717/n525312953.html" target="_blank">消费元宝双倍返</a><span>5-28</span></li>
            <li class="hot"><a href="http://www.dao50.com/news/jdsj_hdgg/index.html" target="_blank">《绝代双骄》更多精彩活动 </a><span>5-28</span></li>       
        </ul>
    </div>
    <div class="subNav">
        <ul>
			<li class="index1"><a href="http://game.dao50.com/UCenter/reg.aspx" target="_blank">立即注册</a></li>
            <li class="index2"><a href="http://game.dao50.com/xsk/" target="_blank">新手礼包</a></li>
            <li class="index3"><a href="http://www.dao50.com/yxzq/jdsj" target="_blank">进入官网</a></li>
            <li class="index4"><a href="http://bbs.dao50.com" target="_blank">在线客服</a></li>
        </ul>
    </div>
    <div id="xzserver" class="serveBox js_chooseServe" style="display:none;">
        <h1>选择服务器</h1>
        <div class="clearfix">
            <div class="serve near">
                <h2>推荐服务器：</h2>
                <div><ul><li><a id="tjserver_xz" href="#"></a></li></ul></div>
                <p class="yl">火爆开启中</p>
            </div>
        </div>
        <div class="serve all">
            <div class="nav"><ul><li>全部服务器</li></ul></div>
            <div class="ul" >
                <ul id="allserver">
                </ul>
            </div>
         </div>
        <div class="serveBox_close js_serveBox_close" onclick="XZServerH()"></div>
    </div>
    </div>
</body>  
</html>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jdsjcl.js"></script>
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
            $('#gamelogin').show();
            $('#gamelogined').hide();
        }
        else {
            $('#gamelogin').hide();
            $('#gamelogined').show();
            ServerLastSel(account);
            ServerSel();
        }
    }); 
</script>
<%=sMsg %>
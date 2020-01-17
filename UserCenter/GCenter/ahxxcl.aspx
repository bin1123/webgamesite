<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ahxxcl.aspx.cs" Inherits="UserCenter.GCenter.ahxxcl" %>
<%=sMsg %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>暗黑修仙登陆器</title>
    <link href="http://file.dao50.com/ahxxcl/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://file.dao50.com/ahxxcl/css/inner.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://file.dao50.com/ahxxcl/js/jquery.js"></script>
    <script type="text/javascript" src="http://file.dao50.com/ahxxcl/js/comman.js"></script>
    <script type="text/javascript" src="http://file.dao50.com/ahxxcl/js/dd.js"></script>
</head>
<body>
<div class="wrap inner">
        <div class="top-box">
            <div class="welcome">
                <span class="user"><%=sAccount %></span> 欢迎您登陆，请选择服务器！ <a href="/Services/userexit.aspx">【注销】</a>
            </div>
            <div class="server-pro">
                <div class="i-logined"><!--最近登录服务器-->
                    <ul id="zjdl" class="server-list">
                    </ul>
                </div>
                <div class="pros"><!--推荐服务器-->
                    <ul id="tjserver" class="server-list">
                    </ul>
                </div>
            </div>
        </div>
        <div class="bottom-box">
            <div class="tabs">
                <ul>
                    <li class="hover">
                        <a show='pro-box' class="pro" href="javascript:void(0)">推荐服务器</a>
                    </li>
                </ul>
                <div class="quick-way">
                    <input id="number" class="text" maxlength="4" type="text"/>
                    <input value="" class="submit" type="submit"/>
                </div>
            </div>
            <div class="box">
                <ul id="ahxxserver" class="server-list tab pro-box">
                </ul>
            </div>
        </div>
    </div>
    <input type="hidden" id="lastgame" value="1"/>
</body>
<script type="text/javascript">
    $(".bottom-box").setTab({
        btn: '.tabs li a',
        div: '.tab'
    });
</script>
<!--[if lt IE 7 ]>
<script>
    DD_belatedPNG.fix(".server-list li,.tabs li a,background");
</script>
<![endif]-->
<script type="text/javascript" src="/JsFolder/ahxxcl.js"></script>
<script type="text/javascript">
    var account = '<%=sAccount %>';
    $(document).ready(function() {
        if (account.length > 4) {
            ServerLoginSel(account);
            ServerSel();
        }
    });
</script>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sssgcl.aspx.cs" Inherits="UserCenter.GCenter.sssgcl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>盛世三国登陆页面</title>
    <link href="<%=sWebUrl %>/yxzq/sg/css/sssgcl.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/Game.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
</head>
<body>
    <div class="denglu">
        <div id="LoginedInfo" class="denglu_left0">
            <!--选择服务器开始-->
            <div class="gonggao">
                <ul id="hdgg">
                </ul>
            </div>
            <div class="right_fwq">
                <div class="huanying">
                    <span id="account">
                        <%=sAccount %></span></div>
                <form id="form1" action="sssgcl.aspx" method="post">
                <div class="fwq">
                    <div class="zxkf">
                        <input type="radio" id="nserverradio" onclick="serverSet(this.value)" name="serverradio" checked="checked" value="sssg6" id="zxkfcheck" />
                        <img src="<%=sWebUrl %>/yxzq/sg/images/zuixinkaifu.jpg" hspace="2" />
                        <span><a id="nserver">唯我独尊</a></span>
                    </div>
                    <div class="zxkf">
                        <input type="radio" id="pserverradio" onclick="serverSet(this.value)" name="serverradio" value="sssg6" id="zjdlcheck" />
                        <img src="<%=sWebUrl %>/yxzq/sg/images/zuijindenglu.jpg" hspace="2" />
                        <span><a id="pserver"></a></span>
                    </div>
                    <div class="qtfwq" onclick="serverShow()"><a href="#" id="oserver">选择其他服务器</a></div>
                </div>
                <input type="hidden" name="gameabbre" id="gameabbre" value="sssg34" />
                </form>
                <div class="jryx">
                    <input type="button" onclick="playgame()" value="进入游戏" class="btn" />&nbsp;&nbsp;<a
                        href="<%=sWebUrl %>/yxzq/sg/index1.html">重新登录</a>
                </div>
            </div>
        </div>
        <div id="servercheck" class="denglu_left00" style="display: none">
            <!--服务器列表-->
            <a class="close_s" href="#" onclick="serverHid()"></a>
            <div class="popup_inner">
                <div id="played_list">
                    <div class="popup_title1"></div>
                    <div class="zj"><span class="zhan"></span><a href="#" id="nserverb" class="s_list"></a></div>
                </div>
                <div class="popup_title2">
                </div>
                <div id="all_list" class="popup_block">
                    <div id="serverinfo" class="just">
                        <a id="sid_S2" class="s_list"><span class="not_zhan"></span>6服_千秋万代<span style="color: #009b80">(新)</span></a>
                    </div>
                </div>
            </div>
        </div>
        <!--选择服务器结束-->
        <div class="denglu_right">
            <!--右侧公共部分-->
            <div class="right_img">
                <a href="http://www.dao50.com/news/20111115/n3947644.html" target="_blank">
                    <img src="http://www.dao50.com/yxzq/sg/images/login_ad.gif" /></a></div>
            <div class="right_text">
                <a href="http://bbs.dao50.com/kefuCenter/list.aspx" target="_blank" class="txt_huang">
                    客服中心</a> <a href="http://www.dao50.com/yxzq/sssg/yxzq_sssg" target="_blank" class="txt_bai">
                        进入官网>> </a>
            </div>
            <div class="right_kf">
                <ul>
                    <li>官方QQ：2460929679</li>
                    <li>官方QQ：2361245014</li>
                    <li><a href="http://bbs.dao50.com/kefuCenter/list.aspx" target="_blank">遇到问题怎么办？</a></li>
                    <li>玩家交流群：175393719</li>
                    <li>玩家交流群：175393858</li>
                    <li>玩家交流群：</li>
                </ul>
            </div>
        </div>
        <!--右侧公共部分-->
    </div>
    <!--到武林游戏登陆结束-->
</body>

<script type="text/javascript">
    var servercheckHtml;
    function serverShow() {
        $("#oserver").html("选择其他服务器");
        $("#servercheck").html(servercheckHtml);
        $("#servercheck").show();
        $("#LoginedInfo").hide();
        $("#nserverradio").attr({ 'disabled': 'disabled' });
        $("#pserverradio").attr({ 'disabled': 'disabled' });
        $("#servercheck").html(servercheckHtml);
        var gameabbre = 'sssg';
        ServerNewSel1(4);
        GameOfServerSel(gameabbre);
    }

    function serverHid() {
        servercheckHtml = $("#servercheck").html();
        $("#servercheck").html("");
        $("#servercheck").hide();
        $("#LoginedInfo").show();
    }

    function serverSet(server) {
        $("#gameabbre").val(server);
    }

    function serverCheck(serverabbre,servername) {
        serverSet(serverabbre);
        $("#oserver").html(servername);
        serverHid();
    }

    function serverSel(objID) {
        var text = $("#" + objID).html();
        $("#oserver").html(text);
        $("#gameabbre").val(objID);
        serverHid();
    }

    function playgame() {
        document.getElementById("form1").submit()
    }

    $(document).ready(function() {
        //var gameabbre = 'sssg';
        var serverabbre = $("#gameabbre").val();
        GameClassSel(serverabbre, 'hdgg', '', 'hdgg');
        //GameOfServerSel(gameabbre);
        ServerNewSel(4, 'nserver');
        //ServerNewSel1(4);
        ServerLastSel(4, 'pserver', '<%=sAccount %>');
        serverHid();
    });
</script>
</html>

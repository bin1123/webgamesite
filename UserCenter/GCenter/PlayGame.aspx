<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlayGame.aspx.cs" Inherits="UserCenter.GCenter.PlayGame" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title><%=sTitle %></title>
        <link href="<%=sWebUrl %>/wldFolder/css/index_css.css" rel="stylesheet" type="text/css" />
        <link href="<%=sWebUrl %>/wldFolder/css/dingbu_css.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
        <script type ="text/javascript" src="<%=sRootUrl %>/JsFolder/Game.js"></script>
        <script type ="text/javascript" src="<%=sRootUrl %>/JsFolder/PlayGame.js"></script>
        <link href="<%=sWebUrl %>/wldFolder/css/PlayGame.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript">
            $(document).ready(function() {
                var gameabbre = '<%=sGameName %>';
                if (gameabbre != '' && gameabbre.length < 10) {
                    //NoticeSel(gameabbre,"msg");
                    if(gameabbre.indexOf('lj') < 0)
                    {
                       document.getElementById('main').height = <%=iHeight %>;
                       document.getElementById('main').width = <%=iWidth %>;
                       $("#rightlan").show();
                       HelpClassSel(gameabbre, 'hdgg', 'font_hong', 'hdgg');
                       HelpClassSel(gameabbre, 'yxzl', 'font_huang', 'yxzl');
                       HelpClassSel(gameabbre, 'yxgl', 'font_lv', 'yxgl');
                       HelpClassSel(gameabbre, 'xszn', 'font_zi', 'xszn');
                    }
		            else
		            {
                    	$("#rightlan").hide();
                        onloadresize();
                        var height = <%=iHeight %>;
                        if(height != 600)
                        {
                          var tb01 = document.getElementById("tb01");
                          var main = document.getElementById("main");
                          tb01.style.height = height + "px";
                          main.style.height = height + "px";
                        }
                    }
                }
                else {
                    $("#rightlan").hide();
                    onloadresize();
                    var height = <%=iHeight %>;
                    if(height != 600)
                    {
                        var tb01 = document.getElementById("tb01");
                        var main = document.getElementById("main");
                        tb01.style.height = height + "px";
                        main.style.height = height + "px";
                    }
                }
//                var game = '<%=Request["gn"] %>';
//                switch (game) {
//                    case "sssg35":
//                        winpopshow();
//                        break;
//                    case "sxd4":
//                        winpopshow("http://www.dao50.com/news/20111115/n8454648.html","<%=sWebUrl %>/wldFolder/images/sxd4tc.jpg");
//                        break;
//                }
            });
        </script>
    </head>
    <body>        
        <div class="top">
		    <div class="top_content">
            <div class="top_logo"><a href="http://www.dao50.com" target="_blank"><img src="<%=sWebUrl %>/wldFolder/images/top_logo.jpg" /></a></div>
            <div class="top_youxi">
            <ul>
            <li class="sssg"><a href="http://www.dao50.com/yxzq/sq/" target="_blank">神曲</a></li>
            <li>|</li> 
            <li class="mg"><a href="http://www.dao50.com/yxzq/dxz/" target="_blank">大侠传</a></li> 
            <li>|</li>
            <li class="ztx"><a href="http://www.dao50.com/yxzq/tzcq/" target="_blank">天尊传奇</a></li>
            <li>|</li>
            <li class="asqx"><a href="http://www.dao50.com/yxzq/sjsg/" target="_blank">神将三国</a></li>
            </ul>
            </div>
            <ul class="xiaoxi">
            <marquee id="msg" onmouseover="this.stop()" onmouseout="this.start()" direction="up" height="20" scrollAmount="1">
                <li><a href="http://www.dao50.com" title="到武林" target="_blank">欢迎来到到武林平台！我们将提供优质的服务！谢谢您的支持！</a></li>
            </marquee>
            </ul>
            <div class="shouye"><a href="http://www.dao50.com" target="_blank">首页</a> | <a href="http://game.dao50.com/pay" target="_blank">充值</a> | <a href="#"  onclick ="javascript:window.external.AddFavorite(document.URL,document.title);return false" rel="sidebar">收藏本页</a></div>
            </div>
	    </div>
        <div>
        <table width="100%" height="700" id="tb01" align="center">
            <tr>
                <td align="center" valign="top">
                    <iframe id="main" src="<%=sUrl %>" width="100%" height="700" frameborder="0" scrolling="no" allowtransparency="yes" style="padding-left:0px; padding-right:0px;"></iframe>
                </td>
            </tr>
        </table>                    
        </div>
        <div class="gonggao" id="rightlan">
		    <div class="gonggao_left"><a href="<%=sGameSite %>" target="_blank" title="<%=sTitle %>"><%=ssTitle %></a></div>
            <div class="gonggao_right">
                <ul id="hdgg" class="zuixinzixun">
                <li class="zxzx_dot"><a href="<%=sGameSite %>" title="点击查看" target="_blank">活动公告</a></li>
                </ul>
                <ul id="yxzl" class="zuixinzixun">
                <li class="zxzx_dot"><a href="<%=sGameSite %>" title="点击查看" target="_blank">游戏资料</a></li>
                </ul>
                <ul id="yxgl" class="zuixinzixun">
                <li class="zxzx_dot"><a href="<%=sGameSite %>" title="点击查看" target="_blank">游戏攻略</a></li>
                </ul>
                <ul id="xszn" class="zuixinzixun">
                <li class="zxzx_dot"><a href="<%=sGameSite %>" title="点击查看" target="_blank">新手指南</a></li>
                </ul>
                <ul class="chongzhi"><A href="<%=sRootUrl %>/Pay/default.aspx" target="_blank"><img src="<%=sWebUrl %>/wldFolder/images/chongzhirukou.jpg" /></A></ul>
            </div>
	    </div>
	    <div id="winpop" style="display:none">
            <div class="con">
                <a id="tc" href="http://www.dao50.cn/soft/clintwei.exe" target="_blank"><img id="tcimg" src="<%=sWebUrl %>/wldFolder/images/tanchuang.jpg" /></a>
                <span class="close" onclick="tips_pop()">X</span>
            </div>
        </div>
        <iframe id="converifr" frameborder="0" width="280px" marginheight="0" marginwidth="0" style="display:none" scrolling="no" class="frm"></iframe>
    </body>
</html>
<script src="<%=sRootUrl %>/JsFolder/bookmark.js" type="text/javascript"></script>
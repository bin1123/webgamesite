<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_tssg.aspx.cs" Inherits="UserCenter.frame.g_left_nav_tssg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style>
        body,div,dl,dt,dd,ul,ol,li,h1,h2,h3,h4,h5,h6,pre,form,fieldset,input,p,blockquote,th,td{margin:0;padding:0;}
        fieldset,img{border:0;}
        address,caption,cite,code,dfn,em,strong,th,var{font-style:normal;font-weight:normal;}
        a:active, a:focus { outline:none; }
        ol,ul{list-style:none;}
        caption,th{text-align:left;}
        h1,h2,h3,h4,h5,h6{font-size:100%;}
        * {
	        font-size:12px; 
            list-style: none outside none;
            margin: 0;
            padding: 0;
        }
        body{ background-color:#0e1d30}
        .leftBox {
            height:auto;
	        overflow:hidden;
            left: 0;
            margin:0;
            position: absolute;
            top: 0;
            width: 110px;
	        background-color:#0e1d30;
        }
        .leftBox ul#menu {
            float: left;
            padding: 0 0 0 3px;
            text-align: center;
            width: 105px;
        }
        .leftBox li {
            display: inline;
        }
        a {
            outline: medium none;
            text-decoration: none;
        }
        .leftBox li img {
            display: block;
            margin-bottom: 5px;
        }
        .leftBox li ul {
            display: none;
            float: none;
            padding: 5px;
            width: auto;
        }
        .leftBox li ul li {
            line-height: 22px;
        }
        .leftBox li ul li a {
            border: 1px solid #0e1d30;
            color: #77d066;
            display: block;
            line-height: 22px;
            text-align: center;
            text-decoration: none;
        }
        .leftBox li ul li a:hover {
            border: 1px solid #666;
            color:#C00;
	        font-weight:bold;
	        background-color:#333;
            display: block;
            line-height: 22px;
            text-align: center;
            text-decoration: none;
        }
        element.style {
            border: 1px solid #000000;
            color: #DDE870;
            line-height: 22px;
            text-align: center;
        }
    </style>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript">
        function showorhide(ulid) {
            $("#" + ulid).toggle();
        }
    </script>
</head>
<body>
<div class="leftBox">
	<ul id="menu">
		<li><a href="http://www.dao50.com/yxzq/yjxy/index.html" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/tssgzl/logo.jpg"></a></li>
		<li onclick="showorhide('cz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/tssgzl/cz.jpg"/></a>
			<ul id="cz">
			<li><a href="http://game.dao50.com/pay/" target="_blank">快速充值入口</a></li>
			<li><a href="http://www.dao50.com/news/201264/n68872650.html" target="_blank">VIP等级介绍</a></li>
			</ul>
		</li>
		<li><a href="http://game.dao50.com/xsk/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/tssgzl/newbie.jpg"></a></li>
		<li onclick="showorhide('bz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/tssgzl/bz.jpg"/></a>
			<ul id="bz" style="display:block">
            <li><a href="http://www.dao50.com/news/201265/n37742671.html" target="_blank">声望与文勋</a></li>
            <li><a href="http://www.dao50.com/news/201264/n77702638.html" target="_blank">添加好友</a></li>
			<li><a href="http://www.dao50.com/news/201265/n37742671.html" target="_blank">武将三围</a></li>
			<li><a href="http://www.dao50.com/news/201265/n70052654.html" target="_blank">职业介绍</a></li>
            <li><a href="http://www.dao50.com/news/201265/n37742671.html" target="_blank">快速赚钱</a></li>
			<li><a href="http://www.dao50.com/news/201265/n42942655.html" target="_blank">新手游戏导航</a></li>
			<li><a href="http://www.dao50.com/news/201265/n37742671.html" target="_blank">潜力提升</a></li>
			<li><a href="http://www.dao50.com/news/201265/n37742671.html" target="_blank">强化装备</a></li>
            <li><a href="http://www.dao50.com/news/201264/n33702651.html" target="_blank">招募名将</a></li>
            <li><a href="http://www.dao50.com/news/201264/n05002639.html" target="_blank">加入帮会</a></li>
            <li><a href="http://www.dao50.com/news/201265/n14092657.html" target="_blank">排兵布阵</a></li>
			</ul>
		</li>
		<li onclick="showorhide('bd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/tssgzl/bd.jpg"/></a>
			<ul id="bd">
            <li><a href="http://www.dao50.com/news/201265/n14422656.html" target="_blank">任务系统</a></li>
            <li><a href="http://www.dao50.com/news/201265/n14422656.html" target="_blank">玩转副本</a></li>
            <li><a href="http://www.dao50.com/news/201265/n34922660.html" target="_blank">野外挂机</a></li>
            <li><a href="http://www.dao50.com/news/201264/n29502646.html" target="_blank">征战天下</a></li>
            <li><a href="http://www.dao50.com/news/201265/n61872659.html" target="_blank">历练武将</a></li>
            <li><a href="http://www.dao50.com/news/201265/n96622662.html" target="_blank">三国争霸</a></li>
            <li><a href="http://www.dao50.com/news/201264/n48212648.html" target="_blank">竞技对决</a></li>
            <li><a href="http://www.dao50.com/news/201264/n71692636.html" target="_blank">BOSS大战</a></li>
            </ul>
		</li>
		<li><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/tssgzl/hd.jpg"/></a>
        <ul>
        <li><a href="http://bbs.dao50.com" target="_blank">游戏论坛</a></li>
        <li><a href="http://bbs.dao50.com/kefuCenter/list.aspx" target="_blank">客服中心</a></li>
        </ul>
		<li style="border: 1px solid #0e1d30;color:#77d066;line-height:22px;text-align:center;">
		<b>官方QQ群</b>
		<br>
		131202362
		</li>
        </li>
	</ul>
</div>
</body>
</html>

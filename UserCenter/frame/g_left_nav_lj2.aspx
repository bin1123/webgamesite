<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_lj2.aspx.cs"
    Inherits="UserCenter.frame.g_left_nav_lj2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <style>
        body, div, dl, dt, dd, ul, ol, li, h1, h2, h3, h4, h5, h6, pre, form, fieldset, input, p, blockquote, th, td
        {
            margin: 0;
            padding: 0;
        }
        fieldset, img
        {
            border: 0;
        }
        address, caption, cite, code, dfn, em, strong, th, var
        {
            font-style: normal;
            font-weight: normal;
        }
        a:active, a:focus
        {
            outline: none;
        }
        ol, ul
        {
            list-style: none;
        }
        caption, th
        {
            text-align: left;
        }
        h1, h2, h3, h4, h5, h6
        {
            font-size: 100%;
        }
        *
        {
            font-size: 12px;
            list-style: none outside none;
            margin: 0;
            padding: 0;
        }
        body
        {
            background-color: #000;
        }
        .leftBox
        {
            height: auto;
            overflow: hidden;
            left: 0;
            margin: 0;
            position: absolute;
            top: 0;
            width: 110px;
            background-color: #000;
        }
        .leftBox ul#menu
        {
            float: left;
            padding: 0 0 0 3px;
            text-align: center;
            width: 105px;
        }
        .leftBox li
        {
            display: inline;
        }
        a
        {
            outline: medium none;
            text-decoration: none;
        }
        .leftBox li img
        {
            display: block;
            margin-bottom: 5px;
        }
        .leftBox li ul
        {
            display: none;
            float: none;
            padding: 5px;
            width: auto;
        }
        .leftBox li ul li
        {
            line-height: 22px;
        }
        .leftBox li ul li a
        {
            border: 1px solid #000;
            color: #b4f67a;
            display: block;
            line-height: 22px;
            text-align: center;
            text-decoration: none;
        }
        .leftBox li ul li a:hover
        {
            border: 1px solid #000;
            color: #C00;
            font-weight: bold;
            background-color: #000;
            display: block;
            line-height: 22px;
            text-align: center;
            text-decoration: none;
        }
        element.style
        {
            border: 1px solid #000;
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
		<li><a href="http://www.dao50.com/yxzq/ljer/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/lj2zl/logo.jpg"></a></li>
		<li onclick="showorhide('cz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/lj2zl/cz.jpg"></a>
			<ul id="cz" style="display:none">
			<li><a href="http://game.dao50.com/pay/">快速充值入口</a></li>
			<li><a href="http://www.dao50.com/news/2013218/n68487600.html" target="_blank">VIP介绍</a></li>
            <li><a href="http://www.dao50.com/news/2013218/n89867599.html" target="_blank">新手FAQ</a></li>
			</ul>
		</li>
		<li onclick="showorhide('lb')"><a href="http://game.dao50.com/xsk/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/lj2zl/lb.jpg"></a>
        <ul id="lb">
            <li><a href="http://www.dao50.com/news/2013218/n89867599.html" target="_blank">新手FAQ</a></li>
			</ul>
        </li>
        <li onclick="showorhide('tswf')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/lj2zl/gsjj.jpg"></a><!--新手指南-->
			<ul id="tswf">
            <li><a href="http://www.dao50.com/news/2013218/n88217603.html" target="_blank">骑宠系统</a></li>
			<li><a href="http://www.dao50.com/news/2013218/n52247604.html" target="_blank">副本系统</a></li>
			<li><a href="http://www.dao50.com/news/2013218/n56417605.html" target="_blank">宝石系统</a></li>
            <li><a href="http://www.dao50.com/news/2013218/n95587608.html" target="_blank">宝石封灵</a></li>
            <li><a href="http://www.dao50.com/news/2013218/n20537610.html" target="_blank">决战龙殿</a></li>
            <li><a href="http://www.dao50.com/news/2013218/n21667596.html" target="_blank">武将介绍</a></li>
            <li><a href="http://www.dao50.com/news/2013218/n59897612.html" target="_blank">闯关玩法</a></li>
			</ul>
		</li>
        <li onclick="showorhide('tswf')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/lj2zl/tswf.jpg"></a><!--特色玩法-->
			<ul id="yxgl" style="display:block">
            <li><a href="http://www.dao50.com/news/2013218/n49527609.html" target="_blank">闹洞房</a></li>
			<li><a href="http://www.dao50.com/news/2013218/n89907611.html" target="_blank">东吴抢亲</a></li>
            <li><a href="http://www.dao50.com/news/2013218/n52437615.html" target="_blank">皇城争霸</a></li>
			<li><a href="http://www.dao50.com/news/2013218/n77247616.html" target="_blank">一夫当关</a></li>
			</ul>
		</li>
       <li onclick="showorhide('jchd')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/lj2zl/jchd.jpg"></a><!--精彩活动-->
			<ul id="jchd" style="display:block">
            <li><a href="http://www.dao50.com/news/2013217/n69907583.html" target="_blank">首冲大礼</a></li>
            <li><a href="http://www.dao50.com/news/2013228/n34717829.html" target="_blank">VIP大礼</a></li>
			<li><a href="http://www.dao50.com/news/2013228/n77667830.html" target="_blank">登陆礼包</a></li>
			<li><a href="http://www.dao50.com/news/2013228/n62347831.html" target="_blank">套装礼包</a></li>
            <li><a href="http://www.dao50.com/news/2013228/n63487832.html" target="_blank">一夫当关排行</a></li>
            <li><a href="http://www.dao50.com/news/2013228/n56147834.html" target="_blank">激情对决</a></li>
			</ul>
	  </li>        
	  <li><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/lj2zl/wjhd.jpg"></a><!--玩家互动-->
        <ul>
        <li><a href="http://bbs.dao50.com/showforum-122.aspx">游戏论坛</a></li>
        <li><a href="http://bbs.dao50.com/showforum-125.aspx" target="_blank">客服中心</a></li>
        </ul>
		<li style="border: 1px solid #000;color:#77d066;line-height:22px;text-align:center;">
		<b>客服热线</b>
		<br>
		400 008 5267<br/>
    </li>
        </li>
	</ul>
</div>
</body>
</html>

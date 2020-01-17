<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_hzw.aspx.cs" Inherits="UserCenter.frame.g_left_nav_hzw" %>

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
        body{ background-color:#1b2446}
        .leftBox {
            height:auto;
	        overflow:hidden;
            left: 0;
            margin:0;
            position: absolute;
            top: 0;
            width: 110px;
	        background-color:#1b2446;
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
            border: 1px solid #1b2446;
            color: #b4f67a;
            display: block;
            line-height: 22px;
            text-align: center;
            text-decoration: none;
        }
        .leftBox li ul li a:hover {
            border: 1px solid #8ebbff;
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
<body style="background-color:#000;">
<div class="leftBox">
    <ul id="menu">
		<li><a href="http://www.dao50.com/yxzq/hzw/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/hzwzl/logo.jpg"></a></li>
		<li onclick="showorhide('cz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/hzwzl/cz.jpg"></a>
			<ul id="cz">
			<li><a href="http://game.dao50.com/pay/">快速充值入口</a></li>
			</ul>
		</li>
		<li><a href="http://game.dao50.com/xsk/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/hzwzl/newbie.jpg"></a></li>
		<li onclick="showorhide('zd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/hzwzl/zd.jpg"></a><!--新手指引-->
			<ul id="zd" style="display:block">
            <li><a href="http://www.dao50.com/news/2012716/n07863437.html" target="_blank">海贼王FAQ</a></li>
			<li><a href="http://www.dao50.com/news/2012716/n07863437.html" target="_blank">VIP系统介绍</a></li>
			<li><a href="http://www.dao50.com/news/2012713/n50413419.html" target="_blank">进度宝箱系统</a></li>
            <li><a href="http://www.dao50.com/news/2012713/n81763405.html" target="_blank">见壳系统</a></li>
			</ul>
		</li>
        <li onclick="showorhide('jchd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/hzwzl/jchd.jpg"></a><!--精彩活动-->
			<ul id="jchd" style="display:block">
            <li><a href="http://www.dao50.com/news/2012716/n54363433.html" target="_blank">单笔充值</a></li>
			<li><a href="http://www.dao50.com/news/2012716/n83663434.html" target="_blank">累积充值</a></li>
			<li><a href="http://www.dao50.com/news/2012716/n90083436.html" target="_blank">开服活动总览</a></li>
            <li><a href="http://www.dao50.com/news/2012716/n80173435.html" target="_blank">升级送好礼</a></li>
			</ul>
		</li>
        <li onclick="showorhide('tswf')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/hzwzl/tswf.jpg"></a><!--特色玩法-->
			<ul id="tswf">
            <li><a href="http://www.dao50.com/news/2012713/n14853417.html" target="_blank">世界BOSS</a></li>
            <li><a href="http://www.dao50.com/news/2012713/n96033420.html" target="_blank">海贼王争霸赛</a></li>
			<li><a href="http://www.dao50.com/news/2012713/n36293412.html" target="_blank">海底宝藏系统</a></li>
			<li><a href="http://www.dao50.com/news/2012713/n37123421.html" target="_blank">海贼团系统</a></li>
			</ul>
		</li>
		<li onclick="showorhide('wjhd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/hzwzl/wjhd.jpg"></a><!--玩家互动-->
        <ul id="wjhd" style="display:block">
        <li><a href="http://bbs.dao50.com/showforum-93.aspx" target="_blank">游戏论坛</a></li>
        </ul>
		<li style="border: 1px solid #1b2446;color:#77d066;line-height:22px;text-align:center;">
        <b>客服中心</b><br />
        400-700-1700<br />
		<b>官方QQ群</b>
		<br>
		249971937
		</li>
        </li>
	</ul>
</div>
</body>
</html>

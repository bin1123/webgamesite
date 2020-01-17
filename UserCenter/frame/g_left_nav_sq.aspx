<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_sq.aspx.cs" Inherits="UserCenter.frame.g_left_nav_sq" %>

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
        body{ background-color:#0c1f23}
        .leftBox {
            height:auto;
	        overflow:hidden;
            left: 0;
            margin:0;
            position: absolute;
            top: 0;
            width: 110px;
	        background-color:#0c1f23;
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
            color: #b4f67a;
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
		<li><a href="http://www.dao50.com/yxzq/sq/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/sqzl/logo.jpg"></a></li>
		<li onclick="showorhide('cz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/sqzl/cz.jpg"></a>
			<ul id="cz">
			<li><a href="http://game.dao50.com/pay/" target="_blank">快速充值入口</a></li>
			</ul>
		</li>
		<li><a href="http://game.dao50.com/xsk/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/sqzl/newbie.jpg"></a></li>
        <li><a href="http://www.dao50.com/news/2012621/n86092984.html" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/sqzl/vip.jpg"></a><!--VIP系统-->	</li>
		<li onclick="showorhide('zd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/sqzl/zd.jpg"></a><!--新手指引-->
			<ul id="zd">
            <li><a href="http://www.dao50.com/news/2012621/n69112975.html" target="_blank">新手FAQ</a></li>
			<li><a href="http://www.dao50.com/news/2012621/n33052979.html" target="_blank">任务系统</a></li>
			<li><a href="http://www.dao50.com/news/2012621/n61192978.html" target="_blank">战斗场面</a></li>
            <li><a href="http://www.dao50.com/news/2012621/n75412980.html" target="_blank">工会系统</a></li>
			<li><a href="http://www.dao50.com/news/2012626/n17993066.html" target="_blank">世界BOSS</a></li>
            <li><a href="http://www.dao50.com/news/2012626/n01243067.html" target="_blank">副本流程</a></li>
			</ul>
		</li>
        <li onclick="showorhide('tswf')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/sqzl/tswf.jpg"></a><!--特色玩法-->
			<ul id="tswf">
            <li><a href="http://www.dao50.com/news/2012621/n20912995.html" target="_blank">资源介绍</a></li>
            <li><a href="http://www.dao50.com/news/2012621/n26512986.html" target="_blank">货币系统</a></li>
			<li><a href="http://www.dao50.com/news/2012626/n58073068.html" target="_blank">宝箱系统</a></li>
			<li><a href="http://www.dao50.com/news/2012621/n25742998.html" target="_blank">占星系统</a></li>
            <li><a href="http://www.dao50.com/news/2012621/n93242994.html" target="_blank">操作系统</a></li>
            <li><a href="http://www.dao50.com/news/2012626/n45653069.html" target="_blank">技能系统</a></li>
			</ul>
		</li>
        <li onclick="showorhide('tz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/sqzl/tz.jpg"></a><!--神曲套装-->
			<ul id="tz">
            <li><a href="http://www.dao50.com/news/2012621/n27513026.html" target="_blank">荣耀套装</a></li>
			<li><a href="http://www.dao50.com/news/2012621/n78433027.html" target="_blank">信仰套装</a></li>
			<li><a href="http://www.dao50.com/news/2012621/n62343028.html" target="_blank">博学套装</a></li>
            <li><a href="http://www.dao50.com/news/2012621/n61253029.html" target="_blank">无畏套装</a></li>
            <li><a href="http://www.dao50.com/news/2012621/n40643030.html" target="_blank">魔罗套装</a></li>
			<li><a href="http://www.dao50.com/news/2012621/n62693031.html" target="_blank">修罗套装</a></li>
			<li><a href="http://www.dao50.com/news/2012621/n96413032.html" target="_blank">元素套装</a></li>
            <li><a href="http://www.dao50.com/news/2012621/n94483033.html" target="_blank">剑师套装</a></li>
			</ul>
		</li>
        <li onclick="showorhide('jchd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/sqzl/jchd.jpg"></a><!--精彩活动-->
			<ul id="jchd" style="display:block">
            <li><a href="http://www.dao50.com/news/2013123/n623821395.html" target="_blank">升级送好礼</a></li>
			<li><a href="http://www.dao50.com/news/2013123/n623821395.html" target="_blank">充值送战魂</a></li>
			<li><a href="http://www.dao50.com/news/2013123/n623821395.html" target="_blank">充值送黄金</a></li>
            <li><a href="http://www.dao50.com/news/2013123/n623821395.html" target="_blank">公会冲级好礼送</a></li>
			<li><a href="http://www.dao50.com/news/2013123/n623821395.html" target="_blank">装备洗练礼品多</a></li>
			<li><a href="http://www.dao50.com/news/2013123/n623821395.html" target="_blank">地下迷宫任我闯</a></li>
            <li><a href="http://www.dao50.com/news/2013123/n623821395.html" target="_blank">超值礼包大乐购</a></li>
			</ul>
		</li>
		<li><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/sqzl/wjhd.jpg"></a><!--玩家互动-->
        <ul style="display:block">
        <li><a href="http://bbs.dao50.com/showforum-86.aspx" target="_blank">游戏论坛</a></li>
        </ul>
		<li style="border: 1px solid #0e1d30;color:#77d066;line-height:22px;text-align:center;">
        <b>客服中心</b><br />
        400-008-5267<br />
		<b>官方QQ群</b>
		<br>
		245594348
		</li>
        </li>
	</ul>
</div>
</body>
</html>

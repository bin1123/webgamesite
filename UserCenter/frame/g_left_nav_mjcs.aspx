<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_mjcs.aspx.cs" Inherits="UserCenter.frame.g_left_nav_mjcs" %>

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
		<li><a href="http://www.dao50.com/yxzq/yjxy/index.html" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/mjcszl/logo.jpg"/></a></li>
		<li onclick="showorhide('cz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/mjcszl/cz.jpg"/></a>
			<ul id="cz">
			<li><a href="http://game.dao50.com/pay/" target="_blank">快速充值入口</a></li>
			<li><a href="http://www.dao50.com/news/201268/n45712755.html" target="_blank">VIP特权</a></li>
			</ul>
		</li>
		<li><a href="http://game.dao50.com/xsk/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/mjcszl/lb.jpg"></a></li>
		<li onclick="showorhide('zd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/mjcszl/zd.jpg"/></a><!--新手指引-->
			<ul id="zd" style="display:block">
            <li><a href="http://www.dao50.com/news/201268/n03212756.html" target="_blank">职业介绍</a></li>
            <li><a href="http://www.dao50.com/news/201268/n10312757.html" target="_blank">新手FAQ</a></li>
			<li><a href="http://www.dao50.com/news/201268/n30162758.html" target="_blank">组队战斗</a></li>
			<li><a href="http://www.dao50.com/news/201268/n18872759.html" target="_blank">新手场景</a></li>
            <li><a href="http://www.dao50.com/news/201268/n28662760.html" target="_blank">通关助手</a></li>
			<li><a href="http://www.dao50.com/news/201268/n93532761.html" target="_blank">阵营选择</a></li>
			</ul>
		</li>
        <li onclick="showorhide('tswf')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/mjcszl/tswf.jpg"/></a><!--特色玩法-->
			<ul id="tswf">
            <li><a href="http://www.dao50.com/news/201268/n32642762.html" target="_blank">武将介绍</a></li>
            <li><a href="http://www.dao50.com/news/201268/n78812767.html" target="_blank">战术心法</a></li>
			<li><a href="http://www.dao50.com/news/201268/n81802768.html" target="_blank">武将培养</a></li>
			<li><a href="http://www.dao50.com/news/2012610/n90852776.html" target="_blank">排兵布阵</a></li>
			</ul>
		</li>
        <li onclick="showorhide('gsjj')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/mjcszl/gsjj.jpg"/></a><!--高手进阶-->
			<ul id="gsjj">
            <li><a href="http://www.dao50.com/news/2012610/n75122774.html" target="_blank">威望系统</a></li>
			<li><a href="http://www.dao50.com/news/2012610/n67222790.html" target="_blank">帝国入侵</a></li>
			<li><a href="http://www.dao50.com/news/2012610/n61742778.html" target="_blank">宝石镶嵌</a></li>
			</ul>
		</li>
        <li onclick="showorhide('yxgl')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/mjcszl/yxgl.jpg"/></a><!--游戏攻略-->
			<ul id="yxgl">
            <li><a href="http://www.dao50.com/news/2012611/n75952799.html" target="_blank">职业选择</a></li>
			<li><a href="http://www.dao50.com/news/2012611/n65322797.html" target="_blank">全职业解析</a></li>
			<li><a href="http://www.dao50.com/news/2012611/n41872800.html" target="_blank">培养秘籍</a></li>
            <li><a href="http://www.dao50.com/news/2012611/n64672798.html" target="_blank">自动挂机系统</a></li>
			</ul>
		</li>
        <li onclick="showorhide('jchd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/mjcszl/jchd.jpg"/></a><!--精彩活动-->
			<ul id="jchd">
            <li><a href="http://www.dao50.com/news/2012611/n14352801.html" target="_blank">升级免费拿奖</a></li>
			<li><a href="http://www.dao50.com/news/2012611/n14352801.html" target="_blank">充值送大礼</a></li>
			<li><a href="http://www.dao50.com/news/2012611/n14352801.html" target="_blank">登陆领金币</a></li>
			</ul>
		</li>
		<li><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/mjcszl/wjhd.jpg"/></a><!--玩家互动-->
        <ul style="display:block">
        <li><a href="http://bbs.dao50.com" target="_blank">游戏论坛</a></li>
        <li><a href="http://bbs.dao50.com/kefuCenter/list.aspx" target="_blank">客服中心</a></li>
        </ul>
		<li style="border: 1px solid #0e1d30;color:#77d066;line-height:22px;text-align:center;">
		<b>官方QQ群</b>
		<br>
		243516905
		</li>
        </li>
	</ul>
</div>
</body>
</html>

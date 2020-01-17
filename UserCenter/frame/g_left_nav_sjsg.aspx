<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_sjsg.aspx.cs" Inherits="UserCenter.frame.g_left_nav_sjsg" %>

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
		<li><a href="http://www.dao50.com/yxzq/sjsg/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/sjsgzl/logo.jpg"></a></li>
		<li onclick="showorhide('cz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/sjsgzl/cz.jpg"></a>
			<ul id="cz" style="display:block">
			<li><a href="http://game.dao50.com/pay/">快速充值入口</a></li>
			<li><a href="http://www.dao50.com/news/201338/n18818018.html" target="_blank">至尊VIP</a></li>
			</ul>
		</li>
		<li><a href="http://game.dao50.com/xsk/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/sjsgzl/lb.jpg"></a>
        <ul style="display:block">
            <li><a href="http://www.dao50.com/news/201338/n55278019.html" target="_blank">新手FAQ</a></li>
			</ul>
        </li>		
        <li onclick="showorhide('gsjj')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/sjsgzl/gsjj.jpg"></a><!--新手指南-->
			<ul id="gsjj">
            <li><a href="http://www.dao50.com/news/201338/n38568025.html" target="_blank">点石成金</a></li>
			<li><a href="http://www.dao50.com/news/201338/n91148028.html" target="_blank">众星捧月</a></li>
			<li><a href="http://www.dao50.com/news/201338/n62318026.html" target="_blank">采灵续命</a></li>
            <li><a href="http://www.dao50.com/news/201338/n04748027.html" target="_blank">再来一箭</a></li>
            <li><a href="http://www.dao50.com/news/201338/n50838030.html" target="_blank">竞技场</a></li>
			</ul>
		</li>
        <li onclick="showorhide('tswf')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/sjsgzl/tswf.jpg"></a><!--特色玩法-->
			<ul id="tswf">
            <li><a href="http://www.dao50.com/news/201338/n93358032.html" target="_blank">三顾茅庐</a></li>
			<li><a href="http://www.dao50.com/news/201338/n07578031.html" target="_blank">神将之力</a></li>
            <li><a href="http://www.dao50.com/news/201338/n40958035.html" target="_blank">武将玩法</a></li>
			<li><a href="http://www.dao50.com/news/201338/n13618036.html" target="_blank">舌战群儒</a></li>
            <li><a href="http://www.dao50.com/news/201338/n24278034.html" target="_blank">荆州争夺</a></li>
			</ul>
		</li>
       <li onclick="showorhide('jchd')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/sjsgzl/jchd.jpg"></a><!--精彩活动-->
			<ul id="jchd" style="display:block">
            <li><a href="http://www.dao50.com/news/201338/n94178017.html" target="_blank">首冲大礼</a></li>
            <li><a href="http://www.dao50.com/news/201338/n79568052.html" target="_blank">在线礼包</a></li>
			<li><a href="http://www.dao50.com/news/201338/n67578056.html" target="_blank">累计充值礼</a></li>
			<li><a href="http://www.dao50.com/news/201338/n73578059.html" target="_blank">封神榜</a></li>
			</ul>
	  </li>
	  <li><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/sjsgzl/wjhd.jpg"></a><!--玩家互动-->
        <ul style="display:block">
        <li><a href="http://www.dao50.com/yxzq/sjsg">游戏官网</a></li>
        <li><a href="http://bbs.dao50.com/showforum-126.aspx">游戏论坛</a></li>
        <li><a href="http://bbs.dao50.com/showforum-129.aspx" target="_blank">客服中心</a></li>
        </ul>
        </li>
	</ul>
</div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_zsg.aspx.cs" Inherits="UserCenter.frame.g_left_nav_zsg" %>

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
body{ background-color:#000}
.leftBox {
	height:auto;
	overflow:hidden;
	left: 0;
	margin:0;
	position: absolute;
	top: 0;
	width: 110px;
	background-color:#000;
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
    border: 1px solid #000;
    color: #b4f67a;
    display: block;
    line-height: 22px;
    text-align: center;
    text-decoration: none;
}
.leftBox li ul li a:hover {
    border: 1px solid #000;
    color:#C00;
	font-weight:bold;
	background-color:#000;
    display: block;
    line-height: 22px;
    text-align: center;
    text-decoration: none;
}
element.style {
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
		<li><a href="http://www.dao50.com/yxzq/zsg/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/zsgzl/logo.jpg"></a></li>
		<li onclick="showorhide('cz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/zsgzl/cz.jpg"></a>
			<ul id="cz">
			<li><a href="http://game.dao50.com/pay/">快速充值入口</a></li>
			<li><a href="http://www.dao50.com/news/2013417/n63058894.html" target="_blank">VIP特权</a></li>
			</ul>
		</li>
		<li><a href="http://game.dao50.com/xsk/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/zsgzl/lb.jpg"></a>
        </li>		
        <li onclick="showorhide('zd')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/zsgzl/zd.jpg"></a><!--新手入门-->
			<ul id="zd">
			<li><a href="http://www.dao50.com/news/2013422/n37358987.html" target="_blank">新手FAQ</a></li>
            <li><a href="http://www.dao50.com/news/2013422/n08328991.html" target="_blank">界面说明</a></li>
            <li><a href="http://www.dao50.com/news/2013422/n71618988.html" target="_blank">游戏背景</a></li>
            <li><a href="http://www.dao50.com/news/zsg_xwgg/" target="_blank">新闻公告</a></li>
            <li><a href="http://www.dao50.com/news/2013423/n20879009.html" target="_blank">热点活动</a></li>
			</ul>
		</li>       
  <li onclick="showorhide('gsjj')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/zsgzl/gsjj.jpg"></a><!--高手-->
			<ul id="gsjj" style="display:block">
            <li><a href="http://www.dao50.com/news/2013423/n88909001.html" target="_blank">武将招募</a></li>
			<li><a href="http://www.dao50.com/news/2013422/n32258992.html" target="_blank">副本系统</a></li>
            <li><a href="http://www.dao50.com/news/2013423/n81308999.html" target="_blank">装备系统</a></li>
			<li><a href="http://www.dao50.com/news/2013423/n12049000.html" target="_blank">布阵系统</a></li>
            <li><a href="http://www.dao50.com/news/2013423/n30139003.html" target="_blank">坐骑系统</a></li>
            <li><a href="http://www.dao50.com/news/2013423/n76349004.html" target="_blank">官职系统</a></li>
			</ul>
		</li>              
	  <li onclick="showorhide('wjhd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/zsgzl/wjhd.jpg"></a><!--玩家互动-->
        <ul id="wjhd" style="display:block">
        <li><a href="http://www.dao50.com/yxzq/zsg/">游戏官网</a></li>
        <li><a href="http://bbs.dao50.com/showforum-134.aspx">游戏论坛</a></li>
        <li style="border: 1px solid #000;color:#77d066;line-height:22px;text-align:center;">
		<b>客服热线</b><br>
		400 008 5267<br/>
        <b>官方QQ</b>
		<br>
		821418824
    </li>
        </ul>
        </li>
	</ul>
</div>
</body>
</html>

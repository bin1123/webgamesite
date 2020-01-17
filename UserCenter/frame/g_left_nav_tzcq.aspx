<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_tzcq.aspx.cs" Inherits="UserCenter.frame.g_left_nav_tzcq" %>

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
		<li><a href="http://www.dao50.com/yxzq/tzcq/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/tzcqzl/logo.jpg"></a></li>
		<li onclick="showorhide('cz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/tzcqzl/cz.jpg"/></a>
			<ul id="cz" style="display:block">
			<li><a href="http://game.dao50.com/pay/">快速充值入口</a></li>
			<li><a href="http://www.dao50.com/news/2013410/n17198703.html" target="_blank">VIP介绍</a></li>
			</ul>
		</li>
		<li><a href="http://game.dao50.com/xsk/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/tzcqzl/lb.jpg"></a>
        <ul style="display:block">
            <li><a href="http://www.dao50.com/news/2013410/n70068731.html" target="_blank">新手FAQ</a></li>
			</ul>
        </li>		
        <li onclick="showorhide('tzrm')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/tzcqzl/tzrm.jpg"></a><!--天尊入门-->
			<ul id="tzrm" style="display:block">
			<li><a href="http://www.dao50.com/news/2013410/n03338700.html" target="_blank">将魂系统</a></li>
            <li><a href="http://www.dao50.com/news/2013410/n77218701.html" target="_blank">宝石系统</a></li>
            <li><a href="http://www.dao50.com/news/2013410/n74098702.html" target="_blank">神器系统</a></li>
			</ul>
		</li>       
        <li onclick="showorhide('tzts')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/tzcqzl/tzts.jpg"></a><!--天尊特色-->
			<ul id="tzts" style="display:block">
            <li><a href="http://www.dao50.com/news/2013410/n58818705.html" target="_blank">煞魔大战</a></li>
			<li><a href="http://www.dao50.com/news/2013410/n08828706.html" target="_blank">boss系统</a></li>
            <li><a href="http://www.dao50.com/news/2013410/n37548708.html" target="_blank">三英榜</a></li>
			<li><a href="http://www.dao50.com/news/2013410/n54118716.html" target="_blank">军团守护</a></li>
            <li><a href="http://www.dao50.com/news/2013410/n11708715.html" target="_blank">丹药系统</a></li>
			</ul>
		</li>       
       <li onclick="showorhide('jchd')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/tzcqzl/jchd.jpg"></a><!--精彩活动-->
			<ul id="jchd" style="display:block">
            <li><a href="http://www.dao50.com/news/2013620/n267211730.html" target="_blank">单笔充值</a></li>
            <li><a href="http://www.dao50.com/news/2013620/n267211730.html" target="_blank">冲级送礼</a></li>
			<li><a href="http://www.dao50.com/news/2013620/n267211730.html" target="_blank">宝石达人</a></li>
			<li><a href="http://www.dao50.com/news/2013620/n267211730.html" target="_blank">装备大师</a></li>
            <li><a href="http://www.dao50.com/news/2013620/n267211730.html" target="_blank">神器降临</a></li>
			</ul>
	  </li>
	  <li onclick="showorhide('wjhd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/tzcqzl/wjhd.jpg"></a><!--玩家互动-->
        <ul id="wjhd" style="display:block">
        <li><a href="http://www.dao50.com/yxzq/tzcq/">游戏官网</a></li>
        <li><a href="http://bbs.dao50.com/showforum-130.aspx">游戏论坛</a></li>
        <li style="border: 1px solid #000;color:#77d066;line-height:22px;text-align:center;">
		<b>客服热线</b><br>
		400 008 5267<br/>
        <b>官方QQ</b>
		<br>
		321418824
    </li>
        </ul>
        </li>
	</ul>
</div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_xlfc.aspx.cs" Inherits="UserCenter.frame.g_left_nav_xlfc" %>

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
            body{ background-color:#24b5f8}
            .leftBox {
                height:auto;
	            overflow:hidden;
                left: 0;
                margin:0;
                position: absolute;
                top: 0;
                width: 110px;
	            background-color:#24b5f8;
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
                border: 1px solid #24b5f8;
                color: #b4f67a;
                display: block;
                line-height: 22px;
                text-align: center;
                text-decoration: none;
            }
            .leftBox li ul li a:hover {
                border: 1px solid #24b5f8;
                color:#C00;
	            font-weight:bold;
	            background-color:#333;
                display: block;
                line-height: 22px;
                text-align: center;
                text-decoration: none;
            }
            element.style {
                border: 1px solid #24b5f8;
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
		<li><a href="http://www.dao50.com/yxzq/xlfc/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/xlfczl/logo.jpg"></a></li>
		<li onclick="showorhide('cz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/xlfczl/cz.jpg"></a>
			<ul id="cz">
			<li><a href="http://game.dao50.com/pay/">快速充值入口</a></li>
            <li><a href="http://www.dao50.com/news/2012720/n51923562.html">首充礼包</a></li>
			<li><a href="http://www.dao50.com/news/2012720/n10583570.html" target="_blank">VIP特权</a></li>
			</ul>
		</li>
		<li><a href="http://game.dao50.com/xsk/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/xlfczl/lb.jpg"></a></li>		
        <li  onclick="showorhide('tswf')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/xlfczl/tswf.jpg"></a><!--特色玩法-->
			<ul id="tswf" style="display:block">
            <li><a href="http://www.dao50.com/news/2012720/n71363639.html" target="_blank">护送寒冰</a></li>
            <li><a href="http://www.dao50.com/news/2012720/n30003637.html" target="_blank">世界boss</a></li>
			<li><a href="http://www.dao50.com/news/2012720/n34083645.html" target="_blank">沙滩日光浴</a></li>
			<li><a href="http://www.dao50.com/news/2012720/n91473638.html" target="_blank">仙宠系统</a></li>
            <li><a href="http://www.dao50.com/news/2012720/n44133635.html" target="_blank">通灵宝玉</a></li>
			</ul>
		</li>
        <li onclick="showorhide('yxgl')" class="block"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/xlfczl/yxgl.jpg"></a><!--游戏攻略-->
			<ul id="yxgl" style="display:block">
            <li><a href="http://www.dao50.com/news/2012720/n84113591.html" target="_blank">职业选择</a></li>
			<li><a href="http://www.dao50.com/news/2012720/n50143593.html" target="_blank">基本操作</a></li>
			</ul>
		</li>
		<li><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/xlfczl/wjhd.jpg"></a><!--玩家互动-->
        <ul id="wjhd" style="display:block">
        <li><a href="http://bbs.dao50.com/showforum-97.aspx">游戏论坛</a></li>
        <li><a href="http://bbs.dao50.com/kefuCenter/list.aspx" target="_blank">客服中心</a></li>
        </ul>
		<li style="border: 1px solid #24b5f8;color:#77d066;line-height:22px;text-align:center;">
		<b>客服热线</b>
		<br>
		400 008 5267<br/>
        <b>官方QQ群</b>
		<br>
		167249393
		</li>
        </li>
	</ul>
</div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_yjxy.aspx.cs" Inherits="UserCenter.frame.g_left_nav_yjxy" %>

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
        .leftBox {
            height:auto;
	        overflow:hidden;
            left: 0;
            margin:0;
            position: absolute;
            top: 0px;
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
            border: 1px solid #000000;
            color: #DDE870;
            display: block;
            line-height: 22px;
            text-align: center;
            text-decoration: none;
        }
        .leftBox li ul li a:hover {
            border: 1px solid #666;
            color:#F60;
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
	    <li><a target="_blank" href="http://www.dao50.com/yxzq/yjxy/index.html"><img alt="" src="<%=sWebUrl %>/wldFolder/yjxyzl/logo.jpg"></a></li>
	    <li onclick="showorhide('cz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/yjxyzl/cz.jpg"/></a>
		    <ul id="cz" style="display:block">
		    <li><a target="_blank" href="http://game.dao50.com/pay/">快速充值入口</a></li>
		    <li><a target="_blank" href="http://www.dao50.com/news/2012526/n09772555.html#"><font color="#FF0000">首次充值送大礼</font></a></li>
		    <li><a target="_blank" href="http://www.dao50.com/news/2012514/n09702417.html">VIP等级介绍</a></li>
            <li><a target="_blank" href="http://www.dao50.com/news/2012526/n09772555.html#"><font color="#FF0000">单笔充值返利</font></a></li>
		    </ul>
	    </li>
	    <li><a href="http://game.dao50.com/xsk/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/yjxyzl/newbie.jpg"></a></li>
	    <li onclick="showorhide('bz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/yjxyzl/bz.jpg"></a>
		    <ul id="bz">
		    <li><a target="_blank" href="http://www.dao50.com/news/2012514/n39392421.html">创建角色</a></li>
		    <li><a target="_blank" href="http://www.dao50.com/news/2012511/n51182357.html">新手导引</a></li>
		    <li><a target="_blank" href="http://www.dao50.com/news/2012521/n09292503.html">职业介绍</a></li>
		    <li><a target="_blank" href="http://www.dao50.com/news/2012514/n06042397.html">新手快速成长</a></li>
            <li><a target="_blank" href="http://www.dao50.com/news/2012511/n67342356.html">界面说明</a></li>
		    <li><a target="_blank" href="http://www.dao50.com/news/2012514/n27222416.html">功能开启条件</a></li>
		    <li><a target="_blank" href="http://www.dao50.com/news/2012511/n54172348.html">组队系统</a></li>
		    <li><a target="_blank" href="http://www.dao50.com/news/2012511/n22902351.html">武技阁</a></li>
            <li><a target="_blank" href="http://www.dao50.com/news/2012511/n99602352.html">目标系统</a></li>
		    </ul>
	    </li>
	    <li onclick="showorhide('bd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/yjxyzl/bd.jpg"></a>
		    <ul id="bd">
            <li><a target="_blank" href="http://www.dao50.com/news/2012514/n61862391.html">经脉系统</a></li>
            <li><a target="_blank" href="http://www.dao50.com/news/2012511/n06772366.html">炼魂系统</a></li>
            <li><a target="_blank" href="http://www.dao50.com/news/2012511/n17772365.html">镇妖塔</a></li>
            <li><a target="_blank" href="http://www.dao50.com/news/2012511/n20792363.html">秘境寻宝</a></li>
            <li><a target="_blank" href="http://www.dao50.com/news/2012511/n29492361.html">神炉系统</a></li>
            <li><a target="_blank" href="http://www.dao50.com/news/2012511/n63472350.html">奴隶系统</a></li>
            <li><a target="_blank" href="http://www.dao50.com/news/2012510/n14132335.html">竞技对决</a></li>
            <li><a target="_blank" href="http://www.dao50.com/news/2012511/n26062349.html">世界BOSS</a></li>
            </ul>
	    </li>
	    <li><img alt="" src="<%=sWebUrl %>/wldFolder/yjxyzl/hd.jpg"/>
        <ul>
        <li><a target="_blank" href="http://bbs.dao50.com/showforum-76.aspx">游戏论坛</a></li>
        </ul>
	    <li style="border: 1px solid #000;color:#DDE870;line-height:22px;text-align:center;">
        <b>客服中心</b>
	    <br>
	    400-700-1700<br />
	    <b>官方QQ群</b>
	    <br>
	    50764751
	    </li>
        </li>
    </ul>
</div>
</body>
</html>

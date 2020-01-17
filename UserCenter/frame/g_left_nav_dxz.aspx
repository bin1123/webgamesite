<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_dxz.aspx.cs" Inherits="UserCenter.frame.g_left_nav_dxz" %>

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
        body{ background-color:#033954}
        .leftBox {
	        height:48;
	        overflow:hidden;
	        left: 0;
	        margin:0;
	        position: absolute;
	        top: 0;
	        width: 110px;
	        background-color:#033954;
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
            border: 1px solid #033954;
            color: #b4f67a;
            display: block;
            line-height: 22px;
            text-align: center;
            text-decoration: none;
        }
        .leftBox li ul li a:hover {
            border: 1px solid #033954;
            color:#C00;
	        font-weight:bold;
	        background-color:#333;
            display: block;
            line-height: 22px;
            text-align: center;
            text-decoration: none;
        }
        element.style {
            border: 1px solid #033954;
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
		<li><a href="http://www.dao50.com/yxzq/dxz/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/dxzzl/logo.jpg"></a></li>
		<li onclick="showorhide('cz')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/dxzzl/cz.jpg"></a>
			<ul id="cz">
			<li><a href="http://game.dao50.com/pay/">快速充值入口</a></li>
			<li><a href="http://www.dao50.com/news/2012914/n16144539.html" target="_blank">VIP介绍</a></li>
			</ul>
		</li>
		<li><a href="http://game.dao50.com/xsk/" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/dxzzl/lb.jpg"></a></li>		
        <li onclick="showorhide('zd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/dxzzl/tswf.jpg"></a><!--新手指南-->
			<ul id="zd">
            <li><a href="http://www.dao50.com/news/2012914/n13934536.html" target="_blank">新手FAQ</a></li>
            <li><a href="http://www.dao50.com/news/2012914/n85944558.html" target="_blank">装备简介</a></li>
			<li><a href="http://www.dao50.com/news/2012914/n11624602.html" target="_blank">日常任务</a></li>
			<li><a href="http://www.dao50.com/news/2012914/n50244603.html" target="_blank">建立帮会</a></li>
            <li><a href="http://www.dao50.com/news/2012914/n89994565.html" target="_blank">帮会任务</a></li>
            <li><a href="http://www.dao50.com/news/2012914/n66434604.html" target="_blank">出阵设置</a></li>
			</ul>
		</li>       
  <li onclick="showorhide('tswf')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/dxzzl/yxgl.jpg"></a><!--特色玩法-->
			<ul id="tswf">
            <li><a href="http://www.dao50.com/news/2012914/n90734543.html" target="_blank">培养段位</a></li>
			<li><a href="http://www.dao50.com/news/2012914/n03274554.html" target="_blank">提升境界</a></li>
            <li><a href="http://www.dao50.com/news/2012914/n60114556.html" target="_blank">武魂强化</a></li>
			<li><a href="http://www.dao50.com/news/2012914/n35664606.html" target="_blank">宅院系统</a></li>
			</ul>
		</li>       
       <li onclick="showorhide('gsjd')"><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/dxzzl/gsjj.jpg"></a><!--高手阶段-->
			<ul id="gsjd" style="display:block">
            <li><a href="http://www.dao50.com/news/2012914/n82974545.html" target="_blank">异兽召唤</a></li>
            <li><a href="http://www.dao50.com/news/2012914/n40894546.html" target="_blank">异兽名典</a></li>
			<li><a href="http://www.dao50.com/news/2012914/n06264548.html" target="_blank">异兽培养</a></li>
			<li><a href="http://www.dao50.com/news/2012914/n42504547.html" target="_blank">异兽天赋</a></li>
            <li><a href="http://www.dao50.com/news/2012914/n10694540.html" target="_blank">侠侣招募</a></li>
            <li><a href="http://www.dao50.com/news/2012914/n22184541.html" target="_blank">侠侣名典</a></li>
			</ul>
	  </li>        
        <li><a href="http://www.dao50.com/news/2012913/n73914499.html" target="_blank"><img alt="" src="<%=sWebUrl %>/wldFolder/dxzzl/hdhl.jpg"></a></li>                
	  <li><a href="#"><img alt="" src="<%=sWebUrl %>/wldFolder/dxzzl/wjhd.jpg"></a><!--玩家互动-->
        <ul id="wjhd" style="display:block">
        <li><a href="http://www.dao50.com/yxzq/dxz/">游戏官网</a></li>
        <li><a href="http://bbs.dao50.com/showforum-105.aspx">游戏论坛</a></li>
        <li><a href="http://bbs.dao50.com/kefuCenter/list.aspx" target="_blank">客服中心</a></li>
        </ul>
		<li style="border: 1px solid #033954;color:#77d066;line-height:22px;text-align:center;">
		<b>客服热线</b>
		<br>
		400 008 5267<br/>
        <b>官方QQ群</b>
		<br>
		263415512
    </li>
        </li>
	</ul>
</div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_top_nav_l.aspx.cs" Inherits="UserCenter.frame.g_top_nav_l" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <link href="<%=sWebUrl %>/wldFolder/css/dingbu_css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="top">
		    <div class="top_content">
            <div class="top_logo"><a href="http://www.dao50.com" target="_blank"><img src="<%=sWebUrl %>/wldFolder/images/top_logo.jpg" /></a></div>
            <div class="top_youxi">
            <ul>
            <li class="sssg"><a href="http://www.dao50.com/yxzq/sq/" target="_blank">神曲</a></li>
            <li>|</li> 
            <li class="mg"><a href="http://www.dao50.com/yxzq/dxz/" target="_blank">大侠传</a></li> 
            <li>|</li>
            <li class="ztx"><a href="http://www.dao50.com/yxzq/swjt/" target="_blank">神武九天</a></li>
            <li>|</li>
            <li class="asqx"><a href="http://www.dao50.com/yxzq/sgyjz/" target="_blank">三国英杰传</a></li>
            </ul>
            </div>
            <ul class="xiaoxi">
            <marquee id="msg" onmouseover="this.stop()" onmouseout="this.start()" direction="up" height="20" scrollAmount="1">
                <li><a href="http://www.dao50.com" title="到武林" target="_blank">欢迎来到到武林平台！我们将提供优质的服务！谢谢您的支持！</a></li>
            </marquee>
            </ul>
            <div class="shouye"><a href="http://www.dao50.com" target="_blank">首页</a> | <a href="http://game.dao50.com/pay" target="_blank">充值</a> | <a href="#"  onclick ="javascript:window.external.AddFavorite(document.URL,document.title);return false" rel="sidebar">收藏本页</a></div>
            </div>
	 </div>
</body>
</html>

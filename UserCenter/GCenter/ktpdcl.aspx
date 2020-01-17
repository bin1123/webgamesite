<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ktpdcl.aspx.cs" Inherits="UserCenter.GCenter.ktpdcl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
	<title>服务器列表</title>
	<meta name="keywords" content="将神,心动将神,心动游戏,将神官网,网页游戏,webgame,官网">
	<meta http-equiv="X-UA-Compatible" content="IE=edge">
	<meta name="description" content="《开天辟地》是一款以上古神话为背景的角色扮演游戏。游戏者将以天界一名修仙者的身份开启一段仙履奇缘，在人间寻踪消失的二十八星宿的过程中，逐渐揭开世间异变的真实端倪。开天辟地采用目前独业内领先的无缝剧情动画衔接，将使游戏者体验到不同凡响的感动。此外游戏内置许多新颖互动玩法，无论是蜀山论剑亦或是守卫唐僧等全新系统都将给人带来耳目一新的感受。">
	<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"> 
	
	<link type="text/css" rel="stylesheet" href="http://file.dao50.com/ktpdcl/css/wd.css" /> 
</head>
<body>
    <div id="nav">
         <a href="http://www.dao50.com/yxzq/ktpd" target="_blank">官网</a>
        | 
        <a href="http://bbs.dao50.com" target="_blank">论坛</a>
    </div>
    <div id="content"></div>
    <div class="fwqlist">
      <div class="title00">
                <h3>服务器列表</h3>
            </div>
            <div class="fwqlistcon">
              <div class="fwq">
                <h2>推荐服务器</h2>
                <div id="tjfwq" class="tjfwq"><a href="javascript:LoadGame('ktpd1');">1服_零零一服</a></div>
                <h2>全部服务器</h2>
                <div id="allserver" class="qbfwq">
                    <a href="javascript:LoadGame('ktpd1');">1服_零零一服</a><br />
                </div>
              </div>
             </div>
    </div>
    <!--新闻公告++攻略心得-->
    <div id="aside">
        <div id="news" class="section">
            <div class="header">
                <h1>新闻公告</h1>
                <a class="more" href="http://www.dao50.com/news/ktpd_yxgg/index.html" target="_blank">更多 +</a>
            </div>
            <ul>
                                    <li><a href="http://www.dao50.com/news/2014319/n499027747.html"  target="_blank">【新闻公告】爱情公寓活动</a><span>2014-03-24</span></li>
                                    <li><a href="http://www.dao50.com/news/2014317/n046327634.html"  target="_blank">【新闻公告】开服活动总揽</a><span>2014-03-24</span></li>
                                    <li><a href="http://www.dao50.com/news/2014319/n682827788.html"  target="_blank">【新闻公告】游戏火爆上线</a><span>2014-03-24</span></li>
                            </ul>
        </div>
        
        <div id="strategy" class="section right">
            <div class="header">
                <h1>玩家攻略</h1>
                <a class="more" href="http://www.dao50.com/news/ktpd_yxgl/index.html" target="_blank">更多 +</a>
            </div>
            <ul>
                           <li>
                                <a href="http://www.dao50.com/news/2014317/n427427635.html" target="_blank" >【新版攻略】开服一个月攻略</a>
                                <span>2014-03-24</span>
                            </li>
                            <li>
                                <a href="http://www.dao50.com/news/2014317/n211127636.html" target="_blank" >【新版攻略】达摩答题前100题解答</a>
                                <span>2014-03-24</span>
                            </li>
                            <li>
                                <a href="http://www.dao50.com/news/2014317/n644427637.html" target="_blank" >【新版攻略】名仙技能</a>
                                <span>2014-03-24</span>
                            </li>
            </ul>
        </div>
    </div>
    </body>
</html>

<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/ktpdcl.js"></script>
<script type="text/javascript">
    
    $(document).ready(function() {
        
        ServerSel();
    });
    
</script>

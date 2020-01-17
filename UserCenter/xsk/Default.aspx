<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserCenter.xsk.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="Keywords" content="墨攻,魔塔世界,傲视天地,明朝时代,弹弹堂,网页游戏平台,网页游戏,webgame,免费网页游戏,门户网页游戏,休闲游戏,最新网页游戏,网页游戏外挂,免费下载,免费在线,到武林游戏平台,到武林"/>
<meta name="description" content="到武林网页游戏平台（http://www.dao50.com/）拥有国内最丰富的WEB游戏和庞大的游戏玩家，这里汇聚了最热门的墨攻、战天下、明珠三国、傲视千雄、傲视天地、明朝时代等精品游戏。力求打造具有民族特色、绿色健康的游戏平台。"/>
<title>新手礼包 | 网页游戏平台-dao50网页游戏| 玩网页游戏就来到武林游戏平台http://www.dao50.com/</title>
<link href="http://css.dao50.com/wldFolder/css/index_css.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/Game.js"></script>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/Code.js"></script>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript">
    function servercodebind(obj) {
        ServerSelOfGameVal('serverlist', obj.value, "==请选择==");
        CodeSel(obj.value);
    }

    function GetNewCode() {
        var ServerAbbre = $("#serverlist").val();
        var CodeType = $("#cardtype").val();
        CodeGet(ServerAbbre,CodeType);
    }

    $(document).ready(function() {
        GameSel('gamelist', "==请选择==");
    });
</script>
</head>

<body><!--top结束-->
<div id="logo">
	<a href="#"><img src="<%=sWebUrl %>/wldFolder/images/logo_big.jpg" /></a>
    <a href="#"><img src="<%=sWebUrl %>/wldFolder/images/top_guanggao.jpg" /></a>
	</div>
<div id="wrap"><!--wrap开始-->
    <!--#include file="../inc/PageTopLink.htm"--><!--nav结束-->
    <div id="content"><!--content开始-->
    <div id="mid_left"><!--左侧公共部分开始-->
	<div id="login_content">
    <iframe src="/Services/Login.aspx" frameborder="0" width="194px" height="212px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div>
    <div id="mid_left02">
    <iframe src="<%=sWebUrl %>/news/czzxzxl/" frameborder="0" width="195px" height="365px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div>
    </div><!--左侧公共部分结束-->
    <div id="mid_right"><!--mid_right开始-->
    <div id="content_mid1">
    <div id="content_mid2">
    <ul>
    <li>
    <span class="m01">到武林红包领取</span>
    </li>
    </ul>
    </div>
    <div id="mid_righthb"><!--红包领取开始-->
    	<ul>
        <li><center><strong>游戏礼包领取</strong></center></li>
        <li class="wz">
        <span style="margin-right:12px;">选择游戏：</span> 
          <select name="gamelist" onchange="servercodebind(this)" id="gamelist">
            <option value="">==请选择==</option>
          </select>
        *请选择对应游戏
        </li>
        <li class="wz">
        选择服务器：
        <select name="serverlist" id="serverlist">
            <option value="">==请选择==</option>
          </select> 
        *请选择对应游戏的服务器
        </li>
        <li class="wz">
        选择卡类型： 
        <select name="cardtype" onchange="CodeUrlSel(this.value)" id="cardtype">
            <option value="">==请选择==</option>
          </select>
        *请选择对应游戏礼包的种类
        </li>
        <li class="wz">
          <a style="border:1px solid #ff0000" href="javascript:" onclick="GetNewCode()">点击申请</a>
          <a style="border:1px solid #ff0000" href="../UCenter/reg.aspx" target="_blank">马上注册账号</a>
        </li>
        <li id="succtext" class="wzh"></li>
        <li><hr color="#77b3ff" size="1" /></li>
        </ul>
    </div>
    <div id="lqsb"><!--领取新手卡失败  显示 开始-->
    <iframe src="" id="xskhelp" frameborder="0" width="606px" height="400px" marginheight="0" marginwidth="0"></iframe>
    <div id="lqhb_sb" style="display:none">
	    <div id="tanchuang01"><font color="#ffffff">领取新手卡失败</font></div>
        <div class="lqsb_01">
       <center><img src="<%=sWebUrl %>/wldFolder/images/xsk_img.jpg" /></center>
        <p id="errtxt" class="text_small"></p>
        <p class="text_small">&nbsp;</p>
        <p><center>
        <input type="button" onclick="errtxthide()" style="background:url(http://image.dao50.com/wldFolder/images/Btn_qeuding.jpg) no-repeat left top; width:92px; height:30px; border:none; margin:0; padding:0" />
        <input type="button" onclick="errtxthide()" style="background:url(http://image.dao50.com/wldFolder/images/Btn_fanhui.jpg) no-repeat left top; width:92px; height:30px; border:none; margin:0; padding:0" /></center></p>
        </div>
    </div>
    </div><!--领取新手卡失败  显示结束-->
    <!--红包领取结束-->
    </div>
    </div><!--mid_right结束-->
    </div><!--mid结束-->
	<div id="foot_00"><!--foot开始-->
    <iframe src="<%=sWebUrl %>/foot.html" frameborder="0" width="824px" height="150px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div> <!--foot结束-->  
</div><!--wrap结束-->
</body>
</html>

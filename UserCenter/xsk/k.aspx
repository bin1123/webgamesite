<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="k.aspx.cs" Inherits="UserCenter.xsk.k" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>新手礼包</title>
<link href="http://css.dao50.com/wldFolder/css/pxsk.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/Game.js"></script>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/Code.js"></script>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript">
    function servercodebind(obj) {
        ServerSelOfGame('serverlist', obj.value, "==请选择==");
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

<body>
<div class="con">
	<div class="con_t"><!--红包领取开始-->
    	<ul>
        <li><a href="javascript:void(0)"><h3>游戏礼包领取</h3></a></li>       
        <li>
        <span style="margin-right:12px;">选择游戏：</span> 
          <select name="gamelist" onchange="servercodebind(this)" id="gamelist">
            <option>==请选择==</option>
          </select>
        *请选择对应游戏
        </li>
        <li>
        选择服务器：
        <select name="serverlist" id="serverlist">
            <option>==请选择==</option>
          </select> 
        *请选择对应游戏的服务器
        </li>
        <li>
        选择卡类型： 
        <select name="cardtype" onchange="PCodeUrlSel(this.value)" id="cardtype">
            <option>==请选择==</option>
          </select>
        *请选择对应游戏礼包的种类
        </li>
        <li>
          <a style="border:solid 1px #F00" href="javascript:void(0)" onclick="GetNewCode()">点击申请</a>
        </li>
        <li id="succtext" class="wzh"></li>
        <li><hr color="#77b3ff" size="1" /></li>
        </ul>
    </div>
    <div class="con_b"><!--领取新手卡失败  显示 开始-->
    <iframe id="xskhelp" src="" frameborder="0" width="720px" height="400px" marginheight="0" marginwidth="0"></iframe>
    <div class="lqhb_sb" style="display:none">
	    <div class="tanchuang01"><font color="#ffffff">领取新手卡失败</font></div>
        <div class="lqsb_01">
       <center><img src="<%=sWebUrl %>/wldFolder/images/xsk_img.jpg" /></center>
        <p id="errtxt" class="text_small"></p>
        <p class="text_small">&nbsp;</p>
        <p><center>
        <input type="button" onclick="errtxthide()" style="background:url(images/Btn_qeuding.jpg) no-repeat left top; width:92px; height:30px; border:none; margin:0; padding:0" />        　
        <input type="button" onclick="errtxthide()" style="background:url(images/Btn_fanhui.jpg) no-repeat left top; width:92px; height:30px; border:none; margin:0; padding:0" /></center></p>
        </div>
    </div>
<!--领取新手卡失败  显示结束-->
    </div>
</div>
</body>
</html>

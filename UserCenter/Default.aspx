<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UserCenter.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="Keywords" content="墨攻,魔塔世界,傲视天地,明朝时代,弹弹堂,网页游戏平台,网页游戏,webgame,免费网页游戏,门户网页游戏,休闲游戏,最新网页游戏,网页游戏外挂,免费下载,免费在线,到武林游戏平台,到武林"/>
    <meta name="description" content="到武林网页游戏平台（http://www.dao50.com/）拥有国内最丰富的WEB游戏和庞大的游戏玩家，这里汇聚了最热门的墨攻、战天下、明珠三国、傲视千雄、傲视天地、明朝时代等精品游戏。力求打造具有民族特色、绿色健康的游戏平台。"/>
    <title>平台登陆页 | 网页游戏平台-dao50网页游戏| 玩网页游戏就来到武林游戏平台http://www.dao50.com/</title>
    <link href="<%=sWebUrl %>/wldFolder/css/index_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript">
        function AccountCheck() {
            var account = $("#account").val();
            if (account != "") {
                return true;
            }
            else {
                alert('账号不能为空！');
                return false;
            }
        }

        function PWDCheck() {
            var PassWordOne = $("#pwdone").val();
            if (PassWordOne != "") {
                return true;
            }
            else {
                alert('密码不能为空！');
                return false;
            }
        }

        function checkAll() {
            if (AccountCheck() && PWDCheck()) {
                document.getElementById("form1").submit();
            }
            else {
                return false;
            }
        }
    </script>
</head>

<body>
    <script type="text/javascript" src="<%=sRootUrl %>/Inc/SiteTop.aspx"></script>
<div id="logo">
	<a href="#"><img src="<%=sWebUrl %>/wldFolder/images/logo_big.jpg" /></a>
    <a href="#"><img src="<%=sWebUrl %>/wldFolder/images/top_guanggao.jpg" /></a>
	</div>
<div id="wrap">        
    <!--wrap开始-->
    <!--#include file="/inc/PageTopLink.htm"-->
	<div id="denglu">
    	<div id="denglu_left">
        <div id="denglu_left01">
        <form id="form1" action="Default.aspx" method="post">
        <p>账 号：
          <input type="text" name="account" maxlength="16" id="account" class="denglu_txt" />
        </p>
        <p>密 码：
          <input type="password" name="password" maxlength="16" id="password" class="denglu_txt" />
        </p>
        <p>
          <input type="checkbox" name="checkbox" id="checkbox" class="denglu_ch" />记住密码
          <a href="#" class="denglu_ch0" target="_blank">找回密码？</a></p>
        <p><a href="#" onclick="checkAll()" title="点击登陆"><img src="<%=sWebUrl %>/wldFolder/images/denglu00.jpg" /></a></p>
        <input type="hidden" name="url" value="<%=sUrl %>"/>
        <input type="hidden" name="gname" value="<%=sGName %>"/>
        </form>
        </div>
        </div>
        <div id="denglu_right">
        	<div id="right_01">
            <img src="<%=sWebUrl %>/wldFolder/images/denglu_img.jpg" border="0" usemap="#Map" />
            <map name="Map" id="Map">
              <area shape="poly" coords="47,205,56,182,155,182,146,206" href="<%=sRootUrl %>/UCenter/reg.aspx" target="_blank" />
            </map>
            </div>
      </div>
    </div>
	<div id="foot_00"><!--foot开始-->
    <iframe src="<%=sWebUrl %>/foot.html" frameborder="0" width="824px" height="150px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div> <!--foot结束-->  
</div><!--wrap结束-->
</body>
<%=sMsg %>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zsgcl.aspx.cs" Inherits="UserCenter.GCenter.zsgcl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>战三国登陆页面</title>
    <link href="<%=sWebUrl %>/wldFolder/zsgcl/style/master.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript">
        function regshow() {
            $('#gamereg').show();
            $('#gamelogin').hide();
        }

        function loginshow() {
            $('#gamereg').hide();
            $('#gamelogin').show();
        }

        function loginVal() {
            var username = $("#account").val();
            var password = $("#pwdone").val();
            if (username == "" || username == "请输入用户名") {
                alert("请输入用户名");
                return false;
            }
            else if (password == "") {
                alert("密码不能为空");
                return false;
            }
            else if (username.length < 4 || username.length > 16) {
                alert("用户名格式不正确!");
                return false;
            }
            else if (password.length < 6 || password.length > 16) {
                alert("密码格式不正确!");
                return false;
            }
            else {
                document.getElementById("login").submit();
                return true;
            }
        }

        function regVal() {
            var username = $("#accountreg").val();
            var password = $("#pwdonereg").val();
            var passwordtwo = $("#pwdtwo").val();
            if (username == "" || username == "请输入您的帐号") {
                alert("请输入帐号");
                return false;
            }
            else if (password == "") {
                alert("密码不能为空");
                return false;
            }
            else if (passwordtwo == "") {
                alert("确认密码不能为空");
                return false;
            }
            else if (username.length < 4 || username.length > 16) {
                alert("用户名格式不正确");
                return false;
            }
            else if (password.length < 6 || password.length > 16) {
                alert("密码格式不正确");
                return false;
            }
            else if (password != passwordtwo) {
                alert("密码和确认密码不一致,请核对!");
                return false;
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/Services/Ajax.ashx",
                    data: "AjaxType=ValName&Account=" + username,
                    beforeSend: function() {
                    },
                    success: function(data) {
                        if ("0" != data) {
                            alert("用户名已经存在!");
                        }
                        else {
                            document.getElementById("reg").submit();
                        }
                    }
                });
            }

        }

        function newchange(newtype) { 
            switch(newtype)
            {
                case "zx":
                    $('#zxxw').show();
                    $('#xw').hide();
                    $('#ggxw').hide();
                    $('#hdxw').hide();
                    break;
                case "xw":
                    $('#zxxw').hide();
                    $('#xw').show();
                    $('#ggxw').hide();
                    $('#hdxw').hide();
                    break;
                case "gg":
                    $('#zxxw').hide();
                    $('#xw').hide();
                    $('#ggxw').show();
                    $('#hdxw').hide();
                    break;
                case "hd":
                    $('#zxxw').hide();
                    $('#sw').hide();
                    $('#ggxw').hide();
                    $('#hdxw').show();
                    break;                    
            }
        }
    </script>
</head>
<body scroll="no" style="overflow:hidden;background-color:#000;">
<div class="layout">
  <div class="menu"> 
  	<a href="http://www.dao50.com/yxzq/zsg/" target="_blank" class="m1"></a> 
    <a href="http://game.dao50.com/pay/" target="_blank" class="m2"></a> 
    <a href="http://www.dao50.com/news/zsg_yxzl/" target="_blank" class="m3"></a> 
    <a href="http://bbs.dao50.com/showforum-134.aspx" target="_blank" class="m4"></a> 
 </div>
  <div class="focus">
    <div class="pices"><a href="http://www.dao50.com/news/zsg_xwgg/" target="_blank"><img src="<%=sWebUrl %>/wldFolder/zsgcl/images/10181361340967911.jpg"></a></div>
    <ul class="trigger">
    </ul>
  </div>
  <div class="news">
    <div class="hd">
      <ul class="newsTab">
        <li><a href="#" onmouseover="newchange('zx')">最新</a></li>
        <li><a href="#" onmouseover="newchange('xw')">新闻</a></li>
        <li><a href="#" onmouseover="newchange('gg')">公告</a></li>
        <li><a href="#" onmouseover="newchange('hd')">活动</a></li>
      </ul>
      <a id="morelink" href="http://www.dao50.com/news/zsg_xwgg/" class="more" target="_blank">更多>></a> </div>
    <div class="bd">
      <ul id="zxxw" class="newsContent" style="display:block;">
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013417/n67278895.html" title="《战·三国》震撼上线" target="_blank">《战·三国》震撼上线</a> </li>
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013423/n78539010.html" title="到武林《战·三国》双线1服“天下争霸”4月24日11:00火爆开启" target="_blank">1服“天下争霸”4月24日11:00火爆开启</a> </li>
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013417/n63058894.html" title="《战·三国》VIP特权" target="_blank">《战·三国》VIP特权</a> </li>
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013423/n71329008.html" title="《战·三国》首冲送礼" target="_blank">《战·三国》首冲送礼</a> </li>
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013423/n20879009.html" title="到武林《战·三国》开服活动" target="_blank">到武林《战·三国》开服活动</a> </li>
      </ul>
      <ul id="xw" class="newsContent">
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013417/n67278895.html" title="《战·三国》震撼上线" target="_blank">《战·三国》震撼上线</a> </li>
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013423/n78539010.html" title="到武林《战·三国》双线1服“天下争霸”4月24日11:00火爆开启" target="_blank">1服“天下争霸”4月24日11:00火爆开启</a> </li>
      </ul>
      <ul id="ggxw" class="newsContent">
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013417/n67278895.html" title="《战·三国》震撼上线" target="_blank">《战·三国》震撼上线</a> </li>
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013423/n78539010.html" title="到武林《战·三国》双线1服“天下争霸”4月24日11:00火爆开启" target="_blank">1服“天下争霸”4月24日11:00火爆开启</a> </li>
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013417/n63058894.html" title="《战·三国》VIP特权" target="_blank">《战·三国》VIP特权</a> </li>
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013423/n71329008.html" title="《战·三国》首冲送礼" target="_blank">《战·三国》首冲送礼</a> </li>
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013423/n20879009.html" title="到武林《战·三国》开服活动" target="_blank">到武林《战·三国》开服活动</a> </li>
      </ul>
      <ul id="hdxw" class="newsContent">
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013417/n63058894.html" title="《战·三国》VIP特权" target="_blank">《战·三国》VIP特权</a> </li>
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013423/n71329008.html" title="《战·三国》首冲送礼" target="_blank">《战·三国》首冲送礼</a> </li>
        <li> <em>04/24</em> <b>·</b><a href="http://www.dao50.com/news/2013423/n20879009.html" title="到武林《战·三国》开服活动" target="_blank">到武林《战·三国》开服活动</a> </li>
      </ul>
    </div>
  </div>
  <br clear="all">
  <div class="login_area">
    <div class="loginbox" id="gamelogin">
        <form id="login" name="login" method="post">
    	<div id="login_ts"></div>
    	<dl class="login">           
            <dd><span>账号：</span><input type="text" class="int1" id="account" name="account" size="15" tabindex="1" maxlength="16" value="" /></span></dd>
            <dd><span>密码：</span><input class="int1" id="pwdone" name="pwdone" type="password" size="15" tabindex="2" maxlength=16 value="" /></dd>            
        <dd class="opt">
        	<input type="checkbox" id="autologin"  />
          <label for="autologin">下次自动登录</label>
          <a href="#" target="_blank">忘记密码</a>
        </dd>
        </dl>
         <a href="#" class="btn_login" onclick="loginVal()"></a>
        <a class="tab_fastreg" href="#" onclick="regshow()"></a>
                <input type="hidden" value="login" name="Type"/>
        </form>
    </div>
  	<div class="fastreg" id="gamereg" style="display:none">
		<div class="regwrite">
	    <form method="post" action="zsgcl.aspx" name="reg" id="reg">
		<ul id="usermsg">
			<li><label>账　　号</label><input name="accountreg" type="text" id="accountreg" maxlength="16" tip="请填写常用账号" /><em id="namealert"></em></li>
			<li><label>密　　码</label><input name="pwdonereg" type="password" id="pwdonereg" maxlength="16" tip="密码由6-16位英文字母及数字组成。" /><em id="pwdalert"></em></li>
			<li><label>确认密码</label><input name="pwdtwo" type="password" id="pwdtwo" maxlength="16" tip="密码由6-16位英文字母及数字组成。" /><em id="reppwdalert"></em></li>			
		</ul>
		<p class="submit" ><span id="subform" class="fastreg_submit"><img onclick="regVal()" src="<%=sWebUrl %>/wldFolder/zsgcl/images/btn_reg.jpg"  alt="" title="立即注册" />
		                   <a href="#"><img src="<%=sWebUrl %>/wldFolder/zsgcl/images/btn_fhdl.jpg" onclick="loginshow()" alt="" title="返回登录" /></a></span>
		</p>
		<div id="tip">
			<span id="tipmsg"></span>
			<span class="read"><input name="protocol1" id="protocol" type="checkbox" checked="checked" />我已查看并同意<a href="http://www.dao50.com/regagree.html" target="_blank">《dao50游戏圈用户协议》</a></span>
		</div>
        <input type="hidden" value="reg" name="Type"/>
	</form>
	</div>
		</div>    
  </div>
</div>
</body>
</html>
<%=sMsg %>
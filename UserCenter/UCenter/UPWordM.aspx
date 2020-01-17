<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UPWordM.aspx.cs" Inherits="UserCenter.UCenter.UPWordM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>到武林_修改密码页面</title>
    <link href="<%=sWebUrl %>/wldFolder/css/index_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/UserVal.js"></script>
    <script type="text/javascript">
        function bPWDCheck() {
            var PassWord = $("#bpassword").val();
            var res = PassWordOneVal(PassWord);
            if (res != "") {
                $("#bPWD").html(res);
                return false;
            }
            else {
                $("#bPWD").html("");
                return true;
            }
        }

        function nPWDCheck() {
            var PassWord = $("#passwordone").val();
            var res = PassWordOneVal(PassWord);
            if (res != "") {
                $("#nPWD").html(res);
                return false;
            }
            else {
                $("#nPWD").html("");
                return true;
            }
        }

        function aPWDCheck() {
            var PassWordTwo = $("#passwordtwo").val();
            var PassWordOne = $("#passwordone").val();
            var res = PassWordTwoVal(PassWordOne, PassWordTwo);
            if (res != "") {
                $("#aPWD").html(res);
                return false;
            }
            else {
                $("#aPWD").html("");
                return true;
            }
        }

        function AllCheck() {
            if (bPWDCheck() && nPWDCheck() && aPWDCheck()) {
                document.getElementById("form1").submit();
                return true;
            }
            else {
                return false;
            }
        }
    </script>

</head>
<body>
<script type="text/javascript" src="http://game.dao50.com/inc/sitetop.aspx"></script><!--top结束-->
<div id="logo">
	<a href="#"><img src="<%=sWebUrl %>/wldFolder/images/logo_big.jpg" /></a>
    <a href="#"><img src="<%=sWebUrl %>/wldFolder/images/top_guanggao.jpg" /></a>
	</div>
<div id="wrap"><!--wrap开始-->
    <!--#include file="/inc/PageTopLink.htm"--><!--nav结束-->
    <div id="content"><!--content开始-->
    <div id="mid_left"><!--左侧公共部分开始-->
	<div id="login_content">
    <iframe src="<%=sRootUrl %>/Services/Login.aspx" frameborder="0" width="194px" height="212px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div>
    <div id="mid_left02">
    <iframe src="<%=sWebUrl %>/news/czzxzxl/" frameborder="0" width="195px" height="365px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div>
    </div><!--左侧公共部分结束-->
    <div id="mid_right"><!--mid_right开始-->
   	<div id="content_left"><img src="<%=sWebUrl %>/wldFolder/images/content_bg_left.jpg" /></div>
    <div id="content_mid">
    <div id="content_mid01">
    <ul>
    <li>
    <span class="m01"><a href="#">用户中心</a> > 修改密码</span>
    </li>
    </ul>
    </div>
    <div id="mid_right03"><!--修改密码内容开始-->
    <form action="UPWordM.aspx"  name="form1" id ="form1" method="post">
        <p><span class="g03">原密码：</span>
        <span><input name="bpassword" id="bpassword" maxlength="16" onblur="bPWDCheck()" type="password" class="txt_yonghu" /><font id="bPWD" color="red"></font></span></p>
        <p><span class="g03">新密码：</span>
        <span><input  id="passwordone" onblur="nPWDCheck()" maxlength="16" type="password" class="txt_yonghu"/><font id="nPWD" color="red"></font></span></p>
        <p><span style="margin-left:126px;">确认新密码：</span>
        <span><input  name="passwordtwo" id="passwordtwo" onblur="aPWDCheck()" type="password"class="txt_yonghu"/><font id="aPWD" color="red"></font></span></p>
        <p style="margin-left:220px; padding-top:40px;"><a href="#" onclick="return AllCheck()"><img src="<%=sWebUrl %>/wldFolder/images/xiayibu.jpg" border="0" /></a></p>
    </form>
    </div><!--修改密码内容结束-->
    </div>
    <div id="content_right"><img src="<%=sWebUrl %>/wldFolder/images/content_bg_right.jpg" /></div>
    </div><!--mid_right结束-->
    </div><!--mid结束-->
	<div id="foot_00"><!--foot开始-->
    <iframe src="<%=sWebUrl %>/foot.html" frameborder="0" width="824px" height="150px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div> <!--foot结束-->  
</div><!--wrap结束-->
</body>
</html>
<%=sMsg %>
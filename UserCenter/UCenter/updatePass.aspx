<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updatePass.aspx.cs" Inherits="UserCenter.UCenter.updatePass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>到武林_找回密码_提示问题</title>
    <link href="<%=sWebUrl %>/wldFolder/css/index_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/UserVal.js"></script>
    <script type="text/javascript">
        function bPWDCheck() {
            var PassWord = $("#passwordtwo").val();
            var res = PassWordOneVal(PassWord);
            if (res != "") {
                alert(res);
                return false;
            }
            else {
                return true;
            }
        }

        function nPWDCheck() {
            var PassWord = $("#passwordone").val();
            var res = PassWordOneVal(PassWord);
            if (res != "") {
                alert(res);
                return false;
            }
            else {
                return true;
            }
        }

        function aPWDCheck() {
            var PassWordTwo = $("#passwordtwo").val();
            var PassWordOne = $("#passwordone").val();
            var res = PassWordTwoVal(PassWordOne, PassWordTwo);
            if (res != "") {
                alert(res)
                return false;
            }
            else {
                return true;
            }
        }

        function AllCheck() {
            if (bPWDCheck() && nPWDCheck() && aPWDCheck()) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

</head>
<body>
<script type="text/javascript" src="http://game.dao50.com/inc/sitetop.aspx"></script>
<!--top结束-->
<div id="logo">
    <a href="#"><img src="<%=sWebUrl %>/wldFolder/images/logo_big.jpg" /></a>
    <a href="#"><img src="<%=sWebUrl %>/wldFolder/images/top_guanggao.jpg" /></a>
</div>
<div id="wrap">
    <!--wrap开始-->
    <!--#include file="/inc/PageTopLink.htm"-->
    <!--nav结束-->
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
    <span class="m01"><a href="http://www.dao50.com/yhzx/" target="_blank">用户中心</a> > 密码找回</span>
    </li>
    </ul>
    </div>
    <div id="mid_right03"><!--密码找回成功内容开始-->
    <form id="form1" action="updatePass.aspx" onsubmit="return AllCheck()" method="post">
    <p><span class="g01">新密码：</span>
    <span class="g02"><input type="password" maxlength="16" name="passwordone" id="passwordone" onblur="nPWDCheck()" class="txt_yonghu"/></span></p>
    <p><span class="g01">确认密码：</span>
    <span style="margin-left:35px;">
    <input name="passwordtwo" id="passwordtwo" maxlength="16" onblur="bPWDCheck();" type="password" class="txt_yonghu" /></span></p>    
    <p style="margin-left:200px; padding-top:40px;">
        <input type="submit" style="height:40px;width:152px;background:url(http://image.dao50.com/wldFolder/images/queding.jpg) no-repeat left top;border:0;" value=""/>        
    </p>
    </form>
    </div><!--密码找回成功内容结束-->
    </div>
    <div id="content_right"><img src="<%=sWebUrl %>/wldFolder/images/content_bg_right.jpg" /></div>
    </div><!--mid_right结束-->
    </div><!--mid结束-->
	<div id="foot_00"><!--foot开始-->
    <iframe src="<%=sWebUrl %>/foot.html" frameborder="0" width="824px" height="150px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div> <!--foot结束-->  
</div><!--wrap结束-->
</body>
<%=sMsg %>
</html>

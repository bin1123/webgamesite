<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailBind.aspx.cs" Inherits="UserCenter.UCenter.EmailBind" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>到武林_邮箱绑定</title>
    <link href="<%=sWebUrl %>/wldFolder/css/index_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript">
        function EmailOneCheck() {
            var Email = $("#Email").val();
            if (Email != "") {
                var myreg = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
                if (!myreg.test(Email)) {
                    alert('提示\n请输入有效的E_mail！');
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                alert("提示\n请输入E_mail！");
                return false;
            }
        }

        function EmailTwoCheck() {
            var Email = $("#Email").val();
            var EmailTwo = $("#EmailTwo").val();
            if (Email != EmailTwo) {
                alert("提示\n确认邮箱与绑定邮箱不一致！");
                return false;
            }
            else {
                if (EmailTwo.length < 4) {
                    alert("提示\n请输入合法的邮箱！");
                    return false;
                }
                return true;
            }
        }
        
        function AllCheck() {
            if (EmailOneCheck() && EmailTwoCheck()) {
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
    <script type="text/javascript" src="http://game.dao50.com/inc/sitetop.aspx"></script>
    <!--top结束-->
    <div id="logo">
        <a href="#">
            <img src="<%=sWebUrl %>/wldFolder/images/logo_big.jpg" /></a> <a href="#">
                <img src="<%=sWebUrl %>/wldFolder/images/top_guanggao.jpg" /></a>
    </div>
    <div id="wrap">
        <!--wrap开始-->
        <!--#include file="/inc/PageTopLink.htm"-->
        <!--nav结束-->
        <div id="content">
            <!--content开始-->
            <div id="mid_left">
                <!--左侧公共部分开始-->
                <div id="login_content">
                    <iframe src="<%=sRootUrl %>/Services/Login.aspx" frameborder="0" width="194px" height="212px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
                </div>
    <div id="mid_left02">
    <iframe src="<%=sWebUrl %>/news/czzxzxl/" frameborder="0" width="195px" height="365px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div>
            </div>
            <!--左侧公共部分结束-->
            <div id="mid_right">
                <!--mid_right开始-->
                <div id="content_left">
                    <img src="<%=sWebUrl %>/wldFolder/images/content_bg_left.jpg" /></div>
                <div id="content_mid">
                    <div id="content_mid01">
                        <ul>
                            <li><span class="m01"><a href="http://www.dao50.com/yhzx/" target="_blank">用户中心</a> > 绑定邮箱</span></li>
                        </ul>
                    </div>
                    <div id="mid_right03">
                        <!--绑定邮箱内容开始-->
                        <form id="form1" method="post" action="EmailBind.aspx">
                        <p><span>绑定邮箱：</span> <span>
                                <input type="text" id="Email" onblur="EmailOneCheck()" class="txt_yonghu" /></span> <span><font color="#FF9900">*请正确填写您常用的电子邮件地址，作为密码取回之用</font></span></p>
                        <p><span>确认邮箱：</span> <span>
                                <input type="text" id="EmailTwo" name="EmailTwo" onblur="EmailTwoCheck()" class="txt_yonghu" /></span> <span><font color="#FF9900">*重复输入一次上面的邮箱。</font></span></p>
                        <p style="margin-left: 220px; padding-top: 40px;">
                            <a href="#" onclick="AllCheck()"><img src="<%=sWebUrl %>/wldFolder/images/xiayibu.jpg" /></a></p>
                        </form>
                    </div>
                    <!--绑定邮箱内容结束-->
                </div>
                <div id="content_right">
                    <img src="<%=sWebUrl %>/wldFolder/images/content_bg_right.jpg" /></div>
            </div>
            <!--mid_right结束-->
        </div>
        <!--mid结束-->
        <div id="foot_00">
            <!--foot开始-->
            <iframe src="<%=sWebUrl %>/foot.html" frameborder="0" width="824px" height="150px" marginheight="0"
                marginwidth="0" scrolling="no"></iframe>
        </div>
        <!--foot结束-->
    </div>
    <!--wrap结束-->
</body>
<%=sMsg %>
</html>

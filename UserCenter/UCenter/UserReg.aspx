<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserReg.aspx.cs" Inherits="UserCenter.UCenter.UserReg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>到武林_用户注册</title>
    <link href="<%=sWebUrl %>/wldFolder/css/UserReg.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/UserVal.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/shenfen.js"></script>
    <script type="text/javascript">
        function AccountCheck() {
            var account = $("#account").val();
            var res = AccountVal(account);
            if ("" == res) {
                ValNameIn(account);
                var flag = $("#RFlag").val();
                if (flag == "t") {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                $("#ErrText").html(res);
                $("#RFlag").val("f");
                return false;
            }
        }

        function PWDCheck() {
            var PassWordOne = $("#pwdone").val();
            var res = PassWordOneVal(PassWordOne);
            if (res != "") {
                $("#ErrText").html(res);
                $("#RFlag").val("f");
                return false;
            }
            else {
                $("#ErrText").html("");
                $("#RFlag").val("t");
                return true;
            }
        }

        function PWDTwoCheck() {
            var PassWordTwo = $("#pwdtwo").val();
            var PassWordOne = $("#pwdone").val();
            var res = PassWordTwoVal(PassWordOne, PassWordTwo);
            if (res != "") {
                $("#ErrText").html(res);
                $("#RFlag").val("f");
                return false;
            }
            else {
                $("#ErrText").html("");
                $("#RFlag").val("t");
                return true;
            }
        }

      

       

        function CheckCodeCheck() {
            var valCode = $("#valcode").val();
            var res = ValsCodeVal(valCode);
            if (res != "") {
                $("#ErrText").html(res);
                $("#RFlag").val("f");
                return false;
            }
            else {
                $("#ErrText").html("");
                $("#RFlag").val("t");
                return true;
            }
        }

       

       
        
        function AllCheck() {
            if ("f" == $("#RFlag").val()) {
                return false;
            }
            else {
                if (PWDCheck() && PWDTwoCheck() &&  AccountCheck()) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        function createcheckcode() {
            var rnd = Math.random();
            var url = "<%=sRootUrl %>/services/checkcode.ashx?rnd=" + rnd;
            var img = document.getElementById("checkcodeimg");
            img.src = url;
        }
    </script>

</head>
<body>
    <script type="text/javascript" src="../Inc/SiteTop.aspx"></script>
    <div id="logo">
        <a href="#">
            <img src="<%=sWebUrl %>/wldFolder/images/logo_big.jpg" /></a> <a href="#">
                <img src="<%=sWebUrl %>/wldFolder/images/top_guanggao.jpg" /></a>
    </div>
    <div id="wrap">
        <!--wrap开始-->
        <!--#include file="/inc/PageTopLink.htm"-->
        <div id="content">
            <!--content开始-->
            <!--左侧公共部分结束-->
            <div id="mid_right">
                <!--mid_right开始-->
                
                <div id="content_mid">
                    
                    <form id="Form1" runat="server">
                    <div id="mid_right03">
                        <!--用户注册内容开始-->
                        <p>
                            <span>账 号：</span> <span style="margin-left: 20px;">
                                <input type="text" name="txz" id="account" onblur="AccountCheck()" maxlength="16"
                                    class="txt_yonghu" /></span><span><font color="#FF9900">*使用英文，数字，总字符数在4-16之间。</font></span>
                        </p>
                        <p>
                            <span>密 码：</span> <span style="margin-left: 20px;">
                                <input name="pwd" id="pwdone" onblur="PWDCheck()" type="password" maxlength="16"
                                    class="txt_yonghu" /></span> <span><font color="#FF9900">
                            *由6-16位英文字母及数字组成。 </font>
                            </span>
                        </p>
                        <p>
                            <span>确认密码：</span> <span>
                                <input type="password" maxlength="16" id="pwdtwo" onblur="PWDTwoCheck()" name="pwdtwo" class="txt_yonghu" /></span>
                            <span><font color="#FF9900">*重复输入一次上面的密码。 </font></span>
                        </p>
                        <p id="ErrText" class="zc_hong"></p>
                        <p style="margin-left: 180px; padding-top: 10px;_padding-top:0px;">
                            <input type="checkbox" checked="checked" />我已看过并同意《到武林用户服务协议》
                        </p>
                        <p class="txt_yonghu02">
                            <asp:ImageButton ID="RegButton" OnClick="RegButton_Click" OnClientClick="return AllCheck()" runat="server" /></p>
                    </div>
                        <input type="hidden" id="RFlag" value="t" />
                    </form>
                    <!--用户注册内容结束-->
                </div>
              
            </div>
            <!--mid_right结束-->
        </div>
        <!--mid结束-->
        <div id="foot">
	<div id="logo_foot">
    <img src="<%=sWebUrl %>/wldFolder/images/logo_foot.jpg" />
    </div>
    <div id="logo_foot_right">
    <p class="foot_p1"><a href="#">关于我们</a> | <a href="#">客服中心</a> | <a href="#">商务合作</a> | <a href="#">联系我们</a> | <a href="#">网站地图</a></p>
    <p class="foot_p1">Copyright © 2006-2011 All Rights Reserved dao50.com 京ICP备11034966号</p>
    <p class="foot_p2">京ICP证020009号 网络文化经营许可证 文网文[2006]021号</p>
    <p class="foot_p2">北京迪信通商贸有限公司 北京北天纵横 北京天行远景</p>
    </div>
    <!--<div><script src="http://s14.cnzz.com/stat.php?id=3393167&web_id=3393167" language="JavaScript" ></script></div>-->
</div>
     <%--   <div id="foot_00">
            <!--foot开始-->
            <iframe src="<%=sWebUrl %>/foot.html" frameborder="0" width="824px" height="150px" marginheight="0"
                marginwidth="0" scrolling="no"></iframe>
        </div>--%>
        <!--foot结束-->
    </div>
    <!--wrap结束-->
</body>
<%=sMsg %>
</html>


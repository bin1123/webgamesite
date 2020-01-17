<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reg.aspx.cs" Inherits="UserCenter.UCenter.reg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>到武林_用户注册</title>
    <link href="<%=sWebUrl %>/wldFolder/css/index_css.css" rel="stylesheet" type="text/css" />
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

        function EmailCheck() {
            var res = checkemail('email');
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

        function CredennumCheck() {
            var res = isIdCardNo('credennum');
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

        function realnamecheck() {
            var realname = $("#realname").val();
            if (realname != "") {
                return true;
            }
            else {
                $("#ErrText").html('真实姓名不能为空！');
                $("#RFlag").val("f");
                return false;
            }
        }

        function answercheck() {
            var answer = $("#answer").val();
            if (answer != "") {
                return true;
            }
            else {
                $("#ErrText").html('问题答案不能为空！');
                $("#RFlag").val("f");
                return false;
            }
        }
        
        function AllCheck() {
            if ("f" == $("#RFlag").val()) {
                return false;
            }
            else {
                if (PWDCheck() && PWDTwoCheck() && answercheck() && realnamecheck() && EmailCheck() && CredennumCheck() && CheckCodeCheck() && AccountCheck()) {
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
            <div id="mid_left">
                <!--左侧公共部分开始-->
                <div id="login_content">
                    <iframe src="../Services/Login.aspx" frameborder="0" width="194px" height="212px"
                        marginheight="0" marginwidth="0" scrolling="no"></iframe>
                </div>
                <!--#include file="/inc/PageLeft.htm"-->
            </div>
            <!--左侧公共部分结束-->
            <div id="mid_right">
                <!--mid_right开始-->
                <div id="content_left">
                    <img src="<%=sWebUrl %>/wldFolder/images/content_bg_left.jpg" /></div>
                <div id="content_mid">
                    <div id="content_mid01">
                        <ul>
                            <li><span class="m01"><a href="#">用户中心</a> > <a href="#">用户注册</a></span> </li>
                        </ul>
                    </div>
                    <form runat="server">
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
                                    class="txt_yonghu" /></span> <span><font color="#FF9900">*由6-16位英文字母及数字组成。 </font>
                            </span>
                        </p>
                        <p>
                            <span>确认密码：</span> <span>
                                <input type="password" maxlength="16" id="pwdtwo" onblur="PWDTwoCheck()" name="pwdtwo" class="txt_yonghu" /></span>
                            <span><font color="#FF9900">*重复输入一次上面的密码。 </font></span>
                        </p>
                        <p>
                            <span>真实姓名：</span> <span>
                                <input type="text" id="realname" name="realname" onblur="realnamecheck()" class="txt_yonghu" maxlength="16" /></span>
                            <span><font color="#FF9900">*请输入正确的中文姓名。 </font></span>
                        </p>
                        <p>
                            <span>身份证号：</span> <span>
                                <input type="text" id="credennum" name="credennum" maxlength="18" onblur=" CredennumCheck()" class="txt_yonghu" /></span>
                            <span><font color="#FF9900">*请正确填写您的真实身份证号。 </font></span>
                        </p>
                        <p>
                            <span>电子邮箱：</span> <span>
                                <input type="text" id="email" name="email" onblur="EmailCheck()" class="txt_yonghu" maxlength="30"/></span> <span><font
                                    color="#FF9900">*请正确填写您常用的电子邮件地址</font></span>
                        </p>
                        <p>
                            <span>问&nbsp;&nbsp;&nbsp;&nbsp;题：</span> <span>
                                <select name="question">
                                    <option value="我最爱看什么类型的电影？">我最爱看什么类型的电影？</option>
                                    <option value="我想去哪个国家旅游？">我想去哪个国家旅游？</option>
                                    <option value="我小学是哪个学校的？">我小学是哪个学校的？</option>
                                    <option value="我第一次远行去的哪个城市？">我第一次远行去的哪个城市？</option>
                                    <option value="我爱吃什么食品？">我爱吃什么食品？</option>
                                    <option value="我爱看的一部动画片？">我爱看的一部动画片？</option>
                                    <option value="昨晚我几点睡觉的？">昨晚我几点睡觉的？</option>
                                </select>
                            </span><span style="margin-left: 5px;"><font color="#FF9900">*请正确选择问题，以便密码找回！ </font>
                            </span>
                        </p>
                        <p>
                            <span>答&nbsp;&nbsp;&nbsp;&nbsp;案：</span> <span>
                                <input type="text" id="answer" name="answer" onblur="answercheck()" class="txt_yonghu" maxlength="50"/></span> <span style="margin-left: 5px;"><font
                                    color="#FF9900">*请正确填选答案，以便密码找回！ </font></span>
                        </p>
                        <p>
                            验证码： <span style="margin-left: 12px;">
                                <input type="text" id="valcode" name="valcode" maxlength="6" onblur="CheckCodeCheck()" class="txt_yonghu01" /></span>
                            <span>
                                <img style="width: 60px; height: 20px;" id="checkcodeimg" onclick="createcheckcode()" src="<%=sRootUrl %>/Services/checkcode.ashx" /></span>
                            <span style="margin-left: 20px;"><font color="#FF9900">*验证码看不清？请刷新。验证码为字母，不区分大小写。 </font>
                            </span>
                        </p>
                        <p id="ErrText" class="zc_hong"></p>
                        <p style="margin-left: 180px; padding-top: 10px;_padding-top:0px;">
                            <input type="checkbox" checked="checked" />我已看过并同意<a href="http://www.dao50.com/regagree.html" title="到武林用户服务协议" target="_blank">《到武林用户服务协议》</a>
                        </p>
                        <p class="txt_yonghu02">
                            <asp:ImageButton ID="RegButton" OnClick="RegButton_Click" OnClientClick="return AllCheck()" runat="server" /></p>
                    </div>
                        <input type="hidden" id="RFlag" value="t" />
                    </form>
                    <!--用户注册内容结束-->
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

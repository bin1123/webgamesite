<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="byreg.aspx.cs" Inherits="UserCenter.GCenter.byreg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>游戏注册页面</title>
<link href="http://file.dao50.com/byl/style/css.css" rel="stylesheet" type="text/css" />    
</head>
<!--[if IE 6]>
<script type="text/javascript" src="http://file.dao50.com/byl/js/DD_belatedPNG_0.0.8a-min.js"></script>
<script type="text/javascript">
window.attachEvent("onload", function(){
   DD_belatedPNG.fix("div, a ,em");
});
</script>
<![endif]-->
<body>
<div class="reg_bg ">
    <div class="pd">
       <form id="reg" action="byreg.aspx" name="reg" method="post">
        <table border="0">
          <tr>
            <td class="ti_9 w87">用户名：</td>
            <td class="h38"><div class="inp_bg in_t"><input name="accountreg" type="text" id="accountreg" maxlength="16"/></div></td>
            <td><p class="info_w"></p></td>
          </tr>
          <tr>
            <td class="ti_9 w87">密码：</td>
            <td class="h38" ><div class="inp_bg in_t"><input name="pwdonereg" type="password" id="pwdonereg" maxlength="16"/></div></td>
            <td><p class="info_w"></p></td>
          </tr>
          <tr>
            <td class="ti_9 w87">确认密码：</td>
            <td class="h38"><div class="inp_bg in_t"><input name="pwdtwo" type="password" id="pwdtwo" maxlength="16"/></div></td>
            <td><p class="info_w"></p></td>
          </tr>
        </table>
       </form>
    </div>
    <div class="pr">
         <a href="#" onclick="regVal();return false;" class="ok ti_9">完成注册</a>
         <a href="byLogin.aspx" class="ov_log">已有账号登陆</a>
    </div>
</div>
</body>
</html>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript">
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
</script>
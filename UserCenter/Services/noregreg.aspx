<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="noregreg.aspx.cs" Inherits="UserCenter.Services.noregreg" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>到武林_快速绑定账号</title>    
    <STYLE>
body{BACKGROUND: url(http://file.dao50.com/noreg/bg.jpg) #000 no-repeat center top; width:986px; height:607px; font-size:12px;}
a{ cursor:pointer}
.line {
	PADDING-BOTTOM: 10px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; CLEAR: both; OVERFLOW: hidden; PADDING-TOP: 10px
}
.line_f {
	PADDING-BOTTOM: 8px; PADDING-LEFT: 0px; PADDING-RIGHT: 0px; CLEAR: both; OVERFLOW: hidden; PADDING-TOP: 8px; font-size:16px; font-family:"微软雅黑"; font-weight:bold; color:#c2b05c
}
.line_f a{width:140px;float:left;height:26px;display:block;background-color:#969696;background-image:url(http://file.dao50.com/noreg/dot.jpg);
	background-position: 5px center;background-repeat:no-repeat;line-height:26px;padding-left:10px;font-size:12px;text-decoration:none; font-weight:normal;color:#FFF;
}
.label {
	TEXT-ALIGN: right;	WIDTH: 70px;	FLOAT: left;	COLOR: #FFF;	FONT-SIZE: 16px;
	 font-family:"微软雅黑";
}
.input {
	BORDER-BOTTOM-STYLE: none; PADDING-BOTTOM: 0px; LINE-HEIGHT: 26px; BORDER-RIGHT-STYLE: none; MARGIN: 0px 10px; PADDING-LEFT: 5px; WIDTH: 147px; PADDING-RIGHT: 5px; BORDER-TOP-STYLE: none; BACKGROUND: url(http://file.dao50.com/noreg/input.jpg) repeat-x; FLOAT: left; HEIGHT: 26px; _HEIGHT: 28px; _LINE-HEIGHT: 28px;BORDER-LEFT-STYLE: none; OVERFLOW: hidden; PADDING-TOP: 0px
}
.info {
	COLOR: #FCFCFC
}
.error {
	COLOR: red
}
.ok {
	COLOR: #1be03f
}
.but{
	PADDING-BOTTOM: 10px;
	PADDING-LEFT: 70px;
	PADDING-RIGHT: 0px;
	PADDING-TOP: 10px
}
.bt{background:url(http://file.dao50.com/noreg/jxyx.jpg) no-repeat left top; width:191px; height:42px; border:none}
.form {
	PADDING-BOTTOM: 0px; PADDING-LEFT: 510px; WIDTH: 350px; PADDING-RIGHT: 0px; PADDING-TOP: 248px
}
.gotoindex {
	COLOR: #900;
	TEXT-DECORATION: underline
}
.tab {
	WIDTH: 150px; FLOAT: left; COLOR: #fff100; FONT-SIZE: 14px; CURSOR: pointer; TEXT-DECORATION: underline
}
.iread {
	PADDING-LEFT: 40px;
	WIDTH: 340px
}
.iread INPUT {
	FLOAT: left; VERTICAL-ALIGN: middle
}
.iread SPAN {
	FLOAT: left;
	COLOR: #FFF;
	PADDING-TOP: 2px
}
.iread SPAN A {
	COLOR: #F90
}
.two_link {
	PADDING-LEFT: 110px;
	PADDING-TOP: 0px
}
.gotoindex {
	PADDING-RIGHT: 35px; FLOAT: left; FONT-SIZE: 14px
}
.gotologin {
	WIDTH: 80px;
	FLOAT: left;
	COLOR: #900;
	FONT-SIZE: 14px;
	TEXT-DECORATION: underline
}
.login_block {
	DISPLAY: none
}
.onError {
	COLOR: #ff0000
}
.onSuccess {
	COLOR: #00ff00
}
</STYLE>
</head>
<body>
<div class="form">
        <div class="reg_block">
            <form method="post" action="noregreg.aspx" id="form1" name="form1">
            <div class="line">
                <span class="label">帐号</span>
                <input class="input" id="account" type="text" onblur="AccountCheck()" maxlength="16" name="txz" />
                <span id="Name" class="info">6-16个字符</span>
            </div>
            <div class="line">
                <span class="label">密码</span>
                <input class="input" name="pwd" id="pwdone" onblur="PWDCheck()" type="password" maxlength="16"/>
                <span id="pass" class="info">6-16字符</span>
            </div>
            <div class="line">
                <span class="label">确认密码</span>
                <input maxlength="16" id="pwdtwo" onblur="PWDTwoCheck()" name="pwdtwo" class="input" type="password" />
                <span id="repassword" class="info">再次输入密码</span>
            </div>
            <div class="line iread">
                <input id="ckeck" type="checkbox" checked="checked" />
                <span>我已仔细阅读并同意<a href="http://www.dao50.com/regagree.html" target="_blank">《游戏用户协议》</a></span>
            </div>
            <div class="but"><input type="button" onclick="return AllCheck()" class="bt" value="" /></div><div id="ErrText" style="margin-left:75px;margin-top:15px;color:Red;"><%=sMsg %></div>            
            </form>
        </div>           
</div>
</body>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript">
        function AccountVal(account) {
            var res = "";
            if ("" == account) {
                res = "通行证不能为空！";
                return res;
            }
            if (account.length < 4) {
                res = "通行证长度必须大于4！";
                return res;
            }
            if (!NumAndCodeVal(account)) {
                res = "通行证输入格式不正确！";
                return res;
            }
            return res;
        }

        function NumAndCodeVal(sText) {
            var AccountReg = /^[A-Za-z0-9]+$/;
            if (!AccountReg.test(sText)) {
                return false;
            }
            else {
                return true;
            }
        }
        
        function AccountCheck() {
            var account = $("#account").val();
            var res = AccountVal(account);
            if ("" == res) {
                $("#ErrText").html("");
                return true;
            }
            else {
                $("#ErrText").html(res);
                return false;
            }
        }

        function PassWordOneVal(PassWordOne) {
            var res = "";
            if ("" == PassWordOne) {
                res = "密码不能为空！";
                return res;
            }
            if (PassWordOne.length < 6) {
                res = "密码长度必须大于6！";
                return res;
            }
            if (!NumAndCodeVal(PassWordOne)) {
                res = "密码输入格式不正确！";
                return res;
            }
            return res;
        }

        function PassWordTwoVal(PassWordOne, PassWordTwo) {
            var res = "";
            if ("" == PassWordTwo) {
                res = "密码确认不能为空！";
                return res;
            }
            if (PassWordTwo != PassWordOne) {
                res = "二次输入密码不一致！";
                return res;
            }
            return res;
        }
        
        function PWDCheck() {
            var PassWordOne = $("#pwdone").val();
            var res = PassWordOneVal(PassWordOne);
            if (res != "") {
                $("#ErrText").html(res);
                return false;
            }
            else {
                $("#ErrText").html();
                return true;
            }
        }

        function PWDTwoCheck() {
            var PassWordTwo = $("#pwdtwo").val();
            var PassWordOne = $("#pwdone").val();
            var res = PassWordTwoVal(PassWordOne, PassWordTwo);
            if (res != "") {
                $("#ErrText").html(res);
                return false;
            }
            else {
                $("#ErrText").html();
                return true;
            }
        }

        function AllCheck() {
            var account = $("#account").val();
            $.ajax({
                type: "POST",
                url: "/Services/Ajax.ashx",
                data: "AjaxType=ValName&Account=" + account,
                beforeSend: function() {
                },
                success: function(data) {
                    if ("0" != data) {
                        $("#ErrText").html("X 账号已存在！请重新输入！");
                        $("#account").focus().select();
                    }
                    else {
                        $("#ErrText").html("");
                        if (PWDCheck() && PWDTwoCheck()) {
                            document.getElementById("form1").submit();
                        }
                    }
                }
            });
        }
    </script>
</html>
<div style="display:none"><script src="http://s14.cnzz.com/stat.php?id=5365112&web_id=5365112" language="JavaScript"></script></div>
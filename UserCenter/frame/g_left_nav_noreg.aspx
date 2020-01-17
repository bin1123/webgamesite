<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_noreg.aspx.cs" Inherits="UserCenter.frame.g_left_nav_noreg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style>
        *{ margin:0; padding:0; font-size:12px;}
        .bd_before{ width:140px; height:800px; background:url(http://file.dao50.com/noreglogintwo/bg_0.gif) no-repeat left top}
        .bd_before_t{ height:160px; width:140px;padding-top:99px; _padding-top:95px; }
        *+html .bd_before_t{ padding-top:95px;}
        .txt{ width:130px; height:20px; line-height:20px; background:transparent; border:none; margin-left:6px; margin-bottom:24px;}
        .bd_before_t label a{ width:116px; height:30px; display:block; cursor:pointer; margin-left:16px;}
        .bd_before_b{ height:540px; width:140px;}
        .bd_before_b a{ width:140px; height:540px; cursor:pointer; display:block}
        .bd_after{ width:140px; height:800px; background:url(http://file.dao50.com/noreglogintwo/bg_1.gif) no-repeat left top}
        .bd_after_t{ height:160px; width:140px;padding-top:106px;}
        .bd_after_t label{ width:115px; height:20px; margin-left:20px; line-height:20px; float:left; font-family:"微软雅黑"; color:#FFF}
        .bd_after_t  a{ width:116px; height:25px;  display:block; cursor:pointer; margin-left:16px; _margin-left:8px; margin-top:50px; float:left; }
        .bd_after_b{ height:530px; width:140px;}
        .bd_after_b a{ width:140px; height:530px; cursor:pointer; display:block}
    </style>
</head>
<body>    
	<div id="userregbind" class="bd_before">
    	<div class="bd_before_t">
    	<label><input type="text" id="account" name="txz" type="text" maxlength="16" class="txt" /></label>
        <label><input name="pwd" id="pwdone" type="password" maxlength="16" class="txt" /></label>
        <label><input type="password" maxlength="16" id="pwdtwo" name="pwdtwo" class="txt" /></label>
        <label><a href="#" onclick="AllCheck();return false;"></a></label>
        </div>
        <div class="bd_before_b"><a href="#" onclick="accountfocus();"></a></div>
    </div>        
    <div id="userbinded" class="bd_after" style="display:none">
    	<div class="bd_after_t">
    	<label id="username"></label>
        <label style="margin-top:30px;"><%=sServerName %></label>
        <a href="#" onclick="getReward();return false;"></a>
        </div>
        <div class="bd_after_b"><a href="#" onclick="accountfocus();"></a></div>
    </div>
</body>
</html>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript">
    $(document).ready(function() {
        var account = '<%=sAccountT %>';
        if (account.length > 4) {
            if (account.indexOf('?') == 0) {
                $("#userbinded").hide();
                $("#userregbind").show();
            }
            else {
                $("#userregbind").hide();
                $("#userbinded").show();
                $("#username").html(account);
            }
        }
        else {
            location.href = 'http://www.dao50.com/';
        }
    });

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
            alert(res);
            return false;
        }
        else {
            return true;
        }
    }

    function PWDTwoCheck() {
        var PassWordTwo = $("#pwdtwo").val();
        var PassWordOne = $("#pwdone").val();
        var res = PassWordTwoVal(PassWordOne, PassWordTwo);
        if (res != "") {
            alert(res);
            return false;
        }
        else {
            return true;
        }
    }

    function AllCheck() {
        var account = $("#account").val();
        var res = AccountVal(account);
        if (res != "") {
            alert(res);
            $("#account").focus().select();
            return false;
        }
        else {
            $.ajax({
                type: "POST",
                url: "/Services/Ajax.ashx",
                data: "AjaxType=ValName&Account=" + account,
                beforeSend: function() {
                    $("#regbindbutton").attr('disabled', "true");
                },
                success: function(data) {
                    if ("0" != data) {
                        alert("账号已存在！请重新输入！");
                        $("#account").focus().select();
                        $("#regbindbutton").removeAttr("disabled");
                    }
                    else {
                        if (PWDCheck() && PWDTwoCheck()) {
                            var PassWordTwo = $("#pwdtwo").val();
                            $.ajax({
                                type: "POST",
                                url: "/Services/Ajax.aspx",
                                data: "AjaxType=NoRegBind&account=" + account + "&pw=" + PassWordTwo,
                                beforeSend: function() {
                                },
                                success: function(data) {
                                    $("#regbindbutton").removeAttr("disabled");
                                    if ("" == data) {
                                        alert("账号绑定成功！请继续游戏到20级有美女相送哦！");
                                        $("#userregbind").hide();
                                        $("#userbinded").show();
                                        $("#username").html(account);
                                    }
                                    else {
                                        alert(data);
                                    }
                                }
                            });
                        }
                    }
                }
            });
        }
    }

    function accountfocus() {
        var username = $("#username").text();
        if (username.length > 3) {
            getReward();
        }
        else {
            alert("请您先注册帐号并绑定角色后再领取！");
            $("#account").focus();
        }
    }

    function getReward() {
        var game = '<%=sGameName %>';
        $.ajax({
            type: "POST",
            url: "/Services/Ajax.aspx",
            data: "AjaxType=getReward&gn=" + game,
            beforeSend: function() {
            },
            success: function(data) {
                if ("0" == data) {
                    alert("您已经成功领取美女，速度在游戏里领取首充礼包吧！");
                    location.href('/GCenter/wan.aspx?gn=' + game);
                }
                else if ("2" == data) {
                    alert('角色等级不够20级，请加油！或者请等会再点击领取，谢谢');
                }
                else if ("1" == data) {
                    alert("您已经成功领取过美女，请勿重复领取！");
                    location.href('http://www.dao50.com/');
                }
                else if ("-1|3" == data) {
                    alert("您好，您所在的IP今天已经领取了三次美女了，下次再试哦！");
                }
                else if ("7" == data)
                {
                    alert("领取中，请勿重复领取！");
                }
                else {
                    alert(data);
                }
            }
        });
    }
</script>
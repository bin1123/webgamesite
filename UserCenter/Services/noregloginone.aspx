<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="noregloginone.aspx.cs" Inherits="UserCenter.Services.noregloginone" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>点击领取获绝世妖娆美女！！！</title>
</head>
<style>
*{ margin:0; padding:0; font-size:12x;}
.dbt_bdjs{ width:780px; height:140px; margin:0 auto}
.dbt_bd{ width:290px; height:132px; margin-top:6px; background:url(http://file.dao50.com/noregloginone/bg.jpg) no-repeat left top; float:left}
.dbt_bdafter{ width:290px; height:132px; margin-top:6px; background:url(http://file.dao50.com/noregloginone/bg0.jpg) no-repeat left top; float:left}
.dbt_bdafter p{ line-height:20px; margin-top:6px; font-size:14px; font-family:"微软雅黑"; color:#d0d123; width:86px; margin-left:180px;}
.txt{ width:160px; height:21px; line-height:21px; background:transparent; border:none; margin-top:9px; _margin-top:7px; margin-left:82px;}
*+html .txt{margin-top:7px;}
.btn{ width:116px; height:30px; background:url(http://file.dao50.com/noregloginone/bdjs.gif) no-repeat left top; margin-top:5px; margin-left:95px; border:none; cursor:pointer}
.btn0{ width:114px; height:26px; background:url(http://file.dao50.com/noregloginone/lqmn.jpg) no-repeat left top; margin-left:95px; border:none; cursor:pointer}
.dbt_gg{ width:465px; height:140px; float:left; background:url(http://file.dao50.com/noregloginone/img01.gif) no-repeat left top; cursor:pointer; }   
</style>
<body style="background-color:Black">
<div style="background-color:White" class="dbt_bdjs">
	<div id="userregbind" class="dbt_bd">
    	    <label><input type="text" id="account" name="txz" type="text" maxlength="16" class="txt" /></label>
            <label><input name="pwd" id="pwdone" type="password" maxlength="16" class="txt" /></label>
            <label><input type="password" maxlength="16" id="pwdtwo" name="pwdtwo" class="txt" /></label>
            <label><input type="button" id="regbindbutton" class="btn" onclick="AllCheck()"/></label>
            <input type="hidden" name="gamename" value="<%=sGameName %>"/>
    </div>
    <div id="userbinded" class="dbt_bdafter" style="display:none">
    	<p id="username" style="height:20px;"></p>
        <p style="height:46px;"><%=sServerName %></p>
        <label><input type="button" class="btn0" onclick="getReward()"/></label>
    </div>
    <div class="dbt_gg" onclick="accountfocus();">
    </div>
</div>
<div style="width:100%;height:700px;">
<iframe style="width:100%;height:700px;" src="/GCenter/ToGame.aspx?gn=<%=sGameName %>"></iframe>
</div>
</body>
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
                                        //$("#loginurl").attr("src", "NoRegAccountC.aspx?un="+account);
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
                else{
                    alert(data);
                }
            }
        }); 
    }
</script>
<%--<iframe style="display:none" id="loginurl"></iframe>--%>
</html>
<%=sMsg %>
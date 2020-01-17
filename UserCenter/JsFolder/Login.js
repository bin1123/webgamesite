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

//验证是否为纯数字和字符
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

function checkAll() {
    var account = $("#account").val();
    var res = AccountVal(account);
    if (res.length > 0) {
        alert(res);
        return false;
    }
    else {
        var password = $("#pwdone").val();
        res = PassWordOneVal(password);
        if (res.length > 0) {
            alert(res);
            return false
        }
        else {
            document.getElementById("form1").submit();
            return true;
        }
    }
}

function LoginVal(name) {
    if (name != null && name.length > 3) {
        return true;
    }
    else {
        $("#loginurl").val(window.location.href);
        return false;
    }
}

function Logined(name, point) {
    $("#welcome").html(name);
    $("#point").html(point);
    $("#nologin").hide();
    $("#logined").show();
    LoginServerSel();
}

function LoginServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.aspx",
        data: "AjaxType=LoginSeverSelAll",
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                var num = 0; var loginone = "";
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    var serverid;
                    $.each(dataObj.root, function(idx, item) {
                        serverid = item.serverid;
                        loginone = "<li><a href='/GCenter/PlayGame.aspx?gn=" + item.abbre + "' target='_blank'><span class='yx_contentname'><font color='#FF0000'>" + item.gamename + "</font></span><span class='yx_contentfwq'>" + item.servername + "</span><span class='yx_contentnum'>" + serverid + "服</span></a></li>";
                        $("#logininfo").append(loginone);
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}
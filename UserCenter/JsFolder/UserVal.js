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

//验证本站用户名是否存在
function CheckNameIn(account) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ValName&Account=" + account,
        beforeSend: function() {
        },
        success: function(data) {
            if ("0" != data) {
                alert("用户名已经存在!");
                $("#RFlag").val("f");
            }
            else {
                $("#RFlag").val("t");
            }
        }
    });
}

//验证本站用户名是否存在
function ValNameIn(account) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ValName&Account=" + account,
        beforeSend: function() {
        },
        success: function(data) {
            if ("0" != data) {
                $("#ErrText").html("X 账号已存在！");
                $("#RFlag").val("f");
            }
            else {
                $("#RFlag").val("t");
            }
        }
    });
}

//验证本站用户名是否存在
function ValPNameIn(account) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.aspx",
        data: "AjaxType=ValName&Account=" + account,
        beforeSend: function() {
        },
        success: function(data) {
            if ("0" == data) {
                $("#accres").html("X 通行证不存在！");
                $("#accres").css("color", "#00BB4F");
                $("#RFlag").val("f");
            }
            else {
                $("#accres").html("√ 此账号可以充值");
                $("#accres").css("color", "red"); $("#RFlag").val("t");
            }
        }
    });
}

function ValCheckCode(checkcode) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.aspx",
        data: "AjaxType=ValCode&CheckCode=" + checkcode,
        beforeSend: function() {
        },
        success: function(data) {
        if ("0" == data) {                
                $("#RFlag").val("t");
            }
            else {
                alert(data);
                $("#RFlag").val("f");
            }
        }
    });
}

function UserLogin(account,password,usertype) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.aspx",
        data: "AjaxType=UserVal&Account=" + account + "&PassWord=" + password + "&utype=" + usertype,
        beforeSend: function() {
        },
        success: function(data) {
            var rarray = data.split("|");
            if ("0" == rarray[0]) {
                //用户验证成功
                $("#top_left1").hide();
                $("#top_left2").show();
            }
            else {
                //用户验证失败
                alert('用户信息验证失败！');
                $("#top_left1").show();
                $("#top_left2").hide();
            }
        }
    });
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
function ValsCodeVal(valCode) {
    var res = "";
    if("" == valCode)
    {
        res = "验证码不能为空！";
        return res;
    }
    if(valCode.length < 5) {
        res = "验证码输入不正确！";
        return res;
    }
    return res;
}

function keydown(evt) {
    evt = (evt) ? evt : ((window.event) ? window.event : "");  //兼容IE和Firefox获得keyBoardEvent对象
    var key = evt.keyCode ? evt.keyCode : evt.which; //兼容IE和Firefox获得keyBoardEvent对象的键值
    if (key == 13) { //判断是否是回车事件。
        checkAll();
    }
}
function AccountCheck() {
    var accountone = $("#paccountone").val();
    var accounttwo = $("#paccounttwo").val();
    if (accountone != "") {
        if (accountone == accounttwo) {
            $("#accounterr").html("");
            return true;
        }
        else {
            $("#accounterr").html("账号与确认账号不一致！");
            $("#paccounttwo").focus().select();
            return false;
        }
    }
    else {
        $("#accounterr").html('账号不能为空！');
        $("#paccountone").focus().select();
        return false;
    }
}

function GameAccountCheck() {
    var gaccount = $("#gameaccount").val();
    if (gaccount != "") {
        $("#gaccounterr").html('');
        return true;
    }
    else {
        $("#gaccounterr").html('账号不能为空！');
        $("#gameaccount").focus().select();
        return false;
    }
}     

function payshow(pname) {
    var ptgi = document.getElementById('paytogameinfo');
    var paytowhere = document.getElementById("paytowhere");
    if (pname == "game") {
        ptgi.style.display = 'block';

    }
    else {
        ptgi.style.display = 'none';
    }
    paytowhere.value = GetRadioValue("payto");
    SetGamePoints();
}

function payshowp(pname) {
    var ptgi = document.getElementById('paytogameinfo');
    var paytowhere = document.getElementById("paytowhere");
    if (pname == "game") {
        ptgi.style.display = 'block';

    }
    else {
        ptgi.style.display = 'none';
    }
    paytowhere.value = GetRadioValue("payto");
    SetGamePointsP();
}

function PayChange(payname) {
    var ptype = document.getElementById('paytype');
    var payinfo = document.getElementById('pinfo');
    var channel = document.getElementById('channel');

    var alipay = document.getElementById('alipay');
    var ibank = document.getElementById('ibank');
    var ypszx = document.getElementById('yp-szx');
    var ypdx = document.getElementById('yp-dx');
    var yplt = document.getElementById('yp-lt');
    var ypzt = document.getElementById('yp-zt');
    var ypsd = document.getElementById('yp-sd');
    var ypjcard = document.getElementById('yp-jcard');
    var ypbank = document.getElementById('yp-bank');
    var gnyhhk = document.getElementById('gnyhhk');
    var vpay = document.getElementById('vpay');
    var szfbank = document.getElementById('szfbank');
    var szfphone = document.getElementById('szfphone');
    var tenpay = document.getElementById('tenpay');
    
    var bank1 = document.getElementById('bank1');
    var bank2 = document.getElementById('bank2');
    var gnyhinfo = document.getElementById('gnyhinfo');
    var chongzhi03_right = document.getElementById('chongzhi03_right');
    var szfcardtype = document.getElementById('szfcardtype');

    channel.value = payname;
    chongzhi03_right.style.display = "block";
    switch (payname) {
        case "alipay":
            ptype.innerHTML = "“支付宝”";
            payinfo.innerHTML = "阿里巴巴旗下专业支付平台。凡是拥有支付宝帐号的用户，都可以进行网上直充，方便快捷，同时支持网银。";
            alipay.className = "cz_bg2";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            bank1.style.display = "none";
            bank2.style.display = "none";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "ibank":
            ptype.innerHTML = "“网银”";
            payinfo.innerHTML = "只要您开通网上银行服务，足不出户即可实现快捷准确的帐号充值。请勿在此通道中使用其他方式充值。";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg2";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            bank1.style.display = "block";
            bank2.style.display = "block";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "yp-szx":
            ptype.innerHTML = "“神州行”";
            payinfo.innerHTML = "支持全国通用的中国移动充值付费卡（200元面值除外），部分地方卡。";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg2";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            bank1.style.display = "none";
            bank2.style.display = "none";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "yp-dx":
            ptype.innerHTML = "“电信卡”";
            payinfo.innerHTML = "支持全国通用的中国电信充值付费卡。支持北京、辽宁、江西、河北、四川、浙江、江苏、福建、陕西、重庆、湖南、上海、山东、广东、贵州、河南、安徽、黑龙江、山西、云南、吉林、内蒙古、西藏、青海、新疆、甘肃全省通用的中国电信充值付费卡";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg2";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            bank1.style.display = "none";
            bank2.style.display = "none";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "yp-lt":
            ptype.innerHTML = "“联通卡”";
            payinfo.innerHTML = "仅支持全国卡，卡号15位，密码19位，不支持地方充值卡。";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg2";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            bank1.style.display = "none";
            bank2.style.display = "none";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "yp-zt":
            ptype.innerHTML = "“征途卡”";
            payinfo.innerHTML = "凡是拥有征途卡的玩家可选此种方式进行充值，安全、有效、便捷。";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg2";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            bank1.style.display = "none";
            bank2.style.display = "none";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "yp-sd":
            ptype.innerHTML = "“盛大卡”";
            payinfo.innerHTML = "请使用卡号以CSC5、CS、S、CA、CSB、YC、YD开头的“盛大互动娱乐卡”进行支付。";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg2";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            bank1.style.display = "none";
            bank2.style.display = "none";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "yp-jcard":
            ptype.innerHTML = "“骏网一卡通”";
            payinfo.innerHTML = "支持骏网一卡通实物卡、虚拟卡充值，只需要在线填写通行证账号，充值后立即到账，更安全更直接。";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.clasaName = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg2";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            bank1.style.display = "none";
            bank2.style.display = "none";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "yp-bank":
            ptype.innerHTML = "“易宝网上银行”";
            payinfo.innerHTML = "只要您开通网上银行服务，足不出户即可实现快捷准确的帐号充值。请勿在此通道中使用其他方式充值。";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg2";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            bank1.style.display = "none";
            bank2.style.display = "none";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "gnyhhk":
            ptype.innerHTML = "“易宝网上银行”";
            payinfo.innerHTML = "只要您开通网上银行服务，足不出户即可实现快捷准确的帐号充值。请勿在此通道中使用其他方式充值。";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg2";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            chongzhi03_right.style.display = "none";
            gnyhinfo.style.display = "block";
            szfcardtype.style.display = "none";
            break;
        case "vpay":
            ptype.innerHTML = "“国内手机电话支付”";
            payinfo.innerHTML = "只要您拿起身边的固定电话或小灵通可购买声讯卡，支持26个省市，先购买再充值。";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg2";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            bank1.style.display = "none";
            bank2.style.display = "none";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "tenpay":
            ptype.innerHTML = "“财付通支付”";
            payinfo.innerHTML = "腾讯旗下专业支付平台。凡是拥有财付通帐号的用户，都可以进行网上直充，方便快捷，同时支持网银。";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg2";
            bank1.style.display = "none";
            bank2.style.display = "none";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "szfbank":
            ptype.innerHTML = "“银行卡充值 （无需网上银行）”";
            payinfo.innerHTML = "没有开通网上银行也可以充值,支持中国工商银行、中国农业银行、招商银行、兴业银行等（单笔不超过2000人民币，全天不超过5000人民币）";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg2";
            szfphone.className = "cz_bg1";
            tenpay.className = "cz_bg1";
            bank1.style.display = "none";
            bank2.style.display = "none";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "none";
            break;
        case "szfphone":
            ptype.innerHTML = "“手机充值卡充值”";
            payinfo.innerHTML = "可以利用手机充值卡给帐户充值。全国各地用户只须在当地售卡点购买 全国通用的神州行充值卡、联通充值卡、电信充值卡就可以进行充值。";
            alipay.className = "cz_bg1";
            ibank.className = "cz_bg1";
            ypszx.className = "cz_bg1";
            ypdx.className = "cz_bg1";
            yplt.className = "cz_bg1";
            ypzt.className = "cz_bg1";
            ypsd.className = "cz_bg1";
            ypjcard.className = "cz_bg1";
            ypbank.className = "cz_bg1";
            gnyhhk.className = "cz_bg1";
            vpay.className = "cz_bg1";
            szfbank.className = "cz_bg1";
            szfphone.className = "cz_bg2";
            tenpay.className = "cz_bg1";
            bank1.style.display = "none";
            bank2.style.display = "none";
            gnyhinfo.style.display = "none";
            szfcardtype.style.display = "block";
            break;
    }
    var IsGift = $("#isgift").val();
    if (IsGift == "on") {
        setGiftPayNums('payprice', payname);
    }
    else {
        setPayNums('payprice', payname);
    }
    SetGamePoints();
}

function show(name) {
    var wlbinfo = document.getElementById("payinfos");
    var gameinfo = document.getElementById("gpayinfos");
    var paytowhere = document.getElementById("paytowhere");
    switch (name) {
        case "pay":
            wlbinfo.style.display = "block";
            gameinfo.style.display = "none";
            paytowhere.value = GetRadioValue("payto");
            break;
        case "GamePay":
            var islogin = document.getElementById('islogin').value;
            if (islogin == "y") {
                wlbinfo.style.display = "none";
                gameinfo.style.display = "block";
                paytowhere.value = "paytogame";
            }
            else {
                location.href = "../Default.aspx?url=" + location.href;
            }
            break;
    }
}

function showp(name) {
    var wlbinfo = document.getElementById("payinfos");
    var gameinfo = document.getElementById("gpayinfos");
    var paytowhere = document.getElementById("paytowhere");
    switch (name) {
        case "pay":
            wlbinfo.style.display = "block";
            gameinfo.style.display = "none";
            paytowhere.value = GetRadioValue("payto");
            break;
        case "GamePay":
            var islogin = document.getElementById('islogin').value;
            if (islogin == "") {
                wlbinfo.style.display = "none";
                gameinfo.style.display = "block";
                paytowhere.value = "paytogame";
            }
            else {
                location.href = "../Pay/pPayErr.aspx?err=" + islogin;
            }
            break;
    }
}

function _displaySelect() {
    var selects = document.getElementsByTagName("select"); //整个页面的所有下拉框
    var objWindow = document.getElementById("WindowDIV");
    var DIVselects = objWindow.getElementsByTagName("select"); //整个弹出层的所有下拉框
    for (var i = 0; i < selects.length; i++) {
        if (selects[i].style.visibility) {
            selects[i].style.visibility = "";
        } else {
            selects[i].style.visibility = "hidden";
            for (var j = 0; i < DIVselects.length; j++) {
                DIVselects[j].style.visibility = "";
            }
        }
    }
}

function openWindow(width, height) {
    var paytowhere = document.getElementById("paytowhere").value;
    switch (paytowhere) {
        case "qpaytogame":
            if (AccountCheck() && QuickPayVal()) {
                openWindows(width, height, "quick");
                window.setTimeout(function() {
                    document.getElementById("tanchuang01").focus();
                }, 0);
            }
            break;
        case "paytopt":
            if (AccountCheck()) {
                openWindows(width, height, "wlb");
                window.setTimeout(function() {
                    document.getElementById("tanchuang01").focus();
                }, 0);
            }
            break;
        case "paytogame":
            if (GameAccountCheck() && GamePayVal()) {
                openWindows(width, height, "game");
                window.setTimeout(function() {
                    document.getElementById("tanchuang01").focus();
                }, 0);
            }
            break;
    }
} 

function openWindowp(width, height) {
    var paytowhere = document.getElementById("paytowhere").value;
    switch (paytowhere) {
        case "qpaytogame":
            if (AccountCheck() && QuickPayVal()) {
                openWindowsp(width, height, "quick");
                window.setTimeout(function() {
                    document.getElementById("tanchuang01").focus();
                }, 0);
            }
            break;
        case "paytopt":
            if (AccountCheck()) {
                openWindowsp(width, height, "wlb");
                window.setTimeout(function() {
                    document.getElementById("tanchuang01").focus();
                }, 0);
            }
            break;
        case "paytogame":
            if (GameAccountCheck() && GamePayVal()) {
                openWindowsp(width, height, "game");
                window.setTimeout(function() {
                    document.getElementById("tanchuang01").focus();
                }, 0);
            }
            break;
    }
}

function openWindows(width, height, type) {
    var objWindow = document.getElementById("WindowDIV");
    var browserinfo = getBrowser();
    var browsers = browserinfo.split("|");
    var browser = browsers[0];
    var browserversion = browsers[1];
    if (browser == "ie") {
        if (browserversion == "9.0") {
            objWindow.className = "WindowDIVFF";
        }
        else if (browserversion == "6.0") {
            document.getElementById("chongzhi03_right04").style.display = "none";
        }
    }
    else {
        objWindow.className = "WindowDIVFF";
    }
    var objLock = document.getElementById("LockWindows"); //这个是用于在IE下屏蔽内容用
    objLock.style.display = "block";
    objLock.style.width = document.body.clientWidth + "px";
    objLock.style.height = document.body.clientHeight + "px";
    objLock.style.minWidth = document.body.clientWidth + "px";
    objLock.style.minHeight = document.body.clientHeight + "px";
    // 判断输入的宽度和高度是否大于当前浏览器的宽度和高度
    if (width > document.body.clientWidth)
        width = document.body.clientWidth;
    if (height > document.body.clientHeight)
        height = document.body.clientHeight;
    objWindow.style.display = "block";
    objWindow.style.width = width + "px";
    objWindow.style.height = height + "px";
    // 将弹出层居中  
    objWindow.style.left = (document.body.offsetWidth - width) / 2 + " px";
    objWindow.style.top = (document.body.offsetHeight - height) / 2 + " px";
    _displaySelect();
    tcshow(type);
}

function openWindowsp(width, height, type) {
    var objWindow = document.getElementById("WindowDIV");
    var browserinfo = getBrowser();
    var browsers = browserinfo.split("|");
    var browser = browsers[0];
    var browserversion = browsers[1];
    if (browser == "ie") {
            if (browserversion == "6.0") {
            document.getElementById("chongzhi03_right04").style.display = "none";
        }
    }
    var objLock = document.getElementById("WindowDIV"); //这个是用于在IE下屏蔽内容用
    objLock.style.display = "block";
    _displaySelect();
    tcshow(type);
}

function tcshow(type) {
    var wlb = document.getElementById('WlbNote');
    var game = document.getElementById('GameNote');
    var Quick = document.getElementById('QuickNote');

    var payname = document.getElementById('channel').value;
    switch (type) {
        case "wlb":
            var waccount = document.getElementById('wlbaccount');
            var wlbnums = document.getElementById('wlbnums');
            var money = document.getElementById('wlbmoney');
            var wlbprice = document.getElementById('payprice').value;
            waccount.innerHTML = document.getElementById('paccounttwo').value;
            wlbnums.innerHTML = wlbprice * 10 * getpFeeScale(payname);
            money.innerHTML = wlbprice;
            wlb.style.display = "block";
            game.style.display = "none";
            Quick.style.display = "none";
            document.getElementById('formbank_account').value = document.getElementById('paccounttwo').value;
            document.getElementById('formbank_phone').value = document.getElementById('pphonenum').value;
            document.getElementById('formbank_payprice').value = wlbprice;
            document.getElementById('formbank_channel').value = document.getElementById('channel').value;
            document.getElementById('formbank_name').value = GetRadioValue('bank');
            document.getElementById('formbank_cardType').value = GetRadioValue('cardtype');
            break;
        case "game":
            var gAccount = document.getElementById('gaccount');
            var wlbnums = document.getElementById('gamewlbs');
            var gname = document.getElementById('gname');
            var sname = document.getElementById('sname');
            var gpoints = document.getElementById('gpoint');
            gAccount.innerHTML = document.getElementById('gameaccount').value;
            var wlbspay = document.getElementById('gpaynums').value;
            var gamename = document.getElementById('ggamename');
            wlbnums.innerHTML = wlbspay;
            gname.innerHTML = gamename.options[gamename.selectedIndex].text;
            var gservername = document.getElementById('gservername');
            sname.innerHTML = gservername.options[gservername.selectedIndex].text;
            var rate = 1;
            gpoints.innerHTML = wlbspay * rate;
            wlb.style.display = "none";
            game.style.display = "block";
            Quick.style.display = "none";
            document.getElementById('formgame_account').value = document.getElementById('gameaccount').value;
            document.getElementById('formgame_phone').value = document.getElementById('gamephone').value;
            document.getElementById('formgame_paynums').value = document.getElementById('gpaynums').value;
            document.getElementById('formgame_servername').value = gservername.options[gservername.selectedIndex].value;
            document.getElementById('formgame_role').value = document.getElementById('role').value;
            break;
        case "quick":
            var qAccount = document.getElementById('quickaccount');
            var qgame = document.getElementById('quickgame');
            var qserver = document.getElementById('quickserver');
            var qgamepoints = document.getElementById('quickgamepoints');
            var qmoney = document.getElementById('quickmoney');
            qAccount.innerHTML = document.getElementById('paccounttwo').value;
            var games = document.getElementById('gamename');
            qgame.innerHTML = games.options[games.selectedIndex].text;
            var gamevalue = 'q' + games.value;
            var qservername = document.getElementById('servername');
            qserver.innerHTML = qservername.options[qservername.selectedIndex].text;
            var qprice = document.getElementById('payprice');
            qgamepoints.innerHTML = qprice.value * 10 * GameRate(games.value) * getpFeeScale(payname);
            qmoney.innerHTML = qprice.value;
            wlb.style.display = "none";
            game.style.display = "none";
            Quick.style.display = "block";
            document.getElementById('formquick_account').value = document.getElementById('paccounttwo').value;
            document.getElementById('formquick_phone').value = document.getElementById('pphonenum').value;
            document.getElementById('formquick_payprice').value = document.getElementById('payprice').value;
            document.getElementById('formquick_channel').value = document.getElementById('channel').value;
            document.getElementById('formquick_servername').value = qservername.options[qservername.selectedIndex].value;
            document.getElementById('formquick_bank').value = GetRadioValue('bank');
            document.getElementById('formquick_cardType').value = GetRadioValue('cardtype');
            document.getElementById('formquick_role').value = document.getElementById('quickrole').value;
            break;
    }
}

function hiddenWindows() {
    var LockWindows = document.getElementById("LockWindows");
    var WindowDIV = document.getElementById("WindowDIV");
    LockWindows.style.display = 'none';
    WindowDIV.style.display = 'none';
    var browserinfo = getBrowser();
    var browsers = browserinfo.split("|");
    var browser = browsers[0];
    var browserversion = browsers[1];
    if (browser == "ie") {
        if (browserversion == "6.0") {
            document.getElementById("chongzhi03_right04").style.display = "block";
        }
    }
    _displaySelect();
}

function hiddenWindowsp() {
    var WindowDIV = document.getElementById("WindowDIV");
    WindowDIV.style.display = 'none';
    var browserinfo = getBrowser();
    var browsers = browserinfo.split("|");
    var browser = browsers[0];
    var browserversion = browsers[1];
    if (browser == "ie") {
        if (browserversion == "6.0") {
            document.getElementById("chongzhi03_right04").style.display = "block";
        }
    }
    _displaySelect();
}

function GetRadioValue(RadioName) {
    var obj;
    obj = document.getElementsByName(RadioName);
    if (obj != null) {
        var i;
        for (i = 0; i < obj.length; i++) {
            if (obj[i].checked) {
                return obj[i].value;
            }
        }
    }
    return null;
}

function SetGamePoints() {
    var paytowhere = document.getElementById('paytowhere');
    var gamepoints = document.getElementById('gamepoints');
    var payprice = document.getElementById("payprice");
    var payname = document.getElementById('channel').value;
    if (paytowhere.value == "qpaytogame") {
        var gamename = document.getElementById('gamename');
        gamepoints.innerHTML = payprice.value * GameRate(gamename.value) * getpFeeScale(payname) * 10 + GameUnit(gamename.value);
    }
    else {
        gamepoints.innerHTML = payprice.value * getpFeeScale(payname) * 10 + "武林币";
    }
}

function SetGamePointsP() {
    var paytowhere = document.getElementById('paytowhere');
    var gamepoints = document.getElementById('gamepoints');
    var payprice = document.getElementById("payprice");
    var payname = document.getElementById('channel').value;
    if (paytowhere.value == "qpaytogame") {
        var gamename = document.getElementById('gamename');
        gamepoints.innerHTML = payprice.value * GameRate(gamename.value) * getpFeeScale(payname) * 10 + GameUnit(gamename.value);
    }
    else {
        gamepoints.innerHTML = payprice.value * getpFeeScale(payname) * 10 + "平台币";
    }
}

function SetGPoints() {
    var gpoints = document.getElementById('gpoints');
    var gpaynums = document.getElementById('gpaynums');
    var gamename = document.getElementById('ggamename');
    gpoints.innerHTML = gpaynums.value * GameRate(gamename.value) + GameUnit(gamename.value);
}

function QuickPayVal() {
    var gamename = $("#gamename").val();
    var servername = $("#servername").val();
    if (gamename == "") {
        $("#gamenameerr").html("请选择游戏！");
        $("#gamename").focus().select();
        return false;
    }
    else {
        if (servername != "") {
            $("#servernameerr").html("");
            if (gamename != "sq") {
                $("#qucikroleinfos").hide();
                return true;
            }
            else {
                if (AccountCheck()) {
                    sqQuickUserSel();
                    $("#qucikroleinfos").show();
                    var role = $("#quickrole").val();
                    if (role != "") {
                        $("#quickroleerr").html("");
                        return true;
                    }
                    else {
                        $("#quickroleerr").html("请选择角色!");
                        return false;
                    }
                }
            }
        }
        else {
            $("#gamenameerr").html("");
            $("#servernameerr").html("请选择服务器！");
            $("#servername").focus().select();
            return false;
        }
    }
}

function QuickRoleVal() {
    var role = $("#quickrole").val();
    if (role != "") {
        $("#quickroleerr").html("");
        return true;
    }
    else {
        $("#quickroleerr").html("请选择角色!");
        return false;
    }
}

function GamePayVal() {
    var gamename = $("#ggamename").val();
    var servername = $("#gservername").val();
    if (gamename == "") {
        $("#ggamenameerr").html("请选择游戏！");
        $("#ggamename").focus().select();
        return false;
    }
    else {
        if (servername != "") {
            $("#gservernameerr").html("");
            if (gamename != "sq") {
                $("#roleinfos").hide(); 
                return true;
            }
            else {
                if (GameAccountCheck()) {
                    sqUserSel();
                    $("#roleinfos").show();
                    var role = $("#role").val();
                    if (role != "") {
                        $("#roleerr").html("");
                        return true;
                    }
                    else {
                        $("#roleerr").html("请选择角色!");
                        return false;
                    }
                }
            }
        }
        else {
            $("#ggamenameerr").html("");
            $("#gservernameerr").html("请选择服务器！");
            $("#gservername").focus().select();
            return false;
        }
    }
}

function RoleVal() {
    var role = $("#role").val();
    if (role != "") {
        $("#roleerr").html("");
        return true;
    }
    else {
        $("#roleerr").html("请选择角色!");
        return false;
    }
}

function sqQuickUserSel() {
    var partner = $("#partner").val();
    var account = $("#paccounttwo").val();
    if (partner == "yykj") {
        account = partner + ":" + account;
    }
    var game = $("#servername").val();
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=sqUserInfo&account=" + account + "&game=" + game,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#quickrole").html("");
                $("#quickrole").append("<option value=''>==请选择==</option>");
                var num = dataObj.root.length;
                $.each(dataObj.root, function(idx, item) {
                    $("#quickrole").append("<option value='" + item.userid + "'>" + item.nickname + "</option>");
                    if (idx == 0) { return true; }
                })
            }
            else {
                $("#quickrole").append("<option value=''>==无角色==</option>");
            }
        }
    });
}

function sqUserSel() {
    var account = $("#gameaccount").val();
    var game = $("#gservername").val();
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=sqUserInfo&account=" + account + "&game=" + game,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#role").html("");
                $("#role").append("<option value=''>==请选择==</option>");
                var num = dataObj.root.length;
                $.each(dataObj.root, function(idx, item) {
                    $("#role").append("<option value='" + item.userid + "'>" + item.nickname + "</option>");
                    if (idx == 0) { return true; }
                })
            }
            else {
                $("#role").append("<option value=''>==无角色,请进入游戏建立==</option>");
            }
        }
    });
}
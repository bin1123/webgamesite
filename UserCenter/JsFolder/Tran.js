function getpFeeScale(channel) {
    var ChannelFee = { alipay: '1', ibank: '1', ypszx: '0.9', ypdx: '0.9', yplt: '0.9', ypzt: '0.8', ypsd: '0.8', ypjcard: '0.8', vpay: '0.5', tenpay: '1', szfbank: '0.95',szfphone:'0.9'};
    var feescale = 1;
    switch(channel){
        case 'alipay':
        feescale = ChannelFee.alipay;
        break;
        case 'ibank':
        feescale = ChannelFee.ibank;
        break;
        case 'yp-szx':
        feescale = ChannelFee.ypszx;
        break;
        case 'yp-dx':
        feescale = ChannelFee.ypdx;
        break;
        case 'yp-lt':
        feescale = ChannelFee.yplt;
        break;
        case 'yp-zt':
        feescale = ChannelFee.ypzt;
        break;
        case 'yp-sd':
        feescale = ChannelFee.ypsd;
        break;
        case 'yp-jcard':
        feescale = ChannelFee.ypjcard;
        break;
        case 'vpay':
        feescale = ChannelFee.vpay;
        break;
        case 'tenpay':
        feescale = ChannelFee.tenpay;
        break;
        case 'szfbank':
        feescale = ChannelFee.szfbank;
        break;
        case 'szfphone':
        feescale = ChannelFee.szfphone;
        break;
    }
    return feescale;
}

function getGiftRate(price) {
    var rate = 0;
    switch(price)
    {
        case 100:        
        case 200:        
        case 300:
        case 400:
            rate = 0.1;
            break;
        case 500:
        case 600:
        case 700:
        case 800:
        case 900:
            rate = 0.12;
            break;
        case 1000:
            rate = 0.14;
            break;
        case 2000:
        case 3000:
            rate = 0.16;
            break;
        case 5000:
        case 10000:
            rate = 0.2;
            break;
        default:
            rate = 0;
            break;
    }
    return rate;
}

function setPayNums(id,channel) {
    var ypszx = "<option value='10'>10元</option><option value='30'>30元</option><option value='50'>50元</option><option value='100'>100元</option><option value='200'>200元</option><option value='300'>300元</option><option value='500'>500元</option><option value='1000'>1000元</option>";
    var ypdx = "<option value='50'>50元</option><option value='100'>100元</option>";
    var yplt = "<option value='20'>20元</option><option value='30'>30元</option><option value='50'>50元</option><option value='100'>100元</option><option value='300'>300元</option><option value='500'>500元</option>";
    var ypzt = "<option value='10'>10元</option><option value='20'>20元</option><option value='30'>30元</option><option value='50'>50元</option><option value='60'>60元</option><option value='100'>100元</option><option value='120'>120元</option><option value='180'>180元</option><option value='300'>300元</option><option value='500'>500元</option>";
    var ypsd = "<option value='10'>10元</option><option value='30'>30元</option><option value='45'>45元</option><option value='50'>50元</option><option value='100'>100元</option><option value='300'>300元</option><option value='350'>350元</option><option value='1000'>1000元</option>";
    var ghzf = "<option value='10'>10元</option><option value='20'>20元</option><option value='30'>30元</option>";
    var df = "<option value='10'>10元</option><option value='20'>20元</option><option value='30'>30元</option><option value='50'>50元</option><option value='100'>100元</option><option value='200'>200元</option><option value='300'>300元</option><option value='500'>500元</option><option value='1000'>1000元</option><option value='2000'>2000元</option><option value='3000'>3000元</option><option value='5000'>5000元</option><option value='10000'>10000元</option><option value='20000'>20000元</option><option value='50000'>50000元</option>";
    switch (channel) {
        case 'yp-szx':
            $("#" + id).html("");
            $("#" + id).append(ypszx);
            break;
        case 'yp-dx':
            $("#" + id).html("");
            $("#" + id).append(ypdx);
            break;
        case 'yp-lt':
            $("#" + id).html("");
            $("#" + id).append(yplt);
            break;
        case 'yp-zt':
            $("#" + id).html("");
            $("#" + id).append(ypzt);
            break;
        case 'yp-sd':
            $("#" + id).html("");
            $("#" + id).append(ypsd);
            break;
        case 'vpay':
            $("#" + id).html("");
            $("#" + id).append(ghzf);
            break;
        default:
            $("#" + id).html("");
            $("#" + id).append(df);
            break;
    }
}

function setGiftPayNums(id, channel) {
    var ypszx = "<option value='10'>10元</option><option value='30'>30元</option><option value='50'>50元</option><option value='100' selected='selected'>100元(赠送100武林币)</option><option value='200'>200元(赠送200武林币)</option><option value='300'>300元(赠送300武林币)</option><option value='500'>500元(赠送600武林币)</option><option value='1000'>1000元(赠送1400武林币)</option>";
    var ypdx = "<option value='50'>50元</option><option value='100' selected='selected'>100元(赠送100武林币)</option>";
    var yplt = "<option value='20'>20元</option><option value='30'>30元</option><option value='50'>50元</option><option value='100' selected='selected'>100元(赠送100武林币)</option><option value='300'>300元(赠送300武林币)</option><option value='500'>500元(赠送600武林币)</option>";
    var ypzt = "<option value='10'>10元</option><option value='20'>20元</option><option value='30'>30元</option><option value='50'>50元</option><option value='60'>60元</option><option value='100' selected='selected'>100元(赠送100武林币)</option><option value='120'>120元(赠送120武林币)</option><option value='180'>180元(赠送180武林币)</option><option value='300'>300元(赠送300武林币)</option><option value='500'>500元(赠送600武林币)</option>";
    var ypsd = "<option value='10'>10元</option><option value='30'>30元</option><option value='45'>45元</option><option value='50'>50元</option><option value='100' selected='selected'>100元(赠送100武林币)</option><option value='300'>300元(赠送300武林币)</option><option value='350'>350元(赠送350武林币)</option><option value='1000'>1000元(赠送1400武林币)</option>";
    var ghzf = "<option value='10'>10元</option><option value='20'>20元</option><option value='30'>30元</option>";
    var szfbank = "<option value='30'>30元</option><option value='50'>50元</option><option value='100' selected='selected'>100元(赠送100武林币)</option><option value='200'>200元(赠送200武林币)</option><option value='300'>300元(赠送300武林币)</option><option value='500'>500元(赠送600武林币)</option><option value='1000'>1000元(赠送1400武林币)</option><option value='2000'>2000元(赠送3200武林币)</option><option value='3000'>3000元(赠送4800武林币)</option>";
    var df = "<option value='10'>10元</option><option value='20'>20元</option><option value='30'>30元</option><option value='50'>50元</option><option value='100' selected='selected'>100元(赠送100武林币)</option><option value='200'>200元(赠送200武林币)</option><option value='300'>300元(赠送300武林币)</option><option value='500'>500元(赠送600武林币)</option><option value='1000'>1000元(赠送1400武林币)</option><option value='2000'>2000元(赠送3200武林币)</option><option value='3000'>3000元(赠送4800武林币)</option><option value='5000'>5000元(赠送10000武林币)</option><option value='10000'>10000元(赠送20000武林币)</option>";
    switch (channel) {
        case 'yp-szx':
            $("#" + id).html("");
            $("#" + id).append(ypszx);
            break;
        case 'yp-dx':
            $("#" + id).html("");
            $("#" + id).append(ypdx);
            break;
        case 'yp-lt':
            $("#" + id).html("");
            $("#" + id).append(yplt);
            break;
        case 'yp-zt':
            $("#" + id).html("");
            $("#" + id).append(ypzt);
            break;
        case 'yp-sd':
            $("#" + id).html("");
            $("#" + id).append(ypsd);
            break;
        case 'vpay':
            $("#" + id).html("");
            $("#" + id).append(ghzf);
            break;
        case 'szfbank':
            $("#" + id).html("");
            $("#" + id).append(szfbank);
            break;
        default:
            $("#" + id).html("");
            $("#" + id).append(df);
            break;
    }
}

function GameRate(gamename) {
    var rate = 1;
    switch (gamename) {
        case "asqx":
        case "sssg":
        case "lc":
        case "nz":
            rate = 10;
            break;
    }
    return rate;
}

function GameUnit(gamename) {
    var Unit = "元宝";
    switch (gamename) {
        case "ztx":
        case "wly":
        case "lj":
        case "mjcs":
        case "hzw":
        case "jtzs":
        case "mxqy":
        case "jdsj":
        case "hyjft":
        case "qszg":
        case "sgyjz":
            Unit = "金币";
            break;
        case "yqdx":
        case "dxz":
        case "djj":
        case "llsg":
            Unit = "黄金";
            break;
        case "sq":
        case "nslm":
            Unit = "钻石";
            break;
        case "sssg":
            Unit = "银;注:1000银=1锭";
            break;
        case "sglj":
            Unit = "通宝"
            break;
    }
    return Unit;
}
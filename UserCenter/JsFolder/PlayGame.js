function tips_pop() {
    var MsgPop = document.getElementById("winpop");
    var converifr = document.getElementById("converifr");
    var popH = parseInt(MsgPop.style.height); //将对象的高度转化为数字
    if (popH == 0) {
        MsgPop.style.display = "block"; //显示隐藏的窗口
        converifr.style.display = "block";
        show = setInterval("changeH('up')", 2);
    }
    else {
        hide = setInterval("changeH('down')", 2);
    }
}

function changeH(str) {
    var MsgPop = document.getElementById("winpop");
    var converifr = document.getElementById("converifr");
    var popH = parseInt(MsgPop.style.height);
    if (str == "up") {
        if (popH <= 200) {
            MsgPop.style.height = (popH + 4).toString() + "px";
            converifr.style.height = (popH + 4).toString() + "px";
        }
        else {
            clearInterval(show);
        }
    }
    if (str == "down") {
        if (popH >= 4) {
            MsgPop.style.height = (popH - 4).toString() + "px";
            converifr.style.height = (popH - 4).toString() + "px";
        }
        else {
            clearInterval(hide);
            MsgPop.style.display = "none";  //隐藏DIV
            converifr.style.display = "none";
        }
    }
}

function winpopshow(href,image) {
    var MsgPop = document.getElementById("winpop");
    var converifr = document.getElementById("converifr");
    MsgPop.style.display = "block";
    converifr.style.display = "block";
    MsgPop.style.height = '0px';
    converifr.style.height = '0px';
    if(href != ""){
        $("#tc").attr("href", href);
    }
    if (image != "") {
        $("#tcimg").attr("src", image);
    }
    setTimeout("tips_pop()", 800); //3秒后调用tips_pop()这个函数
}

function onloadresize() {
    var tb01 = document.getElementById("tb01");
    var main = document.getElementById("main");
    var minheight;
    minheight = document.body.clientHeight;
    tb01.style.height = minheight + "px";
    main.style.height = minheight + "px";
}
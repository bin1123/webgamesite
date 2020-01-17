function bookmark() {
    if (getCookie("bookmark") != "yes") {
        setCookie("bookmark", "yes", 365);
        AddFavorite("http://www.dao50.com/","到武林游戏平台");
    }
}
function setCookie(name, value, days) {
    var exp = new Date();
    exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
}
function getCookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) {
        return unescape(arr[2]);
        return null;
    }
}
function delCookie(name) {
    var exp = new Date();
    exp.setTime(exp.getTime() - 1);
    var cval = getCookie(name);
    if (cval != null) {
        document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
    }
}

function AddFavorite(sURL, sTitle) { try { window.external.addFavorite(sURL, sTitle); } catch (e) { try { window.sidebar.addPanel(sTitle, sURL, ""); } catch (e) { alert("加入收藏失败，请使用Ctrl+D进行添加"); } } }

window.onbeforeunload = function() {
    bookmark();
}
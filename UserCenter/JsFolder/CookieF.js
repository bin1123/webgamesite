function setCookie(CookieName, CookieValue) {
    var dMinute = 10;
    var exp = new Date();
    exp.setTime(exp.getTime() + dMinute * 60 * 1000);
    document.cookie = CookieName + "=" + escape(CookieValue) + ";expires=" + exp.toGMTString();
}

function setCookieHour(CookieName, CookieValue,dHour) {
    var exp = new Date();
    exp.setTime(exp.getTime() + dHour * 3600 * 1000);
    document.cookie = CookieName + "=" + escape(CookieValue) + ";expires=" + exp.toGMTString() + ";path=/";
}

function getCookie(CookieName) {
    var arr = document.cookie.match(new RegExp("(^| )" + CookieName + "=([^;]*)(;|$)"));
    if (arr != null) return unescape(arr[2]); return null;
}

function delCookie(CookieName) {
    if (getCookie(CookieName)) {
        document.cookie = CookieName + "=" + "; expires=Thu, 01-Jan-70 00:00:01 GMT";
    } 
}
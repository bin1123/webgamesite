function StringBuilder() {
    this.__string__ = new Array();
}
StringBuilder.prototype.append = function(str) {
    this.__string__.push(str);
}
StringBuilder.prototype.toString = function() {
    return this.__string__.join("");
}

function getBrowser() {
    var YQ = { browser: 'unknow', browserVersion: '0' };
    var ua = navigator.userAgent.toLowerCase(),
	browserRegExp = {
	    ie: /msie[ ]([\w.]+)/,
	    firefox: /firefox[ |\/]([\w.]+)/,
	    chrome: /chrome[ |\/]([\w.]+)/,
	    safari: /version[ |\/]([\w.]+)[ ]safari/,
	    opera: /opera[ |\/]([\w.]+)/
	};
    for (var i in browserRegExp) {
        var match = browserRegExp[i].exec(ua);
        if (match) {
            YQ.browser = i;
            YQ.browserVersion = match[1];
            break;
        }
    }
    var browserInfo = new StringBuilder();
    browserInfo.append(YQ.browser);
    browserInfo.append('|');
    browserInfo.append(YQ.browserVersion);
    return browserInfo.toString();
}

function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
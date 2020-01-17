function GameSel(gamelist,defaulttext) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=GameAllSel",
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#" + gamelist).html("");
                if(defaulttext != "") {
                    $("#" + gamelist).append("<option value=''>" + defaulttext + "</option>");
                }
                $.each(dataObj.root, function(idx, item) {
                    $("#" + gamelist).append("<option value='" + item.abbre + "'>" + item.gamename + "</option>");
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameAllSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=GameAllSel",
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#gamename").html("");
                $("#ggamename").html("");
                $("#gamename").append("<option value=''>==请选择==</option>");
                $("#ggamename").append("<option value=''>==请选择==</option>");
                $.each(dataObj.root, function(idx, item) {
                    if (item.abbre != "sgyjz") {
                        $("#gamename").append("<option value='" + item.abbre + "'>" + item.gamename + "</option>");
                        $("#ggamename").append("<option value='" + item.abbre + "'>" + item.gamename + "</option>");
                    }
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameAllSelSgyjz() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=GameAllSel",
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#gamename").html("");
                $("#ggamename").html("");
                $("#gamename").append("<option value=''>==请选择==</option>");
                $("#ggamename").append("<option value=''>==请选择==</option>");
                $.each(dataObj.root, function(idx, item) {
                    $("#gamename").append("<option value='" + item.abbre + "'>" + item.gamename + "</option>");
                    $("#ggamename").append("<option value='" + item.abbre + "'>" + item.gamename + "</option>");
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameAllSelCheck(gameid) {
    var gameabbre = GameAbbreSelByID(gameid);
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=GameAllSel",
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#gamename").html("");
                $("#ggamename").html("");
                $("#gamename").append("<option value=''>==请选择==</option>");
                $("#ggamename").append("<option value=''>==请选择==</option>");
                $.each(dataObj.root, function(idx, item) {
                    if (gameabbre == item.abbre) {
                        $("#gamename").append("<option selected='selected' value='" + item.abbre + "'>" + item.gamename + "</option>");
                        $("#ggamename").append("<option selected='selected' value='" + item.abbre + "'>" + item.gamename + "</option>");
                        var server = getUrlParam("server");
                        if (server.length > 1) {
                            server = server.substring(1);
                            ServerSelOfGameCheck('servername', gameabbre, '==请选择==', server);
                            ServerSelOfGameCheck('gservername', gameabbre, '==请选择==', server);
                        }
                    }
                    else {
                        $("#gamename").append("<option value='" + item.abbre + "'>" + item.gamename + "</option>");
                        $("#ggamename").append("<option value='" + item.abbre + "'>" + item.gamename + "</option>");
                    }
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function ServerSelOfGame(serverlist, gameabbre, defaulttext) {
    if (gameabbre == "") {
        $("#" + serverlist).html("");
        $("#" + serverlist).append("<option value=''>" + defaulttext + "</option>");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerSelByGame&Abbre=" + gameabbre,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#" + serverlist).html("");
                if (defaulttext != "") {
                    $("#" + serverlist).append("<option value=''>" + defaulttext + "</option>");
                }
                var num = dataObj.root.length;
                $.each(dataObj.root, function(idx, item) {
                    var abbre = item.abbre;
                    var servernum;
                    if (num > 9999) {
                        servernum = abbre.substr(abbre.length - 5, 5);
                    }
                    else if (num > 999) {
                        servernum = abbre.substr(abbre.length - 4, 4);
                    }
                    else if (num > 99) {
                        servernum = abbre.substr(abbre.length - 3, 3);
                    }
                    else if (num > 9) {
                        servernum = abbre.substr(abbre.length - 2, 2);
                    }
                    else {
                        servernum = abbre.substr(abbre.length - 1, 1);
                    }
                    num = num - 1;
                    $("#" + serverlist).append("<option value='" + abbre + "'>" + servernum + '服_' + item.servername + "</option>");
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function ServerSelOfGameAll(serverlist, gameabbre, defaulttext) {
    if (gameabbre == "") {
        $("#" + serverlist).html("");
        $("#" + serverlist).append("<option value=''>" + defaulttext + "</option>");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=" + gameabbre,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#" + serverlist).html("");
                if (defaulttext != "") {
                    $("#" + serverlist).append("<option value=''>" + defaulttext + "</option>");
                }
                $.each(dataObj.root, function(idx, item) {
                    var abbre = item.abbre;
                    var servernum = item.id;
                    $("#" + serverlist).append("<option value='" + abbre + "'>" + servernum + '服_' + item.servername + "</option>");
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function ServerSelOfGameVal(serverlist, gameabbre, defaulttext) {
    if (gameabbre == "") {
        $("#" + serverlist).html("");
        $("#" + serverlist).append("<option value=''>" + defaulttext + "</option>");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=" + gameabbre,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#" + serverlist).html("");
                if (defaulttext != "") {
                    $("#" + serverlist).append("<option value=''>" + defaulttext + "</option>");
                }
                $.each(dataObj.root, function(idx, item) {
                    var begintime = new Date(item.begintime.replace(/\-/g, "\/"));
                    var now = new Date();
                    var abbre = item.abbre;
                    if (begintime <= now && abbre != 'sgyjz999') {
                        var servernum = item.id;
                        $("#" + serverlist).append("<option value='" + abbre + "'>" + servernum + '服_' + item.servername + "</option>");
                    }
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function ServerSelOfGameCheck(serverlist, gameabbre, defaulttext,serverid) {
    if (gameabbre == "") {
        $("#" + serverlist).html("");
        $("#" + serverlist).append("<option value=''>" + defaulttext + "</option>");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=" + gameabbre,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#" + serverlist).html("");
                if (defaulttext != "") {
                    $("#" + serverlist).append("<option value=''>" + defaulttext + "</option>");
                }
                $.each(dataObj.root, function(idx, item) {
                    var begintime = new Date(item.begintime.replace(/\-/g, "\/"));
                    var now = new Date();
                    var abbre = item.abbre;
                    if (begintime <= now && abbre != 'sgyjz999') {
                        var servernum = item.id;
                        if (servernum == serverid) {
                            $("#" + serverlist).append("<option selected='selected' value='" + abbre + "'>" + servernum + '服_' + item.servername + "</option>");
                        }
                        else {
                            $("#" + serverlist).append("<option value='" + abbre + "'>" + servernum + '服_' + item.servername + "</option>");
                        }
                    }
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function ServerSel(gameabbre) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerSelByGame&Abbre=" + gameabbre,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#servername").html("");
                $("#gservername").html("");
                var num = dataObj.root.length;
                $.each(dataObj.root, function(idx, item) {
                    var abbre = item.abbre;
                    var servernum;
                    if (num > 9999) {
                        servernum = abbre.substr(abbre.length - 5, 5);
                    }
                    else if (num > 999) {
                        servernum = abbre.substr(abbre.length - 4, 4);
                    }
                    else if (num > 99) {
                        servernum = abbre.substr(abbre.length - 3, 3);
                    }
                    else if (num > 9) {
                        servernum = abbre.substr(abbre.length - 2, 2);
                    }
                    else {
                        servernum = abbre.substr(abbre.length - 1, 1);
                    }
                    num = num - 1;
                    $("#servername").append("<option value='" + abbre + "'>" + servernum + '服_' + item.servername + "</option>");
                    $("#gservername").append("<option value='" + abbre + "'>" + servernum + '服_' + item.servername + "</option>");
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameOfServerSel(gameabbre) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerSelByGame&Abbre=" + gameabbre,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            var num = 1;
            if (dataObj.root.length > 0) {
                $("#serverinfo").html("");
                var datanum = dataObj.root.length;
                var num = 1;
                $.each(dataObj.root, function(idx, item) {
                    var abbre = item.abbre;
                    var servernum;
                    if (num > 9999) {
                        servernum = abbre.substr(abbre.length - 5, 5);
                    }
                    else if (num > 999) {
                        servernum = abbre.substr(abbre.length - 4, 4);
                    }
                    else if (num > 99) {
                        servernum = abbre.substr(abbre.length - 3, 3);
                    }
                    else if (datanum > 9) {
                        servernum = abbre.substr(abbre.length - 2, 2);
                    }
                    else {
                        servernum = abbre.substr(abbre.length - 1, 1);
                    }
                    datanum = datanum - 1;
                    var servername = servernum + '服_' + item.servername;
                    if (num == 1) {
                        $("#serverinfo").append("<a class='s_list' onclick='serverCheck(\"" + abbre + "\",\"" + servername + "\")'><span class='zhan'></span>" + servername + "<span style='color: #009b80'>(新)</span></a>");
                    }
                    else {
                        $("#serverinfo").append("<a class='s_list' onclick='serverCheck(\"" + abbre + "\",\"" + servername + "\")'><span class='not_zhan'></span>" + servername + "</a>");
                    }
                    num++;
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameOfSqServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerSelByGame&Abbre=sq",
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            var num = 1;
            if (dataObj.root.length > 0) {
                $("#tjul").html("");
                $("#allgameul").html("");
                var datanum = dataObj.root.length;
                $("#lastgame").val(datanum);
                var num = 1;
                $.each(dataObj.root, function(idx, item) {
                    var abbre = item.abbre;
                    var servernum;
                    if (num > 9999) {
                        servernum = abbre.substr(abbre.length - 5, 5);
                    }
                    else if (num > 999) {
                        servernum = abbre.substr(abbre.length - 4, 4);
                    }
                    else if (num > 99) {
                        servernum = abbre.substr(abbre.length - 3, 3);
                    }
                    else if (datanum > 9) {
                        servernum = abbre.substr(abbre.length - 2, 2);
                    }
                    else {
                        servernum = abbre.substr(abbre.length - 1, 1);
                    }
                    datanum = datanum - 1;
                    var servername = servernum + '服_' + item.servername;
                    if (num == 1) {
                        $("#tjul").append("<li class='hot'><a href='javascript:SetGame(\"" + abbre + "\");'>" + servername + "</a></li>");
                        $("#allgameul").append("<li class='hot'><a href='javascript:SetGame(\"" + abbre + "\");'>" + servername + "</a></li>");
                    }
                    else {
                        $("#allgameul").append("<li><a href='javascript:SetGame(\"" + abbre + "\");'>" + servername + "</a></li>");
                    }
                    num++;
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameOfServerSelWeb(gameabbre) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerSelByGame&Abbre=" + gameabbre,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            var num = 1;
            if (dataObj.root.length > 0) {
                $("#servercheck").html("");
                var num = dataObj.root.length;
                $.each(dataObj.root, function(idx, item) {
                    var abbre = item.abbre;
                    var servernum;
                    if (num > 9999) {
                        servernum = abbre.substr(abbre.length - 5, 5);
                    }
                    else if (num > 999) {
                        servernum = abbre.substr(abbre.length - 4, 4);
                    }
                    else if (num > 99) {
                        servernum = abbre.substr(abbre.length - 3, 3);
                    }
                    else if (num > 9) {
                        servernum = abbre.substr(abbre.length - 2, 2);
                    }
                    else {
                        servernum = abbre.substr(abbre.length - 1, 1);
                    }
                    num = num - 1;
                    $("#servercheck").append("<a id=" + abbre + " href='http://game.dao50.com/GCenter/PlayGame.aspx?gn="+abbre+"' target='_blank'>" + servernum + '服_' + item.servername + "</a>");
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameOfServerSelWebSSSG(gameabbre,num) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerNumSelByGame&Abbre=" + gameabbre + "&num=" + num,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            var num = 1;
            if (dataObj.root.length > 0) {
                $("#servercheck").html("<a href='http://www.dao50.com/news/sssg_fwq/' target='_blank'>更多服务器</a>");
                $.each(dataObj.root, function(idx, item) {
                    var abbre = item.abbre;
                    var servernum = abbre.substr(abbre.length - 2, 2);
                    $("#servercheck").prepend("<a id=" + abbre + " href='http://game.dao50.com/GCenter/PlayGame.aspx?gn=" + abbre + "' target='_blank'>" + servernum + '服_' + item.servername + "</a>");
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameOfServerSelWeb1(gameabbre) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerSelByGame&Abbre=" + gameabbre,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#servercheck").html("");
                var num = dataObj.root.length;
                $.each(dataObj.root, function(idx, item) {
                    var abbre = item.abbre;
                    var servernum;
                    if (num > 9999) {
                        servernum = abbre.substr(abbre.length - 5, 5);
                    }
                    else if (num > 999) {
                        servernum = abbre.substr(abbre.length - 4, 4);
                    }
                    else if (num > 99) {
                        servernum = abbre.substr(abbre.length - 3, 3);
                    }
                    else if (num > 9) {
                        servernum = abbre.substr(abbre.length - 2, 2);
                    }
                    else {
                        servernum = abbre.substr(abbre.length - 1, 1);
                    }
                    num = num - 1;
                    $("#servercheck").append("<span><a id=" + abbre + " href='http://game.dao50.com/GCenter/PlayGame.aspx?gn=" + abbre + "' target='_blank'>" + servernum + '服_' + item.servername + "</a></span>");
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameOfServerSelWebSXD(gameabbre,num) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerNumSelByGame&Abbre=" + gameabbre + "&num=" + num,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#servercheck").html("<span><a href='http://www.dao50.com/news/sxd_fwq/' target='_blank'>更多服务器</a></span>");
                $.each(dataObj.root, function(idx, item) {
                    var abbre = item.abbre;
                    var servernum = abbre.substr(abbre.length - 2, 2);
                    $("#servercheck").prepend("<span><a id=" + abbre + " href='http://game.dao50.com/GCenter/PlayGame.aspx?gn=" + abbre + "' target='_blank'>" + servernum + '服_' + item.servername + "</a></span>");
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function ServerNewSel(gameid,id) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerNewSel&gameid=" + gameid,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $.each(dataObj.root, function(idx, item) {
                    $("#" + id).html(item.servername);
                    $("#" + id + 'radio').val(item.abbre);
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function ServerNewSel1(gameid) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerNewSel&gameid=" + gameid,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $.each(dataObj.root, function(idx, item) {
                    $("#nserverb").html(item.servername);
                    $("#nserverb").click(function() {
                        serverCheck(item.abbre,item.servername);
                    });
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function ServerLastSel(gameid, id,account) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerLoginLastSel&gameid=" + gameid + "&account=" + account,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $.each(dataObj.root, function(idx, item) {
                    $("#" + id).html(item.servername);
                    $("#" + id + 'radio').val(item.abbre);
                    if (idx == 0) { return true; }
                })
            }
        }
    });
} 

function ServerSqLastSel(account) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerLoginLastSel&gameid=12&account=" + account,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $.each(dataObj.root, function(idx, item) {
                    if (item.servername == "") {
                        $("#pserver").html("无登陆记录");
                    }
                    else {
                        $("#pserver").html(item.servername);
                        $("#gamename").val(item.abbre);
                    
                    }
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function ServerNewSel2(gameid,id) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerNewSel&gameid=" + gameid,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $.each(dataObj.root, function(idx, item) {
                    $("#" + id).html(item.servername);
                    var href = "http://game.dao50.com/GCenter/PlayGame.aspx?gn=" + item.abbre;
                    $("#" + id).attr("href",href);
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function ServerLastSelWeb(gameid, id, account) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerLoginLastSel&gameid=" + gameid + "&account=" + account,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $.each(dataObj.root, function(idx, item) {
                    $("#" + id).html(item.servername);
                    var href = "http://game.dao50.com/GCenter/PlayGame.aspx?gn=" + item.abbre;
                    $("#" + id).attr("href",href);
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function HelpClassSel(gameabbre, classname,cssclass,id) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.aspx",
        data: "AjaxType=HelpClassSel&gameabbre=" + gameabbre + "&classname=" + classname,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $.each(dataObj.root, function(idx, item) {
                    var sTitle;
                    if (item.title.length > 7) {
                        sTitle = item.title;
                        sTitle = sTitle.substr(0, 7);
                    }
                    else {
                        sTitle = item.title;
                    }
                    var text = "<li class='" + cssclass + "'><a href='" + item.url + "' title='" + item.title + "' target='_blank'>" + sTitle + "</a></li>";
                    $("#" + id).append(text);
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function HelpClassLJSel(gameabbre, classname, id) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.aspx",
        data: "AjaxType=HelpClassLJSel&gameabbre=" + gameabbre + "&classname=" + classname,
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    var num = 1;
                    var len = dataObj.root.length;
                    $.each(dataObj.root, function(idx, item) {
                        var sTitle;
                        var text;
                        if (item.title.length > 7) {
                            sTitle = item.title;
                            sTitle = sTitle.substr(0, 7);
                        }
                        else {
                            sTitle = item.title;
                        }
                        if (num == 1) {
                            if (classname = "jchd") {
                                text = "<ul style='display:block;'>";
                            }
                            else {
                                text = "<ul>";
                            }
                        }
                        else {
                            text = "";
                        }
                        text = text + "<li><a target='_blank' href='" + item.url + "' title='" + item.title + "'><font color='#f9fc03'>" + sTitle + "</font></a></li>";
                        if (num == len) {
                            text = text + "</ul>";
                        }
                        $("#" + id).append(text);
                        num++;
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}

function NoticeSel(gameabbre, id) {
    var abbre;
    switch(gameabbre)
    {
        case "sssg1":
        case "sssg2":
        case "sssg3":
        case "sssg4":
        case "sssg5":
        case "sssg6":
        case "sssg7":
        case "sssg8":
        case "sssg9":
        case "sssg10":
        case "sssg11":
        case "sssg12":
        case "sssg13":
        case "sssg14":
        case "sssg15":
        case "sssg16":
        case "sssg17":
        case "sssg18":
        case "sssg19":
        case "sssg20":
        case "sssg21":
        case "sssg22":
        case "sssg23":
        case "sssg24":
        case "sssg25":
        case "sssg26":
        case "sssg27":
        case "sssg28":
        case "sssg29":
        case "sssg30":
        case "sssg31":
        case "sssg32":
        case "sssg33":
        case "sssg34":
        case "sssg35":
            abbre = "sssggame";
            break;
        case "mg1":
        case "mg2":
            abbre = "mggame";
            break;
        default:
            abbre = "defaultgame";
            break;
        
    }
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=NoticeSel&abbre=" + abbre,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                var num = 1;
                $.each(dataObj.root, function(idx, item) {
                    var sTitle;
                    num++;
                    if (item.title.length > 36) {
                        sTitle = item.title;
                        sTitle = sTitle.substr(0, 36);
                    }
                    else {
                        sTitle = item.title;
                    }
                    var text = "<li><a href='" + item.url + "' title='" + item.title + "' target='_blank'>" + sTitle + "</a></li>";
                    $("#" + id).append(text);
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameClassSel(gameabbre, classname, cssclass, id) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.aspx",
        data: "AjaxType=HelpClassSel&gameabbre=" + gameabbre + "&classname=" + classname,
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                var num = 0;
                $.each(dataObj.root, function(idx, item) {
                    var sTitle;
                    if (item.title.length > 30) {
                        if (num > 2) {
                            sTitle = item.title;
                            sTitle = sTitle.substr(0, 20);
                        }
                        else {
                            sTitle = item.title;
                        }
                        num++;
                    }
                    else {
                        sTitle = item.title;
                    }
                    var text = "<li class='" + cssclass + "'><a href='" + item.url + "' title='" + item.title + "' target='_blank'>" + sTitle + "</a></li>";
                    $("#" + id).append(text);
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameClassSqSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.aspx",
        data: "AjaxType=HelpClassSqSel",
        beforeSend: function() {
        },
        success: function(data) {
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                var num = 0;
                $.each(dataObj.root, function(idx, item) {
                    var sTitle;
                    if (item.title.length > 30) {
                        if (num > 2) {
                            sTitle = item.title;
                            sTitle = sTitle.substr(0, 20);
                        }
                        else {
                            sTitle = item.title;
                        }
                        num++;
                    }
                    else {
                        sTitle = item.title;
                    }
                    var text = "<li class='" + cssclass + "'><a href='" + item.url + "' title='" + item.title + "' target='_blank'>" + sTitle + "</a></li>";
                    $("#" + id).append(text);
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function GameAbbreSelByID(gameid) {
    var gamejson = [{ id: '16', abbre: 'dxz' }, { id: '29', abbre: 'swjt' }, { id: '38', abbre: 'yjxy' }, { id: '49', abbre: 'qxz' }, { id: '51', abbre: 'wwsg' }, { id: '53', abbre: 'jy' }, { id: '57', abbre: 'dtgzt' }, { id: '60', abbre: 'sgyjz' }, { id: '63', abbre: 'zwx'}];
    var gameabbre = "";
    $.each(gamejson, function(idx, item) {
        if (item.id == gameid) {
            gameabbre = item.abbre;
        }
    });
    return gameabbre;
}
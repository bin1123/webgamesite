function ServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=nslm",
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                var num = 1;
                $.each(dataObj.root, function(idx, item) {
                    var begintime = new Date(item.begintime.replace(/\-/g, "\/"));
                    var now = new Date();
                    if (begintime <= now) {
                        var gamename = item.id + "服_" + item.servername;
                        var gameurl = "http://game.dao50.com/GCenter/nslm.aspx?gn=" + item.abbre;
                        var text = "<li><a href='" + gameurl + "' target=\"_game\" title='到武林 "+gamename;
                        if (num == 1) {
                            $("#tjserver").html("<span>" + gamename + "</span>新服开启");
                            $("#tjserver").attr("href", gameurl);
                            $("#tjserver").attr("title", "到武林 " + gamename);
                            $("#allserver").append(text + "' class='s5'><span>" + gamename + "</span>新服开启</a></li>");
                        }
                        else {
                            $("#allserver").append(text + "' class='s4'><span>" + gamename + "</span>火爆开启</a></li>");
                        }
                        num++;
                    }
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

function ServerLastSel(account) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerLoginLastSel&gameid=37&account=" + account,
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    $.each(dataObj.root, function(idx, item) {
                        var abbre = item.abbre;
                        if (abbre == "") {
                            $("#loginedserver").html("请先登录游戏！谢谢！");
                            return;
                        }
                        var servername = item.servername;
                        $("#loginedserver").attr("href", "http://game.dao50.com/GCenter/nslm.aspx?gn=" + abbre);
                        $("#loginedserver").html("<span>" + servername + "</span>火爆开启");
                        $("#loginedserver").attr("title", "到武林 " + servername);
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}

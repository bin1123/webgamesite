function ServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=jdsj",
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
                        var gamename = "到武林" + item.id + "服_" + item.servername;
                        var gameurl = "jdsj.aspx?gn=" + item.abbre;
                        var text = "<li><a href='" + gameurl + "'>" + gamename + "</a></li>";
                        if (num == 1) {
                            $("#tjserver").html(gamename);
                            $("#tjserver").attr("href", gameurl);
                            $("#tjserver_xz").html(gamename);
                            $("#tjserver_xz").attr("href", gameurl);
                        }
                        $("#allserver").append(text);
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
        data: "AjaxType=ServerLoginLastSel&gameid=31&account=" + account,
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
                        $("#loginedserver").attr("href", "jdsj.aspx?gn=" + abbre);
                        $("#loginedserver").html(servername);
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}

function XZServerS() {
    $("#xzserver").show();
}

function XZServerH() {
    $("#xzserver").hide();
}

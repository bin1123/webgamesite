function LoadGame(gamename) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.aspx",
        data: "AjaxType=ktpdGameCL&game=" + gamename,
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length < 10) {
                alert("登陆异常!");
            }
            else {
                var dataRep = "{\"result\":\"ok\",\"url\":\"" + data + "\"}";
                window.external.StartGame(dataRep);
            }
        }
    });
}

function ServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=ktpd",
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    $("#allserver").html("");
                    var num = 1;
                    var datanum = dataObj.root.length - 1;
                    $.each(dataObj.root, function(idx, item) {
                        var begintime = new Date(item.begintime.replace(/\-/g, "\/"));
                        var now = new Date();
                        if (begintime <= now) {
                            var text = "<a href='javascript:LoadGame(" + item.abbre + ");'>" + item.id + "服_" + item.servername + "</a>";
                            if (num == 1) {
                                $("#tjfwq").html("");
                                $("#tjfwq").html(text);
                            }
                            text = text + "</br>";
                            $("#allserver").append(text);
                            num++;
                        }
                        if (idx == datanum) { return false; }
                    })
                }
            }
        }
    });
}

function ServerLastSel(account) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerLoginLastSel&gameid=55&account=" + account,
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    $.each(dataObj.root, function(idx, item) {
                        var abbre = item.abbre;
                        var servername = item.servername;
                        if (abbre == "") {
                            return;
                        }
                        $("#lastlogingame").val(abbre);
                        $("#zjdl").html(servername);
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}
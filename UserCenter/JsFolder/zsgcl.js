function ServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=zsg",
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    var num = 1;
                    $.each(dataObj.root, function(idx, item) {
                        var begintime = new Date(item.begintime.replace(/\-/g, "\/"));
                        var now = new Date();
                        if (begintime <= now) {
                            var text = "<li><a href='zsgplay.aspx?gn=" + item.abbre + "' class='server_block'><span class='state_icon state0'></span>" + item.id + "服_" + item.servername + "</a></li>";
                            if (num < 6) {
                                $("#tjfw").append(text);
                            }
                            $("#allfw").append(text);
                            num++;
                        }
                        if (idx == 0) { return true; }
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
        data: "AjaxType=ServerLoginLastSel&gameid=24&account=" + account,
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    $.each(dataObj.root, function(idx, item) {
                        var abbre = item.abbre;
                        var servername = item.servername;
                        if (abbre == "") {
                            $("#zjdlname").after("请先登录游戏！谢谢！");
                            $("#start").attr("href", "zsgplay.aspx?gn=zsg1");
                            return;
                        }
                        $("#zjdlurl").attr("href", "zsgplay.aspx?gn=" + abbre);
                        $("#start").attr("href", "zsgplay.aspx?gn=" + abbre);
                        $("#zjdlname").after(servername);
                        $("#start").attr("title", servername);
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}

function ServerTJSel() {
    $("#tjfwli").addClass("on");
    $("#tjfw").show();
    $("#allfwli").removeClass("on");
    $("#allfw").hide();
}

function ServerAllSel() {
    $("#tjfwli").removeClass("on");
    $("#tjfw").hide();
    $("#allfwli").addClass("on");
    $("#allfw").show();
}
function ServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=dntg",
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                $("#allserver").html("");
                $.each(dataObj.root, function(idx, item) {
                    var begintime = new Date(item.begintime.replace(/\-/g, "\/"));
                    var now = new Date();
                    if (begintime <= now) {
                        var gamename = item.id + "服_" + item.servername;
                        var gameurl = "http://game.dao50.com/GCenter/dntg.aspx?gn=" + item.abbre;
                        var text = "<a href='" + gameurl + "' class='s_all' title='到武林 " + gamename + "'>"+gamename+"</a>";
                        $("#allserver").append(text);
                    }
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}

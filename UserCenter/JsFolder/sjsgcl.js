function ServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=sjsg",
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                $("#qbyx").val(data);
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    var num = 1;
                    $.each(dataObj.root, function(idx, item) {
                        var begintime = new Date(item.begintime.replace(/\-/g, "\/"));
                        var now = new Date();
                        if (begintime <= now) {
                            var text = "<li><a href='sjsg.aspx?gn=" + item.abbre + "' onclick='LoadGame(this.href);return false;'>" + item.id + "服_" + item.servername + "</a></li>";
                            $("#sjsgsever").append(text);
                            if (num == 1) {
                                $("#zxkf").attr("href", "sjsg.aspx?gn=" + item.abbre);
                                $("#zxkf").html(item.servername);
                            }
                            else if(num == 15)
                            {
                                $("#tjyx").val($("#sjsgsever").html());
                            }
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
        data: "AjaxType=ServerLoginLastSel&gameid=22&account=" + account,
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
                        $("#zjdl").attr("href", "sjsg.aspx?gn=" + abbre);
                        $("#zjdl").html(servername);
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}

function ServerAllSel() {
    $("#tjf").removeClass("on");
    $("#syf").addClass("on");
    var data = $("#qbyx").val();
    var dataObj = eval("(" + data + ")");
    if (dataObj.root.length > 0) {
        var num = 1;
        $("#sjsgsever").html("");
        $.each(dataObj.root, function(idx, item) {
            var text = "<li><a href='sjsg.aspx?gn=" + item.abbre + "' onclick='LoadGame(this.href);return false;'>" + item.id + "服_" + item.servername + "</a></li>";
            if (num < 15) {
                $("#sjsgsever").append(text);
            }
            num++;
            if (idx == 0) { return true; }
        })
    }
}

function ServerTJSel() {
    $("#syf").removeClass("on");
    $("#tjf").addClass("on");
    var data = $("#tjyx").val();
    $("#sjsgsever").html(data);
}

function LoadGame(flashurl) {
    if (flashurl.length < 2) {
        return fale;
    }
    $.ajax({
        url: flashurl,
        success: function(data) {
            if (data.length > 0) {
                location.href = data;
            }
        }
    });
} 
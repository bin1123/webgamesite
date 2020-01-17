function ServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=ahxy",
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                $("#qbyx").val(data);
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    var num = 1;
                    var datanum = dataObj.root.length - 1;
                    $.each(dataObj.root, function(idx, item) {
                        var begintime = new Date(item.begintime.replace(/\-/g, "\/"));
                        var now = new Date();
                        if (begintime <= now) {
                            var text = "<li><a href='ahxy.aspx?gn=" + item.abbre + "'>" + item.id + "服_" + item.servername + "</a></li>";
                            if (num == 1) {
                                $("#zxkf").attr("href", "ahxy.aspx?gn=" + item.abbre);
                                $("#zxkf").html(item.servername);
                            }
                            if (num < 25) {
                                $("#ahxysever").append(text);
                            }
                            num++;
                        }
                        if (idx == datanum) { $("#tjyx").val($("#ahxysever").html()); return false; }
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
        data: "AjaxType=ServerLoginLastSel&gameid=43&account=" + account,
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
                        $("#zjdl").attr("href", "ahxy.aspx?gn=" + abbre);
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
        $("#ahxysever").html("");
        $.each(dataObj.root, function(idx, item) {
            var begintime = new Date(item.begintime.replace(/\-/g, "\/"));
            var now = new Date();
            if (begintime <= now) {
                var text = "<li><a href='ahxy.aspx?gn=" + item.abbre + "'>" + item.id + "服_" + item.servername + "</a></li>";
                $("#ahxysever").append(text);
            }
            if (idx == 0) { return true; }
        })
    }
}

function ServerTJSel() {
    $("#syf").removeClass("on");
    $("#tjf").addClass("on");
    var data = $("#tjyx").val();
    $("#ahxysever").html(data);
}
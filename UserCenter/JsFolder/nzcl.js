function ServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=nz",
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    var num = 1;
                    var texts;
                    $.each(dataObj.root, function(idx, item) {
                        var text = "<li><a href='nz.aspx?gn=" + item.abbre + "'>" + item.id + "服_" + item.servername + "</a></li>";
                        if (num < 4) {
                            $("#nzsever").append(text);
                        }
                        texts = texts + text;
                        num++;
                        if (idx == 0) {
                            $("#tjyx").val($("#nzsever").html());
                            $("#qbyx").val(texts); return true;
                        }
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
        data: "AjaxType=ServerLoginLastSel&gameid=27&account=" + account,
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
                        $("#zjdl").attr("href", "nz.aspx?gn=" + abbre);
                        $("#zjdl").html(servername);
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}

function ServerAllSel() {
    $("#tjf").removeClass("cur");
    $("#syf").addClass("cur");
    var data = $("#qbyx").val();
    $("#nzsever").html(data);
}

function ServerTJSel() {
    $("#syf").removeClass("cur");
    $("#tjf").addClass("cur");
    var data = $("#tjyx").val();
    $("#nzsever").html(data);
}
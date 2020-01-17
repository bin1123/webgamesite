function show(id, obj) {
    var btn_id = $("#xfdh .on").attr("id");
    btn_id = btn_id.substring(7);
    var btn_html = $("#se_btn_" + btn_id).html();
    $("#se_btn_" + btn_id).html("<a href=\"javascript:show('" + btn_id + "',this);\">" + btn_html + "</a>");
    $("#xfdh p").removeClass("on");
    $("#se_btn_" + id).addClass("on");
    $("#se_btn_" + id).html($("#se_btn_" + id + " a").html());
    $("#qb_list ul").hide();
    $("#s_list_" + id).show();
}

$(function() {
    if (!$("#xfdh p").hasClass("on")) {
        $("#xfdh p:eq(0)").addClass("on");
        var btn_html = $("#se_btn_1 a").html();
        $("#se_btn_1").html(btn_html);
    }
});

function LJ2ServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=ljer",
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    var num = 1;
                    $.each(dataObj.root, function(idx, item) {
                        var text = "<li><a href='lj2.aspx?gn=" + item.abbre + "' class='s1'>" + item.id + "服_" + item.servername + "</a></li>";
                        if (num < 10) {
                            $("#s_list_1").append(text);
                        }
                        $("#s_list_2").append(text);
                        num++;
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}

function ServerLJ2LastSel(account) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerLoginLastSel&gameid=21&account=" + account,
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
                            return;
                        }
                        $("#zjdlurl").attr("href", "lj2.aspx?gn=" + abbre);
                        $("#start").attr("href", "lj2.aspx?gn=" + abbre);
                        $("#zjdlurl").attr("title", servername);
                        $("#start").attr("title", servername);
                        $("#zjdlurl").html(servername);
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}
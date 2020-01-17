function CodeSel(gameabbre) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=CodeTypeSel&GameAbbre=" + gameabbre,
        beforeSend: function() {
        },
        success: function(data) {
            if (gameabbre == "") {
                $("#cardtype").html("");
                $("#cardtype").append("<option value=''>==请选择==</option>");
                return;
            }
            var dataObj = eval("(" + data + ")");
            if (dataObj.root.length > 0) {
                $("#cardtype").html("");
                $("#cardtype").append("<option value=''>==请选择==</option>");
                $.each(dataObj.root, function(idx, item) {
                    $("#cardtype").append("<option value='" + item.abbre + "'>" + item.name + "</option>");
                    if (idx == 0) { return true; }
                })
            }
        }
    });
}


function CodeUrlSel(Abbre) {
    if (Abbre == "") {
        $(window.parent.document).find("#xskhelp").attr("src", ""); 
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=CodeUrlSel&Abbre=" + Abbre,
        beforeSend: function() {
        },
        success: function(data) {            
            $(window.parent.document).find("#xskhelp").attr("src", data);
        }
    });
}

function PCodeUrlSel(Abbre) {
    if (Abbre == "") {
        $("#xskhelp").attr("src", "");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=CodeUrlSel&Abbre=" + Abbre,
        beforeSend: function() {
        },
        success: function(data) {
            $("#xskhelp").attr("src", data);
        }
    });
}

function CodeGet(ServerAbbre, CodeType) {
    if (ServerAbbre == "") {
        alert("请选择游戏服务器！");
        return;
    }
    else if(CodeType == ""){
        alert("请选择点卡类型！");
        return;
    }
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.aspx",
        data: "AjaxType=CodeTake&ServerAbbre=" + ServerAbbre + "&CodeType=" + CodeType,
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 1) {
                var html;
                var arr = data.split("|");
                if (arr[0] == "0") {
                    html = "您的激活码是：";
                }
                else {
                    html = "你已经领取你的激活码,您的激活码是：";
                }
                html = html + arr[1];
                $("#succtext").html(html);
            }
            else if (data == "1") {
                $("#errtxt").html("很抱歉，您要领取的卡已发放完毕");
                $("#lqhb_sb").show();
            }
            else if (data == "2") {
                $("#errtxt").html("很抱歉，领取失败!请从新领取!");
                $("#lqhb_sb").show();
            }
            else {
                $("#errtxt").html("很抱歉，您要领取的卡没有，请选择其他卡"+data);
                $("#lqhb_sb").show();
            }
        }
    });
}

function errtxthide() {
    $("#lqhb_sb").hide();
}
function Top20PaySel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=Top20PaySel",
        beforeSend: function() {
        },
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    $.each(dataObj.root, function(idx, item) {
                        $("#DayPayInfo").before("<p>" + item.num + ".充值账号： &nbsp;&nbsp;" + item.account + "；充值金额：&nbsp;&nbsp;" + item.price + "</p>");                        
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}
function ServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=jy",
        beforeSend: function() {
        },
        success: function(data) {
        var serverHtml = "<ul>";
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                $("#server-list").html("");

               
                var newindex = 0;
                $.each(dataObj.root, function(idx, item) {
                    var begintime = new Date(item.begintime.replace(/\-/g, "\/"));
                    var now = new Date();
                    if (begintime <= now) {
                        var gamename = "双线" + item.id + "区 " + item.servername;
                        var gameurl = "/GCenter/jy.aspx?gn=" + item.abbre;
                        var text = "<li class=\"hot\"><a href='javascript:;' onclick=\"SetGame('" + item.abbre + "');return false;\" > " + gamename + "</a><span>火爆</span></li>"
                        serverHtml += text;
                        //                        if (newindex == 0) {
                        //                            $("#newserver").html("<a href='javascript:;'  onclick=\"SetGame('" + item.abbre + "');return false\" id=\"last-time-server\">" + gamename + "</a>");
                        //                        }
                        //"<a href='" + gameurl + "' class='s_all' title='到武林 " + gamename + "'>" + gamename + "</a>";
                        //$("#allserver").append(text);
                        newindex++;
                    }
                   
                    if (idx == 0) { return true; }
                })
            }
            serverHtml += "</ul>";
            $("#server-list").html(serverHtml);
            
        }
    });
   
}

function ServerSel() {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerJsonSelByGame&Game=ahxx",
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    var num = 0;
                    var datanum = dataObj.root.length - 1;
                    $.each(dataObj.root, function(idx, item) {
                        var begintime = new Date(item.begintime.replace(/\-/g, "\/"));
                        var now = new Date();
                        if (begintime <= now) {
                            var sname = item.id + "服_" + item.servername;
                            if (num < 2) {
                                var tjtext = "<li class='busy'><a href='javascript:submitGame(\"" + item.abbre + "\");'>" + sname + "</a></li>";
                                $("#tjserver").append(tjtext);
                                $("#ahxxserver").append(tjtext);
                            }
                            else {
                                var text = "<li class='normal'><a href='javascript:submitGame(\"" + item.abbre + "\");'>" + sname + "</a></li>";
                                $("#ahxxsever").append(text);
                            }
                            num++;
                        }
                        if (idx == datanum) { $("#lastgame").val(num); return false;}
                    })
                }
            }
        }
    });
}

function ServerLoginSel(account) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.ashx",
        data: "AjaxType=ServerLoginLastSel&gameid=58&account=" + account,
        success: function(data) {
            if (data.length > 8) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    var num = 0;
                    $.each(dataObj.root, function(idx, item) {
                        if(num < 2) {
                            var abbre = item.abbre;
                            var servername = item.servername;
                            var text = "<li class='normal'><a href='javascript:submitGame(\"" + abbre + "\");'>" + servername + "</a></li>";
                            $("#zjdl").append(text);
                        }
                        num++;
                        if (idx == 0) { return true; }
                    })
                }
            }
        }
    });
}

function GameNumGo() {
    var gamenum = $("#number").val();
    var lastgame = $("#lastgame").val();
    var Reg = /^[0-9]+$/;
    if (!Reg.test(gamenum)) {
        alert("请填写游戏服号！必须为数字！");
        return false;
    }
    else if (gamenum > lastgame) {
        alert("游戏服不存在！请输入已经开服的服！");
        return false;
    }
    var game = "ahxx" + gamenum;
    submitGame(game);
}

function submitGame(game) {
    $.ajax({
        type: "POST",
        url: "/Services/Ajax.aspx",
        data: "AjaxType=ahxxCL&account=" + account + "&game=" + game,
        success: function(data) {
            if (data == '') {
                alert("登陆异常!");
            }
            else {
                location.href = 'client://loadgame|' + data;
            }
        }
    });
}
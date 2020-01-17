<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jycl.aspx.cs" Inherits="UserCenter.GCenter.jycl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="Keywords" content="剑影,剑影官网,剑影网页游戏,剑影攻略" />
<meta name="Description" content="剑影是一款即时战斗的角色扮演类网页游戏，游戏主要以封神为主线展开，主打双核心玩法。除秉承传统ARPG优秀玩法（即时玩法）之外,系统另外增加了一套离线玩法，规则简单易上手，但玩法不失研究性和可变性。" />
<title>剑影_剑影登录器_到武林剑影登录器</title>

    
<link rel="stylesheet" href="http://file.dao50.com/jycl1/css/client.css" type="text/css" />

<%--<link rel="stylesheet" href="http://file.dao50.com/jycl/css/jquery.jscrollpane.css" type="text/css" />--%>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/road.js"></script>
<style type="text/css">
.scroll-pane{width:100%;height:120px;overflow:auto;}

</style>
</head>
<body>
    
    
    
    <div class="main">
    <div class="serverList">
        <div class="user-info">
        	<div class="username-wrap">
                <span id="username">zhwx11</span>
            	欢迎您登陆，请选择服务器！
            	<a id="A1" href="../Services/userexit.aspx">[注销]</a>
            </div>
            <div class="quick">
                <a href="javascript:;" onclick="quickToGame()" id="quick-btn">进入</a>
                <input type="text" id="quick-input" maxlength="4" />
                <span>输入服数快速进入游戏</span>
            </div>
        </div>
        <div id="newserver" class="last-time">
        	<a href="javascript:;" id="last-time-server"  onclick="toGame()"></a>
        	<a href="javascript:;" onclick="toGame()" id="last-time-btn"></a>
        </div>
        <div class="server-nav">
            <span class="cur">全部服务器</span>
        </div>
        <input id="gamename" type="hidden" />
        <input id="maxServer" type="hidden" />
        <div id="server-list" class="allServer">
            <ul>
                <li class="hot">
                    <a href="javascript:;" target="_blank">重生2服 星月涅槃 5.23</a>
                </li>
               
            </ul>
        </div>
    </div>    
</div>
    
    
    
    
    
    
    
    
    
    
</body>
</html>

<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jycl.js"></script>
 
<script type="text/javascript">
    $(document).ready(function() {
        var account11 = '<%=sAccount %>';

        $("#username").html(account11);
        ServerSel();
        ServerSqLastSel(account);
    });
    var account = '<%=sAccount %>';
    function SetGame(game) {
        $("#gamename").val(game);
        submitGame();
    }
    function toGame() {
        var game = $('#gamename').val();
        SetGame(game);
    }
    function ServerSqLastSel(account) {
        $.ajax({
            type: "POST",
            url: "/Services/Ajax.ashx",
            data: "AjaxType=ServerLoginLastSel&gameid=53&account=" + account,
            beforeSend: function() {
            },
            success: function(data) {
                var dataObj = eval("(" + data + ")");
                if (dataObj.root.length > 0) {
                    $.each(dataObj.root, function(idx, item) {
                        if (item.servername == "") {
                            $("#last-time-server").html("无登陆记录");
                        }
                        else {
                            $("#last-time-server").html(item.servername);
                            $("#gamename").val(item.abbre);

                        }
                        if (idx == 0) { return true; }
                    })
                }
            }
        });
    }
    function quickToGame() {
        var maxid = $("#maxServer").val();
        var game = $('#quick-input').val();
        if (/^[0-9]*[1-9][0-9]*$/.test(game)) {
      
            if (maxid >= game) {
                $('#gamename').val('jy' + game);
                submitGame();
            } else {
            alert("所填服务器不存在！！");
            }
        } else {
        alert("请输入正整数！！");
        }
    }
    function submitGame() {
       
        var game = $('#gamename').val();
        $.ajax({
            type: "POST",
            url: "/Services/Ajax.ashx",
            data: "AjaxType=jyGameCL&account=" + account + "&game=" + game + "&pc=pc",
            beforeSend: function() {
            },
            success: function(data) {
                if (data == '') {
                    alert("登陆异常!");
                }
                else {
                    LoadGame(data);
                }
            }
        });
    }
   
    
</script>
<%=sMsg %>
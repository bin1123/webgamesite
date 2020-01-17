<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jtzscl.aspx.cs" Inherits="UserCenter.GCenter.jtscl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>dao50_惊天战神</title>
    <link href="<%=sWebUrl %>/wldFolder/jtzs/css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div class="logo"><span><img src="<%=sWebUrl %>/wldFolder/jtzs/images/logo.jpg" alt="惊天战神"/></span></div>
<div class="wrap">
	<div class="mainbody">
    	<div class="main">
        	<div class="left" id="nologin">
            	<div id="login" class="yhdl" >
                    <form id="loginform" name="loginform" action="" method="post">
                        <p class="yhdl_p">账　　号：<input type="text" title="请输入账号" style="color:#333" id="account" name="account" maxlength="16" class="txt" /></p>
                        <p class="yhdl_p">密　　码：<input type="password" name="pwdone" id="pwdone" title="请输入密码" maxlength="16" class="txt" /></p>
                        <p class="yhdl_tsp" id="loginerr"><%=sLoginMsg %></p>                        
                        <input type="hidden" value="login" name="Type"/>
                    </form>
                </div>
                <div  id="reg" class="yhzc" style="display:none" >
                    <form id="regform" name="regform" action="" method="post">
                        <p class="yhzc_p">账　　号：<input type="text" style="color:#333" title="请输入账号" onblur="CheckNameIn()" name="accountreg" id="accountreg" maxlength="16" class="txt" /></p>
                        <p class="yhzc_p">密　　码：<input name="pwdonereg" type="password" id="pwdonereg" title="请输入密码" maxlength="16" class="txt" /></p>
                        <p class="yhzc_p">确认密码：<input name="pwdtwo" type="password" id="pwdtwo" title="请再次输入密码" maxlength="16" class="txt" /></p>
                        <p class="yhzc_tsp" id="regerr"><%=sRegMsg %></p>                        
                        <input type="hidden" value="reg" name="Type"/>
                        <input type="hidden" id="RFlag" value="t" /> 
                    </form>
                </div>
                <div class="yhzc_btn">
                <input type="button" onclick="loginshow()" class="btn0" />
                <input type="button" onclick="regshow()" class="btn1" />
                </div>
            </div>
        	<div class="left" id="logined" style="display:none">            
                 <div class="location">
                   <span class="red">您好，欢迎进入到武林_惊天战神的世界！请选择您的服务器！ <a class="red" href="../Services/userexit.aspx">注销</a></span>                    
                 </div> <!--end location-->
                 <div class="serverslist-tit"><span><img src="<%=sWebUrl %>/wldFolder/jtzs/images/tit_tj.gif" alt="推荐服务器"/></span></div><!--end serverslist-tit-->
                 <div class="serverslist-list">
                     <ul>
                         <li>
                            <h3><a id="tjserver" class="red" href="http://game.dao50.com/GCenter/PlayGame.aspx?gn=jtzs1">1服_惊天动地</a></h3>                            
                         </li>               
                     </ul>
                 </div>                 
                 <div class="serverslist-tit"><span><img src="<%=sWebUrl %>/wldFolder/jtzs/images/tit_fw.gif" alt="服务器列表"/></span></div><!--end serverslist-tit-->                 
                 <div id="tabs-serverslist">
                      <ul class="tabs-serverslist-nav" id="tabsnav">
                          <li class="selectTag"><a onmouseover="selectTag('tabs-1',this)"  href="javascript:void(0)" >1-18服</a></li>
                      </ul>
                      <div class="tabs-serverslist-conts">
                          <div id="tabs-1" class="selectTag tabsconts-box">
                              <div class="serverslist-list">
                                 <ul id="games-1">
                                    <li>
                                        <h3><a href="http://game.dao50.com/GCenter/PlayGame.aspx?gn=jtzs1">1服_惊天动地</a></h3>                                        
                                     </li>                                
                                 </ul>
                             </div>
                          </div><!--end tabs-1-->
                      </div>
                 </div><!--end tabs-serverslist-->
             </div><!--end left-->         
             <div class="right">
             	<div class="serverslist-tit"><span><img src="<%=sWebUrl %>/wldFolder/jtzs/images/tit_xw.gif" alt="新闻活动"/></span></div><!--end serverslist-tit-->
                <div class="news">
                	<ul>
                    	<li><a href="http://www.dao50.com/news/201287/n50623963.html" target="_blank" title="">到武林1服“惊天动地”8月8日11:00火爆开启</a></li>
                    </ul>
                </div>
                <div class="focimg">
                	<div class="changephoto" id="focchange">
                        <a href="http://www.dao50.com/news/201287/n95163955.html" class="a_bigImg" target="_blank">
                          <img src="<%=sWebUrl %>/wldFolder/jtzs/images/foc/foc01.jpg" width="298" height="107"/>
                        </a> 
                    </div>
                </div>
                <div class="side-link">
                	<ul class="fl">
                    	<li><a id="gamebegin" href="#"><img src="<%=sWebUrl %>/wldFolder/jtzs/images/start.gif" alt="开始游戏"/></a></li>
                    </ul>
                    <ul class="fr">
                    	<li><a href="../Pay/" target="_blank"><img src="<%=sWebUrl %>/wldFolder/jtzs/images/cz.gif" alt="游戏充值"/></a></li>
                        <li><a href="http://www.dao50.com/yxzq/jtzs/" target="_blank"><img src="<%=sWebUrl %>/wldFolder/jtzs/images/gow.gif" alt="进入官网"/></a></li>
                        
                    </ul>
                </div>
             </div><!--end right-->            
        </div><!--end main-->        
    </div>  
    <div class="main_bbg"></div>
</div><!--end wrap-->
<div class="footer">
	<p>抵制不良游戏，拒绝盗版游戏。注意自我保护。谨防上当受骗。适度游戏益脑，沉迷游戏伤身。合理安排时间，享受健康生活。</p>
</div>
<input type="hidden" id="divtype" value="<%=sDivType %>"/>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript">
    var account = '<%=sAccount %>';
    function regshow() {
        var divtype = $("#divtype").val();
        if (divtype == 'l') {
            $('#reg').show();
            $('#login').hide();
            $("#divtype").val('r');
        }
        else {
            //注册验证,提交表单
            if (regVal()) {
                document.forms["regform"].submit();
            }
        }
    }

    function loginshow() {
        var divtype = $("#divtype").val();
        if (divtype == 'r') {
            $('#reg').hide();
            $('#login').show();
            $("#divtype").val('l');
        }
        else {
            //登陆验证,提交表单
            if (loginVal()) {
                document.forms["loginform"].submit();
            }
        }
    }

    function loginVal() {
        var username = $("#account").val();
        var password = $("#pwdone").val();
        if (username == "" || username == "请输入用户名") {
            $("#loginerr").html("请输入用户名");
            return false;
        }
        else if (password == "") {
            $("#loginerr").html("密码不能为空");
            return false;
        }
        else if (username.length < 4 || username.length > 16) {
            $("#loginerr").html("用户名格式不正确!");
            return false;
        }
        else if (password.length < 6 || password.length > 16) {
            $("#loginerr").html("密码格式不正确!");
            return false;
        }
        else {
            return true;
        }
    }

    function regVal() {
        var username = $("#accountreg").val();
        var password = $("#pwdonereg").val();
        var passwordtwo = $("#pwdtwo").val();
        if (username == "") {
            $("#regerr").html("请输入帐号");
            return false;
        }
        else if (password == "") {
            $("#regerr").html("密码不能为空");
            return false;
        }
        else if (passwordtwo == "") {
            $("#regerr").html("确认密码不能为空");
            return false;
        }
        else if (username.length < 4 || username.length > 16) {
            $("#regerr").html("用户名格式不正确");
            return false;
        }
        else if (password.length < 6 || password.length > 16) {
            $("#regerr").html("密码格式不正确");
            return false;
        }
        else if (password != passwordtwo) {
            $("#regerr").html("密码和确认密码不一致,请核对!");
            return false;
        }
        else {
            CheckNameIn();
            var flag = $("#RFlag").val();
            if (flag == "f") {
                return false;
            }
            else {
                return true;
            }
        }

    }

    //验证本站用户名是否存在
    function CheckNameIn() {
        var account = $("#accountreg").val();
        $.ajax({
            type: "POST",
            url: "/Services/Ajax.ashx",
            data: "AjaxType=ValName&Account=" + account,
            beforeSend: function() {
            },
            success: function(data) {
                if ("0" != data) {
                    $("#regerr").html("账号已被注册！请重新输入！");
                    $("#RFlag").val("f");
                }
                else {
                    $("#RFlag").val("t");
                }
            }
        });
    }

    $(document).ready(function() {
        if (account == '') {
            $('#nologin').show();
            $('#logined').hide();
        }
        else {
            GameOfJtzsServerSel();
            $('#nologin').hide();
            $('#logined').show();
        }
    });

    function selectTag(showContent, selfObj) {
        // 操作标签
        var tag = document.getElementById("tabsnav").getElementsByTagName("li");
        var taglength = tag.length;
        for (i = 0; i < taglength; i++) {
            tag[i].className = "";
        }
        selfObj.parentNode.className = "selectTag";
        // 操作内容
        for (i = 1; j = document.getElementById("tabs-" + i); i++) {
            j.style.display = "none";
        }
        document.getElementById(showContent).style.display = "block";
    }

    function GameOfJtzsServerSel() {
        $.ajax({
            type: "POST",
            url: "/Services/Ajax.ashx",
            data: "AjaxType=ServerSelByGame&Abbre=jtzs",
            beforeSend: function() {
            },
            success: function(data) {
                var dataObj = eval("(" + data + ")");
                var num = 1;
                if (dataObj.root.length > 0) {
                    var datanum = dataObj.root.length;
                    var bei = (datanum % 18) > 0 ? parseInt(datanum / 18) : parseInt((datanum / 18)) - 1;
                    if (bei > 0) {
                        for (i = 1; i < (bei + 1); i++) {
                            var begin = i * 18 + 1;
                            var end = begin + 17;
                            var pnum = i + 1;
                            var name = begin + '-' + end;
                            $("#tabsnav").append("<li><a onmouseover='selectTag(\"tabs-" + pnum + "\",this)' href='javascript:void(0)'>" + name + "</a></li>");
                            $("#tabs-" + i).after("<div id='tabs-" + pnum + "' class='tabsconts-box'><div class='serverslist-list'><ul id='games-" + pnum + "'></ul></div></div>");
                        }
                    }
                    $("#games-1").html("");
                    var num = 1;
                    $.each(dataObj.root, function(idx, item) {
                        var abbre = item.abbre;
                        var servernum;
                        if (datanum > 99) {
                            servernum = abbre.substr(abbre.length - 3, 3);
                        }
                        else if (datanum > 9) {
                            servernum = abbre.substr(abbre.length - 2, 2);
                        }
                        else {
                            servernum = abbre.substr(abbre.length - 1, 1);
                        }
                        datanum = datanum - 1;
                        var serverbei = (servernum % 18) > 0 ? parseInt((servernum / 18)) + 1 : parseInt((servernum / 18));
                        var servername = servernum + '服_' + item.servername;
                        var gameurl = "http://game.dao50.com/GCenter/PlayGame.aspx?gn=jtzs" + servernum;
                        if (num == 1) {
                            $("#tjserver").attr("href", gameurl);
                            $("#gamebegin").attr("href", gameurl);
                            $("#tjserver").text(servername);
                        }
                        $("#games-" + serverbei).append("<li><h3><a href='" + gameurl + "'>" + servername + "</a></h3></li>");
                        num++;
                        if (idx == 0) { return true; }
                    })
                }
            }
        });
    }
</script>
</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sqcl.aspx.cs" Inherits="UserCenter.GCenter.sqcl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>神曲登陆页面</title>
    <link rel="stylesheet" type="text/css" href="<%=sWebUrl %>/wldFolder/sq/css/normalize.css" />
	<link rel="stylesheet" type="text/css" href="<%=sWebUrl %>/wldFolder/sq/css/sq-client.css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.9.0.min.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/UserVal.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/road.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/Game.js"></script>
    <script type="text/javascript">
        var account = '<%=sAccount %>';

        function focusing(obj) {
            obj.value = '';obj.className = 'input-on';
        }

        function bluring(obj, txt,flag) {
            var val = obj.value;
            if (val == '') {
                obj.value = txt; obj.className = 'input2';
            }
            else { 
                if(val != txt && val.length > 4 && flag == "y") {
                    CheckNameIn(val);
                }
            }
        }

        function loginVal() {
            var username = $("#account").val();
            var password = $("#pwdone").val();
            if (username == "" || username == "请输入用户名") {
                alert("请输入用户名");
                return false;
            }
            else if (password == "") {
                alert("密码不能为空");
                return false;
            }
            else if (username.length < 4 || username.length > 16) {
                alert("用户名格式不正确!");
                return false;
            }
            else if (password.length < 6 || password.length > 16) {
                alert("密码格式不正确!");
                return false;
            }
            else {
                return true;
            }
        }

        function DivShowOn(id) {
            if (id == "tjgame") {
                $('#tjgame').attr('class', 'tab active');
                $('#tjdiv').attr('class', 'server-area');
                $('#allgame').attr('class', 'tab');
                $('#allgamediv').attr('class', 'server-area none');
            }
            else {
                $('#tjgame').attr('class', 'tab');
                $('#tjdiv').attr('class', 'server-area none');
                $('#allgame').attr('class', 'tab active');
                $('#allgamediv').attr('class', 'server-area');
            }
        }

        $(document).ready(function() {
            if (account == '') {
                $('#login').show();
                $('#logined').hide();
            }
            else {
                $('#login').hide();
                $('#logined').show();
                ServerSqLastSel(account); 
                GameOfSqServerSel()
            }
        });

        function SetGame(game) {
            $("#gamename").val(game);
            submitGame();
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
            var game = "sq" + gamenum;
            $("#gamename").val(game);
            submitGame();
        }
        
        function submitGame() {
            if (account == '') {
                $('#nologin').show();
                $('#logined').hide();
                return;
            }
            var game = $('#gamename').val();
            $.ajax({
                type: "POST",
                url: "/Services/Ajax.ashx",
                data: "AjaxType=sqGameCL&account=" + account + "&game="+game,
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
</head>
<body>
    <div id="login" class="doc sign-in clearfix">
        <div class="container">            
	    <div class="news clearfix">
                <div class="hd"></div>
                <div class="bd">
                    <ul class="news-list">                        
                    <li><a href="http://www.dao50.com/news/sq_xwgg/" target="_blank">到武林《神曲》新闻公告</a></li>
                    <li><a href="http://www.dao50.com/news/sq_hdzx/" target="_blank">到武林《神曲》精彩活动公告</a></li>
                    <li><a href="http://www.dao50.com/news/2012821/n58834178.html" target="_blank"> 百年恩爱双心结，千里姻缘一线牵</a></li>
                    <li><a href="http://www.dao50.com/news/2012821/n82964194.html" target="_blank"> 浓情七夕，浪漫来袭</a></li>
                    <li><a href="http://www.dao50.com/news/201287/n88523965.html" target="_blank">天降惊喜，好礼不断</a></li>
                    </ul>
                </div>
            </div>
            <div class="signin clearfix">
                <form id="form1" name="from1" onsubmit="return loginVal()" action="sqcl1.aspx" method="post">
                    <div class="signin-l">
                        <div class="field username-field">
                            <label class="title" for="username">用户名：</label>
                            <input name="account" id="account" tabindex="1" type="text" maxlength="16" />
                            <label class="inside" for="username">请输入用户名</label>
                        </div>
                        <div class="field password-field">
                            <label class="title" for="password">密码：</label>
                            <input name="pwdone" id="pwdone" tabindex="2" type="password" maxlength="16" />
                            <label class="inside" for="password">请输入密码</label>
                        </div>
                        <div class="remember-field">
                            <label for="remember" class="checkbox"></label>
                            <input type="checkbox" id="remember"/>
                            <a href="" class="text">记住帐号</a>
                        </div>    
                    </div>
                    <div class="signin-r">
                        <div class="submit-field clearfix">
                            <input type="submit" id="submit" tabindex="3" class="normal" value="" />
                            <a href="<%=sRootUrl %>/UCenter/reg.aspx" target="_blank"  class="fl">注册帐号</a>
                            <a href="<%=sRootUrl %>/UCenter/searchPass.aspx" target="_blank" class="fr">找回密码</a>
                        </div>
                    </div>
                    <div id="tips"></div>
		        <input type="hidden" value="login" name="Type"/>
                </form>
            </div>
        </div>
    </div>
    <div id="logined" class="doc server clearfix" style="display:none">
        <div class="container">
            <div class="welcome clearfix">
                <h1><em><%=sAccount %></em> 欢迎您登录，请选择服务器！<a href="/Services/userexit.aspx" class="logout">注销</a></h1>
                <p><span class="last"><a href="javascript:submitGame()" id="pserver" class="zc">1服_开天辟地</a></span> <a href="javascript:submitGame()" class="gamestart"></a></p>
            </div>
            <div class="servers">
                <div class="servers-area">
                    <ul class="tabs">
                        <li id="tjgame" onmouseover="DivShowOn('tjgame')" class="tab active"><a href="#">推荐服</a></li>
                        <li id="allgame" onmouseover="DivShowOn('allgame')" class="tab"><a href="#">所有服务器</a></li>
                    </ul>
                    <div class="quick-start clearfix">
                            <input type="text" id="number" maxlength="4"/>
                            <input type="button" id="button" onclick="GameNumGo()" />                        
                    </div>
                    <div id="tjdiv" class="server-area">
                        <div class="scrollbar"><div class="track"><div class="thumb"><div class="end"></div></div></div></div>
                        <div class="viewport">
                            <div class="overview">
                                <ul id="tjul" onmouseover="DivShowOn('tjgame')">
                                    <li class="hot"><a href="">1服_开天辟地</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div id="allgamediv" class="server-area none">
                        <div class="scrollbar" style="height: 200px;"><div class="track" style="height: 200px;"><div class="thumb" style="top: 0px; height: 27.378507871321013px;"><div class="end"></div></div></div></div>
                        <div class="viewport">
                             <div class="overview">
                                <ul id="allgameul" onmouseover="DivShowOn('allgame')">
                                    <li class="hot"><a href="">1服_开天辟地</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>  
                </div>
        </div>
    </div>
    <input type="hidden" id="gamename" value="sq1"/>
    <input type="hidden" id="lastgame" value=""/></div>
    <script src="<%=sRootUrl %>/JsFolder/jquery.tinyscrollbar.min.js"></script>
    <script>
        $(function() {
            // tabs
            $('.tab a').click(function(e) {
                e.preventDefault();
            })
            $('.tabs .tab').on('mouseenter', function() {
                var index = $(".tabs li").index(this);
                $(this).addClass("active").siblings().removeClass("active");

                $(".server-area").eq(index).show().siblings(".server-area").hide();
                // scrollbar
                $('.server-area').eq(index).tinyscrollbar({ size: 97, sizethumb: 30 });
            });

            // scrollbar
            $('.server-area').tinyscrollbar({ size: 97, sizethumb: 30 });
        })
    </script>           	
</body>
</html>
<%=sMsg %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sqserver.aspx.cs" Inherits="UserCenter.GCenter.sqserver" %>
<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="js ie ie6"> <![endif]-->
<!--[if IE 7]>         <html class="js ie ie7"> <![endif]-->
<!--[if IE 8]>         <html class="js ie ie8"> <![endif]-->
<!--[if IE 9]>         <html class="js ie ie9"> <![endif]-->
<!--[if gt IE 9]><!--> <html class="js"> <!--<![endif]-->
<head>
 	<meta charset="utf-8" />
  	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>神曲II登陆页面</title>
    <link rel="stylesheet" href="http://file.dao50.com/sq2cl/css/normalize.css?ver=1385716904216" type="text/css" />
    <link rel="stylesheet" href="http://file.dao50.com/sq2cl/css/sq-client.css?ver=1385716904216" />
    <link rel="stylesheet" href="http://file.dao50.com/sq2cl/css/jquery.jscrollpane.css?ver=1385716904216" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/UserVal.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/road.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/Game.js"></script>
    <script type="text/javascript">
        var account = '<%=sAccount %>';
        function DivShowOn(id) {
            if (id == "tjgame") {
                $('#tjgame').attr('class', 'tab active');
                $('#tjdiv').attr('class', 'server-area scroll-pane');
                $('#allgame').attr('class', 'tab');
                $('#allgamediv').attr('class', 'server-area scroll-pane none');
            }
            else {
                $('#tjgame').attr('class', 'tab');
                $('#tjdiv').attr('class', 'server-area scroll-pane none');
                $('#allgame').attr('class', 'tab active');
                $('#allgamediv').attr('class', 'server-area scroll-pane');
            }
        }

        $(document).ready(function() {
            if (account != '') {
                ServerSqLastSel(account);
                GameOfSqServerSel();
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
                data: "AjaxType=sqGameCL&account=" + account + "&game=" + game,
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
  <div class="doc server-page clearfix"><!-- Document start -->	
    <div class="container">
      <div class="welcome clearfix">
        <div class="hd">
          <h1 class="title"><em><%=sAccount %></em> 欢迎您登录，请选择服务器！<a href="/Services/userexit.aspx" class="logout">注销</a></h1>
        </div>
        <div class="bd">
          <div class="login-box"><span class="last-login"></span><span class="last"><a href="javascript:submitGame();" id="pserver">1服_开天辟地</a></span> <a href="javascript:submitGame();" class="gamestart"></a></div>
        </div>
      </div>
      <div class="servers">
        <div class="servers-area">
          <ul class="tabs">
                <li id="tjgame" onmouseover="DivShowOn('tjgame')" class="tab active">
                  <a title="推荐服务器" href="javascript:;">推荐服务器</a>
              </li>
                <li id="allgame" onmouseover="DivShowOn('allgame')" class="tab">
                  <a  title="所有服务器" href="javascript:;">所有服务器</a>
              </li>
          </ul>
          <div class="servers-box">
            <div class="quick-start clearfix">
                <label for="number">请输入服务器：</label>
                <input type="text" id="number" name="number" maxlength="3" value="" />
                <input type="submit" id="submit" name="submit" onclick="GameNumGo()" value=""/>
            </div>
            <div id="tjdiv" class="server-area scroll-pane">
              <ul id="tjul" onmouseover="DivShowOn('tjgame')"><li class="hot"><a href="javascript:;">1服_开天辟地</a></li></ul>
            </div>
            <div id="allgamediv" class="server-area scroll-pane none">
              <ul id="allgameul" onmouseover="DivShowOn('allgame')">
              <li class="hot"><a href="javascript:;">1服_开天辟地</a></li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>    
    <input type="hidden" id="gamename" value="sq1"/>
    <input type="hidden" id="lastgame" value="294"/>
  </div>
  <script src="http://file.dao50.com/sq2cl/js/jquery-1.7.1.js"></script>
  <script src="http://file.dao50.com/sq2cl/js/jquery.jscrollpane.min.js"></script>
  <script>
      $(function() {
          // tabs
          $('.tab a').click(function(e) {
              e.preventDefault();
          })
          $('.tabs .tab').on('mouseenter', function() {
              var index = $('.tabs li').index(this);
              $(this).addClass('active').siblings().removeClass('active');
              $('.server-area').eq(index).show().siblings('.server-area').hide();
              var api = jscroll('.scroll-pane').data('jsp');
              api.reinitialise();
          });

          // remove anchro outline
          $('a, input[type=submit]').on('focus', function() {
              $(this).blur();
          });

          // jscrollpane
          var jscroll = function(obj) {
              var settings = {
                  showArrows: true,
                  hideFocus: true,
                  verticalDragMinHeight: 30,
                  verticalDragMaxHeight: 30,
                  horizontalDragMinWidth: 30,
                  horizontalDragMaxWidth: 30
              };
              return $(obj).jScrollPane(settings);
          }
          jscroll('.scroll-pane');
      });
  </script>
<!-- Document end -->
<!--[if IE 6]>
<script type="text/javascript" src="http://file.dao50.com/sq2cl/js/DD_belatedPNG.js"></script>
<script type="text/javascript">
DD_belatedPNG.fix(".last-login, .tab a");
DD_belatedPNG.fix(".gamestart, #number, #submit");
</script>
<![endif]-->
</body>
</html>
<%=sMsg %>
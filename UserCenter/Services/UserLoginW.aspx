<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLoginW.aspx.cs" Inherits="UserCenter.Services.UserLoginW" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>武林道_登陆页</title>
    <link href="<%=sWebUrl %>/wldFolder/css/index_css.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/UserVal.js"></script>

    <script type="text/javascript">
        function AccountCheck() {
            var account = $("#account").val();
            if (account != "") {
                return true;
            }
            else {
                alert('账号不能为空！');
                return false;
            }
        }

        function PWDCheck() {
            var PassWordOne = $("#pwdone").val();
            if (PassWordOne != "") {
                return true;
            }
            else {
                alert('密码不能为空！');
                return false;
            }
        }

        function checkAll() {
            if (AccountCheck() && PWDCheck()) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="login" class="login" runat="server"><!--未登录状态-->
    <div class="login01"><a href="<%=sRootUrl %>/UCenter/reg.aspx" target="_blank"><img src="<%=sWebUrl %>/wldFolder/images/dl_btn.jpg" /></a></div>
    <div class="login02">
    <ul>
    <li class="tx_huang">欢迎您加入到武林游戏平台</li>
    <li class="dl_txtzh"><input type="text" id="account" name="account" maxlength="16" class="txt" /></li>
    <li class="dl_txtmm"><input type="password" id="pwdone" name="pwdone" maxlength="16" class="txt" /></li>
    <li class="dl_jzmm"><center><input type="checkbox" name="checkbox" id="checkbox" />记住密码<a href="../UCenter/searchPass.aspx" target="_blank">找回密码？</a></center></li>
    <li class="ljdl"><center><asp:ImageButton ID="userlogin" OnClientClick="return checkAll();" onclick="userlogin_Click" runat="server"/></center></li>
    </ul>
    </div>
    </div><!--未登录状态结束-->
    <div id="logined" class="login" runat="server"><!--登录状态开始-->
    <div class="login03">
    	<div class="dl_tx"><img src="<%=sWebUrl %>/wldFolder/images/dl_tx.jpg" alt="个人头像" /></div>
        <div class="dl_xx">
        <ul>
        <li class="dl_xx_txtlan">尊敬的：<%=sAccount %></li>
        <li class="dl_xx_txtblack">欢迎您来到到武林平台</li>
        <li class="dl_xx_wlb">您的武林币：<%=iPoints %></li>
        </ul>
        </div>
    </div>
    <div class="login04">
      <a href="http://www.dao50.com/yhzx/" target="_blank"><img src="<%=sWebUrl %>/wldFolder/images/dl_yhzx.jpg" /></a>
      <a href="http://www.dao50.com/yxzx/" target="_blank"><img src="<%=sWebUrl %>/wldFolder/images/dl_yxzx.jpg" /></a>
      <a href="http://game.dao50.com/pay/" target="_blank"><img src="<%=sWebUrl %>/wldFolder/images/dl_wycz.jpg" /></a>
      <asp:LinkButton ID="UserExit" OnClick="UserExit_Click" runat="server"><img src="<%=sWebUrl %>//wldFolder/images/dl_wytc.jpg"/></asp:LinkButton>
    </div>
  <div class="login05">
  <ul>
  <li class="login05_bx">最近玩过的游戏</li>
   <%for (int i = 0; i < iGameNum; i++)
      {%> 
        <li><a href="http://game.dao50.com/GCenter/PlayGame.aspx?gn=<%=sPGames[i].Split('-')[0] %>" target="_blank"><span class="yx_name"><font color="#FF0000"><%=sPGames[i].Split('-')[1] %></font></span>
        <span class="yx_fwq"><%=sPGames[i].Split('-')[2] %></span><span class="yx_num">双线<%=sPGames[i].Split('-')[3] %>服</span></a></li>
      <%} 
   %>
  </ul>
  </div>
</div><!--登录状态结束-->
</form>
</body>
<%=sMsg %>
</html>

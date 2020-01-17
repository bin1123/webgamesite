<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nslmcl.aspx.cs" Inherits="UserCenter.GCenter.nslmcl" %>
<!DOCTYPE HTML>
<html lang="zh-CN">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>女神联盟</title>
	<link href="http://file.dao50.com/nslmcl/css/ns_kh.css" rel="stylesheet" type="text/css" />
</head>
<body>

<div class="fuwuqi_d2">
 <div class="fwq_title">女神联盟—当前账号：<%=sAccount %> <a href="/Services/userexit.aspx" title="切换其他账号">切换其他账号</a></div>
 <div class="fwq_more">
  <div class="gundong">
   <!--推荐服务器开始-->
   <div class="fuw_dl">
    <h2 class="fw_ti">推荐服务器列表</h2>
    <div class="fuw_xz">
     <ul class="fuq_xzz">
      <li><a id="tjserver" target="_game" class="s4"></a></li>
     </ul>
     <div class="clear"></div>
    </div>
   </div>
   <!--推荐服务器结束-->
   <!--我登录过的服务器开始-->
   <div class="fuw_dl">
    <h2 class="fw_ti">我登录过的服务器</h2>
    <div class="fuw_xz">
     <ul class="fuq_xzz">
      <li><a id="loginedserver" target="_game" class="s3"></a></li>
     </ul>
     <div class="clear"></div>
    </div>
   </div>
   <!--我登录过的服务器结束-->
   <!--服务器列表开始-->
   <div class="fuw_dl">
    <h2 class="fw_ti">服务器列表</h2>
    <div class="fuw_xz">
     <ul id="allserver" class="fuq_xzz">
     </ul>
     <div class="clear"></div>
    </div>
   </div>
   <!--服务器列表结束-->
   <!--查看所有服务器开始-->
   <div class="fw_text">若服务器无法正常显示，请尝试切换帐号重新登录</div>
  </div>
 </div>
 <!--图标示意开始-->
 <div class="ser_sort"> <span class="sort1">允许激活服务区</span> <span class="sort2">您进过的服务区</span> <span class="sort3">停止激活服务区</span> <span class="sort4">停服维护服务区</span><span class="sort5"><a href="#">切换其他账号</a></span></div>
 <!--图标示意结束-->
</div>
</body>  
</html>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/nslmcl.js"></script>
<script type="text/javascript">
    $(document).ready(function() {
        var account = '<%=sAccount %>';
        if (account.length > 0) {
            ServerLastSel(account);
            ServerSel();
        }
    }); 
</script>
<%=sMsg %>
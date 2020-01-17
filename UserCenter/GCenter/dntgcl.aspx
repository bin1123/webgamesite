<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dntgcl.aspx.cs" Inherits="UserCenter.GCenter.dntgcl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>dao50_大闹天宫</title>
	<link href="http://file.dao50.com/dntgcl/css/main.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div class="dntg_fwq">
	  <div class="toplist">
	    <a class="link_btn_1" href="http://www.dao50.com/news/dntg_yxzl/" target="_blank" title="游戏资料">游戏资料</a>
        <a class="link_btn_2" href="http://www.dao50.com/news/dntg_yxgg/" target="_blank" title="新闻中心">新闻中心</a>
		<a class="link_btn_3" href="http://www.dao50.com/yxzq/dntg/" target="_blank" title="官网首页">官网首页</a>
		<a class="link_btn_4" href="http://www.dao50.com/news/dntg_yxhd/" target="_blank" title="活动公告">活动公告</a>
        <a class="link_btn_5" href="http://bbs.dao50.com/showforum-191.aspx" target="_blank" title="游戏论坛">游戏论坛</a>
      </div>
	  <div class="dntg_fwqlb">
	    <div class="fwqlbtop"></div>
	    <div class="fwqlbbot">
	       <div class="fwqlb00" id="allserver"> 
	         <a href="dntg.aspx?gn=dntg1" class="s_all">零零一服[双线]</a>
	       </div>
	    </div>
	  </div>
</div>
</body>  
</html>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/dntgcl.js"></script>
<script type="text/javascript">
    $(document).ready(function() {
      ServerSel();
    }); 
</script>
<%=sMsg %>
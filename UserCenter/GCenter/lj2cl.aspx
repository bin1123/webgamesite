<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lj2cl.aspx.cs" Inherits="UserCenter.GCenter.lj2cl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>龙将2游戏页</title>
    <link href="<%=sWebUrl %>/wldFolder/lj2/login.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
</head>
<body>
<div id="server_main">
	<div id="log_con">
		<div id="log_info">
			<div id="ico"><span id="ico_1">正常</span><span id="ico_3">期待</span><span id="ico_2">维护</span></div>
			<span style="color:#ffff00;"><%=sAccount %></span> 欢迎您登录，请选择服务器！ <a href="/Services/userexit.aspx" style="color:#FF3300">注销</a>
		</div>
		<div id="log_cont">
			<div id="reco_row">
				<div id="reco"></div>
				<ul id="zjdl">
					<li><a href="#" id="zjdlurl" class="s1">无登陆记录</a></li>
				</ul>
			</div>
			<div id="ingame_row">
				<div id="oth_link"></div>
				<a href="lj2.aspx?gn=ljer1" id="start">开始游戏</a>
			</div>
		</div>
	</div>
	<div id="server_content">
		<div id="xfdh">
			<p id="se_btn_1" class='on'>推荐服务器</p>
			<p id="se_btn_2"><a href="javascript:show('2',this);">全部服务器</a></p>
		</div>
		<div id="qb_list">
			<ul id="s_list_1" class="s_list"><!--s1：正常；s2：期待；s3：维护。-->
			</ul>
			<ul id='s_list_2' class='s_list' style='display:none'>
			</ul>
		</div>
	</div>
</div>
</body>
<script type="text/javascript" src="<%=sRootUrl %>/JsFolder/lj2cl.js"></script>
<script type="text/javascript">
    $(document).ready(function() {
        var account = '<%=sAccount %>';
        ServerLJ2LastSel(account);
        LJ2ServerSel();
    });
</script>
</html>

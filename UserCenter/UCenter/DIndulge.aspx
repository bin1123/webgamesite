<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DIndulge.aspx.cs" Inherits="UserCenter.UCenter.DIndulge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>到武林_防沉迷</title>    
    <link href="<%=sWebUrl %>/wldFolder/css/index_css.css" rel="stylesheet" type="text/css" />    
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>    
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/shenfen.js"></script>
    <script type="text/javascript">
        function AllCheck() {
            if (CredennumCheck() && NameCheck()) {
                document.getElementById("form1").submit();
                return true;
            }
            else {
                return false;
            }
        }

        function NameCheck() {
            var username = $('#UserName').val(); 
            if (username != "") {
                $("#unErr").html("");
                return true;
            }
            else {
                $("#unErr").html("姓名不能为空！");
                return false;
            }
        }
        
        function CredennumCheck() {
            var res = isIdCardNo('CredenNum');
            if (res != "") {
                $("#cnErr").html(res);
                return false;
            }
            else {
                $("#cnErr").html("");
                return true;
            }
        }
    </script>
</head>
<body>
<script type="text/javascript" src="http://game.dao50.com/inc/sitetop.aspx"></script>
<!--top结束-->
<div id="logo">
	<a href="#"><img src="<%=sWebUrl %>/wldFolder/images/logo_big.jpg" /></a>
    <a href="#"><img src="<%=sWebUrl %>/wldFolder/images/top_guanggao.jpg" /></a>
	</div>
<div id="wrap"><!--wrap开始-->
    <!--#include file="/inc/PageTopLink.htm"-->
    <!--nav结束-->
    <div id="content"><!--content开始-->
    <div id="mid_left"><!--左侧公共部分开始-->
	<div id="login_content">
        <iframe src="<%=sRootUrl %>/Services/Login.aspx" frameborder="0" width="194px" height="212px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div>
    <div id="mid_left02">
    <iframe src="<%=sWebUrl %>/news/czzxzxl/" frameborder="0" width="195px" height="365px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div>
    </div><!--左侧公共部分结束-->
    <div id="mid_right"><!--mid_right开始-->
   	<div id="content_left"><img src="<%=sWebUrl %>/wldFolder/images/content_bg_left.jpg" /></div>
    <div id="content_mid">
    <div id="content_mid01">
    <ul>
    <li>
    <span class="m01"><a href="<%=sWebUrl %>/yhzx/" target="_blank">用户中心</a> > 防沉迷</span>
    </li>
    </ul>
    </div>
    <div id="mid_right03"><!--防沉迷内容开始-->
    <form id="form1" name="form1" action="DIndulge.aspx" method="post">
    <p><span>本人姓名：</span>
    <span><input type="text" name="UserName" id="UserName" onblur="NameCheck()" maxlength="16" class="txt_yonghu"/><font id="unErr" color="red"></font></span></p>
    <p><span>身份证号：</span>
    <span><input type="text" name="CredenNum" id="CredenNum" maxlength="18" onblur="CredennumCheck()" class="txt_yonghu"/><font id="cnErr" color="red"></font></span></p>
    <p style="margin-left:220px; padding-top:40px;"><a href="#" onclick="AllCheck()"><img src="<%=sWebUrl %>/wldFolder/images/xiayibu.jpg" border="0" /></a></p>
    </form>
    </div><!--防沉迷内容结束-->
    </div>
    <div id="content_right"><img src="<%=sWebUrl %>/wldFolder/images/content_bg_right.jpg" /></div>
    </div><!--mid_right结束-->
    </div><!--mid结束-->
	<div id="foot_00"><!--foot开始-->
    <iframe src="<%=sWebUrl %>/foot.html" frameborder="0" width="824px" height="150px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div> <!--foot结束-->  
</div><!--wrap结束-->
</body>
</html>
<%=sMsg %>

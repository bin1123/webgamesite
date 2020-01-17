<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DayPaySel.aspx.cs" Inherits="UserCenter.Pay.DayPaySel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <link href="<%=sWebUrl %>/wldFolder/css/index_css.css" rel="stylesheet" type="text/css" />   
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/DayPaySel.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            Top20PaySel();
        });
    </script>
</head>
<body>
<script type="text/javascript" src="/Inc/SiteTop.aspx"></script>
<div id="logo">
        <a href="#">
            <img src="<%=sWebUrl %>/wldFolder/images/logo_big.jpg" /></a> <a href="#">
                <img src="<%=sWebUrl %>/wldFolder/images/top_guanggao.jpg" /></a>
	</div>
<div id="wrap"><!--wrap开始-->
        <!--#include file="/inc/PageTopLink.htm"-->        
    <div id="content"><!--content开始-->
	<div class="zzxy">
    <h3>到武林当天充值排行榜</h3>
    <p id="DayPayInfo">您本日充值金额：<%=dPrice %>（元）；  排名：第<%=iNum %> 位</p>    
    </div>
    </div><!--mid结束-->    
	<div id="foot_00"><!--foot开始-->
    <iframe src="<%=sWebUrl %>/foot.html" frameborder="0" width="824px" height="150px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div> <!--foot结束-->  
</div><!--wrap结束-->
</body>
</html>

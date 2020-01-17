﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayPSucc.aspx.cs" Inherits="UserCenter.Pay.PayPSucc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>平台币充值成功</title>    
    <link rel="stylesheet" type="text/css" href="<%=sWebUrl %>/wldFolder/css/index_css.css" />
</head>
<body>
<script type="text/javascript" src="<%=sRootUrl %>/Inc/SiteTop.aspx"></script>
<div id="logo">
	<a href="#"><img src="<%=sWebUrl %>/wldFolder/images/logo_big.jpg" /></a>
    <a href="#"><img src="<%=sWebUrl %>/wldFolder/images/top_guanggao.jpg" /></a>
	</div>
<div id="wrap"><!--wrap开始-->
    <!--#include file="/inc/PageTopLink.htm"-->
    <div id="content"><!--content开始-->
    <div id="mid_left"><!--左侧公共部分开始-->
	<div id="login_content">
    <iframe src="<%=sRootUrl %>/Services/Login.aspx" frameborder="0" width="194px" height="212px" marginheight="0" marginwidth="0" scrolling="no"></iframe>
    </div>
     <!--#include file="/inc/PageLeft.htm"-->
    </div><!--左侧公共部分结束-->
    <div id="mid_right"><!--mid_right开始-->
   	<div id="content_left"><img src="<%=sWebUrl %>/wldFolder/images/content_bg_left.jpg" /></div>
    <div id="content_mid">
    <div id="content_mid01">
    <ul>
    <li>
    <span class="m01"><a href="<%=sRootUrl %>/Pay/">充值中心</a> > 充值成功</span>
    </li>
    </ul>
    </div>
    <div id="mid_right03"><!--充值成功内容开始-->
    <div id="chongzhi06"><!--这里是成功和失败是背景图--></div>
    <div id="chongzhi07">
    <p>恭喜！平台币充值成功！</p>
    <p>成功充值：<span><%=iPayPoints %></span>武林币</p>
    <p>充值账户余额：<span><%=iUserPoints %></span>武林币</p>
    <p>订单号：<%=Request["TranID"] %></p>
    </div>
    <div id="chongzhi08">
    <a href="<%=sRootUrl %>/Pay/"><img src="<%=sWebUrl %>/wldFolder/images/fanhuiczsy.jpg" /></a>
    </div>
    </div><!--充值成功内容结束-->
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

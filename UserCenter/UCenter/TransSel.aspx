<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransSel.aspx.cs" Inherits="UserCenter.UCenter.TransSel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>到武林_交易信息查询</title>
    <link href="<%=sWebUrl %>/wldFolder/css/index_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            payinfoshow("rmbpaygame");
            rmbpaygame();
            rmbpaywlb();
            wlbpaygame();
        });

        function payinfoshow(objid) {
            switch(objid)
            {
                case "wlbpay":
                    $("#wlbpay").addClass("hui");
                    $("#wlbpayinfo").show();
                    $("#wlbpaygame").removeClass("hui");
                    $("#wlbpaygameinfo").hide();
                    $("#rmbpaygame").removeClass("hui");
                    $("#rmbpaygameinfo").hide();
                    break;
                case "wlbpaygame":
                    $("#wlbpay").removeClass("hui");
                    $("#wlbpayinfo").hide();
                    $("#wlbpaygame").addClass("hui");
                    $("#wlbpaygameinfo").show();
                    $("#rmbpaygame").removeClass("hui");
                    $("#rmbpaygameinfo").hide();
                    break;
                case "rmbpaygame":
                    $("#wlbpay").removeClass("hui");
                    $("#wlbpayinfo").hide();
                    $("#wlbpaygame").removeClass("hui");
                    $("#wlbpaygameinfo").hide();
                    $("#rmbpaygame").addClass("hui");
                    $("#rmbpaygameinfo").show();
                    break;
                    
            }
        }

        function rmbpaygame() {
            $.ajax({
                type: "POST",
                url: "/Services/Ajax.aspx",
                data: "AjaxType=rmbpaygame",
                beforeSend: function() {
                },
                success: function(data) {
                    if (data == "") {
                        return;
                    }
                    var dataObj = eval("(" + data + ")");
                    if (dataObj.root.length > 0) {
                        $.each(dataObj.root, function(idx, item) {
                            $("#rmbpaygamet").append("<tr><td class='xuxian' height='24' align='center'>" + item.time + "</td><td class='xuxian' align='center'>" + item.gamepoints + "</td><td class='xuxian' align='center'>" + item.channel + "</td><td class='xuxian' align='center'>" + item.game + "</td><td class='xuxian' align='center'>" + item.server + "</td></tr>");
                            if (idx == 0) { $("#rmbpaygameinfo").append("</table>"); return true; }
                        })
                        $("#rmbpaygamet").append("<tr><td height='1' colspan='5'></td></tr>");
                    }
                }
            });
        }

        function rmbpaywlb() {
            $.ajax({
                type: "POST",
                url: "/Services/Ajax.aspx",
                data: "AjaxType=rmbpaywlb",
                beforeSend: function() {
                },
                success: function(data) {
                    if (data == "") {
                        return;
                    }
                    var dataObj = eval("(" + data + ")");
                    if (dataObj.root.length > 0) {
                        $.each(dataObj.root, function(idx, item) {
                        $("#wlbpayt").append("<tr><td class='xuxian' height='24' align='center'>" + item.time + "</td><td class='xuxian' align='center'>" + item.price + "</td><td class='xuxian' align='center'>" + item.points + "</td><td class='xuxian' align='center'>" + item.name + "</td><td class='xuxian' align='center'></td></tr>");
                            if (idx == 0) { $("#wlbpayinfo").append("</table>"); return true; }
                        })
                        $("#wlbpayt").append("<tr><td height='1' colspan='5'></td></tr>");
                    }
                }
            });
        }

        function wlbpaygame() {
            $.ajax({
                type: "POST",
                url: "/Services/Ajax.aspx",
                data: "AjaxType=wlbpaygame",
                beforeSend: function() {
                },
                success: function(data) {
                    if (data == "") {
                        return;
                    }
                    var dataObj = eval("(" + data + ")");
                    if (dataObj.root.length > 0) {
                        $.each(dataObj.root, function(idx, item) {
                        $("#wlbpaygamet").append("<tr><td class='xuxian' height='24' align='center'>" + item.time + "</td><td class='xuxian' align='center'>" + item.gamepoints + "</td><td class='xuxian' align='center'>" + item.points + "</td><td class='xuxian' align='center'>" + item.gamename + "</td><td class='xuxian' align='center'>" + item.servername + "</td></tr>");
                            if (idx == 0) { $("#wlbpaygameinfo").append("</table>"); return true; }
                        })
                        $("#wlbpaygamet").append("<tr><td height='1' colspan='5'></td></tr>");
                    }
                }
            });
        }
    </script>
</head>
<body>
<script type="text/javascript" src="../inc/sitetop.aspx"></script><!--top结束-->
<div id="logo">
	<a href="#"><img src="<%=sWebUrl %>/wldFolder/images/logo_big.jpg" /></a>
    <a href="#"><img src="<%=sWebUrl %>/wldFolder/images/top_guanggao.jpg" /></a>
	</div>
<div id="wrap">
     <!--wrap开始-->
    <!--#include file="/inc/PageTopLink.htm"-->
    <!--nav结束-->
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
    <span class="m01"><a href="http://www.dao50.com/yhzx/" target="_blank">用户中心</a> > 充值查询</span>
    </li>
    </ul>
    </div>
    <!--充值查询内容开始--> 
    <div id="mid_right05">
                <div id="chongzhi04">
                账户充值信息查询
                </div>
                <div id="chongzhi05">
                	<div id="chongzhi0501">
                    <p id="rmbpaygame" onmouseover="payinfoshow('rmbpaygame')" class="hui"><a>充值游戏币查询</a></p>
                    <p id="wlbpay" onmouseover="payinfoshow('wlbpay')"><a>充值武林币查询</a></p>
                    <p id="wlbpaygame" onmouseover="payinfoshow('wlbpaygame')"><a>兑换游戏币查询</a></p>
                    </div>
                    <div id="wlbpayinfo" style="display:none;">
                      <table width="100%" border="0">
                      <tbody id="wlbpayt">
                        <tr class="hui">
                          <td width="24%" height="24" align="center">充值时间</td>
                          <td width="20%" align="center">消费人民币</td>
                          <td width="21%" align="center">获取武林币</td>
                          <td width="18%" align="center">充值方式</td>
                          <td width="17%" align="center">&nbsp;</td>
                        </tr>
                        </tbody>
                      </table>
                    </div>
                    <div id="wlbpaygameinfo" style="display:none;"><!--兑换游戏币查询-->
                      <table width="100%" border="0">
                      <tbody id="wlbpaygamet">
                        <tr class="hui">
                          <td width="24%" height="24" align="center">充值时间</td>
                          <td width="20%" height="24" align="center">获取游戏币</td>
                          <td width="21%" height="24" align="center">消费武林币</td>
                          <td width="18%" height="24" align="center">充值游戏</td>
                          <td width="17%" height="24" align="center">充值大区</td>
                        </tr>
                        </tbody>
                      </table>
                  </div>
                    <div id="rmbpaygameinfo"><!--充值游戏币查询-->
                      <table width="100%" border="0">
                        <tbody id="rmbpaygamet">
                        <tr>
                          <td width="24%" height="24" align="center">充值时间</td>
                          <td width="20%" height="24" align="center">获取游戏币</td>
                          <td width="21%" height="24" align="center">充值渠道</td>
                          <td width="18%" height="24" align="center">充值游戏</td>
                          <td width="17%" height="24" align="center">充值大区</td>
                        </tr>
                        </tbody>
                      </table>
                  </div>
                </div>
                </div> 
    <!--充值查询内容结束-->
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


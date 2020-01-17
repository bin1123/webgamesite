<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="by.aspx.cs" Inherits="UserCenter.GCenter.by" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>霸域服务器列表</title>
    <link href="http://file.dao50.com/byl/style/css.css" rel="stylesheet" type="text/css" />  
</head>
<!--[if IE 6]>
<script type="text/javascript" src="http://file.dao50.com/byl/js/DD_belatedPNG_0.0.8a-min.js"></script>
<script type="text/javascript">
window.attachEvent("onload", function(){
   DD_belatedPNG.fix("div, a ,em");
});
</script>
<![endif]-->
<body>
  <div class="ser_bg">
       <div class="new_ser">
       	 <p class="s_title"><span class="yellow">尊敬的<%=sAccount %></span>&nbsp;欢迎您登陆，请选择服务器！ <a href="../Services/userexit.aspx" class="l_out">注销</a></p>            
            <div class="new_server">
             <em class="ti_9 fl">最新游戏</em>
             <div class="ser_s fl">
                 <ul>
                   <li><a id="zxyx" href="#" target="_blank"><i class="state">最新</i></a></li>
                </ul>
             </div>
             <div class="clear"></div>
            </div>
            <div class="near_server">
             <em class="ti_9 fl">最近登录</em>
             <div id="lastplay" class="ser_s fl">
                 <ul>
                  <li><a class="s1" id="zjdlurl" href="#" target="_blank">1服天地初开</a></li>
                 </ul>
             </div>
             <div class="clear"></div>
            </div>
         </div>
       <div class="server">
            <ul class="server_tit">
                <li class="on"><a href="javascript:void(0);">推荐服务器</a></li>
                <li><a href="javascript:void(0)">1-30服</a></li>
           </ul>
      <script type="text/javascript" src="http://file.dao50.com/byl/js/jsScroller.js"></script>
	  <script type="text/javascript" src="http://file.dao50.com/byl/js/jsScrollbar.js"></script>  
        <div class="server_list clear">
        	 <div id="Scrollbar-Container">
                <img src="http://file.dao50.com/byl/images/up_arrow.png" class="Scrollbar-Up"  />
                <img src="http://file.dao50.com/byl/images/down_arrow.png" class="Scrollbar-Down" />
                <div class="Scrollbar-Track">
                   <div class="Scrollbar-Handle"></div>
                </div>
             </div>
            	<div id="Scroller-1">
        		<div class="sList Scroller-Container clear" id="serverlist">
                     <ul id="tjfw"><!--s1表示火爆，s2表示顺畅，s3表示维护，游戏链接要新窗口打开--> 
                    </ul>
					 <ul id="onefw" style="display:none;">     
                    </ul>
                </div>
             <div class="clear"></div>
          </div>
        </div>
    </div>
  </div>
</body>
<script type="text/javascript" src="http://file.dao50.com/byl/js/jquery.js"></script>
<script type="text/javascript">
    function scroll() {
        var scroller = null;
        var scrollbar = null;
        scroller = new jsScroller(document.getElementById("Scroller-1"), 450, 110);
        scrollbar = new jsScrollbar(document.getElementById("Scrollbar-Container"), scroller, false);
    }

    $(document).ready(function() {
        scroll();  //页面加载完成要运行这个函数
        $("#serverlist").height(28*13+39*5);   //为了兼容各个浏览器，请设置一下id=serverlist的div高度要比实际高度高5行，这里的一行高度为39px

        $(".server_tit li").mouseover(function() {
            var i = $(".server_tit li").index(this);
            $(".server_tit li").removeClass('on');
            $(".server_tit li").eq(i).addClass('on');
            $("#serverlist ul").hide().eq(i).show();
            scroll();
        });
    });
</script>
<script type="text/javascript">
    $(document).ready(function() {
        var account = '<%=sAccount %>';
        ServerLastSel(account);
        ServerSel();
    });

    function ServerSel() {
        $.ajax({
            type: "POST",
            url: "/Services/Ajax.ashx",
            data: "AjaxType=ServerJsonSelByGame&Game=by",
            beforeSend: function() {
            },
            success: function(data) {
                if (data.length > 8) {
                    var dataObj = eval("(" + data + ")");
                    var datalen = dataObj.root.length;
                    if (datalen > 0) {
                        var num = 1;
                        $.each(dataObj.root, function(idx, item) {
                            var text = "<li class='mg6'><a class='s1' href='by.aspx?gn=" + item.abbre + "' target='_blank'>" + item.id + "服_" + item.servername + "</a></li>";
                            var begintime = new Date(item.begintime.replace(/\-/g, "\/")); 
                            var now = new Date();
                            if (begintime <= now) {
                                if (num < 31) {
                                    if (num < 10) {
                                        if (num == 1) {
                                            $("#zxyx").html(item.id + "服_" + item.servername);
                                            $("#zxyx").attr("href", "by.aspx?gn=" + item.abbre);
                                        }
                                        $("#tjfw").append(text);
                                        $("#onefw").append(text);
                                    }
                                    else {
                                        $("#onefw").append(text);
                                    }
                                }
                                num++;
                            }
                            if (idx == 0) { return true; }
                        })
                    }
                }
            }
        });
    }

    function ServerLastSel(account) {
        $.ajax({
            type: "POST",
            url: "/Services/Ajax.ashx",
            data: "AjaxType=ServerLoginLastSel&gameid=26&account=" + account,
            beforeSend: function() {
            },
            success: function(data) {
                if (data.length > 8) {
                    var dataObj = eval("(" + data + ")");
                    if (dataObj.root.length > 0) {
                        $.each(dataObj.root, function(idx, item) {
                            var abbre = item.abbre;
                            var servername = item.servername;
                            if (abbre == "") {
                                $("#zjdlurl").html("你未登陆游戏");
                                return;
                            }
                            $("#zjdlurl").attr("href", "by.aspx?gn=" + abbre);
                            $("#zjdlurl").html(servername);
                            if (idx == 0) { return true; }
                        })
                    }
                }
            }
        });
    }
</script>
</html>
<%=sMsg %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lj2Login.aspx.cs" Inherits="UserCenter.GCenter.lj2Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>龙将2登陆页</title>
    <link href="<%=sWebUrl %>/wldFolder/lj2/login.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
</head>
<body>
<div class="main">
	<div class="news">
		<div class="lbpic"><img src="<%=sWebUrl %>/wldFolder/lj2/222949_KCpkVR.jpg" alt=""/></div>
		<div class="list">
			<ul id="dlgg">
            </ul>
		</div>
	</div>
	<div class="login">
	    <form method="post" action="lj2Login.aspx" onsubmit="return loginVal()" id="form1">
		    <div class="login_c">
			    <div class="id">
				    <input name="username" type="text" maxlength="16" class="input_id" id="username" />
			    </div>
			    <div class="pass">
				    <input name="pwd" type="password" maxlength="16" class="input_pwd" id="pwd"/>
			    </div>
		    </div>
		    <div class="login_r" align="center">
			    <input type="submit" value="" id="tolog">
		    </div>
		    <div id="info"></div>
		</form>
	</div>
</div>
</body>
<script type="text/javascript">
    function loginVal() {
        var username = $("#username").val();
        var password = $("#pwd").val();
        if (username == "" || username == "请输入用户名") {
            alert("请输入用户名");
            return false;
        }
        else if (password == "") {
            alert("密码不能为空");
            return false;
        }
        else if (username.length < 4 || username.length > 16) {
            alert("用户名格式不正确!");
            return false;
        }
        else if (password.length < 6 || password.length > 16) {
            alert("密码格式不正确!");
            return false;
        }
        else {
            return true;
        }
    }

    function HelpClassLJ2Sel() {
        $.ajax({
            type: "POST",
            url: "/Services/Ajax.ashx",
            data: "AjaxType=HelpClassLJ2DLGGSel&ClassID=396506452660",
            beforeSend: function() {
            },
            success: function(data) {
                if (data.length > 8) {
                    var dataObj = eval("(" + data + ")");
                    if (dataObj.root.length > 0) {
                        $.each(dataObj.root, function(idx, item) {
                            var sTitle;
                            if (item.title.length > 18) {
                                sTitle = item.title;
                                sTitle = sTitle.substr(0, 18);
                            }
                            else {
                                sTitle = item.title;
                            }
                            $("#dlgg").append("<li><a target='_blank' href='" + item.url + "' title='" + item.title + "'><font color='#ffff00'>" + sTitle + "</font></a></li>");                                                        
                            if (idx == 0) { return true; }
                        })
                    }
                }
            }
        });
    }

    $(document).ready(function() {
        HelpClassLJ2Sel();
    });
</script>
</html>
<%=sMsg %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pPayErr.aspx.cs" Inherits="UserCenter.Pay.pPayErr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="<%=sWebUrl %>/wldFolder/css/cz_css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div class="mid_right03"><!--充值成功内容开始-->
        <div class="chongzhi09"><!--这里是成功和失败是背景图--></div>
        <div class="chongzhi07">
            <p></p>
            <p><%=sErrText %></p>
            <p></p>
            <p></p>
        </div>
    </div>
</body>
</html>

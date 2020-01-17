<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="g_left_nav_l.aspx.cs" Inherits="UserCenter.frame.g_left_nav_l" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="<%=sWebUrl %>/wldFolder/ljzl/PlayGame_lj_left.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>   
    <script type ="text/javascript" src="<%=sRootUrl %>/JsFolder/Game.js"></script>
    <script type="text/javascript">
            function idshow(id) {
                $("#" + id).toggle();
                switch(id)
                {
                    case "xszn_lj":
                        $("#xszn_lj_dt").addClass("curr");
                        $("#gsjj_lj_dt").removeClass("curr");
                        $("#xwgg_lj_dt").removeClass("curr");
                        $("#wjwx_lj_dt").removeClass("curr");
                        $("#jchd_lj_dt").removeClass("curr");
                        $("#kfzx_lj_dt").removeClass("curr"); 
                        break;
                    case "gsjj_lj":
                        $("#xszn_lj_dt").removeClass("curr");
                        $("#gsjj_lj_dt").addClass("curr");
                        $("#xwgg_lj_dt").removeClass("curr");
                        $("#wjwx_lj_dt").removeClass("curr");
                        $("#jchd_lj_dt").removeClass("curr");
                        $("#kfzx_lj_dt").removeClass("curr");
                        break;
                    case "xwgg_lj":
                        $("#xszn_lj_dt").removeClass("curr");
                        $("#gsjj_lj_dt").removeClass("curr");
                        $("#xwgg_lj_dt").addClass("curr");
                        $("#wjwx_lj_dt").removeClass("curr");
                        $("#jchd_lj_dt").removeClass("curr");
                        $("#kfzx_lj_dt").removeClass("curr");
                        break;
                    case "wjwx_lj":
                        $("#xszn_lj_dt").removeClass("curr");
                        $("#gsjj_lj_dt").removeClass("curr");
                        $("#xwgg_lj_dt").removeClass("curr");
                        $("#wjwx_lj_dt").addClass("curr");
                        $("#jchd_lj_dt").removeClass("curr");
                        $("#kfzx_lj_dt").removeClass("curr");
                        break;
                    case "jchd_lj":
                        $("#xszn_lj_dt").removeClass("curr");
                        $("#gsjj_lj_dt").removeClass("curr");
                        $("#xwgg_lj_dt").removeClass("curr");
                        $("#wjwx_lj_dt").removeClass("curr");
                        $("#jchd_lj_dt").addClass("curr");
                        $("#kfzx_lj_dt").removeClass("curr");
                        break;
                    case "kfzx_lj":
                        $("#xszn_lj_dt").removeClass("curr");
                        $("#gsjj_lj_dt").removeClass("curr");
                        $("#xwgg_lj_dt").removeClass("curr");
                        $("#wjwx_lj_dt").removeClass("curr");
                        $("#jchd_lj_dt").removeClass("curr");
                        $("#kfzx_lj_dt").addClass("curr");
                        break;
                }
            }

            $(document).ready(function() {
                var gameabbre = 'lj1';
                HelpClassLJSel(gameabbre, 'xszn', 'xszn_lj');
                HelpClassLJSel(gameabbre, 'gsjj', 'gsjj_lj');
                HelpClassLJSel(gameabbre, 'xwgg', 'xwgg_lj');
                HelpClassLJSel(gameabbre, 'wjwx', 'wjwx_lj');
                HelpClassLJSel(gameabbre, 'jchd', 'jchd_lj');
            });
    </script>
</head>
<body>
    <div id="left_lj" class="leftbar f_l">
        <div id="ljgamelist" class="game_list">
            <div class="game_list_bg">
                <a href="http://www.dao50.com/yxzq/lj/" target="_blank" class="logo" title="龙将官方网站">龙将官方网站</a>
                <!--游戏官网/游戏论坛/游戏充值-->
                <div class="xl_btn clearfix">
                    <a class="czzx" target="_blank" href="http://game.dao50.com/pay/">充值中心</a> 
                    <a class="vip" href="http://www.dao50.com/news/20111215/n3364826.html" target="_blank" title="VIP介绍">VIP介绍</a> 
                    <a class="receive" href="http://game.dao50.com/xsk/" target="_blank" title="领取新手礼包">领取新手礼包</a>
                </div>
                <dl>
                    <dt id="xszn_lj_dt" onclick="idshow('xszn_lj')"><a>【新手指南】</a> </dt>
                    <dd style="display: none;" id="xszn_lj">
                    </dd>
                    <dt id="gsjj_lj_dt" onclick="idshow('gsjj_lj')"><a>【高手进阶】</a> </dt>
                    <dd style="display: none;" id="gsjj_lj">
                    </dd>
                    <dt id="xwgg_lj_dt" onclick="idshow('xwgg_lj')"><a>【新闻公告】</a> </dt>
                    <dd style="display: none;" id="xwgg_lj">
                    </dd>
                    <dt id="wjwx_lj_dt" onclick="idshow('wjwx_lj')"><a>【玩家文选】</a> </dt>
                    <dd style="display: none;" id="wjwx_lj">
                    </dd>
                    <dt id="jchd_lj_dt" class="curr" onclick="idshow('jchd_lj')"><a>【精彩活动】</a> </dt>
                    <dd id="jchd_lj">
                    </dd>
                    <!--客服中心-->
                    <dt id="kfzx_lj_dt"><span onclick="idshow('kfzx_lj')" class="kfzx"><a>【客服中心】</a></span> </dt>
                    <dd id="kfzx_lj" class="service" style="display: none;">
                        <ul class="serul">
                            <li>客服热线<br>
                                <span>400 008 5267</span><br />
                                <a title="到武林客服" target="_blank" href="tencent://message/?uin=2460929679">
                                    <img border="0" title="点击这里咨询客服2460929679" alt="点击这里咨询客服2460929679" src="http://wpa.qq.com/pa?p=1:2460929679:17">
                                    客服qq:2460929679</a><br />
                                <a title="到武林客服" target="_blank" href="tencent://message/?uin=2361245014">
                                    <img border="0" title="点击这里咨询客服2361245014" alt="点击这里咨询客服2460929679" src="http://wpa.qq.com/pa?p=1:2361245014:17">
                                    客服qq:2361245014</a><br />
                                <a title="到武林客服" target="_blank" href="tencent://message/?uin=2357752837">
                                    <img border="0" title="点击这里咨询客服2357752837" alt="点击这里咨询客服2460929679" src="http://wpa.qq.com/pa?p=1:2357752837:17">
                                    客服qq:2357752837</a> </li>
                            <li>玩家交流群</li>
                            <li>182634959</li>
                        </ul>
                    </dd>
                </dl>
                <!--游戏官网/游戏论坛/游戏充值-->
                <div class="xl_btn xl_btn_other clearfix">
                    <a class="bbs" href="http://bbs.dao50.com/showforum-64.aspx" target="_blank"
                        title="游戏论坛">游戏论坛</a>
                </div>
            </div>
        </div>
    </div>
</body>
</html>

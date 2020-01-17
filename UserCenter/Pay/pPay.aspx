<%@ Page Language="C#" Buffer="true" AutoEventWireup="true" CodeBehind="pPay.aspx.cs" Inherits="UserCenter.Pay.pPay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>充值页</title>
    <link href="<%=sWebUrl %>/wldFolder/css/cz_css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/Common.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/Tran.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/Game.js"></script>
    <script type="text/javascript" src="<%=sRootUrl %>/JsFolder/PayDefault.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            GameAllSel();
        });
    </script>
</head>
<body>
    <div class="cz_right"><!--充值成功内容开始-->
    <form id="form1">
    <div class="chongzhi01"><!--chongzhi01开始-->
                       <div class="chongzhi02" ><!--兑换平台币开始-->
                        <a href="javascript:void(0)" onclick="showp('GamePay')"><img src="<%=sWebUrl %>/wldFolder/images/duihuanptb.jpg" border="0" /></a>
                      </div>
                      <div id="wlbpay" class="chongzhi02"><!--游戏充值开始-->
                        <a href="javascript:void(0)" onclick="showp('pay')"><img src="<%=sWebUrl %>/wldFolder/images/youxichongzhi.jpg" border="0" /></a>
                      </div>
                      <div id="payinfos" class="chongzhi03">
                        <div class="chongzhi03_left">
                          <ul>
                          <li id="ibank" class="cz_bg2"><a href="javascript:void(0)" onclick="PayChange('ibank')">网上银行支付</a></li>
                          <li id="alipay" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('alipay')">支付宝支付</a></li>
                          <li id="szfphone" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('szfphone')">手机充值卡支付</a></li>
                          <li id="szfbank" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('szfbank')">无网银银行卡支付</a></li>
                          <li id="tenpay" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('tenpay')">财付通支付</a></li>
                          <li id="yp-bank" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('yp-bank')">易宝网上银行支付</a></li>
                          <li id="vpay" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('vpay')">全国固话支付</a></li>
                          <li id="yp-zt" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('yp-zt')">征途卡支付</a></li>
                          <li id="yp-sd" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('yp-sd')">盛大卡支付</a></li>
                          <li id="yp-jcard" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('yp-jcard')">骏网一卡通支付</a></li>
                          <li id="yp-szx" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('yp-szx')">神州行卡支付</a></li>
                          <li id="yp-dx" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('yp-dx')">电信卡支付</a></li>
                          <li id="yp-lt" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('yp-lt')">联通卡支付</a></li>
                          <li id="gnyhhk" style="display:none" class="cz_bg1"><a href="javascript:void(0)" onclick="PayChange('gnyhhk')">国内银行汇款</a></li>
                          </ul>
                        </div>
                        <div class="chongzhi03_right" id="chongzhi03_right">
                          <div class="chongzhi03_right03">
                            <span><strong>您当前选择的是<font id="paytype" color="#CC0000";>“网银”</font>支付方式</strong></span>
                            <span id="pinfo">只要您开通网上银行服务，足不出户即可实现快捷准确的帐号充值。请勿在此通道中使用其他方式充值。</span>
                            </div>
                             <!--支付帮助暂时隐藏<div id="bangzhu" style="display:none"><img  /></div>-->
                        <div class="chongzhi03_right04" id="chongzhi03_right04">
                          <ul class="cz_01">
                          <li><img src="<%=sWebUrl %>/wldFolder/images/cz_bz1.jpg" /></li>
                          <li><font color="#FF0000">*</font>充值账号：<input type="text" id="paccountone" style="width:150px;" value="<%=sUserName %>" /></li>
                          <li><font color="#FF0000">*</font>确认账号：<input type="text" id="paccounttwo" style="width:150px;" onblur="AccountCheck()" value="" /><span id="accounterr" style="color:#FF3300"></span></li>
                          <li>&nbsp;手机号码：<input type="text" maxlength="11" id="pphonenum" style="width:150px;" /></li>
                          <li class="f19">建议填写您当前使用的手机号码，以便在遇到问题时我们能及时与您沟通。</li>
                          </ul>
                          <ul class="cz_01">
                          <li><img src="<%=sWebUrl %>/wldFolder/images/cz_bz2.jpg" /></li>
                          <li style="display:none">
                            <input type="radio" name="payto" onclick="payshow('game')" id="qpaytogame" checked="checked" value="qpaytogame" />充值到游戏
                            <input type="radio" name="payto" onclick="payshowp('pt')" id="paytopt" value="paytopt" />充值到平台<font color="#FF0000">（平台币）</font>
                          </li>
                          <span id="paytogameinfo">
                          <li>充值到游戏：
                              <select id="gamename" onchange="ServerSelOfGame('servername',this.value,'==请选择==');SetGamePointsP();QuickPayVal()" style="width:150px;z-index:-1;">
                              <option value="">==请选择==</option>
                              </select><span id="gamenameerr" style="color:#FF3300"></span>
                          </li>
                          <li>选择服务器：
                              <select id="servername" onchange="QuickPayVal()" style="width:150px;z-index:-1;">
                              <option value="">==请选择==</option>
                              </select><span id="servernameerr" style="color:#FF3300"></span>
                          </li>                          
                          <li id="qucikroleinfos" style="display:none">选 择角 色：
                              <select id="quickrole" onchange="QuickRoleVal()" style="width:150px;z-index:-1;">
                              <option value="">==请选择==</option>
                              </select><span id="quickroleerr" style="color:#FF3300"></span>
                          </li>
                          <li class="f19">请玩家在充值前，务必确认该账号已在对应的游戏服务器里创建了游戏角色。</li>
                          </span>
                          </ul>
                          <ul class="cz_01">
                          <li><img src="<%=sWebUrl %>/wldFolder/images/cz_bz3.jpg" /></li>
                          <li>充值金额：
                              <select id="payprice" onchange="SetGamePointsP()" style="width:150px;">
				                    <option value="10">10元</option>
                                    <option value="20">20元</option>
                                    <option value="30">30元</option>
                                    <option value="50">50元</option>
                                    <option value="100">100元</option>
                                    <option value="200">200元</option>
                                    <option value="300">300元</option>
                                    <option value="500">500元</option>
                                    <option value="1000">1000元</option>
                                    <option value="2000">2000元</option>
                                    <option value="5000">5000元</option>
                                    <option value="10000">10000元</option>
                              </select> 
                              您可获得：<font id="gamepoints" color="#FF0000">100元宝</font>
                          </li>
                          </ul>
                          <ul id="szfcardtype" style="display:none" class="cz_01">
                            <li>充值卡类型:
                                <input type="radio" name="cardtype" id="yd" checked="checked" value="0" />移动
                                <input type="radio" name="cardtype" id="lt" value="1" />联通
                                <input type="radio" name="cardtype" id="dx" value="2" />电信
                            </li>
                          </ul>
                            <ul id="bank1" class="cz_02">
                          <li><input type="radio" name="bank" checked="checked" value="ICBCB2C" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_icbc.jpg" />
                          </li>
                          <li><input type="radio" name="bank" value="CMB" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_zhaoshang.jpg" />
                          </li>
                          <li><input type="radio" name="bank" value="BOCB2C" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_cb.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="ABC" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_nongye.jpg" />
                          </li>
                          <li><input type="radio" name="bank" value="CCB" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_jianshe.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="COMM" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_jiaotong.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="GDB" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_guangfa.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="SDB" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_shenfa.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="CMBC" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_minsheng.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="PSBC-DEBIT" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_youzheng.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="CIB" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_xingye.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="BJBANK" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_beijing.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="SPABANK" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_pingan.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="SPDB" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_pufa.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="CEBBANK" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_guangda.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="SHBANK" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_shanghai.jpg"/>
                          </li>
                          <li><input type="radio" name="bank" value="CITIC" />
                          <img src="<%=sWebUrl %>/wldFolder/images/bank_ccb.jpg"/>
                          </li>
                          </ul>
                          <ul id="bank2" class="cz_03">
							<li>
								<input name="bank" type="radio" value="BJRCB" />北京农商行 
							</li>
							<li>
								<input name="bank" type="radio" value="HZCBB2C" />杭州银行
							</li>
							<li>
								<input name="bank" type="radio" value="FDB" />富滇银行
							</li>
							<li>
								<input name="bank" type="radio" value="NBBANK" />宁波银行
							</li>
                           </ul>  
                        </div>
                        <div class="chongzhi03_right01">
                        <center><img alt="" onclick="openWindowp('350','250');" src="<%=sWebUrl %>/wldFolder/images/querenzhongzhi.jpg" border="0" /></center>
                        </div>
                        </div>
                        <div id="gnyhinfo" style="display:none" class="chongzhi12">
                           <p><img src="<%=sWebUrl %>/wldFolder/images/lxfs.jpg" /></p>
                       <p>客服QQ：1404126024 &nbsp;&nbsp;客服电话：400 700 1700（9：00-19：00）</p><br />
                       <p><img src="<%=sWebUrl %>/wldFolder/images/czlc.jpg" /></p>
                       <p>A、官方充值帐号：</p><br />
                       <table width="95%" border="0" class="tb_bk">
                         <tr>
                           <td width="14%" height="20" align="center" class="tb_tr">账户</td>
                           <td width="50%" align="center" class="tb_tr">开户行</td>
                           <td width="36%" align="center" class="tb_tr">卡号</td>
                         </tr>
                         <tr>
                           <td height="28" align="center">曹旭恒</td>
                           <td>中国工商银行股份有限公司北京八里庄支行</td>
                           <td>6212 2602 0000 4290 801</td>
                         </tr>
                         <tr>
                           <td height="28" align="center">曹旭恒</td>
                           <td>中国农业银行股份有限公司北京朝阳路北支行</td>
                           <td>6228 4800 1083 4719 311</td>
                         </tr>
                         <tr>
                           <td height="28" align="center">曹旭恒</td>
                           <td>中国建设银行股份有限公司北京远洋支行</td>
                           <td>6227 0000 1178 0269 437</td>
                         </tr>
                         <tr>
                           <td height="28" align="center">曹旭恒</td>
                           <td>交通银行股份有限公司北京红庙支行</td>
                           <td>6222 6009 1006 0332 861</td>
                         </tr>
                         <tr>
                           <td height="28" align="center">曹旭恒</td>
                           <td>中国邮政储蓄银行有限责任公司北京朝阳区慈云寺支行</td>
                           <td>6221 8810 0009 2345 909</td>
                         </tr>
                       </table>
                       <p></p>
                       <p>因收款账号更换，目前汇款查询比较繁琐，我们会在半个工作日内为您处理您的汇款，<font color="#FF0000">如您急需使用元宝，请更换其他方式进行充值或联系在线客服为您处理</font>，对您造成的不便请您谅解。</p>
                       <p>B、兑换比例：兑换比例(同网银比例</p>
                       <p>C、人工充值流程：</p>
                       <p>第1步、请从官网充值中心获取<font color="#FF0000">充值银行帐号</font>信息。</p>
                       <p>第2步、请向官方相应银行帐号汇款或转帐。</p>
                       <p>第3步、联系在线客服（客服QQ：1404126024）以及客服电话。向充值客服提供准确的游戏账号,游戏名称,游戏区服,游戏角色,充值银行,充值金额,真实姓名。例如：游戏帐号：123456游戏名称：盛世三国游戏区服：[双线1服]烽火九州游戏角色：ABC充值银行：农业银行充值金额：100元 真实姓名：赵四</p>
                       <p>第4步、充值完成客服确认玩家汇款或转帐到账，实时充游戏币。第5步、请玩家查收游戏币。</p><br />
                       <p><img src="<%=sWebUrl %>/wldFolder/images/xlcz.jpg" /></p>
                       <p>A、请从官方充值中心获得西联汇款收汇人信息：<font color="#FF0000">First name:xuheng  Last name:cao</font></p>
                       <p>B、在当地支持西联汇款业务的银行进行汇款，并保留好银行底单以便查询。</p>
                       <p>C、通过在线联系方式联系充值客服（客服QQ：1404126024）。需提供一下信息：A.监控号，发汇人名（Last name），发汇人姓（First name），发汇金额，发汇国家，发汇省，发汇城市，发汇地址街道，电话信息。B.游戏帐号、游戏名称、游戏区域、游戏角色。注：以上信息需全部正确填写才可充值成功</p>
                       <p>D、充值客服在线的情况下，查询到账后会第一时间为您处理。注：西联汇款查询时间为北京时间8:00-20:005、请玩家登录游戏查收游戏币。（正常工作日汇款成功后48小时内发放）</p>
                       <br />
                       <p><img src="<%=sWebUrl %>/wldFolder/images/tbsm.jpg" /></p>
                       <p>A、人工充值www.dao50.com官方服务在线快充，<font color="#FF0000">每个账号目前只接受单笔充值100元RMB以上</font>，其他方式请按照官网充值中心操作。</p>
                       <p>B、请汇款或转帐时将转账金额增加角和分，例如：充值100元，请转账100.01元到100.99元之间的金额，方便官方充值客服查询。</p>
                       <p>C、银行汇款或转帐的实到金额享有和网银充值相同的充值比例，异地和跨行汇款或转帐的手续费根据各银行规定由银行向用户收取。</p>
                       <p>D、请汇款或转帐的时候保留单据和转账订单号。</p>
                       <p>E、如海外汇款的玩家，建议先向当地银行咨询所需资料以后再联系www.dao50.com客服人员，确认资料正确后再进行汇款。</p>
                       <p>F、充值完成后请联系www.dao50.com客服人员核对充值金额及提供充值资料。</p>
                       <p>G、请尽量选择我们支持的银行进行汇款，不建议使用跨行汇款，如条件不允许的情况下使用跨行汇款，请务必保留您的汇款凭据以及联系客服时将您的汇款流水号告知客服便于我们查询。跨行汇款处理时间3—5个工作日。</p>
                       <br />
                       <p><img src="<%=sWebUrl %>/wldFolder/images/fwsm.jpg" /></p>
                       <p>A、人工充值需要www.dao50.com官方手动处理，务必正确告知客服（客服QQ：1404126024）您需要充值的游戏帐号，游戏帐号填写错误导致的损失请自行承担！</p>
                       <p>B、充值后如客服在线，半天内充值完成；如客服没在线，请留言，我们承诺第一时间为您充值！</p>
                       <p>C、核实完成后，请玩家立即进入游戏查收游戏币！</p>
                       <p>D、一旦充值成功，系统将不提供充值修正服务，如填写金额错误导致的损失我们不承担！</p>
                       <p>E、人工充值不需要您提供任何密码，请各位玩家提高安全意识，非本页公布的帐号信息均非我平台官方帐号。</p>
                        </div>
                      </div><!--兑换平台币开始-->
                      <div id="gpayinfos" class="chongzhi03" style="display:none" >
                        <div class="chongzhi03_left">
                          &nbsp;&nbsp;
                        </div>
                        <div class="chongzhi03_right">
                          <div class="chongzhi03_right03">
                            <p><strong>您当前选择的是<font color="#CC0000";>“平台币兑换”</font>支付方式</strong></p>
                            <p>您可以使用平台币兑换，选择兑换到平台旗下游戏的游戏币。</p>
                            </div>
                         <!--支付帮助暂时隐藏<div id="bangzhu1" style="display:none"><img  /></div>-->
                        <div class="chongzhi03_right04">
                          <ul class="cz_01">
                          <li><img src="<%=sWebUrl %>/wldFolder/images/cz_bz1.jpg" /></li>
                          <li><font color="#FF0000">*</font>充值账号：<input type="text" id="gameaccount" onblur="GameAccountCheck()" style="width:150px;" value="<%=sUserName %>"/><span id="gaccounterr" style="color:#FF3300"></span></li>
                          <li>&nbsp;手机号码：<input type="text" id="gamephone" maxlength="11" style="width:150px;" /></li>
                          <li class="f19">建议填写您当前使用的手机号码，以便在遇到问题时我们能及时与您沟通。</li>
                          </ul>
                          <ul class="cz_01">
                          <li><img src="<%=sWebUrl %>/wldFolder/images/cz_bz2.jpg" /></li>
                          <li>充值到游戏：
                              <select id="ggamename" onchange="ServerSelOfGame('gservername',this.value,'==请选择==');SetGPoints();GamePayVal()" style="width:150px;z-index:-1;">
                              <option value="">==请选择==</option>
                              </select><span id="ggamenameerr" style="color:#FF3300"></span>
                          </li>
                          <li>选择服务器：
                              <select id="gservername" onchange="GamePayVal()" style="width:150px;z-index:-1;">
                              <option value="">==请选择==</option>
                              </select><span id="gservernameerr" style="color:#FF3300"></span>
                          </li>
                          <li id="roleinfos" style="display:none">选 择角 色：
                              <select id="role" onchange="RoleVal()" style="width:150px;z-index:-1;">
                              <option value="">==请选择==</option>
                              </select><span id="roleerr" style="color:#FF3300"></span>
                          </li>
                          <li class="f19">请玩家在充值前，务必确认该账号已在对应的游戏服务器里创建了游戏角色。</li>
                          </ul>
                          <ul class="cz_01">
                          <li><img src="<%=sWebUrl %>/wldFolder/images/cz_bz3.jpg" /></li>
                          <li>账户余额：<font color="#FF0000"><%=iUserPoints %></font>平台币</li>
                          <li>充值数量：
                              <select id="gpaynums" onchange="SetGPoints()" style="width:150px;">
                                    <option value="10">10平台币</option>
                                    <option value="80">80平台币</option>
                                    <option value="90">90平台币</option>
                                    <option value="100">100平台币</option>
                                    <option value="200">200平台币</option>
                                    <option value="500">500平台币</option>
                                    <option value="1000">1000平台币</option>
                                    <option value="2000">2000平台币</option>
                                    <option value="5000">5000平台币</option>
                                    <option value="10000">10000平台币</option>
                                    <option value="20000">20000平台币</option>
                                    <option value="50000">50000平台币</option>
                                    <option value="100000">100000平台币</option>
                              </select>
                          </li>
                          <li>您可获得：<font id="gpoints" color="#FF0000">80元宝</font></li>
                          </ul>
                        </div>
                        <div class="chongzhi03_right01">
                        <center><img alt="" onclick="openWindowp('350','250')" src="<%=sWebUrl %>/wldFolder/images/querenduihuan.jpg" border="0" /></center>
                        </div>
                        </div>
                      </div><!--兑换平台币结束-->
                    </div><!--chongzhi01结束-->
                    <input type="hidden" id="channel" value="ibank"/>
                    <input type="hidden" id="paytowhere" name="paytowhere" value="qpaytogame" />
                    <input type="hidden" id="islogin" name="islogin" value="<%=sErr %>">
                    <input type="hidden" id="partner" name="partner" value="yykj"/>
        </form>
    </div><!--充值成功内容结束-->
    <!--弹出层隐藏-->
 <div class="WindowDIV" id="WindowDIV" style="display:none">   
  <div id="tanchuang" style="position:relative; z-index:+1">
	<div id="tanchuang01" style="width:350px;height:30px" tabindex="-1"><font color="#fff">充值信息确认</font></div>
    <div id="WlbNote" class="tanchuang03">
    <form name="bankpay" action="BankPayP.ashx" method="post" target="_blank">
    <p class="text_small">&nbsp;</p>
    <p class="text_small">您要充值的账号：<span id="wlbaccount" class="f19"></span></p>
    <p class="text_small">您要充值的平台币：<span id="wlbnums" class="f19"></span></p>
    <p class="text_small">您所花费的金钱(元)：<span id="wlbmoney" class="f19"></span></p>
    <p><center><input type="submit" value="" class="queding" />
    <input type="button" value="" onclick="hiddenWindowsp()" class="fanhui" /></center></p>
    <input type="hidden" id="formbank_account" name="bankaccount" value="" />
    <input type="hidden" id="formbank_phone" name="bankphonenum" value="" />
    <input type="hidden" id="formbank_payprice" name="bankpayprice" value="" />
    <input type="hidden" id="formbank_channel" name="bankchannel" value="" />
    <input type="hidden" id="formbank_name" name="bankname" value="" />
    <input type="hidden" id="formbank_cardType" name="cardTypeCombine" value="0" />
    <input type="hidden" name="pid" value="<%=sPId %>"/>
    </form>
    </div>
    <span style='clear:both;'/> 
    <div id="GameNote" class="tanchuang03" style="display:none;">
    <form name="gamepay" action="PTPayP.ashx" method="post" target="_blank">
    <p class="text_small">&nbsp;</p>
    <p class="text_small">您要充值的账号：<span id="gaccount" class="f19"></span></p>
    <p class="text_small">您要花费的平台币：<span id="gamewlbs" class="f19"></span></p>
    <p class="text_small">您要充值的游戏：<span id="gname" class="f19"></span></p>
    <p class="text_small">您要充值的服务器：<span id="sname" class="f19"></span></p>
    <p class="text_small">您要充值的游戏币：<span id="gpoint" class="f19"></span></p>
    <p><center><input type="submit" value="" class="queding" />
    <input type="button" value="" onclick="hiddenWindowsp()" class="fanhui" /></center></p>    
    <input type="hidden" id="formgame_account" name="gameaccount" value="" />
    <input type="hidden" id="formgame_phone" name="gamephone" value="" />
    <input type="hidden" id="formgame_paynums" name="gamepaynums" value="" />
    <input type="hidden" id="formgame_servername" name="gameservername" value="" />    
    <input type="hidden" id="formgame_role" name="gamerole" value="" />  
    <input type="hidden" name="pid" value="<%=sPId %>"/>   
    </form>
    </div>
    <span style='clear:both;'/> 
    <div id="QuickNote" class="tanchuang03" style="display:none">    
    <form name="quickpay" action="QuickPayP.ashx" method="post" target="_blank">    
    <p class="text_small">&nbsp;</p>
    <p class="text_small">您要充值的账号：<span id="quickaccount" class="f19"></span></p>
    <p class="text_small">您要充值的游戏：<span id="quickgame" class="f19"></span></p>
    <p class="text_small">您要充值的服务器：<span id="quickserver" class="f19"></span></p>
    <p class="text_small">您要充值的游戏币：<span id="quickgamepoints" class="f19"></span></p>
    <p class="text_small">您所花费的金钱(元)：<span id="quickmoney" class="f19"></span></p>
    <p><center><input type="submit" value="" class="queding" />
    <input type="button" value="" onclick="hiddenWindowsp()" class="fanhui" /></center></p>
    <input type="hidden" id="formquick_account" name="quickaccount" value="" />
    <input type="hidden" id="formquick_phone" name="quickphone" value="" />
    <input type="hidden" id="formquick_payprice" name="quickpayprice" value="" />
    <input type="hidden" id="formquick_servername" name="quickservername" value="" />      
    <input type="hidden" id="formquick_channel" name="quickchannel" value="" /> 
    <input type="hidden" id="formquick_bank" name="quickbank" value="" />
    <input type="hidden" id="formquick_cardType" name="quickcardTypeCombine" value="0" />
    <input type="hidden" id="formquick_role" name="quickrole" value="" />
    <input type="hidden" name="pid" value="<%=sPId %>"/>   
    </form>
    </div>
    <span style='clear:both;'/> 
 </div>
   </div>  
    <!--弹出层隐藏结束-->
</body>
</html>

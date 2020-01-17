using System;
using System.Text;
using com.yeepay;
using Common;

namespace Bussiness
{
    public class YeePayBuy
    {
        private const string p1_MerId = "10011371683";            // 商家ID
        private const string keyValue = "4I9A2hY6s87441M910498Ip01E2YJMNbiz904202DJr8P520au43LS2I9M6K";    // 商家密钥

        /// <summary>
        /// 易宝普通支付
        /// </summary>
        /// <param name="p2_Order">商户订单号</param>
        /// <param name="p3_Amt">支付金额,单位:元，精确到分</param>
        /// <param name="sPoints">充值点数</param>
        /// <param name="pa_MP">商户扩展信息,用户账号</param>
        /// <param name="pd_FrpId">银行编码</param>
        /// <returns></returns>
        public static string CreateBuyUrl(string p2_Order, string p3_Amt, string sPoints, string pa_MP, string pd_FrpId, string p8_Url)
        {
            // 设置请求地址
            Buy.NodeAuthorizationURL = @"https://www.yeepay.com/app-merchant-proxy/node";
            // 商家设置用户购买商品的支付信息.
            //易宝支付平台统一使用GBK/GB2312编码方式,参数如用到中文，请注意转码            
            //交易币种,固定值"CNY".
            string p4_Cur = "CNY";
            StringBuilder sbText = new StringBuilder();
            sbText.Append("到武林");
            sbText.Append(sPoints);
            sbText.Append("武林币");
            string p5_Pid = sbText.ToString();
            string p6_Pcat = "wulin B";//商品种类

            //商品描述
            string p7_Pdesc = "wulinbi";
            //商户接收支付成功数据的地址,支付成功后易宝支付会向该地址发送两次成功通知.
            //p8_Url = Request.Form["p8_Url"];
            //送货地址为“1”: 需要用户将送货地址留在易宝支付系统;为“0”: 不需要，默认为 ”0”.
            string p9_SAF = "0";
            //应答机制为"1": 需要应答机制;为"0": 不需要应答机制.
            string pr_NeedResponse = "1";

            return Buy.CreateBuyUrl(p1_MerId, keyValue, p2_Order, p3_Amt, p4_Cur, p5_Pid, p6_Pcat, p7_Pdesc, p8_Url, p9_SAF, pa_MP, pd_FrpId, pr_NeedResponse);
        }

        public static string PayDirect(string sChannel, string sPhone, string sAccount, decimal dPrice, int iCount)
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<form id='yeepaysubmit' name='yeepaysubmit' action='YPay.ashx' method='post'>");
            sbHtml.AppendFormat("<input type='hidden' name='Channel' value='{0}'/>", sChannel);
            sbHtml.AppendFormat("<input type='hidden' name='Phone' value='{0}'/>", sPhone);
            sbHtml.AppendFormat("<input type='hidden' name='Account' value='{0}'/>", sAccount);
            sbHtml.AppendFormat("<input type='hidden' name='Price' value='{0}'/>", dPrice.ToString());
            sbHtml.AppendFormat("<input type='hidden' name='Count' value='{0}'/>", iCount.ToString());
            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='' style='display:none;'></form>");
            sbHtml.Append("<script>document.forms['yeepaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }

        public static string QuickPayDirect(string sChannel, string sPhone, string sAccount, decimal dPrice, int iCount, string sGame)
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<form id='yeepaysubmit' name='yeepaysubmit' action='YPay.ashx' method='post'>");
            sbHtml.AppendFormat("<input type='hidden' name='Channel' value='{0}'/>", sChannel);
            sbHtml.AppendFormat("<input type='hidden' name='Phone' value='{0}'/>", sPhone);
            sbHtml.AppendFormat("<input type='hidden' name='Account' value='{0}'/>", sAccount);
            sbHtml.AppendFormat("<input type='hidden' name='PayUserID' value='{0}'/>", "0");
            sbHtml.AppendFormat("<input type='hidden' name='Price' value='{0}'/>", dPrice.ToString());
            sbHtml.AppendFormat("<input type='hidden' name='Count' value='{0}'/>", iCount.ToString());
            sbHtml.AppendFormat("<input type='hidden' name='Game' value='{0}'/>", sGame);
            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='' style='display:none;'></form>");
            sbHtml.Append("<script>document.forms['yeepaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }

        public static string PayBegin(string sChannel, string sPhone, string sAccount, decimal dPrice, int iCount)
        {

            string sTranIP = ProvideCommon.GetRealIP();
            string p2_Order = TransPBLL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount, sTranIP);//订单号
            string sPoints = (dPrice * 10).ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append("到武林");
            sbText.Append(sPoints);
            sbText.Append("武林币");
            string p5_Pid = sbText.ToString();//商品名称
            string pd_FrpID = string.Empty;//充值方式编码
            switch (sChannel)
            {
                case "yp-szx":
                    pd_FrpID = "SZX-NET";
                    break;
                case "yp-dx":
                    pd_FrpID = "TELECOM-NET";
                    break;
                case "yp-lt":
                    pd_FrpID = "UNICOM-NET";
                    break;
                case "yp-zt":
                    pd_FrpID = "ZHENGTU-NET";
                    break;
                case "yp-sd":
                    pd_FrpID = "SNDACARD-NET";
                    break;
                case "yp-jcard":
                    pd_FrpID = "JUNNET-NET";
                    break;
                case "yp-bank":
                    pd_FrpID = "";
                    break;
            }
            string pa_MP = sAccount;
            string p8_Url = string.Format("{0}/pay/YeeCallback.aspx", ProvideCommon.GetRootURI());
            string sTranUrl = YeePayBuy.CreateBuyUrl(p2_Order, dPrice.ToString(), sPoints, pa_MP, pd_FrpID, p8_Url);
            FirstOfPayPointBLL.Add(p2_Order, sTranIP, sTranUrl);
            return sTranUrl;
        }

        public static string QuickPayBegin(string p2_Order, string sChannel, string sAccount, decimal dPrice, string sGame)
        {
            string sPoints = (dPrice * 10).ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append("到武林");
            sbText.Append(sPoints);
            sbText.Append("武林币");
            string p5_Pid = sbText.ToString();//商品名称
            string pd_FrpID = string.Empty;//充值方式编码
            switch (sChannel)
            {
                case "yp-szx":
                    pd_FrpID = "SZX-NET";
                    break;
                case "yp-dx":
                    pd_FrpID = "TELECOM-NET";
                    break;
                case "yp-lt":
                    pd_FrpID = "UNICOM-NET";
                    break;
                case "yp-zt":
                    pd_FrpID = "ZHENGTU-NET";
                    break;
                case "yp-sd":
                    pd_FrpID = "SNDACARD-NET";
                    break;
                case "yp-jcard":
                    pd_FrpID = "JUNNET-NET";
                    break;
                case "yp-bank":
                    pd_FrpID = "";
                    break;
            }
            string pa_MP = string.Format("{0}|{1}", sAccount, sGame);
            string p8_Url = string.Format("{0}/pay/QuickYeeCallback.aspx", ProvideCommon.GetRootURI());
            string sTranUrl = YeePayBuy.CreateBuyUrl(p2_Order, dPrice.ToString(), sPoints, pa_MP, pd_FrpID, p8_Url);
            string sTranIP = ProvideCommon.GetRealIP();
            FirstOfPayPointBLL.Add(p2_Order, sTranIP, sTranUrl);
            return sTranUrl;
        }

        public static string YeePaySubmit()
        {
            string sRes = string.Empty;
            // 校验返回数据包
            BuyCallbackResult result = Buy.VerifyCallback(p1_MerId, keyValue, Buy.GetQueryString("r0_Cmd"), Buy.GetQueryString("r1_Code"), Buy.GetQueryString("r2_TrxId"),
                Buy.GetQueryString("r3_Amt"), Buy.GetQueryString("r4_Cur"), Buy.GetQueryString("r5_Pid"), Buy.GetQueryString("r6_Order"), Buy.GetQueryString("r7_Uid"),
                Buy.GetQueryString("r8_MP"), Buy.GetQueryString("r9_BType"), Buy.GetQueryString("rp_PayDate"), Buy.GetQueryString("hmac"));
            if (string.IsNullOrEmpty(result.ErrMsg))    //校验返回数据包成功
            {
                StringBuilder sbText = new StringBuilder();
                if (result.R1_Code == "1")  //返回充值成功的标识
                {
                    if (result.R9_BType == "1")  //返回方式1：浏览器重定向方式
                    {
                        string sTranID = result.R6_Order;
                        decimal dPrice = decimal.Parse(result.R3_Amt);
                        string sAccount = result.R8_MP;
                        int iResNum = TransPBLL.PointSalesCommit(sTranID, sAccount, dPrice);
                        if (0 == iResNum)
                        {
                            sbText.Append("1|");
                            sbText.Append(result.R6_Order);
                            sRes = sbText.ToString();
                        }
                        else
                        {
                            if (6 == iResNum)
                            {
                                sbText.Append("1|");
                                sbText.Append(result.R6_Order);
                                sRes = sbText.ToString();
                            }
                            else
                            {
                                sbText.Append("2|");
                                sbText.Append(result.R6_Order);
                                sRes = sbText.ToString();
                            }
                        }
                    }
                    else if (result.R9_BType == "2")
                    {
                        int iRes = TransPBLL.PointSalesCommit(result.R6_Order, result.R8_MP, decimal.Parse(result.R3_Amt));
                        sRes = "4";
                    }
                    else if (result.R9_BType == "3")
                    {
                        sRes = "4";
                    }
                }
                else
                {
                    sbText.Append("0|");
                    sbText.Append(result.R1_Code);
                    sRes = sbText.ToString();
                }
            }
            else
            {
                sRes = "0|valerr";
            }
            return sRes;
        }

        public static string QuickYeePaySubmit()
        {
            string sRes = string.Empty;
            // 校验返回数据包
            BuyCallbackResult result = Buy.VerifyCallback(p1_MerId, keyValue, Buy.GetQueryString("r0_Cmd"), Buy.GetQueryString("r1_Code"), Buy.GetQueryString("r2_TrxId"),
                Buy.GetQueryString("r3_Amt"), Buy.GetQueryString("r4_Cur"), Buy.GetQueryString("r5_Pid"), Buy.GetQueryString("r6_Order"), Buy.GetQueryString("r7_Uid"),
                Buy.GetQueryString("r8_MP"), Buy.GetQueryString("r9_BType"), Buy.GetQueryString("rp_PayDate"), Buy.GetQueryString("hmac"));
            if (string.IsNullOrEmpty(result.ErrMsg))    //校验返回数据包成功
            {
                StringBuilder sbText = new StringBuilder();
                if (result.R1_Code == "1")  //返回充值成功的标识
                {
                    decimal dPrice = decimal.Parse(result.R3_Amt);
                    int iPrice = Convert.ToInt32(dPrice);
                    string sChannel = string.Empty;
                    switch (Buy.GetQueryString("rb_BankId"))
                    {
                        case "SZX-NET":
                            sChannel = "yp-szx";
                            break;
                        case "TELECOM-NET":
                            sChannel = "yp-dx";
                            break;
                        case "UNICOM-NET":
                            sChannel = "yp-lt";
                            break;
                        case "ZHENGTU-NET":
                            sChannel = "yp-zt";
                            break;
                        case "SNDACARD-NET":
                            sChannel = "yp-sd";
                            break;
                        case "JUNNET-NET":
                            sChannel = "yp-jcard";
                            break;
                        default:
                            sChannel = "";
                            break;
                    }
                    if (sChannel != "")
                    {
                        dPrice = dPrice * ChannelBLL.FeeScaleSel(sChannel);
                    }
                    if (result.R9_BType == "1")  //返回方式1：浏览器重定向方式
                    {
                        string sPTranID = result.R6_Order;
                        string sAccount = result.R8_MP.Split('|')[0];
                        string sGame = result.R8_MP.Split('|')[1];
                        int iResNum = TransPBLL.PointSalesCommit(sPTranID, sAccount, iPrice);
                        string sGTranID = TranQuickBLL.TranQuickGTranIDSel(sPTranID);
                        if (0 == iResNum)
                        {
                            TranQuickBLL.TranQuickUpdateP(sPTranID);
                            string sGTRes = string.Empty;
                            if (sGame.IndexOf("sq") == -1)
                            {
                                sGTRes = PayAll.GameQuickPay(sGame, sAccount, dPrice, sGTranID);
                            }
                            else
                            {
                                string sRoleID = result.R8_MP.Split('|')[2];
                                sGTRes = PayAll.sqQuickPay(sGame, sAccount, dPrice, sGTranID, sRoleID);
                            }
                            if (sGTRes == "0")
                            {
                                TranQuickBLL.TranQuickUpdateG(sGTranID);
                                sbText.Append("1|");
                            }
                            else
                            {
                                //游戏冲值失败
                                sbText.Append("3|");
                            }
                            sbText.Append(sGTranID);
                            sbText.AppendFormat("|{0}", sGame);
                            sRes = sbText.ToString();
                        }
                        else
                        {
                            string sQuickState = TranQuickBLL.TransQuickStateSelByP(sPTranID);
                            if (sQuickState == "2")
                            {
                                //游戏充值成功
                                sbText.Append("1|");
                                sbText.Append(sGTranID);
                                sbText.AppendFormat("|{0}", sGame);
                                sRes = sbText.ToString();
                            }
                            else if (sQuickState == "1")
                            {
                                string sGTRes = string.Empty;
                                if (sGame.IndexOf("sq") == -1)
                                {
                                    sGTRes = PayAll.GameQuickPay(sGame, sAccount, dPrice, sGTranID);
                                }
                                else
                                {
                                    string sRoleID = result.R8_MP.Split('|')[2];
                                    sGTRes = PayAll.sqQuickPay(sGame, sAccount, dPrice, sGTranID, sRoleID);
                                }
                                if (sGTRes == "0") //游戏兑换成功
                                {
                                    TranQuickBLL.TranQuickUpdateG(sGTranID);
                                    //游戏充值成功
                                    sbText.Append("1|");
                                }
                                else
                                {
                                    //游戏冲值失败
                                    sbText.Append("3|");
                                }
                                sbText.Append(sGTranID);
                                sbText.AppendFormat("|{0}", sGame);
                                sRes = sbText.ToString();
                            }
                            else
                            {
                                sbText.Append("2|");
                                sbText.Append(sPTranID);
                                sRes = sbText.ToString();
                            }
                        }
                    }
                    else if (result.R9_BType == "2")
                    {
                        string sAccount = result.R8_MP.Split('|')[0];
                        string sGame = result.R8_MP.Split('|')[1];
                        string sPTranID = result.R6_Order;
                        int iRes = TransPBLL.PointSalesCommit(sPTranID, sAccount, iPrice);
                        string sGTranID = TranQuickBLL.TranQuickGTranIDSel(sPTranID);
                        if (iRes == 0)
                        {
                            TranQuickBLL.TranQuickUpdateP(sPTranID);
                            string sGTRes = string.Empty;
                            if (sGame.IndexOf("sq") == -1)
                            {
                                sGTRes = PayAll.GameQuickPay(sGame, sAccount, dPrice, sGTranID);
                            }
                            else
                            {
                                string sRoleID = result.R8_MP.Split('|')[2];
                                sGTRes = PayAll.sqQuickPay(sGame, sAccount, dPrice, sGTranID, sRoleID);
                            }
                            if (sGTRes == "0")
                            {
                                TranQuickBLL.TranQuickUpdateG(sGTranID);
                            }
                        }
                        sRes = "4";
                    }
                    else if (result.R9_BType == "3")
                    {
                        sRes = "4";
                    }
                }
                else
                {
                    sbText.Append("0|");
                    sbText.Append(result.R1_Code);
                    sRes = sbText.ToString();
                }
            }
            else
            {
                sRes = "0|valerr";
            }
            return sRes;
        }

        public static void LastOfPayLog(string sTranIP, char cTranFrom, string sFromUrl)
        {
            string sTranID = Buy.GetQueryString("r6_Order");
            LastOfPayPointBLL.Add(sTranIP, cTranFrom, sFromUrl, sTranID);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;

using tenpay;
using Common;

namespace Bussiness
{
    public class TenPayBuy
    {
        private const string bargainor_id = "1212302001";   //商户号
        private const string key = "a2990e2986ef3b21a5400414216c089a";  //密钥

        public static string PayDirect(string sOrderID, string sAccount, decimal dPrice, string PayType, HttpContext Context, string return_url)
        {
            string sPayUrl = "https://www.tenpay.com/cgi-bin/v1.0/pay_gate.cgi";
            string date = DateTime.Now.ToString("yyyyMMdd");
			string strReq = "" + DateTime.Now.ToString("HHmmss") + TenpayUtil.BuildRandomStr(4);			
			string sp_billno = sOrderID.Substring(0, 32);
			//财付通订单号，10位商户号+8位日期+10位序列号，需保证全局唯一
			string transaction_id = bargainor_id + date + strReq;
			//创建PayRequestHandler实例
            PayRequestHandler reqHandler = new PayRequestHandler(Context);            
			reqHandler.setKey(key);
            reqHandler.setGateUrl(sPayUrl);
            string attach = string.Format("{0}|{1}|{2}", sOrderID.Substring(32), sAccount, PayType);
            decimal price = Convert.ToInt32(dPrice * 100);
            //初始化
			reqHandler.init();
            string desc = string.Empty;
            if (PayType == "wlb")
            {
                desc = string.Format("到武林{0}元武林币充值", dPrice.ToString());
            }
            else
            {
                string sGameAbbre = PayType.Split('|')[0];
                string sGameName = PayAll.GetGameName(sGameAbbre);
                desc = string.Format("到武林{0}元{1}游戏直冲",dPrice.ToString(),sGameName);
            }
			//-----------------------------
			//设置支付参数
			//-----------------------------
			reqHandler.setParameter("bargainor_id", bargainor_id);			//商户号
			reqHandler.setParameter("sp_billno", sp_billno);				//商家订单号
			reqHandler.setParameter("transaction_id", transaction_id);		//财付通交易单号
			reqHandler.setParameter("return_url", return_url);				//支付通知url
            reqHandler.setParameter("desc", desc);	//商品名称
			reqHandler.setParameter("total_fee", price.ToString());						//商品金额,以分为单位
            reqHandler.setParameter("attach", attach);  //商家数据包，原样返回            
            reqHandler.setParameter("cs", "utf-8");
			//用户ip,测试环境时不要加这个ip参数，正式环境再加此参数
            string UserIP = ProvideCommon.GetRealIP();
			reqHandler.setParameter("spbill_create_ip",UserIP);
            //post实现方式
			reqHandler.getRequestURL();
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("<form id='tenpaysubmit' name='tenpaysubmit' method=\"post\" action=\"{0}\">", reqHandler.getGateUrl());
			Hashtable ht = reqHandler.getAllParameters();
			foreach(DictionaryEntry de in ht) 
			{
                sbText.AppendFormat("<input type=\"hidden\" name=\"{0}\" value=\"{1}\" >",de.Key,de.Value);
			}
            sbText.Append("<input type='submit' value='submit' style='display:none;'></form>");
            sbText.Append("<script>document.forms['tenpaysubmit'].submit();</script>");
            FirstOfPayPointBLL.Add(sOrderID, UserIP, sbText.ToString());
            return sbText.ToString();
        }

        public static string QuickPayBegin(string sOrderID,string sAccount,decimal dPrice, string sGame,HttpContext Context)
        {
            string return_url = string.Format("{0}/pay/QuickTenCallBack.aspx", ProvideCommon.GetRootURI());
            return PayDirect(sOrderID,sAccount,dPrice,sGame,Context,return_url);
        }

        public static string PayBegin(string sChannel, string sPhone, string sAccount, decimal dPrice, int iCount, HttpContext Context)
        {
            string return_url = string.Format("{0}/pay/TenCallBack.aspx", ProvideCommon.GetRootURI());
            string sTranIP = ProvideCommon.GetRealIP();
            string sOrderID = TransPBLL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount, sTranIP);
            string sPayType = "wlb";
            return PayDirect(sOrderID,sAccount,dPrice, sPayType, Context, return_url);
        }

        public static string TenPaySubmit(HttpContext Context)
        {
            string sRes = string.Empty;

            //创建PayResponseHandler实例
            PayResponseHandler resHandler = new PayResponseHandler(Context);

            resHandler.setKey(key);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                //交易单号
                string transaction_id = resHandler.getParameter("transaction_id");

                //金额,以分为单位
                string total_fee = resHandler.getParameter("total_fee");

                //支付结果
                string pay_result = resHandler.getParameter("pay_result");

                string sp_billno = resHandler.getParameter("sp_billno");

                string attach = resHandler.getParameter("attach");
                if ("0".Equals(pay_result))
                {
                    //注意交易单不要重复处理
                    //注意判断返回金额
                    string sTranID = string.Format("{0}{1}", sp_billno, attach.Split('|')[0]);
                    decimal dPrice = decimal.Parse(total_fee) / 100;
                    string sAccount = attach.Split('|')[1];
                    int j = TransPBLL.PointSalesCommit(sTranID, sAccount, dPrice);    //确认返回信息无误后提交此定单
                    if(0 == j)
                    {
                        resHandler.doShow(string.Format("PayPSucc.aspx?TranID={0}", sTranID));
                    }
                    sRes = j.ToString();
                }
                else
                {
                    sRes = "-1";
                }
            }
            else
            {
                sRes = "-2";
            }
            return sRes;
        }

        public static string QuickTenPaySubmit(HttpContext Context)
        {
            string sRes = string.Empty;

            //创建PayResponseHandler实例
            PayResponseHandler resHandler = new PayResponseHandler(Context);
            
            resHandler.setKey(key);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                //交易单号
                string transaction_id = resHandler.getParameter("transaction_id");

                //金额金额,以分为单位
                string total_fee = resHandler.getParameter("total_fee");

                //支付结果
                string pay_result = resHandler.getParameter("pay_result");

                string sp_billno = resHandler.getParameter("sp_billno");

                string attach = resHandler.getParameter("attach");
                if ("0".Equals(pay_result))
                {
                    //------------------------------
                    //处理业务开始
                    //------------------------------ 

                    //注意交易单不要重复处理
                    //注意判断返回金额
                    
                    //------------------------------
                    //处理业务完毕
                    //------------------------------
                    string sTranID = string.Format("{0}{1}", sp_billno, attach.Split('|')[0]);
                    decimal dPrice = decimal.Parse(total_fee) / 100;
                    string sAccount = attach.Split('|')[1];
                    string sGameAbbre = attach.Split('|')[2];

                    string sTranIP = ProvideCommon.GetRealIP();
                    string sFromUrl = Context.Request.Url.ToString();                    
                    LastOfPayPointBLL.Add(sTranIP, ' ', sFromUrl, sTranID);
                    
                    int j = TransPBLL.PointSalesCommit(sTranID, sAccount, dPrice);
                    if(0 == j)
                    {
                        string sGTranID = TranQuickBLL.TranQuickGTranIDSel(sTranID);
                        TranQuickBLL.TranQuickUpdateP(sTranID);
                        string sGTRes = string.Empty;
                        if (sGameAbbre.IndexOf("sq") == -1)
                        {
                            sGTRes = PayAll.GameQuickPay(sGameAbbre, sAccount, dPrice, sGTranID);
                        }
                        else
                        {
                            string sRoleID = attach.Split('|')[3];
                            sGTRes = PayAll.sqQuickPay(sGameAbbre, sAccount, dPrice, sGTranID,sRoleID);
                        }
                        
                        string sUrl = string.Empty;
                        if (sGTRes == "0")
                        {
                            TranQuickBLL.TranQuickUpdateG(sGTranID);
                            sUrl = string.Format("PayGSucc.aspx?TranID={0}&gn={1}&type=q", sGTranID, sGameAbbre);
                            //调用doShow, 打印meta值跟js代码,告诉财付通处理成功,并在用户浏览器显示$show页面.
                            resHandler.doShow(sUrl);
                        }
                        sRes = sGTRes;
                    }
                    else if (j == 6)
                    {
                        string sQuickState = TranQuickBLL.TransQuickStateSelByP(sTranID);
                        string sGTranID = TranQuickBLL.TranQuickGTranIDSel(sTranID);
                        if (sQuickState == "2")
                        {
                            string sUrl = string.Format("PayGSucc.aspx?TranID={0}&gn={1}&type=q", sGTranID, sGameAbbre);
                            //调用doShow, 打印meta值跟js代码,告诉财付通处理成功,并在用户浏览器显示$show页面.
                            resHandler.doShow(sUrl);
                            sRes = "0";
                        }
                        else if (sQuickState == "1")
                        {
                            string sGTRes = string.Empty;
                            if (sGameAbbre.IndexOf("sq") == -1)
                            {
                                sGTRes = PayAll.GameQuickPay(sGameAbbre, sAccount, dPrice, sGTranID);
                            }
                            else
                            {
                                string sRoleID = attach.Split('|')[3];
                                sGTRes = PayAll.sqQuickPay(sGameAbbre, sAccount, dPrice, sGTranID, sRoleID);
                            }

                            if (sGTRes == "0") //游戏兑换成功
                            {
                                TranQuickBLL.TranQuickUpdateG(sGTranID);
                                string sUrl = string.Format("PayGSucc.aspx?TranID={0}&gn={1}&type=q", sGTranID, sGameAbbre);
                                //调用doShow, 打印meta值跟js代码,告诉财付通处理成功,并在用户浏览器显示$show页面.
                                resHandler.doShow(sUrl);
                                sRes = "0";
                            }
                            else
                            {
                                //sMsg = "<script>alert('充值武林币成功！游戏兑换失败！如有问题请与客服联系！');</script>";
                                sRes = "-3";
                            }
                        }
                    }
                    else
                    {
                        sRes = "-4";
                    }
                }
                else
                {
                    //当做不成功处理
                    sRes = "-1";
                }
            }
            else
            {
                sRes = "-2";
            }
            return sRes;
        }
    }
}

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;

using Bussiness;
using Com.Alipay;
using Common;

namespace UserCenter.Pay
{
    public partial class PayAli_Return : pagebase.PageBase
    {
        protected string sMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestGet();
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);
                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码

                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                    //string subject = Request.QueryString["subject"];                //商品名称、订单名称
                    //string trade_no = Request.QueryString["trade_no"];              //支付宝交易号
                    //string buyer_email = Request.QueryString["buyer_email"];        //买家支付宝账号
                    string order_no = Request.QueryString["out_trade_no"];	        //获取订单号
                    string total_fee = Request.QueryString["total_fee"];            //获取总金额
                    string body = Request.QueryString["body"];                      //商品描述、订单备注、描述
                    string trade_status = Request.QueryString["trade_status"];      //交易状态
                    string extra_common_param = Request.QueryString["extra_common_param"];//商户回传参数

                    string sTranIP = ProvideCommon.GetRealIP();
                    string sFromUrl = Request.Url.ToString();
                    char cTranFrom = 't';
                    
                    LastOfPayPointBLL.Add(sTranIP, cTranFrom, sFromUrl, order_no);

                    if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                    {
                        int iLen = extra_common_param.Split('|').Length;
                        string sAccount = extra_common_param.Split('|')[0];//获取充值人账户
                        decimal dPrice = Convert.ToDecimal(total_fee);

                        int j = TransPBLL.PointSalesCommit(order_no, sAccount, dPrice);    //确认返回信息无误后提交此定单
                        if (j == 0)
                        {
                            //游戏直冲
                            if (iLen > 1)
                            {
                                TranQuickBLL.TranQuickUpdateP(order_no);
                                string sGTranID = TranQuickBLL.TranQuickGTranIDSel(order_no);
                                string sGame = extra_common_param.Split('|')[1];
                                string sGTRes = string.Empty;
                                if (sGame.IndexOf("sq") == -1)
                                {
                                    sGTRes = PayAll.GameQuickPay(sGame, sAccount, dPrice, sGTranID);
                                }
                                else
                                {
                                    string sRoleID = extra_common_param.Split('|')[2];
                                    sGTRes = PayAll.sqQuickPay(sGame, sAccount, dPrice, sGTranID,sRoleID);
                                }

                                if (sGTRes == "0") //游戏兑换成功
                                {
                                    TranQuickBLL.TranQuickUpdateG(sGTranID);
                                    Response.Redirect(string.Format("PayGSucc.aspx?TranID={0}&gn={1}&type=q", sGTranID,sGame), false);
                                }
                                else
                                {
                                    //sMsg = "<script>alert('充值武林币成功！游戏兑换失败！如有问题请与客服联系！');</script>";
                                    Response.Redirect(string.Format("PayPErr.aspx?err=102&gtres={0}", sGTRes));
                                }
                            }
                            else if(1 == iLen)
                            {
                                Response.Redirect(string.Format("PayPSucc.aspx?TranID={0}", order_no));    //转向银行卡支付成功页面
                            }
                            else
                            {
                                Response.Write(iLen);
                            }
                        }
                        else
                        {
                            if (6 == j)
                            {
                                if (iLen > 1)
                                {
                                    string sQuickState = TranQuickBLL.TransQuickStateSelByP(order_no);
                                    string sGTranID = TranQuickBLL.TranQuickGTranIDSel(order_no);
                                    string sGame = extra_common_param.Split('|')[1];
                                    
                                    if(sQuickState == "2")
                                    {
                                        Response.Redirect(string.Format("PayGSucc.aspx?TranID={0}&gn={1}&type=q", sGTranID, sGame), false);
                                    }
                                    else if(sQuickState == "1")
                                    {
                                        string sGTRes = string.Empty;
                                        if (sGame.IndexOf("sq") == -1)
                                        {
                                            sGTRes = PayAll.GameQuickPay(sGame, sAccount, dPrice, sGTranID);
                                        }
                                        else
                                        {
                                            string sRoleID = extra_common_param.Split('|')[2];
                                            sGTRes = PayAll.sqQuickPay(sGame, sAccount, dPrice, sGTranID, sRoleID);
                                        }
                                        if (sGTRes == "0") //游戏兑换成功
                                        {
                                            TranQuickBLL.TranQuickUpdateG(sGTranID);
                                            Response.Redirect(string.Format("PayGSucc.aspx?TranID={0}&gn={1}&type=q", sGTranID, sGame), false);
                                        }
                                        else
                                        {
                                            //sMsg = "<script>alert('充值武林币成功！游戏兑换失败！如有问题请与客服联系！');</script>";
                                            Response.Redirect(string.Format("PayPErr.aspx?err=102&gtres={0}", sGTRes));
                                        }
                                    }
                                }
                                else if (1 == iLen)
                                {
                                    Response.Redirect(string.Format("PayPSucc.aspx?TranID={0}", order_no));
                                }
                                else
                                {
                                    Response.Write(iLen);
                                }
                            }
                            else
                            {
                                //sMsg = "<script>alert('订单提交失败！如有问题请与客服联系！');</script>";
                                Response.Redirect("PayPErr.aspx?err=101");
                            }
                        }
                    }
                    else
                    {
                        //sMsg = "<script>alert('交易没有成功！如有问题请与客服联系！');</script>";
                        Response.Redirect("PayPErr.aspx?err=111");
                    }
                }
                else//验证失败
                {
                    //sMsg = "<script>alert('验证失败！如有问题请与客服联系！');</script>";
                    Response.Redirect("PayPErr.aspx?err=104");
                }
            }
            else
            {
                //sMsg = "<script>alert('无返回参数！如有问题请与客服联系！');</script>";
                Response.Redirect("PayPErr.aspx?err=103");
            }
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
    }
}

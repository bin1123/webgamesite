using System;
using Bussiness;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using Com.Alipay;
using Common;

namespace UserCenter.Pay
{
    public partial class PayAli_Notify : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestPost();

            //StringBuilder sbText = new StringBuilder();
            //sbText.Append(Server.MapPath("~/Log"));
            //sbText.Append("/Pay");
            //string sPath = sbText.ToString();
            //ProvideCommon pcObject = new ProvideCommon();
            //sbText.Remove(0, sbText.Length);
            //foreach(KeyValuePair<string,string> kvpForm in sPara)
            //{
            //    sbText.Append(string.Format("{0}={1} ",kvpForm.Key,kvpForm.Value));
            //}
            //string sFormUrl = sbText.ToString();
            //sbText.Remove(0,sbText.Length);
            //sbText.AppendFormat("{0},{1}", sFormUrl, DateTime.Now.ToString());
            //pcObject.WriteLogFile(sPath, "PayAliNotify", sbText.ToString());

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码
                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表
                    //string trade_no = Request.Form["trade_no"];         //支付宝交易号
                    //string buyer_email = Request.Form["buyer_email"];   //买家支付宝账号
                    string order_no = Request.Form["out_trade_no"];     //获取订单号
                    string total_fee = Request.Form["total_fee"];       //获取总金额
                    string subject = Request.Form["subject"];           //商品名称、订单名称
                    string body = Request.Form["body"];                 //商品描述、订单备注、描述
                    string trade_status = Request.Form["trade_status"]; //交易状态
                    string extra_common_param = Request.Form["extra_common_param"];//商户回传参数

                    string sTranIP = ProvideCommon.GetRealIP();
                    string sFromUrl = Request.Url.ToString();
                    char cTranFrom = 'y';
                    LastOfPayPointBLL.Add(sTranIP, cTranFrom, sFromUrl, order_no);
                    if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                        string sAccount = extra_common_param.Split('|')[0];//获取充值人账户
                        decimal dPrice = Convert.ToDecimal(total_fee);
                        int iLen = extra_common_param.Split('|').Length;
                        int j = TransPBLL.PointSalesCommit(order_no, sAccount, dPrice);    //确认返回信息无误后提交此定单
                        if (j == 0)
                        {
                            if (iLen > 1)
                            {
                                //游戏直冲
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
                                    if (extra_common_param.Split('|').Length > 2)
                                    {
                                        string sRoleID = extra_common_param.Split('|')[2];
                                        sGTRes = PayAll.sqQuickPay(sGame, sAccount, dPrice, sGTranID, sRoleID);
                                    }
                                }

                                if (sGTRes == "0") //游戏兑换成功
                                {
                                    TranQuickBLL.TranQuickUpdateG(sGTranID);
                                }
                            }
                        }
                        Response.Write("success");  //请不要修改或删除
                    }
                    else
                    {
                        Response.Write("success");  //其他状态判断。普通即时到帐中，其他状态不用判断，直接打印success。有问题，这里需要判断吗？
                    }
                }
                else//验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }
}

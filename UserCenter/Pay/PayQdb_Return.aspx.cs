using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Pay
{
    public partial class PayQdb_Return : System.Web.UI.Page
    {
        protected string sMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string partnerTradeId = CYRequest.GetString("partnerTradeId"); // 商户订单号
            string state = CYRequest.GetString("state"); //支付状态
            string signType = CYRequest.GetString("signType");
            string tradeMoney = CYRequest.GetString("tradeMoney");
            string successMoney = CYRequest.GetString("successMoney");// 实际支付金额
            string tradeId = CYRequest.GetString("tradeId"); // 支付中心订单号
            string bankCode = CYRequest.GetString("bankCode");// 支付银行
            string tradeSuccessTime = CYRequest.GetString("tradeSuccessTime"); //订单成功时间
            string productName = CYRequest.GetString("productName"); //商品名称
            string productUrl = CYRequest.GetString("productUrl"); //商品url
            string remark = CYRequest.GetString("remark"); //备注
            string sign = CYRequest.GetString("sign"); //加密串

            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|", partnerTradeId);
            sbText.AppendFormat("{0}|", state);
            sbText.AppendFormat("{0}|", signType);
            sbText.AppendFormat("{0}|", tradeMoney);
            sbText.AppendFormat("{0}|", successMoney);
            sbText.AppendFormat("{0}|", tradeId);
            sbText.AppendFormat("{0}|", bankCode);
            sbText.AppendFormat("{0}|", tradeSuccessTime);
            sbText.AppendFormat("{0}|", productName);
            sbText.AppendFormat("{0}|", productUrl);
            sbText.AppendFormat("{0}|", remark);
            sbText.Append(QdbPayBuy.GetKey());
            string md5 = QdbPayBuy.GetMD5(sbText.ToString(), "gb2312").ToUpper();

            if (md5.Equals(sign))
            {
                if ("1".Equals(state))
                {
                    //支付成功
                    int iLen = remark.Split('|').Length;
                    string sAccount = remark.Split('|')[1];//获取充值人账户
                    decimal dPrice = Convert.ToDecimal(successMoney);
                    string lbOrderId = string.Format("{0}{1}", partnerTradeId, remark.Split('|')[0]);
                    int j = TransPBLL.PointSalesCommit(lbOrderId, sAccount, dPrice);    //确认返回信息无误后提交此定单
                    if (j == 0)
                    {
                        //游戏直冲
                        if (iLen > 2)
                        {
                            TranQuickBLL.TranQuickUpdateP(lbOrderId);
                            string sGTranID = TranQuickBLL.TranQuickGTranIDSel(lbOrderId);
                            dPrice = (dPrice * 95) / 100;
                            string sGame = remark.Split('|')[2];
                            string sGTRes = string.Empty;
                            if (sGame.IndexOf("sq") == -1)
                            {
                                sGTRes = PayAll.GameQuickPay(sGame, sAccount, dPrice, sGTranID);
                            }
                            else
                            {
                                string sRoleID = remark.Split('|')[3];
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
                        else
                        {
                            Response.Redirect(string.Format("PayPSucc.aspx?TranID={0}", lbOrderId));    //转向银行卡支付成功页面
                        }
                    }
                    else
                    {
                        if (6 == j)
                        {
                            if (iLen > 2)
                            {
                                string sQuickState = TranQuickBLL.TransQuickStateSelByP(lbOrderId);
                                string sGTranID = TranQuickBLL.TranQuickGTranIDSel(lbOrderId);
                                string sGame = remark.Split('|')[2];
                                if (sQuickState == "2")
                                {
                                    Response.Redirect(string.Format("PayGSucc.aspx?TranID={0}&gn={1}&type=q", sGTranID, sGame), false);
                                }
                                else if (sQuickState == "1")
                                {
                                    int iChannelID = TransPBLL.TranPSelChannelIDByID(lbOrderId);
                                    dPrice = (dPrice * 95) / 100;
                                    string sGTRes = string.Empty;
                                    if (sGame.IndexOf("sq") == -1)
                                    {
                                        sGTRes = PayAll.GameQuickPay(sGame, sAccount, dPrice, sGTranID);
                                    }
                                    else
                                    {
                                        string sRoleID = remark.Split('|')[3];
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
                            else
                            {
                                Response.Redirect(string.Format("PayPSucc.aspx?TranID={0}", lbOrderId));
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
                    //支付失败
                    //sMsg = "<script>alert('支付失败！如有问题请与客服联系！');</script>";
                    Response.Redirect("PayPErr.aspx?err=104");
                }
            }
            else
            {
                //sMsg = "<script>alert('验证失败！如有问题请与客服联系！');</script>";
                Response.Redirect("PayPErr.aspx?err=103");
            }
        }
    }
}

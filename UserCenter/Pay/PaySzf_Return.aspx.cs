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
    public partial class PaySzf_Return : System.Web.UI.Page
    {
        protected string sMsg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string lbVersion = CYRequest.GetString("version"); //版本号
            string lbMerId = CYRequest.GetString("merId"); //商户ID
            string lbPayMoney = CYRequest.GetString("payMoney"); //支付金额
            string lbOrderId = CYRequest.GetString("orderId"); //订单号
            string lbPayResult = CYRequest.GetString("payResult"); //支付结果
            string lbPrivateField = CYRequest.GetString("privateField");
            string lbPayDetails = CYRequest.GetString("payDetails");
            string lbMd5String = CYRequest.GetString("md5String"); //MD5校验串
            string lbSignString = CYRequest.GetString("signString"); //神州付支付系统对md5加密后的32位字符串(md5String)进行签名。

            /*
             * MD5校验
             * md5String =md5(version+merId+payMoney+orderId+payResult+privateField+payDetails+privateKey)
             */
            StringBuilder sbText = new StringBuilder();
            sbText.Append(lbVersion);
            sbText.Append(lbMerId);
            sbText.Append(lbPayMoney);
            sbText.Append(lbOrderId);
            sbText.Append(lbPayResult);
            sbText.Append(lbPrivateField);
            sbText.Append(lbPayDetails);
            sbText.Append(SzfPayBuy.GetKey());
            String md5 = ProvideCommon.MD5(sbText.ToString()).ToLower();

            if (md5.Equals(lbMd5String))
            {
                if (SzfPayBuy.veriSig(md5, lbSignString))
                {
                    if ("1".Equals(lbPayResult))
                    {
                        //支付成功
                        int iLen = lbPrivateField.Split('|').Length;
                        string sAccount = lbPrivateField.Split('|')[0];//获取充值人账户
                        decimal dPrice = Convert.ToDecimal(lbPayMoney) / 100;
                        int j = TransPBLL.PointSalesCommit(lbOrderId, sAccount, dPrice);    //确认返回信息无误后提交此定单
                        if (j == 0)
                        {
                            //游戏直冲
                            if (iLen > 1)
                            {
                                TranQuickBLL.TranQuickUpdateP(lbOrderId);
                                string sGTranID = TranQuickBLL.TranQuickGTranIDSel(lbOrderId);
                                int iChannelID = TransPBLL.TranPSelChannelIDByID(lbOrderId);
                                dPrice = dPrice * ChannelBLL.FeeScaleSelByID(iChannelID);
                                string sGame = lbPrivateField.Split('|')[1];
                                string sGTRes = string.Empty;
                                if (sGame.IndexOf("sq") == -1)
                                {
                                    sGTRes = PayAll.GameQuickPay(sGame, sAccount, dPrice, sGTranID);
                                }
                                else
                                { 
                                    string sRoleID = lbPrivateField.Split('|')[2];
                                    sGTRes = PayAll.sqQuickPay(sGame, sAccount, dPrice, sGTranID,sRoleID);   
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
                            else if (1 == iLen)
                            {
                                Response.Redirect(string.Format("PayPSucc.aspx?TranID={0}", lbOrderId));    //转向银行卡支付成功页面
                            }
                        }
                        else
                        {
                            if (6 == j)
                            {
                                if (iLen > 1)
                                {
                                    string sQuickState = TranQuickBLL.TransQuickStateSelByP(lbOrderId);
                                    string sGTranID = TranQuickBLL.TranQuickGTranIDSel(lbOrderId);
                                    string sGame = lbPrivateField.Split('|')[1];
                                    if (sQuickState == "2")
                                    {
                                        Response.Redirect(string.Format("PayGSucc.aspx?TranID={0}&gn={1}&type=q", sGTranID, sGame), false);
                                    }
                                    else if (sQuickState == "1")
                                    {
                                        int iChannelID = TransPBLL.TranPSelChannelIDByID(lbOrderId);
                                        dPrice = dPrice * ChannelBLL.FeeScaleSelByID(iChannelID);
                                        string sGTRes = string.Empty;
                                        if (sGame.IndexOf("sq") == -1)
                                        {
                                            sGTRes = PayAll.GameQuickPay(sGame, sAccount, dPrice, sGTranID);
                                        }
                                        else
                                        {
                                            string sRoleID = lbPrivateField.Split('|')[2];
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
            }
            else
            {
                //sMsg = "<script>alert('验证失败！如有问题请与客服联系！');</script>";
                Response.Redirect("PayPErr.aspx?err=103");
            }
        }
    }
}

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
    public partial class PaySzf_Notify : System.Web.UI.Page
    {
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
                                string sGame = lbPrivateField.Split('|')[1];
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
                                }
                            }
                        }
                    }
                    Response.Write(lbOrderId);
                }
            }
            else
            {
                Response.Write("验证失败");
            }
        }
    }
}

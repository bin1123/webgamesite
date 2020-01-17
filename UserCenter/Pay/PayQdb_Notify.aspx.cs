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
    public partial class PayQdb_Notify : System.Web.UI.Page
    {
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
                            string sGame = remark.Split('|')[2];
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
                                Response.Write("0");
                            }
                            else 
                            {
                                Response.Write("5");
                            }
                        }
                        else
                        {
                            Response.Write("4");
                        }
                    }
                    else
                    {
                        Response.Write("3");
                    }
                }
                else
                {
                    Response.Write("2");
                }
            }
            else
            {
                Response.Write(string.Format("1|{0}:{1}",sign,md5));
            }
        }
    }
}

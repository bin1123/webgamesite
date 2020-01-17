using System;
using System.Web;
using System.Web.Services;

using Common;
using Bussiness;

namespace UserCenter.Pay
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class BankPayMulti : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST")
            {
                string sFromHost = context.Request.UrlReferrer.Host;
                context.Response.Cookies["fromhost"].Value = sFromHost;
                context.Response.Cookies["fromhost"].Expires = DateTime.Now.AddHours(1);
                string sAccount = CYRequest.GetFormString("bankaccount");//充值账号
                string sPhone = CYRequest.GetFormString("bankphonenum");
                string sPayNums = CYRequest.GetFormString("bankpayprice");//充值金额
                int iUserID = UserBll.UserIDSel(sAccount);
                if (iUserID < 1000)
                {
                    context.Response.Write("<script>alert('充值账号不存在！');</script>");
                    return;
                }
                else
                {
                    decimal dPrice = 0;
                    decimal.TryParse(sPayNums, out dPrice);
                    if (dPrice < 10)
                    {
                        context.Response.Redirect("PayPErr.aspx?err=204");
                        return;
                    }
                    string sChannel = CYRequest.GetFormString("bankchannel");
                    string sBankName = string.Empty;
                    if (sChannel == "ibank")
                    {
                        sBankName = CYRequest.GetFormString("bankname");
                    }
                    int iCount = 1;
                    string sPayDirect = string.Empty;
                    if (sChannel == "tenpay")
                    {
                        sPayDirect = TenPayBuy.PayBegin(sChannel,sPhone,sAccount,dPrice,iCount,context);
                    }
                    else if (sChannel == "szfphone")
                    {
                        string cardTypeCombine = CYRequest.GetString("cardTypeCombine");
                        sPayDirect = SzfPayBuy.PayBegin(sChannel, sPhone, sAccount, dPrice, iCount, "0", cardTypeCombine);
                    }
                    else
                    {
                        sPayDirect = PayAll.CreatePay(sChannel, sPhone, sAccount, dPrice, iCount, sBankName);
                    }
                    context.Response.Write(sPayDirect);
                    return;
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

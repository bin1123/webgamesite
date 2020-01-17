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
    public class QuickPayMulti : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST")
            {
                string sFromHost = context.Request.UrlReferrer.Host;
                context.Response.Cookies["fromhost"].Value = sFromHost;
                context.Response.Cookies["fromhost"].Expires = DateTime.Now.AddHours(1);
                string sAccount = CYRequest.GetFormString("quickaccount");//充值账号
                int iUserID = UserBll.UserIDSel(sAccount);
                if (iUserID < 1000)
                {
                    context.Response.Write("<script>alert('充值账号不存在！');</script>");
                    return;
                }

                string sPayNums = CYRequest.GetFormString("quickpayprice");//充值金额
                decimal dPrice = 0;
                decimal.TryParse(sPayNums, out dPrice);
                if (dPrice < 10)
                {
                    context.Response.Redirect("PayPErr.aspx?err=204");
                    return;
                }

                string sGameAbbre = CYRequest.GetFormString("quickservername");
                string sGameIsLogin = PayAll.ValUserLoginGame(sGameAbbre, iUserID.ToString());
                if ("1" == sGameIsLogin)
                {
                    context.Response.Redirect("PayGErr.aspx?err=202");
                    return;
                }

                string sGameAbbreC = string.Empty;
                if (sGameAbbre.IndexOf("sq") == -1)
                {
                    sGameAbbreC = sGameAbbre;
                }
                else
                {
                    string sRoleID = CYRequest.GetFormString("quickrole");
                    sGameAbbreC = string.Format("{0}|{1}",sGameAbbre,sRoleID);
                }
                string sPhone = CYRequest.GetFormString("quickphone");
                string sChannel = CYRequest.GetFormString("quickchannel");
                int iCount = 1;
                string sBankName = string.Empty;
                if (sChannel == "ibank")
                {
                    sBankName = CYRequest.GetFormString("quickbank");
                }
                string sPayDirect = string.Empty;
                if (sChannel == "tenpay")
                {
                    string sTranIP = ProvideCommon.GetRealIP();
                    string sPTranID = TransPBLL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount,sTranIP);
                    decimal dFeeScale = ChannelBLL.FeeScaleSel(sChannel);
                    int iGamePoints = System.Convert.ToInt32(dPrice * 10 * dFeeScale);
                    int iPayUserID = UserBll.UserIDSel(sAccount);
                    string sGTranID = TransGBLL.GameSalesInit(sGameAbbre, iGamePoints, sAccount, sPhone, iPayUserID,sTranIP);
                    TranQuickBLL.TranQuickAdd(sGTranID, sPTranID);
                    sPayDirect = TenPayBuy.QuickPayBegin(sPTranID, sAccount, dPrice, sGameAbbreC, context);
                }
                else if (sChannel == "szfphone")
                {
                    string sTranIP = ProvideCommon.GetRealIP();
                    string sPTranID = TransPBLL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount, sTranIP);
                    decimal dFeeScale = ChannelBLL.FeeScaleSel(sChannel);
                    int iGamePoints = System.Convert.ToInt32(dPrice * 10 * dFeeScale);
                    int iPayUserID = UserBll.UserIDSel(sAccount);
                    string sGTranID = TransGBLL.GameSalesInit(sGameAbbre, iGamePoints, sAccount, sPhone, iPayUserID, sTranIP);
                    TranQuickBLL.TranQuickAdd(sGTranID, sPTranID);
                    string cardTypeCombine = CYRequest.GetFormString("quickcardTypeCombine");
                    sPayDirect = SzfPayBuy.QuickPayBegin(sPTranID, sAccount, dPrice, sGameAbbreC, "0", cardTypeCombine);
                }
                else
                {
                    sPayDirect = PayAll.QuickPay(sChannel, sPhone, sAccount, dPrice, iCount, sBankName,sGameAbbreC);
                }
                context.Response.Write(sPayDirect);
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

using System;
using System.Web;
using System.Web.Services;
using Bussiness;

using Common;

namespace UserCenter.Pay
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class YPay : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if(context.Request.HttpMethod == "POST")
            {
                string sChannle = CYRequest.GetFormString("Channel");
                string sPhone = CYRequest.GetFormString("Phone");
                string sAccount = CYRequest.GetFormString("Account");
                string sPrice = CYRequest.GetFormString("Price");
                decimal dPrice = 0;
                decimal.TryParse(sPrice, out dPrice);
                string sCount = CYRequest.GetFormString("Count");
                int iCount = 0;
                int.TryParse(sCount,out iCount);
                string sGame = CYRequest.GetFormString("Game");
                string sUrl = string.Empty;
                if (sGame == "" || sGame == "unsafe string")
                {
                    sUrl = YeePayBuy.PayBegin(sChannle, sPhone, sAccount, dPrice, iCount);
                }
                else
                {
                    string sGameName = sGame.Split('|')[0];
                    string sTranIP = ProvideCommon.GetRealIP();
                    string sPTranID = TransPBLL.PointSalesInit(sChannle, sPhone, sAccount, dPrice, iCount,sTranIP);//订单号
                    int iPayUserID = UserBll.UserIDSel(sAccount);
                    decimal dFeeScale = ChannelBLL.FeeScaleSel(sChannle);
                    //int iPrice = Convert.ToInt32(dPrice);
                    int iGamePoints = Convert.ToInt32(dPrice * 10 * dFeeScale);
                    string sGTranID = TransGBLL.GameSalesInit(sGameName,iGamePoints,sAccount,sPhone,iPayUserID,sTranIP);
                    TranQuickBLL.TranQuickAdd(sGTranID, sPTranID);
                    sUrl = YeePayBuy.QuickPayBegin(sPTranID, sChannle, sAccount, dPrice, sGame);
                }
                context.Response.Redirect(sUrl,true);
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

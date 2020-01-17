using System.Web;
using System.Web.Services;
using System.Text;

using Common;
using Bussiness;

namespace UserCenter.Pay
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class QuickPayP : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST")
            {
                string sAccount = CYRequest.GetFormString("quickaccount");//充值账号
                string sPid = CYRequest.GetFormString("pid");
                int iPid = 0;
                int.TryParse(sPid,out iPid);
                string sUserName = PartnerUserBLL.PartnerUserNameGet(sAccount, iPid);
                string sPhone = CYRequest.GetFormString("quickphone");
                string sPayNums = CYRequest.GetFormString("quickpayprice");//充值金额
                string sServerName = CYRequest.GetFormString("quickservername");//充值金额
                string sChannel = CYRequest.GetFormString("quickchannel");//充值金额
                string sBank = CYRequest.GetFormString("quickbank");//充值金额
                string sCardType = CYRequest.GetFormString("quickcardTypeCombine");
                StringBuilder sbText = new StringBuilder(200);
                sbText.AppendFormat("<form id='quickpay' name='quickpay' action='{0}' method='post'>", "QuickPay.ashx");
                sbText.AppendFormat("<input type='hidden' name='quickaccount' value='{0}'/>", sUserName);
                sbText.AppendFormat("<input type='hidden' name='quickphone' value='{0}'/>", sPhone);
                sbText.AppendFormat("<input type='hidden' name='quickpayprice' value='{0}'/>", sPayNums);
                sbText.AppendFormat("<input type='hidden' name='quickservername' value='{0}'/>", sServerName);
                sbText.AppendFormat("<input type='hidden' name='quickchannel' value='{0}'/>", sChannel);
                sbText.AppendFormat("<input type='hidden' name='quickbank' value='{0}'/>", sBank);
                sbText.AppendFormat("<input type='hidden' name='quickcardTypeCombine' value='{0}'/>", sCardType);
                //submit按钮控件请不要含有name属性
                sbText.Append("<input type='submit' value='submit' style='display:none;'></form>");
                sbText.Append("<script>document.forms['quickpay'].submit();</script>");
                context.Response.Write(sbText);
                return;
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

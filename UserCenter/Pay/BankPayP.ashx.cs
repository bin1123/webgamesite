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
    public class BankPayP : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST")
            {
                string sAccount = CYRequest.GetFormString("bankaccount");//充值账号
                string sPid = CYRequest.GetFormString("pid");
                int iPid = 0;
                int.TryParse(sPid, out iPid);
                string sUserName = PartnerUserBLL.PartnerUserNameGet(sAccount, iPid);
                string sPhone = CYRequest.GetFormString("bankphonenum");
                string sPayNums = CYRequest.GetFormString("bankpayprice");//充值金额
                string bankchannel = CYRequest.GetFormString("bankchannel");
                string bankname = CYRequest.GetFormString("bankname");
                string cardTypeCombine = CYRequest.GetString("cardTypeCombine");
                StringBuilder sbText = new StringBuilder(200);
                sbText.AppendFormat("<form id='bankpay' name='bankpay' action='{0}' method='post'>", "BankPay.ashx");
                sbText.AppendFormat("<input type='hidden' name='bankaccount' value='{0}'/>", sUserName);
                sbText.AppendFormat("<input type='hidden' name='bankphonenum' value='{0}'/>", sPhone);
                sbText.AppendFormat("<input type='hidden' name='bankpayprice' value='{0}'/>", sPayNums);
                sbText.AppendFormat("<input type='hidden' name='bankchannel' value='{0}'/>", bankchannel);
                sbText.AppendFormat("<input type='hidden' name='bankname' value='{0}'/>", bankname);
                sbText.AppendFormat("<input type='hidden' name='cardTypeCombine' value='{0}'/>", cardTypeCombine);
                //submit按钮控件请不要含有name属性
                sbText.Append("<input type='submit' value='submit' style='display:none;'></form>");
                sbText.Append("<script>document.forms['bankpay'].submit();</script>");
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

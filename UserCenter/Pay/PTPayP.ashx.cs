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
    public class PTPayP : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST")
            {
                string sAccount = CYRequest.GetFormString("gameaccount");//充值账号
                string sPid = CYRequest.GetFormString("pid");
                int iPid = 0;
                int.TryParse(sPid, out iPid);
                string sUserName = PartnerUserBLL.PartnerUserNameGet(sAccount, iPid);
                string sPhone = CYRequest.GetFormString("gamephone");
                string sPayNums = CYRequest.GetFormString("gamepaynums");//充值金额
                string sServername = CYRequest.GetFormString("gameservername");//充值金额
                StringBuilder sbText = new StringBuilder(200);
                sbText.AppendFormat("<form id='ptpay' name='ptpay' action='{0}' method='post'>", "PTPay.aspx");
                sbText.AppendFormat("<input type='hidden' name='gameaccount' value='{0}'/>", sUserName);
                sbText.AppendFormat("<input type='hidden' name='gamephone' value='{0}'/>", sPhone);
                sbText.AppendFormat("<input type='hidden' name='gamepaynums' value='{0}'/>", sPayNums);
                sbText.AppendFormat("<input type='hidden' name='gameservername' value='{0}'/>", sServername);
                //submit按钮控件请不要含有name属性
                sbText.Append("<input type='submit' value='submit' style='display:none;'></form>");
                sbText.Append("<script>document.forms['ptpay'].submit();</script>");
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

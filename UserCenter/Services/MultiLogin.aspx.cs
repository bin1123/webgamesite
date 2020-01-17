using System;
using System.Configuration;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    public partial class MultiLogin : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.HttpMethod == "POST")
            {
                string username = CYRequest.GetFormString("account");
                string password = CYRequest.GetFormString("pwdone");
                string fromurl = CYRequest.GetFormString("url");
                string sErrMsg = UserBll.LoginCheck(username, password);
                string sGoUrl = string.Empty;
                string sHost = string.Empty;
                if (fromurl.Length > 4)
                {
                    sGoUrl = fromurl;
                    sHost = ProvideCommon.getHost(fromurl);
                }
                else
                {
                    sGoUrl = Request.UrlReferrer.ToString();
                    sHost = Request.UrlReferrer.Host;
                }
                if(sErrMsg.Length > 0)
                {
                    Response.Write(string.Format("{0}<script>location.href='{1}'</script>",sErrMsg,sGoUrl));
                    return;
                }
                if (UserBll.UserAllVal(username, password))
                {
                    string sPageUrl = Request.Url.ToString();
                    int iUserID = UserBll.UserIDSel(username);
                    LoginStateSet(username, iUserID, sPageUrl);
                    //string sTypeID = string.Empty;
                    //switch (sHost)
                    //{
                    //    case "www.wanyouxi123.com":
                    //        sTypeID = "20";
                    //        break;
                    //    case "www.99wanyouxi.com":
                    //        sTypeID = "21";
                    //        break;
                    //}
                    //SetUserType(sTypeID);
                    int iPoints = GetUPoints();
                    string sMultiPP = ProvideCommon.getMultiPP(iUserID);
                    string sReturnUrl = string.Format("http://{0}/usercookie.aspx?un={1}&point={2}&GoUrl={3}&pp={4}", sHost, username, iPoints.ToString(), sGoUrl,sMultiPP);
                    Response.Redirect(sReturnUrl, true);
                    return;
                }
                else
                {
                    StringBuilder sbHtml = new StringBuilder();
                    sbHtml.Append("<script>alert('账号信息输入错误！');</script>");
                    sbHtml.AppendFormat("<script>location.href='{0}'</script>", sGoUrl); 
                    Response.Write(sbHtml.ToString());
                    return;
                }
            }
        }
    }
}

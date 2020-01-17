using System;
using System.Configuration;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    public partial class loginform : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.HttpMethod == "POST")
            {
                string username = CYRequest.GetFormString("account");
                string password = CYRequest.GetFormString("pwdone");
                string url = CYRequest.GetFormString("url");                
                StringBuilder sbHtml = new StringBuilder();
                string sErrMsg = "";//UserBll.LoginVal(username, password);
                if(sErrMsg.Length > 0)
                {
                    sbHtml.AppendFormat("<script>alert('{0}');</script>",sErrMsg);
                }
                else if (UserBll.UserAllVal(username, password))
                {
                    string sPageUrl = Request.Url.ToString();
                    int iUserID = UserBll.UserIDSel(username);
                    LoginStateSet(username, iUserID, sPageUrl);
                    string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                    string sBBSUrl = DiscuzUserI.BBSLogin(username, password, sKey);
                    sbHtml.Append(string.Format("<script src='{0}'></script>", sBBSUrl));
                    //string sCUrl = WebConfig.BaseConfig.sWUrl;
                    //string sHost = ProvideCommon.getHost(url);
                    //if(sHost.Length < 5)
                    //{
                    //    sHost = Request.UrlReferrer.Host;
                    //}
                    string sHost = "www.dao50.com";
                    int iPoints = GetUPoints();
                    string sJSUrl = string.Format("http://{0}/{1}?un={2}&point={3}", sHost, "usercookie.aspx", username,iPoints);                  
                    sbHtml.Append(string.Format("<script src='{0}'></script>", sJSUrl));
                }
                else
                {
                    sbHtml.Append("<script>alert('账号信息输入错误！');</script>");
                }
                if (url != "")
                {
                    sbHtml.AppendFormat("<script>location.href='{0}'</script>", url);
                }
                else
                {
                    string sWUrl = WebConfig.BaseConfig.sWUrl;
                    sbHtml.AppendFormat("<script>location.href='{0}'</script>", sWUrl);
                }
                Response.Write(sbHtml.ToString());
            }
        }
    }
}

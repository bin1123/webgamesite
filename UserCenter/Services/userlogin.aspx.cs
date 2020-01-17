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

namespace UserCenter.Services
{
    public partial class userlogin : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.HttpMethod == "POST")
            {
                string sWebUrl = WebConfig.BaseConfig.sWUrl;
                string username = CYRequest.GetString("un");
                string password = CYRequest.GetString("pwd");
                string url = CYRequest.GetString("url");
                string nusername = GetAccount();
                StringBuilder sbHtml = new StringBuilder();
                string sWWWUrl = string.Format("{0}/{1}?un={2}", sWebUrl, "usercookie.aspx", username);
                string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                string sBBSUrl = DiscuzUserI.BBSLogin(username, password, sKey);
                string sJSUrl = string.Format("<script src='{0}'></script><script src='{1}'></script>", sBBSUrl, sWWWUrl);
                if (username == nusername && nusername != "" && username != "unsafe string")
                {
                    //写入cookie
                    sbHtml.AppendFormat("<script src='{0}'></script>", sJSUrl);
                }
                else
                { 
                    //验证账号合法性
                    if (UserBll.UserAllVal(username, password))
                    {
                        //写入cookie
                        sbHtml.AppendFormat("<script src='{0}'></script>", sJSUrl);
                        string sPageUrl = Request.Url.ToString();
                        int iUserID = UserBll.UserIDSel(username);
                        LoginStateSet(username, iUserID, sPageUrl);
                    }
                    else
                    {
                        sbHtml.Append("<script>alert('账号信息输入错误！')</script>");
                    }
                }
                if (url != "" && url != "unsafe string")
                {
                    sbHtml.AppendFormat("<script>location.href='{0}'</script>", url);
                }
                else
                {
                    sbHtml.AppendFormat("<script>location.href='{0}'</script>", sWebUrl);
                }
                Response.Write(sbHtml.ToString());
            }
        }
    }
}

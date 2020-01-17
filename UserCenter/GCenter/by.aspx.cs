using System;
using System.Configuration;

using Common;
using Bussiness;
using DataEnity;

namespace UserCenter.GCenter
{
    public partial class by : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sAccount = string.Empty;
        protected string sMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                int iUserID = GetUserID();
                string sGameAbbre = CYRequest.GetQueryString("gn");
                if(iUserID > 999 & sGameAbbre.Length > 2 && sGameAbbre.IndexOf("by") == 0)
                {
                    string sGameUrl = byGame.Login(iUserID.ToString(), sGameAbbre);
                    Response.Redirect(sGameUrl,true);
                }
                sAccount = GetAccount();                
            }
            else
            {
                Server.Transfer("byLogin.aspx");
            }
        }
    }
}

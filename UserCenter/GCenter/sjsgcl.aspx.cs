using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class sjsgcl : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sAccount = string.Empty;
        protected string sHref = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                sAccount = GetAccount();
            }
            else
            {
                Server.Transfer("sjsgLogin.aspx",false);
            }
        }
    }
}

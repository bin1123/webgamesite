using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class ktpdcl : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
           string sAccount = GetAccount().Trim();
            if (sAccount == null || sAccount.Length < 4)
            {
                Server.Transfer("ktpdlogin.aspx", false);
            }
            //if (LoginSessionVal() == false || isLoginCookie() == false)
            //{
            //    Server.Transfer("ktpdlogin.aspx", false);
            //}
        }
    }
}

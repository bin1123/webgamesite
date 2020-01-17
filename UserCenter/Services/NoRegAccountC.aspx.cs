using System;

using Common;

namespace UserCenter.Services
{
    public partial class NoRegAccountC : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
               string sAccount = CYRequest.GetQueryString("un");
               if(sAccount.Length > 3)
               {
                   setaccount(sAccount);
               }
            }
        }
    }
}

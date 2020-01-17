using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class lj2 : pagebase.PageBase
    {
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                string sGameAbbre = CYRequest.GetString("gn");
                if (!ProvideCommon.valTime(DateTime.Now.ToString(), ServerBLL.ServerTimeSel(sGameAbbre)))
                {
                    Response.Redirect(string.Format("{0}/jjkf", sWUrl), true);
                    return;
                }
                int iUserID = GetUserID();
                string isLocal = "1";
                if(iUserID > 999)
                {
                    string sGameLoginUrl = lj2Game.Login(iUserID.ToString(), sGameAbbre, isLocal);
                    Response.Redirect(sGameLoginUrl,true);
                }
            }
            else
            {
                Server.Transfer("lj2login.aspx",false);
            }
        }
    }
}

using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class sjsg : pagebase.PageBase
    {
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                string sGameAbbre = CYRequest.GetString("gn");
                if (!ProvideCommon.valTime(DateTime.Now.ToString(), ServerBLL.ServerTimeSel(sGameAbbre)))
                {
                    Response.Write("1");
                    return;
                }
                int iUserID = GetUserID();
                if(iUserID > 999)
                {
                    string sUserID = iUserID.ToString();
                    string sGameLoginUrl = string.Format("app://loadgame:{0},{1}|{2}&from_launcher=1",sUserID,sGameAbbre, sjsgGame.Login(sUserID, sGameAbbre));
                    if (GameLogin(sGameAbbre))
                    {
                        GameLoginBLL.GameLoginAdd(iUserID, sGameAbbre, ProvideCommon.GetRealIP(), sGameLoginUrl);
                    }
                    Response.Write(sGameLoginUrl);
                }
            }
            else
            {
                Response.Write("2");
            }
        }
    }
}

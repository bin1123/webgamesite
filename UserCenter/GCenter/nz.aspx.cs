using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class nz : pagebase.PageBase
    {
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                string sGameAbbre = CYRequest.GetString("gn");
                if (!ProvideCommon.valTime(DateTime.Now.ToString(), ServerBLL.ServerTimeSel(sGameAbbre)))
                {
                    Response.Write("<script>alert('即将开服，敬请期待！');location.href='nzLogin.aspx';</script>");
                    return;
                }
                int iUserID = GetUserID();
                string sUrl = nzGame.Login(iUserID.ToString(), sGameAbbre,"pc");
                if (GameLogin(sGameAbbre))
                {
                    GameLoginBLL.GameLoginAdd(iUserID, sGameAbbre, ProvideCommon.GetRealIP(), sUrl);
                }
                Response.Redirect(sUrl,false);
                return;
            }
            else
            {
                Server.Transfer("nzLogin.aspx", false);
            }
        }
    }
}

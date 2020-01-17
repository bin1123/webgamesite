using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class jdsj : pagebase.PageBase
    {
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                string sGameAbbre = CYRequest.GetString("gn");
                if (!ProvideCommon.valTime(DateTime.Now.ToString(), ServerBLL.ServerTimeSel(sGameAbbre)))
                {
                    Response.Write("<script>alert('即将开服，敬请期待！');location.href='jdsjcl.aspx';</script>");
                    return;
                }
                int iUserID = GetUserID();
                string sUrl = jdsjGame.Login(iUserID.ToString(), sGameAbbre);
                if (GameLogin(sGameAbbre))
                {
                    GameLoginBLL.GameLoginAdd(iUserID, sGameAbbre, ProvideCommon.GetRealIP(), sUrl);
                }
                Response.Redirect(sUrl,false);
                return;
            }
            else
            {
                Server.Transfer("jdsjcl.aspx", false);
            }
        }
    }
}

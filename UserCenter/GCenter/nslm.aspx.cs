using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class nslm : pagebase.PageBase
    {
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                //if (!ProvideCommon.valTime(DateTime.Now.ToString(), ServerBLL.ServerTimeSel(sGameAbbre)))
                //{
                //    Response.Write("<script>alert('即将开服，敬请期待！');location.href='nslmcl.aspx';</script>");
                //    return;
                //}
                string sUserIP = ProvideCommon.GetRealIP();
                if (!ProvideCommon.GameIPVal(sUserIP))
                {
                    return;
                }
                int iUserID = GetUserID();
                DateTime dtLoginTime = GetLoginTime();
                if (!PWDUpdateBLL.PwdUpdateVal(iUserID, dtLoginTime))
                {
                    ClearUsersInfo();
                    string sJs = "<script>alert('密码已改，请重新登陆！');location.href='nslmc.html';</script>";
                    Response.Write(sJs);
                    return;
                }
                string sGameAbbre = CYRequest.GetString("gn");
                string sUrl = nslmGame.Login(iUserID.ToString(), sGameAbbre);
                if (GameLogin(sGameAbbre))
                {
                    GameLoginBLL.GameLoginAdd(iUserID, sGameAbbre, ProvideCommon.GetRealIP(), sUrl);
                }
                Response.Redirect(sUrl,false);
                return;
            }
            else
            {
                Server.Transfer("nslmcl.aspx", false);
            }
        }
    }
}

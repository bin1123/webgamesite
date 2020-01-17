using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class zsgplay : pagebase.PageBase
    {
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            { 
                string sGameAbbre = CYRequest.GetString("gn");
                int iUserID = GetUserID();
                string sUrl = zsgGame.Login(iUserID.ToString(), sGameAbbre); 
                if (GameLogin(sGameAbbre))
                {
                    GameLoginBLL.GameLoginAdd(iUserID, sGameAbbre, ProvideCommon.GetRealIP(), sUrl);
                }
                Response.Write(string.Format("<script>location.href='{0}'</script>",sUrl));
                return;
            }
            else
            {
                Server.Transfer("zsgcl.aspx",false);
            }
        }
    }
}

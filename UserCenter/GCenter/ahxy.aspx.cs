using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class ahxy : pagebase.PageBase
    {
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                string sGameAbbre = CYRequest.GetString("gn");
                if (!ProvideCommon.valTime(DateTime.Now.ToString(), ServerBLL.ServerTimeSel(sGameAbbre)))
                {
                    Response.Write("<script>alert('即将开服，敬请期待！');location.href='ahxycl.aspx';</script>");
                    return;
                }
                int iUserID = GetUserID();
                if(iUserID > 999)
                {
                    string sUserID = iUserID.ToString();
                    string sServerID = sGameAbbre.Replace("ahxy","");
                    string sServerName = ServerBLL.ServerNameSelByAbbre(sGameAbbre);
                    string sGameLoginUrl = string.Format("app://loadgame:{0}服-{1}-{2}|{3}&from_launcher=1",
                                                          sServerID,sServerName,sUserID, ahxyGame.Login(sUserID, sGameAbbre));
                    if (GameLogin(sGameAbbre))
                    {
                        GameLoginBLL.GameLoginAdd(iUserID, sGameAbbre, ProvideCommon.GetRealIP(), sGameLoginUrl);
                    } 
                    Response.Write(string.Format("<script>window.location ='{0}';</script>",sGameLoginUrl));
                }
            }
            else
            {
                Response.Write("<script>alert('用户状态不存在，请登陆!');location.href='ahxycl.aspx';</script>");
                return;
            }
        }
    }
}

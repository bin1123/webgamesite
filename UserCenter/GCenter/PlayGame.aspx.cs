using System;
using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class PlayGame : pagebase.PageBase
    {
        protected string sTitle = string.Empty;
        protected string ssTitle = string.Empty;
        protected string sUrl = string.Empty;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;
        protected string sGameSite = string.Empty;
        protected string sGameName = string.Empty;
        protected int iWidth = 1000;
        protected int iHeight = 600;

        protected void Page_Load(object sender, EventArgs e)
        {
            string ip = ProvideCommon.GetRealIP();
            sGameName = CYRequest.GetString("gn");
            if (sGameName == "" || sGameName == "unsafe string")
            {
                Response.Redirect(string.Format("{0}/yxzx", sWUrl), false);
            }
            else
            {
                if (LoginSessionVal() || isLoginCookie())
                {
                    int iUserID = GetUserID();
                    string sUserID = string.Empty;
                    string sAccount = GetAccount();
                    if (!ValUserState(iUserID, sAccount))
                    {
                        iUserID = UserBll.UserIDSel(sAccount);
                        if (iUserID < 1000 || (!ValUserState(iUserID, sAccount)))
                        {
                            Response.Write(string.Format("<script>alert('用户名:{2}与数字ID:{3}不一致，请重新登录！谢谢！');location.href='{0}/Default.aspx?gn={1}';</script>", sRootUrl, sGameName, sAccount, iUserID));
                            ClearUsersInfo();
                            return;
                        }
                    }
                    else
                    {
                        DateTime dtLoginTime = GetLoginTime();
                        if (!PWDUpdateBLL.PwdUpdateVal(iUserID, dtLoginTime))
                        {
                            ClearUsersInfo();
                            Response.Redirect(string.Format("{0}/Default.aspx", sRootUrl), true);
                            return;
                        }
                    }

                    int iUserPoints = GetUPoints();
                    if(iUserPoints > 0)
                    {
                        UserPointsBLL.UPointCheck(iUserID);
                    }

                    sUserID = iUserID.ToString();
                    string sGame = GameInfoBLL.GameInfoAbbreSel(sGameName).TrimEnd();
                    switch (sGame)
                    {
                        case "sssg":
                            string sSource = string.Empty;
                            string client = string.Empty;
                            if (CYRequest.GetString("client") != "pc")
                            {
                                client = "web";
                            }
                            else
                            {
                                client = CYRequest.GetString("client");
                            }
                            sUrl = sssgGame.Login(sUserID, sGameName, sSource, client);
                            if (GameLogin(sGameName))
                            {
                                GameLoginBLL.GameLoginAdd(iUserID, sGameName, ProvideCommon.GetRealIP(), sUrl);
                            }
                            if (client == "pc")
                            {
                                Response.Redirect(sUrl, true);
                                return;
                            }
                            else
                            {
                                sGameName = string.Empty;
                            }
                            break;
                        case "sxd":
                            sUrl = sxdGame.Login(sUserID, sGameName, "");
                            iHeight = 635;
                            if (GameLogin(sGameName))
                            {
                                GameLoginBLL.GameLoginAdd(iUserID, sGameName, ProvideCommon.GetRealIP(), sUrl);
                            }
                            sGameName = string.Empty;
                            break;
                        default:
                            Response.Redirect(string.Format("wan.aspx?gn={0}", sGameName), true);
                            break;
                    }
                    sTitle = ServerBLL.ServerTitleSel(CYRequest.GetString("gn"));
                    if (sTitle.Length > 11)
                    {
                        ssTitle = sTitle.Substring(0, 11);
                    }
                    else
                    {
                        ssTitle = sTitle;
                    }
                }
                else
                {
                    Response.Write(string.Format("<script>alert('用户状态有误或没有登录，请重新登录！谢谢！');location.href='{0}/Default.aspx?gn={1}';</script>", sRootUrl, sGameName));
                }
            }
        }
    }
}

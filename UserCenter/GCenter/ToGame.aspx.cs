using System;
using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class ToGame : pagebase.PageBase
    {
        protected string sUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //直接跳转游戏
            if (!Page.IsPostBack)
            {
                string sGameName = CYRequest.GetString("gn");
                if (sGameName.Length < 2)
                {
                    Response.Redirect(string.Format("http://www.dao50.com/yxzx/?gn={0}", sGameName), false);
                }
                else if (LoginSessionVal() || isLoginCookie())
                {
                    int iUserID = GetUserID();
                    string sUserID = string.Empty;
                    string sAccount = GetAccount();
                    if (!ValUserState(iUserID, sAccount))
                    {
                        iUserID = UserBll.UserIDSel(sAccount);
                        if (iUserID < 1000 || (!ValUserState(iUserID, sAccount)))
                        {
                            //Response.Write(string.Format("<script>alert('用户名:{2}与数字ID:{3}不一致，请重新登录！谢谢！');location.href='{0}/Default.aspx?gn={1}';</script>", sRootUrl, sGameName, sAccount, iUserID));
                            ClearUsersInfo();
                            return;
                        }
                    }
                    string sUserIP = ProvideCommon.GetRealIP();
                    if(!ProvideCommon.GameIPVal(sUserIP))
                    {
                        return;
                    }
                    string sStartTime = DateTime.Now.ToString();
                    string sEndTime = ServerBLL.ServerTimeSel(sGameName);
                    string sGame = GameInfoBLL.GameInfoAbbreSel(sGameName).TrimEnd();
                    sUserID = iUserID.ToString();
                    if (!ProvideCommon.valTime(sStartTime, sEndTime))
                    {
                        if (!UserBll.AdminUserVal(iUserID))
                        {
                            string sWUrl = WebConfig.BaseConfig.sWUrl;
                            Response.Redirect(string.Format("{0}/jjkf", sWUrl), true);
                            return;
                        }
                    }
                    else if(sGame == "dxz")
                    { 
                        int iAddDay = 15;//过期时间
                        if(!ProvideCommon.SeverTimeVal(sEndTime,iAddDay) && !TransPBLL.UserIsTranVal(iUserID) && !dxzGame.GameisLoginVal(sUserID,sGameName))
                        {
                            //Response.Write(dxzGame.GameisLoginTest(sUserID,sGameName));
                            return;
                        }
                    }
                    switch (sGame)
                    {
                        case "lj":
                            sUrl = ljGame.Login(sUserID, sGameName);
                            break;
                        case "yjxy":
                            sUrl = yjxyGame.Login(sUserID, sGameName);
                            break;
                        case "sq":
                            sUrl = sqGame.Login(sUserID, sGameName, false);
                            break;
                        case "dxz":
                            sUrl = dxzGame.Login(sUserID, sGameName);
                            break;
                        case "djj":
                            sUrl = djjGame.Login(sUserID, sGameName);
                            break;
                        case "txj":
                            sUrl = txjGame.Login(sUserID, sGameName);
                            break;
                        case "sjsg":
                            sUrl = sjsgGame.Login(sUserID, sGameName);
                            break;
                        case "tzcq":
                            sUrl = tzcqGame.Login(sUserID, sGameName);
                            break;
                        case "by":
                            sUrl = byGame.Login(sUserID, sGameName);
                            break;
                        case "swjt":
                            sUrl = swjtGame.Login(sUserID, sGameName);
                            break;
                        case "gcld":
                            sUrl = gcldGame.Login(sUserID,sGameName);
                            break;
                        case "khbd":
                            sUrl = khbdGame.Login(sUserID, sGameName);
                            break;
                        case "hyjft":
                            sUrl = hyjftGame.Login(sUserID, sGameName);
                            break;
                        case "nslm":
                            sUrl = nslmGame.Login(sUserID, sGameName);
                            break;
                        case "dtgzt":
                            sUrl = tgztGame.Login(sUserID, sGameName);
                            break;
                        case "mhxy":
                            sUrl = mhxyGame.Login(sUserID, sGameName);
                            break;
                        case "qxz":
                            sUrl = qxzGame.Login(sUserID, sGameName);
                            break;
                        case "qszg":
                            sUrl = qszgGame.Login(sUserID, sGameName);
                            break;
                        case "wwsg":
                            sUrl = wwsgGame.Login(sUserID, sGameName);
                            break;
                        case "dntg":
                            sUrl = dntgGame.Login(sUserID, sGameName,"");
                            break;
                        case "jy":
                            sUrl = jyGame.Login(sUserID, sGameName, "");
                            break;
                        case "sskc":
                            sUrl = sskcGame.Login(sUserID, sGameName, "");
                            break;
                        case "ktpd":
                            sUrl = ktpdGame.Login(sUserID, sGameName,"", "");
                            break;
                        case "mhtj":
                            sUrl = mhtjGame.Login(sUserID, sGameName, "");
                            break;
                        case "jjp":
                            sUrl = jjpGame.Login(sUserID, sGameName);
                            break;
                        case "sgyjz":
                            if (sAccount.IndexOf("banhaotest") < 0 && sGameName == "sgyjz999")
                            {
                                sUrl = "";
                            }
                            else
                            {
                                string fcm = string.Empty;
                                string sfcmAccount = "banhaotest1|banhaotest2|banhaotest3|banhaotest9|banhaotest10";
                                if (sfcmAccount.IndexOf(sAccount) > -1)
                                {
                                    fcm = "0";
                                }
                                else
                                {
                                    fcm = "2";
                                }
                                sUrl = sgyjzGame.Login(sUserID, sGameName, fcm);
                            }
                            break;
                        case "dtgzter":
                            sUrl = tgzt2Game.Login(sUserID, sGameName);
                            break;
                        case "zwx":
                            sUrl = zwxGame.Login(sUserID, sGameName);
                            break;
                        default:
                            sUrl = string.Format("{0}|{1}|{2}",sGameName,sGame,sUserID);
                            break;
                    }
                    if (sUrl.Length > 5 && sUrl.IndexOf("http") > -1)
                    {
                        if (GameLogin(sGameName))
                        {
                            GameLoginBLL.GameLoginAdd(iUserID, sGameName, ProvideCommon.GetRealIP(), sUrl);
                        }
                        Response.Redirect(sUrl, true);
                    }
                    else
                    {
                        Response.Redirect(string.Format("http://www.dao50.com/fwqwh/?url={0}",Server.UrlEncode(sUrl)), true);
                    }
                }
            }
        }
    }
}

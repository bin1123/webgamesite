using System;
using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class MulitToGame : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string sGameName = CYRequest.GetString("gn");
                if (sGameName.Length < 2)
                {
                    Response.Redirect("http://www.wanyouxi123.com/togameerr.html?code=gameisnull", false);
                    return;
                }
                string sStartTime = DateTime.Now.ToString();
                string sEndTime = ServerBLL.ServerTimeSel(sGameName);
                if (!ProvideCommon.valTime(sStartTime, sEndTime))
                {
                    Response.Redirect("http://www.wanyouxi123.com/togameerr.html?code=gamenobegin", true);
                    return;
                }
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
                            ClearUsersInfo();
                            return;
                        }
                    }
                    sUserID = iUserID.ToString();
                    string sGame = GameInfoBLL.GameInfoAbbreSel(sGameName).TrimEnd();
                    string sUrl = string.Empty;
                    switch (sGame)
                    {
                        case "lj":
                            sUrl = ljGame.Login(sUserID, sGameName);
                            break;
                        case "yjxy":
                            sUrl = yjxyGame.Login(sUserID, sGameName);
                            break;
                        case "sq":
                            bool bRes = GameLogin(sGameName);
                            sUrl = sqGame.Login(sUserID, sGameName, bRes);
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
                            sUrl = gcldGame.Login(sUserID, sGameName);
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
                        case "tgzt":
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
                            sUrl = dntgGame.Login(sUserID, sGameName, "");
                            break;
                        case "jy":
                            sUrl = jyGame.Login(sUserID, sGameName, "");
                            break;
                        case "sskc":
                            sUrl = sskcGame.Login(sUserID, sGameName, "");
                            break;
                        case "ktpd":
                            sUrl = ktpdGame.Login(sUserID, sGameName, "", "");
                            break;
                        case "mhtj":
                            sUrl = mhtjGame.Login(sUserID, sGameName, "");
                            break;
                        case "dtgzt":
                            sUrl = tgztGame.Login(sUserID, sGameName);
                            break;
                        case "sgyjz":
                            if (sAccount.IndexOf("banhaotest") > -1)
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
                            else
                            {
                                sUrl = sgyjzGame.Login(sUserID, sGameName, "2");
                            }
                            break;
                        case "dtgzter":
                            sUrl = tgzt2Game.Login(sUserID, sGameName);
                            break;
                        case "zwx":
                            sUrl = zwxGame.Login(sUserID, sGameName);
                            break;
                        default:
                            sUrl = string.Format("{0}|{1}|{2}", sGameName, sGame, sUserID);
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
                        Response.Redirect(string.Format("http://www.wanyouxi123.com/togameerr.html?code=gameurlerr&{0}", sUrl), true);
                    }
                }
                else
                {
                    string sMultiPP = CYRequest.GetString("pp");
                    if(sMultiPP.Length < 10)
                    {
                        Response.Write("pp length is small");
                        return;
                    }
                    string sAccount = CYRequest.GetString("un");
                    int iUserID = UserBll.UserIDSel(sAccount);
                    if(ProvideCommon.valMultiPP(iUserID,sMultiPP))
                    {
                        string sUserID = iUserID.ToString();
                        string sGame = GameInfoBLL.GameInfoAbbreSel(sGameName).TrimEnd();
                        string sUrl = string.Empty;
                        switch (sGame)
                        {
                            case "lj":
                                sUrl = ljGame.Login(sUserID, sGameName);
                                break;
                            case "yjxy":
                                sUrl = yjxyGame.Login(sUserID, sGameName);
                                break;
                            case "sq":
                                bool bRes = GameLogin(sGameName);
                                sUrl = sqGame.Login(sUserID, sGameName, bRes);
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
                                sUrl = gcldGame.Login(sUserID, sGameName);
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
                                sUrl = dntgGame.Login(sUserID, sGameName, "");
                                break;
                            case "jy":
                                sUrl = jyGame.Login(sUserID, sGameName, "");
                                break;
                            case "sskc":
                                sUrl = sskcGame.Login(sUserID, sGameName, "");
                                break;
                            case "ktpd":
                                sUrl = ktpdGame.Login(sUserID, sGameName, "", "");
                                break;
                            case "mhtj":
                                sUrl = mhtjGame.Login(sUserID, sGameName, "");
                                break;
                            case "sgyjz":
                                if (sAccount.IndexOf("banhaotest") > -1)
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
                                sUrl = string.Format("{0}|{1}|{2}", sGameName, sGame, sUserID);
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
                            Response.Redirect(string.Format("http://www.wanyouxi123.com/togameerr.html?code=gameurlerr&{0}", sUrl), true);
                        }
                        
                    }
                }
            }
        }
    }
}

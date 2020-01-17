using System;
using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class TestGame : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!Page.IsPostBack)
            //{
                //string sGameName = CYRequest.GetString("gn");
                //if (sGameName == "")
                //{
                //    Response.Write(sGameName);
                //}
                //else if (LoginSessionVal() || isLoginCookie())
                //{
                    //int iUserID = GetUserID();
                    //string sUserID = string.Empty;
                    //string sAccount = GetAccount();
                    //if (!ValUserState(iUserID, sAccount))
                    //{
                    //    iUserID = UserBll.UserIDSel(sAccount);
                    //    if (iUserID < 1000 || (!ValUserState(iUserID, sAccount)))
                    //    {
                    //        ClearUsersInfo();
                    //        return;
                    //    }
                    //}
                    //string sStartTime = DateTime.Now.ToString();
                    //string sEndTime = ServerBLL.ServerTimeSel(sGameName);
                    //if (!ProvideCommon.valTime(sStartTime, sEndTime))
                    //{
                    //    if(!UserBll.AdminUserVal(iUserID))
                    //    {
                    //        string sWUrl = WebConfig.BaseConfig.sWUrl;
                    //        Response.Redirect(string.Format("{0}/jjkf", sWUrl), true);
                    //        return;
                    //    }
                    //}
                    //sUserID = iUserID.ToString();
                    //string sGame = GameInfoBLL.GameInfoAbbreSel(sGameName).TrimEnd();
                    //switch (sGame)
                    //{
                    //    case "lj":
                    //        sUrl = ljGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "yjxy":
                    //        sUrl = yjxyGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "tssg":
                    //        string fuid = CYRequest.GetString("fuid");
                    //        sUrl = tssgGame.Login(sUserID, sGameName,fuid);
                    //        break;
                    //    case "sq":
                    //        bool bRes = GameLogin(sGameName);
                    //        sUrl = sqGame.Login(sUserID, sGameName, bRes);
                    //        break;
                    //    case "dxz":
                    //        sUrl = dxzGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "djj":
                    //        sUrl = djjGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "txj":
                    //        sUrl = txjGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "sjsg":
                    //        sUrl = sjsgGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "tzcq":
                    //        sUrl = tzcqGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "zsg":
                    //        sUrl = zsgGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "by":
                    //        sUrl = byGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "mxqy":
                    //        sUrl = mxqyGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "swjt":
                    //        sUrl = swjtGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "gcld":
                    //        sUrl = gcldGame.Login(sUserID,sGameName);
                    //        break;
                    //    case "khbd":
                    //        sUrl = khbdGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "sglj":
                    //        sUrl = sgljGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "hyjft":
                    //        sUrl = hyjftGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "llsg":
                    //        sUrl = llsgGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "nslm":
                    //        sUrl = nslmGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "rxzt":
                    //        sUrl = rxztGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "dtgzt":
                    //        sUrl = tgztGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "ahxy":
                    //        sUrl = ahxyGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "sxj":
                    //        sUrl = sxjGame.Login(sUserID,sGameName);
                    //        break;
                    //    case "mhxy":
                    //        sUrl = mhxyGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "zwj":
                    //        sUrl = zwjGame.Login(sUserID,sGameName);
                    //        break;
                    //    case "qxz":
                    //        sUrl = qxzGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "qszg":
                    //        sUrl = qszgGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "wwsg":
                    //        sUrl = wwsgGame.Login(sUserID, sGameName);
                    //        break;
                    //    case "dntg":
                    //        sUrl = dntgGame.Login(sUserID, sGameName,"");
                    //        break;
                    //    case "jy":
                    //        sUrl = jyGame.Login(sUserID, sGameName, "");
                    //        break;
                    //    case "sskc":
                    //        sUrl = sskcGame.Login(sUserID, sGameName, "");
                    //        break;
                    //    case "ktpd":
                    //        sUrl = ktpdGame.Login(sUserID, sGameName,"", "");
                    //        break;
                    //    case "mhtj":
                    //        sUrl = mhtjGame.Login(sUserID, sGameName, "");
                    //        break;
                    //    case "ahxx":
                    //        sUrl = ahxxGame.Login(sUserID, sGameName, "");
                    //        break;
                    //    default:
                    //        sUrl = string.Format("{0}|{1}|{2}",sGameName,sGame,sUserID);
                    //        break;
                    //}
                    //if (sUrl != null && sUrl.Length > 5 && sUrl.IndexOf("http") > -1)
                    //{
                    //    if (GameLogin(sGameName))
                    //    {
                    //        GameLoginBLL.GameLoginAdd(iUserID, sGameName, ProvideCommon.GetRealIP(), sUrl);
                    //    }
                    //    Response.Redirect(sUrl, true);
                    //}
                    //else
                    //{
                    //    Response.Redirect(string.Format("http://www.dao50.com/fwqwh/?url={0}",sUrl), true);
                    //}
                //}
            //}
        }
    }
}

using System;
using System.Web;
using System.Web.Services;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Ajax : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string sAjaxType = CYRequest.GetString("AjaxType");
            int iUserID = 0;
            switch(sAjaxType)
            {
                case "ValName":
                    context.Response.Write(AccountVal(CYRequest.GetString("Account")));
                    break;
                case "GameAllSel":
                    context.Response.Write(GameAllSel());
                    break;
                case "ServerSelByGame":
                    context.Response.Write(ServerSelByGame(CYRequest.GetString("Abbre")));
                    break;
                case "ServerNumSelByGame":
                    context.Response.Write(ServerNumSelByGame(CYRequest.GetString("Abbre"), CYRequest.GetString("Num")));
                    break;
                case "CodeTypeSel":
                    context.Response.Write(CodeTypeSel(CYRequest.GetString("GameAbbre")));
                    break;
                case "CodeUrlSel":
                    context.Response.Write(CodeUrlSel(CYRequest.GetString("Abbre")));
                    break;
                case "loginval":
                    context.Response.Write(UserVal(CYRequest.GetString("username"), CYRequest.GetString("password")));
                    break;
                case "ServerNewSel":
                    int iGameID = 0;
                    int.TryParse(CYRequest.GetString("gameid"), out iGameID);
                    if(iGameID > 0)
                    {
                        context.Response.Write(ServerNewSel(iGameID));
                    }
                    break;
                case "ServerLoginLastSel":
                    iGameID = 0;
                    int.TryParse(CYRequest.GetString("gameid"), out iGameID);
                    iUserID = UserBll.UserIDSel(CYRequest.GetString("account"));
                    if (iGameID > 0 && iUserID > 999)
                    {
                        context.Response.Write(ServerLoginLastSel(iUserID, iGameID));
                    }
                    break;
                case "NoticeSel":
                    context.Response.Write(NoticeSel(CYRequest.GetString("abbre")));
                    break;
                case "sqGameCL":
                    context.Response.Write(sqGameCL(CYRequest.GetString("account"),CYRequest.GetString("game")) );
                    break;
                case "jyGameCL":
                    context.Response.Write(jyGameCL(CYRequest.GetString("account"), CYRequest.GetString("game"), CYRequest.GetString("pc")));
                    break;
                case "sqUserInfo":
                    context.Response.Write(sqUserInfos(CYRequest.GetString("account"), CYRequest.GetString("game")));
                    break;
                case "Top20PaySel":
                    context.Response.Write(Top20PaySel());
                    break;
                case "Top20PayAllSel":
                    context.Response.Write(Top20PayAllSel());
                    break;
                case "ServerJsonSelByGame":
                    context.Response.Write(ServerJsonSelByGame((CYRequest.GetString("Game"))));
                    break;
                case "HelpClassLJ2DLGGSel":
                    context.Response.Write(HelpClassLJ2DLGGSel(CYRequest.GetString("ClassID")));
                    break;
                case "LoginSeverSel":
                    context.Response.Write(LoginSeverSel(CYRequest.GetString("un")));
                    break;
                case "UserInfoSel":
                    context.Response.Write(UserInfoSel(CYRequest.GetString("un")));
                    break;
                case "CodeTake":
                    iUserID = UserBll.UserIDSel(CYRequest.GetString("un"));
                    if (iUserID > 999)
                    {
                        string sTakeRes = string.Empty;
                        string sGameAbbre = CYRequest.GetString("ServerAbbre");
                        string sGame = GameInfoBLL.GameInfoAbbreSel(sGameAbbre).TrimEnd();
                        switch (sGame)
                        {
                            case "sxd":
                                sTakeRes = string.Format("0|{0}", sxdGame.GetNewCode(sGameAbbre, iUserID.ToString()));
                                break;
                            case "lj":
                                if (CYRequest.GetString("CodeType") == "ljxsk")
                                {
                                    sTakeRes = string.Format("0|{0}", ljGame.GetNewCode(sGameAbbre, iUserID.ToString()));
                                }
                                else
                                {
                                    sTakeRes = CodeTake(sGameAbbre, CYRequest.GetString("CodeType"), iUserID);
                                }
                                break;
                            case "swjt":
                                sTakeRes = string.Format("0|{0}", swjtGame.GetNewCode(sGameAbbre, iUserID.ToString()));
                                break;
                            case "wwsg":
                                sTakeRes = string.Format("0|{0}", wwsgGame.GetNewCode(iUserID.ToString(), sGameAbbre));
                                break;
                            case "ktpd":
                                sTakeRes = string.Format("0|{0}", ktpdGame.GetNewCode(iUserID.ToString(), sGameAbbre));
                                break;
                            case "sgyjz":
                                sTakeRes = string.Format("0|{0}", sgyjzGame.GetNewCode(iUserID.ToString(), sGameAbbre.ToString(), ""));
                                break;
                            case "zwx":
                                if (CYRequest.GetString("CodeType") == "zwxxsk")
                                {
                                    sTakeRes = string.Format("0|{0}", zwxGame.GetNewCode(iUserID.ToString(), sGameAbbre));
                                }
                                else
                                {
                                    sTakeRes = CodeTake(sGameAbbre, CYRequest.GetString("CodeType"), iUserID);
                                }
                                break;
                            default:
                                sTakeRes = CodeTake(sGameAbbre, CYRequest.GetString("CodeType"), iUserID);
                                break;
                        }
                        context.Response.Write(sTakeRes);
                    }
                    break;
                case "FirstGift":
                    context.Response.Write(FirstGift(CYRequest.GetString("gn"), CYRequest.GetString("un"), CYRequest.GetString("pp"),CYRequest.GetString("host")));
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        protected string NoticeSel(string sAbbre)
        {
            return NoticeBLL.JsonNoticeSelFromCMS(sAbbre);
        }

        protected string ServerLoginLastSel(int iUserID, int iGameID)
        {
            return GameLoginBLL.GameLoginLastServerJsonSel(iUserID, iGameID);
        }

        protected string ServerNewSel(int iGameID)
        {
            return ServerBLL.ServerNewJsonSel(iGameID);
        }

        protected int AccountVal(string sAccount)
        {
            return UserBll.AccountsVal(sAccount);
        }

        protected string GameAllSel()
        {
            return GameInfoBLL.GameInfoJsonSel();
        }

        protected string ServerSelByGame(string sAbbre)
        {
            return ServerBLL.ServerJsonSel(sAbbre);
        }

        protected string ServerNumSelByGame(string sAbbre,string sNum)
        {
            return ServerBLL.ServerNumJsonSel(sAbbre,sNum);
        }

        protected string CodeTypeSel(string sGameAbbre)
        {
            return CodeBLL.CodeJsonSel(sGameAbbre);
        }

        protected string CodeUrlSel(string sAbbre)
        {
            return CodeBLL.CodeUrlSel(sAbbre);
        }

        protected string UserVal(string sAccount, string sPassWord)
        {
            string sReturn = string.Empty;
            string sMD5PassWord = UserBll.PassWordMD5(sAccount, sPassWord);
            string sRes = UserBll.UserVal(sAccount, sMD5PassWord);
            if ("0" == sRes)
            {
                int iUserID = UserBll.UserIDSel(sAccount);
                if (iUserID > 999)
                {
                    sReturn = sRes;
                }
                else
                {
                    sReturn = "2";//登陆失败
                }
            }
            else
            {
                string sMD5PassWordNew = UserBll.PassWordMD5New(sAccount, sPassWord);
                if ("0" == UserBll.UserVal(sAccount, sMD5PassWordNew))
                {
                    int iUserID = UserBll.UserIDSel(sAccount);
                    if (iUserID > 999)
                    {
                        sReturn = "0";
                    }
                    else
                    {
                        sReturn = "2";//登陆失败
                    }
                }
            }
            return sReturn;
        }

        protected string sqGameCL(string sAccount,string sGame)
        {
            string sUserIP = ProvideCommon.GetRealIP();
            if (!ProvideCommon.GameIPVal(sUserIP))
            {
                return "ip kill";
            }
            int iUserID = UserBll.UserIDSel(sAccount);
            string sRes = string.Empty;
            if (iUserID > 999)
            {
                sRes = sqGame.cLogin(iUserID.ToString(), sGame,true);
            }
            return sRes;
        }

        protected string jyGameCL(string sAccount, string sGame,string pc)
        {
            int iUserID = UserBll.UserIDSel(sAccount);
            string sRes = string.Empty;
            if (iUserID > 999)
            {
                sRes = jyGame.Login(iUserID.ToString(), sGame, pc); 
                GameLoginBLL.GameLoginAdd(iUserID, sGame, ProvideCommon.GetRealIP(), sRes);
            }
            return sRes;
        }
        protected string sqUserInfos(string sAccount, string sGameAbbre)
        {
            int iUserID = UserBll.UserIDSel(sAccount);
            return sqGame.GetUserInfoJson(iUserID.ToString(), sGameAbbre);
        }

        protected string Top20PaySel()
        {
            DateTime dtBegin = DateTime.Today;
            DateTime dtEnd = dtBegin.AddDays(1);
            return TransPBLL.TranPTop20PayJsonSel(dtBegin, dtEnd);
        }

        protected string Top20PayAllSel()
        {
            DateTime dtBegin = DateTime.Today.AddDays(-1);
            DateTime dtEnd = dtBegin.AddDays(1);
            return TransPBLL.Top20PayJsonAllSel(dtBegin, dtEnd);
        }

        protected string ServerJsonSelByGame(string sGame)
        {
            return ServerBLL.ServerJsonSelByGame(sGame);
        }

        protected string HelpClassLJ2DLGGSel(string sClassID)
        {
            return GameBLL.GameHelpLJ2DDGGSel(sClassID);
        }

        public string LoginSeverSel(string sAccount)
        {
            string sReturn = string.Empty;
            if (sAccount.Length > 4 || sAccount.Length < 17)
            {
                int iUserID = UserBll.UserIDSel(sAccount);
                sReturn = GameLoginBLL.GameLoginLastSelCJson(iUserID);
            }
            return sReturn;
        }

        public string UserInfoSel(string sAccount)
        {
            string sUserInfo = string.Empty;
            if (sAccount.Length > 4 || sAccount.Length < 17)
            {
                int iUserID = UserBll.UserIDSel(sAccount);
                string sUserPoints = UserPointsBLL.UPointAllSel(iUserID).ToString();
                StringBuilder sbText = new StringBuilder();
                sbText.Append("{\"point\":\"");
                sbText.Append(sUserPoints);
                sbText.Append("\"}");
                sUserInfo = sbText.ToString();
            }
            return sUserInfo;
        }

        protected string CodeTake(string sServerAbbre, string sCodeType, int iUserID)
        {
            int iCodeCount = GameCodeBLL.GameCodeCountSel(sServerAbbre, sCodeType);
            StringBuilder sbText = new StringBuilder();
            if (iCodeCount > 0)
            {
                string sCode = GameCodeBLL.GameCodeSelByUserID(sServerAbbre, sCodeType, iUserID);
                if (sCode.Length > 1)
                {
                    sbText.AppendFormat("3|{0}", sCode);
                }
                else
                {
                    //获取激活码
                    string sIP = ProvideCommon.GetRealIP();
                    string sRes = GameCodeBLL.GameCodeGet(sServerAbbre, iUserID, sCodeType, sIP);
                    if (sRes.Length > 1)
                    {
                        sbText.AppendFormat("0|{0}", sRes);
                    }
                    else
                    {
                        sbText.Append(sRes);
                    }
                }
            }
            else
            {
                sbText.Append("4");
            }
            return sbText.ToString();
        }

        public string FirstGift(string sGameName, string sAccount, string sMultiPP,string sHost)
        {
            if (sMultiPP.Length < 10)
            {
                return "pplengthissmall";
            }
            string sBeginTime = ServerBLL.ServerTimeSel(sGameName);
            if (!FirstGiftBLL.valTime(sBeginTime))
            {
                return "timeerr";
            }
            if (sHost.Length < 4)
            {
                return "hostlenerr";
            }
            int iUserID = UserBll.UserIDSel(sAccount);
            if (!ProvideCommon.valMultiPP(iUserID, sMultiPP))
            {
                return "pperr";
            }
            string sGiftState = FirstGiftBLL.GiftStateSel(iUserID, sGameName);
            int iPoint = 10;
            string sTranIP = ProvideCommon.GetRealIP();
            string sGiftRes = string.Empty;
            string sRes = string.Empty;
            switch (sGiftState)
            {
                case "0":
                    sGiftRes = FirstGiftBLL.GiftToGame(iUserID, sGameName, sAccount, iPoint, sTranIP, sHost);
                    if (sGiftRes == "0")
                    {
                        sRes = "0";
                        FirstGiftBLL.GiftStateUpate(iUserID, sGameName, 1);
                    }
                    else
                    {
                        sRes = sGiftRes;
                    }
                    break;
                case "1":
                    sRes = "1";
                    break;
                case "2":
                    sRes = "7";
                    break;
                default:
                    string sLevel = FirstGiftBLL.ULevelSel(iUserID, sGameName);
                    int iLevel = 0;
                    int.TryParse(sLevel, out iLevel);
                    int iTrueLevel = GameLevelSel(sGameName);
                    if (iLevel > iTrueLevel)
                    {
                        FirstGiftBLL.GiftAdd(iUserID, iLevel, sGameName);
                        sGiftRes = FirstGiftBLL.GiftToGame(iUserID, sGameName, sAccount, iPoint, sTranIP, sHost);
                        if (sGiftRes == "0")
                        {
                            FirstGiftBLL.GiftStateUpate(iUserID, sGameName, 1);
                            sRes = "0";
                        }
                        else
                        {
                            FirstGiftBLL.GiftStateUpate(iUserID, sGameName, 0);
                            sRes = sGiftRes;
                        }
                    }
                    else
                    {
                        sRes = "2";
                        //sRes = string.Format("2|{0}",iTrueLevel.ToString());
                    }
                    break;
            }
            return sRes;
        }

        private int GameLevelSel(string sGameName)
        {
            int iTrueLevel = 29;
            switch(sGameName)
            {
                case "zwx":
                    iTrueLevel = 39;
                    break;
            }
            return iTrueLevel;
        }
    }
}

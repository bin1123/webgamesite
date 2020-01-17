using System;
using System.Configuration;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.services
{
    public partial class Ajax : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int iUserID = GetUserID();
            if (Request.HttpMethod == "POST" || Request.HttpMethod == "GET")
            {
                string sAjaxType = CYRequest.GetString("AjaxType");
                switch (sAjaxType)
                {
                    case "GLoginSel":
                        Response.Write(GLoginSel());
                        break;
                    case "CodeTake":
                        if(iUserID > 999)
                        {
                            string sTakeRes = string.Empty;
                            string sGameAbbre = CYRequest.GetString("ServerAbbre");
                            string sGame = GameInfoBLL.GameInfoAbbreSel(sGameAbbre).TrimEnd();
                            switch (sGame)
                            {
                                case "sxd":
                                    sTakeRes = string.Format("0|{0}",sxdGame.GetNewCode(sGameAbbre,iUserID.ToString()));
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
                                    sTakeRes = string.Format("0|{0}", sgyjzGame.GetNewCode(iUserID.ToString(),sGameAbbre.ToString(),""));
                                    break;
                                case "zwx":
                                    if (CYRequest.GetString("CodeType") == "zwxxsk")
                                    {
                                        sTakeRes = string.Format("0|{0}", zwxGame.GetNewCode(iUserID.ToString(),sGameAbbre));
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
                            Response.Write(sTakeRes);
                        }
                        break;
                    case "rmbpaygame":
                        if (iUserID > 999)
                        {
                            Response.Write(RMBPayGame(iUserID));
                        }
                        break;
                    case "rmbpaywlb":
                        if (iUserID > 999)
                        {
                            Response.Write(PTransUser(iUserID,1,1));
                        }
                        break;
                    case "wlbpaygame":
                        if (iUserID > 999)
                        {
                            Response.Write(GTransUser(iUserID,1,1));
                        }
                        break;
                    case "HelpClassSel":
                        if (iUserID > 999)
                        {
                            string sGameAbbre = CYRequest.GetString("gameabbre");
                            string sClassName = CYRequest.GetString("classname");
                            int iGameID = GameInfoBLL.GameInfoIDSel(sGameAbbre);
                            Response.Write(HelpClassSel(iGameID, sClassName));
                        }
                        break;
                    case "HelpClassLJSel":
                        if (iUserID > 999)
                        {
                            string sGameAbbre = CYRequest.GetString("gameabbre");
                            string sClassName = CYRequest.GetString("classname");
                            int iGameID = GameInfoBLL.GameInfoIDSel(sGameAbbre);
                            Response.Write(HelpClassLJSel(iGameID, sClassName));
                        }
                        break;
                    case "AccountSel":
                        if(iUserID > 999)
                        {
                            Response.Write(GetAccount());
                        }
                        break;
                    case "LoginSeverSel":
                        if (iUserID > 999)
                        {
                            Response.Write(LoginSeverSel());
                        }
                        break;
                    case "LoginSeverSelAll":
                        if (iUserID > 999)
                        {
                            Response.Write(LoginSeverSelAll());
                        }
                        break;
                    case "UserPointsSel":
                        if (iUserID > 999)
                        {
                            Response.Write(UserPointsSel(iUserID));
                        }
                        break;
                    case "getReward": 
                        if (iUserID > 999)
                        {
                            Response.Write(getReward(CYRequest.GetString("gn"),iUserID));
                        }
                        break;
                    case "NoRegBind":
                        if (iUserID > 999)
                        {
                            Response.Write(NoRegBind(CYRequest.GetString("account"),CYRequest.GetString("pw")));
                        }
                        break;
                    case "UserVal":
                        Response.Write(UserVal(CYRequest.GetString("un"), CYRequest.GetString("pwd")));
                        break;
                    case "ktpdGameCL":
                        Response.Write(ktpdGameCL(iUserID,CYRequest.GetString("game")));
                        break;
                }
            }
        }

        /// <summary>
        /// 判断账号是否注册
        /// </summary>
        /// <param name="sAccount">添加后缀后的账号</param>
        /// <returns></returns>
        protected int AccountVal(string sAccount)
        {
            return UserBll.AccountsVal(sAccount);
        }

        protected string PTransUser(int iUserID, int iPage, int iNum)
        {
            return TransPBLL.UserTranSelByUID(iUserID, iPage, iNum);
        }

        protected string GTransUser(int iUserID, int iPage, int iNum)
        {
            return TransGBLL.UserTranSelByUID(iUserID, iPage, iNum);
        }

        protected string UserVal(string sAccount, string sPassWord)
        {
            string sValMessage = UserBll.LoginCheckText(sAccount, sPassWord);
            if (sValMessage != "")
            {
                return sValMessage;
            }
            string sReturn = string.Empty;
            string sMD5PassWord = UserBll.PassWordMD5(sAccount, sPassWord);
            string sRes = UserBll.UserVal(sAccount, sMD5PassWord);
            if ("0" == sRes)
            {
                string sNickName = string.Empty;
                int iUserID = UserBll.UserIDSel(sAccount);
                string sPageUrl = Request.Url.ToString();
                LoginStateSet(sAccount, iUserID, sPageUrl);
                if (iUserID < 1000)
                {
                    sReturn = "登陆失败";//登陆失败
                }
            }
            else
            {
                sReturn = "非法用户";
            }
            return sReturn;
        }

        public string GLoginSel()
        {
            int iUserID = GetUserID();
            return GameLoginBLL.GameLoginLastSel(iUserID);
        }

        protected string CodeTake(string sServerAbbre, string sCodeType,int iUserID)
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

        protected string RMBPayGame(int iUserID)
        {
            return TranQuickBLL.TransQuickLogSel(iUserID);
        }

        protected string HelpClassSel(int iGameID, string sClassName)
        {
            return GameBLL.GameHelpJsonSelFromCMS(iGameID, sClassName);
        }

        protected string HelpClassLJSel(int iGameID, string sClassName)
        {
            return GameBLL.GameHelpLJJsonSelFromCMS(iGameID, sClassName);
        }

        public string LoginSeverSel()
        {
            string sReturn = string.Empty;
            string sLoginServer = GetLoginTop3();
            if (sLoginServer.Length > 1)
            {
                sReturn = ServerBLL.ServerLoginJsonSel(sLoginServer);
            }
            return sReturn;
        }

        public string LoginSeverSelAll()
        {
            string sReturn = string.Empty;
            string sLoginServer = GetLoginTop3();
            if (sLoginServer.Length > 1)
            {
                sReturn = ServerBLL.ServerSelByAbbreAll(sLoginServer);
            }
            return sReturn;
        }

        public int UserPointsSel(int iUserID)
        { 
            int iUserPoints = UserPointsBLL.UPointAllSel(iUserID);
            return iUserPoints;
        }

        public string getReward(string sGameName,int iUserID)
        {
            if (iUserID < 1000000)
            {
                return "useriderr!";
            }
            string sAccount = UserBll.AccountSel(iUserID);
            string sRes = string.Empty;    
            if (!NoRegGiftBLL.NoRegGiftUserIDSel(iUserID))
            {
                sRes = "1";
            }
            else
            { 
                string sLevel = NoRegGiftBLL.ULevelSel(iUserID, sGameName);
                string sTranIP = ProvideCommon.GetRealIP();
                int iPoint = 10;
                if (sLevel == "-2")
                {
                    string sGiftRes = NoRegGiftBLL.GiftToGame(iUserID, sGameName, sAccount, iPoint, sTranIP);
                    if (sGiftRes == "0")
                    {
                        NoRegGiftBLL.NoRegGiftAdd(iUserID, -2, sGameName);
                        sRes = "0";
                    }
                    else
                    {
                        sRes = sGiftRes;
                    }
                }
                else
                {
                    int iLevel = 0;
                    int.TryParse(sLevel,out iLevel);
                    if (iLevel > 14)
                    {
                        string sGiftRes = NoRegGiftBLL.GiftToGame(iUserID, sGameName, sAccount, iPoint, sTranIP);
                        if (sGiftRes == "0")
                        {
                            NoRegGiftBLL.NoRegGiftAdd(iUserID, iLevel, sGameName);
                            sRes = "0";
                        }
                        else
                        {
                            sRes = sGiftRes;
                        }
                    }
                    else
                    {
                        sRes = "2";
                    }
                }
            }
            return sRes;
        }

        public string NoRegBind(string sAccount,string sPassWord)
        {
            string sRes = string.Empty;
            string sValMessage = UserBll.RegCheckText(sAccount, sPassWord);
            if (sValMessage != "")
            {
                sRes = string.Format("<script>alert('{0}')</script>", sValMessage);
                return sRes;
            }

            int iUserID = GetUserID();
            if (iUserID > 1000)
            {
                //string sState = UserBll.RegStateSel(iUserID);
                //if(sState == "0")
                string sOldAccount = GetAccount();
                if (sOldAccount.IndexOf("?") != 0)
                {
                    sRes = "<script>alert('账号已绑定，请勿重复绑定！谢谢！')</script>";
                    return sRes;
                }
                string sMD5PassWord = UserBll.PassWordMD5(sAccount, sPassWord);
                int iNum = UserBll.UserUpdateNamePWD(iUserID, sAccount, sMD5PassWord);
                if (iNum > 0)
                {
                    string sPageUrl = Request.Url.ToString();
                    LoginStateSet(sAccount, iUserID, sPageUrl);
                    NoRegLoginBLL.NameReg(sAccount, iUserID.ToString());
                    return sRes;
                }
                else
                {
                    sRes = "<script>alert('绑定账号失败！请重试！')</script>";
                    return sRes;
                }
            }
            else
            {
                sRes = "<script>alert('数字ID不存在绑定账号失败！')</script>";
                return sRes;
            }
        }

        public string ktpdGameCL(int iUserID,string sGameName)
        { 
            string sClient = "pc";
            return ktpdGame.Login(iUserID.ToString(), sGameName, "", sClient);
        }

        public string FirstGift(string sGameName, int iUserID)
        {
            if (iUserID < 1000000)
            {
                return "useriderr!";
            }
            string sBeginTime = ServerBLL.ServerTimeSel(sGameName);
            if (!FirstGiftBLL.valTime(sBeginTime))
            {
                return "timeerr";
            }
            if(Request.UrlReferrer.Host.Length < 6)
            {
                return "hostlenerr";
            }
            if (Request.UrlReferrer.Host.Split('.').Length < 2)
            {
                return "hosterr";
            }
            string sFromHost = Request.UrlReferrer.Host.Split('.')[1];
            string sAccount = UserBll.AccountSel(iUserID).Trim();
            string sGiftState = FirstGiftBLL.GiftStateSel(iUserID, sGameName);
            string sRes = string.Empty;
            int iPoint = 10;
            string sTranIP = ProvideCommon.GetRealIP();
            string sGiftRes = string.Empty;
            switch (sGiftState)
            {
                case "0":
                    sGiftRes = FirstGiftBLL.GiftToGame(iUserID, sGameName, sAccount, iPoint, sTranIP,sFromHost);
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
                    if (iLevel > 29)
                    {
                        FirstGiftBLL.GiftAdd(iUserID, iLevel, sGameName);
                        sGiftRes = FirstGiftBLL.GiftToGame(iUserID, sGameName, sAccount, iPoint, sTranIP,sFromHost);
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
                    }
                    break;
            }
            return sRes;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

using Common;
using DataEnity;

namespace Bussiness
{
    public class nslmGame
    {
        private const string key = "NNvN39by61SjTSdX";

        public static string Login(string sUserID, string sGame)
        {
            string op_id = "115";
            string sid = GetServerID(sGame);
            string account = sUserID;
            string ad_info = "";
            string time = ProvideCommon.getTime().ToString();
            string sAuth = string.Format("op_id={0}&sid={1}&game_id=31&account={2}&adult_flag=1&game_time=&ip=&ad_info={3}&time={4}",op_id,sid,account,ad_info,time);
            string sBase64Auth = Base64.EncodeBase64(sAuth);
            string sVerify = ProvideCommon.MD5(string.Format("{0}{1}",sBase64Auth,key));
            string sGameUrl = string.Format("http://up.uuzu.com/api/commonAPI/Login?auth={0}&verify={1}", sBase64Auth, sVerify);
            return sGameUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string op_id = "115";
            string sid = GetServerID(sGame);
            string account = sUserID;
            int iMoney = Convert.ToInt32(dMoney);
            int iGameMoney = iMoney * 10;
            string game_money = iGameMoney.ToString();
            string u_money = iMoney.ToString();
            string time = ProvideCommon.getTime().ToString();
            string sAuth = string.Format("op_id={0}&sid={1}&game_id=31&account={2}&order_id={3}&game_money={4}&u_money={5}&time={6}",
                                          op_id, sid, account,sOrderID,game_money,u_money,time);
            string sBase64Auth = Base64.EncodeBase64(sAuth);
            string sVerify = ProvideCommon.MD5(string.Format("{0}{1}", sBase64Auth, key));
            string sPayUrl = string.Format("http://up.uuzu.com/api/commonAPI/charge?auth={0}&verify={1}", sBase64Auth, sVerify);
            string sRes = ProvideCommon.GetPageInfo(sPayUrl);
            string user_ip = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID,out iUserID);
            GamePayBLL.GamePayAdd(user_ip, sPayUrl, sOrderID, sRes, sGame,iUserID);
            string sCode = ProvideCommon.getJsonValue("status", sRes);
            return sCode;
        }

        public static string nslmPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            int iMoney = iPayPoints / 10;
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre, iPayPoints, sUserName, sPhone, iGUserID, sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if (sTGRes != "0")
            {
                return sTGRes;
            }
            string sRes = Pay(iGUserID.ToString(), iMoney, sTranID, sGameAbbre);
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "0":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string nslmQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName); 
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = Convert.ToInt32(dPrice * 10);
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "0":
                    int iGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    if (iGRes == 0)
                    {
                        sReturn = "0";
                    }
                    else
                    {
                        sReturn = "-1";
                    }
                    break;
                default: 
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string sReturn = string.Empty;
            string op_id = "115";
            string sid = GetServerID(sGameAbbre);
            string account = sUserID;
            string time = ProvideCommon.getTime().ToString();
            string sAuth = string.Format("op_id={0}&sid={1}&game_id=31&account={2}&time={3}",
                                          op_id,sid,account,time);
            string sBase64Auth = Base64.EncodeBase64(sAuth);
            string sVerify = ProvideCommon.MD5(string.Format("{0}{1}", sBase64Auth, key));
            string sPayUrl = string.Format("http://up.uuzu.com/api/commonAPI/roleverify?auth={0}&verify={1}", sBase64Auth, sVerify);
            string sRes = ProvideCommon.GetPageInfo(sPayUrl);
            string sCode = ProvideCommon.getJsonValue("status", sRes);
            switch (sCode)
            {
                case "8":
                    sReturn = "1";
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sServerID = sGame.Replace("nslm", "");
            int iBeginServerID = 50000000;
            int iSID = 1;
            int.TryParse(sServerID,out iSID);
            int iServerID = iBeginServerID + iSID;
            string sGameID = string.Format("310011{0}", iServerID.ToString());
            return sGameID;
        }
    }
}

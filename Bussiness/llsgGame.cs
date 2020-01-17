using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

using Common;
using DataEnity;

namespace Bussiness
{
    public class llsgGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string spid = "dao50";//平台id
            string gameid = "4";
            string serverid = GetServerID(sGame);
            string key = "qrEyrtZc9aBW0z5i";
            string ltime = ProvideCommon.getTime().ToString();
            string isminor = "1";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|", spid);
            sbText.AppendFormat("{0}|", gameid);
            sbText.AppendFormat("{0}|", serverid);
            sbText.AppendFormat("{0}|", sUserID);
            sbText.AppendFormat("{0}|", ltime);
            sbText.AppendFormat("{0}|", isminor);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//sign = md5(spid|gameid|serverid|userid|ltime|isminor|key)
            string sUrl = string.Format("http://mid.gamefy.cn/union_mid/login?userid={0}&spid={1}&gameid={2}&serverid={3}&ltime={4}&isminor={5}&sign={6}",
                                        sUserID,spid,gameid,serverid,ltime,isminor,sign);
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string spid = "dao50";//平台id
            string gameid = "4";
            string serverid = GetServerID(sGame);
            string key = "qrEyrtZc9aBW0z5i";
            int iMoney = Convert.ToInt32(dMoney*100);//单位:分 
            string user_ip = ProvideCommon.GetRealIP();
            string ctime = ProvideCommon.getTime().ToString();
            string orderid = sOrderID.Substring(0, 32);
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|", spid);
            sbText.AppendFormat("{0}|", gameid);
            sbText.AppendFormat("{0}|", serverid);
            sbText.AppendFormat("{0}|", sUserID);
            sbText.AppendFormat("{0}|", orderid);
            sbText.AppendFormat("{0}|", iMoney.ToString());
            sbText.AppendFormat("{0}|", user_ip);
            sbText.AppendFormat("{0}|", ctime);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString()).ToLower();//md5(spid|gameid|serverid|userid|orderid|money|userip|ctime|key)
            string sUrl = string.Format("http://mid.gamefy.cn/union_mid/charge?userid={0}&spid={1}&gameid={2}&serverid={3}&orderid={4}&money={5}&userip={6}&ctime={7}&sign={8}",
                                         sUserID,spid,gameid,serverid,orderid,iMoney.ToString(),user_ip,ctime,sign);
            string sRes = ProvideCommon.GetPageInfo(sUrl.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(user_ip, sUrl, sOrderID, sRes, sGame,iUserID);
            return sRes;
        }

        public static string llsgPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                case "1000":
                case "2006":
                case "2007":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string llsgQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "1000":
                case "2006":
                case "2007":
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
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GetDomain(string sGame)
        {
            string sServerID = sGame.Replace("llsg", "");
            string sDomain = string.Format("s{0}.llsg.dao50.com",sServerID);
            return sDomain;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string spid = "dao50";//平台id
            string gameid = "4";
            string serverid = GetServerID(sGameAbbre);
            string key = "qrEyrtZc9aBW0z5i";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|", spid);
            sbText.AppendFormat("{0}|", gameid);
            sbText.AppendFormat("{0}|", serverid);
            sbText.AppendFormat("{0}|", sUserID);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString()).ToLower();//sign = md5(spid|gameid|serverid|userid|key)
            string sHost = GetDomain(sGameAbbre);
            string preUrl = string.Format("http://mid.gamefy.cn/union_mid/query_user?userid={0}&spid={1}&gameid={2}&serverid={3}&sign={4}", sUserID, spid, gameid, serverid, sign);
            string sRes = ProvideCommon.GetPageInfo(preUrl);
            string sReturn = string.Empty;
            string sRet = ProvideCommon.getJsonValue("code", sRes); 
            if (sRet == "2001")
            {
                sReturn = "1";                
            }
            else
            {
                sReturn = sRet;
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sServerID = sGame.Replace("llsg", "");
            int iBeginServerID = 122200;
            int iSID = 1;
            int.TryParse(sServerID, out iSID);
            int iServerID = iBeginServerID + iSID;
            return iServerID.ToString();
        }
    }
}

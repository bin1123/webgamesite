using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Bussiness
{
    public class hzwGame
    {
        private const string key = "ReyNv45mRwp9qsWn";

        public static string Login(string sUserID, string sGame)
        {
            string spid = "dao50";
            string gameid = "2";
            string serverid = GetServerID(sGame);
            string ltime = ProvideCommon.getTime().ToString();//标准时间戳
            string isminor = "2";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|",spid);
            sbText.AppendFormat("{0}|", gameid);
            sbText.AppendFormat("{0}|", serverid);
            sbText.AppendFormat("{0}|", sUserID);
            sbText.AppendFormat("{0}|", ltime);
            sbText.AppendFormat("{0}|", isminor);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5(spid|gameid|serverid|userid|ltime|isminor|key)
            sbText.Remove(0, sbText.Length);
            sbText.Append("http://mid.gamefy.cn/union_mid/login?");
            sbText.AppendFormat("userid={0}&", sUserID);
            sbText.AppendFormat("spid={0}&", spid);
            sbText.AppendFormat("gameid={0}&", gameid);
            sbText.AppendFormat("serverid={0}&", serverid);
            sbText.AppendFormat("ltime={0}&", ltime);
            sbText.AppendFormat("isminor={0}&", isminor);
            sbText.AppendFormat("sign={0}&", sign);            
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string spid = "dao50";
            string gameid = "2";
            string serverid = GetServerID(sGame);
            int gold = Convert.ToInt32((dMoney * 100));//充值人民币，单位：分            
            string ctime = ProvideCommon.getTime().ToString();//标准时间戳
            string user_ip = ProvideCommon.GetRealIP();
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|", spid);
            sbText.AppendFormat("{0}|", gameid);
            sbText.AppendFormat("{0}|", serverid);
            sbText.AppendFormat("{0}|", sUserID);
            sbText.AppendFormat("{0}|", sOrderID);
            sbText.AppendFormat("{0}|", gold.ToString());
            sbText.AppendFormat("{0}|", user_ip);
            sbText.AppendFormat("{0}|", ctime);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5(spid|gameid|serverid|userid|orderid|money|userip|ctime|key)
            sbText.Remove(0, sbText.Length);
            string TranURL = "http://mid.gamefy.cn/union_mid/charge";
            sbText.Append(TranURL);
            sbText.AppendFormat("?userid={0}&", sUserID);
            sbText.AppendFormat("spid={0}&", spid);
            sbText.AppendFormat("gameid={0}&", gameid);
            sbText.AppendFormat("serverid={0}&", serverid);
            sbText.AppendFormat("orderid={0}&", sOrderID);
            sbText.AppendFormat("money={0}&", gold.ToString());
            sbText.AppendFormat("userip={0}&", user_ip);
            sbText.AppendFormat("ctime={0}&", ctime);
            sbText.AppendFormat("sign={0}", sign);  
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string hzwPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            decimal dMoney = Convert.ToDecimal(iPayPoints / 10);
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre, iPayPoints, sUserName, sPhone, iGUserID, sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if (sTGRes != "0")
            {
                return sTGRes;
            }
            string sRes = Pay(iGUserID.ToString(), dMoney, sTranID, sGameAbbre);
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "1000":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string hzwQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "2006":
                case "2007":
                    sReturn = "0";
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GameErr(string sErrID)
        {
            string sErrRes = string.Empty;
            switch (sErrID)
            {
                case "0":
                    sErrRes = "充值失败，订单处于待充状态";
                    break;
            }
            return sErrRes;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string spid = "dao50";
            string gameid = "2";
            string serverid = GetServerID(sGameAbbre);
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|", spid);
            sbText.AppendFormat("{0}|", gameid);
            sbText.AppendFormat("{0}|", serverid);
            sbText.AppendFormat("{0}|", sUserID);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5(spid|gameid|serverid|userid|key)
            sbText.Remove(0, sbText.Length);
            string TranURL = "http://mid.gamefy.cn/union_mid/query_user";
            sbText.Append(TranURL);
            sbText.AppendFormat("?userid={0}&", sUserID);
            sbText.AppendFormat("spid={0}&", spid);
            sbText.AppendFormat("gameid={0}&", gameid);
            sbText.AppendFormat("serverid={0}&", serverid);
            sbText.AppendFormat("sign={0}", sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sReturn = string.Empty;
            JSONObject json = JSONConvert.DeserializeObject(sRes);
            string sCode = json["code"].ToString();
            JSONConvert.clearJson();
            switch (sCode)
            {
                case "2001":
                    sReturn = "1";
                    break;
                default:
                    sReturn = "0";
                    break;
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sID = sGame.Replace("hzw", "");
            int iID = 1;
            int.TryParse(sID, out iID);
            int iBeginID = 19200;
            int iServerID = iBeginID + iID;
            return iServerID.ToString();
        }
    }
}

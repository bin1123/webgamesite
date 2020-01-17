using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

using Common;
using DataEnity;

namespace Bussiness
{
    public class gcldGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string yx = "dao50";//运营商标识
            string userid = sUserID;
            string tp = ProvideCommon.getTime().ToString();
            string additionalKey = sUserID;
            string loginKey = "&$^!@@(dao50::LOGIN)@@TdPYfsqOVVCzINMQxX";
            StringBuilder sbText = new StringBuilder();
            //预登陆
            sbText.Append(yx);
            sbText.Append(userid);
            sbText.Append(tp);
            sbText.Append(loginKey);
            string preloginticket = ProvideCommon.MD5(sbText.ToString());//yx+userId+tp+loginKey
            string preUrl = string.Format("http://{0}/root/preLogin.action?yx={1}&userId={2}&tp={3}&additionalKey={4}&ticket={5}", ServerHost(sGame), yx, userid, tp, additionalKey, preloginticket);
            string spreReturn = ProvideCommon.GetPageInfo(preUrl);

            sbText.Remove(0, sbText.Length);
            sbText.Append(yx);
            sbText.Append(userid);
            sbText.Append(tp);
            sbText.Append(additionalKey);
            sbText.Append(loginKey);
            string loginticket = ProvideCommon.MD5(sbText.ToString());//yx+userId+sfid+tp+additionalKey +loginKey
            string Url = string.Format("http://{0}/root/login.action?yx={1}&userId={2}&userName={2}&tp={3}&sfid=&adult=1&yxSource=&ticket={4}&preReturn={5}",
                                       ServerHost(sGame), yx, userid, tp, loginticket,spreReturn);
            return Url;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame, string playerId)
        {
            string yx = "dao50";//运营商标识
            string userid = sUserID;
            string sTranTime = TransGBLL.TransTimeSelByTID(sOrderID);
            string orderId = ProvideCommon.getTime(DateTime.Parse(sTranTime)).ToString();
            int iGold = Convert.ToInt32(dMoney * 10);
            string gold = iGold.ToString();
            string tp = ProvideCommon.getTime().ToString();
            string payKey = "!~#$@@(dao50::PAY)@@ngxXvk5Ax7T8B6huK4";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(yx);
            sbText.Append(userid);
            sbText.Append(orderId);
            sbText.Append(gold);
            sbText.Append(tp);
            sbText.Append(payKey);
            string ticket = ProvideCommon.MD5(sbText.ToString());//yx+userId+orderId+gold+tp+payKey
            string sGamePayUrl = string.Format("http://{0}/root/pay.action?yx={1}&userId={2}&playerId={3}&orderId={4}&gold={5}&tp={6}&ticket={7}",
                                                ServerHost(sGame), yx, userid, playerId, orderId, gold, tp, ticket);
            string sRes = ProvideCommon.GetPageInfo(sGamePayUrl);
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(sTranIP, sGamePayUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string gcldPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            string sPlayerID = GetPlayID(iGUserID.ToString(), sGameAbbre);
            decimal dMoney = (Convert.ToDecimal(iPayPoints)) / 10;
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre, iPayPoints, sUserName, sPhone, iGUserID, sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if (sTGRes != "0")
            {
                return sTGRes;
            }
            string sRes = Pay(iGUserID.ToString(), dMoney, sTranID, sGameAbbre, sPlayerID);
            string sReturn = string.Empty;
            if (sRes == "{\"state\":1,\"data\":1}" || sRes == "{\"state\":0,\"data\":5}")
            {
                sReturn = string.Format("0|{0}", sTranID);
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string gcldQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = Convert.ToInt32(dPrice * 10);
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sUserID = iUserID.ToString();
            string sPlayerID = GetPlayID(sUserID, sGameAbbre);
            string sRes = Pay(sUserID, dPrice, sTranID, sGameAbbre, sPlayerID);
            string sReturn = string.Empty;
            if (sRes == "{\"state\":1,\"data\":1}" || sRes == "{\"state\":0,\"data\":5}")
            {
                int iGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                if (iGRes == 0)
                {
                    sReturn = "0";
                }
                else
                {
                    sReturn = "-1";
                }
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string GetPlayID(string sUserID, string sGame)
        {
            string yx = "dao50";
            string getPlayIDUrl = string.Format("http://{0}/root/playerInfo.action?yx={1}&userId={2}", ServerHost(sGame), yx, sUserID);
            string sReturn = ProvideCommon.GetPageInfo(getPlayIDUrl);
            int iId = sReturn.IndexOf("playerId");
            string sPlayId = string.Empty;
            if (iId > 0)
            {
                int iEnd = sReturn.IndexOf(",", iId);
                int iBegin = iId + 10;
                int iLen = iEnd - iBegin;
                sPlayId = sReturn.Substring(iBegin, iLen);
            }
            return sPlayId;
        }

        public static string ServerHost(string sGame)
        {
            string sServer = string.Empty;
            //string sServerID = sGame.Replace("gcld", "");
            //int iServerID = 1;
            //int.TryParse(sServerID, out iServerID);
            int iRealServerID = 1;
            //if (iServerID > 57)
            //{
            //    switch (iServerID)
            //    {
            //        case 58:
            //        case 59:
            //        case 60:
            //        case 61:
            //        case 62:
            //        case 63:
            //        case 64:
            //            iRealServerID = 58;
            //            break;

            //    }
            //}
            sServer = string.Format("s{0}.gcld.dao50.com", iRealServerID.ToString());
            return sServer;
        }
    }
}

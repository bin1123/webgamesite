using System;
using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
    public class mjcsGame
    {
        public static string Login(string sUserID,string sGame)
        {
            string yx = "dao50";//运营商标识
            string userid = sUserID;
            string tp = ProvideCommon.getTime().ToString();
            string key = sUserID;
            string loginKey = "#!$$@@(dao50::LOGIN)@@annzrFtNKgRwih2x";
            StringBuilder sbText = new StringBuilder();
            //预登陆
            sbText.Append(yx);
            sbText.Append(userid);
            sbText.Append(tp);
            sbText.Append(loginKey);
            string preloginticket = ProvideCommon.MD5(sbText.ToString());//yx+userId+tp+loginKey
            string preUrl = string.Format("http://{0}/yx/preLogin?yx={1}&userId={2}&tp={3}&key={4}&ticket={5}",ServerHost(sGame),yx,userid,tp,key,preloginticket);
            ProvideCommon.GetPageInfo(preUrl);
            
            sbText.Remove(0, sbText.Length);
            sbText.Append(yx);
            sbText.Append(userid);
            sbText.Append(tp);
            sbText.Append(key);
            sbText.Append(loginKey);
            string loginticket = ProvideCommon.MD5(sbText.ToString());//yx+userId+sfid+tp+key+loginKey
            string Url = string.Format("http://{0}/yx/login?yx={1}&userId={2}&userName={2}&tp={3}&sfid=&adult=1&yxSource=&ticket={4}",
                                       ServerHost(sGame), yx, userid, tp, loginticket);
            return Url;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame,string playerId)
        {
            string yx = "dao50";//运营商标识
            string userid = sUserID;
            string sTranTime = TransGBLL.TransTimeSelByTID(sOrderID);
            string orderId = ProvideCommon.getTime(DateTime.Parse(sTranTime)).ToString();
            int iGold = Convert.ToInt32(dMoney * 10);
            string gold = iGold.ToString();
            string tp = ProvideCommon.getTime().ToString();
            string payKey = "##$~@@(dao50::PAY)@@RMd3YAtihg5GhO6LsU";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(yx);
            sbText.Append(userid);
            sbText.Append(orderId);
            sbText.Append(gold);
            sbText.Append(tp);
            sbText.Append(payKey);
            string ticket = ProvideCommon.MD5(sbText.ToString());//yx+userId+orderId+gold+tp+payKey
            string sGamePayUrl = string.Format("http://{0}/yx/pay?yx={1}&userId={2}&playerId={3}&orderId={4}&gold={5}&tp={6}&ticket={7}", 
                                                ServerHost(sGame), yx, userid, playerId, orderId, gold,tp,ticket);
            string sRes = ProvideCommon.GetPageInfo(sGamePayUrl);
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(sTranIP, sGamePayUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string mjcsPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            string sPlayerID = GetPlayID(iGUserID.ToString(),sGameAbbre);
            decimal dMoney = (Convert.ToDecimal(iPayPoints))/10;
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre, iPayPoints, sUserName, sPhone, iGUserID, sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if (sTGRes != "0")
            {
                return sTGRes;
            }
            string sRes = Pay(iGUserID.ToString(), dMoney, sTranID, sGameAbbre,sPlayerID);
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "1":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string mjcsQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
            string sRes = Pay(sUserID, dPrice, sTranID, sGameAbbre,sPlayerID);
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "1":
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

        public static string GetPlayID(string sUserID,string sGame)
        {
            string yx = "dao50";
            string getPlayIDUrl = string.Format("http://{0}/yx/getCharacter?yx={1}&userId={2}", ServerHost(sGame), yx, sUserID);
            string sReturn = ProvideCommon.GetPageInfo(getPlayIDUrl);
            int iId = sReturn.IndexOf("id");
            string sPlayId = string.Empty;
            if (iId > 0)
            {
                int iEnd = sReturn.IndexOf(",", iId);
                int iBegin = iId + 4;
                int iLen = iEnd - iBegin;
                sPlayId = sReturn.Substring(iBegin, iLen);
            }
            return sPlayId;
        }

        public static string ServerHost(string sGame)
        {
            string sServer = string.Empty;
            string sid = sGame.Replace("mjcs", "");
            string serverid = string.Empty;
            switch (sid)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    serverid = "1";
                    break;
                default:
                    serverid = sid;
                    break;
            }
            sServer = string.Format("s{0}.mjcs.dao50.com", serverid);
            return sServer;
        }
    }
}

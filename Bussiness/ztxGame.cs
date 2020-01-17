using System;
using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
    public class ztxGame
    {
        private const string sp_id = "1089";

        public static string Login(string sUserID,string sGame)
        {
            string login_ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string login_key = "2157655494752420761";
            string user_identity = "-1";
            string user_ip = ProvideCommon.GetRealIP();
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|{1}|{2}|{3}|{4}", sp_id,sUserID,login_ts,user_ip,login_key);
            string sign_str = ProvideCommon.MD5(sbText.ToString()).ToLower();//MD5(sp_id|user_id|login_ts|user_ip|login_key)
            sbText.Remove(0, sbText.Length);
            string sLoginUrl = "http://login.ss517.com/xdj/uniteGameLoginBackCookieValues.htm";
            sbText.AppendFormat("sp_id={0}", sp_id);
            sbText.AppendFormat("&user_id={0}", sUserID);
            sbText.AppendFormat("&user_name={0}", sUserID);
            sbText.AppendFormat("&user_identity={0}", user_identity);
            sbText.AppendFormat("&user_ip={0}", user_ip);
            sbText.AppendFormat("&login_ts={0}", HttpUtility.UrlEncode(login_ts));
            sbText.AppendFormat("&sign_str={0}",sign_str);
            sbText.Append("&game_id=3");
            string sValKey = ProvideCommon.GetPageInfoByPost(sLoginUrl, sbText.ToString(), "UTF-8");
            string sUrl = string.Empty;
            if (sValKey.Length > 2)
            {
                string prizeServerId = ServerName(sGame);//区服编号
                sUrl = string.Format("{2}?{0}&{1}", prizeServerId,sValKey,ServerUrl(sGame));
            }
            else 
            {
                sUrl = sValKey;
            }
            return sUrl;
        }

        public static string Pay(string sUserID, int iMoney, string sOrderID, string sGame)
        {
            string serial_id = string.Format("{0}{1}", sp_id, sOrderID.Substring(0, 20));
            string ctime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            int point = iMoney*10;//游戏虚拟货币数量 
            string user_ip = ProvideCommon.GetRealIP();
            string target_game = "3";
            string prizeServerId = ServerName(sGame);//区服编号
            string pay_key = "0346857442528289273";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|{1}|{2}|{3}|{4}|{5}|{6}", sUserID,serial_id, point, ctime, sp_id,user_ip,pay_key);
            string sign_str = ProvideCommon.MD5(sbText.ToString());//MD5(user_id|serial_id|point|ctime|spid|user_ip|pay_key)
            sbText.Remove(0, sbText.Length);
            string TranURL = "http://pay.bluepanda.cn/charge/cooperateExchange.action";
            sbText.AppendFormat("spid={0}&user_id={1}&serial_id={2}&point={3}&ctime={4}", sp_id, sUserID, serial_id, point.ToString(), HttpUtility.UrlEncode(ctime));
            sbText.AppendFormat("&user_ip={0}&target_game={1}&prizeServerId={2}&sign_str={3}", user_ip, target_game, prizeServerId,sign_str);
            string sRes = ProvideCommon.GetPageInfoByPost(TranURL, sbText.ToString(), "UTF-8");
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", TranURL, sbText.ToString());
            GamePayBLL.GamePayAdd(user_ip, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string ztxPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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

        public static string ztxQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iMoney = Convert.ToInt32(dPrice);
            int iUserID = UserBll.UserIDSel(sUserName);
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = iMoney * 10;
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sRes = Pay(iUserID.ToString(), iMoney, sTranID, sGameAbbre);
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "0":
                    int iGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    if (iGRes == 0)
                    {
                        sReturn = sRes;
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

        public static string ServerUrl(string sGame)
        {
            string sServer = string.Empty;
            switch (sGame)
            {
                case "ztx1":
                    sServer = "http://ztx1.dao50.com/s1/index.html";
                    break;
                case "ztx2":
                    sServer = "http://ztx1.dao50.com/s2/index.html";
                    break;
                case "ztx3":
                    sServer = "http://ztx1.dao50.com/s3/index.html";
                    break;
            }
            return sServer;
        }

        public static string ServerName(string sGame)
        {
            string sServer = string.Empty;
            switch (sGame)
            {
                case "ztx1":
                    sServer = "222";
                    break;
                case "ztx2":
                    sServer = "224";
                    break;
                case "ztx3":
                    sServer = "225";
                    break;
            }
            return sServer;
        }
    }
}

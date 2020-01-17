using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Bussiness
{
   public class ahxyGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string serverid = GetServerID(sGame);
            string sServer = "S" + serverid;
            string ts = ProvideCommon.getTime().ToString();
            string key = "Gvbk0y4wHHZU7CFXQJBkIwcW7Dwr8nlM";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", ts);
            sbText.AppendFormat("{0}", key);
            sbText.AppendFormat("{0}", "1");
            sbText.AppendFormat("{0}",sServer);
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$ts|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://{0}.ahxy.dao50.com/doLogon.php?", sServer);
            sbText.AppendFormat("username={0}", sUserID);
            sbText.AppendFormat("&server={0}", sServer);
            sbText.AppendFormat("&time={0}", ts);
            sbText.AppendFormat("&flag={0}",ticket);
            sbText.AppendFormat("&cm={0}", "1");
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
           
            string paytime = ProvideCommon.getTime().ToString();
            int iMoney = Convert.ToInt32(dMoney);
            int gold = iMoney * 10;
            string serverid = GetServerID(sGame);
            string sServer = "S" + serverid;
            string key = "8dndyldpqr0qr0asd834heusdjajdwfsdfdr";
            string strGame = "ahxy";
            string sAgent = "dao50";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", key);
            sbText.AppendFormat("{0}",sUserID);
            sbText.AppendFormat("{0}", sOrderID.Substring(0, 30));
            sbText.AppendFormat("{0}", iMoney.ToString());
            sbText.AppendFormat("{0}", paytime);
            sbText.AppendFormat("{0}", sAgent);
            sbText.AppendFormat("{0}",sServer);
            sbText.AppendFormat("{0}", strGame);
            string sSign = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$paytime|$gold|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            string sGamePayUrl = "http://pay.union.qq499.com:8029/pay_sync_togame.php";
            sbText.AppendFormat("game={0}", strGame);
            sbText.AppendFormat("&agent={0}", sAgent);
            sbText.AppendFormat("&user={0}", sUserID);
            sbText.AppendFormat("&order={0}", sOrderID.Substring(0, 30));
            sbText.AppendFormat("&money={0}", iMoney);
            sbText.AppendFormat("&server={0}", sServer);
            sbText.AppendFormat("&time={0}", paytime);
            sbText.AppendFormat("&sign={0}", sSign);

            string sRes = ProvideCommon.GetPageInfoByPost(sGamePayUrl, sbText.ToString(), "UTF-8");
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;

        }

        public static string ahxyPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            decimal dMoney = (Convert.ToDecimal(iPayPoints)) / 10;
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
                case "1":
                case "-7":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string ahxyQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);

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
                case "-7":
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    sReturn = "0";
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sID = sGame.Replace("ahxy", "");
            return sID.ToString();
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string serverid = GetServerID(sGameAbbre);
            string sServer = "S" + serverid;
            string ts = ProvideCommon.getTime().ToString();
            string key = "0E7575E6347511E3957A73321EB9F18F";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", ts);
            sbText.AppendFormat("{0}", key);
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$ts|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://{0}.ahxy.dao50.com/phpCommon/union/role_list.php?", sServer);
            sbText.AppendFormat("username={0}", sUserID);
            sbText.AppendFormat("&time={0}", ts);
            sbText.AppendFormat("&flag={0}", ticket);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string sReturn = string.Empty;
            switch (sRes)
            { 
                case "-1":
                case "-2":
                case "-3":
                    sReturn = "1";
                    break;
                default:
                    sReturn = "0";
                    break;
            }
            return sReturn;
        }

        public static string GetNewCode(string sUserID)
        {
            string key = "Gvbk0y4wHHZU7CFXQJBkIwcW7Dwr8nlM";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(key);
            sbText.Append(sUserID);
            string sNewCode = ProvideCommon.MD5(sbText.ToString());
            return sNewCode;
        }
    }
}

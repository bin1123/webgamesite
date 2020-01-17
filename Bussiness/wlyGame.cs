using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common;

namespace Bussiness
{
    public class wlyGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string SECURITY_TICKET_LOGIN = "uqee20110301";
            string accid = sUserID;
            string accname = sUserID;
            string serverid = getServerID(sGame);
            string tstamp = ProvideCommon.getTime().ToString();
            string fcm = "1";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(accid);
            sbText.Append(accname);
            sbText.Append(serverid);
            sbText.Append(tstamp);
            sbText.Append(fcm);
            sbText.Append(SECURITY_TICKET_LOGIN);
            string ticket = string.Empty;
            ticket = ProvideCommon.MD5(sbText.ToString());//md5(accid + accname + serverid + tstamp + fcm + SECURITY_TICKET_LOGIN)
            sbText.Remove(0, sbText.Length);
            string serverdomain = getLoginDomain(sGame);
            sbText.AppendFormat("http://{0}/Start.aspx?", serverdomain);
            sbText.AppendFormat("accid={0}&", accid);
            sbText.AppendFormat("accname={0}&", accname);
            sbText.AppendFormat("tstamp={0}&", tstamp);
            sbText.AppendFormat("fcm={0}&", fcm);
            sbText.AppendFormat("serverid={0}&", serverid);
            sbText.AppendFormat("ticket={0}", ticket);
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, int iMoney, string sOrderID, string sGame)
        {
            string SECURITY_TICKET_PAY = "dao50dfvq34WUIOQRFl9HzbsdiKWQcBri2t465346df2h345I2u";
            string orderid = sOrderID;
            string loginname = sUserID;
            int golden = iMoney*10;
            string tstamp = ProvideCommon.getTime().ToString();//标准时间戳
            string order = sOrderID;
            StringBuilder sbText = new StringBuilder();
            sbText.Append(SECURITY_TICKET_PAY);
            sbText.Append(orderid);
            sbText.Append(loginname);
            sbText.Append(golden);
            sbText.Append(tstamp);
            string ticket = ProvideCommon.MD5(sbText.ToString());//ticket = md5(SECURITY_TICKET_PAY +orderid + loginname + golden + tstamp);
            sbText.Remove(0, sbText.Length);
            string serverdomain = getPayDomain(sGame);
            sbText.AppendFormat("http://{0}/ops/addpay?", serverdomain);
            sbText.AppendFormat("loginname={0}&", loginname);
            sbText.AppendFormat("orderid={0}&", orderid);
            sbText.AppendFormat("golden={0}&", golden);
            sbText.AppendFormat("tstamp={0}&", tstamp);
            sbText.AppendFormat("ticket={0}", ticket);
            string sTranURL = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sTranURL);
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sTranURL, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string wlyPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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

        public static string wlyQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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

        private static string getLoginDomain(string sGame)
        {
            string sDomain = string.Empty;
            switch (sGame)
            {
                case "wly1":
                    sDomain = "wly1.dao50.com";
                    break;
            }
            return sDomain;
        }

        private static string getPayDomain(string sGame)
        {
            string sDomain = string.Empty;
            switch (sGame)
            {
                case "wly1":
                    sDomain = "wly1.dao50.com:9130";
                    break;
            }
            return sDomain;
        }

        private static string getServerID(string sGame)
        {
            string sServerID = string.Empty;
            switch(sGame)
            {
                case "wly1":
                    sServerID = "wly-86-010-bj-dao50-001";
                    break;
            }
            return sServerID;
        }
    }
}

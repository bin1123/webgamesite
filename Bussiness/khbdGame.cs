using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;
using System.Text.RegularExpressions;

using Common;
using DataEnity;

namespace Bussiness
{
    public class khbdGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string SECURITY_TICKET_LOGIN = "khbd_mhjh_123456";
            string serverid = GetServerID(sGame);
            string tstamp = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(serverid);
            sbText.Append(tstamp);
            sbText.Append("1");
            sbText.Append(SECURITY_TICKET_LOGIN);
            string ticket = ProvideCommon.MD5(sbText.ToString());// loginname + serverid + tstamp + fcm + SECURITY_TICKET_LOGIN
            string sGameUrl = string.Format("http://s{1}.khbd.dao50.com/login.php?loginname={0}&serverid={1}&fcm=1&tstamp={2}&ticket={3}&platform=&source=",
                                            sUserID, serverid, tstamp, ticket);
            return sGameUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string SECURITY_TICKET_PAY = "59cffe0f86ceb8bda13947b277f47ddc";
            int golden = Convert.ToInt32(dMoney) * 10;
            string tstamp = ProvideCommon.getTime().ToString();
            string serverid = GetServerID(sGame);
            StringBuilder sbText = new StringBuilder();
            sbText.Append(SECURITY_TICKET_PAY);
            sbText.AppendFormat("golden{0}", golden);
            sbText.AppendFormat("loginname{0}", sUserID);
            sbText.AppendFormat("orderid{0}", sOrderID);
            sbText.AppendFormat("serverid{0}", serverid);
            sbText.AppendFormat("tstamp{0}", tstamp);
            string ticket = ProvideCommon.MD5(sbText.ToString());
            string TranURL = string.Format("http://s{0}.khbd.dao50.com:9130/ops/addpay/ops/addpay", serverid);
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("orderid={0}&loginname={1}&golden={2}&tstamp={3}&ticket={4}&serverid={5}",
                                sOrderID, sUserID, golden, tstamp, ticket, serverid);
            string sRes = ProvideCommon.GetPageInfoByPost(TranURL, sbText.ToString(), "UTF-8");
            string user_ip = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", TranURL, sbText.ToString());
            GamePayBLL.GamePayAdd(user_ip, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string khbdPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            string sPayCode = ProvideCommon.getJsonValue("code", sRes);
            string sReturn = string.Empty;
            if (sPayCode == "0" || sPayCode == "2")
            {
                sReturn = string.Format("0|{0}", sTranID);
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string khbdQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = Convert.ToInt32(dPrice * 10);
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string sPayCode = ProvideCommon.getJsonValue("code", sRes);
            string sReturn = string.Empty;
            if (sPayCode == "0" || sPayCode == "2")
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

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string sReturn = string.Empty;
            string serverid = GetServerID(sGameAbbre);
            string sQueryUrl = string.Format("http://s{0}.khbd.dao50.com:9130/ops/query?type=1&name={1}", serverid, sUserID);
            string sRes = ProvideCommon.GetPageInfo(sQueryUrl);
            if (sRes == "")
            {
                sReturn = "1";
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sServerID = sGame.Replace("khbd", "");
            string sRealSID = string.Empty;
            switch (sServerID)
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
                case "10":
                case "11":
                case "12":
                case "13":
                case "14":
                case "15":
                case "16":
                case "17":
                case "18":
                case "19":
                case "20":
                case "21":
                case "22":
                case "23":
                case "24":
                case "25":
                case "26":
                    sRealSID = "1001";
                    break;
                case "27":
                case "28":
                case "29":
                case "30":
                case "31":
                case "32":
                    sRealSID = "1002";
                    break;
                default:
                    sRealSID = sServerID;
                    break;
            }
            return sRealSID;
        }
    }
}

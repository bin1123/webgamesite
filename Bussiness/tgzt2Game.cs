using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Bussiness
{
    public class tgzt2Game
    {
        public static string Login(string sUserID, string sGame)
        {
            string agentName = "dao50v2";
            string TICKET_LOGIN = "dao50v2::TGZT::LOGIN::KEY::ZggbxpI4ktSglj";
            string serverid = GetServerID(sGame);
            string stamp = ProvideCommon.getTime().ToString();//标准时间戳
            string fcm = "1";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(TICKET_LOGIN);
            sbText.Append(sUserID);
            sbText.Append(stamp);
            sbText.Append(agentName);
            sbText.Append(serverid);
            sbText.Append(fcm);
            string flag = ProvideCommon.MD5(sbText.ToString());//md5(LOGIN_KEY + accountName + stamp + agentName + serverID + fcm)

            sbText.Remove(0, sbText.Length);
            sbText.Append("http://web.tgzt.mingchaoonline.com/api/v1/mc/start.php?");
            sbText.AppendFormat("accountName={0}&", sUserID);
            sbText.AppendFormat("stamp={0}&", stamp);
            sbText.AppendFormat("agentName={0}&", agentName);
            sbText.AppendFormat("serverID={0}&", serverid);
            sbText.AppendFormat("fcm={0}&", fcm);
            sbText.AppendFormat("flag={0}", flag);           
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string agentName = "dao50v2";
            string TICKET_PAY = "dao50v2::TGZT::PAY::KEY::ChCAVUxKfmnEAk";
            string server = GetServerID(sGame);
            int iMoney = Convert.ToInt32(dMoney);
            string money = iMoney.ToString();
            string gold = (iMoney * 10).ToString();         
            string time= ProvideCommon.getTime().ToString();//标准时间戳
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sOrderID);
            sbText.Append(sUserID);
            sbText.Append(gold);
            sbText.Append(time);
            sbText.Append(TICKET_PAY);
            sbText.Append(agentName);
            sbText.Append(server);
            sbText.Append(money);
            string flag = ProvideCommon.MD5(sbText.ToString());//md5(order + username + gold + time + PAY_KEY + agentName + server + money)
            sbText.Remove(0, sbText.Length);
            string TranURL = "http://web.tgzt.mingchaoonline.com/api/v1/mc/pay.php?";
            sbText.Append(TranURL);
            sbText.AppendFormat("order={0}&", sOrderID);
            sbText.AppendFormat("username={0}&", sUserID);
            sbText.AppendFormat("money={0}&", money);
            sbText.AppendFormat("gold={0}&", gold);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("flag={0}&", flag);
            sbText.AppendFormat("server={0}&", server);
            sbText.AppendFormat("agentName={0}&", agentName);
            sbText.Append("moneyType=1");
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string tgztPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                case "1":
                case "2":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string tgztQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "2":
                    sReturn = "0";
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string sReturn = string.Empty;
            string agentName = "dao50v2";
            string TICKET_SEARCH = "dao50v2::TGZT::INFO::KEY::YSk99ZTjpXb2Nw";
            string serverid = GetServerID(sGameAbbre);
            string stamp = ProvideCommon.getTime().ToString();//标准时间戳
            StringBuilder sbText = new StringBuilder();
            sbText.Append(TICKET_SEARCH);
            sbText.Append(sUserID);
            sbText.Append(agentName);
            sbText.Append(serverid);
            sbText.Append(stamp);
            string flag = ProvideCommon.MD5(sbText.ToString());//md5(SEARCH_KEY + accountName+ AgentName + serverID + stamp) 
            sbText.Remove(0, sbText.Length);
            sbText.Append("http://web.tgzt.mingchaoonline.com/api/v1/mc/getAccountInfo.php?");
            sbText.AppendFormat("accountName={0}&", sUserID);
            sbText.AppendFormat("agentName={0}&", agentName);
            sbText.AppendFormat("serverID={0}&", serverid);
            sbText.AppendFormat("stamp={0}&", stamp);
            sbText.AppendFormat("flag={0}", flag);   
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            if (sRes == "-3")
            {
                sReturn = "1";
            }
            else
            {
                sReturn = "0";
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sID = sGame.Replace("dtgzter", "");
            return sID;
        }
    }
}

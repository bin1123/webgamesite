using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Bussiness
{
    public class xlfcGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string agentid = "15";
            string TICKET_LOGIN = "dao50::XLFC::Login::KEY::Y1YV62ZxyXEWOu6v8rsD";
            string serverid = GetServerID(sGame);
            string tstamp = ProvideCommon.getTime().ToString();//标准时间戳
            string fcm = "1";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(TICKET_LOGIN);
            sbText.Append(sUserID);
            sbText.Append(tstamp);
            sbText.Append(agentid);
            sbText.Append(serverid);
            sbText.Append(fcm);
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5(API_SECURITY_TICKET_LOGIN + account + tstamp+ agentid + serverid +fcm)

            sbText.Remove(0, sbText.Length);
            sbText.Append("http://web.xlfc.dao50.com/user/start.php?");
            sbText.AppendFormat("account={0}&", sUserID);
            sbText.AppendFormat("tstamp={0}&", tstamp);
            sbText.AppendFormat("agentid={0}&", agentid);
            sbText.AppendFormat("serverid={0}&", serverid);
            sbText.AppendFormat("fcm={0}&", fcm);
            sbText.AppendFormat("ticket={0}&", ticket);           
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string AgentID = "15";
            string TICKET_PAY = "dao50::XLFC::Pay::KEY::m9uMnDX64RMYzCk931Gn";
            string Serverid = GetServerID(sGame);
            int iMoney = Convert.ToInt32(dMoney);
            string PayMoney = iMoney.ToString();
            string PayGold = (iMoney * 10).ToString();          
            string PayTime= ProvideCommon.getTime().ToString();//标准时间戳
            string user_ip = ProvideCommon.GetRealIP();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(TICKET_PAY);
            sbText.Append(sOrderID);
            sbText.Append(sUserID);
            sbText.Append(PayMoney);
            sbText.Append(PayGold);
            sbText.Append(PayTime);
            sbText.Append(AgentID);
            sbText.Append(Serverid);
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5($API_SECURITY_TICKET_PAY . PayNum . PayToUser . PayMoney . PayGold . PayTime . AgentID. ServerID);
            sbText.Remove(0, sbText.Length);
            string TranURL = "http://web.xlfc.dao50.com/api/pay.php?";
            sbText.Append(TranURL);
            sbText.AppendFormat("PayNum={0}&", sOrderID);
            sbText.AppendFormat("PayToUser={0}&", sUserID);
            sbText.AppendFormat("PayMoney={0}&", PayMoney);
            sbText.AppendFormat("PayGold={0}&", PayGold);
            sbText.AppendFormat("PayTime={0}&", PayTime);
            sbText.AppendFormat("AgentID={0}&", AgentID);
            sbText.AppendFormat("ServerID={0}&", Serverid);
            sbText.AppendFormat("ticket={0}", ticket);  
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string xlfcPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                case "success":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string xlfcQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "success":
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
                case "PayNum exist":
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
            string sReturn = string.Empty;
            string TICKET_INFO = "dao50::XLFC::INFO::KEY::03IAM3DmCp8myAwfqWre";
            string agentid = "15";
            string serverid = GetServerID(sGameAbbre);
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(TICKET_INFO);
            sbText.Append(time);
            sbText.Append(sUserID);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5($API_SECURITY_TICKET_INFO,$time.$user_name)
            sbText.Remove(0, sbText.Length);
            string TranURL = "http://web.xlfc.dao50.com/api/info.player.php?";
            sbText.Append(TranURL);
            sbText.AppendFormat("user_name={0}&", sUserID);
            sbText.AppendFormat("agentid={0}&", agentid);
            sbText.AppendFormat("serverid={0}&", serverid);
            sbText.AppendFormat("t={0}&", time);
            sbText.AppendFormat("s={0}", sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            if (sRes == "not found")
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
            string sID = sGame.Replace("xlfc", "");
            string serverid = "1001";
            return serverid;
        }

        public static string GetNewCode(string sGameAbbre, string sUserID,string sCodeType)
        {
            StringBuilder sbText = new StringBuilder();
            string N = new Random().Next(1, 1000).ToString();
            string TICKET_LOGIN = "dao50::XLFC::Login::KEY::Y1YV62ZxyXEWOu6v8rsD";
            string agentid = "15";
            string name = "xlfc";
            string sServer = string.Format("S{0}",GetServerID(sGameAbbre));
            string PublishKey = string.Empty;
            switch(sCodeType)
            {
                case "xlfcxsk":
                    PublishKey = "xs";
                    break;
                case "xlfclthy":
                    PublishKey = "m2";
                    break;
                case "xlfcxccj":
                    PublishKey = "m3";
                    break;
                case "xlfcyylm":
                    PublishKey = "m1";
                    break;
            }
            sbText.Append(TICKET_LOGIN);
            sbText.Append(PublishKey);
            sbText.Append(name);
            sbText.Append(agentid);
            sbText.Append(sServer);
            sbText.Append(N);
            string sCode = ProvideCommon.MD5(sbText.ToString());//$Key.$PublishKey.”xlfc”.$agentID.”S1”.$N
            string sNewCode = string.Format("{0}-{1}-{2}",PublishKey,N,sCode);//$publishKey.”-”.$N.”-”.$Code
            return sNewCode;
        }
    }
}

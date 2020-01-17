using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Bussiness
{
   public class zwxGame
   {
       public static string Login(string sUserID, string sGame)
       {
           string accid = "0";
           string accname = sUserID;
           string SERVER_KEY = "=zwx=dao50=VyUVNN:nLFdOW:GMNIoC";
           string serverid = GetServerID(sGame);
           string tstamp = ProvideCommon.getTime().ToString();//标准时间戳
           string fcm = "1";
           StringBuilder sbText = new StringBuilder();
           sbText.Append(accid);
           sbText.Append(accname);
           sbText.Append(tstamp);
           sbText.Append(SERVER_KEY);
            // md5(accid + accname + tstamp+ SERVER_KEY)
           string strTicket = ProvideCommon.MD5(sbText.ToString());//md5(LOGIN_KEY + accountName + stamp + agentName + serverID + fcm)

           sbText.Remove(0, sbText.Length);
           sbText.AppendFormat("http://s{0}.zwx.dao50.com/user/start.php?",serverid);
           sbText.AppendFormat("accid={0}&", accid);
           sbText.AppendFormat("accname={0}&", accname);
           sbText.AppendFormat("tstamp={0}&", tstamp);
           sbText.AppendFormat("fcm={0}&", fcm);
           sbText.AppendFormat("ticket={0}", strTicket);
           string sUrl = sbText.ToString();
           return sUrl;
       }

       public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
       {
           string PayToUser = sUserID;
           string PAY_KEY = "=zwx=dao50=X1QZ7j:oc9fQV:1nUGTm";
           string Mode = "1";
           string serverid = GetServerID(sGame);
           int iMoney = Convert.ToInt32(dMoney);
           string money = iMoney.ToString();
           string gold = (iMoney * 10).ToString();
           string time = ProvideCommon.getTime().ToString();//标准时间戳
           StringBuilder sbText = new StringBuilder();
           sbText.Append(PAY_KEY);
           sbText.Append(Mode);
           sbText.Append(sOrderID);
           sbText.Append(PayToUser);
           sbText.Append(money);
           sbText.Append(gold);
           sbText.Append(time);
           //md5(PAY_KEY + Mode + PayNum + PayToUser + PayMoney + PayGold + PayTime )
           string flag = ProvideCommon.MD5(sbText.ToString());//md5(order + username + gold + time + PAY_KEY + agentName + server + money)
           sbText.Remove(0, sbText.Length);
           string TranURL = string.Format("http://s{0}.zwx.dao50.com/user/pay.php?",serverid);
           sbText.Append(TranURL);
           sbText.AppendFormat("PayToUser={0}&", sUserID);
           sbText.AppendFormat("serverid={0}&", serverid);
           sbText.AppendFormat("PayNum={0}&", sOrderID);
           sbText.AppendFormat("PayMoney={0}&", money);
           sbText.AppendFormat("PayGold={0}&", gold);
           sbText.AppendFormat("Mode={0}&", Mode);
           sbText.AppendFormat("ticket={0}&", flag);
           sbText.AppendFormat("PayTime={0}", time);
           //?PayToUser=123&serverid=1&PayNum=1332147643&PayMoney=100&PayGold=1000&Mode=1&ticket=e819b728fa59685a3587f9d2813b312b&PayTime=1332147643
           string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
           int iUserID = 0;
           int.TryParse(sUserID, out iUserID);
           string user_ip = ProvideCommon.GetRealIP();
           GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
           return sRes;
       }

       public static string zwxPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
               
               case "true":
                   sReturn = string.Format("0|{0}", sTranID);
                   break;
               default:
                   sReturn = sRes;
                   break;
           }
           return sReturn;
       }

       public static string zwxQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
               case "true":
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
               case "paynum_exist":
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
           string TICKET_SEARCH = "=zwx=dao50=VyUVNN:nLFdOW:GMNIoC";
           string serverid = GetServerID(sGameAbbre);
           string stamp = ProvideCommon.getTime().ToString();//标准时间戳
           StringBuilder sbText = new StringBuilder();
           sbText.Append(stamp);
           sbText.Append(TICKET_SEARCH);
           
           //md5(time  + SERVER_KEY)
           string flag = ProvideCommon.MD5(sbText.ToString());//md5(SEARCH_KEY + accountName+ AgentName + serverID + stamp) 
           sbText.Remove(0, sbText.Length);
           sbText.AppendFormat("http://s{0}.zwx.dao50.com/user/active.php?",serverid);
           sbText.AppendFormat("username={0}&", sUserID);
           sbText.AppendFormat("time={0}&", stamp);
           sbText.AppendFormat("ticket={0}", flag);
           //username=123&time=1234567891&ticket= a906449d5769fa7361d7ecc6aa3f6d28
           string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
           if (sRes == "0")
           {
               sReturn = "1";
           }
           else
           {
               sReturn = "0";
           }
           return sReturn;
       }

       public static string GetNewCode(string sUserID, string sGameAbbre)
       {
           string sLoginkey = "sZ5Y9EejuXRwMynE";
           string sServerID = sGameAbbre.Replace("zwx","S");
           
           StringBuilder sbText = new StringBuilder();
           sbText.Append(sUserID);
           sbText.Append(sServerID);
           sbText.Append(sLoginkey);
           string sNewCode = ProvideCommon.MD5(sbText.ToString()).ToUpper();//md5(平台账号 . 服务器号 . KEY)
           return sNewCode;
       }

       public static string GetServerID(string sGame)
       {
           string sID = sGame.Replace("zwx", "");
           return sID;
       }
    }
}

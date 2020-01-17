using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Bussiness
{
   public class jyqzGame
   {
       public static string Login(string sUserID, string sGame, string sClient)
       {
           string serverid = GetServerID(sGame);
           string timestamp = ProvideCommon.getTime().ToString();
           string ptid = "";
           string isAdult = "1";
           string key = "QY7RODDD-3rSDEDE-7roadjy-489385j-25255fdf-7ROADJyfd-SHENQU-Lvoe7road";
           StringBuilder sbText = new StringBuilder();
           sbText.AppendFormat("{0}", serverid);
           sbText.AppendFormat("{0}", sUserID);
           sbText.AppendFormat("{0}", timestamp);
           sbText.AppendFormat("{0}", isAdult);
           sbText.AppendFormat("{0}", key);
           sbText.AppendFormat("{0}", ptid);
           string sign = ProvideCommon.MD5(sbText.ToString()).ToLower();//MD5(coopname=&serverid=&userid=&key=&timestamp=)
           sbText.Remove(0, sbText.Length);
           sbText.Append("http://login.ly.cdn.com/TJMain.htm?");
           sbText.AppendFormat("ptid={0}", ptid);
           sbText.AppendFormat("&sid={0}", serverid);
           sbText.AppendFormat("&username={0}", sUserID);
           sbText.AppendFormat("&time={0}", timestamp);
           sbText.AppendFormat("&isAdult={0}", isAdult);
           sbText.AppendFormat("&flag={0}", sign);
           return sbText.ToString();
       }

       public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
       {
           string serverid = GetServerID(sGame);
           string amount = dMoney.ToString(); ;
           string timestamp = ProvideCommon.getTime().ToString();
           string p6_Status="1";
           string ptid = "";
           string p1_MerId = "";
           decimal money = Convert.ToInt32(dMoney * 10);
           string key = "QY569fdd-56812ef-loveWAN-7roadjy-25ea3495-7R53MYCNX-shenqu-lovedede7";
           StringBuilder sbText = new StringBuilder();
           sbText.AppendFormat("{0}", p1_MerId);
           sbText.AppendFormat("{0}", sUserID);
           sbText.AppendFormat("{0}", sOrderID);
           sbText.AppendFormat("{0}", dMoney);
           sbText.AppendFormat("{0}", serverid);
           sbText.AppendFormat("{0}", p6_Status);
           sbText.AppendFormat("{0}", money);
           sbText.AppendFormat("{0}", ptid);
           sbText.AppendFormat("{0}", key);
           string sSign = ProvideCommon.MD5(sbText.ToString());//MD5签名sign = md5(chargeid + username +money +payway + needmoney +key);
           sbText.Remove(0, sbText.Length);
           sbText.Append("http://pay.jyqz.cy.cn/pay.jsp?");
           sbText.AppendFormat("p1_MerId={0}", p1_MerId);
           sbText.AppendFormat("&p2_User={0}", sUserID);
           sbText.AppendFormat("&p3_Order={0}", sOrderID);
           sbText.AppendFormat("&p4_Amt={0}", dMoney);
           sbText.AppendFormat("&p5_Sid={0}", serverid);
           sbText.AppendFormat("&p6_Status={0}", p6_Status);
           sbText.AppendFormat("&p7_Coin={0}", money);
           sbText.AppendFormat("&p8_Ptid={0}", ptid);
           sbText.AppendFormat("&hmac={0}", sSign);
           string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
           string sTranIP = ProvideCommon.GetRealIP();
           int iUserID = 0;
           int.TryParse(sUserID, out iUserID);
           string sGamePayUrl = string.Empty;
           string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
           GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
           return sRes;
       }

       public static string jyqzPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
               case "0":
               case "-5":
                   sReturn = string.Format("0|{0}", sTranID);
                   break;
               default:
                   sReturn = sRes;
                   break;
           }
           return sReturn;
       }

       public static string jyqzQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
       {
           int iUserID = UserBll.UserIDSel(sUserName);
           string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
           string sReturn = string.Empty;
           switch (sRes)
           {
               case "0":
               case "-5":
                   int iGRes =TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
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

       public static string GetServerID(string sGame)
       {
           string sID = sGame.Replace("mhtj", "");
           return sID.ToString();
       }
       public static string GameisLogin(string sUserID, string sGameAbbre)
       {
           string serverid = GetServerID(sGameAbbre);
           string merId = "";
           string ptid = "";
           StringBuilder sbText = new StringBuilder();
           sbText.Append("http://pay.jyqz.cy.cn/checkuser.jsp?");
           sbText.AppendFormat("merId={0}", merId);
           sbText.AppendFormat("&serverId={0}", serverid);
           sbText.AppendFormat("&loginName={0}", sUserID);
           sbText.AppendFormat("&ptid={0}", ptid);
           string sUrl = sbText.ToString();
           string sRes = ProvideCommon.GetPageInfo(sUrl).Trim();
           string sReturn = string.Empty;
           if (sRes == "-1")
           {
               sReturn = "1";
           }
           else
           {
               sReturn = "0";
           }
           return sReturn;
       }
      
    }
}

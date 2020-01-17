using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common;

namespace Bussiness
{
    public class sssgGame
    {
        private const string key = "ybMimAkVSQG8EdHubl0j";

        public static string Login(string sUserID, string sGame,string sSource,string client)
        {
            string user = sUserID;
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.AppendFormat("_{0}_",time);
            string sign = string.Empty;
            if (sSource != null && sSource.Length > 0)
            {
                sbText.AppendFormat("{0}_",sSource);
            }
            sbText.Append(key);
            sign = ProvideCommon.MD5(sbText.ToString());//md5(user_time_key)
            sbText.Remove(0, sbText.Length);
            string serverdomain = GetDomain(sGame);
            sbText.AppendFormat("http://{0}/api/login?",serverdomain);
            sbText.AppendFormat("user={0}&",user);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("sign={0}&", sign);
            sbText.AppendFormat("source={0}&", sSource);
            sbText.AppendFormat("client={0}&", client);
            sbText.Append("nickname=&fangchenmi=0");
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string user = sUserID;
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            string order = sOrderID;
            int gold = Convert.ToInt32((dMoney * 100));//充值银两,比例1:100，即1RMB=100gold
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.AppendFormat("_{0}_",time);
            sbText.AppendFormat("{0}_",order);
            sbText.AppendFormat("{0}_",gold);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//sign的值为user,time,order,gold,密钥这5个参数用下划线拼接后md5值
            sbText.Remove(0,sbText.Length);
            string serverdomain = GetDomain(sGame);
            string TranURL = string.Format("http://{0}/api/charge", serverdomain); 
            sbText.AppendFormat("user={0}&",user);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("order={0}&", sOrderID);
            sbText.AppendFormat("gold={0}&", gold);
            sbText.AppendFormat("sign={0}", sign);
            string sRes = ProvideCommon.GetPageInfoByPost(TranURL, sbText.ToString(), "UTF-8");
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", TranURL, sbText.ToString());
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string sssgPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                case "1":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string sssgQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string user = sUserID;
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            string sDomain = GetDomain(sGameAbbre);
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.AppendFormat("_{0}_", time);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//sign的值为user,time,密钥这3个参数用下划线拼接后md5值
            sbText.Remove(0, sbText.Length);
            string TranURL = string.Format("http://{0}/api/verify_user",sDomain);
            sbText.Append(TranURL);
            sbText.AppendFormat("?user={0}&", user);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("sign={0}", sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sReturn = string.Empty;
            switch(sRes)
            {
                case "1":
                case "2":
                    sReturn = "0";
                    break;
                case "-4":
                case "-5":
                    sReturn = "1";
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GetDomain(string sGame)
        {
            string sDomain = string.Empty;
            string sServerID = sGame.Replace("sssg", "");
            sDomain = string.Format("sg{0}.dao50.com",sServerID);
            return sDomain;            //switch (sGame)
            //{
            //    case "sssg1":
            //        sDomain = "sg1.dao50.com";
            //        break;
            //    case "sssg2":
            //        sDomain = "sg2.dao50.com";
            //        break;
            //    case "sssg3":
            //        sDomain = "sg3.dao50.com";
            //        break;
            //    case "sssg4":
            //        sDomain = "sg4.dao50.com";
            //        break;
            //    case "sssg5":
            //        sDomain = "sg5.dao50.com";
            //        break;
            //    case "sssg6":
            //        sDomain = "sg6.dao50.com";
            //        break;
            //    case "sssg7":
            //        sDomain = "sg7.dao50.com";
            //        break;
            //    case "sssg8":
            //        sDomain = "sg8.dao50.com";
            //        break;
            //    case "sssg9":
            //        sDomain = "sg9.dao50.com";
            //        break;
            //    case "sssg10":
            //        sDomain = "sg10.dao50.com";
            //        break;
            //    case "sssg11":
            //        sDomain = "sg11.dao50.com";
            //        break;
            //    case "sssg12":
            //        sDomain = "sg12.dao50.com";
            //        break;
            //    case "sssg13":
            //        sDomain = "sg13.dao50.com";
            //        break;
            //    case "sssg14":
            //        sDomain = "sg14.dao50.com";
            //        break;
            //    case "sssg15":
            //        sDomain = "sg15.dao50.com";
            //        break;
            //    case "sssg16":
            //        sDomain = "sg16.dao50.com";
            //        break;
            //    case "sssg17":
            //        sDomain = "sg17.dao50.com";
            //        break;
            //    case "sssg18":
            //        sDomain = "sg18.dao50.com";
            //        break;
            //    case "sssg19":
            //        sDomain = "sg19.dao50.com";
            //        break;
            //    case "sssg20":
            //        sDomain = "sg20.dao50.com";
            //        break;
            //    case "sssg21":
            //        sDomain = "sg21.dao50.com";
            //        break;
            //    case "sssg22":
            //        sDomain = "sg22.dao50.com";
            //        break;
            //    case "sssg23":
            //        sDomain = "sg23.dao50.com";
            //        break;
            //    case "sssg24":
            //        sDomain = "sg24.dao50.com";
            //        break;
            //    case "sssg25":
            //        sDomain = "sg25.dao50.com";
            //        break;
            //    case "sssg26":
            //        sDomain = "sg26.dao50.com";
            //        break;
            //    case "sssg27":
            //        sDomain = "sg27.dao50.com";
            //        break;
            //    case "sssg28":
            //        sDomain = "sg28.dao50.com";
            //        break;
            //    case "sssg29":
            //        sDomain = "sg29.dao50.com";
            //        break;
            //    case "sssg30":
            //        sDomain = "sg30.dao50.com";
            //        break;
            //    case "sssg31":
            //        sDomain = "sg31.dao50.com";
            //        break;
            //    case "sssg32":
            //        sDomain = "sg32.dao50.com";
            //        break;
            //    case "sssg33":
            //        sDomain = "sg33.dao50.com";
            //        break;
            //    case "sssg34":
            //        sDomain = "sg34.dao50.com";
            //        break;
            //    case "sssg35":
            //        sDomain = "sg35.dao50.com";
            //        break;
            //}
        }
    }
}

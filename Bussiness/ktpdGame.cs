using System;
using System.Text;

using Common;

namespace Bussiness
{
    public class ktpdGame
    {
        private const string key = "1CdxOux9Gc2jwXICCfrT";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sUserID"></param>
        /// <param name="sGame"></param>
        /// <param name="sSource"></param>
        /// <param name="sClient">pc:程序端登录,web:浏览器登录</param>
        /// <returns></returns>
        public static string Login(string sUserID, string sGame,string sSource,string sClient)
        {
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.AppendFormat("_{0}_",time);
            if (sSource != null && sSource.Length > 0)
            {
                sbText.AppendFormat("{0}_",sSource);
            }
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());
            sbText.Remove(0, sbText.Length);
            string fangchenmi = "0";
            string serverdomain = GetDomain(sGame);
            sbText.AppendFormat("http://{0}/api/login?", serverdomain);
            sbText.AppendFormat("user={0}&",sUserID);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("sign={0}&", sign);
            sbText.AppendFormat("source={0}&", sSource);
            sbText.AppendFormat("fangchenmi={0}&", fangchenmi);
            sbText.AppendFormat("client={0}&", sClient);
            sbText.Append("non_kid=1");
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            int gold = Convert.ToInt32((dMoney * 10));//充值元宝,比例1:10，即1RMB=10元宝
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.AppendFormat("_{0}_",time);
            sbText.AppendFormat("{0}_",sOrderID);
            string sGold = gold.ToString();
            sbText.AppendFormat("{0}_",sGold);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//user,time,order,gold,密钥这5个参数用下划线拼接后md5值 
            sbText.Remove(0,sbText.Length);
            string domain = GetDomain(sGame);
            string TranURL = string.Format("http://{0}/api/charge",domain);
            sbText.Append(TranURL);
            sbText.AppendFormat("?user={0}&",sUserID);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("order={0}&", sOrderID);
            sbText.AppendFormat("gold={0}&", gold);
            sbText.AppendFormat("sign={0}", sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string ktpdPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string ktpdQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "-15":
                    sReturn = "0";
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GetNewCode(string sUserID,string sGameAbbre)
        {
            StringBuilder sbText = new StringBuilder();
            string sDomain = getCodeDomain(sGameAbbre);
            sbText.AppendFormat("{0}_{1}", sUserID,sDomain);
            string sNewCode = ProvideCommon.MD5(sbText.ToString());
            return sNewCode;
        }


        public static string getCodeDomain(string sGame)
        {
            string sDomain = string.Empty;
            string sServerID = sGame.Replace("ktpd", "");
            sDomain = string.Format("s{0}.dao50_new.xd.com", sServerID);
            return sDomain;
        }

        public static string GameisLogin(string sUserID,string sGameAbbre)
        {
            string user = sUserID;
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            sbText.AppendFormat("_{0}_", time);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//user,time,密钥这3个参数用下划线拼接后md5值
            sbText.Remove(0, sbText.Length);
            string sDomain = GetDomain(sGameAbbre);
            string TranURL = string.Format("http://{0}/api/verify_user",sDomain);
            sbText.Append(TranURL);
            sbText.AppendFormat("?user={0}&", user);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("sign={0}", sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "1":
                    sReturn = "0";
                    break;
                case "2":
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
            string sServerID = sGame.Replace("ktpd", "");
            sDomain = string.Format("s{0}.ktpd.dao50.com",sServerID);
            return sDomain;
        }
    }
}

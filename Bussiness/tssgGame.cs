using System;
using System.Text;

using Common;

namespace Bussiness
{
    public class tssgGame
    {
        private const string key = "2HmMa4jGqwST39j";

        public static string Login(string sUserID, string sGame, string fuid)
        {
            string user = sUserID;
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.AppendFormat("_{0}_", time);
            string sign = string.Empty;
            sbText.Append(key);
            sign = ProvideCommon.MD5(sbText.ToString());//md5(user_time_平台密钥)
            sbText.Remove(0, sbText.Length);
            string serverdomain = GetDomain(sGame);
            sbText.AppendFormat("http://{0}/signin.ashx?", serverdomain);
            sbText.AppendFormat("account={0}&", sUserID);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("sign={0}&", sign);
            sbText.AppendFormat("fuid={0}&", fuid);
            sbText.Append("fcm=1");
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string user = sUserID;
            string order = sOrderID;
            int gold = Convert.ToInt32((dMoney * 10));//充值元宝,比例1:10，即1RMB=10元宝
            string token = "0";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.AppendFormat("_{0}_", gold);
            sbText.AppendFormat("{0}_", order);
            sbText.AppendFormat("{0}_", token);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//sign的值为md5(user_gold_order_domain_平台密钥) 算法生成的哈希值(小写) 
            sbText.Remove(0, sbText.Length);
            string serverdomain = GetDomain(sGame);
            sbText.AppendFormat("http://{0}/pay.ashx", serverdomain);
            sbText.AppendFormat("?account={0}&", user);
            sbText.AppendFormat("gold={0}&", gold);
            sbText.AppendFormat("order={0}&", sOrderID);
            sbText.AppendFormat("token={0}&",token);
            sbText.AppendFormat("sign={0}", sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string tssgPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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

        public static string tssgQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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

        public static string GetDomain(string sGame)
        {
            string sDomain = string.Empty;
            string sServerID = sGame.Replace("tssg", "");
            sDomain = string.Format("s{0}.ts.dao50.com", sServerID);
            return sDomain;
        }
    }
}

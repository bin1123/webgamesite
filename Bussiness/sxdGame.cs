using System;
using System.Text;

using Common;

namespace Bussiness
{
    public class sxdGame
    {
        private const string key = "{b779f2dd-d532-3566-e0ab-9d3312f20919}";//"{D4EE863A-3714-4EE9-9F04-C7E3DC3E9924}";

        public static string Login(string sUserID, string sGame,string sSource)
        {
            string user = sUserID;
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sRegDate = UserBll.RegTimeSel(iUserID);
            string sRegDateC = ProvideCommon.getTime(DateTime.Parse(sRegDate)).ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.AppendFormat("_{0}_",time);
            string hash = string.Empty;
            if (sSource != null && sSource.Length > 0)
            {
                sbText.AppendFormat("{0}_",sSource);
            }
            sbText.Append(key);
            hash = ProvideCommon.MD5(sbText.ToString());//md5(user_time_平台密钥)
            sbText.Remove(0, sbText.Length);
            string serverdomain = GetDomain(sGame);
            sbText.AppendFormat("http://{0}/login_api.php?", serverdomain);
            sbText.AppendFormat("user={0}&",user);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("hash={0}&", hash);
            sbText.AppendFormat("source={0}&", sSource);
            sbText.AppendFormat("regdate={0}&",sRegDateC);
            sbText.Append("non_kid=1");
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string user = sUserID;
            string order = sOrderID;
            int gold = Convert.ToInt32((dMoney * 10));//充值元宝,比例1:10，即1RMB=10元宝
            string domain = GetDomain(sGame);
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.AppendFormat("_{0}_",gold);
            sbText.AppendFormat("{0}_",order);
            sbText.AppendFormat("{0}_",domain);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//sign的值为md5(user_gold_order_domain_平台密钥) 算法生成的哈希值(小写) 
            sbText.Remove(0,sbText.Length);
            string serverdomain = GetDomain(sGame);
            string TranURL = "http://api.sxd.xd.com/api/buygold.php";
            sbText.Append(TranURL);
            sbText.AppendFormat("?user={0}&",user);
            sbText.AppendFormat("domain={0}&", domain);
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

        public static string sxdPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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

        public static string sxdQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "6":
                    sReturn = "0";
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GetNewCode(string sGameAbbre,string sUserID)
        {
            StringBuilder sbText = new StringBuilder(sUserID);
            string sDomain = GetDomain(sGameAbbre);
            sbText.AppendFormat("_{0}",sDomain);
            string sNewCode = ProvideCommon.MD5(sbText.ToString());
            return sNewCode;
        }

        public static string GameErr(string sErrID)
        {
            string sErrRes = string.Empty;
            switch (sErrID)
            {
                case "0":
                    sErrRes = "充值失败，订单处于待充状态";
                    break;
                case "1":
                    sErrRes = "充值成功";
                    break;
                case "2":
                    sErrRes = "充值的服务器不存在";
                    break;
                case "3":
                    sErrRes = "充值游戏币有误";
                    break;
                case "4":
                    sErrRes = "不允许的访问的IP";
                    break;
                case "5":
                    sErrRes = "md5错误";
                    break;
                case "6":
                    sErrRes = "订单已经成功充过值";
                    break;
                case "7":
                    sErrRes = "不存在此账号";
                    break;
                case "8":
                    sErrRes = "充值接口关闭";
                    break;
            }
            return sErrRes;
        }

        public static string GameisLogin(string sUserID,string sGameAbbre)
        {
            string user = sUserID;
            string sDomain = GetDomain(sGameAbbre);
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.AppendFormat("_{0}_", sDomain);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5(user_ domain _平台密钥)
            sbText.Remove(0, sbText.Length);
            string TranURL = "http://api.sxd.xd.com/api/check_user.php";
            sbText.Append(TranURL);
            sbText.AppendFormat("?user={0}&", user);
            sbText.AppendFormat("domain={0}&", sDomain);
            sbText.AppendFormat("sign={0}", sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "1":
                    sReturn = "0";
                    break;
                case "4":
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
            string sServerID = sGame.Replace("sxd", "");
            sDomain = string.Format("sxd{0}.dao50.com",sServerID);
            return sDomain;
            //switch (sGame)
            //{
            //    case "sxd1":
            //        sDomain = "sxd1.dao50.com";
            //        break;
            //    case "sxd10":
            //        sDomain = "sxd10.dao50.com";
            //        break;
            //    case "sxd11":
            //        sDomain = "sxd11.dao50.com";
            //        break;
            //    case "sxd21":
            //        sDomain = "sxd21.dao50.com";
            //        break;
            //    case "sxd32":
            //        sDomain = "sxd32.dao50.com";
            //        break;
            //    case "sxd41":
            //        sDomain = "sxd41.dao50.com";
            //        break;
            //    case "sxd50":
            //        sDomain = "sxd50.dao50.com";
            //        break;
            //}
        }
    }
}

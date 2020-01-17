using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
namespace Bussiness
{
    public class jjpGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string center_id = "1073";
            string serverid = GetServerID(sGame);
            string fcm = "1";
            string time = ProvideCommon.getTime().ToString();
            string key = "siawVVD8vDf834jFiek";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", center_id);
            sbText.AppendFormat("{0}", serverid);
            sbText.AppendFormat("{0}", fcm);
            sbText.AppendFormat("{0}", time);
            sbText.AppendFormat("{0}", key);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5 ( username . server_num . KEY . time )
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.jjp.dao50.com/inf_login.jsp?", serverid);
            sbText.AppendFormat("account={0}", sUserID);
            sbText.AppendFormat("&center_id={0}", center_id);
            sbText.AppendFormat("&server_id={0}", serverid);
            sbText.AppendFormat("&fcm={0}", fcm);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&sign={0}", sign);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string center_id = "1073";
            string response_format = "1";
            string pay_type = "1";
            string time = ProvideCommon.getTime().ToString();
            int rmb = Convert.ToInt32(dMoney);
            int gold = rmb * 100;
            string serverid = GetServerID(sGame);
            string key = "siawVVD8vDf834jFiek";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", center_id);
            sbText.AppendFormat("{0}", serverid);
            sbText.AppendFormat("{0}", time);
            sbText.AppendFormat("{0}", sOrderID);
            sbText.AppendFormat("{0}", gold);
            sbText.AppendFormat("{0}", response_format);
            sbText.AppendFormat("{0}", pay_type);
            sbText.AppendFormat("{0}", key);
            string sSign = ProvideCommon.MD5(sbText.ToString());//md5 ( $user.$gold.$server_id.$order_id.KEY )
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.jjp.dao50.com//pay/payment.py?", serverid);
            sbText.AppendFormat("account={0}", sUserID);
            sbText.AppendFormat("&center_id={0}", center_id);
            sbText.AppendFormat("&server_id={0}", serverid);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&bill_no={0}", sOrderID);
            sbText.AppendFormat("&amt={0}", gold);
            sbText.AppendFormat("&pay_type={0}", pay_type);
            sbText.AppendFormat("&response_format={0}", response_format);
            sbText.AppendFormat("&debug={0}", "0");
            sbText.AppendFormat("&sign={0}", sSign);

            string sRes = ProvideCommon.GetPageInfo(sbText.ToString(), "UTF-8");
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(sTranIP, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;

        }

        public static string jjpPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            string sress = ProvideCommon.getJsonValue("code", sRes).Trim();
            switch (sress)
            {
                case "1000":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string jjpQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string sress = ProvideCommon.getJsonValue("code", sRes).Trim();
            string sReturn = string.Empty;
            switch (sress)
            {
                case "1000":
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
                //case "4":
                //    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                //    sReturn = "0";
                //    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sID = sGame.Replace("jjp", "");
            return sID.ToString();
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string key = "siawVVD8vDf834jFiek";
            string server_num = GetServerID(sGameAbbre);
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", key);
            string sign = ProvideCommon.MD5(sbText.ToString());//($username.$server_num.KEY)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.jjp.dao50.com/pay/QueryInfo?", server_num);
            sbText.AppendFormat("account={0}", sUserID);
            sbText.AppendFormat("&sign={0}", sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sReturn = string.Empty;
            string ssres = ProvideCommon.getJsonValue("code", sRes);
            switch (ssres)
            {
                case "1001":
                    sReturn = "1";
                    break;
                default:
                    sReturn = "0";
                    break;
            }
            return sReturn;
        }
    }
}

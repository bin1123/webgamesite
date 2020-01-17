using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Web;

namespace Bussiness
{
   public class zwjGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string user_id = sUserID;
            string user_name = sUserID;
            string server_id = GetServerID(sGame);
            string time = ProvideCommon.getTime().ToString();
            string is_adult = "1";
            string client = "1";
          
            string key = "b906592f135016f44c3b194180268e21";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", user_id);
            sbText.AppendFormat("{0}", user_name);
            sbText.AppendFormat("{0}", server_id);
            sbText.AppendFormat("{0}", is_adult);
            sbText.AppendFormat("{0}", time);
            sbText.AppendFormat("{0}", key);

            string ticket = ProvideCommon.MD5(sbText.ToString());
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.zwj.dao50.com/index.php?", server_id);
            sbText.AppendFormat("user_id={0}", user_id);
            sbText.AppendFormat("&user_name={0}", user_name);
            sbText.AppendFormat("&server_id={0}",server_id);
            sbText.AppendFormat("&time={0}",time);
            sbText.AppendFormat("&sign={0}", ticket);
            sbText.AppendFormat("&is_adult={0}", is_adult);
            sbText.AppendFormat("&client={0}", client);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string sGamePayUrl="";
            string server_id = GetServerID(sGame);
            string order_id = sOrderID;
            string user_id = sUserID;
            string coin =Convert.ToString(dMoney*10);
            string money = dMoney.ToString(); ;
            string time = ProvideCommon.getTime().ToString();
            string role_id = GetRoleId(sUserID,sGame);
            string key="b906592f135016f44c3b194180268e21";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", order_id);
            sbText.AppendFormat("{0}", user_id);
            sbText.AppendFormat("{0}", server_id);
            sbText.AppendFormat("{0}", coin);
            sbText.AppendFormat("{0}", money);
            sbText.AppendFormat("{0}", time);
            sbText.AppendFormat("{0}",key );
            string sSign = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$paytime|$gold|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("{0}", "http://open.zuiwuji.com/api/dao50/order_submit.php?");
            sbText.AppendFormat("order_id={0}", order_id);
            sbText.AppendFormat("&user_id={0}", user_id);
            sbText.AppendFormat("&server_id={0}", server_id);
            sbText.AppendFormat("&coin={0}", coin);
            sbText.AppendFormat("&money={0}", money);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&role_id={0}", role_id);
            sbText.AppendFormat("&sign={0}", sSign);

            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string zwjPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            string ssRes = ProvideCommon.getJsonValue("code", sRes).Trim().ToString();
            switch (ssRes)
            {
                case "1":
                case "-5":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string zwjQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string ssRes = ProvideCommon.getJsonValue("code", sRes).Trim().ToString();
            string sReturn = string.Empty;
            switch (ssRes)
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
                case "-5":
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    sReturn = "0";
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sID = sGame.Replace("zwj", "");
            return sID.ToString();
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string user_id = sUserID;
            string server_id = GetServerID(sGameAbbre);
            string time = ProvideCommon.getTime().ToString();
            string key = "b906592f135016f44c3b194180268e21";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", server_id);
            sbText.AppendFormat("{0}", user_id);
            sbText.AppendFormat("{0}", time);
            sbText.AppendFormat("{0}", key);
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$ts|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("{0}", "http://open.zuiwuji.com/api/dao50/role_query.php?");
            sbText.AppendFormat("server_id={0}", server_id);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&user_id={0}", user_id);
            sbText.AppendFormat("&sign={0}", ticket);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string ssRes = ProvideCommon.getJsonValue("code", sRes).Trim();
            string sReturn = string.Empty;
            switch (ssRes)
            {
                case "-1":
                    sReturn = "1";
                    break;
                default:
                    //sReturn = "1";
                    sReturn = "0";
                    break;
            }
            return sReturn;
        }
        public static string GetRoleId(string sUserID, string sGameAbbre)
        {
            string user_id = sUserID;
            string server_id = GetServerID(sGameAbbre);
            string time = ProvideCommon.getTime().ToString();
            string key = "b906592f135016f44c3b194180268e21";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", server_id);
            sbText.AppendFormat("{0}", user_id);
            sbText.AppendFormat("{0}", time);
            sbText.AppendFormat("{0}", key);
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$ts|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("{0}", "http://open.zuiwuji.com/api/dao50/role_query.php?");
            sbText.AppendFormat("server_id={0}", server_id);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&user_id={0}", user_id);
            sbText.AppendFormat("&sign={0}", ticket);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string ssRes = ProvideCommon.getJsonValue("id", sRes).Trim();
            string sReturn = string.Empty;
            if (!string.IsNullOrEmpty(ssRes))
            {
                sReturn = ssRes;
            }
            return sReturn;
        }
    }
}

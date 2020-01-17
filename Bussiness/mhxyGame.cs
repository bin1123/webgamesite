using System;
using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
    public class mhxyGame
    {
        public static string Login(string sUserID,string sGame)
        {
            string serverid = GetServerID(sGame);
            string ts = ProvideCommon.getTime().ToString();
            string key = "0aec0cac12c6b47b5f899d5ff6ed9177";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|", sUserID);
            sbText.AppendFormat("{0}|", ts);
            sbText.AppendFormat("{0}|", serverid);
            sbText.Append(key);
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$ts|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.mhxy.dao50.com/flash/login.php?", serverid);
            sbText.AppendFormat("accname={0}", sUserID);
            sbText.AppendFormat("&ts={0}", ts);
            sbText.AppendFormat("&serverid={0}", serverid);
            sbText.Append("&fcm=1");
            sbText.AppendFormat("&sign={0}&via=", ticket);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string paytime = ProvideCommon.getTime().ToString();
            int iMoney = Convert.ToInt32(dMoney);
            int gold = iMoney*10;
            string serverid = GetServerID(sGame);           
            string key = "0aec0cac12c6b47b5f899d5ff6ed9177";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|", sUserID);
            sbText.AppendFormat("{0}|", paytime);
            sbText.AppendFormat("{0}|", gold.ToString());
            sbText.AppendFormat("{0}|", serverid);
            sbText.Append(key);

            string sSign = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$paytime|$gold|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            string sGamePayUrl = string.Format("http://s{0}.mhxy.dao50.com/intf/general/pay.php", serverid);
            sbText.AppendFormat("accname={0}&paytime={1}&gold={2}&billno={3}&serverid={4}&sign={5}",
                                 sUserID,paytime,gold.ToString(),sOrderID,serverid,sSign);
            string sRes = ProvideCommon.GetPageInfoByPost(sGamePayUrl,sbText.ToString(),"UTF-8");
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string mhxyPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            decimal dMoney = (Convert.ToDecimal(iPayPoints))/10;
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre, iPayPoints, sUserName, sPhone, iGUserID, sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if (sTGRes != "0")
            {
                return sTGRes;
            }
            string sRes = Pay(iGUserID.ToString(), dMoney, sTranID, sGameAbbre);
            string sJsonRes = ProvideCommon.getJsonValue("ret",sRes);
            string sReturn = string.Empty;
            switch (sJsonRes)
            {
                case "0":
                case "2":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string mhxyQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string sJsonRes = ProvideCommon.getJsonValue("ret", sRes);
            string sReturn = string.Empty;
            switch (sJsonRes)
            {
                case "0":
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
            string sID = sGame.Replace("mhxy", "");
            return sID.ToString();
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string serverid = GetServerID(sGameAbbre);
            string ts = ProvideCommon.getTime().ToString();
            string key = "0aec0cac12c6b47b5f899d5ff6ed9177";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|", sUserID);
            sbText.AppendFormat("{0}|", ts);
            sbText.AppendFormat("{0}|", serverid);
            sbText.Append(key);
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$ts|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.mhxy.dao50.com/intf/general/get_user_info.php?", serverid);
            sbText.AppendFormat("accname={0}", sUserID);
            sbText.AppendFormat("&ts={0}", ts);
            sbText.AppendFormat("&serverid={0}", serverid);
            sbText.AppendFormat("&sign={0}", ticket);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string sReturn = string.Empty;
            if (sRes.IndexOf("\"ret\":1") > -1)
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

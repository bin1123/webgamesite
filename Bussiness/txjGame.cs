using System;
using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
    public class txjGame
    {
        public static string Login(string sUserID,string sGame)
        {
            string server_id = GetServerID(sGame);
            string timestamp = ProvideCommon.getTime().ToString();
            string sLoginKey = "dao50_sdfwrsd2g_dfgd4sdf3dsf";
            string cm = "1";
            string site = "txj";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(timestamp);
            sbText.Append(sLoginKey);
            sbText.Append(cm);
            sbText.Append(site);
            sbText.Append(server_id);
            string flag = ProvideCommon.MD5(sbText.ToString());//md5($username . $time . $key . $cm . $site . $server_id)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.txj.dao50.com/game/login.php?", server_id);
            sbText.AppendFormat("username={0}", sUserID);
            sbText.AppendFormat("&time={0}", timestamp);
            sbText.AppendFormat("&flag={0}", flag);
            sbText.AppendFormat("&cm={0}&site={1}",cm,site);
            sbText.AppendFormat("&server_id={0}", server_id);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string sServerID = GetServerID(sGame);
            string money = dMoney.ToString();
            string gold = Convert.ToInt32(dMoney * 10).ToString();
            string key = "dao50_df545dfg43_3s435csdf34";
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sOrderID);
            sbText.Append(sUserID);
            sbText.Append(gold);
            sbText.Append(money);
            sbText.Append(time);
            sbText.Append(key);
            string flag = ProvideCommon.MD5(sbText.ToString());//md5(orderid + username + gold + money + time + 密钥)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.txj.dao50.com/game/api/pay.php", sServerID);
            sbText.AppendFormat("?orderid={0}", sOrderID);
            sbText.AppendFormat("&username={0}", sUserID);
            sbText.AppendFormat("&gold={0}", gold);
            sbText.AppendFormat("&money={0}", money);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&flag={0}", flag);
            sbText.AppendFormat("&channel={0}", "");
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string user_ip = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(),sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string txjPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "1":
                case "2":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string txjQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "2":
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
            string sid = sGame.Replace("txj", "");            
            return sid;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string sServerID = GetServerID(sGameAbbre);
            string sReturn = string.Empty;
            string key = "dao50_334dfg437_56dvfdgh4";
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(time);
            sbText.Append(key);
            string flag = ProvideCommon.MD5(sbText.ToString()); ;//md5(username + time + 密钥)
            string sUrl = string.Format("http://s{0}.txj.dao50.com/game/api/get_player_info.php?username={1}&time={2}&flag={3}", sServerID, sUserID, time, flag);
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            switch (sRes)
            {
                case "2":
                    sReturn = "1";
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }
    }
}

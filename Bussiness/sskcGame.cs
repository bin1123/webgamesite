using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
namespace Bussiness
{
   public class sskcGame
    {
        public static string Login(string sUserID, string sGame, string sClient)
        {
            string serverid = GetServerID(sGame);
            string agentid = "24";
            string is_adult = "1";
            string timestamp = ProvideCommon.getTime().ToString();
            string key = "Wfe^j*3Hi2";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("uid={0}", sUserID);
            sbText.AppendFormat("&time={0}", timestamp);
            sbText.AppendFormat("&server_id=s{0}", serverid);
            sbText.AppendFormat("{0}", key);
            string sign = ProvideCommon.MD5(sbText.ToString());//MD5(coopname=&serverid=&userid=&key=&timestamp=)
            sbText.Remove(0, sbText.Length);
           // sbText.AppendFormat("http://s{0}.sskc.dao50.com/api/login.php", serverid);
            sbText.Append("http://bleach.sina.gametrees.com/api/dao50/login.php?");
            sbText.AppendFormat("uid={0}", sUserID);
            sbText.AppendFormat("&time={0}", timestamp);
            sbText.AppendFormat("&server_id=s{0}", serverid);
            sbText.AppendFormat("&agentid={0}", agentid);
            sbText.AppendFormat("&sign={0}", sign);
            sbText.AppendFormat("&is_adult={0}", is_adult);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string serverid = GetServerID(sGame);
            string amount = dMoney.ToString(); ;
            string timestamp = ProvideCommon.getTime().ToString();
            string order_id = sOrderID.Substring(0, 32);
            string agentid="24";
            decimal order_amount = Convert.ToInt32(dMoney);
            string sRess = ValBind(sUserID, serverid, agentid);
            if (sRess == "0")
            {
                return "0";
            }
            string key = "Wfe^j*3Hi2";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", order_amount);
            sbText.AppendFormat("{0}", order_id);
            sbText.AppendFormat("{0}", "s"+serverid);
            sbText.AppendFormat("{0}", key);
            string sSign = ProvideCommon.MD5(sbText.ToString());//MD5签名sign = md5(chargeid + username +money +payway + needmoney +key);
            sbText.Remove(0, sbText.Length);
            //sbText.AppendFormat("http://s{0}.sskc.dao50.com/api/active.php?", serverid);
            sbText.Append("http://bleach.sina.gametrees.com/api/dao50/payment.php?");
            sbText.AppendFormat("uid={0}", sUserID);
            sbText.AppendFormat("&server_id=s{0}", serverid);
            sbText.AppendFormat("&order_amount={0}", order_amount);
            sbText.AppendFormat("&order_id={0}", order_id);
            sbText.AppendFormat("&agentid={0}",agentid );
            sbText.AppendFormat("&sign={0}", sSign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sGamePayUrl = string.Empty;
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string jyPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            string intRes = ProvideCommon.getJsonValue("status", sRes).Trim();
            if (sRes == "1")
            {
                sReturn = string.Format("0|{0}", sTranID);
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string jyQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string sReturn = string.Empty;

            if (sRes == "1")
            {
                TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                sReturn = "0";
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sID = sGame.Replace("sskc", "");
            return sID.ToString();
        }
        public static string ValBind(string sUserId, string sServerid,string agentid)
        {
            StringBuilder sbText = new StringBuilder();
            sbText.Append("http://bleach.sina.gametrees.com/api/dao50/active.php?");
            sbText.AppendFormat("uid={0}", sUserId);
            sbText.AppendFormat("&server_id={0}","s"+sServerid);
            sbText.AppendFormat("&agentid={0}", agentid);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString()).Trim();
            return sRes;
        
        }
        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string serverid = GetServerID(sGameAbbre);

            StringBuilder sbText = new StringBuilder();
            string timestamp = ProvideCommon.getTime().ToString();
            string agentid = "24";
            string key = "Wfe^j*3Hi2";

           string sign=ProvideCommon.MD5(key+timestamp+sUserID);
            sbText.Append("http://bleach.sina.gametrees.com/api/dao50/info.player.php?");
            sbText.AppendFormat("user_name={0}", sUserID);
            sbText.AppendFormat("&agentid={0}", agentid);
            sbText.AppendFormat("&serverid=s{0}", serverid);
            sbText.AppendFormat("&t={0}", timestamp);
            sbText.AppendFormat("&s={0}", sign);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl).Trim();
            string sReturn = string.Empty;
            string intRes = ProvideCommon.getJsonValue("error_code", sRes).Trim();
            if (intRes == "1001")
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

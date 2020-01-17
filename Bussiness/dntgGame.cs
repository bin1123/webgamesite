using System;
using System.Text;
using Common;

namespace Bussiness
{
   public class dntgGame
    {
        public static string Login(string sUserID, string sGame,string sClient)
        {
            string serverid = GetServerID(sGame);
            string timestamp = ProvideCommon.getTime().ToString();
            string coopname = "dao50";
            string cmflag = "0";

            string key = "c4be0bb733977b8c0602bb39f072";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("coopname={0}", coopname);
            sbText.AppendFormat("&serverid={0}", serverid);
            sbText.AppendFormat("&userid={0}", sUserID);
            sbText.AppendFormat("&key={0}", key);
            sbText.AppendFormat("&timestamp={0}", timestamp);

            string ticket = ProvideCommon.MD5(sbText.ToString());//MD5(coopname=&serverid=&userid=&key=&timestamp=)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.dntg.dao50.com/dntgg/dntgtylg?", serverid);
            sbText.AppendFormat("userid={0}", sUserID);
            sbText.AppendFormat("&coopname={0}", coopname);
            sbText.AppendFormat("&serverid={0}", serverid);
            sbText.AppendFormat("&cmflag={0}", cmflag);
            sbText.AppendFormat("&timestamp={0}", timestamp);
            sbText.AppendFormat("&sign={0}", ticket);
            sbText.AppendFormat("&client={0}", sClient);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string serverid = GetServerID(sGame);
            string amount = dMoney.ToString(); ;
            string timestamp = ProvideCommon.getTime().ToString();
            string coopname = "dao50";
            string key = "c4be0bb733977b8c0602bb39f072";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("coopname={0}", coopname);
            sbText.AppendFormat("&serverid={0}", serverid);
            sbText.AppendFormat("&userid={0}", sUserID);
            sbText.AppendFormat("&amount={0}", amount);
            sbText.AppendFormat("&orderid={0}", sOrderID);
            sbText.AppendFormat("&key={0}", key);
            sbText.AppendFormat("&timestamp={0}", timestamp);
            string sSign = ProvideCommon.MD5(sbText.ToString());//MD5(coopname=&serverid=&userid=&amount=&orderid=&key=&timestamp=)
            sbText.Remove(0, sbText.Length);
            sbText.Append("http://dntg.pay.lingyuwangluo.com/dntgc/dntgdcs?");
            sbText.AppendFormat("coopname={0}", coopname);
            sbText.AppendFormat("&userid={0}", sUserID);
            sbText.AppendFormat("&serverid={0}", serverid);
            sbText.AppendFormat("&orderid={0}", sOrderID);
            sbText.AppendFormat("&amount={0}", amount);
            sbText.Append("&currency=CNY");
            sbText.Append("&result=1");
            sbText.AppendFormat("&timestamp={0}", timestamp);
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

        public static string dntgPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            if (sRes.IndexOf("recv=ok") > -1)
            {
                sReturn = string.Format("0|{0}", sTranID);
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string dntgQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string sReturn = string.Empty;
            if (sRes.IndexOf("recv=ok") > -1)
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
            string sID = sGame.Replace("dntg", "");
            return sID.ToString();
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string serverid = GetServerID(sGameAbbre);
            string timestamp = ProvideCommon.getTime().ToString();
            string coopname = "dao50";
            string key = "c4be0bb733977b8c0602bb39f072";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("coopname={0}", coopname);
            sbText.AppendFormat("&serverid={0}", serverid);
            sbText.AppendFormat("&userid={0}", sUserID);
            sbText.AppendFormat("&key={0}", key);
            sbText.AppendFormat("&timestamp={0}", timestamp);
            string sSign = ProvideCommon.MD5(sbText.ToString());//MD5(coopname=&serverid=&userid=&key=&timestamp=)
            sbText.Remove(0, sbText.Length);
            sbText.Append("http://dntg.pay.lingyuwangluo.com/dntgc/dntgdrns?");
            sbText.AppendFormat("coopname={0}", coopname);
            sbText.AppendFormat("&serverid={0}", serverid);
            sbText.AppendFormat("&userid={0}", sUserID);
            sbText.AppendFormat("&timestamp={0}", timestamp);
            sbText.AppendFormat("&sign={0}", sSign);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl).Trim();
            string sReturn = string.Empty;
            if (sRes == "ERROR_-1406")
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

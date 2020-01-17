using System;
using System.Text;
using Common;

namespace Bussiness
{
    public class nzGame
    {
        public static string Login(string sUserID, string sGame,string sType)
        {
            string server_id = GetServerID(sGame);//游戏各个分区的编号，一区为1，二区为2
            string time = ProvideCommon.getTime().ToString();
            string sLoginKey = "1ovPBxOkGKX";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}{0}{1}1{2}",sUserID,time,sLoginKey);

            string sign = ProvideCommon.MD5(sbText.ToString());//md5(u. n. t. cm.KEY)

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.nz.dao50.com/api/login.php?u={1}&n={1}&t={2}&cm=1&p={3}&type={4}&s={0}", server_id, sUserID, time,sign,sType);       
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string server_id = GetServerID(sGame);
            string time = ProvideCommon.getTime().ToString();
            string order_amount = dMoney.ToString();
            string sPayKey = "BywqzsGK3FGA";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}{1}{2}{3}{4}", order_amount, sUserID,sOrderID,time, sPayKey);
            string sign = ProvideCommon.MD5(sbText.ToString()).ToLower() ;//md5(amount.u.order_no.time.key)

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.nz.dao50.com/api/exchange.php?amount={1}&u={2}&order_no={3}&time={4}&sign={5}",
                                 server_id, order_amount, sUserID, sOrderID, time,sign);      
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string nzPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                case "success":
                case "err_repeat":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string nzQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "success":
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
                case "err_repeat":
                    sReturn = "0";
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string server_id = GetServerID(sGameAbbre);//游戏各个分区的编号，一区为1，二区为2
            string sKey = "1ovPBxOkGKX";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}{1}{2}", sUserID, server_id, sKey);

            string sign = ProvideCommon.MD5(sbText.ToString());//md5($u.$s.$key)

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.nz.dao50.com/api/active.php?u={1}&s={0}&sign={2}", server_id, sUserID, sign);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string sReturn = string.Empty;
            if (sRes == "0")
            {
                sReturn = "1";
            }
            else
            {
                sReturn = "0";
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sID = sGame.Replace("nz", "");
            return sID.ToString();
        }
    }
}

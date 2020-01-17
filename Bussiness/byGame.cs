using System;
using System.Text;
using Common;

namespace Bussiness
{
    public class byGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string server_id = GetServerID(sGame);//游戏各个分区的编号，一区为1，二区为2
            string time = ProvideCommon.getTime().ToString();
            string sLoginKey = "daaa5376-b6bb-11e2-87ff-842b2b627011";
            string isAdult = "1";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|{1}|{2}|{3}|{4}",sUserID,isAdult,server_id,time,sLoginKey);

            string sign = ProvideCommon.MD5(sbText.ToString());//sign =md5($uid . '|' . $ isAdult . '|' . $server_id . '|' . $time . '|' . $key)

            sbText.Remove(0, sbText.Length);
            //sbText.AppendFormat("http://user.test.by.91wan.com/auth/dao50/login.php?uid={0}&isAdult={1}&server_id={2}&time={3}&sign={4}", sUserID, isAdult, server_id, time, sign);       
            sbText.AppendFormat("http://user.by.dao50.com/auth/dao50/login.php?uid={0}&isAdult={1}&server_id={2}&time={3}&sign={4}", sUserID, isAdult, server_id, time, sign);       
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string server_id = GetServerID(sGame);
            string time = ProvideCommon.getTime().ToString();
            int iMoney = Convert.ToInt32(dMoney);
            string order_amount = (iMoney*10).ToString();
            string sPayKey = "daaa5376-b6bb-11e2-87ff-842b2b627011";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|{1}|{2}|{3}|{4}|{5}", sUserID, server_id,sOrderID, order_amount,time, sPayKey);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5( $uid . '|' . $server_id . '|' . $order_id . '|' . $order_amount . '|' . $time . '|' . $key );

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://user.by.dao50.com/auth/dao50/pay.php?uid={0}&server_id={1}&order_id={2}&order_amount={3}&time={4}&sign={5}",
                                 sUserID, server_id, sOrderID, order_amount, time,sign);      
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string byPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                case "2":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string byQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
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
            string time = ProvideCommon.getTime().ToString();
            string sKey = "daaa5376-b6bb-11e2-87ff-842b2b627011";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|{1}|{2}|{3}", sUserID, server_id, time, sKey);

            string sign = ProvideCommon.MD5(sbText.ToString());//md5( $uid . '|' . $server_id . '|' . $time . '|' . $key )

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://user.by.dao50.com/auth/dao50/check.php?uid={0}&server_id={1}&time={2}&sign={3}", sUserID, server_id, time, sign);
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
            string sID = sGame.Replace("by", "");
            return sID.ToString();
        }
    }
}

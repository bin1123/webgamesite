using System;
using System.Text;

using Common;

namespace Bussiness
{
    public class zsgGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string from = "dao50";
            string game = "zsg";
            string server = GetServerID(sGame);
            string login_secret_signature = "58d9e356904f3f8267ee41072769a295";
            string user_id = sUserID;
            string fatigue = "1";
            string t = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("from={0}game={1}server={2}user_id={3}fatigue={4}t={5}login_secret_signature={6}", from, game, server,user_id,fatigue,t,login_secret_signature);
            string signature = ProvideCommon.MD5(sbText.ToString()).ToLower();
            string sLoginUrl = "http://interface.lianyun.173.com/login";
            string sUrl = string.Format("{0}?from={1}&game={2}&server={3}&user_id={4}&fatigue={5}&t={6}&signature={7}",sLoginUrl,from,game,server,user_id,fatigue,t,signature);
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string from = "dao50";
            string game = "zsg";
            string server = GetServerID(sGame);
            string transfer_secret_signature = "d060e2a1ac5f5b7ecfdcc4bfd75562fc";
            string user_id = sUserID;
            string order_number = sOrderID;
            string t = ProvideCommon.getTime().ToString();

            int iMoney = Convert.ToInt32(dMoney);
            string amount = (iMoney * 100).ToString();//充入的人民币数量（单位分，为10的整数倍）,大于10

            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("from={0}game={1}server={2}user_id={3}amount={4}order_number={5}t={6}transfer_secret_signature={7}", from, game, server, user_id, amount,order_number, t, transfer_secret_signature);
            string signature = ProvideCommon.MD5(sbText.ToString()).ToLower();

            string sGamePayUrl = "http://api.lianyun.173.com/api/pay"; 
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("from={0}&game={1}&server={2}&user_id={3}&amount={4}&order_number={5}&t={6}&signature={7}",
                                 from, game, server,user_id,amount,order_number,t,signature);
            string sRes = ProvideCommon.GetPageInfoByPost(sGamePayUrl, sbText.ToString(), "UTF-8");
            string status = string.Empty;
            if (sRes.IndexOf("status") > -1)
            {
                try
                {
                    JSONObject json = JSONConvert.DeserializeObject(sRes);
                    status = json["status"].ToString();
                }
                finally
                {
                    JSONConvert.clearJson();
                }
            }
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return status;
        }

        public static string zsgPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                case "-6":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string zsgQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "-6":
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

        public static string GetServerID(string sGameAbbre)
        {
            string serverid = sGameAbbre.Replace("zsg","");
            return serverid;
        }
    }
}

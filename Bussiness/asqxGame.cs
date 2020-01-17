using System;
using System.Text;

using Common;

namespace Bussiness
{
    public class asqxGame
    {
        public static string Login(string sUserID)
        {
            string sLoginTime = ProvideCommon.getTime().ToString();
            string sKey = "yVHLNG_n@RX*T3D1OhC0ZiPOZ9PD)k";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("qid={0}",sUserID);
            sbText.AppendFormat("&time={0}", sLoginTime);
            sbText.Append(sKey);
            string sSign = ProvideCommon.MD5(sbText.ToString()).ToLower();//md5($username.$time.$key) 这里传递的md5字串为小写字母
            sbText.Remove(0, sbText.Length);
            sbText.Append("http://asqx1.dao50.com/app/cklogin.php?");
            sbText.AppendFormat("qid={0}",sUserID);
            sbText.AppendFormat("&time={0}",sLoginTime);
            sbText.AppendFormat("&sign={0}",sSign);
            sbText.Append("&isAdult=1");
            return sbText.ToString();
        }

        public static string Pay(string sUserID, int iMoney, string sOrderID, string sGame)
        {
            string sUnion = "dao50";
            string sKey = "DBKJWY￥%@&*dkhwpd&###skSBNK";
            string sGamePayUrl = "http://pay.union.qq499.com:8029/api/sp/pay_sync_asqx.php";
            string server = ServerName(sGame);
            long lTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            string sGTranID = sOrderID.Substring(0, 30);
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(sUnion);
            sbText.Append(iMoney.ToString());
            sbText.Append(sGTranID);
            sbText.Append(server);
            sbText.Append(lTime);
            sbText.Append(sKey);

            string sSign = Common.ProvideCommon.MD5(sbText.ToString());//sign = md5($username . $union . $money . $order . $server . $time . $key)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("union={0}&username={1}&order={2}&money={3}&server={4}&time={5}&sign={6}", sUnion, sUserID, sGTranID, iMoney.ToString(), server, lTime.ToString(),sSign);
            string sRes = ProvideCommon.GetPageInfoByPost(sGamePayUrl,sbText.ToString(),"UTF-8");
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string asqxPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            int iMoney = iPayPoints/10;
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre, iPayPoints, sUserName, sPhone, iGUserID, sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if (sTGRes != "0")
            {
                return sTGRes;
            }
            string sRes = Pay(iGUserID.ToString(), iMoney, sTranID, sGameAbbre);
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

        public static string asqxQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iMoney = Convert.ToInt32(dPrice);
            int iUserID = UserBll.UserIDSel(sUserName);
            //int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            //int iGamePoints = iMoney * 10;
            //if (iUserPoints < iGamePoints)
            //{
            //    return "-2";
            //}
            string sRes = Pay(iUserID.ToString(), iMoney, sTranID, sGameAbbre);
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
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string ServerName(string sGame)
        {
            string sServer = string.Empty;
            switch (sGame)
            {
                case "asqx1":
                    sServer = "S1";
                    break;
            }
            return sServer;
        }
    }
}

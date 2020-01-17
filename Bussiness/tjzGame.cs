using System;
using System.Text;
using Common;

namespace Bussiness
{
    public class tjzGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string server_id = GetServerID(sGame);
            string time = ProvideCommon.getTime().ToString();
            string sLoginKey = "dao50jsfj390skd#D43$^&**(Gkfa1";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}{1}1{2}{3}",sUserID,server_id,time,sLoginKey);

            string sign = ProvideCommon.MD5(sbText.ToString());//md5("$user.$server.$fcm.$time.$key")

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.tjz.dao50.com/cgi-bin/login.cgi?time={2}&server={0}&sign={3}&user={1}&fcm=1", 
                                 server_id, sUserID, time, sign);       
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string server_id = GetServerID(sGame);
            string time = ProvideCommon.getTime().ToString();
            int iMoney = Convert.ToInt32(dMoney);
            string money = iMoney.ToString();
            string gold = (iMoney * 10).ToString();
            string sPayKey = "qPVrR2gxPtdCZ6zL4EzK5EPmYYopUKXG";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}{1}{2}{3}{4}{5}{6}", sOrderID, sUserID, server_id, money, gold, time, sPayKey);
            string sign = ProvideCommon.MD5(sbText.ToString()).ToLower();//md5(orderid + user + server + money + gold + time + 密钥) 
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://pay.zhuqueok.com:8088/api/daoWLCharge.do?orderid={0}&user={1}&gold={2}&money={3}&time={4}&sign={5}&server={6}",
                                 sOrderID,sUserID,gold,money,time,sign,server_id);      
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string tjzPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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

        public static string tjzQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
            string sKey = "dao50$%^&K:NVCx12#$~@#$d87d9d0";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}{1}{2}", sUserID, time, sKey);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5(user + time + 密钥) 

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.tjz.dao50.com/cgi-bin/queryrole.cgi?time={1}&server={0}&sign={3}&user={2}&fcm=1", server_id, time, sUserID, sign);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string sReturn = string.Empty;
            try
            {
                JSONObject json = JSONConvert.DeserializeObject(sRes);
                string sCode = json["status"].ToString();
                if (sCode == "-3")
                {
                    sReturn = "1";
                }
                else
                {
                    sReturn = "0";
                }
            }
            finally
            {
                JSONConvert.clearJson();
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sID = sGame.Replace("tjz", "");
            return sID.ToString();
        }
    }
}

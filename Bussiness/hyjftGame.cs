using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

using Common;
using DataEnity;

namespace Bussiness
{
    public class hyjftGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string key = "76ju^j*3Hi2";
            string server_id = "S"+GetServerID(sGame);
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("uid={0}",sUserID);
            sbText.AppendFormat("&time={0}",time);
            sbText.AppendFormat("&server_id={0}", server_id);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//Sign= md5(“uid=”.$uid.”&time=”.$time.”&server_id=”.$server_id.$key)
            string sGameUrl = string.Format("http://domestic.naruto.gametrees.com/api/dao50/login.php?uid={0}&server_id={1}&time={2}&sign={3}&is_adult=1&agentid=29", sUserID, server_id, time, sign);
            return sGameUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string key = "76ju^j*3Hi2";
            string agentid = "29";
            string serverid ="S"+GetServerID(sGame);
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(dMoney);
            sbText.Append(sOrderID);
            sbText.Append(serverid);
            sbText.Append(key);
           string iSActive= ActiveUser(sUserID, serverid, agentid);
           if (iSActive == "0")
           {
               return "3";
           }
            string sign = ProvideCommon.MD5(sbText.ToString());//sign=md5($uid.$order_amount.$order_id.$server_id.$key);
            string sTranUrl = string.Format("http://domestic.naruto.gametrees.com/api/dao50/payment.php?uid={0}&order_amount={1}&order_id={2}&server_id={3}&sign={4}&agentid=29",
                                             sUserID,dMoney,sOrderID,serverid,sign);
            string sRes = ProvideCommon.GetPageInfo(sTranUrl);
            string user_ip = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID,out iUserID);
            GamePayBLL.GamePayAdd(user_ip, sTranUrl, sOrderID, sRes, sGame,iUserID);
            
            return sRes;
        }

        public static string hyjftPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            int iMoney = iPayPoints / 10;
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
                case "2":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string hyjftQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "2":
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

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string key = "76ju^j*3Hi2";
            string sReturn = string.Empty;
            string server_id = GetServerID(sGameAbbre);
            string time = ProvideCommon.getTime().ToString();
            string agentid = "29";
            string sign = ProvideCommon.MD5(key+time+sUserID);
            string sQueryUrl = string.Format("http://domestic.naruto.gametrees.com/api/dao50/info.player.php?user_name={0}&agentid={1}&serverid={2}&t={3}&s={4} ", sUserID, agentid, server_id, time, sign);
            string sRes = ProvideCommon.GetPageInfo(sQueryUrl);
            try
            {
                JSONObject json = JSONConvert.DeserializeObject(sRes);
                string sCode = json["error_code"].ToString();
                if (sCode != "0")
                {
                    sReturn = "1";
                }
                else
                {
                    sReturn = sCode;
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
            string sServerID = sGame.Replace("hyjft", "");
            return sServerID.ToString();
        }

        public static string ActiveUser(string sUid, string sSId, string sAgentid)
        {
            string sUrl = string.Format("http://domestic.naruto.gametrees.com/api/dao50/active.php?uid={0}&server_id={1}&agentid={2}", sUid, sSId, sAgentid);
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            return sRes;
        }
    }
}

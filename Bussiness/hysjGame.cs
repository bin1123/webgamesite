using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
    public class hysjGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string key = "413fa83b08741ce828b00d55da4e";
            string userId = sUserID;
            string userName = sUserID;
            string serverId = GetSeverID(sGame);
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            StringBuilder sbText = new StringBuilder();
            string sign = string.Empty;
            sbText.AppendFormat("userName={0}",userName);
            sbText.AppendFormat("&serverId={0}",serverId);
            sbText.AppendFormat("&userId={0}", userId);
            sbText.AppendFormat("&key={0}", key);
            sbText.AppendFormat("&time={0}",time);
            sbText.Append("&userPass=");
            sign = ProvideCommon.MD5(sbText.ToString());//md5("userName=kkkkkk&serverId=1&userId=1002010&key=123456&time=1314005855&userPass=");
            sbText.Remove(0, sbText.Length);
            string serverdomain = GetDomain(sGame);
            sbText.AppendFormat("http://{0}/game.html?", serverdomain);
            sbText.AppendFormat("userName={0}", userName);
            sbText.AppendFormat("&serverId={0}", serverId);
            sbText.AppendFormat("&userId={0}", userId);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&sign={0}", sign);
            sbText.Append("&isAdult=1");
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, int iMoney, string sOrderID, string sGame)
        {
            string key = "f06b9a55978836e041d174a616ebba25";
            string unionCode = "unionhysjdaowulin";
            string sTranTime = TransGBLL.TransTimeSelByTID(sOrderID);
            string orderId = ProvideCommon.getTime(DateTime.Parse(sTranTime)).ToString();
            string userName = HttpUtility.UrlEncode(sUserID);
            string chargeTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string chargeMoney = iMoney.ToString("f2");
            int iGamePoints = iMoney * 10;
            string chargeAmount = iGamePoints.ToString();
            string Gatewayid = GetPaySeverID(sGame);
            string Gameid = "820";
            string clientIp = ProvideCommon.GetRealIP();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(unionCode);
            sbText.Append(orderId);
            sbText.Append(userName);
            sbText.Append(chargeTime);
            sbText.Append(chargeMoney);
            sbText.Append(chargeAmount);
            sbText.Append(Gatewayid);
            sbText.Append(Gameid);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//小写英文字母与数字组合,md5(unionCode+orderId+userName+chargeTime+chargeMoney+chargeAmount+gatewayId+gameId+key)注意：chargeMoney 参与md5 计算时应保留小数点两位，例如35 应该转换为35.00。key 由蓝港在线生成，告知联运方。
            sbText.Remove(0,sbText.Length);
            string serverdomain = GetDomain(sGame);
            string TranURL = "http://59.151.39.189:8080/union_mid/charging.do";
            sbText.Append(TranURL);
            sbText.AppendFormat("?unionCode={0}&", unionCode);
            sbText.AppendFormat("orderId={0}&", orderId);
            sbText.AppendFormat("userName={0}&", userName);
            sbText.AppendFormat("chargeTime={0}&", chargeTime);
            sbText.AppendFormat("chargeMoney={0}&", chargeMoney);
            sbText.AppendFormat("chargeAmount={0}&", chargeAmount);
            sbText.AppendFormat("gatewayId={0}&", Gatewayid);
            sbText.AppendFormat("gameId={0}&", Gameid);
            sbText.AppendFormat("clientIp={0}&", clientIp);
            sbText.AppendFormat("sign={0}", sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string hysjPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string hysjQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iMoney = Convert.ToInt32(dPrice);
            int iUserID = UserBll.UserIDSel(sUserName);
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = iMoney * 10;
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
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

        public static string GetNewCode(string sGameAbbre, string sUserID)
        {
            string key = "413fa83b08741ce828b00d55da4e";
            StringBuilder sbText = new StringBuilder();
            string sDomain = GetDomain(sGameAbbre);
            sbText.AppendFormat("{0}{1}{2}",sUserID,GetCodeSeverID(sGameAbbre),key);//$sign = md5($userId . $serverId . $key);
            string sNewCode = string.Format("XSK{0}",ProvideCommon.MD5(sbText.ToString()));
            return sNewCode;
        }

        public static bool signVal(string sDate,string sSign)
        {
            string sKey = "a09ea43603584c75";
            StringBuilder sbText = new StringBuilder(sDate);
            sbText.Append(sKey);
            string sNSign = ProvideCommon.MD5(sbText.ToString()).ToUpper();
            bool bRes = false;
            if(sNSign == sSign)
            {
                bRes = true;
            }
            return bRes;
        }
        
        public static string GetCodeSeverID(string sGame)
        {
            string sServerID = string.Empty;
            switch (sGame)
            {
                case "hysj1":
                    sServerID = "1";
                    break;
            }
            return sServerID;
        }

        public static string GetPaySeverID(string sGame)
        {
            string sServerID = string.Empty;
            switch (sGame)
            {
                case "hysj1":
                    sServerID = "820001";
                    break;
            }
            return sServerID;
        }

        public static string GetSeverID(string sGame)
        {
            string sServerID = string.Empty;
            switch (sGame)
            {
                case "hysj1":
                    sServerID = "860001";
                    break;
            }
            return sServerID;
        }

        public static string GetDomain(string sGame)
        {
            string sDomain = string.Empty;
            switch (sGame)
            {
                case "hysj1":
                    sDomain = "s1.hysj.dao50.com";
                    break;
            }
            return sDomain;
        }
    }
}

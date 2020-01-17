using System;
using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
    public class lcGame
    {
        public static string Login(string sUserID,string sGame)
        {
            string sLoginTime = ProvideCommon.getTime().ToString();
            string sKey = "sr7NMhk)rG9xLWj)l4JJGqCpA:4Y1s";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("uname={0}",sUserID);
            sbText.AppendFormat("&time={0}", sLoginTime);
            sbText.Append(sKey);
            string sSign = ProvideCommon.MD5(sbText.ToString());//md5($username.$time.$key) 这里传递的md5字串为小写字母
            sbText.Remove(0, sbText.Length);
            string sServerHost = ServerHost(sGame);
            sbText.AppendFormat("http://{0}/app/cklogin.php?", sServerHost);
            sbText.AppendFormat("uname={0}", sUserID);
            sbText.AppendFormat("&time={0}",sLoginTime);
            sbText.AppendFormat("&sign={0}",sSign);
            sbText.Append("&isAdult=1");
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string partnerid = "8";
            string gameid = "1";
            string sid = sGame.Replace("lc", ""); 
            string serverid = string.Empty;
            switch (sid)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                    serverid = "1";
                    break;
                default:
                    serverid = sid;
                    break;
            }
            string username = sUserID;
            string money = dMoney.ToString();
            string api_key = ")MSPu!ZLAMmG4)^p^M(af=CZVYJdGj";
            string sGamePayUrl = "http://api.lianyun.62you.com/pay_formal_togame.php";
            long lTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            string time = lTime.ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(gameid);
            sbText.Append(partnerid);
            sbText.Append(serverid);
            sbText.Append(sUserID);
            sbText.Append(username);
            sbText.Append(sOrderID);
            sbText.Append(money);
            sbText.Append(time);
            sbText.Append(api_key);

            string sSign = Common.ProvideCommon.MD5(sbText.ToString());//md5(gameid+ partnerid+sid+userid+urlencode(username)+orderid+money+time+api_key)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("orderid={0}&partnerid={1}&gameid={2}&sid={3}&userid={4}&username={5}&actorid=&actorname=&money={6}&time={7}&sign={8}",
                                 sOrderID,partnerid,gameid,serverid,sUserID,username,money,time,sSign);
            string sRes = ProvideCommon.GetPageInfoByPost(sGamePayUrl,sbText.ToString(),"UTF-8");
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string lcPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string lcQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string ServerHost(string sGame)
        {
            string sServer = string.Empty;
            string sid = sGame.Replace("lc", "");
            string serverid = string.Empty;
            switch (sid)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                    serverid = "1";
                    break;
                default:
                    serverid = sid;
                    break;
            }
            sServer = string.Format("s{0}.lc.dao50.com",serverid);
            return sServer;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string sReturn = string.Empty;
            string sGamePayUrl = ServerHost(sGameAbbre);
            string sUrl = string.Format("{0}/username?account={1}", sGamePayUrl, sUserID);
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            switch (sRes)
            {
                case "0":
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

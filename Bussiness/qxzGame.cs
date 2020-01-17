using System;
using System.Text;
using Common;

namespace Bussiness
{
    public class qxzGame
    {
        public static string Login(string sUserID, string sGameName)
        {
            string sTime = ProvideCommon.getTime().ToString();
            string sKey = "qxzd7#dc579da$%3ty8e@f09ef8%d7dc";
            string password = Guid.NewGuid().ToString("D").ToUpper();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(password);
            sbText.Append(sTime);
            sbText.Append(sKey);
            string sign = ProvideCommon.MD5(sbText.ToString()).ToLower();//md5(user+password+time + key)
            string Content = string.Format("{0}|{1}|{2}|{3}",sUserID,password,sTime,sign); //user|password|time|sign
            string site = "dao50";
            string sServer = ServerName(sGameName);
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("content={0}&site={1}&server={2}", Content, site,sServer);
            string LoginUrl = string.Format("http://s{0}.qxz.dao50.com/index.php?{1}", sServer,sbText.ToString());
            return LoginUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string channel = "19";
            string server = ServerName(sGame);
            string sKey = "0b4c853574e51142";
            int iGold = Convert.ToInt32(dMoney * 10);
            string count = iGold.ToString();
            Random rNum = new Random();
            int iCur = rNum.Next(0, sKey.Length-1);
            string cur = iCur.ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sKey.Substring(0,iCur));
            sbText.Append(channel);
            sbText.Append(sUserID);
            sbText.Append(sOrderID);
            sbText.Append(count);
            sbText.Append(server);
            sbText.Append(sKey.Substring(iCur));

            string sSign = Common.ProvideCommon.MD5(sbText.ToString());//md5(substr($keyM,0,$cur).$channel.$user.$order.$count.$server.substr($keyM,$cur));
            sbText.Remove(0, sbText.Length);
            string sGamePayUrl = "http://charge.qxz.youxi567.com/i_wl.php";
            sbText.AppendFormat("channel={0}&", channel);
            sbText.AppendFormat("server={0}&", server);
            sbText.AppendFormat("user={0}&", sUserID);
            sbText.AppendFormat("order={0}&", sOrderID);
            sbText.AppendFormat("count={0}&", count);
            sbText.AppendFormat("cur={0}&", cur);
            sbText.AppendFormat("key={0}", sSign);
            string sRes = ProvideCommon.GetPageInfoByPost(sGamePayUrl, sbText.ToString(), "UTF-8");
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string qxzPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            string ssRes = ProvideCommon.getJsonValue("success", sRes).Trim().ToString();
            switch (ssRes)
            {
                case "1":
                case "-1":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string qxzQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string ssRes = ProvideCommon.getJsonValue("success", sRes).Trim().ToString();
            string sReturn = string.Empty;
            switch (ssRes)
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
                case "-1":
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    sReturn = "0";
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string channel = "19";
            string server = ServerName(sGameAbbre);
            string sKey = "0b4c853574e51142";
            Random rNum = new Random();
            int iCur = rNum.Next(0, sKey.Length - 1);
            string cur = iCur.ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sKey.Substring(0, iCur));
            sbText.Append(channel);
            sbText.Append(server);
            sbText.Append(sUserID);
            sbText.Append(sKey.Substring(iCur));

            string sSign = Common.ProvideCommon.MD5(sbText.ToString());//md5(substr($channelKey,0,$cur).$channel.$server.$account.substr($channelKey,$cur))
            sbText.Remove(0, sbText.Length);
            string sQueryUrl = "http://gm.qxz.youxi567.com/rest/wl_query_user.php";
            sbText.Append(sQueryUrl);
            sbText.AppendFormat("?channel={0}&", channel);
            sbText.AppendFormat("server={0}&", server);
            sbText.AppendFormat("account={0}&", sUserID);
            sbText.AppendFormat("cur={0}&", cur);
            sbText.AppendFormat("key={0}", sSign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sJsonRes = ProvideCommon.getJsonValue("success", sRes);
            string sReturn = string.Empty;
            if (sJsonRes == "-1")
            {
                sReturn = "1";
            }
            else
            {
                sReturn = "0";
            }
            return sReturn;
        }

        public static string ServerName(string sGame)
        {
            string sServer = sGame.Replace("qxz", "");
            return sServer;
        }
    }
}

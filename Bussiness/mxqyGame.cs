using System;
using System.Text;

using Common;

namespace Bussiness
{
    public class mxqyGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string sLoginkey = "haoyuekey))*&&13411^16";
            string user = sUserID;
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}{1}{2}1", user,time,sLoginkey);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5(username + time + 密钥 + cm)
            sbText.Remove(0, sbText.Length);
            string serverdomain = GetDomain(sGame);
            sbText.AppendFormat("http://{0}/login.php?", serverdomain);
            sbText.AppendFormat("username={0}&", sUserID);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("flag={0}&", sign);
            sbText.Append("cm=1");
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string game = "mxqy";//游戏简称
            string agent = "dao50";//合作方简称，由双方协商确定
            string user = sUserID;
            string order = sOrderID.Substring(0, 30);//订单号，不允许超过30位

            int iMoney = Convert.ToInt32(dMoney);
            string money = iMoney.ToString();

            string server = sGame.Replace("mxqy", "S");//游戏服，为 Sn 的格式，n 为大于/等于 1 的整数，注意“S”为大写            
            string key = "shc83j0wdj2egoirwr301jkwe02ije9f804jfjedqsle";
            string sGamePayUrl = "http://pay.union.qq499.com:8029/pay_sync_togame.php";
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.Append(agent);
            sbText.Append(order);
            sbText.Append(money);
            sbText.Append(key);
            sbText.Append(time);
            sbText.Append(server);
            sbText.Append(game);

            string sSign = ProvideCommon.MD5(sbText.ToString());//md5(urlencode($user).$agent.$order.$money.$key.$time.$server.$game)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("game={0}&agent={1}&user={2}&order={3}&money={4}&server={5}&time={6}&sign={7}",
                                 game, agent, user, order, money, server, time, sSign);
            string sRes = ProvideCommon.GetPageInfoByPost(sGamePayUrl, sbText.ToString(), "UTF-8");
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string mxqyPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                case "-7":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string mxqyQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "-7":
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

        public static string GetDomain(string sGame)
        {
            string sDomain = string.Empty;
            string sServerID = sGame.Replace("mxqy", "");
            sDomain = string.Format("s{0}.mxqy.dao50.com", sServerID);
            return sDomain;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string sReturn = string.Empty;
            string key = "dao50_0ther_squSotStyw*tv(ZFwv(ZEwDZvqref";
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(time);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString()); ;//	md5($username.$time.OTHER_KEY)
            string serverdomain = GetDomain(sGameAbbre);
            string sUrl = string.Format("http://{0}/api/user_info.php?accountName={1}&time={2}&sign={3}", serverdomain, sUserID, time, sign);
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            if (sRes.IndexOf("\"data\":\"\"") > -1)
            {
                sReturn = "1";
            }
            return sReturn;
        }

        public static string GetNewCode(string sGameAbbre, string sUserID)
        {
            string sLoginkey = "haoyuekey))*&&13411^16";
            string sServerID = sGameAbbre.Replace("mxqy", "");
            StringBuilder sbText = new StringBuilder(sUserID);
            sbText.Append(sServerID);
            sbText.Append(sLoginkey);
            string sNewCode = ProvideCommon.MD5(sbText.ToString()).ToLower();//md5(accountName, serverId, 登陆KEY)
            return sNewCode;
        }
    }
}

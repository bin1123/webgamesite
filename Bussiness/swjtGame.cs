using System;
using System.Text;

using Common;

namespace Bussiness
{
    public class swjtGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string sLoginkey = "4399hy-1116jdu3-cm3d_dao50";
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            string site = sGame.Replace("swjt", "S");
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}{1}{2}1", sUserID,time,sLoginkey);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5(username + time + 密钥 + cm + site + server_id)
            sbText.Remove(0, sbText.Length);
            string serverdomain = GetDomain(sGame);
            sbText.AppendFormat("http://{0}/gamelogin.php?", serverdomain);
            sbText.AppendFormat("username={0}&", sUserID);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("flag={0}&", sign);
            sbText.AppendFormat("cm=1&site={0}",site);
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string game = "swjt";//游戏简称
            string agent = "dao50";//合作方简称，由双方协商确定
            string user = sUserID;
            string order = sOrderID.Substring(0, 30);//订单号，不允许超过30位

            int iMoney = Convert.ToInt32(dMoney);
            string money = iMoney.ToString();

            string server = sGame.Replace("swjt", "S");//游戏服，为 Sn 的格式，n 为大于/等于 1 的整数，注意“S”为大写            
            string key = "dj9djw9efj2fud0dfowefujdfirioef0jfndoffwewfwe";
            string sGamePayUrl = "http://pay.union.qq499.com:8029/pay_sync_togame.php";
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.Append(key);
            sbText.Append(time);
            sbText.Append(money);
            sbText.Append(agent);
            sbText.Append(server);
            sbText.Append(game);
            sbText.Append(order);

            string sSign = ProvideCommon.MD5(sbText.ToString());//md5(urlencode($user).$key.$time.$money.$agent.$server.$game.$order)
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

        public static string swjtPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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

        public static string swjtQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
            string sServerID = sGame.Replace("swjt", "");
            sDomain = string.Format("s{0}.swjt.dao50.com", sServerID);
            return sDomain;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string sReturn = string.Empty;
            string key = "4399hy-1116jdu3-cm3d_dao50";
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(time);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString()); ;//	md5(username + time + 密钥)
            string serverdomain = GetDomain(sGameAbbre);
            string sUrl = string.Format("http://{0}/api/get_player_info.php?username={1}&time={2}&flag={3}", serverdomain, sUserID, time, sign);
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            if (sRes.IndexOf("[]") > -1)
            {
                sReturn = "1";
            }
            return sReturn;
        }

        public static string GetNewCode(string sGameAbbre, string sUserID)
        {
            string sLoginkey = "4399hy-1116jdu3-cm3d_dao50";
            string sServerID = sGameAbbre.Replace("swjt", "S");
            string type = "1";
            StringBuilder sbText = new StringBuilder(sUserID);
            sbText.Append(sServerID);
            sbText.Append(type);
            sbText.Append(sLoginkey);
            string sign = ProvideCommon.MD5(sbText.ToString()).ToLower();//md5($username. $server. $type. $key)
            string serverdomain = GetDomain(sGameAbbre);
            string sUrl = string.Format("http://{0}/api/new_card.php?type={1}&username={2}&sign={3}&server={4}", 
                                        serverdomain, type, sUserID, sign,sServerID);
            string sNewCode = ProvideCommon.GetPageInfo(sUrl);
            return sNewCode;
        }
    }
}

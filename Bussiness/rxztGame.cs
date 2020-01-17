using System;
using System.Text;

using Common;

namespace Bussiness
{
    public class rxztGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string sLoginkey = "d63c6f221cd41117a39d21622684d0a6";
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            string aid = "5";
            string sid = GetServerID(sGame);
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("account={0}&", sUserID);
            sbText.AppendFormat("aid={0}&", aid);
            sbText.AppendFormat("fcm=1&");
            sbText.AppendFormat("sid={0}&", sid);
            sbText.AppendFormat("time={0}&", time);
            sbText.Append(sLoginkey);
            string sign = ProvideCommon.MD5(sbText.ToString()).ToLower();//md5(username + time + 密钥 + cm + site + server_id)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.rxzt.dao50.com/user/start.php?", sid);
            sbText.AppendFormat("account={0}&", sUserID);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("aid={0}&", aid);
            sbText.AppendFormat("sid={0}&", sid);
            sbText.Append("fcm=1&");
            sbText.AppendFormat("sign={0}", sign);
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string game = "rxzt";//游戏简称
            string agent = "dao50";//合作方简称，由双方协商确定
            string user = sUserID;
            string order = sOrderID.Substring(0, 30);//订单号，不允许超过30位

            int iMoney = Convert.ToInt32(dMoney);
            string money = iMoney.ToString();

            string server = sGame.Replace("rxzt", "S");//游戏服，为 Sn 的格式，n 为大于/等于 1 的整数，注意“S”为大写            
            string key = "sc38fu0wejsh9q82ejws02eiwekjeeh2ujeiek20eksdf";
            string sGamePayUrl = "http://pay.union.qq499.com:8029/pay_sync_togame.php";
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.Append(order);
            sbText.Append(server);
            sbText.Append(agent);
            sbText.Append(game);
            sbText.Append(key);
            sbText.Append(time);
            sbText.Append(money);

            string sSign = ProvideCommon.MD5(sbText.ToString());//md5(urlencode($user).$order.$server.$agent.$game.$key.$time.$money)

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

        public static string rxztPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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

        public static string rxztQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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

        public static string GetServerID(string sGameAbbre)
        {
            string serverid = sGameAbbre.Replace("rxzt", "");
            return serverid;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string skey = "dao50::RXZT::INFO::KEY::xodi3pv5iyx7srafw32l";
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            string aid = "5";
            string sid = GetServerID(sGameAbbre);
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("account={0}&", sUserID);
            sbText.AppendFormat("aid={0}&", aid);
            sbText.AppendFormat("sid={0}&", sid);
            sbText.AppendFormat("time={0}&", time);
            sbText.Append(skey);
            string sign = ProvideCommon.MD5(sbText.ToString()).ToLower();//md5(username + time + 密钥 + cm + site + server_id)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.rxzt.dao50.com/api/get_player_info.php?", sid);
            sbText.AppendFormat("account={0}&", sUserID);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("aid={0}&", aid);
            sbText.AppendFormat("sid={0}&", sid);
            sbText.AppendFormat("sign={0}", sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sReturn = string.Empty;
            if (sRes.IndexOf("\"msg\":\"Not found\"") > -1)
            {
                sReturn = "1";
            }
            return sReturn;
        }
    }
}

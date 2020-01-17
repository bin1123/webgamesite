using System;
using System.Text;

using Common;

namespace Bussiness
{
    public class wwsgGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string sLoginkey = "mF2XQ6KR7DehWxu9MBipEIbpIiqhoMUp";
            string user = sUserID;
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            string sServerID = GetServerID(sGame);
            string server = sGame.Replace("wwsg", "S");//游戏服，为 Sn 的格式，n 为大于/等于 1 的整数，注意“S”为大写   
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}{1}{2}1{3}", user,time,sLoginkey,server);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5(username + time + 密钥 + cm + server)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.wwsg.dao50.com/index.php?", sServerID);
            sbText.AppendFormat("username={0}&", sUserID);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("flag={0}&", sign);
            sbText.AppendFormat("server={0}&", server);
            sbText.Append("cm=1");
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string game = "wwsg";//游戏简称
            string agent = "dao50";//合作方简称，由双方协商确定
            string user = sUserID;
            string order = sOrderID.Substring(0, 30);//订单号，不允许超过30位

            int iMoney = Convert.ToInt32(dMoney);
            string money = iMoney.ToString();

            string server = sGame.Replace("wwsg", "S");//游戏服，为 Sn 的格式，n 为大于/等于 1 的整数，注意“S”为大写            
            string key = "cuensoudcbw9eu34h9asgrb394grr3";
            string sGamePayUrl = "http://pay.union.qq499.com:8029/pay_sync_togame.php";
            string time = ProvideCommon.getTime().ToString();//标准时间戳
            StringBuilder sbText = new StringBuilder();
            sbText.Append(order);
            sbText.Append(user);
            sbText.Append(server);
            sbText.Append(game);
            sbText.Append(key);
            sbText.Append(agent);
            sbText.Append(time);
            sbText.Append(money);

            string sSign = ProvideCommon.MD5(sbText.ToString());//$sign = md5($order.urlencode($user).$server.$game.$key.$agent.$time.$money)
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

        public static string wwsgPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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

        public static string wwsgQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string serverid = GetServerID(sGameAbbre);
            string ts = ProvideCommon.getTime().ToString();
            string key = "ySfWw4F0AVyK7TqyfxPnjOtuVCZLOvSu";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", ts);
            sbText.AppendFormat("{0}", key);
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$ts|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.wwsg.dao50.com/api/union/user_info.php?", serverid);
            sbText.AppendFormat("username={0}", sUserID);
            sbText.AppendFormat("&time={0}", ts);
            sbText.AppendFormat("&flag={0}", ticket);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string sJsonRes = ProvideCommon.getJsonValue("code",sRes);
            string sReturn = string.Empty;
            switch (sJsonRes)
            {
                case "-3":
                    sReturn = "1";
                    break;
                default:
                    sReturn = "0";
                    break;
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sID = sGame.Replace("wwsg", "");
            return sID.ToString();
        }

        public static string GetNewCode(string sUserID,string sGame)
        {
            string key = "mF2XQ6KR7DehWxu9MBipEIbpIiqhoMUp";
            string sServerID = GetServerID(sGame);
            StringBuilder sbText = new StringBuilder();
            sbText.Append(key);
            sbText.Append("wwsg");
            sbText.Append(sServerID);
            sbText.Append("1");
            sbText.Append(sUserID);
            string sNewCode = string.Format("1{0}",ProvideCommon.MD5(sbText.ToString()));//md5(Key + wwsg + 服务器ID + 激活码类型 + 平台账号)
            return sNewCode;
        }
    }
}

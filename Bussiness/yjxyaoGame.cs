using System;
using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
    public class yjxyGame
    {
        public static string Login(string sUserID,string sGame)
        {
            string account = sUserID;
            string tstamp = ProvideCommon.getTime().ToString();
            string fcm = "1";//0为未通过 1为通过 2未填写
            string server_id = sGame.Replace("yjxy", "");
            string GAME_TICKET_SUBFIX = "15bc795ee011b9b2f82b915a4c85ec73";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(account);
            sbText.Append(tstamp);
            sbText.Append(fcm);
            sbText.Append(server_id);
            sbText.Append(GAME_TICKET_SUBFIX);
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5(account+timestamp+fcm+server_id+GAME_TICKET_SUBFIX)
            sbText.Remove(0, sbText.Length);
            string sServerHost = ServerHost(sGame);
            sbText.AppendFormat("http://{0}/start.php?", sServerHost);
            sbText.AppendFormat("account={0}", account);
            sbText.AppendFormat("&tstamp={0}", tstamp);
            sbText.AppendFormat("&fcm={0}", fcm);
            sbText.AppendFormat("&server_id={0}",server_id);
            sbText.AppendFormat("&ticket={0}",ticket);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string game = "yjxy";//游戏简称
            string agent = "dao50";//合作方简称，由双方协商确定
            string user = sUserID;
            string order = sOrderID.Substring(0,30);//订单号，不允许超过30位
            
            int iMoney = Convert.ToInt32(dMoney);
            string money = iMoney.ToString();
            
            string server = sGame.Replace("yjxy", "S");//游戏服，为 Sn 的格式，n 为大于/等于 1 的整数，注意“S”为大写            
            string key = "fadfadf%dfd$DFADFASFDdfdfa$D";
            string sGamePayUrl = "http://pay.union.qq499.com:8029/pay_sync_togame.php";
            long lTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            string time = lTime.ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(key);
            sbText.Append(agent);
            sbText.Append(money);
            sbText.Append(time);
            sbText.Append(order);
            sbText.Append(user);
            sbText.Append(game);

            string sSign = ProvideCommon.MD5(sbText.ToString());//md5($key.$agent.$money.$time.$order.urlencode($user).$game)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("game={0}&agent={1}&user={2}&order={3}&money={4}&server={5}&time={6}&sign={7}",
                                 game,agent,user,order,money,server,time,sSign);
            string sRes = ProvideCommon.GetPageInfoByPost(sGamePayUrl,sbText.ToString(),"UTF-8");
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string yjxyPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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

        public static string yjxyQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
            string sid = sGame.Replace("yjxy", "");
            sServer = string.Format("s{0}.yjxy.dao50.com", sid);
            return sServer;
        }
    }
}

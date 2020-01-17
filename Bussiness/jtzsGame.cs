using System;
using System.Text;
using Common;

namespace Bussiness
{
    public class jtzsGame
    {
        private const string secretkey = "000d1197374b1b9541374f0ab2ac1p98";

        public static string Login(string sUserID, string sGame)
        {
            string accountid = sUserID;
            string accounts = sUserID;
            string ad = "";
            string cm_flag = "0";
            string timestamp = ProvideCommon.getTime().ToString();//标准时间戳
            string gameid = "51";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(accounts);
            sbText.Append(accountid);
            sbText.Append(gameid);
            sbText.Append(timestamp);
            sbText.Append(secretkey);            
            string sign = ProvideCommon.MD5(sbText.ToString());//MD5(accounts+accountid +gameid+timestamp +secretkey)

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.jtzs.dao50.com/index.php?",GetServerID(sGame));
            sbText.AppendFormat("accountid={0}&", accountid);
            sbText.AppendFormat("accounts={0}&", accounts);
            sbText.AppendFormat("ad={0}&", ad);
            sbText.AppendFormat("cm_flag={0}&", cm_flag);
            sbText.AppendFormat("timestamp={0}&", timestamp);
            sbText.AppendFormat("sign={0}", sign);           
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string accountid = sUserID;            
            string gameid = "51";
            string serverid = GetServerID(sGame);
            int iMoney = Convert.ToInt32(dMoney);
            int point = iMoney * 10;
            string giftcoin = "0";
            string timestamp = ProvideCommon.getTime().ToString();
            string remark = "0";
            string order = sOrderID.Substring(0, 30);//订单号，不允许超过30位
            StringBuilder sbText = new StringBuilder();            
            sbText.Append(accountid);
            sbText.Append(gameid);
            sbText.Append(serverid);
            sbText.Append(order);
            sbText.Append(point);
            sbText.Append(giftcoin); 
            sbText.Append(timestamp);
            sbText.Append(remark);
            sbText.Append(secretkey);
            string sign = ProvideCommon.MD5(sbText.ToString());//MD5(accountid +gameid+ serverid +orderid+ point + giftcoin +timestamp+remark+secretkey)

            sbText.Remove(0, sbText.Length);
            string TranURL = "http://login.dao50.z.ucjoy.com:3333/pay.php?";
            sbText.Append(TranURL);
            sbText.AppendFormat("accountid={0}&", accountid);
            sbText.AppendFormat("gameid={0}&", gameid);
            sbText.AppendFormat("serverid={0}&", serverid);
            sbText.AppendFormat("orderid={0}&", order);
            sbText.AppendFormat("point={0}&", point.ToString());
            sbText.AppendFormat("giftcoin={0}&", giftcoin);
            sbText.AppendFormat("timestamp={0}&", timestamp);
            sbText.AppendFormat("remark={0}&", remark);
            sbText.AppendFormat("sign={0}", sign);    
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), order, sRes, sGame, iUserID);
            return sRes;
        }

        public static string jtzsPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string jtzsQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "0":
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
            string account = sUserID;
            string serverid = GetServerID(sGameAbbre);
            string sURL = string.Format("http://login.dao50.z.ucjoy.com/accountquery.php?account={0}&serverid={1}",account,serverid);
            string sRes = ProvideCommon.GetPageInfo(sURL);
            string sReturn = string.Empty;
            if (sRes == "0")
            {
                sReturn = "1";
            }
            else
            {
                sReturn = "0";
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sID = sGame.Replace("jtzs", "");
            return sID.ToString();
        }
    }
}

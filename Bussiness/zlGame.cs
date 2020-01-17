using System;
using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
    public class zlGame
    {
        public static string Login(string sUserID,string sGame)
        {
            string sServerID = GetServerID(sGame);
            string identityCard = "1";
            string timestamp = ProvideCommon.getTime().ToString();
            string srcUrl = "dao50.com";
            string sLoginKey = string.Format("05718632f91b940awss{0}",sServerID);
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(identityCard);
            sbText.Append(srcUrl);
            sbText.Append(timestamp);
            sbText.Append(sLoginKey);
            string sSign = ProvideCommon.MD5(sbText.ToString());//md5(userName+identityCard+srcUrl+timestamp+密钥)
            string gameServer = string.Format("dao50s{0}", GetServerID(sGame));//目标服务器标识
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.zl.dao50.com/?",sServerID);
            sbText.AppendFormat("userName={0}", sUserID);
            sbText.AppendFormat("&identityCard={0}", identityCard);
            sbText.AppendFormat("&srcUrl={0}", srcUrl);
            sbText.AppendFormat("&sign={0}", sSign);
            sbText.AppendFormat("&timestamp={0}", timestamp);
            sbText.AppendFormat("&gameServer={0}", gameServer);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string sServerID = GetServerID(sGame);
            string rmb = dMoney.ToString();
            string fee = Convert.ToInt32(dMoney * 10).ToString();
            string code = string.Format("dao50s{0}",sServerID);//充值目标服务器标识
            string key = "caf578020f0799051436020954711c8e";
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sOrderID);
            sbText.Append(sUserID);
            sbText.Append(fee);
            sbText.Append(code);
            sbText.Append(time);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//MD5(ono+account+fee+code+time+密钥)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://183.61.84.210:8081/charge.do?ono={0}", sOrderID);
            sbText.AppendFormat("&account={0}", sUserID);
            sbText.AppendFormat("&rmb={0}", rmb);
            sbText.AppendFormat("&fee={0}", fee);
            sbText.AppendFormat("&code={0}", code);
            sbText.AppendFormat("&sign={0}", sign);
            sbText.AppendFormat("&time={0}", time);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string user_ip = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(),sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string zlPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                case "100":
                case "204":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string zlQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "100":
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
                case "204":
                    sReturn = "0";
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sid = sGame.Replace("zl", "");            
            return sid;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string sReturn = string.Empty;
            string sServerID = GetServerID(sGameAbbre);
            string sCode = string.Format("dao50s{0}",sServerID);
            string key = "caf578020f0799051436020954711c8e";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(sCode);
            sbText.Append(key);
            string sSign = ProvideCommon.MD5(sbText.ToString());;//MD5(account+code+密钥)
            string sUrl = string.Format("http://113.105.247.124:8081/charge!isUserExist.do?account={0}&code={1}&sign={2}",sUserID,sCode,sSign);
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            switch (sRes)
            {
                case "205":
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

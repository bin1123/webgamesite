using System;
using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
   public class ftzGame
    {

        public static string Login(string sUserID, string sGame)
        {
            string serverid = GetServerID(sGame);
            string ts = ProvideCommon.getTime().ToString();
            string key = "O^S%&RYL:#JBE%ASJ#jdl!@sm)^r12%jk";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}","{");
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", key);
            sbText.AppendFormat("{0}", ts);
            sbText.AppendFormat("{0}","}");
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$ts|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.ftz.dao50.com/index.php?", serverid);
            sbText.AppendFormat("userName={0}", sUserID);
            sbText.AppendFormat("&time={0}", ts);
            sbText.AppendFormat("&sign={0}&source=dao50", ticket);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            
                string paytime = ProvideCommon.getTime().ToString();
                int iMoney = Convert.ToInt32(dMoney);
                int gold = iMoney * 10;
                string serverid = GetServerID(sGame);
                string key = "gO^S%&RYL:#JBE%ASJ#jdl!@)^r12%jkmftz";
                StringBuilder sbText = new StringBuilder();
                sbText.AppendFormat("[{0}-", key);
                sbText.AppendFormat("{0}-", sUserID);
                sbText.AppendFormat("{0}-", "1");
                sbText.AppendFormat("{0}-", iMoney.ToString());
                sbText.AppendFormat("{0}]", paytime);
                string sSign = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$paytime|$gold|$serverid|密钥”)
                sbText.Remove(0, sbText.Length);
                string sGamePayUrl = string.Format("http://s{0}.ftz.dao50.com/sfPay.php", serverid);
                // sbText.AppendFormat("accname={0}&paytime={1}&gold={2}&billno={3}&serverid={4}&sign={5}", sUserID, paytime, gold.ToString(), sOrderID, serverid, sSign);
                sbText.AppendFormat("userName={0}", sUserID);
                sbText.AppendFormat("&goodsId={0}", 1);
                sbText.AppendFormat("&num={0}", iMoney);
                sbText.AppendFormat("&time={0}", paytime);
                sbText.AppendFormat("&sign={0}", sSign);
                sbText.AppendFormat("&orderId={0}", sOrderID);
                sbText.AppendFormat("&source={0}", "dao50");
                sbText.AppendFormat("&serverId={0}", "s" + serverid);

                string sRes = ProvideCommon.GetPageInfoByPost(sGamePayUrl, sbText.ToString(), "UTF-8");
                string sTranIP = ProvideCommon.GetRealIP();
                int iUserID = 0;
                int.TryParse(sUserID, out iUserID);
                string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
                GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
                return sRes;
          
        }

        public static string ftzPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            switch (sRes)
            {
                case "1":
                case "2":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string ftzQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "2":
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
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
            string sID = sGame.Replace("ftz", "");
            return sID.ToString();
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string serverid = GetServerID(sGameAbbre);
            string ts = ProvideCommon.getTime().ToString();
            string key = "gO^S%&RYL:#JBE%ASJ#jdl!@)^r12%jkmftz";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("[{0}", key);
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}]", ts);
            string ticket = ProvideCommon.MD5(sbText.ToString());//md5(“$accname|$ts|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.ftz.dao50.com/gm.php?", serverid);
            sbText.AppendFormat("userName={0}", sUserID);
            sbText.AppendFormat("&time={0}", ts);
            sbText.AppendFormat("&sign={0}", ticket);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string sReturn = string.Empty;
            if (sRes=="0")
            {
                sReturn = "1";
            }
            else
            {
                sReturn = "0";
            }
          
            return sReturn;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Bussiness
{
   public class ahxxGame
    {
        public static string Login(string sUserID, string sGame,string sClient)
        {
            string spid = "lin";
            string server_num = GetServerID(sGame);
            string fcm = "1";
            string time = ProvideCommon.getTime().ToString();
            string key = "5c22fb494ba87294287fe5e743a7fe07";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", server_num);
            sbText.AppendFormat("{0}", key);
            sbText.AppendFormat("{0}", time);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5 ( username . server_num . KEY . time )
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.ahxx.dao50.com/login.php?", server_num);
            sbText.AppendFormat("spid={0}", spid);
            sbText.AppendFormat("&username={0}", sUserID);
            sbText.AppendFormat("&server_num={0}", server_num);
            sbText.AppendFormat("&fcm={0}", fcm);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&sign={0}", sign);
            sbText.AppendFormat("&client={0}", sClient);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string spid = "lin";
            string time = ProvideCommon.getTime().ToString();
            int rmb = Convert.ToInt32(dMoney);
            int gold = rmb * 10;
            string server_num = GetServerID(sGame);
            string key = "5c22fb494ba87294287fe5e743a7fe07";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}",sUserID);
            sbText.AppendFormat("{0}",gold.ToString());
            sbText.AppendFormat("{0}", server_num);
            sbText.AppendFormat("{0}", sOrderID);
            sbText.AppendFormat("{0}", key);
            string sSign = ProvideCommon.MD5(sbText.ToString());//md5 ( $user.$gold.$server_id.$order_id.KEY )
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.ahxx.dao50.com/pay.php?", server_num);
            sbText.AppendFormat("spid={0}", spid);
            sbText.AppendFormat("&username={0}", sUserID);
            sbText.AppendFormat("&order={0}", sOrderID);
            sbText.AppendFormat("&server_num={0}", server_num);
            sbText.AppendFormat("&rmb={0}", rmb);
            sbText.AppendFormat("&gold={0}", gold);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&sign={0}", sSign);

            string sRes = ProvideCommon.GetPageInfo(sbText.ToString(), "UTF-8");
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(sTranIP, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;

        }

        public static string ahxxPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                case "4":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string ahxxQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "4":
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
            string sID = sGame.Replace("ahxx", "");
            return sID.ToString();
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string key = "5c22fb494ba87294287fe5e743a7fe07";
            string spid = "lin";
            string server_num = GetServerID(sGameAbbre);
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", server_num);
            sbText.AppendFormat("{0}", key);
            string sign = ProvideCommon.MD5(sbText.ToString());//($username.$server_num.KEY)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.ahxx.dao50.com/user.php?", server_num);
            sbText.AppendFormat("spid={0}", spid);
            sbText.AppendFormat("&username={0}", sUserID);
            sbText.AppendFormat("&server_num={0}", server_num);
            sbText.AppendFormat("&sign={0}", sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sReturn = string.Empty;
            switch (sRes)
            { 
                case "0":
                case "5":
                    sReturn = "1";
                    break;
                default:
                    sReturn = "0";
                    break;
            }
            return sReturn;
        }

        public static string GetNewCode(string sUserID,string sGameAbbre,string sCodeType)
        {
            string spid = "lin";
            string time = ProvideCommon.getTime().ToString();
            string server_num = GetServerID(sGameAbbre);
            string key = "5c22fb494ba87294287fe5e743a7fe07";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", spid);
            sbText.AppendFormat("{0}", server_num);
            sbText.AppendFormat("{0}", sCodeType);//0:新手卡 1:手机绑定卡
            sbText.AppendFormat("{0}", time);
            sbText.AppendFormat("{0}", key);
            string sSign = ProvideCommon.MD5(sbText.ToString());//($user.$spid.$server_num.$type.$time.KEY)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.ahxx.dao50.com/getcard.php?", server_num);
            sbText.AppendFormat("spid={0}", spid);
            sbText.AppendFormat("&user={0}", sUserID);
            sbText.AppendFormat("&server_num={0}", server_num);
            sbText.AppendFormat("&type={0}", sCodeType);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&sign={0}", sSign);

            string sRes = ProvideCommon.GetPageInfo(sbText.ToString(), "UTF-8");
            return sRes;
        }
    }
}

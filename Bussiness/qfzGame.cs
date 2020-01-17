using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;
using System.Text.RegularExpressions;

using Common;
using DataEnity;

namespace Bussiness
{
    public class qfzGame
    {
        public static string Login(string sUserID, string sGame, string sLoginType)
        {
            string platform = "dao50";
            string gkey = "qfz";
            string skey = GetServerID(sGame);
            string time = ProvideCommon.getTime().ToString();
            string is_adult = "1";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(platform);
            sbText.Append(gkey);
            sbText.Append(skey);
            sbText.Append(time);
            sbText.Append(is_adult);
            sbText.Append("#");
            string lkey = "XV1IHNkM2FzZGZrIG8g(*(*^986j2lu2lu8$aWdu1fhi";
            sbText.Append(lkey);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5($uid.$platform.$gkey.$skey.$time.$is_adult.'#'.$lkey)
            string back_url = "http://www.dao50.com/yxzq/qfz/";
            string sGameUrl = string.Format("http://{3}.qfz.dao50.com/login.html?uid={0}&platform={1}&gkey={2}&skey={3}&time={4}&is_adult={8}&back_url={5}&type={6}&sign={7}",
                                            sUserID, platform, gkey, skey, time, back_url, sLoginType, sign,is_adult);
            return sGameUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string platform = "dao50";
            string gkey = "qfz";
            string skey = GetServerID(sGame);
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(platform);
            sbText.Append(gkey);
            sbText.Append(skey);
            sbText.Append(time);
            sbText.Append(sOrderID);
            string coins = "";
            sbText.Append(coins);
            string money = "";
            sbText.Append(money);
            sbText.Append("#");
            string pkey = "eX@V1IHi2kuIGV4aGNhbmdlX24(^^ki*UL9LDOdl2kio";
            sbText.Append(pkey);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5($uid.$platform.$gkey.$skey.$time.$order_id.$coins.$money'#'.$pkey)
            string TranURL = string.Format("http://{0}.qfz.dao50.com/exchange.html", skey);
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("gkey={0}&skey={1}&platform={2}&order_id={3}&uid={4}&coins={5}&money={6}&time={7}&sign={8}",
                                gkey, skey, platform, sOrderID, sUserID, coins,money,time,sign);
            string sRes = ProvideCommon.GetPageInfoByPost(TranURL, sbText.ToString(), "UTF-8");
            string user_ip = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", TranURL, sbText.ToString());
            GamePayBLL.GamePayAdd(user_ip, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string khbdPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            int iMoney = iPayPoints / 10;
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre, iPayPoints, sUserName, sPhone, iGUserID, sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if (sTGRes != "0")
            {
                return sTGRes;
            }
            string sRes = Pay(iGUserID.ToString(), iMoney, sTranID, sGameAbbre);
            string sPayCode = ProvideCommon.getJsonValue("errno", sRes);
            string sReturn = string.Empty;
            if (sPayCode == "0" || sPayCode == "1")
            {
                sReturn = string.Format("0|{0}", sTranID);
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string khbdQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = Convert.ToInt32(dPrice * 10);
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string sPayCode = ProvideCommon.getJsonValue("errno", sRes);
            string sReturn = string.Empty;
            if (sPayCode == "0" || sPayCode == "1")
            {
                int iGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                if (iGRes == 0)
                {
                    sReturn = "0";
                }
                else
                {
                    sReturn = "-1";
                }
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string sReturn = string.Empty;
            string platform = "dao50";
            string gkey = "qfz";
            string skey = GetServerID(sGameAbbre);
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(platform);
            sbText.Append(gkey);
            sbText.Append(skey);
            sbText.Append(time);
            sbText.Append("#");
            string lkey = "XV1IHNkM2FzZGZrIG8g(*(*^986j2lu2lu8$aWdu1fhi";
            sbText.Append(lkey);
            string sign = ProvideCommon.MD5(sbText.ToString());//$uid.$platform.$gkey.$skey.$time.'#'.$lkey
            string sQueryUrl = string.Format("http://{0}.qfz.dao50.com/checkuser.html?uid={1}&platform={1}&gkey={2}&skey={3}&time={4}&sign={5}", 
                                             skey,sUserID,platform,gkey,skey,time,sign);
            string sRes = ProvideCommon.GetPageInfo(sQueryUrl);
            string sErrNo = ProvideCommon.getJsonValue("errno", sRes);
            if (sRes == "-1")
            {
                sReturn = "1";
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sServerID = sGame.Replace("qfz", "s");
            return sServerID;
        }
    }
}

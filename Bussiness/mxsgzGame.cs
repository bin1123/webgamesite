using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Bussiness
{
   public class mxsgzGame
    {
        public static string Login(string sUserID, string sGame,string fcm)
        {
            string serverid = GetServerID(sGame);
            string ts = ProvideCommon.getHMTime();
            string key = "58714e3110344ea5988039ab240e594d";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("accountId={0}", sUserID);
            sbText.AppendFormat("&accountName={0}", sUserID);
            sbText.AppendFormat("&ts={0}", ts);
            sbText.AppendFormat("{0}",key);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5("accountId=XXX&accountName=XXX&ts=XXX"+privateKey);
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.mxsgz.niuzei.com/index.php?", serverid);
            sbText.AppendFormat("accountId={0}", sUserID);
            sbText.AppendFormat("&accountName={0}", sUserID);
            sbText.AppendFormat("&fcm={0}", fcm);
            sbText.AppendFormat("&ts={0}", ts);
            sbText.AppendFormat("&sign={0}", sign);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            int iMoney = Convert.ToInt32(dMoney);//元
            int cash = iMoney * 10;//角
            int iGameMoney = cash;//金币
            string serverid = GetServerID(sGame);
            string ts = ProvideCommon.getHMTime();
            string key = "c0cc80a136af40c8b8a7a6b6cbc7ef39";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("account={0}", sUserID);
            sbText.AppendFormat("&cash={0}", cash.ToString());
            sbText.AppendFormat("&money={0}", iGameMoney.ToString());
            sbText.AppendFormat("&orderId={0}", sOrderID);
            sbText.AppendFormat("&sid=s{0}",serverid);
            sbText.AppendFormat("&ts={0}", ts);
            sbText.Append(key);
            string sSign = ProvideCommon.MD5(sbText.ToString());//md5(account=&cash=&money=&orderId=&profileId=&sid=&ts=)
            sbText.Remove(0, sbText.Length);
            string sGamePayUrl = string.Format("http://s{0}.mxsgz.niuzei.com:9001/recharge", serverid);
            sbText.AppendFormat("cash={0}", cash.ToString());
            sbText.AppendFormat("&money={0}", iGameMoney.ToString());
            sbText.AppendFormat("&orderId={0}", sOrderID);
            sbText.AppendFormat("&ts={0}", ts);
            sbText.AppendFormat("&sid=s{0}", serverid);
            sbText.AppendFormat("&sign={0}", sSign);
            sbText.AppendFormat("&account={0}", sUserID);

            string sRes = ProvideCommon.GetPageInfoByPost(sGamePayUrl, sbText.ToString(), "UTF-8");
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string mxsgzPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
                case "1200":
                case "1401":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string mxsgzQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);

            string sReturn = string.Empty;
            switch (sRes)
            {
                case "1200":
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
                case "1401":
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
            string sID = sGame.Replace("mxsgz", "");
            return sID.ToString();
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string serverid = GetServerID(sGameAbbre);
            string ts = ProvideCommon.getHMTime();
            string key = "596a47fa13e747239861edf3e8032d23";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("account={0}", sUserID);
            sbText.AppendFormat("&json=true&sid=s{0}", serverid);
            sbText.AppendFormat("&ts={0}",ts);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5(account = testAccount1&json=true&sid=s1&ts=1373250689790 + checkuserKey)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.mxsgz.niuzei.com:9002/checkuser?", serverid);
            sbText.AppendFormat("account={0}", sUserID);
            sbText.AppendFormat("&ts={0}&json=true", ts);
            sbText.AppendFormat("&sid=s{0}", serverid);
            sbText.AppendFormat("&sign={0}", sign);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string sReturn = string.Empty;
            if (sRes == "2401")
            {
                sReturn = "1";
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string GetNewCode(string sUserID,string sGameAbbre,string sCodeType)
        {
            string key = "1776f6b5b3f4aaed73539619ec7a4b7b";
            string ts = ProvideCommon.getHMTime();
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("account={0}", sUserID);
            string couponId = "10091";//兑换码的礼包ID
            sbText.AppendFormat("&couponId={0}",couponId);
            string operatorId = "10032";
            sbText.AppendFormat("&operatorId={0}", operatorId);
            string sServerID = GetServerID(sGameAbbre);
            sbText.AppendFormat("&sid=s{0}",sServerID);
            sbText.AppendFormat("&ts={0}",ts);
            sbText.Append(key);
            string sSign = ProvideCommon.MD5(sbText.ToString());
            sbText.Remove(0, sbText.Length);
            string sCodeUrl = "http://gm.sgyjz.dao50.com:9001/coupon?";
            sbText.Append(sCodeUrl);
            sbText.AppendFormat("account={0}",sUserID);
            sbText.AppendFormat("&operatorId={0}",operatorId);
            sbText.AppendFormat("&couponId={0}",couponId);
            sbText.AppendFormat("&ts={0}",ts);
            sbText.AppendFormat("&sid=s{0}",sServerID);
            sbText.AppendFormat("&sign={0}",sSign);
            string sNewCode = ProvideCommon.GetPageInfo(sbText.ToString());
            return sNewCode;
        }
    }
}

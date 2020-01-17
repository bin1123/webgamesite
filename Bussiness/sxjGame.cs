using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Web;
using System.Security.Cryptography;

namespace Bussiness
{
   public class sxjGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string HTTPType = "GET";
            string platform = "dao50";
            string sURI = string.Format("/{0}/login",platform);
            string serverid = GetServerID(sGame);
            string sHost = "http://203.195.183.207:9500";
            string ts = ProvideCommon.getTime().ToString();
            string appkey = "9c22bf69682149a4aeae39532b0ab4b3&";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("adult={0}", "1");
            sbText.AppendFormat("&time={0}", ts);
            sbText.AppendFormat("&userid={0}", sUserID);
            sbText.AppendFormat("&zoneid={0}", serverid);
            string sUrlEnQuery = UrlReplace(HttpUtility.UrlEncode(sbText.ToString()));
            string sURLEnURI = UrlReplace(HttpUtility.UrlEncode(sURI));
            Common.HmacSha1.Hasher hs = new HmacSha1.Hasher();
            string data=HTTPType + "&" + sURLEnURI + "&" + sUrlEnQuery;
            hs.HashKey = System.Text.Encoding.Default.GetBytes(appkey);
            hs.HashText = data;
            string sShaCode = hs.HMACSHA1Hasher();
            string ticket = UrlReplace(HttpUtility.UrlEncode(sShaCode));
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("{0}{1}?",sHost,sURI);
            sbText.AppendFormat("checksum={0}", ticket);
            sbText.AppendFormat("&adult={0}", "1");
            sbText.AppendFormat("&userid={0}", HttpUtility.UrlEncode(sUserID));
            sbText.AppendFormat("&zoneid={0}", HttpUtility.UrlEncode(serverid));
            sbText.AppendFormat("&time={0}", HttpUtility.UrlEncode(ts));
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string serverid = GetServerID(sGame);
            string HTTPType = "POST";
            string platform = "dao50";
            string sURI = string.Format("/{0}/pay", platform);
            string sHost = "http://203.195.183.207:9501";
            string paytime = ProvideCommon.getTime().ToString();
            int iMoney = Convert.ToInt32(dMoney);
            int gold = iMoney * 10;
            string appkey = "a21d0ab9a35f4cff87a1ebac72bd9a2a&";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("gold={0}", gold);
            sbText.AppendFormat("&order={0}", sOrderID);
            sbText.AppendFormat("&paytime={0}", paytime); 
            sbText.AppendFormat("&rmb={0}", iMoney);
            sbText.AppendFormat("&time={0}", paytime);
            sbText.AppendFormat("&userid={0}", sURI); 
            sbText.AppendFormat("&zoneid={0}", serverid);
            string sUrlEnQuery = UrlReplace(HttpUtility.UrlEncode(sbText.ToString()));
            string sURLEnURI = UrlReplace(HttpUtility.UrlEncode(sURI));
            Common.HmacSha1.Hasher hs = new HmacSha1.Hasher();
            string data = HTTPType + "&" + sURLEnURI + "&" + sUrlEnQuery;
            hs.HashKey = System.Text.Encoding.Default.GetBytes(appkey);
            hs.HashText = data;
            string sShaCode = hs.HMACSHA1Hasher();
            string sSign = UrlReplace(HttpUtility.UrlEncode(sShaCode));//md5(“$accname|$paytime|$gold|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            string sGamePayUrl = string.Format("{0}{1}",sHost,sURI);
            sbText.AppendFormat("userid={0}", sUserID);
            sbText.AppendFormat("&zoneid={0}", serverid);
            sbText.AppendFormat("&order={0}", sOrderID);
            sbText.AppendFormat("&rmb={0}", iMoney);
            sbText.AppendFormat("&gold={0}", gold);
            sbText.AppendFormat("&checksum={0}", sSign);
            sbText.AppendFormat("&time={0}", paytime);
            sbText.AppendFormat("&paytime={0}", paytime);

            string sRes = ProvideCommon.GetPageInfoByPost(sGamePayUrl, sbText.ToString(), "UTF-8");
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string sxjPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            string ssRes = ProvideCommon.getJsonValue("ret", sRes).Trim().ToString();
            switch (ssRes)
            {
                case "0":
                case "101":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string sxjQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string ssRes = ProvideCommon.getJsonValue("ret", sRes).Trim().ToString();
            string sReturn = string.Empty;
            switch (ssRes)
            {
                case "0":
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
                case "101":
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
            string sID = sGame.Replace("sxj", "");
            return sID.ToString();
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string serverid = GetServerID(sGameAbbre);
            string HTTPType = "GET";
            string platform = "dao50";
            string sURI = string.Format("/{0}/player_info", platform);
            //string sHost = string.Format("http://s{0}.sxj.dao50.com", serverid);
            string sHost = "http://203.195.183.207:9502";
            string sServer = "S" + serverid;
            string ts = ProvideCommon.getTime().ToString();
            string appkey = "0c61f6f0cc964deb925373260ef530fc&";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("time={0}",ts );
            sbText.AppendFormat("&userid={0}", sUserID);
            sbText.AppendFormat("&zoneid={0}", serverid);
            string sUrlEnQuery = UrlReplace(HttpUtility.UrlEncode(sbText.ToString()));
            string sURLEnURI = UrlReplace(HttpUtility.UrlEncode(sURI));
            Common.HmacSha1.Hasher hs = new HmacSha1.Hasher();
            string data = HTTPType + "&" + sURLEnURI + "&" + sUrlEnQuery;
            hs.HashKey = System.Text.Encoding.Default.GetBytes(appkey);
            hs.HashText = data;
            string sShaCode = hs.HMACSHA1Hasher();
            string ticket = UrlReplace(HttpUtility.UrlEncode(sShaCode));//md5(“$accname|$ts|$serverid|密钥”)
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("{0}{1}?",sHost,sURI);
            sbText.AppendFormat("time={0}", ts);
            sbText.AppendFormat("&userid={0}", sUserID);
            sbText.AppendFormat("&zoneid={0}", serverid);
            sbText.AppendFormat("&checksum={0}", ticket);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string ssRes = ProvideCommon.getJsonValue("ret", sRes).Trim();
            string sReturn = string.Empty;
            switch (ssRes)
            {
                case "-2":
                    sReturn = "1";
                    break;
                default:
                    //sReturn = "1";
                    sReturn = "0";
                    break;
            }
            return sReturn;
        }

        public static string GetNewCode(string sUserID,string sGameAbbre,string sCodeType)
        {
            string sNewCode = string.Empty;
            string platform = "dao50";
            string card_secret = "dffe2746ad0442858f97ec09d02b3281";
            string sServerID = GetServerID(sGameAbbre);
            string codename = string.Empty;
            switch(sCodeType)
            {
                case "sxjxsk":
                    codename = "XSK";
                   sNewCode= GetXsk(platform, sServerID, sUserID, codename, card_secret);
                    break;
                case "sxjghk":
                    codename = "GHK";
                    sNewCode = GetMTandGH(platform,codename, card_secret);
                    break;
                case "sxjmtk":
                    codename = "MTK";
                    sNewCode = GetMTandGH(platform, codename, card_secret);
                    break;
            }
            
            //平台号+服务器编号+用户ID+卡片标志码+card_secret（中间用:间隔）

            return sNewCode;
        }
        public static string GetMTandGH(string platform,string codename, string card_secret)
        {
            long sCardNum = ProvideCommon.GenerateIntID();
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}:", platform);
            sbText.AppendFormat("{0}:", card_secret);
            sbText.AppendFormat("{0}:", codename);
            sbText.AppendFormat("{0}", sCardNum);
            string sNewCode = ProvideCommon.MD5(sbText.ToString()) + codename+sCardNum;
            return sNewCode;
        }
        public static string GetXsk(string platform, string sServerID, string sUserID, string codename, string card_secret)
        {
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}:", platform);
            sbText.AppendFormat("{0}:", sServerID);
            sbText.AppendFormat("{0}:", sUserID);
            sbText.AppendFormat("{0}:", codename);
            sbText.AppendFormat("{0}", card_secret);
            string sNewCode = ProvideCommon.MD5(sbText.ToString())+codename;
            return sNewCode;
        }
        public static string UrlReplace(string urlEndcode)
        {
            string sH = HttpUtility.UrlEncode("&");
            string sD = HttpUtility.UrlEncode("=");
            urlEndcode = urlEndcode.Replace("%3d", "%3D").Replace("%2f", "%2F");
            return urlEndcode;

        }
    }
}

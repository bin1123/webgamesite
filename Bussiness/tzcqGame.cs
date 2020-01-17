using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

using Common;
using DataEnity;

namespace Bussiness
{
    public class tzcqGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string pfid = "12";//平台id
            string LOGIN_KEY = "=TZ=::dao50::Login::KEY::(!~*k0KkEkQ8~!)";
            string sSHALOGINKEY = ProvideCommon.SHA1(LOGIN_KEY).ToLower(); //"01ee72e03689fda8456f7e9486e937904364ebf1";
            string serverid = sGame.Replace("tzcq", "s");
            string tstamp = ProvideCommon.getTime().ToString();
            string fcm = "1";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(pfid);
            sbText.Append(sUserID);
            sbText.Append(sUserID);
            sbText.Append(serverid);
            sbText.Append(tstamp);
            sbText.Append(fcm);
            sbText.Append(sSHALOGINKEY);
            string sig = ProvideCommon.SHA1(sbText.ToString()).ToLower();//sha1(pfid + uid + name + serverid+ tstamp + fcm + sha1(LOGIN_KEY))
            string sHost = GetDomain(sGame);
            string preUrl = string.Format("http://{0}/prelogin.php?pfid={1}&uid={2}&name={2}&serverid={3}&tstamp={4}&fcm={5}&sig={6}", sHost,pfid,sUserID,serverid,tstamp,fcm,sig);
            string sRes = ProvideCommon.GetPageInfo(preUrl);
            string sUrl = string.Empty;
            if (sRes.IndexOf("\"ret\":0") > -1)
            {
                string ticket = ProvideCommon.getJsonValue("ticket", sRes);
                sUrl = string.Format("http://{0}/platformlogin.php?pfid={1}&uid={2}&name={2}&serverid={3}&tstamp={4}&ticket={5}", sHost, pfid, sUserID, serverid, tstamp, ticket);
                //try
                //{
                //    JSONObject json = JSONConvert.DeserializeObject(sRes);
                //    string ticket = json["ticket"].ToString();
                //    sUrl = string.Format("http://{0}/platformlogin.php?pfid={1}&uid={2}&name={2}&serverid={3}&tstamp={4}&ticket={5}", sHost, pfid, sUserID, serverid, tstamp, ticket);
                //}
                //catch (Exception ex)
                //{
                //    System.Text.StringBuilder sbErrText = new System.Text.StringBuilder();
                //    string sPath = @"D:\usercenter\Log\tzcq";
                //    sbErrText.AppendFormat("ErrMes:{0},", ex.Message);
                //    sbErrText.AppendFormat("Url:{0},", preUrl);
                //    sbErrText.AppendFormat("Res:{0}。", sRes);
                //    ProvideCommon pcObject = new ProvideCommon();
                //    pcObject.WriteLogFile(sPath, "loginerr", sbErrText.ToString());
                //}
                //finally
                //{
                //    JSONConvert.clearJson();
                //}
            }
            else
            {
                sUrl = string.Format("http://www.dao50.com/fwqwh/?{0}|{1}", preUrl, sbText.ToString());
            }
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string pfid = "12";//平台id
            int iMoney = Convert.ToInt32(dMoney);
            int iGameMoney = iMoney * 10;
            string money = iGameMoney.ToString();
            string PAY_KEY = "=TZ=::dao50::PayZL::KEY::*!!ieSi40OSigi2~!*";
            string sSHAPAYKEY = ProvideCommon.SHA1(PAY_KEY).ToLower();
            string serverid = sGame.Replace("tzcq", "s");
            string tstamp = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(pfid);
            sbText.Append(sOrderID);
            sbText.Append(sUserID);
            sbText.Append(sUserID);
            sbText.Append(money);
            sbText.Append(serverid);
            sbText.Append(tstamp);
            sbText.Append(sSHAPAYKEY);
            string sig = ProvideCommon.SHA1(sbText.ToString()).ToLower();//sha1(pfid + orderid + uid + name + money + serverid + tstamp +sha1(PAY_KEY))
            string sHost = GetDomain(sGame);
            string sUrl = string.Format("http://{0}/pay.php?pfid={1}&orderid={2}&uid={3}&name={3}&money={4}&serverid={5}&tstamp={6}&sig={7}",sHost,pfid,sOrderID,sUserID,money,serverid,tstamp,sig);
            string sRes = ProvideCommon.GetPageInfo(sUrl.ToString());
            string sRet = string.Empty;
            try
            {
                JSONObject json = JSONConvert.DeserializeObject(sRes);
                sRet = json["ret"].ToString();
            }
            finally
            {
                JSONConvert.clearJson();           
            }
            string user_ip = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(user_ip, sUrl, sOrderID, sRes, sGame,iUserID);
            return sRet;
        }

        public static string tzcqPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "0":
                case "4":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string tzcqQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                case "0":
                case "4":
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

        public static string GetDomain(string sGame)
        {
            string sServerID = sGame.Replace("tzcq", "");
            string sDomain = string.Format("s{0}.tzcq.dao50.com",sServerID);
            return sDomain;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string pfid = "12";//平台id
            string PAY_KEY = "=TZ=::dao50::PayZL::KEY::*!!ieSi40OSigi2~!*";
            string sSHAPAYKEY = ProvideCommon.SHA1(PAY_KEY).ToLower();
            string serverid = sGameAbbre.Replace("tzcq", "s");
            string tstamp = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(pfid);
            sbText.Append(sUserID);
            sbText.Append(sUserID);
            sbText.Append(serverid);
            sbText.Append(tstamp);
            sbText.Append(sSHAPAYKEY);
            string sig = ProvideCommon.SHA1(sbText.ToString()).ToLower();//sha1(pfid + uid + name + serverid + tstamp + sha1(PAY_KEY))
            string sHost = GetDomain(sGameAbbre);
            string preUrl = string.Format("http://{0}/queryuser.php?pfid={1}&uid={2}&name={2}&serverid={3}&tstamp={4}&sig={5}", sHost, pfid, sUserID, serverid, tstamp, sig);
            string sRes = ProvideCommon.GetPageInfo(preUrl);
            string sReturn = string.Empty;
            string sRet = ProvideCommon.getJsonValue("ret", sRes); 
            if (sRet == "0")
            {
                string is_exist = ProvideCommon.getJsonValue("is_exist", sRes);
                if (is_exist == "0")
                {
                    sReturn = "1";
                }
                else
                {
                    sReturn = sRes;
                }
            }
            else
            {
                sReturn = sRet;
            }
            //try
            //{
            //    JSONObject json = JSONConvert.DeserializeObject(sRes);
            //    string sRet = json["ret"].ToString();
            //    if (sRet == "0")
            //    {
            //        string is_exist = json["is_exist"].ToString();
            //        if (is_exist == "0")
            //        {
            //            sReturn = "1";
            //        }
            //        else
            //        {
            //            sReturn = sRes;
            //        }
            //    }
            //    else
            //    {
            //        sReturn = sRet;
            //    }
            //}
            //finally
            //{
            //    JSONConvert.clearJson();
            //}
            return sReturn;
        }
    }
}

using System;
using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
    public class jdsjGame
    {
        public static string Login(string sUserID,string sGame)
        {
            string type = "dao50";
            string key = "VbCDiBFqysSzToDfJXLbQUtgeHMrnuXb";
            string sid = GetServerID(sGame);
            string lgtime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string uip = ProvideCommon.GetRealIP().Replace('.','_');
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("uid={0}&uname={0}&lgtime={1}&uip={2}&type={3}&sid={4}&key={5}", sUserID, lgtime, uip, type,sid,key);
            string sign = ProvideCommon.MD5(sbText.ToString()).ToLower();//("uid=XXX&uname=XXX&lgtime=XXXX&uip=XXX&type=XXXX&sid=&key=")            
            string sGameUrl = "http://user.jdsj.dao50.com/User/unitelogin";
            string sLoginUrl = string.Format("{0}?uid={1}&uname={1}&lgtime={2}&uip={3}&type={4}&sid={5}&sign={6}",
                                             sGameUrl,sUserID,lgtime,uip,type,sid,sign);           
            return sLoginUrl;
        }

        public static string Pay(string sUserID, int iMoney, string sOrderID, string sGame)
        {
            string type = "dao50";
            string serverid = GetServerID(sGame);
            string key = "VViVnYZgpwrXMMQtqYUCsLqVGSuuQsUy";
            string time = DateTime.Now.ToString("yyyyMMddHHmmss");
            int point = iMoney*10;//游戏虚拟货币数量 
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("uid={0}&uname={0}&serverid={1}&point={2}&amount={3}&oid={4}&time={5}&type={6}&key={7}", 
                                sUserID, serverid, point, iMoney, sOrderID, time, type,key);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5("uid=&uname=&serverid=&point=&amount=&oid=&time=&type=&key=")；
            sbText.Remove(0, sbText.Length);
            //string format = "plain";//默认json可选值plain、xml
            string TranURL = "http://user.jdsj.dao50.com/VouchV2/AddGameCoin";
            sbText.AppendFormat("uid={0}&uname={0}&serverid={1}&point={2}&amount={3}", sUserID,serverid,point,iMoney);
            sbText.AppendFormat("&oid={0}&time={1}&type={2}&sign={3}", sOrderID,time,type,sign);
            string sRes = ProvideCommon.GetPageInfoByPost(TranURL, sbText.ToString(), "UTF-8");
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sUrl = string.Format("{0}?{1}", TranURL, sbText.ToString());
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string jdsjPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            try
            {
                JSONObject json = JSONConvert.DeserializeObject(sRes);
                string status = json["status"].ToString();
                if (status == "400")
                {
                    sReturn = string.Format("0|{0}", sTranID);
                }
                else
                {
                    sReturn = sRes;
                }
            }
            finally
            {
                JSONConvert.clearJson();
            }
            return sReturn;
        }

        public static string jdsjQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iMoney = Convert.ToInt32(dPrice);
            int iUserID = UserBll.UserIDSel(sUserName);
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = iMoney * 10;
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sRes = Pay(iUserID.ToString(), iMoney, sTranID, sGameAbbre);
            string sReturn = string.Empty;
            try
            {
                JSONObject json = JSONConvert.DeserializeObject(sRes);
                string status = json["status"].ToString();
                if (status == "400")
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
            }
            finally
            {
                JSONConvert.clearJson();
            }
            return sReturn;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string sType = "dao50";
            string sServerID = GetServerID(sGameAbbre);
            string sReturn = string.Empty;
            string key = "VViVnYZgpwrXMMQtqYUCsLqVGSuuQsUy";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("uid={0}&uname={0}&serverid={1}&type={2}&key={3}", sUserID, sServerID, sType,key);
            string sSign = ProvideCommon.MD5(sbText.ToString()); ;//md5("uid=123456&uname=xunlei001&serverid=10180&type=xunlei&key=XXXXX");
            string valUrl = "http://user.jdsj.dao50.com/Char/getCharInfo";
            string sUrl = string.Format("{0}?uid={1}&uname={1}&serverid={2}&type={3}&sign={4}",valUrl,sUserID,sServerID,sType,sSign);
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            if (sRes.IndexOf("\"status\":609") > 0 || sRes.IndexOf("\"status\":610") > 0)
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
            //string sServerID = sGame.Replace("jdsj", "");            
            //int sBeginServerID = 113050;
            //int iSID = 1;
            //int.TryParse(sServerID, out iSID);
            //int iServerID = 113096;            
            //switch(iSID)
            //{
            //    case 1:
            //    case 2:
            //    case 3:
            //    case 4:
            //    case 5:
            //    case 6:
            //        break;
            //    case 7:
            //    case 8:
            //    case 9:
            //    case 10:
            //    case 11:
            //    case 12:
            //    case 13:
            //    case 14:
            //        iServerID = 113097;
            //        break;
            //    default:
            //        iServerID = sBeginServerID + iSID;
            //        break;
            //}
            //return iServerID.ToString();
            return "113096";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

using Common;
using DataEnity;

namespace Bussiness
{
    public class sqGame
    {
        public static string Login(string sUserID, string sGame, bool bRes)
        {
            string site = GetSite(sGame);
            string key = "DNTQ-16DD11-WAN-0668-daoDS50N-7ROAD-shenLQg-111SHEN";
            string user = sUserID;
            string password = Guid.NewGuid().ToString("D");
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(user);
            sbText.Append(password);
            sbText.Append(time);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//user+password+time+key
            string context = string.Format("{0}|{1}|{2}|{3}",user,password,time,sign);
            string sHost = GetDomain(sGame);
            string preUrl = string.Format("http://{0}/createlogin",sHost);
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("content={0}&site={1}", context, site);
            int iUserID = 0;
            int iIsPay = 0;
            if (int.TryParse(sUserID, out iUserID))
            {
                string sTranID = TransPBLL.SelIsTranByUID(iUserID);
                if (sTranID.Length > 32)
                {
                    iIsPay = 1;
                }
            }
            sbText.AppendFormat("&isCharge={0}", iIsPay.ToString());
            string sRes = ProvideCommon.GetPageInfoByPost(preUrl, sbText.ToString(), "UTF-8");
            if (sRes == "0")
            {
                string Url = string.Format("http://{0}/client/game.jsp?user={1}&key={2}&site={3}", sHost, user, password, site);
                if (bRes)
                {
                    GameLoginBLL.GameLoginAdd(iUserID, sGame, ProvideCommon.GetRealIP(), Url);
                }
                return Url;
            }
            else
            {
                return string.Format("http://www.dao50.com/fwqwh/?{0}|{1}",preUrl,sbText.ToString());
            }
        }

        public static string cLogin(string sUserID, string sGame,bool bRes)
        {
            string sUrl = Login(sUserID, sGame,bRes);
            if(sUrl.IndexOf("game.jsp") > 0)
            {
                sUrl = sUrl.Replace("game.jsp","Loading.swf");
            }
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame,string sRoleID)
        {
            string site = GetSite(sGame);
            string chargeID = sOrderID;
            string username = sUserID;
            int iMoney = Convert.ToInt32(dMoney);
            int point = iMoney * 10;
            string payway = "1";
            string money = iMoney.ToString();
            string moneytype = "rmb";
            string key = string.Format("CSIT-16dd22-WAN-0668-DAO50-56744-7ROAD-SHKKTANG-232F3SEN");            
            StringBuilder sbText = new StringBuilder();
            sbText.Append(chargeID);
            sbText.Append(username);
            sbText.Append(point);
            sbText.Append(payway);
            sbText.Append(money);
            sbText.Append(moneytype);
            sbText.Append(key);
            string sign = ProvideCommon.MD5(sbText.ToString());//sign = md5(chargeID + username + point + payway + money + moneytype+ key)      
            string context = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}",
                                           chargeID,username,point,payway,money,moneytype,sign);            
            if(sRoleID == "")
            {
                sRoleID = GetRoleID(sUserID, sGame);
            }
            string sUrl = string.Format("http://{0}/chargemoney?content={1}&site={2}&userid={3}", GetDomain(sGame), context, site, sRoleID);
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string user_ip = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID,out iUserID);
            GamePayBLL.GamePayAdd(user_ip, sUrl, sOrderID, sRes, sGame,iUserID);
            return sRes;
        }

        public static string sqPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID, string sRoleID)
        {
            int iMoney = iPayPoints / 10;
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre, iPayPoints, sUserName, sPhone, iGUserID, sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if (sTGRes != "0")
            {
                return sTGRes;
            }
            string sRes = Pay(iGUserID.ToString(), iMoney, sTranID, sGameAbbre,sRoleID);
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

        public static string sqQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID,string sRoleID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = Convert.ToInt32(dPrice * 10);
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre,sRoleID);
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

        public static string GetRoleID(string sUserID, string sGameAbbre)
        {
            string sUrl = string.Format("http://{0}/loginselectlist?username={1}&site={2}", GetDomain(sGameAbbre),sUserID,GetSite(sGameAbbre));
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.Load(sUrl);
            XmlNode nodeObject = objXmlDoc.SelectSingleNode("/Result");
            string sGameSite = GetSite(sGameAbbre);
            string sRoleID = string.Empty;
            foreach (XmlNode xn in nodeObject)
            {
                XmlElement xe = (XmlElement)xn;
                string sSiteValue = xe.GetAttribute("Site");
                if(sSiteValue == sGameSite)
                {
                    sRoleID = xe.GetAttribute("ID");
                    break;
                }
            }
            return sRoleID;
        }

        public static List<TextTwo> GetUserInfo(string sUserID, string sGameAbbre)
        {
            string sUrl = string.Format("http://{0}/loginselectlist?username={1}&site={2}", GetDomain(sGameAbbre), sUserID, GetSite(sGameAbbre));
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.Load(sUrl);
            XmlNode nodeObject = objXmlDoc.SelectSingleNode("/Result");
            string sGameSite = GetSite(sGameAbbre);
            List<TextTwo> ttLObject = new List<TextTwo>();
            foreach (XmlNode xn in nodeObject)
            {
                XmlElement xe = (XmlElement)xn;
                TextTwo ttObject = new TextTwo();                
                ttObject.first = xe.GetAttribute("ID");
                ttObject.second = xe.GetAttribute("NickName");
                ttLObject.Add(ttObject); 
            }
            return ttLObject;
        }

        public static string GetUserInfoJson(string sUserID, string sGameAbbre)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            List<TextTwo> dgObject = GetUserInfo(sUserID,sGameAbbre);
            foreach (TextTwo ttObject in dgObject)
            {
                string nickname = HttpUtility.UrlDecode(ttObject.second, Encoding.UTF8);
                sbText.Append("{");
                sbText.AppendFormat("userid:'{0}',nickname:'{1}'", ttObject.first,nickname);
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }
       
        public static string GetDomain(string sGame)
        {
            string sDomain = string.Empty;
            string sServerID = sGame.Replace("sq", "");
            int newserver = 0;
            int ser = Convert.ToInt32(sServerID);
            string xmlUrl = @"/Inc/sqGame.xml";
            string xmlPath = AppDomain.CurrentDomain.BaseDirectory + xmlUrl;
            XmlDocument xml = new XmlDocument();
            xml.Load(xmlPath);
            XmlNodeList xnl = xml.SelectSingleNode("config").ChildNodes;
            foreach (XmlNode node in xnl)
            {
                XmlElement xe = (XmlElement)node;
                int min = Convert.ToInt32(xe.GetAttribute("min"));
                int max = Convert.ToInt32(xe.GetAttribute("max"));
                int zhu = Convert.ToInt32(xe.GetAttribute("zhu"));
                if (ser >= min && ser <= max)
                {
                    newserver = zhu;
                    break;
                }
                else
                {
                    continue;
                }

            }
            if (newserver > 0)
            {
                return sDomain = string.Format("s{0}.sq.dao50.com", newserver); ;
            }
            else
            {
                return sDomain = string.Format("s{0}.sq.dao50.com", sServerID); ;
            }   
        }

        public static string GetSite(string sGame)
        {
            string sSite = string.Empty;
            string sServerID = sGame.Replace("sq", "");
            int iServerID = 1;
            int.TryParse(sServerID, out iServerID);
            if(iServerID < 10)
            {
                sSite = string.Format("dao50_000{0}", sServerID);
            }
            else if(iServerID < 100)
            {
                sSite = string.Format("dao50_00{0}", sServerID);
            }
            else if (iServerID < 1000)
            {
                sSite = string.Format("dao50_0{0}", sServerID);
            }
            else
            {
                sSite = string.Format("dao50_{0}", sServerID);
            }
            return sSite;
        }
    }
}

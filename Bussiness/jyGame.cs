using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Xml;
namespace Bussiness
{
   public class jyGame
    {
        public static string Login(string sUserID, string sGame, string sClient)
        {
            string serverid = GetServerID(sGame);
            string timestamp = ProvideCommon.getTime().ToString();
            string password = Guid.NewGuid().ToString().Substring(0, 32);
            string site = GetSite(sGame);//"jydao50_0001"; //+serverid;
            string key = "QY7RODDD-3rSDEDE-7roadjy-489385j-25255fdf-7ROADJyfd-SHENQU-Lvoe7road";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", password);
            sbText.AppendFormat("{0}", timestamp);
            sbText.AppendFormat("{0}", key);
            string sign = ProvideCommon.MD5(sbText.ToString()).ToLower();//MD5(coopname=&serverid=&userid=&key=&timestamp=)
            sbText.Remove(0, sbText.Length);
            string sUrl = string.Format("http://s{0}.jy.dao50.com/createlogin", serverid);
            string content = string.Format("{0}|{1}|{2}|{3}",sUserID,password,timestamp,sign);
            sbText.AppendFormat("content={0}&", content);
            sbText.AppendFormat("site={0}", site);
            string sRes = ProvideCommon.GetPageInfoByPost(sUrl, sbText.ToString(), "UTF-8").Trim();
            switch (sRes)
            { 
                case "0":
                    sbText.Remove(0, sbText.Length);
                    if (sClient == "pc")
                    {
                        sbText.AppendFormat("http://s{0}.jy.dao50.com/client/Loading.swf?", serverid);
                    }
                    else
                    {
                        sbText.AppendFormat("http://s{0}.jy.dao50.com/client/game.jsp?", serverid);
                    }
                    sbText.AppendFormat("user={0}&",sUserID);
                    sbText.AppendFormat("key={0}&", password);
                    sbText.AppendFormat("site={0}", site);
                    break;
                default:
                    sbText.Remove(0, sbText.Length);
                    sbText.Append(sRes);
                    break;
            }
            return sbText.ToString();
        }

       
        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string serverid = GetServerID(sGame);
            string amount = dMoney.ToString(); ;
            string timestamp = ProvideCommon.getTime().ToString();
            string chargeid = sOrderID.Substring(0, 32);
            string payway = "1";
            string roleid = GetRoleId(sUserID, sGame);
            decimal money = Convert.ToInt32(dMoney * 10);
            decimal needmoney=Convert.ToInt32(dMoney);
            //string moneytype = "CNY";
            string site = GetSite(sGame);//"jydao50_0001"; //+serverid;
            string key = "QY569fdd-56812ef-loveWAN-7roadjy-25ea3495-7R53MYCNX-shenqu-lovedede7";
            StringBuilder sbText = new StringBuilder();

            sbText.AppendFormat("{0}", chargeid);
            sbText.AppendFormat("{0}", sUserID);
            sbText.AppendFormat("{0}", money);
            sbText.AppendFormat("{0}", payway);
            sbText.AppendFormat("{0}", needmoney);
            sbText.AppendFormat("{0}", key);
            string sSign = ProvideCommon.MD5(sbText.ToString());//MD5签名sign = md5(chargeid + username +money +payway + needmoney +key);
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://assist{0}.jy.dao50.com/chargemoney?", serverid);
            sbText.AppendFormat("chargeID={0}", chargeid);
            sbText.AppendFormat("&username={0}", sUserID);
            sbText.AppendFormat("&money={0}", money);
            sbText.AppendFormat("&payway={0}", payway);
            sbText.AppendFormat("&needmoney={0}", needmoney);
            sbText.Append("&moneytype=CNY");
            sbText.AppendFormat("&sign={0}", sSign);
            sbText.AppendFormat("&userid={0}", roleid);
           // sbText.AppendFormat("&timestamp={0}", timestamp);
            sbText.AppendFormat("&site={0}", site);

            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string sGamePayUrl = string.Empty;
            string sUrl = string.Format("{0}?{1}", sGamePayUrl, sbText.ToString());
            GamePayBLL.GamePayAdd(sTranIP, sUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string jyPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            string intRes = ProvideCommon.getJsonValue("status", sRes).Trim();
            if (intRes=="1")
            {
                sReturn = string.Format("0|{0}", sTranID);
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        public static string jyQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string sReturn = string.Empty;
            string intRes = ProvideCommon.getJsonValue("status", sRes).Trim();
            if (intRes == "1")
            {
                TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                sReturn = "0";
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }
        public static string GetServerID(string sGame)
        {
            string sDomain = string.Empty;
            string sServerID = sGame.Replace("jy", "");
            int newserver = 0;
            int ser = Convert.ToInt32(sServerID);
            string xmlUrl = @"/Inc/jyGame.xml";
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
                return newserver.ToString();
            }
            else
            {
               return sServerID; ;
            }   
        }
      
        public static string GetSite(string sGame)
        {
            
            int starNum = 10000;
            string newSite = string.Empty;
            string serverid = sGame.Replace("jy", "");
            if (!string.IsNullOrEmpty(serverid))
            {
                newSite = Convert.ToString(starNum + Convert.ToInt32(serverid)).Substring(1, 4);
                newSite = "jydao50_" + newSite;
            }
            return newSite;
        }
        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string serverid = GetServerID(sGameAbbre);
            
            StringBuilder sbText = new StringBuilder();
            string site = GetSite(sGameAbbre);//"jydao50_0001"; //+serverid;
            sbText.AppendFormat("http://assist{0}.jy.dao50.com/rolelist?", serverid);
            sbText.AppendFormat("username={0}&", sUserID);
            sbText.AppendFormat("site={0}", site);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl).Trim();
            string sReturn = string.Empty;
            string intRes = ProvideCommon.getJsonValue("status", sRes).Trim();
            if (intRes == "0")
            {
                sReturn = "1";
            }
            else
            {
                sReturn = "0";
            }
            return sReturn;
        }

        public static string GetRoleId(string sUserID, string sGameAbbre)
        {
            string serverid = GetServerID(sGameAbbre);

            StringBuilder sbText = new StringBuilder();
            string site = GetSite(sGameAbbre);//"jydao50_0001"; //+serverid;
            sbText.AppendFormat("http://assist{0}.jy.dao50.com/rolelist?", serverid);
            sbText.AppendFormat("username={0}&", sUserID);
            sbText.AppendFormat("site={0}", site);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl).Trim();
            string sReturn = string.Empty;
            string intRes = ProvideCommon.getJsonValue("Id", sRes).Trim();

            return intRes;
        }
    }
}
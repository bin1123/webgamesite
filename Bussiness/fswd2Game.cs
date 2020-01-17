using System;
using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
    public class fswd2Game
    {
        public static string Login(string sUserID,string sGame)
        {
            string ubpartnerid = "dao50";
            string gameid = "912000";
            string serverid = getServerID(sGame);
            string username = sUserID;
            string idcard = "1";
            string eventtime = DateTime.Now.ToString("yyyyMMddhhmmss");
            string ubpartnerkey = "365ub-W6W1YKETLVH;JN?@=BI1CVT.G:<Tdao50";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat(string.Format("ubpartnerid={0}&",ubpartnerid));
            sbText.AppendFormat(string.Format("gameid={0}&", gameid));
            sbText.AppendFormat(string.Format("serverid={0}&", serverid));
            sbText.AppendFormat(string.Format("username={0}&", username));
            sbText.AppendFormat(string.Format("idcard={0}&", idcard));
            sbText.AppendFormat(string.Format("eventtime={0}&", eventtime));
            sbText.AppendFormat(string.Format("ubpartnerkey={0}", ubpartnerkey));
            //MD5("ubpartnerid=&gameid=&serverid=&username=&idcard=&eventtime=&ubpartnerkey=".ToUpper()).ToUpper();
            string sSign = ProvideCommon.MD5(sbText.ToString().ToUpper()).ToUpper();
            sbText.Remove(0, sbText.Length);
            sbText.Append("http://api.365ub.com/loginbygetmethod.ashx?");
            sbText.AppendFormat("ubpartnerid={0}", ubpartnerid);
            sbText.AppendFormat("&gameid={0}", gameid);
            sbText.AppendFormat("&serverid={0}", serverid);
            sbText.AppendFormat("&username={0}", sUserID);
            sbText.AppendFormat("&idcard={0}", idcard);
            sbText.AppendFormat("&eventtime={0}&realname=", eventtime);
            sbText.AppendFormat("&sign={0}",sSign);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string ubpartnerid = "dao50";
            string gameid = "912000";
            string serverid = getServerID(sGame);
            string username = sUserID;
            string eventtime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string orderid = sOrderID;
            int iGameMoney = Convert.ToInt32(dMoney * 10);
            string gamemoney = iGameMoney.ToString();
            string responsetype = "1";
            string ubpartnerkey = "365ub-W6W1YKETLVH;JN?@=BI1CVT.G:<Tdao50";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat(string.Format("ubpartnerid={0}&", ubpartnerid));
            sbText.AppendFormat(string.Format("gameid={0}&", gameid));
            sbText.AppendFormat(string.Format("serverid={0}&", serverid));
            sbText.AppendFormat(string.Format("username={0}&", username));
            sbText.AppendFormat(string.Format("eventtime={0}&", eventtime));
            sbText.AppendFormat(string.Format("orderid={0}&", orderid));
            sbText.AppendFormat(string.Format("gamemoney={0}&", gamemoney));
            sbText.AppendFormat(string.Format("responsetype={0}&", responsetype));
            sbText.AppendFormat(string.Format("ubpartnerkey={0}", ubpartnerkey));
            //Md5(ubpartnerid=&gameid=&serverid=&username=&eventtime=&orderid=&gamemoney=&responsetype=&ubpartnerkey=)
            string sSign = ProvideCommon.MD5(sbText.ToString().ToUpper()).ToUpper();
            sbText.Remove(0, sbText.Length);
            sbText.Append("http://api.365ub.com/pay.ashx?");
            sbText.AppendFormat("ubpartnerid={0}", ubpartnerid);
            sbText.AppendFormat("&gameid={0}", gameid);
            sbText.AppendFormat("&serverid={0}", serverid);
            sbText.AppendFormat("&username={0}", sUserID);
            sbText.AppendFormat("&eventtime={0}", eventtime);
            sbText.AppendFormat("&orderid={0}", orderid);
            sbText.AppendFormat("&gamemoney={0}", gamemoney);
            sbText.AppendFormat("&responsetype={0}", responsetype);
            sbText.AppendFormat("&sign={0}", sSign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string user_ip = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(),sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string fswd2Pay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            decimal dMoney = (Convert.ToDecimal(iPayPoints))/10;
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre, iPayPoints, sUserName, sPhone, iGUserID, sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if (sTGRes != "0")
            {
                return sTGRes;
            }
            string sRes = PayResult(Pay(iGUserID.ToString(), dMoney, sTranID, sGameAbbre));
            if (sRes == "0")
            {
                return string.Format("0|{0}", sTranID);
            }
            else
            {
                return sRes;
            }
        }

        public static string fswd2QucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName);
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = Convert.ToInt32(dPrice * 10);
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
            string sReturn = PayResult(sRes);
            if(sReturn == "0")
            {
                TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
            }
            return sReturn;
        }

        public static string PayResult(string sRes)
        {
            int iIndex = sRes.IndexOf("orderstatus=");
            string sResult = string.Empty;
            if (iIndex > -1)
            {
                if (sRes.Substring(iIndex+12, 1) == "5")
                {
                    sResult = "0";
                }
                else
                {
                    sResult = "-1";
                }
            }
            else
            {
                iIndex = sRes.IndexOf("errcode=");
                if(iIndex > -1)
                {
                    if (sRes.Substring(iIndex+8) == "9003")
                    {
                        sResult = "0";
                    }
                    else
                    {
                        sResult = sRes.Substring(iIndex, 1);
                    }
                }
            }
            return sResult;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string ubpartnerid = "dao50";
            string gameid = "912000";
            string serverid = getServerID(sGameAbbre);
            string username = sUserID;
            string eventtime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string ubpartnerkey = "365ub-W6W1YKETLVH;JN?@=BI1CVT.G:<Tdao50";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat(string.Format("ubpartnerid={0}&", ubpartnerid));
            sbText.AppendFormat(string.Format("gameid={0}&", gameid));
            sbText.AppendFormat(string.Format("serverid={0}&", serverid));
            sbText.AppendFormat(string.Format("username={0}&", username));
            sbText.AppendFormat(string.Format("eventtime={0}&", eventtime));
            sbText.AppendFormat(string.Format("ubpartnerkey={0}", ubpartnerkey));
            //MD5("ubpartnerid=&gameid=&serverid=&username=&eventtime=&ubpartnerkey=".ToUpper()).ToUpper();
            string sSign = ProvideCommon.MD5(sbText.ToString().ToUpper()).ToUpper();
            sbText.Remove(0, sbText.Length);
            sbText.Append("http://api.365ub.com/CheckUser.ashx?");
            sbText.AppendFormat("ubpartnerid={0}", ubpartnerid);
            sbText.AppendFormat("&gameid={0}", gameid);
            sbText.AppendFormat("&serverid={0}", serverid);
            sbText.AppendFormat("&username={0}", sUserID);
            sbText.AppendFormat("&eventtime={0}", eventtime);
            sbText.AppendFormat("&sign={0}", sSign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sReturn = string.Empty;
            if (sRes.IndexOf("isexists=0&errcode=0") > -1)
            {
                sReturn = "1";
            }
            else
            {
                sReturn = sRes;
            }
            return sReturn;
        }

        private static string getServerID(string sGame)
        {
            string sServerID = "91210005";
            //switch(sGame)
            //{
            //    case "fswd21":
            //        sServerID = "91210005";
            //        break;
            //    case "fswd22":
            //        sServerID = "91210010";
            //        break;
            //    case "fswd23":
            //        sServerID = "91210011";
            //        break;
            //    default:
            //        sServerID = "91210005";
            //        break;
            //}
            return sServerID;
        }
    }
}

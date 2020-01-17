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
    public class sgljGame
    {
        /// <summary>
        /// 游戏登陆
        /// </summary>
        /// <param name="sUserID">用户id</param>
        /// <param name="sGame">游戏缩写</param>
        /// <returns>游戏url</returns>
        public static string Login(string sUserID, string sGame)
        {
            string logKey = "Yxnh6lG02pmbEyVU0kZ0";
            string opt = "dao50";
            string server = "s"+GetServerID(sGame);
            string user = sUserID;
            string time =  ProvideCommon.getTime().ToString();
            string sign = ProvideCommon.MD5(opt + server + user + time + logKey);
            string fcm = "1";
            string sGameUrl = string.Format("http://{0}.sglj.dao50.com/?opt={1}&server={2}&user={3}&time={4}&sign={5}&fcm={6}",server,opt,server,user,time,sign,fcm);
            return sGameUrl;
        }

        /// <summary>
        /// 游戏充值
        /// </summary>
        /// <param name="sUserID">用户id</param>
        /// <param name="dMoney">充值金额</param>
        /// <param name="sOrderID">定单号</param>
        /// <param name="sGame">游戏缩写</param>
        /// <returns></returns>
        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string payKey = "dzCHSdZiT3XDbsWRe2Oz";
            string opt = "dao50";
            string server = "s" + GetServerID(sGame);
            string user = sUserID;
            int iMoney = Convert.ToInt32(dMoney);
            decimal tombo = iMoney * 10;
            string order = sOrderID;
            string sign = ProvideCommon.MD5(opt + server + user + tombo + order + payKey);
            string sPayUrl = string.Format("http://pay.ebogame.com/pay?opt={0}&server={1}&user={2}&tombo={3}&order={4}&sign={5}",opt,server,user,tombo,order,sign);
            string sRes = ProvideCommon.GetPageInfo(sPayUrl);
            string user_ip = ProvideCommon.GetRealIP();
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            GamePayBLL.GamePayAdd(user_ip, sPayUrl, sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string sgljPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
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
            
            ///成功返回0|订单号，失败返回游戏失败结果
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "0":
                case "-6":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string sgljQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iUserID = UserBll.UserIDSel(sUserName); 
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = Convert.ToInt32(dPrice * 10);
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sRes = Pay(iUserID.ToString(), dPrice, sTranID, sGameAbbre);
           
            ///成功返回0并提交订单扣点，失败返回游戏失败结果
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "0":
                case "-6":
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

        /// <summary>
        /// 判断账号是否登陆
        /// </summary>
        /// <param name="sUserID">用户id</param>
        /// <param name="sGameAbbre">游戏缩写</param>
        /// <returns>1为角色或用户不存在，其它为存在</returns>
        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string logKey = "Yxnh6lG02pmbEyVU0kZ0";
            string opt = "dao50";
            string server = "s" + GetServerID(sGameAbbre);
            string user =  sUserID;
            string sign = ProvideCommon.MD5(opt + server + user+logKey);
            string sUserUrl = string.Format("http://{0}.sglj.dao50.com/getlevelbyuser.php?opt={1}&server={2}&user={3}&sign={4}", server, opt, server, user, sign);
            string sUserRes = ProvideCommon.GetPageInfo(sUserUrl);            
            string sReturn = string.Empty;
            switch (sUserRes)
            { 
                case "-3":
                    sReturn = "1";
                    break;
                default:
                    sReturn = "0";
                    break;
            }
            return sReturn;
        }

        /// <summary>
        /// 获取游戏id
        /// </summary>
        /// <param name="sGame">游戏缩写</param>
        /// <returns></returns>
        public static string GetServerID(string sGame)
        {
            string sServerID = sGame.Replace("sglj", "");
            return sServerID;
        }
    }
}

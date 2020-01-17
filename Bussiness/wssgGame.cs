using System;
using System.Text;
using Common;

namespace Bussiness
{
    public class wssgGame
    {
        public static string Login(string sUserID, string sGame)
        {
            string server_id = GetServerID(sGame);//游戏各个分区的编号，一区为1，二区为2
            string time = ProvideCommon.getTime().ToString();
            string pid = "39";
            string sLoginKey = "4590oaLHJFURFDDJG843sdhf345435";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("qid={0}",sUserID);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&server_id={0}", server_id);
            sbText.AppendFormat("&pid={0}", pid);
            sbText.AppendFormat("&key={0}", sLoginKey);

            string sign = ProvideCommon.MD5(sbText.ToString());//md5(“qid=”+$qid+”&time=”+$time+”&server_id=”+$server_id+”&pid=”$pid+”&key=”$key);

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.wssg.dao50.com/wssg/login?qid={1}&server_id={0}&time={2}&pid={3}&sign={4}&isAdult=1",server_id,sUserID,time,pid,sign);       
            string sUrl = sbText.ToString();
            return sUrl;
        }

        public static string Pay(string sUserID, decimal dMoney, string sOrderID, string sGame)
        {
            string server_id = GetServerID(sGame);
            string time = ProvideCommon.getTime().ToString();
            string order_amount = dMoney.ToString();
            string pid = "39";
            string sPayKey = "SRWSE9346ksdhfqweigLVL49dfg445";
            string order_id = sOrderID;
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("qid={0}", sUserID);
            sbText.AppendFormat("&order_amount={0}", order_amount);
            sbText.AppendFormat("&order_id={0}", order_id);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&server_id={0}", server_id);
            sbText.AppendFormat("&pid={0}", pid);
            sbText.AppendFormat("&key={0}", sPayKey);
            string sign = ProvideCommon.MD5(sbText.ToString());//md5(“qid=”+$qid+”&order_amount=”+$order_amount+”&order_id=”+$order_id+”&time=”+$time+”&server_id=”+$server_id+”&pid=”+$pid+”&key=”+$key)

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.wssg.dao50.com/wssg/recharge?qid={1}&order_amount={2}&order_id={3}&time={4}&server_id={0}&pid={5}&sign={6}",
                                 server_id, sUserID, order_amount, order_id, time,pid,sign);      
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            string user_ip = ProvideCommon.GetRealIP();
            GamePayBLL.GamePayAdd(user_ip, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string wssgPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            decimal dMoney = Convert.ToDecimal(iPayPoints / 10);
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
                case "2":
                    int iServerID = int.Parse(GetServerID(sGameAbbre));
                    GamePaySucBLL.GamePaySucAdd(iUserID, iGUserID, iPayPoints, sTranID, "wssg",iServerID);
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string wssgQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
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
                    int iPoint = Convert.ToInt32(dPrice) * 10;
                    int iServerID = int.Parse(GetServerID(sGameAbbre));
                    GamePaySucBLL.GamePaySucAdd(iUserID, iUserID, iPoint, sTranID, "wssg",iServerID);
                    break;
                case "2":
                    sReturn = "0";
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string server_id = GetServerID(sGameAbbre);//游戏各个分区的编号，一区为1，二区为2
            string time = ProvideCommon.getTime().ToString();
            string pid = "39";
            string sLoginKey = "4590oaLHJFURFDDJG843sdhf345435";
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("qid={0}", sUserID);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&server_id={0}", server_id);
            sbText.AppendFormat("&pid={0}", pid);
            sbText.AppendFormat("&key={0}", sLoginKey);

            string sign = ProvideCommon.MD5(sbText.ToString());//md5(”qid=”+$qid+”&time=”+$time+”&server_id=”+$server_id+”&pid=”+$pid+”&key=”$key);

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.wssg.dao50.com/wssg/queryUser?qid={1}&server_id={0}&time={2}&pid={3}&sign={4}", server_id, sUserID, time, pid,sign);
            string sUrl = sbText.ToString();
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string sReturn = string.Empty;
            if (sRes == "1")
            {
                sReturn = "0";
            }
            else
            {
                sReturn = "1";
            }
            return sReturn;
        }

        public static string GetServerID(string sGame)
        {
            string sID = sGame.Replace("wssg", "");
            return sID.ToString();
        }

        public static string RechargePrize(string sUserID, string prizeid, string sOrderID, string sGame)
        {
            string server_id = GetServerID(sGame);
            string time = ProvideCommon.getTime().ToString();
            string pid = "39";
            string sPayKey = "SRWSE9346ksdhfqweigLVL49dfg445";
            string order_id = sOrderID;
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("qid={0}", sUserID);
            sbText.AppendFormat("&prizeid={0}", prizeid);
            sbText.AppendFormat("&order_id={0}", order_id);
            sbText.AppendFormat("&time={0}", time);
            sbText.AppendFormat("&server_id={0}", server_id);
            sbText.AppendFormat("&pid={0}", pid);
            sbText.AppendFormat("&key={0}", sPayKey);
            string sign = ProvideCommon.MD5(sbText.ToString());//qid=+$qid+&prizeid=”+$prizeid+”&order_id=”+$order_id+”&time=”+$time+”&server_id=”+$server_id+”&pid=”+$pid+”&key=”+$key

            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("http://s{0}.wssg.dao50.com/wssg/rechargePrize?qid={1}&prizeid={2}&order_id={3}&time={4}&server_id={0}&pid={5}&sign={6}",
                                 server_id,sUserID,prizeid,sOrderID,time,pid,sign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            return sRes;
        }

        public static int UserDWCJNum(int iUserID)
        {
            int iNum = 0;
            DateTime dtBegin = new DateTime(2013, 6, 9);
            DateTime dtEnd = new DateTime(2013, 6, 17);
            int iPoint = GamePaySucBLL.UPointSelByGNTime("wssg", iUserID, dtBegin, dtEnd);
            if (iPoint > 999)
            {
                int iPayNum = iPoint / 1000;
                int iPayedNum = GameGiftBLL.UCountSelByGift(iUserID, "wssg2013dw");
                iNum = iPayNum - iPayedNum;
            }
            return iNum;
        }

        public static string CJBegin(int iUserID,string sGift,string sGameAbbre)
        {
            string sCJRes = string.Empty;
            int iNum = UserDWCJNum(iUserID);
            if (iNum > 0)
            {
                if (sGameAbbre.IndexOf("wssg") == 0)
                {
                    string sServerID = GetServerID(sGameAbbre);
                    int iServerID = 1;
                    if (int.TryParse(sServerID, out iServerID))
                    {
                        //抽奖开始
                        Random rdObject = new Random();
                        int iRandomNum = rdObject.Next(1, 30);
                        string sGiftID = string.Empty;
                        if (iRandomNum < 10)
                        {
                            sGiftID = string.Format("2930000{0}", iRandomNum); 
                        }
                        else
                        {
                            sGiftID = string.Format("293000{0}", iRandomNum);    
                        }
                        string sTranID = ProvideCommon.GenerateStringID();
                        string sRes = RechargePrize(iUserID.ToString(),sGiftID,sTranID,sGameAbbre);
                        if (sRes == "1")
                        {
                            sCJRes = string.Format("0|{0}", iRandomNum.ToString());
                            GameGiftBLL.GameGiftAdd(iServerID, iUserID, sGift, "wssg", sGiftID,sTranID);
                        }
                        else if(sRes == "2")
                        {
                            sCJRes = "1";
                        }
                    }
                    else
                    {
                        sCJRes = "servererr";
                    }
                }
                else
                {
                    sCJRes = "gameerr";
                }
            }
            else
            {
                sCJRes = "numless";
            }
            return sCJRes;
        }
    }
}

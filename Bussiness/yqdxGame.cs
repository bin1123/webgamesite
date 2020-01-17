using System;
using System.Text;
using Common;

namespace Bussiness
{
    public class yqdxGame
    {
        private const string tid = "113";  //商户编号
        private const string key = "TC97qGGWolh06XQwK4lKjljPwLPxAQJ1";  //商户密匙

        public static string Login(string sUserID, string sGameName)
        {
            string sid = GetServerID(sGameName);//服务器编号
            string account = sUserID;
            string pwd = account.Substring(0,4);
            string ip = ProvideCommon.GetRealIP();
            string time = ProvideCommon.getTime().ToString();
            string adultflag = "3";//0表示未认证未成年，1表示未认证成年，2表示认证的未成年，3表示为认证的成年
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("tid={0}&",tid);
            sbText.AppendFormat("sid={0}&", sid);
            sbText.AppendFormat("account={0}&", account);
            sbText.AppendFormat("pwd={0}&", pwd);
            sbText.AppendFormat("ip={0}&", ip);
            sbText.AppendFormat("time={0}&", time);
            sbText.AppendFormat("adultFlag={0}", adultflag);
            string auth = Base64.EncodeBase64(sbText.ToString());            
            string verify = ProvideCommon.MD5(string.Format("{0}{1}",auth,key));
            string sUrl = string.Format("http://passport.9787.com/api/1/uinterface.php?action=login&auth={0}&verify={1}",auth,verify);
            return sUrl;
        }

        public static string Pay(string sUserID, int iMoney, string sOrderID, string sGame,string ip,string otype)
        {
            string sid = GetServerID(sGame);//服务器编号
            string account = sUserID;
            string oid = sOrderID.Substring(0,32);//订单号 char (32)
            string money = iMoney.ToString();
            string gold = (iMoney * 10).ToString();
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("tid={0}&", tid);
            sbText.AppendFormat("sid={0}&", sid);
            sbText.AppendFormat("account={0}&", account);
            sbText.AppendFormat("oid={0}&", oid);
            sbText.AppendFormat("otype={0}&", otype);
            sbText.AppendFormat("money={0}&", money);
            sbText.AppendFormat("gold={0}&", gold);
            sbText.AppendFormat("ip={0}&", ip);
            sbText.AppendFormat("time={0}", time);
            string auth = Base64.EncodeBase64(sbText.ToString());
            string verify = ProvideCommon.MD5(string.Format("{0}{1}", auth, key));
            string sUrl = string.Format("http://passport.9787.com/api/1/uinterface.php?action=charge&auth={0}&verify={1}", auth, verify);
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            JSONObject json = JSONConvert.DeserializeObject(sRes);
            string result = json["result"].ToString();
            JSONConvert.clearJson();
            return result;
        }

        public static string yqdxPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints, string sPhone, int iGUserID)
        {
            int iMoney = iPayPoints / 10;
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre, iPayPoints, sUserName, sPhone, iGUserID, sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if (sTGRes != "0")
            {
                return sTGRes;
            }
            string otype = "1";//订单类型(0,直接充值,1虚拟币兑换)
            string sRes = Pay(iGUserID.ToString(), iMoney, sTranID, sGameAbbre,sTranIP,otype);
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "1":
                    sReturn = string.Format("0|{0}", sTranID);
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string yqdxQucikPay(string sGameAbbre, string sUserName, decimal dPrice, string sTranID)
        {
            int iMoney = Convert.ToInt32(dPrice);
            int iUserID = UserBll.UserIDSel(sUserName);
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = iMoney * 10;
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sTranIP = ProvideCommon.GetRealIP();
            string otype = "0";//订单类型(0,直接充值,1虚拟币兑换)
            string sRes = Pay(iUserID.ToString(), iMoney, sTranID, sGameAbbre,sTranIP,otype);
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
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GetNewCode(string sGame,string sUserID)
        {
            string sid = GetServerID(sGame);//服务器编号
            string account = sUserID;
            string time = ProvideCommon.getTime().ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("tid={0}&", tid);
            sbText.AppendFormat("sid={0}&", sid);
            sbText.AppendFormat("account={0}&", account);
            sbText.AppendFormat("time={0}&", time);
            string auth = Base64.EncodeBase64(sbText.ToString());
            string verify = ProvideCommon.MD5(string.Format("{0}{1}", auth, key));
            string sUrl = string.Format("http://up.9787.com/api/1/uinterface.php?action=getNewCard&auth={0}&verify={1}", auth, verify);
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            string sResult = string.Empty;
            JSONObject json = JSONConvert.DeserializeObject(sRes);
            string status = json["status"].ToString();
            if (status == "-1" || status == "1")
            {
                sResult = json["card"].ToString();
            }
            else
            {
                sResult = status;
            }
            JSONConvert.clearJson();
            return sResult;
        }

        public static string GetServerID(string sGame)
        {
            string sServerID = string.Empty;
            switch (sGame)
            {
                case "yqdx1":
                    sServerID = "13701";
                    break;
            }
            return sServerID;
        }
    }
}

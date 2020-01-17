using System;
using System.Text;
using Common;

namespace Bussiness
{
    public class mgGame
    {
        public static string Login(string sUserID,string sGameName)
        {
            string sLoginTime = ProvideCommon.getTime().ToString();
            string sKey = "Mg_dao50_gs_KEY_1OOpePFrTIUEKE23ll33P3hE3JeoOepeKJKJEKLOOeEeE";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sUserID);
            sbText.Append(sLoginTime);
            sbText.Append(sKey);
            string sFlag = Common.ProvideCommon.MD5(sbText.ToString()).ToLower();//md5($username.$time.$key) 这里传递的md5字串为小写字母

            sbText.Remove(0, sbText.Length);
            sbText.Append(LoginGameUrl(sGameName));
            sbText.Append("?username=");
            sbText.Append(sUserID);
            sbText.Append("&time=");
            sbText.Append(sLoginTime);
            sbText.Append("&isAdult=1");
            sbText.Append("&flag=");
            sbText.Append(sFlag);
            sbText.Append("&op=dao50");
            string sServer = ServerName(sGameName);
            sbText.AppendFormat("&server={0}",sServer);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, int iMoney, string sOrderID,string sGame)
        {
            string sKey = "Mg_dao50_pay_KEY_Oie2KopJE3KeJeQEukPWpIoOkeKepKlseEELKekePoee";
            string sGamePayUrl = GameUrl(sGame);
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sOrderID);
            sbText.Append(sKey);

            string sSign = Common.ProvideCommon.MD5(sbText.ToString());//uid+money+orderid+leagId+key+add_key
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("{0}/username?name={1}&money={2}&gold={3}&coupon=0&coin=0&order={4}&sign={5}",sGamePayUrl, sUserID, iMoney,iMoney/10,sOrderID, sSign);
            string sRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sTranIP = ProvideCommon.GetRealIP();
            int iUserID = 0; 
            int.TryParse(sUserID,out iUserID);
            GamePayBLL.GamePayAdd(sTranIP, sbText.ToString(), sOrderID, sRes, sGame, iUserID);
            return sRes;
        }

        public static string MGPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints,string sPhone,int iGUserID)
        {
            int iMoney = iPayPoints * 10;//单位:分
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre,iPayPoints,sUserName,sPhone,iGUserID,sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if(sTGRes != "0")
            {
                return sTGRes;
            }
            string sRes = GetReturn(Pay(iGUserID.ToString(), iMoney, sTranID, sGameAbbre));
            string sReturn = string.Empty;
            switch(sRes)
            {
                case "0":
                case "1":
                    sReturn = string.Format("0|{0}",sTranID); 
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string MGQucikPay(string sGameAbbre, string sUserName, decimal dPrice,string sTranID)
        {
            int iMoney = Convert.ToInt32(dPrice * 100);//单位:分;充值100元，给用户加1000金币0礼卷0铜币
            int iUserID = UserBll.UserIDSel(sUserName);
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = iMoney / 10;
            if(iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sPayRes = Pay(iUserID.ToString(), iMoney, sTranID, sGameAbbre);
            string sRes = GetReturn(sPayRes);
            string sReturn = string.Empty;
            switch (sRes)
            {
                case "0":
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

        public static string GetReturn(string sText)
        {
            int iId = sText.IndexOf("code");
            string sErrNo = string.Empty;
            if (iId > 0)
            {
                JSONObject json = JSONConvert.DeserializeObject(sText);
                sErrNo = json["code"].ToString();
                JSONConvert.clearJson();
            }
            return sErrNo;
        }

        public static string GameUrl(string sGame)
        {
            string sUrl = string.Empty;
            switch (sGame)
            { 
                case "mg1":
                    sUrl = "http://mg1.dao50.com:8080/pay/dao50/1/by";
                    break;
                case "mg2":
                    sUrl = "http://mg2.dao50.com:8080/pay/dao50/2/by";
                    break;
            }
            return sUrl;
        }

        public static string LoginGameUrl(string sGame)
        {
            string sUrl = string.Empty;
            switch (sGame)
            {
                case "mg1":
                    sUrl = "http://mg1.dao50.com/login.php";
                    break;
                case "mg2":
                    sUrl = "http://mg2.dao50.com/login.php";
                    break;
            }
            return sUrl;
        }

        public static string ServerName(string sGame)
        {
            string sServer = string.Empty;
            switch(sGame)
            {
                case "mg1":
                    sServer = "1";
                    break;
                case "mg2":
                    sServer = "2";
                    break;
            }
            return sServer;
        }

        public static string GameIsLogin(string sUserID, string sGame)
        { 
            string sReturn = string.Empty;
            string sServer = sGame.Replace("mg", "");
            string sUrl = string.Format("http://mg{0}.dao50.com:8080/query/dao50/{0}/by/username?name={1}", sServer, sUserID);
            string sJsonRes = ProvideCommon.GetPageInfo(sUrl);
            string sRes = GetReturn(sJsonRes);
            switch(sRes)
            {
                case "-1":
                    sReturn = "1";
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }
    }
}

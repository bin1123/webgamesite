using System;
using System.Text;
using Common;

namespace Bussiness
{
    public class ljGame
    {
        public static string Login(string sUserID,string sGameName)
        {
            string sLoginTime = ProvideCommon.getTime().ToString();
            string sKey = "long_dao50_gs_KEY_Ker9744JJelorerJEIrADw84er21WKREtKekw";
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
            sbText.Append("&agent=dao50");
            string sServer = ServerName(sGameName);
            sbText.AppendFormat("&server={0}",sServer);
            return sbText.ToString();
        }

        public static string Pay(string sUserID, int iMoney, string sOrderID,string sGame)
        {
            string sKey = "long_dao50_pay_KEY_WeeoieEwu3ReKejewrre9KejeRiee2Jekw";
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

        public static string LJPay(string sGameAbbre, int iUserID, string sUserName, int iPayPoints,string sPhone,int iGUserID)
        {
            int iMoney = iPayPoints * 10;//单位:分
            string sTranIP = ProvideCommon.GetRealIP();
            string sTranID = TransGBLL.GameSalesInit(sGameAbbre, iPayPoints, sUserName, sPhone, iGUserID, sTranIP);
            string sTGRes = TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre).ToString();
            if (sTGRes != "0")
            {
                return sTGRes;
            }
            string sRes = GetReturn(Pay(iGUserID.ToString(), iMoney, sTranID, sGameAbbre));
            string sReturn = string.Empty;
            switch(sRes)
            {
                case "0":
                    sReturn = string.Format("0|{0}",sTranID); 
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string LJQucikPay(string sGameAbbre, string sUserName, decimal dPrice,string sTranID)
        {
            int iMoney = Convert.ToInt32(dPrice * 100);//单位:分;充值100元，给用户加1000金币0礼卷0铜币
            int iUserID = UserBll.UserIDSel(sUserName);
            int iUserPoints = UserPointsBLL.UPointSel(iUserID);
            int iGamePoints = iMoney / 10;
            if (iUserPoints < iGamePoints)
            {
                return "-2";
            }
            string sPayRes = Pay(iUserID.ToString(), iMoney, sTranID, sGameAbbre);
            string sRes = GetReturn(sPayRes);
            string sReturn = string.Empty;
            switch (sRes)
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
                case "-5":
                case "-8":
                    TransGBLL.GameSalesCommit(sTranID, sUserName, sGameAbbre);
                    sReturn = "0";
                    break;
                default:
                    sReturn = sRes;
                    break;
            }
            return sReturn;
        }

        public static string GetNewCode(string sGameAbbre, string sUserID)
        {
            StringBuilder sbText = new StringBuilder();
            string game = "long";
            string agent = "dao50";
            string server = ServerName(sGameAbbre);          
            sbText.Append(game);
            sbText.Append(agent);
            sbText.Append(server);
            sbText.Append(sUserID);
            string sNewCode = ProvideCommon.MD5(sbText.ToString()).ToUpper();
            return sNewCode;//md5($game.$agent.$server.$username) 
        }

        public static string GetReturn(string sText)
        {
            int iId = sText.IndexOf("code");
            string sErrNo = string.Empty;
            if (iId > 0)
            {
                try
                {
                    JSONObject json = JSONConvert.DeserializeObject(sText);
                    sErrNo = json["code"].ToString();
                }
                finally
                {
                    JSONConvert.clearJson();
                }
            }
            return sErrNo;
        }

        public static string GameErr(string sErrID)
        {
            string sErrRes = string.Empty;
            switch(sErrID)
            {
                case "0":
                    sErrRes = "成功";
                    break;
                case "-1":
                    sErrRes = "未创建角色";
                    break;
                case "-2":
                    sErrRes = "无法识别的充值服务器";
                    break;
                case "-3":
                    sErrRes = "无效的请求签名，签名加密错误时返回";
                    break;
                case "-4":
                    sErrRes = "帐号不存在";
                    break;
                case "-5":
                    sErrRes = "重复的订单，同一个订单号重复充值时返回";
                    break;
                case "-8":
                    sErrRes = "网络异常";
                    break;
                case "-9":
                    sErrRes = "游戏服务器内部错误";
                    break;
            }
            return sErrRes;
        }

        public static string GameisLogin(string sUserID, string sGameAbbre)
        {
            string user = sUserID;
            string sGameNum = ServerName(sGameAbbre);
            StringBuilder sbText = new StringBuilder();
            string TranURL = string.Format("http://pay.my9yu.com/long/query/dao50/{0}/by/username", sGameNum);
            sbText.Append(TranURL);
            sbText.AppendFormat("?name={0}", user);
            string sJsonRes = ProvideCommon.GetPageInfo(sbText.ToString());
            string sRes = GetReturn(sJsonRes);
            string sReturn = string.Empty;
            switch (sRes)
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

        public static string GameUrl(string sGame)
        {
            string sUrl = string.Empty;
            string sServerID = ServerName(sGame);
            sUrl = string.Format("http://pay.my9yu.com/long/pay/dao50/{0}/by",sServerID);
            return sUrl;
        }

        public static string LoginGameUrl(string sGame)
        {
            string sUrl = string.Empty;
            string sServerID = ServerName(sGame);
            sUrl = string.Format("http://s{0}.long.dao50.com/login.php",sServerID);
            return sUrl;
        }

        public static string ServerName(string sGame)
        {
            string sServer = sGame.Replace("lj", "");
            return sServer;
        }
    }
}

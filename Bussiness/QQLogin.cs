using System;
using System.Text;
using System.Web;
using System.Collections.Generic;

using Common;

namespace Bussiness
{
    public class QQLogin
    {
        private const string client_id = "100225329";
        private const string client_secret = "a7a94f5cf02c8b46bc40f3597f535e46";
        private const string sAppID = "100225329";

        public static string GetCode(string code)
        {
            string response_type = "code";
            string state = Guid.NewGuid().ToString().Replace("-", "");
            string md5State = ProvideCommon.MD5(state);
            string redirect_uri = HttpUtility.UrlEncode(string.Format("http://game.dao50.com/Services/qqCallBack.aspx?ms={0}", md5State));
            string sQQCodeUrl = string.Format("https://graph.qq.com/oauth2.0/authorize?response_type={0}&client_id={1}&redirect_uri={2}&state={3}",
                                              response_type, client_id, redirect_uri, state);
            return sQQCodeUrl;
        }

        public static string GetAccessToken(string redirect_uri, string code)
        {
            string grant_type = "authorization_code";
            string sQQTokenUrl = string.Format("https://graph.qq.com/oauth2.0/token?grant_type={0}&client_id={1}&redirect_uri={2}&code={3}&client_secret={4}",
                                                  grant_type, client_id, redirect_uri, code, client_secret);
            string sTokenReturn = ProvideCommon.GetPageInfo(sQQTokenUrl);
            string[] sTokenReturnArray = sTokenReturn.Split('&');
            Dictionary<string, string> dTokenReturn = new Dictionary<string, string>();
            foreach (string i in sTokenReturnArray)
            {
                string[] sParams = i.Split('=');
                if (sParams.Length == 2)
                {
                    dTokenReturn.Add(sParams[0], sParams[1]);
                }
            }
            string sAccessToken = dTokenReturn["access_token"].ToString();
            return sAccessToken;
        }

        public static string GetOpenID(string sAccessToken)
        {
            string sQQOpenIDUrl = string.Format("https://graph.qq.com/oauth2.0/me?access_token={0}", sAccessToken);
            string sOpenIDReturn = ProvideCommon.GetPageInfo(sQQOpenIDUrl).Trim();
            string sOpenID = ProvideCommon.getJsonValueC("openid", sOpenIDReturn);
            return sOpenID;
        }

        public static string GetNickName(string sAccessToken, string sOpenID)
        {
            string sQQInfoUrl = string.Format("https://graph.qq.com/user/get_user_info?access_token={0}&oauth_consumer_key={1}&openid={2}",
                                                          sAccessToken, sAppID, sOpenID);
            string sQQInfoReturn = ProvideCommon.GetPageInfo(sQQInfoUrl).Trim();
            string sNickName = ProvideCommon.getJsonValue("nickname", sQQInfoReturn).Trim();
            return sNickName;
        }

        public static string GetAccount(string sNickName)
        {
            string sAccount = string.Empty;
            if (sNickName.Length < 1)
            {
                sAccount = ProvideCommon.GenerateStringID();
            }
            else
            {
                int iUserID = UserBll.UserIDSel(sNickName);
                if (iUserID > 999)
                {
                    sAccount = RNameAutoBLL.AutoNameCreate(sNickName);
                }
                else
                {
                    sAccount = sNickName;
                }
            }
            return sAccount;
        }
    }
}

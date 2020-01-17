using System;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Collections.Generic;

using Bussiness;
using Common;

namespace UserCenter
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class qqCallBack : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //获取Authorization Code
            string usercancel = CYRequest.GetQueryString("usercancel", false);
            if (usercancel.Length == 0)
            {
                //通过Authorization Code获取Access Token
                string code = CYRequest.GetQueryString("code", false);
                string grant_type = "authorization_code";
                string client_id = "100225329";
                string client_secret = "a7a94f5cf02c8b46bc40f3597f535e46";
                string state = CYRequest.GetQueryString("state", false);
                string md5State = ProvideCommon.MD5(state);
                string redirect_uri = context.Server.UrlEncode(string.Format("http://game.dao50.com/Services/qqCallBack.ashx?ms={0}", md5State));
                string sQQTokenUrl = string.Format("https://graph.qq.com/oauth2.0/token?grant_type={0}&client_id={1}&redirect_uri={2}&code={3}&client_secret={4}",
                                                  grant_type, client_id, redirect_uri, code,client_secret);
                string sTokenReturn = ProvideCommon.GetPageInfo(sQQTokenUrl);
                string[] sTokenReturnArray = sTokenReturn.Split('&');
                Dictionary<string,string> dTokenReturn = new Dictionary<string,string>();
                foreach(string i in sTokenReturnArray)
                {
                    string[] sParams = i.Split('=');
                    if(sParams.Length == 2){
                        dTokenReturn.Add(sParams[0],sParams[1]);
                    }
                }
                string sAccessToken = dTokenReturn["access_token"].ToString();//Access_Token的有效期默认是3个月
                //获取用户OpenID
                string sQQOpenIDUrl = string.Format("https://graph.qq.com/oauth2.0/me?access_token={0}",sAccessToken);
                string sOpenIDReturn = ProvideCommon.GetPageInfo(sQQOpenIDUrl).Trim();
                string sOpenID = ProvideCommon.getJsonValueC("openid", sOpenIDReturn);
                //context.Response.Write(string.Format("Retrun:{0}；OpenID:{1}",sOpenIDReturn,sOpenID));
                //判断openid是否存在
                int iUserID = QQUserBLL.QQUserUseridSelByOpenID(sOpenID);
                if (iUserID < 1000)
                {
                    //不存在则注册
                }
                else
                {
                    //存在则新用户登陆
                    
                }
            }
            else
            { 
                
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

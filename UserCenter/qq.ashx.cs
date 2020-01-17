using System;
using System.Web;
using System.Web.Services;
using System.Text;

using Common;

namespace UserCenter
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class qq : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string response_type = "code";
            string client_id = "100225329";
            string state = Guid.NewGuid().ToString().Replace("-", "");
            string md5State = ProvideCommon.MD5(state);
            string redirect_uri = context.Server.UrlEncode(string.Format("http://game.dao50.com/Services/qqCallBack.aspx?ms={0}",md5State));
            string sQQCodeUrl = string.Format("https://graph.qq.com/oauth2.0/authorize?response_type={0}&client_id={1}&redirect_uri={2}&state={3}",
                                              response_type,client_id,redirect_uri,state);
            context.Response.Redirect(sQQCodeUrl,true);
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

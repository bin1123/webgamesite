using System;
using System.Web;
using System.Web.Services;

namespace UserCenter.Services
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class nzto : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Cookies["type"].Value = "2";
            context.Response.Cookies["type"].Expires = DateTime.Now.AddDays(1); 
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

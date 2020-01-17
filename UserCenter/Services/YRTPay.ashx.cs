using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class YRTPay : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string uid = CYRequest.GetString("uid");
            string vcpoints = CYRequest.GetString("vcpoints");
            string tid = CYRequest.GetString("tid");
            string offer_name = CYRequest.GetString("offer_name");
            string pass = CYRequest.GetString("pass");
            int iPoint = 0;
            if (!int.TryParse(vcpoints, out iPoint))
            {
                context.Response.Write("vcpoints err");
                return;
            }
            int iUserID = 0;
            if (!int.TryParse(uid, out iUserID))
            {
                context.Response.Write("uid err");
                return;
            }
            string sParmValRes = YRTPayBLL.ParmVal(iUserID,iPoint,tid,pass);
            if(sParmValRes.Length > 0)
            {
                context.Response.Write(sParmValRes);
                return;
            }
            //验证ip
            //验证pass
            string sYRTPayRes = string.Empty;
            if (YRTPayBLL.PassVal(uid, vcpoints, tid, pass))
            {
                sYRTPayRes = YRTPayBLL.Pay(tid, iPoint, iUserID, offer_name);
                context.Response.Write(sYRTPayRes);
            }
            else
            {
                sYRTPayRes = string.Format("{\"uid\":\"{0}\",\"vcpoints\":\"{1}\",\"tid\":\"{2}\",\"offer_name\":\"{3}\",\"status\":\"1002\"}",
                                     uid, vcpoints, tid, offer_name);
                context.Response.Write(sYRTPayRes);
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

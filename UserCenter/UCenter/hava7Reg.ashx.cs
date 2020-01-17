using System;
using System.Web;
using System.Web.Services;

using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class hava7Reg : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string sAccount = context.Request["Uname"].Trim();
            string sPassWord = context.Request["Upwd"].Trim();
            string sValMessage = UserBll.RegCheck(sAccount, sPassWord);
            if (sValMessage.Length > 1)
            {
                context.Response.Write(string.Format("<script>{0};location.href='http://www.7hava.com/reg.html';</script>",sValMessage));
                return;
            }

            int iUID = UserBll.UserReg(sAccount, sPassWord);
            if (iUID < 1000)
            {
                context.Response.Write("<script>alert('注册失败，请重试！');location.href='http://www.7hava.com/reg.html';</script>");
                return;
            }
            else
            {
                string sPP = ProvideCommon.getMultiPP(iUID);
                context.Response.Redirect(string.Format("http://www.7hava.com/usercookie.aspx?un={0}&point=0&GoUrl=http://www.7hava.com&pp={1}", sAccount,sPP), true);
                return;
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

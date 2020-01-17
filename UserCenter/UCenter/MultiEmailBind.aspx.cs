using System;
using System.Web.UI;

using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    public partial class MultiEmailBind : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                if (Request.HttpMethod == "POST")
                {
                    int iUID = GetUserID();
                    string sEmail = UserInfoBLL.UserEmailSel(iUID);
                    string sBackUrL = Request.UrlReferrer.ToString();
                    string sGoUrL = string.Format("http://{0}/user.html",Request.UrlReferrer.Host);
                    if (sEmail != null && sEmail.Length > 4)
                    {
                        Response.Write(string.Format("<script>alert('邮箱已绑定！');location.href='{0}';</script>", sBackUrL));
                        return;
                    }
                    string sBindEmail = CYRequest.GetFormString("EmailTwo");
                    int iNum = UserInfoBLL.UserInfoUpdateOfEmail(sBindEmail, iUID);
                    if (iNum > 0)
                    {
                        Response.Write(string.Format("<script>alert('邮箱绑定成功！');location.href='{0}';</script>", sGoUrL));
                        return;
                    }
                    else
                    {
                        Response.Write(string.Format("<script>alert('邮箱绑定失败！');location.href='{0}';</script>", sBackUrL));
                        return;
                    }
                }
            }
        }
    }
}

using System;
using System.Web.UI;

using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    public partial class MultiMMBH : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                if (Request.HttpMethod == "POST")
                {
                    int iUserID = GetUserID();
                    string sBindQuestion = UserInfoBLL.UserQuestionSelByID(iUserID);
                    string sBackUrL = Request.UrlReferrer.ToString();
                    string sGoUrL = string.Format("http://{0}/user.html", Request.UrlReferrer.Host);
                    if (sBindQuestion != null && sBindQuestion.Length > 4)
                    {
                        Response.Write(string.Format("<script>alert('密码保护已绑定！');location.href='{0}';</script>", sBackUrL));
                        return;
                    }
                    string sQuestion = CYRequest.GetFormString("question");
                    string sAnswer = CYRequest.GetFormString("mbda");
                    int iNum = UserInfoBLL.UserInfoUpdateOfQuestion(sQuestion, sAnswer, iUserID);
                    if (iNum > 0)
                    {
                        Response.Write(string.Format("<script>alert('密码保护绑定成功！');location.href='{0}';</script>", sGoUrL));
                        return;
                    }
                    else
                    {
                        Response.Write(string.Format("<script>alert('密码保护绑定失败！');location.href='{0}';</script>", sBackUrL));
                        return;
                    }
                }
            }
        }
    }
}

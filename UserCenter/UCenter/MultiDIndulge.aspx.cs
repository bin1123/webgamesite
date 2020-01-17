using System;
using System.Web.UI;

using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    public partial class MultiDIndulge : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (LoginSessionVal() || isLoginCookie())
                {
                    if (Request.HttpMethod == "POST")
                    {
                        int iUID = GetUserID();
                        string sCredenNum = UserInfoBLL.UserCredennumSel(iUID);
                        string sBackUrL = Request.UrlReferrer.ToString();
                        string sGoUrL = string.Format("http://{0}/user.html", Request.UrlReferrer.Host);
                        if (sCredenNum.Length > 14)
                        {
                            Response.Write(string.Format("<script>alert('已经解除防沉迷！');location.href='{0}';</script>",sBackUrL));
                            return;
                        }
                        string sUserName = CYRequest.GetFormString("RealName");
                        string sCredenNumPost = CYRequest.GetFormString("CredenNum");
                        int iNum = UserInfoBLL.UserInfoUpdateOfIndulge(sUserName, sCredenNumPost, iUID);
                        if (iNum > 0)
                        {
                            Response.Write(string.Format("<script>alert('防沉迷解除成功！谢谢！');location.href='{0}';</script>",sGoUrL));
                            return;
                        }
                        else
                        {
                            Response.Write(string.Format("<script>alert('防沉迷解除失败！');location.href='{0}';</script>", sBackUrL));
                            return;
                        }
                    }
                }
        }
    }
}

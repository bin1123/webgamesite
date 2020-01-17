using System;
using System.Web.UI;

using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    public partial class EmailBind : pagebase.PageBase
    {
        string sEmail = string.Empty;
        protected string sMsg = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            int iUID = GetUserID();
            if(!Page.IsPostBack)
            {
                if (!(LoginSessionVal() || isLoginCookie()))
                {
                    Response.Redirect("../Default.aspx", true);
                }
                else
                {
                    sEmail = UserInfoBLL.UserEmailSel(iUID);
                    if (sEmail != null && sEmail.Length > 4)
                    {
                        string sUrl = string.Format("EmailModify.aspx?Email={0}",sEmail);
                        Response.Redirect(sUrl, true);
                    }
                }
            }
            if(Request.HttpMethod == "POST")
            {
                string sBindEmail = CYRequest.GetFormString("EmailTwo");
                int iNum = UserInfoBLL.UserInfoUpdateOfEmail(sBindEmail, iUID);
                if (iNum > 0)
                {
                    //更新成功
                    sMsg = "<script>alert('邮箱绑定成功！');location.href='../Default.aspx';</script>";
                    return;
                }
                else
                {
                    //更新失败
                    sMsg = "<script>alert('邮箱绑定失败！');</script>";
                    return;
                }
            }
        }
    }
}

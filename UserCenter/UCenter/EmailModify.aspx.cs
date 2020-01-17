using System;

using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    public partial class EmailModify : pagebase.PageBase
    {
        protected string oEmail = string.Empty;
        protected string sMsg = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            int iUID = GetUserID();
            if (!(LoginSessionVal() || isLoginCookie()))
            {
                Response.Redirect("../Default.aspx", true);    
            }
            else
            {
                oEmail = CYRequest.GetString("Email");
                if (oEmail == "" || oEmail == "unsafe string")
                {
                    oEmail = UserInfoBLL.UserEmailSel(iUID);
                    if(oEmail.Length < 1)
                    {
                        Response.Redirect("EmailBind.aspx", true);
                    }
                }
            }
            if (Request.HttpMethod == "POST")
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

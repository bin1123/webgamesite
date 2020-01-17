using System;
using System.Configuration;

using Common;
using Bussiness;
using DataEnity;

namespace UserCenter.GCenter
{
    public partial class sqserver : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sAccount = string.Empty;
        protected string sMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                DateTime dtLoginTime = GetLoginTime();
                int iUserID = UserBll.UserIDSel(GetAccount());
                if (!PWDUpdateBLL.PwdUpdateVal(iUserID, dtLoginTime))
                {
                    sAccount = string.Empty;
                    ClearUsersInfo();
                    sMsg = "<script>alert('密码已改，请重新登陆！')</script>";
                    return;
                }
                else
                {
                    sAccount = GetAccount();
                }
            }
            else
            {
                Server.Transfer("sq2cl.aspx",false);
            }
        }
    }
}

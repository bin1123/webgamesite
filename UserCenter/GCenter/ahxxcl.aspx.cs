using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class ahxxcl : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sMsg = string.Empty;
        protected string sAccount = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                sAccount = CYRequest.GetFormString("username").Trim();
                string sPassWord = CYRequest.GetFormString("pwd").Trim();
                string sResult = UserBll.LoginCheck(sAccount, sPassWord);
                if (sResult.Length < 1)
                {
                    if (UserBll.UserAllVal(sAccount, sPassWord))
                    {
                        string sPageUrl = Request.Url.ToString();
                        int iUserID = UserBll.UserIDSel(sAccount);
                        LoginStateSet(sAccount, iUserID, sPageUrl);
                    }
                    else
                    {
                        sMsg = "<script>alert('登陆失败，用户名密码不正确！');location.href='ahxxc.html';</script>";
                        return;
                    }
                }
                else
                {
                    sMsg = string.Format("{0}<script>location.href='ahxxc.html';</script>",sResult);
                    return;
                }
            }
            else if (LoginSessionVal() || isLoginCookie())
            {
                sAccount = GetAccount();
            }
            else
            {
                Server.Transfer("ahxxc.html",false);
            }
        }
    }
}

using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class nzLogin : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sMsg = string.Empty;
        protected string sAccount = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                string sUserName = CYRequest.GetFormString("username").Trim();
                string sPassWord = CYRequest.GetFormString("pwd").Trim();
                string sResult = UserBll.LoginCheck(sUserName, sPassWord);
                if (sResult.Length < 1)
                {
                    if (UserBll.UserAllVal(sUserName, sPassWord))
                    {
                        string sPageUrl = Request.Url.ToString();
                        int iUserID = UserBll.UserIDSel(sUserName);
                        LoginStateSet(sUserName, iUserID, sPageUrl);
                    }
                    else
                    {
                        sMsg = "<script>alert('登陆失败，用户名密码不正确！')</script>";
                    }
                }
                else
                {
                    sMsg = sResult;
                }
            }
            sAccount = GetAccount();
        }
    }
}

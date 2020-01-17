using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class lj2Login : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.HttpMethod == "POST")
            {
                string sAccount = CYRequest.GetFormString("username").Trim();
                string sPassWord = CYRequest.GetFormString("pwd").Trim();
                string sResult = UserBll.LoginCheck(sAccount, sPassWord);
                if (sResult.Length < 1)
                {
                    if (UserBll.UserAllVal(sAccount, sPassWord))
                    {
                        string sPageUrl = Request.Url.ToString();
                        int iUserID = UserBll.UserIDSel(sAccount);
                        LoginStateSet(sAccount, iUserID, sPageUrl);
                        Server.Transfer("lj2cl.aspx", false);
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
        }
    }
}

using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class ktpdlogin : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        protected string sAccount = string.Empty;
        protected string sRootUrl = ProvideCommon.GetRootURI();

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
                        sAccount = GetAccount().Trim();
                    }
                    else
                    {
                        sMsg = "<script>alert('登陆失败，用户名密码不正确！');location.href='jyc.html';</script>";
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

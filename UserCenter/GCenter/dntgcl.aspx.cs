using System;
using System.Configuration;

using Common;
using Bussiness;
using DataEnity;

namespace UserCenter.GCenter
{
    public partial class dntgcl : pagebase.PageBase
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
                        sAccount = GetAccount().Trim();
                    }
                    else
                    {
                        sMsg = "<script>alert('登陆失败，用户名密码不正确！');location.href='dntgc.html';</script>";
                    }
                }
                else
                {
                    sMsg = sResult;
                }
            }
            else
            {
                sAccount = GetAccount().Trim();
                if (sAccount == null || sAccount.Length < 4)
                {
                    Server.Transfer("dntgc.html", false);
                }
            }
        }
    }
}

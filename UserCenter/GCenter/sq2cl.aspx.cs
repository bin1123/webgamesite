using System;
using System.Configuration;

using Common;
using Bussiness;
using DataEnity;

namespace UserCenter.GCenter
{
    public partial class sq2cl : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RequestType == "POST")
            {
                string sType = CYRequest.GetString("Type");
                if (sType == "login")
                {
                    //用户登陆
                    string sUserName = CYRequest.GetString("account");
                    string pwd = CYRequest.GetString("pwdone");
                    gameval(sUserName, pwd);
                }
            }
            else
            {
                if (LoginSessionVal() || isLoginCookie())
                {
                    DateTime dtLoginTime = GetLoginTime();
                    int iUserID = UserBll.UserIDSel(GetAccount());
                    if (!PWDUpdateBLL.PwdUpdateVal(iUserID, dtLoginTime))
                    {
                        ClearUsersInfo();
                        sMsg = "<script>alert('密码已改，请重新登陆！')</script>";
                        return;
                    }
                    else
                    {
                        Server.Transfer("sqserver.aspx", false);
                    }
                }
            }
        }

        private void gameval(string sUserName, string sPassWord)
        {
            string sMD5PassWord = UserBll.PassWordMD5(sUserName, sPassWord);
            string sRes = UserBll.UserVal(sUserName, sMD5PassWord);
            string sPageUrl = Request.Url.ToString();
            if (sRes == "0")
            {
                int iUserID = UserBll.UserIDSel(sUserName);
                LoginStateSet(sUserName, iUserID, sPageUrl);
            }
            else
            {
                string sMD5PassWordNew = UserBll.PassWordMD5New(sUserName, sPassWord);
                if ("0" == UserBll.UserVal(sUserName, sMD5PassWordNew))
                {
                    int iUserID = UserBll.UserIDSel(sUserName);
                    LoginStateSet(sUserName, iUserID, sPageUrl);
                }
                else
                {
                    sMsg = "<script>alert('登陆失败，请重试！')</script>";
                    return;
                }
            }
            Server.Transfer("sqserver.aspx", false);
        }
    }
}

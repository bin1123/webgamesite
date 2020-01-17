using System;
using System.Configuration;

using Common;
using Bussiness;
using DataEnity;

namespace UserCenter.GCenter
{
    public partial class sqcl : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sAccount = string.Empty;
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
                    gameval(sUserName,pwd);
                    if(sAccount == "")
                    {
                        sMsg = "<script>alert('登陆失败，请重试！')</script>";
                    }
                }
                //else if (sType == "reg")
                //{
                //    //用户注册
                //    string sUserName = CYRequest.GetString("accountreg");
                //    string pwdone = CYRequest.GetString("pwdonereg");
                //    string pwdtwo = CYRequest.GetString("pwdtwo");

                //    if (pwdone != pwdtwo)
                //    {
                //        sMsg = "<script>alert('注册失败，密码与确认密码不一致,请正确输入！')</script>";
                //        return;
                //    }

                //    string sValMessage = UserBll.RegCheck(sUserName, pwdtwo);
                //    if (sValMessage != "")
                //    {
                //        sMsg = sValMessage;
                //        return;
                //    }

                //    int iUID = UserBll.UserReg(sUserName, pwdtwo);
                //    if (-1 == iUID)
                //    {
                //        sMsg = "<script>alert('注册失败，请重试！')</script>";
                //        return;
                //    }
                //    else if (iUID > 999)
                //    {
                //        string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                //        string sR = DiscuzUserI.BBSReg(sAccount, pwdtwo, sKey);
                //        string sPageUrl = Request.Url.ToString();
                //        LoginStateSet(sUserName, iUID, sPageUrl);
                //        sAccount = sUserName;
                //        return;
                //    }
                //}
            }
            else
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
            }
        }

        private void gameval(string sUserName,string sPassWord)
        {
            string sMD5PassWord = UserBll.PassWordMD5(sUserName, sPassWord);
            string sRes = UserBll.UserVal(sUserName, sMD5PassWord);
            string sPageUrl = Request.Url.ToString();
            if (sRes == "0")
            {
                int iUserID = UserBll.UserIDSel(sUserName);
                LoginStateSet(sUserName, iUserID, sPageUrl);
                sAccount = sUserName;
            }
            else
            { 
                string sMD5PassWordNew = UserBll.PassWordMD5New(sUserName, sPassWord);
                if ("0" == UserBll.UserVal(sUserName, sMD5PassWordNew))
                {
                    int iUserID = UserBll.UserIDSel(sUserName);
                    LoginStateSet(sUserName, iUserID, sPageUrl);
                    sAccount = sUserName;
                }
            }
        }
    }
}

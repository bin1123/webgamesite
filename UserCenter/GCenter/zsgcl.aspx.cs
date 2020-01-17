using System;
using System.Configuration;

using Common;
using Bussiness;
using DataEnity;

namespace UserCenter.GCenter
{
    public partial class zsgcl : pagebase.PageBase
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
                else if (sType == "reg")
                {
                    //用户注册
                    string sUserName = CYRequest.GetString("accountreg");
                    string pwdone = CYRequest.GetString("pwdonereg");
                    string pwdtwo = CYRequest.GetString("pwdtwo");

                    if (pwdone != pwdtwo)
                    {
                        sMsg = "<script>alert('注册失败，密码与确认密码不一致,请正确输入！')</script>";
                        return;
                    }

                    string sValMessage = UserBll.RegCheck(sUserName, pwdtwo);
                    if (sValMessage != "")
                    {
                        sMsg = sValMessage;
                        return;
                    }

                    int iUID = UserBll.UserReg(sUserName, pwdtwo);
                    if (-1 == iUID)
                    {
                        sMsg = "<script>alert('注册失败，请重试！')</script>";
                        return;
                    }
                    else if (iUID > 999)
                    {
                        string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                        string sR = DiscuzUserI.BBSReg(sUserName, pwdtwo, sKey);
                        string sPageUrl = Request.Url.ToString();
                        LoginStateSet(sUserName, iUID, sPageUrl);
                        Server.Transfer("zsg.aspx", false);
                    }
                }
            }
            else
            {
                if (LoginSessionVal() || isLoginCookie())
                {
                    Server.Transfer("zsg.aspx",false);
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
                Server.Transfer("zsg.aspx",false);
            }
            else
            { 
                string sMD5PassWordNew = UserBll.PassWordMD5New(sUserName, sPassWord);
                if ("0" == UserBll.UserVal(sUserName, sMD5PassWordNew))
                {
                    int iUserID = UserBll.UserIDSel(sUserName);
                    LoginStateSet(sUserName, iUserID, sPageUrl);
                    Server.Transfer("zsg.aspx",false);
                }
            }
        }
    }
}

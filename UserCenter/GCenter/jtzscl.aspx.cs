using System;
using System.Configuration;

using Common;
using Bussiness;
using DataEnity;

namespace UserCenter.GCenter
{
    public partial class jtscl : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sAccount = string.Empty;
        protected string sLoginMsg = string.Empty;
        protected string sRegMsg = string.Empty;
        protected string sDivType = string.Empty;

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
                    sDivType = "l";
                    gameval(sUserName,pwd);
                    if(sAccount == "")
                    {
                        sLoginMsg = "帐号密码错误，请重试！";
                    }
                }
                else if (sType == "reg")
                {
                    //用户注册
                    string sUserName = CYRequest.GetString("accountreg");
                    string pwdone = CYRequest.GetString("pwdonereg");
                    string pwdtwo = CYRequest.GetString("pwdtwo");
                    sDivType = "r";
                    if (pwdone != pwdtwo)
                    {
                        sRegMsg = "注册失败，密码与确认密码不一致,请正确输入！";
                        return;
                    }

                    string sValMessage = UserBll.RegCheckText(sUserName, pwdtwo);
                    if (sValMessage != "")
                    {
                        sRegMsg = sValMessage;
                        return;
                    }

                    int iUID = UserBll.UserReg(sUserName, pwdtwo);
                    if (-1 == iUID)
                    {
                        sRegMsg = "注册失败，请重试！";
                        return;
                    }
                    else if (iUID > 999)
                    {
                        string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                        string sR = DiscuzUserI.BBSReg(sAccount, pwdtwo, sKey);
                        string sPageUrl = Request.Url.ToString();
                        LoginStateSet(sUserName, iUID, sPageUrl);
                        sAccount = sUserName;
                        return;
                    }
                }
            }
            else
            {
                if (LoginSessionVal() || isLoginCookie())
                {
                    sAccount = GetAccount();
                }
                sDivType = "l";
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

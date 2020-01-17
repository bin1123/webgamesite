using System;
using System.Configuration;

using Common;
using Bussiness;
using DataEnity;

namespace UserCenter.GCenter
{
    public partial class byreg : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RequestType == "POST")
            {
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
                    Server.Transfer("by.aspx", false);
                }
            }
        }
    }
}

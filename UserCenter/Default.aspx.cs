using System;
using System.Configuration;

using Bussiness;
using Common;

namespace UserCenter
{
    public partial class Default : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sUrl = string.Empty;
        protected string sGName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RequestType == "POST")
            {
                string sAccountVal = CYRequest.GetFormString("account").Trim();
                string sPassWord = CYRequest.GetFormString("password").Trim();
                if(sAccountVal.Length < 4 && sPassWord.Length < 4)
                {
                    return;
                }
                string sState = string.Empty;
                string sMD5PassWord = UserBll.PassWordMD5(sAccountVal, sPassWord);
                sState = UserBll.UserVal(sAccountVal, sMD5PassWord);
                string sWWWUrl = string.Format("{0}/{1}?un={2}",sWUrl,"usercookie.aspx",sAccountVal);
                string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                string sBBSUrl = DiscuzUserI.BBSLogin(sAccountVal, sPassWord, sKey);
                string sJSUrl = string.Format("<script src='{0}'></script><script src='{1}'></script>", sBBSUrl,sWWWUrl);
                if ("0" == sState)
                {
                    int iUserID = UserBll.UserIDSel(sAccountVal);
                    string sPageUrl = Request.Url.ToString();
                    LoginStateSet(sAccountVal, iUserID, sPageUrl);
                    string sGameName = CYRequest.GetFormString("gname");
                    if (sGameName != "" && sGameName != "unsafe string")
                    {
                        sMsg = string.Format("{0}<script>window.location.href='{1}/GCenter/PlayGame.aspx?gn={2}'</script>", sJSUrl, sRootUrl, sGameName);
                        return;
                    }
                    else
                    {
                        string sFormUrl = CYRequest.GetFormString("url");
                        sMsg = string.Format("{0}<script>window.location.href='{1}';</script>",sJSUrl,sFormUrl);
                        return;
                    }
                }
                else
                {
                    string sMD5PassWordNew = UserBll.PassWordMD5New(sAccountVal, sPassWord);
                    if ("0" == UserBll.UserVal(sAccountVal, sMD5PassWordNew))
                    {
                        int iUserID = UserBll.UserIDSel(sAccountVal);
                        string sPageUrl = Request.Url.ToString();
                        LoginStateSet(sAccountVal, iUserID, sPageUrl);
                        string sGameName = CYRequest.GetFormString("gname");
                        if (sGameName != "" && sGameName != "unsafe string")
                        {
                            sMsg = string.Format("{0}<script>window.location.href='{1}/GCenter/PlayGame.aspx?gn={2}'</script>", sJSUrl, sRootUrl, sGameName);
                            return;
                        }
                        else
                        {
                            string sFormUrl = CYRequest.GetFormString("url");
                            sMsg = string.Format("{0}<script>window.location.href='{1}';</script>", sJSUrl, sFormUrl);
                            return;
                        }
                    }
                    else
                    {
                        sUrl = CYRequest.GetFormString("url");
                        sMsg = "<script>alert('用户信息输入错误，验证失败！')</script>";
                        return;
                    }
                }
            }
            sGName = CYRequest.GetString("gn");
            string sGetUrl = CYRequest.GetString("url");
            if (sGetUrl == "" || sGetUrl == "unsafe string")
            {
                if (sGName != "" && sGName != "unsafe string")
                {
                    sUrl = string.Format("{0}/GCenter/PlayGame.aspx?gn={1}", sRootUrl, sGName);
                }
                else
                {
                    sUrl = string.Format("{0}/yhzx/", sWUrl);
                }
            }
            else
            {
                sUrl = sGetUrl;
            }
            if (LoginSessionVal() || isLoginCookie())
            {
                Response.Redirect(sUrl, true);
            }
        }
    }
}

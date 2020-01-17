using System;
using System.Configuration;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    public partial class UserLoginW : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sAccount = string.Empty;
        protected int iUInterals = 0;
        protected int iPoints = 0;
        //protected string sUserName = string.Empty;
        protected int iGameNum = 0;
        protected string[] sPGames = new string[3]; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                login.Visible = false;
                logined.Visible = true;
                iPoints = GetUPoints();
                sAccount = PAccountC(GetAccount());
                int iUserID = GetUserID();
                GLoginInfo(iUserID);
            }
            else
            {
                //sUserName = GetUserName();
                login.Visible = true;
                logined.Visible = false;
            }
            userlogin.ImageUrl = string.Format("{0}/wldFolder/images/dl_ljdl.jpg", sWebUrl);
            //UserExit.ImageUrl = string.Format("{0}/wldFolder/images/dl_wytc.jpg", sWebUrl);
        }

        protected void UserExit_Click(object sender, EventArgs e)
        {
            ClearUsersInfo();
            sMsg = PageRefresh();
        }

        protected void userlogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            string sAccountVal = CYRequest.GetFormString("account").Trim();
            string sPassWord = CYRequest.GetFormString("pwdone").Trim();
            string sState = string.Empty;
            string sMD5PassWord = UserBll.PassWordMD5(sAccountVal, sPassWord);
            sState = UserBll.UserVal(sAccountVal, sMD5PassWord);
            string sCUrl = WebConfig.BaseConfig.sWUrl;
            if ("0" == sState)
            {
                sAccount = sAccountVal;
                int iUserID = UserBll.UserIDSel(sAccountVal);
                iPoints = UserPointsBLL.UPointAllSel(iUserID);
                GLoginInfo(iUserID);
                string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                string sUrl = DiscuzUserI.BBSLogin(sAccountVal, sPassWord, sKey);
                string sPageUrl = Request.Url.ToString();
                LoginStateSet(sAccount, iUserID, sPageUrl);
                string sJSUrl = string.Format("{0}/{1}?un={2}", sCUrl, "usercookie.aspx", sAccount);
                sMsg = string.Format("<script src='{0}'></script><script src='{1}'></script>{2}", sUrl, sJSUrl, PageRefresh());
            }
            else
            {
                string sMD5PassWordNew = UserBll.PassWordMD5New(sAccountVal,sPassWord);
                if ("0" == UserBll.UserVal(sAccountVal, sMD5PassWordNew))
                {
                    sAccount = sAccountVal;
                    int iUserID = UserBll.UserIDSel(sAccountVal);
                    iPoints = UserPointsBLL.UPointAllSel(iUserID);
                    GLoginInfo(iUserID);
                    string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                    string sUrl = DiscuzUserI.BBSLogin(sAccountVal, sPassWord, sKey);
                    string sPageUrl = Request.Url.ToString();
                    LoginStateSet(sAccount, iUserID, sPageUrl);
                    string sJSUrl = string.Format("{0}/{1}?un={2}", sCUrl, "usercookie.aspx", sAccount);
                    sMsg = string.Format("<script src='{0}'></script><script src='{1}'></script>{2}", sUrl, sJSUrl, PageRefresh());
                }
                else
                {
                    sMsg = "<script>alert('用户信息输入错误，验证失败！')</script>";
                    return;
                }
            }
        }

        protected void GLoginInfo(int iUserID)
        {
            string sPGameInfo = GameLoginBLL.GameLoginLastSel(iUserID);
            if (sPGameInfo != null && sPGameInfo.Length > 4)
            {
                iGameNum = sPGameInfo.Split('|').Length;
            }
            else
            {
                iGameNum = 0;
            }
            sPGames = sPGameInfo.Split('|');
        }
    }
}

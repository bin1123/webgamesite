using System;

using Common;
using Bussiness;

namespace UserCenter.GCenter
{
    public partial class sssgcl : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;
        protected string sAccount = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RequestType == "POST")
            {
                string sType = CYRequest.GetFormString("Type");
                if (sType == "login")
                {
                    gameval();
                }
                else
                {
                    sAccount = GetAccount();
                    string sGameAbbre = CYRequest.GetFormString("gameabbre");
                    int iUserID = GetUserID();
                    if(iUserID < 1000)
                    {
                        iUserID = UserBll.UserIDSel(sAccount);
                    }
                    gamelogin(sGameAbbre, iUserID);
                }
            }
            else
            {
                if (LoginSessionVal() || isLoginCookie())
                {
                    sAccount = GetAccount();
                }
                else
                {
                    Response.Redirect(string.Format("{0}/yxzq/sg/index1.html",sWUrl), true);
                }
            }
        }

        private void gameval()
        {
            sAccount = CYRequest.GetString("account").Trim();
            string sPassWord = CYRequest.GetString("passwordl");
            string sMD5PassWord = UserBll.PassWordMD5(sAccount, sPassWord);
            string sRes = UserBll.UserVal(sAccount, sMD5PassWord);
            string sPageUrl = Request.Url.ToString();
            if (sRes == "0")
            {
                int iUserID = UserBll.UserIDSel(sAccount);
                LoginStateSet(sAccount, iUserID, sPageUrl);
                return;
            }
            else
            { 
                string sMD5PassWordNew = UserBll.PassWordMD5New(sAccount, sPassWord);
                if ("0" == UserBll.UserVal(sAccount, sMD5PassWordNew))
                {
                    int iUserID = UserBll.UserIDSel(sAccount);
                    LoginStateSet(sAccount, iUserID, sPageUrl);
                }
                else
                {
                    Response.Redirect(string.Format("{0}/yxzq/sg/index1.html", sWUrl), true);
                }
            }
        }

        private void gamelogin(string sGameAbbre,int iUserID)
        {
            string sSource = string.Empty;
            string sUrl = string.Empty;
            string client = "pc";
            sUrl = sssgGame.Login(iUserID.ToString(), sGameAbbre, sSource, client);
            GameLoginBLL.GameLoginAdd(iUserID, sGameAbbre, ProvideCommon.GetRealIP(), sUrl);
            Response.Redirect(sUrl, true);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;

using DataEnity;
using Bussiness;
using Common;

namespace UserCenter.Services
{
    public partial class qqCallBack : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取Authorization Code
            string usercancel = CYRequest.GetQueryString("usercancel", false);
            if (usercancel.Length == 0)
            {
                //通过Authorization Code获取Access Token
                string code = CYRequest.GetQueryString("code", false);
                string state = CYRequest.GetQueryString("state", false);
                string sMD5State = CYRequest.GetQueryString("ms", false);
                string sMD5StateVal = ProvideCommon.MD5(state);
                if (sMD5State == sMD5StateVal)
                {
                    string redirect_uri = Server.UrlEncode(string.Format("http://game.dao50.com/Services/qqCallBack.aspx?ms={0}", sMD5StateVal));
                    string sAccessToken = QQLogin.GetAccessToken(redirect_uri, code);
                    string sOpenID = QQLogin.GetOpenID(sAccessToken);
                    //判断openid是否存在
                    int iUserID = QQUserBLL.QQUserUseridSelByOpenID(sOpenID);
                    if (iUserID < 1000)
                    {
                        string sNickName = QQLogin.GetNickName(sAccessToken, sOpenID);
                        string sAccount = QQLogin.GetAccount(sNickName);
                        int iType = 3;
                        int iUID = UserBll.UserReg(sAccount, sOpenID,iType);
                        if (-1 == iUID)
                        {
                            Response.Write("<script>alert('注册失败，请重试！location.href='http://www.dao50.com/';')</script>");
                            return;
                        }
                        else if (iUID > 999)
                        {
                            UserInfo uiObject = new UserInfo();
                            uiObject.Credennum = "";
                            uiObject.Answer = "";
                            uiObject.Email = "";
                            uiObject.Name = sNickName;
                            uiObject.question = "";
                            uiObject.regip = ProvideCommon.GetRealIP();
                            uiObject.uid = iUID;
                            UserInfoBLL.UserInfoAdd(uiObject);
                            string sPageUrl = Request.Url.ToString();
                            QQUserBLL.QQUserAdd(iUID, sOpenID, sPageUrl);
                            LoginStateSet(sAccount, iUID, sPageUrl);
                            string sWUrl = WebConfig.BaseConfig.sWUrl;
                            string sWWWUrl = string.Format("{0}/{1}?un={2}", sWUrl, "usercookie.aspx", sAccount);
                            string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                            string sBBSUrl = DiscuzUserI.BBSLogin(sAccount, sOpenID, sKey);
                            string sJSUrl = string.Format("<script src='{0}'></script><script src='{1}'></script>", sBBSUrl, sWWWUrl);
                            Response.Write(string.Format("{0}<script>alert('注册成功！');location.href='http://www.dao50.com/';</script>", sJSUrl));
                            return;
                        }
                    }
                    else
                    {
                        string sAccount = UserBll.AccountSel(iUserID).Trim();
                        int iPoints = UserPointsBLL.UPointAllSel(iUserID);
                        string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                        string sUrl = DiscuzUserI.BBSLogin(sAccount, sOpenID, sKey);
                        string sPageUrl = Request.Url.ToString();
                        LoginStateSet(sAccount, iUserID, sPageUrl);
                        string sCUrl = WebConfig.BaseConfig.sWUrl;
                        string sJSUrl = string.Format("{0}/{1}?un={2}", sCUrl, "usercookie.aspx", sAccount);
                        Response.Write(string.Format("<script src='{0}'></script><script src='{1}'></script><script>location.href='{2}'</script>",
                                       sUrl, sJSUrl, sCUrl));
                    }
                }
                else
                {
                    Response.Write("state err");
                }
            }
            else
            {
                Response.Write("<script>alert('登陆失败！location.href='http://www.dao50.com/';')</script>");
                return;
            }
        }
    }
}

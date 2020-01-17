using System;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    public partial class noregreg : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                string sAccount = CYRequest.GetString("txz").Trim();
                string sPassWord = CYRequest.GetString("pwdtwo").Trim();
                string sWUrl = WebConfig.BaseConfig.sWUrl;

                string sValMessage = UserBll.RegCheckText(sAccount, sPassWord);
                if (sValMessage != "")
                {
                    sMsg = sValMessage;
                    return;
                }

                int iUserID = GetUserID();
                if (iUserID > 1000)
                {
                    //string sState = UserBll.RegStateSel(iUserID);
                    //if(sState == "0")
                    string sOldAccount = GetAccount();
                    if(sOldAccount.Substring(0,1) != "?")
                    {
                        sMsg = "你的账号已经绑定，请勿重复绑定注册！谢谢！";
                        return;
                    }
                    string sMD5PassWord = UserBll.PassWordMD5(sAccount, sPassWord);
                    int iNum = UserBll.UserUpdateNamePWD(iUserID, sAccount, sMD5PassWord);
                    if (iNum > 0)
                    {
                        NoRegLoginBLL.NameReg(sAccount, iUserID.ToString());
                        string sUrl = string.Empty;
                        string sGameName = CYRequest.GetString("gn");
                        if (sGameName.Length > 1)
                        {
                            sUrl = string.Format("/gcenter/wan.aspx?gn={0}", sGameName);
                        }
                        else
                        {
                            string sLoginedGame = GetLogin();
                            string sFirstGame = sLoginedGame.Split('|')[0];
                            if (sFirstGame.Length > 0)
                            {
                                sUrl = string.Format("/gcenter/wan.aspx?gn={0}", sFirstGame);
                            }
                            else
                            {
                                sUrl = sWUrl;
                            }
                        }
                        string sPageUrl = Request.Url.ToString();
                        LoginStateSet(sAccount, iUserID, sPageUrl);
                        Response.Redirect(sUrl,true);
                        return;
                    }
                    else
                    {
                        sMsg = "绑定账号失败！请重试！谢谢！";
                        return;
                    }
                }
                else
                {
                    sMsg = "数字ID不存在绑定账号失败！谢谢！";
                    return;
                }
            }
        }
    }
}

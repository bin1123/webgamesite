using System;

using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    public partial class MultiUPWordM : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                if (Request.HttpMethod == "POST")
                {
                    int iUserID = GetUserID();
                    DateTime dtLoginTime = GetLoginTime();
                    string sBackUrL = Request.UrlReferrer.ToString();
                    string sHost = Request.UrlReferrer.Host;
                    string sGoUrL = string.Format("http://{0}/user.html", sHost);
                    if (!PWDUpdateBLL.PwdUpdateVal(iUserID, dtLoginTime))
                    {
                        ClearUsersInfo();
                        string sToUrl = string.Format("http://{0}/usercookie.aspx?type=del&GoUrl={1}", sHost, sGoUrL);
                        Response.Redirect(sToUrl, true);
                        return;
                    }
                    string sPassWord = CYRequest.GetFormString("passwordtwo");
                    string sAccountC = GetAccount();
                    string sOPassWord = UserBll.PassWordMD5(sAccountC, CYRequest.GetString("bpassword"));
                    int iRes = UserBll.PWDVal(iUserID, sOPassWord);
                    if (iRes > 999)
                    {
                        string sMD5PassWord = UserBll.PassWordMD5(sAccountC, sPassWord);
                        if (1 == UserBll.UserUpdatePWD(iUserID, sMD5PassWord))
                        {
                            ClearUsersInfo();
                            string sToUrl = string.Format("http://{0}/usercookie.aspx?type=del&GoUrl={1}", sHost, sGoUrL);
                            Response.Write(string.Format("<script>alert('修改密码成功！请重新登陆!');location.href='{0}';</script>",sToUrl));
                        }
                        else
                        {
                            Response.Write(string.Format("<script>alert('修改密码失败！');location.href='{0}';</script>", sBackUrL));
                        }
                    }
                    else
                    {
                        string sMD5PassWordNew = UserBll.PassWordMD5New(sAccountC, CYRequest.GetFormString("bpassword"));
                        iRes = UserBll.PWDVal(iUserID, sMD5PassWordNew);
                        if (iRes > 999)
                        {
                            string sMD5PassWord = UserBll.PassWordMD5(sAccountC, sPassWord);
                            if (1 == UserBll.UserUpdatePWD(iUserID, sMD5PassWord))
                            {
                                ClearUsersInfo();
                                string sToUrl = string.Format("http://{0}/usercookie.aspx?type=del&GoUrl={1}", sHost, sGoUrL);
                                Response.Write(string.Format("<script>alert('修改密码成功！请重新登陆!');location.href='{0}';</script>", sToUrl));
                            }
                            else
                            {
                                Response.Write(string.Format("<script>alert('修改密码失败！');location.href='{0}';</script>", sBackUrL));
                            }
                        }
                        else
                        {
                            Response.Write(string.Format("<script>alert('原始密码错误！');location.href='{0}';</script>", sBackUrL));
                        }
                    }
                }
            }
        }
    }
}

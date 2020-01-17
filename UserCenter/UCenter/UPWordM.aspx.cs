using System;

using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    public partial class UPWordM : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected int iPoints = 0;
        protected string sAccount = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(LoginSessionVal() || isLoginCookie()))
            {
                Response.Redirect(string.Format("{0}/Default.aspx", sRootUrl), true);
                return;
            }
            else
            {
                int iUserID = GetUserID();
                DateTime dtLoginTime = GetLoginTime();
                if(!PWDUpdateBLL.PwdUpdateVal(iUserID,dtLoginTime))
                {
                    ClearUsersInfo();
                    Response.Redirect(string.Format("{0}/Default.aspx", sRootUrl), true);
                    return;
                }
            }
            iPoints = GetUPoints();
            sAccount = GetAccount(); 
            if(Request.HttpMethod == "POST")
            {
                int iUserID = GetUserID();
                string sPassWord= CYRequest.GetFormString("passwordtwo");
                string sAccountC = UserBll.AccountSel(iUserID).Trim();
                string sOPassWord = UserBll.PassWordMD5(sAccountC,CYRequest.GetFormString("bpassword"));                
                int iRes = UserBll.PWDVal(iUserID, sOPassWord);          
                if (iRes > 999)
                {
                    string sMD5PassWord = UserBll.PassWordMD5(sAccountC,sPassWord);
                    if (1 == UserBll.UserUpdatePWD(iUserID,sMD5PassWord))
                    {
                        ClearUsersInfo();
                        sMsg = "<script>alert('修改密码成功！请重新登陆!');location.href='../Default.aspx';</script>";
                    }
                    else
                    {
                        //更新失败
                        sMsg = "<script>alert('修改密码失败！');</script>";
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
                            sMsg = "<script>alert('修改密码成功！请重新登陆!');location.href='../Default.aspx';</script>";
                        }
                        else
                        {
                            //更新失败
                            sMsg = "<script>alert('修改密码失败！');</script>";
                        }
                    }
                    else
                    {
                        sMsg = "<script>alert('原始密码输入错误！');</script>";
                    }
                }
            }
        }
    }
}

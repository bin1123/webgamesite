using System;
using System.Text;

using Bussiness;
using Common;


namespace UserCenter.UCenter
{
    public partial class UserService : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sRes = string.Empty;
            if (LoginSessionVal() || isLoginCookie())
            {
                if (Request.HttpMethod == "POST" || Request.HttpMethod == "GET")
                {
                    int iUserID = GetUserID();
                    string sType = CYRequest.GetString("Type");
                    switch (sType)
                    { 
                        case "UpdatePass":
                             Response.Write(UpdatePass(iUserID));
                            break;
                        case "PassProtect":
                            Response.Write(PassProtect(iUserID));
                            break;
                        case "DIndulge":
                            Response.Write(DIndulge(iUserID));
                            break;
                        case "EmailBind":
                            Response.Write(EmailBind(iUserID));
                            break;
                        case "UserInfoUpdate":
                            Response.Write(UserInfoUpdate());
                            break;
                        case "UserMoreSel":
                            Response.Write(UserMoreSel(iUserID));
                            break;
                    }
                }
            }
            else
            {
                sRes = "-1";
            }
            Response.Write(sRes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passwordtwo">新密码</param>
        /// <param name="bpassword">原密码</param>
        /// <returns>0成功,1失败,2原始密码验证失败</returns>
        private string UpdatePass(int iUserID)
        {
            string passwordtwo = CYRequest.GetString("passwordtwo");
            string bpassword = CYRequest.GetString("bpassword");
            string sMsg = string.Empty;
            string sAccountC = UserBll.AccountSel(iUserID).Trim();
            string sOPassWord = UserBll.PassWordMD5(sAccountC, bpassword);
            int iRes = UserBll.PWDVal(iUserID, sOPassWord);
            if (iRes > 999)
            {
                string sMD5PassWord = UserBll.PassWordMD5(sAccountC, passwordtwo);
                if (1 == UserBll.UserUpdatePWD(iUserID, sMD5PassWord))
                {
                    sMsg = "0";
                }
                else
                {
                    sMsg = "1";
                }
            }
            else
            {
                string sMD5PassWordNew = UserBll.PassWordMD5New(sAccountC, bpassword);
                iRes = UserBll.PWDVal(iUserID, sMD5PassWordNew);
                if (iRes > 999)
                {
                    string sMD5PassWord = UserBll.PassWordMD5(sAccountC, passwordtwo);
                    if (1 == UserBll.UserUpdatePWD(iUserID, sMD5PassWord))
                    {
                        ClearUsersInfo();
                        sMsg = "0";
                    }
                    else
                    {
                        sMsg = "1";
                    }
                }
                else
                {
                    sMsg = "2";
                }
            }
            return sMsg;
        }

        private string PassProtect(int iUserID)
        {
            string sQuestion = CYRequest.GetString("question");
            string sAnswer = CYRequest.GetString("mbda");
            int iNum = UserInfoBLL.UserInfoUpdateOfQuestion(sQuestion, sAnswer, iUserID);
            string sMsg = string.Empty;
            if (iNum > 0)
            {
                sMsg = "0";
            }
            else
            {
                sMsg = "1";
            }
            return sMsg;
        }

        private string DIndulge(int iUserID)
        {
            string sUserName = CYRequest.GetFormString("UserName");
            string sCredenNum = CYRequest.GetFormString("CredenNum");
            int iNum = UserInfoBLL.UserInfoUpdateOfIndulge(sUserName, sCredenNum, iUserID);
            string sMsg = string.Empty;
            if (iNum > 0)
            {
                //更新成功
                sMsg = "0";
            }
            else
            {
                //更新失败
                sMsg = "1";
            }
            return sMsg;
        }

        private string EmailBind(int iUserID)
        {
            string sBindEmail = CYRequest.GetFormString("EmailTwo");
            int iNum = UserInfoBLL.UserInfoUpdateOfEmail(sBindEmail, iUserID);
            string sMsg = string.Empty;
            if (iNum > 0)
            {
                //更新成功
                sMsg = "0";
            }
            else
            {
                //更新失败
                sMsg = "1";
            }
            return sMsg;
        }

        private string UserInfoUpdate()
        {
            return "";
        }

        private string UserMoreSel(int iUserID)
        {
            return UserMoreBLL.UserMoreJsonSel(iUserID);
        }
    }
}

using System;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ServiceNoLogin : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod == "POST" || context.Request.HttpMethod == "GET")
            {
                string sAjaxType = CYRequest.GetString("AjaxType");
                string sRes = string.Empty;
                string account = string.Empty;
                string question = string.Empty;
                string answer = string.Empty;
                switch (sAjaxType)
                {
                    case "QuestionVal":
                        account = CYRequest.GetString("un");
                        question = CYRequest.GetString("question");
                        answer = CYRequest.GetString("answer");
                        context.Response.Write(QuestionVal(account, question, answer));
                        break;
                    case "PassWordFind":
                        account = CYRequest.GetString("un");
                        question = CYRequest.GetString("question");
                        answer = CYRequest.GetString("answer");
                        if (QuestionVal(account, question, answer) == "2")
                        {
                            string sPassWordTwo = CYRequest.GetString("pwdtwo");
                            sRes = PassWordFind(account, sPassWordTwo);
                        }
                        else
                        {
                            sRes = "1";
                        }
                        context.Response.Write(sRes);
                        break;
                    case "UserMoreSel":
                        account = CYRequest.GetString("un");
                        context.Response.Write(UserMoreSel(account));
                        break;
                    case "UserCredenSel":
                        account = CYRequest.GetString("un");
                        context.Response.Write(UserCredenSel(account));
                        break;
                    case "UserEmailSel":
                        account = CYRequest.GetString("un");
                        context.Response.Write(UserEmailSel(account));
                        break;
                    case "UserQuestionSel":
                        account = CYRequest.GetString("un");
                        context.Response.Write(UserQuestionSel(account));
                        break;
                }
            }
        }

        protected string QuestionVal(string sAccount,string sQuestion,string sAnswer)
        {
            SqlParameter fanhui = new SqlParameter();
            fanhui.ParameterName = "@returnValue";
            fanhui.Direction = ParameterDirection.ReturnValue;
            SqlParameter[] pm = new SqlParameter[4];
            pm[0] = fanhui;
            pm[1] = new SqlParameter("@userName", SqlDbType.VarChar);
            pm[1].Value = sAccount;
            pm[2] = new SqlParameter("@question", SqlDbType.VarChar);
            pm[2].Value = sQuestion;
            pm[3] = new SqlParameter("@answer", SqlDbType.VarChar);
            pm[3].Value = sAnswer;
            UserInfoBLL.getn("SP_UserInfo_searchPass", pm);
            return fanhui.Value.ToString();
        }

        protected string PassWordFind(string sAccount,string sPassWordTwo)
        {
            string sRes = string.Empty;
            int iUserID = UserBll.UserIDSel(sAccount);
            if (iUserID > 999)
            {
                string sMD5PassWord = UserBll.PassWordMD5(sAccount, sPassWordTwo);
                if (1 == UserBll.UserUpdatePWD(iUserID, sMD5PassWord))
                {
                    sRes = "0";
                }
                else
                {
                    sRes = "4";
                }
            }
            else
            {
                sRes = "3";
            }
            return sRes;
        }

        private string UserMoreSel(string sAccount)
        {
            int iUserID = UserBll.UserIDSel(sAccount);
            return UserMoreBLL.UserMoreJsonSel(iUserID);
        }

        private string UserCredenSel(string sAccount)
        {
            int iUserID = UserBll.UserIDSel(sAccount);
            string sCredenNum = UserInfoBLL.UserCredennumSel(iUserID);
            string sRes = string.Empty;
            if (sCredenNum.Length > 14)
            {
                sRes = "1";
            }
            return sRes;
        }

        private string UserEmailSel(string sAccount)
        {
            int iUserID = UserBll.UserIDSel(sAccount);
            string sEmail = UserInfoBLL.UserEmailSel(iUserID);
            return sEmail;
        }

        private string UserQuestionSel(string sAccount)
        {
            int iUserID = UserBll.UserIDSel(sAccount);
            string sBindQuestion = UserInfoBLL.UserQuestionSelByID(iUserID);
            string sRes = string.Empty;
            if (sBindQuestion.Length > 6)
            {
                sRes = "1";
            }
            return sRes;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

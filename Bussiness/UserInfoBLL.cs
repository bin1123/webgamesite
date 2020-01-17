using System.Data.SqlClient;
using DataAccess;
using DataEnity;
using Common;

namespace Bussiness
{
    public class UserInfoBLL
    {
        #region 增,删,改
        public static int UserInfoAdd(UserInfo uiObject)
        {
            return UserInfoDAL.UserInfoAdd(uiObject);
        }

        public static void UserInfoAdd(int iUserID)
        {
            UserInfo uiObject = new UserInfo();
            uiObject.Answer = "";
            uiObject.question = "";
            uiObject.regip = ProvideCommon.GetRealIP();//注册ip
            uiObject.uid = iUserID;
            uiObject.Email = "";
            UserInfoAdd(uiObject);
        }

        public static string UserEmailSel(int iUID)
        {
            string sEmail = UserInfoDAL.UserEmailSelByID(iUID);
            return sEmail;
        }

        public static string UserCredennumSel(int iUID)
        {
            string sCredennum = UserInfoDAL.UserCredennumSelByID(iUID);
            if (string.IsNullOrEmpty(sCredennum) || "" == sCredennum.Trim())
            {
                return "";
            }
            else
            {
                return sCredennum;
            }
        }

        public static string UserQuestionSelByID(int iUID)
        {
            return UserInfoDAL.UserQuestionSelByID(iUID);
        }

        public static int UserInfoDel(int iUserID)
        {
            return UserInfoDAL.UserInfoDel(iUserID);
        }

        public static int UserInfoUpdate(UserInfo uiObject)
        {
            return UserInfoDAL.UserInfoUpdate(uiObject);
        }

        public static int UserInfoUpdateOfEmail(string sEmail, int iUserID)
        {
            return UserInfoDAL.UserInfoUpdateOfEmail(sEmail, iUserID);
        }

        public static int UserInfoUpdateOfIndulge(string sName, string sCredennum, int iUserID)
        {
            return UserInfoDAL.UserInfoUpdateOfIndulge(sName, sCredennum, iUserID);
        }

        public static int getn(string cunchuName, params SqlParameter[] canshu)
        {
            return UserInfoDAL.getn(cunchuName, canshu);
        }

        public static int UserInfoUpdateOfQuestion(string sQuestion, string sAnswer, int iUserID)
        {
            return UserInfoDAL.UserInfoUpdateOfQuestion(sQuestion,sAnswer,iUserID);
        }
        #endregion

        #region 用户信息查询
        public static UserInfo UserInfoSelByID(int iUserID)
        {
            return UserInfoDAL.UserInfoSelByID(iUserID);
        }
        #endregion
    }
}

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class UserInfoDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static UserInfo UserInfoBind(IDataReader drUserInfo)
        {
            UserInfo uiObject = new UserInfo();
            if (drUserInfo.Read())
            {
                uiObject.Answer = drUserInfo["answer"].ToString();
                uiObject.Credennum = drUserInfo["credennum"].ToString();
                uiObject.Name = drUserInfo["name"].ToString();
                uiObject.question = drUserInfo["question"].ToString();
                uiObject.regip = drUserInfo["regip"].ToString();
                uiObject.Email = drUserInfo["email"].ToString();
                uiObject.uid = Convert.ToInt32(drUserInfo["userid"]);
            }
            else
            {
                uiObject.Answer = "";
                uiObject.Credennum = "";
                uiObject.Name = "";
                uiObject.question = "";
                uiObject.regip = "";
            }
            drUserInfo.Close();
            return uiObject;
        }

        #region 用户的增删改查
        /// <summary>
        /// 用户基本信息添加
        /// </summary>
        /// <param name="uObejct">用户对象</param>
        /// <returns>用户id</returns>
        public static int UserInfoAdd(UserInfo uiObject)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUInfoAdd = dbUCenter.GetStoredProcCommand("SP_UserInfo_Add");

            dbUCenter.AddInParameter(dcUInfoAdd, "@userid", DbType.Int32, uiObject.uid);
            dbUCenter.AddInParameter(dcUInfoAdd, "@answer", DbType.String, uiObject.Answer);
            dbUCenter.AddInParameter(dcUInfoAdd, "@Credennum", DbType.String, uiObject.Credennum);
            dbUCenter.AddInParameter(dcUInfoAdd, "@name", DbType.String, uiObject.Name);
            dbUCenter.AddInParameter(dcUInfoAdd, "@question", DbType.String, uiObject.question);
            dbUCenter.AddInParameter(dcUInfoAdd, "@regip",DbType.String,uiObject.regip);
            dbUCenter.AddInParameter(dcUInfoAdd, "@email", DbType.String, uiObject.Email);

            return dbUCenter.ExecuteNonQuery(dcUInfoAdd);
        }

        public static int UserInfoDel(int iUID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserInfoDel = dbUCenter.GetStoredProcCommand("SP_UserInfo_DelByID");

            dbUCenter.AddInParameter(dcUserInfoDel, "@userid", DbType.Int32, iUID);

            return dbUCenter.ExecuteNonQuery(dcUserInfoDel);
        }

        public static int UserInfoUpdate(UserInfo uiObject)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserInfoUpdate = dbUCenter.GetStoredProcCommand("SP_UserInfo_UpdateByID");

            dbUCenter.AddInParameter(dcUserInfoUpdate, "@userid", DbType.Int32, uiObject.uid);
            dbUCenter.AddInParameter(dcUserInfoUpdate, "@answer", DbType.String, uiObject.Answer);
            dbUCenter.AddInParameter(dcUserInfoUpdate, "@Credennum", DbType.String, uiObject.Credennum);
            dbUCenter.AddInParameter(dcUserInfoUpdate, "@name", DbType.String, uiObject.Name);
            dbUCenter.AddInParameter(dcUserInfoUpdate, "@question", DbType.String, uiObject.question);
            dbUCenter.AddInParameter(dcUserInfoUpdate, "@regip", DbType.String, uiObject.regip);
            dbUCenter.AddInParameter(dcUserInfoUpdate, "@email", DbType.String, uiObject.Email);

            return dbUCenter.ExecuteNonQuery(dcUserInfoUpdate);
        }

        public static int UserInfoUpdateOfEmail(string sEmail,int iUserID)
        { 
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserInfoUpdate = dbUCenter.GetStoredProcCommand("SP_UserInfo_EmailUpdateByID");

            dbUCenter.AddInParameter(dcUserInfoUpdate, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcUserInfoUpdate, "@email", DbType.String, sEmail);

            return dbUCenter.ExecuteNonQuery(dcUserInfoUpdate);
        }

        public static int UserInfoUpdateOfIndulge(string sName, string sCredennum, int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserInfoUpdate = dbUCenter.GetStoredProcCommand("SP_UserInfo_IndulgeUpdateByID");

            dbUCenter.AddInParameter(dcUserInfoUpdate, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcUserInfoUpdate, "@name", DbType.String, sName);
            dbUCenter.AddInParameter(dcUserInfoUpdate, "@credennum", DbType.String, sCredennum);

            return dbUCenter.ExecuteNonQuery(dcUserInfoUpdate);
        }

        /// <summary>
        /// 用户信息查询
        /// </summary>
        /// <returns></returns>
        public static UserInfo UserInfoSelByID(int iUID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUserInfoVal = dbUCenter.GetStoredProcCommand("SP_UserInfo_SelByID");

            dbUCenter.AddInParameter(dcUserInfoVal, "@userid", DbType.String, iUID);

            return UserInfoBind(dbUCenter.ExecuteReader(dcUserInfoVal));
        }

        /// <summary>
        /// 用户邮箱查询
        /// </summary>
        /// <returns></returns>
        public static string UserEmailSelByID(int iUID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUserInfoVal = dbUCenter.GetStoredProcCommand("SP_UserInfoEmail_SelByID");

            dbUCenter.AddInParameter(dcUserInfoVal, "@userid", DbType.String, iUID);

            IDataReader drUserInfo = dbUCenter.ExecuteReader(dcUserInfoVal);
            string sEmail = string.Empty;

            if (drUserInfo.Read())
            {
                sEmail = drUserInfo["email"].ToString();
            }
            drUserInfo.Close();
            return sEmail;
        }

        /// <summary>
        /// 用户身份证号查询
        /// </summary>
        /// <returns></returns>
        public static string UserCredennumSelByID(int iUID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUserInfoVal = dbUCenter.GetStoredProcCommand("SP_UserCredennum_SelByID");

            dbUCenter.AddInParameter(dcUserInfoVal, "@userid", DbType.String, iUID);

            IDataReader drUserInfo = dbUCenter.ExecuteReader(dcUserInfoVal);
            string sCredennum = string.Empty;

            if (drUserInfo.Read())
            {
                sCredennum = drUserInfo["credennum"].ToString();
            }
            drUserInfo.Close();
            return sCredennum;
        }

        /// <summary>
        /// 用户密报问题查询
        /// </summary>
        /// <returns></returns>
        public static string UserQuestionSelByID(int iUID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUserInfoVal = dbUCenter.GetStoredProcCommand("UserInfo_QuestionSelByUID");

            dbUCenter.AddInParameter(dcUserInfoVal, "@userid", DbType.String, iUID);

            IDataReader drUserInfo = dbUCenter.ExecuteReader(dcUserInfoVal);
            string sEmail = string.Empty;

            if (drUserInfo.Read())
            {
                sEmail = drUserInfo[0].ToString();
            }
            drUserInfo.Close();
            return sEmail;
        }
        #endregion

        //新写的封装方法
        public static int getn(string cunchuName, params SqlParameter[] canshu)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUserInfoUpdate = dbUCenter.GetStoredProcCommand(cunchuName);
            for (int i = 0; i < canshu.Length; i++)
            {
                dcUserInfoUpdate.Parameters.Add(canshu[i]);
            }
            return dbUCenter.ExecuteNonQuery(dcUserInfoUpdate);
        }

        public static int UserInfoUpdateOfQuestion(string sQuestion, string sAnswer, int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserInfoUpdate = dbUCenter.GetStoredProcCommand("UserInfo_AddQuestion");

            dbUCenter.AddInParameter(dcUserInfoUpdate, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcUserInfoUpdate, "@question", DbType.String, sQuestion);
            dbUCenter.AddInParameter(dcUserInfoUpdate, "@answer", DbType.String, sAnswer);

            return dbUCenter.ExecuteNonQuery(dcUserInfoUpdate);
        }
    }
}

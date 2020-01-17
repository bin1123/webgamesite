using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class UserDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        #region 用户的增删改查
        /// <summary>
        /// 用户基本信息添加
        /// </summary>
        /// <param name="uObejct">用户对象</param>
        /// <returns>用户id</returns>
        public static int UserAdd(User uObject)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserAdd = dbUCenter.GetStoredProcCommand("SP_User_Add");
            
            dbUCenter.AddOutParameter(dcUserAdd,"@userid",DbType.Int32,4);
            dbUCenter.AddInParameter(dcUserAdd, "@account", DbType.String,uObject.account);
            dbUCenter.AddInParameter(dcUserAdd, "@password", DbType.String,uObject.password);
            dbUCenter.AddInParameter(dcUserAdd, "@typeid",DbType.Int16,uObject.typeid);
            dbUCenter.AddInParameter(dcUserAdd, "@state",DbType.Int16,uObject.state);

            dbUCenter.ExecuteNonQuery(dcUserAdd);
            int iUserID = 0;
            int.TryParse(dbUCenter.GetParameterValue(dcUserAdd, "@userid").ToString(), out iUserID);
            dcUserAdd.Dispose();
            return iUserID;
         }

        public static int UserDel(int iUID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserDel = dbUCenter.GetStoredProcCommand("SP_User_DelByID");

            dbUCenter.AddInParameter(dcUserDel, "@userid", DbType.Int32,iUID);

            return dbUCenter.ExecuteNonQuery(dcUserDel);             
        }

        public static int UserUpdatePWD(int iUserID,string sPassWord)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserUpdate = dbUCenter.GetStoredProcCommand("SP_User_UpdateByID");

            dbUCenter.AddInParameter(dcUserUpdate, "@userid",DbType.Int32,iUserID);
            dbUCenter.AddInParameter(dcUserUpdate, "@password", DbType.String, sPassWord);

            return dbUCenter.ExecuteNonQuery(dcUserUpdate);
        }

        /// <summary>
        /// 用户验证
        /// </summary>
        /// <returns></returns>
        public static string UserVal(string sAccount, string sPassWord)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUserVal = dbUCenter.GetStoredProcCommand("SP_User_SelectOfVal");

            dbUCenter.AddInParameter(dcUserVal, "@account", DbType.String, sAccount);
            dbUCenter.AddInParameter(dcUserVal, "@password", DbType.String, sPassWord);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUserVal);
            string sState = string.Empty;
            if (drUser.Read())
            {
                sState = drUser["state"].ToString();
            }
            //else
            //{
            //    sState = UserValFromWriteDB(sAccount,sPassWord);
            //}
            drUser.Close();
            dcUserVal.Dispose();
            return sState;
        }

        /// <summary>
        /// 用户验证从写库
        /// </summary>
        /// <returns></returns>
        public static string UserValFromWriteDB(string sAccount, string sPassWord)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserVal = dbUCenter.GetStoredProcCommand("SP_User_SelectOfVal");

            dbUCenter.AddInParameter(dcUserVal, "@account", DbType.String, sAccount);
            dbUCenter.AddInParameter(dcUserVal, "@password", DbType.String, sPassWord);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUserVal);
            string sState = string.Empty;
            if (drUser.Read())
            {
                sState = drUser["state"].ToString();
            }
            drUser.Close();
            dcUserVal.Dispose();
            return sState;
        }

        public static int UserIDSel(string sAccount)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("SP_User_UIDSelByAccount");

            dbUCenter.AddInParameter(dcUser, "@account", DbType.String, sAccount);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            int iUserID = 0;
            if (drUser.Read())
            {
                int.TryParse(drUser["userid"].ToString(), out iUserID);
            }
            //else
            //{ 
            //    iUserID = UserIDSelFromWriteDB(sAccount);
            //}
            drUser.Close();
            dcUser.Dispose();
            return iUserID;
        }

        public static int UserIDSelFromWriteDB(string sAccount)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("SP_User_UIDSelByAccount");

            dbUCenter.AddInParameter(dcUser, "@account", DbType.String, sAccount);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            int iUserID = 0;
            if (drUser.Read())
            {
                int.TryParse(drUser["userid"].ToString(), out iUserID);
            }
            drUser.Close();
            dcUser.Dispose();
            return iUserID;
        }

        public static int UserIDSelByType(string sAccount,int iType)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("SP_User_UIDSelByType");

            dbUCenter.AddInParameter(dcUser, "@account", DbType.String, sAccount);
            dbUCenter.AddInParameter(dcUser, "@typeid", DbType.Int32, iType);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            int iUserID = 0;
            if (drUser.Read())
            {
                int.TryParse(drUser["userid"].ToString(), out iUserID);
            }
            drUser.Close();
            dcUser.Dispose();
            return iUserID;
        }

        public static string AccountSel(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("SP_User_AccountSelByID");

            dbUCenter.AddInParameter(dcUser,"@UserID",DbType.Int32,iUserID);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            string sAccount = string.Empty;
            if (drUser.Read())
            {
                sAccount = drUser["account"].ToString();
            }
            //else
            //{
            //    sAccount = AccountSelFromWriteDB(iUserID);
            //}
            drUser.Close();
            dcUser.Dispose();
            return sAccount;
        }

        public static string AccountSelFromWriteDB(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("SP_User_AccountSelByID");

            dbUCenter.AddInParameter(dcUser, "@UserID", DbType.Int32, iUserID);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            string sAccount = string.Empty;
            if (drUser.Read())
            {
                sAccount = drUser["account"].ToString();
            }
            drUser.Close();
            dcUser.Dispose();
            return sAccount;
        }

        /// <summary>
        /// 验证通行证是否存在
        /// </summary>
        /// <param name="sAccount"></param>
        /// <returns></returns>
        public static int AccountVal(string sAccount)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("SP_User_AccountVal");

            dbUCenter.AddInParameter(dcUser, "@account", DbType.String, sAccount);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            int iRes = -1;
            if (drUser.Read())
            {
                bool bReS = false;
                bReS = int.TryParse(drUser[0].ToString(), out iRes);
                if (!bReS)
                {
                    iRes = -1;
                }
            }
            drUser.Close();
            dcUser.Dispose();
            return iRes;
        }

        public static int AccountValFromWriteDB(string sAccount)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("SP_User_AccountVal");

            dbUCenter.AddInParameter(dcUser, "@account", DbType.String, sAccount);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            int iRes = -1;
            if (drUser.Read())
            {
                bool bReS = false;
                bReS = int.TryParse(drUser[0].ToString(), out iRes);
                if (!bReS)
                {
                    iRes = -1;
                }
            }
            drUser.Close();
            dcUser.Dispose();
            return iRes;
        }

        public static int PWDVal(int iUserID,string sPassWord)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("User_PWDVal");

            dbUCenter.AddInParameter(dcUser, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcUser, "@password", DbType.String, sPassWord);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            int iRes = -1;

            if (drUser.Read())
            {
                int.TryParse(drUser[0].ToString(),out iRes);
            }
            drUser.Close();
            dcUser.Dispose();
            return iRes;
        }

        public static int PWDValFromWriteDB(int iUserID, string sPassWord)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("User_PWDVal");

            dbUCenter.AddInParameter(dcUser, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcUser, "@password", DbType.String, sPassWord);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            int iRes = -1;

            if (drUser.Read())
            {
                int.TryParse(drUser[0].ToString(), out iRes);
            }
            drUser.Close();
            dcUser.Dispose();
            return iRes;
        }

        public static string RegTimeSel(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("SP_User_RegTimeSelByID");

            dbUCenter.AddInParameter(dcUser, "@UserID", DbType.Int32, iUserID);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            string sTime = string.Empty;
            if (drUser.Read())
            {
                sTime = drUser["dtime"].ToString();
            }
            else
            {
                sTime = DateTime.Now.ToString();
            }
            drUser.Close();
            dcUser.Dispose();
            return sTime;
        }

        public static int UserUpdateNamePWD(int iUserID,string sAccount, string sPassWord)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserUpdate = dbUCenter.GetStoredProcCommand("User_UpdateNamePWDByID");

            dbUCenter.AddInParameter(dcUserUpdate, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcUserUpdate, "@account", DbType.String, sAccount);
            dbUCenter.AddInParameter(dcUserUpdate, "@password", DbType.String, sPassWord);

            return dbUCenter.ExecuteNonQuery(dcUserUpdate);
        }

        public static string RegStateSel(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("User_StateSelect");

            dbUCenter.AddInParameter(dcUser, "@UserID", DbType.Int32, iUserID);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            string sState = string.Empty;
            if (drUser.Read())
            {
                sState = drUser[0].ToString();
            }
            drUser.Close();
            dcUser.Dispose();
            return sState;
        }

        public static string UserTypeSel(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUser = dbUCenter.GetStoredProcCommand("User_TypeSel");

            dbUCenter.AddInParameter(dcUser, "@UserID", DbType.Int32, iUserID);

            IDataReader drUser = dbUCenter.ExecuteReader(dcUser);
            string sType = string.Empty;
            if (drUser.Read())
            {
                sType = drUser[0].ToString();
            }
            drUser.Close();
            dcUser.Dispose();
            return sType;
        }
        #endregion
    }
}

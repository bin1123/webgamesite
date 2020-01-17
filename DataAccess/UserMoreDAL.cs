using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class UserMoreDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static UserMore UserMoreBind(IDataReader drUserMore)
        {
            UserMore umObject = new UserMore();
            if (drUserMore.Read())
            {
                umObject.birthday = drUserMore["birthday"].ToString();
                umObject.nickname = drUserMore["nickname"].ToString();
                umObject.phone = drUserMore["phone"].ToString();
                umObject.qq = drUserMore["qq"].ToString();
                umObject.work = drUserMore["work"].ToString();
                umObject.sex = drUserMore["sex"].ToString();               
            }
            else
            {
                umObject.birthday = "1970-01-01";
                umObject.nickname = "";
                umObject.phone = "";
                umObject.qq = "";
                umObject.work = "";
                umObject.sex = "0";
            }
            drUserMore.Close();
            return umObject;
        }

        #region 用户的增删改查
        /// <summary>
        /// 用户基本信息添加
        /// </summary>
        /// <param name="uObejct">用户对象</param>
        /// <returns>用户id</returns>
        public static int UserMoreAdd(UserMore umObject)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserMore = dbUCenter.GetStoredProcCommand("UserMore_Add");

            dbUCenter.AddInParameter(dcUserMore, "@userid", DbType.Int32, umObject.userid);
            dbUCenter.AddInParameter(dcUserMore, "@nickname", DbType.String, umObject.nickname);
            dbUCenter.AddInParameter(dcUserMore, "@birthday", DbType.String, umObject.birthday);
            dbUCenter.AddInParameter(dcUserMore, "@work", DbType.String, umObject.work);
            dbUCenter.AddInParameter(dcUserMore, "@phone", DbType.String, umObject.phone);
            dbUCenter.AddInParameter(dcUserMore, "@qq", DbType.String, umObject.qq);
            dbUCenter.AddInParameter(dcUserMore, "@sex", DbType.String, umObject.sex);

            return dbUCenter.ExecuteNonQuery(dcUserMore);
        }

        public static int UserMoreDel(int iUID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserMoreDel = dbUCenter.GetStoredProcCommand("UserMore_DelByID");

            dbUCenter.AddInParameter(dcUserMoreDel, "@userid", DbType.Int32, iUID);

            return dbUCenter.ExecuteNonQuery(dcUserMoreDel);
        }

        public static int UserMoreUpdate(UserMore umObject)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserMore = dbUCenter.GetStoredProcCommand("UserMore_UpdateByID");

            dbUCenter.AddInParameter(dcUserMore, "@userid", DbType.Int32, umObject.userid);
            dbUCenter.AddInParameter(dcUserMore, "@nickname", DbType.String, umObject.nickname);
            dbUCenter.AddInParameter(dcUserMore, "@birthday", DbType.String, umObject.birthday);
            dbUCenter.AddInParameter(dcUserMore, "@work", DbType.String, umObject.work);
            dbUCenter.AddInParameter(dcUserMore, "@phone", DbType.String, umObject.phone);
            dbUCenter.AddInParameter(dcUserMore, "@qq", DbType.String, umObject.qq);
            dbUCenter.AddInParameter(dcUserMore, "@sex", DbType.String, umObject.sex);

            return dbUCenter.ExecuteNonQuery(dcUserMore);
        }

        /// <summary>
        /// 用户信息查询
        /// </summary>
        /// <returns></returns>
        public static UserMore UserMoreSelByID(int iUID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcUserMoreVal = dbUCenter.GetStoredProcCommand("UserMore_SelByID");

            dbUCenter.AddInParameter(dcUserMoreVal, "@userid", DbType.Int32, iUID);

            return UserMoreBind(dbUCenter.ExecuteReader(dcUserMoreVal));
        }
        #endregion
    }
}

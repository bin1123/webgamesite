using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class TranQuickDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        #region 增删改查
        /// <summary>
        /// 用户基本信息添加
        /// </summary>
        /// <param name="uObejct">用户对象</param>
        /// <returns>用户id</returns>
        public static int TranQuickAdd(TranQuick tqObject)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTranQuick = dbUCenter.GetStoredProcCommand("SP_TransQuick_Ins");

            dbUCenter.AddInParameter(dcTranQuick, "@ptranid", DbType.String, tqObject.PTranID);
            dbUCenter.AddInParameter(dcTranQuick, "@gtranid", DbType.String, tqObject.GTranID);

            return dbUCenter.ExecuteNonQuery(dcTranQuick);
        }

        public static int TranQuickUpdateP(string sPTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTranQuick = dbUCenter.GetStoredProcCommand("SP_TransQuick_PUpdate");

            dbUCenter.AddInParameter(dcTranQuick, "@ptranid", DbType.String, sPTranID);

            return dbUCenter.ExecuteNonQuery(dcTranQuick);
        }

        public static int TranQuickUpdateG(string sGTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTranQuick = dbUCenter.GetStoredProcCommand("SP_TransQuick_GUpdate");

            dbUCenter.AddInParameter(dcTranQuick, "@gtranid", DbType.String, sGTranID);

            return dbUCenter.ExecuteNonQuery(dcTranQuick);
        }

        public static string TranQuickGTranIDSel(string sPTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTranQuick = dbUCenter.GetStoredProcCommand("SP_TransQuick_GTranIDSel");

            dbUCenter.AddInParameter(dcTranQuick, "@ptranid", DbType.String, sPTranID);

            IDataReader drTranQuick = dbUCenter.ExecuteReader(dcTranQuick);
            string sGTranID = string.Empty;
            if(drTranQuick.Read())
            {
                sGTranID = drTranQuick["gtranid"].ToString();
            }
            drTranQuick.Close();
            drTranQuick.Dispose();
            return sGTranID;
        }

        public static string TransQuickStateSelByP(string sPTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTranQuick = dbUCenter.GetStoredProcCommand("SP_TransQuick_StateSelByP");

            dbUCenter.AddInParameter(dcTranQuick, "@ptranid", DbType.String, sPTranID);

            IDataReader drTranQuick = dbUCenter.ExecuteReader(dcTranQuick);
            string sGTranID = string.Empty;
            if (drTranQuick.Read())
            {
                sGTranID = drTranQuick["state"].ToString();
            }
            drTranQuick.Close();
            drTranQuick.Dispose();
            return sGTranID;
        }

        public static IDataReader TransQuickLogSel(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcTranQuick = dbUCenter.GetStoredProcCommand("SP_TransQuick_LogSel");

            dbUCenter.AddInParameter(dcTranQuick, "@userid", DbType.Int32, iUserID);

            IDataReader drTranQuick = dbUCenter.ExecuteReader(dcTranQuick);
            return drTranQuick;
        }
        #endregion
    }
}

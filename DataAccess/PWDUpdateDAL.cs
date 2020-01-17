using System.Text;
using System.Data;
using System.Data.Common;
using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class PWDUpdateDAL
    {
        private const string sConn = "datacenter";
        private const string sReadConn = "datacenterread";

        public static int PwdUpdateAdd(int userid,string sIP)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcPwdUpdate = dbDCenter.GetStoredProcCommand("pwdupdate_add");

            dbDCenter.AddInParameter(dcPwdUpdate, "@userid", DbType.Int32, userid);
            dbDCenter.AddInParameter(dcPwdUpdate, "@ip", DbType.String, sIP);
            dbDCenter.AddInParameter(dcPwdUpdate, "@datetime", DbType.DateTime, DateTime.Now);

            return dbDCenter.ExecuteNonQuery(dcPwdUpdate);
        }

        public static DateTime PwdUpdateLastSel(int iUserID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sReadConn);
            DbCommand dcPwdUpdate = dbDCenter.GetStoredProcCommand("pwdupdate_LTimeSel");

            dbDCenter.AddInParameter(dcPwdUpdate, "@userid", DbType.Int32, iUserID);

            IDataReader drPwdUpdate = dbDCenter.ExecuteReader(dcPwdUpdate);
            DateTime dtTime = new DateTime();
            if (drPwdUpdate.Read())
            {
                DateTime.TryParse(drPwdUpdate[0].ToString(), out dtTime);
            }
            drPwdUpdate.Close();
            drPwdUpdate.Dispose();
            return dtTime;
        }
    }
}

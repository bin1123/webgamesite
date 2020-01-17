using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class CodeHDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static int CodeHAdd(CodeH chObject)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcCodeH = dbUCenter.GetStoredProcCommand("SP_CodeH_Add");

            dbUCenter.AddOutParameter(dcCodeH, "@userid", DbType.Int32, chObject.UserID);
            dbUCenter.AddInParameter(dcCodeH, "@codeid", DbType.Int32, chObject.CodeID);
            dbUCenter.AddInParameter(dcCodeH, "@ip", DbType.String, chObject.UserIP);

            return dbUCenter.ExecuteNonQuery(dcCodeH);
        }
    }
}

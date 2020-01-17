using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class LastOfPayPointDAL
    {
        private const string sConn = "datacenter";
        private const string sReadConn = "datacenterread";

        public static int Add(LastOfPayPoint lpObject)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcLastOfPayPoint = dbDCenter.GetStoredProcCommand("SP_LastOfPayP_Add");

            dbDCenter.AddInParameter(dcLastOfPayPoint, "@tranfrom", DbType.String, lpObject.TranFrom);
            dbDCenter.AddInParameter(dcLastOfPayPoint, "@tranip", DbType.String, lpObject.TranIP);
            dbDCenter.AddInParameter(dcLastOfPayPoint, "@fromurl", DbType.String, lpObject.FromUrl);
            dbDCenter.AddInParameter(dcLastOfPayPoint, "@tranid", DbType.String, lpObject.TranID);

            return dbDCenter.ExecuteNonQuery(dcLastOfPayPoint);
        }
    }
}

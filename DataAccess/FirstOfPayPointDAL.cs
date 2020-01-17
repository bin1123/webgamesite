using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class FirstOfPayPointDAL
    {
        private const string sConn = "datacenter";
        private const string sReadConn = "datacenterread";

        public static int Add(FirstOfPayPoint fpObject)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcFirstOfPayPoint = dbDCenter.GetStoredProcCommand("SP_FirstOfPayP_Add");

            dbDCenter.AddInParameter(dcFirstOfPayPoint, "@tranip", DbType.String, fpObject.TranIP);
            dbDCenter.AddInParameter(dcFirstOfPayPoint, "@tranurl", DbType.String, fpObject.TranUrl);
            dbDCenter.AddInParameter(dcFirstOfPayPoint, "@tranid", DbType.String, fpObject.TranID);
            return dbDCenter.ExecuteNonQuery(dcFirstOfPayPoint);
        }
    }
}

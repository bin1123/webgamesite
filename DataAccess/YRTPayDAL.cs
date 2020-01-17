using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class YRTPayDAL
    {
        private const string sConn = "datacenter";

        public static int Add(YRTPay yrtObject)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcFirstOfPayPoint = dbDCenter.GetStoredProcCommand("YRTPay_Add");

            dbDCenter.AddInParameter(dcFirstOfPayPoint, "@tid", DbType.String, yrtObject.TID);
            dbDCenter.AddInParameter(dcFirstOfPayPoint, "@trantime", DbType.String, yrtObject.TranTime);
            dbDCenter.AddInParameter(dcFirstOfPayPoint, "@point", DbType.Int32, yrtObject.Point);
            dbDCenter.AddInParameter(dcFirstOfPayPoint, "@tranip", DbType.String, yrtObject.TranIP);
            dbDCenter.AddInParameter(dcFirstOfPayPoint, "@userid", DbType.Int32, yrtObject.UserID);
            dbDCenter.AddInParameter(dcFirstOfPayPoint, "@tranid", DbType.String, yrtObject.TranID);
            dbDCenter.AddInParameter(dcFirstOfPayPoint, "@offername", DbType.String, yrtObject.OfferName);
            return dbDCenter.ExecuteNonQuery(dcFirstOfPayPoint);
        }
    }
}

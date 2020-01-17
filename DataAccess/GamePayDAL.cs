using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class GamePayDAL
    {
        private const string sConn = "datacenter";
        private const string sReadConn = "datacenterread";

        public static int GamePayAdd(GamePay gpObject)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUrLPay = dbDCenter.GetStoredProcCommand("SP_GamePay_Add");

            dbDCenter.AddInParameter(dcUrLPay, "@tranip", DbType.String, gpObject.TranIP);
            dbDCenter.AddInParameter(dcUrLPay, "@tranid", DbType.String, gpObject.TranID);
            dbDCenter.AddInParameter(dcUrLPay, "@tranurl", DbType.String, gpObject.TranUrl);
            dbDCenter.AddInParameter(dcUrLPay, "@tranreturn", DbType.String, gpObject.TranReturn);
            dbDCenter.AddInParameter(dcUrLPay, "@gameabbre", DbType.String, gpObject.GameAbbre);
            dbDCenter.AddInParameter(dcUrLPay, "@userid", DbType.String, gpObject.UserID);

            return dbDCenter.ExecuteNonQuery(dcUrLPay);
        }
    }
}

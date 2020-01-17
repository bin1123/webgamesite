using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class CenterLoginTodayDAL
    {
        private const string sConn = "datacenter";
        private const string sReadConn = "datacenterread";

        public static int CenterLoginAdd(int iUserID,string sUserIP)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcCenterLogin = dbDCenter.GetStoredProcCommand("CenterLoginToday_Add");

            dbDCenter.AddInParameter(dcCenterLogin, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcCenterLogin, "@loginip", DbType.String, sUserIP);

            return dbDCenter.ExecuteNonQuery(dcCenterLogin);
        }
    }
}

using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class GameLoginTodayDAL
    {
        private const string sConn = "datacenter";
        private const string sReadConn = "datacenterread";

        public static int GameLoginAdd(int iUserID,string sUserIP,string sGameAbbre)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcCenterLogin = dbDCenter.GetStoredProcCommand("GameLoginToday_Add");

            dbDCenter.AddInParameter(dcCenterLogin, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcCenterLogin, "@loginip", DbType.String, sUserIP);
            dbDCenter.AddInParameter(dcCenterLogin, "@gameabbre", DbType.String, sGameAbbre);

            return dbDCenter.ExecuteNonQuery(dcCenterLogin);
        }
    }
}

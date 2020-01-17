using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class QQUserDAL
    {
        private const string sConn = "datacenter";

        public static int QQUserAdd(int iUserID, string sOpenID, string sFromUrl)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("qquser_Add");

            dbDCenter.AddInParameter(dcGameLogin, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcGameLogin, "@openid", DbType.String, sOpenID);
            dbDCenter.AddInParameter(dcGameLogin, "@fromurl", DbType.String, sFromUrl);

            return dbDCenter.ExecuteNonQuery(dcGameLogin);
        }

        public static string QQUserUseridSelByOpenID(string sOpenID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcServer = dbDCenter.GetStoredProcCommand("qquser_UserIDSelByOpenID");

            dbDCenter.AddInParameter(dcServer, "@openid", DbType.String, sOpenID);

            IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
            string unionid = string.Empty;
            if (drServer.Read())
            {
                unionid = drServer[0].ToString();
            }
            drServer.Close();
            return unionid;
        }
    }
}

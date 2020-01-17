using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class NoRegLoginDAL
    {
        private const string sConn = "datacenter";
        private const string sReadConn = "datacenterread";

        public static int NoRegLoginAdd(int iUserID,string sUnionID,string sGameName)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("NoRegLogin_Add");

            dbDCenter.AddInParameter(dcGameLogin, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcGameLogin, "@gamename", DbType.String, sGameName);
            dbDCenter.AddInParameter(dcGameLogin, "@unionid", DbType.String, sUnionID);

            return dbDCenter.ExecuteNonQuery(dcGameLogin);
        }

        public static string NoRegLoginUnionidSel(string sUnionID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcServer = dbDCenter.GetStoredProcCommand("NoRegLogin_UnionidSel");

            dbDCenter.AddInParameter(dcServer, "@unionid", DbType.String, sUnionID);

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

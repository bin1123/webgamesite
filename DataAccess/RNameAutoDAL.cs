using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class RNameAutoDAL
    {
        private const string sConn = "datacenter";

        public static int RNameAutoAdd(string sName, int iNum)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("rnameauto_Add");
            dbDCenter.AddInParameter(dcGameLogin, "@rname", DbType.String, sName);
            dbDCenter.AddInParameter(dcGameLogin, "@num", DbType.Int32, iNum);

            return dbDCenter.ExecuteNonQuery(dcGameLogin);
        }

        public static int NumSelByName(string sRName)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcServer = dbDCenter.GetStoredProcCommand("rnameauto_NumSelByName");

            dbDCenter.AddInParameter(dcServer, "@name", DbType.String, sRName);

            IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
            int iNum = 0;
            if (drServer.Read())
            {
                int.TryParse(drServer[0].ToString(), out iNum);
            }
            drServer.Close();
            return iNum;
        }
    }
}

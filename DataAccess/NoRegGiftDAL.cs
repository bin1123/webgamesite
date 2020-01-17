using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class NoRegGiftDAL
    {
        private const string sConn = "datacenter";

        public static int NoRegGiftAdd(int iUserID,int iLevel,string sGameName)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("NoRegGift_Add");

            dbDCenter.AddInParameter(dcGameLogin, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcGameLogin, "@level", DbType.Int32, iLevel);
            dbDCenter.AddInParameter(dcGameLogin, "@gamename", DbType.String, sGameName);

            return dbDCenter.ExecuteNonQuery(dcGameLogin);
        }

        public static string NoRegGiftUseridSel(int iUserID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcServer = dbDCenter.GetStoredProcCommand("NoRegGift_UserIDSel");

            dbDCenter.AddInParameter(dcServer, "@userid", DbType.Int32, iUserID);

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

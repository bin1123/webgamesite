using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class FirstGiftDAL
    {
        private const string sConn = "datacenter";

        public static int GiftAdd(int iUserID,int iLevel,string sGameName)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("FirstGift_Add");

            dbDCenter.AddInParameter(dcGameLogin, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcGameLogin, "@level", DbType.Int32, iLevel);
            dbDCenter.AddInParameter(dcGameLogin, "@gamename", DbType.String, sGameName);

            return dbDCenter.ExecuteNonQuery(dcGameLogin);
        }

        public static string GiftUserIDSel(int iUserID,string sGameName)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcServer = dbDCenter.GetStoredProcCommand("FirstGift_UserIDSel");

            dbDCenter.AddInParameter(dcServer, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcServer, "@gamename", DbType.String, sGameName);

            IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
            string sUserID = string.Empty;
            if (drServer.Read())
            {
                sUserID = drServer[0].ToString();
            }
            drServer.Close();
            return sUserID;
        }

        public static string GiftStateSel(int iUserID, string sGameName)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcServer = dbDCenter.GetStoredProcCommand("FirstGift_StateSel");

            dbDCenter.AddInParameter(dcServer, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcServer, "@gamename", DbType.String, sGameName);

            IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
            string sState = string.Empty;
            if (drServer.Read())
            {
                sState = drServer[0].ToString();
            }
            drServer.Close();
            return sState;
        }

        public static int GiftStateUpate(int iUserID, string sGameName,int iState)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGameGift = dbDCenter.GetStoredProcCommand("FirstGift_StateUpdate");

            dbDCenter.AddInParameter(dcGameGift, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcGameGift, "@gamename", DbType.String, sGameName);
            dbDCenter.AddInParameter(dcGameGift, "@state", DbType.Int16, iState);

            return dbDCenter.ExecuteNonQuery(dcGameGift);
        }
    }
}

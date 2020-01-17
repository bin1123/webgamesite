using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class GameGiftDAL
    {
        private const string sConn = "datacenter";

        public static int GameGiftAdd(int iServerID, int iUserID,string sGift,string sGameName,string sGiftThing,string sGiftTranID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGamePaySuc = dbDCenter.GetStoredProcCommand("GameGift_Add");

            dbDCenter.AddInParameter(dcGamePaySuc, "@gameabbre", DbType.String, sGameName);
            dbDCenter.AddInParameter(dcGamePaySuc, "@gift", DbType.String, sGift);
            dbDCenter.AddInParameter(dcGamePaySuc, "@serverid", DbType.Int32, iServerID);
            dbDCenter.AddInParameter(dcGamePaySuc, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcGamePaySuc, "@giftthing", DbType.String, sGiftThing);
            dbDCenter.AddInParameter(dcGamePaySuc, "@gifttranid", DbType.String, sGiftTranID);

            return dbDCenter.ExecuteNonQuery(dcGamePaySuc);
        }

        public static int UCountSelByGift(int iUserID, string sGift)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGamePaySuc = dbDCenter.GetStoredProcCommand("GameGift_UCountSelByGift");

            dbDCenter.AddInParameter(dcGamePaySuc, "@userid", DbType.String, iUserID);
            dbDCenter.AddInParameter(dcGamePaySuc, "@gift", DbType.String, sGift);

            IDataReader drServer = dbDCenter.ExecuteReader(dcGamePaySuc);
            int iCount = 0;
            if (drServer.Read())
            {
                int.TryParse(drServer[0].ToString(),out iCount);
            }
            drServer.Close();
            return iCount;
        }
    }
}

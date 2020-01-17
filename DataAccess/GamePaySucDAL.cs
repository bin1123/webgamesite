using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class GamePaySucDAL
    {
        private const string sConn = "datacenter";

        public static int GamePaySucAdd(int payuserid, int guserid, int point,string sTranID,string sGameName,int iServerID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGamePaySuc = dbDCenter.GetStoredProcCommand("GamePaySuc_Add");

            dbDCenter.AddInParameter(dcGamePaySuc, "@payuserid", DbType.Int32, payuserid);
            dbDCenter.AddInParameter(dcGamePaySuc, "@guserid", DbType.Int32, guserid);
            dbDCenter.AddInParameter(dcGamePaySuc, "@point", DbType.Int32, point);
            dbDCenter.AddInParameter(dcGamePaySuc, "@tranid", DbType.String, sTranID);
            dbDCenter.AddInParameter(dcGamePaySuc, "@gameabbre", DbType.String, sGameName);
            dbDCenter.AddInParameter(dcGamePaySuc, "@serverid", DbType.Int32, iServerID);

            return dbDCenter.ExecuteNonQuery(dcGamePaySuc);
        }

        public static int UPointSelByGNTime(string sGameAbbre, int iPayUserID, DateTime dtBeginTime,DateTime dtEndTime)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGamePaySuc = dbDCenter.GetStoredProcCommand("GamePaySuc_UPointSelByGNTime");

            dbDCenter.AddInParameter(dcGamePaySuc, "@gameabbre", DbType.String, sGameAbbre);
            dbDCenter.AddInParameter(dcGamePaySuc, "@payuserid", DbType.Int32, iPayUserID);
            dbDCenter.AddInParameter(dcGamePaySuc, "@begintime", DbType.DateTime, dtBeginTime);
            dbDCenter.AddInParameter(dcGamePaySuc, "@endtime", DbType.DateTime, dtEndTime);

            IDataReader drServer = dbDCenter.ExecuteReader(dcGamePaySuc);
            int iPoint = 0;
            if (drServer.Read())
            {
                int.TryParse(drServer[0].ToString(),out iPoint);
            }
            drServer.Close();
            return iPoint;
        }
    }
}

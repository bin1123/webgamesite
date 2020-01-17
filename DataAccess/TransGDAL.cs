using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class TransGDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static TransG TranDataBind(IDataReader drTran)
        {
            TransG tObject = new TransG();
            if(drTran.Read())
            {
                tObject.GameID = null == drTran["gameid"] ? 0 : Convert.ToInt32(drTran["gameid"]);
                tObject.GamePoints = null == drTran["gamepoints"] ? 0 : Convert.ToInt32(drTran["gamepoints"]);
                tObject.state = null == drTran["state"] ? 0 : Convert.ToInt32(drTran["state"]);
                tObject.TranGiftPoints = null == drTran["GiftPoints"] ? 0 : Convert.ToInt32(drTran["GiftPoints"]);
                tObject.TranPoints = null == drTran["Points"] ? 0 : Convert.ToInt32(drTran["Points"]);
                tObject.TranTime = null == drTran["time"] ? DateTime.Now : Convert.ToDateTime(drTran["time"]);
                tObject.UserID = null == drTran["userid"] ? 0 : Convert.ToInt32(drTran["userid"]);
                tObject.GUserID = null == drTran["userid"] ? 0 : Convert.ToInt32(drTran["guserid"]);
                tObject.TranID = null == drTran["id"] ? string.Empty : drTran["id"].ToString();
                tObject.TranIP = null == drTran["TranIP"] ? string.Empty : drTran["TranIP"].ToString();
                tObject.Phone = null == drTran["phone"] ? string.Empty : drTran["phone"].ToString();
                drTran.Close();
            }
            return tObject;
        }

        public static int TransSelGPointsByGame(int iGameID,DateTime dBeginDate,DateTime dEndDate)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranG_SelGPointsByGame");

            dbUCenter.AddInParameter(dcTran, "@gameid", DbType.Int32, iGameID);
            dbUCenter.AddInParameter(dcTran, "@begindate", DbType.DateTime,dBeginDate);
            dbUCenter.AddInParameter(dcTran, "@enddate", DbType.DateTime,dEndDate);

            IDataReader drReader = dbUCenter.ExecuteReader(dcTran);

            int iGamePoints = 0;
            if (drReader.Read())
            {
                int.TryParse(drReader[0].ToString(), out iGamePoints);
            }
            drReader.Close();
            dcTran.Dispose();
            return iGamePoints;
        }

        public static int TransSelGPointsByID(string sTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranG_SelGPointsByTID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            IDataReader drReader = dbUCenter.ExecuteReader(dcTran);

            int iGamePoints = 0;
            if (drReader.Read())
            {
                int.TryParse(drReader[0].ToString(),out iGamePoints);
            }
            drReader.Close();
            dcTran.Dispose();
            return iGamePoints;
        }

        public static int TransSelPointsByID(string sTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranG_SelPointsByTID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            IDataReader drReader = dbUCenter.ExecuteReader(dcTran);

            int iGamePoints = 0;
            if (drReader.Read())
            {
                int.TryParse(drReader[0].ToString(), out iGamePoints);
            }
            drReader.Close();
            dcTran.Dispose();
            return iGamePoints;
        }

        public static int TransSelGPointsByUser(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranG_SelGPointsByUser");

            dbUCenter.AddInParameter(dcTran, "@userid", DbType.Int32,iUserID);

            IDataReader drReader = dbUCenter.ExecuteReader(dcTran);

            int iGamePoints = 1;
            if (drReader.Read())
            {
                int.TryParse(drReader[0].ToString(), out iGamePoints);
            }
            drReader.Close();
            dcTran.Dispose();
            return iGamePoints;
        }

        public static string TransSelTimeByID(string sTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranG_SelTimeByTID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            IDataReader drReader = dbUCenter.ExecuteReader(dcTran);

            string sTime = string.Empty;
            if (drReader.Read())
            {
                sTime = drReader[0].ToString();
            }
            drReader.Close();
            dcTran.Dispose();
            return sTime;
        }

        public static int TransSelStateByID(string sTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("TranG_SelStateByID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            IDataReader drReader = dbUCenter.ExecuteReader(dcTran);

            int iState = -1;
            if (drReader.Read())
            {
                int.TryParse(drReader[0].ToString(), out iState);
            }
            drReader.Close();
            dcTran.Dispose();
            return iState;
        }

        public static int TransSelGUserIDByTID(string sTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranG_SelGUserIDByTID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            IDataReader drReader = dbUCenter.ExecuteReader(dcTran);

            int iGUserID = 0;
            if (drReader.Read())
            {
                int.TryParse(drReader["guserid"].ToString(),out iGUserID);
            }
            drReader.Close();
            dcTran.Dispose();
            return iGUserID;
        }

        public static TransG TransSelectByID(string  sTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranG_SelectByID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            return TranDataBind(dbUCenter.ExecuteReader(dcTran));
        }

        /// <summary>
        /// 查询第一页记录
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNum"></param>
        /// <returns></returns>
        public static IDataReader TransSelOneByUID(int iUserID,int iNum)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranG_SelOneByUID");

            dbUCenter.AddInParameter(dcTran, "@userid", DbType.Int32, iUserID);

            return dbUCenter.ExecuteReader(dcTran);
        }

        public static string GameSalesInit(string sGame, int iGamePoints, string sAccount,string sPhone,int iGUserID,string sTranIP)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_GameCharge_Init");

            dbUCenter.AddOutParameter(dcTran, "@tranid", DbType.String, 40);
            dbUCenter.AddOutParameter(dcTran, "@result", DbType.Int32, 4);
            dbUCenter.AddInParameter(dcTran, "@game", DbType.String, sGame);
            dbUCenter.AddInParameter(dcTran, "@gamepoints", DbType.Int32, iGamePoints);
            dbUCenter.AddInParameter(dcTran, "@phone", DbType.String, sPhone);
            dbUCenter.AddInParameter(dcTran, "@guserid", DbType.Int32, iGUserID);
            dbUCenter.AddInParameter(dcTran, "@account", DbType.String, sAccount);
            dbUCenter.AddInParameter(dcTran, "@tranip", DbType.String, sTranIP);

            dbUCenter.ExecuteNonQuery(dcTran);
            string sRes = dbUCenter.GetParameterValue(dcTran, "@result").ToString();
            if ("0" == sRes)
            {
                sRes = dbUCenter.GetParameterValue(dcTran, "@tranid").ToString();
            }
            dcTran.Dispose();
            return sRes;
        }

        public static string GameSalesGiftInit(string sGame, int iGamePoints, string sAccount)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_GameChargeGift_Init");

            dbUCenter.AddOutParameter(dcTran, "@tranid", DbType.String, 40);
            dbUCenter.AddOutParameter(dcTran, "@result", DbType.Int32, 4);
            dbUCenter.AddInParameter(dcTran, "@game", DbType.String, sGame);
            dbUCenter.AddInParameter(dcTran, "@gamepoints", DbType.Int32, iGamePoints);
            dbUCenter.AddInParameter(dcTran, "@account", DbType.String, sAccount);

            dbUCenter.ExecuteNonQuery(dcTran);
            string sRes = dbUCenter.GetParameterValue(dcTran, "@result").ToString();
            if ("0" == sRes)
            {
                sRes = dbUCenter.GetParameterValue(dcTran, "@tranid").ToString();
            }
            dcTran.Dispose();
            return sRes;
        }

        public static int GaemSalesCommit(string sTranID, string sAccount, string sGameName)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("sp_gamecharge_commit");

            dbUCenter.AddOutParameter(dcTran, "@result", DbType.Int32, 4);
            dbUCenter.AddInParameter(dcTran, "@tranid", DbType.String, sTranID);
            dbUCenter.AddInParameter(dcTran, "@account", DbType.String, sAccount);
            dbUCenter.AddInParameter(dcTran, "@game", DbType.String, sGameName);

            dbUCenter.ExecuteNonQuery(dcTran);
            int iRes = 0; 
            int.TryParse(dbUCenter.GetParameterValue(dcTran, "@result").ToString(), out iRes);
            dcTran.Dispose();
            return iRes;
        }
    }
}

using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class UserPointsDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        /// <summary>
        /// 用户彩游币和赠点查询
        /// </summary>
        /// <param name="iUserID">用户id</param>
        /// <returns></returns>
        public static UserPoints UserPointsSelect(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserPoints = dbUCenter.GetStoredProcCommand("SP_UserPoints_SelectByID");

            dbUCenter.AddInParameter(dcUserPoints, "@UserID", DbType.Int32, iUserID);

            IDataReader drUserPoints = dbUCenter.ExecuteReader(dcUserPoints);
            UserPoints upObject = new UserPoints();
            int iPoints = 0;
            int iGiftPoints = 0;
            upObject.uid = iUserID;
            try
            {
                if (drUserPoints.Read())
                {
                    int.TryParse(drUserPoints["Points"].ToString(), out iPoints);
                    int.TryParse(drUserPoints["giftpoints"].ToString(), out iGiftPoints);
                    upObject.Points = iPoints;
                    upObject.GiftPoints = iGiftPoints;
                }
            }
            catch(Exception e)
            {
            
            }
            finally
            {
                drUserPoints.Close();
                dcUserPoints.Dispose();
            }
            return upObject;
        }

        /// <summary>
        /// 彩游币查询
        /// </summary>
        /// <param name="iUserID">用户id</param>
        /// <returns></returns>
        public static int UserPointSel(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserPoints = dbUCenter.GetStoredProcCommand("SP_UserPoint_SelByID");

            dbUCenter.AddInParameter(dcUserPoints, "@UserID", DbType.Int32, iUserID);

            IDataReader drUserPoints = dbUCenter.ExecuteReader(dcUserPoints);
            UserPoints upObject = new UserPoints();
            int iPoints = 0;
            try
            {
                if (drUserPoints.Read())
                {
                    int.TryParse(drUserPoints["points"].ToString(), out iPoints);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                drUserPoints.Close();
                dcUserPoints.Dispose();
            }
            return iPoints;
        }

        /// <summary>
        /// 赠点查询
        /// </summary>
        /// <param name="iUserID">用户id</param>
        /// <returns></returns>
        public static int UGPointSel(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserPoints = dbUCenter.GetStoredProcCommand("SP_UserGiftPoint_SelByID");

            dbUCenter.AddInParameter(dcUserPoints, "@UserID", DbType.Int32, iUserID);

            IDataReader drUserPoints = dbUCenter.ExecuteReader(dcUserPoints);
            UserPoints upObject = new UserPoints();
            int iGiftPoints = 0;
            try
            {
                if (drUserPoints.Read())
                {
                    int.TryParse(drUserPoints["giftpoints"].ToString(), out iGiftPoints);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                drUserPoints.Close();
                dcUserPoints.Dispose();
            }
            return iGiftPoints;
        }

        public static int UPointUpdate(int iUserID, int iUserPoints)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserUpdate = dbUCenter.GetStoredProcCommand("SP_UserPoints_UpdateByID");

            dbUCenter.AddInParameter(dcUserUpdate, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcUserUpdate, "@points", DbType.Int32, iUserPoints);

            return dbUCenter.ExecuteNonQuery(dcUserUpdate);
        }

        public static int UGiftPointUpdate(int iUserID, int iGiftPoints)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcUserUpdate = dbUCenter.GetStoredProcCommand("UserGiftPoints_UpdateByID");

            dbUCenter.AddInParameter(dcUserUpdate, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcUserUpdate, "@giftpoints", DbType.Int32, iGiftPoints);

            return dbUCenter.ExecuteNonQuery(dcUserUpdate);
        }
    }
}

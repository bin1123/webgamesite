using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class TransPDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static TransP TranDataBind(IDataReader drTran)
        {
            TransP tObject = new TransP();
            if(drTran.Read())
            {
                tObject.ChannelID = null == drTran["channelid"] ? 0 : Convert.ToInt32(drTran["channelid"]);
                tObject.price = null == drTran["price"] ? 0 : Convert.ToDouble(drTran["price"]);
                tObject.state = null == drTran["state"] ? 0 : Convert.ToInt32(drTran["state"]);
                tObject.TranGiftPoints = null == drTran["GiftPoints"] ? 0 : Convert.ToInt32(drTran["GiftPoints"]);
                tObject.TranPoints = null == drTran["Points"] ? 0 : Convert.ToInt32(drTran["Points"]);
                tObject.TranTime = null == drTran["time"] ? DateTime.Now : Convert.ToDateTime(drTran["time"]);
                tObject.UserID = null == drTran["userid"] ? 0 : Convert.ToInt32(drTran["userid"]);
                tObject.TranID = null == drTran["id"] ? string.Empty : drTran["id"].ToString();
                tObject.TranIP = null == drTran["tranip"] ? string.Empty : drTran["tranip"].ToString();
                tObject.phone = null == drTran["phone"] ? string.Empty : drTran["phone"].ToString();
            }
            drTran.Close();
            return tObject;
        }

        public static TransP TranPSelPointsByID(string sTranID)
        { 
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranP_SelPointsByID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            IDataReader drRead = dbUCenter.ExecuteReader(dcTran);

            TransP tpObject = new TransP();
            if(drRead.Read())
            {
                tpObject.TranPoints = Convert.ToInt32(drRead["points"]);
                tpObject.TranGiftPoints = Convert.ToInt32(drRead["giftpoints"]);
            }
            drRead.Close();
            dcTran.Dispose();
            return tpObject;
        }

        public static int TranPSelPointByID(string sTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranP_SelPointByID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            IDataReader drRead = dbUCenter.ExecuteReader(dcTran);

            int iPayPoint = 0;
            if (drRead.Read())
            {
               int.TryParse(drRead["points"].ToString(),out iPayPoint);
            }
            drRead.Close();
            dcTran.Dispose();
            return iPayPoint;
        }

        public static int TranPSelPointsByUser(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranP_SelPPointsByUser");

            dbUCenter.AddInParameter(dcTran, "@userid", DbType.Int32,iUserID);

            IDataReader drRead = dbUCenter.ExecuteReader(dcTran);

            int iPayPoint = -1;
            if (drRead.Read())
            {
                int.TryParse(drRead["points"].ToString(), out iPayPoint);
            }
            drRead.Close();
            dcTran.Dispose();
            return iPayPoint;
        }

        public static int TranPSelUserIDByID(string sTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranP_SelUserIDByID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            IDataReader drRead = dbUCenter.ExecuteReader(dcTran);

            int iUserID = 0;
            if (drRead.Read())
            {
                int.TryParse(drRead["userid"].ToString(), out iUserID);
            }
            drRead.Close();
            dcTran.Dispose();
            return iUserID;
        }

        public static int TranPSelChannelIDByID(string sTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranP_SelChannelByID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            IDataReader drRead = dbUCenter.ExecuteReader(dcTran);

            int channelid = 0;
            if (drRead.Read())
            {
                int.TryParse(drRead["channelid"].ToString(), out channelid);
            }
            drRead.Close();
            dcTran.Dispose();
            return channelid;
        }

        public static int TranPTodayNumSel(double dPrice, DateTime dtBegin, DateTime dtEnd)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("TranP_TodayNumSel");

            dbUCenter.AddInParameter(dcTran, "@price", DbType.Double, dPrice);
            dbUCenter.AddInParameter(dcTran, "@begintime", DbType.DateTime, dtBegin);
            dbUCenter.AddInParameter(dcTran, "@endtime", DbType.DateTime, dtEnd);

            IDataReader drRead = dbUCenter.ExecuteReader(dcTran);

            int iNum = 0;
            if (drRead.Read())
            {
                int.TryParse(drRead["num"].ToString(), out iNum);
            }
            drRead.Close();
            iNum++;
            return iNum;
        }

        public static double TranPTodayPriceSel(int iUserID,DateTime dtBegin,DateTime dtEnd)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("TranP_TodayPriceSel");

            dbUCenter.AddInParameter(dcTran, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcTran, "@begintime", DbType.DateTime, dtBegin);
            dbUCenter.AddInParameter(dcTran, "@endtime", DbType.DateTime, dtEnd);

            IDataReader drRead = dbUCenter.ExecuteReader(dcTran);

            double dPrice = 0;
            if (drRead.Read())
            {
                dPrice = double.Parse(drRead["price"].ToString());
            }
            drRead.Close();
            return dPrice;
        }

        public static List<TextTwo> TranPTop20PaySel(DateTime dtBegin, DateTime dtEnd)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("TranP_Top20PaySel");

            dbUCenter.AddInParameter(dcTran, "@begintime", DbType.DateTime, dtBegin);
            dbUCenter.AddInParameter(dcTran, "@endtime", DbType.DateTime, dtEnd);

            IDataReader drRead = dbUCenter.ExecuteReader(dcTran);

            List<TextTwo> lObject = new List<TextTwo>();
            while (drRead.Read())
            {
                TextTwo ttObject = new TextTwo();
                ttObject.first = drRead["account"].ToString();
                ttObject.second = drRead["price"].ToString();
                lObject.Add(ttObject);
            }
            drRead.Close();
            return lObject;
        }

        public static int TranPSelStateByID(string sTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("TranP_SelStateByID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            IDataReader drRead = dbUCenter.ExecuteReader(dcTran);

            int iState = -1;
            if (drRead.Read())
            {
                int.TryParse(drRead[0].ToString(), out iState);
            }
            drRead.Close();
            dcTran.Dispose();
            return iState;
        }

        public static TransP TransSelectByID(string sTranID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranP_SelectByID");

            dbUCenter.AddInParameter(dcTran, "@id", DbType.String, sTranID);

            return TranDataBind(dbUCenter.ExecuteReader(dcTran));
        }

        public static IDataReader TransSelOneByUID(int iUserID,int iNum)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TranP_SelOneByUID");

            dbUCenter.AddInParameter(dcTran, "@userid", DbType.Int32, iUserID); 

            return dbUCenter.ExecuteReader(dcTran);
        }

        public static string PointSalesInit(string sChannel,string sPhone,string sAccount,decimal dPrice,int iCount,string sTranIP)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_SALES_Init");

            dbUCenter.AddOutParameter(dcTran, "@tranid",DbType.String,40);
            dbUCenter.AddOutParameter(dcTran, "@result", DbType.Int32, 4);
            dbUCenter.AddInParameter(dcTran, "@channel",DbType.String,sChannel);
            dbUCenter.AddInParameter(dcTran,"@phone",DbType.String,sPhone);
            dbUCenter.AddInParameter(dcTran,"@account",DbType.String,sAccount);
            dbUCenter.AddInParameter(dcTran,"@price",DbType.Decimal,dPrice);
            dbUCenter.AddInParameter(dcTran, "@count", DbType.Int32, iCount);
            dbUCenter.AddInParameter(dcTran, "@tranip", DbType.String, sTranIP);

            dbUCenter.ExecuteNonQuery(dcTran);
            string sRes = string.Empty;
            if ("0" == dbUCenter.GetParameterValue(dcTran, "@result").ToString())
            {
                sRes = dbUCenter.GetParameterValue(dcTran, "@tranid").ToString();
            }
            else
            {
                sRes = dbUCenter.GetParameterValue(dcTran, "@result").ToString();
            }
            return sRes;
        }

        public static string PointSalesGiftInit(string sChannel, string sPhone, string sAccount, decimal dPrice, int iCount, string sTranIP)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SALES_GiftInit");

            dbUCenter.AddOutParameter(dcTran, "@tranid", DbType.String, 40);
            dbUCenter.AddOutParameter(dcTran, "@result", DbType.Int32, 4);
            dbUCenter.AddInParameter(dcTran, "@channel", DbType.String, sChannel);
            dbUCenter.AddInParameter(dcTran, "@phone", DbType.String, sPhone);
            dbUCenter.AddInParameter(dcTran, "@account", DbType.String, sAccount);
            dbUCenter.AddInParameter(dcTran, "@price", DbType.Decimal, dPrice);
            dbUCenter.AddInParameter(dcTran, "@count", DbType.Int32, iCount);
            dbUCenter.AddInParameter(dcTran, "@tranip", DbType.String, sTranIP);

            dbUCenter.ExecuteNonQuery(dcTran);
            string sRes = string.Empty;
            if ("0" == dbUCenter.GetParameterValue(dcTran, "@result").ToString())
            {
                sRes = dbUCenter.GetParameterValue(dcTran, "@tranid").ToString();
            }
            else
            {
                sRes = dbUCenter.GetParameterValue(dcTran, "@result").ToString();
            }
            return sRes;
        }

        public static string getDingDanId(string rtcoid)//获取用户的订单号
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("vbi_getDingDan");
            dbUCenter.AddInParameter(dcTran, "@rtcoid", DbType.String, rtcoid);
            dbUCenter .AddOutParameter(dcTran ,"@id",DbType.String,100);
            dbUCenter.ExecuteNonQuery(dcTran);
           return  dbUCenter.GetParameterValue(dcTran, "@id").ToString ();
        }

        public static int PointSalesCommit(string sTranID,string sAccount,decimal dPrice)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_Sales_Commit");

            dbUCenter.AddOutParameter(dcTran, "@result", DbType.Int32, 4);
            dbUCenter.AddInParameter(dcTran, "@tranid", DbType.String, sTranID);
            dbUCenter.AddInParameter(dcTran, "@account", DbType.String, sAccount);
            dbUCenter.AddInParameter(dcTran, "@price", DbType.Decimal, dPrice);

            dbUCenter.ExecuteNonQuery(dcTran);
            int iRes = -1;
            int.TryParse(dbUCenter.GetParameterValue(dcTran, "@result").ToString(), out iRes);
            dcTran.Dispose();
            return iRes;
        }

        public static int PointSalesGiftCommit(string sTranID, string sAccount, decimal dPrice)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("Sales_GiftCommit");

            dbUCenter.AddOutParameter(dcTran, "@result", DbType.Int32, 4);
            dbUCenter.AddInParameter(dcTran, "@tranid", DbType.String, sTranID);
            dbUCenter.AddInParameter(dcTran, "@account", DbType.String, sAccount);
            dbUCenter.AddInParameter(dcTran, "@price", DbType.Decimal, dPrice);

            dbUCenter.ExecuteNonQuery(dcTran);
            int iRes = -1;
            int.TryParse(dbUCenter.GetParameterValue(dcTran, "@result").ToString(), out iRes);
            dcTran.Dispose();
            return iRes;
        }

        public static string GiftPointsSend(int iUserID, int iGiftPoint, string sTranIP)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("NoRegGiftPointsSend");

            dbUCenter.AddOutParameter(dcTran, "@result", DbType.String, 4);
            dbUCenter.AddInParameter(dcTran, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcTran, "@giftpoints", DbType.Int32, iGiftPoint);
            dbUCenter.AddInParameter(dcTran, "@tranip", DbType.String, sTranIP);

            dbUCenter.ExecuteNonQuery(dcTran);
            string sRes = dbUCenter.GetParameterValue(dcTran, "@result").ToString();
            dcTran.Dispose();
            return sRes;
        }

        public static string YRTSalesInit(int iUserID, int iPoint, decimal dPrice, string sTranIP)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("YRTSALES_Init");

            dbUCenter.AddOutParameter(dcTran, "@tranid", DbType.String, 40);
            dbUCenter.AddInParameter(dcTran, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcTran, "@point", DbType.Int32, iPoint);
            dbUCenter.AddInParameter(dcTran, "@price", DbType.Decimal, dPrice);
            dbUCenter.AddInParameter(dcTran, "@tranip", DbType.String, sTranIP);

            dbUCenter.ExecuteNonQuery(dcTran);
            string sRes = dbUCenter.GetParameterValue(dcTran, "@tranid").ToString();
            dcTran.Dispose();
            return sRes;
        }

        public static int YRTSalesCommit(string sTranID,int iUserID, int iPoint)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("YRTSales_Commit");

            dbUCenter.AddOutParameter(dcTran, "@result", DbType.Int32, 4);
            dbUCenter.AddInParameter(dcTran, "@tranid", DbType.String, sTranID);
            dbUCenter.AddInParameter(dcTran, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcTran, "@point", DbType.Int32, iPoint);

            dbUCenter.ExecuteNonQuery(dcTran);
            int iRes = -1;
            int.TryParse(dbUCenter.GetParameterValue(dcTran, "@result").ToString(), out iRes);
            dcTran.Dispose();
            return iRes;
        }

        public static string FirstGiftSend(int iUserID, int iGiftPoint, string sTranIP, int iChannid)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("FirstGiftSend");

            dbUCenter.AddOutParameter(dcTran, "@result", DbType.String, 4);
            dbUCenter.AddInParameter(dcTran, "@userid", DbType.Int32, iUserID);
            dbUCenter.AddInParameter(dcTran, "@giftpoints", DbType.Int32, iGiftPoint);
            dbUCenter.AddInParameter(dcTran, "@tranip", DbType.String, sTranIP);
            dbUCenter.AddInParameter(dcTran, "@channelid", DbType.Int32, iChannid);

            dbUCenter.ExecuteNonQuery(dcTran);
            string sRes = dbUCenter.GetParameterValue(dcTran, "@result").ToString();
            dcTran.Dispose();
            return sRes;
        }

        public static string SelIsTranByUID(int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("TranP_SelISTranByUID");

            dbUCenter.AddInParameter(dcTran, "@userid", DbType.Int32, iUserID);

            IDataReader drRead = dbUCenter.ExecuteReader(dcTran);

            string  sTranID = string.Empty;
            if (drRead.Read())
            {
               sTranID = drRead[0].ToString();
            }
            drRead.Close();
            dcTran.Dispose();
            return sTranID;
        }

        #region SaveTransMsg
        public static void TransMsgSave(string sAccount, string sTranID, decimal dPrice, string sCode, string sCodeMsg, string sBid)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcTran = dbUCenter.GetStoredProcCommand("SP_TransMsg_Save");

            dbUCenter.AddInParameter(dcTran,"@account",DbType.String,sAccount);
            dbUCenter.AddInParameter(dcTran,"@bid",DbType.String,sBid);
            dbUCenter.AddInParameter(dcTran,"@code",DbType.String,sCode);
            dbUCenter.AddInParameter(dcTran,"@errmsg",DbType.String,sCodeMsg);
            dbUCenter.AddInParameter(dcTran,"@price",DbType.Decimal,dPrice);
            dbUCenter.AddInParameter(dcTran,"@tranid",DbType.String,sTranID);

            dbUCenter.ExecuteNonQuery(dcTran);
            dcTran.Dispose();
        } 
	    #endregion
    }
}

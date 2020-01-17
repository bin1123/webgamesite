using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class GameCodeDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static int GameCodeCountSel(string sServerAbbre, string sCodeType)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGameCode = dbUCenter.GetStoredProcCommand("SP_GameCode_IDCountSel");

            dbUCenter.AddInParameter(dcGameCode, "@gameabbre", DbType.String, sServerAbbre);
            dbUCenter.AddInParameter(dcGameCode, "@codetype", DbType.String, sCodeType);

            IDataReader drGameCode = dbUCenter.ExecuteReader(dcGameCode);
            int iCount = 0;
            if (drGameCode.Read())
            {
                int.TryParse(drGameCode[0].ToString(), out iCount);
            }
            drGameCode.Close();
            dcGameCode.Dispose();
            return iCount;
        }

        public static string GameCodeSelByUserID(string sServerAbbre, string sCodeType,int iUserID)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGameCode = dbUCenter.GetStoredProcCommand("SP_GameCode_CodeSelByUserID");
            
            dbUCenter.AddInParameter(dcGameCode, "@gameabbre", DbType.String, sServerAbbre);
            dbUCenter.AddInParameter(dcGameCode, "@userid", DbType.String, iUserID);
            dbUCenter.AddInParameter(dcGameCode, "@codetype", DbType.String, sCodeType);

            IDataReader drGameCode = dbUCenter.ExecuteReader(dcGameCode);
            string sGameCode = string.Empty;
            if (drGameCode.Read())
            {
                sGameCode = drGameCode[0].ToString();
            }
            drGameCode.Close();
            dcGameCode.Dispose();
            return sGameCode;
        }

        public static string GameCodeGet(string sServerAbbre,int iUserID,string sCodeType,string sIp)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGameCode = dbUCenter.GetStoredProcCommand("SP_GameCode_CodeGet");

            dbUCenter.AddInParameter(dcGameCode, "@gameabbre", DbType.String, sServerAbbre);
            dbUCenter.AddInParameter(dcGameCode, "@userid", DbType.String, iUserID);
            dbUCenter.AddInParameter(dcGameCode, "@codetype", DbType.String, sCodeType);
            dbUCenter.AddInParameter(dcGameCode, "@ip", DbType.String, sIp);
            dbUCenter.AddOutParameter(dcGameCode, "@gamecode", DbType.String, 100);
            dbUCenter.AddOutParameter(dcGameCode, "@result", DbType.Int32, 4);

            dbUCenter.ExecuteNonQuery(dcGameCode);
            string sRes = dbUCenter.GetParameterValue(dcGameCode, "@result").ToString();
            if ("0" == sRes)
            {
                sRes = dbUCenter.GetParameterValue(dcGameCode, "@gamecode").ToString();
            }
            dcGameCode.Dispose();
            return sRes;
        }
    }
}

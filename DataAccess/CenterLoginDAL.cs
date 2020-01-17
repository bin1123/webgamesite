using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class CenterLoginDAL
    {
        private const string sConn = "datacenter";
        private const string sReadConn = "datacenterread";

        public static int CenterLoginAdd(CenterLogin clObject)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcCenterLogin = dbDCenter.GetStoredProcCommand("SP_CenterLogin_Add");

            dbDCenter.AddInParameter(dcCenterLogin, "@userid", DbType.Int32, clObject.UserID);
            dbDCenter.AddInParameter(dcCenterLogin, "@loginip", DbType.String, clObject.LoginIp);
            dbDCenter.AddInParameter(dcCenterLogin, "@account", DbType.String, clObject.Account);
            dbDCenter.AddInParameter(dcCenterLogin, "@fromurl", DbType.String, clObject.FromUrl);

            return dbDCenter.ExecuteNonQuery(dcCenterLogin);
        }

        //public static string GameLoginLastSel(int iUserID)
        //{
        //    Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
        //    DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("SP_GameLogin_LastSel");

        //    dbDCenter.AddInParameter(dcGameLogin, "@userid", DbType.Int32,iUserID);

        //    DataSet dsObject = dbDCenter.ExecuteDataSet(dcGameLogin);
        //    StringBuilder sbText = new StringBuilder(5);            
        //    foreach(DataRow drObject in dsObject.Tables[0].Rows)
        //    {
        //        sbText.AppendFormat("{0}|",drObject["gamename"].ToString().Trim());                
        //    }
        //    return sbText.ToString();
        //}
    }
}

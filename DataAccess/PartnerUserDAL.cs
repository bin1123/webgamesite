using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using DataEnity;

namespace DataAccess
{
    public class PartnerUserDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static string PartnerUserNameSel(int iUserID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcPartnerUser = dbDCenter.GetStoredProcCommand("SP_PartnerUser_UserNameSel");

            dbDCenter.AddInParameter(dcPartnerUser, "@userid", DbType.Int32, iUserID);

            IDataReader drPartnerUser = dbDCenter.ExecuteReader(dcPartnerUser);
            string sRes = string.Empty;
            if (drPartnerUser.Read())
            {
                sRes = drPartnerUser["username"].ToString();
            }
            drPartnerUser.Close();
            drPartnerUser.Dispose();
            return sRes;
        }

        public static int PartnerUserPIDSel(int iUserID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcPartnerUser = dbDCenter.GetStoredProcCommand("SP_PartnerUser_PIDSel");

            dbDCenter.AddInParameter(dcPartnerUser, "@userid", DbType.Int32, iUserID);

            IDataReader drPartnerUser = dbDCenter.ExecuteReader(dcPartnerUser);
            int iRes = 0;
            if (drPartnerUser.Read())
            {
                int.TryParse(drPartnerUser["pid"].ToString(), out iRes);
            }
            drPartnerUser.Close();
            drPartnerUser.Dispose();
            return iRes;
        }

        public static int PartnerUserIDSel(string sUserName,int iPId)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcPartnerUser = dbDCenter.GetStoredProcCommand("SP_PartnerUser_UserIDSel");

            dbDCenter.AddInParameter(dcPartnerUser, "@username", DbType.String, sUserName);
            dbDCenter.AddInParameter(dcPartnerUser, "@pid", DbType.Int32, iPId);

            IDataReader drPartnerUser = dbDCenter.ExecuteReader(dcPartnerUser);
            int iRes = 0;
            if (drPartnerUser.Read())
            {
                int.TryParse(drPartnerUser["userid"].ToString(),out iRes);
            }
            drPartnerUser.Close();
            drPartnerUser.Dispose();
            return iRes;
        }

        public static int PartnerUserAdd(PartnerUser puObject)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcPartnerUser = dbDCenter.GetStoredProcCommand("SP_PartnerUser_Add");

            dbDCenter.AddInParameter(dcPartnerUser, "@userid", DbType.Int32, puObject.userid);
            dbDCenter.AddInParameter(dcPartnerUser, "@username", DbType.String, puObject.username);
            dbDCenter.AddInParameter(dcPartnerUser, "@regip", DbType.String, puObject.regip);
            dbDCenter.AddInParameter(dcPartnerUser, "@pid", DbType.Int32, puObject.pid);

            return dbDCenter.ExecuteNonQuery(dcPartnerUser);
        }
    }
}

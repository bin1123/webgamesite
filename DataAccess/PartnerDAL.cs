using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using DataEnity;

namespace DataAccess
{
    public class PartnerDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static string PartnerAbbreSel(int pid)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcPartner = dbDCenter.GetStoredProcCommand("SP_Partner_AbbreSel");

            dbDCenter.AddInParameter(dcPartner, "@id", DbType.Int32, pid);

            IDataReader drPartner = dbDCenter.ExecuteReader(dcPartner);
            string sRes = string.Empty;
            if (drPartner.Read())
            {
                sRes = drPartner["abbre"].ToString();
            }
            drPartner.Close();
            drPartner.Dispose();
            return sRes;
        }

        public static string PartnerKeySel(int pid)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcPartner = dbDCenter.GetStoredProcCommand("SP_Partner_KeySel");

            dbDCenter.AddInParameter(dcPartner, "@id", DbType.Int32, pid);

            IDataReader drPartner = dbDCenter.ExecuteReader(dcPartner);
            string sRes = string.Empty;
            if (drPartner.Read())
            {
                sRes = drPartner["key"].ToString();
            }
            drPartner.Close();
            drPartner.Dispose();
            return sRes;
        }
    }
}

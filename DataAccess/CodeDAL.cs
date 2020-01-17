using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;
using System.Collections.Generic;

namespace DataAccess
{
    public class CodeDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static List<Code> CodeTypeSel(string sGameAbbre)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcCode = dbUCenter.GetStoredProcCommand("SP_Code_SelByGame");

            dbUCenter.AddInParameter(dcCode, "@gameabbre", DbType.String,sGameAbbre);

            IDataReader drCode = dbUCenter.ExecuteReader(dcCode);
            List<Code> lcObject = new List<Code>();
            while (drCode.Read())
            {
                Code cObject = new Code();
                cObject.CodeName = drCode["name"].ToString();
                cObject.Abbre = drCode["abbre"].ToString();
                lcObject.Add(cObject);
            }
            drCode.Close();
            dcCode.Dispose();
            return lcObject;
        }

        public static string CodeUrlSel(string sAbbre)
        {
            Database dbUCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcCode = dbUCenter.GetStoredProcCommand("SP_Code_UrlSelByGame");

            dbUCenter.AddInParameter(dcCode, "@abbre", DbType.String, sAbbre);

            IDataReader drCode = dbUCenter.ExecuteReader(dcCode);
            string sUrl = string.Empty;
            while (drCode.Read())
            {
                Code cObject = new Code();
                sUrl = drCode["Url"].ToString();
            }
            drCode.Close();
            dcCode.Dispose();
            return sUrl;
        }
    }
}

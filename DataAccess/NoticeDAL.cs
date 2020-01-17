using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;
using System.Collections.Generic;

namespace DataAccess
{
    public class NoticeDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";
        private const string sCMSConn = "cms";

        public static Dictionary<string, string> NoticeSel(string sAbbre)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("SP_Notice_Sel");

            dbDCenter.AddInParameter(dcGame, "@abbre", DbType.String, sAbbre);

            IDataReader drGame = dbDCenter.ExecuteReader(dcGame);
            Dictionary<string, string> sDRes = new Dictionary<string, string>();
            while (drGame.Read())
            {
                sDRes.Add(drGame["NewsTitle"].ToString(), drGame["filename"].ToString());
            }
            drGame.Close();
            drGame.Dispose();
            return sDRes;
        }

        public static List<TextTwo> NoticeSelC(string sAbbre)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("SP_Notice_Sel");

            dbDCenter.AddInParameter(dcGame, "@abbre", DbType.String, sAbbre);

            IDataReader drGame = dbDCenter.ExecuteReader(dcGame);
            List<TextTwo> sDRes = new List<TextTwo>();
            while (drGame.Read())
            {
                TextTwo ttObject = new TextTwo();
                ttObject.first = drGame["NewsTitle"].ToString();
                ttObject.second = drGame["filename"].ToString();
                sDRes.Add(ttObject);
            }
            drGame.Close();
            drGame.Dispose();
            return sDRes;
        }

        public static List<TextTwo> NoticeSelFromCMS(string sClassID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sCMSConn);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("Notice_Sel");

            dbDCenter.AddInParameter(dcGame, "@classid", DbType.String, sClassID);

            IDataReader drGame = dbDCenter.ExecuteReader(dcGame);
            List<TextTwo> sDRes = new List<TextTwo>();
            while (drGame.Read())
            {
                TextTwo ttObject = new TextTwo();
                ttObject.first = drGame["NewsTitle"].ToString();
                ttObject.second = drGame["filename"].ToString();
                sDRes.Add(ttObject);
            }
            drGame.Close();
            drGame.Dispose();
            return sDRes;
        }

        public static string NoticeClassIDSel(string sAbbre)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("Notice_ClassIDSel");

            dbDCenter.AddInParameter(dcGame, "@abbre", DbType.String, sAbbre);

            IDataReader drGame = dbDCenter.ExecuteReader(dcGame);
            string sClassID = string.Empty;
            if(drGame.Read())
            {
                sClassID = drGame[0].ToString();                
            }
            drGame.Close();
            drGame.Dispose();
            return sClassID;
        }
    }    
}

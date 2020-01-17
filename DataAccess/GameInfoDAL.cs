using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using DataEnity;

namespace DataAccess
{
    public class GameInfoDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static string GameInfoAbbreSel(string sAbbre)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGameInfo = dbDCenter.GetStoredProcCommand("SP_GameInfo_AbbreSel");

            dbDCenter.AddInParameter(dcGameInfo, "@abbre", DbType.String, sAbbre);

            IDataReader drGameInfo = dbDCenter.ExecuteReader(dcGameInfo);
            string sRes = string.Empty;
            if (drGameInfo.Read())
            {
                sRes = drGameInfo["abbre"].ToString();
            }
            drGameInfo.Close();
            drGameInfo.Dispose();
            return sRes;
        }

        public static List<GameInfo> GameInfoSel()
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGameInfo = dbDCenter.GetStoredProcCommand("SP_GameInfo_AllSel");

            IDataReader drGameInfo = dbDCenter.ExecuteReader(dcGameInfo);
            List<GameInfo> lGameInfoObject = new List<GameInfo>();
            while (drGameInfo.Read())
            {
                GameInfo gObject = new GameInfo();
                gObject.GameName = drGameInfo["gamename"].ToString();
                gObject.abbre = drGameInfo["abbre"].ToString();
                lGameInfoObject.Add(gObject);
            }
            drGameInfo.Close();
            drGameInfo.Dispose();
            return lGameInfoObject;
        }

        public static List<ObjectThree> GameInfoSelC()
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGameInfo = dbDCenter.GetStoredProcCommand("GameInfo_AllSel");

            IDataReader drGameInfo = dbDCenter.ExecuteReader(dcGameInfo);
            List<ObjectThree> lGameInfoObject = new List<ObjectThree>();
            while (drGameInfo.Read())
            {
                ObjectThree otObject = new ObjectThree();
                otObject.first = drGameInfo["id"].ToString();
                otObject.second = drGameInfo["gamename"].ToString();
                otObject.third = drGameInfo["abbre"].ToString();
                lGameInfoObject.Add(otObject);
            }
            drGameInfo.Close();
            drGameInfo.Dispose();
            return lGameInfoObject;
        }

        public static int GameInfoIDSel(string sAbbre)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGameInfo = dbDCenter.GetStoredProcCommand("SP_GameInfo_IDSelByAbbre");

            dbDCenter.AddInParameter(dcGameInfo, "@abbre", DbType.String, sAbbre);

            IDataReader drGameInfo = dbDCenter.ExecuteReader(dcGameInfo);
            int iGameID = 0;
            if (drGameInfo.Read())
            {
                 int.TryParse(drGameInfo["gameid"].ToString(),out iGameID);
            }
            drGameInfo.Close();
            drGameInfo.Dispose();
            return iGameID;
        }
    }
}

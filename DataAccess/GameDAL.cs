using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;
using System.Collections.Generic;

namespace DataAccess
{
    public class GameDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";
        private const string sCMSConn = "cms";

        public static string GameAbbreSel(int iGameID,int iServerID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("SP_Game_AbbreSel");

            dbDCenter.AddInParameter(dcGame, "@gameid", DbType.Int32,iGameID);
            dbDCenter.AddInParameter(dcGame, "@serverid", DbType.Int32,iServerID);

            IDataReader drGame = dbDCenter.ExecuteReader(dcGame);
            string sRes = string.Empty;
            if (drGame.Read())
            {
                sRes = drGame["abbre"].ToString();
            }
            drGame.Close();
            drGame.Dispose();
            return sRes;
        }

        public static Dictionary<string,string> GameHelpSel(int iGameID, string sClassName)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("SP_Game_Help");

            dbDCenter.AddInParameter(dcGame, "@gameid", DbType.Int32, iGameID);
            dbDCenter.AddInParameter(dcGame, "@classname", DbType.String, sClassName);

            IDataReader drGame = dbDCenter.ExecuteReader(dcGame);
            Dictionary<string,string> sDRes = new Dictionary<string,string>();
            while (drGame.Read())
            {
                sDRes.Add(drGame["NewsTitle"].ToString(), drGame["filename"].ToString());
            }
            drGame.Close();
            drGame.Dispose();
            return sDRes;
        }

        public static List<TextTwo> GameHelpSelC(int iGameID, string sClassName)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("SP_Game_Help");

            dbDCenter.AddInParameter(dcGame, "@gameid", DbType.Int32, iGameID);
            dbDCenter.AddInParameter(dcGame, "@classname", DbType.String, sClassName);

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

        public static List<TextTwo> GameHelpSelFromCMS(string sClassID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sCMSConn);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("Game_Help");

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

        public static Dictionary<string, string> GameHelpLJSel(int iGameID, string sClassName)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("SP_Game_LJ_Help");

            dbDCenter.AddInParameter(dcGame, "@gameid", DbType.Int32, iGameID);
            dbDCenter.AddInParameter(dcGame, "@classname", DbType.String, sClassName);

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

        public static List<TextTwo> GameHelpLJSelFromCMS(string sClassID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sCMSConn);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("GameLJ_Help");

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

        public static List<TextTwo> GameHelpLJ2SelFromCMS(string sClassID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sCMSConn);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("GameLJ2_Help");

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

        public static List<TextTwo> GameHelpLJSelC(int iGameID, string sClassName)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("SP_Game_LJ_Help");

            dbDCenter.AddInParameter(dcGame, "@gameid", DbType.Int32, iGameID);
            dbDCenter.AddInParameter(dcGame, "@classname", DbType.String, sClassName);

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

        public static string GameHelpClassIDSel(int iGameID, string sClassName)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcGame = dbDCenter.GetStoredProcCommand("GameHelp_ClassIDSel");

            dbDCenter.AddInParameter(dcGame, "@gameid", DbType.Int32, iGameID);
            dbDCenter.AddInParameter(dcGame, "@classname", DbType.String, sClassName);

            IDataReader drGame = dbDCenter.ExecuteReader(dcGame);
            string sClassID = string.Empty;
            if (drGame.Read())
            {
                sClassID = drGame[0].ToString();
            }
            drGame.Close();
            drGame.Dispose();
            return sClassID;
        }
    }
}

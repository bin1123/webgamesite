using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class GameLoginDAL
    {
        private const string sConn = "datacenter";
        private const string sReadConn = "datacenterread";

        public static int GameLoginAdd(GameLogin glObject)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("SP_GameLogin_Add");

            dbDCenter.AddInParameter(dcGameLogin, "@userid", DbType.Int32, glObject.UserID);
            dbDCenter.AddInParameter(dcGameLogin, "@gamename", DbType.String, glObject.GameName);
            dbDCenter.AddInParameter(dcGameLogin, "@loginip", DbType.String, glObject.LoginIp);
            dbDCenter.AddInParameter(dcGameLogin, "@loginurl", DbType.String, glObject.LoginUrl);

            return dbDCenter.ExecuteNonQuery(dcGameLogin);
        }

        public static string GameLoginLastSel(int iUserID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sReadConn);
            DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("GameLogin_LastSel");

            dbDCenter.AddInParameter(dcGameLogin, "@userid", DbType.Int32,iUserID);

            DataSet dsObject = dbDCenter.ExecuteDataSet(dcGameLogin);
            StringBuilder sbText = new StringBuilder(5);
            int i = 0;
            foreach(DataRow drObject in dsObject.Tables[0].Rows)
            {
                if (i == 0)
                {
                    sbText.Append(drObject["gamename"].ToString().Trim());
                }
                else
                {
                    sbText.AppendFormat("|{0}",drObject["gamename"].ToString().Trim());
                }
                i++;
            }
            dcGameLogin.Dispose();            
            return sbText.ToString();
        }

        public static string[] GameLoginLastServerSel(int iUserID, int iGameID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sReadConn);
            DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("GameLogin_LastServerSel");

            dbDCenter.AddInParameter(dcGameLogin, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcGameLogin, "@gameid", DbType.Int32, iGameID);

            IDataReader drGameLogin = dbDCenter.ExecuteReader(dcGameLogin);
            string[] sLastServer = new string[2];
            if(drGameLogin.Read())
            {
                sLastServer[0] = drGameLogin["servername"].ToString();
                sLastServer[1] = drGameLogin["abbre"].ToString().Trim();
            }
            drGameLogin.Close();
            drGameLogin.Dispose();
            return sLastServer;
        }

        public static bool GameLoginIsLogin(int iUserID,string sGameAbbre)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sReadConn);
            DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("SP_GameLogin_IsLogin");

            dbDCenter.AddInParameter(dcGameLogin, "@userid", DbType.Int32, iUserID);
            dbDCenter.AddInParameter(dcGameLogin, "@gamename", DbType.String, sGameAbbre);

            IDataReader drGameLogin = dbDCenter.ExecuteReader(dcGameLogin);
            bool bRes = false;
            if (drGameLogin.Read())
            {
                bRes = true;
            }
            else
            {
                bRes = false;
            }
            drGameLogin.Close();
            drGameLogin.Dispose();
            return bRes;
        }

        public static string GameLoginAbbreSel(int iUserID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sReadConn);
            DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("GameLogin_Top3AbbreSel");

            dbDCenter.AddInParameter(dcGameLogin, "@userid", DbType.Int32, iUserID);

            StringBuilder sbGameInfo = new StringBuilder();
            IDataReader drGameLogin = dbDCenter.ExecuteReader(dcGameLogin);
            int i = 1;
            while (drGameLogin.Read())
            {
                if (i == 1)
                {
                    sbGameInfo.Append(drGameLogin[0].ToString().Trim());
                }
                else
                {
                    sbGameInfo.AppendFormat("|{0}", drGameLogin[0].ToString().Trim());
                }
                i++;
            }
            drGameLogin.Close();
            drGameLogin.Dispose();
            return sbGameInfo.ToString();
        }

        public static string GameLoginLastSelCJson(int iUserID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
            DbCommand dcGameLogin = dbDCenter.GetStoredProcCommand("GameLogin_LastSelC");

            dbDCenter.AddInParameter(dcGameLogin, "@userid", DbType.Int32, iUserID);

            IDataReader drGameLogin = dbDCenter.ExecuteReader(dcGameLogin);
            StringBuilder sbText = new StringBuilder("{root:[");
            while (drGameLogin.Read())
            {
                sbText.Append("{");
                sbText.AppendFormat("gamename:'{0}',servername:'{1}',serverabbre:'{2}',serverid:'{3}'",drGameLogin["gamename"].ToString().Trim(),
                                     drGameLogin["servername"].ToString().Trim(), drGameLogin["serverabbre"].ToString().Trim(), drGameLogin["serverid"].ToString());
                sbText.Append("},");
            }
            drGameLogin.Close();
            dcGameLogin.Dispose();
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }
    }
}

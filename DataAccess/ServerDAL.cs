using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DataEnity;

namespace DataAccess
{
    public class ServerDAL
    {
        private const string sConn = "userscenter";
        private const string sConnRead = "userscenterread";

        public static Dictionary<string,string> ServerSel(string sAbbre)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcServer = dbDCenter.GetStoredProcCommand("SP_Server_AllSel");

            dbDCenter.AddInParameter(dcServer, "@gameabbre", DbType.String, sAbbre);

            IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
            Dictionary<string, string> dServerObject = new Dictionary<string, string>();
            while (drServer.Read())
            {
                dServerObject.Add(drServer["abbre"].ToString(), drServer["servername"].ToString());
            }
            drServer.Close();
            drServer.Dispose();
            return dServerObject;
        }

        public static List<ObjectFour> ServerSelByGID(int iGameID)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcServer = dbDCenter.GetStoredProcCommand("Server_AllSelByGID");

            dbDCenter.AddInParameter(dcServer, "@gameid", DbType.String, iGameID);

            IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
            List<ObjectFour> dServerObject = new List<ObjectFour>();
            while (drServer.Read())
            {
                ObjectFour otObject = new ObjectFour();
                otObject.first = drServer["serverid"].ToString();
                otObject.second = drServer["abbre"].ToString();
                otObject.third = drServer["servername"].ToString(); 
                otObject.Fourth = drServer["begintime"].ToString();
                dServerObject.Add(otObject);
            }
            drServer.Close();
            drServer.Dispose();
            return dServerObject;
        }

        public static Dictionary<string, string> ServerNumSel(string sAbbre,string sNum)
        {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcServer = dbDCenter.GetStoredProcCommand("SP_Server_NumSel");

            dbDCenter.AddInParameter(dcServer, "@gameabbre", DbType.String, sAbbre);
            dbDCenter.AddInParameter(dcServer, "@num", DbType.String, sNum);

            IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
            Dictionary<string, string> dServerObject = new Dictionary<string, string>();
            while (drServer.Read())
            {
                dServerObject.Add(drServer["abbre"].ToString(), drServer["servername"].ToString());
            }
            drServer.Close();
            drServer.Dispose();
            return dServerObject;
        }

       public static string[] ServerNewSel(int iGameID)
       {
            Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
            DbCommand dcServer = dbDCenter.GetStoredProcCommand("SP_Server_NewSel");

            dbDCenter.AddInParameter(dcServer, "@gameid", DbType.Int32, iGameID);

            IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
            string[] sSevers= new string[2]; 
            if(drServer.Read())
            {
                sSevers[0] = drServer["servername"].ToString();
                sSevers[1] = drServer["abbre"].ToString().Trim();
            }
            drServer.Close();
            return sSevers;
       }

       public static string ServerTitleSel(string sGameAbbre)
       {
           Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
           DbCommand dcServer = dbDCenter.GetStoredProcCommand("SP_Server_TitleSel");

           dbDCenter.AddInParameter(dcServer, "@gameabbre", DbType.String, sGameAbbre);

           IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
           string sTitle = string.Empty;
           if (drServer.Read())
           {
               sTitle = drServer["servername"].ToString();
           }
           drServer.Close();
           return sTitle;
       }

       public static string ServerTitleSelC(string sGameAbbre)
       {
           Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
           DbCommand dcServer = dbDCenter.GetStoredProcCommand("Server_TitleSel");

           dbDCenter.AddInParameter(dcServer, "@gameabbre", DbType.String, sGameAbbre);

           IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
           string sTitle = string.Empty;
           if (drServer.Read())
           {
               sTitle = drServer["servername"].ToString();
           }
           drServer.Close();
           return sTitle;
       }

       public static string ServerTitleNoSNameSel(string sGameAbbre)
       {
           Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
           DbCommand dcServer = dbDCenter.GetStoredProcCommand("Server_TitleNoSNameSel");

           dbDCenter.AddInParameter(dcServer, "@gameabbre", DbType.String, sGameAbbre);

           IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
           string sTitle = string.Empty;
           if (drServer.Read())
           {
               sTitle = drServer["servername"].ToString();
           }
           drServer.Close();
           return sTitle;
       }

       public static string ServerTimeSel(string sGameAbbre)
       {
           Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
           DbCommand dcServer = dbDCenter.GetStoredProcCommand("SP_Server_BeginTimeSelByAbbre");

           dbDCenter.AddInParameter(dcServer, "@gameabbre", DbType.String, sGameAbbre);

           IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
           string sTime = string.Empty;
           if (drServer.Read())
           {
               sTime = drServer["begintime"].ToString();
           }
           drServer.Close();
           return sTime;
       }

       public static List<ObjectFour> ServerStartAtTime(DateTime dtBegin, DateTime dtEnd)
       {
           Database dbDCenter = DatabaseFactory.CreateDatabase(sConnRead);
           DbCommand dcServer = dbDCenter.GetStoredProcCommand("Server_StartAtTime");

           dbDCenter.AddInParameter(dcServer, "@begintime", DbType.DateTime, dtBegin);
           dbDCenter.AddInParameter(dcServer, "@endtime", DbType.DateTime, dtEnd);

           IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
           List<ObjectFour> dServerObject = new List<ObjectFour>();
           while (drServer.Read())
           {
               ObjectFour otObject = new ObjectFour();
               otObject.first = drServer["serverid"].ToString();
               otObject.second = drServer["abbre"].ToString();
               otObject.third = drServer["gamename"].ToString();
               otObject.Fourth = drServer["begintime"].ToString();
               dServerObject.Add(otObject);
           }
           drServer.Close();
           drServer.Dispose();
           return dServerObject;
       }

       public static string ServerNameSelByAbbre(string sGameAbbre)
       {
           Database dbDCenter = DatabaseFactory.CreateDatabase(sConn);
           DbCommand dcServer = dbDCenter.GetStoredProcCommand("Server_NameSelByAbbre");

           dbDCenter.AddInParameter(dcServer, "@gameabbre", DbType.String, sGameAbbre);

           IDataReader drServer = dbDCenter.ExecuteReader(dcServer);
           string sName = string.Empty;
           if (drServer.Read())
           {
               sName = drServer[0].ToString();
           }
           drServer.Close();
           return sName;
       }
    }
}

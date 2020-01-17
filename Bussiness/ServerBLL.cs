using System;
using DataAccess;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using DataEnity;
using Common;

namespace Bussiness
{
    public class ServerBLL
    {
        public static Dictionary<string, string> ServerSel(string sAbbre)
        {
           return ServerDAL.ServerSel(sAbbre);
        }

        public static List<ObjectFour> ServerSelByGID(int iGameID)
        {
            return ServerDAL.ServerSelByGID(iGameID);
        }

        public static Dictionary<string, string> ServerNumSel(string sAbbre,string sNum)
        {
            return ServerDAL.ServerNumSel(sAbbre,sNum);
        }

        public static string ServerNumJsonSel(string sAbbre, string sNum)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            Dictionary<string, string> dObject = ServerNumSel(sAbbre,sNum);
            foreach (KeyValuePair<string, string> kObject in dObject)
            {
                sbText.Append("{");
                sbText.AppendFormat("servername:'{0}',abbre:'{1}'", kObject.Value, kObject.Key.Trim());
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static string ServerJsonSel(string sAbbre)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            Dictionary<string, string> dObject = ServerSel(sAbbre);
            foreach (KeyValuePair<string,string> kObject in dObject)
            {
                sbText.Append("{");
                sbText.AppendFormat("servername:'{0}',abbre:'{1}'",kObject.Value,kObject.Key.Trim());
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1; 
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static string[] ServerNewSel(int iGameID)
        {
            return ServerDAL.ServerNewSel(iGameID);
        }

        public static string ServerNewJsonSel(int iGameID)
        {
            string[] sServerInfo = ServerNewSel(iGameID);
            StringBuilder sbText = new StringBuilder("{root:[");
            sbText.Append("{");
            sbText.AppendFormat("servername:'{0}',abbre:'{1}'", sServerInfo[0], sServerInfo[1]);
            sbText.Append("},");
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static string ServerTitleSel(string sGameAbbre)
        {
            StringBuilder sbTitle = new StringBuilder("到武林_");
            sbTitle.Append(ServerDAL.ServerTitleSel(sGameAbbre));
            sbTitle.Append("--http://www.dao50.com/");
            return sbTitle.ToString();
        }

        public static string ServerTitleSelC(string sGameAbbre)
        {
            return ServerDAL.ServerTitleSelC(sGameAbbre);
        }

        public static string ServerTitleNoSNameSel(string sGameAbbre)
        {
            return ServerDAL.ServerTitleNoSNameSel(sGameAbbre);
        }

        public static string ServerTimeSel(string sGameAbbre)
        {
            return ServerDAL.ServerTimeSel(sGameAbbre);
        }

        public static string ServerJsonSelByGame(string sGameAbbre)
        {
            string sUrl = string.Format("http://db.dao50.com/data/server/{0}.txt",sGameAbbre);
            string sJson = ProvideCommon.GetPageInfo(sUrl, "UTF-8");
            return sJson;
        }

        public static List<TextTwo> ServerSelByAbbre(string sServerAbbres)
        {
            List<string> lsServerAbbre = new List<string>(sServerAbbres.Split('|'));
            XElement xdObject = XElement.Load("http://db.dao50.com/data/game.xml");

            var serverinfo = from server in xdObject.Elements("game").Elements("server")
                             from serverabbre in lsServerAbbre
                             where serverabbre == server.Attribute("abbre").Value
                             select new
                             {
                                 gamename = server.Parent.Attribute("name").Value,
                                 serverid = server.Attribute("id").Value,
                                 servername = server.Value,
                                 serverabbre = server.Attribute("abbre").Value
                             };
            List<TextTwo> lttServerInfo = new List<TextTwo>();
            foreach (var server in serverinfo)
            {
                TextTwo ttServer = new TextTwo();
                ttServer.first = string.Format("{0} {1} {2}服", server.gamename, server.servername, server.serverid);
                ttServer.second = server.serverabbre.ToString();
                lttServerInfo.Add(ttServer);
            }
            return lttServerInfo;
        }

        public static string ServerSelByAbbreAll(string sServerAbbres)
        {
            List<string> lsServerAbbre = new List<string>(sServerAbbres.Split('|'));
            XElement xdObject = XElement.Load("http://db.dao50.com/data/game.xml");

            var serverinfo = from server in xdObject.Elements("game").Elements("server")
                             from serverabbre in lsServerAbbre
                             where serverabbre == server.Attribute("abbre").Value
                             select new
                             {
                                 gamename = server.Parent.Attribute("name").Value,
                                 serverid = server.Attribute("id").Value,
                                 servername = server.Value,
                                 serverabbre = server.Attribute("abbre").Value
                             };
            StringBuilder sbText = new StringBuilder("{root:[");
            foreach (var server in serverinfo)
            {
                sbText.Append("{");
                sbText.AppendFormat("gamename:'{0}',servername:'{1}',abbre:'{2}',serverid:'{3}'",
                                     server.gamename,server.servername,server.serverabbre,server.serverid);
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static string ServerLoginJsonSel(string sServerAbbres)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            List<TextTwo> lttObjext = ServerSelByAbbre(sServerAbbres);
            foreach (TextTwo ttObject in lttObjext)
            {
                sbText.Append("{");
                sbText.AppendFormat("servername:'{0}',abbre:'{1}'", ttObject.first, ttObject.second);
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static List<ObjectFour> ServerStartAtTime(DateTime dtBegin, DateTime dtEnd)
        {
            return ServerDAL.ServerStartAtTime(dtBegin,dtEnd);
        }

        public static string ServerNameSelByAbbre(string sGameAbbre)
        {
            return ServerDAL.ServerNameSelByAbbre(sGameAbbre);
        }
    }
}

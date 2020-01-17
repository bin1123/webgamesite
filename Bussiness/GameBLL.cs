using System.Collections.Generic;
using System.Text;

using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class GameBLL
    {
        public static string GameAbbreSel(int iGameID,int iServerID)
        {
            return GameDAL.GameAbbreSel(iGameID, iServerID);
        }

        public static string GamesInfoXml()
        {
            StringBuilder sbText = new StringBuilder("<?xml version='1.0' encoding='UTF-8'?><root>");
            List<ObjectThree> otLObject = GameInfoBLL.GameInfoSelC();
            foreach(ObjectThree otObject in otLObject)
            {
                sbText.AppendFormat("<game name='{0}' abbre='{1}'>",otObject.second,otObject.third.TrimEnd());
                int iGameID = int.Parse(otObject.first);
                List<ObjectFour> ofLObject = ServerBLL.ServerSelByGID(iGameID);
                foreach(ObjectFour ofObject in ofLObject)
                {
                    sbText.AppendFormat("<server id='{0}' abbre='{1}' begintime='{2}'>{3}</server>",
                                        ofObject.first,ofObject.second.TrimEnd(),ofObject.Fourth,ofObject.third);
                }
                sbText.Append("</game>");
            }
            sbText.Append("</root>");
            return sbText.ToString();
        }

        public static string GameDBXml()
        {
            string sUrl = "http://db.dao50.com/data/game.xml";
            string sXml = Common.ProvideCommon.GetPageInfo(sUrl, "UTF-8");
            return sXml;
        }

        public static List<TextTwo> GameHelpSelC(int iGameID, string sClassName)
        {
            return GameDAL.GameHelpSelC(iGameID, sClassName);
        }

        public static List<TextTwo> GameHelpSelFromCMS(string sClassID)
        {
            return GameDAL.GameHelpSelFromCMS(sClassID);
        }

        public static List<TextTwo> GameHelpLJSelC(int iGameID, string sClassName)
        {
            return GameDAL.GameHelpLJSelC(iGameID, sClassName);
        }

        public static List<TextTwo> GameHelpLJSelFromCMS(string sClassID)
        {
            return GameDAL.GameHelpLJSelFromCMS(sClassID);
        }

        public static List<TextTwo> GameHelpLJ2SelFromCMS(string sClassID)
        {
            return GameDAL.GameHelpLJ2SelFromCMS(sClassID);
        }

        public static string GameHelpJsonSel(int iGameID, string sClassName)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            List<TextTwo> dgObject = GameHelpSelC(iGameID, sClassName);
            foreach (TextTwo ttObject in dgObject)
            {
                sbText.Append("{");
                sbText.AppendFormat("title:'{0}',url:'{1}'", ttObject.first, ttObject.second);
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static string GameHelpJsonSelFromCMS(int iGameID, string sClassName)
        {
            string sClassID = GameHelpClassIDSel(iGameID, sClassName);
            StringBuilder sbText = new StringBuilder("{root:[");
            List<TextTwo> dgObject = GameHelpSelFromCMS(sClassID);
            foreach (TextTwo ttObject in dgObject)
            {
                sbText.Append("{");
                sbText.AppendFormat("title:'{0}',url:'{1}'", ttObject.first, ttObject.second);
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static string GameHelpLJJsonSel(int iGameID, string sClassName)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            List<TextTwo> dgObject = GameHelpLJSelC(iGameID, sClassName);
            foreach (TextTwo ttObject in dgObject)
            {
                sbText.Append("{");
                sbText.AppendFormat("title:'{0}',url:'{1}'", ttObject.first, ttObject.second);
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static string GameHelpLJJsonSelFromCMS(int iGameID, string sClassName)
        {
            string sClassID = GameHelpClassIDSel(iGameID, sClassName);
            StringBuilder sbText = new StringBuilder("{root:[");
            List<TextTwo> dgObject = GameHelpLJSelFromCMS(sClassID);
            foreach (TextTwo ttObject in dgObject)
            {
                sbText.Append("{");
                sbText.AppendFormat("title:'{0}',url:'{1}'", ttObject.first, ttObject.second);
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static string GameHelpLJ2DDGGSel(string sClassID)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            List<TextTwo> dgObject = GameHelpLJ2SelFromCMS(sClassID);
            foreach (TextTwo ttObject in dgObject)
            {
                sbText.Append("{");
                sbText.AppendFormat("title:'{0}',url:'{1}'", ttObject.first, ttObject.second);
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static string GameHelpClassIDSel(int iGameID, string sClassName)
        {
            return GameDAL.GameHelpClassIDSel(iGameID, sClassName);
        }
    }
}

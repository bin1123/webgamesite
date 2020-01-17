using DataAccess;
using DataEnity;
using System.Text;

namespace Bussiness
{
    public class GameLoginBLL
    {
        public static int GameLoginAdd(GameLogin glObject)
        {
            return GameLoginDAL.GameLoginAdd(glObject); 
        }

        public static int GameLoginAdd(int iUserID,string sGameName,string sLoginIP,string sLoginUrl)
        {
            GameLoginTodayBLL.GameLoginAdd(iUserID, sLoginIP, sGameName);
            GameLogin glObject = new GameLogin();
            glObject.UserID = iUserID;
            glObject.GameName = sGameName;
            glObject.LoginIp = sLoginIP;
            glObject.LoginUrl = sLoginUrl;
            return GameLoginDAL.GameLoginAdd(glObject);
        }

        public static string GameLoginLastSel(int iUserID)
        {
            return GameLoginDAL.GameLoginLastSel(iUserID);
        }

        public static string[] GameLoginLastServerSel(int iUserID, int iGameID)
        {
            return GameLoginDAL.GameLoginLastServerSel(iUserID, iGameID);
        }

        public static string GameLoginLastServerJsonSel(int iUserID, int iGameID)
        {
            string[] sServerInfo = GameLoginLastServerSel(iUserID,iGameID);
            StringBuilder sbText = new StringBuilder("{root:[");
            sbText.Append("{");
            sbText.AppendFormat("servername:'{0}',abbre:'{1}'", sServerInfo[0], sServerInfo[1]);
            sbText.Append("},");
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static bool GameLoginIsLogin(int iUserID, string sGameAbbre)
        {
            return GameLoginDAL.GameLoginIsLogin(iUserID, sGameAbbre);
        }

        public static string GameLoginAbbreSel(int iUserID)
        {
            return GameLoginDAL.GameLoginAbbreSel(iUserID);
        }

        public static string GameLoginLastSelCJson(int iUserID)
        {
            return GameLoginDAL.GameLoginLastSelCJson(iUserID);
        }
    }
}

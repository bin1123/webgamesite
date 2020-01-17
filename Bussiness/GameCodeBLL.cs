
using DataAccess;

namespace Bussiness
{
    public class GameCodeBLL
    {
        public static int GameCodeCountSel(string sServerAbbre, string sCodeType)
        {
            return GameCodeDAL.GameCodeCountSel(sServerAbbre, sCodeType);
        }

        public static string GameCodeSelByUserID(string sServerAbbre, string sCodeType, int iUserID)
        {
            return GameCodeDAL.GameCodeSelByUserID(sServerAbbre, sCodeType, iUserID);
        }

        public static string GameCodeGet(string sServerAbbre, int iUserID, string sCodeType, string sIp)
        {
            return GameCodeDAL.GameCodeGet(sServerAbbre, iUserID, sCodeType, sIp);
        }
    }
}

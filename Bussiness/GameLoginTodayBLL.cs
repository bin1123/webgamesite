using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class GameLoginTodayBLL
    {
        public static int GameLoginAdd(int iUserID, string sLoginIP,string sGameAbbre)
        { 
            return GameLoginTodayDAL.GameLoginAdd(iUserID,sLoginIP,sGameAbbre);
        }
    }
}

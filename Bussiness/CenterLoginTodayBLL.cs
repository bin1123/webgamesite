using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class CenterLoginTodayBLL
    {
        public static int CenterLoginAdd(int iUserID, string sLoginIP)
        { 
            return CenterLoginTodayDAL.CenterLoginAdd(iUserID,sLoginIP);
        }
    }
}

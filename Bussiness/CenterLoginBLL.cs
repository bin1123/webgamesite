using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class CenterLoginBLL
    {
        public static int CenterLoginAdd(CenterLogin clObject)
        {
            return CenterLoginDAL.CenterLoginAdd(clObject); 
        }

        public static int CenterLoginAdd(int iUserID, string sLoginIP, string sAccount, string sFromUrl)
        {
            //if(iUserID > 999)
            //{
            //    CenterLoginTodayBLL.CenterLoginAdd(iUserID, sLoginIP);
            //}
            CenterLogin clObject = new CenterLogin();
            clObject.UserID = iUserID;            
            clObject.LoginIp = sLoginIP;            
            clObject.Account = sAccount;
            clObject.FromUrl = sFromUrl;
            return CenterLoginDAL.CenterLoginAdd(clObject);
        }

        //public static string GameLoginLastSel(int iUserID)
        //{
        //    return GameLoginDAL.GameLoginLastSel(iUserID);
        //}
    }
}

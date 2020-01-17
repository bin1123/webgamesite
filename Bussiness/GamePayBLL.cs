using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class GamePayBLL
    {
        public static int GamePayAdd(GamePay gpObject)
        {
            return GamePayDAL.GamePayAdd(gpObject); 
        }

        public static int GamePayAdd(string sTranIP,string sTranUrl,string sTranID,string sTranReturn,string sGameAbbre,int iUserID)
        {
            GamePay gpObject = new GamePay();
            gpObject.TranIP = sTranIP;
            gpObject.TranUrl = sTranUrl;
            gpObject.TranID = sTranID;
            gpObject.TranReturn = sTranReturn;
            gpObject.GameAbbre = sGameAbbre;
            gpObject.UserID = iUserID;
            return GamePayDAL.GamePayAdd(gpObject);
        }
    }
}

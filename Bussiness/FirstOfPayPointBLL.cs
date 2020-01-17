using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class FirstOfPayPointBLL
    {
        public static int Add(FirstOfPayPoint fpObject)
        {
            return FirstOfPayPointDAL.Add(fpObject); 
        }

        public static int Add(string sTranID,string sTranIP,string sTranUrl)
        {
            int iRes = 0;
            if(sTranID.Trim().Length > 30)
            {
                FirstOfPayPoint fpObject = new FirstOfPayPoint();
                fpObject.TranIP = sTranIP;
                fpObject.TranUrl = sTranUrl;
                fpObject.TranID = sTranID;
                iRes = FirstOfPayPointDAL.Add(fpObject);
            }
            return iRes;
        }
    }
}

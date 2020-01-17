using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class LastOfPayPointBLL
    {
        public static int Add(LastOfPayPoint lpObject)
        {
            return LastOfPayPointDAL.Add(lpObject); 
        }

        public static int Add(string sTranIP,char cTranFrom,string sFromUrl,string TranID)
        {
            int iRes = 0;
            if (TranID.Trim().Length > 30)
            {
                LastOfPayPoint lpObject = new LastOfPayPoint();
                lpObject.TranIP = sTranIP;
                lpObject.TranFrom = cTranFrom;
                lpObject.FromUrl = sFromUrl;
                lpObject.TranID = TranID;
                iRes = LastOfPayPointDAL.Add(lpObject);
            }
            return iRes;
        }
    }
}

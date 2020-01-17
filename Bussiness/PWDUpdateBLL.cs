using DataAccess;
using DataEnity;
using System;

namespace Bussiness
{
    public class PWDUpdateBLL
    {
        public static int PwdUpdateAdd(int userid, string sIP)
        {
            return PWDUpdateDAL.PwdUpdateAdd(userid, sIP);
        }

        public static DateTime PwdUpdateLastSel(int iUserID)
        {
            return PWDUpdateDAL.PwdUpdateLastSel(iUserID);
        }

        public static bool PwdUpdateVal(int iUserID,DateTime dtLoginTime)
        {
            if(iUserID < 1000)
            {
                return false;
            }
            DateTime dtPwdUpdateTime = PWDUpdateBLL.PwdUpdateLastSel(iUserID);
            int iRes = DateTime.Compare(dtPwdUpdateTime,dtLoginTime);
            if (iRes > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

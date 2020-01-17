using DataAccess;
using DataEnity;
using System.Text;

using Common;

namespace Bussiness
{
    public class QQUserBLL
    {
        public static int QQUserAdd(int iUserID, string sOpenID, string sFromUrl)
        {
            return QQUserDAL.QQUserAdd(iUserID, sOpenID, sFromUrl);
        }

        public static bool QQUserIsReg(string sOpenID)
        {
            string sRes = QQUserDAL.QQUserUseridSelByOpenID(sOpenID);
            if (sRes.Length > 4)
            {
                return false;
            }
            else {
                return true;
            }
        }

        public static int QQUserUseridSelByOpenID(string sOpenID)
        {
            string sUserID = QQUserDAL.QQUserUseridSelByOpenID(sOpenID);
            int iUserID = 0;
            int.TryParse(sUserID, out iUserID);
            return iUserID;
        }
    }
}

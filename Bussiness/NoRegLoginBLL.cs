using DataAccess;
using DataEnity;
using System.Text;

using Common;

namespace Bussiness
{
    public class NoRegLoginBLL
    {
        public static int NoRegLoginAdd(int iUserID, string sUnionID, string sGameName)
        {
            return NoRegLoginDAL.NoRegLoginAdd(iUserID, sUnionID, sGameName);
        }

        public static string AddUserid(string uniqueid, string userid)
        {
            StringBuilder sbText = new StringBuilder();
            sbText.Append(userid);
            sbText.Append(uniqueid);
            sbText.Append(ProvideCommon.MD5("dao50"));
            string sSign = ProvideCommon.MD5(sbText.ToString()).ToLower();
            string sPageUrl = string.Format("http://union.dao50.com/interface/nouser/addUserid.aspx?uniqueid={0}&userid={1}&sign={2}",uniqueid,userid,sSign);
            string sRes = ProvideCommon.GetPageInfo(sPageUrl);
            return sRes;
        }

        public static string NameReg(string username, string userid)
        {
            StringBuilder sbText = new StringBuilder();
            sbText.Append(username);
            sbText.Append(userid);
            sbText.Append(ProvideCommon.MD5("dao50"));
            string sSign = ProvideCommon.MD5(sbText.ToString()).ToLower();
            string sPageUrl = string.Format("http://union.dao50.com/interface/nouser/nameReg.aspx?username={0}&userid={1}&sign={2}", username, userid, sSign);
            string sRes = ProvideCommon.GetPageInfo(sPageUrl);
            return sRes;
        }

        public static bool NoRegLoginUnionidSel(string sUnionID)
        {
            string sRes = NoRegLoginDAL.NoRegLoginUnionidSel(sUnionID);
            if (sRes.Length > 0)
            {
                return false;
            }
            else {
                return true;
            }
        }
    }
}

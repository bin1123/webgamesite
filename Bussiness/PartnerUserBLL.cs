using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class PartnerUserBLL
    {
        public static string PartnerUserNameSel(int iUserID)
        {
            return PartnerUserDAL.PartnerUserNameSel(iUserID);
        }

        public static int PartnerUserIDSel(string sUserName,int iPId)
        {
            return PartnerUserDAL.PartnerUserIDSel(sUserName,iPId);
        }

        public static int PartnerUserPIDSel(int iUserID)
        {
            return PartnerUserDAL.PartnerUserPIDSel(iUserID);
        }

        public static int PartnerUserAdd(PartnerUser puObject)
        {
            return PartnerUserDAL.PartnerUserAdd(puObject);
        }

        public static int PartnerUserAdd(int pid,string regip,int userid,string username)
        {         
            PartnerUser puObject = new PartnerUser();
            puObject.pid = pid;
            puObject.regip = regip;
            puObject.userid = userid;
            puObject.username = username;
            return PartnerUserAdd(puObject);
        }

        public static string PartnerUserNameGet(string sAccount,int pid)
        {
            string sAbbre = PartnerBLL.PartnerAbbreSel(pid);
            string sUserName = string.Format("{0}:{1}",sAbbre,sAccount);
            return sUserName;
        }
    }
}

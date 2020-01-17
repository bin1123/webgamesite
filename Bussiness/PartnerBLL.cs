using DataAccess;

namespace Bussiness
{
    public class PartnerBLL
    {
        public static string PartnerAbbreSel(int pid)
        {
            return PartnerDAL.PartnerAbbreSel(pid);
        }

        public static string PartnerKeySel(int pid)
        {
            return PartnerDAL.PartnerKeySel(pid);
        }
    }
}
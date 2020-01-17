using System.Text;
using DataEnity;
using DataAccess;
using Common;

namespace Bussiness
{
    public class ChannelBLL
    {
        public static decimal FeeScaleSel(string sAbbre)
        {
           return ChannelDAL.FeeScaleSel(sAbbre);
        }

        public static decimal FeeScaleSelByID(int channelid)
        {
            return ChannelDAL.FeeScaleSelByID(channelid);
        }

        public static int ChannelIDSelByAbbre(string sAbbre)
        {
            return ChannelDAL.ChannelIDSelByAbbre(sAbbre);
        }
    }
}

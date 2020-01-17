using System.Collections.Generic;
using System.Text;

using DataEnity;
using DataAccess;

namespace Bussiness
{
    public class NoticeBLL
    {
        public static Dictionary<string, string> NoticeSel(string sAbbre)
        {
            return NoticeDAL.NoticeSel(sAbbre);
        }

        public static List<TextTwo> NoticeSelC(string sAbbre)
        {
            return NoticeDAL.NoticeSelC(sAbbre);
        }

        public static string JsonNoticeSel(string sAbbre)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            List<TextTwo> dgObject = NoticeSelC(sAbbre);
            foreach (TextTwo kObject in dgObject)
            {
                sbText.Append("{");
                sbText.AppendFormat("title:'{0}',url:'{1}'", kObject.first, kObject.second);
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static List<TextTwo> NoticeSelFromCMS(string sClassID)
        {
            return NoticeDAL.NoticeSelFromCMS(sClassID);
        }

        public static string JsonNoticeSelFromCMS(string sAbbre)
        {
            string sClassID = NoticeClassIDSel(sAbbre);
            StringBuilder sbText = new StringBuilder("{root:[");
            List<TextTwo> dgObject = NoticeSelFromCMS(sAbbre);
            foreach (TextTwo kObject in dgObject)
            {
                sbText.Append("{");
                sbText.AppendFormat("title:'{0}',url:'{1}'", kObject.first, kObject.second);
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }

        public static string NoticeClassIDSel(string sAbbre)
        {
            return NoticeDAL.NoticeClassIDSel(sAbbre);
        }
    }
}

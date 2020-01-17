using System.Text;
using System.Collections.Generic;

using DataEnity;
using DataAccess;

namespace Bussiness
{
    public class CodeBLL
    {
        public static List<Code> CodeTypeSel(string sGameAbbre)
        {
            return CodeDAL.CodeTypeSel(sGameAbbre);
        }

        public static string CodeUrlSel(string sAbbre)
        {
            return CodeDAL.CodeUrlSel(sAbbre);
        }

        public static string CodeJsonSel(string sGameAbbre)
        {
            StringBuilder sbText = new StringBuilder("{root:[");
            List<Code> clObject = CodeTypeSel(sGameAbbre);
            foreach (Code cObject in clObject)
            {
                sbText.Append("{");
                sbText.AppendFormat("name:'{0}',abbre:'{1}'", cObject.CodeName, cObject.Abbre);
                sbText.Append("},");
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            return sbText.ToString();
        }
    }
}

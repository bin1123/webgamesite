using DataEnity;
using DataAccess;

namespace Bussiness
{
    public class CodeHBLL
    {
        public static int CodeHAdd(CodeH chObject)
        {
            return CodeHDAL.CodeHAdd(chObject);
        }
    }
}

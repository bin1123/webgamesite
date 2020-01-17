using DataAccess;
using DataEnity;
using System.Text;

using Common;

namespace Bussiness
{
    public class RNameAutoBLL
    {
        public static int RNameAutoAdd(string sName, int iNum)
        {
            return RNameAutoDAL.RNameAutoAdd(sName, iNum);
        }

        public static int NumSelByName(string sRName)
        {
            return RNameAutoDAL.NumSelByName(sRName);
        }

        public static string AutoNameCreate(string sName)
        {
            if (UserBll.AccountIfAt(sName))
            {
                int iRAutoNum = RNameAutoBLL.NumSelByName(sName);
                int iNum = iRAutoNum + 1;
                string sAccount = string.Format("{0}_{1}", sName, iNum);//
                RNameAutoBLL.RNameAutoAdd(sName, iNum);
                if (UserBll.AccountIfAt(sAccount))
                {
                    return ProvideCommon.GenerateStringID();
                }
                else 
                {
                    return sAccount;
                }
            }
            else
            {
                return sName;
            }
        }
    }
}

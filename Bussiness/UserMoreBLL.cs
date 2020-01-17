using System.Text;

using DataAccess;
using DataEnity;
using Common;

namespace Bussiness
{
    public class UserMoreBLL
    {
        #region 增,删,改
        public static int UserMoreAdd(UserMore umObject)
        {
            return UserMoreDAL.UserMoreAdd(umObject);
        }

        public static int UserMoreDel(int iUID)
        {
            return UserMoreDel(iUID);
        }

        public static int UserMoreUpdate(UserMore umObject)
        {
            return UserMoreDAL.UserMoreUpdate(umObject);
        }
        #endregion

        #region 用户信息查询
        public static UserMore UserMoreSelByID(int iUserID)
        {
            return UserMoreDAL.UserMoreSelByID(iUserID);
        }


        public static string UserMoreJsonSel(int iUserID)
        {
            UserMore umObject = UserMoreSelByID(iUserID);
            StringBuilder sbText = new StringBuilder("{");
            sbText.AppendFormat("nickname:'{0}',",umObject.nickname.Trim());
            sbText.AppendFormat("birthday:'{0}',",umObject.birthday);
            sbText.AppendFormat("work:'{0}',",umObject.work);
            sbText.AppendFormat("phone:'{0}',",umObject.phone);
            sbText.AppendFormat("qq:'{0}',",umObject.qq.Trim());
            sbText.AppendFormat("sex:'{0}'",umObject.sex);
            sbText.Append("}");
            return sbText.ToString();
        }
        #endregion
    }
}

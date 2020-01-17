using System.Text;
using DataEnity;
using DataAccess;
using Common;

namespace Bussiness
{
    public class UserBll
    {
        #region 增,删,改
        public static int UserAdd(User uObject)
        {
           return UserDAL.UserAdd(uObject);
        }

        /// <summary>
        /// 用户中心用户注册
        /// </summary>
        /// <param name="sAccount">账号名</param>
        /// <param name="sPassWord">原文密码</param>
        /// <param name="iTypeID">用户来源</param>
        /// <returns></returns>
        public static int UserReg(string sAccount, string sPassWord)
        {
            DataEnity.User uObject = new User();
            uObject.account = sAccount;
            uObject.password = UserBll.PassWordMD5(sAccount, sPassWord);
            uObject.state = 0;
            uObject.typeid = 0;
            uObject.uid = UserAdd(uObject);
            return uObject.uid;
        }

        /// <summary>
        /// 外来用户注册
        /// </summary>
        /// <param name="sAccount">账号名</param>
        /// <param name="sPassWord">md5加密后的密码</param>
        /// <param name="iTypeID">用户来源</param>
        /// <returns></returns>
        public static int UserReg(string sAccount, string sPassWord, int iTypeID)
        {
            DataEnity.User uObject = new User();
            uObject.account = sAccount;
            uObject.password = sPassWord;
            uObject.state = 0;
            uObject.typeid = iTypeID;
            uObject.uid = UserAdd(uObject);
            return uObject.uid;
        }

        /// <summary>
        /// 无注册用户注册
        /// </summary>
        /// <param name="sAccount">账号名</param>
        /// <param name="iTypeID">用户来源</param>
        /// <param name="iState">是否激活</param>
        /// <returns></returns>
        public static int UserReg(string sAccount, string sPassWord, int iTypeID,int iState)
        {
            DataEnity.User uObject = new User();
            uObject.account = sAccount;
            uObject.password = sPassWord;
            uObject.state = iState;
            uObject.typeid = iTypeID;
            uObject.uid = UserAdd(uObject);
            return uObject.uid;
        }

        public static int UserDel(int iUserID)
        {
           return UserDAL.UserDel(iUserID);
        }

        public static int UserUpdatePWD(int iUserID, string sPassWord)
        {
            int iRes = 0;
            iRes = UserDAL.UserUpdatePWD(iUserID,sPassWord);
            if(iRes == 1)
            {
                string sIP = ProvideCommon.GetRealIP();
                PWDUpdateBLL.PwdUpdateAdd(iUserID, sIP);
            }
            return iRes;
        }
        #endregion

        #region 用户验证
        /// <summary>
        /// 用户注册信息验证
        /// </summary>
        /// <param name="sAccount"></param>
        /// <param name="sPassWord"></param>
        /// <returns></returns>
        public static string RegCheck(string sAccount,string sPassWord)
        {
            string sMsg = string.Empty;
            if ("unsafe string" == sAccount || "unsafe string" == sPassWord)
            {
                sMsg = "<script>alert('输入非法，请正确输入！！！')</script>";
                return sMsg;
            }
          
            int iAByTes = Encoding.Default.GetBytes(sAccount).Length;
            int iPBytes = Encoding.Default.GetBytes(sPassWord).Length;

            if (iAByTes < 4 || iPBytes < 4 || iAByTes > 32 || iPBytes > 32)
            {
                sMsg = "<script>alert('信息输入有误，请从新输入！！！')</script>";
                return sMsg;
            }

            if (sAccount.Length < 4 || sPassWord.Length < 4 || sAccount.Length > 16 || sPassWord.Length > 16)
            {
                sMsg = "<script>alert('信息输入有误，请从新输入！！！')</script>";
                return sMsg;
            }

            if (AccountVal(sAccount) > 0)
            {
                sMsg = "<script>alert('该账号已被注册，请更换账号！')</script>";
                return sMsg;
            }
            return sMsg;
        }
        #endregion

        public static string RegCheckText(string sAccount, string sPassWord)
        {
            string sMsg = string.Empty;
            if ("unsafe string" == sAccount || "unsafe string" == sPassWord)
            {
                sMsg = "输入非法，请正确输入！！！";
                return sMsg;
            }

            int iAByTes = Encoding.Default.GetBytes(sAccount).Length;
            int iPBytes = Encoding.Default.GetBytes(sPassWord).Length;

            if (iAByTes < 4 || iPBytes < 4 || iAByTes > 32 || iPBytes > 32)
            {
                sMsg = "信息输入有误，请从新输入！！！";
                return sMsg;
            }

            if (sAccount.Length < 4 || sPassWord.Length < 4 || sAccount.Length > 16 || sPassWord.Length > 16)
            {
                sMsg = "信息输入有误，请从新输入！！！";
                return sMsg;
            }

            if (AccountVal(sAccount) > 0)
            {
                sMsg = "该账号已被注册，请更换账号！'";
                return sMsg;
            }
            return sMsg;
        }

        public static string LoginCheck(string sAccount, string sPassWord)
        {
            string sMsg = string.Empty;            

            int iAByTes = Encoding.Default.GetBytes(sAccount).Length;
            int iPBytes = Encoding.Default.GetBytes(sPassWord).Length;

            if (iAByTes < 4 || iPBytes < 4 || iAByTes > 32 || iPBytes > 32)
            {
                sMsg = string.Format("<script>alert('信息输入有误|{0}，请从新输入！|{1}')</script>",sAccount,sPassWord);
            }
            else if(sAccount.Length < 4 || sPassWord.Length < 4 || sAccount.Length > 16 || sPassWord.Length > 16)
            {
                sMsg = "<script>alert('信息输入有误，长度不正确,请从新输入！！！')</script>";                
            }
            return sMsg;
        }

        public static string LoginCheckText(string sAccount, string sPassWord)
        {
            string sMsg = string.Empty;

            int iAByTes = Encoding.Default.GetBytes(sAccount).Length;
            int iPBytes = Encoding.Default.GetBytes(sPassWord).Length;

            if (iAByTes < 4 || iPBytes < 4 || iAByTes > 32 || iPBytes > 32)
            {
                sMsg = "信息输入有误，请重新输入";
            }
            else if (sAccount.Length < 4 || sPassWord.Length < 4 || sAccount.Length > 16 || sPassWord.Length > 16)
            {
                sMsg = "信息输入有误，长度不正确,请重新输入";
            }
            return sMsg;
        }

        #region 用户验证，用户昵称等查询
        /// <summary>
        /// 用户中心用户验证
        /// </summary>
        /// <param name="sAccount"></param>
        /// <param name="sPassWord"></param>
        /// <returns></returns>
        public static string UserVal(string sAccount,string sPassWord)
        {
           return UserDAL.UserVal(sAccount, sPassWord);
        }

        /// <summary>
        /// 用户名总查询
        /// </summary>
        /// <param name="sAccount">用户名</param>
        /// <param name="sPassWord">密码</param>
        /// <param name="iTypeID">用户类型</param>
        /// <returns></returns>
        public static int AccountsVal(string sAccount)
        {
            int iRes = -1;
            iRes = AccountVal(sAccount);
            return iRes;
        }
         
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="sAccount">账号</param>
        /// <param name="sPassWord">密码</param>
        /// <returns></returns>
        public static string PassWordMD5(string sAccount,string sPassWord)
        {
            StringBuilder sbText = new StringBuilder(sAccount.ToLower());
            sbText.Append(sPassWord);
            return ProvideCommon.MD5(sbText.ToString());
        }

        /// <summary>
        /// 账号不做处理加密密码
        /// </summary>
        /// <param name="sAccount">账号</param>
        /// <param name="sPassWord">密码</param>
        /// <returns></returns>
        public static string PassWordMD5New(string sAccount, string sPassWord)
        {
            StringBuilder sbText = new StringBuilder(sAccount);
            sbText.Append(sPassWord);
            return ProvideCommon.MD5(sbText.ToString());
        }

        public static int UserIDSel(string sAccount)
        {
            int iUserID = 0;
            if(sAccount.Length > 3 && sAccount.Length < 21)
            {
                iUserID = UserDAL.UserIDSel(sAccount);
            }
            return iUserID;
        }

        public static int UserIDSelByType(string sAccount,int iType)
        {
            return UserDAL.UserIDSelByType(sAccount,iType);
        }

        public static string AccountSel(int iUserID)
        {
            return UserDAL.AccountSel(iUserID);
        }

        public static int AccountVal(string sAccount)
        {
            return UserDAL.AccountVal(sAccount);
        }

        public static int PWDVal(int iUserID,string sPassWord)
        {
            return UserDAL.PWDVal(iUserID, sPassWord);
        }

        public static string RegTimeSel(int iUserID)
        {
            return UserDAL.RegTimeSel(iUserID);
        }

        public static bool UserAllVal(string sAccount, string sPassWord)
        {
            if(sAccount.Trim().Length < 4 && sPassWord.Trim().Length < 4)
            {
                return false;
            }
            string sMD5PassWord = PassWordMD5(sAccount, sPassWord);
            string sRes = UserVal(sAccount, sMD5PassWord);
            bool bRes = false;
            if (sRes == "0")
            {
                bRes = true;
            }
            else
            {
                string sMD5PassWordNew = PassWordMD5New(sAccount, sPassWord);
                if ("0" == UserVal(sAccount, sMD5PassWordNew))
                {
                    bRes = true;
                }
            }
            return bRes;
        }

        public static int UserUpdateNamePWD(int iUserID, string sAccount, string sPassWord)
        {
            return UserDAL.UserUpdateNamePWD(iUserID, sAccount, sPassWord);
        }

        public static string RegStateSel(int iUserID)
        {
            return UserDAL.RegStateSel(iUserID);
        }

        public static string UserTypeSel(int iUserID)
        {
            return UserDAL.UserTypeSel(iUserID);
        }

        public static bool AccountIfAt(string sAccount)
        {
            int iUserID = UserIDSel(sAccount);
            bool bRes = false;
            if(iUserID > 999)
            {
                bRes = true;
            } 
            return bRes;
        }

        public static bool AdminUserVal(int iUserID)
        {
            string sUserType = UserTypeSel(iUserID);
            bool bRes = false;
            if(sUserType == "4")
            {
                bRes = true; 
            }
            return bRes;
        }
        #endregion
    }
}

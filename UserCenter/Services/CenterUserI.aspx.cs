using System;
using System.Configuration;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    public partial class CenterUserI : pagebase.PageBase
    {
        private string sValKey = ConfigurationManager.AppSettings["UserValKey"].ToString(); 

        protected void Page_Load(object sender, EventArgs e)
        {            
            string sType = CYRequest.GetString("Type");
            string sAccount = CYRequest.GetString("Account",true);
            string sKey = CYRequest.GetString("Key");
            string sPassWord = CYRequest.GetString("PsssWord",true);//md5加密后的密码
            string sUForm = CYRequest.GetString("UFrom", true);
            if(ValKey(sAccount,sKey))
            {
                switch (sType)
                {
                    case "reg":
                        Response.Write(UserReg(sAccount, sPassWord,sUForm));
                        break;
                    case "login":
                        Response.Write(UserLogin(sAccount,sPassWord,sUForm));
                        break;
                    case "namesel":
                        Response.Write(UserNameSel(sAccount));
                        break;
                    case "UserInfoVal":
                        Response.Write(UsersVal(sAccount,sPassWord));
                        break;
                }
            }
        }

        /// <summary>
        /// 用户验证
        /// </summary>
        /// <param name="sAccount">账号</param>
        /// <param name="sPassWord">密码原文</param>
        /// <returns></returns>
        private string UsersVal(string sAccount,string sPassWord)
        {
            string sState = string.Empty;
            string sMD5PassWord = UserBll.PassWordMD5(sAccount, sPassWord);
            sState = UserBll.UserVal(sAccount, sMD5PassWord);
            if(sState != "0")
            {
                string sMD5PassWordNew = UserBll.PassWordMD5New(sAccount, sPassWord);
                sState = UserBll.UserVal(sAccount,sMD5PassWordNew);
            }
            return sState;
        }

        private string UserNameSel(string sAccount)
        {
            int iReturn = UserBll.AccountsVal(sAccount);
            if (iReturn > 0)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        private string UserLogin(string sAccount,string sPassWord,string sUserFrom)
        {
            string sRes = string.Empty;
            int iUserID = UserBll.UserIDSel(sAccount);
            if (iUserID < 1000)
            {
                sRes = UserReg(sAccount, sPassWord, sUserFrom);
            }
            else
            {
                UserLogin(sAccount);
                sRes = "0";
            }
            return sRes;
        }

        private void UserLogin(string sAccount)
        { 
            int iUID = UserBll.UserIDSel(sAccount);
            string sPageUrl = Request.Url.ToString();
            LoginStateSet(sAccount, iUID, sPageUrl); 
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="sAccount">用户账号</param>
        /// <param name="sPassWord">md5用户密码</param>
        /// <param name="sUserFrom">用户来源</param>
        /// <returns></returns>
        private string UserReg(string sAccount,string sPassWord,string sUserFrom)
        {
            int iTypeID = WebConfig.BaseConfig.UserForm(sUserFrom);
            string sRes = string.Empty;
            //查询账号是否存在
            int iUID = UserBll.UserIDSel(sAccount);
            if (iUID < 1)
            {
                iUID = UserBll.UserReg(sAccount, sPassWord, iTypeID);
                if (iUID > 1000)
                {
                    UserInfoBLL.UserInfoAdd(iUID);
                    string sPageUrl = Request.Url.ToString();
                    LoginStateSet(sAccount,iUID,sPageUrl); 
                    sRes = "0";
                }
                else
                {
                    sRes = "1";
                }
            }
            else
            {
                sRes = "2";
            }
            return sRes;
        }

        private bool ValKey(string sAccount,string sKey)
        { 
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sAccount);
            sbText.Append(sValKey);

            string sMD5Key = ProvideCommon.MD5(sbText.ToString());
            if (("" != sAccount) && sMD5Key == sKey)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

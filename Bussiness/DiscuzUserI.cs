using System.Text;
using Common;

namespace Bussiness
{
    /// <summary>
    /// DiscuzBBS用户接口
    /// </summary>
    public class DiscuzUserI
    {
        public static string BBSReg(string sAccount,string sPassWord,string sKey)
        {
            StringBuilder sbText = new StringBuilder();
            sbText.Append("http://bbs.dao50.com/services/BBSUserI.aspx?Type=reg&UserName=");
            sbText.Append(sAccount);
            sbText.Append("&PassWord=");
            sbText.Append(ProvideCommon.MD5(sPassWord));
            sbText.Append("&Email=");
            sbText.Append("&Key=");
            sbText.Append(MD5Key(sAccount,sKey));
            return ProvideCommon.GetPageInfo(sbText.ToString());
        }

        public static string BBSLogin(string sAccount, string sPassWord,string sKey)
        {
            StringBuilder sbText = new StringBuilder();
            sbText.Append("http://bbs.dao50.com/services/BBSUserI.aspx?Type=login&UserName=");
            sbText.Append(sAccount);
            sbText.Append("&PassWord=");
            sbText.Append(ProvideCommon.MD5(sPassWord));
            sbText.Append("&Email="); 
            sbText.Append("&Key=");
            sbText.Append(MD5Key(sAccount, sKey));
            return sbText.ToString();
        }

        private static string MD5Key(string sAccount,string sKey)
        {
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sAccount);
            sbText.Append(sKey);
            return ProvideCommon.MD5(sbText.ToString());
        }
    }
}

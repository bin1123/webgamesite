using System.Configuration;

namespace UserCenter.WebConfig
{
    public class BaseConfig
    {
        public static string sWebUrl = ConfigurationManager.AppSettings["FileUrl"].ToString();
        public static string sWUrl = ConfigurationManager.AppSettings["WebUrl"].ToString();
        public static string sIsGift = ConfigurationManager.AppSettings["GiftSwitch"].ToString();

        /// <summary>
        /// 用户来源id获取
        /// </summary>
        /// <param name="sFromName">用户来源英文缩写</param>
        /// <returns></returns>
        public static int UserForm(string sFromName)
        {
            int iUserFrom = 0;
            switch(sFromName)
            {
                case "bbs":
                    iUserFrom = 1;
                    break;
            }
            return iUserFrom;
        }        
    }
}

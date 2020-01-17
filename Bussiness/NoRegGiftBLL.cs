using DataAccess;
using DataEnity;
using System.Text;

using Common;

namespace Bussiness
{
    public class NoRegGiftBLL
    {
        public static int NoRegGiftAdd(int iUserID, int iLevel, string sGameName)
        {
            return NoRegGiftDAL.NoRegGiftAdd(iUserID, iLevel, sGameName);
        }

        public static bool NoRegGiftUserIDSel(int iUserID)
        {
            string sRes = NoRegGiftDAL.NoRegGiftUseridSel(iUserID);
            if (sRes.Length > 0)
            {
                return false;
            }
            else {
                return true;
            }
        }

        public static string ULevelSel(int iUserID,string sGameAbbre)
        {
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sGameAbbre);
            sbText.Append(iUserID.ToString());
            sbText.Append(ProvideCommon.MD5("dao50"));
            string sSign = ProvideCommon.MD5(sbText.ToString()).ToLower();
            string sUrl = string.Format("http://union.dao50.com/interface/nouser/getRole.aspx?abbre={0}&userid={1}&sign={2}",sGameAbbre,iUserID.ToString(),sSign);
            string sRes = ProvideCommon.GetPageInfo(sUrl);
            return sRes;
        }

        public static string GiftToGame(int iUserID, string sGameAbbre,string sAccount,int iPoint,string sTranIP)
        {
            string sRes = string.Empty;
            string sGiftPointRes = TransPBLL.GiftPointsSend(iUserID, iPoint, sTranIP);
            if (sGiftPointRes == "0")
            {
                string sGiftGameRes = PayAll.GamePay(sGameAbbre, iUserID, sAccount, iPoint, "", iUserID);
                if (sGiftGameRes.Split('|')[0] == "0")
                {
                    sRes = "0"; 
                }
                else
                {
                    UserPointsBLL.UGiftPointUpdate(iUserID, 0);
                    sRes = string.Format(string.Format("-2|{0}",sGiftGameRes));
                }
            }
            else
            {
                sRes = string.Format("-1|{0}", sGiftPointRes);
            }
            return sRes;
        }
    }
}

using DataAccess;
using DataEnity;
using System.Text;
using System;

using Common;

namespace Bussiness
{
    public class FirstGiftBLL
    {
        public static int GiftAdd(int iUserID, int iLevel, string sGameName)
        {
            return FirstGiftDAL.GiftAdd(iUserID, iLevel, sGameName);
        }

        public static bool GiftUserIDSel(int iUserID,string sGameName)
        {
            string sRes = FirstGiftDAL.GiftUserIDSel(iUserID,sGameName);
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

        public static string GiftToGame(int iUserID, string sGameAbbre,string sAccount,int iPoint,string sTranIP,string sChannelAbbre)
        {
            int iChannelID = ChannelBLL.ChannelIDSelByAbbre(sChannelAbbre);
            if(iChannelID < 20)
            {
                return "-3";
            }
            string sGiftPointRes = TransPBLL.FirstGiftSend(iUserID, iPoint, sTranIP, iChannelID);
            string sRes = string.Empty;
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

        public static bool valTime(string sStartTime)
        {
            DateTime dtNow = DateTime.Now;
            //DateTime dtStartTime = Convert.ToDateTime(sStartTime).AddDays(1);
            DateTime dtStartTime = Convert.ToDateTime(sStartTime).AddHours(6);
            int i = dtNow.CompareTo(dtStartTime);
            if (i < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GiftStateSel(int iUserID, string sGameName)
        {
            return FirstGiftDAL.GiftStateSel(iUserID, sGameName);
        }

        public static int GiftStateUpate(int iUserID, string sGameName, int iState)
        {
            return FirstGiftDAL.GiftStateUpate(iUserID, sGameName, iState);
        }
    }
}

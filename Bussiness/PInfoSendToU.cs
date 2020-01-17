using System;
using System.Text;
using System.Data;
using DataAccess;
using DataEnity;

using Common;

namespace Bussiness
{
    public class PInfoSendToU
    {
        public static void pointpaysend(string sPTranID)
        {
            TransP tpObject = TransPBLL.UserTranSel(sPTranID);
            string sReturn = string.Empty;
            if(tpObject.state == 1)
            {
                string sUrl = "http://union.dao50.com/tarns/transPoints.aspx";
                StringBuilder sbText = new StringBuilder();
                sbText.AppendFormat("orderid={0}&",tpObject.TranID.Trim());
                sbText.AppendFormat("userid={0}&",tpObject.UserID);
                sbText.AppendFormat("ordertime={0}&",tpObject.TranTime);
                sbText.AppendFormat("channel={0}&",tpObject.ChannelID);
                sbText.AppendFormat("price={0}&",tpObject.price);
                sbText.AppendFormat("points={0}&",tpObject.TranPoints);
                sbText.AppendFormat("giftpoints={0}&",tpObject.TranGiftPoints);
                sbText.AppendFormat("tranip={0}",tpObject.TranIP);
                sReturn = string.Format("{0}?{1}",sUrl,sbText.ToString());
            }
            ProvideCommon.GetPageInfo(sReturn);
        }

        public static void gamepaysend(string sGTranID)
        {
            TransG tgObject = TransGBLL.UserTranSel(sGTranID);
            string sReturn = string.Empty;
            if (tgObject.state == 1)
            {
                string sUrl = "http://union.dao50.com/tarns/transGame.aspx";
                int price = tgObject.TranPoints / 10;
                StringBuilder sbText = new StringBuilder();
                sbText.AppendFormat("orderid={0}&", tgObject.TranID.Trim());
                sbText.AppendFormat("userid={0}&", tgObject.UserID);
                sbText.AppendFormat("gameid={0}&", tgObject.GameID);
                sbText.AppendFormat("ordertime={0}&", tgObject.TranTime);
                sbText.AppendFormat("gameuserid={0}&", tgObject.GUserID);
                sbText.AppendFormat("price={0}&", price.ToString());
                sbText.AppendFormat("points={0}&", tgObject.TranPoints);
                sbText.AppendFormat("giftpoints={0}&", tgObject.TranGiftPoints);
                sbText.AppendFormat("tranip={0}", tgObject.TranIP);
                sReturn = string.Format("{0}?{1}", sUrl, sbText.ToString());
            }
            ProvideCommon.GetPageInfo(sReturn);
        }
    }
}

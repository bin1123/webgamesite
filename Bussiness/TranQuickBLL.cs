using System.Text;
using System.Data;

using DataAccess;
using DataEnity;

namespace Bussiness
{
    public class TranQuickBLL
    {
        public static int TranQuickAdd(TranQuick tqObject)
        {
            return TranQuickDAL.TranQuickAdd(tqObject);
        }

        public static void TranQuickAdd(string sGTranID, string sPTranID)
        {
            if (sGTranID.Length > 1 && sPTranID.Length > 1)
            {
                TranQuick tqObject = new TranQuick();
                tqObject.GTranID = sGTranID;
                tqObject.PTranID = sPTranID;
                TranQuickAdd(tqObject);
            }
        }

        public static int TranQuickUpdateP(string sPTranID)
        {
            return TranQuickDAL.TranQuickUpdateP(sPTranID);
        }

        public static int TranQuickUpdateG(string sGTranID)
        {
            return TranQuickDAL.TranQuickUpdateG(sGTranID);
        }

        public static string TranQuickGTranIDSel(string sPTranID)
        {
            return TranQuickDAL.TranQuickGTranIDSel(sPTranID).Trim();
        }

        public static string TransQuickStateSelByP(string sPTranID)
        {
            return TranQuickDAL.TransQuickStateSelByP(sPTranID);
        }

        public static string TransQuickLogSel(int iUserID)
        {
            IDataReader drTranQuick = TranQuickDAL.TransQuickLogSel(iUserID);
            StringBuilder sbText = new StringBuilder("{root:[");
            while(drTranQuick.Read())
            {
                sbText.Append("{");
                sbText.AppendFormat("time:'{0}',gamepoints:'{1}',channel:'{2}',game:'{3}',server:'{4}'", drTranQuick["time"].ToString(), drTranQuick["gamepoints"].ToString(),drTranQuick["channel"].ToString().Trim(),drTranQuick["game"].ToString(),drTranQuick["server"].ToString());
                sbText.Append("},");
            }
            if (sbText.Length == 7)
            {
                return "";
            }
            int iIndex = sbText.Length - 1;
            sbText.Remove(iIndex, 1);
            sbText.Append("]}");
            drTranQuick.Close();
            drTranQuick.Dispose();
            return sbText.ToString();
        }
    }
}

using System;
using Bussiness;
using Common;

namespace UserCenter.Pay
{
    public partial class PayPSucc : pagebase.PageBase
    {       
        protected int iPayPoints = 0;
        protected int iUserPoints = 0;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sTranID = CYRequest.GetQueryString("TranID");
            iPayPoints = TransPBLL.TranPSelPointByID(sTranID);
            int iUserID = TransPBLL.TranPSelUserIDByID(sTranID);
            iUserPoints = UserPointsBLL.UPointAllSel(iUserID);
            int iLUserID = GetUserID();
            if (iLUserID > 999 && (iUserID == iLUserID))
            {
                SetPoints(iUserPoints);
            }
            string sFromHost = GetFromHost();
            if (sFromHost.Length > 5)
            {
                string sQueryString = string.Format("{0}|{1}|{2}", sTranID, iPayPoints, iUserPoints);
                string sEncodeQueryString = Server.UrlEncode(sQueryString);
                string sGoUrl = string.Format("http://{0}/PayPSucc.html?{1}", sFromHost, sEncodeQueryString);
                Response.Redirect(sGoUrl, true);
                return;
            }
        }
    }
}

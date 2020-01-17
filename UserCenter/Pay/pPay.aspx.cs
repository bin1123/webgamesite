using System;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Pay
{
    public partial class pPay : pagebase.PageBase
    {
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected int iUserPoints = 0;
        protected string sPId = string.Empty;
        protected string sUserName = string.Empty;
        protected string sErr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sAccount = CYRequest.GetString("account");
            sPId = CYRequest.GetString("agentid");
            string tstamp = CYRequest.GetString("tstamp");
            string sign = CYRequest.GetString("sign");
            int pid;
            int.TryParse(sPId, out pid);

            TimeSpan tsNow = new TimeSpan(DateTime.Now.Ticks);
            long lTime = long.Parse(tstamp + "0000000");
            TimeSpan tsGet = new TimeSpan(lTime);
            int iMinutes = tsNow.Subtract(tsGet).Minutes;

            string sTicket = string.Empty;
            if(sAccount == "" || sPId == "" || tstamp == "" || sign == "")
            {
                sErr = "null";
            }
            else if (sAccount.Length > 20 || sAccount.Length < 6 || sAccount == "unsafe string")
            {
                sErr = "name"; 
            }
            else if (pid < 1)
            {
                sErr = "pid";
            }
            else if (iMinutes > 20)
            {
                sErr = "time";
            }
            else
            {
                sTicket = PartnerBLL.PartnerKeySel(pid);
                if (sTicket == null || sTicket.Length < 1)
                {
                    sErr = "ticket";
                }
            }

            if (string.IsNullOrEmpty(sErr))
            {
                StringBuilder sbText = new StringBuilder(50);
                sbText.Append(sAccount);
                sbText.Append(sPId);
                sbText.Append(tstamp);
                sbText.Append(sTicket);
                string sValSign = ProvideCommon.MD5(sbText.ToString());// md5(account + agentid  + tstamp  +  TICKEY_PAY)
                if (sign != sValSign)
                {
                    sErr = "sign";
                }
            }

            if (string.IsNullOrEmpty(sErr))
            {
                int iUserID = PartnerUserBLL.PartnerUserIDSel(sAccount,pid);
                if (iUserID < 1000)
                {
                    sErr = "userid";
                }
                else
                {
                    sUserName = sAccount;
                    iUserPoints = UserPointsBLL.UPointSel(iUserID);
                    if (iUserPoints > 0)
                    {
                        UserPointsBLL.UPointCheck(iUserID);
                    }
                    string sPartnerAbbre = PartnerBLL.PartnerAbbreSel(pid);
                    if (sPartnerAbbre == null || sPartnerAbbre.Length < 1)
                    {
                        sErr = "partner";
                    }
                    string sAccountC = string.Format("{0}:{1}", sPartnerAbbre, sAccount);                    
                    string sPageUrl = Request.Url.ToString();
                    LoginStateSet(sAccountC, iUserID, sPageUrl);
                }
            }
        }
    }
}

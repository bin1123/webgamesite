using System;
using System.Text;
using Common;
using Bussiness;

namespace UserCenter.xsk
{
    public partial class k : pagebase.PageBase
    {
        //protected string sMsg = string.Empty;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sAccount = CYRequest.GetString("account");
            string sPId = CYRequest.GetString("agentid");
            string sign = CYRequest.GetString("sign");
            int pid = 0;
            int.TryParse(sPId, out pid);
            if(pid == 1)
            {
                string sTicket = PartnerBLL.PartnerKeySel(pid); 
                StringBuilder sbText = new StringBuilder(50);
                sbText.Append(sAccount);
                sbText.Append(sPId);
                sbText.Append(sTicket);
                string sValSign = ProvideCommon.MD5(sbText.ToString());//md5(account + agentid  +  TICKEY_PAY) 
                if (sign != sValSign)
                {
                    Response.Redirect("http://www.682.com/Home/xsk");
                }
                else
                {
                    string sPartnerAbbre = PartnerBLL.PartnerAbbreSel(pid);
                    string sAccountC = string.Format("{0}:{1}", sPartnerAbbre, sAccount);                    
                    int iUserID = PartnerUserBLL.PartnerUserIDSel(sAccount,pid);
                    string sPageUrl = Request.Url.ToString();
                    LoginStateSet(sAccountC, iUserID, sPageUrl);
                }
            }
            else if (!(LoginSessionVal() || isLoginCookie()))
            {
                Response.Redirect("http://www.682.com/Home/xsk");
            }
        }
    }
}

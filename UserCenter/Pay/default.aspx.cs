using System;
using Bussiness;
using Common;

namespace UserCenter.Pay
{
    public partial class PIndex : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sUserName = string.Empty;
        protected string sIsLogin = string.Empty;
        protected string sIsGift = WebConfig.BaseConfig.sIsGift;
        protected int iUserPoints = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sUID = CYRequest.GetQueryString("uid").Trim();
            string sType = string.Empty;
            if(sUID.Length > 3)
            {
                string sHava7UID = "18967189|18967207|18967219|18967230|18967236|18967241";
                if (sHava7UID.IndexOf(sUID) > -1)
                {
                    Response.Redirect("http://www.7hava.com/");
                    return;
                }
                sType = GetUserType(sUID);
            }
            string sGoUrl = string.Empty;
            string sQueryString = Request.Url.Query;
            switch(sType)
            {
                case "2":
                    sGoUrl = string.Format("http://game.niuzei.com/pay/{0}", sQueryString);
                    break;
                case "20":
                    sGoUrl = string.Format("http://www.wanyouxi123.com/pay.html{0}", sQueryString);
                    break;
                case "21":
                    sGoUrl = string.Format("http://www.99wanyouxi.com/pay.html{0}", sQueryString);
                    break;
                case "22":
                    sGoUrl = string.Format("http://www.50shouyou.com/pay.html{0}", sQueryString);
                    break;
                case "23":
                    sGoUrl = string.Format("http://www.50dao.com/pay.html{0}", sQueryString);
                    break;
                case "24":
                    sGoUrl = string.Format("http://www.niu50.com/pay.html{0}", sQueryString);
                    break;
                case "25":
                    sGoUrl = string.Format("http://www.99nbwan.com/pay.html{0}", sQueryString);
                    break;
                case "26":
                    sGoUrl = string.Format("http://www.97nbwan.com/pay.html{0}", sQueryString);
                    break;
            }
            if (sGoUrl.Length > 5)
            {
                Response.Redirect(sGoUrl, true);
                return;
            }
            if (LoginSessionVal() || isLoginCookie())
            {
                int iUserID = GetUserID();
                sUserName = GetAccount();
                iUserPoints = UserPointsBLL.UPointAllSel(iUserID);
                if(iUserPoints > 0)
                {
                    UserPointsBLL.UPointCheck(iUserID);
                }
                SetPoints(iUserPoints);
                sIsLogin = "y";
            }
            else
            {
                sIsLogin = "n";
            }
        }
    }
}

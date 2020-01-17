using System;
using Common;
using Bussiness;

namespace UserCenter.frame
{
    public partial class g_left_nav_noreg : pagebase.PageBase
    {
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sMsg = string.Empty;
        protected string sServerName = string.Empty;
        protected string sAccountT = string.Empty;
        protected string sGameName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                sGameName = CYRequest.GetString("gn");
                sAccountT = GetAccount();
                if (sGameName.Length > 0 && sAccountT.IndexOf("?") == 0)
                {
                    sServerName = ServerBLL.ServerTitleNoSNameSel(sGameName);
                }
                else
                {
                    Response.Redirect("http://www.dao50.com/", false);
                }
            }
        }
    }
}

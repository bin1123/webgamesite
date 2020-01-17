using System;

using Common;

namespace UserCenter.Services
{
    public partial class MultiExit : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sUrl = string.Empty;
            if (Request.ServerVariables["HTTP_Referer"] != null)
            {
                ClearUsersInfo();
                sUrl = Request.ServerVariables["HTTP_Referer"];
                string sHost = ProvideCommon.getHost(sUrl);
                string sToUrl = string.Format("http://{0}/usercookie.aspx?type=del&GoUrl={1}",sHost ,sUrl);
                Response.Redirect(sToUrl,true);
            }
        }
    }
}

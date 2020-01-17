using System;

using Common;
using Bussiness;

namespace UserCenter.UCenter
{
    public partial class TransSel : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl; 
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(LoginSessionVal() || isLoginCookie()))
            {
                Response.Redirect(string.Format("../Default.aspx?url={0}",Request.Url.ToString()),true);
            }
        }
    }
}

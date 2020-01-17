using System;
using System.Web;
using Common;
using Bussiness;

namespace UserCenter.frame
{
    public partial class g_mainframe_ftz : System.Web.UI.Page
    {
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;
        protected string sGame = string.Empty;
        //protected string noresize = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                string sGameName = CYRequest.GetString("gn");
                if (sGameName == "" || sGameName == "unsafe string")
                {
                    Response.Redirect(string.Format("{0}/yxzx", sWUrl), false);
                }
                else
                {
                    sGame = sGameName;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;

namespace UserCenter.frame
{
    public partial class g_mainframe_jy : System.Web.UI.Page
    {
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;
        protected string sGame = string.Empty;
        //protected string noresize = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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

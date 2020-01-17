using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Bussiness;

namespace UserCenter.Pay
{
    public partial class TenCallBack : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sRes = TenPayBuy.TenPaySubmit(Context);
            if(sRes != "0")
            {
                Response.Write(sRes);
            }
        }
    }
}

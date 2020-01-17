using System;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    public partial class gamesinfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string agentid = CYRequest.GetString("agentid"); 
            string sign = CYRequest.GetString("sign");//md5(agentid + TICKEY)
            string TICKEY = "dao50GSel_ko(*dg_)12:?do";
            StringBuilder sbText = new StringBuilder(50);
            sbText.Append(agentid);
            sbText.Append(TICKEY);
            string sValSign = ProvideCommon.MD5(sbText.ToString());
            if (sign == sValSign)
            {
                Response.Write(GameBLL.GameDBXml());
            }
            else
            {
                Response.Write("-1");
            }
        }
    }
}

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
using System.Text;

using Common;
using Bussiness;

namespace UserCenter.Pay
{
    public partial class QuickTenCallback : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sRes = TenPayBuy.QuickTenPaySubmit(Context);
            
            //StringBuilder sbText = new StringBuilder();            
            //sbText.Append(Server.MapPath("~/Log"));
            //sbText.Append("/Pay");
            //string sPath = sbText.ToString();
            //ProvideCommon pcObject = new ProvideCommon();
            //sbText.Remove(0, sbText.Length);
            //sbText.AppendFormat("{0},{1},{2}", Request.Url.ToString(), DateTime.Now.ToString(), sRes);
            //pcObject.WriteLogFile(sPath, "QuickTenCallback", sbText.ToString());

            StringBuilder sbHtml = new StringBuilder();
            switch(sRes)
            {
                case "0":
                    break;
                case "-1": 
                    sbHtml.Append("<html><head><script language=\"javascript\">");
                    sbHtml.Append("alert('支付失败');");
                    sbHtml.Append("</script></head><body></body></html>");
                    Response.Write(sbHtml.ToString());
                    break;
                case "-2": 
                    sbHtml.Append("<html><head><script language=\"javascript\">");
                    sbHtml.Append("alert('认证签名失败');");
                    sbHtml.Append("</script></head><body></body></html>");
                    Response.Write(sbHtml.ToString());
                    break;
                case "-3":
                    sbHtml.Append("<html><head><meta name=\"TENCENT_ONLINE_PAYMENT\" content=\"China TENCENT\"><script language=\"javascript\">");                    
                    sbHtml.Append("alert('武林币充值成功！游戏充值失败！请进入武林币兑换页面进行兑换！');");
                    sbHtml.AppendFormat("window.location.href='{0}';","default.aspx");
                    sbHtml.Append("</script></head><body></body></html>");
                    Response.Write(sbHtml.ToString());
                    break;
                default:
                    Response.Write(sRes);
                    break;
            }
        }
    }
}

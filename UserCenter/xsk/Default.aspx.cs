using System;
using Bussiness;
using Common;

namespace UserCenter.xsk
{
    public partial class Default : pagebase.PageBase
    {
        //protected string sMsg = string.Empty;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                int iUserID = GetUserID();
                string sUserName = GetAccount();
            }
            else
            {
                Response.Redirect(string.Format("../Default.aspx?url={0}", Request.Url.ToString()), true);
                //sMsg = string.Format("<script>alert('请登录平台，谢谢！')</script><script>location.href='{0}/Default.aspx?url={1}'</script>", sRootUrl,Request.Url.ToString());
                //return;
            }
        }
    }
}

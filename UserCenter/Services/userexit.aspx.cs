using System;

namespace UserCenter.Services
{
    public partial class userexit : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sUrl = string.Empty;
            if (Request.ServerVariables["HTTP_Referer"] != null)
            {
                sUrl = Request.ServerVariables["HTTP_Referer"];
                ClearUsersInfo();
                string sWWWExitJS = "<script src='http://www.dao50.com/usercookie.aspx?type=del'></script>";
                string sToUrl = string.Format("<script>location.href='{0}'</script>", sUrl);
                Response.Write(string.Format("{0}{1}", sWWWExitJS, sToUrl));
            }
            //string sUrl = CYRequest.GetString("url");
            //if (sUrl != "" && sUrl != "unsafe string")
            //{
            //    ClearUsersInfo();
            //    Response.Redirect(sUrl,true);
            //}
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;

using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    public partial class searchPass : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "POST")
            {
                string account = CYRequest.GetString("account");
                string question = CYRequest.GetString("question");
                string answer = CYRequest.GetString("answer");

                SqlParameter fanhui = new SqlParameter();
                fanhui.ParameterName = "@returnValue";
                fanhui.Direction = ParameterDirection.ReturnValue;
                SqlParameter[] pm = new SqlParameter[4];
                pm[0] = fanhui;
                pm[1] = new SqlParameter("@userName", SqlDbType.VarChar);
                pm[1].Value = account;
                pm[2] = new SqlParameter("@question", SqlDbType.VarChar);
                pm[2].Value = question;
                pm[3] = new SqlParameter("@answer", SqlDbType.VarChar);
                pm[3].Value = answer;
                UserInfoBLL.getn("SP_UserInfo_searchPass", pm);

                if (fanhui.Value.ToString() == "1")
                {
                    sMsg = "<script>alert('提示问题和答案不正确!');</script>";
                    return;
                }
                else if (fanhui.Value.ToString() == "2")
                {
                    SetSearchName(account);
                    Response.Redirect("updatePass.aspx",true);
                }
                else if (fanhui.Value.ToString() == "3")
                {
                    sMsg = "<script>alert('用户名错误！');</script>";
                }
            }
        }

        private void SetSearchName(string sName)
        {
            string sKey = "1$3*dwq12vc.";
            string sSign = ProvideCommon.MD5(string.Format("{0}{1}",sName,sKey));
            Response.Cookies["searchname"].Value = string.Format("{0}|{1}",sName,sSign);
            Response.Cookies["searchname"].Expires = DateTime.Now.AddMinutes(30);
        }
    }
}

using System;
using System.Text;

using Common;
using Bussiness;

namespace UserCenter.Services
{
    public partial class MultiUserLoginTo : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RequestType == "GET" || Request.RequestType == "POST")
            {
                string skey = "Pd23AS!2lh*2B";
                string sUserName = CYRequest.GetString("username");
                string sPassWord = CYRequest.GetString("password");
                string sPage = Server.UrlDecode(CYRequest.GetString("page"));
                string sSign = CYRequest.GetString("sign");
                if (sSign != "")
                {
                    StringBuilder sbText = new StringBuilder();
                    sbText.AppendFormat("{0}{1}{2}",sUserName,sPassWord,skey);
                    string sValSign = ProvideCommon.MD5(sbText.ToString());
                    if (sValSign == sSign)
                    {
                        if (UserBll.UserAllVal(sUserName, sPassWord))
                        {
                            string sPageUrl = Request.Url.ToString();
                            int iUserID = UserBll.UserIDSel(sUserName);
                            LoginStateSet(sUserName, iUserID, sPageUrl);
                            if (sPage != "")
                            {
                                string sHost = ProvideCommon.getHost(sPage);
                                int iUserPoint = GetUPoints();
                                string sPP = ProvideCommon.getMultiPP(iUserID);
                                Response.Redirect(string.Format("http://{0}/usercookie.aspx?un={1}&point={2}&GoUrl={3}&pp={4}",sHost,sUserName,iUserPoint,sPage,sPP),true);
                            }
                            else
                            {
                                Response.Write("page is null");
                            }
                        }
                        else
                        {
                            Response.Write("login val err");
                        }
                    }
                    else
                    {
                        Response.Write("sign val err");
                    }
                }
            }
        }
    }
}

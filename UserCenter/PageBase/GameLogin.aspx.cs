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

using Common;
using Bussiness;

namespace UserCenter.PageBase
{
    public partial class GameLogin : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sAccount = string.Empty;
        protected string sMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            sAccount = GetAccount();
            if (Request.HttpMethod == "POST")
            {
                string sAccountVal = CYRequest.GetFormString("account");
                string sPassWord = CYRequest.GetFormString("pwd");
                string sMD5PassWord = UserBll.PassWordMD5(sAccountVal, sPassWord);
                string sRes = UserBll.UserVal(sAccountVal, sMD5PassWord);
                string sPageUrl = Request.Url.ToString();
                if (sRes == "0")
                {
                    int iUserID = UserBll.UserIDSel(sAccountVal);
                    LoginStateSet(sAccountVal, iUserID, sPageUrl);
                }
                else
                {
                    string sMD5PassWordNew = UserBll.PassWordMD5New(sAccountVal, sPassWord);
                    if ("0" == UserBll.UserVal(sAccountVal, sMD5PassWordNew))
                    {
                        int iUserID = UserBll.UserIDSel(sAccountVal);
                        LoginStateSet(sAccountVal, iUserID, sPageUrl);
                    }
                }
                string sUrl = string.Empty;
                if (Request.ServerVariables["HTTP_Referer"] != null)
                {
                    sUrl = Request.ServerVariables["HTTP_Referer"];
                    Response.Redirect(sUrl, true);
                }
                return;
            }
        }
    }
}

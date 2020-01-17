using System;
using System.Text;
using System.Configuration;

using Bussiness;
using DataEnity;
using Common;

namespace UserCenter.UCenter
{
    public partial class reg : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                RegButton.ImageUrl = string.Format("{0}/wldFolder/images/lijizhuce.jpg", sWebUrl);
            }
        }

        protected void RegButton_Click(object sender, EventArgs e)
        {
            string sAccount = CYRequest.GetFormString("txz").Trim();
            string sPassWord = CYRequest.GetFormString("pwdtwo").Trim();

            string sValCode = Request["ValCode"].ToString();
            string sRes = ValCheckCode(sValCode);
            if(sRes != "0")
            {
                StringBuilder sbText = new StringBuilder();
                sbText.Append("<script>alert('");
                sbText.Append(sRes);
                sbText.Append("')</script>");
                sMsg = sbText.ToString();
                return;
            }

            string sValMessage = UserBll.RegCheck(sAccount,sPassWord);
            if(sValMessage != "")
            {
                sMsg = sValMessage;
                return;
            }

            int iUID = UserBll.UserReg(sAccount, sPassWord);
            if (-1 == iUID)
            {
                sMsg = "<script>alert('注册失败，请重试！')</script>";
                return;
            }
            else if(iUID > 999)
            {
                string sQuestion = CYRequest.GetString("question");
                string sAnswer = CYRequest.GetString("answer");
                string sEmail = CYRequest.GetString("email");
                string sName = CYRequest.GetString("realname");
                string sCrednnum = CYRequest.GetString("credennum");
                UserInfo uiObject = new UserInfo();
                uiObject.Credennum = sCrednnum;
                uiObject.Answer = sAnswer;
                uiObject.Email = sEmail;
                uiObject.Name = sName;
                uiObject.question = sQuestion;
                uiObject.regip = ProvideCommon.GetRealIP();
                uiObject.uid = iUID;
                UserInfoBLL.UserInfoAdd(uiObject);
                string sPageUrl = Request.Url.ToString();
                LoginStateSet(sAccount, iUID, sPageUrl);
                string sWUrl = WebConfig.BaseConfig.sWUrl;
                string sWWWUrl = string.Format("{0}/{1}?un={2}", sWUrl, "usercookie.aspx", sAccount);
                string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                string sBBSUrl = DiscuzUserI.BBSLogin(sAccount, sPassWord, sKey);
                string sJSUrl = string.Format("<script src='{0}'></script><script src='{1}'></script>", sBBSUrl, sWWWUrl);
                sMsg = string.Format("{0}<script>alert('注册成功！');location.href='http://www.dao50.com/';</script>",sJSUrl);
                return;
            }
        }
    }
}

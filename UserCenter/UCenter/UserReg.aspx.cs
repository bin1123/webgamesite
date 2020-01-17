using System;
using System.Text;
using System.Configuration;

using Bussiness;
using DataEnity;
using Common;
using System.Net;
using System.IO;

namespace UserCenter.UCenter
{
    public partial class UserReg : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RegButton.ImageUrl = string.Format("{0}/wldFolder/images/lijizhuce.jpg", sWebUrl);
            }

        }
        protected void RegButton_Click(object sender, EventArgs e)
        {
            string sAccount = CYRequest.GetFormString("txz").Trim();
            string sPassWord = CYRequest.GetFormString("pwdtwo").Trim();
            string sValMessage = UserBll.RegCheck(sAccount, sPassWord);
            if (sValMessage != "")
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
            else if (iUID > 999)
            {
                string sChannel=CYRequest.GetString("channel");
                //string sQuestion = "";
                //string sAnswer = "";
                //string sEmail = "";
                //string sName = "";
                //string sCrednnum = "";
                //UserInfo uiObject = new UserInfo();
                //uiObject.Credennum = sCrednnum;
                //uiObject.Answer = sAnswer;
                //uiObject.Email = sEmail;
                //uiObject.Name = sName;
                //uiObject.question = sQuestion;
                //uiObject.regip = ProvideCommon.GetRealIP();
                //uiObject.uid = iUID;
                //UserInfoBLL.UserInfoAdd(uiObject);
                string sKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                string sR = DiscuzUserI.BBSReg(sAccount, sPassWord, sKey);
                string sPageUrl = Request.Url.ToString();
                string url = string.Format("http://union.dao50.com/Interface/other/UsercenterReg.aspx?name={0}&userid={1}&channel={2}", sAccount, iUID, sChannel);
               GetPageInfo(url);
               // LoginStateSet(sAccount, iUID, sPageUrl);
                //更新成功
                sMsg = "<script>alert('注册成功！');location.href='http://www.dao50.com/';</script>";
                return;
            }
        }

        /// <summary>
        /// 模拟浏览器访问
        /// </summary>
        /// <param name="sUrl">访问地址</param>
        /// <returns></returns>
        public static string GetPageInfo(string sUrl)
        {
            string sReturn = string.Empty;
            try
            {
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(sUrl);
                Request.Timeout = 20000;
                Request.UserAgent = "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                Stream HttpStream = Response.GetResponseStream();
                StreamReader sr = new StreamReader(HttpStream);
                sReturn = sr.ReadToEnd();
            }
            catch (Exception exp)
            {
                sReturn = exp.Message;
            }
            return sReturn;
        }
    }
}

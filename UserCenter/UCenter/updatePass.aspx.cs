using System;
using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    public partial class updatePass : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sName = GetSearchName();
            if (sName.Length < 4)
            {
                Response.Redirect("searchPass.aspx",true);
            }
            if (Request.HttpMethod == "POST")
            {
                string sPassWordOne = CYRequest.GetString("passwordone");
                string sPassWord = CYRequest.GetString("passwordtwo");
                int iUserID = UserBll.UserIDSel(sName);
                string sMD5PassWord = UserBll.PassWordMD5(sName, sPassWord);
                if (sPassWord != "" && sName != "")
                {
                    if (sPassWord == sPassWordOne && sPassWord != "unsafe string")
                    {
                        if (1 == UserBll.UserUpdatePWD(iUserID, sMD5PassWord))
                        {
                            Response.Cookies["searchname"].Expires = DateTime.Now.AddDays(-1);
                            sMsg = "<script>alert('修改密码成功！');location.href='../Default.aspx';</script>";
                            return;
                        }
                        else
                        {
                            //更新失败
                            sMsg = "<script>alert('修改密码失败！')</script>";
                            return;
                        }
                    }
                    else
                    {
                         sMsg = "<script>alert('两次输入的密码不一致！');</script>";
                         return;
                    }
                }
                else
                {
                    sMsg = "<script>alert('用户名或者密码不能为空！');</script>";
                    return;
                }
            }

        }

        private string GetSearchName()
        {
            string sSearchName = string.Empty;
            if (Request.Cookies["searchname"] != null)
            {
                string sNameCookie = Request.Cookies["searchname"].Value;
                if(sNameCookie.Split('|').Length == 2)
                {
                    string sSign = sNameCookie.Split('|')[1];
                    string sName = sNameCookie.Split('|')[0];
                    string sKey = "1$3*dwq12vc.";
                    string sMD5Sign = ProvideCommon.MD5(string.Format("{0}{1}", sName, sKey));
                    if(sMD5Sign == sSign)
                    {
                        sSearchName = sName;
                    }
                }
            }
            return sSearchName;
        }
    }
}

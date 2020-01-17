using System;
using System.Text;

using Bussiness;
using DataEnity;
using Common;


namespace UserCenter.UCenter
{
    public partial class MultiUserMoreAdd : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sRes = string.Empty;
            if (LoginSessionVal() || isLoginCookie())
            {
                if (Request.HttpMethod == "POST")
                {
                    string sBirthday = CYRequest.GetFormString("birthday").Trim();
                    string sNickname = CYRequest.GetFormString("nickname").Trim();
                    string sPhone = CYRequest.GetFormString("phone").Trim();
                    string sQQ = CYRequest.GetFormString("qq").Trim();
                    string sSex = CYRequest.GetFormString("sex").Trim();
                    string sWork = CYRequest.GetFormString("work").Trim();
                    string sGoUrL = CYRequest.GetFormString("fromurl").Trim();
                    string sBackUrL = Request.UrlReferrer.ToString();
                    if (sNickname.Length > 0 && sPhone.Length == 11 && sBirthday.Length > 7 && sQQ.Length > 4 && sSex.Length == 1 && sWork.Length > 0)
                    {
                        int iUserID = GetUserID();
                        UserMore umObject = new UserMore();
                        umObject.userid = iUserID;
                        umObject.birthday = sBirthday;
                        umObject.nickname = sNickname;
                        umObject.phone = sPhone;
                        umObject.qq = sQQ;
                        umObject.sex = sSex;
                        umObject.work = sWork;
                        int iRes = UserMoreBLL.UserMoreAdd(umObject);
                        if (iRes > 0)
                        {
                            Response.Write(string.Format("<script>alert('修改成功！')</script><script>location.href='{0}'</script>", sGoUrL));
                            return;
                        }
                        else
                        {
                            Response.Write(string.Format("<script>alert('添加失败！')</script><script>location.href='{0}'</script>", sBackUrL));
                            return;
                        }
                    }
                    else
                    {
                        Response.Write(string.Format("<script>alert('输入内容有误，请从新输入!')</script><script>location.href='{0}'</script>", sBackUrL));
                        return;
                    }
                }
            }
        }
    }
}

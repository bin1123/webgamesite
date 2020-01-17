using System;
using System.Web.UI;

using Bussiness;
using Common;

namespace UserCenter.UCenter
{
    public partial class DIndulge : pagebase.PageBase
    {
        string sEmail = string.Empty;
        protected string sMsg = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            int iUID = GetUserID();
            if (!Page.IsPostBack)
            {
                if (!(LoginSessionVal() || isLoginCookie()))
                {
                    Response.Redirect("../Default.aspx", true);
                }
                else
                {
                    string sCredenNum = UserInfoBLL.UserCredennumSel(iUID);
                    if(sCredenNum.Length > 14)
                    {
                        //防沉迷已经添加
                        sMsg = "<script>alert('已经解除防沉迷！不用再次解除！');location.href='../Default.aspx';</script>";
                        return;
                    }
                }
            }
            if (Request.HttpMethod == "POST")
            {
                string sUserName = CYRequest.GetFormString("UserName");
                string sCredenNum = CYRequest.GetFormString("CredenNum");
                int iNum = UserInfoBLL.UserInfoUpdateOfIndulge(sUserName, sCredenNum, iUID);
                if (iNum > 0)
                {
                    //更新成功
                    sMsg = "<script>alert('防沉迷解除成功！谢谢！');location.href='../Default.aspx';</script>";
                    return;
                }
                else
                {
                    //更新失败
                    sMsg = "<script>alert('防沉迷解除失败！')</script>";
                    return;
                }
            }
        }
    }
}

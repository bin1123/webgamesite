using System;
using Bussiness;
using Common;

namespace UserCenter.Pay
{
    public partial class PayErr : pagebase.PageBase
    {
        protected string sErrText = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sErr = CYRequest.GetQueryString("err");
            switch (sErr)
            {
                case "201":
                    sErrText = "充值游戏账号不存在";
                    break;
                case "202":
                    sErrText = "充值的游戏未创建角色";
                    break;
                case "203":
                    sErrText = "网络繁忙!武林币获取失败";
                    break;
                case "204":
                    sErrText = "账号余额为0 ";
                    break;
                case "205":
                    sErrText = "角色名错误";
                    break;
                case "206":
                    sErrText = "游戏充值失败,如有问题请联系客服";
                    break;
                default:
                    sErrText = "";
                    break;
            }
            string sFromHost = GetFromHost();
            if (sFromHost.Length > 5)
            {
                string sGoUrl = string.Format("http://{0}/PayGErr.html?{1}", sFromHost, sErr);
                Response.Redirect(sGoUrl, true);
                return;
            }
        }
    }
}

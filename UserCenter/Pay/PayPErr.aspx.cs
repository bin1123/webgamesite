using System;
using Bussiness;
using Common;

namespace UserCenter.Pay
{
    public partial class PayPErr : pagebase.PageBase
    {
        protected int iUserPoints = 0;
        protected string sErrText = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sErr = CYRequest.GetQueryString("err");
            switch (sErr)
            {
                case "101":
                    sErrText = "充值订单提交失败";
                    break;
                case "102":
                    sErrText = "游戏兑换失败!兑换武林币成功，请使用武林币兑换游戏";
                    break;
                case "103":
                    sErrText = "无返回参数";
                    break;
                case "104":
                    sErrText = "认证签名失败";
                    break;
                case "105":
                    sErrText = "支付失败";
                    break;
                case "110":
                    sErrText = "正在充值中，请稍等查看余额";
                    break;
                case "111":
                    sErrText = "交易没有成功！如有问题请与客服联系！";
                    break;
                case "204":
                    sErrText = "充值金额有误!";
                    break;
            }
            string sFromHost = GetFromHost();
            if (sFromHost.Length > 5)
            {
                string sGoUrl = string.Format("http://{0}/PayPErr.html?{1}", sFromHost, sErr);
                Response.Redirect(sGoUrl, true);
                return;
            }
        }
    }
}

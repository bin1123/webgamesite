using System;
using Bussiness;
using Common;
using DataEnity;
using System.Text;

namespace UserCenter.Pay
{
    public partial class QucikVCallBack : pagebase.PageBase
    {
        protected string sMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //StringBuilder sbText = new StringBuilder();
            //sbText.Append(Server.MapPath("~/Log"));
            //sbText.Append("/Pay");
            //string sPath = sbText.ToString();
            //ProvideCommon pcObject = new ProvideCommon();
            //sbText.Remove(0, sbText.Length);
            //sbText.AppendFormat("{0},{1}", Request.Url.ToString(), DateTime.Now.ToString());
            //pcObject.WriteLogFile(sPath, "QuickVCallback", sbText.ToString());

            string sRes = VPayBuy.QuickVPaySubmit();
            string[] sARes = sRes.Split('|');
            string sUrl = string.Empty;
            if ("1" == sARes[0])
            {
                sUrl = string.Format("PayGSucc.aspx?TranID={0}&gn={1}&type=q", sARes[1], sARes[2]);
                Response.Redirect(sUrl, true);
            }
            else if ("3" == sARes[0])
            {
                //sMsg = "<script>alert('武林币充值成功！游戏充值失败！请进入武林币兑换页面进行兑换！');</script>";
                Response.Redirect(string.Format("PayPErr.aspx?err=102&code={0}", sRes));
            }
            else if ("2" == sARes[0])
            {
                //sMsg = "<script>alert('提交订单失败！请联系客服！');</script>";
                Response.Redirect("PayPErr.aspx?err=101");
            }
            else
            {
                sMsg = string.Format("<script>alert('{0}');</script>", sRes);
            }
        }
    }
}

using System;
using Bussiness;
using Common;
using DataEnity;
using System.Text;

namespace UserCenter.Pay
{
    public partial class QucikYeeCallBack : pagebase.PageBase
    {
        protected string sMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            int iUserID = GetUserID();
            string sRes = YeePayBuy.QuickYeePaySubmit(); 
            string sTranIP = ProvideCommon.GetRealIP();
            string sFromUrl = Request.Url.ToString();
            char cTranFrom = 't';
            if ("4" == sRes)
            {
                cTranFrom = 'y';
                YeePayBuy.LastOfPayLog(sTranIP, cTranFrom, sFromUrl);
                if (iUserID > 999)
                {
                    UserPoints upObject = UserPointsBLL.UPointsSel(iUserID);
                    SetPoints(upObject.Points);
                }
                Response.Write("SUCCESS");
                return;
            }
            else
            {
                YeePayBuy.LastOfPayLog(sTranIP, cTranFrom, sFromUrl);
                string[] sARes = sRes.Split('|');
                string sUrl = string.Empty;
                if ("1" == sARes[0])
                {
                    sUrl = string.Format("PayGSucc.aspx?TranID={0}&gn={1}&type=q",sARes[1],sARes[2]);
                    Response.Redirect(sUrl,true);
                }
                else if ("3" == sARes[0])
                {
                    //sMsg = "<script>alert('武林币充值成功！游戏充值失败！请进入武林币兑换页面进行兑换！');</script>";                    
                    Response.Redirect("PayPErr.aspx?err=101");
                }
                else if ("2" == sARes[0])
                {
                    //sMsg = "<script>alert('提交订单失败！请联系客服！');</script>";
                    Response.Redirect(string.Format("PayPErr.aspx?err=102&code={0}",sRes));
                }
                else if ("0" == sARes[0])
                {
                    //充值失败，原因见sRes[1]
                    //sMsg = "<script>alert('请稍等查看余额，如有问题请联系客服！');</script>";
                    Response.Redirect("PayPErr.aspx?err=110");
                }
                else
                {
                    sMsg = string.Format("<script>alert('{0}');</script>",sRes);
                }
            }
        }
    }
}

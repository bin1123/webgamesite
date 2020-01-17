using System;
using Bussiness;
using Common;
using DataEnity;

namespace UserCenter.Pay
{
    public partial class YeeCallBack : pagebase.PageBase
    {
        protected string sMsg = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sRes = YeePayBuy.YeePaySubmit(); 
            string sTranIP = ProvideCommon.GetRealIP();
            string sFromUrl = Request.Url.ToString();
            char cTranFrom = 't';
            if ("4" == sRes)
            {
                int iUserID = GetUserID();
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
                if ("1" == sARes[0])
                {
                    Response.Redirect("PayPSucc.aspx?TranID="+sARes[1]);
                }
                else if ("2" == sARes[0])
                {
                    //sMsg = "<script>alert('提交订单失败！请联系客服！');location.href='default.aspx';</script>";
                    Response.Redirect("PayPErr.aspx?err=101");
                }
                else if ("0" == sARes[0])
                {
                    //sMsg = "<script>alert('请稍等查看余额，如有问题请联系客服！');location.href='default.aspx';</script>";
                    Response.Redirect("PayPErr.aspx?err=110");
                }
            }
        }
    }
}

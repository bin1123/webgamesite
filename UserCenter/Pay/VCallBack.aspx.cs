using System;
using Bussiness;
using Common;
using DataEnity;

namespace UserCenter.Pay
{
    public partial class VCallBack : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sRes = VPayBuy.VPaySubmit();
            string[] sARes = sRes.Split('|');
            if (sARes[0] == "0")
            {
                Response.Redirect(string.Format("PayPSucc.aspx?TranID={0}", sARes[1]));
            }
            else
            {
                Response.Redirect(WebConfig.BaseConfig.sWebUrl);
            }
        }
    }
}

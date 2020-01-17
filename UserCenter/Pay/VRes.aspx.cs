using System;
using System.Text;

using Bussiness;
using Common;
using DataEnity;

namespace UserCenter.Pay
{
    public partial class VRes : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sRes = VPayBuy.VPayVal();
            if (sRes == "0")
            {
                Response.AddHeader("Data-Received", "ok_vpay8");// '此句不能删除也不能修改，作为探测成功之用
            }
            else
            {
                Response.Write(sRes);
            }
            //StringBuilder sbText = new StringBuilder();
            //sbText.Append(Server.MapPath("~/Log"));
            //sbText.Append("/Pay");
            //string sPath = sbText.ToString();
            //ProvideCommon pcObject = new ProvideCommon();
            //sbText.Remove(0,sbText.Length);
            //sbText.AppendFormat("url:{0}",Request.Url.ToString());
            //sbText.AppendFormat("；res:{0}",sRes);
            //pcObject.WriteLogFile(sPath,"VRes",sbText.ToString());
        }
    }
}

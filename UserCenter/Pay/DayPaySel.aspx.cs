using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Common;
using Bussiness;

namespace UserCenter.Pay
{
    public partial class DayPaySel : pagebase.PageBase
    {
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected double dPrice = 0;
        protected int iNum = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginSessionVal() || isLoginCookie())
            {
                int iUserID = GetUserID();
                DateTime dtBegin = DateTime.Today;
                DateTime dtEnd = dtBegin.AddDays(1);
                dPrice = TransPBLL.TranPTodayPriceSel(iUserID, dtBegin, dtEnd);
                if(dPrice > 0)
                {
                    iNum = TransPBLL.TranPTodayNumSel(dPrice, dtBegin, dtEnd);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Common;

namespace UserCenter.Pay
{
    public partial class pPayErr : System.Web.UI.Page
    {
        protected string sErrText = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sErr = CYRequest.GetQueryString("err");
            switch(sErr)
            {
                case "null":
                case "account":
                case "pid":
                    sErrText = "参数有问题!";
                    break;
                case "time":
                    sErrText = "时间超出！请从新操作!";
                    break;
                case "ticket":
                    sErrText = "sign验证失败！";
                    break;
                case "userid":
                    sErrText = "还没登陆游戏，请登陆游戏先!";
                    break;
                case "partner":
                    sErrText = "合作商不合法!";
                    break;
                default:
                    sErrText = "请从新登陆!";
                    break;
            }
        }
    }
}

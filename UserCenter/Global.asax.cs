using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Net;
using System.Text;

using Common;

namespace UserCenter
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        private void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        private void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        private void Application_Error(object sender, EventArgs e)
        {
            ProvideCommon pcObject = new ProvideCommon();
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString();
            StringBuilder sbText = new StringBuilder();
            sbText.Append(Server.MapPath("~/Error"));
            sbText.Append(year);
            sbText.Append("/");
            sbText.Append(month);
            pcObject.WriteSysErr(Server.GetLastError().GetBaseException(), Request.Url.ToString(), sbText.ToString());
            Server.ClearError();
            sbText.Remove(0, sbText.Length);
            sbText.Append(WebConfig.BaseConfig.sWUrl);
            sbText.Append("/Err/?url=");
            sbText.Append(Request.Url.ToString());
            Response.Redirect(sbText.ToString(), false);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.UI.Page" %> 
<%@ Import Namespace="System" %> 
<%@ Import Namespace="System.Web" %> 
<%@ Import Namespace="System.Net"%> 
<%@ Import Namespace="System.IO"%>

<script runat="server"> 
protected void Page_Load(object sender, EventArgs e) 
{
    string sUrl = string.Empty;
    string sReturn = string.Empty;
    if (Request.QueryString["url"] != null)
    {
        try
        {
            sUrl = Server.UrlDecode(Request.QueryString["url"]);
            WebRequest WRequest = WebRequest.Create(sUrl);
            WRequest.Timeout = 20000;
            WebResponse WResponse = (HttpWebResponse)WRequest.GetResponse();
            Stream HttpStream = WResponse.GetResponseStream();
            StreamReader sr = new StreamReader(HttpStream);
            sReturn = sr.ReadToEnd();
        }
        catch (Exception exp)
        {
            sReturn = exp.Message;
        } 
    }
    Response.Write(sReturn); 
}
</script> 

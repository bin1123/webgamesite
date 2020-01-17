<%@ Page Language="C#" AutoEventWireup="true" Inherits="System.Web.UI.Page" %> 
<%@ Import Namespace="System" %> 
<%@ Import Namespace="System.Web" %>

<script runat="server"> 
protected void Page_Load(object sender, EventArgs e) 
{
    if (Request.QueryString["un"] != null)
    {
        Response.Cookies["un"].Value = Request.QueryString["un"].ToString();
        Response.Cookies["un"].Expires = DateTime.Now.AddDays(1);
        Response.Cookies["time"].Value = DateTime.Now.ToString();
        Response.Cookies["time"].Expires = DateTime.Now.AddDays(1);
    }
}
</script> 

using System;
using System.Text;

using Common;
using Bussiness;

namespace UserCenter.Services
{
    public partial class userloginto : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RequestType == "GET" || Request.RequestType == "POST")
            {
                string skey = "Pd23AS!2lh*2B";
                string sUserName = CYRequest.GetString("username");
                string sPassWord = CYRequest.GetString("password");
                string sPage = Server.UrlDecode(CYRequest.GetString("page"));
                string sSign = CYRequest.GetString("sign");
                if (sSign != "")
                {
                    StringBuilder sbText = new StringBuilder();
                    sbText.AppendFormat("{0}{1}{2}",sUserName,sPassWord,skey);
                    string sValSign = ProvideCommon.MD5(sbText.ToString());//md5(username+password+key)
                    string sRes = string.Empty;
                    if (sValSign == sSign)
                    {
                        if (UserBll.UserAllVal(sUserName, sPassWord))
                        {
                            sRes = "suc";
                            string sPageUrl = Request.Url.ToString();
                            int iUserID = UserBll.UserIDSel(sUserName);
                            LoginStateSet(sUserName, iUserID, sPageUrl);
                            if (sPage != "")
                            {
                                sbText.Remove(0,sbText.Length);
                                sbText.AppendFormat("{0}{1}{2}",sUserName,sRes,skey);
                                string sSucSign = ProvideCommon.MD5(sbText.ToString());//md5(username+res+key)
                                if (sPage.IndexOf("?") > -1)
                                {
                                    Response.Redirect(string.Format("{0}&res={1}&username={2}&sign={3}", sPage, sRes, sUserName, sSucSign), true);
                                }
                                else
                                {
                                    Response.Redirect(string.Format("{0}?res={1}&username={2}&sign={3}", sPage, sRes, sUserName, sSucSign), true);
                                }
                            }
                            else
                            {
                                Response.Write(sRes);
                            }
                        }
                        else
                        {
                            sRes = "loginerr";
                            if (sPage != "" && sPage != "unsafe string")
                            {
                                if (sPage.IndexOf("?") > -1)
                                {
                                    Response.Redirect(string.Format("{0}&res={1}", sPage, sRes), true);
                                }
                                else
                                {
                                    Response.Redirect(string.Format("{0}?res={1}", sPage, sRes), true);
                                }
                            }
                            else
                            {
                                Response.Write(sRes);
                            }
                        }
                    }
                    else
                    {
                        sRes = "valerr";
                        if (sPage != "" && sPage != "unsafe string")
                        {
                            if (sPage.IndexOf("?") > -1)
                            {
                                Response.Redirect(string.Format("{0}&res={1}", sPage, sRes), true);
                            }
                            else
                            {
                                Response.Redirect(string.Format("{0}?res={1}", sPage, sRes), true);
                            }
                        }
                        else
                        {
                            Response.Write(sRes);
                        }
                    }
                }
            }
        }
    }
}

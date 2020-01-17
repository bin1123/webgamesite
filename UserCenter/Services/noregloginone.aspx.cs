using System;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    public partial class noregloginone : pagebase.PageBase
    {
        protected string sQueryString = string.Empty;
        protected string sGameName = string.Empty;
        protected string sUrl = string.Empty;
        protected string sMsg = string.Empty;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sAccountT = string.Empty;
        protected string sServerName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Request.HttpMethod == "GET")
            {
                string sWebUrl = WebConfig.BaseConfig.sWUrl;
                string uniqueid = CYRequest.GetString("uniqueid");
                sGameName = CYRequest.GetString("game");
                string sign = CYRequest.GetString("sign");
                string sKey = "1!s@k#d)}w[l<>";
                StringBuilder sbText = new StringBuilder();
                sbText.Append(uniqueid);
                sbText.Append(sGameName);
                sbText.Append(sKey);
                string sValSign = ProvideCommon.MD5(sbText.ToString()).ToLower();
                if (sign == sValSign)
                {
                    sServerName = ServerBLL.ServerTitleNoSNameSel(sGameName);
                    if (!NoRegLoginBLL.NoRegLoginUnionidSel(uniqueid))
                    {
                        //sMsg = "<script>alert('uniqueid 重复！')</script>";
                        sAccountT = GetAccount();
                        return;
                    }
                    string sUserName = string.Format("?{0}",ProvideCommon.GenerateStringID());
                    int iTypeID = 1;
                    int iState = 1;
                    string sPassWord = "";
                    int iUID = UserBll.UserReg(sUserName,sPassWord,iTypeID,iState);
                    if (iUID > 1000)
                    {
                        sAccountT = sUserName;
                        string sPageUrl = Request.Url.ToString();
                        LoginStateSet(sUserName, iUID, sPageUrl);
                        NoRegLoginBLL.NoRegLoginAdd(iUID,uniqueid,sGameName);
                        NoRegLoginBLL.AddUserid(uniqueid, iUID.ToString());
                        if (sGameName.Length > 0)
                        {
                            string sGame = GameInfoBLL.GameInfoAbbreSel(sGameName).TrimEnd();
                            switch (sGame)
                            {
                                case "lj":
                                case "yjxy":
                                case "sq":
                                case "hzw":
                                case "xlfc":
                                case "dxz":
                                case "djj":
                                case "zl":
                                case "fswd2":
                                case "txj":
                                case "ljer":
                                case "sjsg":
                                case "tzcq":
                                case "zsg":
                                case "wssg":
                                case "by":
                                case "nz":
                                case "mjcs":
                                    if (ProvideCommon.valTime(DateTime.Now.ToString(), ServerBLL.ServerTimeSel(sGameName)))
                                    {
                                        sQueryString = string.Format("?gn={0}", sGameName);
                                        sUrl = string.Format("/frame/g_mainframe_{0}.aspx{1}", sGame, sQueryString);
                                    }
                                    break;
                                case "tssg":
                                    string fuid = CYRequest.GetString("fuid");
                                    sQueryString = string.Format("?gn={0}&fuid={1}", sGameName, fuid);
                                    sUrl = string.Format("/frame/g_mainframe_{0}.aspx{1}", sGame, sQueryString);
                                    break;
                                default:
                                    sUrl = string.Format("/GCenter/PlayGame.aspx?gn={0}", sGameName);
                                    break;
                            }

                        }
                        else
                        {
                            Response.StatusCode = 301;
                            Response.Status = "301 Moved Permanently";
                            Response.RedirectLocation = string.Format("{0}/yxzx/", sWebUrl);
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('注册失败!');location.href='http://www.dao50.com/';</script>");
                    }
                }
                else
                {
                    //sMsg = "<script>alert('sign error')</script>";
                    return;
                }
            }
        }
    }
}

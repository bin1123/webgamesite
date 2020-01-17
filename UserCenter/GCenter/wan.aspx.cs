using System;
using Common;
using Bussiness;
using System.Configuration;

namespace UserCenter.GCenter
{
    public partial class wan : pagebase.PageBase
    {
        protected string sTitle = string.Empty;
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sWUrl = WebConfig.BaseConfig.sWUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();
        protected string sGame = string.Empty;
        protected string sGameName = string.Empty;
        protected string sQueryString = string.Empty;
        protected string sHeight = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //判断是否登陆
            sGameName = CYRequest.GetString("gn");
            if (sGameName == "" || sGameName == "unsafe string")
            {
                Response.Redirect(string.Format("{0}/yxzx", sWUrl), false);
            }
            else
            {
                if (LoginSessionVal() || isLoginCookie())
                {
                    int iUserID = GetUserID();
                    string sUserID = string.Empty;
                    string sAccount = GetAccount();
                    if (!ValUserState(iUserID, sAccount))
                    {
                        iUserID = UserBll.UserIDSel(sAccount);
                        if (iUserID < 1000 || (!ValUserState(iUserID, sAccount)))
                        {
                            sHeight = "100%";
                            sGame = "lj";
                            Response.Write(string.Format("<script>alert('用户名:{2}与数字ID:{3}不一致，请重新登录！谢谢！');location.href='{0}/Default.aspx?gn={1}';</script>", sRootUrl, sGameName, sAccount, iUserID));
                            ClearUsersInfo();
                            return;
                        }
                    }
                    else
                    {
                        DateTime dtLoginTime = GetLoginTime();
                        if (!PWDUpdateBLL.PwdUpdateVal(iUserID, dtLoginTime))
                        {
                            ClearUsersInfo();
                            Response.Redirect(string.Format("{0}/Default.aspx", sRootUrl), true);
                            return;
                        }
                    }

                    sUserID = iUserID.ToString();

                    sGame = GameInfoBLL.GameInfoAbbreSel(sGameName).TrimEnd();
                    switch (sGame)
                    { 
                        case "lj":
                        case "yjxy":
                        case "sq":
                        case "hzw":
                        case "xlfc":
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
                        case "dxz":
                        case "mxqy":
                        case "swjt":
                        case "gcld":
                        case "jdsj":
                        case "tjz":
                        case "khbd":
                        case "sglj":
                        case "hyjft":
                        case "llsg":
                        case "nslm":
                        case "rxzt":
                        case "ftz":
                        case "ahxy":
                        case "mhxy":
                        case "sxj":
                        case "zwj":
                        case "qxz":
                        case "qszg":
                        case "wwsg":
                        case "dntg":
                        case "jy":
                        case "sskc":
                        case "ktpd":
                        case "mhtj":
                        case "dtgzt":
                        case "ahxx":
                        case "jjp":
                        case "sgyjz":
                        case "zwx":
                            string sStartTime = DateTime.Now.ToString();
                            string sEndTime = ServerBLL.ServerTimeSel(sGameName);
                            if (!ProvideCommon.valTime(sStartTime, sEndTime))
                            {
                                Response.Redirect(string.Format("{0}/jjkf", sWUrl), true);
                                return;
                            }
                            //else
                            //{
                            //    if(iUserID < 2000)
                            //    {
                            //        System.Text.StringBuilder sbText = new System.Text.StringBuilder();
                            //        sbText.Append(Server.MapPath("~/Log"));
                            //        sbText.Append("/wan");
                            //        string sPath = sbText.ToString();
                            //        ProvideCommon pcObject = new ProvideCommon();
                            //        sbText.Remove(0, sbText.Length);
                            //        sbText.AppendFormat("StartTime:{0}", sStartTime);
                            //        sbText.AppendFormat("；EndTime:{0}", sEndTime);
                            //        pcObject.WriteLogFile(sPath, "log", sbText.ToString());
                            //    }
                            //}
                            sHeight = "100%";
                            sTitle = ServerBLL.ServerTitleSel(CYRequest.GetString("gn"));
                            sQueryString = string.Format("?gn={0}",sGameName);
                            break;
                        case "mjcs":
                            sHeight = "630px";
                            sTitle = ServerBLL.ServerTitleSel(CYRequest.GetString("gn"));
                            sQueryString = string.Format("?gn={0}", sGameName);
                            break;
                        case "tssg":
                            sHeight = "640px";
                            sTitle = ServerBLL.ServerTitleSel(CYRequest.GetString("gn"));
                            string fuid = CYRequest.GetString("fuid");
                            sQueryString = string.Format("?gn={0}&fuid={1}", sGameName,fuid);
                            break;                            
                        default:
                            sHeight = "100%";
                            Response.Redirect(string.Format("{0}/", sWUrl), true);
                            break;
                    }
                }
                else
                {
                    Response.Redirect(string.Format("{0}/Default.aspx?gn={1}", sRootUrl, sGameName));
                }
            }
        }
    }
}

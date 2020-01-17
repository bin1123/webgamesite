using System;
using System.Text;
using System.Configuration;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    public partial class pLogin : pagebase.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string account = CYRequest.GetString("account");
            string agentid = CYRequest.GetString("agentid");
            string tstamp = CYRequest.GetString("tstamp");
            string url = CYRequest.GetString("url");

            if (account.Length > 20 || account.Length < 6)
            {
                Response.Write("2");
                return;
            }

            TimeSpan tsNow = new TimeSpan(DateTime.Now.Ticks);
            long lTime = long.Parse(tstamp + "0000000");
            TimeSpan tsGet = new TimeSpan(lTime);
            DateTime dtGet = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)).Add(tsGet);
            int iMinutes = 0;
            if (dtGet > DateTime.Now)
            {
                TimeSpan tsgetSpan = new TimeSpan(dtGet.Ticks);
                iMinutes = tsgetSpan.Subtract(tsNow).Minutes;
            }
            else
            {
                iMinutes = tsNow.Subtract(tsGet).Minutes;
            } 
            
            if (iMinutes > 10)
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                Response.Write(string.Format("3|{0}|{1}:{2}|{3}", iMinutes, DateTime.Now.ToString(), dtGet, tstamp));
                return;
            }

            string sign = CYRequest.GetString("sign");//md5(account + agentid + gameabbre + tstamp + TICKEY_LOGIN)
            int pid = 0;
            int.TryParse(agentid, out pid);
            if (pid < 1)
            {
                Response.Write("4");
                return;
            }
            string TICKEY_LOGIN = PartnerBLL.PartnerKeySel(pid);
            StringBuilder sbText = new StringBuilder(50);
            sbText.Append(account);
            sbText.Append(agentid);
            sbText.Append(tstamp);
            sbText.Append(TICKEY_LOGIN);
            string sValSign = ProvideCommon.MD5(sbText.ToString());
            if (sign == sValSign)
            {
                string sPageUrl = Request.Url.ToString();
                string sPartnerAbbre = PartnerBLL.PartnerAbbreSel(pid);
                if (sPartnerAbbre == null || sPartnerAbbre.Length < 1)
                {
                    Response.Write("5");
                    return;
                }
                string sAccountC = string.Format("{0}:{1}", sPartnerAbbre, account);
                string sAccountLogin = GetAccount();
                string sUserIP = ProvideCommon.GetRealIP();
                int iUserID;
                if (sAccountC != sAccountLogin)
                {
                    //1.检查帐号是否存在
                    iUserID = PartnerUserBLL.PartnerUserIDSel(account,pid);
                    if (iUserID < 1000)
                    {
                        //2.不存在，则注册帐号
                        int iUID = UserBll.UserReg(sAccountC, "");
                        if (iUID < 1000)
                        {
                            Response.Write("6");
                            return;
                        }
                        else
                        {
                            int iRow = PartnerUserBLL.PartnerUserAdd(pid, sUserIP, iUID, account);
                            if (iRow < 1)
                            {
                                Response.Write("7");
                                return;
                            }
                        }
                        iUserID = iUID;
                        LoginStateSet(sAccountC, iUID, sPageUrl);
                    }
                    else
                    {
                        LoginStateSet(sAccountC, iUserID, sPageUrl);
                    }
                }
                else
                {
                    iUserID = GetUserID();
                    if (!ValUserState(iUserID, sAccountC))
                    {
                        iUserID = UserBll.UserIDSel(sAccountC);
                        if (iUserID < 1000 || (!ValUserState(iUserID, sAccountC)))
                        {
                            ClearUsersInfo();
                            Response.Write("10");
                            return;
                        }
                        else
                        {
                            ClearUsersInfo();
                            LoginStateSet(sAccountC, iUserID, sPageUrl);
                        }
                    }
                }

                if (url.Length > 5)
                {
                    string sUrl = Server.UrlDecode(url);
                    Response.Redirect(sUrl,true);
                }
            }
            else
            {
                Response.Write("9");
            }
        }
    }
}

using System;
using System.Web;
using System.Text;
using Bussiness;
using DataEnity;
using Common;

namespace UserCenter.pagebase
{
    public class PageBase : System.Web.UI.Page 
    {
        private string sDESKey = "s8z7x6a5";

        public void setcookies(string sAccount,int iUserID) 
        {
            Crypto3DES DesObject = new Crypto3DES();
            DesObject.Key = sDESKey;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Response.Cookies["UserID"].Value =  DesObject.Encrypt3DES(iUserID.ToString());
            Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(1);
            Response.Cookies["Account"].Value = HttpUtility.UrlEncode(sAccount, enc);
            Response.Cookies["Account"].Expires = DateTime.Now.AddDays(1);
            Response.Cookies["logintime"].Value = DateTime.Now.ToString();
            Response.Cookies["logintime"].Expires = DateTime.Now.AddDays(1);
            UserPoints upObject = UserPointsBLL.UPointsSel(iUserID);
            int iPoints = upObject.Points;
            int iGiftPoints = upObject.GiftPoints;
            SetPoints(iPoints+iGiftPoints);
            string sLoginInfo = GetUserLogin(iUserID);
            SetLogin(sLoginInfo);
        }

        public void setaccount(string sAccount)
        {
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Response.Cookies["Account"].Value = HttpUtility.UrlEncode(sAccount, enc);
            Response.Cookies["Account"].Expires = DateTime.Now.AddDays(1);
            //Session["Account"] = sAccount;
        }

        public void SetPoints(int iPoints)
        {
            if (iPoints < 0)
            {
                iPoints = 0;
            }
            Response.Cookies["UPoints"].Value = iPoints.ToString();
            Response.Cookies["UPoints"].Expires = DateTime.Now.AddDays(1);            
        }

        public void SetRUInfo(string sUserName)
        {
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Response.Cookies["UserName"].Value = HttpUtility.UrlEncode(sUserName,enc);
            Response.Cookies["UserName"].Expires = DateTime.Now.AddYears(1);
        }

        public void SetLogin(string sLoginInfo)
        {
            if (sLoginInfo.Length < 1)
            {
                Response.Cookies["Login"].Value = "|";
            }
            else
            {
                Response.Cookies["Login"].Value = sLoginInfo;
            }
            Response.Cookies["Login"].Expires = DateTime.Now.AddDays(1);
        }

        public string GetUserName()
        {
            string sUserName = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            if (Request.Cookies["UserName"] != null)
            {
                sUserName = HttpUtility.UrlDecode(Request.Cookies["UserName"].Value,enc);
            }
            else
            {
                sUserName = "";
            }
            return sUserName;
        }

        public int GetUserID()
        {
            Crypto3DES DesObject = new Crypto3DES();
            DesObject.Key = sDESKey;
            int iUserID = 0;
            if (Request.Cookies["UserID"] != null)
            {
                string sDesUserID = DesObject.Decrypt3DES(Request.Cookies["UserID"].Value);
                int.TryParse(sDesUserID, out iUserID);
            }
            return iUserID;
        }

        public string GetAccount()
        {
            string sAccount = string.Empty;
            if (Request.Cookies["Account"] == null || string.IsNullOrEmpty(Request.Cookies["Account"].Value))
            {
                sAccount = "";
            }
            else
            {
                Encoding enc = Encoding.GetEncoding("UTF-8");
                sAccount = HttpUtility.UrlDecode(Request.Cookies["Account"].Value, enc);
            }
            return sAccount;
        }

        /// <summary>
        /// 获取用户彩游币
        /// </summary>
        /// <returns></returns>
        public int GetUPoints()
        {
            int iUPoints = 0;
            if (Request.Cookies["UPoints"] != null)
            {
                int.TryParse(Request.Cookies["UPoints"].Value, out iUPoints);
            }
            return iUPoints;
        }

        public DateTime GetLoginTime()
        {
            DateTime dtTime = new DateTime();
            if (Request.Cookies["logintime"] != null)
            {
                DateTime.TryParse(Request.Cookies["logintime"].Value, out dtTime);
            }
            return dtTime;
        }

        public string GetLogin()
        {
            string sLoginInfo = string.Empty;
            if (Request.Cookies["Login"] != null)
            {
                sLoginInfo = Request.Cookies["Login"].Value;
            }
            return sLoginInfo;
        }

        public string GetUserLogin(int iUserID)
        {
            string sLoginInfo = string.Empty;
            if (Request.Cookies["Login"] != null)
            {
                string sLogin = Request.Cookies["Login"].Value;
                string[] saLogin = sLogin.Split('|');
                if (saLogin.Length > 2)
                {
                    sLoginInfo = string.Format("{0}|{1}", saLogin[0], saLogin[1]);
                }
                else
                {
                    sLoginInfo = sLogin;
                }
            }
            else
            {
                sLoginInfo = GameLoginBLL.GameLoginAbbreSel(iUserID);
            }
            return sLoginInfo;
        }

        public string GetLoginTop2()
        {
            string sLoginInfo = string.Empty;
            if (Request.Cookies["Login"] != null)
            {
                string sLogin = Request.Cookies["Login"].Value;
                string[] saLogin = sLogin.Split('|');
                if (saLogin.Length > 2)
                {
                    sLoginInfo = string.Format("{0}|{1}", saLogin[0], saLogin[1]);
                }
                else
                {
                    sLoginInfo = sLogin;
                }
            }
            return sLoginInfo;
        }

        public void ClearRUInfo()
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
        }

        public void ClearUsersCookie()
        {
            Response.Cookies["Account"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["UPoints"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["logintime"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["TypeID"].Expires = DateTime.Now.AddDays(-1);
        }

        public void ClearUsersSession()
        {
            Session["Account"] = null;
            Session["UserID"] = null;
        }

        public void ClearUsersInfo()
        {
            //ClearUsersSession();
            ClearUsersCookie();
        }
        
        public bool isLoginCookie() 
        {
            if (Request.Cookies["Account"] == null)
            {
                return false;
            }
            else
            {
                int iUserID = GetUserID();
                if (iUserID < 1000)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 用户登陆状态设置
        /// </summary>
        /// <param name="account"></param>
        public void LoginStateSet(string sAccount,int iUserID,string sFormUrl)
        {
            if(sAccount.Trim().Length > 3 &&iUserID > 1000)
            {
                string sLoginIP = ProvideCommon.GetRealIP();
                setcookies(sAccount, iUserID);
                //Session["Account"] = sAccount;
                //Session["UserID"] = iUserID.ToString();
                UserPointsBLL.UPointCheck(iUserID);
                CenterLoginBLL.CenterLoginAdd(iUserID, sLoginIP, sAccount, sFormUrl);
            }
        }

        /// <summary>
        /// 验证Session是否存在
        /// </summary>
        /// <returns></returns>
        public bool LoginSessionVal()
        {
            if (null == Session["Account"] || null == Session["UserID"])
            {
                return false;
            }
            else
            {
                return true;
            }
            //return false;
        }

        public bool ValUserState(int iUserID,string sAccount)
        {
            if (iUserID < 1000 || sAccount.Length < 1)
            {
                return false;
            }
            int iVUserID = UserBll.UserIDSel(sAccount);
            if (iUserID == iVUserID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string ValCheckCode(string sValCode)
        {
            string sRes = string.Empty;
            if (Session["CheckCode"] != null)
            {
                string sCheckCode = Session["CheckCode"].ToString();
                if (sValCode.ToLower() != sCheckCode.ToLower())
                {
                    sRes = "验证码输入不正确!";
                }
                else
                {
                    sRes = "0";
                }
            }
            else
            {
                if ("" == sValCode || sValCode.Length < 5)
                {
                    sRes = "验证码格式输入不正确!";
                }
                else
                {
                    sRes = "0";
                }
            }
            return sRes;
        }

        public void SetUserInfo(string sAccount,string sMD5PassWord)
        {
            HttpCookie cookie = new HttpCookie("UserInfo");
            cookie.Expires = DateTime.Now.AddYears(1);
            Crypto3DES DesObject = new Crypto3DES();
            DesObject.Key = sDESKey;
            cookie.Values.Add("account", DesObject.Encrypt3DES(sAccount));
            cookie.Values.Add("pwd", DesObject.Encrypt3DES(sMD5PassWord));
            Response.AppendCookie(cookie);
        }

        public string[] GetUserInfo()
        { 
            string[] sUserInfo = new string[2];
            Crypto3DES DesObject = new Crypto3DES();
            DesObject.Key = sDESKey;
            sUserInfo[0] = DesObject.Decrypt3DES(Request.Cookies["UserInfo"]["account"].ToString());
            sUserInfo[1] = DesObject.Decrypt3DES(Request.Cookies["UserInfo"]["pwd"].ToString());
            return sUserInfo;
        }

        public string GetLoginTop3()
        {
            string sLoginInfo = string.Empty;
            if (Request.Cookies["Login"] != null)
            {
                string sLogin = Request.Cookies["Login"].Value;
                string[] saLogin = sLogin.Split('|');
                if (saLogin.Length > 2)
                {
                    sLoginInfo = string.Format("{0}|{1}|{2}", saLogin[0], saLogin[1], saLogin[2]);
                }
                else
                {
                    sLoginInfo = sLogin;
                }
            }
            return sLoginInfo;
        }

        public void DelUserInfo()
        {
            Response.Cookies["UserInfo"].Expires = DateTime.Now.AddDays(-1);
        }

        public string PageRefresh()
        {
            string sUrl = CYRequest.GetString("url");
            string sMsg = string.Empty;
            if (sUrl == "" || sUrl == "unsafe string")
            {
                //sMsg = string.Format("<script>parent.location.reload();</script>", sUrl);
                //sMsg = string.Format("<script>parent.location.href=parent.location.href+'?'+ Math.random();</script>", sUrl);
                sMsg = "<script>parent.location.href=parent.location.href;</script>";
            }
            else
            {
                //sMsg = string.Format("<script>window.parent.location.href='{0}'+'?'+ Math.random();</script>", sUrl);
                sUrl = sUrl.Split('?')[0];
                string sMD5Time = ProvideCommon.MD5(DateTime.Now.ToString());
                sMsg = string.Format("<script>parent.location.href='{0}?{1}'</script>", sUrl,sMD5Time);
            }
            return sMsg;
        }

        public string PAccountC(string sAccount)
        {
            int iIndex = sAccount.IndexOf(":");
            string sAccountC = string.Empty;
            if (iIndex > 0)
            {
                int iLen = iIndex + 1;
                sAccountC = sAccount.Remove(0, iLen);
            }
            else
            {
                sAccountC = sAccount;
            }
            return sAccountC;
        }

        public bool GameLogin(string sGameAbbre)
        {
            bool bRes = false;
            string sLoginedGame = GetLogin();
            string sLoginGame = string.Empty;
            int iIndexGame = sLoginedGame.IndexOf(sGameAbbre);
            if (iIndexGame < 0)
            {
                if (sLoginedGame.Length > 1)
                {
                    sLoginGame = string.Format("{0}|{1}", sGameAbbre, sLoginedGame);
                }
                else
                {
                    sLoginGame = sGameAbbre;
                }
                SetLogin(sLoginGame);
                bRes = true;
            }
            else
            {
                if (iIndexGame == 0)
                {
                    bRes = false;
                }
                else
                {
                    string[] sLoginedGameArray = sLoginedGame.Split('|');
                    StringBuilder sbText = new StringBuilder(sGameAbbre);
                    foreach (string sLogined in sLoginedGameArray)
                    {
                        if (sGameAbbre != sLogined)
                        {
                            sbText.AppendFormat("|{0}", sLogined);
                        }
                    }
                    SetLogin(sbText.ToString());
                    bRes = true;
                }
            }
            return bRes;
        }

        public string GetUserType(string sUserID)
        {
            string sType = string.Empty;
            int iUserID = 0;
            if (int.TryParse(sUserID, out iUserID) && iUserID > 1000000)
            {
                sType = UserBll.UserTypeSel(iUserID);
            }
            else
            {
                sType = "0";
            }
            return sType;
        }

        public string GetUserType()
        {
            string sType = string.Empty;
            if (Request.Cookies["TypeID"] != null)
            {
                sType = Request.Cookies["TypeID"].Value;
            }
            else
            {
                int iUserID = GetUserID();
                if (iUserID > 1000000)
                {
                    sType = UserBll.UserTypeSel(iUserID);
                }
                else
                {
                    sType = "0";
                }
                SetUserType(sType);
            }
            return sType;
        }

        public void SetUserType(string sTypeID)
        {
            Response.Cookies["TypeID"].Value = sTypeID;
            Response.Cookies["TypeID"].Expires = DateTime.Now.AddDays(1);
        }

        public void SetFromHost(string sFromHost)
        {
            Response.Cookies["fromhost"].Value = sFromHost;
            Response.Cookies["fromhost"].Expires = DateTime.Now.AddHours(1);
        }

        public string GetFromHost()
        {
            string sFromHost = string.Empty;
            if (Request.Cookies["fromhost"] != null)
            {
                sFromHost = Request.Cookies["fromhost"].Value;
            }
            return sFromHost;
        }
    }
}

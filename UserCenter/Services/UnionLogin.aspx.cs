using System;
using System.Configuration;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    public partial class UnionLogin : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        private string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sUserName = CYRequest.GetString("username");
            string sPWD = CYRequest.GetString("pwd");
            string sKey = CYRequest.GetString("key");
            string sGameID = CYRequest.GetString("gameid");
            string sServerid = CYRequest.GetString("serverid");
            if (ValKey(sUserName, sPWD, sKey))
            {
                string sState = UserBll.UserVal(sUserName, sPWD);
                if ("0" == sState)
                {
                    int iUserID = UserBll.UserIDSel(sUserName);
                    string sbbsKey = ConfigurationManager.AppSettings["UserValKey"].ToString();
                    string sUrl = DiscuzUserI.BBSLogin(sUserName, sPWD, sbbsKey);
                    string sPageUrl = Request.Url.ToString();
                    LoginStateSet(sUserName, iUserID, sPageUrl);
                    int iGameID = 0;
                    int.TryParse(sGameID, out iGameID);
                    int iServerID = 0;
                    int.TryParse(sServerid, out iServerID);
                    string sGameAbbre = GameBLL.GameAbbreSel(iGameID, iServerID).Trim();
                    string sGame = GameInfoBLL.GameInfoAbbreSel(sGameAbbre).TrimEnd();
                    string sGameUrl = string.Empty;
                    switch(sGame)
                    {
                        case "sssg":
                            string client = CYRequest.GetString("client");
                            sGameUrl = string.Format("{0}/GCenter/PlayGame.aspx?gn={1}&client={2}", sRootUrl, sGameAbbre,client);
                            break;
                        case "tssg":
                            string fuid = CYRequest.GetString("fuid");
                            sGameUrl = string.Format("{0}/GCenter/PlayGame.aspx?gn={1}&fuid={2}", sRootUrl, sGameAbbre, fuid);
                            break;
                        default:
                            sGameUrl = string.Format("{0}/GCenter/PlayGame.aspx?gn={1}", sRootUrl, sGameAbbre);
                            break;
                    }
                    sMsg = string.Format("<script>location.href='{0}'</script><script src='{1}'></script>", sGameUrl, sUrl);
                }
                else
                {
                    sMsg = string.Format("<script>alert('用户信息输入错误，验证失败！')</script><script>location.href='{0}/Default.aspx'</script>",sRootUrl);
                    return;
                }
            }
            else
            {
                Response.Redirect("../Default.aspx",true);
            }
        }

        private bool ValKey(string sAccount,string sPassWord,string sKey)
        {
            string sValKey = "dao50_unionlogincenter";
            StringBuilder sbText = new StringBuilder();
            sbText.Append(sAccount);
            sbText.Append(sPassWord);
            sbText.Append(sValKey);

            string sMD5Key = ProvideCommon.MD5(sbText.ToString());
            if (("" != sAccount) && ("" != sPassWord) && sMD5Key == sKey)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

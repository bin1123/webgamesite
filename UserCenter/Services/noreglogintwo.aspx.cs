using System;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    public partial class noreglogintwo : pagebase.PageBase
    {
        protected string sMsg = string.Empty;
        protected string sUrl = string.Empty;
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "GET")
            {
                string sWebUrl = WebConfig.BaseConfig.sWUrl;
                string uniqueid = CYRequest.GetString("uniqueid");
                string sGameName = CYRequest.GetString("game");
                string sign = CYRequest.GetString("sign");
                string sKey = "1!s@k#d)}w[l<>";
                StringBuilder sbText = new StringBuilder();
                sbText.Append(uniqueid);
                sbText.Append(sGameName);
                sbText.Append(sKey);
                string sValSign = ProvideCommon.MD5(sbText.ToString()).ToLower();
                if (sign == sValSign)
                {
                    if (!NoRegLoginBLL.NoRegLoginUnionidSel(uniqueid))
                    {
                        sMsg = "uniqueid重复";
                        return;
                    }
                    string sUserName = string.Format("?{0}", ProvideCommon.GenerateStringID());
                    int iTypeID = 1;
                    int iState = 1;
                    string sPassWord = "";
                    int iUID = UserBll.UserReg(sUserName, sPassWord, iTypeID, iState);
                    if (iUID > 1000)
                    {
                        string sPageUrl = Request.Url.ToString();
                        LoginStateSet(sUserName, iUID, sPageUrl);
                        NoRegLoginBLL.NoRegLoginAdd(iUID, uniqueid, sGameName);
                        NoRegLoginBLL.AddUserid(uniqueid, iUID.ToString());
                        if (sGameName.Length > 0)
                        {
                            sUrl = string.Format("/frame/g_mainframe_noreg.aspx?gn={0}",sGameName);
                            return;
                        }
                    }
                    else
                    {
                        sMsg = "注册失败";
                        return;
                    }
                }
                else
                {
                    sMsg = "sign error";
                    return;
                }
            }
        }
    }
}

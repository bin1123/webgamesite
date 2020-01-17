using System;
using System.Text;
using Bussiness;
using Common;

namespace UserCenter.Pay
{
    public partial class PayRes : pagebase.PageBase
    {
        protected string sGameName = string.Empty;
        protected string sGameM = string.Empty;
        protected int iGamePoints = 0;
        protected int iUserPoints = 0;
        protected string sAccount = string.Empty;//消费账号
        protected string sPayAccount = string.Empty;//充值账号
        protected string sWebUrl = WebConfig.BaseConfig.sWebUrl;
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sTranID = CYRequest.GetQueryString("TranID");
            int iPayUserID = TransGBLL.TransSelGUserIDByTID(sTranID);
            sPayAccount = UserBll.AccountSel(iPayUserID).Trim();
            iGamePoints = TransGBLL.TransGamePointsSelByTID(sTranID);
            string sAbbre = CYRequest.GetQueryString("gn");
            string sType = CYRequest.GetQueryString("type");
            string sGAbbre = GameInfoBLL.GameInfoAbbreSel(sAbbre).Trim();
            switch (sGAbbre)
            {
                case "sssg":
                    sGameName = "盛世三国";
                    sGameM = "银(注:1000银=1锭)";
                    break;
                case "sxd":
                    sGameName = "神仙道";
                    sGameM = "元宝";
                    break;
                case "lj":
                    sGameName = "龙将";
                    sGameM = "金币";
                    break;
                case "yjxy":
                    sGameName = "一剑轩辕";
                    sGameM = "元宝";
                    break;
                case "dxz":
                    sGameName = "大侠传";
                    sGameM = "黄金";
                    break;
                case "djj":
                    sGameName = "大将军";
                    sGameM = "黄金";
                    break;
                case "txj":
                    sGameName = "天行剑";
                    sGameM = "元宝";
                    break;
                case "sjsg":
                    sGameName = "神将三国";
                    sGameM = "元宝";
                    break;
                case "tzcq":
                    sGameName = "天尊传奇";
                    sGameM = "元宝";
                    break;
                case "zsg":
                    sGameName = "战三国";
                    sGameM = "元宝";
                    break;
                case "wssg":
                    sGameName = "无双三国";
                    sGameM = "元宝";
                    break;
                case "by":
                    sGameName = "霸域";
                    sGameM = "元宝";
                    break;
                case "mxqy":
                    sGameName = "冒险契约";
                    sGameM = "金币";
                    break;
                case "swjt":
                    sGameName = "神武九天";
                    sGameM = "元宝";
                    break;
                case "gcld":
                    sGameName = "攻城掠地";
                    sGameM = "金币";
                    break;
                case "tjz":
                    sGameName = "天劫传";
                    sGameM = "元宝";
                    break;
                case "khbd":
                    sGameName = "葵花宝典";
                    sGameM = "元宝";
                    break;
                case "sglj":
                    sGameName = "三国论剑";
                    sGameM = "通宝";
                    break;
                case "hyjft":
                    sGameName = "火影疾风坛";
                    sGameM = "金币";
                    break;
                case "llsg":
                    sGameName = "龙狼三国";
                    sGameM = "黄金";
                    break;
                case "nslm":
                    sGameName = "女神联盟";
                    sGameM = "钻石";
                    break;
                case "rxzt":
                    sGameName = "热血遮天";
                    sGameM = "元宝";
                    break;
                case "ahxy":
                    sGameName = "暗黑西游";
                    sGameM = "元宝";
                    break;
                case "mhxy":
                    sGameName = "梦回轩辕";
                    sGameM = "元宝";
                    break;
                case "sxj":
                    sGameName = "神仙劫";
                    sGameM = "元宝";
                    break;
                case "zwj":
                    sGameName = "最无极";
                    sGameM = "元宝";
                    break;
                case "qxz":
                    sGameName = "群侠传";
                    sGameM = "元宝";
                    break;
                case "qszg":
                    sGameName = "骑士战歌";
                    sGameM = "金币";
                    break;
                case "wwsg":
                    sGameName = "威武三国";
                    sGameM = "元宝";
                    break;
                case "dntg":
                    sGameName = "大闹天宫";
                    sGameM = "元宝";
                    break;
                case "ahxx":
                    sGameName = "暗黑修仙";
                    sGameM = "元宝";
                    break;
                case "jjp":
                    sGameName = "将军破";
                    sGameM = "元宝";
                    break;
                case "sgyjz":
                    sGameName = "三国英杰传";
                    sGameM = "金币";
                    break;
                case "dtgzt":
                    sGameName = "太古遮天";
                    sGameM = "元宝";
                    break;
                case "dtgzter":
                    sGameName = "太古遮天2";
                    sGameM = "元宝";
                    break;
            }
            if ("q" == sType)
            {
                sAccount = sPayAccount;
                iUserPoints = UserPointsBLL.UPointAllSel(iPayUserID);
            }
            else
            {
                int iUserID = GetUserID();
                if (iUserID > 999)
                {
                    iUserPoints = UserPointsBLL.UPointAllSel(iUserID);
                    sAccount = GetAccount();
                }
            }
            SetPoints(iUserPoints);
            string sFromHost = GetFromHost();
            if (sFromHost.Length > 5)
            {
                string sServerNum = sAbbre.Replace(sGAbbre, "");
                string sQueryString = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", sTranID, sGameName, sServerNum, sPayAccount, sAccount, iGamePoints, iUserPoints, sGameM);
                string sEncodeQueryString = Server.UrlEncode(sQueryString);
                string sGoUrl = string.Format("http://{0}/PayGSucc.html?{1}", sFromHost, sEncodeQueryString);
                Response.Redirect(sGoUrl, true);
                return;
            }
        }
    }
}

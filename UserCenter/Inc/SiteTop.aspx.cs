using System;
using System.Collections.Generic;
using System.Text;

using Common;
using Bussiness;
using DataEnity;

namespace UserCenter.Inc
{
    public partial class SiteTop : pagebase.PageBase
    {
        protected string sHtml = string.Empty;
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            //function exit(){if(confirm('确认退出平台？')){window.location.href = '<%=sRootUrl %>/Services/userexit.aspx?url='+document.location.href+;};}
            //string sLogined = string.Empty;
            //string sLogin = string.Empty;
            //if (LoginSessionVal() || isLoginCookie())
            //{
            //    string sAccount = GetAccount();
            //    sLogined = string.Format("<div class=top_content><span class=f02><a href=http://www.dao50.com target=blank><img src=http://image.dao50.com/wldFolder/images/logo_small.jpg /></a></span>" +
            //               "<span class=f01>欢迎,<font class=f18>{0}</font><font class=f19>玩武侠游戏来到武林！</font></span>" +
            //               "<span class=f03 onMouseMove=gamehidden()><img src=http://image.dao50.com/wldFolder/images/top_star.jpg vspace=4/></span><span class=f04 onMouseOver=gameshow()><img src=http://image.dao50.com/wldFolder/images/top_youxileibiao.jpg vspace=4/></span>" +
            //               "<span class=f05 onMouseMove=gamehidden()><a href=http://game.dao50.com/pay target=blank>充值</a>|<a onclick=\"return exit()\" href={1}/Services/userexit.aspx>退出</a>&nbsp</span><span class=f06><a href=# onclick=setHomepage()>设为首页</a>|<a href=# onClick =javascript:window.external.AddFavorite(document.URL,document.title);return false rel=sidebar>收藏本页</a>|<a href=http://www.dao50.com/shorturl.asp>保存到武林到桌面</a></span></div>", sAccount, sRootUrl);
            //}
            //else
            //{
            //    sLogin = "<div class=top_content><span class=f02><a href=http://www.dao50.com target=blank><img src=http://image.dao50.com/wldFolder/images/logo_small.jpg /></a></span><span class=f01>欢迎来www.dao50.com<font class=f19>玩武侠游戏来到武林！</font></span>" +
            //                 "<span class=f03 onMouseMove=gamehidden()><img src=http://image.dao50.com/wldFolder/images/top_star.jpg vspace=4/></span><span class=f04 onMouseOver=gameshow()  onMouseOut=gamehidden()><img src=http://image.dao50.com/wldFolder/images/top_youxileibiao.jpg vspace=4 /></span>" +
            //                 "<span class=f05 onMouseMove=gamehidden()><a href=http://game.dao50.com>登陆</a>|<a href=http://game.dao50.com/UCenter/reg.aspx target=blank>注册</a>|<a href=http://game.dao50.com/pay target=blank>充值</a>&nbsp</span>" +
            //                 "<span class=f06><a href=# onclick=setHomepage()>设为首页</a>|<a href=# onClick=javascript:window.external.AddFavorite(document.URL,document.title);return false rel=sidebar>收藏本页</a>|<a href=http://www.dao50.com/shorturl.asp>保存到武林到桌面</a></span></div>";
            //}
            //string sGameList = getGameList();
            //sHtml = string.Format("<div id=top>{0}{1}<div id=youxiliebiao onMouseMove=gameshow() onMouseOut=gamehidden() style=display:none;>{2}</div></div>", sLogin, sLogined, sGameList);
            //Response.Write(string.Format("document.write('{0}');",sHtml));
        }

        //private string getGameList()
        //{
        //    List<GameInfo> lGObject = GameInfoBLL.GameInfoSel();
        //    StringBuilder sbText = new StringBuilder();
        //    foreach(GameInfo giObject in lGObject)
        //    {
        //        sbText.AppendFormat("<a href=http://www.dao50.com/yxzq/{0} target=_blank>{1}</a>",giObject.abbre,giObject.GameName);
        //    }
        //    return sbText.ToString();
        //}
    }
}

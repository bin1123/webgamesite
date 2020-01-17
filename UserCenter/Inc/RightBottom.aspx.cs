using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Common;
using Bussiness;
using DataEnity;

namespace UserCenter.Inc
{
    public partial class RightBottom : pagebase.PageBase
    {
        protected string sRootUrl = ProvideCommon.GetRootURI();

        protected void Page_Load(object sender, EventArgs e)
        {
            XmlDocument objXmlDoc = new XmlDocument();
            string sXmlPath = string.Format("{0}\\{1}", Server.MapPath("~/Inc"), "rightbottom.xml");
            objXmlDoc.Load(sXmlPath);
            string sFirstUrl = objXmlDoc.SelectSingleNode("/data/firstad").Attributes["url"].Value;
            string sFirstText = objXmlDoc.SelectSingleNode("/data/firstad").Attributes["text"].Value;
            string sFirstImg = objXmlDoc.SelectSingleNode("/data/firstad").Attributes["img"].Value;
            string sSecondUrl = objXmlDoc.SelectSingleNode("/data/secondad").Attributes["url"].Value;
            string sSecondText = objXmlDoc.SelectSingleNode("/data/secondad").Attributes["text"].Value;
            string sSecondImg = objXmlDoc.SelectSingleNode("/data/secondad").Attributes["img"].Value;
            
            DateTime dtToday = DateTime.Now.Date;
            DateTime dtTomorow = DateTime.Now.AddDays(1).Date;
            List<ObjectFour> lofObject = ServerBLL.ServerStartAtTime(dtToday,dtTomorow);
            StringBuilder sbTop3Server = new StringBuilder();
            StringBuilder sbAllSever = new StringBuilder();
            int iNumber = 1;
            foreach(ObjectFour ofObject in lofObject)
            {
                if(iNumber < 4)
                {
                    sbTop3Server.AppendFormat("<li><em class=jdtj_dq>{0}</em><a href=http://game.dao50.com/GCenter/wan.aspx?gn={1} target=_blank>{2}</a><a class=jdtj_wenzi href=http://game.dao50.com/GCenter/wan.aspx?gn={1} target=_blank>{3}服</a></li>",
                                                 ofObject.Fourth,ofObject.second,ofObject.third,ofObject.first);
                }
                sbAllSever.AppendFormat("<li><em class=jdtj_dq>{0}</em><a href=http://game.dao50.com/GCenter/wan.aspx?gn={1} target=_blank>{2}</a><a class=jdtj_wenzi href=http://game.dao50.com/GCenter/wan.aspx?gn={1} target=_blank>{3}服</a></li>",
                                             ofObject.Fourth, ofObject.second, ofObject.third, ofObject.first);
                iNumber++;
            }
            string sPath = string.Format("{0}\\{1}", Server.MapPath("~/Inc"), "RightBottom.htm");
            string sHtml = string.Format(ProvideCommon.ReadFile(sPath),sFirstUrl,sFirstText,sFirstImg,sSecondUrl,sSecondText,sSecondImg,sbTop3Server.ToString(),sbAllSever.ToString());
            Response.Write(string.Format("document.write('{0}');",sHtml));
        }

        private string getGameList()
        {
            List<GameInfo> lGObject = GameInfoBLL.GameInfoSel();
            StringBuilder sbText = new StringBuilder();
            foreach(GameInfo giObject in lGObject)
            {
                sbText.AppendFormat("<a href=http://www.dao50.com/yxzq/{0} target=_blank>{1}</a>",giObject.abbre,giObject.GameName);
            }
            return sbText.ToString();
        }
    }
}

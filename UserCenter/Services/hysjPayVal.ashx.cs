using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Text;

using Bussiness;
using Common;

namespace UserCenter.Services
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class hysjPayVal : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";
            StringBuilder sbXml = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sbXml.Append("<response>");
            string sBeginDate = CYRequest.GetString("date");
            if (sBeginDate == "" || sBeginDate == "unsafe string")
            {
                sbXml.Append("<result>0</result>");
            }
            else
            {
                string sSign = CYRequest.GetString("sign");
                if (sSign == "" || sSign == "unsafe string")
                {
                    sbXml.Append("<result>0</result>");
                }
                else
                {
                    if (hysjGame.signVal(sBeginDate, sSign))
                    {
                        IFormatProvider format = new System.Globalization.CultureInfo("zh-CN");
                        string TarStr = "yyyyMMdd";
                        DateTime dBeginDate = DateTime.ParseExact(sBeginDate, TarStr, format);
                        DateTime dEndDate = dBeginDate.AddDays(1);
                        int iGameID = 6;
                        int iGamePoints = TransGBLL.TransSelGPointsByGame(iGameID, dBeginDate, dEndDate);
                        int iPrice = iGamePoints / 10;
                        sbXml.Append("<result>1</result>");
                        sbXml.Append("<exchangeRate>10</exchangeRate>");
                        sbXml.AppendFormat("<localMoneyAmount>{0}</localMoneyAmount>", iPrice.ToString("f2"));
                        sbXml.AppendFormat("<gameMoneyAmount>{0}</gameMoneyAmount>", iGamePoints);
                    }
                    else
                    {
                        sbXml.Append("<result>0</result>");
                    }
                }
            }
            sbXml.Append("</response>");
            context.Response.Write(sbXml.ToString());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}

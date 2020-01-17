using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Security.Cryptography;
using System.Net;
using System.IO;

namespace Common
{
    public class ProvideCommon
    {
        /// <summary>
        /// 通过代理获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetRealIP()
        {
            string ip = "";
            HttpRequest request = HttpContext.Current.Request;

            if (request.Headers["X-Forwarded-For"] != null && request.Headers["X-Forwarded-For"].ToString() != string.Empty)
            {
                string[] ips = request.Headers["X-Forwarded-For"].ToString().Split(',');
                if (ips[0] == "")
                {
                    if (ips.Length > 1)
                        ip = ips[1].Trim();
                }
                else
                {
                    ip = ips[0].Trim();
                }
            }
            if (string.IsNullOrEmpty(ip))
                ip = request.UserHostAddress;
            return ip;
        }

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');

            return ret;
        }

        public static string SHA1(string str)
        {
            string sSHA1String = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1");
            return sSHA1String;
        }

        /// <summary>
        /// 获取网站路径
        /// </summary>
        /// <returns></returns>
        public static string GetRootURI()
        {
            string AppPath = "";
            HttpContext HttpCurrent = HttpContext.Current;
            HttpRequest Req;
            if (HttpCurrent != null)
            {
                Req = HttpCurrent.Request;

                string UrlAuthority = Req.Url.GetLeftPart(UriPartial.Authority);
                if (Req.ApplicationPath == null || Req.ApplicationPath == "/")
                    //直接安装在   Web   站点   
                    AppPath = UrlAuthority;
                else
                    //安装在虚拟子目录下   
                    AppPath = UrlAuthority + Req.ApplicationPath;
            }
            return AppPath;
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="sPath">文件路径</param>
        /// <param name="sFileName">文件名称</param>
        /// <param name="sText">文件内容</param>
        public void WriteLogFile(string sPath,string sFileName,string sText)
        {
            //独占方式，因为文件只能由一个进程写入.
            System.IO.StreamWriter writer = null;
            try
            {
                lock (this)
                {
                    string filename = sFileName + ".txt";
                    if (!System.IO.Directory.Exists(sPath))
                    {
                        System.IO.Directory.CreateDirectory(sPath);
                    }
                    System.IO.FileInfo file = new System.IO.FileInfo(sPath + "/" + filename);
                    writer = new System.IO.StreamWriter(file.FullName,true);
                    writer.WriteLine(sText);
                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="sPath">文件路径</param>
        /// <param name="sFileName">文件名称</param>
        /// <param name="sText">文件内容</param>
        public static string ReadFile(string sPath)
        {
            string sFileText = File.ReadAllText(sPath,Encoding.Default);
            return sFileText;
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="objErr">错误对象</param>
        /// <param name="sPageUrl">报错页面</param>
        /// <param name="sPath">日志路径</param>
        public void WriteSysErr(Exception objErr,string sPageUrl,string sPath)
        {
            string sUserIP = GetRealIP();
            string error = string.Empty;
            string errortime = string.Empty;
            string erroraddr = string.Empty;
            string errorinfo = string.Empty;
            string errorsource = string.Empty;
            string errortrace = string.Empty;

            error += "发生时间:" + System.DateTime.Now.ToString() + "<br>";
            errortime = "发生时间:" + System.DateTime.Now.ToString();

            error += "发生异常页: " + sPageUrl + "<br>";
            erroraddr = "发生异常页: " + sPageUrl;

            error += "异常信息: " + objErr.Message + "<br>";
            errorinfo = "异常信息: " + objErr.Message;
            errorsource = "错误源:" + objErr.Source;
            errortrace = "堆栈信息:" + objErr.StackTrace;
            error += "--------------------------------------<br>";

            //独占方式，因为文件只能由一个进程写入.
            System.IO.StreamWriter writer = null;
            try
            {
                lock (this)
                {
                    string filename = DateTime.Now.Day.ToString() + ".txt";
                    if (!System.IO.Directory.Exists(sPath))
                    {
                        System.IO.Directory.CreateDirectory(sPath);
                    }
                    System.IO.FileInfo file = new System.IO.FileInfo(sPath + "/" + filename);     
                    writer = new System.IO.StreamWriter(file.FullName, true);
                    writer.WriteLine("用户IP:" + sUserIP);
                    writer.WriteLine(errortime);
                    writer.WriteLine(erroraddr);
                    writer.WriteLine(errorinfo);
                    writer.WriteLine(errorsource);
                    writer.WriteLine(errortrace);
                    writer.WriteLine("--------------------------------------------------------------------------------------");
                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        public static string GetPageInfo(string sUrl)
        {
            string sReturn = string.Empty;
            try
            {
                WebRequest Request = WebRequest.Create(sUrl);
                Request.Timeout = 20000;
                WebResponse Response = (HttpWebResponse)Request.GetResponse();
                Stream HttpStream = Response.GetResponseStream();
                StreamReader sr = new StreamReader(HttpStream);
                sReturn = sr.ReadToEnd();
            }                
            catch (Exception exp)
            {
                sReturn = exp.Message;
            }
            return sReturn;
        }

        public static string GetPageInfo(string sUrl, string sEncoding)
        {
            string sReturn = string.Empty;
            try
            {
                Encoding encoding = Encoding.GetEncoding(sEncoding);
                WebRequest Request = WebRequest.Create(sUrl);
                Request.Timeout = 20000;
                WebResponse Response = (HttpWebResponse)Request.GetResponse();
                Stream HttpStream = Response.GetResponseStream();
                StreamReader sr = new StreamReader(HttpStream, encoding);
                sReturn = sr.ReadToEnd();
            }
            catch (Exception exp)
            {
                sReturn = exp.Message;
            }
            return sReturn;
        }

        public static string GetPageInfoByPost(string maiurl, string paramurl,string sEncoding)
        {
            string strHtmlContent = string.Empty;
            HttpWebRequest request;
            try
            {
                Encoding encoding = Encoding.GetEncoding(sEncoding);

                //声明一个HttpWebRequest请求
                request = (HttpWebRequest)WebRequest.Create(maiurl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.AllowAutoRedirect = true;
                byte[] Postbyte = Encoding.ASCII.GetBytes(paramurl);
                request.ContentLength = Postbyte.Length;

                Stream newStream = request.GetRequestStream();
                newStream.Write(Postbyte, 0, Postbyte.Length);//把参数用流对象写入request对象中   
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();//获得服务器响应对象  
                Stream resStream = response.GetResponseStream();//转成流对象   
                StreamReader sr = new StreamReader(resStream, encoding);
                strHtmlContent = sr.ReadToEnd();
                response.Close();
            }
            catch (Exception ex)
            {
                strHtmlContent = ex.ToString();
            }
            finally
            {
                request = null;
            }
            if (strHtmlContent == null)
                strHtmlContent = "";
            return strHtmlContent;
        }
       
        /// <summary>
        /// 验证是否为字符串是否为整数串
        /// </summary>
        /// <param name="message">待验证的字符串</param>
        /// <param name="result">转换结果</param>
        /// <returns></returns>
        public static bool isNumberic(string message, out int result)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            result = -1;
            if (rex.IsMatch(message))
            {
                result = int.Parse(message);
                return true;
            }
            else
                return false;
        }
         
        public static UInt32 getTime()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            return UInt32.Parse(((DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000).ToString());
        }

        public static UInt32 getTime(DateTime dTime)
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            return UInt32.Parse(((dTime.Ticks - timeStamp.Ticks) / 10000000).ToString());
        }

        public static string getHMTime()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            return ((DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000).ToString();
        }

        /// <summary>
        /// 字符串转unicode编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUnicodeString(string str)
        {
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    strResult.Append("\\u");
                    strResult.Append(((int)str[i]).ToString("x"));
                }
            }
            return strResult.ToString();
        }

        /// <summary>
        /// unicode串转字符串
        /// </summary>
        /// <param name="str">包含unicode的串</param>
        /// <returns></returns>
        public static string FromUnicodeString(string str)
        {
            //最直接的方法Regex.Unescape(str);
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        int charCode = Convert.ToInt32(strlist[i], 16);
                        strResult.Append((char)charCode);
                    }
                }
                catch (FormatException ex)
                {
                    return Regex.Unescape(str);
                }
            }
            return strResult.ToString();
        }

        public static bool valTime(string sStartTime, string sEndTime)
        {
            DateTime dtNow = DateTime.Now;
            DateTime dtEndTime;
            if (!DateTime.TryParse(sEndTime, out dtEndTime))
            {
                return false;
            }

            int i = dtNow.CompareTo(dtEndTime);
            if (i > 0)
            {
                return true;
            }
            else
            {
                DateTime dtStartTime = Convert.ToDateTime(sStartTime);
                i = dtNow.CompareTo(dtStartTime);
                if (i < 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 唯一字符串生成
        /// </summary>
        /// <returns></returns>
        public static string GenerateStringID()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {

                i *= ((int)b + 1);

            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 唯一数字序列生成
        /// </summary>
        /// <returns></returns>
        public static long GenerateIntID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// 获取json值
        /// </summary>
        /// <param name="sJsonName">json名</param>
        /// <param name="sJsonText">json值</param>
        /// <returns></returns>
        public static string getJsonValue(string sJsonName, string sJsonText)
        {
            string sJsonValue = string.Empty;
            string sRealJsonText = sJsonText.Replace(" ", "");
            int iJsonIndex = sRealJsonText.IndexOf(sJsonName);
            if (iJsonIndex > -1)
            {
                int iNextIndex = sRealJsonText.IndexOf(',', iJsonIndex);
                string sReplaceString = string.Empty;
                if (iNextIndex < iJsonIndex)
                {
                    sReplaceString = sRealJsonText.Substring(iJsonIndex).Replace("\"", "").Replace("}", "").Replace(string.Format("{0}:", sJsonName), "");
                }
                else
                {
                    int iLen = iNextIndex - iJsonIndex;
                    sReplaceString = sRealJsonText.Substring(iJsonIndex, iLen).Replace("\"", "").Replace(string.Format("{0}:", sJsonName), "");
                }
                sJsonValue = sReplaceString;
            }
            return sJsonValue;
        }

        public static string getJsonValueC(string sJsonName, string sJsonText)
        {
            string sJsonValue = string.Empty;
            int iJsonIndex = sJsonText.IndexOf(sJsonName);
            if (iJsonIndex > -1)
            {
                int iNextIndex = sJsonText.IndexOf(',', iJsonIndex);
                string sReplaceString = string.Empty;
                if (iNextIndex < iJsonIndex)
                {
                    int iEndIndex = sJsonText.IndexOf('}', iJsonIndex);
                    int iLen = iEndIndex - iJsonIndex;
                    sReplaceString = sJsonText.Substring(iJsonIndex,iLen).Replace("\"", "").Replace(string.Format("{0}:", sJsonName), "");
                }
                else
                {
                    int iLen = iNextIndex - iJsonIndex;
                    sReplaceString = sJsonText.Substring(iJsonIndex, iLen).Replace("\"", "").Replace(string.Format("{0}:", sJsonName), "");
                }
                sJsonValue = sReplaceString;
            }
            return sJsonValue;
        }

        public static string getHost(string sUrl)
        {
            string sHost = string.Empty;
            if(sUrl.Length > 5)
            {
                Uri uObject = new Uri(sUrl);
                sHost = uObject.Host;
            }
            return sHost;
        }

        public static string getMultiPP(int iUserID)
        {
            string sMultiPP = string.Empty;
            string sKey = "MULTI_LOGIN_KEY_c1OOpePFrTIUEKE23ll33P3hE3JeoOepeKJKJEKLOOeEeE";
            sMultiPP = ProvideCommon.MD5(string.Format("{0}{1}",iUserID.ToString(),sKey));
            return sMultiPP;
        }

        public static bool valMultiPP(int iUserID,string sMultiPP)
        {
            string sKey = "MULTI_LOGIN_KEY_c1OOpePFrTIUEKE23ll33P3hE3JeoOepeKJKJEKLOOeEeE";
            string sValMultiPP = string.Empty;
            sValMultiPP = ProvideCommon.MD5(string.Format("{0}{1}", iUserID.ToString(), sKey));
            bool bRes = false;
            if(iUserID > 999 && sMultiPP == sValMultiPP)
            {
                bRes = true;
            }
            return bRes;
        }

        public static bool SeverTimeVal(string sEndTime,int iAddDay)
        {
            DateTime dtNow = DateTime.Now;
            DateTime dtEndTime;
            if (!DateTime.TryParse(sEndTime, out dtEndTime))
            {
                return false;
            }
            dtEndTime = dtEndTime.AddDays(iAddDay);
            int i = dtNow.CompareTo(dtEndTime);
            if (i < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool GameIPVal(string sUserIP)
        { 
            bool bRes = false;
            //string sIPFile = @"/CenterData/NoGameIP.txt";
            //string sGameIpPath = string.Format("{0}{1}",AppDomain.CurrentDomain.BaseDirectory,sIPFile);
            //StreamReader srFileRead = new StreamReader(sGameIpPath, Encoding.Default);
            //string sIPData = srFileRead.ReadToEnd();
            //srFileRead.Close();
            string sIPUrl = "http://after.dao50.com/Data/NoGameIP.txt";
            string sIPData = ProvideCommon.GetPageInfo(sIPUrl);
            if(sIPData.IndexOf(sUserIP) < 0)
            {
                bRes = true;
            }
            return bRes;
        }
    }
}

using System;
using System.Web;
using Bussiness;
using Common;
using System.Net;
using System.IO;
using System.Collections;
using System.Text;
//using Memcached.ClientLibrary;

namespace UserCenter
{
    public partial class infoshow : pagebase.PageBase
    {
        protected string sAccount = string.Empty;
        protected int iUserID = 0;
        protected string sUpdatePwdTime = string.Empty;
        protected string sLoginTime = string.Empty;
        //protected string sType = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.test.com/index.htm");
            //System.Net.WebProxy proxy = new WebProxy("127.0.0.1", 80);
            //request.Proxy = proxy;
            //using (WebResponse response = (HttpWebResponse)request.GetResponse())
            //{
            //    StreamReader sr = new StreamReader(response.GetResponseStream());
            //    Response.Write(sr.ReadToEnd());
            //}
            if (!IsPostBack)
            {
                if (Request["action"] == "clear")
                    this.clear();
                else
                    this.test();
            }
            Response.Write(string.Empty.IndexOf("swjt"));
        }

        public void clear()
        {
            //string[] servers = {"127.0.0.1:11211"};

            ////初始化池
            //SockIOPool pool = SockIOPool.GetInstance();
            //pool.SetServers(servers);
            //pool.InitConnections = 3;
            //pool.MinConnections = 3;
            //pool.MaxConnections = 5;
            //pool.SocketConnectTimeout = 1000;
            //pool.SocketTimeout = 3000;
            //pool.MaintenanceSleep = 30;
            //pool.Failover = true;
            //pool.Nagle = false;
            //pool.Initialize();
            //MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            //mc.EnableCompression = false;
            //mc.Delete("cache");
            //mc.Delete("endCache");
            //Response.Write("清空缓存成功");
        }

        public void test()
        {
            ////分布Memcachedf服务IP 端口
            //string[] servers = {"127.0.0.1:11211"};

            ////初始化池
            //SockIOPool pool = SockIOPool.GetInstance();
            //pool.SetServers(servers);
            //pool.InitConnections = 3;
            //pool.MinConnections = 3;
            //pool.MaxConnections = 5;
            //pool.SocketConnectTimeout = 1000;
            //pool.SocketTimeout = 3000;
            //pool.MaintenanceSleep = 30;
            //pool.Failover = true;
            //pool.Nagle = false;
            //pool.Initialize();
            ////客户端实例
            //MemcachedClient mc = new Memcached.ClientLibrary.MemcachedClient();
            //mc.EnableCompression = false;
            //StringBuilder sb = new StringBuilder();
            ////写入缓存
            //sb.AppendLine("写入缓存测试：");
            //sb.AppendLine("<br>_______________________________________<br>");
            //if (mc.KeyExists("cache"))
            //{
            //    sb.AppendLine("缓存cache已存在");
            //}
            //else
            //{
            //    mc.Set("cache", "写入缓存时间：" + DateTime.Now.ToString());
            //    sb.AppendLine("缓存已成功写入到cache");
            //}
            //sb.AppendLine("<br>_______________________________________<br>");
            //sb.AppendLine("读取缓存内容如下：<br>");
            //sb.AppendLine(mc.Get("cache").ToString());

            ////测试缓存过期
            //sb.AppendLine("<br>_______________________________________<br>");
            //if (mc.KeyExists("endCache"))
            //{
            //    sb.AppendLine("缓存endCache已存在，过期时间为：" + mc.Get("endCache").ToString());
            //}
            //else
            //{
            //    mc.Set("endCache", DateTime.Now.AddMinutes(1).ToString(), DateTime.Now.AddMinutes(1));
            //    sb.AppendLine("缓存已更新写入到endCache，写入时间：" + DateTime.Now.ToString() + " 过期时间：" + DateTime.Now.AddMinutes(1).ToString());
            //}

            ////分析缓存状态
            //Hashtable ht = mc.Stats();
            //sb.AppendLine("<br>_______________________________________<br>");
            //sb.AppendLine("Memcached Stats:");
            //sb.AppendLine("<br>_______________________________________<br>");
            //foreach (DictionaryEntry de in ht)
            //{
            //    Hashtable info = (Hashtable)de.Value;
            //    foreach (DictionaryEntry de2 in info)
            //    {
            //        sb.AppendLine(de2.Key.ToString() + ":&nbsp;&nbsp;&nbsp;&nbsp;" + de2.Value.ToString() + "<br>");
            //    }
            //}
            //Response.Write(sb.ToString());
        }
    }
}

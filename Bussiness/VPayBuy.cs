using System.Text;
using System.Web;

using Common;

namespace Bussiness
{
    public class VPayBuy
    {
        private const string spid = "11487";    //商户sp号码
        private const string sppwd = "826672265045182445";  //商户sp校验密钥

        public static string PayDirect(string sOrderID, string sAccount, decimal dPrice, string spcustom, string sprec)
        {
            string servadd = "http://s3.vnetone.com/Default.aspx";    //v币交易提交地址
            string spreq = string.Format("{0}/Pay/",ProvideCommon.GetRootURI());   //换成商户请求地址
            string spname = HttpUtility.UrlEncode("到武林平台");
            string userip = ProvideCommon.GetRealIP();
            string spoid = sOrderID.Substring(0, 30);
            string snpcustom = HttpUtility.UrlEncode(string.Format("{0}|{1}", sOrderID.Substring(30), spcustom));
            string spversion = "vpay1001"; //  '此接口的版本号码 此版本是"vpay1001"            
            string urlcode = "utf-8";//'编码  gbk  gb2312   utf-8  unicode   big5(注意不能一个繁体和简体字交叉写)  你程序的编码
            string money = dPrice.ToString();
            string userid = HttpUtility.UrlEncode(sAccount);
            StringBuilder sbText = new StringBuilder();
            sbText.Append(spoid);
            sbText.Append(spreq);
            sbText.Append(sprec);
            sbText.Append(spid);
            sbText.Append(sppwd);
            sbText.Append(spversion);
            sbText.Append(money);
            string post_key = sbText.ToString();
            string spmd5 = ProvideCommon.MD5(post_key).ToUpper();//  '先MD532 然后转大写
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("<form id='vpaysubmit' name='vpaysubmit' action='{0}' method='post'>", servadd);
            sbText.AppendFormat("<input type='hidden' name='spid' value='{0}'/>", spid);
            sbText.AppendFormat("<input type='hidden' name='spname' value='{0}'/>", spname);
            sbText.AppendFormat("<input type='hidden' name='spoid' value='{0}'/>", spoid);
            sbText.AppendFormat("<input type='hidden' name='spreq' value='{0}'/>", spreq);
            sbText.AppendFormat("<input type='hidden' name='sprec' value='{0}'/>", sprec);
            sbText.AppendFormat("<input type='hidden' name='userid' value='{0}'/>", userid);
            sbText.AppendFormat("<input type='hidden' name='userip' value='{0}'/>", userip);
            sbText.AppendFormat("<input type='hidden' name='spmd5' value='{0}'/>", spmd5);
            sbText.AppendFormat("<input type='hidden' name='spcustom' value='{0}'/>", snpcustom);
            sbText.AppendFormat("<input type='hidden' name='spversion' value='{0}'/>", spversion);
            sbText.AppendFormat("<input type='hidden' name='money' value='{0}'/>", money);
            sbText.AppendFormat("<input type='hidden' name='urlcode' value='{0}'/>", urlcode);
            //submit按钮控件请不要含有name属性
            sbText.Append("<input type='submit' value='submit' style='display:none;'></form>");
            sbText.Append("<script>document.forms['vpaysubmit'].submit();</script>");

            FirstOfPayPointBLL.Add(sOrderID, userip, sbText.ToString());
            return sbText.ToString();
        }

        public static string QuickPayBegin(string sOrderID,string sAccount, decimal dPrice, string sGame)
        {
            string sprec = string.Format("{0}/Pay/QuickVCallBack.aspx", ProvideCommon.GetRootURI());  //换成商户接收地址
            string spcustom = sGame;        //  需要 Server.UrlEncode编码  '客户自定义 30字符内 只能是数字、字母或数字字母的组合。不能用汉字。
            return PayDirect(sOrderID, sAccount, dPrice, spcustom,sprec);
        }

        public static string PayBegin(string sChannel, string sPhone, string sAccount, decimal dPrice, int iCount)
        {
            string sprec = string.Format("{0}/Pay/VCallBack.aspx", ProvideCommon.GetRootURI());  //换成商户接收地址
            string spcustom = "wlb"; //需要 Server.UrlEncode编码  '客户自定义 30字符内 只能是数字、字母或数字字母的组合。不能用汉字。
            string sTranIP = ProvideCommon.GetRealIP();
            string sOrderID = TransPBLL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount, sTranIP);   //定单号;
            return PayDirect(sOrderID, sAccount, dPrice, spcustom, sprec);
        }

        public static string VPayVal()
        {
            string sRes = string.Empty;
            //'接受服务器url get参数                            
            string rtmd5 = CYRequest.GetString("v1"); //   '服务器MD5 
            string trka = CYRequest.GetString("v2");  //  'V币号码15位
            string rtmi = CYRequest.GetString("v3"); //   '密码'V币密码6位 （可能为空 老V币没有密码）
            string rtmz = CYRequest.GetString("v4");    //  '面值1-999 整数
            string rtlx = CYRequest.GetString("v5");    //  '卡的类型  1 2 3 
            string rtoid = CYRequest.GetString("v6"); // '网盈一号通服务器端订单 
            string rtcoid = CYRequest.GetString("v7");  //  '商户自己订单
            string rtuserid = CYRequest.GetString("v8");// '商户的用户ID 
            string rtcustom = CYRequest.GetString("v9"); //'商户自己定义数据
            string rtflag = CYRequest.GetString("v10"); // '返回状态. 1正常发送 2补单发送
            StringBuilder sbText = new StringBuilder();
            sbText.Append(trka);
            sbText.Append(rtmi);
            sbText.Append(rtoid);
            sbText.Append(spid);
            sbText.Append(sppwd);
            sbText.Append(rtcoid);
            sbText.Append(rtflag);
            sbText.Append(rtmz);
            string get_key = sbText.ToString();//string get_key = trka + rtmi + rtoid + spid + sppwd + rtcoid + rtflag + rtmz;
            string md5password = ProvideCommon.MD5(get_key).ToUpper();   //  '先MD5 32 然后转大写
            string sAccount = rtuserid;//获取充值人账户
            decimal dPrice = 0;
            decimal.TryParse(rtmz,out dPrice);
            if (rtflag == "1" || rtflag == "2")
            {
                if (md5password == rtmd5)
                {
                    string sTranID = string.Format("{0}{1}", rtcoid, rtcustom.Split('|')[0]);
                    int j = TransPBLL.PointSalesCommit(sTranID, sAccount, dPrice);    //确认返回信息无误后提交此定单
                    sRes = j.ToString();
                }
                else
                {
                    sRes = string.Format("{0}|{1}",md5password,rtmd5);
                }
            }
            else
            {
                sRes = string.Format("rtflag:{0}",rtflag);
            }
            return sRes;
        }

        public static string VPaySubmit()
        {
            string sRes = string.Empty;
            //'接受服务器url get参数                            
            string rtmd5 = CYRequest.GetString("v1"); //   '服务器MD5 
            string trka = CYRequest.GetString("v2");  //  'V币号码15位
            string rtmi = CYRequest.GetString("v3"); //   '密码'V币密码6位 （可能为空 老V币没有密码）
            string rtmz = CYRequest.GetString("v4");    //  '面值1-999 整数
            string rtlx = CYRequest.GetString("v5");    //  '卡的类型  1 2 3 
            string rtoid = CYRequest.GetString("v6"); // '网盈一号通服务器端订单 
            string rtcoid = CYRequest.GetString("v7");  //  '商户自己订单
            string rtuserid = CYRequest.GetString("v8");// '商户的用户ID 
            string rtcustom = CYRequest.GetString("v9"); //'商户自己定义数据
            string rtflag = CYRequest.GetString("v10"); // '返回状态. 1正常发送 2补单发送
            StringBuilder sbText = new StringBuilder();
            sbText.Append(trka);
            sbText.Append(rtmi);
            sbText.Append(rtoid);
            sbText.Append(spid);
            sbText.Append(sppwd);
            sbText.Append(rtcoid);
            sbText.Append(rtflag);
            sbText.Append(rtmz);
            string get_key = sbText.ToString();//string get_key = trka + rtmi + rtoid + spid + sppwd + rtcoid + rtflag + rtmz;
            //'卡+密+网盈一号通服务器端订单+ 5位spid+ 18位SP密码+商户订单+rtflag返回类型1或2 +面值
            //'LCase函数是将字符转换为小写; Ucase函数是将字符转换为大写
            //'全国声讯支付联盟全国声讯电话支付接口对MD5值只认大写字符串，所以小写的MD5值得转换为大写
            string md5password = ProvideCommon.MD5(get_key).ToUpper();   //  '先MD5 32 然后转大写
            if (md5password == rtmd5)
            {
                string sTranID = string.Format("{0}{1}", rtcoid, rtcustom.Split('|')[0]);
                sRes = string.Format("0|{0}", sTranID);
            }
            else
            {
                sRes = "1";
            }
            return sRes;
        }

        public static string QuickVPaySubmit()
        {
            string sRes = string.Empty;
            //'接受服务器url get参数                            
            string rtmd5 = CYRequest.GetString("v1"); //   '服务器MD5 
            string trka = CYRequest.GetString("v2");  //  'V币号码15位
            string rtmi = CYRequest.GetString("v3"); //   '密码'V币密码6位 （可能为空 老V币没有密码）
            string rtmz = CYRequest.GetString("v4");    //  '面值1-999 整数
            string rtlx = CYRequest.GetString("v5");    //  '卡的类型  1 2 3 
            string rtoid = CYRequest.GetString("v6"); // '网盈一号通服务器端订单 
            string rtcoid = CYRequest.GetString("v7");  //  '商户自己订单
            string rtuserid = CYRequest.GetString("v8");// '商户的用户ID 
            string rtcustom = CYRequest.GetString("v9"); //'商户自己定义数据
            string rtflag = CYRequest.GetString("v10"); // '返回状态. 1正常发送 2补单发送
            StringBuilder sbText = new StringBuilder();
            sbText.Append(trka);
            sbText.Append(rtmi);
            sbText.Append(rtoid);
            sbText.Append(spid);
            sbText.Append(sppwd);
            sbText.Append(rtcoid);
            sbText.Append(rtflag);
            sbText.Append(rtmz);
            string get_key = sbText.ToString();//string get_key = trka + rtmi + rtoid + spid + sppwd + rtcoid + rtflag + rtmz;
            //'卡+密+网盈一号通服务器端订单+ 5位spid+ 18位SP密码+商户订单+rtflag返回类型1或2 +面值
            //'LCase函数是将字符转换为小写; Ucase函数是将字符转换为大写
            //'全国声讯支付联盟全国声讯电话支付接口对MD5值只认大写字符串，所以小写的MD5值得转换为大写
            string md5password = ProvideCommon.MD5(get_key).ToUpper();   //  '先MD5 32 然后转大写
            if (md5password == rtmd5)
            {
                decimal dRtmz = decimal.Parse(rtmz)/2;
                string sTranID = string.Format("{0}{1}", rtcoid, rtcustom.Split('|')[0]);
                string sGTranID = TranQuickBLL.TranQuickGTranIDSel(sTranID);
                TranQuickBLL.TranQuickUpdateP(sTranID);
                string sGameName = rtcustom.Split('|')[1];
                string sGTRes = string.Empty;
                if(sGameName.IndexOf("sq") == -1)
                {
                    sGTRes = PayAll.GameQuickPay(sGameName, rtuserid, dRtmz, sGTranID);
                }
                else
                {
                    string sRoleID = rtcustom.Split('|')[2];
                    sGTRes = PayAll.sqQuickPay(sGameName, rtuserid, dRtmz, sGTranID,sRoleID);
                }
                sbText.Remove(0, sbText.Length);
                if (sGTRes == "0")
                {
                    TranQuickBLL.TranQuickUpdateG(sGTranID);
                    sbText.Append("1|");
                }
                else
                {
                    //游戏冲值失败
                    sbText.Append("3|");
                }
                sbText.Append(sGTranID);
                sbText.AppendFormat("|{0}", rtcustom);
                sRes = sbText.ToString();
            }
            else
            {
                sRes = "验证失败";
            }
            return sRes;
        }
    }
}

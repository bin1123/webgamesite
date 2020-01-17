using System;
using System.Text;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

using Common;

namespace Bussiness
{
    public class QdbPayBuy
    {
        private const string md5key = "Cptfhb6gH4uzz7XtrkTr";  //商户sp校验密钥 

        public static string PayDirect(string sOrderID, decimal dPrice, string privateField, string itemName)
        {
            string servadd = "https://payinterface.qiandai.com/qdpay/entrance.do";
            string interfaceVer = "1.0";
            string tradeMoney = dPrice.ToString("f2");
            string partnerTradeId = sOrderID.Substring(0, 32);//小于等于32位
            string childItem = "ccc";
            string partnerNo = "1000010706";
            string returnUrl = string.Format("{0}/Pay/PayQdb_Return.aspx", ProvideCommon.GetRootURI());//页面返回地址;
            string notifyUrl = string.Format("{0}/Pay/PayQdb_Notify.aspx", ProvideCommon.GetRootURI());//服务器返回地址
            string productName = itemName;
            string productUrl = string.Format("{0}/pay/", ProvideCommon.GetRootURI());
            string remark = string.Format("{0}|{1}", sOrderID.Substring(32), privateField);
            string bankCode = "cnUpop";
            string signType = "1";
            string userIp = ProvideCommon.GetRealIP();
            string tradeFailureTime = "10";
            string place = "externalMerchant";
            string orderTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string isDirect = "1";
            string phone = "1";
            string email = "dao50@dao50.com";
            //组织验签数据
            string signStr = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}", interfaceVer, tradeMoney, partnerTradeId,
                                partnerNo, returnUrl, notifyUrl, productName, productUrl, remark, bankCode, signType,
                                userIp, tradeFailureTime, place, orderTime, md5key);
            //验签数据加密
            string sign = GetMD5(signStr, "gb2312");
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("<form id='qdbpaysubmit' name='qdbpaysubmit' action='{0}' method='post'>", servadd);
            sbText.AppendFormat("<input type='hidden' name='interfaceVer' value='{0}'/>", interfaceVer);
            sbText.AppendFormat("<input type='hidden' name='tradeMoney' value='{0}'/>", tradeMoney);
            sbText.AppendFormat("<input type='hidden' name='partnerTradeId' value='{0}'/>", partnerTradeId);
            sbText.AppendFormat("<input type='hidden' name='partnerNo' value='{0}'/>", partnerNo);
            sbText.AppendFormat("<input type='hidden' name='childItem' value='{0}'/>", childItem);
            sbText.AppendFormat("<input type='hidden' name='returnUrl' value='{0}'/>", returnUrl);
            sbText.AppendFormat("<input type='hidden' name='notifyUrl' value='{0}'/>", notifyUrl);
            sbText.AppendFormat("<input type='hidden' name='productName' value='{0}'/>", productName);
            sbText.AppendFormat("<input type='hidden' name='productUrl' value='{0}'/>", productUrl);
            sbText.AppendFormat("<input type='hidden' name='remark' value='{0}'/>", remark);
            sbText.AppendFormat("<input type='hidden' name='bankCode' value='{0}'/>", bankCode);
            sbText.AppendFormat("<input type='hidden' name='signType' value='{0}'/>", signType);
            sbText.AppendFormat("<input type='hidden' name='userIp' value='{0}'/>", userIp);
            sbText.AppendFormat("<input type='hidden' name='tradeFailureTime' value='{0}'/>", tradeFailureTime);
            sbText.AppendFormat("<input type='hidden' name='place' value='{0}'/>", place);
            sbText.AppendFormat("<input type='hidden' name='orderTime' value='{0}'/>", orderTime);
            sbText.AppendFormat("<input type='hidden' name='sign' value='{0}'/>", sign);
            sbText.AppendFormat("<input type='hidden' name='isDirect' value='{0}'/>", isDirect);
            sbText.AppendFormat("<input type='hidden' name='phone' value='{0}'/>", phone);
            sbText.AppendFormat("<input type='hidden' name='email' value='{0}'/>", email);
            //submit按钮控件请不要含有name属性
            sbText.Append("<input type='submit' value='submit' style='display:none;'></form>");
            sbText.Append("<script>document.forms['qdbpaysubmit'].submit();</script>");
            FirstOfPayPointBLL.Add(sOrderID, userIp, sbText.ToString());
            return sbText.ToString();
        }

        public static string QuickPayBegin(string sOrderID, string sAccount, decimal dPrice, string sGame)
        {
            string privateField = string.Format("{0}|{1}", sAccount, sGame);
            //string sGameName = PayAll.GetGameName(sGame.Split('|')[0]);
            //string itemName = string.Format("{0}{1}元游戏币充值",sGameName,dPrice.ToString());
            string itemName = "gamequcikpay";
            return PayDirect(sOrderID, dPrice, privateField, itemName);
        }

        public static string PayBegin(string sChannel, string sPhone, string sAccount, decimal dPrice, int iCount)
        {
            string sTranIP = ProvideCommon.GetRealIP();
            string sOrderID = TransPBLL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount, sTranIP);   //定单号;
            //string itemName = string.Format("{0}武林币充值",(dPrice*10).ToString());
            string itemName = "wlbpay";
            return PayDirect(sOrderID, dPrice, sAccount, itemName);
        }

        /// <summary>
        /// md5加密，按照字符串格式gb2312
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        public static string GetMD5(string dataStr, string codeType)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(System.Text.Encoding.GetEncoding(codeType).GetBytes(dataStr));
            System.Text.StringBuilder sb = new System.Text.StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        public static string GetKey()
        {
            return md5key;
        }
    }
}

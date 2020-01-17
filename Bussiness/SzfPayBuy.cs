using System;
using System.Text;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

using Common;

namespace Bussiness
{
    public class SzfPayBuy
    {
        private const string privateKey = "yth91678";  //商户sp校验密钥 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sOrderID">订单号</param>
        /// <param name="dPrice">价格</param>
        /// <param name="privateField">商户私有数据</param>
        /// <param name="itemName">产品名称</param>
        /// <param name="gatewayId">支付方式id</param>
        /// <param name="cardTypeCombine">0：移动，1：联通，2：电信</param>
        /// <returns></returns>
        public static string PayDirect(string sOrderID, decimal dPrice, string privateField, string itemName, string gatewayId, string cardTypeCombine)
        {
            string servadd = "http://pay3.shenzhoufu.com/interface/version3/entry.aspx";    //交易提交地址
            string version = "3";//版本号 *
            string merId = "101959";//商户ID *
            int ipayMoney = Convert.ToInt32(dPrice * 100);
            string payMoney = ipayMoney.ToString();//支付金额(单位：分) *
            string orderId = sOrderID;//订单号（格式：yyyyMMdd-merId-SN） * 
            string pageReturnUrl = string.Format("{0}/Pay/PaySzf_Return.aspx", ProvideCommon.GetRootURI());//页面返回地址
            string serverReturnUrl = string.Format("{0}/Pay/PaySzf_Notify.aspx", ProvideCommon.GetRootURI());//服务器返回地址
            string merUserName = "到武林";//商户的用户姓名
            string merUserMail = "";//商户的用户Email
            string itemDesc = "";//产品描述
            string bankId = "";//平台银行ID
            string verifyType = "1";//数据校验方式
            string returnType = "3";//返回结果方式
            string isDebug = "0";//是否调试
            StringBuilder sbText = new StringBuilder();
            sbText.AppendFormat("{0}|",version);
            sbText.AppendFormat("{0}|",merId);
            sbText.AppendFormat("{0}|", payMoney);
            sbText.AppendFormat("{0}|", orderId);
            sbText.AppendFormat("{0}|", pageReturnUrl);
            sbText.AppendFormat("{0}|", serverReturnUrl);
            sbText.AppendFormat("{0}|", privateField);
            sbText.AppendFormat("{0}|", privateKey);
            sbText.AppendFormat("{0}|", verifyType);
            sbText.AppendFormat("{0}|", returnType);
            sbText.Append(isDebug);
            string post_key = sbText.ToString();
            //进行MD5加密
            /*md5String=md5( version+"|"  + merId+"|"  + payMoney+"|"  + orderId+"|"  + pageReturnUrl+"|"  + serverReturnUrl+"|"  + privateField+"|"  + privateKey+"|"  + verifyType+"|"  + returnType+"|"  + isDebug)*/
            string md5String = ProvideCommon.MD5(post_key).ToLower();
            sbText.Remove(0, sbText.Length);
            sbText.AppendFormat("<form id='szfpaysubmit' name='szfpaysubmit' action='{0}' method='post'>", servadd);
            sbText.AppendFormat("<input type='hidden' name='version' value='{0}'/>", version);
            sbText.AppendFormat("<input type='hidden' name='merId' value='{0}'/>", merId);
            sbText.AppendFormat("<input type='hidden' name='payMoney' value='{0}'/>", payMoney);
            sbText.AppendFormat("<input type='hidden' name='orderId' value='{0}'/>", orderId);
            sbText.AppendFormat("<input type='hidden' name='pageReturnUrl' value='{0}'/>", pageReturnUrl);
            sbText.AppendFormat("<input type='hidden' name='serverReturnUrl' value='{0}'/>", serverReturnUrl);
            sbText.AppendFormat("<input type='hidden' name='merUserName' value='{0}'/>", merUserName);
            sbText.AppendFormat("<input type='hidden' name='merUserMail' value='{0}'/>", merUserMail);
            sbText.AppendFormat("<input type='hidden' name='itemName' value='{0}'/>", itemName);
            sbText.AppendFormat("<input type='hidden' name='itemDesc' value='{0}'/>", itemDesc);
            sbText.AppendFormat("<input type='hidden' name='bankId' value='{0}'/>", bankId);
            sbText.AppendFormat("<input type='hidden' name='privateField' value='{0}'/>", privateField);
            sbText.AppendFormat("<input type='hidden' name='md5String' value='{0}'/>", md5String);
            sbText.AppendFormat("<input type='hidden' name='gatewayId' value='{0}'/>", gatewayId);
            sbText.AppendFormat("<input type='hidden' name='cardTypeCombine' value='{0}'/>", cardTypeCombine);
            sbText.AppendFormat("<input type='hidden' name='verifyType' value='{0}'/>", verifyType);
            sbText.AppendFormat("<input type='hidden' name='returnType' value='{0}'/>", returnType);
            sbText.AppendFormat("<input type='hidden' name='isDebug' value='{0}'/>", isDebug);
            sbText.Append("<input type='hidden' name='signString' value=''/>");
            //submit按钮控件请不要含有name属性
            sbText.Append("<input type='submit' value='submit' style='display:none;'></form>");
            sbText.Append("<script>document.forms['szfpaysubmit'].submit();</script>");
            string userip = ProvideCommon.GetRealIP();
            FirstOfPayPointBLL.Add(sOrderID, userip, sbText.ToString());
            return sbText.ToString();
        }

        public static string QuickPayBegin(string sOrderID, string sAccount, decimal dPrice, string sGame, string gatewayId, string cardTypeCombine)
        {
            string privateField = string.Format("{0}|{1}",sAccount,sGame);
            string sGameName = PayAll.GetGameName(sGame.Split('|')[0]);
            string itemName = string.Format("{0}{1}元游戏币充值",sGameName,dPrice.ToString());
            return PayDirect(sOrderID, dPrice,privateField,itemName,gatewayId,cardTypeCombine);
        }

        public static string PayBegin(string sChannel, string sPhone, string sAccount, decimal dPrice, int iCount, string gatewayId, string cardTypeCombine)
        {
            string sTranIP = ProvideCommon.GetRealIP();
            string sOrderID = TransPBLL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount, sTranIP);   //定单号;
            string itemName = string.Format("{0}武林币充值",(dPrice*10).ToString());
            return PayDirect(sOrderID, dPrice, sAccount, itemName,gatewayId,cardTypeCombine);
        }

        public static bool veriSig(String data, String sign)
        {
            X509Store store = new X509Store(StoreName.Root);
            store.Open(OpenFlags.ReadWrite);
            try
            {
                //for .Net Framework2.0(VS .NET 2005)
                X509Certificate2 certificate = new X509Certificate2("c:\\shenzhoufuPay.cer");
                /*
                //for .Net Framework2.0(VS .NET 2005)
                X509Certificate certificate = X509Certificate.CreateFromCertFile("c:\\shenzhoufuPay.cer");	
                */
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

                rsa.FromXmlString(certificate.PublicKey.Key.ToXmlString(false));
                if (rsa.VerifyData(Encoding.UTF8.GetBytes(data), "MD5", Convert.FromBase64String(sign)))
                {
                    //Console.Write("签名认证通过！");
                    return true;
                }
                else
                {
                    //Console.Write("签名认证失败！");
                    return false;
                }
            }
            catch (Exception e)
            {
                //Console.Write(e.Message);
                return false;
            }
        }

        public static string GetKey()
        {
            return privateKey;
        }
    }
}

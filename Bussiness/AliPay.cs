using System.Collections.Generic;

using Com.Alipay;
using Common;
 
/// <summary
/// New Interface for AliPay
/// </summary>
namespace Bussiness
{
    /// <summary>
    /// created by zc 2011.8.18��
    /// </summary>
    public class AliPay
    {
        public static string PayDirect(string sChannel, string sPhone, string sAccount, decimal dPrice, int iCount,string sBankName)
        {
            string sTranIP = ProvideCommon.GetRealIP();
            string out_trade_no = TransPBLL.PointSalesInit(sChannel, sPhone, sAccount, dPrice, iCount,sTranIP);   //������;
            string body = string.Format("Dao50{0}Ԫ��ֵ", dPrice.ToString());            //����������������ϸ��������ע����ʾ��֧��������̨��ġ���Ʒ��������
            string extra_common_param = sAccount;   //�Զ���������ɴ���κ����ݣ���=��&�������ַ��⣩��������ʾ��ҳ����
            string sHtmlText = ParaCreate(out_trade_no,dPrice,sChannel,sBankName,sAccount,body,extra_common_param);
            return sHtmlText;
        }

        private static string ParaCreate(string out_trade_no, decimal dPrice, string sChannel, string sBankName, string sAccount, string body, string extra_common_param)
        {
            ////////////////////////////////////////////�������////////////////////////////////////////////
            //�������//
            //�������ƣ���ʾ��֧��������̨��ġ���Ʒ���ơ����ʾ��֧�����Ľ��׹���ġ���Ʒ���ơ����б��
            string subject = "Dao50GamePAY";
            //�����ܽ���ʾ��֧��������̨��ġ�Ӧ���ܶ��
            string total_fee = dPrice.ToString();

            //��չ���ܲ�������Ĭ��֧����ʽ//

            //Ĭ��֧����ʽ�����������ʱ���ʽӿڡ������ĵ�            
            string paymethod = string.Empty;
            switch (sChannel)
            {
                case "alipay":
                    paymethod = "directPay";
                    break;
                case "ibank":
                    paymethod = "bankPay";
                    break;
            }
            //Ĭ���������ţ������б������ʱ���ʽӿڡ������ĵ�����¼�����������б�
            string defaultbank = sBankName;

            //��չ���ܲ�������������//

            //������ʱ���
            string anti_phishing_key = "";
            //��ȡ�ͻ��˵�IP��ַ�����飺��д��ȡ�ͻ���IP��ַ�ĳ���
            string exter_invoke_ip = ProvideCommon.GetRealIP();
            //ע�⣺
            //������ѡ���Ƿ��������㹦��
            //exter_invoke_ip��anti_phishing_keyһ�������ù�����ô���Ǿͻ��Ϊ�������
            //����ʹ��POST��ʽ��������
            //ʾ����
            //exter_invoke_ip = "";
            //Service aliQuery_timestamp = new Service();
            //anti_phishing_key = aliQuery_timestamp.Query_timestamp();               //��ȡ������ʱ�������

            //��չ���ܲ�����������//

            //��Ʒչʾ��ַ��Ҫ��http:// ��ʽ������·�����������?id=123�����Զ������
            string show_url = "http://game.dao50.com/Pay/";
            //Ĭ�����֧�����˺�
            string buyer_email = "";

            //��չ���ܲ�����������(��Ҫʹ�ã��밴��ע��Ҫ��ĸ�ʽ��ֵ)//

            //������ͣ���ֵΪ�̶�ֵ��10������Ҫ�޸�
            string royalty_type = "";
            //�����Ϣ��
            string royalty_parameters = "";
            //ע�⣺
            //����Ҫ����̻���վ���������̬��ȡÿ�ʽ��׵ĸ������տ��˺š��������������˵�������ֻ������10��
            //����������ܺ���С�ڵ���total_fee
            //�����Ϣ����ʽΪ���տEmail_1^���1^��ע1|�տEmail_2^���2^��ע2
            //ʾ����
            //royalty_type = "10";
            //royalty_parameters = "111@126.com^0.01^����עһ|222@126.com^0.01^����ע��";

            ////////////////////////////////////////////////////////////////////////////////////////////////

            //������������������
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("body", body);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("paymethod", paymethod);
            sParaTemp.Add("defaultbank", defaultbank);
            sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);
            sParaTemp.Add("extra_common_param", extra_common_param);
            sParaTemp.Add("buyer_email", buyer_email);
            sParaTemp.Add("royalty_type", royalty_type);
            sParaTemp.Add("royalty_parameters", royalty_parameters);

            //���켴ʱ���ʽӿڱ��ύHTML���ݣ������޸�
            Service ali = new Service();
            string sHtmlText = ali.Create_direct_pay_by_user(sParaTemp);
            string sTranIP = ProvideCommon.GetRealIP();
            FirstOfPayPointBLL.Add(out_trade_no, sTranIP, sHtmlText);
            return sHtmlText;
        }

        public static string QuickPayDirect(string sTranID, decimal dPrice, string sChannel, string sBankName, string sAccount,string sGameName)
        { 
            string body = string.Format("Dao50{0}Ԫ��Ϸ��ֵ", dPrice.ToString());
            string extra_common_param = string.Format("{0}|{1}", sAccount, sGameName);
            string sHtmlText = ParaCreate(sTranID, dPrice, sChannel, sBankName, sAccount, body, extra_common_param);
            return sHtmlText;
        }
    }
}
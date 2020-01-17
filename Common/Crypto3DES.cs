using System;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// _3DESArithMetic 的摘要说明
/// c#加结密类
/// </summary>
public class Crypto3DES
{
    public Crypto3DES()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private System.Text.Encoding encoding;
    private string key;
    ///<summary>
    /// 获取密匙
    ///</summary>
    public string Key
    {
        get
        {
            return key;
        }
        set
        {
            key = value;
        }
    }

    ///<summary>
    /// 获取或设置加密解密的编码
    ///</summary>
    public System.Text.Encoding Encoding
    {
        get
        { 
            if(encoding == null)
            {
                encoding = System.Text.Encoding.UTF8;
            }
            return encoding;
        }
        set
        {
            encoding = value; 
        }
    }

    ///<summary>
    /// 3DES加密
    ///</summary>
    public string Encrypt3DES(string str_string)
    {
        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        DES.Key = Encoding.GetBytes(this.key);
        DES.Mode = CipherMode.ECB;
        DES.Padding = PaddingMode.Zeros;

        ICryptoTransform DESEncrypt = DES.CreateEncryptor();

        byte[] Buffer = encoding.GetBytes(str_string);

        return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer,0,Buffer.Length));
    }

    ///<summary>
    ///3DES解密
    ///</summary>
    public string Decrypt3DES(string str_string)
    {
        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        DES.Key = Encoding.GetBytes(this.key);
        DES.Mode = CipherMode.ECB;
        DES.Padding = PaddingMode.Zeros;

        ICryptoTransform DESDecrypt = DES.CreateDecryptor();
        byte[] Buffer = Convert.FromBase64String(str_string);
        return UTF8Encoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
    }
}

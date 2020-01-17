using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Common
{
   public class HmacSha1
    {
        /// <summary>
        ///Copyright (C), 2004, kwklover(邝伟科)
        ///File name:Hasher.cs
        ///Author:邝伟科 Version:1.0 Date:2004年4月22日
        ///Description:哈希（不可逆）加密通用类库函数
        /// </summary>
        public class Hasher
        {
            private byte[] _HashKey; //哈希密钥存储变量
            private string _HashText; //待加密的字符串
            public Hasher()
            {
                //
                // TODO: 在此处添加构造函数逻辑
                //
            }

            /// <summary>
            /// 哈希密钥
            /// </summary>
            public byte[] HashKey
            {
                set
                {
                    _HashKey = value;
                }
                get
                {
                    return _HashKey;
                }
            }

            /// <summary>
            /// 需要产生加密哈希的字符串
            /// </summary>
            public string HashText
            {
                set
                {
                    _HashText = value;
                }
                get
                {
                    return _HashText;
                }
            }

            /// <summary>
            /// 使用HMACSHA1类产生长度为 20 字节的哈希序列。需提供相应的密钥，接受任何大小的密钥。
            /// </summary>
            /// <returns></returns>
            public string HMACSHA1Hasher()
            {
                byte[] HmacKey = HashKey;
                byte[] HmacData = System.Text.Encoding.UTF8.GetBytes(HashText);

                HMACSHA1 Hmac = new HMACSHA1(HmacKey);

                CryptoStream cs = new CryptoStream(Stream.Null, Hmac, CryptoStreamMode.Write);
                cs.Write(HmacData, 0, HmacData.Length);
                cs.Close();

                byte[] Result = Hmac.Hash;

                return Convert.ToBase64String(Result); //返回长度为28字节字符串
            }

            /// <summary>
            /// 使用MACTripleDES类产生长度为 8 字节的哈希序列。需提供相应的密钥，密钥长度可为 8、16 或 24 字节的密钥。
            /// </summary>
            /// <returns></returns>
            public string MACTripleDESHasher()
            {
                byte[] MacKey = HashKey;
                byte[] MacData = System.Text.Encoding.UTF8.GetBytes(HashText);

                MACTripleDES Mac = new MACTripleDES(MacKey);

                byte[] Result = Mac.ComputeHash(MacData);

                return Convert.ToBase64String(Result); //返回长度为12字节字符串
            }

            /// <summary>
            /// 使用MD5CryptoServiceProvider类产生哈希值。不需要提供密钥。
            /// </summary>
            /// <returns></returns>
            public string MD5Hasher()
            {
                byte[] MD5Data = System.Text.Encoding.UTF8.GetBytes(HashText);

                MD5 Md5 = new MD5CryptoServiceProvider();

                byte[] Result = Md5.ComputeHash(MD5Data);

                return Convert.ToBase64String(Result); //返回长度为25字节字符串
            }

            /// <summary>
            /// 使用SHA1Managed类产生长度为160位哈希值。不需要提供密钥。
            /// </summary>
            /// <returns></returns>
            public string SHA1ManagedHasher()
            {
                byte[] SHA1Data = System.Text.Encoding.UTF8.GetBytes(HashText);

                SHA1Managed Sha1 = new SHA1Managed();

                byte[] Result = Sha1.ComputeHash(SHA1Data);

                return Convert.ToBase64String(Result); //返回长度为28字节的字符串
            }

            /// <summary>
            /// 使用SHA256Managed类产生长度为256位哈希值。不需要提供密钥。
            /// </summary>
            /// <returns></returns>
            public string SHA256ManagedHasher()
            {
                byte[] SHA256Data = System.Text.Encoding.UTF8.GetBytes(HashText);

                SHA256Managed Sha256 = new SHA256Managed();

                byte[] Result = Sha256.ComputeHash(SHA256Data);

                return Convert.ToBase64String(Result); //返回长度为44字节的字符串
            }

            /// <summary>
            /// 使用SHA384Managed类产生长度为384位哈希值。不需要提供密钥。
            /// </summary>
            /// <returns></returns>
            public string SHA384ManagedHasher()
            {
                byte[] SHA384Data = System.Text.Encoding.UTF8.GetBytes(HashText);

                SHA384Managed Sha384 = new SHA384Managed();

                byte[] Result = Sha384.ComputeHash(SHA384Data);

                return Convert.ToBase64String(Result); //返回长度为64字节的字符串
            }

            /// <summary>
            /// 使用SHA512Managed类产生长度为512位哈希值。不需要提供密钥。
            /// </summary>
            /// <returns></returns>
            public string SHA512ManagedHasher()
            {
                byte[] SHA512Data = System.Text.Encoding.UTF8.GetBytes(HashText);

                SHA512Managed Sha512 = new SHA512Managed();

                byte[] Result = Sha512.ComputeHash(SHA512Data);

                return Convert.ToBase64String(Result); //返回长度为88字节的字符串
            }
        }
    }
}

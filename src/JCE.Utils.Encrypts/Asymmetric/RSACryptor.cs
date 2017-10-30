using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace JCE.Utils.Encrypts.Asymmetric
{
    /// <summary>
    /// RSA 加密
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class RSACryptor
    {        
        /// <summary>
        /// 创建Xml格式的公钥和私钥
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="dwKeySize">生成密钥位数，只能为：512、1024、2048、4096</param>
        public static void CreateKey(out string xmlPrivateKey, out string xmlPublicKey, int dwKeySize = 1024)
        {
            if (!(dwKeySize == 512 || dwKeySize == 1024 || dwKeySize == 2048 || dwKeySize == 4096))
            {
                throw new ArgumentOutOfRangeException(nameof(dwKeySize),"dwKeySize 只能为 512、1024、2048、4096.");
            }
            xmlPrivateKey = null;
            xmlPublicKey = null;
            using (RSA rsa=RSA.Create())
            {
                rsa.KeySize = dwKeySize;
                xmlPrivateKey = rsa.ToExtXmlString(true);
                xmlPublicKey = rsa.ToExtXmlString(false);
            }                                               
        }

        //public static string Encrypt(string value, string key)
        //{
            
        //}

        //public static string Encrypt(string value, string key, Encoding encoding)
        //{
            
        //}
    }

}

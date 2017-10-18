using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using JCE.Utils.Encrypts.Internal;
using JCE.Utils.Extensions;
using JCE.Utils.Helpers;

namespace JCE.Utils.Encrypts
{
    /// <summary>
    /// AES（Advanced Encryption Standard）算法
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class AESCrypt
    {
        /// <summary>
        /// 加密密钥长度
        /// </summary>
        private const int CryptKeyLength = 32;

        /// <summary>
        /// 加密偏移量长度
        /// </summary>
        private const int CryptIvLength = 16;

        /// <summary>
        /// 随机字符串数据源
        /// </summary>
        private static readonly char[] Source =
        {
            'a', 'b', 'd', 'c', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'p', 'r', 'q', 's', 't', 'u', 'v',
            'w', 'z', 'y', 'x',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Q', 'P', 'R', 'T', 'S', 'V', 'U',
            'W', 'X', 'Y', 'Z'
        };

        #region CreateKey(创建 AES 秘钥)

        /// <summary>
        /// 创建 AES 秘钥
        /// </summary>
        /// <returns></returns>
        public static AESKey CreateKey()
        {
            return new AESKey()
            {
                Key = GetRandomStr(CryptKeyLength),
                IV = GetRandomStr(CryptIvLength)
            };
        }

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns></returns>
        private static string GetRandomStr(int length)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
            {
                sb.Append(Source[random.Next(0, Source.Length)].ToString());
            }
            return sb.ToString();
        }

        #endregion

        #region Encrypt(加密)

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key)
        {
            return Encrypt(data, key, Encoding.UTF8);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="encoding">编码方式，默认：UTF-8</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, Encoding encoding)
        {
            data.CheckNotNullOrEmpty(nameof(data));

            key.CheckNotNullOrEmpty(nameof(key));
            key.Length.CheckBetween(nameof(key), CryptKeyLength, CryptKeyLength, true, true);

            if (encoding == null)
            {
                encoding=Encoding.UTF8;
            }

            byte[] plainBytes = encoding.GetBytes(data);
            byte[] keys = new byte[CryptKeyLength];
            Array.Copy(encoding.GetBytes(key.PadRight(keys.Length)), keys, keys.Length);

            var result = Encrypt(plainBytes, keys);
            return System.Convert.ToBase64String(result);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">偏移量</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, string iv)
        {
            return Encrypt(data, key, iv, Encoding.UTF8);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">偏移量</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, string iv, Encoding encoding)
        {
            data.CheckNotNullOrEmpty(nameof(data));

            key.CheckNotNullOrEmpty(nameof(key));
            key.Length.CheckBetween(nameof(key), CryptKeyLength, CryptKeyLength, true, true);

            iv.CheckNotNullOrEmpty(nameof(iv));
            iv.Length.CheckBetween(nameof(iv), CryptIvLength, CryptIvLength, true, true);

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] plainBytes = encoding.GetBytes(data);
            byte[] keys = new byte[CryptKeyLength];
            Array.Copy(encoding.GetBytes(key.PadRight(keys.Length)), keys, keys.Length);
            byte[] ivs = new byte[CryptIvLength];
            Array.Copy(encoding.GetBytes(iv.PadRight(ivs.Length)), ivs, ivs.Length);                        

            var result = Encrypt(plainBytes, keys, ivs);
            return System.Convert.ToBase64String(result);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">偏移量</param>
        /// <param name="keySize">秘钥大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="cipherMode">加密块密码模式</param>
        /// <param name="paddingMode">填充模式</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv = null, int keySize = 256, int blockSize = 128,
            CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = keySize;
                aes.BlockSize = blockSize;
                aes.Key = key;
                if (iv != null)
                {
                    aes.IV = iv;
                }
                aes.Mode = cipherMode;
                aes.Padding = paddingMode;

                ICryptoTransform cTransform = iv!=null?aes.CreateEncryptor(key,iv): aes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(data, 0, data.Length);
                return resultArray;
            }
        }

        //public static string Encrypt(string data,string key,string iv=string.Empty,int )

        #endregion

        #region Decrypt(解密)

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key)
        {
            return Decrypt(data, key, Encoding.UTF8);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">秘钥</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key, Encoding encoding)
        {
            data.CheckNotNullOrEmpty(nameof(data));

            key.CheckNotNullOrEmpty(nameof(key));
            key.Length.CheckBetween(nameof(key), CryptKeyLength, CryptKeyLength, true, true);

            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] encryptedBytes = System.Convert.FromBase64String(data);
            byte[] keys = new byte[CryptKeyLength];
            Array.Copy(encoding.GetBytes(key.PadRight(keys.Length)), keys, keys.Length);

            var resultArray = Decrypt(encryptedBytes, keys);
            return encoding.GetString(resultArray);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">偏移量</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key, string iv)
        {
            return Decrypt(data, key, iv, Encoding.UTF8);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">偏移量</param>
        /// <param name="encoding">编码方式，默认：UTF-8</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key, string iv, Encoding encoding)
        {
            data.CheckNotNullOrEmpty(nameof(data));

            key.CheckNotNullOrEmpty(nameof(key));
            key.Length.CheckBetween(nameof(key),CryptKeyLength,CryptKeyLength,true,true);

            iv.CheckNotNullOrEmpty(nameof(iv));
            iv.Length.CheckBetween(nameof(iv), CryptIvLength, CryptIvLength, true, true);

            if (encoding == null)
            {
                encoding=Encoding.UTF8;
            }

            byte[] encryptedBytes = System.Convert.FromBase64String(data);
            byte[] keys=new byte[CryptKeyLength];
            Array.Copy(encoding.GetBytes(key.PadRight(keys.Length)), keys, keys.Length);
            byte[] ivs = new byte[CryptIvLength];
            Array.Copy(encoding.GetBytes(iv.PadRight(ivs.Length)), ivs, ivs.Length);

            var resultArray = Decrypt(encryptedBytes, keys, ivs);
            return encoding.GetString(resultArray);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">偏移量</param>
        /// <param name="keySize">秘钥大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="cipherMode">加密块密码模式</param>
        /// <param name="paddingMode">填充类型</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv = null, int keySize = 256, int blockSize = 128,
            CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            using (Aes aes=Aes.Create())
            {
                aes.KeySize = keySize;
                aes.BlockSize = blockSize;
                aes.Mode = cipherMode;
                aes.Padding = paddingMode;
                aes.Key = key;
                if (iv != null)
                {
                    aes.IV = iv;
                }
                ICryptoTransform cTransform = aes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(data, 0, data.Length);
                return resultArray;
            }
        }

        #endregion
    }
}

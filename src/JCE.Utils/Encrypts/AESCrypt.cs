using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace JCE.Utils.Encrypts
{
    /// <summary>
    /// AES（Advanced Encryption Standard）算法
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class AESCrypt
    {
        #region 字段

        /// <summary>
        /// AES加密服务提供程序
        /// </summary>
        private AesCryptoServiceProvider _aesCryptoServiceProvider;

        /// <summary>
        /// 加密秘钥长度
        /// </summary>
        private const int CryptoKeyLength = 32;

        /// <summary>
        /// 返回错误码
        /// </summary>
        private const string RetError = "x07x07x07x07x07";

        /// <summary>
        /// 对称算法初始化向量
        /// </summary>
        private readonly byte[] _iv =
        {
            0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD,
            0xEF
        };

        #endregion

        #region 属性

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 密文中是否包含秘钥
        /// </summary>
        public bool ContainKey { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="AESCrypt"/>类型的实例
        /// </summary>
        /// <param name="containKey">密文中是否包含秘钥</param>
        public AESCrypt(bool containKey=true)
        {
            _aesCryptoServiceProvider=new AesCryptoServiceProvider();
            ContainKey = containKey;
            Message = string.Empty;
        }

        #endregion

        #region Encrypt(加密)

        /// <summary>
        /// 加密，动态生成秘钥
        /// </summary>
        /// <param name="data">明文</param>
        /// <returns></returns>
        public string Encrypt(string data)
        {
            byte[] key=new byte[CryptoKeyLength];
            _aesCryptoServiceProvider.GenerateKey();
            key = _aesCryptoServiceProvider.Key;
            return Encrypt(data, key);
        }

        /// <summary>
        /// 加密，指定秘钥对明文进行AES加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">加密秘钥</param>
        /// <returns></returns>
        public string Encrypt(string data, string key)
        {
            byte[] keys=new byte[CryptoKeyLength];
            byte[] temp = String2Byte(key);
            if (temp.Length > keys.Length)
            {
                Message = "Key 太长，必须小于32字节";
                return RetError;
            }
            keys = String2Byte(key.PadRight(keys.Length));
            return Encrypt(data, keys);
        }        

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        private string Encrypt(string data, byte[] key)
        {
            string result = string.Empty;
            try
            {
                byte[] crypto = String2Byte(data);
                _aesCryptoServiceProvider.Key = key;
                _aesCryptoServiceProvider.IV = _iv;
                ICryptoTransform ct = _aesCryptoServiceProvider.CreateEncryptor();
                byte[] encrypted = ct.TransformFinalBlock(crypto, 0, crypto.Length);
                if (ContainKey)
                {
                    result += Byte2HexString(encrypted);
                }
                result += Byte2HexString(encrypted);
                return result;
            }
            catch (Exception ex)
            {
                Message = ex.ToString() + "加密失败。";
                return RetError;
            }
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">秘钥</param>
        /// <param name="encoding">编码方式</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key, Encoding encoding)
        {
            string result = string.Empty;
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrWhiteSpace(data))
            {
                return result;
            }
            if (encoding == null)
            {
                encoding=Encoding.UTF8;
            }
            try
            {
                byte[] keyArray = encoding.GetBytes(key);
                byte[] toEncryptArray = encoding.GetBytes(data);
                var resultArray = Encrypt(keyArray, toEncryptArray);
                result = Convert.ToBase64String(resultArray);
            }
            catch
            {
                result = RetError;
            }
            return result;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="keyArray">秘钥</param>
        /// <param name="data">明文</param>
        /// <param name="iv">偏移量</param>
        /// <param name="keySize">秘钥大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="cipherMode">加密块密码模式</param>
        /// <param name="paddingMode">填充类型</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] keyArray, byte[] data, byte[] iv = null, int keySize = 256,
            int blockSize = 128, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            using (Aes aes=Aes.Create())
            {
                aes.KeySize = keySize;
                aes.BlockSize = blockSize;
                aes.Key = keyArray;
                if (iv != null)
                {
                    aes.IV = iv;
                }
                aes.Mode = cipherMode;
                aes.Padding = paddingMode;

                ICryptoTransform cTransform = aes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(data, 0, data.Length);
                return resultArray;
            }
        }
        #endregion

        #region Decrypt(解密)

        /// <summary>
        /// 解密，从密文中解析出秘钥，并对密文进行AES解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <returns></returns>
        public string Decrypt(string data)
        {
            string keyStr = string.Empty;
            byte[] key=new byte[CryptoKeyLength];
            if (data.Length <= CryptoKeyLength * 2)
            {
                Message = "加密字符串无效。";
                return RetError;
            }
            if (ContainKey)
            {
                keyStr = data.Substring(0, CryptoKeyLength * 2);
                data = data.Substring(CryptoKeyLength * 2);
            }
            key = HexString2Byte(keyStr);
            return Decrypt(data, key);
        }

        /// <summary>
        /// 解密，指定秘钥对密文进行AES解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        public string Decrypt(string data, string key)
        {
            byte[] keys=new byte[CryptoKeyLength];
            byte[] temp = String2Byte(key);
            if (temp.Length > keys.Length)
            {
                Message = "Key 太长，必须小于32字节";
                return RetError;
            }
            keys = String2Byte(key.PadRight(keys.Length));
            if (ContainKey)
            {
                data = data.Substring(CryptoKeyLength * 2);
            }
            return Decrypt(data, keys);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">密文</param>
        /// <param name="key">秘钥</param>
        /// <returns></returns>
        private string Decrypt(string data, byte[] key)
        {
            string result = string.Empty;
            try
            {
                byte[] encrypted = HexString2Byte(data);
                _aesCryptoServiceProvider.Key = key;
                _aesCryptoServiceProvider.IV = _iv;
                ICryptoTransform ct = _aesCryptoServiceProvider.CreateDecryptor();
                byte[] decrypted = ct.TransformFinalBlock(encrypted, 0, encrypted.Length);
                result += Byte2String(decrypted);
                return result;
            }
            catch (Exception ex)
            {
                Message = ex.ToString() + "解密失败。";
                return RetError;
            }
        }

        public static string Decrypt(string data, string key, Encoding encoding)
        {
            string result = string.Empty;
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrWhiteSpace(data))
            {
                return data;
            }
            if (encoding == null)
            {
                encoding=Encoding.UTF8;
            }
            try
            {
                byte[] keyArray = encoding.GetBytes(key);
                byte[] toDecryptArray = Convert.FromBase64String(data);
                var resultArray = Decrypt(keyArray, toDecryptArray);
                result = encoding.GetString(resultArray);
            }
            catch
            {
                result = RetError;
            }
            return result;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="keyArray">秘钥</param>
        /// <param name="toDecryptArray">密文</param>
        /// <param name="iv">偏移量</param>
        /// <param name="keySize">秘钥大小</param>
        /// <param name="blockSize">块大小</param>
        /// <param name="cipherMode">加密块密码模式</param>
        /// <param name="paddingMode">填充类型</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] keyArray, byte[] toDecryptArray, byte[] iv = null, int keySize = 256,
            int blockSize = 128, CipherMode cipherMode = CipherMode.ECB, PaddingMode paddingMode = PaddingMode.PKCS7)
        {
            using (Aes aes=Aes.Create())
            {
                aes.KeySize = keySize;
                aes.BlockSize = blockSize;
                aes.Mode = cipherMode;
                aes.Padding = paddingMode;
                aes.Key = keyArray;
                if (iv != null)
                {
                    aes.IV = iv;
                }
                ICryptoTransform cTransform = aes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
                return resultArray;
            }
        }
        #endregion

        #region 辅助方法

        /// <summary>
        /// Byte[]转16进制字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        private string Byte2HexString(byte[] bytes)
        {
            StringBuilder sb=new StringBuilder();
            foreach (var b in bytes)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 16进制字符串转Byte[]
        /// </summary>
        /// <param name="hex">16进制字符串</param>
        /// <returns></returns>
        private byte[] HexString2Byte(string hex)
        {
            int len = hex.Length / 2;
            byte[] bytes=new byte[len];
            for (int i = 0; i < len; i++)
            {
                bytes[i] = (byte) Convert.ToInt32(hex.Substring(i * 2, 2), 16);
            }
            return bytes;
        }

        /// <summary>
        /// 字符串转Byte[]
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        private byte[] String2Byte(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// Byte[]转字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        private string Byte2String(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        #endregion
    }
}

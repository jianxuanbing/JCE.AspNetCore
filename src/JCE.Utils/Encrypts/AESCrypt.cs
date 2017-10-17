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
        /// 加密，指定秘钥对明文进行AES加密
        /// </summary>
        /// <param name="data">明文</param>
        /// <param name="key">加密秘钥</param>
        /// <returns></returns>
        public string Encrypt(string data, string key)
        {
            byte[] keys=new byte[CryptoKeyLength];
            return null;
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

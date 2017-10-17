using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using JCE.Utils.Extensions;

namespace JCE.Utils.Encrypts
{
    /// <summary>
    /// MD5（Message Digest Algorithm）算法
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class MD5Crypt
    {
        #region Encrypt16(16位加密)

        /// <summary>
        /// 加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <returns></returns>
        public static string Encrypt16(string text)
        {
            return Encrypt16(text, Encoding.UTF8);
        }

        /// <summary>
        /// 加密，返回16位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Encrypt16(string text, Encoding encoding)
        {
            return Encrypt(text, encoding, 4, 8);
        }

        #endregion

        #region Encrypt32(32位加密)

        /// <summary>
        /// 加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <returns></returns>
        public static string Encrypt32(string text)
        {
            return Encrypt32(text, Encoding.UTF8);
        }

        /// <summary>
        /// 加密，返回32位结果
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">字符编码</param>
        /// <returns></returns>
        public static string Encrypt32(string text, Encoding encoding)
        {
            return Encrypt(text, encoding, null, null);
        }

        #endregion

        #region Encrypt(加密)

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text">待加密字符串</param>
        /// <param name="encoding">编码方式</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string Encrypt(string text, Encoding encoding, int? startIndex, int? length)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return string.Empty;
            }
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string result;
            try
            {
                if (startIndex == null)
                {
                    result = BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(text)));
                }
                else
                {
                    result = BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(text)), startIndex.SafeValue(),
                        length.SafeValue());
                }
            }
            finally
            {
                md5.Clear();
            }
            return result.Replace("-", "");
        }

        #endregion

    }
}

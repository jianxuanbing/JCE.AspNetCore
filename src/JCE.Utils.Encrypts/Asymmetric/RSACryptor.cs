using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using JCE.Utils.Extensions;
using Org.BouncyCastle.Crypto.Tls;

namespace JCE.Utils.Encrypts.Asymmetric
{
    /// <summary>
    /// RSA 加密
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class RSACryptor
    {
        #region CreateKey(创建密钥对)

        /// <summary>
        /// 创建密钥对，Xml格式的公钥和私钥
        /// </summary>
        /// <param name="xmlPrivateKey">公钥</param>
        /// <param name="xmlPublicKey">私钥</param>
        /// <param name="size">生成秘钥位数</param>
        public static void CreateKey(out string xmlPrivateKey, out string xmlPublicKey, RsaSize size = RsaSize.R1024)
        {
            CreateKey(out xmlPrivateKey, out xmlPublicKey, size.Value());
        }

        /// <summary>
        /// 创建密钥对，Xml格式的公钥和私钥
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="dwKeySize">生成密钥位数，只能为：512、1024、2048、4096</param>
        public static void CreateKey(out string xmlPrivateKey, out string xmlPublicKey, int dwKeySize = 1024)
        {
            if (!(dwKeySize == 512 || dwKeySize == 1024 || dwKeySize == 2048 || dwKeySize == 4096))
            {
                throw new ArgumentOutOfRangeException(nameof(dwKeySize), "dwKeySize 只能为 512、1024、2048、4096.");
            }
            xmlPrivateKey = null;
            xmlPublicKey = null;
            using (RSA rsa = RSA.Create())
            {
                rsa.KeySize = dwKeySize;
                xmlPrivateKey = rsa.ToExtXmlString(true);
                xmlPublicKey = rsa.ToExtXmlString(false);
            }
        }

        #endregion

        #region Sign(签名)

        /// <summary>
        /// 签名，使用私钥签名
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">密钥</param>
        /// <param name="type">算法类型，默认Rsa</param>
        /// <returns></returns>
        public static string Sign(string value, string key, RsaType type = RsaType.Rsa)
        {
            return Sign(value, key, Encoding.UTF8, type);
        }

        /// <summary>
        /// 签名，使用私钥签名
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string Sign(string value, string key, Encoding encoding)
        {
            return Sign(value, key, encoding, RsaType.Rsa);
        }

        /// <summary>
        /// 签名，使用私钥签名
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        /// <param name="type">算法类型</param>        
        /// <returns></returns>
        public static string Sign(string value, string key, Encoding encoding, RsaType type)
        {
            return Sign(value, key, encoding, type, RSASignaturePadding.Pkcs1);
        }

        /// <summary>
        /// 签名，使用私钥签名
        /// </summary>
        /// <param name="value">待签名的值</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        /// <param name="type">算法类型</param>
        /// <param name="padding">填充方式</param>
        /// <returns></returns>
        public static string Sign(string value, string key, Encoding encoding, RsaType type,
            RSASignaturePadding padding)
        {
            byte[] data = encoding.GetBytes(value);
            var provider = CreateRsaProviderFromPrivateKey(key);
            var signatureBytes = provider.SignData(data, GetHashAlgorithmName(type), padding);
            return Convert.ToBase64String(signatureBytes);
        }

        /// <summary>
        /// 获取哈希算法名
        /// </summary>
        /// <param name="type">Rsa 算法类型</param>
        /// <returns></returns>
        private static HashAlgorithmName GetHashAlgorithmName(RsaType type)
        {
            switch (type)
            {
                case RsaType.Rsa:
                    return HashAlgorithmName.SHA1;
                case RsaType.Rsa2:
                    return HashAlgorithmName.SHA256;
                default:
                    return HashAlgorithmName.SHA1;
            }
        }
        #endregion

        #region Verify(验签，使用公钥验证签名)

        /// <summary>
        /// 验签，使用公钥验证签名
        /// </summary>
        /// <param name="value">原始数据</param>
        /// <param name="sign">签名</param>
        /// <param name="key">密钥</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool Verify(string value, string sign, string key, RsaType type = RsaType.Rsa)
        {
            return Verify(value, sign, key, Encoding.UTF8, RsaType.Rsa);
        }

        /// <summary>
        /// 验签，使用公钥验证签名
        /// </summary>
        /// <param name="value">原始数据</param>
        /// <param name="sign">签名</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static bool Verify(string value, string sign, string key, Encoding encoding)
        {
            return Verify(value, sign, key, encoding, RsaType.Rsa);
        }

        /// <summary>
        /// 验签，使用公钥验证签名
        /// </summary>
        /// <param name="value">原始数据</param>
        /// <param name="sign">签名</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        /// <param name="type">算法类型</param>
        /// <returns></returns>
        public static bool Verify(string value, string sign, string key, Encoding encoding, RsaType type)
        {
            return Verify(value, sign, key, encoding, type, RSASignaturePadding.Pkcs1);
        }

        /// <summary>
        /// 验签，使用公钥验证签名
        /// </summary>
        /// <param name="value">原始数据</param>
        /// <param name="sign">签名</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">编码</param>
        /// <param name="type">算法类型</param>
        /// <param name="padding">填充方式</param>
        /// <returns></returns>
        public static bool Verify(string value, string sign, string key, Encoding encoding, RsaType type,
            RSASignaturePadding padding)
        {
            byte[] dataBytes = encoding.GetBytes(value);
            byte[] signBytes = Convert.FromBase64String(sign);
            var provider = CreateRsaProviderFromPublicKey(key);
            return provider.VerifyData(dataBytes, signBytes, GetHashAlgorithmName(type), padding);
        }

        #endregion

        #region Encrypt(加密)

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">公钥</param>
        /// <returns></returns>
        public static string Encrypt(string value, string key)
        {
            return Encrypt(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">公钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string Encrypt(string value, string key, Encoding encoding)
        {
            return Encrypt(value, key, Encoding.UTF8, RSAEncryptionPadding.Pkcs1);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">公钥</param>
        /// <param name="encoding">编码</param>
        /// <param name="padding">填充方式</param>
        /// <returns></returns>
        public static string Encrypt(string value, string key, Encoding encoding, RSAEncryptionPadding padding)
        {
            byte[] data = encoding.GetBytes(value);
            var provider = CreateRsaProviderFromPublicKey(key);
            return Convert.ToBase64String(provider.Encrypt(data, padding));
        }

        #endregion

        #region EncryptBlock(分块加密)

        /// <summary>
        /// 分块加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">公钥</param>
        /// <returns></returns>
        public static string EncryptBlock(string value, string key)
        {
            return EncryptBlock(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// 分块加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">公钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string EncryptBlock(string value, string key, Encoding encoding)
        {
            return EncryptBlock(value, key, encoding, RSAEncryptionPadding.Pkcs1);
        }

        /// <summary>
        /// 分块加密
        /// </summary>
        /// <param name="value">待加密的值</param>
        /// <param name="key">公钥</param>
        /// <param name="encoding">编码</param>
        /// <param name="padding">填充方式</param>
        /// <returns></returns>
        public static string EncryptBlock(string value, string key, Encoding encoding, RSAEncryptionPadding padding)
        {
            byte[] data = encoding.GetBytes(value);
            var provider = CreateRsaProviderFromPublicKey(key);

            int bufferSize = (provider.KeySize / 8) - 11;//单块最大长度
            var buffer=new byte[bufferSize];
            using (MemoryStream inputStream=new MemoryStream(data),outStream=new MemoryStream())
            {
                while (true)
                {
                    // 分段加密
                    int readSize = inputStream.Read(buffer, 0, bufferSize);
                    if (readSize <= 0)
                    {
                        break;
                    }
                    var temp = new byte[readSize];
                    Array.Copy(buffer,0,temp,0,readSize);
                    RSACryptoServiceProvider a=new RSACryptoServiceProvider();
                    var encryptedBytes = provider.Encrypt(temp, padding);
                    outStream.Write(encryptedBytes,0,encryptedBytes.Length);
                }
                provider.Dispose();
                return Convert.ToBase64String(outStream.ToArray());
            }
        }

        #endregion

        #region Decrypt(解密)

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value">已加密的值</param>
        /// <param name="key">私钥</param>
        /// <returns></returns>
        public static string Decrypt(string value, string key)
        {
            return Decrypt(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value">已加密的值</param>
        /// <param name="key">私钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string Decrypt(string value, string key, Encoding encoding)
        {
            return Decrypt(value, key, encoding, RSAEncryptionPadding.Pkcs1);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="value">已加密的值</param>
        /// <param name="key">私钥</param>
        /// <param name="encoding">编码</param>
        /// <param name="padding">填充方式</param>
        /// <returns></returns>
        public static string Decrypt(string value, string key, Encoding encoding, RSAEncryptionPadding padding)
        {
            byte[] data = Convert.FromBase64String(value);
            var provider = CreateRsaProviderFromPrivateKey(key);
            return encoding.GetString(provider.Decrypt(data, padding));
        }

        #endregion

        #region DecryptBlock(分块解密)

        /// <summary>
        /// 分块解密
        /// </summary>
        /// <param name="value">已加密的值</param>
        /// <param name="key">私钥</param>
        /// <returns></returns>
        public static string DecryptBlock(string value, string key)
        {
            return DecryptBlock(value, key, Encoding.UTF8);
        }

        /// <summary>
        /// 分块解密
        /// </summary>
        /// <param name="value">已加密的值</param>
        /// <param name="key">私钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string DecryptBlock(string value, string key, Encoding encoding)
        {
            return DecryptBlock(value, key, encoding, RSAEncryptionPadding.Pkcs1);
        }

        /// <summary>
        /// 分块解密
        /// </summary>
        /// <param name="value">已加密的值</param>
        /// <param name="key">私钥</param>
        /// <param name="encoding">编码</param>
        /// <param name="padding">填充方式</param>
        /// <returns></returns>
        public static string DecryptBlock(string value, string key, Encoding encoding, RSAEncryptionPadding padding)
        {
            byte[] data = Convert.FromBase64String(value);
            var provider = CreateRsaProviderFromPrivateKey(key);

            int bufferSize = provider.KeySize / 8;//单块最大长度
            var buffer = new byte[bufferSize];

            using (MemoryStream inputStream=new MemoryStream(data),outputStream=new MemoryStream())
            {
                while (true)
                {
                    int readSize = inputStream.Read(buffer, 0, bufferSize);
                    if (readSize <= 0)
                    {
                        break;
                    }
                    var temp = new byte[readSize];
                    Array.Copy(buffer,0,temp,0,readSize);
                    var rawBytes = provider.Decrypt(temp, padding);
                    outputStream.Write(rawBytes,0,rawBytes.Length);
                }
                provider.Dispose();
                return encoding.GetString(outputStream.ToArray());
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 根据私钥创建RSA实例
        /// </summary>
        /// <param name="key">私钥</param>
        /// <returns></returns>
        private static RSA CreateRsaProviderFromPrivateKey(string key)
        {
            var privateKeyBits = Convert.FromBase64String(key);

            var rsa = RSA.Create();
            var rsaParameters = new RSAParameters();

            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                rsaParameters.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.D = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.P = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.Q = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DP = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.DQ = binr.ReadBytes(GetIntegerSize(binr));
                rsaParameters.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            rsa.ImportParameters(rsaParameters);
            return rsa;
        }

        /// <summary>
        /// 根据公钥创建RSA实例
        /// </summary>
        /// <param name="key">公钥</param>
        /// <returns></returns>
        private static RSA CreateRsaProviderFromPublicKey(string key)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];

            var x509Key = Convert.FromBase64String(key);

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------

            using (MemoryStream mem = new MemoryStream(x509Key))
            {
                using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
                {
                    byte bt = 0;
                    ushort twobytes = 0;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    seq = binr.ReadBytes(15);       //read the Sequence OID
                    if (!CompareByteArrays(seq, seqOid))    //make sure Sequence for OID is correct
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x00)     //expect null byte next
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                    int modsize = BitConverter.ToInt32(modint, 0);

                    int firstbyte = binr.PeekChar();
                    if (firstbyte == 0x00)
                    {   //if first byte (highest order) of modulus is zero, don't include it
                        binr.ReadByte();    //skip this null byte
                        modsize -= 1;   //reduce modulus buffer size by 1
                    }

                    byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                        return null;
                    int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                    byte[] exponent = binr.ReadBytes(expbytes);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    var rsa = RSA.Create();
                    RSAParameters rsaKeyInfo = new RSAParameters
                    {
                        Modulus = modulus,
                        Exponent = exponent
                    };
                    rsa.ImportParameters(rsaKeyInfo);

                    return rsa;
                }

            }
        }

        #region 导入密钥算法

        /// <summary>
        /// 获取流长度
        /// </summary>
        /// <param name="reader">流</param>
        /// <returns></returns>
        private static int GetIntegerSize(BinaryReader reader)
        {
            byte bt = 0;
            int count = 0;
            bt = reader.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = reader.ReadByte();

            if (bt == 0x81)
                count = reader.ReadByte();
            else
            if (bt == 0x82)
            {
                var highbyte = reader.ReadByte();
                var lowbyte = reader.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (reader.ReadByte() == 0x00)
            {
                count -= 1;
            }
            reader.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }

        /// <summary>
        /// 比较Byte数组
        /// </summary>
        /// <param name="a">数组A</param>
        /// <param name="b">数组B</param>
        /// <returns></returns>
        private static bool CompareByteArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }
            int i = 0;
            foreach (var c in a)
            {
                if (c != b[i])
                {
                    return false;
                }
                i++;
            }
            return true;
        }

        #endregion

        #endregion
    }
}

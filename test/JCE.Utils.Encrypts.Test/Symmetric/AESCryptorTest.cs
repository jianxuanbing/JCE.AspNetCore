using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using JCE.Utils.Encrypts.Symmetric;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Encrypts.Test.Symmetric
{
    public class AESCryptorTest:TestBase
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        private const string Key = "waP1AF5utIarcBqdhYTZpbGbiNQ9M5IL";

        public AESCryptorTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test_Encrypt()
        {
            var result = AESCryptor.Encrypt("测试一下内容先", Key);
            Output.WriteLine(result);
            Assert.Equal("JWm0M2KzKwxe8ylzkfujgQkyEq+kG1ZVirENQqCl8BI=", result);
        }

        [Fact]
        public void Test_Decrypt()
        {
            var result = AESCryptor.Decrypt("JWm0M2KzKwxe8ylzkfujgQkyEq+kG1ZVirENQqCl8BI=", Key);
            Output.WriteLine(result);
            Assert.Equal("测试一下内容先", result);
        }

        [Fact]
        public void Test_EncryptIv()
        {
            string iv = InitIv(16);
            Output.WriteLine(iv);
            var result = AESCryptor.Encrypt("测试一下内容先", Key, Encoding.UTF8, iv, 256, 128, CipherMode.CBC,
                PaddingMode.PKCS7);
            Output.WriteLine(result);
            Assert.Equal("JWm0M2KzKwxe8ylzkfujgV8i5XUnUaHOpY7MaND0Z3g=",result);
        }

        [Fact]
        public void Test_DecryptIv()
        {
            string iv = InitIv(16);
            Output.WriteLine(iv);
            var result = AESCryptor.Decrypt("JWm0M2KzKwxe8ylzkfujgV8i5XUnUaHOpY7MaND0Z3g=", Key, Encoding.UTF8, iv, 256, 128, CipherMode.CBC,
                PaddingMode.PKCS7);
            Output.WriteLine(result);
            Assert.Equal("测试一下内容先", result);
        }

        [Fact]
        public void Test_EncodingBase64Key()
        {
            byte[] result1 = Convert.FromBase64String(Key);
            byte[] result2 = Encoding.UTF8.GetBytes(Key);
            Assert.Equal(result1,result2);
        }

        private string InitIv(int blockSize)
        {
            byte[] iv=new byte[blockSize];
            for (int i = 0; i < blockSize; i++)
            {
                iv[i] = (byte) 0x0;
            }
            return Encoding.UTF8.GetString(iv);
        }
    }
}

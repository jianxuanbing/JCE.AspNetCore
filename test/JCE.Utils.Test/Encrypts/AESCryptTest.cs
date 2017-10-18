using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Encrypts;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Test.Encrypts
{
    public class AESCryptTest:TestBase
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        private const string Key = "waP1AF5utIarcBqdhYTZpbGbiNQ9M5IL";

        /// <summary>
        /// 偏移量
        /// </summary>
        private const string Iv = "St7Ed8XQ0fJbJhYF";

        public AESCryptTest(ITestOutputHelper output) : base(output)
        {
        }

        /// <summary>
        /// 测试 创建AES秘钥
        /// </summary>
        [Fact]
        public void TestCreateKey()
        {
            var result = AESCrypt.CreateKey();
            Output.WriteLine(result.Key);
            Output.WriteLine(result.IV);
        }

        /// <summary>
        /// 测试 AES加密，无IV
        /// </summary>
        [Fact]
        public void TestEncrypt()
        {
            var result = AESCrypt.Encrypt("测试一下内容先", Key);
            Output.WriteLine(result);
            Assert.Equal("JWm0M2KzKwxe8ylzkfujgQkyEq+kG1ZVirENQqCl8BI=", result);
        }

        /// <summary>
        /// 测试 AES加密，自定义IV
        /// </summary>
        [Fact]
        public void TestEncrypt_1()
        {
            var result = AESCrypt.Encrypt("测试一下内容先", Key,Iv);
            Output.WriteLine(result);
            Assert.Equal("JWm0M2KzKwxe8ylzkfujgQkyEq+kG1ZVirENQqCl8BI=", result);
        }

        /// <summary>
        /// 测试 AES解密，默认IV
        /// </summary>
        [Fact]
        public void TestDecrypt()
        {
            string data = "MNV9brQItfXVZ4T+hW5mgOl4MyApEZ3lZPeYT9mK1Hw=";
            //var result = AESCrypt.Decrypt(data, Key);
            //Output.WriteLine(result);
            //Assert.Equal("测试一下内容先",result);
        }

        /// <summary>
        /// 测试 AES解密，自定义IV
        /// </summary>
        [Fact]
        public void TestDecrypt_1()
        {
            string data = "o1Ab5JjBQJoEBEuYwTavpU7zj4qt1DwbC2NsnUO2LK8=";
            var result = AESCrypt.Decrypt(data, Key,Iv);
            Output.WriteLine(result);
            Assert.Equal("测试一下内容先", result);
        }
    }
}

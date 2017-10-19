using System;
using System.Collections.Generic;
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
    }
}

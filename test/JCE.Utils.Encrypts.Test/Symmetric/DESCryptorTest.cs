using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Encrypts.Symmetric;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Encrypts.Test.Symmetric
{
    public class DESCryptorTest:TestBase
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        private const string Key = "waP1AF5u";

        public DESCryptorTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test_Encrypt()
        {            
            var result = DESCryptor.Encrypt("测试一下内容先", Key);
            Output.WriteLine(result);
            Assert.Equal("W4Xfcp0fM+aKGhBwYvfcPTUkRL0JQVsH", result);
        }

        [Fact]
        public void Test_Decrypt()
        {
            var result = DESCryptor.Decrypt("W4Xfcp0fM+aKGhBwYvfcPTUkRL0JQVsH", Key).Trim();
            Output.WriteLine(result);
            Assert.Equal("测试一下内容先", result);
        }
    }
}

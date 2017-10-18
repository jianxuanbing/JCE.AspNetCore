using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Encrypts.Test
{
    public class Base64CryptoTest:TestBase
    {
        public Base64CryptoTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test_Encrypt()
        {
            var result = Base64Crypto.Encrypt("JCE");
            Output.WriteLine(result);
            Assert.Equal("SkNF",result);
        }

        [Fact]
        public void Test_Decrypt()
        {
            var result = Base64Crypto.Decrypt("SkNF");
            Output.WriteLine(result);
            Assert.Equal("JCE", result);
        }
    }
}

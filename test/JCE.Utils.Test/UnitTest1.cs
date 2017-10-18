using JCE.Utils.Encrypts;
using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Test
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void Test1()
        {
            AESCrypt crypt=new AESCrypt();
            var result=crypt.Encrypt("jce123456","jce");
            Assert.Equal("1A6165D8194F68ACD16516A2C28518201A6165D8194F68ACD16516A2C2851820",result);
            _output.WriteLine(result);

        }
    }
}

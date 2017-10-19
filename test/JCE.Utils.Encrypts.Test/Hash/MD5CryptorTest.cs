using JCE.Utils.Encrypts.Hash;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Encrypts.Test.Hash
{
    public class MD5CryptorTest:TestBase
    {
        public MD5CryptorTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test_Encrypt16()
        {
            var result = MD5Cryptor.Encrypt16("JCE");
            Output.WriteLine(result);
            Assert.Equal("94BA44A879194C4B", result);
        }

        [Fact]
        public void Test_Encrypt32()
        {
            var result = MD5Cryptor.Encrypt32("JCE");
            Output.WriteLine(result);
            Assert.Equal("B89293A694BA44A879194C4BF027AB6B",result);
        }

        [Fact]
        public void Test_Encrypt64()
        {
            var result = MD5Cryptor.Encrypt64("JCE");
            Output.WriteLine(result);
            Assert.Equal("uJKTppS6RKh5GUxL8Ceraw==",result);
        }
    }
}

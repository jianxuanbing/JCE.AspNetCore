using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Encrypts.Hash;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Encrypts.Test.Hash
{
    public class SHACryptorTest:TestBase
    {        
        private const string Source = "MD201711011120370001";
        public SHACryptorTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test_Sha1()
        {
            var result = SHACryptor.Sha1(Source);
            Output.WriteLine(result);
            Assert.Equal("29288e3dd3bc4e2de7676817df387164dde1af39",result);
        }

        [Fact]
        public void Test_Sha256()
        {
            var result = SHACryptor.Sha256(Source);
            Output.WriteLine(result);
            Assert.Equal("bb359fa9532e95ec9e09283dac96943c17206c007f89afdf2c00fc7c41930a9e",result);
        }

        [Fact]
        public void Test_Sha384()
        {
            var result = SHACryptor.Sha384(Source);
            Output.WriteLine(result);
            Assert.Equal("551d245291a875146cbcd640a0fbc8d9eaf447cd4cba272834444a4f401f436dad7654035e473dd82389382ddb646d61", result);
        }

        [Fact]
        public void Test_Sha512()
        {
            var result = SHACryptor.Sha512(Source);
            Output.WriteLine(result);
            Assert.Equal("29443332fe291c69edbce0b9060f0ddd4c912009772a468d52731f4b41867eb666b6bc9ff766180a06352061643abd666b9c6bdbad302c230aac68668b98eaf9", result);
        }
    }
}

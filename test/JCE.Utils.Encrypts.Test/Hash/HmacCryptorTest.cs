using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Encrypts.Hash;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Encrypts.Test.Hash
{
    public class HmacCryptorTest:TestBase
    {
        private const string Key = "123456";
        private const string Source = "MD201711011120370001";
        public HmacCryptorTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test_HmacSha1()
        {
            var result = HmacCryptor.HmacSha1(Source, Key);
            Output.WriteLine(result);
            Assert.Equal("4d5b8eed4e4a8d4cdcdc78ef1537de34cdc4690f", result);
        }

        [Fact]
        public void Test_HmacSha256()
        {
            var result = HmacCryptor.HmacSha256(Source, Key);
            Output.WriteLine(result);
            Assert.Equal("516548a67fc4d0094cdac8886cd372e015aa835291145714e77da11fd1d1c471", result);
        }

        [Fact]
        public void Test_HmacSha384()
        {
            var result = HmacCryptor.HmacSha384(Source, Key);
            Output.WriteLine(result);
            Assert.Equal("204b2b899b5979c1c5ad17f65c3461786294a86888c29c0eeb2bebac13125a31e252334650e1c55664956b2b609a0d53", result);
        }

        [Fact]
        public void Test_HmacSha512()
        {
            var result = HmacCryptor.HmacSha512(Source, Key);
            Output.WriteLine(result);
            Assert.Equal("194058ee50e9f24b42c6abdcc7fb79a008e458b960766f1aa2fd2df8043216c036c433948222269e38dc1bcaadd4fc16507e583f07fe0370216528571fe7d6e0", result);
        }

        [Fact]
        public void Test_HmacMd5()
        {
            var result = HmacCryptor.HmacMd5(Source, Key);
            Output.WriteLine(result);
            Assert.Equal("1e538360d7a8ce373efc8721f6e7043d", result);
        }
    }
}

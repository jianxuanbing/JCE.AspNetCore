using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Encrypts.Asymmetric;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Encrypts.Test.Asymmetric
{
    public class RSACryptorTest:TestBase
    {
        public RSACryptorTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void CreateKey()
        {
            string privateKey;
            string publicKey;
            int size = 512;
            RSACryptor.CreateKey(out privateKey,out publicKey,size);
            Output.WriteLine("密匙长度:" + size);
            Output.WriteLine("私匙:"+Base64Cryptor.Encrypt(privateKey));
            Output.WriteLine("公匙:" + publicKey);
        }
    }
}

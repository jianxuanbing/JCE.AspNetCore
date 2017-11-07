using System;
using JCE.Tools.QrCode.ZXing;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Tools.QrCode.ZXing.Test
{
    public class UnitTest1:TestBase
    {
        [Fact]
        public void Test1()
        {
            var factory = new QrCodeFactory();
            var service=factory.Create("D:\\Test");
            service.Correction(ErrorCorrectionLevel.M);
            service.Size(QrSize.Large);
            var fileName = service.Save("http://www.cnblogs.com/jianxuanbing/p/7376757.html");
            Output.WriteLine(fileName);
        }

        [Fact]
        public void Test2()
        {
            var factory = new QrCodeFactory();
            var service = factory.Create("D:\\Test");
            service.Correction(ErrorCorrectionLevel.M);
            service.Size(QrSize.Large);
            service.Logo(@"D:\Test\test.jpg");
            var fileName = service.Save("http://www.cnblogs.com/jianxuanbing/p/7376757.html");
            Output.WriteLine(fileName);
        }

        public UnitTest1(ITestOutputHelper output) : base(output)
        {
        }
    }
}

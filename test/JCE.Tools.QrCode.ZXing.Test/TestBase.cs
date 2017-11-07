using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace JCE.Tools.QrCode.ZXing.Test
{
    public class TestBase
    {
        protected readonly ITestOutputHelper Output;

        public TestBase(ITestOutputHelper output)
        {
            Output = output;
        }
    }
}

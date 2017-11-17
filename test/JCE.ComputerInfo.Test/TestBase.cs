using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace JCE.ComputerInfo.Test
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

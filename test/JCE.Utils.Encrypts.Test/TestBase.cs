using System;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Encrypts.Test
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

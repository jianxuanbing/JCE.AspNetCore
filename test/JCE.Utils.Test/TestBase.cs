using JCE.Utils.Encrypts;
using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Test
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

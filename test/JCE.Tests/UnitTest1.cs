using System;
using AspectCore.DynamicProxy;
using JCE.Aspects.Base;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Tests
{
    public class UnitTest1:TestBase
    {
        public UnitTest1(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test1()
        {
            Output.WriteLine(typeof(NonAspectAttribute).MetadataToken.ToString());
            Output.WriteLine(typeof(InterceptorBase).MetadataToken.ToString());
        }

       
    }
}

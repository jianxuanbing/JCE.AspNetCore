using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Extensions;
using JCE.Utils.Helpers;
using JCE.Utils.IdGenerators;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Test.Helpers
{
    public class IdTest:TestBase
    {
        public IdTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void TestGenerateId()
        {
            for (int i = 0; i < 100; i++)
            {
                var id = Id.ObjectId();
                Output.WriteLine(id);
                Output.WriteLine(new Guid(id).ToString());
            }
        }

        [Fact]
        public void TestGuid()
        {
            for (int i = 0; i < 100; i++)
            {
                Output.WriteLine(SequentialGuidGenerator.Instance.Create(SequentialGuidType.SequentialAsBinary).ToString());
            }
            
        }
    }
}

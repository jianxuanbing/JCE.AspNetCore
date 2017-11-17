using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace JCE.ComputerInfo.Test
{
    public class CpuInfoTest:TestBase
    {
        public CpuInfoTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test_GetCpuName()
        {
            var result = CpuInfo.GetName();
            Output.WriteLine(result);
        }

        [Fact]
        public void Test_GetProcessorId()
        {
            var result = CpuInfo.GetProcessorId();
            Output.WriteLine(result);
        }

        [Fact]
        public void Test_GetManufacturer()
        {
            var result = CpuInfo.GetManufacturer();
            Output.WriteLine(result);
        }

        [Fact]
        public void Test_GetCurrentMhz()
        {
            var result = CpuInfo.GetCurrentMhz();
            foreach (var item in result)
            {
                Output.WriteLine(item);
            }            
        }

        [Fact]
        public void Test_GetMaxMhz()
        {
            var result = CpuInfo.GetMaxMhz();
            Output.WriteLine(result);
        }

        [Fact]
        public void Test_GetExtMhz()
        {
            var result = CpuInfo.GetExtMhz();
            Output.WriteLine(result);
        }

        [Fact]
        public void Test_GetCurrentVoltage()
        {
            var result = CpuInfo.GetCurrentVoltage();
            Output.WriteLine(result);
        }
    }
}

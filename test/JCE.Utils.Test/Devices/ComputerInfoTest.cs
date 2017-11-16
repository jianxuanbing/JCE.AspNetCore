using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using JCE.Utils.Devices;
using Xunit;
using Xunit.Abstractions;

namespace JCE.Utils.Test.Devices
{
    public class ComputerInfoTest:TestBase
    {
        public ComputerInfoTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test_GetDiskInfos()
        {                                              
            //var result = ComputerInfo.GetDiskInfos();
            //foreach (var diskInfo in result)
            //{
            //    Output.WriteLine($"磁盘名：{diskInfo.Name}，磁盘大小：{diskInfo.Size} KB");
            //}
        }
    }
}
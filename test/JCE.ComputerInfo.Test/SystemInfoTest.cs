using JCE.Utils.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace JCE.ComputerInfo.Test
{
    public class SystemInfoTest:TestBase
    {
        public SystemInfoTest(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Test_GetDiskInfos()
        {

            var result = SystemInfo.GetDiskInfos();
            foreach (var diskInfo in result)
            {
                Output.WriteLine($"磁盘名：{diskInfo.Name}，磁盘大小：{diskInfo.Size} KB");
            }
        }

        [Fact]
        public void Test_GetTotalPhysicalMemory()
        {
            var result = SystemInfo.GetTotalPhysicalMemory();
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Test_GetOsName()
        {
            var result = SystemInfo.GetOsName();
            Output.WriteLine(result);
            Output.WriteLine(Environment.MachineName.ToString());
        }

        [Fact]
        public void Test_GetOsManufacturer()
        {
            var result = SystemInfo.GetOsManufacturer();
            Output.WriteLine(result);
        }

        [Fact]
        public void Test_GetComputerName()
        {
            var result = SystemInfo.GetComputerName();
            Output.WriteLine(result);
            Output.WriteLine(Environment.MachineName);
        }

        [Fact]
        public void Test_GetWindowsDirectory()
        {
            var result = SystemInfo.GetWindowsDirectory();
            Output.WriteLine(result);
        }

        [Fact]
        public void Test_GetSystemDirectory()
        {
            var result = SystemInfo.GetSystemDirectory();
            Output.WriteLine(result);
        }

        [Fact]
        public void Test_GetBootDevice()
        {
            var result = SystemInfo.GetBootDevice();
            Output.WriteLine(result);
        }

        [Fact]
        public void Test_GetFreePhysicalMemory()
        {
            var result = SystemInfo.GetFreePhysicalMemory();
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Test_GetTotalVirtualMemory()
        {
            var result = SystemInfo.GetTotalVirtualMemory();
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Test_GetFreeVirtualMemory()
        {
            var result = SystemInfo.GetFreeVirtualMemory();
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Test_GetPagingFileSize()
        {
            var result = SystemInfo.GetPagingFileSize();
            Output.WriteLine(result.ToString());
        }

        [Fact]
        public void Test_Instance()
        {
            var result = SystemInfo.Instance().ToJson();
            Output.WriteLine(result);
        }
    }
}

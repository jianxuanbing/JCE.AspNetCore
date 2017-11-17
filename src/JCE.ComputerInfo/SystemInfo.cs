using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using JCE.ComputerInfo.Internal;

namespace JCE.ComputerInfo
{
    /// <summary>
    /// 系统信息
    /// </summary>
    public class SystemInfo
    {
        #region 属性        
        
        /// <summary>
        /// 系统名称
        /// </summary>
        public string OsName { get; private set; }

        /// <summary>
        /// 系统制造商
        /// </summary>
        public string OsManufacturer { get; private set; }

        /// <summary>
        /// 系统版本
        /// </summary>
        public string OsVersion { get; private set; }

        /// <summary>
        /// 计算机名
        /// </summary>
        public string ComputerName { get; private set; }

        /// <summary>
        /// Windows 目录
        /// </summary>
        public string WindowsDirectory { get; private set; }

        /// <summary>
        /// 系统目录
        /// </summary>
        public string SystemDirectory { get; private set; }

        /// <summary>
        /// 启动设备
        /// </summary>
        public string BootDevice { get; private set; }

        /// <summary>
        /// 地区
        /// </summary>
        public string Country { get; private set; }

        /// <summary>
        /// 时区
        /// </summary>
        public string TimeZone { get; private set; }

        /// <summary>
        /// 总物理内存
        /// </summary>
        public string TotalPhysicalMemory { get; private set; }

        /// <summary>
        /// 可用物理内存
        /// </summary>
        public string FreePhysicalMemory { get; private set; }

        /// <summary>
        /// 总虚拟内存
        /// </summary>
        public string TotalVirtualMemory { get; private set; }

        /// <summary>
        /// 可用虚拟内存
        /// </summary>
        public string FreeVirtualMemory { get;private set; }

        /// <summary>
        /// 页面文件大小
        /// </summary>
        public string PagingFileSize { get; private set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 初始化一个<see cref="SystemInfo"/>类型的实例
        /// </summary>
        public SystemInfo()
        {
            InitSystemInfo();
        }

        #endregion

        #region Instance(获取系统信息实例)

        /// <summary>
        /// 实例
        /// </summary>
        private static SystemInfo _instance;

        /// <summary>
        /// 获取系统信息实例
        /// </summary>
        /// <returns></returns>
        public static SystemInfo Instance()
        {
            if (_instance == null)
            {
                _instance=new SystemInfo();
            }
            return _instance;
        }

        #endregion

        #region GetOsName(获取系统名称)

        /// <summary>
        /// 获取系统名称
        /// </summary>
        /// <returns></returns>        
        public static string GetOsName()
        {
            using (ManagementClass mc=new ManagementClass(WmiPathConst.OperatingSystem))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.Caption].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetOsManufacturer(获取系统制造商)

        /// <summary>
        /// 获取系统制造商
        /// </summary>
        /// <returns></returns>
        public static string GetOsManufacturer()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.OperatingSystem))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.Manufacturer].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetComputerName(获取计算机名称)

        /// <summary>
        /// 获取计算机名称
        /// </summary>
        /// <returns></returns>
        public static string GetComputerName()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.OperatingSystem))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.CSName].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetWindowsDirectory(获取Windows目录)

        /// <summary>
        /// 获取Windows目录
        /// </summary>
        /// <returns></returns>
        public static string GetWindowsDirectory()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.OperatingSystem))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.WindowsDirectory].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetSystemDirectory(获取系统目录)

        /// <summary>
        /// 获取系统目录
        /// </summary>
        /// <returns></returns>
        public static string GetSystemDirectory()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.OperatingSystem))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.SystemDirectory].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetBootDevice(获取启动设备)

        /// <summary>
        /// 获取启动设备
        /// </summary>
        /// <returns></returns>
        public static string GetBootDevice()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.OperatingSystem))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.BootDevice].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetTotalPhysicalMemory(获取总物理内存)

        /// <summary>
        /// 获取总物理内存
        /// </summary>
        /// <returns></returns>
        public static long GetTotalPhysicalMemory()
        {
            // 方法2：WmiPathConst.OperatingSystem、ManagementObjectConst.TotalVisibleMemorySize            
            using (ManagementClass mc = new ManagementClass(WmiPathConst.ComputerSystem))
            {
                ManagementObjectCollection moc = mc.GetInstances();                
                return moc.OfType<ManagementObject>()
                    .Select(mo => Convert.ToInt64(mo[ManagementObjectConst.TotalPhysicalMemory].ToString()))
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetFreePhysicalMemory(获取可用物理内存)

        /// <summary>
        /// 获取可用物理内存
        /// </summary>
        /// <returns></returns>
        public static long GetFreePhysicalMemory()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.OperatingSystem))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => Convert.ToInt64(mo[ManagementObjectConst.FreePhysicalMemory].ToString()))
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetTotalVirtualMemory(获取总虚拟内存)

        /// <summary>
        /// 获取总虚拟内存
        /// </summary>
        /// <returns></returns>
        public static long GetTotalVirtualMemory()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.OperatingSystem))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => Convert.ToInt64(mo[ManagementObjectConst.TotalVirtualMemorySize].ToString()))
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetFreeVirtualMemory(获取可用虚拟内存)

        /// <summary>
        /// 获取可用虚拟内存
        /// </summary>
        /// <returns></returns>
        public static long GetFreeVirtualMemory()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.OperatingSystem))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => Convert.ToInt64(mo[ManagementObjectConst.FreeVirtualMemory].ToString()))
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetPagingFileSize(获取页面文件大小)

        /// <summary>
        /// 获取页面文件大小
        /// </summary>
        /// <returns></returns>
        public static long GetPagingFileSize()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.OperatingSystem))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => Convert.ToInt64(mo[ManagementObjectConst.SizeStoredInPagingFiles].ToString()))
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetDiskInfos(获取硬盘信息)

        /// <summary>
        /// 获取硬盘信息
        /// </summary>
        /// <returns></returns>
        public static List<DiskInfo> GetDiskInfos()
        {            
            using (ManagementClass mc=new ManagementClass(WmiPathConst.DiskDrive))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>().Select(mo =>
                    new DiskInfo(
                        mo[ManagementObjectConst.Model].ToString(),
                        Convert.ToInt64(mo[ManagementObjectConst.Size])
                    )
                ).ToList();
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 初始化系统信息
        /// </summary>
        private void InitSystemInfo()
        {
            OsVersion = Environment.OSVersion.ToString();
            ComputerName = GetComputerName();
            Country = System.Globalization.CultureInfo.CurrentCulture.DisplayName;
            TimeZone = System.TimeZoneInfo.Local.DisplayName;

            OsName = GetOsName();
            OsManufacturer = GetOsManufacturer();
            WindowsDirectory = GetWindowsDirectory();
            SystemDirectory = GetSystemDirectory();
            BootDevice = GetBootDevice();
            TotalPhysicalMemory = ConvertKbToGb(GetTotalPhysicalMemory());
            FreePhysicalMemory = ConvertKbToGb(GetFreePhysicalMemory());
            TotalVirtualMemory = ConvertKbToGb(GetTotalVirtualMemory());
            FreeVirtualMemory = ConvertKbToGb(GetFreeVirtualMemory());
            PagingFileSize = ConvertKbToGb(GetPagingFileSize());
        }

        /// <summary>
        /// 转换KB为GB
        /// </summary>
        /// <param name="size">字节</param>
        /// <returns></returns>
        private string ConvertKbToGb(long size)
        {
            return (size / 1024.0 / 1024).ToString("#0.00") + "G";
        }

        #endregion

    }
}

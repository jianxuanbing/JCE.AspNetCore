using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using JCE.ComputerInfo.Internal;

namespace JCE.ComputerInfo
{
    /// <summary>
    /// Cpu信息
    /// </summary>
    public class CpuInfo
    {
        #region GetName(获取CPU名称)

        /// <summary>
        /// 获取CPU名称
        /// </summary>
        /// <returns></returns>
        public static string GetName()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.Processor))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.Name].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetProcessorId(获取CPU Id)

        /// <summary>
        /// 获取CPU Id
        /// </summary>
        /// <returns></returns>
        public static string GetProcessorId()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.Processor))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.ProcessorId].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetManufacturer(获取CPU制造商)

        /// <summary>
        /// 获取CPU制造商
        /// </summary>
        /// <returns></returns>
        public static string GetManufacturer()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.Processor))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.Manufacturer].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetCurrentMhz(获取CPU当前频率)

        /// <summary>
        /// 获取CPU当前频率
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCurrentMhz()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.Processor))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.CurrentClockSpeed].ToString())
                    .ToList();
            }
        }

        #endregion

        #region GetMaxMhz(获取CPU最大频率)

        /// <summary>
        /// 获取CPU最大频率
        /// </summary>
        /// <returns></returns>
        public static string GetMaxMhz()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.Processor))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.MaxClockSpeed].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetExtMhz(获取CPU外部频率)

        /// <summary>
        /// 获取CPU外部频率
        /// </summary>
        /// <returns></returns>
        public static string GetExtMhz()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.Processor))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.ExtClock].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetCurrentVoltage(获取CPU当前电压)

        /// <summary>
        /// 获取CPU当前电压
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentVoltage()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.Processor))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.CurrentVoltage].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetL2CacheSize(获取CPU二级缓存)

        /// <summary>
        /// 获取CPU二级缓存
        /// </summary>
        /// <returns></returns>
        public static string GetL2CacheSize()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.Processor))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.L2CacheSize].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetDataWidth(获取CPU数据带宽)

        /// <summary>
        /// 获取CPU数据带宽
        /// </summary>
        /// <returns></returns>
        public static string GetDataWidth()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.Processor))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.DataWidth].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetAddressWidth(获取CPU地址带宽)

        /// <summary>
        /// 获取CPU地址带宽
        /// </summary>
        /// <returns></returns>
        public static string GetAddressWidth()
        {
            using (ManagementClass mc = new ManagementClass(WmiPathConst.Processor))
            {
                ManagementObjectCollection moc = mc.GetInstances();
                return moc.OfType<ManagementObject>()
                    .Select(mo => mo[ManagementObjectConst.AddressWidth].ToString())
                    .FirstOrDefault();
            }
        }

        #endregion

        #region GetTemperature(获取CPU温度)

        /// <summary>
        /// 获取CPU温度
        /// </summary>
        /// <returns></returns>
        public static double GetTemperature()
        {
            ManagementObjectSearcher mos=new ManagementObjectSearcher(@"root\wmi", @"select * from MSAcpi_ThermalZoneTemperature");
            return mos.Get().OfType<ManagementObject>()
                .Select(mo =>
                    Convert.ToDouble(Convert.ToDouble(mo[ManagementObjectConst.CurrentTemperature].ToString()) - 2732) /
                    10)
                .FirstOrDefault();
        }

        #endregion

        #region GetLoadPercentge(获取CPU使用率)
        /// <summary>
        /// 获取CPU使用率
        /// </summary>
        /// <returns></returns>
        public static float GetLoadPercentge()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher(@"root\CIMV2", @"select * from Win32_Processor");
            return mos.Get().OfType<ManagementObject>()
                .Select(mo =>
                    Convert.ToSingle(mo[ManagementObjectConst.CurrentTemperature].ToString()))
                .FirstOrDefault();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using JCE.Utils.Devices.Internal;

namespace JCE.Utils.Devices
{
    /// <summary>
    /// 电脑信息
    /// </summary>
    public class ComputerInfo
    {

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
    }
}

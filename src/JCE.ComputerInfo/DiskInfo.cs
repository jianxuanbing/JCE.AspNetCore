using System;
using System.Collections.Generic;
using System.Text;

namespace JCE.ComputerInfo
{
    /// <summary>
    /// 硬盘信息
    /// </summary>
    public struct DiskInfo
    {
        /// <summary>
        /// 硬盘名
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 容量大小，单位：字节
        /// </summary>
        public long Size { get; }

        /// <summary>
        /// 初始化一个<see cref="DiskInfo"/>类型的实例
        /// </summary>
        /// <param name="name">硬盘名</param>
        /// <param name="size">容量大小，单位：字节</param>
        public DiskInfo(string name, long size)
        {
            Name = name;
            Size = size;
        }
    }
}

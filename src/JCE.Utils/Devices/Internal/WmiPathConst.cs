using System;
using System.Collections.Generic;
using System.Text;

namespace JCE.Utils.Devices.Internal
{
    /// <summary>
    /// WMI 路径常量
    /// </summary>
    public sealed class WmiPathConst
    {
        /// <summary>
        /// 硬件 - CPU处理器
        /// </summary>
        public const string Processor = "Win32_Processor";

        /// <summary>
        /// 硬件 - 物理内存条
        /// </summary>
        public const string PhysicalMemory = "Win32_PhysicalMemory";

        /// <summary>
        /// 硬件 - 键盘
        /// </summary>
        public const string Keyboard = "Win32_Keyboard";

        /// <summary>
        /// 硬件 - 点输入设备，包括鼠标
        /// </summary>
        public const string PointingDevice = "Win32_PointingDevice";

        /// <summary>
        /// 硬件 - 软盘驱动器
        /// </summary>
        public const string FloppyDrive = "Win32_FloppyDrive";

        /// <summary>
        /// 硬件 - 硬盘驱动器
        /// </summary>
        public const string DiskDrive = "Win32_DiskDrive";

        /// <summary>
        /// 硬件 - 光盘驱动器
        /// </summary>
        public const string CdRomDrive = "Win32_CDROMDrive";

        /// <summary>
        /// 硬件 - 主板
        /// </summary>
        public const string BaseBoard = "Win32_BaseBoard";

        /// <summary>
        /// 硬件 - BIOS芯片
        /// </summary>
        public const string Bios = "Win32_BIOS";

        /// <summary>
        /// 硬件 - 并口
        /// </summary>
        public const string ParallelPort = "Win32_ParallelPort";

        /// <summary>
        /// 硬件 - 串口
        /// </summary>
        public const string SerialPort = "Win32_SerialPort";

        /// <summary>
        /// 硬件 - 串口配置
        /// </summary>
        public const string SerialPortConfiguration = "Win32_SerialPortConfiguration";

        /// <summary>
        /// 硬件 - 多媒体设置，一般指声卡
        /// </summary>
        public const string SoundDevice = "Win32_SoundDevice";

        /// <summary>
        /// 硬件 - 主板插槽（ISA & PCI & AGP）
        /// </summary>
        public const string SystemSlot = "Win32_SystemSlot";

        /// <summary>
        /// 硬件 - USB控制器
        /// </summary>
        public const string UsbController = "Win32_USBController";

        /// <summary>
        /// 硬件 - 网络适配器
        /// </summary>
        public const string NetworkAdapter = "Win32_NetworkAdapter";

        /// <summary>
        /// 硬件 - 网络适配器配置
        /// </summary>
        public const string NetworkAdapterConfiguration = "Win32_NetworkAdapterConfiguration";

        /// <summary>
        /// 硬件 - 打印机
        /// </summary>
        public const string Printer = "Win32_Printer";

        /// <summary>
        /// 硬件 - 打印机配置
        /// </summary>
        public const string PrinterConfiguration = "Win32_PrinterConfiguration";

        /// <summary>
        /// 硬件 - 打印机端口
        /// </summary>
        public const string TcpIpPrinterPort = "Win32_TCPIPPrinterPort";

        /// <summary>
        /// 硬件 - MODEM
        /// </summary>
        public const string PotsModem = "Win32_POTSModem";

        /// <summary>
        /// 硬件 - MODEM端口
        /// </summary>
        public const string PotsModemToSerialPort = "Win32_POTSModemToSerialPort";

        /// <summary>
        /// 硬件 - 显示器
        /// </summary>
        public const string DesktopMonitor = "Win32_DesktopMonitor";

        /// <summary>
        /// 硬件 - 显卡配置
        /// </summary>
        public const string DisplayConfiguration = "Win32_DisplayConfiguration";

        /// <summary>
        /// 硬件 - 显卡控制器配置
        /// </summary>
        public const string DisplayControllerConfiguration = "Win32_DisplayControllerConfiguration";

        /// <summary>
        /// 硬件 - 显卡细节
        /// </summary>
        public const string VideoController = "Win32_VideoController";

        /// <summary>
        /// 硬件 - 显卡支持的显示模式
        /// </summary>
        public const string VideoSettings = "Win32_VideoSettings";

        /// <summary>
        /// 操作系统 - 时区
        /// </summary>
        public const string TimeZone = "Win32_TimeZone";

        /// <summary>
        /// 操作系统 - 驱动程序
        /// </summary>
        public const string SystemDriver = "Win32_SystemDriver";

        /// <summary>
        /// 操作系统 - 磁盘分区
        /// </summary>
        public const string DiskPartition = "Win32_DiskPartition";

        /// <summary>
        /// 操作系统 - 逻辑磁盘
        /// </summary>
        public const string LogicalDisk = "Win32_LogicalDisk";

        /// <summary>
        /// 操作系统 - 逻辑磁盘所在分区及始末位置
        /// </summary>
        public const string LogicalDiskToPartition = "Win32_LogicalDiskToPartition";

        /// <summary>
        /// 操作系统 - 逻辑内存配置
        /// </summary>
        public const string LogicalMemoryConfiguration = "Win32_LogicalMemoryConfiguration";

        /// <summary>
        /// 操作系统 - 系统页文件信息
        /// </summary>
        public const string PageFile = "Win32_PageFile";

        /// <summary>
        /// 操作系统 - 页文件设置
        /// </summary>
        public const string PageFileSetting = "Win32_PageFileSetting";

        /// <summary>
        /// 操作系统 - 系统启动配置
        /// </summary>
        public const string BootConfiguration = "Win32_BootConfiguration";

        /// <summary>
        /// 操作系统 - 计算机信息简要
        /// </summary>
        public const string ComputerSystem = "Win32_ComputerSystem";

        /// <summary>
        /// 操作系统 - 操作系统信息
        /// </summary>
        public const string OperatingSystem = "Win32_OperatingSystem";

        /// <summary>
        /// 操作系统 - 系统自动启动程序
        /// </summary>
        public const string StartupCommand = "Win32_StartupCommand";

        /// <summary>
        /// 操作系统 - 系统安装的服务
        /// </summary>
        public const string Service = "Win32_Service";

        /// <summary>
        /// 操作系统 - 系统管理组
        /// </summary>
        public const string Group = "Win32_Group";

        /// <summary>
        /// 操作系统 - 系统组账号
        /// </summary>
        public const string GroupUser = "Win32_GroupUser";

        /// <summary>
        /// 操作系统 - 用户账号
        /// </summary>
        public const string UserAccount = "Win32_UserAccount";

        /// <summary>
        /// 操作系统 - 系统进程
        /// </summary>
        public const string Process = "Win32_Process";

        /// <summary>
        /// 操作系统 - 系统线程
        /// </summary>
        public const string Thread = "Win32_Thread";

        /// <summary>
        /// 操作系统 - 共享
        /// </summary>
        public const string Share = "Win32_Share";

        /// <summary>
        /// 操作系统 - 已安装的网络客户端
        /// </summary>
        public const string NetworkClient = "Win32_NetworkClient";

        /// <summary>
        /// 操作系统 - 已安装的网络协议
        /// </summary>
        public const string NetworkProtocol = "Win32_NetworkProtocol";
    }
}

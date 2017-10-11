using System;
using System.Collections.Generic;
using System.Text;

namespace JCE.GlobalConfigs.Models
{
    /// <summary>
    /// 框架配置信息实体
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class JCEConfig
    {
        /// <summary>
        /// 日志相关
        /// </summary>
        public LogConfig Logger { get; set; }

        /// <summary>
        /// 初始化一个<see cref="JCEConfig"/>类型的实例
        /// </summary>
        public JCEConfig()
        {
            Logger=new LogConfig();
        }
    }
}

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
        /// 日志 相关
        /// </summary>
        public LogConfig Logger { get; set; }

        /// <summary>
        /// 用户上下文 相关
        /// </summary>
        public UserContextConfig UserContext { get; set; }

        /// <summary>
        /// 初始化一个<see cref="JCEConfig"/>类型的实例
        /// </summary>
        public JCEConfig()
        {
            Logger=new LogConfig();
            UserContext = new UserContextConfig();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JCE.GlobalConfigs.Models;
using JCE.Utils.Json;

namespace JCE.GlobalConfigs
{
    /// <summary>
    /// 框架级配置信息初始化，默认使用json进行存储
    /// </summary>
    public class ConfigManager
    {
        #region 属性

        /// <summary>
        /// 框架配置
        /// </summary>
        private static JCEConfig _config;

        /// <summary>
        /// 框架配置文件路径
        /// </summary>
        private static string _fileName = Path.Combine(Directory.GetCurrentDirectory(), "JCEConfig.json");

        /// <summary>
        /// 对象锁
        /// </summary>
        private static object _lockObj=new object();

        /// <summary>
        /// 模型初始化配置
        /// </summary>
        private static JCEConfig _initConfig;

        /// <summary>
        /// 配置字典，单例模式
        /// </summary>
        public static JCEConfig Config
        {
            get
            {
                if (_config == null)
                {
                    lock (_lockObj)
                    {
                        var old = JsonUtil.DeserializeFromFile<JCEConfig>(_fileName);
                        if (old != null)
                        {
                            _config = old;
                        }
                        else
                        {
                            JsonUtil.SerializableToFile(_fileName,_initConfig);
                            _config = _initConfig;
                        }
                    }
                }
                return _config;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ConfigManager()
        {
            _initConfig=new JCEConfig();

            _initConfig.Logger.EnabledDebug = true;
            _initConfig.Logger.EnabledTrace = true;
            _initConfig.Logger.Level = "DEBUG";
            _initConfig.Logger.Type = "File";
            _initConfig.Logger.ProjectName = "JCE";

            _initConfig.UserContext.EnabledUserName = false;
        }

        #endregion        
    }
}

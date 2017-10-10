using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JCE.Utils.Configs
{
    /// <summary>
    /// 配置辅助操作类
    /// </summary>
    public static class ConfigUtil
    {
        /// <summary>
        /// 获取Json配置文件
        /// </summary>
        /// <param name="configFileName">配置文件名，默认：appsettings.json</param>
        /// <param name="basePath">基路径</param>
        /// <returns></returns>
        public static IConfigurationRoot GetJsonConfig(string configFileName="appsettings.json",string basePath = "")
        {
            basePath = string.IsNullOrWhiteSpace(basePath) ? Directory.GetCurrentDirectory() : basePath;

            var builder = new ConfigurationBuilder().SetBasePath(basePath).AddJsonFile(configFileName);
            return builder.Build();
        }

        /// <summary>
        /// 获取Xml配置文件
        /// </summary>
        /// <param name="configFileName">配置文件名，默认：appsettings.xml</param>
        /// <param name="basePath">基路径</param>
        /// <returns></returns>
        public static IConfigurationRoot GetXmlConfig(string configFileName="appsettings.xml",string basePath = "")
        {
            basePath = string.IsNullOrWhiteSpace(basePath) ? Directory.GetCurrentDirectory() : basePath;

            var builder = new ConfigurationBuilder().AddXmlFile(config=>
            {
                config.Path = configFileName;
                config.FileProvider = new PhysicalFileProvider(basePath);
            });
            return builder.Build();
        }
    }
}

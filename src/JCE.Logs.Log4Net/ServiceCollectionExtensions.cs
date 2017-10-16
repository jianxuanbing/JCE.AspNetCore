
using System.IO;
using System.Xml;
using JCE.Logs.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace JCE.Logs.Log4Net
{
    /// <summary>
    /// 日志服务 扩展
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册Log4Net日志操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="log4NetConfigFile">log4net配置文件</param>
        public static void AddLog4Net(this IServiceCollection services, string log4NetConfigFile="log4net.config")
        {
            services.AddScoped<ILogProviderFactory, JCE.Logs.Log4Net.LogProviderFactory>();
            services.AddScoped<ILogFormat, JCE.Logs.Formats.ContentFormat>();
            services.AddScoped<ILogContext, JCE.Logs.Core.LogContext>();
            services.AddScoped<ILog, JCE.Logs.Log>();

            Log4NetProvider.InitRepository(log4NetConfigFile);
        }
    }
}
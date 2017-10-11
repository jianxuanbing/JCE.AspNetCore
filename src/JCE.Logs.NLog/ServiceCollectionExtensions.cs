using System;
using System.Collections.Generic;
using System.Text;
using JCE.Logs.Abstractions;
using JCE.Logs.Formats;
using Microsoft.Extensions.DependencyInjection;

namespace JCE.Logs.NLog
{
    /// <summary>
    /// 日志服务 扩展
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册NLog日志操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns></returns>
        public static void AddNLog(this IServiceCollection services)
        {
            services.AddScoped<ILogProviderFactory, JCE.Logs.NLog.LogProviderFactory>();
            services.AddSingleton<ILogFormat, ContentFormat>();
            services.AddScoped<ILogContext, JCE.Logs.Core.LogContext>();
            services.AddScoped<ILog, JCE.Logs.Log>();
        }
    }
}

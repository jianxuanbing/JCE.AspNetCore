using System;
using System.Collections.Generic;
using System.Text;
using Exceptionless;
using JCE.Logs.Abstractions;
using JCE.Logs.Core;
using Microsoft.Extensions.DependencyInjection;

namespace JCE.Logs.Exceptionless
{
    /// <summary>
    /// 日志服务 扩展
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册Exceptionless日志操作
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置操作</param>
        public static void AddExceptionless(this IServiceCollection services,
            Action<ExceptionlessConfiguration> configAction)
        {
            services.AddScoped<ILogProviderFactory, JCE.Logs.Exceptionless.LogProviderFactory>();
            services.AddSingleton(typeof(ILogFormat), t => NullLogFormat.Instance);
            services.AddScoped<ILogContext, JCE.Logs.Exceptionless.LogContext>();
            services.AddScoped<ILog, Log>();
            configAction?.Invoke(ExceptionlessClient.Default.Configuration);
        }
    }
}

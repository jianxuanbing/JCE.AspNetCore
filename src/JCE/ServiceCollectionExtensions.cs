using System;
using System.Collections.Generic;
using System.Text;
using JCE.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace JCE
{
    /// <summary>
    /// 系统扩展 - 基础设施扩展
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册JCE基础设施服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configs">依赖配置</param>
        /// <returns></returns>
        public static IServiceProvider AddJce(this IServiceCollection services, params IConfig[] configs)
        {
            return new DependencyConfiguration(services,configs).Config();
        }
    }
}

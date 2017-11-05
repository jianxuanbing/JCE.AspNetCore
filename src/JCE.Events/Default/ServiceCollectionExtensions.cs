using System;
using System.Collections.Generic;
using System.Text;
using JCE.Events.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace JCE.Events.Default
{
    /// <summary>
    /// 事件总线 扩展
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册事件总线服务
        /// </summary>
        /// <param name="services">服务集合</param>
        public static void AddEventBus(this IServiceCollection services)
        {
            services.AddSingleton<IEventHandlerFactory, EventHandlerFactory>();
            services.AddSingleton<IEventBus, JCE.Events.Default.EventBus>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore.CAP;
using JCE.Events.Default;
using JCE.Events.Handlers;
using JCE.Events.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace JCE.Events.Cap
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
        /// <param name="action">配置操作</param>
        public static void AddEventBus(this IServiceCollection services, Action<CapOptions> action)
        {
            services.AddCap(action);
            services.AddSingleton<IMessageEventBus, MessageEventBus>();
            services.AddSingleton<IEventHandlerFactory, EventHandlerFactory>();
            services.AddSingleton<IEventBus, EventBus>();
        }
    }
}

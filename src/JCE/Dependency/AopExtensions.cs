using System;
using System.Collections.Generic;
using System.Text;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using AspectCore.Extensions.AspectScope;
using AspectCore.Extensions.Autofac;
using Autofac;

namespace JCE.Dependency
{
    /// <summary>
    /// AspectCore扩展
    /// </summary>
    public static class AopExtensions
    {
        /// <summary>
        /// 启用Aop
        /// </summary>
        /// <param name="builder">容器生成器</param>
        public static void EnableAop(this ContainerBuilder builder)
        {
            builder.RegisterDynamicProxy(config => config.EnableParameterAspect());
            builder.EnableAspectScoped();
        }

        /// <summary>
        /// 启用Aop作用域
        /// </summary>
        /// <param name="builder">容器生成器</param>
        public static void EnableAspectScoped(this ContainerBuilder builder)
        {
            builder.AddSingleton<IAspectScheduler, ScopeAspectScheduler>();
            builder.AddSingleton<IAspectBuilderFactory, ScopeAspectBuilderFactory>();
            builder.AddScoped<IAspectContextFactory, ScopeAspectContextFactory>();
        }
    }
}

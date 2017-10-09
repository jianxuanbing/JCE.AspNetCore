using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using JCE.Helpers;
using JCE.Reflections;
using JCE.Utils.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace JCE.Dependency
{
    /// <summary>
    /// 依赖配置
    /// </summary>
    public class DependencyConfiguration
    {
        /// <summary>
        /// 服务集合
        /// </summary>
        private readonly IServiceCollection _services;

        /// <summary>
        /// 依赖配置
        /// </summary>
        private readonly IConfig[] _configs;

        /// <summary>
        /// 容器生成器
        /// </summary>
        private ContainerBuilder _builder;

        /// <summary>
        /// 类型查找器
        /// </summary>
        private ITypeFinder _finder;

        /// <summary>
        /// 程序集列表
        /// </summary>
        private List<Assembly> _assemblies;

        /// <summary>
        /// 初始化一个<see cref="DependencyConfiguration"/>类型的实例
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configs">依赖配置</param>
        public DependencyConfiguration(IServiceCollection services, IConfig[] configs)
        {
            _services = services;
            _configs = configs;
        }

        /// <summary>
        /// 配置依赖
        /// </summary>
        /// <returns></returns>
        public IServiceProvider Config()
        {
            return Ioc.DefaultContainer.Register(_services, RegistServices, _configs);
        }

        /// <summary>
        /// 注册服务集合
        /// </summary>
        /// <param name="builder">容器生成器</param>
        private void RegistServices(ContainerBuilder builder)
        {
            _builder = builder;
            _finder=new WebAppTypeFinder();
            _assemblies = _finder.GetAssemblies();
            RegistInfrastracture();

        }

        /// <summary>
        /// 注册基础设施
        /// </summary>
        private void RegistInfrastracture()
        {
            EnableAop();
            RegistFinder();

        }

        /// <summary>
        /// 启用Aop
        /// </summary>
        private void EnableAop()
        {
            
        }

        /// <summary>
        /// 注册类型查找器
        /// </summary>
        private void RegistFinder()
        {
            
        }

        /// <summary>
        /// 注册上下文
        /// </summary>
        private void RegistContext()
        {
            
        }

        /// <summary>
        /// 注册Http上下文访问器
        /// </summary>
        private void ReigstHttpContextAccessor()
        {
            Web.SetHttpContextAccessor(Ioc.Create<IHttpContextAccessor>());
        }

        /// <summary>
        /// 注册事件处理器
        /// </summary>
        private void RegistEventHandlers()
        {
            
        }

        /// <summary>
        /// 查找并注册依赖
        /// </summary>
        private void RegistDependency()
        {
            
        }

        /// <summary>
        /// 注册单例依赖
        /// </summary>
        private void RegistSingletonDependency()
        {
            
        }

        /// <summary>
        /// 注册作用域依赖
        /// </summary>
        private void RegistScopeDependency()
        {
            
        }

        /// <summary>
        /// 注册瞬态依赖
        /// </summary>
        private void RegistTransientDependency()
        {
            
        }

        /// <summary>
        /// 解析依赖注册器
        /// </summary>
        private void ResolveDependencyRegistrar()
        {
            
        }

        /// <summary>
        /// 获取类型集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        private Type[] GetTypes<T>()
        {
            return _finder.Find<T>(_assemblies).ToArray();
        }

        /// <summary>
        /// 获取类型集合
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        private Type[] GetTypes(Type type)
        {
            return _finder.Find(type, _assemblies).ToArray();
        }
    }
}

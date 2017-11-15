using System;
using System.Collections.Generic;
using System.Text;
using JCE.Datas.EntityFramework.Configs;
using JCE.Datas.EntityFramework.Core;
using JCE.Datas.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JCE.Datas.EntityFramework
{
    /// <summary>
    /// 数据服务 扩展
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置操作</param>
        /// <returns></returns>
        public static IServiceCollection AddUnitOfWork<TService, TImplementation>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> configAction) 
            where TService : class, IUnitOfWork
            where TImplementation : UnitOfWorkBase, TService
        {
            services.AddDbContext<TImplementation>(configAction);
            services.AddScoped<TService, TImplementation>();
            return services;
        }        
    }
}

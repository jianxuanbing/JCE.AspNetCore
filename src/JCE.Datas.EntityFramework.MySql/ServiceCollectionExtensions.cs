using System;
using System.Collections.Generic;
using System.Text;
using JCE.Datas.EntityFramework.Configs;
using JCE.Datas.EntityFramework.Core;
using JCE.Datas.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JCE.Datas.EntityFramework.MySql
{
    /// <summary>
    /// 数据服务 扩展
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册MySql工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="connection">连接字符串</param>
        /// <param name="level">EF日志级别</param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServerUnitOfWork<TService, TImplementation>(
            this IServiceCollection services,
            string connection, EfLogLevel level = EfLogLevel.Sql)
            where TService : class, IUnitOfWork
            where TImplementation : UnitOfWorkBase, TService
        {
            EfConfig.LogLevel = level;
            return services.AddUnitOfWork<TService, TImplementation>(builder =>
            {
                builder.UseMySql(connection);
            });
        }
    }
}

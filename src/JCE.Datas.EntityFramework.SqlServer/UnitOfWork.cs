using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Text;
using JCE.Datas.EntityFramework.Core;
using JCE.Datas.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace JCE.Datas.EntityFramework.SqlServer
{
    /// <summary>
    /// SqlServer 工作单元
    /// </summary>
    public abstract class UnitOfWork:UnitOfWorkBase
    {
        /// <summary>
        /// 初始化一个<see cref="UnitOfWork"/>类型的实例
        /// </summary>
        /// <param name="connection">连接字符串</param>
        /// <param name="manager">工作单元管理器</param>
        protected UnitOfWork(string connection, IUnitOfWorkManager manager = null) : base(
            new DbContextOptionsBuilder().UseSqlServer(connection).Options, manager)
        {            
        }

        /// <summary>
        /// 初始化一个<see cref="UnitOfWork"/>类型的实例
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="manager">工作单元管理器</param>
        protected UnitOfWork(DbConnection connection, IUnitOfWorkManager manager = null) : base(
            new DbContextOptionsBuilder().UseSqlServer(connection).Options, manager)
        {            
        }

        /// <summary>
        /// 初始化一个<see cref="UnitOfWork"/>类型的实例
        /// </summary>
        /// <param name="options">配置</param>
        /// <param name="manager">工作单元管理器</param>
        protected UnitOfWork(DbContextOptions options, IUnitOfWorkManager manager) : base(options, manager)
        {
        }

        /// <summary>
        /// 获取映射类型列表
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <returns></returns>
        protected override IEnumerable<JCE.Datas.EntityFramework.Core.IMap> GetMapTypes(Assembly assembly)
        {
            return JCE.Utils.Helpers.Reflection.GetTypesByInterface<IMap>(assembly);
        }
    }
}

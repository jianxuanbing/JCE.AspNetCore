using System;
using System.Collections.Generic;
using System.Text;
using JCE.Datas.EntityFramework.Core;
using JCE.Domains.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JCE.Datas.EntityFramework.SqlServer
{
    /// <summary>
    /// 聚合根映射配置
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class AggregateRootMap<TEntity>:MapBase<TEntity>,IMap where TEntity:class,IVersion
    {
        /// <summary>
        /// 映射乐观离线锁
        /// </summary>
        /// <param name="builder">实体类型生成器</param>
        protected override void MapVersion(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(t => t.Version).IsRowVersion();
        }
    }
}

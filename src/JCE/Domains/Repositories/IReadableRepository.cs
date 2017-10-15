using System;
using System.Collections.Generic;
using System.Text;
using JCE.Domains.Entities;

namespace JCE.Domains.Repositories
{
    /// <summary>
    /// 仓储 - 可读
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public interface IReadableRepository<TEntity,in TKey> where TEntity:class,IAggregateRoot
    {
    }
}

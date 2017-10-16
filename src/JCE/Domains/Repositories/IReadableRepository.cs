using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
        /// <summary>
        /// 获取未跟踪的实体集
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> FindAsNoTracking();

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Find();

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="criteria">查询条件对象</param>
        /// <returns></returns>
        IQueryable<TEntity> Find(ICriteria<TEntity> criteria);

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns></returns>
        TEntity Find(object id);

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(object id);

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        /// <returns></returns>
        List<TEntity> FindByIds(params TKey[] ids);

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        /// <returns></returns>
        List<TEntity> FindByIds(IEnumerable<TKey> ids);

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">逗号分隔的Id列表，范例："1,2"</param>
        /// <returns></returns>
        List<TEntity> FindByIds(string ids);

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        /// <returns></returns>
        List<TEntity> FindByIdsAsync(params TKey[] ids);

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">实体标识集合</param>
        /// <returns></returns>
        List<TEntity> FindByIdsAsync(IEnumerable<TKey> ids);

        /// <summary>
        /// 查找实体集合
        /// </summary>
        /// <param name="ids">逗号分隔的Id列表，范例："1,2"</param>
        /// <returns></returns>
        List<TEntity> FindByIdsAsync(string ids);
    }
}

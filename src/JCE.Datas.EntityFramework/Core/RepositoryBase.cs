using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using JCE.Domains.Entities;
using JCE.Domains.Repositories;

namespace JCE.Datas.EntityFramework.Core
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract class RepositoryBase<TEntity,TKey>:IRepository<TEntity,TKey> where TEntity:class,IAggregateRoot<TEntity,TKey>
    {
        
        public IQueryable<TEntity> FindAsNoTracking()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Find()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Find(ICriteria<TEntity> criteria)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity Find(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(object id)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindByIds(params TKey[] ids)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindByIds(IEnumerable<TKey> ids)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindByIds(string ids)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindByIdsAsync(params TKey[] ids)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindByIdsAsync(IEnumerable<TKey> ids)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindByIdsAsync(string ids)
        {
            throw new NotImplementedException();
        }

        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Exists(params TKey[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(params TKey[] ids)
        {
            throw new NotImplementedException();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> Query(IQueryBase<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryAsync(IQueryBase<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> QueryAsNoTracking(IQueryBase<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryAsNoTrackingAsync(IQueryBase<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public PagerList<TEntity> PagerQuery(IQueryBase<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public Task<PagerList<TEntity>> PagerQueryAsync(IQueryBase<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public PagerList<TEntity> PagerQueryAsNoTracking(IQueryBase<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public Task<PagerList<TEntity>> PagerQueryAsNoTrackingAsync(IQueryBase<TEntity> query)
        {
            throw new NotImplementedException();
        }

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(IEnumerable<TKey> ids)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IEnumerable<TKey> ids)
        {
            throw new NotImplementedException();
        }

        public void Remove(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}

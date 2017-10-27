using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JCE.Applications.Dtos;
using JCE.Contexts;
using JCE.Datas.Queries;
using JCE.Domains.Entities;
using JCE.Domains.Repositories;
using JCE.Logs;
using JCE.Utils.AutoMapper;

namespace JCE.Applications
{
    /// <summary>
    /// 查询服务
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TQueryParameter">查询参数类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract class QueryServiceBase<TEntity,TDto,TQueryParameter,TKey>:IQueryService<TDto,TQueryParameter>
        where TEntity:class,IAggregateRoot<TEntity,TKey> 
        where TDto : IDto, new()
        where TQueryParameter:IQueryParameter
    {
        /// <summary>
        /// 仓储
        /// </summary>
        private readonly IRepository<TEntity, TKey> _repository;

        /// <summary>
        /// 日志组件
        /// </summary>
        public ILog Log { get; set; }

        /// <summary>
        /// 用户上下文
        /// </summary>
        public IUserContext UserContext { get; set; }

        /// <summary>
        /// 初始化一个<see cref="QueryServiceBase{TEntity,TDto,TQueryParameter,TKey}"/>类型的实例
        /// </summary>
        /// <param name="repository">仓储</param>
        protected QueryServiceBase(IRepository<TEntity, TKey> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Log = JCE.Logs.Log.Null;
            UserContext = JCE.Contexts.UserContext.Null;
        }

        /// <summary>
        /// 转换为数据传输对象
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        protected virtual TDto ToDto(TEntity entity)
        {
            return entity.MapTo<TDto>();
        }

        public List<TDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<TDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public TDto GetById(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TDto> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public List<TDto> GetByIds(string ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<TDto>> GetByIdsAsync(string ids)
        {
            throw new NotImplementedException();
        }

        public List<TDto> Query(TQueryParameter parameter)
        {
            throw new NotImplementedException();
        }

        public Task<List<TDto>> QueryAsync(TQueryParameter parameter)
        {
            throw new NotImplementedException();
        }

        public PagerList<TDto> PagerQuery(TQueryParameter parameter)
        {
            throw new NotImplementedException();
        }

        public Task<PagerList<TDto>> PagerQueryAsync(TQueryParameter parameter)
        {
            throw new NotImplementedException();
        }
    }
}

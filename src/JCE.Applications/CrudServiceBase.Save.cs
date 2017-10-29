using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Applications
{
    /// <summary>
    /// 增删改查服务 - Save
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDto">数据传输对象类型</typeparam>
    /// <typeparam name="TRequest">请求参数类型</typeparam>
    /// <typeparam name="TQueryParameter">查询参数类型</typeparam>
    /// <typeparam name="TKey">实体标识类型</typeparam>
    public abstract partial class CrudServiceBase<TEntity,TDto,TRequest, TQueryParameter, TKey>
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="request">请求参数</param>
        public void Save(TRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            SaveBefore(request);
            var entity = ToEntity(request);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (IsNew(request, entity))
            {
                Add(entity);
            }
            else
            {
                Update(entity);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        public async Task SaveAsync(TRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            SaveBefore(request);
            var entity = ToEntity(request);
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            if (IsNew(request, entity))
            {
                await AddAsync(entity);
            }
            else
            {
                await UpdateAsync(entity);
            }
        }

        /// <summary>
        /// 提交后操作 - 该方法由工作单元拦截器调用
        /// </summary>
        public void CommitAfter()
        {
            SaveAfter();
        }

        /// <summary>
        /// 保存前操作
        /// </summary>
        /// <param name="request">请求参数</param>
        protected virtual void SaveBefore(TRequest request)
        {
        }

        /// <summary>
        /// 是否新增
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        protected virtual bool IsNew(TRequest request, TEntity entity)
        {
            return string.IsNullOrWhiteSpace(request.Id) || entity.Id.Equals(default(TKey));
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        protected void Add(TEntity entity)
        {
            AddBefore(entity);
            entity.Init();
            _repository.Add(entity);
            AddAfter(entity);
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        protected async Task AddAsync(TEntity entity)
        {
            AddBefore(entity);
            entity.Init();
            await _repository.AddAsync(entity);
            AddAfter(entity);
        }

        /// <summary>
        /// 添加前操作
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual void AddBefore(TEntity entity)
        {            
        }

        /// <summary>
        /// 添加后操作
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual void AddAfter(TEntity entity)
        {
            AddLog(entity);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        protected void Update(TEntity entity)
        {
            UpdateBefore(entity);
            _repository.Update(entity);
            UpdateAfter(entity);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        protected async Task UpdateAsync(TEntity entity)
        {
            UpdateBefore(entity);
            await _repository.UpdateAsync(entity);
            UpdateAfter(entity);
        }

        /// <summary>
        /// 修改前操作
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual void UpdateBefore(TEntity entity)
        {            
        }

        /// <summary>
        /// 修改后操作
        /// </summary>
        /// <param name="entity">实体</param>
        protected virtual void UpdateAfter(TEntity entity)
        {
            AddLog(entity);
        }

        /// <summary>
        /// 保存后操作
        /// </summary>
        protected virtual void SaveAfter()
        {
            WriteLog($"保存{EntityDescription}成功");
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCE.Datas.UnitOfWorks
{
    /// <summary>
    /// 工作单元管理器
    /// </summary>
    public class UnitOfWorkManager:IUnitOfWorkManager
    {
        /// <summary>
        /// 工作单元集合
        /// </summary>
        private readonly List<IUnitOfWork> _unitOfWorks;

        /// <summary>
        /// 初始化一个<see cref="UnitOfWorkManager"/>类型的实例
        /// </summary>
        public UnitOfWorkManager()
        {
            _unitOfWorks=new List<IUnitOfWork>();
        }

        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        public Task CommitAsync()
        {
            throw new NotImplementedException();
        }

        public void Register(IUnitOfWork unitOfWork)
        {
            throw new NotImplementedException();
        }
    }
}

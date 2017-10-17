using JCE.Datas.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;
using JCE.Tests.XUnitHelpers;
using NSubstitute;
using Xunit;

namespace JCE.Tests.Datas.UnitOfWorks
{
    /// <summary>
    /// 工作单元管理器测试
    /// </summary>
    public class UnitOfWorkManagerTest
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 工作单元2
        /// </summary>
        private readonly IUnitOfWork _unitOfWork2;

        /// <summary>
        /// 工作单元管理器
        /// </summary>
        private readonly UnitOfWorkManager _manager;

        /// <summary>
        /// 测试初始化
        /// </summary>
        public UnitOfWorkManagerTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _unitOfWork2 = Substitute.For<IUnitOfWork>();
            _manager=new UnitOfWorkManager();
        }

        /// <summary>
        /// 注册一个空工作单元
        /// </summary>
        [Fact]
        public void TestRegister_Null()
        {
            AssertHelper.Throws<ArgumentNullException>(() => _manager.Register(null));
        }
    }
}

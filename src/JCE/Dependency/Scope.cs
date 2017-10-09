using Autofac;
using System;

namespace JCE.Dependency
{
    /// <summary>
    /// 作用域
    /// </summary>
    internal class Scope:IScope
    {
        /// <summary>
        /// autofac作用域
        /// </summary>
        private readonly ILifetimeScope _scope;

        /// <summary>
        /// 初始化一个<see cref="Scope"/>类型的实例
        /// </summary>
        /// <param name="scope">autofac作用域</param>
        public Scope(ILifetimeScope scope)
        {
            _scope = scope;
        }

        /// <summary>
        /// 释放对象
        /// </summary>
        public void Dispose()
        {
            _scope.Dispose();
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns></returns>
        public T Create<T>()
        {
            return _scope.Resolve<T>();
        }

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public object Create(Type type)
        {
            return _scope.Resolve(type);
        }
    }
}

using System;

namespace JCE.Dependency
{
    /// <summary>
    /// 作用域
    /// </summary>
    public interface IScope:IDisposable
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <returns></returns>
        T Create<T>();

        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        object Create(Type type);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using JCE.Dependency;
using JCE.Domains.Entities;

namespace JCE.Datas.Persistence
{
    /// <summary>
    /// 持久化对象
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IPersistentObject<out TKey>:IKey<TKey>,IScopeDependency,IVersion
    {
    }
}

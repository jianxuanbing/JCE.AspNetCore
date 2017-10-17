using System;
using System.Collections.Generic;
using System.Text;
using JCE.Datas.EntityFramework.Core;

namespace JCE.Datas.EntityFramework.SqlServer
{
    /// <summary>
    /// 实体映射配置
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class EntityMap<TEntity>:MapBase<TEntity>,IMap where TEntity:class
    {
    }
}

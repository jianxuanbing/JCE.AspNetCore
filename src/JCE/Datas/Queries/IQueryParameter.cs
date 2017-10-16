using System;
using System.Collections.Generic;
using System.Text;
using JCE.Domains.Repositories;

namespace JCE.Datas.Queries
{
    /// <summary>
    /// 查询参数
    /// </summary>
    public interface IQueryParameter:IPager
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        string Keyword { get; set; }
    }
}

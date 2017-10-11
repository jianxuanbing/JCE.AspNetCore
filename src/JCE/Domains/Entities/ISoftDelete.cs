using System;
using System.Collections.Generic;
using System.Text;

namespace JCE.Domains.Entities
{
    /// <summary>
    /// 逻辑删除
    /// </summary>
    public interface ISoftDelete
    {
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}

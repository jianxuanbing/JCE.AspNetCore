using System;
using System.Collections.Generic;
using System.Text;

namespace JCE.Utils.IdGenerators
{
    /// <summary>
    /// Id生成器
    /// </summary>
    public interface IGuidGenerator
    {
        /// <summary>
        /// 创建 Guid
        /// </summary>
        /// <returns></returns>
        Guid Create();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace JCE.Utils.Randoms
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public interface IRandomGenerator
    {
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        string Generate();
    }
}

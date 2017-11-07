using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Helpers;

namespace JCE.Utils.Randoms
{
    /// <summary>
    /// Guid随机数生成器，每次创建一个新的Guid字符串，去掉了Guid的分隔符
    /// </summary>
    public class GuidRandomGenerator:IRandomGenerator
    {
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        public string Generate()
        {
            return Id.Guid();
        }

        public static readonly IRandomGenerator Instance=new GuidRandomGenerator();
    }
}

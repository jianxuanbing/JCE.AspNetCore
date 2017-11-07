using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JCE.Utils.Randoms;

namespace JCE.Utils.IO.Paths
{
    /// <summary>
    /// 默认路径生成器
    /// </summary>
    public class DefaultPathGenerator:PathGeneratorBase
    {
        /// <summary>
        /// 基础路径
        /// </summary>
        private readonly string _basePath;

        /// <summary>
        /// 初始化路径生成器
        /// </summary>
        /// <param name="basePath">基础路径</param>
        /// <param name="randomGenerator">随机数生成器</param>
        public DefaultPathGenerator(string basePath,IRandomGenerator randomGenerator=null) : base(randomGenerator)
        {
            _basePath = basePath;
        }

        /// <summary>
        /// 创建完整路径
        /// </summary>
        /// <param name="fileName">被处理过的安全有效的文件名</param>
        /// <returns></returns>
        protected override string GeneratePath(string fileName)
        {
            return Path.Combine(_basePath, fileName);
        }
    }
}

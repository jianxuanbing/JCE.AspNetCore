using System;
using System.Collections.Generic;
using System.Text;

namespace JCE.Utils.IO.Paths
{
    /// <summary>
    /// 路径生成器
    /// </summary>
    public interface IPathGenerator
    {
        /// <summary>
        /// 添加路径参数
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        IPathGenerator Data(string key, string value);

        /// <summary>
        /// 生成完整路径
        /// </summary>
        /// <param name="fileName">文件名，必须包含扩展名，如果仅传入扩展名则生成随机文件名</param>
        /// <returns></returns>
        string Generate(string fileName);
    }
}

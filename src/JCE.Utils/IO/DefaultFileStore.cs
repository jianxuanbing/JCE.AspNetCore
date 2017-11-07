using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Extensions;
using JCE.Utils.IO.Paths;

namespace JCE.Utils.IO
{
    /// <summary>
    /// 本地文件存储服务
    /// </summary>
    public class DefaultFileStore:IFileStore
    {
        /// <summary>
        /// 路径生成器
        /// </summary>
        private readonly IPathGenerator _generator;

        /// <summary>
        /// 初始化一个<see cref="DefaultFileStore"/>类型的实例
        /// </summary>
        /// <param name="pathGenerator">路径生成器</param>
        public DefaultFileStore(IPathGenerator pathGenerator)
        {
            _generator = pathGenerator;
        }

        /// <summary>
        /// 保存文件，返回完整文件路径
        /// </summary>
        /// <param name="stream">文件流</param>
        /// <param name="fileName">文件名，包含扩展名</param>
        /// <returns></returns>
        public string Save(byte[] stream, string fileName)
        {
            var path = _generator.Generate(fileName);
            stream.ToFile(path);
            return path;
        }
    }
}

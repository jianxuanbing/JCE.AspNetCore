using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JCE.Utils.IO
{
    /// <summary>
    /// 文件操作辅助类
    /// </summary>
    public class FileUtil
    {
        #region CreateIfNotExists(创建文件，如果文件不存在)

        /// <summary>
        /// 创建文件，如果文件不存在
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static void CreateIfNotExists(string fileName)
        {
            if (File.Exists(fileName))
            {
                return;
            }
            File.Create(fileName);
        }

        #endregion
    }
}

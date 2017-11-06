using System;
using System.Collections.Generic;
using System.Text;

namespace JCE.Tools.QrCode
{
    /// <summary>
    /// 二维码服务 工厂
    /// </summary>
    public interface IQrCodeFactory
    {
        /// <summary>
        /// 创建二维码服务
        /// </summary>
        /// <param name="path">二维码文件存储目录</param>
        /// <returns></returns>
        IQrCodeService Create(string path);
    }
}

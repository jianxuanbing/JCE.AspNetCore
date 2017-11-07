using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.IO;
using JCE.Utils.IO.Paths;

namespace JCE.Tools.QrCode.QrCoder
{
    /// <summary>
    /// QRCoder 二维码服务工厂
    /// </summary>
    public class QrCodeFactory:IQrCodeFactory
    {
        /// <summary>
        /// 创建二维码服务
        /// </summary>
        /// <param name="path">二维码文件存储目录</param>
        /// <returns></returns>
        public IQrCodeService Create(string path)
        {
            return new QrCoderService(new DefaultFileStore(new DefaultPathGenerator(path)));
        }
    }
}

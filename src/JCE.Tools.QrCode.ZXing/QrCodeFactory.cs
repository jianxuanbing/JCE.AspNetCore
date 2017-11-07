using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.IO;
using JCE.Utils.IO.Paths;

namespace JCE.Tools.QrCode.ZXing
{
    /// <summary>
    /// ZXing.Net 二维码服务工厂
    /// </summary>
    public class QrCodeFactory:IQrCodeFactory
    {
        ///// <summary>
        ///// 文件存储服务
        ///// </summary>
        //private IFileStore _fileStore;

        ///// <summary>
        ///// 初始化一个
        ///// </summary>
        ///// <param name="fileStore"></param>
        //public QrCodeServiceFactory(IFileStore fileStore)
        //{
        //    _fileStore = fileStore;
        //}

        
        public IQrCodeService Create(string path)
        {
            return new ZXingQrCodeService(new DefaultFileStore(new DefaultPathGenerator(path)));
        }
    }
}

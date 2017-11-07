using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Extensions;
using JCE.Utils.IO;
using QRCoder;

namespace JCE.Tools.QrCode.QrCoder
{
    /// <summary>
    /// QRCoder 二维码服务
    /// </summary>
    public class QrCoderService:IQrCodeService
    {
        /// <summary>
        /// 文件存储服务
        /// </summary>
        private readonly IFileStore _fileStore;

        /// <summary>
        /// 二维码尺寸
        /// </summary>
        private int _size;

        /// <summary>
        /// 容错级别
        /// </summary>
        private QRCodeGenerator.ECCLevel _level;

        /// <summary>
        /// 初始化一个<see cref="QrCoderService"/>类型的实例
        /// </summary>
        /// <param name="fileStore">文件存储服务</param>
        public QrCoderService(IFileStore fileStore)
        {
            _fileStore = fileStore;
            _size = 10;
            _level = QRCodeGenerator.ECCLevel.L;
        }

        /// <summary>
        /// 设置二维码尺寸
        /// </summary>
        /// <param name="size">二维码尺寸</param>
        /// <returns></returns>
        public IQrCodeService Size(QrSize size)
        {
            return Size(size.Value());
        }

        /// <summary>
        /// 设置二维码尺寸
        /// </summary>
        /// <param name="size">二维码尺寸</param>
        /// <returns></returns>
        public IQrCodeService Size(int size)
        {
            _size = size;
            return this;
        }

        /// <summary>
        /// 容错处理
        /// </summary>
        /// <param name="level">容错级别</param>
        /// <returns></returns>
        public IQrCodeService Correction(ErrorCorrectionLevel level)
        {
            switch (level)
            {
                case ErrorCorrectionLevel.L:
                    _level = QRCodeGenerator.ECCLevel.L;
                    break;
                case ErrorCorrectionLevel.M:
                    _level = QRCodeGenerator.ECCLevel.M;
                    break;
                case ErrorCorrectionLevel.Q:
                    _level = QRCodeGenerator.ECCLevel.Q;
                    break;
                case ErrorCorrectionLevel.H:
                    _level = QRCodeGenerator.ECCLevel.H;
                    break;
            }
            return this;
        }

        public IQrCodeService Logo(string filePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 生成二维码并保存到指定位置
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public string Save(string content)
        {
            var qrCode = CreateQrCode(content);
            return _fileStore.Save(qrCode, "jpg");
        }

        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        private byte[] CreateQrCode(string content)
        {
            QRCodeGenerator generator=new QRCodeGenerator();
            QRCodeData data = generator.CreateQrCode(content, _level);
            BitmapByteQRCode qrCode=new BitmapByteQRCode(data);
            return qrCode.GetGraphic(_size);
        }
    }
}

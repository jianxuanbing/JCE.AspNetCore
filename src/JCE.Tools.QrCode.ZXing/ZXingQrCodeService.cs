using System;
using System.Collections.Generic;
using System.Text;
using JCE.Utils.Extensions;
using System.IO;
using JCE.Utils.IO;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZEI= global::ZXing.QrCode.Internal;

namespace JCE.Tools.QrCode.ZXing
{
    /// <summary>
    /// ZXing.Net 二维码服务
    /// </summary>
    public class ZXingQrCodeService:IQrCodeService
    {
        /// <summary>
        /// 二维码尺寸
        /// </summary>
        private int _size;

        /// <summary>
        /// 容错级别
        /// </summary>
        private ZEI.ErrorCorrectionLevel _level;

        private IDictionary<EncodeHintType, object> _dic;

        public ZXingQrCodeService()
        {
            _size = 10;
            _level = ZEI.ErrorCorrectionLevel.L;
            _dic=new Dictionary<EncodeHintType, object>();
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
                    _level= ZEI.ErrorCorrectionLevel.L;
                    break;
                case ErrorCorrectionLevel.M:
                    _level = ZEI.ErrorCorrectionLevel.M;
                    break;
                case ErrorCorrectionLevel.Q:
                    _level = ZEI.ErrorCorrectionLevel.Q;
                    break;
                case ErrorCorrectionLevel.H:
                    _level = ZEI.ErrorCorrectionLevel.H;
                    break;
            }
            return this;
        }

        public IQrCodeService Logo(string filePath)
        {
            throw new NotImplementedException();
        }

        public string Save(string content)
        {
            var qrCode = CreateQrCode(content);
            return string.Empty;
        }

        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        private byte[] CreateQrCode(string content)
        {            
            var qrCodeWriter=new BarcodeWriterPixelData()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions()
                {
                    Height = 250,
                    Width = 250,
                    Margin = 0
                }
            };
            var pixelData = qrCodeWriter.Write(content);
            using (var bitmap=new System.Drawing.Bitmap(pixelData.Width,pixelData.Height,System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms=new MemoryStream())
                {
                    var bitmapData =
                        bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height),
                            System.Drawing.Imaging.ImageLockMode.WriteOnly,
                            System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0,
                            pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }

                    bitmap.Save(ms,System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }
}

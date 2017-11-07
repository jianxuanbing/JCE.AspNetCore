using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using JCE.Utils.Extensions;
using System.IO;
using JCE.Utils.IO;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZQI= global::ZXing.QrCode.Internal;

namespace JCE.Tools.QrCode.ZXing
{
    /// <summary>
    /// ZXing.Net 二维码服务
    /// </summary>
    public class ZXingQrCodeService:IQrCodeService
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
        private ZQI.ErrorCorrectionLevel _level;

        private IDictionary<EncodeHintType, object> _dic;

        /// <summary>
        /// Logo路径
        /// </summary>
        private string _logoPath;

        /// <summary>
        /// 初始化一个<see cref="ZXingQrCodeService"/>类型的实例
        /// </summary>
        /// <param name="fileStore">文件存储服务</param>
        public ZXingQrCodeService(IFileStore fileStore)
        {
            _fileStore = fileStore;
            _size = 10;
            _level = ZQI.ErrorCorrectionLevel.L;
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
                    _level= ZQI.ErrorCorrectionLevel.L;
                    break;
                case ErrorCorrectionLevel.M:
                    _level = ZQI.ErrorCorrectionLevel.M;
                    break;
                case ErrorCorrectionLevel.Q:
                    _level = ZQI.ErrorCorrectionLevel.Q;
                    break;
                case ErrorCorrectionLevel.H:
                    _level = ZQI.ErrorCorrectionLevel.H;
                    break;
            }
            return this;
        }

        public IQrCodeService Logo(string filePath)
        {
            _logoPath = filePath;
            return this;
        }

        public string Save(string content)
        {
            var qrCode = string.IsNullOrWhiteSpace(_logoPath) ? CreateQrCode(content) : CreateQrCodeLogo(content);
            return _fileStore.Save(qrCode, "jpg");
        }

        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns></returns>
        private byte[] CreateQrCode(string content)
        {
            var qrCodeWriter = new BarcodeWriterPixelData()
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
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
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

                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }

        private byte[] CreateQrCodeLogo(string content)
        {
            var qrCodeWriter = new BarcodeWriterPixelData()
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions()
                {
                    Height = 250,
                    Width = 250,
                    Margin = 0,
                    ErrorCorrection = _level,
                    CharacterSet = "UTF-8"
                }
            };
            var temp = qrCodeWriter.Encode(content);
            var pixelData = qrCodeWriter.Write(content);

            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
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
                    Bitmap logo = new Bitmap(_logoPath);
                    // 获取二维码实际尺寸（去掉二维码两边空白后的实际尺寸）
                    int[] rectangle = temp.getEnclosingRectangle();

                    // 计算插入图片的大小和位置
                    int middleW = Math.Min((int)(rectangle[2] / 3.5), logo.Width);
                    int middleH = Math.Min((int)(rectangle[3] / 3.5), logo.Height);
                    int middleL = (bitmap.Width - middleW) / 2;
                    int middleT = (bitmap.Height - middleH) / 2;

                    // 将img转换成bmp格式，否则后面无法创建Graphics对象
                    //Bitmap bmpImg = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppArgb);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.DrawImage(bitmap, 0, 0);
                    }
                    // 将二维码插入图片
                    Graphics mg = Graphics.FromImage(bitmap);
                    // 白底
                    mg.FillRectangle(Brushes.White, middleL, middleT, middleW, middleH);
                    mg.DrawImage(logo, middleL, middleT, middleW, middleH);

                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                }
            }                     
        }
    }
}

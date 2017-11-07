using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            var qrCode = string.IsNullOrWhiteSpace(_logoPath) ? CreateQrCode(content) : CreateQrCodeLogo2(content);
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

        private byte[] CreateQrCodeLogo2(string content)
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
            var pixelData = qrCodeWriter.Write(content);

            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
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

                return MergeQrImg(bitmap, logo);
            }
        }

        /// <summary>
        /// 合并二维码以及Logo
        /// </summary>
        /// <param name="qrImg"></param>
        /// <param name="logoImg"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static byte[] MergeQrImg(Bitmap qrImg, Bitmap logoImg, double n = 0.23)
        {
            int margin = 10;
            float dpix = qrImg.HorizontalResolution;
            float dpiy = qrImg.VerticalResolution;

            var newWidth = (10 * qrImg.Width - 46 * margin) * 1.0f / 46;
            var newLogoImg = ZoomPic(logoImg, newWidth / logoImg.Width);
            // 处理Logo
            int newImgWidth = newLogoImg.Width + margin;
            Bitmap logoBgImg=new Bitmap(newImgWidth,newImgWidth);
            logoBgImg.MakeTransparent();
            Graphics g=Graphics.FromImage(logoBgImg);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            Pen p=new Pen(new SolidBrush(Color.White));
            Rectangle rect=new Rectangle(0,0,newImgWidth-1,newImgWidth-1);
            using (GraphicsPath path=CreateRoundedRectanglePath(rect,7))
            {
                g.DrawPath(p,path);
                g.FillPath(new SolidBrush(Color.White), path);
            }
            // 画Logo
            Bitmap img1 = new Bitmap(newLogoImg.Width, newLogoImg.Height);
            Graphics g1=Graphics.FromImage(img1);
            g1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g1.Clear(Color.Transparent);
            Pen p1=new Pen(new SolidBrush(Color.Gray));
            Rectangle rect1 = new Rectangle(0, 0, newLogoImg.Width - 1, newLogoImg.Height - 1);
            using (GraphicsPath path1 = CreateRoundedRectanglePath(rect1, 7))
            {
                g1.DrawPath(p1,path1);
                TextureBrush brush=new TextureBrush(newLogoImg);
                g1.FillPath(brush,path1);
            }
            g1.Dispose();

            PointF center=new PointF((newImgWidth-newLogoImg.Width)/2,(newImgWidth-newLogoImg.Height)/2);
            g.DrawImage(img1,center.X,center.Y,newLogoImg.Width,newLogoImg.Height);
            g.Dispose();
            
            Bitmap backgroundImg=new Bitmap(qrImg.Width,qrImg.Height);
            backgroundImg.MakeTransparent();
            backgroundImg.SetResolution(dpix,dpiy);
            logoBgImg.SetResolution(dpix,dpiy);

            Graphics g2=Graphics.FromImage(backgroundImg);
            g2.Clear(Color.Transparent);
            g2.DrawImage(qrImg,0,0);
            PointF center2=new PointF((qrImg.Width-logoBgImg.Width)/2,(qrImg.Height-logoBgImg.Height)/2);
            g2.DrawImage(logoBgImg,center2);
            g2.Dispose();

            using (var ms = new MemoryStream())
            {
                backgroundImg.Save(ms,ImageFormat.Png);
                return ms.ToArray();
            }            
        }

        /// <summary>
        /// 创建圆角矩形
        /// </summary>
        /// <param name="rect">区域</param>
        /// <param name="cornerRadius">圆角角度</param>
        /// <returns></returns>
        private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect=new GraphicsPath();
            roundedRect.AddArc(rect.X,rect.Y,cornerRadius*2,cornerRadius*2,180,90);
            roundedRect.AddLine(rect.X+cornerRadius,rect.Y,rect.Right-cornerRadius*2,rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270,
                90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right,
                rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2,
                cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);

            roundedRect.CloseFigure();

            return roundedRect;
        }

        /// <summary>
        /// 图片按比例缩放
        /// </summary>
        /// <param name="initImage">需要缩放的图片</param>
        /// <param name="n">缩放比例</param>
        /// <returns></returns>
        private static Image ZoomPic(Image initImage, double n)
        {
            // 缩略图宽、高计算
            var newWidth = n * initImage.Width;
            var newHeight = n * initImage.Height;
            // 生成新图
            System.Drawing.Image newImage = new System.Drawing.Bitmap((int) newWidth, (int) newHeight);
            // 新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(newImage);
            // 设置质量
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            // 设置背景色
            g.Clear(Color.Transparent);
            // 画图
            g.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height),
                new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height),
                System.Drawing.GraphicsUnit.Pixel);
            g.Dispose();
            return newImage;
        }
    }
}

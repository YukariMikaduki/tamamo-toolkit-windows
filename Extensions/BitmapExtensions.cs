using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Application = System.Windows.Application;

namespace TamamoToolkit.Extensions
{
    /// <summary>
    /// <see cref="Bitmap"/> 扩展类
    /// </summary>
    public static class BitmapExtensions
    {
        extension(Bitmap src)
        {
            /// <summary>
            /// 将 <see cref="Bitmap"/> 转换为 <see cref="BitmapSource"/>
            /// </summary>
            /// <remarks>参见 <see href="http://www.codeproject.com/Articles/104929/Bitmap-to-BitmapSource"/></remarks>
            /// <returns></returns>
            public BitmapSource ToBitmapSource()
            {
                return src.ToBitmapSource(ImageFormat.Png);
            }

            /// <summary>
            /// 将 <see cref="Bitmap"/> 转换为 <see cref="BitmapSource"/>
            /// </summary>
            /// <param name="format">图片格式</param>
            /// <remarks>参见 <see href="http://www.codeproject.com/Articles/104929/Bitmap-to-BitmapSource"/></remarks>
            /// <returns></returns>
            public BitmapSource ToBitmapSource(ImageFormat format)
            {
                if (Application.Current?.Dispatcher is null)
                {
                    using var memoryStream = new MemoryStream();
                    src.Save(memoryStream, format);
                    return CreateBitmapSourceFromBitmap(memoryStream);
                }
                using (var memoryStream = new MemoryStream())
                {
                    // 需要手动指定Image格式
                    src.Save(memoryStream, format);
                    _ = memoryStream.Seek(0, SeekOrigin.Begin);

                    // 确保在UI线程创建Bitmap
                    return IsInvokeRequired()
                        ? (BitmapSource)Application.Current.Dispatcher.Invoke(CreateBitmapSourceFromBitmap, DispatcherPriority.Normal, memoryStream)
                        : CreateBitmapSourceFromBitmap(memoryStream);
                }
            }
        }

        private static WriteableBitmap CreateBitmapSourceFromBitmap(Stream stream)
        {
            var bitmapDecoder = BitmapDecoder.Create(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);

            // Image完成后释放资源
            var writable = new WriteableBitmap(bitmapDecoder.Frames.Single());
            writable.Freeze();

            return writable;
        }

        private static bool IsInvokeRequired()
        {
            return Dispatcher.CurrentDispatcher != Application.Current.Dispatcher;
        }
    }
}
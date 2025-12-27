using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TamamoToolkit.Extensions
{
    /// <summary>
    /// <see cref="WriteableBitmap"/> 扩展类
    /// </summary>
    public static class WriteableBitmapExtensions
    {
        extension(WriteableBitmap bitmap)
        {
            /// <summary>
            /// 清空位图中的像素
            /// </summary>
            public void Clear()
            {
                byte[] empty = new byte[bitmap.PixelWidth * bitmap.PixelHeight * bitmap.Format.BitsPerPixel / 8];
                bitmap.WritePixels(empty);
            }

            /// <summary>
            /// 使用像素数组更新位图中的像素
            /// </summary>
            /// <param name="source">用来更新位图的像素数组</param>
            /// <exception cref="NotImplementedException">像素数组为不支持的格式</exception>
            public void WritePixels(Array source)
            {
                try
                {
                    int width = bitmap.PixelWidth * (bitmap.Format.BitsPerPixel / 8);
                    //位图实际行宽（亦称跨距或步长） = 图像像素宽度 * 每像素字节数，并补齐为4的倍数
                    int readWidth = width % 4 == 0 ? width : width + 4 - (width % 4);
                    byte[] bytes = new byte[readWidth * bitmap.PixelHeight];
                    _ = Parallel.For(0, bitmap.PixelHeight, i => Buffer.BlockCopy(source, width * i, bytes, readWidth * i, width));
                    bitmap.Lock();
                    Marshal.Copy(bytes, 0, bitmap.BackBuffer, bytes.Length);
                    bitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
                    bitmap.Unlock();
                }
                catch (Exception ex)
                {
                    throw new NotImplementedException("像素数组为不支持的格式", ex);
                }
            }
        }
    }
}
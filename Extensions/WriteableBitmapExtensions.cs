using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Chaldea.Components.Extensions
{
    /// <summary>
    /// <see cref="WriteableBitmap"/> 扩展类
    /// </summary>
    public static class WriteableBitmapExtensions
    {
        /// <summary>
        /// 清空位图中的像素
        /// </summary>
        /// <param name="bitmap"></param>
        public static void Clear(this WriteableBitmap bitmap)
        {
            byte[] empty = new byte[bitmap.PixelWidth * bitmap.PixelHeight * bitmap.Format.BitsPerPixel / 8];
            bitmap.WritePixels(empty);
        }

        /// <summary>
        /// 使用像素数组更新位图中的像素
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="source">用来更新位图的像素数数组</param>
        /// <exception cref="NotImplementedException">像素数组为不支持的格式</exception>
        public static void WritePixels(this WriteableBitmap bitmap, Array source)
        {
            try
            {
                byte[] bytes = new byte[bitmap.PixelWidth * bitmap.PixelHeight * bitmap.Format.BitsPerPixel / 8];
                Buffer.BlockCopy(source, 0, bytes, 0, bytes.Length);
                bitmap.Lock();
                Marshal.Copy(bytes, 0, bitmap.BackBuffer, bytes.Length);
                bitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
                bitmap.Unlock();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("像素数据为不支持的格式", ex);
            }
        }
    }
}
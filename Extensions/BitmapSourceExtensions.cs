using System.IO;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;

namespace TamamoToolkit.Extensions
{
    /// <summary>
    /// <see cref="BitmapSource"/> 扩展类
    /// </summary>
    public static class BitmapSourceExtensions
    {
        /// <summary>
        /// 获取 <see cref="BitmapSource"/> 中的像素数组
        /// </summary>
        /// <param name="image">待获取的 <see cref="BitmapSource"/></param>
        /// <returns><see cref="BitmapSource"/> 中的像素数组</returns>
        public static byte[] GetPixelsBytes(this BitmapSource image)
        {
            int width = image.PixelWidth * (image.Format.BitsPerPixel / 8);
            //位图实际行宽（亦称跨距或步长） = 图像像素宽度 * 每像素字节数，并补齐为4的倍数
            int readWidth = width % 4 == 0 ? width : width + 4 - (width % 4);
            byte[] bytes = new byte[readWidth * image.PixelHeight];
            image.CopyPixels(bytes, readWidth, 0);
            return bytes;
        }

        /// <summary>
        /// <see cref="BitmapSource"/> 保存为png、jpg或bmp
        /// </summary>
        /// <param name="image">待保存的图像</param>
        /// <param name="filePath">文件完整路径</param>
        public static void SaveToFile(this BitmapSource image, string filePath)
        {
            string? dir = Path.GetDirectoryName(filePath);
            string extension = Path.GetExtension(filePath).ToLower();
            if (dir is string str && !Directory.Exists(str))
            {
                _ = Directory.CreateDirectory(str);
            }
            BitmapEncoder encoder = extension switch
            {
                ".png" => new PngBitmapEncoder(),
                ".jpg" or ".jpeg" => new JpegBitmapEncoder(),
                _ => new BmpBitmapEncoder()
            };
            encoder.Frames.Add(BitmapFrame.Create(image));
            using var stream = new FileStream(filePath, FileMode.Create);
            encoder.Save(stream);
        }
    }
}
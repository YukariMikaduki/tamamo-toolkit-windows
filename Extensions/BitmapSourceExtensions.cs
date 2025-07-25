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
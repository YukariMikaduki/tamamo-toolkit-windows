using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TamamoToolkit.Utils
{
    /// <summary>
    /// <see cref="WriteableBitmap"/> 帮助类
    /// </summary>
    public class WriteableBitmapHelper
    {
        /// <inheritdoc cref="WriteableBitmap(int, int, double, double, PixelFormat, BitmapPalette)"/>
        public static WriteableBitmap Create(int pixelWidth, int pixelHeight, PixelFormat pixelFormat, BitmapPalette? palette)
        {
            return new WriteableBitmap(pixelWidth, pixelHeight, 96d, 96d, pixelFormat, palette);
        }
    }
}
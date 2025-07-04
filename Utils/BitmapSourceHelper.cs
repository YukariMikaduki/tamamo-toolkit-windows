using Chaldea.Components.Extensions;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Chaldea.Components.Utils
{
    /// <summary>
    /// <see cref="BitmapSource"/> 帮助类
    /// </summary>
    public class BitmapSourceHelper
    {
        /// <inheritdoc cref="BitmapSource.Create(int, int, double, double, PixelFormat, BitmapPalette, Array, int)"/>
        public static BitmapSource Create(int pixelWidth, int pixelHeight, PixelFormat pixelFormat, BitmapPalette? palette, Array pixels)
        {
            var bitmap = WriteableBitmapHelper.Create(pixelWidth, pixelHeight, pixelFormat, palette);
            bitmap.WritePixels(pixels);
            return bitmap;
        }
    }
}
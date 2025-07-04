using System.Runtime.InteropServices;

namespace Chaldea.Components.Utils
{
    /// <summary>
    /// 文件名比较器，升序排列文件名字符串
    /// </summary>
    public class FileNameComparer : IComparer<string>
    {
        /// <summary>
        /// 获取 <see cref="FileNameComparer"/> 的实例
        /// </summary>
        public static FileNameComparer Instance { get; } = new FileNameComparer();

        private FileNameComparer()
        {
        }

        /// <inheritdoc/>
        public int Compare(string? x, string? y)
        {
            return string.IsNullOrEmpty(x) && string.IsNullOrEmpty(y)
                ? 0
                : string.IsNullOrEmpty(x) ? -1 : string.IsNullOrEmpty(y) ? 1 : StrCmpLogicalW(x, y);
        }

        [DllImport("Shlwapi.dll", CharSet = CharSet.Unicode)]
        private static extern int StrCmpLogicalW(string x, string y);
    }
}
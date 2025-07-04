using System.Runtime.InteropServices;

namespace Chaldea.Components.DllImport
{
    /// <summary>
    /// <see langword="User32.dll"/> 互操作类
    /// </summary>
    public static class User32
    {
        /// <summary>
        /// 获取当前处于前台的窗口句柄
        /// </summary>
        /// <returns>当前处于前台的窗口句柄</returns>
        public static nint GetForegroundWindow()
        {
            return Nested.GetForegroundWindow();
        }

        /// <summary>
        /// 将创建指定窗口句柄的线程设置到前台，并且激活该窗口
        /// <para>系统给创建前台窗口的线程分配的权限稍高于其他线程</para>
        /// </summary>
        /// <param name="hWnd">将要被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口被设置为前台，返回 <see langword="true"/>，否则返回 <see langword="false"/></returns>
        public static bool SetForegroundWindow(nint hWnd)
        {
            if (GetForegroundWindow() == hWnd)
            {
                return true;
            }
            _ = SetWindowPos(hWnd, WindowZIndex.TopMost, 0, 0, 0, 0, SetWindowPosFlags.NoSize | SetWindowPosFlags.NoMove);
            _ = SetWindowPos(hWnd, WindowZIndex.NoTopMost, 0, 0, 0, 0, SetWindowPosFlags.NoSize | SetWindowPosFlags.NoMove);
            return Nested.SetForegroundWindow(hWnd);
        }

        /// <summary>
        /// 改变窗口的位置与状态
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="windowZIndex">窗口的Z顺序</param>
        /// <param name="x">窗口左侧的新位置</param>
        /// <param name="y">窗口顶部的新位置</param>
        /// <param name="cx">窗口的新宽度（像素）</param>
        /// <param name="cy">窗口的新高度（像素）</param>
        /// <param name="uFlags">窗口大小调整和定位标志，可以是 <see cref="SetWindowPosFlags"/> 枚举的各项组合</param>
        /// <returns></returns>
        public static bool SetWindowPos(nint hwnd, WindowZIndex windowZIndex, int x, int y, int cx, int cy, SetWindowPosFlags uFlags)
        {
            nint hWndInsertAfter = (int)windowZIndex;
            return Nested.SetWindowPos(hwnd, hWndInsertAfter, x, y, cx, cy, (uint)uFlags);
        }

        /// <summary>
        /// 设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="windowState">要为窗口设置的显示状态</param>
        /// <returns>如果窗口原本可见，返回 <see langword="true"/>，否则返回 <see langword="false"/></returns>
        public static bool ShowWindowAsync(nint hWnd, WindowState windowState)
        {
            return Nested.ShowWindowAsync(hWnd, (int)windowState);
        }

        private static class Nested
        {
            [DllImport("User32.dll")]
            public static extern nint GetForegroundWindow();

            [DllImport("User32.dll")]
            public static extern bool SetForegroundWindow(nint hWnd);

            [DllImport("User32.dll")]
            public static extern bool SetWindowPos(nint hwnd, nint hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

            [DllImport("User32.dll")]
            public static extern bool ShowWindowAsync(nint hWnd, int cmdShow);
        }

        /// <summary>
        /// 窗口大小调整和定位标志
        /// </summary>
        [Flags]
        public enum SetWindowPosFlags : uint
        {
            /// <summary>
            /// 忽略 <see langword="cx"/> 和 <see langword="cy"/>，保持大小
            /// </summary>
            NoSize = 0x0001,

            /// <summary>
            /// 忽略 <see langword="x"/> 和 <see langword="y"/>，不改变位置
            /// </summary>
            NoMove = 0x0002,

            /// <summary>
            /// 忽略 <see langword="windowZIndex"/>，保持Z顺序
            /// </summary>
            NoZOrder = 0x0004,

            /// <summary>
            /// 不重绘
            /// </summary>
            NoRedraw = 0x0008,

            /// <summary>
            /// 不激活
            /// </summary>
            NoActivate = 0x0010,

            /// <summary>
            /// 强制发送 WM_NCCALCSIZE 消息，一般只是在改变大小时才发送此消息
            /// </summary>
            FrameChanged = 0x0020,

            /// <summary>
            /// 显示窗口
            /// </summary>
            ShowWindow = 0x0040,

            /// <summary>
            /// 隐藏窗口
            /// </summary>
            HideWindow = 0x0080,
        }

        /// <summary>
        /// 窗口显示状态
        /// </summary>
        public enum WindowState
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 1,

            /// <summary>
            /// 最小化
            /// </summary>
            Minimized = 2,

            /// <summary>
            /// 最大化
            /// </summary>
            Maximized = 3
        }

        /// <summary>
        /// 窗体的Z顺序
        /// </summary>
        public enum WindowZIndex
        {
            /// <summary>
            /// 将窗口放在所有“TopMost”类型窗口的后面、其它类型窗口的前面
            /// </summary>
            NoTopMost = -2,

            /// <summary>
            /// 使窗口成为“TopMost”类型的窗口，这种类型的窗口总是在其它窗口的前面，直到它被关闭
            /// </summary>
            TopMost = -1,

            /// <summary>
            /// 将窗口放在Z轴的前面，即所有窗口的前面
            /// </summary>
            Top = 0,

            /// <summary>
            /// 把窗口放在Z轴的最后，即所有窗口的后面
            /// </summary>
            Bottom = 1,
        }
    }
}
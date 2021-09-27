using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace VitNX.Win32
{
    internal sealed class NativeFunctions
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        internal extern static IntPtr SetFocus(IntPtr hWnd);
        public static void RemoveFocus() { SetFocus(IntPtr.Zero); }

        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateRoundRectRgn(int int_0, int int_1, int int_2, int int_3, int int_4, int int_5);

        [DllImport("gdi32.dll")]
        internal static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("shell32.dll")]
        internal static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        [DllImport("dwmApi")]
        internal static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

    }
}
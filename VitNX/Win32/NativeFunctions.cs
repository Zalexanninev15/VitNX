using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace VitNX.Win32
{
    public  class NativeFunctions
    {
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point point);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, int uFlags);

        [DllImport("user32.dll")]
        public extern static IntPtr SetFocus(IntPtr hWnd);
        public static void RemoveFocus() { SetFocus(IntPtr.Zero); }

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRoundRectRgn(int int_0, int int_1, int int_2, int int_3, int int_4, int int_5);

        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("shell32.dll")]
        public static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        [DllImport("dwmApi")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
    }
}
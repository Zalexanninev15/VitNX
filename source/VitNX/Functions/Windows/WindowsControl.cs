using System;

using VitNX.Functions.Windows.Win32;

namespace VitNX.Functions.Windows
{
    public class WindowsControl
    {
        public static void MinimizeAllWindows()
        {
            IntPtr lHwnd = Import.FindWindow("Shell_TrayWnd", null);
            Import.SendMessage(lHwnd, 0x111, (IntPtr)419, IntPtr.Zero);
        }

        public static void MaximizeAllWindows()
        {
            IntPtr lHwnd = Import.FindWindow("Shell_TrayWnd", null);
            Import.SendMessage(lHwnd, 0x111, (IntPtr)416, IntPtr.Zero);
        }

        public static void HideConsoleWindow()
        {
            IntPtr handle = Import.GetConsoleWindow();
            Import.ShowWindow(handle, 0);
        }
    }
}
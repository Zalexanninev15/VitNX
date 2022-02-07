using System;

using VitNX.Functions.Windows.Win32;

namespace VitNX.Functions.Windows
{
    public class WindowsControl
    {
        /// <summary>
        /// Minimizes the all windows.
        /// </summary>
        public static void MinimizeAllWindows()
        {
            IntPtr lHwnd = Import.FindWindow("Shell_TrayWnd", null);
            Import.SendMessage(lHwnd, 0x111, (IntPtr)419, IntPtr.Zero);
        }

        /// <summary>
        /// Maximizes the all windows.
        /// </summary>
        public static void MaximizeAllWindows()
        {
            IntPtr lHwnd = Import.FindWindow("Shell_TrayWnd", null);
            Import.SendMessage(lHwnd, 0x111, (IntPtr)416, IntPtr.Zero);
        }

        /// <summary>
        /// Hides the console window.
        /// </summary>
        public static void HideConsoleWindow()
        {
            IntPtr handle = Import.GetConsoleWindow();
            Import.ShowWindow(handle, 0);
        }

        //public static async Task WindowNormalStartAnimationAsync(double Opacity)
        //{
        //    for (Opacity = 0; Opacity < 1; Opacity += 0.05)
        //        await Task.Delay(10);
        //}

        //public static void WindowNormalExitAnimation(double Opacity)
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Thread.Sleep(20);
        //        Opacity = Opacity - 0.05;
        //    }
        //}
    }
}
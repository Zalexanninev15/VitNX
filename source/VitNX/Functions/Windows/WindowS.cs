using System;
using System.Drawing;
using System.Windows.Forms;

using VitNX.Functions.Windows.Win32;

using static VitNX.Functions.Windows.Win32.Enums;

namespace VitNX.Functions.Windows
{
    public class WindowS
    {
        /// <summary>
        /// Minimizes the all windows.
        /// </summary>
        public static void MinimizeAllWindows()
        {
            IntPtr lHwnd = Import.FindWindow("Shell_TrayWnd", null);
            Import.SendMessage(lHwnd, 0x111, 
                (IntPtr)419,
                IntPtr.Zero);
        }

        /// <summary>
        /// Maximizes the all windows.
        /// </summary>
        public static void MaximizeAllWindows()
        {
            IntPtr lHwnd = Import.FindWindow("Shell_TrayWnd", null);
            Import.SendMessage(lHwnd, 0x111,
                (IntPtr)416,
                IntPtr.Zero);
        }

        /// <summary>
        /// Hides the console window.
        /// </summary>
        public static void HideConsoleWindow()
        {
            IntPtr handle = Import.GetConsoleWindow();
            Import.ShowWindow(handle, 0);
        }

        /// <summary>
        /// Sets the window to the lower right corner.
        /// </summary>
        /// <param name="Handler">The handler.</param>
        public static void WindowToLowerRightCorner(IntPtr Handler)
        {
            Import.GetWindowRect(Handler, out RECT rct);
            Rectangle screen = Screen.FromHandle(Handler).Bounds;
            Point pt = new Point(screen.Left + screen.Width / 2 - (rct.Right - rct.Left) / 2,
                screen.Top + screen.Height / 2 - (rct.Bottom - rct.Top) / 2);
            Import.SetWindowPos(Handler, (IntPtr)SpecialWindowHandles.HWND_NOTOPMOST,
                pt.X, pt.Y, 0, 0,
                SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOSIZE |
                SetWindowPosFlags.SWP_SHOWWINDOW);
        }

        /// <summary>
        /// Applying a native dark window title for the application if it runs on Windows 10 or higher..
        /// </summary>
        /// <param name="Handler">The handler.</param>
        public static void SetWindowsTenAndHighStyleForWinFormTitleToDark(IntPtr Handler)
        {
            if (Import.DwmSetWindowAttribute(Handler, 19, new[] { 1 }, 4) != 0)
                Import.DwmSetWindowAttribute(Handler, 20,  new[] { 1 }, 4);
        }

        /// <summary>
        /// Applying Windows 11 roundings to a window(s).
        /// </summary>
        /// <param name="Handler">The handler.</param>
        public static void SetWindowsElevenStyleForWinForm(IntPtr Handler)
        {
            var attribute = Constants.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            Import.DwmSetWindowAttribute(Handler,
                attribute,
                new[] { Convert.ToInt32(preference) }, sizeof(uint));
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
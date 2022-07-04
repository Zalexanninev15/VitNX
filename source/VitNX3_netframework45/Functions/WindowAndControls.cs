using System;
using System.Drawing;
using System.Windows.Forms;

using VitNX3.Functions.Win32;

using static VitNX3.Functions.Win32.Enums;

namespace VitNX3.Functions.WindowAndControls
{
    public class Window
    {
        /// <summary>
        /// Minimizes the all windows.
        /// </summary>
        public static void MinimizeAll()
        {
            IntPtr lHwnd = Import.FindWindow("Shell_TrayWnd", null);
            Import.SendMessage(lHwnd,
                0x111,
                (IntPtr)419,
                IntPtr.Zero);
        }

        /// <summary>
        /// Maximizes the all windows.
        /// </summary>
        public static void MaximizeAll()
        {
            IntPtr lHwnd = Import.FindWindow("Shell_TrayWnd", null);
            Import.SendMessage(lHwnd,
                0x111,
                (IntPtr)416,
                IntPtr.Zero);
        }

        /// <summary>
        /// Hides the console window.
        /// </summary>
        public static void HideConsoleWindow()
        {
            IntPtr handle = Import.GetConsoleWindow();
            Import.ShowWindow(handle, SW_SH.SW_HIDE);
        }

        /// <summary>
        /// Shows the console window.
        /// </summary>
        public static void ShowConsoleWindow()
        {
            IntPtr handle = Import.GetConsoleWindow();
            Import.ShowWindow(handle, SW_SH.SW_SHOW);
        }

        /// <summary>
        /// Shows the window as TopMost.
        /// </summary>
        /// <param name="Handle">Handle.</param>
        public static void ShowAsTopMost(IntPtr Handle)
        {
            Import.SetWindowPos(Handle,
               new IntPtr((int)HWND.HWND_TOPMOST),
               0, 0, 0, 0,
               (int)SET_WINDOW_POS_FLAGS.SWP_IGNORE_MOVE |
               (int)SET_WINDOW_POS_FLAGS.SWP_IGNORE_RESIZE);
        }

        /// <summary>
        /// Sets the window to the lower right corner.
        /// </summary>
        /// <param name="Handle">Handle.</param>
        public static void WindowToLowerRightCorner(IntPtr Handle)
        {
            Import.GetWindowRect(Handle, out RECT rct);
            Rectangle screen = Screen.FromHandle(Handle).Bounds;
            Point pt = new Point(screen.Left + screen.Width / 2 - (rct.Right - rct.Left) / 2,
                screen.Top + screen.Height / 2 - (rct.Bottom - rct.Top) / 2);
            Import.SetWindowPos(Handle, (IntPtr)SpecialWindowHandles.HWND_NOTOPMOST,
                pt.X, pt.Y, 0, 0,
                SET_WINDOW_POS_FLAGS.SWP_NO_ZORDER |
                SET_WINDOW_POS_FLAGS.SWP_IGNORE_RESIZE |
                SET_WINDOW_POS_FLAGS.SWP_SHOW_WINDOW);
        }

        /// <summary>
        /// Applying a native dark window title for the application if it runs on Windows 10 or higher..
        /// </summary>
        /// <param name="Handle">Handle.</param>
        public static void SetWindowsTenAndHighStyleForWinFormTitleToDark(IntPtr Handle)
        {
            if (Import.DwmSetWindowAttribute(Handle,
                DWM_GET_WINDOW_ATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE,
                new[] { 1 }, 4) != 0)
                Import.DwmSetWindowAttribute(Handle,
                    DWM_GET_WINDOW_ATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE_NOT,
                    new[] { 1 }, 4);
        }

        /// <summary>
        /// Applying Windows 11 roundings to a window(s).
        /// </summary>
        /// <param name="Handle">Handle.</param>
        /// <param name="windowWidth">The width of window.</param>
        /// <param name="windowHeight">The height of window.</param>
        public static Region SetWindowsElevenStyleForWinForm(IntPtr Handle,
            int windowWidth,
            int windowHeight)
        {
            Import.DwmSetWindowAttribute(Handle,
                DWM_GET_WINDOW_ATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE,
                new[] { Convert.ToInt32(DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND) },
                sizeof(uint));
            return Region.FromHrgn(Import.CreateRoundRectRgn(0, 0,
                windowWidth,
                windowHeight,
                15, 15));
        }

        /// <summary>
        /// Adds the borders for window.
        /// </summary>
        /// <param name="hwnd">Handle</param>
        /// <param name="rc">RECT</param>
        /// <param name="dpi">DPI</param>
        public static void AddWindowBorders(IntPtr hwnd,
            ref RECT rc,
            int dpi)
        {
            uint windowStyle = (uint)Import.GetWindowLong(hwnd, -16);
            uint windowStyleEx = (uint)Import.GetWindowLong(hwnd, -20);
            if ((Convert.ToInt64(Information.Windows.GetWindowsCurrentBuildNumberFromRegistry()) >= 1607 ||
                Convert.ToInt64(Information.Windows.GetWindowsCurrentBuildNumberFromRegistry()) >= 16070)
                && hwnd != IntPtr.Zero)
                Import.AdjustWindowRectExForDpi(ref rc,
                    windowStyle,
                    false,
                    windowStyleEx,
                    (uint)dpi);
            else
                Import.AdjustWindowRect(ref rc,
                    windowStyle,
                    false);
        }

        /// <summary>
        /// Gets the DPI for window.
        /// </summary>
        /// <param name="hwnd">The hwnd.</param>
        /// <returns>An int.</returns>
        public static int GetDpiForWindow(IntPtr hwnd)
        {
            if ((Convert.ToInt64(Information.Windows.GetWindowsCurrentBuildNumberFromRegistry()) >= 1607 ||
                Convert.ToInt64(Information.Windows.GetWindowsCurrentBuildNumberFromRegistry()) >= 16070) &&
                hwnd != IntPtr.Zero)
                return GetDpiForWindow(hwnd);
            else
                using (Graphics gx = Graphics.FromHwnd(hwnd))
                    return Import.GetDeviceCaps(gx.GetHdc(), 88);
        }
    }

    public class Controls
    {
        private static uint SavedVolumeLevel;
        private static bool VolumeLevelSaved = false;

        /// <summary>
        /// Enable/disable sound (nasty) when focusing on an item/control..
        /// </summary>
        /// <param name="off">If true, off.</param>
        public static void PlayFocusSound(bool off = true)
        {
            if (off)
            {
                Import.WaveOutGetVolume(IntPtr.Zero,
                    out SavedVolumeLevel);
                VolumeLevelSaved = true;
                Import.WaveOutSetVolume(IntPtr.Zero, 0);
            }
            else
            {
                if (VolumeLevelSaved)
                {
                    Import.WaveOutSetVolume(IntPtr.Zero,
                        SavedVolumeLevel);
                    VolumeLevelSaved = true;
                }
            }
        }

        /// <summary>
        /// Sets the native Windows System theme for controls.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="themeMode">The theme mode.</param>
        public static void SetNativeThemeForControls(IntPtr handle,
            string themeMode = "DarkMode_Explorer")
        {
            Import.SetWindowTheme(handle, themeMode, null);
        }
    }
}
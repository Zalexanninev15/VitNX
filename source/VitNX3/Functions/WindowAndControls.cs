using System;
using System.Drawing;

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
            Import.ShowWindow(handle, 0);
        }

        /// <summary>
        /// Shows the window as TopMost.
        /// </summary>
        /// <param name="Handler">The handler.</param>
        public static void ShowAsTopMost(IntPtr Handler)
        {
            Import.SetWindowPos(Handler,
               new IntPtr((int)HWND.HWND_TOPMOST),
               0, 0, 0, 0,
               (int)SET_WINDOW_POS_FLAGS.SWP_IGNORE_MOVE |
               (int)SET_WINDOW_POS_FLAGS.SWP_IGNORE_RESIZE);
        }

        /// <summary>
        /// Applying a native dark window title for the application if it runs on Windows 10 or higher..
        /// </summary>
        /// <param name="Handler">The handler.</param>
        public static void SetWindowsTenAndHighStyleForWinFormTitleToDark(IntPtr Handler)
        {
            if (Import.DwmSetWindowAttribute(Handler,
                DWM_GET_WINDOW_ATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE,
                new[] { 1 }, 4) != 0)
                Import.DwmSetWindowAttribute(Handler,
                    DWM_GET_WINDOW_ATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE_NOT,
                    new[] { 1 }, 4);
        }

        /// <summary>
        /// Applying Windows 11 roundings to a window(s).
        /// </summary>
        /// <param name="Handler">The handler.</param>
        /// <param name="windowWidth">The width of window</param>
        /// <param name="windowHeight">The height of window</param>
        public static Region SetWindowsElevenStyleForWinForm(IntPtr Handler, int windowWidth, int windowHeight)
        {
            Import.DwmSetWindowAttribute(Handler,
                DWM_GET_WINDOW_ATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE,
                new[] { Convert.ToInt32(DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND) },
                sizeof(uint));
            return Region.FromHrgn(Import.CreateRoundRectRgn(0, 0,
                windowWidth,
                windowHeight,
                15, 15));
        }
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
    public static void VolumeOnFocus(bool off = true)
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
    public static void SetNativeThemeForControls(IntPtr handle, string themeMode = "DarkMode_Explorer")
    {
        Import.SetWindowTheme(handle, themeMode, null);
    }
}
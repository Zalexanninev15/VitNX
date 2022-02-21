using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

using VitNX.Functions.Windows.Controllers;

using static VitNX.Functions.Windows.Win32.Enums;

namespace VitNX.Functions.Windows.Win32
{
    /// <summary>
    /// Import the Windows System functions from native DLL.
    /// About functions: https://www.pinvoke.net
    /// </summary>
    public class Import
    {
        [DllImport("user32.dll")]
        public static extern bool AdjustWindowRectEx(ref RECT lpRect,
            int dwStyle,
            bool bMenu,
            int dwExStyle);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("msimg32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool AlphaBlend(IntPtr hdcDest,
            int nXOriginDest,
            int nYOriginDest,
            int nWidthDest,
            int nHeightDest,
            IntPtr hdcSrc,
            int nXOriginSrc,
            int nYOriginSrc,
            int nWidthSrc,
            int nHeightSrc,
            BLENDFUNCTION blendFunction);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern bool AnimateWindow(IntPtr hwnd,
            int dwTime,
            int dwFlags);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr BeginPaint(IntPtr hWnd,
            ref PAINTSTRUCT lpPaint);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("gdi32.dll",
            SetLastError = true)]
        public static extern IntPtr CancelDC(IntPtr hdc);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent,
            POINT Point);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd,
            ref POINT lpPoint);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("uxtheme.dll",
            CharSet = CharSet.Unicode)]
        public static extern int CloseThemeData(IntPtr hTheme);

        [return: MarshalAs(UnmanagedType.Interface)]
        [DllImport("ole32.dll",
            ExactSpelling = true,
            PreserveSig = false)]
        public static extern object CoCreateInstance([In] ref Guid clsid,
            [MarshalAs(UnmanagedType.Interface)] object punkOuter,
            int context,
            [In] ref Guid iid);

        [DllImport("gdi32.dll")]
        public static extern int CombineRgn(IntPtr hrgnDest,
            IntPtr hrgnSrc1,
            IntPtr hrgnSrc2,
            int fnCombineMode);

        [DllImport("gdi32.dll",
            CharSet = CharSet.Auto,
            ExactSpelling = true)]
        public static extern IntPtr CreateBitmap(int nWidth,
            int nHeight,
            int nPlanes,
            int nBitsPerPixel,
            [MarshalAs(UnmanagedType.LPArray)] short[] lpvBits);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateBrushIndirect(ref LOGBRUSH lplb);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC,
            int nWidth,
            int nHeight);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFontIndirect(ref LOGFONT lplf);

        [DllImport("gdi32",
            CharSet = CharSet.Auto)]
        public static extern IntPtr CreatePatternBrush(IntPtr hBitmap);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgn(int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgnIndirect(ref RECT lprc);

        [DllImport("gdi32",
            CharSet = CharSet.Auto)]
        public static extern IntPtr CreateSolidBrush(int crColor);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern int DeleteObject(IntPtr hObject);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool DispatchMessage(ref MSG msg);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool DrawEdge(IntPtr hdc,
            ref RECT qrc,
            int edge,
            int grfFlags);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool DrawFrameControl(IntPtr hdc,
            ref RECT lprc,
            int uType,
            int uState);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern int DrawText(IntPtr hdc,
            string lpString,
            int cbString,
            ref RECT lpRect,
            int uFormat);

        [DllImport("uxtheme.dll")]
        public static extern int DrawThemeBackground(IntPtr hTheme,
            IntPtr hdc,
            int iPartId,
            int iStateId,
            ref RECT pRect,
            ref RECT pClipRect);

        [DllImport("uxtheme.dll")]
        public static extern int DrawThemeBackgroundEx(IntPtr hTheme,
            IntPtr hdc,
            int iPartId,
            int iStateId,
            ref RECT pRect,
            ref DTBGOPTS pOptions);

        [DllImport("uxtheme.dll")]
        public static extern int DrawThemeEdge(IntPtr hTheme,
            IntPtr hdc,
            int iPartId,
            int iStateId,
            ref RECT
            pDestRect,
            int uEdge,
            int uFlags,
            ref RECT pContentRect);

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd,
            bool bEnable);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool EndPaint(IntPtr hWnd,
            ref PAINTSTRUCT lpPaint);

        [DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(uint dwThreadId,
            EnumThreadWindowsCallBack lpfn,
            IntPtr lParam);

        [DllImport("gdi32.dll",
            CharSet = CharSet.Auto)]
        public static extern int ExcludeClipRect(IntPtr hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(StringBuilder lpszClass,
            StringBuilder lpszWindow);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent,
            IntPtr hwndChildAfter,
            StringBuilder lpszClass,
            StringBuilder lpszWindow);

        [DllImport("gdiplus.dll",
            CharSet = CharSet.Unicode,
            ExactSpelling = true)]
        internal static extern int GdipCreateBitmapFromScan0(int width,
            int height,
            int stride,
            int format,
            HandleRef scan0,
            out IntPtr bitmap);

        [DllImport("gdiplus.dll",
            CharSet = CharSet.Unicode,
            ExactSpelling = true)]
        internal static extern int GdipCreateHBITMAPFromBitmap(HandleRef nativeBitmap,
            out IntPtr hbitmap,
            int argbBackground);

        [DllImport("gdiplus.dll",
            CharSet = CharSet.Unicode,
            ExactSpelling = true)]
        internal static extern int GdipGetDC(HandleRef graphics,
            out IntPtr hdc);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr GetAncestor(IntPtr hWnd,
            uint gaFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetCapture();

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd,
            ref RECT lpRect);

        [DllImport("gdi32.dll")]
        public static extern int GetClipBox(IntPtr hdc,
            ref RECT lprc);

        [DllImport("uxtheme.dll",
            CharSet = CharSet.Unicode)]
        public static extern int GetCurrentThemeName(StringBuilder pszThemeFileName,
            int dwMaxNameChars,
            StringBuilder pszColorBuff,
            int cchMaxColorChars,
            StringBuilder pszSizeBuff,
            int cchMaxSizeChars);

        [DllImport("kernel32.dll",
            CharSet = CharSet.Auto)]
        public static extern int GetCurrentThreadId();

        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll",
            SetLastError = true)]
        public static extern int GetDeviceCaps(IntPtr hdc,
            int nIndex);

        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("gdi32.dll")]
        public static extern int GetGraphicsMode(IntPtr hdc);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool GetIconInfo(IntPtr hIcon,
            ref ICONINFO iconInfo);

        [DllImport("user32.dll")]
        private static extern long GetKeyboardLayoutName(StringBuilder pwszKLID);

        [DllImport("kernel32.dll")]
        public static extern int GetLastError();

        [DllImport("gdi32.dll")]
        public static extern uint GetLayout(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern int GetMapMode(IntPtr hdc);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool GetMessage(ref MSG msg,
            int hWnd,
            uint wFilterMin,
            uint wFilterMax);

        [DllImport("user32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetScrollBarInfo(IntPtr hWnd,
            uint idObject,
            ref SCROLLBARINFO psbi);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetScrollInfo(IntPtr hWnd,
            int fnBar,
            [MarshalAs(UnmanagedType.Struct)] ref SCROLLINFO lpsi);

        [DllImport("user32.dll")]
        public static extern int GetSysColor(int nIndex);

        [DllImport("gdi32.dll")]
        public static extern uint GetTextAlign(IntPtr hdc);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool GetTextExtentExPoint(IntPtr hdc,
            string lpszStr,
            int cchString,
            int nMaxExtent,
            ref short lpnFit,
            IntPtr alpDx,
            ref SIZE lpSize);
        [return: MarshalAs(UnmanagedType.Bool)]

        [DllImport("gdi32.dll")]
        public static extern bool GetTextExtentPoint32(IntPtr hdc,
            string lpString,
            int cbString,
            ref SIZE lpSize);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool GetTextMetrics(IntPtr hdc, IntPtr lptm);

        [DllImport("uxtheme.dll",
            CharSet = CharSet.Unicode,
            SetLastError = true)]
        public static extern int GetThemeMargins(IntPtr hTheme,
            IntPtr hdc,
            int iPartId, int iStateId,
            int iPropId,
            ref RECT prc,
            ref MARGINS pMargins);

        [DllImport("uxtheme.dll",
            CharSet = CharSet.Unicode)]
        public static extern int GetThemeMetric(IntPtr hTheme,
            IntPtr hdc,
            int iPartId,
            int iStateId,
            int iPropId,
            ref int piVal);

        [DllImport("uxtheme.dll",
            CharSet = CharSet.Unicode,
            SetLastError = true)]
        public static extern int GetThemePartSize(IntPtr hTheme,
            IntPtr hdc,
            int iPartId,
            int iStateId,
            ref RECT prc,
            THEMESIZE eSize,
            ref SIZE psz);

        [DllImport("uxtheme.dll",
            CharSet = CharSet.Unicode)]
        public static extern int GetThemeSysFont(IntPtr hTheme,
            int iIntID,
            out LOGFONT pFont);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool GetUpdateRect(IntPtr hWnd,
            ref RECT lpRect,
            bool erase);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hWnd,
            uint uCmd);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(IntPtr hWnd,
            int nIndex);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd,
            ref WINDOWPLACEMENT lpwndpl);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd,
            ref int lpdwProcessId);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool HideCaret(IntPtr hWnd);

        [DllImport("gdi32.dll",
            CharSet = CharSet.Auto)]
        public static extern int IntersectClipRect(IntPtr hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool InvalidateRect(IntPtr hWnd,
            ref RECT lpRect,
            bool bErase);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool InvalidateRect(IntPtr hWnd,
            IntPtr rectangle,
            bool bErase);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr KillTimer(IntPtr hWnd,
            IntPtr nIDEvent);

        [DllImport("user32.dll")]
        private static extern long LoadKeyboardLayout(string pwszKLID,
            uint Flags);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern int MapVirtualKey(int uCode,
            int uMapType);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool MaskBlt(IntPtr hdcDest,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc,
            int nXSrc,
            int nYSrc,
            IntPtr hbmMask,
            int xMask,
            int yMask,
            uint dwRop);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            ExactSpelling = true)]
        public static extern bool MessageBeep(int type);

        [DllImport("gdi32.dll")]
        public static extern int ModifyWorldTransform(IntPtr tmp_hDC,
            ref XFORM lpXform,
            uint iMode);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            ExactSpelling = true)]
        public static extern int MsgWaitForMultipleObjects(int nCount,
            int pHandles,
            bool fWaitAll,
            int dwMilliseconds,
            int dwWakeMask);

        [DllImport("gdi32.dll")]
        public static extern uint OffsetViewportOrgEx(IntPtr hdc,
            int nXOffset,
            int nYOffset,
            ref POINT lpPoint);

        [DllImport("gdi32.dll")]
        public static extern int OffsetWindowOrgEx(IntPtr hdc,
            int nXOffset,
            int nYOffset,
            ref POINT lpPoint);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess,
            bool bInheritHandle,
            int dwProcessId);

        [DllImport("uxtheme.dll",
            CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenThemeData(IntPtr hWnd,
            string ClassList);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool PatBlt(IntPtr hdc,
            int nXLeft,
            int nYLeft,
            int nWidth,
            int nHeight,
            int dwRop);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool PeekMessage(ref MSG msg,
            int hWnd,
            uint wFilterMin,
            uint wFilterMax,
            uint wFlag);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool PostMessage(IntPtr hWnd,
            int Msg,
            uint wParam,
            uint lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool PrintWindow(IntPtr hwnd,
            IntPtr hdc,
            int nFlags);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess,
            IntPtr lpBaseAddress,
            IntPtr lpBuffer,
            int nSize,
            ref int lpNumberOfBytesWritten);

        [DllImport("gdi32",
            CharSet = CharSet.Auto)]
        public static extern bool Rectangle(IntPtr hdc,
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool RedrawWindow(IntPtr hWnd,
            IntPtr lprcUpdate,
            IntPtr hrgnUpdate,
            int flags);

        [DllImport("gdi32.dll")]
        public static extern int SelectClipRgn(IntPtr hdc,
            IntPtr hrgn);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC,
            IntPtr hObject);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr SendNotifyMessage(IntPtr hWnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern int SetBkColor(IntPtr hdc,
            int crColor);

        [DllImport("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hdc,
            int iBkMode);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool SetCaretPos(int X,
            int Y);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern int SetGraphicsMode(IntPtr hdc,
            int iMode);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd,
            int crKey,
            byte bAlpha,
            int dwFlags);

        [DllImport("gdi32.dll")]
        public static extern uint SetLayout(IntPtr hdc,
            uint dwLayout);

        [DllImport("gdi32.dll")]
        public static extern int SetMapMode(IntPtr hdc,
            int fnMapMode);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr SetParent(IntPtr hWndChild,
            IntPtr hWndNewParent);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool SetScrollInfo(IntPtr hWnd,
            int fnBar,
            [MarshalAs(UnmanagedType.Struct)] ref SCROLLINFO lpsi,
            [MarshalAs(UnmanagedType.Bool)] bool fRedraw);

        [DllImport("gdi32.dll")]
        public static extern uint SetTextAlign(IntPtr hdc,
            uint fMode);

        [DllImport("gdi32.dll")]
        public static extern int SetTextColor(IntPtr hdc,
            int crColor);

        [DllImport("user32.dll",
            ExactSpelling = true)]
        static extern IntPtr SetTimer(IntPtr hWnd,
            IntPtr nIDEvent,
            uint uElapse,
            IntPtr lpTimerFunc);

        [DllImport("gdi32.dll")]
        public static extern int SetViewportExtEx(IntPtr hdc,
            int nXOffset,
            int nYOffset,
            ref POINT lpPoint);

        [DllImport("gdi32.dll")]
        public static extern int SetViewportOrgEx(IntPtr hdc,
            int nXOffset,
            int nYOffset,
            ref POINT lpPoint);

        [DllImport("gdi32.dll")]
        public static extern bool SetWindowExtEx(IntPtr hdc,
            int X,
            int Y,
            ref POINT lpPoint);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern int SetWindowLong(IntPtr hWnd,
            int nIndex,
            int dwNewLong);

        [DllImport("gdi32.dll")]
        public static extern bool SetWindowOrgEx(IntPtr hdc,
            int X,
            int Y,
            ref POINT lpPoint);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int iX,
            int iY,
            int cX,
            int cY,
            uint uFlags);

        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hWnd,
            IntPtr hRgn,
            int bRedraw);

        [DllImport("gdi32.dll")]
        public static extern int SetWorldTransform(IntPtr tmp_hDC,
            ref XFORM lpXform);

        [DllImport("shlwapi.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr SHAutoComplete(IntPtr hwndEdit,
            uint dwFlags);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool ShowCaret(IntPtr hWnd);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd,
            int nCmdShow);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool SystemParametersInfo(int uiAction,
            int uiParam,
            IntPtr pvParam,
            int fWinIni);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool TextOut(IntPtr hdc,
            int nXStart,
            int nYStart,
            string lpString,
            int cbString);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool TrackMouseEvent(ref TRACKMOUSEEVENT lpEventTrack);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool TranslateMessage(ref MSG msg);


        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd,
            IntPtr hdcDst,
            ref POINT pptDst,
            ref SIZE psize,
            IntPtr hdcSrc,
            ref POINT pprSrc,
            int crKey,
            ref BLENDFUNCTION pblend,
            int dwFlags);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool UpdateWindow(IntPtr hwnd);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool ValidateRect(IntPtr hWnd,
            IntPtr rectangle);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool ValidateRect(IntPtr hWnd,
            ref RECT lpRect);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess,
            IntPtr lpAddress,
            int dwSize,
            int flAllocationType,
            int flProtect);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess,
            IntPtr lpAddress,
            int dwSize,
            int dwFreeType);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern short VkKeyScan(char ch);
        [return: MarshalAs(UnmanagedType.Bool)]

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool WaitMessage();

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll",
            CharSet = CharSet.Ansi,
            SetLastError = true)]
        public static extern IntPtr RegisterDeviceNotification(IntPtr recipient,
            IntPtr notificationFilter,
            int flags);

        [DllImport("user32.dll")]
        public static extern bool UnregisterDeviceNotification(IntPtr handle);

        [DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection",
            CallingConvention = CallingConvention.StdCall,
            CharSet = CharSet.Unicode)]
        public static extern void InstallHinfSection([In] IntPtr hwnd,
        [In] IntPtr ModuleHandle,
        [In, MarshalAs(UnmanagedType.LPWStr)] string CmdLineBuffer,
        int nCmdShow);

        [DllImport("winmm.dll")]
        public static extern int WaveOutGetVolume(IntPtr h,
            out uint dwVolume);

        [DllImport("kernel32",
            CharSet = CharSet.Unicode)]
        public static extern long WritePrivateProfileString(string Section,
            string Key,
            string Value,
            string FilePath);

        [DllImport("kernel32",
            CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileString(string Section,
            string Key,
            string Default,
            StringBuilder RetVal,
            int Size,
            string FilePath);

        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook,
               LowLevelKeyboardProcDelegate lpfn,
               IntPtr hMod, int dwThreadId);

        public delegate IntPtr LowLevelKeyboardProcDelegate(int nCode,
                IntPtr wParam,
                IntPtr lParam);

        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern IntPtr CallNextHookEx(
            IntPtr hhk,
            int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("winmm.dll")]
        public static extern int WaveOutSetVolume(IntPtr h,
            uint dwVolume);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd,
            ref MARGINS pMarInset);

        [DllImport("dwmapi.dll",
            SetLastError = true)]
        public static extern int DwmSetWindowAttribute(IntPtr hWnd,
            int attribute,
            int[] attrValue,
            uint cbAttribute);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        [DllImport("kernel32.dll",
            SetLastError = true,
            ExactSpelling = true)]
        public static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess,
            ref bool isDebuggerPresent);

        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn(int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd,
           out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point point);

        [DllImport("uxtheme.dll", EntryPoint = "#95")]
        public static extern uint GetImmersiveColorFromColorSetEx(uint dwImmersiveColorSet,
            uint dwImmersiveColorType,
            bool bIgnoreHighContrast,
            uint dwHighContrastCacheMode);

        [DllImport("uxtheme.dll", EntryPoint = "#96")]
        public static extern uint GetImmersiveColorTypeFromName(IntPtr pName);

        [DllImport("uxtheme.dll", EntryPoint = "#98")]
        public static extern int GetImmersiveUserColorSetPreference(bool bForceCheckRegistry,
            bool bSkipCheckOnFail);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hObjectSource,
            int nXSrc,
            int nYSrc,
            int dwRop);

        [DllImport("user32.dll")]
        public static extern int GetDisplayConfigBufferSizes(QUERY_DEVICE_CONFIG_FLAGS flags,
            out uint numPathArrayElements,
            out uint numModeInfoArrayElements);

        [DllImport("user32.dll")]
        public static extern int QueryDisplayConfig(QUERY_DEVICE_CONFIG_FLAGS flags,
            ref uint numPathArrayElements,
            [Out] Monitor.DISPLAYCONFIG_PATH_INFO[] PathInfoArray,
            ref uint numModeInfoArrayElements,
            [Out] Monitor.DISPLAYCONFIG_MODE_INFO[] ModeInfoArray,
            IntPtr currentTopologyId);

        [DllImport("user32.dll")]
        public static extern int DisplayConfigGetDeviceInfo(ref Monitor.
            DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName);

        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int x,
            int y,
            int cx,
            int cy,
            SetWindowPosFlags uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd,
            IntPtr hDC);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk,
            byte bScan,
            int dwFlags,
            int dwExtraInfo);

        [DllImport("shcore.dll",
            SetLastError = true)]
        public static extern int SetProcessDpiAwareness(PROCESS_DPI_AWARENESS PROCESS_DPI_UNAWARE);

        [DllImport("kernel32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [DllImport("uxtheme.dll",
            SetLastError = true,
            ExactSpelling = true,
            CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd,
            string pszSubAppName,
            string pszSubIdList);

        [DllImport("shell32.dll")]
        public static extern int SHEmptyRecycleBin(IntPtr hWnd,
            string pszRootPath,
            SHERB_RECYCLE dwFlags);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd,
            uint Msg,
            uint WParam,
            uint LParam);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern bool ExitWindowsEx(uint uFlags,
            uint dwReason);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern void LockWorkStation();

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern void mouse_event(int dwFlags,
            int dx,
            int dy,
            int dwData,
            UIntPtr dwExtraInfo);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd,
            uint Msg,
            IntPtr wParam,
            IntPtr lParam);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern bool MessageBeep(uint type);
    }

    /// <summary>
    /// The constants for imported functions.
    /// </summary>
    public class Constants
    {
        public const int CS_DROPSHADOW = 0x20000;
        public const uint WM_SYSCOMMAND = 0x0112;
        public const uint DOMOVE = 0xF012;
        public const int STD_OUTPUT_HANDLE = -11;
        public const int MOUSEEVENTF_MOVE = 0x0001;
        public const int SC_MONITORPOWER = 0xF170;
        public const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        public const int WM_NCHITTEST = 0x84;
        public const int HTCLIENT = 0x1;
        public const int HTCAPTION = 0x2;
        public const int WM_NCPAINT = 0x0085;
        public const int WM_ACTIVATEAPP = 0x001C;
        public const int WH_KEYBOARD_LL = 13;
        public const int SRCCOPY = 0x00CC0020;

        public static class SWP
        {
            public static readonly int
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000;
        }
    }

    /// <summary>
    /// The enums for imported functions.
    /// </summary>
    public class Enums
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DTBGOPTS
        {
            public int dwSize;
            public int dwFlags;
            public RECT rcClip;
        }

        public delegate bool EnumThreadWindowsCallBack(IntPtr hWnd, IntPtr lParam);

        internal delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            bool fIcon;
            Int32 xHotspot;
            Int32 yHotspot;
            IntPtr hbmMask;
            IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LOGBRUSH
        {
            public uint lbStyle;
            public uint lbColor;
            public uint lbHatch;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string lfFaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEHOOKSTRUCT
        {
            public int pt_x;
            public int pt_y;
            public IntPtr hWnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEHOOKSTRUCTEX
        {
            public MOUSEHOOKSTRUCT MOUSEHOOKSTRUCT;
            public uint mouseData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSG
        {
            public IntPtr hwnd;
            public int message;
            public IntPtr wParam;
            public IntPtr lParam;
            public int time;
            public int pt_x;
            public int pt_y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NCCALCSIZE_PARAMS
        {
            public RECT rgrc0;
            public RECT rgrc1;
            public RECT rgrc2;
            public IntPtr lppos;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct NONCLIENTMETRICS
        {
            public int cbSize;
            public int iBorderWidth;
            public int iScrollWidth;
            public int iScrollHeight;
            public int iCaptionWidth;
            public int iCaptionHeight;
            public LOGFONT lfCaptionFont;
            public int iSmCaptionWidth;
            public int iSmCaptionHeight;
            public LOGFONT lfSmCaptionFont;
            public int iMenuWidth;
            public int iMenuHeight;
            public LOGFONT lfMenuFont;
            public LOGFONT lfStatusFont;
            public LOGFONT lfMessageFont;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public int rcPaint_left;
            public int rcPaint_top;
            public int rcPaint_right;
            public int rcPaint_bottom;
            public bool fRestore;
            public bool fIncUpdate;
            public int reserved1;
            public int reserved2;
            public int reserved3;
            public int reserved4;
            public int reserved5;
            public int reserved6;
            public int reserved7;
            public int reserved8;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SCROLLBARINFO
        {
            public int cbSize;
            public RECT rcScrollBar;
            public int dxyLineButton;
            public int xyThumbTop;
            public int xyThumbBottom;
            public int reserved;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public int[] rgstate;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SCROLLINFO
        {
            public int cbSize;
            public int fMask;
            public int nMin;
            public int nMax;
            public int nPage;
            public int nPos;
            public int nTrackPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SIZE
        {
            public int cx;
            public int cy;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TBBUTTON
        {
            public int iBitmap;
            public int idCommand;
            public byte fsState;
            public byte fsStyle;
            public short bReserved;
            public IntPtr dwData;
            public IntPtr iString;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TEXTMETRIC
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public char tmFirstChar;
            public char tmLastChar;
            public char tmDefaultChar;
            public char tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
        }

        public enum THEMESIZE
        {
            TS_MIN,
            TS_TRUE,
            TS_DRAW
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TRACKMOUSEEVENT
        {
            public uint cbSize;
            public uint dwFlags;
            public IntPtr hwndTrack;
            public uint dwHoverTime;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcNormalPosition;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct WINDOWPOS
        {
            public IntPtr hwnd;
            public IntPtr hwndInsertAfter;
            public int x;
            public int y;
            public int cx;
            public int cy;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct XFORM
        {
            public float eM11;
            public float eM12;
            public float eM21;
            public float eM22;
            public float eDx;
            public float eDy;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KeyboardHookStruct
        {
            public readonly int VirtualKeyCode;
            public readonly int ScanCode;
            public readonly int Flags;
            public readonly int Time;
            public readonly IntPtr ExtraInfo;
        }

        public static class HWND
        {
            public static IntPtr
            NoTopMost = new IntPtr(-2),
            TopMost = new IntPtr(-1),
            Top = new IntPtr(0),
            Bottom = new IntPtr(1);
        }

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            SWP_ASYNCWINDOWPOS = 0x4000,
            SWP_DEFERERASE = 0x2000,
            SWP_DRAWFRAME = 0x0020,
            SWP_FRAMECHANGED = 0x0020,
            SWP_HIDEWINDOW = 0x0080,
            SWP_NOACTIVATE = 0x0010,
            SWP_NOCOPYBITS = 0x0100,
            SWP_NOMOVE = 0x0002,
            SWP_NOOWNERZORDER = 0x0200,
            SWP_NOREDRAW = 0x0008,
            SWP_NOREPOSITION = 0x0200,
            SWP_NOSENDCHANGING = 0x0400,
            SWP_NOSIZE = 0x0001,
            SWP_NOZORDER = 0x0004,
            SWP_SHOWWINDOW = 0x0040
        }

        public enum KEYBOARD_PRESETS : int
        {
            HIDE_THIS_WINDOW = 0,
            HIDE_OR_SHOW_ALL_WINDOWS = 1,
            SHOW_ALL_WINDOWS = 2
        }

        public enum SpecialWindowHandles
        {
            HWND_TOP = 0,
            HWND_BOTTOM = 1,
            HWND_TOPMOST = -1,
            HWND_NOTOPMOST = -2
        }

        public enum QUERY_DEVICE_CONFIG_FLAGS : uint
        {
            QDC_ALL_PATHS = 0x00000001,
            QDC_ONLY_ACTIVE_PATHS = 0x00000002,
            QDC_DATABASE_CURRENT = 0x00000004
        }

        public enum DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY : uint
        {
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_OTHER = 0xFFFFFFFF,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15 = 0,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SVIDEO = 1,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPOSITE_VIDEO = 2,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPONENT_VIDEO = 3,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DVI = 4,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HDMI = 5,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_LVDS = 6,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN = 8,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDI = 9,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EXTERNAL = 10,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EMBEDDED = 11,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EXTERNAL = 12,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EMBEDDED = 13,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE = 14,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_MIRACAST = 15,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_INTERNAL = 0x80000000,
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_FORCE_UINT32 = 0xFFFFFFFF
        }

        public enum DISPLAYCONFIG_SCANLINE_ORDERING : uint
        {
            DISPLAYCONFIG_SCANLINE_ORDERING_UNSPECIFIED = 0,
            DISPLAYCONFIG_SCANLINE_ORDERING_PROGRESSIVE = 1,
            DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED = 2,
            DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_UPPERFIELDFIRST = 2,
            DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_LOWERFIELDFIRST = 3,
            DISPLAYCONFIG_SCANLINE_ORDERING_FORCE_UINT32 = 0xFFFFFFFF
        }

        public enum DISPLAYCONFIG_ROTATION : uint
        {
            DISPLAYCONFIG_ROTATION_IDENTITY = 1,
            DISPLAYCONFIG_ROTATION_ROTATE90 = 2,
            DISPLAYCONFIG_ROTATION_ROTATE180 = 3,
            DISPLAYCONFIG_ROTATION_ROTATE270 = 4,
            DISPLAYCONFIG_ROTATION_FORCE_UINT32 = 0xFFFFFFFF
        }

        public enum DISPLAYCONFIG_SCALING : uint
        {
            DISPLAYCONFIG_SCALING_IDENTITY = 1,
            DISPLAYCONFIG_SCALING_CENTERED = 2,
            DISPLAYCONFIG_SCALING_STRETCHED = 3,
            DISPLAYCONFIG_SCALING_ASPECTRATIOCENTEREDMAX = 4,
            DISPLAYCONFIG_SCALING_CUSTOM = 5,
            DISPLAYCONFIG_SCALING_PREFERRED = 128,
            DISPLAYCONFIG_SCALING_FORCE_UINT32 = 0xFFFFFFFF
        }

        public enum DISPLAYCONFIG_PIXELFORMAT : uint
        {
            DISPLAYCONFIG_PIXELFORMAT_8BPP = 1,
            DISPLAYCONFIG_PIXELFORMAT_16BPP = 2,
            DISPLAYCONFIG_PIXELFORMAT_24BPP = 3,
            DISPLAYCONFIG_PIXELFORMAT_32BPP = 4,
            DISPLAYCONFIG_PIXELFORMAT_NONGDI = 5,
            DISPLAYCONFIG_PIXELFORMAT_FORCE_UINT32 = 0xFFFFFFFF
        }

        public enum DISPLAYCONFIG_MODE_INFO_TYPE : uint
        {
            DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE = 1,
            DISPLAYCONFIG_MODE_INFO_TYPE_TARGET = 2,
            DISPLAYCONFIG_MODE_INFO_TYPE_FORCE_UINT32 = 0xFFFFFFFF
        }

        public enum DISPLAYCONFIG_DEVICE_INFO_TYPE : uint
        {
            DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME = 1,
            DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME = 2,
            DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE = 3,
            DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME = 4,
            DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE = 5,
            DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE = 6,
            DISPLAYCONFIG_DEVICE_INFO_FORCE_UINT32 = 0xFFFFFFFF
        }

        internal enum WINDOW_MESSAGE : uint
        {
            NULL = 0x0000,
            CREATE = 0x0001,
            DESTROY = 0x0002,
            MOVE = 0x0003,
            SIZE = 0x0005,
            ACTIVATE = 0x0006,
            SETFOCUS = 0x0007,
            KILLFOCUS = 0x0008,
            ENABLE = 0x000A,
            SETREDRAW = 0x000B,
            SETTEXT = 0x000C,
            GETTEXT = 0x000D,
            GETTEXTLENGTH = 0x000E,
            PAINT = 0x000F,
            CLOSE = 0x0010,
            QUERYENDSESSION = 0x0011,
            QUERYOPEN = 0x0013,
            ENDSESSION = 0x0016,
            QUIT = 0x0012,
            ERASEBKGND = 0x0014,
            SYSCOLORCHANGE = 0x0015,
            SHOWWINDOW = 0x0018,
            WININICHANGE = 0x001A,
            SETTINGCHANGE = WININICHANGE,
            DEVMODECHANGE = 0x001B,
            ACTIVATEAPP = 0x001C,
            FONTCHANGE = 0x001D,
            TIMECHANGE = 0x001E,
            CANCELMODE = 0x001F,
            SETCURSOR = 0x0020,
            MOUSEACTIVATE = 0x0021,
            CHILDACTIVATE = 0x0022,
            QUEUESYNC = 0x0023,
            GETMINMAXINFO = 0x0024,
            PAINTICON = 0x0026,
            ICONERASEBKGND = 0x0027,
            NEXTDLGCTL = 0x0028,
            SPOOLERSTATUS = 0x002A,
            DRAWITEM = 0x002B,
            MEASUREITEM = 0x002C,
            DELETEITEM = 0x002D,
            VKEYTOITEM = 0x002E,
            CHARTOITEM = 0x002F,
            SETFONT = 0x0030,
            GETFONT = 0x0031,
            SETHOTKEY = 0x0032,
            GETHOTKEY = 0x0033,
            QUERYDRAGICON = 0x0037,
            COMPAREITEM = 0x0039,
            GETOBJECT = 0x003D,
            COMPACTING = 0x0041,
            COMMNOTIFY = 0x0044,
            WINDOWPOSCHANGING = 0x0046,
            WINDOWPOSCHANGED = 0x0047,
            POWER = 0x0048,
            COPYDATA = 0x004A,
            CANCELJOURNAL = 0x004B,
            NOTIFY = 0x004E,
            INPUTLANGCHANGEREQUEST = 0x0050,
            INPUTLANGCHANGE = 0x0051,
            TCARD = 0x0052,
            HELP = 0x0053,
            USERCHANGED = 0x0054,
            NOTIFYFORMAT = 0x0055,
            CONTEXTMENU = 0x007B,
            STYLECHANGING = 0x007C,
            STYLECHANGED = 0x007D,
            DISPLAYCHANGE = 0x007E,
            GETICON = 0x007F,
            SETICON = 0x0080,
            NCCREATE = 0x0081,
            NCDESTROY = 0x0082,
            NCCALCSIZE = 0x0083,
            NCHITTEST = 0x0084,
            NCPAINT = 0x0085,
            NCACTIVATE = 0x0086,
            GETDLGCODE = 0x0087,
            SYNCPAINT = 0x0088,
            NCMOUSEMOVE = 0x00A0,
            NCLBUTTONDOWN = 0x00A1,
            NCLBUTTONUP = 0x00A2,
            NCLBUTTONDBLCLK = 0x00A3,
            NCRBUTTONDOWN = 0x00A4,
            NCRBUTTONUP = 0x00A5,
            NCRBUTTONDBLCLK = 0x00A6,
            NCMBUTTONDOWN = 0x00A7,
            NCMBUTTONUP = 0x00A8,
            NCMBUTTONDBLCLK = 0x00A9,
            NCXBUTTONDOWN = 0x00AB,
            NCXBUTTONUP = 0x00AC,
            NCXBUTTONDBLCLK = 0x00AD,
            INPUT_DEVICE_CHANGE = 0x00FE,
            INPUT = 0x00FF,
            KEYFIRST = 0x0100,
            KEYDOWN = 0x0100,
            KEYUP = 0x0101,
            CHAR = 0x0102,
            DEADCHAR = 0x0103,
            SYSKEYDOWN = 0x0104,
            SYSKEYUP = 0x0105,
            SYSCHAR = 0x0106,
            SYSDEADCHAR = 0x0107,
            UNICHAR = 0x0109,
            KEYLAST = 0x0109,
            IME_STARTCOMPOSITION = 0x010D,
            IME_ENDCOMPOSITION = 0x010E,
            IME_COMPOSITION = 0x010F,
            IME_KEYLAST = 0x010F,
            INITDIALOG = 0x0110,
            COMMAND = 0x0111,
            SYSCOMMAND = 0x0112,
            TIMER = 0x0113,
            HSCROLL = 0x0114,
            VSCROLL = 0x0115,
            INITMENU = 0x0116,
            INITMENUPOPUP = 0x0117,
            MENUSELECT = 0x011F,
            MENUCHAR = 0x0120,
            ENTERIDLE = 0x0121,
            MENURBUTTONUP = 0x0122,
            MENUDRAG = 0x0123,
            MENUGETOBJECT = 0x0124,
            UNINITMENUPOPUP = 0x0125,
            MENUCOMMAND = 0x0126,
            CHANGEUISTATE = 0x0127,
            UPDATEUISTATE = 0x0128,
            QUERYUISTATE = 0x0129,
            CTLCOLORMSGBOX = 0x0132,
            CTLCOLOREDIT = 0x0133,
            CTLCOLORLISTBOX = 0x0134,
            CTLCOLORBTN = 0x0135,
            CTLCOLORDLG = 0x0136,
            CTLCOLORSCROLLBAR = 0x0137,
            CTLCOLORSTATIC = 0x0138,
            MOUSEFIRST = 0x0200,
            MOUSEMOVE = 0x0200,
            LBUTTONDOWN = 0x0201,
            LBUTTONUP = 0x0202,
            LBUTTONDBLCLK = 0x0203,
            RBUTTONDOWN = 0x0204,
            RBUTTONUP = 0x0205,
            RBUTTONDBLCLK = 0x0206,
            MBUTTONDOWN = 0x0207,
            MBUTTONUP = 0x0208,
            MBUTTONDBLCLK = 0x0209,
            MOUSEWHEEL = 0x020A,
            XBUTTONDOWN = 0x020B,
            XBUTTONUP = 0x020C,
            XBUTTONDBLCLK = 0x020D,
            MOUSEHWHEEL = 0x020E,
            MOUSELAST = 0x020E,
            PARENTNOTIFY = 0x0210,
            ENTERMENULOOP = 0x0211,
            EXITMENULOOP = 0x0212,
            NEXTMENU = 0x0213,
            SIZING = 0x0214,
            CAPTURECHANGED = 0x0215,
            MOVING = 0x0216,
            POWERBROADCAST = 0x0218,
            DEVICECHANGE = 0x0219,
            MDICREATE = 0x0220,
            MDIDESTROY = 0x0221,
            MDIACTIVATE = 0x0222,
            MDIRESTORE = 0x0223,
            MDINEXT = 0x0224,
            MDIMAXIMIZE = 0x0225,
            MDITILE = 0x0226,
            MDICASCADE = 0x0227,
            MDIICONARRANGE = 0x0228,
            MDIGETACTIVE = 0x0229,
            MDISETMENU = 0x0230,
            ENTERSIZEMOVE = 0x0231,
            EXITSIZEMOVE = 0x0232,
            DROPFILES = 0x0233,
            MDIREFRESHMENU = 0x0234,
            IME_SETCONTEXT = 0x0281,
            IME_NOTIFY = 0x0282,
            IME_CONTROL = 0x0283,
            IME_COMPOSITIONFULL = 0x0284,
            IME_SELECT = 0x0285,
            IME_CHAR = 0x0286,
            IME_REQUEST = 0x0288,
            IME_KEYDOWN = 0x0290,
            IME_KEYUP = 0x0291,
            MOUSEHOVER = 0x02A1,
            MOUSELEAVE = 0x02A3,
            NCMOUSEHOVER = 0x02A0,
            NCMOUSELEAVE = 0x02A2,
            WTSSESSION_CHANGE = 0x02B1,
            TABLET_FIRST = 0x02c0,
            TABLET_LAST = 0x02df,
            CUT = 0x0300,
            COPY = 0x0301,
            PASTE = 0x0302,
            CLEAR = 0x0303,
            UNDO = 0x0304,
            RENDERFORMAT = 0x0305,
            RENDERALLFORMATS = 0x0306,
            DESTROYCLIPBOARD = 0x0307,
            DRAWCLIPBOARD = 0x0308,
            PAINTCLIPBOARD = 0x0309,
            VSCROLLCLIPBOARD = 0x030A,
            SIZECLIPBOARD = 0x030B,
            ASKCBFORMATNAME = 0x030C,
            CHANGECBCHAIN = 0x030D,
            HSCROLLCLIPBOARD = 0x030E,
            QUERYNEWPALETTE = 0x030F,
            PALETTEISCHANGING = 0x0310,
            PALETTECHANGED = 0x0311,
            HOTKEY = 0x0312,
            PRINT = 0x0317,
            PRINTCLIENT = 0x0318,
            APPCOMMAND = 0x0319,
            THEMECHANGED = 0x031A,
            CLIPBOARDUPDATE = 0x031D,
            DWMCOMPOSITIONCHANGED = 0x031E,
            DWMNCRENDERINGCHANGED = 0x031F,
            DWMCOLORIZATIONCOLORCHANGED = 0x0320,
            DWMWINDOWMAXIMIZEDCHANGE = 0x0321,
            GETTITLEBARINFOEX = 0x033F,
            HANDHELDFIRST = 0x0358,
            HANDHELDLAST = 0x035F,
            AFXFIRST = 0x0360,
            AFXLAST = 0x037F,
            PENWINFIRST = 0x0380,
            PENWINLAST = 0x038F,
            APP = 0x8000,
            USER = 0x0400,
            CPL_LAUNCH = USER + 0x1000,
            CPL_LAUNCHED = USER + 0x1001,
            SYSTIMER = 0x118,
            HSHELL_ACCESSIBILITYSTATE = 11,
            HSHELL_ACTIVATESHELLWINDOW = 3,
            HSHELL_APPCOMMAND = 12,
            HSHELL_GETMINRECT = 5,
            HSHELL_LANGUAGE = 8,
            HSHELL_REDRAW = 6,
            HSHELL_TASKMAN = 7,
            HSHELL_WINDOWCREATED = 1,
            HSHELL_WINDOWDESTROYED = 2,
            HSHELL_WINDOWACTIVATED = 4,
            HSHELL_WINDOWREPLACED = 13
        }

        public enum WindowPosFlags : int
        {
            HWND_TOPMOST = -1,
            SWP_NOMOVE = 0x0002,
            SWP_NOSIZE = 0x0001
        }

        public enum KEYEVENTF : int
        {
            KEYEVENTF_EXTENDEDKEY = 1,
            KEYEVENTF_KEYUP = 2
        }

        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        public enum DWM_WINDOW_CORNER_PREFERENCE : int
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        public enum PROCESS_DPI_AWARENESS : int
        {
            PROCESS_DPI_UNAWARE = 0,
            PROCESS_SYSTEM_DPI_AWARE = 1,
            PROCESS_PER_MONITOR_DPI_Aware = 2
        }

        public enum SHERB_RECYCLE : int
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000001,
            SHERB_NOSOUND = 0x00000004
        }

        public enum E_DATA_FLOW
        {
            eRender,
            eCapture,
            eAll,
            E_DATA_FLOW_enum_count
        }

        public enum E_ROLE
        {
            eConsole,
            eMultimedia,
            eCommunications,
            E_ROLE_enum_count
        }

        public enum TASKBAR_STATES : int
        {
            NoProgress = 0,
            Indeterminate = 0x1,
            Normal = 0x2,
            Error = 0x4,
            Paused = 0x8
        }
    }
}
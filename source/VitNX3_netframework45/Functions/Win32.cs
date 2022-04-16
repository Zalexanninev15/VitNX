using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

using VitNX3.Functions.WinControllers;

using static VitNX3.Functions.Win32.Enums;
using static VitNX3.Functions.Win32.Constants;
using System.Security;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VitNX3.Functions.Win32
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

        [DllImport("kernel32.dll",
            EntryPoint = "GetSystemFirmwareTable",
            SetLastError = true,
            ThrowOnUnmappableChar = true)]
        public static extern uint GetSystemFirmwareTable(FirmwareTableType FirmwareTableProviderSignature,
        uint FirmwareTableID,
        IntPtr pFirmwareTableBuffer,
        uint BufferSize);

        [DllImport("kernel32.dll",
            EntryPoint = "EnumSystemFirmwareTables",
            SetLastError = true)]
        public static extern uint EnumSystemFirmwareTables(
        FirmwareTableType FirmwareTableProviderSignature,
        IntPtr pFirmwareTableEnuM_BUffer,
        uint BufferSize);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern uint GetFirmwareEnvironmentVariableA(string lpName,
            string lpGuid,
            IntPtr pBuffer,
            uint nSize);

        [DllImport("kernel32.dll",
            EntryPoint = "SetFirmwareEnvironmentVariable",
            SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetFirmwareEnvironmentVariable([In][MarshalAs(UnmanagedType.LPTStr)] string lpName,
        [In][MarshalAs(UnmanagedType.LPTStr)] string lpGuid,
        [In] IntPtr pBuffer,
        uint nSize);

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

        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd,
            bool bRevert);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern bool GetMenuItemInfo(IntPtr hMenu,
            uint uItem,
            bool fByPosition,
            [In, Out] MENU_ITEM_INFO lpmii);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern bool InsertMenuItem(IntPtr hMenu,
            uint uItem,
            bool fByPosition, [In]
           MENU_ITEM_INFO lpmii);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto,
            SetLastError = true)]
        public static extern bool SetMenuItemInfo(IntPtr hMenu,
            uint uItem,
            bool fByPosition,
            [In] MENU_ITEM_INFO lpmii);

        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern bool RemoveMenu(IntPtr hMenu,
            uint uItem,
            bool fByPosition);

        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWinEventHook(uint eventMin,
            uint eventMax,
            IntPtr hmodWinEventProc,
            WinEventDelegate lpfnWinEventProc,
            uint idProcess,
            uint idThread,
            uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [SecurityCritical, DllImport("shell32.dll",
            CharSet = CharSet.Auto)]
        public static extern int Shell_NotifyIcon(int message,
            NOTIFY_ICON_DATA pnid);

        [DllImport("kernel32.dll",
            EntryPoint = "CopyMemory",
            SetLastError = false,
            CharSet = CharSet.Auto)]
        public static extern void CopyMemory(IntPtr dest,
            IntPtr src,
            uint count);

        public delegate void WinEventDelegate(IntPtr hWinEventHook,
            uint eventType,
            IntPtr hwnd,
            int idObject,
            int idChild,
            uint dwEventThread,
            uint dwmsEventTime);

        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOW_INFO pwi);

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

        [DllImport("gdiplus.dll",
            CharSet = CharSet.Auto)]
        public static extern int GdipCreateHICONFromBitmap(HandleRef nativeBitmap,
            out IntPtr hicon);

        [DllImport("gdi32.dll",
            EntryPoint = "DeleteObject",
            CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

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
            THEME_SIZE eSize,
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
        private static extern IntPtr SetTimer(IntPtr hWnd,
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

        [DllImport("ntdll.dll",
            SetLastError = true)]
        private static extern int NtQueryTimerResolution(out int MinimumResolution,
        out int MaximumResolution,
        out int CurrentResolution);

        [DllImport("ntdll.dll",
            SetLastError = true)]
        private static extern int NtSetTimerResolution(int DesiredResolution,
            bool SetResolution,
            out int CurrentResolution);

        [DllImport("ntdll.dll",
            SetLastError = true)]
        private static extern unsafe int NtDelayExecution(bool alertable,
            long* delayInterval);

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
            DWM_GET_WINDOW_ATTRIBUTE attribute,
            int[] attrValue,
            uint cbAttribute);

        [DllImport("dwmapi.dll",
            EntryPoint = "#127",
            PreserveSig = false,
            CharSet = CharSet.Unicode)]
        public static extern void DwmGetColorizationParameters(out DWM_COLORIZATION_PARAMS dwParameters);

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

        public delegate bool EnumThreadWindowsCallBack(IntPtr hWnd,
        IntPtr lParam);

        public delegate IntPtr HookProc(int nCode,
            IntPtr wParam,
            IntPtr lParam);

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
            SET_WINDOW_POS_FLAGS uFlags);

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
        public const uint MFT_STRING = 0x00000000;
        public const uint MFS_CHECKED = 0x00000008;
        public const uint MFS_UNCHECKED = 0x00000000;
        public const int ERROR_INVALID_FUNCTION = 1;

        [StructLayout(LayoutKind.Sequential,
            CharSet = CharSet.Auto)]
        public class MENU_ITEM_INFO
        {
            public MENU_ITEM_INFO()
            { }
            public int cbSize = Marshal.SizeOf(typeof(MENU_ITEM_INFO));
            public uint fType;
            public uint fState;
            public uint wID;
            public IntPtr hSubMenu;
            public IntPtr hbmpChecked;
            public IntPtr hbmpUnchecked;
            public IntPtr dwItemData;
            public string dwTypeData = null;
            public uint cch;
            public IntPtr hbmpItem;
        }

        [StructLayout(LayoutKind.Sequential,
            CharSet = CharSet.Auto)]
        public class NOTIFY_ICON_DATA
        {
            public int cbSize = Marshal.SizeOf(typeof(NOTIFY_ICON_DATA));
            public IntPtr hWnd;
            public int uID;
            public int uCallbackMessage;
            public IntPtr hIcon;
            [MarshalAs(UnmanagedType.ByValTStr,
                SizeConst = 0x80)]
            public string szTip;
            public int dwState;
            public int dwStateMask;
            [MarshalAs(UnmanagedType.ByValTStr,
                SizeConst = 0x100)]
            public string szInfo;
            public int uTimeoutOrVersion;
            [MarshalAs(UnmanagedType.ByValTStr,
                SizeConst = 0x40)]
            public string szInfoTitle;
            public int dwInfoFlags;
        }
    }

    /// <summary>
    /// The enums for imported functions.
    /// </summary>
    public class Enums
    {
        public enum DWM_GET_WINDOW_ATTRIBUTE : uint
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE_NOT = 19,
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,
            DWMWA_SYSTEMBACKDROP_TYPE = 38,
            DWMWA_MICA_EFFECT = 1029,
        }

        [StructLayout(LayoutKind.Sequential,
            Pack = 8,
            CharSet = CharSet.Unicode)]
        public struct THUMBBUTTON
        {
            public const int THBN_CLICKED = 0x1800;
            public THB dwMask;
            public uint iId;
            public uint iBitmap;
            public IntPtr hIcon;
            [MarshalAs(UnmanagedType.ByValTStr,
                SizeConst = 260)]
            public string szTip;
            public THBF dwFlags;
        }

        [Flags]
        public enum STPF
        {
            NONE = 0x00000000,
            USE_APP_THUMBNAIL_ALWAYS = 0x00000001,
            USE_APP_THUMBNAIL_WHEN_ACTIVE = 0x00000002,
            USE_APP_PEEK_ALWAYS = 0x00000004,
            USE_APP_PEEK_WHEN_ACTIVE = 0x00000008,
        }

        [Flags]
        public enum THB : uint
        {
            BITMAP = 0x0001,
            ICON = 0x0002,
            TOOLTIP = 0x0004,
            FLAGS = 0x0008,
        }

        [Flags]
        public enum THBF : uint
        {
            ENABLED = 0x0000,
            DISABLED = 0x0001,
            DISMISSON_CLICK = 0x0002,
            NO_BACKGROUND = 0x0004,
            HIDDEN = 0x0008,
            NO_INTERACTIVE = 0x0010,
        }

        [Flags]
        public enum DWM_SBT : uint
        {
            DWMSBT_AUTO = 0,
            DWMSBT_DISABLE = 1,
            DWMSBT_MAINWINDOW = 2,
            DWMSBT_TRANSIENTWINDOW = 3,
            DWMSBT_TABBEDWINDOW = 4
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        public struct DWM_COLORIZATION_PARAMS
        {
            public uint clrColor;
            public uint clrAfterGlow;
            public uint nIntensity;
            public uint clrAfterGlowBalance;
            public uint clrBlurBalance;
            public uint clrGlassReflectionIntensity;
            public bool fOpaque;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DTBGOPTS
        {
            public int dwSize;
            public int dwFlags;
            public RECT rcClip;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOW_INFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            private bool fIcon;
            private int xHotspot;
            private int yHotspot;
            private IntPtr hbmMask;
            private IntPtr hbmColor;
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

        [StructLayout(LayoutKind.Sequential,
            CharSet = CharSet.Unicode)]
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

            [MarshalAs(UnmanagedType.ByValTStr,
                SizeConst = 0x20)]
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

        [StructLayout(LayoutKind.Sequential,
            CharSet = CharSet.Unicode)]
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

        [Flags]
        public enum DWM_WINDOW_ATTRIBUTE : uint
        {
            DWMWA_ALLOW_NC_PAINT = 4,
            DWMWA_CAPTION_BUTTON_BOUNDS = 5,
            DWMWA_FORCE_ICONIC_REPRESENTATION = 7,
            DWMWA_CLOAK = 13,
            DWMWA_CLOAKED = 14,
            DWMWA_FREEZE_REPRESENTATION = 15,
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,
            DWMWA_BORDER_COLOR = 34,
            DWMWA_CAPTION_COLOR = 35,
            DWMWA_TEXT_COLOR = 36,
            DWMWA_VISIBLE_FRAME_BORDER_THICKNESS = 37,
            DWMWA_SYSTEMBACKDROP_TYPE = 38,
            DWMWA_MICA_EFFECT = 1029
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

            [MarshalAs(UnmanagedType.ByValArray,
                SizeConst = 6)]
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

        [StructLayout(LayoutKind.Sequential,
            CharSet = CharSet.Unicode)]
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

        [Flags]
        public enum THEME_SIZE
        {
            TS_MIN,
            TS_TRUE,
            TS_DRAW
        }

        [Flags]
        public enum EVENTS : uint
        {
            EVENT_OBJECT_INVOKED = 0x8013,
            EVENT_OBJECT_FOCUS = 0x8005,
            WINEVENT_OUTOFCONTEXT = 0
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
            HWND_NO_TOPMOST = new IntPtr(-2),
            HWND_TOPMOST = new IntPtr(-1),
            HWND_TOP = new IntPtr(0),
            HWND_BOTTOM = new IntPtr(1);
        }

        [Flags]
        public enum SET_WINDOW_POS_FLAGS : uint
        {
            SWP_ASYNCHRONOUS_WINDOW_POS = 0x4000,
            SWP_DEFER_ERASE = 0x2000,
            SWP_DRAW_FRAME = 0x0020,
            SWP_FRAME_CHANGED = 0x0020,
            SWP_HIDE_WINDOW = 0x0080,
            SWP_DO_NOT_ACTIVATE = 0x0010,
            SWP_DO_NOT_COPY_BITS = 0x0100,
            SWP_IGNORE_MOVE = 0x0002,
            SWP_DO_NOT_CHANGE_OWNER_ZORDER = 0x0200,
            SWP_DO_NOT_REDRAW = 0x0008,
            SWP_DO_NOT_REPOSITION = 0x0200,
            SWP_DO_NOT_SEND_CHANGING_EVENT = 0x0400,
            SWP_IGNORE_RESIZE = 0x0001,
            SWP_NO_ZORDER = 0x0004,
            SWP_SHOW_WINDOW = 0x0040
        }

        [Flags]
        public enum KEYBOARD_PRESETS : int
        {
            HIDE_THIS_WINDOW = 0,
            HIDE_OR_SHOW_ALL_WINDOWS = 1,
            SHOW_ALL_WINDOWS = 2
        }

        [Flags]
        public enum SpecialWindowHandles
        {
            HWND_TOP = 0,
            HWND_BOTTOM = 1,
            HWND_TOPMOST = -1,
            HWND_NOTOPMOST = -2
        }

        [Flags]
        public enum MIIM
        {
            BITMAP = 0x00000080,
            CHECKMARKS = 0x00000008,
            DATA = 0x00000020,
            FTYPE = 0x00000100,
            ID = 0x00000002,
            STATE = 0x00000001,
            STRING = 0x00000040,
            SUBMENU = 0x00000004,
            TYPE = 0x00000010
        }

        [Flags]
        public enum QUERY_DEVICE_CONFIG_FLAGS : uint
        {
            QDC_ALL_PATHS = 0x00000001,
            QDC_ONLY_ACTIVE_PATHS = 0x00000002,
            QDC_DATABASE_CURRENT = 0x00000004
        }

        [Flags]
        public enum PV_ATTRIBUTE
        {
            Disable = 0x00,
            Enable = 0x01
        }

        [Flags]
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

        [Flags]
        public enum DISPLAYCONFIG_SCANLINE_ORDERING : uint
        {
            DISPLAYCONFIG_SCANLINE_ORDERING_UNSPECIFIED = 0,
            DISPLAYCONFIG_SCANLINE_ORDERING_PROGRESSIVE = 1,
            DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED = 2,
            DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_UPPER_FIELD_FIRST = 2,
            DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_LOWER_FIELD_FIRST = 3,
            DISPLAYCONFIG_SCANLINE_ORDERING_FORCE_UINT32 = 0xFFFFFFFF
        }

        [Flags]
        public enum DISPLAYCONFIG_ROTATION : uint
        {
            DISPLAYCONFIG_ROTATION_IDENTITY = 1,
            DISPLAYCONFIG_ROTATION_ROTATE90 = 2,
            DISPLAYCONFIG_ROTATION_ROTATE180 = 3,
            DISPLAYCONFIG_ROTATION_ROTATE270 = 4,
            DISPLAYCONFIG_ROTATION_FORCE_UINT32 = 0xFFFFFFFF
        }

        [Flags]
        public enum DISPLAYCONFIG_SCALING : uint
        {
            DISPLAYCONFIG_SCALING_IDENTITY = 1,
            DISPLAYCONFIG_SCALING_CENTERED = 2,
            DISPLAYCONFIG_SCALING_STRETCHED = 3,
            DISPLAYCONFIG_SCALING_ASPECT_RATIO_CENTERED_MAX = 4,
            DISPLAYCONFIG_SCALING_CUSTOM = 5,
            DISPLAYCONFIG_SCALING_PREFERRED = 128,
            DISPLAYCONFIG_SCALING_FORCE_UINT32 = 0xFFFFFFFF
        }

        [Flags]
        public enum DISPLAYCONFIG_PIXELFORMAT : uint
        {
            DISPLAYCONFIG_PIXELFORMAT_8BPP = 1,
            DISPLAYCONFIG_PIXELFORMAT_16BPP = 2,
            DISPLAYCONFIG_PIXELFORMAT_24BPP = 3,
            DISPLAYCONFIG_PIXELFORMAT_32BPP = 4,
            DISPLAYCONFIG_PIXELFORMAT_NONGDI = 5,
            DISPLAYCONFIG_PIXELFORMAT_FORCE_UINT32 = 0xFFFFFFFF
        }

        [Flags]
        public enum DISPLAYCONFIG_MODE_INFO_TYPE : uint
        {
            DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE = 1,
            DISPLAYCONFIG_MODE_INFO_TYPE_TARGET = 2,
            DISPLAYCONFIG_MODE_INFO_TYPE_FORCE_UINT32 = 0xFFFFFFFF
        }

        [Flags]
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

        [Flags]
        public enum WINDOW_MESSAGE : uint
        {
            NULL = 0x0000,
            CREATE = 0x0001,
            DESTROY = 0x0002,
            MOVE = 0x0003,
            SIZE = 0x0005,
            ACTIVATE = 0x0006,
            SET_FOCUS = 0x0007,
            KILL_FOCUS = 0x0008,
            ENABLE = 0x000A,
            SET_REDRAW = 0x000B,
            SET_TEXT = 0x000C,
            GET_TEXT = 0x000D,
            GET_TEXT_LENGTH = 0x000E,
            PAINT = 0x000F,
            CLOSE = 0x0010,
            QUERY_END_SESSION = 0x0011,
            QUERY_OPEN = 0x0013,
            END_SESSION = 0x0016,
            QUIT = 0x0012,
            ERASE_BKGND = 0x0014,
            SYS_COLOR_CHANGE = 0x0015,
            SHOW_WINDOW = 0x0018,
            WIN_IN_CHANGE = 0x001A,
            SET_TING_CHANGE = WIN_IN_CHANGE,
            DEV_MODE_CHANGE = 0x001B,
            ACTIVATE_APP = 0x001C,
            FONT_CHANGE = 0x001D,
            TIME_CHANGE = 0x001E,
            CANCEL_MODE = 0x001F,
            SET_CURSOR = 0x0020,
            MOUSE_ACTIVATE = 0x0021,
            CHILD_ACTIVATE = 0x0022,
            QUEUE_SYNC = 0x0023,
            GET_MIN_MAX_INFO = 0x0024,
            PAINT_ICON = 0x0026,
            ICON_ERASE_BKGND = 0x0027,
            NEXT_DLGCTL = 0x0028,
            SPOOLER_STATUS = 0x002A,
            DRAW_ITEM = 0x002B,
            MEASURE_ITEM = 0x002C,
            DELETE_ITEM = 0x002D,
            V_KEY_TO_ITEM = 0x002E,
            CHAR_TO_ITEM = 0x002F,
            SET_FONT = 0x0030,
            GET_FONT = 0x0031,
            SET_HOTKEY = 0x0032,
            GET_HOTKEY = 0x0033,
            QUERY_DRAG_ICON = 0x0037,
            COMPARE_ITEM = 0x0039,
            GET_OBJECT = 0x003D,
            COMPACTING = 0x0041,
            COMM_NOTIFY = 0x0044,
            WINDOW_POS_CHANGING = 0x0046,
            WINDOW_POS_CHANGED = 0x0047,
            POWER = 0x0048,
            COPY_DATA = 0x004A,
            CANCEL_JOURNAL = 0x004B,
            NOTIFY = 0x004E,
            INPUT_LANG_CHANGE_REQUEST = 0x0050,
            INPUT_LANG_CHANGE = 0x0051,
            T_CARD = 0x0052,
            HELP = 0x0053,
            USER_CHANGED = 0x0054,
            NOTIFY_FORMAT = 0x0055,
            CONTEX_TMENU = 0x007B,
            STYLE_CHANGING = 0x007C,
            STYLE_CHANGED = 0x007D,
            DISPLAY_CHANGE = 0x007E,
            GET_ICON = 0x007F,
            SET_ICON = 0x0080,
            NC_CREATE = 0x0081,
            NC_DESTROY = 0x0082,
            NC_CALC_SIZE = 0x0083,
            NC_HIT_TEST = 0x0084,
            NC_PAINT = 0x0085,
            NC_ACTIVATE = 0x0086,
            GET_DLG_CODE = 0x0087,
            SYNC_PAINT = 0x0088,
            NC_MOUSE_MOVE = 0x00A0,
            NCL_BUTTON_DOWN = 0x00A1,
            NCL_BUTTON_UP = 0x00A2,
            NCL_BUTTON_DBL_CLK = 0x00A3,
            NCR_BUTTON_DOWN = 0x00A4,
            NCR_BUTTON_UP = 0x00A5,
            NCR_BUTTON_DBL_CLK = 0x00A6,
            NCM_BUTTON_DOWN = 0x00A7,
            NCM_BUTTON_UP = 0x00A8,
            NCM_BUTTON_DBL_CLK = 0x00A9,
            NCX_BUTTON_DOWN = 0x00AB,
            NCX_BUTTON_UP = 0x00AC,
            NCX_BUTTON_DBL_CLK = 0x00AD,
            INPUT_DEVICE_CHANGE = 0x00FE,
            INPUT = 0x00FF,
            KEY_FIRST = 0x0100,
            KEY_DOWN = 0x0100,
            KEY_UP = 0x0101,
            CHAR = 0x0102,
            DEAD_CHAR = 0x0103,
            SYS_KEY_DOWN = 0x0104,
            SYS_KEY_UP = 0x0105,
            SYS_CHAR = 0x0106,
            SYS_DEAD_CHAR = 0x0107,
            UNICHAR = 0x0109,
            KEYLAST = 0x0109,
            IME_START_COMPOSITION = 0x010D,
            IME_END_COMPOSITION = 0x010E,
            IME_COMPOSITION = 0x010F,
            IME_KEYLAST = 0x010F,
            INIT_DIALOG = 0x0110,
            COMMAND = 0x0111,
            SYS_COMMAND = 0x0112,
            TIMER = 0x0113,
            H_SCROLL = 0x0114,
            V_SCROLL = 0x0115,
            INIT_MENU = 0x0116,
            INIT_MENU_POPUP = 0x0117,
            MENU_SELECT = 0x011F,
            MENU_CHAR = 0x0120,
            ENTER_IDLE = 0x0121,
            MENU_R_BUTTON_UP = 0x0122,
            MENU_DRAG = 0x0123,
            MENU_GET_OBJECT = 0x0124,
            UNINIT_MENU_POPUP = 0x0125,
            MENU_COMMAND = 0x0126,
            CHANGE_UI_STATE = 0x0127,
            UPDATE_UI_STATE = 0x0128,
            QUERY_UI_STATE = 0x0129,
            CTL_COLOR_MSGBOX = 0x0132,
            CTL_COLOR_EDIT = 0x0133,
            CTL_COLOR_LISTBOX = 0x0134,
            CTL_COLOR_BTN = 0x0135,
            CTL_COLOR_DLG = 0x0136,
            CTL_COLOR_SCROLLBAR = 0x0137,
            CTL_COLOR_STATIC = 0x0138,
            MOUSE_FIRST = 0x0200,
            MOUSE_MOVE = 0x0200,
            L_BUTTON_DOWN = 0x0201,
            L_BUTTON_UP = 0x0202,
            L_BUTTON_DBL_CLK = 0x0203,
            R_BUTTON_DOWN = 0x0204,
            R_BUTTON_UP = 0x0205,
            R_BUTTON_DBL_CLK = 0x0206,
            M_BUTTON_DOWN = 0x0207,
            M_BUTTON_UP = 0x0208,
            M_BUTTON_DBL_CLK = 0x0209,
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
            MOUSE_HOVER = 0x02A1,
            MOUSE_LEAVE = 0x02A3,
            NC_MOUSE_HOVER = 0x02A0,
            NC_MOUSE_LEAVE = 0x02A2,
            WTS_SESSION_CHANGE = 0x02B1,
            TABLET_FIRST = 0x02c0,
            TABLET_LAST = 0x02df,
            CUT = 0x0300,
            COPY = 0x0301,
            PASTE = 0x0302,
            CLEAR = 0x0303,
            UNDO = 0x0304,
            RENDER_FORMAT = 0x0305,
            RENDER_ALL_FORMATS = 0x0306,
            DESTROY_CLIPBOARD = 0x0307,
            DRAW_CLIPBOARD = 0x0308,
            PAINT_CLIPBOARD = 0x0309,
            V_SCROLL_CLIPBOARD = 0x030A,
            SIZE_CLIPBOARD = 0x030B,
            ASK_CB_FORMATNAME = 0x030C,
            CHANGE_CB_CHAIN = 0x030D,
            HS_CROLL_CLIPBOARD = 0x030E,
            QUERY_NEW_PALETTE = 0x030F,
            PALETREIS_CHANGING = 0x0310,
            PALERTE_CHANGED = 0x0311,
            HOTKEY = 0x0312,
            PRINT = 0x0317,
            PRINT_CLIENT = 0x0318,
            APP_COMMAND = 0x0319,
            THEME_CHANGED = 0x031A,
            CLIPBOARD_UPDATE = 0x031D,
            DWM_COMPOSITION_CHANGED = 0x031E,
            DWM_NCR_CHANGED = 0x031F,
            DWM_COLORIZATION_COLOR_CHANGED = 0x0320,
            DWM_WINDOW_MAXIMIZED_CHANGE = 0x0321,
            GET_TITLE_BAR_INFO_EX = 0x033F,
            HANDHELD_FIRST = 0x0358,
            HANDHELD_LAST = 0x035F,
            AFX_FIRST = 0x0360,
            AFX_LAST = 0x037F,
            PEN_WIN_FIRST = 0x0380,
            PEN_WIN_LAST = 0x038F,
            APP = 0x8000,
            USER = 0x0400,
            CPL_LAUNCH = USER + 0x1000,
            CPL_LAUNCHED = USER + 0x1001,
            SYS_TIMER = 0x118,
            HSHELL_ACCESS_IBILITY_STATE = 11,
            HSHELL_ACTIVATE_SHELL_WINDOW = 3,
            HSHELL_APP_COMMAND = 12,
            HSHELL_GET_MIN_RECT = 5,
            HSHELL_LANGUAGE = 8,
            HSHELL_REDRAW = 6,
            HSHELL_TASKMAN = 7,
            HSHELL_WINDOW_CREATED = 1,
            HSHELL_WINDOW_DESTROYED = 2,
            HSHELL_WINDOW_ACTIVATED = 4,
            HSHELL_WINDOW_REPLACED = 13
        }

        [Flags]
        public enum FirmwareTableType : uint
        {
            Acpi = 0x41435049,
            Firm = 0x4649524D,
            Rsmb = 0x52534D42,
        }

        [Flags]
        public enum KEYEVENTF : int
        {
            KEYEVENTF_EXTENDEDKEY = 1,
            KEYEVENTF_KEYUP = 2
        }

        [Flags]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        [Flags]
        public enum DWM_WINDOW_CORNER_PREFERENCE : int
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        [Flags]
        public enum PROCESS_DPI_AWARENESS : int
        {
            PROCESS_DPI_UNAWARE = 0,
            PROCESS_SYSTEM_DPI_AWARE = 1,
            PROCESS_PER_MONITOR_DPI_Aware = 2
        }

        [Flags]
        public enum HResult : uint
        {
            S_OK = 0x00000000,
            E_ABORT = 0x80004004,
            E_ACCESS_DENIED = 0x80070005,
            E_FAIL = 0x80004005,
            E_HANDLE = 0x80070006,
            E_INVALID_ARG = 0x80070057,
            E_NO_INTERFACE = 0x80004002,
            E_NO_TIMPL = 0x80004001,
            E_OUT_OF_MEMORY = 0x8007000E,
            E_POINTER = 0x80004003,
            E_UNEXPECTED = 0x8000FFFF,
        }

        [Flags]
        public enum SHERB_RECYCLE : int
        {
            SHERB_NO_CONFIRMATION = 0x00000001,
            SHERB_NO_PROGRESS_UI = 0x00000001,
            SHERB_NO_SOUND = 0x00000004
        }

        [Flags]
        public enum E_DATA_FLOW
        {
            eRender,
            eCapture,
            eAll,
            E_DATA_FLOW_enum_count
        }

        [Flags]
        public enum E_ROLE
        {
            eConsole,
            eMultimedia,
            eCommunications,
            E_ROLE_enum_count
        }

        [Flags]
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
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

using static VitNX3.Functions.Win32.Constants;
using static VitNX3.Functions.Win32.Enums;

using Monitor = VitNX3.Functions.WinControllers.Monitor;

namespace VitNX3.Functions.Win32
{
    /// <summary>
    /// Import the Windows System functions from native DLL.
    /// About functions: https://www.pinvoke.net
    /// </summary>
    public class Import
    {
        [DllImport("kernel32.dll",
        SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        public static byte[] GetDataFromResource(IntPtr module,
            IntPtr type,
            IntPtr name)
        {
            IntPtr resourceInfo = FindResource(module,
                name,
                type);
            if (resourceInfo == IntPtr.Zero) throw new Win32Exception();
            IntPtr resourceData = LoadResource(module,
                resourceInfo);
            if (resourceData == IntPtr.Zero) throw new Win32Exception();
            IntPtr resourceLock = LockResource(resourceData);
            if (resourceLock == IntPtr.Zero) throw new Win32Exception();
            uint size = SizeofResource(module,
                resourceInfo);
            if (size == 0) throw new Win32Exception();
            byte[] buffer = new byte[size];
            Marshal.Copy(resourceLock,
                buffer, 0,
                buffer.Length);
            return buffer;
        }

        [UnmanagedFunctionPointer(CallingConvention.Winapi,
        SetLastError = true,
        CharSet = CharSet.Unicode)]
        public delegate bool EnumResourceNamesCallback(IntPtr module,
        IntPtr lpszType,
        IntPtr lpszName,
        IntPtr lParam);

        [DllImport("kernel32.dll",
            SetLastError = true,
            CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibraryEx(string fileName,
            IntPtr file,
            uint flags);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr module);

        [DllImport("kernel32.dll",
            SetLastError = true,
            CharSet = CharSet.Unicode)]
        public static extern bool EnumResourceNames(IntPtr module,
            IntPtr type,
            EnumResourceNamesCallback callback,
            IntPtr lParam);

        [DllImport("kernel32.dll",
            SetLastError = true,
            CharSet = CharSet.Unicode)]
        public static extern IntPtr FindResource(IntPtr module,
            IntPtr name,
            IntPtr type);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern IntPtr LoadResource(IntPtr module,
            IntPtr resourceInfo);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern IntPtr LockResource(IntPtr resourceData);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern uint SizeofResource(IntPtr module,
            IntPtr resourceInfo);

        [DllImport("ntdll.dll")]
        public static extern uint RtlGetCompressionWorkSpaceSize(ushort compressionFormat,
            out uint workSpaceSize,
            out uint fragmentWorkSpaceSize);

        [DllImport("ntdll.dll")]
        public static extern uint RtlCompressBuffer(ushort compressionFormat,
            byte[] buffer,
            int bufferSize,
            byte[] compressedBuffer,
            int compressedBufferSize,
            uint chunkSize,
            out int finalCompressedSize,
            IntPtr workSpace);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern IntPtr LocalAlloc(int flags,
            IntPtr size);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr buffer);

        [DllImport("shell32.dll")]
        public static extern int SHGetStockIconInfo(SHSTOCKICONID siid,
            SHSTOCKICONFLAGS uFlags,
            ref SHSTOCKICONINFO info);

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr handle);

        [DllImport("kernel32.dll")]
        public static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string path);

        [DllImport("user32.dll")]
        public static extern uint ActivateKeyboardLayout(IntPtr hkl,
            uint flags);

        [DllImport("user32.dll",
            CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle,
            IntPtr childAfter,
            string lclassName,
            string windowTitle);

        [DllImport("user32.dll",
            CharSet = CharSet.Unicode)]
        public static extern IntPtr PostMessage(IntPtr hWnd,
            int Msg,
            IntPtr wParam,
            IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool AllowSetForegroundWindow(int dwProcessId);

        [DllImport("user32.dll")]
        public static extern int GetDpiForWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool AdjustWindowRect(ref RECT lpRect,
            uint dwStyle,
            bool bMenu);

        [DllImport("user32.dll")]
        public static extern bool AdjustWindowRectExForDpi(ref RECT lpRect,
            uint dwStyle,
            bool bMenu,
            uint dwExStyle,
            uint dpi);

        [DllImport("user32.dll",
            EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLong32(IntPtr hWnd,
            int nIndex);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowLongPtr(IntPtr hWnd,
            int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern IntPtr SetWindowLong32(IntPtr hWnd,
            int nIndex,
            uint dwNewLong);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowLongPtr(IntPtr hWnd,
            int nIndex,
            uint dwNewLong);

        [DllImport("MediaInfo.dll")]
        public static extern IntPtr MediaInfo_New();

        [DllImport("MediaInfo.dll",
            CharSet = CharSet.Unicode)]
        public static extern int MediaInfo_Open(IntPtr handle,
            string path);

        [DllImport("MediaInfo.dll",
            CharSet = CharSet.Unicode)]
        public static extern IntPtr MediaInfo_Option(IntPtr handle,
            string option,
            string value);

        [DllImport("MediaInfo.dll")]
        public static extern IntPtr MediaInfo_Inform(IntPtr handle,
            int reserved);

        [DllImport("MediaInfo.dll")]
        public static extern int MediaInfo_Close(IntPtr handle);

        [DllImport("MediaInfo.dll")]
        public static extern void MediaInfo_Delete(IntPtr handle);

        [DllImport("MediaInfo.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr MediaInfo_Get(IntPtr handle,
            MEDIAINFOSTREAMKIND kind,
            int stream,
            string parameter,
            MEDIAINFOKIND infoKind,
            MEDIAINFOKIND searchKind);

        [DllImport("MediaInfo.dll",
            CharSet = CharSet.Unicode)]
        public static extern int MediaInfo_Count_Get(IntPtr handle,
            MEDIAINFOSTREAMKIND streamKind,
            int stream);

        [DllImport("advapi32.dll")]
        public static extern bool InitiateSystemShutdown(string lpMachinename,
        string lpMessage,
        int dwTimeout,
        bool bForceAppsClosed,
        bool bRebootAfterShutdown);

        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();

        [DllImport("kernel32.dll")]
        public static extern uint FormatMessage(uint Flags,
            IntPtr Source,
            uint MessageID,
            uint LanguageID,
            StringBuilder Buffer,
            uint Size,
            IntPtr Args);

        [DllImport("advapi32.dll")]
        public static extern int LogonUser(string lpszUserName,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        [DllImport("advapi32.dll",
            SetLastError = true)]
        public static extern int DuplicateToken(IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll",
            SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("user32.dll",
            SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern int SetWindowCompositionAttribute(IntPtr hWnd,
            ref WINCOMPATTRDATA data);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool SetForegroundWindow(HandleRef hWnd);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern bool PostMessage(HandleRef hWnd,
            WINDOW_MESSAGE msg,
            IntPtr wParam,
            IntPtr lParam);

        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern bool ShutdownBlockReasonCreate(IntPtr hWnd,
            [MarshalAs(UnmanagedType.LPWStr)]
        string reason);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("ntdll.dll",
            SetLastError = true)]
        public static extern void RtlSetProcessIsCritical(uint v1,
            uint v2,
            uint v3);

        [DllImport("user32.dll",
            SetLastError = true)]
        public static extern bool ShutdownBlockReasonDestroy(IntPtr hWnd);

        [DllImport("kernel32.dll")]
        public static extern bool SetProcessShutdownParameters(uint dwLevel,
            uint dwFlags);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam);

        [DllImport("user32.dll",
            CharSet = CharSet.Auto)]
        public static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32.dll")]
        public static extern bool AdjustWindowRectEx(ref RECT lpRect,
            int dwStyle,
            bool bMenu,
            int dwExStyle);

        [DllImport("kernel32.dll",
            SetLastError = true)]
        public static extern bool GetVolumeNameForVolumeMountPoint(string lpszVolumeMountPoint,
                [Out] StringBuilder lpszVolumeName,
                int cchBufferLength);

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
        public static extern uint EnumSystemFirmwareTables(FirmwareTableType FirmwareTableProviderSignature,
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

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool DeviceIoControl(IntPtr hDevice,
        uint dwIoControlCode,
        IntPtr lpInBuffer,
        uint nInBufferSize,
        IntPtr lpOutBuffer,
        uint nOutBufferSize,
        out uint lpBytesReturned,
        IntPtr lpOverlapped);

        [DllImport("kernel32.dll")]
        public static extern bool QueryPerformanceCounter(ref long nPfCt);

        [DllImport("kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(ref long nPfFreq);

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
        public static extern bool GetWindowInfo(IntPtr hwnd,
            ref WINDOW_INFO pwi);

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
        public static extern long GetKeyboardLayoutName(StringBuilder pwszKLID);

        [DllImport("mpr.dll")]
        public static extern uint WNetAddConnection3(IntPtr hwndOwner,
            ref NET_RESOURCE lpNetResource,
            string lpPassword,
            string lpUserName,
            uint dwFlags);

        [DllImport("mpr.dll")]
        public static extern uint WNetCancelConnection2(string lpName,
            uint dwFlags,
            uint fForce);

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
        public static extern long LoadKeyboardLayout(string pwszKLID,
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
        public static extern IntPtr SetTimer(IntPtr hWnd,
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
        public static extern int NtQueryTimerResolution(out int MinimumResolution,
        out int MaximumResolution,
        out int CurrentResolution);

        [DllImport("ntdll.dll",
            SetLastError = true)]
        public static extern int NtSetTimerResolution(int DesiredResolution,
            bool SetResolution,
            out int CurrentResolution);

        [DllImport("ntdll.dll",
            SetLastError = true)]
        public static extern unsafe int NtDelayExecution(bool alertable,
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
            SW_SH value);

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
        public const int WM_QUERY_END_SESSION = 0x0011;
        public const int WM_END_SESSION = 0x0016;
        public const uint SHUTDOWN_NO_RETRY = 0x00000001;

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

        public class ResultWin32
        {
            public static string GetErrorName(int result)
            {
                FieldInfo[] fields = typeof(ResultWin32).GetFields();
                foreach (FieldInfo fi in fields)
                    if ((int)fi.GetValue(null) == result)
                        return fi.Name;
                return string.Empty;
            }

            public const int ERROR_SUCCESS = 0;
            public const int ERROR_INVALID_FUNCTION = 1;
            public const int ERROR_FILE_NOT_FOUND = 2;
            public const int ERROR_PATH_NOT_FOUND = 3;
            public const int ERROR_TOO_MANY_OPEN_FILES = 4;
            public const int ERROR_ACCESS_DENIED = 5;
            public const int ERROR_INVALID_HANDLE = 6;
            public const int ERROR_ARENA_TRASHED = 7;
            public const int ERROR_NOT_ENOUGH_MEMORY = 8;
            public const int ERROR_INVALID_BLOCK = 9;
            public const int ERROR_BAD_ENVIRONMENT = 10;
            public const int ERROR_BAD_FORMAT = 11;
            public const int ERROR_INVALID_ACCESS = 12;
            public const int ERROR_INVALID_DATA = 13;
            public const int ERROR_OUTOFMEMORY = 14;
            public const int ERROR_INVALID_DRIVE = 15;
            public const int ERROR_CURRENT_DIRECTORY = 16;
            public const int ERROR_NOT_SAME_DEVICE = 17;
            public const int ERROR_NO_MORE_FILES = 18;
            public const int ERROR_WRITE_PROTECT = 19;
            public const int ERROR_BAD_UNIT = 20;
            public const int ERROR_NOT_READY = 21;
            public const int ERROR_BAD_COMMAND = 22;
            public const int ERROR_CRC = 23;
            public const int ERROR_BAD_LENGTH = 24;
            public const int ERROR_SEEK = 25;
            public const int ERROR_NOT_DOS_DISK = 26;
            public const int ERROR_SECTOR_NOT_FOUND = 27;
            public const int ERROR_OUT_OF_PAPER = 28;
            public const int ERROR_WRITE_FAULT = 29;
            public const int ERROR_READ_FAULT = 30;
            public const int ERROR_GEN_FAILURE = 31;
            public const int ERROR_SHARING_VIOLATION = 32;
            public const int ERROR_LOCK_VIOLATION = 33;
            public const int ERROR_WRONG_DISK = 34;
            public const int ERROR_SHARING_BUFFER_EXCEEDED = 36;
            public const int ERROR_HANDLE_EOF = 38;
            public const int ERROR_HANDLE_DISK_FULL = 39;
            public const int ERROR_NOT_SUPPORTED = 50;
            public const int ERROR_REM_NOT_LIST = 51;
            public const int ERROR_DUP_NAME = 52;
            public const int ERROR_BAD_NETPATH = 53;
            public const int ERROR_NETWORK_BUSY = 54;
            public const int ERROR_DEV_NOT_EXIST = 55;
            public const int ERROR_TOO_MANY_CMDS = 56;
            public const int ERROR_ADAP_HDW_ERR = 57;
            public const int ERROR_BAD_NET_RESP = 58;
            public const int ERROR_UNEXP_NET_ERR = 59;
            public const int ERROR_BAD_REM_ADAP = 60;
            public const int ERROR_PRINTQ_FULL = 61;
            public const int ERROR_NO_SPOOL_SPACE = 62;
            public const int ERROR_PRINT_CANCELLED = 63;
            public const int ERROR_NETNAME_DELETED = 64;
            public const int ERROR_NETWORK_ACCESS_DENIED = 65;
            public const int ERROR_BAD_DEV_TYPE = 66;
            public const int ERROR_BAD_NET_NAME = 67;
            public const int ERROR_TOO_MANY_NAMES = 68;
            public const int ERROR_TOO_MANY_SESS = 69;
            public const int ERROR_SHARING_PAUSED = 70;
            public const int ERROR_REQ_NOT_ACCEP = 71;
            public const int ERROR_REDIR_PAUSED = 72;
            public const int ERROR_FILE_EXISTS = 80;
            public const int ERROR_CANNOT_MAKE = 82;
            public const int ERROR_FAIL_I24 = 83;
            public const int ERROR_OUT_OF_STRUCTURES = 84;
            public const int ERROR_ALREADY_ASSIGNED = 85;
            public const int ERROR_INVALID_PASSWORD = 86;
            public const int ERROR_INVALID_PARAMETER = 87;
            public const int ERROR_NET_WRITE_FAULT = 88;
            public const int ERROR_NO_PROC_SLOTS = 89;
            public const int ERROR_TOO_MANY_SEMAPHORES = 100;
            public const int ERROR_EXCL_SEM_ALREADY_OWNED = 101;
            public const int ERROR_SEM_IS_SET = 102;
            public const int ERROR_TOO_MANY_SEM_REQUESTS = 103;
            public const int ERROR_INVALID_AT_INTERRUPT_TIME = 104;
            public const int ERROR_SEM_OWNER_DIED = 105;
            public const int ERROR_SEM_USER_LIMIT = 106;
            public const int ERROR_DISK_CHANGE = 107;
            public const int ERROR_DRIVE_LOCKED = 108;
            public const int ERROR_BROKEN_PIPE = 109;
            public const int ERROR_OPEN_FAILED = 110;
            public const int ERROR_BUFFER_OVERFLOW = 111;
            public const int ERROR_DISK_FULL = 112;
            public const int ERROR_NO_MORE_SEARCH_HANDLES = 113;
            public const int ERROR_INVALID_TARGET_HANDLE = 114;
            public const int ERROR_INVALID_CATEGORY = 117;
            public const int ERROR_INVALID_VERIFY_SWITCH = 118;
            public const int ERROR_BAD_DRIVER_LEVEL = 119;
            public const int ERROR_CALL_NOT_IMPLEMENTED = 120;
            public const int ERROR_SEM_TIMEOUT = 121;
            public const int ERROR_INSUFFICIENT_BUFFER = 122;
            public const int ERROR_INVALID_NAME = 123;
            public const int ERROR_INVALID_LEVEL = 124;
            public const int ERROR_NO_VOLUME_LABEL = 125;
            public const int ERROR_MOD_NOT_FOUND = 126;
            public const int ERROR_PROC_NOT_FOUND = 127;
            public const int ERROR_WAIT_NO_CHILDREN = 128;
            public const int ERROR_CHILD_NOT_COMPLETE = 129;
            public const int ERROR_DIRECT_ACCESS_HANDLE = 130;
            public const int ERROR_NEGATIVE_SEEK = 131;
            public const int ERROR_SEEK_ON_DEVICE = 132;
            public const int ERROR_IS_JOIN_TARGET = 133;
            public const int ERROR_IS_JOINED = 134;
            public const int ERROR_IS_SUBSTED = 135;
            public const int ERROR_NOT_JOINED = 136;
            public const int ERROR_NOT_SUBSTED = 137;
            public const int ERROR_JOIN_TO_JOIN = 138;
            public const int ERROR_SUBST_TO_SUBST = 139;
            public const int ERROR_JOIN_TO_SUBST = 140;
            public const int ERROR_SUBST_TO_JOIN = 141;
            public const int ERROR_BUSY_DRIVE = 142;
            public const int ERROR_SAME_DRIVE = 143;
            public const int ERROR_DIR_NOT_ROOT = 144;
            public const int ERROR_DIR_NOT_EMPTY = 145;
            public const int ERROR_IS_SUBST_PATH = 146;
            public const int ERROR_IS_JOIN_PATH = 147;
            public const int ERROR_PATH_BUSY = 148;
            public const int ERROR_IS_SUBST_TARGET = 149;
            public const int ERROR_SYSTEM_TRACE = 150;
            public const int ERROR_INVALID_EVENT_COUNT = 151;
            public const int ERROR_TOO_MANY_MUXWAITERS = 152;
            public const int ERROR_INVALID_LIST_FORMAT = 153;
            public const int ERROR_LABEL_TOO_Int32 = 154;
            public const int ERROR_TOO_MANY_TCBS = 155;
            public const int ERROR_SIGNAL_REFUSED = 156;
            public const int ERROR_DISCARDED = 157;
            public const int ERROR_NOT_LOCKED = 158;
            public const int ERROR_BAD_THREADID_ADDR = 159;
            public const int ERROR_BAD_ARGUMENTS = 160;
            public const int ERROR_BAD_PATHNAME = 161;
            public const int ERROR_SIGNAL_PENDING = 162;
            public const int ERROR_MAX_THRDS_REACHED = 164;
            public const int ERROR_LOCK_FAILED = 167;
            public const int ERROR_BUSY = 170;
            public const int ERROR_CANCEL_VIOLATION = 173;
            public const int ERROR_ATOMIC_LOCKS_NOT_SUPPORTED = 174;
            public const int ERROR_INVALID_SEGMENT_NUMBER = 180;
            public const int ERROR_INVALID_ORDINAL = 182;
            public const int ERROR_ALREADY_EXISTS = 183;
            public const int ERROR_INVALID_FLAG_NUMBER = 186;
            public const int ERROR_SEM_NOT_FOUND = 187;
            public const int ERROR_INVALID_STARTING_CODESEG = 188;
            public const int ERROR_INVALID_STACKSEG = 189;
            public const int ERROR_INVALID_MODULETYPE = 190;
            public const int ERROR_INVALID_EXE_SIGNATURE = 191;
            public const int ERROR_EXE_MARKED_INVALID = 192;
            public const int ERROR_BAD_EXE_FORMAT = 193;
            public const int ERROR_ITERATED_DATA_EXCEEDS_64k = 194;
            public const int ERROR_INVALID_MINALLOCSIZE = 195;
            public const int ERROR_DYNLINK_FROM_INVALID_RING = 196;
            public const int ERROR_IOPL_NOT_ENABLED = 197;
            public const int ERROR_INVALID_SEGDPL = 198;
            public const int ERROR_AUTODATASEG_EXCEEDS_64k = 199;
            public const int ERROR_RING2SEG_MUST_BE_MOVABLE = 200;
            public const int ERROR_RELOC_CHAIN_XEEDS_SEGLIM = 201;
            public const int ERROR_INFLOOP_IN_RELOC_CHAIN = 202;
            public const int ERROR_ENVVAR_NOT_FOUND = 203;
            public const int ERROR_NO_SIGNAL_SENT = 205;
            public const int ERROR_FILENAME_EXCED_RANGE = 206;
            public const int ERROR_RING2_STACK_IN_USE = 207;
            public const int ERROR_META_EXPANSION_TOO_Int32 = 208;
            public const int ERROR_INVALID_SIGNAL_NUMBER = 209;
            public const int ERROR_THREAD_1_INACTIVE = 210;
            public const int ERROR_LOCKED = 212;
            public const int ERROR_TOO_MANY_MODULES = 214;
            public const int ERROR_NESTING_NOT_ALLOWED = 215;
            public const int ERROR_EXE_MACHINE_TYPE_MISMATCH = 216;
            public const int ERROR_EXE_CANNOT_MODIFY_SIGNED_BINARY = 217;
            public const int ERROR_EXE_CANNOT_MODIFY_STRONG_SIGNED_BINARY = 218;
            public const int ERROR_BAD_PIPE = 230;
            public const int ERROR_PIPE_BUSY = 231;
            public const int ERROR_NO_DATA = 232;
            public const int ERROR_PIPE_NOT_CONNECTED = 233;
            public const int ERROR_MORE_DATA = 234;
            public const int ERROR_VC_DISCONNECTED = 240;
            public const int ERROR_INVALID_EA_NAME = 254;
            public const int ERROR_EA_LIST_INCONSISTENT = 255;
            public const int WAIT_TIMEOUT = 258;
            public const int ERROR_NO_MORE_ITEMS = 259;
            public const int ERROR_CANNOT_COPY = 266;
            public const int ERROR_DIRECTORY = 267;
            public const int ERROR_EAS_DIDNT_FIT = 275;
            public const int ERROR_EA_FILE_CORRUPT = 276;
            public const int ERROR_EA_TABLE_FULL = 277;
            public const int ERROR_INVALID_EA_HANDLE = 278;
            public const int ERROR_EAS_NOT_SUPPORTED = 282;
            public const int ERROR_NOT_OWNER = 288;
            public const int ERROR_TOO_MANY_POSTS = 298;
            public const int ERROR_PARTIAL_COPY = 299;
            public const int ERROR_OPLOCK_NOT_GRANTED = 300;
            public const int ERROR_INVALID_OPLOCK_PROTOCOL = 301;
            public const int ERROR_DISK_TOO_FRAGMENTED = 302;
            public const int ERROR_DELETE_PENDING = 303;
            public const int ERROR_MR_MID_NOT_FOUND = 317;
            public const int ERROR_SCOPE_NOT_FOUND = 318;
            public const int ERROR_INVALID_ADDRESS = 487;
            public const int ERROR_ARITHMETIC_OVERFLOW = 534;
            public const int ERROR_PIPE_CONNECTED = 535;
            public const int ERROR_PIPE_LISTENING = 536;
            public const int ERROR_EA_ACCESS_DENIED = 994;
            public const int ERROR_OPERATION_ABORTED = 995;
            public const int ERROR_IO_INCOMPLETE = 996;
            public const int ERROR_IO_PENDING = 997;
            public const int ERROR_NOACCESS = 998;
            public const int ERROR_SWAPERROR = 999;
            public const int ERROR_STACK_OVERFLOW = 1001;
            public const int ERROR_INVALID_MESSAGE = 1002;
            public const int ERROR_CAN_NOT_COMPLETE = 1003;
            public const int ERROR_INVALID_FLAGS = 1004;
            public const int ERROR_UNRECOGNIZED_VOLUME = 1005;
            public const int ERROR_FILE_INVALID = 1006;
            public const int ERROR_FULLSCREEN_MODE = 1007;
            public const int ERROR_NO_TOKEN = 1008;
            public const int ERROR_BADDB = 1009;
            public const int ERROR_BADKEY = 1010;
            public const int ERROR_CANTOPEN = 1011;
            public const int ERROR_CANTREAD = 1012;
            public const int ERROR_CANTWRITE = 1013;
            public const int ERROR_REGISTRY_RECOVERED = 1014;
            public const int ERROR_REGISTRY_CORRUPT = 1015;
            public const int ERROR_REGISTRY_IO_FAILED = 1016;
            public const int ERROR_NOT_REGISTRY_FILE = 1017;
            public const int ERROR_KEY_DELETED = 1018;
            public const int ERROR_NO_LOG_SPACE = 1019;
            public const int ERROR_KEY_HAS_CHILDREN = 1020;
            public const int ERROR_CHILD_MUST_BE_VOLATILE = 1021;
            public const int ERROR_NOTIFY_ENUM_DIR = 1022;
            public const int ERROR_DEPENDENT_SERVICES_RUNNING = 1051;
            public const int ERROR_INVALID_SERVICE_CONTROL = 1052;
            public const int ERROR_SERVICE_REQUEST_TIMEOUT = 1053;
            public const int ERROR_SERVICE_NO_THREAD = 1054;
            public const int ERROR_SERVICE_DATABASE_LOCKED = 1055;
            public const int ERROR_SERVICE_ALREADY_RUNNING = 1056;
            public const int ERROR_INVALID_SERVICE_ACCOUNT = 1057;
            public const int ERROR_SERVICE_DISABLED = 1058;
            public const int ERROR_CIRCULAR_DEPENDENCY = 1059;
            public const int ERROR_SERVICE_DOES_NOT_EXIST = 1060;
            public const int ERROR_SERVICE_CANNOT_ACCEPT_CTRL = 1061;
            public const int ERROR_SERVICE_NOT_ACTIVE = 1062;
            public const int ERROR_FAILED_SERVICE_CONTROLLER_CONNECT = 1063;
            public const int ERROR_EXCEPTION_IN_SERVICE = 1064;
            public const int ERROR_DATABASE_DOES_NOT_EXIST = 1065;
            public const int ERROR_SERVICE_SPECIFIC_ERROR = 1066;
            public const int ERROR_PROCESS_ABORTED = 1067;
            public const int ERROR_SERVICE_DEPENDENCY_FAIL = 1068;
            public const int ERROR_SERVICE_LOGON_FAILED = 1069;
            public const int ERROR_SERVICE_START_HANG = 1070;
            public const int ERROR_INVALID_SERVICE_LOCK = 1071;
            public const int ERROR_SERVICE_MARKED_FOR_DELETE = 1072;
            public const int ERROR_SERVICE_EXISTS = 1073;
            public const int ERROR_ALREADY_RUNNING_LKG = 1074;
            public const int ERROR_SERVICE_DEPENDENCY_DELETED = 1075;
            public const int ERROR_BOOT_ALREADY_ACCEPTED = 1076;
            public const int ERROR_SERVICE_NEVER_STARTED = 1077;
            public const int ERROR_DUPLICATE_SERVICE_NAME = 1078;
            public const int ERROR_DIFFERENT_SERVICE_ACCOUNT = 1079;
            public const int ERROR_CANNOT_DETECT_DRIVER_FAILURE = 1080;
            public const int ERROR_CANNOT_DETECT_PROCESS_ABORT = 1081;
            public const int ERROR_NO_RECOVERY_PROGRAM = 1082;
            public const int ERROR_SERVICE_NOT_IN_EXE = 1083;
            public const int ERROR_NOT_SAFEBOOT_SERVICE = 1084;
            public const int ERROR_END_OF_MEDIA = 1100;
            public const int ERROR_FILEMARK_DETECTED = 1101;
            public const int ERROR_BEGINNING_OF_MEDIA = 1102;
            public const int ERROR_SETMARK_DETECTED = 1103;
            public const int ERROR_NO_DATA_DETECTED = 1104;
            public const int ERROR_PARTITION_FAILURE = 1105;
            public const int ERROR_INVALID_BLOCK_LENGTH = 1106;
            public const int ERROR_DEVICE_NOT_PARTITIONED = 1107;
            public const int ERROR_UNABLE_TO_LOCK_MEDIA = 1108;
            public const int ERROR_UNABLE_TO_UNLOAD_MEDIA = 1109;
            public const int ERROR_MEDIA_CHANGED = 1110;
            public const int ERROR_BUS_RESET = 1111;
            public const int ERROR_NO_MEDIA_IN_DRIVE = 1112;
            public const int ERROR_NO_UNICODE_TRANSLATION = 1113;
            public const int ERROR_DLL_INIT_FAILED = 1114;
            public const int ERROR_SHUTDOWN_IN_PROGRESS = 1115;
            public const int ERROR_NO_SHUTDOWN_IN_PROGRESS = 1116;
            public const int ERROR_IO_DEVICE = 1117;
            public const int ERROR_SERIAL_NO_DEVICE = 1118;
            public const int ERROR_IRQ_BUSY = 1119;
            public const int ERROR_MORE_WRITES = 1120;
            public const int ERROR_COUNTER_TIMEOUT = 1121;
            public const int ERROR_FLOPPY_ID_MARK_NOT_FOUND = 1122;
            public const int ERROR_FLOPPY_WRONG_CYLINDER = 1123;
            public const int ERROR_FLOPPY_UNKNOWN_ERROR = 1124;
            public const int ERROR_FLOPPY_BAD_REGISTERS = 1125;
            public const int ERROR_DISK_RECALIBRATE_FAILED = 1126;
            public const int ERROR_DISK_OPERATION_FAILED = 1127;
            public const int ERROR_DISK_RESET_FAILED = 1128;
            public const int ERROR_EOM_OVERFLOW = 1129;
            public const int ERROR_NOT_ENOUGH_SERVER_MEMORY = 1130;
            public const int ERROR_POSSIBLE_DEADLOCK = 1131;
            public const int ERROR_MAPPED_ALIGNMENT = 1132;
            public const int ERROR_SET_POWER_STATE_VETOED = 1140;
            public const int ERROR_SET_POWER_STATE_FAILED = 1141;
            public const int ERROR_TOO_MANY_LINKS = 1142;
            public const int ERROR_OLD_WIN_VERSION = 1150;
            public const int ERROR_APP_WRONG_OS = 1151;
            public const int ERROR_SINGLE_INSTANCE_APP = 1152;
            public const int ERROR_RMODE_APP = 1153;
            public const int ERROR_INVALID_DLL = 1154;
            public const int ERROR_NO_ASSOCIATION = 1155;
            public const int ERROR_DDE_FAIL = 1156;
            public const int ERROR_DLL_NOT_FOUND = 1157;
            public const int ERROR_NO_MORE_USER_HANDLES = 1158;
            public const int ERROR_MESSAGE_SYNC_ONLY = 1159;
            public const int ERROR_SOURCE_ELEMENT_EMPTY = 1160;
            public const int ERROR_DESTINATION_ELEMENT_FULL = 1161;
            public const int ERROR_ILLEGAL_ELEMENT_ADDRESS = 1162;
            public const int ERROR_MAGAZINE_NOT_PRESENT = 1163;
            public const int ERROR_DEVICE_REINITIALIZATION_NEEDED = 1164;
            public const int ERROR_DEVICE_REQUIRES_CLEANING = 1165;
            public const int ERROR_DEVICE_DOOR_OPEN = 1166;
            public const int ERROR_DEVICE_NOT_CONNECTED = 1167;
            public const int ERROR_NOT_FOUND = 1168;
            public const int ERROR_NO_MATCH = 1169;
            public const int ERROR_SET_NOT_FOUND = 1170;
            public const int ERROR_POINT_NOT_FOUND = 1171;
            public const int ERROR_NO_TRACKING_SERVICE = 1172;
            public const int ERROR_NO_VOLUME_ID = 1173;
            public const int ERROR_UNABLE_TO_REMOVE_REPLACED = 1175;
            public const int ERROR_UNABLE_TO_MOVE_REPLACEMENT = 1176;
            public const int ERROR_UNABLE_TO_MOVE_REPLACEMENT_2 = 1177;
            public const int ERROR_JOURNAL_DELETE_IN_PROGRESS = 1178;
            public const int ERROR_JOURNAL_NOT_ACTIVE = 1179;
            public const int ERROR_POTENTIAL_FILE_FOUND = 1180;
            public const int ERROR_JOURNAL_ENTRY_DELETED = 1181;
            public const int ERROR_BAD_DEVICE = 1200;
            public const int ERROR_CONNECTION_UNAVAIL = 1201;
            public const int ERROR_DEVICE_ALREADY_REMEMBERED = 1202;
            public const int ERROR_NO_NET_OR_BAD_PATH = 1203;
            public const int ERROR_BAD_PROVIDER = 1204;
            public const int ERROR_CANNOT_OPEN_PROFILE = 1205;
            public const int ERROR_BAD_PROFILE = 1206;
            public const int ERROR_NOT_CONTAINER = 1207;
            public const int ERROR_EXTENDED_ERROR = 1208;
            public const int ERROR_INVALID_GROUPNAME = 1209;
            public const int ERROR_INVALID_COMPUTERNAME = 1210;
            public const int ERROR_INVALID_EVENTNAME = 1211;
            public const int ERROR_INVALID_DOMAINNAME = 1212;
            public const int ERROR_INVALID_SERVICENAME = 1213;
            public const int ERROR_INVALID_NETNAME = 1214;
            public const int ERROR_INVALID_SHARENAME = 1215;
            public const int ERROR_INVALID_PASSUInt16NAME = 1216;
            public const int ERROR_INVALID_MESSAGENAME = 1217;
            public const int ERROR_INVALID_MESSAGEDEST = 1218;
            public const int ERROR_SESSION_CREDENTIAL_CONFLICT = 1219;
            public const int ERROR_REMOTE_SESSION_LIMIT_EXCEEDED = 1220;
            public const int ERROR_DUP_DOMAINNAME = 1221;
            public const int ERROR_NO_NETWORK = 1222;
            public const int ERROR_CANCELLED = 1223;
            public const int ERROR_USER_MAPPED_FILE = 1224;
            public const int ERROR_CONNECTION_REFUSED = 1225;
            public const int ERROR_GRACEFUL_DISCONNECT = 1226;
            public const int ERROR_ADDRESS_ALREADY_ASSOCIATED = 1227;
            public const int ERROR_ADDRESS_NOT_ASSOCIATED = 1228;
            public const int ERROR_CONNECTION_INVALID = 1229;
            public const int ERROR_CONNECTION_ACTIVE = 1230;
            public const int ERROR_NETWORK_UNREACHABLE = 1231;
            public const int ERROR_HOST_UNREACHABLE = 1232;
            public const int ERROR_PROTOCOL_UNREACHABLE = 1233;
            public const int ERROR_PORT_UNREACHABLE = 1234;
            public const int ERROR_REQUEST_ABORTED = 1235;
            public const int ERROR_CONNECTION_ABORTED = 1236;
            public const int ERROR_RETRY = 1237;
            public const int ERROR_CONNECTION_COUNT_LIMIT = 1238;
            public const int ERROR_LOGIN_TIME_RESTRICTION = 1239;
            public const int ERROR_LOGIN_WKSTA_RESTRICTION = 1240;
            public const int ERROR_INCORRECT_ADDRESS = 1241;
            public const int ERROR_ALREADY_REGISTERED = 1242;
            public const int ERROR_SERVICE_NOT_FOUND = 1243;
            public const int ERROR_NOT_AUTHENTICATED = 1244;
            public const int ERROR_NOT_LOGGED_ON = 1245;
            public const int ERROR_CONTINUE = 1246;
            public const int ERROR_ALREADY_INITIALIZED = 1247;
            public const int ERROR_NO_MORE_DEVICES = 1248;
            public const int ERROR_NO_SUCH_SITE = 1249;
            public const int ERROR_DOMAIN_CONTROLLER_EXISTS = 1250;
            public const int ERROR_ONLY_IF_CONNECTED = 1251;
            public const int ERROR_OVERRIDE_NOCHANGES = 1252;
            public const int ERROR_BAD_USER_PROFILE = 1253;
            public const int ERROR_NOT_SUPPORTED_ON_SBS = 1254;
            public const int ERROR_SERVER_SHUTDOWN_IN_PROGRESS = 1255;
            public const int ERROR_HOST_DOWN = 1256;
            public const int ERROR_NON_ACCOUNT_SID = 1257;
            public const int ERROR_NON_DOMAIN_SID = 1258;
            public const int ERROR_APPHELP_BLOCK = 1259;
            public const int ERROR_ACCESS_DISABLED_BY_POLICY = 1260;
            public const int ERROR_REG_NAT_CONSUMPTION = 1261;
            public const int ERROR_CSCSHARE_OFFLINE = 1262;
            public const int ERROR_PKINIT_FAILURE = 1263;
            public const int ERROR_SMARTCARD_SUBSYSTEM_FAILURE = 1264;
            public const int ERROR_DOWNGRADE_DETECTED = 1265;
            public const int ERROR_MACHINE_LOCKED = 1271;
            public const int ERROR_CALLBACK_SUPPLIED_INVALID_DATA = 1273;
            public const int ERROR_SYNC_FOREGROUND_REFRESH_REQUIRED = 1274;
            public const int ERROR_DRIVER_BLOCKED = 1275;
            public const int ERROR_INVALID_IMPORT_OF_NON_DLL = 1276;
            public const int ERROR_ACCESS_DISABLED_WEBBLADE = 1277;
            public const int ERROR_ACCESS_DISABLED_WEBBLADE_TAMPER = 1278;
            public const int ERROR_RECOVERY_FAILURE = 1279;
            public const int ERROR_ALREADY_FIBER = 1280;
            public const int ERROR_ALREADY_THREAD = 1281;
            public const int ERROR_STACK_BUFFER_OVERRUN = 1282;
            public const int ERROR_PARAMETER_QUOTA_EXCEEDED = 1283;
            public const int ERROR_DEBUGGER_INACTIVE = 1284;
            public const int ERROR_DELAY_LOAD_FAILED = 1285;
            public const int ERROR_VDM_DISALLOWED = 1286;
            public const int ERROR_NOT_ALL_ASSIGNED = 1300;
            public const int ERROR_SOME_NOT_MAPPED = 1301;
            public const int ERROR_NO_QUOTAS_FOR_ACCOUNT = 1302;
            public const int ERROR_LOCAL_USER_SESSION_KEY = 1303;
            public const int ERROR_NULL_LM_PASSUInt16 = 1304;
            public const int ERROR_UNKNOWN_REVISION = 1305;
            public const int ERROR_REVISION_MISMATCH = 1306;
            public const int ERROR_INVALID_OWNER = 1307;
            public const int ERROR_INVALID_PRIMARY_GROUP = 1308;
            public const int ERROR_NO_IMPERSONATION_TOKEN = 1309;
            public const int ERROR_CANT_DISABLE_MANDATORY = 1310;
            public const int ERROR_NO_LOGON_SERVERS = 1311;
            public const int ERROR_NO_SUCH_LOGON_SESSION = 1312;
            public const int ERROR_NO_SUCH_PRIVILEGE = 1313;
            public const int ERROR_PRIVILEGE_NOT_HELD = 1314;
            public const int ERROR_INVALID_ACCOUNT_NAME = 1315;
            public const int ERROR_USER_EXISTS = 1316;
            public const int ERROR_NO_SUCH_USER = 1317;
            public const int ERROR_GROUP_EXISTS = 1318;
            public const int ERROR_NO_SUCH_GROUP = 1319;
            public const int ERROR_MEMBER_IN_GROUP = 1320;
            public const int ERROR_MEMBER_NOT_IN_GROUP = 1321;
            public const int ERROR_LAST_ADMIN = 1322;
            public const int ERROR_WRONG_PASSWORD = 1323;
            public const int ERROR_ILL_FORMED_PASSWORD = 1324;
            public const int ERROR_PASSWORD_RESTRICTION = 1325;
            public const int ERROR_LOGON_FAILURE = 1326;
            public const int ERROR_ACCOUNT_RESTRICTION = 1327;
            public const int ERROR_INVALID_LOGON_HOURS = 1328;
            public const int ERROR_INVALID_WORKSTATION = 1329;
            public const int ERROR_PASSUInt16_EXPIRED = 1330;
            public const int ERROR_ACCOUNT_DISABLED = 1331;
            public const int ERROR_NONE_MAPPED = 1332;
            public const int ERROR_TOO_MANY_LUIDS_REQUESTED = 1333;
            public const int ERROR_LUIDS_EXHAUSTED = 1334;
            public const int ERROR_INVALID_SUB_AUTHORITY = 1335;
            public const int ERROR_INVALID_ACL = 1336;
            public const int ERROR_INVALID_SID = 1337;
            public const int ERROR_INVALID_SECURITY_DESCR = 1338;
            public const int ERROR_BAD_INHERITANCE_ACL = 1340;
            public const int ERROR_SERVER_DISABLED = 1341;
            public const int ERROR_SERVER_NOT_DISABLED = 1342;
            public const int ERROR_INVALID_ID_AUTHORITY = 1343;
            public const int ERROR_ALLOTTED_SPACE_EXCEEDED = 1344;
            public const int ERROR_INVALID_GROUP_ATTRIBUTES = 1345;
            public const int ERROR_BAD_IMPERSONATION_LEVEL = 1346;
            public const int ERROR_CANT_OPEN_ANONYMOUS = 1347;
            public const int ERROR_BAD_VALIDATION_CLASS = 1348;
            public const int ERROR_BAD_TOKEN_TYPE = 1349;
            public const int ERROR_NO_SECURITY_ON_OBJECT = 1350;
            public const int ERROR_CANT_ACCESS_DOMAIN_INFO = 1351;
            public const int ERROR_INVALID_SERVER_STATE = 1352;
            public const int ERROR_INVALID_DOMAIN_STATE = 1353;
            public const int ERROR_INVALID_DOMAIN_ROLE = 1354;
            public const int ERROR_NO_SUCH_DOMAIN = 1355;
            public const int ERROR_DOMAIN_EXISTS = 1356;
            public const int ERROR_DOMAIN_LIMIT_EXCEEDED = 1357;
            public const int ERROR_INTERNAL_DB_CORRUPTION = 1358;
            public const int ERROR_INTERNAL_ERROR = 1359;
            public const int ERROR_GENERIC_NOT_MAPPED = 1360;
            public const int ERROR_BAD_DESCRIPTOR_FORMAT = 1361;
            public const int ERROR_NOT_LOGON_PROCESS = 1362;
            public const int ERROR_LOGON_SESSION_EXISTS = 1363;
            public const int ERROR_NO_SUCH_PACKAGE = 1364;
            public const int ERROR_BAD_LOGON_SESSION_STATE = 1365;
            public const int ERROR_LOGON_SESSION_COLLISION = 1366;
            public const int ERROR_INVALID_LOGON_TYPE = 1367;
            public const int ERROR_CANNOT_IMPERSONATE = 1368;
            public const int ERROR_RXACT_INVALID_STATE = 1369;
            public const int ERROR_RXACT_COMMIT_FAILURE = 1370;
            public const int ERROR_SPECIAL_ACCOUNT = 1371;
            public const int ERROR_SPECIAL_GROUP = 1372;
            public const int ERROR_SPECIAL_USER = 1373;
            public const int ERROR_MEMBERS_PRIMARY_GROUP = 1374;
            public const int ERROR_TOKEN_ALREADY_IN_USE = 1375;
            public const int ERROR_NO_SUCH_ALIAS = 1376;
            public const int ERROR_MEMBER_NOT_IN_ALIAS = 1377;
            public const int ERROR_MEMBER_IN_ALIAS = 1378;
            public const int ERROR_ALIAS_EXISTS = 1379;
            public const int ERROR_LOGON_NOT_GRANTED = 1380;
            public const int ERROR_TOO_MANY_SECRETS = 1381;
            public const int ERROR_SECRET_TOO_Int32 = 1382;
            public const int ERROR_INTERNAL_DB_ERROR = 1383;
            public const int ERROR_TOO_MANY_CONTEXT_IDS = 1384;
            public const int ERROR_LOGON_TYPE_NOT_GRANTED = 1385;
            public const int ERROR_NT_CROSS_ENCRYPTION_REQUIRED = 1386;
            public const int ERROR_NO_SUCH_MEMBER = 1387;
            public const int ERROR_INVALID_MEMBER = 1388;
            public const int ERROR_TOO_MANY_SIDS = 1389;
            public const int ERROR_LM_CROSS_ENCRYPTION_REQUIRED = 1390;
            public const int ERROR_NO_INHERITANCE = 1391;
            public const int ERROR_FILE_CORRUPT = 1392;
            public const int ERROR_DISK_CORRUPT = 1393;
            public const int ERROR_NO_USER_SESSION_KEY = 1394;
            public const int ERROR_LICENSE_QUOTA_EXCEEDED = 1395;
            public const int ERROR_WRONG_TARGET_NAME = 1396;
            public const int ERROR_MUTUAL_AUTH_FAILED = 1397;
            public const int ERROR_TIME_SKEW = 1398;
            public const int ERROR_CURRENT_DOMAIN_NOT_ALLOWED = 1399;
            public const int ERROR_INVALID_WINDOW_HANDLE = 1400;
            public const int ERROR_INVALID_MENU_HANDLE = 1401;
            public const int ERROR_INVALID_CURSOR_HANDLE = 1402;
            public const int ERROR_INVALID_ACCEL_HANDLE = 1403;
            public const int ERROR_INVALID_HOOK_HANDLE = 1404;
            public const int ERROR_INVALID_DWP_HANDLE = 1405;
            public const int ERROR_TLW_WITH_WSCHILD = 1406;
            public const int ERROR_CANNOT_FIND_WND_CLASS = 1407;
            public const int ERROR_WINDOW_OF_OTHER_THREAD = 1408;
            public const int ERROR_HOTKEY_ALREADY_REGISTERED = 1409;
            public const int ERROR_CLASS_ALREADY_EXISTS = 1410;
            public const int ERROR_CLASS_DOES_NOT_EXIST = 1411;
            public const int ERROR_CLASS_HAS_WINDOWS = 1412;
            public const int ERROR_INVALID_INDEX = 1413;
            public const int ERROR_INVALID_ICON_HANDLE = 1414;
            public const int ERROR_PRIVATE_DIALOG_INDEX = 1415;
            public const int ERROR_LISTBOX_ID_NOT_FOUND = 1416;
            public const int ERROR_NO_WILDCARD_CHARACTERS = 1417;
            public const int ERROR_CLIPBOARD_NOT_OPEN = 1418;
            public const int ERROR_HOTKEY_NOT_REGISTERED = 1419;
            public const int ERROR_WINDOW_NOT_DIALOG = 1420;
            public const int ERROR_CONTROL_ID_NOT_FOUND = 1421;
            public const int ERROR_INVALID_COMBOBOX_MESSAGE = 1422;
            public const int ERROR_WINDOW_NOT_COMBOBOX = 1423;
            public const int ERROR_INVALID_EDIT_HEIGHT = 1424;
            public const int ERROR_DC_NOT_FOUND = 1425;
            public const int ERROR_INVALID_HOOK_FILTER = 1426;
            public const int ERROR_INVALID_FILTER_PROC = 1427;
            public const int ERROR_HOOK_NEEDS_HMOD = 1428;
            public const int ERROR_GLOBAL_ONLY_HOOK = 1429;
            public const int ERROR_JOURNAL_HOOK_SET = 1430;
            public const int ERROR_HOOK_NOT_INSTALLED = 1431;
            public const int ERROR_INVALID_LB_MESSAGE = 1432;
            public const int ERROR_SETCOUNT_ON_BAD_LB = 1433;
            public const int ERROR_LB_WITHOUT_TABSTOPS = 1434;
            public const int ERROR_DESTROY_OBJECT_OF_OTHER_THREAD = 1435;
            public const int ERROR_CHILD_WINDOW_MENU = 1436;
            public const int ERROR_NO_SYSTEM_MENU = 1437;
            public const int ERROR_INVALID_MSGBOX_STYLE = 1438;
            public const int ERROR_INVALID_SPI_VALUE = 1439;
            public const int ERROR_SCREEN_ALREADY_LOCKED = 1440;
            public const int ERROR_HWNDS_HAVE_DIFF_PARENT = 1441;
            public const int ERROR_NOT_CHILD_WINDOW = 1442;
            public const int ERROR_INVALID_GW_COMMAND = 1443;
            public const int ERROR_INVALID_THREAD_ID = 1444;
            public const int ERROR_NON_MDICHILD_WINDOW = 1445;
            public const int ERROR_POPUP_ALREADY_ACTIVE = 1446;
            public const int ERROR_NO_SCROLLBARS = 1447;
            public const int ERROR_INVALID_SCROLLBAR_RANGE = 1448;
            public const int ERROR_INVALID_SHOWWIN_COMMAND = 1449;
            public const int ERROR_NO_SYSTEM_RESOURCES = 1450;
            public const int ERROR_NONPAGED_SYSTEM_RESOURCES = 1451;
            public const int ERROR_PAGED_SYSTEM_RESOURCES = 1452;
            public const int ERROR_WORKING_SET_QUOTA = 1453;
            public const int ERROR_PAGEFILE_QUOTA = 1454;
            public const int ERROR_COMMITMENT_LIMIT = 1455;
            public const int ERROR_MENU_ITEM_NOT_FOUND = 1456;
            public const int ERROR_INVALID_KEYBOARD_HANDLE = 1457;
            public const int ERROR_HOOK_TYPE_NOT_ALLOWED = 1458;
            public const int ERROR_REQUIRES_INTERACTIVE_WINDOWSTATION = 1459;
            public const int ERROR_TIMEOUT = 1460;
            public const int ERROR_INVALID_MONITOR_HANDLE = 1461;
            public const int ERROR_EVENTLOG_FILE_CORRUPT = 1500;
            public const int ERROR_EVENTLOG_CANT_START = 1501;
            public const int ERROR_LOG_FILE_FULL = 1502;
            public const int ERROR_EVENTLOG_FILE_CHANGED = 1503;
            public const int ERROR_INSTALL_SERVICE_FAILURE = 1601;
            public const int ERROR_INSTALL_USEREXIT = 1602;
            public const int ERROR_INSTALL_FAILURE = 1603;
            public const int ERROR_INSTALL_SUSPEND = 1604;
            public const int ERROR_UNKNOWN_PRODUCT = 1605;
            public const int ERROR_UNKNOWN_FEATURE = 1606;
            public const int ERROR_UNKNOWN_COMPONENT = 1607;
            public const int ERROR_UNKNOWN_PROPERTY = 1608;
            public const int ERROR_INVALID_HANDLE_STATE = 1609;
            public const int ERROR_BAD_CONFIGURATION = 1610;
            public const int ERROR_INDEX_ABSENT = 1611;
            public const int ERROR_INSTALL_SOURCE_ABSENT = 1612;
            public const int ERROR_INSTALL_PACKAGE_VERSION = 1613;
            public const int ERROR_PRODUCT_UNINSTALLED = 1614;
            public const int ERROR_BAD_QUERY_SYNTAX = 1615;
            public const int ERROR_INVALID_FIELD = 1616;
            public const int ERROR_DEVICE_REMOVED = 1617;
            public const int ERROR_INSTALL_ALREADY_RUNNING = 1618;
            public const int ERROR_INSTALL_PACKAGE_OPEN_FAILED = 1619;
            public const int ERROR_INSTALL_PACKAGE_INVALID = 1620;
            public const int ERROR_INSTALL_UI_FAILURE = 1621;
            public const int ERROR_INSTALL_LOG_FAILURE = 1622;
            public const int ERROR_INSTALL_LANGUAGE_UNSUPPORTED = 1623;
            public const int ERROR_INSTALL_TRANSFORM_FAILURE = 1624;
            public const int ERROR_INSTALL_PACKAGE_REJECTED = 1625;
            public const int ERROR_FUNCTION_NOT_CALLED = 1626;
            public const int ERROR_FUNCTION_FAILED = 1627;
            public const int ERROR_INVALID_TABLE = 1628;
            public const int ERROR_DATATYPE_MISMATCH = 1629;
            public const int ERROR_UNSUPPORTED_TYPE = 1630;
            public const int ERROR_CREATE_FAILED = 1631;
            public const int ERROR_INSTALL_TEMP_UNWRITABLE = 1632;
            public const int ERROR_INSTALL_PLATFORM_UNSUPPORTED = 1633;
            public const int ERROR_INSTALL_NOTUSED = 1634;
            public const int ERROR_PATCH_PACKAGE_OPEN_FAILED = 1635;
            public const int ERROR_PATCH_PACKAGE_INVALID = 1636;
            public const int ERROR_PATCH_PACKAGE_UNSUPPORTED = 1637;
            public const int ERROR_PRODUCT_VERSION = 1638;
            public const int ERROR_INVALID_COMMAND_LINE = 1639;
            public const int ERROR_INSTALL_REMOTE_DISALLOWED = 1640;
            public const int ERROR_SUCCESS_REBOOT_INITIATED = 1641;
            public const int ERROR_PATCH_TARGET_NOT_FOUND = 1642;
            public const int ERROR_PATCH_PACKAGE_REJECTED = 1643;
            public const int ERROR_INSTALL_TRANSFORM_REJECTED = 1644;
            public const int ERROR_INSTALL_REMOTE_PROHIBITED = 1645;
            public const int RPC_S_INVALID_STRING_BINDING = 1700;
            public const int RPC_S_WRONG_KIND_OF_BINDING = 1701;
            public const int RPC_S_INVALID_BINDING = 1702;
            public const int RPC_S_PROTSEQ_NOT_SUPPORTED = 1703;
            public const int RPC_S_INVALID_RPC_PROTSEQ = 1704;
            public const int RPC_S_INVALID_STRING_UUID = 1705;
            public const int RPC_S_INVALID_ENDPOINT_FORMAT = 1706;
            public const int RPC_S_INVALID_NET_ADDR = 1707;
            public const int RPC_S_NO_ENDPOINT_FOUND = 1708;
            public const int RPC_S_INVALID_TIMEOUT = 1709;
            public const int RPC_S_OBJECT_NOT_FOUND = 1710;
            public const int RPC_S_ALREADY_REGISTERED = 1711;
            public const int RPC_S_TYPE_ALREADY_REGISTERED = 1712;
            public const int RPC_S_ALREADY_LISTENING = 1713;
            public const int RPC_S_NO_PROTSEQS_REGISTERED = 1714;
            public const int RPC_S_NOT_LISTENING = 1715;
            public const int RPC_S_UNKNOWN_MGR_TYPE = 1716;
            public const int RPC_S_UNKNOWN_IF = 1717;
            public const int RPC_S_NO_BINDINGS = 1718;
            public const int RPC_S_NO_PROTSEQS = 1719;
            public const int RPC_S_CANT_CREATE_ENDPOINT = 1720;
            public const int RPC_S_OUT_OF_RESOURCES = 1721;
            public const int RPC_S_SERVER_UNAVAILABLE = 1722;
            public const int RPC_S_SERVER_TOO_BUSY = 1723;
            public const int RPC_S_INVALID_NETWORK_OPTIONS = 1724;
            public const int RPC_S_NO_CALL_ACTIVE = 1725;
            public const int RPC_S_CALL_FAILED = 1726;
            public const int RPC_S_CALL_FAILED_DNE = 1727;
            public const int RPC_S_PROTOCOL_ERROR = 1728;
            public const int RPC_S_UNSUPPORTED_TRANS_SYN = 1730;
            public const int RPC_S_UNSUPPORTED_TYPE = 1732;
            public const int RPC_S_INVALID_TAG = 1733;
            public const int RPC_S_INVALID_BOUND = 1734;
            public const int RPC_S_NO_ENTRY_NAME = 1735;
            public const int RPC_S_INVALID_NAME_SYNTAX = 1736;
            public const int RPC_S_UNSUPPORTED_NAME_SYNTAX = 1737;
            public const int RPC_S_UUID_NO_ADDRESS = 1739;
            public const int RPC_S_DUPLICATE_ENDPOINT = 1740;
            public const int RPC_S_UNKNOWN_AUTHN_TYPE = 1741;
            public const int RPC_S_MAX_CALLS_TOO_SMALL = 1742;
            public const int RPC_S_STRING_TOO_Int32 = 1743;
            public const int RPC_S_PROTSEQ_NOT_FOUND = 1744;
            public const int RPC_S_PROCNUM_OUT_OF_RANGE = 1745;
            public const int RPC_S_BINDING_HAS_NO_AUTH = 1746;
            public const int RPC_S_UNKNOWN_AUTHN_SERVICE = 1747;
            public const int RPC_S_UNKNOWN_AUTHN_LEVEL = 1748;
            public const int RPC_S_INVALID_AUTH_IDENTITY = 1749;
            public const int RPC_S_UNKNOWN_AUTHZ_SERVICE = 1750;
            public const int EPT_S_INVALID_ENTRY = 1751;
            public const int EPT_S_CANT_PERFORM_OP = 1752;
            public const int EPT_S_NOT_REGISTERED = 1753;
            public const int RPC_S_NOTHING_TO_EXPORT = 1754;
            public const int RPC_S_INCOMPLETE_NAME = 1755;
            public const int RPC_S_INVALID_VERS_OPTION = 1756;
            public const int RPC_S_NO_MORE_MEMBERS = 1757;
            public const int RPC_S_NOT_ALL_OBJS_UNEXPORTED = 1758;
            public const int RPC_S_INTERFACE_NOT_FOUND = 1759;
            public const int RPC_S_ENTRY_ALREADY_EXISTS = 1760;
            public const int RPC_S_ENTRY_NOT_FOUND = 1761;
            public const int RPC_S_NAME_SERVICE_UNAVAILABLE = 1762;
            public const int RPC_S_INVALID_NAF_ID = 1763;
            public const int RPC_S_CANNOT_SUPPORT = 1764;
            public const int RPC_S_NO_CONTEXT_AVAILABLE = 1765;
            public const int RPC_S_INTERNAL_ERROR = 1766;
            public const int RPC_S_ZERO_DIVIDE = 1767;
            public const int RPC_S_ADDRESS_ERROR = 1768;
            public const int RPC_S_FP_DIV_ZERO = 1769;
            public const int RPC_S_FP_UNDERFLOW = 1770;
            public const int RPC_S_FP_OVERFLOW = 1771;
            public const int RPC_X_NO_MORE_ENTRIES = 1772;
            public const int RPC_X_SS_CHAR_TRANS_OPEN_FAIL = 1773;
            public const int RPC_X_SS_CHAR_TRANS_Int16_FILE = 1774;
            public const int RPC_X_SS_IN_NULL_CONTEXT = 1775;
            public const int RPC_X_SS_CONTEXT_DAMAGED = 1777;
            public const int RPC_X_SS_HANDLES_MISMATCH = 1778;
            public const int RPC_X_SS_CANNOT_GET_CALL_HANDLE = 1779;
            public const int RPC_X_NULL_REF_POINTER = 1780;
            public const int RPC_X_ENUM_VALUE_OUT_OF_RANGE = 1781;
            public const int RPC_X_BYTE_COUNT_TOO_SMALL = 1782;
            public const int RPC_X_BAD_STUB_DATA = 1783;
            public const int ERROR_INVALID_USER_BUFFER = 1784;
            public const int ERROR_UNRECOGNIZED_MEDIA = 1785;
            public const int ERROR_NO_TRUST_LSA_SECRET = 1786;
            public const int ERROR_NO_TRUST_SAM_ACCOUNT = 1787;
            public const int ERROR_TRUSTED_DOMAIN_FAILURE = 1788;
            public const int ERROR_TRUSTED_RELATIONSHIP_FAILURE = 1789;
            public const int ERROR_TRUST_FAILURE = 1790;
            public const int RPC_S_CALL_IN_PROGRESS = 1791;
            public const int ERROR_NETLOGON_NOT_STARTED = 1792;
            public const int ERROR_ACCOUNT_EXPIRED = 1793;
            public const int ERROR_REDIRECTOR_HAS_OPEN_HANDLES = 1794;
            public const int ERROR_PRINTER_DRIVER_ALREADY_INSTALLED = 1795;
            public const int ERROR_UNKNOWN_PORT = 1796;
            public const int ERROR_UNKNOWN_PRINTER_DRIVER = 1797;
            public const int ERROR_UNKNOWN_PRINTPROCESSOR = 1798;
            public const int ERROR_INVALID_SEPARATOR_FILE = 1799;
            public const int ERROR_INVALID_PRIORITY = 1800;
            public const int ERROR_INVALID_PRINTER_NAME = 1801;
            public const int ERROR_PRINTER_ALREADY_EXISTS = 1802;
            public const int ERROR_INVALID_PRINTER_COMMAND = 1803;
            public const int ERROR_INVALID_DATATYPE = 1804;
            public const int ERROR_INVALID_ENVIRONMENT = 1805;
            public const int RPC_S_NO_MORE_BINDINGS = 1806;
            public const int ERROR_NOLOGON_INTERDOMAIN_TRUST_ACCOUNT = 1807;
            public const int ERROR_NOLOGON_WORKSTATION_TRUST_ACCOUNT = 1808;
            public const int ERROR_NOLOGON_SERVER_TRUST_ACCOUNT = 1809;
            public const int ERROR_DOMAIN_TRUST_INCONSISTENT = 1810;
            public const int ERROR_SERVER_HAS_OPEN_HANDLES = 1811;
            public const int ERROR_RESOURCE_DATA_NOT_FOUND = 1812;
            public const int ERROR_RESOURCE_TYPE_NOT_FOUND = 1813;
            public const int ERROR_RESOURCE_NAME_NOT_FOUND = 1814;
            public const int ERROR_RESOURCE_LANG_NOT_FOUND = 1815;
            public const int ERROR_NOT_ENOUGH_QUOTA = 1816;
            public const int RPC_S_NO_INTERFACES = 1817;
            public const int RPC_S_CALL_CANCELLED = 1818;
            public const int RPC_S_BINDING_INCOMPLETE = 1819;
            public const int RPC_S_COMM_FAILURE = 1820;
            public const int RPC_S_UNSUPPORTED_AUTHN_LEVEL = 1821;
            public const int RPC_S_NO_PRINC_NAME = 1822;
            public const int RPC_S_NOT_RPC_ERROR = 1823;
            public const int RPC_S_UUID_LOCAL_ONLY = 1824;
            public const int RPC_S_SEC_PKG_ERROR = 1825;
            public const int RPC_S_NOT_CANCELLED = 1826;
            public const int RPC_X_INVALID_ES_ACTION = 1827;
            public const int RPC_X_WRONG_ES_VERSION = 1828;
            public const int RPC_X_WRONG_STUB_VERSION = 1829;
            public const int RPC_X_INVALID_PIPE_OBJECT = 1830;
            public const int RPC_X_WRONG_PIPE_ORDER = 1831;
            public const int RPC_X_WRONG_PIPE_VERSION = 1832;
            public const int RPC_S_GROUP_MEMBER_NOT_FOUND = 1898;
            public const int EPT_S_CANT_CREATE = 1899;
            public const int RPC_S_INVALID_OBJECT = 1900;
            public const int ERROR_INVALID_TIME = 1901;
            public const int ERROR_INVALID_FORM_NAME = 1902;
            public const int ERROR_INVALID_FORM_SIZE = 1903;
            public const int ERROR_ALREADY_WAITING = 1904;
            public const int ERROR_PRINTER_DELETED = 1905;
            public const int ERROR_INVALID_PRINTER_STATE = 1906;
            public const int ERROR_PASSUInt16_MUST_CHANGE = 1907;
            public const int ERROR_DOMAIN_CONTROLLER_NOT_FOUND = 1908;
            public const int ERROR_ACCOUNT_LOCKED_OUT = 1909;
            public const int OR_INVALID_OXID = 1910;
            public const int OR_INVALID_OID = 1911;
            public const int OR_INVALID_SET = 1912;
            public const int RPC_S_SEND_INCOMPLETE = 1913;
            public const int RPC_S_INVALID_ASYNC_HANDLE = 1914;
            public const int RPC_S_INVALID_ASYNC_CALL = 1915;
            public const int RPC_X_PIPE_CLOSED = 1916;
            public const int RPC_X_PIPE_DISCIPLINE_ERROR = 1917;
            public const int RPC_X_PIPE_EMPTY = 1918;
            public const int ERROR_NO_SITENAME = 1919;
            public const int ERROR_CANT_ACCESS_FILE = 1920;
            public const int ERROR_CANT_RESOLVE_FILENAME = 1921;
            public const int RPC_S_ENTRY_TYPE_MISMATCH = 1922;
            public const int RPC_S_NOT_ALL_OBJS_EXPORTED = 1923;
            public const int RPC_S_INTERFACE_NOT_EXPORTED = 1924;
            public const int RPC_S_PROFILE_NOT_ADDED = 1925;
            public const int RPC_S_PRF_ELT_NOT_ADDED = 1926;
            public const int RPC_S_PRF_ELT_NOT_REMOVED = 1927;
            public const int RPC_S_GRP_ELT_NOT_ADDED = 1928;
            public const int RPC_S_GRP_ELT_NOT_REMOVED = 1929;
            public const int ERROR_KM_DRIVER_BLOCKED = 1930;
            public const int ERROR_CONTEXT_EXPIRED = 1931;
            public const int ERROR_PER_USER_TRUST_QUOTA_EXCEEDED = 1932;
            public const int ERROR_ALL_USER_TRUST_QUOTA_EXCEEDED = 1933;
            public const int ERROR_USER_DELETE_TRUST_QUOTA_EXCEEDED = 1934;
            public const int ERROR_AUTHENTICATION_FIREWALL_FAILED = 1935;
            public const int ERROR_REMOTE_PRINT_CONNECTIONS_BLOCKED = 1936;
            public const int ERROR_INVALID_PIXEL_FORMAT = 2000;
            public const int ERROR_BAD_DRIVER = 2001;
            public const int ERROR_INVALID_WINDOW_STYLE = 2002;
            public const int ERROR_METAFILE_NOT_SUPPORTED = 2003;
            public const int ERROR_TRANSFORM_NOT_SUPPORTED = 2004;
            public const int ERROR_CLIPPING_NOT_SUPPORTED = 2005;
            public const int ERROR_INVALID_CMM = 2010;
            public const int ERROR_INVALID_PROFILE = 2011;
            public const int ERROR_TAG_NOT_FOUND = 2012;
            public const int ERROR_TAG_NOT_PRESENT = 2013;
            public const int ERROR_DUPLICATE_TAG = 2014;
            public const int ERROR_PROFILE_NOT_ASSOCIATED_WITH_DEVICE = 2015;
            public const int ERROR_PROFILE_NOT_FOUND = 2016;
            public const int ERROR_INVALID_COLORSPACE = 2017;
            public const int ERROR_ICM_NOT_ENABLED = 2018;
            public const int ERROR_DELETING_ICM_XFORM = 2019;
            public const int ERROR_INVALID_TRANSFORM = 2020;
            public const int ERROR_COLORSPACE_MISMATCH = 2021;
            public const int ERROR_INVALID_COLORINDEX = 2022;
            public const int ERROR_CONNECTED_OTHER_PASSUInt16 = 2108;
            public const int ERROR_CONNECTED_OTHER_PASSUInt16_DEFAULT = 2109;
            public const int ERROR_BAD_USERNAME = 2202;
            public const int ERROR_NOT_CONNECTED = 2250;
            public const int ERROR_OPEN_FILES = 2401;
            public const int ERROR_ACTIVE_CONNECTIONS = 2402;
            public const int ERROR_DEVICE_IN_USE = 2404;
            public const int ERROR_UNKNOWN_PRINT_MONITOR = 3000;
            public const int ERROR_PRINTER_DRIVER_IN_USE = 3001;
            public const int ERROR_SPOOL_FILE_NOT_FOUND = 3002;
            public const int ERROR_SPL_NO_STARTDOC = 3003;
            public const int ERROR_SPL_NO_ADDJOB = 3004;
            public const int ERROR_PRINT_PROCESSOR_ALREADY_INSTALLED = 3005;
            public const int ERROR_PRINT_MONITOR_ALREADY_INSTALLED = 3006;
            public const int ERROR_INVALID_PRINT_MONITOR = 3007;
            public const int ERROR_PRINT_MONITOR_IN_USE = 3008;
            public const int ERROR_PRINTER_HAS_JOBS_QUEUED = 3009;
            public const int ERROR_SUCCESS_REBOOT_REQUIRED = 3010;
            public const int ERROR_SUCCESS_RESTART_REQUIRED = 3011;
            public const int ERROR_PRINTER_NOT_FOUND = 3012;
            public const int ERROR_PRINTER_DRIVER_WARNED = 3013;
            public const int ERROR_PRINTER_DRIVER_BLOCKED = 3014;
            public const int ERROR_WINS_INTERNAL = 4000;
            public const int ERROR_CAN_NOT_DEL_LOCAL_WINS = 4001;
            public const int ERROR_STATIC_INIT = 4002;
            public const int ERROR_INC_BACKUP = 4003;
            public const int ERROR_FULL_BACKUP = 4004;
            public const int ERROR_REC_NON_EXISTENT = 4005;
            public const int ERROR_RPL_NOT_ALLOWED = 4006;
            public const int ERROR_DHCP_ADDRESS_CONFLICT = 4100;
            public const int ERROR_WMI_GUID_NOT_FOUND = 4200;
            public const int ERROR_WMI_INSTANCE_NOT_FOUND = 4201;
            public const int ERROR_WMI_ITEMID_NOT_FOUND = 4202;
            public const int ERROR_WMI_TRY_AGAIN = 4203;
            public const int ERROR_WMI_DP_NOT_FOUND = 4204;
            public const int ERROR_WMI_UNRESOLVED_INSTANCE_REF = 4205;
            public const int ERROR_WMI_ALREADY_ENABLED = 4206;
            public const int ERROR_WMI_GUID_DISCONNECTED = 4207;
            public const int ERROR_WMI_SERVER_UNAVAILABLE = 4208;
            public const int ERROR_WMI_DP_FAILED = 4209;
            public const int ERROR_WMI_INVALID_MOF = 4210;
            public const int ERROR_WMI_INVALID_REGINFO = 4211;
            public const int ERROR_WMI_ALREADY_DISABLED = 4212;
            public const int ERROR_WMI_READ_ONLY = 4213;
            public const int ERROR_WMI_SET_FAILURE = 4214;
            public const int ERROR_INVALID_MEDIA = 4300;
            public const int ERROR_INVALID_LIBRARY = 4301;
            public const int ERROR_INVALID_MEDIA_POOL = 4302;
            public const int ERROR_DRIVE_MEDIA_MISMATCH = 4303;
            public const int ERROR_MEDIA_OFFLINE = 4304;
            public const int ERROR_LIBRARY_OFFLINE = 4305;
            public const int ERROR_EMPTY = 4306;
            public const int ERROR_NOT_EMPTY = 4307;
            public const int ERROR_MEDIA_UNAVAILABLE = 4308;
            public const int ERROR_RESOURCE_DISABLED = 4309;
            public const int ERROR_INVALID_CLEANER = 4310;
            public const int ERROR_UNABLE_TO_CLEAN = 4311;
            public const int ERROR_OBJECT_NOT_FOUND = 4312;
            public const int ERROR_DATABASE_FAILURE = 4313;
            public const int ERROR_DATABASE_FULL = 4314;
            public const int ERROR_MEDIA_INCOMPATIBLE = 4315;
            public const int ERROR_RESOURCE_NOT_PRESENT = 4316;
            public const int ERROR_INVALID_OPERATION = 4317;
            public const int ERROR_MEDIA_NOT_AVAILABLE = 4318;
            public const int ERROR_DEVICE_NOT_AVAILABLE = 4319;
            public const int ERROR_REQUEST_REFUSED = 4320;
            public const int ERROR_INVALID_DRIVE_OBJECT = 4321;
            public const int ERROR_LIBRARY_FULL = 4322;
            public const int ERROR_MEDIUM_NOT_ACCESSIBLE = 4323;
            public const int ERROR_UNABLE_TO_LOAD_MEDIUM = 4324;
            public const int ERROR_UNABLE_TO_INVENTORY_DRIVE = 4325;
            public const int ERROR_UNABLE_TO_INVENTORY_SLOT = 4326;
            public const int ERROR_UNABLE_TO_INVENTORY_TRANSPORT = 4327;
            public const int ERROR_TRANSPORT_FULL = 4328;
            public const int ERROR_CONTROLLING_IEPORT = 4329;
            public const int ERROR_UNABLE_TO_EJECT_MOUNTED_MEDIA = 4330;
            public const int ERROR_CLEANER_SLOT_SET = 4331;
            public const int ERROR_CLEANER_SLOT_NOT_SET = 4332;
            public const int ERROR_CLEANER_CARTRIDGE_SPENT = 4333;
            public const int ERROR_UNEXPECTED_OMID = 4334;
            public const int ERROR_CANT_DELETE_LAST_ITEM = 4335;
            public const int ERROR_MESSAGE_EXCEEDS_MAX_SIZE = 4336;
            public const int ERROR_VOLUME_CONTAINS_SYS_FILES = 4337;
            public const int ERROR_INDIGENOUS_TYPE = 4338;
            public const int ERROR_NO_SUPPORTING_DRIVES = 4339;
            public const int ERROR_CLEANER_CARTRIDGE_INSTALLED = 4340;
            public const int ERROR_FILE_OFFLINE = 4350;
            public const int ERROR_REMOTE_STORAGE_NOT_ACTIVE = 4351;
            public const int ERROR_REMOTE_STORAGE_MEDIA_ERROR = 4352;
            public const int ERROR_NOT_A_REPARSE_POINT = 4390;
            public const int ERROR_REPARSE_ATTRIBUTE_CONFLICT = 4391;
            public const int ERROR_INVALID_REPARSE_DATA = 4392;
            public const int ERROR_REPARSE_TAG_INVALID = 4393;
            public const int ERROR_REPARSE_TAG_MISMATCH = 4394;
            public const int ERROR_VOLUME_NOT_SIS_ENABLED = 4500;
            public const int ERROR_DEPENDENT_RESOURCE_EXISTS = 5001;
            public const int ERROR_DEPENDENCY_NOT_FOUND = 5002;
            public const int ERROR_DEPENDENCY_ALREADY_EXISTS = 5003;
            public const int ERROR_RESOURCE_NOT_ONLINE = 5004;
            public const int ERROR_HOST_NODE_NOT_AVAILABLE = 5005;
            public const int ERROR_RESOURCE_NOT_AVAILABLE = 5006;
            public const int ERROR_RESOURCE_NOT_FOUND = 5007;
            public const int ERROR_SHUTDOWN_CLUSTER = 5008;
            public const int ERROR_CANT_EVICT_ACTIVE_NODE = 5009;
            public const int ERROR_OBJECT_ALREADY_EXISTS = 5010;
            public const int ERROR_OBJECT_IN_LIST = 5011;
            public const int ERROR_GROUP_NOT_AVAILABLE = 5012;
            public const int ERROR_GROUP_NOT_FOUND = 5013;
            public const int ERROR_GROUP_NOT_ONLINE = 5014;
            public const int ERROR_HOST_NODE_NOT_RESOURCE_OWNER = 5015;
            public const int ERROR_HOST_NODE_NOT_GROUP_OWNER = 5016;
            public const int ERROR_RESMON_CREATE_FAILED = 5017;
            public const int ERROR_RESMON_ONLINE_FAILED = 5018;
            public const int ERROR_RESOURCE_ONLINE = 5019;
            public const int ERROR_QUORUM_RESOURCE = 5020;
            public const int ERROR_NOT_QUORUM_CAPABLE = 5021;
            public const int ERROR_CLUSTER_SHUTTING_DOWN = 5022;
            public const int ERROR_INVALID_STATE = 5023;
            public const int ERROR_RESOURCE_PROPERTIES_STORED = 5024;
            public const int ERROR_NOT_QUORUM_CLASS = 5025;
            public const int ERROR_CORE_RESOURCE = 5026;
            public const int ERROR_QUORUM_RESOURCE_ONLINE_FAILED = 5027;
            public const int ERROR_QUORUMLOG_OPEN_FAILED = 5028;
            public const int ERROR_CLUSTERLOG_CORRUPT = 5029;
            public const int ERROR_CLUSTERLOG_RECORD_EXCEEDS_MAXSIZE = 5030;
            public const int ERROR_CLUSTERLOG_EXCEEDS_MAXSIZE = 5031;
            public const int ERROR_CLUSTERLOG_CHKPOINT_NOT_FOUND = 5032;
            public const int ERROR_CLUSTERLOG_NOT_ENOUGH_SPACE = 5033;
            public const int ERROR_QUORUM_OWNER_ALIVE = 5034;
            public const int ERROR_NETWORK_NOT_AVAILABLE = 5035;
            public const int ERROR_NODE_NOT_AVAILABLE = 5036;
            public const int ERROR_ALL_NODES_NOT_AVAILABLE = 5037;
            public const int ERROR_RESOURCE_FAILED = 5038;
            public const int ERROR_CLUSTER_INVALID_NODE = 5039;
            public const int ERROR_CLUSTER_NODE_EXISTS = 5040;
            public const int ERROR_CLUSTER_JOIN_IN_PROGRESS = 5041;
            public const int ERROR_CLUSTER_NODE_NOT_FOUND = 5042;
            public const int ERROR_CLUSTER_LOCAL_NODE_NOT_FOUND = 5043;
            public const int ERROR_CLUSTER_NETWORK_EXISTS = 5044;
            public const int ERROR_CLUSTER_NETWORK_NOT_FOUND = 5045;
            public const int ERROR_CLUSTER_NETINTERFACE_EXISTS = 5046;
            public const int ERROR_CLUSTER_NETINTERFACE_NOT_FOUND = 5047;
            public const int ERROR_CLUSTER_INVALID_REQUEST = 5048;
            public const int ERROR_CLUSTER_INVALID_NETWORK_PROVIDER = 5049;
            public const int ERROR_CLUSTER_NODE_DOWN = 5050;
            public const int ERROR_CLUSTER_NODE_UNREACHABLE = 5051;
            public const int ERROR_CLUSTER_NODE_NOT_MEMBER = 5052;
            public const int ERROR_CLUSTER_JOIN_NOT_IN_PROGRESS = 5053;
            public const int ERROR_CLUSTER_INVALID_NETWORK = 5054;
            public const int ERROR_CLUSTER_NODE_UP = 5056;
            public const int ERROR_CLUSTER_IPADDR_IN_USE = 5057;
            public const int ERROR_CLUSTER_NODE_NOT_PAUSED = 5058;
            public const int ERROR_CLUSTER_NO_SECURITY_CONTEXT = 5059;
            public const int ERROR_CLUSTER_NETWORK_NOT_INTERNAL = 5060;
            public const int ERROR_CLUSTER_NODE_ALREADY_UP = 5061;
            public const int ERROR_CLUSTER_NODE_ALREADY_DOWN = 5062;
            public const int ERROR_CLUSTER_NETWORK_ALREADY_ONLINE = 5063;
            public const int ERROR_CLUSTER_NETWORK_ALREADY_OFFLINE = 5064;
            public const int ERROR_CLUSTER_NODE_ALREADY_MEMBER = 5065;
            public const int ERROR_CLUSTER_LAST_INTERNAL_NETWORK = 5066;
            public const int ERROR_CLUSTER_NETWORK_HAS_DEPENDENTS = 5067;
            public const int ERROR_INVALID_OPERATION_ON_QUORUM = 5068;
            public const int ERROR_DEPENDENCY_NOT_ALLOWED = 5069;
            public const int ERROR_CLUSTER_NODE_PAUSED = 5070;
            public const int ERROR_NODE_CANT_HOST_RESOURCE = 5071;
            public const int ERROR_CLUSTER_NODE_NOT_READY = 5072;
            public const int ERROR_CLUSTER_NODE_SHUTTING_DOWN = 5073;
            public const int ERROR_CLUSTER_JOIN_ABORTED = 5074;
            public const int ERROR_CLUSTER_INCOMPATIBLE_VERSIONS = 5075;
            public const int ERROR_CLUSTER_MAXNUM_OF_RESOURCES_EXCEEDED = 5076;
            public const int ERROR_CLUSTER_SYSTEM_CONFIG_CHANGED = 5077;
            public const int ERROR_CLUSTER_RESOURCE_TYPE_NOT_FOUND = 5078;
            public const int ERROR_CLUSTER_RESTYPE_NOT_SUPPORTED = 5079;
            public const int ERROR_CLUSTER_RESNAME_NOT_FOUND = 5080;
            public const int ERROR_CLUSTER_NO_RPC_PACKAGES_REGISTERED = 5081;
            public const int ERROR_CLUSTER_OWNER_NOT_IN_PREFLIST = 5082;
            public const int ERROR_CLUSTER_DATABASE_SEQMISMATCH = 5083;
            public const int ERROR_RESMON_INVALID_STATE = 5084;
            public const int ERROR_CLUSTER_GUM_NOT_LOCKER = 5085;
            public const int ERROR_QUORUM_DISK_NOT_FOUND = 5086;
            public const int ERROR_DATABASE_BACKUP_CORRUPT = 5087;
            public const int ERROR_CLUSTER_NODE_ALREADY_HAS_DFS_ROOT = 5088;
            public const int ERROR_RESOURCE_PROPERTY_UNCHANGEABLE = 5089;
            public const int ERROR_CLUSTER_MEMBERSHIP_INVALID_STATE = 5890;
            public const int ERROR_CLUSTER_QUORUMLOG_NOT_FOUND = 5891;
            public const int ERROR_CLUSTER_MEMBERSHIP_HALT = 5892;
            public const int ERROR_CLUSTER_INSTANCE_ID_MISMATCH = 5893;
            public const int ERROR_CLUSTER_NETWORK_NOT_FOUND_FOR_IP = 5894;
            public const int ERROR_CLUSTER_PROPERTY_DATA_TYPE_MISMATCH = 5895;
            public const int ERROR_CLUSTER_EVICT_WITHOUT_CLEANUP = 5896;
            public const int ERROR_CLUSTER_PARAMETER_MISMATCH = 5897;
            public const int ERROR_NODE_CANNOT_BE_CLUSTERED = 5898;
            public const int ERROR_CLUSTER_WRONG_OS_VERSION = 5899;
            public const int ERROR_CLUSTER_CANT_CREATE_DUP_CLUSTER_NAME = 5900;
            public const int ERROR_CLUSCFG_ALREADY_COMMITTED = 5901;
            public const int ERROR_CLUSCFG_ROLLBACK_FAILED = 5902;
            public const int ERROR_CLUSCFG_SYSTEM_DISK_DRIVE_LETTER_CONFLICT = 5903;
            public const int ERROR_CLUSTER_OLD_VERSION = 5904;
            public const int ERROR_CLUSTER_MISMATCHED_COMPUTER_ACCT_NAME = 5905;
            public const int ERROR_ENCRYPTION_FAILED = 6000;
            public const int ERROR_DECRYPTION_FAILED = 6001;
            public const int ERROR_FILE_ENCRYPTED = 6002;
            public const int ERROR_NO_RECOVERY_POLICY = 6003;
            public const int ERROR_NO_EFS = 6004;
            public const int ERROR_WRONG_EFS = 6005;
            public const int ERROR_NO_USER_KEYS = 6006;
            public const int ERROR_FILE_NOT_ENCRYPTED = 6007;
            public const int ERROR_NOT_EXPORT_FORMAT = 6008;
            public const int ERROR_FILE_READ_ONLY = 6009;
            public const int ERROR_DIR_EFS_DISALLOWED = 6010;
            public const int ERROR_EFS_SERVER_NOT_TRUSTED = 6011;
            public const int ERROR_BAD_RECOVERY_POLICY = 6012;
            public const int ERROR_EFS_ALG_BLOB_TOO_BIG = 6013;
            public const int ERROR_VOLUME_NOT_SUPPORT_EFS = 6014;
            public const int ERROR_EFS_DISABLED = 6015;
            public const int ERROR_EFS_VERSION_NOT_SUPPORT = 6016;
            public const int ERROR_NO_BROWSER_SERVERS_FOUND = 6118;
            public const int SCHED_E_SERVICE_NOT_LOCALSYSTEM = 6200;
            public const int ERROR_CTX_WINSTATION_NAME_INVALID = 7001;
            public const int ERROR_CTX_INVALID_PD = 7002;
            public const int ERROR_CTX_PD_NOT_FOUND = 7003;
            public const int ERROR_CTX_WD_NOT_FOUND = 7004;
            public const int ERROR_CTX_CANNOT_MAKE_EVENTLOG_ENTRY = 7005;
            public const int ERROR_CTX_SERVICE_NAME_COLLISION = 7006;
            public const int ERROR_CTX_CLOSE_PENDING = 7007;
            public const int ERROR_CTX_NO_OUTBUF = 7008;
            public const int ERROR_CTX_MODEM_INF_NOT_FOUND = 7009;
            public const int ERROR_CTX_INVALID_MODEMNAME = 7010;
            public const int ERROR_CTX_MODEM_RESPONSE_ERROR = 7011;
            public const int ERROR_CTX_MODEM_RESPONSE_TIMEOUT = 7012;
            public const int ERROR_CTX_MODEM_RESPONSE_NO_CARRIER = 7013;
            public const int ERROR_CTX_MODEM_RESPONSE_NO_DIALTONE = 7014;
            public const int ERROR_CTX_MODEM_RESPONSE_BUSY = 7015;
            public const int ERROR_CTX_MODEM_RESPONSE_VOICE = 7016;
            public const int ERROR_CTX_TD_ERROR = 7017;
            public const int ERROR_CTX_WINSTATION_NOT_FOUND = 7022;
            public const int ERROR_CTX_WINSTATION_ALREADY_EXISTS = 7023;
            public const int ERROR_CTX_WINSTATION_BUSY = 7024;
            public const int ERROR_CTX_BAD_VIDEO_MODE = 7025;
            public const int ERROR_CTX_GRAPHICS_INVALID = 7035;
            public const int ERROR_CTX_LOGON_DISABLED = 7037;
            public const int ERROR_CTX_NOT_CONSOLE = 7038;
            public const int ERROR_CTX_CLIENT_QUERY_TIMEOUT = 7040;
            public const int ERROR_CTX_CONSOLE_DISCONNECT = 7041;
            public const int ERROR_CTX_CONSOLE_CONNECT = 7042;
            public const int ERROR_CTX_SHADOW_DENIED = 7044;
            public const int ERROR_CTX_WINSTATION_ACCESS_DENIED = 7045;
            public const int ERROR_CTX_INVALID_WD = 7049;
            public const int ERROR_CTX_SHADOW_INVALID = 7050;
            public const int ERROR_CTX_SHADOW_DISABLED = 7051;
            public const int ERROR_CTX_CLIENT_LICENSE_IN_USE = 7052;
            public const int ERROR_CTX_CLIENT_LICENSE_NOT_SET = 7053;
            public const int ERROR_CTX_LICENSE_NOT_AVAILABLE = 7054;
            public const int ERROR_CTX_LICENSE_CLIENT_INVALID = 7055;
            public const int ERROR_CTX_LICENSE_EXPIRED = 7056;
            public const int ERROR_CTX_SHADOW_NOT_RUNNING = 7057;
            public const int ERROR_CTX_SHADOW_ENDED_BY_MODE_CHANGE = 7058;
            public const int ERROR_ACTIVATION_COUNT_EXCEEDED = 7059;
            public const int FRS_ERR_INVALID_API_SEQUENCE = 8001;
            public const int FRS_ERR_STARTING_SERVICE = 8002;
            public const int FRS_ERR_STOPPING_SERVICE = 8003;
            public const int FRS_ERR_INTERNAL_API = 8004;
            public const int FRS_ERR_INTERNAL = 8005;
            public const int FRS_ERR_SERVICE_COMM = 8006;
            public const int FRS_ERR_INSUFFICIENT_PRIV = 8007;
            public const int FRS_ERR_AUTHENTICATION = 8008;
            public const int FRS_ERR_PARENT_INSUFFICIENT_PRIV = 8009;
            public const int FRS_ERR_PARENT_AUTHENTICATION = 8010;
            public const int FRS_ERR_CHILD_TO_PARENT_COMM = 8011;
            public const int FRS_ERR_PARENT_TO_CHILD_COMM = 8012;
            public const int FRS_ERR_SYSVOL_POPULATE = 8013;
            public const int FRS_ERR_SYSVOL_POPULATE_TIMEOUT = 8014;
            public const int FRS_ERR_SYSVOL_IS_BUSY = 8015;
            public const int FRS_ERR_SYSVOL_DEMOTE = 8016;
            public const int FRS_ERR_INVALID_SERVICE_PARAMETER = 8017;
            public const int ERROR_DS_NOT_INSTALLED = 8200;
            public const int ERROR_DS_MEMBERSHIP_EVALUATED_LOCALLY = 8201;
            public const int ERROR_DS_NO_ATTRIBUTE_OR_VALUE = 8202;
            public const int ERROR_DS_INVALID_ATTRIBUTE_SYNTAX = 8203;
            public const int ERROR_DS_ATTRIBUTE_TYPE_UNDEFINED = 8204;
            public const int ERROR_DS_ATTRIBUTE_OR_VALUE_EXISTS = 8205;
            public const int ERROR_DS_BUSY = 8206;
            public const int ERROR_DS_UNAVAILABLE = 8207;
            public const int ERROR_DS_NO_RIDS_ALLOCATED = 8208;
            public const int ERROR_DS_NO_MORE_RIDS = 8209;
            public const int ERROR_DS_INCORRECT_ROLE_OWNER = 8210;
            public const int ERROR_DS_RIDMGR_INIT_ERROR = 8211;
            public const int ERROR_DS_OBJ_CLASS_VIOLATION = 8212;
            public const int ERROR_DS_CANT_ON_NON_LEAF = 8213;
            public const int ERROR_DS_CANT_ON_RDN = 8214;
            public const int ERROR_DS_CANT_MOD_OBJ_CLASS = 8215;
            public const int ERROR_DS_CROSS_DOM_MOVE_ERROR = 8216;
            public const int ERROR_DS_GC_NOT_AVAILABLE = 8217;
            public const int ERROR_SHARED_POLICY = 8218;
            public const int ERROR_POLICY_OBJECT_NOT_FOUND = 8219;
            public const int ERROR_POLICY_ONLY_IN_DS = 8220;
            public const int ERROR_PROMOTION_ACTIVE = 8221;
            public const int ERROR_NO_PROMOTION_ACTIVE = 8222;
            public const int ERROR_DS_OPERATIONS_ERROR = 8224;
            public const int ERROR_DS_PROTOCOL_ERROR = 8225;
            public const int ERROR_DS_TIMELIMIT_EXCEEDED = 8226;
            public const int ERROR_DS_SIZELIMIT_EXCEEDED = 8227;
            public const int ERROR_DS_ADMIN_LIMIT_EXCEEDED = 8228;
            public const int ERROR_DS_COMPARE_FALSE = 8229;
            public const int ERROR_DS_COMPARE_TRUE = 8230;
            public const int ERROR_DS_AUTH_METHOD_NOT_SUPPORTED = 8231;
            public const int ERROR_DS_STRONG_AUTH_REQUIRED = 8232;
            public const int ERROR_DS_INAPPROPRIATE_AUTH = 8233;
            public const int ERROR_DS_AUTH_UNKNOWN = 8234;
            public const int ERROR_DS_REFERRAL = 8235;
            public const int ERROR_DS_UNAVAILABLE_CRIT_EXTENSION = 8236;
            public const int ERROR_DS_CONFIDENTIALITY_REQUIRED = 8237;
            public const int ERROR_DS_INAPPROPRIATE_MATCHING = 8238;
            public const int ERROR_DS_CONSTRAINT_VIOLATION = 8239;
            public const int ERROR_DS_NO_SUCH_OBJECT = 8240;
            public const int ERROR_DS_ALIAS_PROBLEM = 8241;
            public const int ERROR_DS_INVALID_DN_SYNTAX = 8242;
            public const int ERROR_DS_IS_LEAF = 8243;
            public const int ERROR_DS_ALIAS_DEREF_PROBLEM = 8244;
            public const int ERROR_DS_UNWILLING_TO_PERFORM = 8245;
            public const int ERROR_DS_LOOP_DETECT = 8246;
            public const int ERROR_DS_NAMING_VIOLATION = 8247;
            public const int ERROR_DS_OBJECT_RESULTS_TOO_LARGE = 8248;
            public const int ERROR_DS_AFFECTS_MULTIPLE_DSAS = 8249;
            public const int ERROR_DS_SERVER_DOWN = 8250;
            public const int ERROR_DS_LOCAL_ERROR = 8251;
            public const int ERROR_DS_ENCODING_ERROR = 8252;
            public const int ERROR_DS_DECODING_ERROR = 8253;
            public const int ERROR_DS_FILTER_UNKNOWN = 8254;
            public const int ERROR_DS_PARAM_ERROR = 8255;
            public const int ERROR_DS_NOT_SUPPORTED = 8256;
            public const int ERROR_DS_NO_RESULTS_RETURNED = 8257;
            public const int ERROR_DS_CONTROL_NOT_FOUND = 8258;
            public const int ERROR_DS_CLIENT_LOOP = 8259;
            public const int ERROR_DS_REFERRAL_LIMIT_EXCEEDED = 8260;
            public const int ERROR_DS_SORT_CONTROL_MISSING = 8261;
            public const int ERROR_DS_OFFSET_RANGE_ERROR = 8262;
            public const int ERROR_DS_ROOT_MUST_BE_NC = 8301;
            public const int ERROR_DS_ADD_REPLICA_INHIBITED = 8302;
            public const int ERROR_DS_ATT_NOT_DEF_IN_SCHEMA = 8303;
            public const int ERROR_DS_MAX_OBJ_SIZE_EXCEEDED = 8304;
            public const int ERROR_DS_OBJ_STRING_NAME_EXISTS = 8305;
            public const int ERROR_DS_NO_RDN_DEFINED_IN_SCHEMA = 8306;
            public const int ERROR_DS_RDN_DOESNT_MATCH_SCHEMA = 8307;
            public const int ERROR_DS_NO_REQUESTED_ATTS_FOUND = 8308;
            public const int ERROR_DS_USER_BUFFER_TO_SMALL = 8309;
            public const int ERROR_DS_ATT_IS_NOT_ON_OBJ = 8310;
            public const int ERROR_DS_ILLEGAL_MOD_OPERATION = 8311;
            public const int ERROR_DS_OBJ_TOO_LARGE = 8312;
            public const int ERROR_DS_BAD_INSTANCE_TYPE = 8313;
            public const int ERROR_DS_MASTERDSA_REQUIRED = 8314;
            public const int ERROR_DS_OBJECT_CLASS_REQUIRED = 8315;
            public const int ERROR_DS_MISSING_REQUIRED_ATT = 8316;
            public const int ERROR_DS_ATT_NOT_DEF_FOR_CLASS = 8317;
            public const int ERROR_DS_ATT_ALREADY_EXISTS = 8318;
            public const int ERROR_DS_CANT_ADD_ATT_VALUES = 8320;
            public const int ERROR_DS_SINGLE_VALUE_CONSTRAINT = 8321;
            public const int ERROR_DS_RANGE_CONSTRAINT = 8322;
            public const int ERROR_DS_ATT_VAL_ALREADY_EXISTS = 8323;
            public const int ERROR_DS_CANT_REM_MISSING_ATT = 8324;
            public const int ERROR_DS_CANT_REM_MISSING_ATT_VAL = 8325;
            public const int ERROR_DS_ROOT_CANT_BE_SUBREF = 8326;
            public const int ERROR_DS_NO_CHAINING = 8327;
            public const int ERROR_DS_NO_CHAINED_EVAL = 8328;
            public const int ERROR_DS_NO_PARENT_OBJECT = 8329;
            public const int ERROR_DS_PARENT_IS_AN_ALIAS = 8330;
            public const int ERROR_DS_CANT_MIX_MASTER_AND_REPS = 8331;
            public const int ERROR_DS_CHILDREN_EXIST = 8332;
            public const int ERROR_DS_OBJ_NOT_FOUND = 8333;
            public const int ERROR_DS_ALIASED_OBJ_MISSING = 8334;
            public const int ERROR_DS_BAD_NAME_SYNTAX = 8335;
            public const int ERROR_DS_ALIAS_POINTS_TO_ALIAS = 8336;
            public const int ERROR_DS_CANT_DEREF_ALIAS = 8337;
            public const int ERROR_DS_OUT_OF_SCOPE = 8338;
            public const int ERROR_DS_OBJECT_BEING_REMOVED = 8339;
            public const int ERROR_DS_CANT_DELETE_DSA_OBJ = 8340;
            public const int ERROR_DS_GENERIC_ERROR = 8341;
            public const int ERROR_DS_DSA_MUST_BE_INT_MASTER = 8342;
            public const int ERROR_DS_CLASS_NOT_DSA = 8343;
            public const int ERROR_DS_INSUFF_ACCESS_RIGHTS = 8344;
            public const int ERROR_DS_ILLEGAL_SUPERIOR = 8345;
            public const int ERROR_DS_ATTRIBUTE_OWNED_BY_SAM = 8346;
            public const int ERROR_DS_NAME_TOO_MANY_PARTS = 8347;
            public const int ERROR_DS_NAME_TOO_Int32 = 8348;
            public const int ERROR_DS_NAME_VALUE_TOO_Int32 = 8349;
            public const int ERROR_DS_NAME_UNPARSEABLE = 8350;
            public const int ERROR_DS_NAME_TYPE_UNKNOWN = 8351;
            public const int ERROR_DS_NOT_AN_OBJECT = 8352;
            public const int ERROR_DS_SEC_DESC_TOO_Int16 = 8353;
            public const int ERROR_DS_SEC_DESC_INVALID = 8354;
            public const int ERROR_DS_NO_DELETED_NAME = 8355;
            public const int ERROR_DS_SUBREF_MUST_HAVE_PARENT = 8356;
            public const int ERROR_DS_NCNAME_MUST_BE_NC = 8357;
            public const int ERROR_DS_CANT_ADD_SYSTEM_ONLY = 8358;
            public const int ERROR_DS_CLASS_MUST_BE_CONCRETE = 8359;
            public const int ERROR_DS_INVALID_DMD = 8360;
            public const int ERROR_DS_OBJ_GUID_EXISTS = 8361;
            public const int ERROR_DS_NOT_ON_BACKLINK = 8362;
            public const int ERROR_DS_NO_CROSSREF_FOR_NC = 8363;
            public const int ERROR_DS_SHUTTING_DOWN = 8364;
            public const int ERROR_DS_UNKNOWN_OPERATION = 8365;
            public const int ERROR_DS_INVALID_ROLE_OWNER = 8366;
            public const int ERROR_DS_COULDNT_CONTACT_FSMO = 8367;
            public const int ERROR_DS_CROSS_NC_DN_RENAME = 8368;
            public const int ERROR_DS_CANT_MOD_SYSTEM_ONLY = 8369;
            public const int ERROR_DS_REPLICATOR_ONLY = 8370;
            public const int ERROR_DS_OBJ_CLASS_NOT_DEFINED = 8371;
            public const int ERROR_DS_OBJ_CLASS_NOT_SUBCLASS = 8372;
            public const int ERROR_DS_NAME_REFERENCE_INVALID = 8373;
            public const int ERROR_DS_CROSS_REF_EXISTS = 8374;
            public const int ERROR_DS_CANT_DEL_MASTER_CROSSREF = 8375;
            public const int ERROR_DS_SUBTREE_NOTIFY_NOT_NC_HEAD = 8376;
            public const int ERROR_DS_NOTIFY_FILTER_TOO_COMPLEX = 8377;
            public const int ERROR_DS_DUP_RDN = 8378;
            public const int ERROR_DS_DUP_OID = 8379;
            public const int ERROR_DS_DUP_MAPI_ID = 8380;
            public const int ERROR_DS_DUP_SCHEMA_ID_GUID = 8381;
            public const int ERROR_DS_DUP_LDAP_DISPLAY_NAME = 8382;
            public const int ERROR_DS_SEMANTIC_ATT_TEST = 8383;
            public const int ERROR_DS_SYNTAX_MISMATCH = 8384;
            public const int ERROR_DS_EXISTS_IN_MUST_HAVE = 8385;
            public const int ERROR_DS_EXISTS_IN_MAY_HAVE = 8386;
            public const int ERROR_DS_NONEXISTENT_MAY_HAVE = 8387;
            public const int ERROR_DS_NONEXISTENT_MUST_HAVE = 8388;
            public const int ERROR_DS_AUX_CLS_TEST_FAIL = 8389;
            public const int ERROR_DS_NONEXISTENT_POSS_SUP = 8390;
            public const int ERROR_DS_SUB_CLS_TEST_FAIL = 8391;
            public const int ERROR_DS_BAD_RDN_ATT_ID_SYNTAX = 8392;
            public const int ERROR_DS_EXISTS_IN_AUX_CLS = 8393;
            public const int ERROR_DS_EXISTS_IN_SUB_CLS = 8394;
            public const int ERROR_DS_EXISTS_IN_POSS_SUP = 8395;
            public const int ERROR_DS_RECALCSCHEMA_FAILED = 8396;
            public const int ERROR_DS_TREE_DELETE_NOT_FINISHED = 8397;
            public const int ERROR_DS_CANT_DELETE = 8398;
            public const int ERROR_DS_ATT_SCHEMA_REQ_ID = 8399;
            public const int ERROR_DS_BAD_ATT_SCHEMA_SYNTAX = 8400;
            public const int ERROR_DS_CANT_CACHE_ATT = 8401;
            public const int ERROR_DS_CANT_CACHE_CLASS = 8402;
            public const int ERROR_DS_CANT_REMOVE_ATT_CACHE = 8403;
            public const int ERROR_DS_CANT_REMOVE_CLASS_CACHE = 8404;
            public const int ERROR_DS_CANT_RETRIEVE_DN = 8405;
            public const int ERROR_DS_MISSING_SUPREF = 8406;
            public const int ERROR_DS_CANT_RETRIEVE_INSTANCE = 8407;
            public const int ERROR_DS_CODE_INCONSISTENCY = 8408;
            public const int ERROR_DS_DATABASE_ERROR = 8409;
            public const int ERROR_DS_GOVERNSID_MISSING = 8410;
            public const int ERROR_DS_MISSING_EXPECTED_ATT = 8411;
            public const int ERROR_DS_NCNAME_MISSING_CR_REF = 8412;
            public const int ERROR_DS_SECURITY_CHECKING_ERROR = 8413;
            public const int ERROR_DS_SCHEMA_NOT_LOADED = 8414;
            public const int ERROR_DS_SCHEMA_ALLOC_FAILED = 8415;
            public const int ERROR_DS_ATT_SCHEMA_REQ_SYNTAX = 8416;
            public const int ERROR_DS_GCVERIFY_ERROR = 8417;
            public const int ERROR_DS_DRA_SCHEMA_MISMATCH = 8418;
            public const int ERROR_DS_CANT_FIND_DSA_OBJ = 8419;
            public const int ERROR_DS_CANT_FIND_EXPECTED_NC = 8420;
            public const int ERROR_DS_CANT_FIND_NC_IN_CACHE = 8421;
            public const int ERROR_DS_CANT_RETRIEVE_CHILD = 8422;
            public const int ERROR_DS_SECURITY_ILLEGAL_MODIFY = 8423;
            public const int ERROR_DS_CANT_REPLACE_HIDDEN_REC = 8424;
            public const int ERROR_DS_BAD_HIERARCHY_FILE = 8425;
            public const int ERROR_DS_BUILD_HIERARCHY_TABLE_FAILED = 8426;
            public const int ERROR_DS_CONFIG_PARAM_MISSING = 8427;
            public const int ERROR_DS_COUNTING_AB_INDICES_FAILED = 8428;
            public const int ERROR_DS_HIERARCHY_TABLE_MALLOC_FAILED = 8429;
            public const int ERROR_DS_INTERNAL_FAILURE = 8430;
            public const int ERROR_DS_UNKNOWN_ERROR = 8431;
            public const int ERROR_DS_ROOT_REQUIRES_CLASS_TOP = 8432;
            public const int ERROR_DS_REFUSING_FSMO_ROLES = 8433;
            public const int ERROR_DS_MISSING_FSMO_SETTINGS = 8434;
            public const int ERROR_DS_UNABLE_TO_SURRENDER_ROLES = 8435;
            public const int ERROR_DS_DRA_GENERIC = 8436;
            public const int ERROR_DS_DRA_INVALID_PARAMETER = 8437;
            public const int ERROR_DS_DRA_BUSY = 8438;
            public const int ERROR_DS_DRA_BAD_DN = 8439;
            public const int ERROR_DS_DRA_BAD_NC = 8440;
            public const int ERROR_DS_DRA_DN_EXISTS = 8441;
            public const int ERROR_DS_DRA_INTERNAL_ERROR = 8442;
            public const int ERROR_DS_DRA_INCONSISTENT_DIT = 8443;
            public const int ERROR_DS_DRA_CONNECTION_FAILED = 8444;
            public const int ERROR_DS_DRA_BAD_INSTANCE_TYPE = 8445;
            public const int ERROR_DS_DRA_OUT_OF_MEM = 8446;
            public const int ERROR_DS_DRA_MAIL_PROBLEM = 8447;
            public const int ERROR_DS_DRA_REF_ALREADY_EXISTS = 8448;
            public const int ERROR_DS_DRA_REF_NOT_FOUND = 8449;
            public const int ERROR_DS_DRA_OBJ_IS_REP_SOURCE = 8450;
            public const int ERROR_DS_DRA_DB_ERROR = 8451;
            public const int ERROR_DS_DRA_NO_REPLICA = 8452;
            public const int ERROR_DS_DRA_ACCESS_DENIED = 8453;
            public const int ERROR_DS_DRA_NOT_SUPPORTED = 8454;
            public const int ERROR_DS_DRA_RPC_CANCELLED = 8455;
            public const int ERROR_DS_DRA_SOURCE_DISABLED = 8456;
            public const int ERROR_DS_DRA_SINK_DISABLED = 8457;
            public const int ERROR_DS_DRA_NAME_COLLISION = 8458;
            public const int ERROR_DS_DRA_SOURCE_REINSTALLED = 8459;
            public const int ERROR_DS_DRA_MISSING_PARENT = 8460;
            public const int ERROR_DS_DRA_PREEMPTED = 8461;
            public const int ERROR_DS_DRA_ABANDON_SYNC = 8462;
            public const int ERROR_DS_DRA_SHUTDOWN = 8463;
            public const int ERROR_DS_DRA_INCOMPATIBLE_PARTIAL_SET = 8464;
            public const int ERROR_DS_DRA_SOURCE_IS_PARTIAL_REPLICA = 8465;
            public const int ERROR_DS_DRA_EXTN_CONNECTION_FAILED = 8466;
            public const int ERROR_DS_INSTALL_SCHEMA_MISMATCH = 8467;
            public const int ERROR_DS_DUP_LINK_ID = 8468;
            public const int ERROR_DS_NAME_ERROR_RESOLVING = 8469;
            public const int ERROR_DS_NAME_ERROR_NOT_FOUND = 8470;
            public const int ERROR_DS_NAME_ERROR_NOT_UNIQUE = 8471;
            public const int ERROR_DS_NAME_ERROR_NO_MAPPING = 8472;
            public const int ERROR_DS_NAME_ERROR_DOMAIN_ONLY = 8473;
            public const int ERROR_DS_NAME_ERROR_NO_SYNTACTICAL_MAPPING = 8474;
            public const int ERROR_DS_CONSTRUCTED_ATT_MOD = 8475;
            public const int ERROR_DS_WRONG_OM_OBJ_CLASS = 8476;
            public const int ERROR_DS_DRA_REPL_PENDING = 8477;
            public const int ERROR_DS_DS_REQUIRED = 8478;
            public const int ERROR_DS_INVALID_LDAP_DISPLAY_NAME = 8479;
            public const int ERROR_DS_NON_BASE_SEARCH = 8480;
            public const int ERROR_DS_CANT_RETRIEVE_ATTS = 8481;
            public const int ERROR_DS_BACKLINK_WITHOUT_LINK = 8482;
            public const int ERROR_DS_EPOCH_MISMATCH = 8483;
            public const int ERROR_DS_SRC_NAME_MISMATCH = 8484;
            public const int ERROR_DS_SRC_AND_DST_NC_IDENTICAL = 8485;
            public const int ERROR_DS_DST_NC_MISMATCH = 8486;
            public const int ERROR_DS_NOT_AUTHORITIVE_FOR_DST_NC = 8487;
            public const int ERROR_DS_SRC_GUID_MISMATCH = 8488;
            public const int ERROR_DS_CANT_MOVE_DELETED_OBJECT = 8489;
            public const int ERROR_DS_PDC_OPERATION_IN_PROGRESS = 8490;
            public const int ERROR_DS_CROSS_DOMAIN_CLEANUP_REQD = 8491;
            public const int ERROR_DS_ILLEGAL_XDOM_MOVE_OPERATION = 8492;
            public const int ERROR_DS_CANT_WITH_ACCT_GROUP_MEMBERSHPS = 8493;
            public const int ERROR_DS_NC_MUST_HAVE_NC_PARENT = 8494;
            public const int ERROR_DS_CR_IMPOSSIBLE_TO_VALIDATE = 8495;
            public const int ERROR_DS_DST_DOMAIN_NOT_NATIVE = 8496;
            public const int ERROR_DS_MISSING_INFRASTRUCTURE_CONTAINER = 8497;
            public const int ERROR_DS_CANT_MOVE_ACCOUNT_GROUP = 8498;
            public const int ERROR_DS_CANT_MOVE_RESOURCE_GROUP = 8499;
            public const int ERROR_DS_INVALID_SEARCH_FLAG = 8500;
            public const int ERROR_DS_NO_TREE_DELETE_ABOVE_NC = 8501;
            public const int ERROR_DS_COULDNT_LOCK_TREE_FOR_DELETE = 8502;
            public const int ERROR_DS_COULDNT_IDENTIFY_OBJECTS_FOR_TREE_DELETE = 8503;
            public const int ERROR_DS_SAM_INIT_FAILURE = 8504;
            public const int ERROR_DS_SENSITIVE_GROUP_VIOLATION = 8505;
            public const int ERROR_DS_CANT_MOD_PRIMARYGROUPID = 8506;
            public const int ERROR_DS_ILLEGAL_BASE_SCHEMA_MOD = 8507;
            public const int ERROR_DS_NONSAFE_SCHEMA_CHANGE = 8508;
            public const int ERROR_DS_SCHEMA_UPDATE_DISALLOWED = 8509;
            public const int ERROR_DS_CANT_CREATE_UNDER_SCHEMA = 8510;
            public const int ERROR_DS_INSTALL_NO_SRC_SCH_VERSION = 8511;
            public const int ERROR_DS_INSTALL_NO_SCH_VERSION_IN_INIFILE = 8512;
            public const int ERROR_DS_INVALID_GROUP_TYPE = 8513;
            public const int ERROR_DS_NO_NEST_GLOBALGROUP_IN_MIXEDDOMAIN = 8514;
            public const int ERROR_DS_NO_NEST_LOCALGROUP_IN_MIXEDDOMAIN = 8515;
            public const int ERROR_DS_GLOBAL_CANT_HAVE_LOCAL_MEMBER = 8516;
            public const int ERROR_DS_GLOBAL_CANT_HAVE_UNIVERSAL_MEMBER = 8517;
            public const int ERROR_DS_UNIVERSAL_CANT_HAVE_LOCAL_MEMBER = 8518;
            public const int ERROR_DS_GLOBAL_CANT_HAVE_CROSSDOMAIN_MEMBER = 8519;
            public const int ERROR_DS_LOCAL_CANT_HAVE_CROSSDOMAIN_LOCAL_MEMBER = 8520;
            public const int ERROR_DS_HAVE_PRIMARY_MEMBERS = 8521;
            public const int ERROR_DS_STRING_SD_CONVERSION_FAILED = 8522;
            public const int ERROR_DS_NAMING_MASTER_GC = 8523;
            public const int ERROR_DS_DNS_LOOKUP_FAILURE = 8524;
            public const int ERROR_DS_COULDNT_UPDATE_SPNS = 8525;
            public const int ERROR_DS_CANT_RETRIEVE_SD = 8526;
            public const int ERROR_DS_KEY_NOT_UNIQUE = 8527;
            public const int ERROR_DS_WRONG_LINKED_ATT_SYNTAX = 8528;
            public const int ERROR_DS_SAM_NEED_BOOTKEY_PASSUInt16 = 8529;
            public const int ERROR_DS_SAM_NEED_BOOTKEY_FLOPPY = 8530;
            public const int ERROR_DS_CANT_START = 8531;
            public const int ERROR_DS_INIT_FAILURE = 8532;
            public const int ERROR_DS_NO_PKT_PRIVACY_ON_CONNECTION = 8533;
            public const int ERROR_DS_SOURCE_DOMAIN_IN_FOREST = 8534;
            public const int ERROR_DS_DESTINATION_DOMAIN_NOT_IN_FOREST = 8535;
            public const int ERROR_DS_DESTINATION_AUDITING_NOT_ENABLED = 8536;
            public const int ERROR_DS_CANT_FIND_DC_FOR_SRC_DOMAIN = 8537;
            public const int ERROR_DS_SRC_OBJ_NOT_GROUP_OR_USER = 8538;
            public const int ERROR_DS_SRC_SID_EXISTS_IN_FOREST = 8539;
            public const int ERROR_DS_SRC_AND_DST_OBJECT_CLASS_MISMATCH = 8540;
            public const int ERROR_SAM_INIT_FAILURE = 8541;
            public const int ERROR_DS_DRA_SCHEMA_INFO_SHIP = 8542;
            public const int ERROR_DS_DRA_SCHEMA_CONFLICT = 8543;
            public const int ERROR_DS_DRA_EARLIER_SCHEMA_CONFLICT = 8544;
            public const int ERROR_DS_DRA_OBJ_NC_MISMATCH = 8545;
            public const int ERROR_DS_NC_STILL_HAS_DSAS = 8546;
            public const int ERROR_DS_GC_REQUIRED = 8547;
            public const int ERROR_DS_LOCAL_MEMBER_OF_LOCAL_ONLY = 8548;
            public const int ERROR_DS_NO_FPO_IN_UNIVERSAL_GROUPS = 8549;
            public const int ERROR_DS_CANT_ADD_TO_GC = 8550;
            public const int ERROR_DS_NO_CHECKPOINT_WITH_PDC = 8551;
            public const int ERROR_DS_SOURCE_AUDITING_NOT_ENABLED = 8552;
            public const int ERROR_DS_CANT_CREATE_IN_NONDOMAIN_NC = 8553;
            public const int ERROR_DS_INVALID_NAME_FOR_SPN = 8554;
            public const int ERROR_DS_FILTER_USES_CONTRUCTED_ATTRS = 8555;
            public const int ERROR_DS_UNICODEPWD_NOT_IN_QUOTES = 8556;
            public const int ERROR_DS_MACHINE_ACCOUNT_QUOTA_EXCEEDED = 8557;
            public const int ERROR_DS_MUST_BE_RUN_ON_DST_DC = 8558;
            public const int ERROR_DS_SRC_DC_MUST_BE_SP4_OR_GREATER = 8559;
            public const int ERROR_DS_CANT_TREE_DELETE_CRITICAL_OBJ = 8560;
            public const int ERROR_DS_INIT_FAILURE_CONSOLE = 8561;
            public const int ERROR_DS_SAM_INIT_FAILURE_CONSOLE = 8562;
            public const int ERROR_DS_FOREST_VERSION_TOO_HIGH = 8563;
            public const int ERROR_DS_DOMAIN_VERSION_TOO_HIGH = 8564;
            public const int ERROR_DS_FOREST_VERSION_TOO_LOW = 8565;
            public const int ERROR_DS_DOMAIN_VERSION_TOO_LOW = 8566;
            public const int ERROR_DS_INCOMPATIBLE_VERSION = 8567;
            public const int ERROR_DS_LOW_DSA_VERSION = 8568;
            public const int ERROR_DS_NO_BEHAVIOR_VERSION_IN_MIXEDDOMAIN = 8569;
            public const int ERROR_DS_NOT_SUPPORTED_SORT_ORDER = 8570;
            public const int ERROR_DS_NAME_NOT_UNIQUE = 8571;
            public const int ERROR_DS_MACHINE_ACCOUNT_CREATED_PRENT4 = 8572;
            public const int ERROR_DS_OUT_OF_VERSION_STORE = 8573;
            public const int ERROR_DS_INCOMPATIBLE_CONTROLS_USED = 8574;
            public const int ERROR_DS_NO_REF_DOMAIN = 8575;
            public const int ERROR_DS_RESERVED_LINK_ID = 8576;
            public const int ERROR_DS_LINK_ID_NOT_AVAILABLE = 8577;
            public const int ERROR_DS_AG_CANT_HAVE_UNIVERSAL_MEMBER = 8578;
            public const int ERROR_DS_MODIFYDN_DISALLOWED_BY_INSTANCE_TYPE = 8579;
            public const int ERROR_DS_NO_OBJECT_MOVE_IN_SCHEMA_NC = 8580;
            public const int ERROR_DS_MODIFYDN_DISALLOWED_BY_FLAG = 8581;
            public const int ERROR_DS_MODIFYDN_WRONG_GRANDPARENT = 8582;
            public const int ERROR_DS_NAME_ERROR_TRUST_REFERRAL = 8583;
            public const int ERROR_NOT_SUPPORTED_ON_STANDARD_SERVER = 8584;
            public const int ERROR_DS_CANT_ACCESS_REMOTE_PART_OF_AD = 8585;
            public const int ERROR_DS_CR_IMPOSSIBLE_TO_VALIDATE_V2 = 8586;
            public const int ERROR_DS_THREAD_LIMIT_EXCEEDED = 8587;
            public const int ERROR_DS_NOT_CLOSEST = 8588;
            public const int ERROR_DS_CANT_DERIVE_SPN_WITHOUT_SERVER_REF = 8589;
            public const int ERROR_DS_SINGLE_USER_MODE_FAILED = 8590;
            public const int ERROR_DS_NTDSCRIPT_SYNTAX_ERROR = 8591;
            public const int ERROR_DS_NTDSCRIPT_PROCESS_ERROR = 8592;
            public const int ERROR_DS_DIFFERENT_REPL_EPOCHS = 8593;
            public const int ERROR_DS_DRS_EXTENSIONS_CHANGED = 8594;
            public const int ERROR_DS_REPLICA_SET_CHANGE_NOT_ALLOWED_ON_DISABLED_CR = 8595;
            public const int ERROR_DS_NO_MSDS_INTID = 8596;
            public const int ERROR_DS_DUP_MSDS_INTID = 8597;
            public const int ERROR_DS_EXISTS_IN_RDNATTID = 8598;
            public const int ERROR_DS_AUTHORIZATION_FAILED = 8599;
            public const int ERROR_DS_INVALID_SCRIPT = 8600;
            public const int ERROR_DS_REMOTE_CROSSREF_OP_FAILED = 8601;
            public const int ERROR_DS_CROSS_REF_BUSY = 8602;
            public const int ERROR_DS_CANT_DERIVE_SPN_FOR_DELETED_DOMAIN = 8603;
            public const int ERROR_DS_CANT_DEMOTE_WITH_WRITEABLE_NC = 8604;
            public const int ERROR_DS_DUPLICATE_ID_FOUND = 8605;
            public const int ERROR_DS_INSUFFICIENT_ATTR_TO_CREATE_OBJECT = 8606;
            public const int ERROR_DS_GROUP_CONVERSION_ERROR = 8607;
            public const int ERROR_DS_CANT_MOVE_APP_BASIC_GROUP = 8608;
            public const int ERROR_DS_CANT_MOVE_APP_QUERY_GROUP = 8609;
            public const int ERROR_DS_ROLE_NOT_VERIFIED = 8610;
            public const int ERROR_DS_WKO_CONTAINER_CANNOT_BE_SPECIAL = 8611;
            public const int ERROR_DS_DOMAIN_RENAME_IN_PROGRESS = 8612;
            public const int ERROR_DS_EXISTING_AD_CHILD_NC = 8613;
            public const int ERROR_DS_REPL_LIFETIME_EXCEEDED = 8614;
            public const int ERROR_DS_DISALLOWED_IN_SYSTEM_CONTAINER = 8615;
            public const int ERROR_DS_LDAP_SEND_QUEUE_FULL = 8616;
            public const int DNS_ERROR_RESPONSE_CODES_BASE = 9000;
            public const int DNS_ERROR_RCODE_FORMAT_ERROR = 9001;
            public const int DNS_ERROR_RCODE_SERVER_FAILURE = 9002;
            public const int DNS_ERROR_RCODE_NAME_ERROR = 9003;
            public const int DNS_ERROR_RCODE_NOT_IMPLEMENTED = 9004;
            public const int DNS_ERROR_RCODE_REFUSED = 9005;
            public const int DNS_ERROR_RCODE_YXDOMAIN = 9006;
            public const int DNS_ERROR_RCODE_YXRRSET = 9007;
            public const int DNS_ERROR_RCODE_NXRRSET = 9008;
            public const int DNS_ERROR_RCODE_NOTAUTH = 9009;
            public const int DNS_ERROR_RCODE_NOTZONE = 9010;
            public const int DNS_ERROR_RCODE_BADSIG = 9016;
            public const int DNS_ERROR_RCODE_BADKEY = 9017;
            public const int DNS_ERROR_RCODE_BADTIME = 9018;
            public const int DNS_ERROR_PACKET_FMT_BASE = 9500;
            public const int DNS_INFO_NO_RECORDS = 9501;
            public const int DNS_ERROR_BAD_PACKET = 9502;
            public const int DNS_ERROR_NO_PACKET = 9503;
            public const int DNS_ERROR_RCODE = 9504;
            public const int DNS_ERROR_UNSECURE_PACKET = 9505;
            public const int DNS_ERROR_NO_MEMORY = ERROR_OUTOFMEMORY;
            public const int DNS_ERROR_INVALID_NAME = ERROR_INVALID_NAME;
            public const int DNS_ERROR_INVALID_DATA = ERROR_INVALID_DATA;
            public const int DNS_ERROR_GENERAL_API_BASE = 9550;
            public const int DNS_ERROR_INVALID_TYPE = 9551;
            public const int DNS_ERROR_INVALID_IP_ADDRESS = 9552;
            public const int DNS_ERROR_INVALID_PROPERTY = 9553;
            public const int DNS_ERROR_TRY_AGAIN_LATER = 9554;
            public const int DNS_ERROR_NOT_UNIQUE = 9555;
            public const int DNS_ERROR_NON_RFC_NAME = 9556;
            public const int DNS_STATUS_FQDN = 9557;
            public const int DNS_STATUS_DOTTED_NAME = 9558;
            public const int DNS_STATUS_SINGLE_PART_NAME = 9559;
            public const int DNS_ERROR_INVALID_NAME_CHAR = 9560;
            public const int DNS_ERROR_NUMERIC_NAME = 9561;
            public const int DNS_ERROR_NOT_ALLOWED_ON_ROOT_SERVER = 9562;
            public const int DNS_ERROR_NOT_ALLOWED_UNDER_DELEGATION = 9563;
            public const int DNS_ERROR_CANNOT_FIND_ROOT_HINTS = 9564;
            public const int DNS_ERROR_INCONSISTENT_ROOT_HINTS = 9565;
            public const int DNS_ERROR_ZONE_BASE = 9600;
            public const int DNS_ERROR_ZONE_DOES_NOT_EXIST = 9601;
            public const int DNS_ERROR_NO_ZONE_INFO = 9602;
            public const int DNS_ERROR_INVALID_ZONE_OPERATION = 9603;
            public const int DNS_ERROR_ZONE_CONFIGURATION_ERROR = 9604;
            public const int DNS_ERROR_ZONE_HAS_NO_SOA_RECORD = 9605;
            public const int DNS_ERROR_ZONE_HAS_NO_NS_RECORDS = 9606;
            public const int DNS_ERROR_ZONE_LOCKED = 9607;
            public const int DNS_ERROR_ZONE_CREATION_FAILED = 9608;
            public const int DNS_ERROR_ZONE_ALREADY_EXISTS = 9609;
            public const int DNS_ERROR_AUTOZONE_ALREADY_EXISTS = 9610;
            public const int DNS_ERROR_INVALID_ZONE_TYPE = 9611;
            public const int DNS_ERROR_SECONDARY_REQUIRES_MASTER_IP = 9612;
            public const int DNS_ERROR_ZONE_NOT_SECONDARY = 9613;
            public const int DNS_ERROR_NEED_SECONDARY_ADDRESSES = 9614;
            public const int DNS_ERROR_WINS_INIT_FAILED = 9615;
            public const int DNS_ERROR_NEED_WINS_SERVERS = 9616;
            public const int DNS_ERROR_NBSTAT_INIT_FAILED = 9617;
            public const int DNS_ERROR_SOA_DELETE_INVALID = 9618;
            public const int DNS_ERROR_FORWARDER_ALREADY_EXISTS = 9619;
            public const int DNS_ERROR_ZONE_REQUIRES_MASTER_IP = 9620;
            public const int DNS_ERROR_ZONE_IS_SHUTDOWN = 9621;
            public const int DNS_ERROR_DATAFILE_BASE = 9650;
            public const int DNS_ERROR_PRIMARY_REQUIRES_DATAFILE = 9651;
            public const int DNS_ERROR_INVALID_DATAFILE_NAME = 9652;
            public const int DNS_ERROR_DATAFILE_OPEN_FAILURE = 9653;
            public const int DNS_ERROR_FILE_WRITEBACK_FAILED = 9654;
            public const int DNS_ERROR_DATAFILE_PARSING = 9655;
            public const int DNS_ERROR_DATABASE_BASE = 9700;
            public const int DNS_ERROR_RECORD_DOES_NOT_EXIST = 9701;
            public const int DNS_ERROR_RECORD_FORMAT = 9702;
            public const int DNS_ERROR_NODE_CREATION_FAILED = 9703;
            public const int DNS_ERROR_UNKNOWN_RECORD_TYPE = 9704;
            public const int DNS_ERROR_RECORD_TIMED_OUT = 9705;
            public const int DNS_ERROR_NAME_NOT_IN_ZONE = 9706;
            public const int DNS_ERROR_CNAME_LOOP = 9707;
            public const int DNS_ERROR_NODE_IS_CNAME = 9708;
            public const int DNS_ERROR_CNAME_COLLISION = 9709;
            public const int DNS_ERROR_RECORD_ONLY_AT_ZONE_ROOT = 9710;
            public const int DNS_ERROR_RECORD_ALREADY_EXISTS = 9711;
            public const int DNS_ERROR_SECONDARY_DATA = 9712;
            public const int DNS_ERROR_NO_CREATE_CACHE_DATA = 9713;
            public const int DNS_ERROR_NAME_DOES_NOT_EXIST = 9714;
            public const int DNS_WARNING_PTR_CREATE_FAILED = 9715;
            public const int DNS_WARNING_DOMAIN_UNDELETED = 9716;
            public const int DNS_ERROR_DS_UNAVAILABLE = 9717;
            public const int DNS_ERROR_DS_ZONE_ALREADY_EXISTS = 9718;
            public const int DNS_ERROR_NO_BOOTFILE_IF_DS_ZONE = 9719;
            public const int DNS_ERROR_OPERATION_BASE = 9750;
            public const int DNS_INFO_AXFR_COMPLETE = 9751;
            public const int DNS_ERROR_AXFR = 9752;
            public const int DNS_INFO_ADDED_LOCAL_WINS = 9753;
            public const int DNS_ERROR_SECURE_BASE = 9800;
            public const int DNS_STATUS_CONTINUE_NEEDED = 9801;
            public const int DNS_ERROR_SETUP_BASE = 9850;
            public const int DNS_ERROR_NO_TCPIP = 9851;
            public const int DNS_ERROR_NO_DNS_SERVERS = 9852;
            public const int DNS_ERROR_DP_BASE = 9900;
            public const int DNS_ERROR_DP_DOES_NOT_EXIST = 9901;
            public const int DNS_ERROR_DP_ALREADY_EXISTS = 9902;
            public const int DNS_ERROR_DP_NOT_ENLISTED = 9903;
            public const int DNS_ERROR_DP_ALREADY_ENLISTED = 9904;
            public const int DNS_ERROR_DP_NOT_AVAILABLE = 9905;
            public const int WSABASEERR = 10000;
            public const int WSAEINTR = 10004;
            public const int WSAEBADF = 10009;
            public const int WSAEACCES = 10013;
            public const int WSAEFAULT = 10014;
            public const int WSAEINVAL = 10022;
            public const int WSAEMFILE = 10024;
            public const int WSAEWOULDBLOCK = 10035;
            public const int WSAEINPROGRESS = 10036;
            public const int WSAEALREADY = 10037;
            public const int WSAENOTSOCK = 10038;
            public const int WSAEDESTADDRREQ = 10039;
            public const int WSAEMSGSIZE = 10040;
            public const int WSAEPROTOTYPE = 10041;
            public const int WSAENOPROTOOPT = 10042;
            public const int WSAEPROTONOSUPPORT = 10043;
            public const int WSAESOCKTNOSUPPORT = 10044;
            public const int WSAEOPNOTSUPP = 10045;
            public const int WSAEPFNOSUPPORT = 10046;
            public const int WSAEAFNOSUPPORT = 10047;
            public const int WSAEADDRINUSE = 10048;
            public const int WSAEADDRNOTAVAIL = 10049;
            public const int WSAENETDOWN = 10050;
            public const int WSAENETUNREACH = 10051;
            public const int WSAENETRESET = 10052;
            public const int WSAECONNABORTED = 10053;
            public const int WSAECONNRESET = 10054;
            public const int WSAENOBUFS = 10055;
            public const int WSAEISCONN = 10056;
            public const int WSAENOTCONN = 10057;
            public const int WSAESHUTDOWN = 10058;
            public const int WSAETOOMANYREFS = 10059;
            public const int WSAETIMEDOUT = 10060;
            public const int WSAECONNREFUSED = 10061;
            public const int WSAELOOP = 10062;
            public const int WSAENAMETOOInt32 = 10063;
            public const int WSAEHOSTDOWN = 10064;
            public const int WSAEHOSTUNREACH = 10065;
            public const int WSAENOTEMPTY = 10066;
            public const int WSAEPROCLIM = 10067;
            public const int WSAEUSERS = 10068;
            public const int WSAEDQUOT = 10069;
            public const int WSAESTALE = 10070;
            public const int WSAEREMOTE = 10071;
            public const int WSASYSNOTREADY = 10091;
            public const int WSAVERNOTSUPPORTED = 10092;
            public const int WSANOTINITIALISED = 10093;
            public const int WSAEDISCON = 10101;
            public const int WSAENOMORE = 10102;
            public const int WSAECANCELLED = 10103;
            public const int WSAEINVALIDPROCTABLE = 10104;
            public const int WSAEINVALIDPROVIDER = 10105;
            public const int WSAEPROVIDERFAILEDINIT = 10106;
            public const int WSASYSCALLFAILURE = 10107;
            public const int WSASERVICE_NOT_FOUND = 10108;
            public const int WSATYPE_NOT_FOUND = 10109;
            public const int WSA_E_NO_MORE = 10110;
            public const int WSA_E_CANCELLED = 10111;
            public const int WSAEREFUSED = 10112;
            public const int WSAHOST_NOT_FOUND = 11001;
            public const int WSATRY_AGAIN = 11002;
            public const int WSANO_RECOVERY = 11003;
            public const int WSANO_DATA = 11004;
            public const int WSA_QOS_RECEIVERS = 11005;
            public const int WSA_QOS_SENDERS = 11006;
            public const int WSA_QOS_NO_SENDERS = 11007;
            public const int WSA_QOS_NO_RECEIVERS = 11008;
            public const int WSA_QOS_REQUEST_CONFIRMED = 11009;
            public const int WSA_QOS_ADMISSION_FAILURE = 11010;
            public const int WSA_QOS_POLICY_FAILURE = 11011;
            public const int WSA_QOS_BAD_STYLE = 11012;
            public const int WSA_QOS_BAD_OBJECT = 11013;
            public const int WSA_QOS_TRAFFIC_CTRL_ERROR = 11014;
            public const int WSA_QOS_GENERIC_ERROR = 11015;
            public const int WSA_QOS_ESERVICETYPE = 11016;
            public const int WSA_QOS_EFLOWSPEC = 11017;
            public const int WSA_QOS_EPROVSPECBUF = 11018;
            public const int WSA_QOS_EFILTERSTYLE = 11019;
            public const int WSA_QOS_EFILTERTYPE = 11020;
            public const int WSA_QOS_EFILTERCOUNT = 11021;
            public const int WSA_QOS_EOBJLENGTH = 11022;
            public const int WSA_QOS_EFLOWCOUNT = 11023;
            public const int WSA_QOS_EUNKOWNPSOBJ = 11024;
            public const int WSA_QOS_EPOLICYOBJ = 11025;
            public const int WSA_QOS_EFLOWDESC = 11026;
            public const int WSA_QOS_EPSFLOWSPEC = 11027;
            public const int WSA_QOS_EPSFILTERSPEC = 11028;
            public const int WSA_QOS_ESDMODEOBJ = 11029;
            public const int WSA_QOS_ESHAPERATEOBJ = 11030;
            public const int WSA_QOS_RESERVED_PETYPE = 11031;
            public const int ERROR_SXS_SECTION_NOT_FOUND = 14000;
            public const int ERROR_SXS_CANT_GEN_ACTCTX = 14001;
            public const int ERROR_SXS_INVALID_ACTCTXDATA_FORMAT = 14002;
            public const int ERROR_SXS_ASSEMBLY_NOT_FOUND = 14003;
            public const int ERROR_SXS_MANIFEST_FORMAT_ERROR = 14004;
            public const int ERROR_SXS_MANIFEST_PARSE_ERROR = 14005;
            public const int ERROR_SXS_ACTIVATION_CONTEXT_DISABLED = 14006;
            public const int ERROR_SXS_KEY_NOT_FOUND = 14007;
            public const int ERROR_SXS_VERSION_CONFLICT = 14008;
            public const int ERROR_SXS_WRONG_SECTION_TYPE = 14009;
            public const int ERROR_SXS_THREAD_QUERIES_DISABLED = 14010;
            public const int ERROR_SXS_PROCESS_DEFAULT_ALREADY_SET = 14011;
            public const int ERROR_SXS_UNKNOWN_ENCODING_GROUP = 14012;
            public const int ERROR_SXS_UNKNOWN_ENCODING = 14013;
            public const int ERROR_SXS_INVALID_XML_NAMESPACE_URI = 14014;
            public const int ERROR_SXS_ROOT_MANIFEST_DEPENDENCY_NOT_INSTALLED = 14015;
            public const int ERROR_SXS_LEAF_MANIFEST_DEPENDENCY_NOT_INSTALLED = 14016;
            public const int ERROR_SXS_INVALID_ASSEMBLY_IDENTITY_ATTRIBUTE = 14017;
            public const int ERROR_SXS_MANIFEST_MISSING_REQUIRED_DEFAULT_NAMESPACE = 14018;
            public const int ERROR_SXS_MANIFEST_INVALID_REQUIRED_DEFAULT_NAMESPACE = 14019;
            public const int ERROR_SXS_PRIVATE_MANIFEST_CROSS_PATH_WITH_REPARSE_POINT = 14020;
            public const int ERROR_SXS_DUPLICATE_DLL_NAME = 14021;
            public const int ERROR_SXS_DUPLICATE_WINDOWCLASS_NAME = 14022;
            public const int ERROR_SXS_DUPLICATE_CLSID = 14023;
            public const int ERROR_SXS_DUPLICATE_IID = 14024;
            public const int ERROR_SXS_DUPLICATE_TLBID = 14025;
            public const int ERROR_SXS_DUPLICATE_PROGID = 14026;
            public const int ERROR_SXS_DUPLICATE_ASSEMBLY_NAME = 14027;
            public const int ERROR_SXS_FILE_HASH_MISMATCH = 14028;
            public const int ERROR_SXS_POLICY_PARSE_ERROR = 14029;
            public const int ERROR_SXS_XML_E_MISSINGQUOTE = 14030;
            public const int ERROR_SXS_XML_E_COMMENTSYNTAX = 14031;
            public const int ERROR_SXS_XML_E_BADSTARTNAMECHAR = 14032;
            public const int ERROR_SXS_XML_E_BADNAMECHAR = 14033;
            public const int ERROR_SXS_XML_E_BADCHARINSTRING = 14034;
            public const int ERROR_SXS_XML_E_XMLDECLSYNTAX = 14035;
            public const int ERROR_SXS_XML_E_BADCHARDATA = 14036;
            public const int ERROR_SXS_XML_E_MISSINGWHITESPACE = 14037;
            public const int ERROR_SXS_XML_E_EXPECTINGTAGEND = 14038;
            public const int ERROR_SXS_XML_E_MISSINGSEMICOLON = 14039;
            public const int ERROR_SXS_XML_E_UNBALANCEDPAREN = 14040;
            public const int ERROR_SXS_XML_E_INTERNALERROR = 14041;
            public const int ERROR_SXS_XML_E_UNEXPECTED_WHITESPACE = 14042;
            public const int ERROR_SXS_XML_E_INCOMPLETE_ENCODING = 14043;
            public const int ERROR_SXS_XML_E_MISSING_PAREN = 14044;
            public const int ERROR_SXS_XML_E_EXPECTINGCLOSEQUOTE = 14045;
            public const int ERROR_SXS_XML_E_MULTIPLE_COLONS = 14046;
            public const int ERROR_SXS_XML_E_INVALID_DECIMAL = 14047;
            public const int ERROR_SXS_XML_E_INVALID_HEXIDECIMAL = 14048;
            public const int ERROR_SXS_XML_E_INVALID_UNICODE = 14049;
            public const int ERROR_SXS_XML_E_WHITESPACEORQUESTIONMARK = 14050;
            public const int ERROR_SXS_XML_E_UNEXPECTEDENDTAG = 14051;
            public const int ERROR_SXS_XML_E_UNCLOSEDTAG = 14052;
            public const int ERROR_SXS_XML_E_DUPLICATEATTRIBUTE = 14053;
            public const int ERROR_SXS_XML_E_MULTIPLEROOTS = 14054;
            public const int ERROR_SXS_XML_E_INVALIDATROOTLEVEL = 14055;
            public const int ERROR_SXS_XML_E_BADXMLDECL = 14056;
            public const int ERROR_SXS_XML_E_MISSINGROOT = 14057;
            public const int ERROR_SXS_XML_E_UNEXPECTEDEOF = 14058;
            public const int ERROR_SXS_XML_E_BADPEREFINSUBSET = 14059;
            public const int ERROR_SXS_XML_E_UNCLOSEDSTARTTAG = 14060;
            public const int ERROR_SXS_XML_E_UNCLOSEDENDTAG = 14061;
            public const int ERROR_SXS_XML_E_UNCLOSEDSTRING = 14062;
            public const int ERROR_SXS_XML_E_UNCLOSEDCOMMENT = 14063;
            public const int ERROR_SXS_XML_E_UNCLOSEDDECL = 14064;
            public const int ERROR_SXS_XML_E_UNCLOSEDCDATA = 14065;
            public const int ERROR_SXS_XML_E_RESERVEDNAMESPACE = 14066;
            public const int ERROR_SXS_XML_E_INVALIDENCODING = 14067;
            public const int ERROR_SXS_XML_E_INVALIDSWITCH = 14068;
            public const int ERROR_SXS_XML_E_BADXMLCASE = 14069;
            public const int ERROR_SXS_XML_E_INVALID_STANDALONE = 14070;
            public const int ERROR_SXS_XML_E_UNEXPECTED_STANDALONE = 14071;
            public const int ERROR_SXS_XML_E_INVALID_VERSION = 14072;
            public const int ERROR_SXS_XML_E_MISSINGEQUALS = 14073;
            public const int ERROR_SXS_PROTECTION_RECOVERY_FAILED = 14074;
            public const int ERROR_SXS_PROTECTION_PUBLIC_KEY_TOO_Int16 = 14075;
            public const int ERROR_SXS_PROTECTION_CATALOG_NOT_VALID = 14076;
            public const int ERROR_SXS_UNTRANSLATABLE_HRESULT = 14077;
            public const int ERROR_SXS_PROTECTION_CATALOG_FILE_MISSING = 14078;
            public const int ERROR_SXS_MISSING_ASSEMBLY_IDENTITY_ATTRIBUTE = 14079;
            public const int ERROR_SXS_INVALID_ASSEMBLY_IDENTITY_ATTRIBUTE_NAME = 14080;
            public const int ERROR_IPSEC_QM_POLICY_EXISTS = 13000;
            public const int ERROR_IPSEC_QM_POLICY_NOT_FOUND = 13001;
            public const int ERROR_IPSEC_QM_POLICY_IN_USE = 13002;
            public const int ERROR_IPSEC_MM_POLICY_EXISTS = 13003;
            public const int ERROR_IPSEC_MM_POLICY_NOT_FOUND = 13004;
            public const int ERROR_IPSEC_MM_POLICY_IN_USE = 13005;
            public const int ERROR_IPSEC_MM_FILTER_EXISTS = 13006;
            public const int ERROR_IPSEC_MM_FILTER_NOT_FOUND = 13007;
            public const int ERROR_IPSEC_TRANSPORT_FILTER_EXISTS = 13008;
            public const int ERROR_IPSEC_TRANSPORT_FILTER_NOT_FOUND = 13009;
            public const int ERROR_IPSEC_MM_AUTH_EXISTS = 13010;
            public const int ERROR_IPSEC_MM_AUTH_NOT_FOUND = 13011;
            public const int ERROR_IPSEC_MM_AUTH_IN_USE = 13012;
            public const int ERROR_IPSEC_DEFAULT_MM_POLICY_NOT_FOUND = 13013;
            public const int ERROR_IPSEC_DEFAULT_MM_AUTH_NOT_FOUND = 13014;
            public const int ERROR_IPSEC_DEFAULT_QM_POLICY_NOT_FOUND = 13015;
            public const int ERROR_IPSEC_TUNNEL_FILTER_EXISTS = 13016;
            public const int ERROR_IPSEC_TUNNEL_FILTER_NOT_FOUND = 13017;
            public const int ERROR_IPSEC_MM_FILTER_PENDING_DELETION = 13018;
            public const int ERROR_IPSEC_TRANSPORT_FILTER_PENDING_DELETION = 13019;
            public const int ERROR_IPSEC_TUNNEL_FILTER_PENDING_DELETION = 13020;
            public const int ERROR_IPSEC_MM_POLICY_PENDING_DELETION = 13021;
            public const int ERROR_IPSEC_MM_AUTH_PENDING_DELETION = 13022;
            public const int ERROR_IPSEC_QM_POLICY_PENDING_DELETION = 13023;
            public const int WARNING_IPSEC_MM_POLICY_PRUNED = 13024;
            public const int WARNING_IPSEC_QM_POLICY_PRUNED = 13025;
            public const int ERROR_IPSEC_IKE_NEG_STATUS_BEGIN = 13800;
            public const int ERROR_IPSEC_IKE_AUTH_FAIL = 13801;
            public const int ERROR_IPSEC_IKE_ATTRIB_FAIL = 13802;
            public const int ERROR_IPSEC_IKE_NEGOTIATION_PENDING = 13803;
            public const int ERROR_IPSEC_IKE_GENERAL_PROCESSING_ERROR = 13804;
            public const int ERROR_IPSEC_IKE_TIMED_OUT = 13805;
            public const int ERROR_IPSEC_IKE_NO_CERT = 13806;
            public const int ERROR_IPSEC_IKE_SA_DELETED = 13807;
            public const int ERROR_IPSEC_IKE_SA_REAPED = 13808;
            public const int ERROR_IPSEC_IKE_MM_ACQUIRE_DROP = 13809;
            public const int ERROR_IPSEC_IKE_QM_ACQUIRE_DROP = 13810;
            public const int ERROR_IPSEC_IKE_QUEUE_DROP_MM = 13811;
            public const int ERROR_IPSEC_IKE_QUEUE_DROP_NO_MM = 13812;
            public const int ERROR_IPSEC_IKE_DROP_NO_RESPONSE = 13813;
            public const int ERROR_IPSEC_IKE_MM_DELAY_DROP = 13814;
            public const int ERROR_IPSEC_IKE_QM_DELAY_DROP = 13815;
            public const int ERROR_IPSEC_IKE_ERROR = 13816;
            public const int ERROR_IPSEC_IKE_CRL_FAILED = 13817;
            public const int ERROR_IPSEC_IKE_INVALID_KEY_USAGE = 13818;
            public const int ERROR_IPSEC_IKE_INVALID_CERT_TYPE = 13819;
            public const int ERROR_IPSEC_IKE_NO_PRIVATE_KEY = 13820;
            public const int ERROR_IPSEC_IKE_DH_FAIL = 13822;
            public const int ERROR_IPSEC_IKE_INVALID_HEADER = 13824;
            public const int ERROR_IPSEC_IKE_NO_POLICY = 13825;
            public const int ERROR_IPSEC_IKE_INVALID_SIGNATURE = 13826;
            public const int ERROR_IPSEC_IKE_KERBEROS_ERROR = 13827;
            public const int ERROR_IPSEC_IKE_NO_PUBLIC_KEY = 13828;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR = 13829;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_SA = 13830;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_PROP = 13831;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_TRANS = 13832;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_KE = 13833;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_ID = 13834;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_CERT = 13835;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_CERT_REQ = 13836;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_HASH = 13837;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_SIG = 13838;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_NONCE = 13839;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_NOTIFY = 13840;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_DELETE = 13841;
            public const int ERROR_IPSEC_IKE_PROCESS_ERR_VENDOR = 13842;
            public const int ERROR_IPSEC_IKE_INVALID_PAYLOAD = 13843;
            public const int ERROR_IPSEC_IKE_LOAD_SOFT_SA = 13844;
            public const int ERROR_IPSEC_IKE_SOFT_SA_TORN_DOWN = 13845;
            public const int ERROR_IPSEC_IKE_INVALID_COOKIE = 13846;
            public const int ERROR_IPSEC_IKE_NO_PEER_CERT = 13847;
            public const int ERROR_IPSEC_IKE_PEER_CRL_FAILED = 13848;
            public const int ERROR_IPSEC_IKE_POLICY_CHANGE = 13849;
            public const int ERROR_IPSEC_IKE_NO_MM_POLICY = 13850;
            public const int ERROR_IPSEC_IKE_NOTCBPRIV = 13851;
            public const int ERROR_IPSEC_IKE_SECLOADFAIL = 13852;
            public const int ERROR_IPSEC_IKE_FAILSSPINIT = 13853;
            public const int ERROR_IPSEC_IKE_FAILQUERYSSP = 13854;
            public const int ERROR_IPSEC_IKE_SRVACQFAIL = 13855;
            public const int ERROR_IPSEC_IKE_SRVQUERYCRED = 13856;
            public const int ERROR_IPSEC_IKE_GETSPIFAIL = 13857;
            public const int ERROR_IPSEC_IKE_INVALID_FILTER = 13858;
            public const int ERROR_IPSEC_IKE_OUT_OF_MEMORY = 13859;
            public const int ERROR_IPSEC_IKE_ADD_UPDATE_KEY_FAILED = 13860;
            public const int ERROR_IPSEC_IKE_INVALID_POLICY = 13861;
            public const int ERROR_IPSEC_IKE_UNKNOWN_DOI = 13862;
            public const int ERROR_IPSEC_IKE_INVALID_SITUATION = 13863;
            public const int ERROR_IPSEC_IKE_DH_FAILURE = 13864;
            public const int ERROR_IPSEC_IKE_INVALID_GROUP = 13865;
            public const int ERROR_IPSEC_IKE_ENCRYPT = 13866;
            public const int ERROR_IPSEC_IKE_DECRYPT = 13867;
            public const int ERROR_IPSEC_IKE_POLICY_MATCH = 13868;
            public const int ERROR_IPSEC_IKE_UNSUPPORTED_ID = 13869;
            public const int ERROR_IPSEC_IKE_INVALID_HASH = 13870;
            public const int ERROR_IPSEC_IKE_INVALID_HASH_ALG = 13871;
            public const int ERROR_IPSEC_IKE_INVALID_HASH_SIZE = 13872;
            public const int ERROR_IPSEC_IKE_INVALID_ENCRYPT_ALG = 13873;
            public const int ERROR_IPSEC_IKE_INVALID_AUTH_ALG = 13874;
            public const int ERROR_IPSEC_IKE_INVALID_SIG = 13875;
            public const int ERROR_IPSEC_IKE_LOAD_FAILED = 13876;
            public const int ERROR_IPSEC_IKE_RPC_DELETE = 13877;
            public const int ERROR_IPSEC_IKE_BENIGN_REINIT = 13878;
            public const int ERROR_IPSEC_IKE_INVALID_RESPONDER_LIFETIME_NOTIFY = 13879;
            public const int ERROR_IPSEC_IKE_INVALID_CERT_KEYLEN = 13881;
            public const int ERROR_IPSEC_IKE_MM_LIMIT = 13882;
            public const int ERROR_IPSEC_IKE_NEGOTIATION_DISABLED = 13883;
            public const int ERROR_IPSEC_IKE_NEG_STATUS_END = 13884;
        }

        public class ResultCom
        {
            public const int E_NOTIMPL = (int)(0x80000001 - 0x100000000);
            public const int E_OUTOFMEMORY = (int)(0x80000002 - 0x100000000);
            public const int E_INVALIDARG = (int)(0x80000003 - 0x100000000);
            public const int E_NOINTERFACE = (int)(0x80000004 - 0x100000000);
            public const int E_POINTER = (int)(0x80000005 - 0x100000000);
            public const int E_HANDLE = (int)(0x80000006 - 0x100000000);
            public const int E_ABORT = (int)(0x80000007 - 0x100000000);
            public const int E_FAIL = (int)(0x80000008 - 0x100000000);
            public const int E_ACCESSDENIED = (int)(0x80000009 - 0x100000000);
            public const int E_PENDING = (int)(0x8000000A - 0x100000000);
            public const int CO_E_INIT_TLS = (int)(0x80004006 - 0x100000000);
            public const int CO_E_INIT_SHARED_ALLOCATOR = (int)(0x80004007 - 0x100000000);
            public const int CO_E_INIT_MEMORY_ALLOCATOR = (int)(0x80004008 - 0x100000000);
            public const int CO_E_INIT_CLASS_CACHE = (int)(0x80004009 - 0x100000000);
            public const int CO_E_INIT_RPC_CHANNEL = (int)(0x8000400A - 0x100000000);
            public const int CO_E_INIT_TLS_SET_CHANNEL_CONTROL = (int)(0x8000400B - 0x100000000);
            public const int CO_E_INIT_TLS_CHANNEL_CONTROL = (int)(0x8000400C - 0x100000000);
            public const int CO_E_INIT_UNACCEPTED_USER_ALLOCATOR = (int)(0x8000400D - 0x100000000);
            public const int CO_E_INIT_SCM_MUTEX_EXISTS = (int)(0x8000400E - 0x100000000);
            public const int CO_E_INIT_SCM_FILE_MAPPING_EXISTS = (int)(0x8000400F - 0x100000000);
            public const int CO_E_INIT_SCM_MAP_VIEW_OF_FILE = (int)(0x80004010 - 0x100000000);
            public const int CO_E_INIT_SCM_EXEC_FAILURE = (int)(0x80004011 - 0x100000000);
            public const int CO_E_INIT_ONLY_SINGLE_THREADED = (int)(0x80004012 - 0x100000000);
            public const int CO_E_CANT_REMOTE = (int)(0x80004013 - 0x100000000);
            public const int CO_E_BAD_SERVER_NAME = (int)(0x80004014 - 0x100000000);
            public const int CO_E_WRONG_SERVER_IDENTITY = (int)(0x80004015 - 0x100000000);
            public const int CO_E_OLE1DDE_DISABLED = (int)(0x80004016 - 0x100000000);
            public const int CO_E_RUNAS_SYNTAX = (int)(0x80004017 - 0x100000000);
            public const int CO_E_CREATEPROCESS_FAILURE = (int)(0x80004018 - 0x100000000);
            public const int CO_E_RUNAS_CREATEPROCESS_FAILURE = (int)(0x80004019 - 0x100000000);
            public const int CO_E_RUNAS_LOGON_FAILURE = (int)(0x8000401A - 0x100000000);
            public const int CO_E_LAUNCH_PERMSSION_DENIED = (int)(0x8000401B - 0x100000000);
            public const int CO_E_START_SERVICE_FAILURE = (int)(0x8000401C - 0x100000000);
            public const int CO_E_REMOTE_COMMUNICATION_FAILURE = (int)(0x8000401D - 0x100000000);
            public const int CO_E_SERVER_START_TIMEOUT = (int)(0x8000401E - 0x100000000);
            public const int CO_E_CLSREG_INCONSISTENT = (int)(0x8000401F - 0x100000000);
            public const int CO_E_IIDREG_INCONSISTENT = (int)(0x80004020 - 0x100000000);
            public const int CO_E_NOT_SUPPORTED = (int)(0x80004021 - 0x100000000);
            public const int CO_E_RELOAD_DLL = (int)(0x80004022 - 0x100000000);
            public const int CO_E_MSI_ERROR = (int)(0x80004023 - 0x100000000);
            public const int CO_E_ATTEMPT_TO_CREATE_OUTSIDE_CLIENT_CONTEXT = (int)(0x80004024 - 0x100000000);
            public const int CO_E_SERVER_PAUSED = (int)(0x80004025 - 0x100000000);
            public const int CO_E_SERVER_NOT_PAUSED = (int)(0x80004026 - 0x100000000);
            public const int CO_E_CLASS_DISABLED = (int)(0x80004027 - 0x100000000);
            public const int CO_E_CLRNOTAVAILABLE = (int)(0x80004028 - 0x100000000);
            public const int CO_E_ASYNC_WORK_REJECTED = (int)(0x80004029 - 0x100000000);
            public const int CO_E_SERVER_INIT_TIMEOUT = (int)(0x8000402A - 0x100000000);
            public const int CO_E_NO_SECCTX_IN_ACTIVATE = (int)(0x8000402B - 0x100000000);
            public const int CO_E_TRACKER_CONFIG = (int)(0x80004030 - 0x100000000);
            public const int CO_E_THREADPOOL_CONFIG = (int)(0x80004031 - 0x100000000);
            public const int CO_E_SXS_CONFIG = (int)(0x80004032 - 0x100000000);
            public const int CO_E_MALFORMED_SPN = (int)(0x80004033 - 0x100000000);
            public const int S_OK = 0x00000000;
            public const int S_FALSE = 0x00000001;
            public const int OLE_E_FIRST = (int)(0x80040000 - 0x100000000);
            public const int OLE_E_LAST = (int)(0x800400FF - 0x100000000);
            public const int OLE_S_FIRST = 0x00040000;
            public const int OLE_S_LAST = 0x000400FF;
            public const int OLE_E_OLEVERB = (int)(0x80040000 - 0x100000000);
            public const int OLE_E_ADVF = (int)(0x80040001 - 0x100000000);
            public const int OLE_E_ENUM_NOMORE = (int)(0x80040002 - 0x100000000);
            public const int OLE_E_ADVISENOTSUPPORTED = (int)(0x80040003 - 0x100000000);
            public const int OLE_E_NOCONNECTION = (int)(0x80040004 - 0x100000000);
            public const int OLE_E_NOTRUNNING = (int)(0x80040005 - 0x100000000);
            public const int OLE_E_NOCACHE = (int)(0x80040006 - 0x100000000);
            public const int OLE_E_BLANK = (int)(0x80040007 - 0x100000000);
            public const int OLE_E_CLASSDIFF = (int)(0x80040008 - 0x100000000);
            public const int OLE_E_CANT_GETMONIKER = (int)(0x80040009 - 0x100000000);
            public const int OLE_E_CANT_BINDTOSOURCE = (int)(0x8004000A - 0x100000000);
            public const int OLE_E_STATIC = (int)(0x8004000B - 0x100000000);
            public const int OLE_E_PROMPTSAVECANCELLED = (int)(0x8004000C - 0x100000000);
            public const int OLE_E_INVALIDRECT = (int)(0x8004000D - 0x100000000);
            public const int OLE_E_WRONGCOMPOBJ = (int)(0x8004000E - 0x100000000);
            public const int OLE_E_INVALIDHWND = (int)(0x8004000F - 0x100000000);
            public const int OLE_E_NOT_INPLACEACTIVE = (int)(0x80040010 - 0x100000000);
            public const int OLE_E_CANTCONVERT = (int)(0x80040011 - 0x100000000);
            public const int OLE_E_NOSTORAGE = (int)(0x80040012 - 0x100000000);
            public const int DV_E_FORMATETC = (int)(0x80040064 - 0x100000000);
            public const int DV_E_DVTARGETDEVICE = (int)(0x80040065 - 0x100000000);
            public const int DV_E_STGMEDIUM = (int)(0x80040066 - 0x100000000);
            public const int DV_E_STATDATA = (int)(0x80040067 - 0x100000000);
            public const int DV_E_LINDEX = (int)(0x80040068 - 0x100000000);
            public const int DV_E_TYMED = (int)(0x80040069 - 0x100000000);
            public const int DV_E_CLIPFORMAT = (int)(0x8004006A - 0x100000000);
            public const int DV_E_DVASPECT = (int)(0x8004006B - 0x100000000);
            public const int DV_E_DVTARGETDEVICE_SIZE = (int)(0x8004006C - 0x100000000);
            public const int DV_E_NOIVIEWOBJECT = (int)(0x8004006D - 0x100000000);
            public const int DRAGDROP_E_FIRST = (int)(0x80040100 - 0x100000000);
            public const int DRAGDROP_E_LAST = (int)(0x8004010F - 0x100000000);
            public const int DRAGDROP_S_FIRST = 0x00040100;
            public const int DRAGDROP_S_LAST = 0x0004010F;
            public const int DRAGDROP_E_NOTREGISTERED = (int)(0x80040100 - 0x100000000);
            public const int DRAGDROP_E_ALREADYREGISTERED = (int)(0x80040101 - 0x100000000);
            public const int DRAGDROP_E_INVALIDHWND = (int)(0x80040102 - 0x100000000);
            public const int CLASSFACTORY_E_FIRST = (int)(0x80040110 - 0x100000000);
            public const int CLASSFACTORY_E_LAST = (int)(0x8004011F - 0x100000000);
            public const int CLASSFACTORY_S_FIRST = 0x00040110;
            public const int CLASSFACTORY_S_LAST = 0x0004011F;
            public const int CLASS_E_NOAGGREGATION = (int)(0x80040110 - 0x100000000);
            public const int CLASS_E_CLASSNOTAVAILABLE = (int)(0x80040111 - 0x100000000);
            public const int CLASS_E_NOTLICENSED = (int)(0x80040112 - 0x100000000);
            public const int MARSHAL_E_FIRST = (int)(0x80040120 - 0x100000000);
            public const int MARSHAL_E_LAST = (int)(0x8004012F - 0x100000000);
            public const int MARSHAL_S_FIRST = 0x00040120;
            public const int MARSHAL_S_LAST = 0x0004012F;
            public const int DATA_E_FIRST = (int)(0x80040130 - 0x100000000);
            public const int DATA_E_LAST = (int)(0x8004013F - 0x100000000);
            public const int DATA_S_FIRST = 0x00040130;
            public const int DATA_S_LAST = 0x0004013F;
            public const int VIEW_E_FIRST = (int)(0x80040140 - 0x100000000);
            public const int VIEW_E_LAST = (int)(0x8004014F - 0x100000000);
            public const int VIEW_S_FIRST = 0x00040140;
            public const int VIEW_S_LAST = 0x0004014F;
            public const int VIEW_E_DRAW = (int)(0x80040140 - 0x100000000);
            public const int REGDB_E_FIRST = (int)(0x80040150 - 0x100000000);
            public const int REGDB_E_LAST = (int)(0x8004015F - 0x100000000);
            public const int REGDB_S_FIRST = 0x00040150;
            public const int REGDB_S_LAST = 0x0004015F;
            public const int REGDB_E_READREGDB = (int)(0x80040150 - 0x100000000);
            public const int REGDB_E_WRITEREGDB = (int)(0x80040151 - 0x100000000);
            public const int REGDB_E_KEYMISSING = (int)(0x80040152 - 0x100000000);
            public const int REGDB_E_INVALIDVALUE = (int)(0x80040153 - 0x100000000);
            public const int REGDB_E_CLASSNOTREG = (int)(0x80040154 - 0x100000000);
            public const int REGDB_E_IIDNOTREG = (int)(0x80040155 - 0x100000000);
            public const int REGDB_E_BADTHREADINGMODEL = (int)(0x80040156 - 0x100000000);
            public const int CAT_E_FIRST = (int)(0x80040160 - 0x100000000);
            public const int CAT_E_LAST = (int)(0x80040161 - 0x100000000);
            public const int CAT_E_CATIDNOEXIST = (int)(0x80040160 - 0x100000000);
            public const int CAT_E_NODESCRIPTION = (int)(0x80040161 - 0x100000000);
            public const int CS_E_FIRST = (int)(0x80040164 - 0x100000000);
            public const int CS_E_LAST = (int)(0x8004016F - 0x100000000);
            public const int CS_E_PACKAGE_NOTFOUND = (int)(0x80040164 - 0x100000000);
            public const int CS_E_NOT_DELETABLE = (int)(0x80040165 - 0x100000000);
            public const int CS_E_CLASS_NOTFOUND = (int)(0x80040166 - 0x100000000);
            public const int CS_E_INVALID_VERSION = (int)(0x80040167 - 0x100000000);
            public const int CS_E_NO_CLASSSTORE = (int)(0x80040168 - 0x100000000);
            public const int CS_E_OBJECT_NOTFOUND = (int)(0x80040169 - 0x100000000);
            public const int CS_E_OBJECT_ALREADY_EXISTS = (int)(0x8004016A - 0x100000000);
            public const int CS_E_INVALID_PATH = (int)(0x8004016B - 0x100000000);
            public const int CS_E_NETWORK_ERROR = (int)(0x8004016C - 0x100000000);
            public const int CS_E_ADMIN_LIMIT_EXCEEDED = (int)(0x8004016D - 0x100000000);
            public const int CS_E_SCHEMA_MISMATCH = (int)(0x8004016E - 0x100000000);
            public const int CS_E_INTERNAL_ERROR = (int)(0x8004016F - 0x100000000);
            public const int CACHE_E_FIRST = (int)(0x80040170 - 0x100000000);
            public const int CACHE_E_LAST = (int)(0x8004017F - 0x100000000);
            public const int CACHE_S_FIRST = 0x00040170;
            public const int CACHE_S_LAST = 0x0004017F;
            public const int CACHE_E_NOCACHE_UPDATED = (int)(0x80040170 - 0x100000000);
            public const int OLEOBJ_E_FIRST = (int)(0x80040180 - 0x100000000);
            public const int OLEOBJ_E_LAST = (int)(0x8004018F - 0x100000000);
            public const int OLEOBJ_S_FIRST = 0x00040180;
            public const int OLEOBJ_S_LAST = 0x0004018F;
            public const int OLEOBJ_E_NOVERBS = (int)(0x80040180 - 0x100000000);
            public const int OLEOBJ_E_INVALIDVERB = (int)(0x80040181 - 0x100000000);
            public const int CLIENTSITE_E_FIRST = (int)(0x80040190 - 0x100000000);
            public const int CLIENTSITE_E_LAST = (int)(0x8004019F - 0x100000000);
            public const int CLIENTSITE_S_FIRST = 0x00040190;
            public const int CLIENTSITE_S_LAST = 0x0004019F;
            public const int INPLACE_E_NOTUNDOABLE = (int)(0x800401A0 - 0x100000000);
            public const int INPLACE_E_NOTOOLSPACE = (int)(0x800401A1 - 0x100000000);
            public const int INPLACE_E_FIRST = (int)(0x800401A0 - 0x100000000);
            public const int INPLACE_E_LAST = (int)(0x800401AF - 0x100000000);
            public const int INPLACE_S_FIRST = 0x000401A0;
            public const int INPLACE_S_LAST = 0x000401AF;
            public const int ENUM_E_FIRST = (int)(0x800401B0 - 0x100000000);
            public const int ENUM_E_LAST = (int)(0x800401BF - 0x100000000);
            public const int ENUM_S_FIRST = 0x000401B0;
            public const int ENUM_S_LAST = 0x000401BF;
            public const int CONVERT10_E_FIRST = (int)(0x800401C0 - 0x100000000);
            public const int CONVERT10_E_LAST = (int)(0x800401CF - 0x100000000);
            public const int CONVERT10_S_FIRST = 0x000401C0;
            public const int CONVERT10_S_LAST = 0x000401CF;
            public const int CONVERT10_E_OLESTREAM_GET = (int)(0x800401C0 - 0x100000000);
            public const int CONVERT10_E_OLESTREAM_PUT = (int)(0x800401C1 - 0x100000000);
            public const int CONVERT10_E_OLESTREAM_FMT = (int)(0x800401C2 - 0x100000000);
            public const int CONVERT10_E_OLESTREAM_BITMAP_TO_DIB = (int)(0x800401C3 - 0x100000000);
            public const int CONVERT10_E_STG_FMT = (int)(0x800401C4 - 0x100000000);
            public const int CONVERT10_E_STG_NO_STD_STREAM = (int)(0x800401C5 - 0x100000000);
            public const int CONVERT10_E_STG_DIB_TO_BITMAP = (int)(0x800401C6 - 0x100000000);
            public const int CLIPBRD_E_FIRST = (int)(0x800401D0 - 0x100000000);
            public const int CLIPBRD_E_LAST = (int)(0x800401DF - 0x100000000);
            public const int CLIPBRD_S_FIRST = 0x000401D0;
            public const int CLIPBRD_S_LAST = 0x000401DF;
            public const int CLIPBRD_E_CANT_OPEN = (int)(0x800401D0 - 0x100000000);
            public const int CLIPBRD_E_CANT_EMPTY = (int)(0x800401D1 - 0x100000000);
            public const int CLIPBRD_E_CANT_SET = (int)(0x800401D2 - 0x100000000);
            public const int CLIPBRD_E_BAD_DATA = (int)(0x800401D3 - 0x100000000);
            public const int CLIPBRD_E_CANT_CLOSE = (int)(0x800401D4 - 0x100000000);
            public const int MK_E_FIRST = (int)(0x800401E0 - 0x100000000);
            public const int MK_E_LAST = (int)(0x800401EF - 0x100000000);
            public const int MK_S_FIRST = 0x000401E0;
            public const int MK_S_LAST = 0x000401EF;
            public const int MK_E_CONNECTMANUALLY = (int)(0x800401E0 - 0x100000000);
            public const int MK_E_EXCEEDEDDEADLINE = (int)(0x800401E1 - 0x100000000);
            public const int MK_E_NEEDGENERIC = (int)(0x800401E2 - 0x100000000);
            public const int MK_E_UNAVAILABLE = (int)(0x800401E3 - 0x100000000);
            public const int MK_E_SYNTAX = (int)(0x800401E4 - 0x100000000);
            public const int MK_E_NOOBJECT = (int)(0x800401E5 - 0x100000000);
            public const int MK_E_INVALIDEXTENSION = (int)(0x800401E6 - 0x100000000);
            public const int MK_E_INTERMEDIATEINTERFACENOTSUPPORTED = (int)(0x800401E7 - 0x100000000);
            public const int MK_E_NOTBINDABLE = (int)(0x800401E8 - 0x100000000);
            public const int MK_E_NOTBOUND = (int)(0x800401E9 - 0x100000000);
            public const int MK_E_CANTOPENFILE = (int)(0x800401EA - 0x100000000);
            public const int MK_E_MUSTBOTHERUSER = (int)(0x800401EB - 0x100000000);
            public const int MK_E_NOINVERSE = (int)(0x800401EC - 0x100000000);
            public const int MK_E_NOSTORAGE = (int)(0x800401ED - 0x100000000);
            public const int MK_E_NOPREFIX = (int)(0x800401EE - 0x100000000);
            public const int MK_E_ENUMERATION_FAILED = (int)(0x800401EF - 0x100000000);
            public const int CO_E_FIRST = (int)(0x800401F0 - 0x100000000);
            public const int CO_E_LAST = (int)(0x800401FF - 0x100000000);
            public const int CO_S_FIRST = 0x000401F0;
            public const int CO_S_LAST = 0x000401FF;
            public const int CO_E_NOTINITIALIZED = (int)(0x800401F0 - 0x100000000);
            public const int CO_E_ALREADYINITIALIZED = (int)(0x800401F1 - 0x100000000);
            public const int CO_E_CANTDETERMINECLASS = (int)(0x800401F2 - 0x100000000);
            public const int CO_E_CLASSSTRING = (int)(0x800401F3 - 0x100000000);
            public const int CO_E_IIDSTRING = (int)(0x800401F4 - 0x100000000);
            public const int CO_E_APPNOTFOUND = (int)(0x800401F5 - 0x100000000);
            public const int CO_E_APPSINGLEUSE = (int)(0x800401F6 - 0x100000000);
            public const int CO_E_ERRORINAPP = (int)(0x800401F7 - 0x100000000);
            public const int CO_E_DLLNOTFOUND = (int)(0x800401F8 - 0x100000000);
            public const int CO_E_ERRORINDLL = (int)(0x800401F9 - 0x100000000);
            public const int CO_E_WRONGOSFORAPP = (int)(0x800401FA - 0x100000000);
            public const int CO_E_OBJNOTREG = (int)(0x800401FB - 0x100000000);
            public const int CO_E_OBJISREG = (int)(0x800401FC - 0x100000000);
            public const int CO_E_OBJNOTCONNECTED = (int)(0x800401FD - 0x100000000);
            public const int CO_E_APPDIDNTREG = (int)(0x800401FE - 0x100000000);
            public const int CO_E_RELEASED = (int)(0x800401FF - 0x100000000);
            public const int EVENT_E_FIRST = (int)(0x80040200 - 0x100000000);
            public const int EVENT_E_LAST = (int)(0x8004021F - 0x100000000);
            public const int EVENT_S_FIRST = 0x00040200;
            public const int EVENT_S_LAST = 0x0004021F;
            public const int EVENT_S_SOME_SUBSCRIBERS_FAILED = 0x00040200;
            public const int EVENT_E_ALL_SUBSCRIBERS_FAILED = (int)(0x80040201 - 0x100000000);
            public const int EVENT_S_NOSUBSCRIBERS = 0x00040202;
            public const int EVENT_E_QUERYSYNTAX = (int)(0x80040203 - 0x100000000);
            public const int EVENT_E_QUERYFIELD = (int)(0x80040204 - 0x100000000);
            public const int EVENT_E_INTERNALEXCEPTION = (int)(0x80040205 - 0x100000000);
            public const int EVENT_E_INTERNALERROR = (int)(0x80040206 - 0x100000000);
            public const int EVENT_E_INVALID_PER_USER_SID = (int)(0x80040207 - 0x100000000);
            public const int EVENT_E_USER_EXCEPTION = (int)(0x80040208 - 0x100000000);
            public const int EVENT_E_TOO_MANY_METHODS = (int)(0x80040209 - 0x100000000);
            public const int EVENT_E_MISSING_EVENTCLASS = (int)(0x8004020A - 0x100000000);
            public const int EVENT_E_NOT_ALL_REMOVED = (int)(0x8004020B - 0x100000000);
            public const int EVENT_E_COMPLUS_NOT_INSTALLED = (int)(0x8004020C - 0x100000000);
            public const int EVENT_E_CANT_MODIFY_OR_DELETE_UNCONFIGURED_OBJECT = (int)(0x8004020D - 0x100000000);
            public const int EVENT_E_CANT_MODIFY_OR_DELETE_CONFIGURED_OBJECT = (int)(0x8004020E - 0x100000000);
            public const int EVENT_E_INVALID_EVENT_CLASS_PARTITION = (int)(0x8004020F - 0x100000000);
            public const int EVENT_E_PER_USER_SID_NOT_LOGGED_ON = (int)(0x80040210 - 0x100000000);
            public const int XACT_E_FIRST = (int)(0x8004D000 - 0x100000000);
            public const int XACT_E_LAST = (int)(0x8004D029 - 0x100000000);
            public const int XACT_S_FIRST = 0x0004D000;
            public const int XACT_S_LAST = 0x0004D010;
            public const int XACT_E_ALREADYOTHERSINGLEPHASE = (int)(0x8004D000 - 0x100000000);
            public const int XACT_E_CANTRETAIN = (int)(0x8004D001 - 0x100000000);
            public const int XACT_E_COMMITFAILED = (int)(0x8004D002 - 0x100000000);
            public const int XACT_E_COMMITPREVENTED = (int)(0x8004D003 - 0x100000000);
            public const int XACT_E_HEURISTICABORT = (int)(0x8004D004 - 0x100000000);
            public const int XACT_E_HEURISTICCOMMIT = (int)(0x8004D005 - 0x100000000);
            public const int XACT_E_HEURISTICDAMAGE = (int)(0x8004D006 - 0x100000000);
            public const int XACT_E_HEURISTICDANGER = (int)(0x8004D007 - 0x100000000);
            public const int XACT_E_ISOLATIONLEVEL = (int)(0x8004D008 - 0x100000000);
            public const int XACT_E_NOASYNC = (int)(0x8004D009 - 0x100000000);
            public const int XACT_E_NOENLIST = (int)(0x8004D00A - 0x100000000);
            public const int XACT_E_NOISORETAIN = (int)(0x8004D00B - 0x100000000);
            public const int XACT_E_NORESOURCE = (int)(0x8004D00C - 0x100000000);
            public const int XACT_E_NOTCURRENT = (int)(0x8004D00D - 0x100000000);
            public const int XACT_E_NOTRANSACTION = (int)(0x8004D00E - 0x100000000);
            public const int XACT_E_NOTSUPPORTED = (int)(0x8004D00F - 0x100000000);
            public const int XACT_E_UNKNOWNRMGRID = (int)(0x8004D010 - 0x100000000);
            public const int XACT_E_WRONGSTATE = (int)(0x8004D011 - 0x100000000);
            public const int XACT_E_WRONGUOW = (int)(0x8004D012 - 0x100000000);
            public const int XACT_E_XTIONEXISTS = (int)(0x8004D013 - 0x100000000);
            public const int XACT_E_NOIMPORTOBJECT = (int)(0x8004D014 - 0x100000000);
            public const int XACT_E_INVALIDCOOKIE = (int)(0x8004D015 - 0x100000000);
            public const int XACT_E_INDOUBT = (int)(0x8004D016 - 0x100000000);
            public const int XACT_E_NOTIMEOUT = (int)(0x8004D017 - 0x100000000);
            public const int XACT_E_ALREADYINPROGRESS = (int)(0x8004D018 - 0x100000000);
            public const int XACT_E_ABORTED = (int)(0x8004D019 - 0x100000000);
            public const int XACT_E_LOGFULL = (int)(0x8004D01A - 0x100000000);
            public const int XACT_E_TMNOTAVAILABLE = (int)(0x8004D01B - 0x100000000);
            public const int XACT_E_CONNECTION_DOWN = (int)(0x8004D01C - 0x100000000);
            public const int XACT_E_CONNECTION_DENIED = (int)(0x8004D01D - 0x100000000);
            public const int XACT_E_REENLISTTIMEOUT = (int)(0x8004D01E - 0x100000000);
            public const int XACT_E_TIP_CONNECT_FAILED = (int)(0x8004D01F - 0x100000000);
            public const int XACT_E_TIP_PROTOCOL_ERROR = (int)(0x8004D020 - 0x100000000);
            public const int XACT_E_TIP_PULL_FAILED = (int)(0x8004D021 - 0x100000000);
            public const int XACT_E_DEST_TMNOTAVAILABLE = (int)(0x8004D022 - 0x100000000);
            public const int XACT_E_TIP_DISABLED = (int)(0x8004D023 - 0x100000000);
            public const int XACT_E_NETWORK_TX_DISABLED = (int)(0x8004D024 - 0x100000000);
            public const int XACT_E_PARTNER_NETWORK_TX_DISABLED = (int)(0x8004D025 - 0x100000000);
            public const int XACT_E_XA_TX_DISABLED = (int)(0x8004D026 - 0x100000000);
            public const int XACT_E_UNABLE_TO_READ_DTC_CONFIG = (int)(0x8004D027 - 0x100000000);
            public const int XACT_E_UNABLE_TO_LOAD_DTC_PROXY = (int)(0x8004D028 - 0x100000000);
            public const int XACT_E_ABORTING = (int)(0x8004D029 - 0x100000000);
            public const int XACT_E_CLERKNOTFOUND = (int)(0x8004D080 - 0x100000000);
            public const int XACT_E_CLERKEXISTS = (int)(0x8004D081 - 0x100000000);
            public const int XACT_E_RECOVERYINPROGRESS = (int)(0x8004D082 - 0x100000000);
            public const int XACT_E_TRANSACTIONCLOSED = (int)(0x8004D083 - 0x100000000);
            public const int XACT_E_INVALIDLSN = (int)(0x8004D084 - 0x100000000);
            public const int XACT_E_REPLAYREQUEST = (int)(0x8004D085 - 0x100000000);
            public const int XACT_S_ASYNC = 0x0004D000;
            public const int XACT_S_DEFECT = 0x0004D001;
            public const int XACT_S_READONLY = 0x0004D002;
            public const int XACT_S_SOMENORETAIN = 0x0004D003;
            public const int XACT_S_OKINFORM = 0x0004D004;
            public const int XACT_S_MADECHANGESCONTENT = 0x0004D005;
            public const int XACT_S_MADECHANGESINFORM = 0x0004D006;
            public const int XACT_S_ALLNORETAIN = 0x0004D007;
            public const int XACT_S_ABORTING = 0x0004D008;
            public const int XACT_S_SINGLEPHASE = 0x0004D009;
            public const int XACT_S_LOCALLY_OK = 0x0004D00A;
            public const int XACT_S_LASTRESOURCEMANAGER = 0x0004D010;
            public const int CONTEXT_E_FIRST = (int)(0x8004E000 - 0x100000000);
            public const int CONTEXT_E_LAST = (int)(0x8004E02F - 0x100000000);
            public const int CONTEXT_S_FIRST = 0x0004E000;
            public const int CONTEXT_S_LAST = 0x0004E02F;
            public const int CONTEXT_E_ABORTED = (int)(0x8004E002 - 0x100000000);
            public const int CONTEXT_E_ABORTING = (int)(0x8004E003 - 0x100000000);
            public const int CONTEXT_E_NOCONTEXT = (int)(0x8004E004 - 0x100000000);
            public const int CONTEXT_E_WOULD_DEADLOCK = (int)(0x8004E005 - 0x100000000);
            public const int CONTEXT_E_SYNCH_TIMEOUT = (int)(0x8004E006 - 0x100000000);
            public const int CONTEXT_E_OLDREF = (int)(0x8004E007 - 0x100000000);
            public const int CONTEXT_E_ROLENOTFOUND = (int)(0x8004E00C - 0x100000000);
            public const int CONTEXT_E_TMNOTAVAILABLE = (int)(0x8004E00F - 0x100000000);
            public const int CO_E_ACTIVATIONFAILED = (int)(0x8004E021 - 0x100000000);
            public const int CO_E_ACTIVATIONFAILED_EVENTLOGGED = (int)(0x8004E022 - 0x100000000);
            public const int CO_E_ACTIVATIONFAILED_CATALOGERROR = (int)(0x8004E023 - 0x100000000);
            public const int CO_E_ACTIVATIONFAILED_TIMEOUT = (int)(0x8004E024 - 0x100000000);
            public const int CO_E_INITIALIZATIONFAILED = (int)(0x8004E025 - 0x100000000);
            public const int CONTEXT_E_NOJIT = (int)(0x8004E026 - 0x100000000);
            public const int CONTEXT_E_NOTRANSACTION = (int)(0x8004E027 - 0x100000000);
            public const int CO_E_THREADINGMODEL_CHANGED = (int)(0x8004E028 - 0x100000000);
            public const int CO_E_NOIISINTRINSICS = (int)(0x8004E029 - 0x100000000);
            public const int CO_E_NOCOOKIES = (int)(0x8004E02A - 0x100000000);
            public const int CO_E_DBERROR = (int)(0x8004E02B - 0x100000000);
            public const int CO_E_NOTPOOLED = (int)(0x8004E02C - 0x100000000);
            public const int CO_E_NOTCONSTRUCTED = (int)(0x8004E02D - 0x100000000);
            public const int CO_E_NOSYNCHRONIZATION = (int)(0x8004E02E - 0x100000000);
            public const int CO_E_ISOLEVELMISMATCH = (int)(0x8004E02F - 0x100000000);
            public const int OLE_S_USEREG = 0x00040000;
            public const int OLE_S_STATIC = 0x00040001;
            public const int OLE_S_MAC_CLIPFORMAT = 0x00040002;
            public const int DRAGDROP_S_DROP = 0x00040100;
            public const int DRAGDROP_S_CANCEL = 0x00040101;
            public const int DRAGDROP_S_USEDEFAULTCURSORS = 0x00040102;
            public const int DATA_S_SAMEFORMATETC = 0x00040130;
            public const int VIEW_S_ALREADY_FROZEN = 0x00040140;
            public const int CACHE_S_FORMATETC_NOTSUPPORTED = 0x00040170;
            public const int CACHE_S_SAMECACHE = 0x00040171;
            public const int CACHE_S_SOMECACHES_NOTUPDATED = 0x00040172;
            public const int OLEOBJ_S_INVALIDVERB = 0x00040180;
            public const int OLEOBJ_S_CANNOT_DOVERB_NOW = 0x00040181;
            public const int OLEOBJ_S_INVALIDHWND = 0x00040182;
            public const int INPLACE_S_TRUNCATED = 0x000401A0;
            public const int CONVERT10_S_NO_PRESENTATION = 0x000401C0;
            public const int MK_S_REDUCED_TO_SELF = 0x000401E2;
            public const int MK_S_ME = 0x000401E4;
            public const int MK_S_HIM = 0x000401E5;
            public const int MK_S_US = 0x000401E6;
            public const int MK_S_MONIKERALREADYREGISTERED = 0x000401E7;
            public const int SCHED_S_TASK_READY = 0x00041300;
            public const int SCHED_S_TASK_RUNNING = 0x00041301;
            public const int SCHED_S_TASK_DISABLED = 0x00041302;
            public const int SCHED_S_TASK_HAS_NOT_RUN = 0x00041303;
            public const int SCHED_S_TASK_NO_MORE_RUNS = 0x00041304;
            public const int SCHED_S_TASK_NOT_SCHEDULED = 0x00041305;
            public const int SCHED_S_TASK_TERMINATED = 0x00041306;
            public const int SCHED_S_TASK_NO_VALID_TRIGGERS = 0x00041307;
            public const int SCHED_S_EVENT_TRIGGER = 0x00041308;
            public const int SCHED_E_TRIGGER_NOT_FOUND = (int)(0x80041309 - 0x100000000);
            public const int SCHED_E_TASK_NOT_READY = (int)(0x8004130A - 0x100000000);
            public const int SCHED_E_TASK_NOT_RUNNING = (int)(0x8004130B - 0x100000000);
            public const int SCHED_E_SERVICE_NOT_INSTALLED = (int)(0x8004130C - 0x100000000);
            public const int SCHED_E_CANNOT_OPEN_TASK = (int)(0x8004130D - 0x100000000);
            public const int SCHED_E_INVALID_TASK = (int)(0x8004130E - 0x100000000);
            public const int SCHED_E_ACCOUNT_INFORMATION_NOT_SET = (int)(0x8004130F - 0x100000000);
            public const int SCHED_E_ACCOUNT_NAME_NOT_FOUND = (int)(0x80041310 - 0x100000000);
            public const int SCHED_E_ACCOUNT_DBASE_CORRUPT = (int)(0x80041311 - 0x100000000);
            public const int SCHED_E_NO_SECURITY_SERVICES = (int)(0x80041312 - 0x100000000);
            public const int SCHED_E_UNKNOWN_OBJECT_VERSION = (int)(0x80041313 - 0x100000000);
            public const int SCHED_E_UNSUPPORTED_ACCOUNT_OPTION = (int)(0x80041314 - 0x100000000);
            public const int SCHED_E_SERVICE_NOT_RUNNING = (int)(0x80041315 - 0x100000000);
            public const int CO_E_CLASS_CREATE_FAILED = (int)(0x80080001 - 0x100000000);
            public const int CO_E_SCM_ERROR = (int)(0x80080002 - 0x100000000);
            public const int CO_E_SCM_RPC_FAILURE = (int)(0x80080003 - 0x100000000);
            public const int CO_E_BAD_PATH = (int)(0x80080004 - 0x100000000);
            public const int CO_E_SERVER_EXEC_FAILURE = (int)(0x80080005 - 0x100000000);
            public const int CO_E_OBJSRV_RPC_FAILURE = (int)(0x80080006 - 0x100000000);
            public const int MK_E_NO_NORMALIZED = (int)(0x80080007 - 0x100000000);
            public const int CO_E_SERVER_STOPPING = (int)(0x80080008 - 0x100000000);
            public const int MEM_E_INVALID_ROOT = (int)(0x80080009 - 0x100000000);
            public const int MEM_E_INVALID_LINK = (int)(0x80080010 - 0x100000000);
            public const int MEM_E_INVALID_SIZE = (int)(0x80080011 - 0x100000000);
            public const int CO_S_NOTALLINTERFACES = 0x00080012;
            public const int CO_S_MACHINENAMENOTFOUND = 0x00080013;
            public const int DISP_E_UNKNOWNINTERFACE = (int)(0x80020001 - 0x100000000);
            public const int DISP_E_MEMBERNOTFOUND = (int)(0x80020003 - 0x100000000);
            public const int DISP_E_PARAMNOTFOUND = (int)(0x80020004 - 0x100000000);
            public const int DISP_E_TYPEMISMATCH = (int)(0x80020005 - 0x100000000);
            public const int DISP_E_UNKNOWNNAME = (int)(0x80020006 - 0x100000000);
            public const int DISP_E_NONAMEDARGS = (int)(0x80020007 - 0x100000000);
            public const int DISP_E_BADVARTYPE = (int)(0x80020008 - 0x100000000);
            public const int DISP_E_EXCEPTION = (int)(0x80020009 - 0x100000000);
            public const int DISP_E_OVERFLOW = (int)(0x8002000A - 0x100000000);
            public const int DISP_E_BADINDEX = (int)(0x8002000B - 0x100000000);
            public const int DISP_E_UNKNOWNLCID = (int)(0x8002000C - 0x100000000);
            public const int DISP_E_ARRAYISLOCKED = (int)(0x8002000D - 0x100000000);
            public const int DISP_E_BADPARAMCOUNT = (int)(0x8002000E - 0x100000000);
            public const int DISP_E_PARAMNOTOPTIONAL = (int)(0x8002000F - 0x100000000);
            public const int DISP_E_BADCALLEE = (int)(0x80020010 - 0x100000000);
            public const int DISP_E_NOTACOLLECTION = (int)(0x80020011 - 0x100000000);
            public const int DISP_E_DIVBYZERO = (int)(0x80020012 - 0x100000000);
            public const int DISP_E_BUFFERTOOSMALL = (int)(0x80020013 - 0x100000000);
            public const int TYPE_E_BUFFERTOOSMALL = (int)(0x80028016 - 0x100000000);
            public const int TYPE_E_FIELDNOTFOUND = (int)(0x80028017 - 0x100000000);
            public const int TYPE_E_INVDATAREAD = (int)(0x80028018 - 0x100000000);
            public const int TYPE_E_UNSUPFORMAT = (int)(0x80028019 - 0x100000000);
            public const int TYPE_E_REGISTRYACCESS = (int)(0x8002801C - 0x100000000);
            public const int TYPE_E_LIBNOTREGISTERED = (int)(0x8002801D - 0x100000000);
            public const int TYPE_E_UNDEFINEDTYPE = (int)(0x80028027 - 0x100000000);
            public const int TYPE_E_QUALIFIEDNAMEDISALLOWED = (int)(0x80028028 - 0x100000000);
            public const int TYPE_E_INVALIDSTATE = (int)(0x80028029 - 0x100000000);
            public const int TYPE_E_WRONGTYPEKIND = (int)(0x8002802A - 0x100000000);
            public const int TYPE_E_ELEMENTNOTFOUND = (int)(0x8002802B - 0x100000000);
            public const int TYPE_E_AMBIGUOUSNAME = (int)(0x8002802C - 0x100000000);
            public const int TYPE_E_NAMECONFLICT = (int)(0x8002802D - 0x100000000);
            public const int TYPE_E_UNKNOWNLCID = (int)(0x8002802E - 0x100000000);
            public const int TYPE_E_DLLFUNCTIONNOTFOUND = (int)(0x8002802F - 0x100000000);
            public const int TYPE_E_BADMODULEKIND = (int)(0x800288BD - 0x100000000);
            public const int TYPE_E_SIZETOOBIG = (int)(0x800288C5 - 0x100000000);
            public const int TYPE_E_DUPLICATEID = (int)(0x800288C6 - 0x100000000);
            public const int TYPE_E_INVALIDID = (int)(0x800288CF - 0x100000000);
            public const int TYPE_E_TYPEMISMATCH = (int)(0x80028CA0 - 0x100000000);
            public const int TYPE_E_OUTOFBOUNDS = (int)(0x80028CA1 - 0x100000000);
            public const int TYPE_E_IOERROR = (int)(0x80028CA2 - 0x100000000);
            public const int TYPE_E_CANTCREATETMPFILE = (int)(0x80028CA3 - 0x100000000);
            public const int TYPE_E_CANTLOADLIBRARY = (int)(0x80029C4A - 0x100000000);
            public const int TYPE_E_INCONSISTENTPROPFUNCS = (int)(0x80029C83 - 0x100000000);
            public const int TYPE_E_CIRCULARTYPE = (int)(0x80029C84 - 0x100000000);
            public const int STG_E_INVALIDFUNCTION = (int)(0x80030001 - 0x100000000);
            public const int STG_E_FILENOTFOUND = (int)(0x80030002 - 0x100000000);
            public const int STG_E_PATHNOTFOUND = (int)(0x80030003 - 0x100000000);
            public const int STG_E_TOOMANYOPENFILES = (int)(0x80030004 - 0x100000000);
            public const int STG_E_ACCESSDENIED = (int)(0x80030005 - 0x100000000);
            public const int STG_E_INVALIDHANDLE = (int)(0x80030006 - 0x100000000);
            public const int STG_E_INSUFFICIENTMEMORY = (int)(0x80030008 - 0x100000000);
            public const int STG_E_INVALIDPOINTER = (int)(0x80030009 - 0x100000000);
            public const int STG_E_NOMOREFILES = (int)(0x80030012 - 0x100000000);
            public const int STG_E_DISKISWRITEPROTECTED = (int)(0x80030013 - 0x100000000);
            public const int STG_E_SEEKERROR = (int)(0x80030019 - 0x100000000);
            public const int STG_E_WRITEFAULT = (int)(0x8003001D - 0x100000000);
            public const int STG_E_READFAULT = (int)(0x8003001E - 0x100000000);
            public const int STG_E_SHAREVIOLATION = (int)(0x80030020 - 0x100000000);
            public const int STG_E_LOCKVIOLATION = (int)(0x80030021 - 0x100000000);
            public const int STG_E_FILEALREADYEXISTS = (int)(0x80030050 - 0x100000000);
            public const int STG_E_INVALIDPARAMETER = (int)(0x80030057 - 0x100000000);
            public const int STG_E_MEDIUMFULL = (int)(0x80030070 - 0x100000000);
            public const int STG_E_PROPSETMISMATCHED = (int)(0x800300F0 - 0x100000000);
            public const int STG_E_ABNORMALAPIEXIT = (int)(0x800300FA - 0x100000000);
            public const int STG_E_INVALIDHEADER = (int)(0x800300FB - 0x100000000);
            public const int STG_E_INVALIDNAME = (int)(0x800300FC - 0x100000000);
            public const int STG_E_UNKNOWN = (int)(0x800300FD - 0x100000000);
            public const int STG_E_UNIMPLEMENTEDFUNCTION = (int)(0x800300FE - 0x100000000);
            public const int STG_E_INVALIDFLAG = (int)(0x800300FF - 0x100000000);
            public const int STG_E_INUSE = (int)(0x80030100 - 0x100000000);
            public const int STG_E_NOTCURRENT = (int)(0x80030101 - 0x100000000);
            public const int STG_E_REVERTED = (int)(0x80030102 - 0x100000000);
            public const int STG_E_CANTSAVE = (int)(0x80030103 - 0x100000000);
            public const int STG_E_OLDFORMAT = (int)(0x80030104 - 0x100000000);
            public const int STG_E_OLDDLL = (int)(0x80030105 - 0x100000000);
            public const int STG_E_SHAREREQUIRED = (int)(0x80030106 - 0x100000000);
            public const int STG_E_NOTFILEBASEDSTORAGE = (int)(0x80030107 - 0x100000000);
            public const int STG_E_EXTANTMARSHALLINGS = (int)(0x80030108 - 0x100000000);
            public const int STG_E_DOCFILECORRUPT = (int)(0x80030109 - 0x100000000);
            public const int STG_E_BADBASEADDRESS = (int)(0x80030110 - 0x100000000);
            public const int STG_E_DOCFILETOOLARGE = (int)(0x80030111 - 0x100000000);
            public const int STG_E_NOTSIMPLEFORMAT = (int)(0x80030112 - 0x100000000);
            public const int STG_E_INCOMPLETE = (int)(0x80030201 - 0x100000000);
            public const int STG_E_TERMINATED = (int)(0x80030202 - 0x100000000);
            public const int STG_S_CONVERTED = 0x00030200;
            public const int STG_S_BLOCK = 0x00030201;
            public const int STG_S_RETRYNOW = 0x00030202;
            public const int STG_S_MONITORING = 0x00030203;
            public const int STG_S_MULTIPLEOPENS = 0x00030204;
            public const int STG_S_CONSOLIDATIONFAILED = 0x00030205;
            public const int STG_S_CANNOTCONSOLIDATE = 0x00030206;
            public const int STG_E_STATUS_COPY_PROTECTION_FAILURE = (int)(0x80030305 - 0x100000000);
            public const int STG_E_CSS_AUTHENTICATION_FAILURE = (int)(0x80030306 - 0x100000000);
            public const int STG_E_CSS_KEY_NOT_PRESENT = (int)(0x80030307 - 0x100000000);
            public const int STG_E_CSS_KEY_NOT_ESTABLISHED = (int)(0x80030308 - 0x100000000);
            public const int STG_E_CSS_SCRAMBLED_SECTOR = (int)(0x80030309 - 0x100000000);
            public const int STG_E_CSS_REGION_MISMATCH = (int)(0x8003030A - 0x100000000);
            public const int STG_E_RESETS_EXHAUSTED = (int)(0x8003030B - 0x100000000);
            public const int RPC_E_CALL_REJECTED = (int)(0x80010001 - 0x100000000);
            public const int RPC_E_CALL_CANCELED = (int)(0x80010002 - 0x100000000);
            public const int RPC_E_CANTPOST_INSENDCALL = (int)(0x80010003 - 0x100000000);
            public const int RPC_E_CANTCALLOUT_INASYNCCALL = (int)(0x80010004 - 0x100000000);
            public const int RPC_E_CANTCALLOUT_INEXTERNALCALL = (int)(0x80010005 - 0x100000000);
            public const int RPC_E_CONNECTION_TERMINATED = (int)(0x80010006 - 0x100000000);
            public const int RPC_E_SERVER_DIED = (int)(0x80010007 - 0x100000000);
            public const int RPC_E_CLIENT_DIED = (int)(0x80010008 - 0x100000000);
            public const int RPC_E_INVALID_DATAPACKET = (int)(0x80010009 - 0x100000000);
            public const int RPC_E_CANTTRANSMIT_CALL = (int)(0x8001000A - 0x100000000);
            public const int RPC_E_CLIENT_CANTMARSHAL_DATA = (int)(0x8001000B - 0x100000000);
            public const int RPC_E_CLIENT_CANTUNMARSHAL_DATA = (int)(0x8001000C - 0x100000000);
            public const int RPC_E_SERVER_CANTMARSHAL_DATA = (int)(0x8001000D - 0x100000000);
            public const int RPC_E_SERVER_CANTUNMARSHAL_DATA = (int)(0x8001000E - 0x100000000);
            public const int RPC_E_INVALID_DATA = (int)(0x8001000F - 0x100000000);
            public const int RPC_E_INVALID_PARAMETER = (int)(0x80010010 - 0x100000000);
            public const int RPC_E_CANTCALLOUT_AGAIN = (int)(0x80010011 - 0x100000000);
            public const int RPC_E_SERVER_DIED_DNE = (int)(0x80010012 - 0x100000000);
            public const int RPC_E_SYS_CALL_FAILED = (int)(0x80010100 - 0x100000000);
            public const int RPC_E_OUT_OF_RESOURCES = (int)(0x80010101 - 0x100000000);
            public const int RPC_E_ATTEMPTED_MULTITHREAD = (int)(0x80010102 - 0x100000000);
            public const int RPC_E_NOT_REGISTERED = (int)(0x80010103 - 0x100000000);
            public const int RPC_E_FAULT = (int)(0x80010104 - 0x100000000);
            public const int RPC_E_SERVERFAULT = (int)(0x80010105 - 0x100000000);
            public const int RPC_E_CHANGED_MODE = (int)(0x80010106 - 0x100000000);
            public const int RPC_E_INVALIDMETHOD = (int)(0x80010107 - 0x100000000);
            public const int RPC_E_DISCONNECTED = (int)(0x80010108 - 0x100000000);
            public const int RPC_E_RETRY = (int)(0x80010109 - 0x100000000);
            public const int RPC_E_SERVERCALL_RETRYLATER = (int)(0x8001010A - 0x100000000);
            public const int RPC_E_SERVERCALL_REJECTED = (int)(0x8001010B - 0x100000000);
            public const int RPC_E_INVALID_CALLDATA = (int)(0x8001010C - 0x100000000);
            public const int RPC_E_CANTCALLOUT_ININPUTSYNCCALL = (int)(0x8001010D - 0x100000000);
            public const int RPC_E_WRONG_THREAD = (int)(0x8001010E - 0x100000000);
            public const int RPC_E_THREAD_NOT_INIT = (int)(0x8001010F - 0x100000000);
            public const int RPC_E_VERSION_MISMATCH = (int)(0x80010110 - 0x100000000);
            public const int RPC_E_INVALID_HEADER = (int)(0x80010111 - 0x100000000);
            public const int RPC_E_INVALID_EXTENSION = (int)(0x80010112 - 0x100000000);
            public const int RPC_E_INVALID_IPID = (int)(0x80010113 - 0x100000000);
            public const int RPC_E_INVALID_OBJECT = (int)(0x80010114 - 0x100000000);
            public const int RPC_S_CALLPENDING = (int)(0x80010115 - 0x100000000);
            public const int RPC_S_WAITONTIMER = (int)(0x80010116 - 0x100000000);
            public const int RPC_E_CALL_COMPLETE = (int)(0x80010117 - 0x100000000);
            public const int RPC_E_UNSECURE_CALL = (int)(0x80010118 - 0x100000000);
            public const int RPC_E_TOO_LATE = (int)(0x80010119 - 0x100000000);
            public const int RPC_E_NO_GOOD_SECURITY_PACKAGES = (int)(0x8001011A - 0x100000000);
            public const int RPC_E_ACCESS_DENIED = (int)(0x8001011B - 0x100000000);
            public const int RPC_E_REMOTE_DISABLED = (int)(0x8001011C - 0x100000000);
            public const int RPC_E_INVALID_OBJREF = (int)(0x8001011D - 0x100000000);
            public const int RPC_E_NO_CONTEXT = (int)(0x8001011E - 0x100000000);
            public const int RPC_E_TIMEOUT = (int)(0x8001011F - 0x100000000);
            public const int RPC_E_NO_SYNC = (int)(0x80010120 - 0x100000000);
            public const int RPC_E_FULLSIC_REQUIRED = (int)(0x80010121 - 0x100000000);
            public const int RPC_E_INVALID_STD_NAME = (int)(0x80010122 - 0x100000000);
            public const int CO_E_FAILEDTOIMPERSONATE = (int)(0x80010123 - 0x100000000);
            public const int CO_E_FAILEDTOGETSECCTX = (int)(0x80010124 - 0x100000000);
            public const int CO_E_FAILEDTOOPENTHREADTOKEN = (int)(0x80010125 - 0x100000000);
            public const int CO_E_FAILEDTOGETTOKENINFO = (int)(0x80010126 - 0x100000000);
            public const int CO_E_TRUSTEEDOESNTMATCHCLIENT = (int)(0x80010127 - 0x100000000);
            public const int CO_E_FAILEDTOQUERYCLIENTBLANKET = (int)(0x80010128 - 0x100000000);
            public const int CO_E_FAILEDTOSETDACL = (int)(0x80010129 - 0x100000000);
            public const int CO_E_ACCESSCHECKFAILED = (int)(0x8001012A - 0x100000000);
            public const int CO_E_NETACCESSAPIFAILED = (int)(0x8001012B - 0x100000000);
            public const int CO_E_WRONGTRUSTEENAMESYNTAX = (int)(0x8001012C - 0x100000000);
            public const int CO_E_INVALIDSID = (int)(0x8001012D - 0x100000000);
            public const int CO_E_CONVERSIONFAILED = (int)(0x8001012E - 0x100000000);
            public const int CO_E_NOMATCHINGSIDFOUND = (int)(0x8001012F - 0x100000000);
            public const int CO_E_LOOKUPACCSIDFAILED = (int)(0x80010130 - 0x100000000);
            public const int CO_E_NOMATCHINGNAMEFOUND = (int)(0x80010131 - 0x100000000);
            public const int CO_E_LOOKUPACCNAMEFAILED = (int)(0x80010132 - 0x100000000);
            public const int CO_E_SETSERLHNDLFAILED = (int)(0x80010133 - 0x100000000);
            public const int CO_E_FAILEDTOGETWINDIR = (int)(0x80010134 - 0x100000000);
            public const int CO_E_PATHTOOInt32 = (int)(0x80010135 - 0x100000000);
            public const int CO_E_FAILEDTOGENUUID = (int)(0x80010136 - 0x100000000);
            public const int CO_E_FAILEDTOCREATEFILE = (int)(0x80010137 - 0x100000000);
            public const int CO_E_FAILEDTOCLOSEHANDLE = (int)(0x80010138 - 0x100000000);
            public const int CO_E_EXCEEDSYSACLLIMIT = (int)(0x80010139 - 0x100000000);
            public const int CO_E_ACESINWRONGORDER = (int)(0x8001013A - 0x100000000);
            public const int CO_E_INCOMPATIBLESTREAMVERSION = (int)(0x8001013B - 0x100000000);
            public const int CO_E_FAILEDTOOPENPROCESSTOKEN = (int)(0x8001013C - 0x100000000);
            public const int CO_E_DECODEFAILED = (int)(0x8001013D - 0x100000000);
            public const int CO_E_ACNOTINITIALIZED = (int)(0x8001013F - 0x100000000);
            public const int CO_E_CANCEL_DISABLED = (int)(0x80010140 - 0x100000000);
            public const int RPC_E_UNEXPECTED = (int)(0x8001FFFF - 0x100000000);
            public const int ERROR_AUDITING_DISABLED = (int)(0xC0090001 - 0x100000000);
            public const int ERROR_ALL_SIDS_FILTERED = (int)(0xC0090002 - 0x100000000);
            public const int NTE_BAD_UID = (int)(0x80090001 - 0x100000000);
            public const int NTE_BAD_HASH = (int)(0x80090002 - 0x100000000);
            public const int NTE_BAD_KEY = (int)(0x80090003 - 0x100000000);
            public const int NTE_BAD_LEN = (int)(0x80090004 - 0x100000000);
            public const int NTE_BAD_DATA = (int)(0x80090005 - 0x100000000);
            public const int NTE_BAD_SIGNATURE = (int)(0x80090006 - 0x100000000);
            public const int NTE_BAD_VER = (int)(0x80090007 - 0x100000000);
            public const int NTE_BAD_ALGID = (int)(0x80090008 - 0x100000000);
            public const int NTE_BAD_FLAGS = (int)(0x80090009 - 0x100000000);
            public const int NTE_BAD_TYPE = (int)(0x8009000A - 0x100000000);
            public const int NTE_BAD_KEY_STATE = (int)(0x8009000B - 0x100000000);
            public const int NTE_BAD_HASH_STATE = (int)(0x8009000C - 0x100000000);
            public const int NTE_NO_KEY = (int)(0x8009000D - 0x100000000);
            public const int NTE_NO_MEMORY = (int)(0x8009000E - 0x100000000);
            public const int NTE_EXISTS = (int)(0x8009000F - 0x100000000);
            public const int NTE_PERM = (int)(0x80090010 - 0x100000000);
            public const int NTE_NOT_FOUND = (int)(0x80090011 - 0x100000000);
            public const int NTE_DOUBLE_ENCRYPT = (int)(0x80090012 - 0x100000000);
            public const int NTE_BAD_PROVIDER = (int)(0x80090013 - 0x100000000);
            public const int NTE_BAD_PROV_TYPE = (int)(0x80090014 - 0x100000000);
            public const int NTE_BAD_PUBLIC_KEY = (int)(0x80090015 - 0x100000000);
            public const int NTE_BAD_KEYSET = (int)(0x80090016 - 0x100000000);
            public const int NTE_PROV_TYPE_NOT_DEF = (int)(0x80090017 - 0x100000000);
            public const int NTE_PROV_TYPE_ENTRY_BAD = (int)(0x80090018 - 0x100000000);
            public const int NTE_KEYSET_NOT_DEF = (int)(0x80090019 - 0x100000000);
            public const int NTE_KEYSET_ENTRY_BAD = (int)(0x8009001A - 0x100000000);
            public const int NTE_PROV_TYPE_NO_MATCH = (int)(0x8009001B - 0x100000000);
            public const int NTE_SIGNATURE_FILE_BAD = (int)(0x8009001C - 0x100000000);
            public const int NTE_PROVIDER_DLL_FAIL = (int)(0x8009001D - 0x100000000);
            public const int NTE_PROV_DLL_NOT_FOUND = (int)(0x8009001E - 0x100000000);
            public const int NTE_BAD_KEYSET_PARAM = (int)(0x8009001F - 0x100000000);
            public const int NTE_FAIL = (int)(0x80090020 - 0x100000000);
            public const int NTE_SYS_ERR = (int)(0x80090021 - 0x100000000);
            public const int NTE_SILENT_CONTEXT = (int)(0x80090022 - 0x100000000);
            public const int NTE_TOKEN_KEYSET_STORAGE_FULL = (int)(0x80090023 - 0x100000000);
            public const int NTE_TEMPORARY_PROFILE = (int)(0x80090024 - 0x100000000);
            public const int NTE_FIXEDPARAMETER = (int)(0x80090025 - 0x100000000);
            public const int SEC_E_INSUFFICIENT_MEMORY = (int)(0x80090300 - 0x100000000);
            public const int SEC_E_INVALID_HANDLE = (int)(0x80090301 - 0x100000000);
            public const int SEC_E_UNSUPPORTED_FUNCTION = (int)(0x80090302 - 0x100000000);
            public const int SEC_E_TARGET_UNKNOWN = (int)(0x80090303 - 0x100000000);
            public const int SEC_E_INTERNAL_ERROR = (int)(0x80090304 - 0x100000000);
            public const int SEC_E_SECPKG_NOT_FOUND = (int)(0x80090305 - 0x100000000);
            public const int SEC_E_NOT_OWNER = (int)(0x80090306 - 0x100000000);
            public const int SEC_E_CANNOT_INSTALL = (int)(0x80090307 - 0x100000000);
            public const int SEC_E_INVALID_TOKEN = (int)(0x80090308 - 0x100000000);
            public const int SEC_E_CANNOT_PACK = (int)(0x80090309 - 0x100000000);
            public const int SEC_E_QOP_NOT_SUPPORTED = (int)(0x8009030A - 0x100000000);
            public const int SEC_E_NO_IMPERSONATION = (int)(0x8009030B - 0x100000000);
            public const int SEC_E_LOGON_DENIED = (int)(0x8009030C - 0x100000000);
            public const int SEC_E_UNKNOWN_CREDENTIALS = (int)(0x8009030D - 0x100000000);
            public const int SEC_E_NO_CREDENTIALS = (int)(0x8009030E - 0x100000000);
            public const int SEC_E_MESSAGE_ALTERED = (int)(0x8009030F - 0x100000000);
            public const int SEC_E_OUT_OF_SEQUENCE = (int)(0x80090310 - 0x100000000);
            public const int SEC_E_NO_AUTHENTICATING_AUTHORITY = (int)(0x80090311 - 0x100000000);
            public const int SEC_I_CONTINUE_NEEDED = 0x00090312;
            public const int SEC_I_COMPLETE_NEEDED = 0x00090313;
            public const int SEC_I_COMPLETE_AND_CONTINUE = 0x00090314;
            public const int SEC_I_LOCAL_LOGON = 0x00090315;
            public const int SEC_E_BAD_PKGID = (int)(0x80090316 - 0x100000000);
            public const int SEC_E_CONTEXT_EXPIRED = (int)(0x80090317 - 0x100000000);
            public const int SEC_I_CONTEXT_EXPIRED = 0x00090317;
            public const int SEC_E_INCOMPLETE_MESSAGE = (int)(0x80090318 - 0x100000000);
            public const int SEC_E_INCOMPLETE_CREDENTIALS = (int)(0x80090320 - 0x100000000);
            public const int SEC_E_BUFFER_TOO_SMALL = (int)(0x80090321 - 0x100000000);
            public const int SEC_I_INCOMPLETE_CREDENTIALS = 0x00090320;
            public const int SEC_I_RENEGOTIATE = 0x00090321;
            public const int SEC_E_WRONG_PRINCIPAL = (int)(0x80090322 - 0x100000000);
            public const int SEC_I_NO_LSA_CONTEXT = 0x00090323;
            public const int SEC_E_TIME_SKEW = (int)(0x80090324 - 0x100000000);
            public const int SEC_E_UNTRUSTED_ROOT = (int)(0x80090325 - 0x100000000);
            public const int SEC_E_ILLEGAL_MESSAGE = (int)(0x80090326 - 0x100000000);
            public const int SEC_E_CERT_UNKNOWN = (int)(0x80090327 - 0x100000000);
            public const int SEC_E_CERT_EXPIRED = (int)(0x80090328 - 0x100000000);
            public const int SEC_E_ENCRYPT_FAILURE = (int)(0x80090329 - 0x100000000);
            public const int SEC_E_DECRYPT_FAILURE = (int)(0x80090330 - 0x100000000);
            public const int SEC_E_ALGORITHM_MISMATCH = (int)(0x80090331 - 0x100000000);
            public const int SEC_E_SECURITY_QOS_FAILED = (int)(0x80090332 - 0x100000000);
            public const int SEC_E_UNFINISHED_CONTEXT_DELETED = (int)(0x80090333 - 0x100000000);
            public const int SEC_E_NO_TGT_REPLY = (int)(0x80090334 - 0x100000000);
            public const int SEC_E_NO_IP_ADDRESSES = (int)(0x80090335 - 0x100000000);
            public const int SEC_E_WRONG_CREDENTIAL_HANDLE = (int)(0x80090336 - 0x100000000);
            public const int SEC_E_CRYPTO_SYSTEM_INVALID = (int)(0x80090337 - 0x100000000);
            public const int SEC_E_MAX_REFERRALS_EXCEEDED = (int)(0x80090338 - 0x100000000);
            public const int SEC_E_MUST_BE_KDC = (int)(0x80090339 - 0x100000000);
            public const int SEC_E_STRONG_CRYPTO_NOT_SUPPORTED = (int)(0x8009033A - 0x100000000);
            public const int SEC_E_TOO_MANY_PRINCIPALS = (int)(0x8009033B - 0x100000000);
            public const int SEC_E_NO_PA_DATA = (int)(0x8009033C - 0x100000000);
            public const int SEC_E_PKINIT_NAME_MISMATCH = (int)(0x8009033D - 0x100000000);
            public const int SEC_E_SMARTCARD_LOGON_REQUIRED = (int)(0x8009033E - 0x100000000);
            public const int SEC_E_SHUTDOWN_IN_PROGRESS = (int)(0x8009033F - 0x100000000);
            public const int SEC_E_KDC_INVALID_REQUEST = (int)(0x80090340 - 0x100000000);
            public const int SEC_E_KDC_UNABLE_TO_REFER = (int)(0x80090341 - 0x100000000);
            public const int SEC_E_KDC_UNKNOWN_ETYPE = (int)(0x80090342 - 0x100000000);
            public const int SEC_E_UNSUPPORTED_PREAUTH = (int)(0x80090343 - 0x100000000);
            public const int SEC_E_DELEGATION_REQUIRED = (int)(0x80090345 - 0x100000000);
            public const int SEC_E_BAD_BINDINGS = (int)(0x80090346 - 0x100000000);
            public const int SEC_E_MULTIPLE_ACCOUNTS = (int)(0x80090347 - 0x100000000);
            public const int SEC_E_NO_KERB_KEY = (int)(0x80090348 - 0x100000000);
            public const int SEC_E_CERT_WRONG_USAGE = (int)(0x80090349 - 0x100000000);
            public const int SEC_E_DOWNGRADE_DETECTED = (int)(0x80090350 - 0x100000000);
            public const int SEC_E_SMARTCARD_CERT_REVOKED = (int)(0x80090351 - 0x100000000);
            public const int SEC_E_ISSUING_CA_UNTRUSTED = (int)(0x80090352 - 0x100000000);
            public const int SEC_E_REVOCATION_OFFLINE_C = (int)(0x80090353 - 0x100000000);
            public const int SEC_E_PKINIT_CLIENT_FAILURE = (int)(0x80090354 - 0x100000000);
            public const int SEC_E_SMARTCARD_CERT_EXPIRED = (int)(0x80090355 - 0x100000000);
            public const int SEC_E_NO_S4U_PROT_SUPPORT = (int)(0x80090356 - 0x100000000);
            public const int SEC_E_CROSSREALM_DELEGATION_FAILURE = (int)(0x80090357 - 0x100000000);
            public const int SEC_E_NO_SPM = SEC_E_INTERNAL_ERROR;
            public const int SEC_E_NOT_SUPPORTED = SEC_E_UNSUPPORTED_FUNCTION;
            public const int CRYPT_E_MSG_ERROR = (int)(0x80091001 - 0x100000000);
            public const int CRYPT_E_UNKNOWN_ALGO = (int)(0x80091002 - 0x100000000);
            public const int CRYPT_E_OID_FORMAT = (int)(0x80091003 - 0x100000000);
            public const int CRYPT_E_INVALID_MSG_TYPE = (int)(0x80091004 - 0x100000000);
            public const int CRYPT_E_UNEXPECTED_ENCODING = (int)(0x80091005 - 0x100000000);
            public const int CRYPT_E_AUTH_ATTR_MISSING = (int)(0x80091006 - 0x100000000);
            public const int CRYPT_E_HASH_VALUE = (int)(0x80091007 - 0x100000000);
            public const int CRYPT_E_INVALID_INDEX = (int)(0x80091008 - 0x100000000);
            public const int CRYPT_E_ALREADY_DECRYPTED = (int)(0x80091009 - 0x100000000);
            public const int CRYPT_E_NOT_DECRYPTED = (int)(0x8009100A - 0x100000000);
            public const int CRYPT_E_RECIPIENT_NOT_FOUND = (int)(0x8009100B - 0x100000000);
            public const int CRYPT_E_CONTROL_TYPE = (int)(0x8009100C - 0x100000000);
            public const int CRYPT_E_ISSUER_SERIALNUMBER = (int)(0x8009100D - 0x100000000);
            public const int CRYPT_E_SIGNER_NOT_FOUND = (int)(0x8009100E - 0x100000000);
            public const int CRYPT_E_ATTRIBUTES_MISSING = (int)(0x8009100F - 0x100000000);
            public const int CRYPT_E_STREAM_MSG_NOT_READY = (int)(0x80091010 - 0x100000000);
            public const int CRYPT_E_STREAM_INSUFFICIENT_DATA = (int)(0x80091011 - 0x100000000);
            public const int CRYPT_I_NEW_PROTECTION_REQUIRED = 0x00091012;
            public const int CRYPT_E_BAD_LEN = (int)(0x80092001 - 0x100000000);
            public const int CRYPT_E_BAD_ENCODE = (int)(0x80092002 - 0x100000000);
            public const int CRYPT_E_FILE_ERROR = (int)(0x80092003 - 0x100000000);
            public const int CRYPT_E_NOT_FOUND = (int)(0x80092004 - 0x100000000);
            public const int CRYPT_E_EXISTS = (int)(0x80092005 - 0x100000000);
            public const int CRYPT_E_NO_PROVIDER = (int)(0x80092006 - 0x100000000);
            public const int CRYPT_E_SELF_SIGNED = (int)(0x80092007 - 0x100000000);
            public const int CRYPT_E_DELETED_PREV = (int)(0x80092008 - 0x100000000);
            public const int CRYPT_E_NO_MATCH = (int)(0x80092009 - 0x100000000);
            public const int CRYPT_E_UNEXPECTED_MSG_TYPE = (int)(0x8009200A - 0x100000000);
            public const int CRYPT_E_NO_KEY_PROPERTY = (int)(0x8009200B - 0x100000000);
            public const int CRYPT_E_NO_DECRYPT_CERT = (int)(0x8009200C - 0x100000000);
            public const int CRYPT_E_BAD_MSG = (int)(0x8009200D - 0x100000000);
            public const int CRYPT_E_NO_SIGNER = (int)(0x8009200E - 0x100000000);
            public const int CRYPT_E_PENDING_CLOSE = (int)(0x8009200F - 0x100000000);
            public const int CRYPT_E_REVOKED = (int)(0x80092010 - 0x100000000);
            public const int CRYPT_E_NO_REVOCATION_DLL = (int)(0x80092011 - 0x100000000);
            public const int CRYPT_E_NO_REVOCATION_CHECK = (int)(0x80092012 - 0x100000000);
            public const int CRYPT_E_REVOCATION_OFFLINE = (int)(0x80092013 - 0x100000000);
            public const int CRYPT_E_NOT_IN_REVOCATION_DATABASE = (int)(0x80092014 - 0x100000000);
            public const int CRYPT_E_INVALID_NUMERIC_STRING = (int)(0x80092020 - 0x100000000);
            public const int CRYPT_E_INVALID_PRINTABLE_STRING = (int)(0x80092021 - 0x100000000);
            public const int CRYPT_E_INVALID_IA5_STRING = (int)(0x80092022 - 0x100000000);
            public const int CRYPT_E_INVALID_X500_STRING = (int)(0x80092023 - 0x100000000);
            public const int CRYPT_E_NOT_CHAR_STRING = (int)(0x80092024 - 0x100000000);
            public const int CRYPT_E_FILERESIZED = (int)(0x80092025 - 0x100000000);
            public const int CRYPT_E_SECURITY_SETTINGS = (int)(0x80092026 - 0x100000000);
            public const int CRYPT_E_NO_VERIFY_USAGE_DLL = (int)(0x80092027 - 0x100000000);
            public const int CRYPT_E_NO_VERIFY_USAGE_CHECK = (int)(0x80092028 - 0x100000000);
            public const int CRYPT_E_VERIFY_USAGE_OFFLINE = (int)(0x80092029 - 0x100000000);
            public const int CRYPT_E_NOT_IN_CTL = (int)(0x8009202A - 0x100000000);
            public const int CRYPT_E_NO_TRUSTED_SIGNER = (int)(0x8009202B - 0x100000000);
            public const int CRYPT_E_MISSING_PUBKEY_PARA = (int)(0x8009202C - 0x100000000);
            public const int CRYPT_E_OSS_ERROR = (int)(0x80093000 - 0x100000000);
            public const int OSS_MORE_BUF = (int)(0x80093001 - 0x100000000);
            public const int OSS_NEGATIVE_UINTEGER = (int)(0x80093002 - 0x100000000);
            public const int OSS_PDU_RANGE = (int)(0x80093003 - 0x100000000);
            public const int OSS_MORE_INPUT = (int)(0x80093004 - 0x100000000);
            public const int OSS_DATA_ERROR = (int)(0x80093005 - 0x100000000);
            public const int OSS_BAD_ARG = (int)(0x80093006 - 0x100000000);
            public const int OSS_BAD_VERSION = (int)(0x80093007 - 0x100000000);
            public const int OSS_OUT_MEMORY = (int)(0x80093008 - 0x100000000);
            public const int OSS_PDU_MISMATCH = (int)(0x80093009 - 0x100000000);
            public const int OSS_LIMITED = (int)(0x8009300A - 0x100000000);
            public const int OSS_BAD_PTR = (int)(0x8009300B - 0x100000000);
            public const int OSS_BAD_TIME = (int)(0x8009300C - 0x100000000);
            public const int OSS_INDEFINITE_NOT_SUPPORTED = (int)(0x8009300D - 0x100000000);
            public const int OSS_MEM_ERROR = (int)(0x8009300E - 0x100000000);
            public const int OSS_BAD_TABLE = (int)(0x8009300F - 0x100000000);
            public const int OSS_TOO_Int32 = (int)(0x80093010 - 0x100000000);
            public const int OSS_CONSTRAINT_VIOLATED = (int)(0x80093011 - 0x100000000);
            public const int OSS_FATAL_ERROR = (int)(0x80093012 - 0x100000000);
            public const int OSS_ACCESS_SERIALIZATION_ERROR = (int)(0x80093013 - 0x100000000);
            public const int OSS_NULL_TBL = (int)(0x80093014 - 0x100000000);
            public const int OSS_NULL_FCN = (int)(0x80093015 - 0x100000000);
            public const int OSS_BAD_ENCRULES = (int)(0x80093016 - 0x100000000);
            public const int OSS_UNAVAIL_ENCRULES = (int)(0x80093017 - 0x100000000);
            public const int OSS_CANT_OPEN_TRACE_WINDOW = (int)(0x80093018 - 0x100000000);
            public const int OSS_UNIMPLEMENTED = (int)(0x80093019 - 0x100000000);
            public const int OSS_OID_DLL_NOT_LINKED = (int)(0x8009301A - 0x100000000);
            public const int OSS_CANT_OPEN_TRACE_FILE = (int)(0x8009301B - 0x100000000);
            public const int OSS_TRACE_FILE_ALREADY_OPEN = (int)(0x8009301C - 0x100000000);
            public const int OSS_TABLE_MISMATCH = (int)(0x8009301D - 0x100000000);
            public const int OSS_TYPE_NOT_SUPPORTED = (int)(0x8009301E - 0x100000000);
            public const int OSS_REAL_DLL_NOT_LINKED = (int)(0x8009301F - 0x100000000);
            public const int OSS_REAL_CODE_NOT_LINKED = (int)(0x80093020 - 0x100000000);
            public const int OSS_OUT_OF_RANGE = (int)(0x80093021 - 0x100000000);
            public const int OSS_COPIER_DLL_NOT_LINKED = (int)(0x80093022 - 0x100000000);
            public const int OSS_CONSTRAINT_DLL_NOT_LINKED = (int)(0x80093023 - 0x100000000);
            public const int OSS_COMPARATOR_DLL_NOT_LINKED = (int)(0x80093024 - 0x100000000);
            public const int OSS_COMPARATOR_CODE_NOT_LINKED = (int)(0x80093025 - 0x100000000);
            public const int OSS_MEM_MGR_DLL_NOT_LINKED = (int)(0x80093026 - 0x100000000);
            public const int OSS_PDV_DLL_NOT_LINKED = (int)(0x80093027 - 0x100000000);
            public const int OSS_PDV_CODE_NOT_LINKED = (int)(0x80093028 - 0x100000000);
            public const int OSS_API_DLL_NOT_LINKED = (int)(0x80093029 - 0x100000000);
            public const int OSS_BERDER_DLL_NOT_LINKED = (int)(0x8009302A - 0x100000000);
            public const int OSS_PER_DLL_NOT_LINKED = (int)(0x8009302B - 0x100000000);
            public const int OSS_OPEN_TYPE_ERROR = (int)(0x8009302C - 0x100000000);
            public const int OSS_MUTEX_NOT_CREATED = (int)(0x8009302D - 0x100000000);
            public const int OSS_CANT_CLOSE_TRACE_FILE = (int)(0x8009302E - 0x100000000);
            public const int CRYPT_E_ASN1_ERROR = (int)(0x80093100 - 0x100000000);
            public const int CRYPT_E_ASN1_INTERNAL = (int)(0x80093101 - 0x100000000);
            public const int CRYPT_E_ASN1_EOD = (int)(0x80093102 - 0x100000000);
            public const int CRYPT_E_ASN1_CORRUPT = (int)(0x80093103 - 0x100000000);
            public const int CRYPT_E_ASN1_LARGE = (int)(0x80093104 - 0x100000000);
            public const int CRYPT_E_ASN1_CONSTRAINT = (int)(0x80093105 - 0x100000000);
            public const int CRYPT_E_ASN1_MEMORY = (int)(0x80093106 - 0x100000000);
            public const int CRYPT_E_ASN1_OVERFLOW = (int)(0x80093107 - 0x100000000);
            public const int CRYPT_E_ASN1_BADPDU = (int)(0x80093108 - 0x100000000);
            public const int CRYPT_E_ASN1_BADARGS = (int)(0x80093109 - 0x100000000);
            public const int CRYPT_E_ASN1_BADREAL = (int)(0x8009310A - 0x100000000);
            public const int CRYPT_E_ASN1_BADTAG = (int)(0x8009310B - 0x100000000);
            public const int CRYPT_E_ASN1_CHOICE = (int)(0x8009310C - 0x100000000);
            public const int CRYPT_E_ASN1_RULE = (int)(0x8009310D - 0x100000000);
            public const int CRYPT_E_ASN1_UTF8 = (int)(0x8009310E - 0x100000000);
            public const int CRYPT_E_ASN1_PDU_TYPE = (int)(0x80093133 - 0x100000000);
            public const int CRYPT_E_ASN1_NYI = (int)(0x80093134 - 0x100000000);
            public const int CRYPT_E_ASN1_EXTENDED = (int)(0x80093201 - 0x100000000);
            public const int CRYPT_E_ASN1_NOEOD = (int)(0x80093202 - 0x100000000);
            public const int CERTSRV_E_BAD_REQUESTSUBJECT = (int)(0x80094001 - 0x100000000);
            public const int CERTSRV_E_NO_REQUEST = (int)(0x80094002 - 0x100000000);
            public const int CERTSRV_E_BAD_REQUESTSTATUS = (int)(0x80094003 - 0x100000000);
            public const int CERTSRV_E_PROPERTY_EMPTY = (int)(0x80094004 - 0x100000000);
            public const int CERTSRV_E_INVALID_CA_CERTIFICATE = (int)(0x80094005 - 0x100000000);
            public const int CERTSRV_E_SERVER_SUSPENDED = (int)(0x80094006 - 0x100000000);
            public const int CERTSRV_E_ENCODING_LENGTH = (int)(0x80094007 - 0x100000000);
            public const int CERTSRV_E_ROLECONFLICT = (int)(0x80094008 - 0x100000000);
            public const int CERTSRV_E_RESTRICTEDOFFICER = (int)(0x80094009 - 0x100000000);
            public const int CERTSRV_E_KEY_ARCHIVAL_NOT_CONFIGURED = (int)(0x8009400A - 0x100000000);
            public const int CERTSRV_E_NO_VALID_KRA = (int)(0x8009400B - 0x100000000);
            public const int CERTSRV_E_BAD_REQUEST_KEY_ARCHIVAL = (int)(0x8009400C - 0x100000000);
            public const int CERTSRV_E_NO_CAADMIN_DEFINED = (int)(0x8009400D - 0x100000000);
            public const int CERTSRV_E_BAD_RENEWAL_CERT_ATTRIBUTE = (int)(0x8009400E - 0x100000000);
            public const int CERTSRV_E_NO_DB_SESSIONS = (int)(0x8009400F - 0x100000000);
            public const int CERTSRV_E_ALIGNMENT_FAULT = (int)(0x80094010 - 0x100000000);
            public const int CERTSRV_E_ENROLL_DENIED = (int)(0x80094011 - 0x100000000);
            public const int CERTSRV_E_TEMPLATE_DENIED = (int)(0x80094012 - 0x100000000);
            public const int CERTSRV_E_DOWNLEVEL_DC_SSL_OR_UPGRADE = (int)(0x80094013 - 0x100000000);
            public const int CERTSRV_E_UNSUPPORTED_CERT_TYPE = (int)(0x80094800 - 0x100000000);
            public const int CERTSRV_E_NO_CERT_TYPE = (int)(0x80094801 - 0x100000000);
            public const int CERTSRV_E_TEMPLATE_CONFLICT = (int)(0x80094802 - 0x100000000);
            public const int CERTSRV_E_SUBJECT_ALT_NAME_REQUIRED = (int)(0x80094803 - 0x100000000);
            public const int CERTSRV_E_ARCHIVED_KEY_REQUIRED = (int)(0x80094804 - 0x100000000);
            public const int CERTSRV_E_SMIME_REQUIRED = (int)(0x80094805 - 0x100000000);
            public const int CERTSRV_E_BAD_RENEWAL_SUBJECT = (int)(0x80094806 - 0x100000000);
            public const int CERTSRV_E_BAD_TEMPLATE_VERSION = (int)(0x80094807 - 0x100000000);
            public const int CERTSRV_E_TEMPLATE_POLICY_REQUIRED = (int)(0x80094808 - 0x100000000);
            public const int CERTSRV_E_SIGNATURE_POLICY_REQUIRED = (int)(0x80094809 - 0x100000000);
            public const int CERTSRV_E_SIGNATURE_COUNT = (int)(0x8009480A - 0x100000000);
            public const int CERTSRV_E_SIGNATURE_REJECTED = (int)(0x8009480B - 0x100000000);
            public const int CERTSRV_E_ISSUANCE_POLICY_REQUIRED = (int)(0x8009480C - 0x100000000);
            public const int CERTSRV_E_SUBJECT_UPN_REQUIRED = (int)(0x8009480D - 0x100000000);
            public const int CERTSRV_E_SUBJECT_DIRECTORY_GUID_REQUIRED = (int)(0x8009480E - 0x100000000);
            public const int CERTSRV_E_SUBJECT_DNS_REQUIRED = (int)(0x8009480F - 0x100000000);
            public const int CERTSRV_E_ARCHIVED_KEY_UNEXPECTED = (int)(0x80094810 - 0x100000000);
            public const int CERTSRV_E_KEY_LENGTH = (int)(0x80094811 - 0x100000000);
            public const int CERTSRV_E_SUBJECT_EMAIL_REQUIRED = (int)(0x80094812 - 0x100000000);
            public const int CERTSRV_E_UNKNOWN_CERT_TYPE = (int)(0x80094813 - 0x100000000);
            public const int CERTSRV_E_CERT_TYPE_OVERLAP = (int)(0x80094814 - 0x100000000);
            public const int XENROLL_E_KEY_NOT_EXPORTABLE = (int)(0x80095000 - 0x100000000);
            public const int XENROLL_E_CANNOT_ADD_ROOT_CERT = (int)(0x80095001 - 0x100000000);
            public const int XENROLL_E_RESPONSE_KA_HASH_NOT_FOUND = (int)(0x80095002 - 0x100000000);
            public const int XENROLL_E_RESPONSE_UNEXPECTED_KA_HASH = (int)(0x80095003 - 0x100000000);
            public const int XENROLL_E_RESPONSE_KA_HASH_MISMATCH = (int)(0x80095004 - 0x100000000);
            public const int XENROLL_E_KEYSPEC_SMIME_MISMATCH = (int)(0x80095005 - 0x100000000);
            public const int TRUST_E_SYSTEM_ERROR = (int)(0x80096001 - 0x100000000);
            public const int TRUST_E_NO_SIGNER_CERT = (int)(0x80096002 - 0x100000000);
            public const int TRUST_E_COUNTER_SIGNER = (int)(0x80096003 - 0x100000000);
            public const int TRUST_E_CERT_SIGNATURE = (int)(0x80096004 - 0x100000000);
            public const int TRUST_E_TIME_STAMP = (int)(0x80096005 - 0x100000000);
            public const int TRUST_E_BAD_DIGEST = (int)(0x80096010 - 0x100000000);
            public const int TRUST_E_BASIC_CONSTRAINTS = (int)(0x80096019 - 0x100000000);
            public const int TRUST_E_FINANCIAL_CRITERIA = (int)(0x8009601E - 0x100000000);
            public const int MSSIPOTF_E_OUTOFMEMRANGE = (int)(0x80097001 - 0x100000000);
            public const int MSSIPOTF_E_CANTGETOBJECT = (int)(0x80097002 - 0x100000000);
            public const int MSSIPOTF_E_NOHEADTABLE = (int)(0x80097003 - 0x100000000);
            public const int MSSIPOTF_E_BAD_MAGICNUMBER = (int)(0x80097004 - 0x100000000);
            public const int MSSIPOTF_E_BAD_OFFSET_TABLE = (int)(0x80097005 - 0x100000000);
            public const int MSSIPOTF_E_TABLE_TAGORDER = (int)(0x80097006 - 0x100000000);
            public const int MSSIPOTF_E_TABLE_Int32UInt16 = (int)(0x80097007 - 0x100000000);
            public const int MSSIPOTF_E_BAD_FIRST_TABLE_PLACEMENT = (int)(0x80097008 - 0x100000000);
            public const int MSSIPOTF_E_TABLES_OVERLAP = (int)(0x80097009 - 0x100000000);
            public const int MSSIPOTF_E_TABLE_PADBYTES = (int)(0x8009700A - 0x100000000);
            public const int MSSIPOTF_E_FILETOOSMALL = (int)(0x8009700B - 0x100000000);
            public const int MSSIPOTF_E_TABLE_CHECKSUM = (int)(0x8009700C - 0x100000000);
            public const int MSSIPOTF_E_FILE_CHECKSUM = (int)(0x8009700D - 0x100000000);
            public const int MSSIPOTF_E_FAILED_POLICY = (int)(0x80097010 - 0x100000000);
            public const int MSSIPOTF_E_FAILED_HINTS_CHECK = (int)(0x80097011 - 0x100000000);
            public const int MSSIPOTF_E_NOT_OPENTYPE = (int)(0x80097012 - 0x100000000);
            public const int MSSIPOTF_E_FILE = (int)(0x80097013 - 0x100000000);
            public const int MSSIPOTF_E_CRYPT = (int)(0x80097014 - 0x100000000);
            public const int MSSIPOTF_E_BADVERSION = (int)(0x80097015 - 0x100000000);
            public const int MSSIPOTF_E_DSIG_STRUCTURE = (int)(0x80097016 - 0x100000000);
            public const int MSSIPOTF_E_PCONST_CHECK = (int)(0x80097017 - 0x100000000);
            public const int MSSIPOTF_E_STRUCTURE = (int)(0x80097018 - 0x100000000);
            public const int NTE_OP_OK = 0;
            public const int TRUST_E_PROVIDER_UNKNOWN = (int)(0x800B0001 - 0x100000000);
            public const int TRUST_E_ACTION_UNKNOWN = (int)(0x800B0002 - 0x100000000);
            public const int TRUST_E_SUBJECT_FORM_UNKNOWN = (int)(0x800B0003 - 0x100000000);
            public const int TRUST_E_SUBJECT_NOT_TRUSTED = (int)(0x800B0004 - 0x100000000);
            public const int DIGSIG_E_ENCODE = (int)(0x800B0005 - 0x100000000);
            public const int DIGSIG_E_DECODE = (int)(0x800B0006 - 0x100000000);
            public const int DIGSIG_E_EXTENSIBILITY = (int)(0x800B0007 - 0x100000000);
            public const int DIGSIG_E_CRYPTO = (int)(0x800B0008 - 0x100000000);
            public const int PERSIST_E_SIZEDEFINITE = (int)(0x800B0009 - 0x100000000);
            public const int PERSIST_E_SIZEINDEFINITE = (int)(0x800B000A - 0x100000000);
            public const int PERSIST_E_NOTSELFSIZING = (int)(0x800B000B - 0x100000000);
            public const int TRUST_E_NOSIGNATURE = (int)(0x800B0100 - 0x100000000);
            public const int CERT_E_EXPIRED = (int)(0x800B0101 - 0x100000000);
            public const int CERT_E_VALIDITYPERIODNESTING = (int)(0x800B0102 - 0x100000000);
            public const int CERT_E_ROLE = (int)(0x800B0103 - 0x100000000);
            public const int CERT_E_PATHLENCONST = (int)(0x800B0104 - 0x100000000);
            public const int CERT_E_CRITICAL = (int)(0x800B0105 - 0x100000000);
            public const int CERT_E_PURPOSE = (int)(0x800B0106 - 0x100000000);
            public const int CERT_E_ISSUERCHAINING = (int)(0x800B0107 - 0x100000000);
            public const int CERT_E_MALFORMED = (int)(0x800B0108 - 0x100000000);
            public const int CERT_E_UNTRUSTEDROOT = (int)(0x800B0109 - 0x100000000);
            public const int CERT_E_CHAINING = (int)(0x800B010A - 0x100000000);
            public const int TRUST_E_FAIL = (int)(0x800B010B - 0x100000000);
            public const int CERT_E_REVOKED = (int)(0x800B010C - 0x100000000);
            public const int CERT_E_UNTRUSTEDTESTROOT = (int)(0x800B010D - 0x100000000);
            public const int CERT_E_REVOCATION_FAILURE = (int)(0x800B010E - 0x100000000);
            public const int CERT_E_CN_NO_MATCH = (int)(0x800B010F - 0x100000000);
            public const int CERT_E_WRONG_USAGE = (int)(0x800B0110 - 0x100000000);
            public const int TRUST_E_EXPLICIT_DISTRUST = (int)(0x800B0111 - 0x100000000);
            public const int CERT_E_UNTRUSTEDCA = (int)(0x800B0112 - 0x100000000);
            public const int CERT_E_INVALID_POLICY = (int)(0x800B0113 - 0x100000000);
            public const int CERT_E_INVALID_NAME = (int)(0x800B0114 - 0x100000000);
            public const int SPAPI_E_EXPECTED_SECTION_NAME = (int)(0x800F0000 - 0x100000000);
            public const int SPAPI_E_BAD_SECTION_NAME_LINE = (int)(0x800F0001 - 0x100000000);
            public const int SPAPI_E_SECTION_NAME_TOO_Int32 = (int)(0x800F0002 - 0x100000000);
            public const int SPAPI_E_GENERAL_SYNTAX = (int)(0x800F0003 - 0x100000000);
            public const int SPAPI_E_WRONG_INF_STYLE = (int)(0x800F0100 - 0x100000000);
            public const int SPAPI_E_SECTION_NOT_FOUND = (int)(0x800F0101 - 0x100000000);
            public const int SPAPI_E_LINE_NOT_FOUND = (int)(0x800F0102 - 0x100000000);
            public const int SPAPI_E_NO_BACKUP = (int)(0x800F0103 - 0x100000000);
            public const int SPAPI_E_NO_ASSOCIATED_CLASS = (int)(0x800F0200 - 0x100000000);
            public const int SPAPI_E_CLASS_MISMATCH = (int)(0x800F0201 - 0x100000000);
            public const int SPAPI_E_DUPLICATE_FOUND = (int)(0x800F0202 - 0x100000000);
            public const int SPAPI_E_NO_DRIVER_SELECTED = (int)(0x800F0203 - 0x100000000);
            public const int SPAPI_E_KEY_DOES_NOT_EXIST = (int)(0x800F0204 - 0x100000000);
            public const int SPAPI_E_INVALID_DEVINST_NAME = (int)(0x800F0205 - 0x100000000);
            public const int SPAPI_E_INVALID_CLASS = (int)(0x800F0206 - 0x100000000);
            public const int SPAPI_E_DEVINST_ALREADY_EXISTS = (int)(0x800F0207 - 0x100000000);
            public const int SPAPI_E_DEVINFO_NOT_REGISTERED = (int)(0x800F0208 - 0x100000000);
            public const int SPAPI_E_INVALID_REG_PROPERTY = (int)(0x800F0209 - 0x100000000);
            public const int SPAPI_E_NO_INF = (int)(0x800F020A - 0x100000000);
            public const int SPAPI_E_NO_SUCH_DEVINST = (int)(0x800F020B - 0x100000000);
            public const int SPAPI_E_CANT_LOAD_CLASS_ICON = (int)(0x800F020C - 0x100000000);
            public const int SPAPI_E_INVALID_CLASS_INSTALLER = (int)(0x800F020D - 0x100000000);
            public const int SPAPI_E_DI_DO_DEFAULT = (int)(0x800F020E - 0x100000000);
            public const int SPAPI_E_DI_NOFILECOPY = (int)(0x800F020F - 0x100000000);
            public const int SPAPI_E_INVALID_HWPROFILE = (int)(0x800F0210 - 0x100000000);
            public const int SPAPI_E_NO_DEVICE_SELECTED = (int)(0x800F0211 - 0x100000000);
            public const int SPAPI_E_DEVINFO_LIST_LOCKED = (int)(0x800F0212 - 0x100000000);
            public const int SPAPI_E_DEVINFO_DATA_LOCKED = (int)(0x800F0213 - 0x100000000);
            public const int SPAPI_E_DI_BAD_PATH = (int)(0x800F0214 - 0x100000000);
            public const int SPAPI_E_NO_CLASSINSTALL_PARAMS = (int)(0x800F0215 - 0x100000000);
            public const int SPAPI_E_FILEQUEUE_LOCKED = (int)(0x800F0216 - 0x100000000);
            public const int SPAPI_E_BAD_SERVICE_INSTALLSECT = (int)(0x800F0217 - 0x100000000);
            public const int SPAPI_E_NO_CLASS_DRIVER_LIST = (int)(0x800F0218 - 0x100000000);
            public const int SPAPI_E_NO_ASSOCIATED_SERVICE = (int)(0x800F0219 - 0x100000000);
            public const int SPAPI_E_NO_DEFAULT_DEVICE_INTERFACE = (int)(0x800F021A - 0x100000000);
            public const int SPAPI_E_DEVICE_INTERFACE_ACTIVE = (int)(0x800F021B - 0x100000000);
            public const int SPAPI_E_DEVICE_INTERFACE_REMOVED = (int)(0x800F021C - 0x100000000);
            public const int SPAPI_E_BAD_INTERFACE_INSTALLSECT = (int)(0x800F021D - 0x100000000);
            public const int SPAPI_E_NO_SUCH_INTERFACE_CLASS = (int)(0x800F021E - 0x100000000);
            public const int SPAPI_E_INVALID_REFERENCE_STRING = (int)(0x800F021F - 0x100000000);
            public const int SPAPI_E_INVALID_MACHINENAME = (int)(0x800F0220 - 0x100000000);
            public const int SPAPI_E_REMOTE_COMM_FAILURE = (int)(0x800F0221 - 0x100000000);
            public const int SPAPI_E_MACHINE_UNAVAILABLE = (int)(0x800F0222 - 0x100000000);
            public const int SPAPI_E_NO_CONFIGMGR_SERVICES = (int)(0x800F0223 - 0x100000000);
            public const int SPAPI_E_INVALID_PROPPAGE_PROVIDER = (int)(0x800F0224 - 0x100000000);
            public const int SPAPI_E_NO_SUCH_DEVICE_INTERFACE = (int)(0x800F0225 - 0x100000000);
            public const int SPAPI_E_DI_POSTPROCESSING_REQUIRED = (int)(0x800F0226 - 0x100000000);
            public const int SPAPI_E_INVALID_COINSTALLER = (int)(0x800F0227 - 0x100000000);
            public const int SPAPI_E_NO_COMPAT_DRIVERS = (int)(0x800F0228 - 0x100000000);
            public const int SPAPI_E_NO_DEVICE_ICON = (int)(0x800F0229 - 0x100000000);
            public const int SPAPI_E_INVALID_INF_LOGCONFIG = (int)(0x800F022A - 0x100000000);
            public const int SPAPI_E_DI_DONT_INSTALL = (int)(0x800F022B - 0x100000000);
            public const int SPAPI_E_INVALID_FILTER_DRIVER = (int)(0x800F022C - 0x100000000);
            public const int SPAPI_E_NON_WINDOWS_NT_DRIVER = (int)(0x800F022D - 0x100000000);
            public const int SPAPI_E_NON_WINDOWS_DRIVER = (int)(0x800F022E - 0x100000000);
            public const int SPAPI_E_NO_CATALOG_FOR_OEM_INF = (int)(0x800F022F - 0x100000000);
            public const int SPAPI_E_DEVINSTALL_QUEUE_NONNATIVE = (int)(0x800F0230 - 0x100000000);
            public const int SPAPI_E_NOT_DISABLEABLE = (int)(0x800F0231 - 0x100000000);
            public const int SPAPI_E_CANT_REMOVE_DEVINST = (int)(0x800F0232 - 0x100000000);
            public const int SPAPI_E_INVALID_TARGET = (int)(0x800F0233 - 0x100000000);
            public const int SPAPI_E_DRIVER_NONNATIVE = (int)(0x800F0234 - 0x100000000);
            public const int SPAPI_E_IN_WOW64 = (int)(0x800F0235 - 0x100000000);
            public const int SPAPI_E_SET_SYSTEM_RESTORE_POINT = (int)(0x800F0236 - 0x100000000);
            public const int SPAPI_E_INCORRECTLY_COPIED_INF = (int)(0x800F0237 - 0x100000000);
            public const int SPAPI_E_SCE_DISABLED = (int)(0x800F0238 - 0x100000000);
            public const int SPAPI_E_ERROR_NOT_INSTALLED = (int)(0x800F1000 - 0x100000000);
            public const int SCARD_F_INTERNAL_ERROR = (int)(0x80100001 - 0x100000000);
            public const int SCARD_E_CANCELLED = (int)(0x80100002 - 0x100000000);
            public const int SCARD_E_INVALID_HANDLE = (int)(0x80100003 - 0x100000000);
            public const int SCARD_E_INVALID_PARAMETER = (int)(0x80100004 - 0x100000000);
            public const int SCARD_E_INVALID_TARGET = (int)(0x80100005 - 0x100000000);
            public const int SCARD_E_NO_MEMORY = (int)(0x80100006 - 0x100000000);
            public const int SCARD_F_WAITED_TOO_Int32 = (int)(0x80100007 - 0x100000000);
            public const int SCARD_E_INSUFFICIENT_BUFFER = (int)(0x80100008 - 0x100000000);
            public const int SCARD_E_UNKNOWN_READER = (int)(0x80100009 - 0x100000000);
            public const int SCARD_E_TIMEOUT = (int)(0x8010000A - 0x100000000);
            public const int SCARD_E_SHARING_VIOLATION = (int)(0x8010000B - 0x100000000);
            public const int SCARD_E_NO_SMARTCARD = (int)(0x8010000C - 0x100000000);
            public const int SCARD_E_UNKNOWN_CARD = (int)(0x8010000D - 0x100000000);
            public const int SCARD_E_CANT_DISPOSE = (int)(0x8010000E - 0x100000000);
            public const int SCARD_E_PROTO_MISMATCH = (int)(0x8010000F - 0x100000000);
            public const int SCARD_E_NOT_READY = (int)(0x80100010 - 0x100000000);
            public const int SCARD_E_INVALID_VALUE = (int)(0x80100011 - 0x100000000);
            public const int SCARD_E_SYSTEM_CANCELLED = (int)(0x80100012 - 0x100000000);
            public const int SCARD_F_COMM_ERROR = (int)(0x80100013 - 0x100000000);
            public const int SCARD_F_UNKNOWN_ERROR = (int)(0x80100014 - 0x100000000);
            public const int SCARD_E_INVALID_ATR = (int)(0x80100015 - 0x100000000);
            public const int SCARD_E_NOT_TRANSACTED = (int)(0x80100016 - 0x100000000);
            public const int SCARD_E_READER_UNAVAILABLE = (int)(0x80100017 - 0x100000000);
            public const int SCARD_P_SHUTDOWN = (int)(0x80100018 - 0x100000000);
            public const int SCARD_E_PCI_TOO_SMALL = (int)(0x80100019 - 0x100000000);
            public const int SCARD_E_READER_UNSUPPORTED = (int)(0x8010001A - 0x100000000);
            public const int SCARD_E_DUPLICATE_READER = (int)(0x8010001B - 0x100000000);
            public const int SCARD_E_CARD_UNSUPPORTED = (int)(0x8010001C - 0x100000000);
            public const int SCARD_E_NO_SERVICE = (int)(0x8010001D - 0x100000000);
            public const int SCARD_E_SERVICE_STOPPED = (int)(0x8010001E - 0x100000000);
            public const int SCARD_E_UNEXPECTED = (int)(0x8010001F - 0x100000000);
            public const int SCARD_E_ICC_INSTALLATION = (int)(0x80100020 - 0x100000000);
            public const int SCARD_E_ICC_CREATEORDER = (int)(0x80100021 - 0x100000000);
            public const int SCARD_E_UNSUPPORTED_FEATURE = (int)(0x80100022 - 0x100000000);
            public const int SCARD_E_DIR_NOT_FOUND = (int)(0x80100023 - 0x100000000);
            public const int SCARD_E_FILE_NOT_FOUND = (int)(0x80100024 - 0x100000000);
            public const int SCARD_E_NO_DIR = (int)(0x80100025 - 0x100000000);
            public const int SCARD_E_NO_FILE = (int)(0x80100026 - 0x100000000);
            public const int SCARD_E_NO_ACCESS = (int)(0x80100027 - 0x100000000);
            public const int SCARD_E_WRITE_TOO_MANY = (int)(0x80100028 - 0x100000000);
            public const int SCARD_E_BAD_SEEK = (int)(0x80100029 - 0x100000000);
            public const int SCARD_E_INVALID_CHV = (int)(0x8010002A - 0x100000000);
            public const int SCARD_E_UNKNOWN_RES_MNG = (int)(0x8010002B - 0x100000000);
            public const int SCARD_E_NO_SUCH_CERTIFICATE = (int)(0x8010002C - 0x100000000);
            public const int SCARD_E_CERTIFICATE_UNAVAILABLE = (int)(0x8010002D - 0x100000000);
            public const int SCARD_E_NO_READERS_AVAILABLE = (int)(0x8010002E - 0x100000000);
            public const int SCARD_E_COMM_DATA_LOST = (int)(0x8010002F - 0x100000000);
            public const int SCARD_E_NO_KEY_CONTAINER = (int)(0x80100030 - 0x100000000);
            public const int SCARD_E_SERVER_TOO_BUSY = (int)(0x80100031 - 0x100000000);
            public const int SCARD_W_UNSUPPORTED_CARD = (int)(0x80100065 - 0x100000000);
            public const int SCARD_W_UNRESPONSIVE_CARD = (int)(0x80100066 - 0x100000000);
            public const int SCARD_W_UNPOWERED_CARD = (int)(0x80100067 - 0x100000000);
            public const int SCARD_W_RESET_CARD = (int)(0x80100068 - 0x100000000);
            public const int SCARD_W_REMOVED_CARD = (int)(0x80100069 - 0x100000000);
            public const int SCARD_W_SECURITY_VIOLATION = (int)(0x8010006A - 0x100000000);
            public const int SCARD_W_WRONG_CHV = (int)(0x8010006B - 0x100000000);
            public const int SCARD_W_CHV_BLOCKED = (int)(0x8010006C - 0x100000000);
            public const int SCARD_W_EOF = (int)(0x8010006D - 0x100000000);
            public const int SCARD_W_CANCELLED_BY_USER = (int)(0x8010006E - 0x100000000);
            public const int SCARD_W_CARD_NOT_AUTHENTICATED = (int)(0x8010006F - 0x100000000);
            public const int COMADMIN_E_OBJECTERRORS = (int)(0x80110401 - 0x100000000);
            public const int COMADMIN_E_OBJECTINVALID = (int)(0x80110402 - 0x100000000);
            public const int COMADMIN_E_KEYMISSING = (int)(0x80110403 - 0x100000000);
            public const int COMADMIN_E_ALREADYINSTALLED = (int)(0x80110404 - 0x100000000);
            public const int COMADMIN_E_APP_FILE_WRITEFAIL = (int)(0x80110407 - 0x100000000);
            public const int COMADMIN_E_APP_FILE_READFAIL = (int)(0x80110408 - 0x100000000);
            public const int COMADMIN_E_APP_FILE_VERSION = (int)(0x80110409 - 0x100000000);
            public const int COMADMIN_E_BADPATH = (int)(0x8011040A - 0x100000000);
            public const int COMADMIN_E_APPLICATIONEXISTS = (int)(0x8011040B - 0x100000000);
            public const int COMADMIN_E_ROLEEXISTS = (int)(0x8011040C - 0x100000000);
            public const int COMADMIN_E_CANTCOPYFILE = (int)(0x8011040D - 0x100000000);
            public const int COMADMIN_E_NOUSER = (int)(0x8011040F - 0x100000000);
            public const int COMADMIN_E_INVALIDUSERIDS = (int)(0x80110410 - 0x100000000);
            public const int COMADMIN_E_NOREGISTRYCLSID = (int)(0x80110411 - 0x100000000);
            public const int COMADMIN_E_BADREGISTRYPROGID = (int)(0x80110412 - 0x100000000);
            public const int COMADMIN_E_AUTHENTICATIONLEVEL = (int)(0x80110413 - 0x100000000);
            public const int COMADMIN_E_USERPASSWDNOTVALID = (int)(0x80110414 - 0x100000000);
            public const int COMADMIN_E_CLSIDORIIDMISMATCH = (int)(0x80110418 - 0x100000000);
            public const int COMADMIN_E_REMOTEINTERFACE = (int)(0x80110419 - 0x100000000);
            public const int COMADMIN_E_DLLREGISTERSERVER = (int)(0x8011041A - 0x100000000);
            public const int COMADMIN_E_NOSERVERSHARE = (int)(0x8011041B - 0x100000000);
            public const int COMADMIN_E_DLLLOADFAILED = (int)(0x8011041D - 0x100000000);
            public const int COMADMIN_E_BADREGISTRYLIBID = (int)(0x8011041E - 0x100000000);
            public const int COMADMIN_E_APPDIRNOTFOUND = (int)(0x8011041F - 0x100000000);
            public const int COMADMIN_E_REGISTRARFAILED = (int)(0x80110423 - 0x100000000);
            public const int COMADMIN_E_COMPFILE_DOESNOTEXIST = (int)(0x80110424 - 0x100000000);
            public const int COMADMIN_E_COMPFILE_LOADDLLFAIL = (int)(0x80110425 - 0x100000000);
            public const int COMADMIN_E_COMPFILE_GETCLASSOBJ = (int)(0x80110426 - 0x100000000);
            public const int COMADMIN_E_COMPFILE_CLASSNOTAVAIL = (int)(0x80110427 - 0x100000000);
            public const int COMADMIN_E_COMPFILE_BADTLB = (int)(0x80110428 - 0x100000000);
            public const int COMADMIN_E_COMPFILE_NOTINSTALLABLE = (int)(0x80110429 - 0x100000000);
            public const int COMADMIN_E_NOTCHANGEABLE = (int)(0x8011042A - 0x100000000);
            public const int COMADMIN_E_NOTDELETEABLE = (int)(0x8011042B - 0x100000000);
            public const int COMADMIN_E_SESSION = (int)(0x8011042C - 0x100000000);
            public const int COMADMIN_E_COMP_MOVE_LOCKED = (int)(0x8011042D - 0x100000000);
            public const int COMADMIN_E_COMP_MOVE_BAD_DEST = (int)(0x8011042E - 0x100000000);
            public const int COMADMIN_E_REGISTERTLB = (int)(0x80110430 - 0x100000000);
            public const int COMADMIN_E_SYSTEMAPP = (int)(0x80110433 - 0x100000000);
            public const int COMADMIN_E_COMPFILE_NOREGISTRAR = (int)(0x80110434 - 0x100000000);
            public const int COMADMIN_E_COREQCOMPINSTALLED = (int)(0x80110435 - 0x100000000);
            public const int COMADMIN_E_SERVICENOTINSTALLED = (int)(0x80110436 - 0x100000000);
            public const int COMADMIN_E_PROPERTYSAVEFAILED = (int)(0x80110437 - 0x100000000);
            public const int COMADMIN_E_OBJECTEXISTS = (int)(0x80110438 - 0x100000000);
            public const int COMADMIN_E_COMPONENTEXISTS = (int)(0x80110439 - 0x100000000);
            public const int COMADMIN_E_REGFILE_CORRUPT = (int)(0x8011043B - 0x100000000);
            public const int COMADMIN_E_PROPERTY_OVERFLOW = (int)(0x8011043C - 0x100000000);
            public const int COMADMIN_E_NOTINREGISTRY = (int)(0x8011043E - 0x100000000);
            public const int COMADMIN_E_OBJECTNOTPOOLABLE = (int)(0x8011043F - 0x100000000);
            public const int COMADMIN_E_APPLID_MATCHES_CLSID = (int)(0x80110446 - 0x100000000);
            public const int COMADMIN_E_ROLE_DOES_NOT_EXIST = (int)(0x80110447 - 0x100000000);
            public const int COMADMIN_E_START_APP_NEEDS_COMPONENTS = (int)(0x80110448 - 0x100000000);
            public const int COMADMIN_E_REQUIRES_DIFFERENT_PLATFORM = (int)(0x80110449 - 0x100000000);
            public const int COMADMIN_E_CAN_NOT_EXPORT_APP_PROXY = (int)(0x8011044A - 0x100000000);
            public const int COMADMIN_E_CAN_NOT_START_APP = (int)(0x8011044B - 0x100000000);
            public const int COMADMIN_E_CAN_NOT_EXPORT_SYS_APP = (int)(0x8011044C - 0x100000000);
            public const int COMADMIN_E_CANT_SUBSCRIBE_TO_COMPONENT = (int)(0x8011044D - 0x100000000);
            public const int COMADMIN_E_EVENTCLASS_CANT_BE_SUBSCRIBER = (int)(0x8011044E - 0x100000000);
            public const int COMADMIN_E_LIB_APP_PROXY_INCOMPATIBLE = (int)(0x8011044F - 0x100000000);
            public const int COMADMIN_E_BASE_PARTITION_ONLY = (int)(0x80110450 - 0x100000000);
            public const int COMADMIN_E_START_APP_DISABLED = (int)(0x80110451 - 0x100000000);
            public const int COMADMIN_E_CAT_DUPLICATE_PARTITION_NAME = (int)(0x80110457 - 0x100000000);
            public const int COMADMIN_E_CAT_INVALID_PARTITION_NAME = (int)(0x80110458 - 0x100000000);
            public const int COMADMIN_E_CAT_PARTITION_IN_USE = (int)(0x80110459 - 0x100000000);
            public const int COMADMIN_E_FILE_PARTITION_DUPLICATE_FILES = (int)(0x8011045A - 0x100000000);
            public const int COMADMIN_E_CAT_IMPORTED_COMPONENTS_NOT_ALLOWED = (int)(0x8011045B - 0x100000000);
            public const int COMADMIN_E_AMBIGUOUS_APPLICATION_NAME = (int)(0x8011045C - 0x100000000);
            public const int COMADMIN_E_AMBIGUOUS_PARTITION_NAME = (int)(0x8011045D - 0x100000000);
            public const int COMADMIN_E_REGDB_NOTINITIALIZED = (int)(0x80110472 - 0x100000000);
            public const int COMADMIN_E_REGDB_NOTOPEN = (int)(0x80110473 - 0x100000000);
            public const int COMADMIN_E_REGDB_SYSTEMERR = (int)(0x80110474 - 0x100000000);
            public const int COMADMIN_E_REGDB_ALREADYRUNNING = (int)(0x80110475 - 0x100000000);
            public const int COMADMIN_E_MIG_VERSIONNOTSUPPORTED = (int)(0x80110480 - 0x100000000);
            public const int COMADMIN_E_MIG_SCHEMANOTFOUND = (int)(0x80110481 - 0x100000000);
            public const int COMADMIN_E_CAT_BITNESSMISMATCH = (int)(0x80110482 - 0x100000000);
            public const int COMADMIN_E_CAT_UNACCEPTABLEBITNESS = (int)(0x80110483 - 0x100000000);
            public const int COMADMIN_E_CAT_WRONGAPPBITNESS = (int)(0x80110484 - 0x100000000);
            public const int COMADMIN_E_CAT_PAUSE_RESUME_NOT_SUPPORTED = (int)(0x80110485 - 0x100000000);
            public const int COMADMIN_E_CAT_SERVERFAULT = (int)(0x80110486 - 0x100000000);
            public const int COMQC_E_APPLICATION_NOT_QUEUED = (int)(0x80110600 - 0x100000000);
            public const int COMQC_E_NO_QUEUEABLE_INTERFACES = (int)(0x80110601 - 0x100000000);
            public const int COMQC_E_QUEUING_SERVICE_NOT_AVAILABLE = (int)(0x80110602 - 0x100000000);
            public const int COMQC_E_NO_IPERSISTSTREAM = (int)(0x80110603 - 0x100000000);
            public const int COMQC_E_BAD_MESSAGE = (int)(0x80110604 - 0x100000000);
            public const int COMQC_E_UNAUTHENTICATED = (int)(0x80110605 - 0x100000000);
            public const int COMQC_E_UNTRUSTED_ENQUEUER = (int)(0x80110606 - 0x100000000);
            public const int MSDTC_E_DUPLICATE_RESOURCE = (int)(0x80110701 - 0x100000000);
            public const int COMADMIN_E_OBJECT_PARENT_MISSING = (int)(0x80110808 - 0x100000000);
            public const int COMADMIN_E_OBJECT_DOES_NOT_EXIST = (int)(0x80110809 - 0x100000000);
            public const int COMADMIN_E_APP_NOT_RUNNING = (int)(0x8011080A - 0x100000000);
            public const int COMADMIN_E_INVALID_PARTITION = (int)(0x8011080B - 0x100000000);
            public const int COMADMIN_E_SVCAPP_NOT_POOLABLE_OR_RECYCLABLE = (int)(0x8011080D - 0x100000000);
            public const int COMADMIN_E_USER_IN_SET = (int)(0x8011080E - 0x100000000);
            public const int COMADMIN_E_CANTRECYCLELIBRARYAPPS = (int)(0x8011080F - 0x100000000);
            public const int COMADMIN_E_CANTRECYCLESERVICEAPPS = (int)(0x80110811 - 0x100000000);
            public const int COMADMIN_E_PROCESSALREADYRECYCLED = (int)(0x80110812 - 0x100000000);
            public const int COMADMIN_E_PAUSEDPROCESSMAYNOTBERECYCLED = (int)(0x80110813 - 0x100000000);
            public const int COMADMIN_E_CANTMAKEINPROCSERVICE = (int)(0x80110814 - 0x100000000);
            public const int COMADMIN_E_PROGIDINUSEBYCLSID = (int)(0x80110815 - 0x100000000);
            public const int COMADMIN_E_DEFAULT_PARTITION_NOT_IN_SET = (int)(0x80110816 - 0x100000000);
            public const int COMADMIN_E_RECYCLEDPROCESSMAYNOTBEPAUSED = (int)(0x80110817 - 0x100000000);
            public const int COMADMIN_E_PARTITION_ACCESSDENIED = (int)(0x80110818 - 0x100000000);
            public const int COMADMIN_E_PARTITION_MSI_ONLY = (int)(0x80110819 - 0x100000000);
            public const int COMADMIN_E_LEGACYCOMPS_NOT_ALLOWED_IN_1_0_FORMAT = (int)(0x8011081A - 0x100000000);
            public const int COMADMIN_E_LEGACYCOMPS_NOT_ALLOWED_IN_NONBASE_PARTITIONS = (int)(0x8011081B - 0x100000000);
            public const int COMADMIN_E_COMP_MOVE_SOURCE = (int)(0x8011081C - 0x100000000);
            public const int COMADMIN_E_COMP_MOVE_DEST = (int)(0x8011081D - 0x100000000);
            public const int COMADMIN_E_COMP_MOVE_PRIVATE = (int)(0x8011081E - 0x100000000);
            public const int COMADMIN_E_BASEPARTITION_REQUIRED_IN_SET = (int)(0x8011081F - 0x100000000);
            public const int COMADMIN_E_CANNOT_ALIAS_EVENTCLASS = (int)(0x80110820 - 0x100000000);
            public const int COMADMIN_E_PRIVATE_ACCESSDENIED = (int)(0x80110821 - 0x100000000);
            public const int COMADMIN_E_SAFERINVALID = (int)(0x80110822 - 0x100000000);
            public const int COMADMIN_E_REGISTRY_ACCESSDENIED = (int)(0x80110823 - 0x100000000);
            public const int COMADMIN_E_PARTITIONS_DISABLED = (int)(0x80110824 - 0x100000000);
            public const int NS_E_FILE_OPEN_FAILED = (int)(0xC00D001DL - 0x100000000);

            public static bool Succeeded(int result) => result >= 0;

            public static bool Failed(int result) => result < 0;
        }
    }

    /// <summary>
    /// The enums for imported functions.
    /// </summary>
    public class Enums
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct ACCENT_POLICY
        {
            public ACCENT_STATE AccentState;
            public uint AccentFlags;
            public uint GradientColor;
            public uint AnimationId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINCOMPATTRDATA
        {
            public WINDOW_COMPATTR Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        [Flags]
        public enum WINDOW_COMPATTR : int
        {
            WCA_UNDEFINED = 0,
            WCA_NCRENDERING_ENABLED = 1,
            WCA_NCRENDERING_POLICY = 2,
            WCA_TRANSITIONS_FORCEDISABLED = 3,
            WCA_ALLOW_NCPAINT = 4,
            WCA_CAPTION_BUTTON_BOUNDS = 5,
            WCA_NONCLIENT_RTL_LAYOUT = 6,
            WCA_FORCE_ICONIC_REPRESENTATION = 7,
            WCA_EXTENDED_FRAME_BOUNDS = 8,
            WCA_HAS_ICONIC_BITMAP = 9,
            WCA_THEME_ATTRIBUTES = 10,
            WCA_N_CRENDERING_EXILED = 11,
            WCA_N_CADORNMENT_INFO = 12,
            WCA_EXCLUDED_FROM_LIVEPREVIEW = 13,
            WCA_VIDEO_OVERLAY_ACTIVE = 14,
            WCA_FORCE_ACTIVE_WINDOW_APPEARANCE = 15,
            WCA_DISALLOW_PEEK = 16,
            WCA_CLOAK = 17,
            WCA_CLOAKED = 18,
            WCA_ACCENT_POLICY = 19,
            WCA_FREEZE_REPRESENTATION = 20,
            WCA_EVER_UNCLOAKED = 21,
            WCA_VISUAL_OWNER = 22,
            WCA_HOLOGRAPHIC = 23,
            WCA_EXCLUDED_FROM_DDA = 24,
            WCA_PASSIVE_UPDATE_MODE = 25,
            WCA_USE_DARK_MODE_COLORS = 26,
            WCA_CORNER_STYLE = 27,
            WCA_PART_COLOR = 28,
            WCA_DISABLE_MOVESIZE_FEEDBACK = 29,
            WCA_LAST = 30
        }

        public enum DWM_GET_WINDOW_ATTRIBUTE : uint
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE_NOT = 19,
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,
            DWMWA_SYSTEMBACKDROP_TYPE = 38,
            DWMWA_MICA_EFFECT = 1029,
        }

        [Flags]
        public enum ACCENT_STATE
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            ACCENT_INVALID_STATE = 5
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
        public enum SW_SH : int
        {
            SW_HIDE = 0,
            SW_SHOW = 5
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

        [Flags]
        public enum WINDOW_STYLES : uint
        {
            MAXIMIZEBOX = 0x10000,
            MINIMIZEBOX = 0x20000,
            SIZEBOX = 0x40000,
            SYS_MENU = 0x80000
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHSTOCKICONINFO
        {
            public uint cbSize;
            public IntPtr hIcon;
            private int iSysImageIndex;
            private int iIcon;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            private string szPath;
        }

        [Flags]
        public enum SHSTOCKICONFLAGS : uint
        {
            SHGSI_ICONLOCATION = 0,
            SHGSI_ICON = 0x000000100,
            SHGSI_SYSICONINDEX = 0x000004000,
            SHGSI_LINKOVERLAY = 0x000008000,
            SHGSI_SELECTED = 0x000010000,
            SHGSI_LARGEICON = 0x000000000,
            SHGSI_SMALLICON = 0x000000001,
            SHGSI_SHELLICONSIZE = 0x000000004
        }

        [Flags]
        public enum SHSTOCKICONID : uint
        {
            DOCUMENT_NOT_ASSOCIATED = 0,
            DOCUMENT_ASSOCIATED = 1,
            APPLICATION = 2,
            FOLDER = 3,
            FOLDER_OPEN = 4,
            DRIVE_525 = 5,
            DRIVE_35 = 6,
            DRIVE_REMOVE = 7,
            DRIVE_FIXED = 8,
            DRIVE_NETWORK = 9,
            DRIVE_NETWORK_DISABLED = 10,
            DRIVE_CD = 11,
            DRIVE_RAM = 12,
            WORLD = 13,
            SERVER = 15,
            PRINTER = 16,
            MY_NETWORK = 17,
            FIND = 22,
            HELP = 23,
            SHARE = 28,
            LINK = 29,
            SLOW_FILE = 30,
            RECYCLER = 31,
            RECYCLER_FULL = 32,
            MEDIA_CDAUDIO = 40,
            LOCK = 47,
            AUTOLIST = 49,
            PRINTER_NET = 50,
            SERVER_SHARE = 51,
            PRINTER_FAX = 52,
            PRINTER_FAX_NET = 53,
            PRINTER_FILE = 54,
            STACK = 55,
            MEDIA_SV_CD = 56,
            STUFFED_FOLDER = 57,
            DRIVE_UNKNOWN = 58,
            DRIVE_DVD = 59,
            MEDIA_DVD = 60,
            MEDIA_DVD_RAM = 61,
            MEDIA_DVDRW = 62,
            MEDIA_DVDR = 63,
            MEDIA_DVDROM = 64,
            MEDIA_CDAUDIO_PLUS = 65,
            MEDIA_CDRW = 66,
            MEDIA_CDR = 67,
            MEDIA_CD_BURN = 68,
            MEDIA_BLANK_CD = 69,
            MEDIA_CDROM = 70,
            AUDIO_FILES = 71,
            IMAGE_FILES = 72,
            VIDEO_FILES = 73,
            MIXED_FILES = 74,
            FOLDER_BACK = 75,
            FOLDER_FRONT = 76,
            SHIELD = 77,
            WARNING = 78,
            INFO = 79,
            ERROR = 80,
            KEY = 81,
            SOFTWARE = 82,
            RENAME = 83,
            DELETE = 84,
            MEDIA_AUDIO_DVD = 85,
            MEDIA_MOVIE_DVD = 86,
            MEDIA_ENHANCED_CD = 87,
            MEDIA_ENHANCED_DVD = 88,
            MEDIA_HD_DVD = 89,
            MEDIA_BLURAY = 90,
            MEDIA_VCD = 91,
            MEDIA_DVDPLUSR = 92,
            MEDIA_DVDPLUSRW = 93,
            DESKTOP_PC = 94,
            MOBILE_PC = 95,
            USERS = 96,
            MEDIA_SMART_MEDIA = 97,
            MEDIA_COMPACT_FLASH = 98,
            DEVICE_CELLPHONE = 99,
            DEVICE_CAMERA = 100,
            DEVICE_VIDEOCAMERA = 101,
            DEVICE_AUDIOPLAYER = 102,
            NETWORK_CONNECT = 103,
            INTERNET = 104,
            ZIP_FILE = 105,
            SETTINGS = 106
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
        public struct KeyboardHookStruct
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

        public enum MEDIAINFOSTREAMKIND
        {
            GENERAL,
            VIDEO,
            AUDIO,
            TEXT,
            OTHER,
            IMAGE,
            MENU,
            MAX,
        }

        public enum MEDIAINFOKIND
        {
            NAME,
            TEXT,
            MEASURE,
            OPTIONS,
            NAMETEXT,
            MEASURETEXT,
            INFO,
            HOWTO
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

        [StructLayout(LayoutKind.Sequential)]
        public struct NET_RESOURCE
        {
            public uint dwScope;
            public uint dwType;
            public uint dwDisplayType;
            public uint dwUsage;
            public string lpLocalName;
            public string lpRemoteName;
            public string lpComment;
            public string lpProvider;
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
            MOUSE_WHEEL = 0x020A,
            X_BUTTON_DOWN = 0x020B,
            X_BUTTON_UP = 0x020C,
            X_BUTTON_DBL_CLK = 0x020D,
            MOUSE_H_WHEEL = 0x020E,
            MOUSE_LAST = 0x020E,
            PARENT_NOTIFY = 0x0210,
            ENTER_MENU_LOOP = 0x0211,
            EXIT_MENU_LOOP = 0x0212,
            NEXT_MENU = 0x0213,
            SIZING = 0x0214,
            CAPTURE_CHANGED = 0x0215,
            MOVING = 0x0216,
            POWER_BROADCAST = 0x0218,
            DEVICE_CHANGE = 0x0219,
            MDI_CREATE = 0x0220,
            MDI_DESTROY = 0x0221,
            MDI_ACTIVATE = 0x0222,
            MDI_RESTORE = 0x0223,
            MDI_NEXT = 0x0224,
            MDI_MAXIMIZE = 0x0225,
            MDI_TILE = 0x0226,
            MDI_CASCADE = 0x0227,
            MDI_ICON_ARRANGE = 0x0228,
            MDI_GET_ACTIVE = 0x0229,
            MDI_SET_MENU = 0x0230,
            ENTER_SIZE_MOVE = 0x0231,
            EXIT_SIZE_MOVE = 0x0232,
            DROP_FILES = 0x0233,
            MDI_REFRESH_MENU = 0x0234,
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
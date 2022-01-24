using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VitNX.Functions.Windows.Win32;

namespace VitNX.Functions.Windows.NativeControls
{
    public static class TaskBarProgressBar
    {
        [ComImport()]
        [Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface ITaskbarList3
        {
            [PreserveSig]
            void HrInit();

            [PreserveSig]
            void AddTab(IntPtr hwnd);

            [PreserveSig]
            void DeleteTab(IntPtr hwnd);

            [PreserveSig]
            void ActivateTab(IntPtr hwnd);

            [PreserveSig]
            void SetActiveAlt(IntPtr hwnd);

            [PreserveSig]
            void MarkFullscreenWindow(IntPtr hwnd, [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

            [PreserveSig]
            void SetProgressValue(IntPtr hwnd, UInt64 ullCompleted, UInt64 ullTotal);

            [PreserveSig]
            void SetProgressState(IntPtr hwnd, Enums.TASKBAR_STATES state);
        }

        [ComImport()]
        [Guid("56fdf344-fd6d-11d0-958a-006097c9a090")]
        [ClassInterface(ClassInterfaceType.None)]
        private class TaskbarInstance
        { }

        private static ITaskbarList3 taskbarInstance = (ITaskbarList3)new TaskbarInstance();

        private static bool taskbarSupported = Environment.OSVersion.Version >= new Version(6, 1);

        public static void SetState(IntPtr windowHandle,
            Enums.TASKBAR_STATES taskbarState)
        {
            if (taskbarSupported)
                taskbarInstance.SetProgressState(windowHandle, taskbarState);
        }

        public static void SetValue(IntPtr windowHandle,
            double Value,
            double Max)
        {
            if (taskbarSupported)
                taskbarInstance.SetProgressValue(windowHandle, (ulong)Value, (ulong)Max);
        }
    }

    public class NewFolderDialog
    {
        private string _initialDirectory;
        private string _title;
        private string _fileName = "";

        public string InitialDirectory
        {
            get
            {
                return string.IsNullOrEmpty(_initialDirectory) ? Environment.CurrentDirectory : _initialDirectory;
            }
            set
            {
                _initialDirectory = value;
            }
        }

        public string Title
        {
            get
            {
                return _title ?? "Select a folder";
            }
            set
            {
                _title = value;
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        public bool Show()
        { return Show(IntPtr.Zero); }

        public bool Show(IntPtr hWndOwner)
        {
            var result = Environment.OSVersion.Version.Major >= 6 ? ModernDialog.Show(hWndOwner, InitialDirectory, Title) : ShowXpDialog(hWndOwner, InitialDirectory, Title);
            _fileName = result.FileName;
            return result.Result;
        }

        private struct ShowDialogResult
        {
            public bool Result { get; set; }
            public string FileName { get; set; }
        }

        private static ShowDialogResult ShowXpDialog(IntPtr ownerHandle,
            string initialDirectory,
            string title)
        {
            var folderBrowserDialog = new FolderBrowserDialog
            { Description = title, SelectedPath = initialDirectory, ShowNewFolderButton = false };
            var dialogResult = new ShowDialogResult();
            if (folderBrowserDialog.ShowDialog(new WindowWrapper(ownerHandle)) == DialogResult.OK)
            {
                dialogResult.Result = true;
                dialogResult.FileName = folderBrowserDialog.SelectedPath;
            }
            return dialogResult;
        }

        private static class ModernDialog
        {
            private const string c_foldersFilter = "Folders|\n";
            private const BindingFlags c_flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            private static readonly Assembly s_windowsFormsAssembly = typeof(FileDialog).Assembly;
            private static readonly Type s_iFileDialogType = s_windowsFormsAssembly.GetType("System.Windows.Forms.FileDialogNative+IFileDialog");
            private static readonly MethodInfo s_createVistaDialogMethodInfo = typeof(OpenFileDialog).GetMethod("CreateVistaDialog", c_flags);
            private static readonly MethodInfo s_onBeforeVistaDialogMethodInfo = typeof(OpenFileDialog).GetMethod("OnBeforeVistaDialog", c_flags);
            private static readonly MethodInfo s_getOptionsMethodInfo = typeof(FileDialog).GetMethod("GetOptions", c_flags);
            private static readonly MethodInfo s_setOptionsMethodInfo = s_iFileDialogType.GetMethod("SetOptions", c_flags);
            private static readonly uint s_fosPickFoldersBitFlag = (uint)s_windowsFormsAssembly.GetType("System.Windows.Forms.FileDialogNative+FOS").GetField("FOS_PICKFOLDERS").GetValue(null);
            private static readonly ConstructorInfo s_VistaDialogEventsConstructorInfo = s_windowsFormsAssembly.GetType("System.Windows.Forms.FileDialog+VistaDialogEvents").GetConstructor(c_flags, null, new[] { typeof(FileDialog) }, null);
            private static readonly MethodInfo s_adviseMethodInfo = s_iFileDialogType.GetMethod("Advise");
            private static readonly MethodInfo s_unAdviseMethodInfo = s_iFileDialogType.GetMethod("Unadvise");
            private static readonly MethodInfo s_showMethodInfo = s_iFileDialogType.GetMethod("Show");

            public static ShowDialogResult Show(IntPtr ownerHandle, string initialDirectory, string title)
            {
                var openFileDialog = new OpenFileDialog
                {
                    AddExtension = false,
                    CheckFileExists = false,
                    DereferenceLinks = true,
                    Filter = c_foldersFilter,
                    InitialDirectory = initialDirectory,
                    Multiselect = false,
                    Title = title
                };
                var iFileDialog = s_createVistaDialogMethodInfo.Invoke(openFileDialog, new object[] { });
                s_onBeforeVistaDialogMethodInfo.Invoke(openFileDialog, new[] { iFileDialog });
                s_setOptionsMethodInfo.Invoke(iFileDialog, new object[] { (uint)s_getOptionsMethodInfo.Invoke(openFileDialog, new object[] { }) | s_fosPickFoldersBitFlag });
                var adviseParametersWithOutputConnectionToken = new[] { s_VistaDialogEventsConstructorInfo.Invoke(new object[] { openFileDialog }), 0U };
                s_adviseMethodInfo.Invoke(iFileDialog, adviseParametersWithOutputConnectionToken);
                try
                {
                    int retVal = (int)s_showMethodInfo.Invoke(iFileDialog, new object[] { ownerHandle });
                    return new ShowDialogResult { Result = retVal == 0, FileName = openFileDialog.FileName };
                }
                finally { s_unAdviseMethodInfo.Invoke(iFileDialog, new[] { adviseParametersWithOutputConnectionToken[1] }); }
            }
        }

        private class WindowWrapper : IWin32Window
        {
            private readonly IntPtr _handle;

            public WindowWrapper(IntPtr handle)
            { _handle = handle; }

            public IntPtr Handle
            {
                get
                {
                    return _handle;
                }
            }
        }
    }

    public class VolumeControl
    {
        [ComImport]
        [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
        internal class MMDeviceEnumerator
        { }

        [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IMMDeviceEnumerator
        {
            int NotImpl1();

            [PreserveSig]
            int GetDefaultAudioEndpoint(Enums.E_DATA_FLOW dataFlow,
                Enums.E_ROLE role,
                out IMMDevice ppDevice);
        }

        [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IMMDevice
        {
            [PreserveSig]
            int Activate(ref Guid iid,
                int dwClsCtx,
                IntPtr pActivationParams,
                [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
        }

        [Guid("657804FA-D6AD-4496-8A60-352752AF4F89"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IAudioEndpointVolumeCallback
        {
            int OnNotify(IntPtr pNotifyData);
        };

        [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IAudioEndpointVolume
        {
            int RegisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);

            int UnregisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);

            int GetChannelCount(out int pnChannelCount);

            int SetMasterVolumeLevel(float fLevelDB, Guid pguidEventContext);

            int SetMasterVolumeLevelScalar(float fLevel, Guid pguidEventContext);

            int GetMasterVolumeLevel(out float pfLevelDB);

            int GetMasterVolumeLevelScalar(out float pfLevel);

            int SetChannelVolumeLevel(uint nChannel, float fLevelDB, Guid pguidEventContext);

            int SetChannelVolumeLevelScalar(uint nChannel, float fLevel, Guid pguidEventContext);

            int GetChannelVolumeLevel(uint nChannel, out float pfLevelDB);

            int GetChannelVolumeLevelScalar(uint nChannel, out float pfLevel);

            int SetMute([MarshalAs(UnmanagedType.Bool)] bool bMute, Guid pguidEventContext);

            int GetMute(out bool pbMute);

            int GetVolumeStepInfo(out uint pnStep, out uint pnStepCount);

            int VolumeStepUp(Guid pguidEventContext);

            int VolumeStepDown(Guid pguidEventContext);

            int QueryHardwareSupport(out uint pdwHardwareSupportMask);

            int GetVolumeRange(out float pflVolumeMindB, out float pflVolumeMaxdB, out float pflVolumeIncrementdB);
        }

        public void Set(float level)
        {
            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            IMMDevice speakers = null;
            IAudioEndpointVolume vol = null;
            deviceEnumerator.GetDefaultAudioEndpoint(Enums.E_DATA_FLOW.eRender,
                Enums.E_ROLE.eMultimedia,
                out speakers);
            Guid IID_IAudioEndpointVolume = typeof(IAudioEndpointVolume).GUID;
            speakers.Activate(ref IID_IAudioEndpointVolume,
                0,
                IntPtr.Zero,
                out object o);
            vol = (IAudioEndpointVolume)o;
            vol.SetMasterVolumeLevelScalar(level, Guid.Empty);
            if (vol != null) Marshal.ReleaseComObject(vol);
            if (speakers != null) Marshal.ReleaseComObject(speakers);
            if (deviceEnumerator != null) Marshal.ReleaseComObject(deviceEnumerator);
        }

        public float Get()
        {
            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            IMMDevice speakers = null;
            IAudioEndpointVolume vol = null;
            deviceEnumerator.GetDefaultAudioEndpoint(Enums.E_DATA_FLOW.eRender,
                Enums.E_ROLE.eMultimedia,
                out speakers);
            Guid IID_IAudioEndpointVolume = typeof(IAudioEndpointVolume).GUID;
            speakers.Activate(ref IID_IAudioEndpointVolume, 0, IntPtr.Zero, out object o);
            vol = (IAudioEndpointVolume)o;
            float currentVolume = 0;
            vol.GetMasterVolumeLevelScalar(out currentVolume);
            if (vol != null) Marshal.ReleaseComObject(vol);
            if (speakers != null) Marshal.ReleaseComObject(speakers);
            if (deviceEnumerator != null) Marshal.ReleaseComObject(deviceEnumerator);
            return currentVolume;
        }
    }

    public class GetClipboardText
    {
        private string _GetText;

        private void _thGetText(object format)
        {
            try
            {
                if (format == null) { _GetText = Clipboard.GetText(); }
                else { _GetText = Clipboard.GetText((TextDataFormat)format); }
            }
            catch { _GetText = string.Empty; }
        }

        public static string GetText()
        {
            GetClipboardText instance = new GetClipboardText();
            Thread staThread = new Thread(instance._thGetText);
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return instance._GetText;
        }
    }

    public static class STATask
    {
        public static Task<TResult> Run<TResult>(Func<TResult> function)
        {
            var tcs = new TaskCompletionSource<TResult>();

            var thread = new Thread(() =>
            {
                try { tcs.SetResult(function()); }
                catch (Exception e) { tcs.SetException(e); }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

        public static Task Run(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception e) { tcs.SetException(e); }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }
    }

    public class Keyboard
    {
        public static void KeyDown(Keys vKey)
        {
            Import.keybd_event((byte)vKey, 0, (int)Enums.KEYEVENTF.KEYEVENTF_EXTENDEDKEY, 0);
        }

        public static void KeyUp(Keys vKey)
        {
            Import.keybd_event((byte)vKey, 0, (int)Enums.KEYEVENTF.KEYEVENTF_EXTENDEDKEY | (int)Enums.KEYEVENTF.KEYEVENTF_KEYUP, 0);
        }

        public static void WindowsKeyboardEventsAPI(int status, string keys = "none")
        {
            switch (status)
            {
                case -1:
                    {
                        KeyDown(Keys.LWin);
                        KeyDown(Keys.D);
                        KeyUp(Keys.LWin);
                        KeyUp(Keys.D);
                        break;
                    }
                case 0:
                    {
                        KeyDown(Keys.LWin);
                        KeyDown(Keys.M);
                        KeyUp(Keys.LWin);
                        KeyUp(Keys.M);
                        break;
                    }
                case 1:
                    {
                        KeyDown(Keys.LWin);
                        KeyDown(Keys.LShiftKey);
                        KeyDown(Keys.M);
                        KeyUp(Keys.LWin);
                        KeyUp(Keys.LShiftKey);
                        KeyUp(Keys.M);
                        break;
                    }
            }
        }
    }
}
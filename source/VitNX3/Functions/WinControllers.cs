using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

using VitNX3.Functions.AppsAndProcesses;
using VitNX3.Functions.Win32;

using static VitNX3.Functions.Win32.Enums;

namespace VitNX3.Functions.WinControllers
{
    /// <summary>
    /// Work with progressbar on taskbar.
    /// </summary>
    public static class TaskBarProgressBar
    {
        /// <summary>
        /// The taskbar list3.
        /// </summary>
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

            /// <summary>
            /// Marks the fullscreen window.
            /// </summary>
            /// <param name="hwnd">The hwnd.</param>
            /// <param name="fFullscreen">If true, f fullscreen.</param>
            [PreserveSig]
            void MarkFullscreenWindow(IntPtr hwnd,
                [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

            /// <summary>
            /// Sets the progress value.
            /// </summary>
            /// <param name="hwnd">The hwnd.</param>
            /// <param name="ullCompleted">The ull completed.</param>
            /// <param name="ullTotal">The ull total.</param>
            [PreserveSig]
            void SetProgressValue(IntPtr hwnd,
                ulong ullCompleted,
                ulong ullTotal);

            /// <summary>
            /// Sets the progress state.
            /// </summary>
            /// <param name="hwnd">The hwnd.</param>
            /// <param name="state">The state.</param>
            [PreserveSig]
            void SetProgressState(IntPtr hwnd,
                TASKBAR_STATES state);
        }

        /// <summary>
        /// The progressbar of taskbar instance.
        /// </summary>
        [ComImport()]
        [Guid("56fdf344-fd6d-11d0-958a-006097c9a090")]
        [ClassInterface(ClassInterfaceType.None)]
        private class TaskbarInstance
        { }

        private static ITaskbarList3 taskbarInstance = (ITaskbarList3)new TaskbarInstance();
        private static bool taskbarSupported = Environment.OSVersion.Version >= new Version(6, 1);

        /// <summary>
        /// Sets the state.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        /// <param name="taskbarState">The taskbar state.</param>
        public static void SetState(IntPtr windowHandle,
            TASKBAR_STATES taskbarState)
        {
            if (taskbarSupported)
                taskbarInstance.SetProgressState(windowHandle, taskbarState);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="windowHandle">The window handle.</param>
        /// <param name="Value">The value.</param>
        /// <param name="Max">The max.</param>
        public static void SetValue(IntPtr windowHandle,
            double Value,
            double Max)
        {
            if (taskbarSupported)
                taskbarInstance.SetProgressValue(windowHandle, (ulong)Value, (ulong)Max);
        }
    }

    /// <summary>
    /// The volume control.
    /// </summary>
    public class VolumeControl
    {
        [ComImport]
        [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
        internal class MMDeviceEnumerator
        { }

        [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"),
            InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IMMDeviceEnumerator
        {
            int NotImpl1();

            [PreserveSig]
            int GetDefaultAudioEndpoint(E_DATA_FLOW dataFlow,
                E_ROLE role,
                out IMMDevice ppDevice);
        }

        [Guid("D666063F-1587-4E43-81F1-B948E807363F"),
            InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IMMDevice
        {
            [PreserveSig]
            int Activate(ref Guid iid,
                int dwClsCtx,
                IntPtr pActivationParams,
                [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
        }

        [Guid("657804FA-D6AD-4496-8A60-352752AF4F89"),
            InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IAudioEndpointVolumeCallback
        {
            int OnNotify(IntPtr pNotifyData);
        };

        [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"),
            InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IAudioEndpointVolume
        {
            int RegisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);

            int UnregisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);

            /// <summary>
            /// Gets the volume channel(s) count.
            /// </summary>
            /// <param name="pnChannelCount">The pn channel count.</param>
            /// <returns>An int.</returns>
            int GetChannelCount(out int pnChannelCount);

            /// <summary>
            /// Sets the master volume level.
            /// </summary>
            /// <param name="fLevelDB">The f level d b.</param>
            /// <param name="pguidEventContext">The pguid event context.</param>
            /// <returns>An int.</returns>
            int SetMasterVolumeLevel(float fLevelDB,
                Guid pguidEventContext);

            /// <summary>
            /// Sets the master volume level in scalar.
            /// </summary>
            /// <param name="fLevel">The f level.</param>
            /// <param name="pguidEventContext">The pguid event context.</param>
            /// <returns>An int.</returns>
            int SetMasterVolumeLevelScalar(float fLevel,
                Guid pguidEventContext);

            /// <summary>
            /// Gets the master volume level.
            /// </summary>
            /// <param name="pfLevelDB">The pf level d b.</param>
            /// <returns>An int.</returns>
            int GetMasterVolumeLevel(out float pfLevelDB);

            /// <summary>
            /// Gets the master volume level in scalar.
            /// </summary>
            /// <param name="pfLevel">The pf level.</param>
            /// <returns>An int.</returns>
            int GetMasterVolumeLevelScalar(out float pfLevel);

            /// <summary>
            /// Sets the channel volume level.
            /// </summary>
            /// <param name="nChannel">The n channel.</param>
            /// <param name="fLevelDB">The f level d b.</param>
            /// <param name="pguidEventContext">The pguid event context.</param>
            /// <returns>An int.</returns>
            int SetChannelVolumeLevel(uint nChannel,
                float fLevelDB,
                Guid pguidEventContext);

            /// <summary>
            /// Sets the channel volume level scalar.
            /// </summary>
            /// <param name="nChannel">The n channel.</param>
            /// <param name="fLevel">The f level.</param>
            /// <param name="pguidEventContext">The pguid event context.</param>
            /// <returns>An int.</returns>
            int SetChannelVolumeLevelScalar(uint nChannel,
                float fLevel,
                Guid pguidEventContext);

            /// <summary>
            /// Gets the channel volume level.
            /// </summary>
            /// <param name="nChannel">The n channel.</param>
            /// <param name="pfLevelDB">The pf level d b.</param>
            /// <returns>An int.</returns>
            int GetChannelVolumeLevel(uint nChannel,
                out float pfLevelDB);

            int GetChannelVolumeLevelScalar(uint nChannel,
                out float pfLevel);

            /// <summary>
            /// Sets the mute.
            /// </summary>
            /// <param name="bMute">If true, b mute.</param>
            /// <param name="pguidEventContext">The pguid event context.</param>
            /// <returns>An int.</returns>
            int SetMute([MarshalAs(UnmanagedType.Bool)] bool bMute, Guid pguidEventContext);

            /// <summary>
            /// Gets the mute.
            /// </summary>
            /// <param name="pbMute">If true, pb mute.</param>
            /// <returns>An int.</returns>
            int GetMute(out bool pbMute);

            int GetVolumeStepInfo(out uint pnStep,
                out uint pnStepCount);

            int VolumeStepUp(Guid pguidEventContext);

            int VolumeStepDown(Guid pguidEventContext);

            int QueryHardwareSupport(out uint pdwHardwareSupportMask);

            int GetVolumeRange(out float pflVolumeMindB,
                out float pflVolumeMaxdB,
                out float pflVolumeIncrementdB);
        }

        /// <summary>
        /// Sets the current total sound volume.
        /// </summary>
        /// <param name="level">The level.</param>
        public void Set(float level)
        {
            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            IMMDevice speakers = null;
            IAudioEndpointVolume vol = null;
            deviceEnumerator.GetDefaultAudioEndpoint(E_DATA_FLOW.eRender,
                E_ROLE.eMultimedia,
                out speakers);
            Guid IID_IAudioEndpointVolume = typeof(IAudioEndpointVolume).GUID;
            speakers.Activate(ref IID_IAudioEndpointVolume, 0,
                IntPtr.Zero,
                out object o);
            vol = (IAudioEndpointVolume)o;
            vol.SetMasterVolumeLevelScalar(level, Guid.Empty);
            if (vol != null)
                Marshal.ReleaseComObject(vol);
            if (speakers != null)
                Marshal.ReleaseComObject(speakers);
            if (deviceEnumerator != null)
                Marshal.ReleaseComObject(deviceEnumerator);
        }

        /// <summary>
        /// Gets the current total sound volume.
        /// </summary>
        /// <returns>A float.</returns>
        public float Get()
        {
            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();
            IMMDevice speakers = null;
            IAudioEndpointVolume vol = null;
            deviceEnumerator.GetDefaultAudioEndpoint(E_DATA_FLOW.eRender,
                E_ROLE.eMultimedia,
                out speakers);
            Guid IID_IAudioEndpointVolume = typeof(IAudioEndpointVolume).GUID;
            speakers.Activate(ref IID_IAudioEndpointVolume, 0,
                IntPtr.Zero,
                out object o);
            vol = (IAudioEndpointVolume)o;
            float currentVolume = 0;
            vol.GetMasterVolumeLevelScalar(out currentVolume);
            if (vol != null)
                Marshal.ReleaseComObject(vol);
            if (speakers != null)
                Marshal.ReleaseComObject(speakers);
            if (deviceEnumerator != null)
                Marshal.ReleaseComObject(deviceEnumerator);
            return currentVolume;
        }
    }

    /// <summary>
    /// Sets text to clipboard.
    /// </summary>
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

    /// <summary>
    /// Control the keyboard.
    /// </summary>
    public class Keyboard
    {
        /// <summary>
        /// Shows the virtual keyboard.
        /// </summary>
        /// <returns>A string.</returns>
        public static bool ShowVirtualKeyboard()
        {
            try
            {
                Processes.Run("osk");
                List<string> processes = Processes.GetList();
                foreach (string process in processes)
                    return process == "osk" ? true : false;
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Hides the virtual keyboard.
        /// </summary>
        /// <returns>A bool.</returns>
        public static bool HideVirtualKeyboard()
        {
            Processes.Kill("osk.exe");
            List<string> processes = Processes.GetList();
            foreach (string process in processes)
                return process == "osk" ? false : true;
            return false;
        }
    }

#pragma warning disable CS1591

    /// <summary>
    /// Work with monitor.
    /// </summary>
    public static class Monitor
    {
        public const int ERROR_SUCCESS = 0;

        [StructLayout(LayoutKind.Sequential)]
        public struct LUID
        {
            public uint LowPart;
            public int HighPart;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_PATH_SOURCE_INFO
        {
            public LUID adapterId;
            public uint id;
            public uint modeInfoIdx;
            public uint statusFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_PATH_TARGET_INFO
        {
            public LUID adapterId;
            public uint id;
            public uint modeInfoIdx;
            private DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;
            private DISPLAYCONFIG_ROTATION rotation;
            private DISPLAYCONFIG_SCALING scaling;
            private DISPLAYCONFIG_RATIONAL refreshRate;
            private DISPLAYCONFIG_SCANLINE_ORDERING scanLineOrdering;
            public bool targetAvailable;
            public uint statusFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_RATIONAL
        {
            public uint Numerator;
            public uint Denominator;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_PATH_INFO
        {
            public DISPLAYCONFIG_PATH_SOURCE_INFO sourceInfo;
            public DISPLAYCONFIG_PATH_TARGET_INFO targetInfo;
            public uint flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_2DREGION
        {
            public uint cx;
            public uint cy;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_VIDEO_SIGNAL_INFO
        {
            public ulong pixelRate;
            public DISPLAYCONFIG_RATIONAL hSyncFreq;
            public DISPLAYCONFIG_RATIONAL vSyncFreq;
            public DISPLAYCONFIG_2DREGION activeSize;
            public DISPLAYCONFIG_2DREGION totalSize;
            public uint videoStandard;
            public DISPLAYCONFIG_SCANLINE_ORDERING scanLineOrdering;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_TARGET_MODE
        {
            public DISPLAYCONFIG_VIDEO_SIGNAL_INFO targetVideoSignalInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTL
        {
            private int x;
            private int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_SOURCE_MODE
        {
            public uint width;
            public uint height;
            public DISPLAYCONFIG_PIXELFORMAT pixelFormat;
            public POINTL position;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DISPLAYCONFIG_MODE_INFO_UNION
        {
            [FieldOffset(0)]
            public DISPLAYCONFIG_TARGET_MODE targetMode;

            [FieldOffset(0)]
            public DISPLAYCONFIG_SOURCE_MODE sourceMode;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_MODE_INFO
        {
            public DISPLAYCONFIG_MODE_INFO_TYPE infoType;
            public uint id;
            public LUID adapterId;
            public DISPLAYCONFIG_MODE_INFO_UNION modeInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS
        {
            public uint value;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_DEVICE_INFO_HEADER
        {
            public DISPLAYCONFIG_DEVICE_INFO_TYPE type;
            public uint size;
            public LUID adapterId;
            public uint id;
        }

        [StructLayout(LayoutKind.Sequential,
            CharSet = CharSet.Unicode)]
        public struct DISPLAYCONFIG_TARGET_DEVICE_NAME
        {
            public DISPLAYCONFIG_DEVICE_INFO_HEADER header;
            public DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAGS flags;
            public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY outputTechnology;
            public ushort edidManufactureId;
            public ushort edidProductCodeId;
            public uint connectorInstance;

            [MarshalAs(UnmanagedType.ByValTStr,
                SizeConst = 64)]
            public string monitorFriendlyDeviceName;

            [MarshalAs(UnmanagedType.ByValTStr,
                SizeConst = 128)]
            public string monitorDevicePath;
        }

#pragma warning restore CS1591
        /// <summary>
        /// Monitors the friendly name.
        /// </summary>
        /// <param name="adapterId">The adapter id.</param>
        /// <param name="targetId">The target id.</param>
        /// <returns>A string.</returns>
        private static string MonitorFriendlyName(LUID adapterId,
            uint targetId)
        {
            var deviceName = new DISPLAYCONFIG_TARGET_DEVICE_NAME
            {
                header =
                {
                    size = (uint)Marshal.SizeOf(typeof (DISPLAYCONFIG_TARGET_DEVICE_NAME)),
                    adapterId = adapterId,
                    id = targetId,
                    type = DISPLAYCONFIG_DEVICE_INFO_TYPE.
                    DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME
                }
            };
            var error = Import.DisplayConfigGetDeviceInfo(ref deviceName);
            if (error != ERROR_SUCCESS)
                throw new Win32Exception(error);
            return deviceName.monitorFriendlyDeviceName;
        }

        /// <summary>
        /// Gets the all monitors friendly names.
        /// </summary>
        /// <returns>A list of string.</returns>
        private static IEnumerable<string> GetAllMonitorsFriendlyNames()
        {
            uint pathCount, modeCount;
            var error = Import.GetDisplayConfigBufferSizes(QUERY_DEVICE_CONFIG_FLAGS.
                QDC_ONLY_ACTIVE_PATHS,
                out pathCount,
                out modeCount);
            if (error != ERROR_SUCCESS)
                throw new Win32Exception(error);
            var displayPaths = new DISPLAYCONFIG_PATH_INFO[pathCount];
            var displayModes = new DISPLAYCONFIG_MODE_INFO[modeCount];
            error = Import.QueryDisplayConfig(QUERY_DEVICE_CONFIG_FLAGS.
                QDC_ONLY_ACTIVE_PATHS,
                ref pathCount,
                displayPaths,
                ref modeCount,
                displayModes,
                IntPtr.Zero);
            if (error != ERROR_SUCCESS)
                throw new Win32Exception(error);
            for (var i = 0; i < modeCount; i++)
                if (displayModes[i].infoType == DISPLAYCONFIG_MODE_INFO_TYPE.
                    DISPLAYCONFIG_MODE_INFO_TYPE_TARGET)
                    yield return MonitorFriendlyName(displayModes[i].adapterId,
                        displayModes[i].id);
        }
    }

    /// <summary>
    /// Work with system.
    /// </summary>
    public static class WorkWithSystem
    {
        /// <summary>
        /// Installs the INF driver file into the Windows System.
        /// </summary>
        /// <param name="pathToInf">The path to INF driver file.</param>
        public static void InstallInfDriver(string pathToInf)
        {
            Import.InstallHinfSection(IntPtr.Zero, IntPtr.Zero, pathToInf, 0);
        }
    }
}
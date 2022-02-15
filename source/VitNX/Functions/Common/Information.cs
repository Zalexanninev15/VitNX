using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Reflection;
using System.Windows.Forms;

using VitNX.Functions.Windows.Apps;
using VitNX.Functions.Windows.Win32;

namespace VitNX.Functions.Common.Information
{
    /// <summary>
    /// Work with informations of Windows System.
    /// </summary>
    public class Windows
    {
        /// <summary>
        /// Gets the name of PC.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetComputerName() => Environment.MachineName;

        /// <summary>
        /// Gets the current user (name).
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetCurrentUsername() => Environment.UserName;

        /// <summary>
        /// Gets locals the time.
        /// </summary>
        /// <returns>A DateTime.</returns>
        public static DateTime GetLocalTime() => new Microsoft.VisualBasic.Devices.Clock().LocalTime;

        /// <summary>
        /// Windows is x64 (64-bit).
        /// </summary>
        /// <returns>A bool.</returns>
        public static bool Is64bit() => Environment.Is64BitOperatingSystem;

        /// <summary>
        /// Gets the Windows version.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetWindowsVersion() => Convert.ToString(Environment.OSVersion.Version);

        /// <summary>
        /// Gets the Windows version from the Windows Registry.
        /// </summary>
        /// <returns>A double.</returns>
        public static double GetWindowsVersionFromREG() => double.Parse((string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion",
            "CurrentVersion", ""),
            System.Globalization.CultureInfo.InvariantCulture);

        /// <summary>
        /// Gets the Windows edition from the Windows Registry.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetWindowsEditionIDFromREG() => (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion",
            "EditionID", "");

        /// <summary>
        /// Gets the Windows current build number from the Windows Registry.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetWindowsCurrentBuildNumberFromREG() => (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion",
            "CurrentBuildNumber", "");

        /// <summary>
        /// Gets the Windows product name from the Windows Registry.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetWindowsProductNameFromREG() => (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion",
            "ProductName", "");

        /// <summary>
        /// Gets the Windows displayed version from the Windows Registry.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetWindowsDisplayVersionFromREG() => (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion",
            "DisplayVersion", "");

        /// <summary>
        /// Gets the windows release id from the Windows Registry.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetWindowsReleaseIdFromREG() => (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion",
            "ReleaseId", "");

        /// <summary>
        /// Gets the Windows startup folder path.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetWindowsStartupFolderPath() => Environment.GetFolderPath(Environment.SpecialFolder.Startup);

        /// <summary>
        /// Windows use light theme in system from the Windows Registry, for Windows 10+.
        /// </summary>
        /// <returns>A bool.</returns>
        public static bool WindowsUseLightThemeInSystem() => (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
            "SystemUsesLightTheme", "1") == 1 ? true : false;

        /// <summary>
        /// Windows use light theme for apps from the Windows Registry, for Windows 10+.
        /// </summary>
        /// <returns>A bool.</returns>
        public static bool WindowsUseLightThemeForApps() => (int)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
            "AppsUseLightTheme", "1") == 1 ? true : false;

        /// <summary>
        /// Gets Windows the serial key.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetWindowsSerialKey()
        {
            ManagementObjectSearcher searcher112 = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectCollection information112 = searcher112.Get();
            foreach (ManagementObject obj112 in information112)
                return Convert.ToString(obj112["SerialNumber"]);
            return "S-E-R-I-A-L_K-E-Y";
        }

        /// <summary>
        /// Gets the Windows accent color.
        /// </summary>
        /// <returns>A Color.</returns>
        public static Color GetWindowsAccentColor()
        {
            var userColorSet = Import.GetImmersiveUserColorSetPreference(false, false);
            var colorType = Import.GetImmersiveColorTypeFromName(System.Runtime.InteropServices.Marshal.StringToHGlobalUni("ImmersiveStartSelectionBackground"));
            var colorSetEx = Import.GetImmersiveColorFromColorSetEx((uint)userColorSet,
                colorType,
                false, 0);
            return CShap.ConvertDWordColorToRGB(colorSetEx);
        }
    }

    /// <summary>
    /// Work with informations of CPU.
    /// </summary>
    public class Cpu
    {
        /// <summary>
        /// Gets all characteristics.
        /// </summary>
        /// <returns>An array of string.</returns>
        public static string[] Characteristics() => Set().Split('/');

        /// <summary>
        /// Sets (gets) values for CPU's characteristics.
        /// </summary>
        private static string Set()
        {
            string toCPU = "";
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                toCPU += obj["Name"].ToString() + "/";
                toCPU += obj["DeviceID"].ToString() + "/";
                toCPU += obj["Manufacturer"].ToString() + "/";
                toCPU += obj["CurrentClockSpeed"].ToString() + "/";
                toCPU += obj["MaxClockSpeed"].ToString() + "/";
                toCPU += obj["Caption"].ToString() + "/";
                toCPU += obj["NumberOfCores"].ToString() + "/";
                toCPU += obj["NumberOfEnabledCore"].ToString() + "/";
                toCPU += obj["NumberOfLogicalProcessors"].ToString() + "/";
                toCPU += obj["Architecture"].ToString() + "/";
                toCPU += obj["Family"].ToString() + "/";
                toCPU += obj["ProcessorType"].ToString() + "/";
                toCPU += obj["Characteristics"].ToString() + "/";
                toCPU += obj["AddressWidth"].ToString() + "/";
                toCPU += obj["SerialNumber"].ToString() + "/";
                toCPU += obj["ThreadCount"].ToString() + "/";
                toCPU += obj["LoadPercentage"].ToString() + "/";
                toCPU += obj["CurrentVoltage"].ToString() + "/";
            }
            toCPU += Convert.ToString(Convert.ToInt32(
                new PerformanceCounter("Processor", "% Processor Time", "_Total").NextValue())) + "/";
            return toCPU;
        }

        /// <summary>
        /// Gets the architecture.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetArchitecture()
        {
            string result = string.Empty;
            switch (typeof(string).Assembly.GetName().ProcessorArchitecture)
            {
                case ProcessorArchitecture.X86:
                    result = "x86";
                    break;

                case ProcessorArchitecture.Amd64:
                    result = "x64";
                    break;

                case ProcessorArchitecture.Arm:
                    result = "ARM";
                    break;
            }
            return result;
        }

        /// <summary>
        /// Gets the clock speed.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetClockSpeed()
        {
            string clockSpeed = "";
            foreach (var item in new ManagementObjectSearcher("select MaxClockSpeed from Win32_Processor").Get())
            {
                var clockSpeedx = (uint)item["MaxClockSpeed"];
                clockSpeed = clockSpeedx.ToString();
            }
            return clockSpeed;
        }
    }

    /// <summary>
    /// Work with informations of GPU(s).
    /// </summary>
    public class Gpu
    {
        /// <summary>
        /// Gets all characteristics.
        /// </summary>
        /// <returns>An array of string.</returns>
        public static string[] Characteristics() => Set().Split('/');

        /// <summary>
        /// Sets (gets) values for GPU's characteristics.
        /// </summary>
        private static string Set()
        {
            string toGPU = "";
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                toGPU += obj["Name"].ToString() + "/";
                toGPU += obj["Status"].ToString() + "/";
                toGPU += obj["DeviceID"].ToString() + "/";
                toGPU += Data.Text.SizeSuffix((long)Convert.ToDouble(obj["AdapterRAM"])) + "/";
                toGPU += obj["AdapterDACType"].ToString() + "/";
                toGPU += obj["Monochrome"].ToString() + "/";
                toGPU += obj["InstalledDisplayDrivers"].ToString() + "/";
                toGPU += obj["DriverVersion"].ToString() + "/";
                toGPU += obj["VideoArchitecture"].ToString() + "/";
                toGPU += obj["VideoMemoryType"].ToString() + "/";
                toGPU += obj["MaxRefreshRate"].ToString() + "/";
                toGPU += obj["MinRefreshRate"].ToString() + "/";
                toGPU += obj["VideoModeDescription"].ToString() + "/";
            }
            return toGPU;
        }
    }

    /// <summary>
    /// Work with informations of Disk(s).
    /// </summary>
    public class Disk
    {
        /// <summary>
        /// Gets Windows Disk characteristics.
        /// </summary>
        /// <returns>An array of string.</returns>
        public static long[] WindowsDisk() => SetCWindowsSize();

        /// <summary>
        /// Set (get) values for Disk's characteristics (size of Windows).
        /// </summary>
        public static long[] SetCWindowsSize()
        {
            long[] rt = new long[2];
            DriveInfo driveInfo = new DriveInfo(@"C:\Windows");
            rt[0] = driveInfo.AvailableFreeSpace / 1000000;
            rt[1] = driveInfo.TotalSize / 1000000;
            return rt;
        }

        /// <summary>
        /// Gets the all (logical).
        /// </summary>
        /// <returns>A list of string.</returns>
        public static List<string> GetAll()
        {
            List<string> drives = new List<string>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                string DriveString = string.Empty;
                DriveString = d.Name + ";";
                if (d.IsReady)
                {
                    DriveString +=
                        d.DriveFormat + ";" +
                        d.AvailableFreeSpace + ";" +
                        d.TotalSize;
                    drives.Add(DriveString);
                }
            }
            return drives;
        }

        /// <summary>
        /// Gets the total free space.
        /// </summary>
        /// <param name="driveName">The drive name.</param>
        /// <returns>A long.</returns>
        public static long GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                    return drive.AvailableFreeSpace;
            }
            return -1;
        }

        /// <summary>
        /// Gets the total space.
        /// </summary>
        /// <param name="driveName">The drive name.</param>
        /// <returns>A long.</returns>
        public static long GetTotalSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                    return drive.TotalSize;
            }
            return -1;
        }
    }

    /// <summary>
    /// Work with informations of Monitor(s).
    /// </summary>
    public class Monitor
    {
        /// <summary>
        /// Gets the working area of monitor in Windows.
        /// </summary>
        public static Rectangle WorkingArea() => Screen.PrimaryScreen.WorkingArea;

        /// <summary>
        /// Captures the screen to memory stream.
        /// </summary>
        /// <returns>A MemoryStream.</returns>
        public static MemoryStream CaptureScreenToMemoryStream()
        {
            Bitmap BM = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height);
            Graphics GH = Graphics.FromImage(BM);
            GH.CopyFromScreen(0, 0, 0, 0, BM.Size);
            MemoryStream memoryStream = new MemoryStream();
            BM.Save(memoryStream, ImageFormat.Png);
            memoryStream.Position = 0;
            return memoryStream;
        }

        /// <summary>
        /// Captures the screen.
        /// </summary>
        /// <returns>An Image.</returns>
        public static Image CaptureScreen()
        {
            return CaptureWindow(Import.GetDesktopWindow());
        }

        /// <summary>
        /// Captures the window to file.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="format">The format.</param>
        public static void CaptureWindowToFile(IntPtr handle,
            string filename,
            ImageFormat format)
        {
            Image img = CaptureWindow(handle);
            img.Save(filename, format);
        }

        /// <summary>
        /// Captures the screen to file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="format">The format.</param>
        public static void CaptureScreenToFile(string filename,
            ImageFormat format)
        {
            Image img = CaptureScreen();
            img.Save(filename, format);
        }

        /// <summary>
        /// Captures the window.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns>An Image.</returns>
        public static Image CaptureWindow(IntPtr handle)
        {
            IntPtr hdcSrc = Import.GetWindowDC(handle);
            Enums.RECT windowRect = new Enums.RECT();
            Import.GetWindowRect(handle, out windowRect);
            int width = windowRect.Right - windowRect.Left;
            int height = windowRect.Bottom - windowRect.Top;
            IntPtr hdcDest = Import.CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = Import.CreateCompatibleBitmap(hdcSrc,
                width,
                height);
            IntPtr hOld = Import.SelectObject(hdcDest,
                hBitmap);
            Import.BitBlt(hdcDest, 0, 0,
                width,
                height, hdcSrc,
                0, 0,
                Constants.SRCCOPY);
            Import.SelectObject(hdcDest, hOld);
            Import.DeleteDC(hdcDest);
            Import.ReleaseDC(handle, hdcSrc);
            Image img = Image.FromHbitmap(hBitmap);
            Import.DeleteObject(hBitmap);
            return img;
        }

        /// <summary>
        /// Gets the resolution (method 2).
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetResolution2()
        {
            string size = string.Empty;
            Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = graphics.GetHdc();
            int monitorHeight = Import.GetDeviceCaps(desktop, 6);
            int monitorWidth = Import.GetDeviceCaps(desktop, 4);
            size = $"{Math.Sqrt(Math.Pow(monitorHeight, 2) + Math.Pow(monitorWidth, 2)) / 25,4:#,##0.00}";
            return size;
        }

        /// <summary>
        /// Gets the resolution (method 1).
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetResolution()
        {
            uint width = 0;
            uint height = 0;
            foreach (var desktopMonitor in new ManagementObjectSearcher("ROOT\\CIMV2",
                "SELECT * FROM Win32_DesktopMonitor").Get())
            {
                width = (uint)desktopMonitor["ScreenWidth"];
                height = (uint)desktopMonitor["ScreenHeight"];
            }
            return $"{Convert.ToString(width)}x{Convert.ToString(height)}";
        }

        /// <summary>
        /// Gets the all.
        /// </summary>
        /// <returns>A list of string.</returns>
        public static List<string> GetAll()
        {
            List<string> screensall = new List<string>();
            Screen[] allScreens = Screen.AllScreens;
            foreach (var screen in allScreens)
            {
                string screenString = string.Empty;
                screenString =
                    screen.DeviceName.Replace("\\", "").Replace(".", "") + ";" +
                    screen.Bounds.Width + "|" + screen.Bounds.Height + ";" +
                    Functions.Windows.Controllers.Monitor.FriendlyName(screen) + ";" +
                    screen.Primary.ToString();
                screensall.Add(screenString);
            }
            return screensall;
        }
    }

    /// <summary>
    /// Work with informations of Motherboard.
    /// </summary>
    public class Motherboard
    {
        /// <summary>
        /// Gets the firmware type.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetFirmwareType()
        {
            return Processes.Execute("cmd",
                "/C echo %firmware_type%");
        }
    }

    /// <summary>
    /// Work with informations of COM port.
    /// </summary>
    public class ComPort
    {
        /// <summary>
        /// Gets all COM ports (devices).
        /// </summary>
        /// <returns>An array of string.</returns>
        public static string[] GetAllDevices()
        {
            return System.IO.Ports.SerialPort.GetPortNames();
        }
    }

    /// <summary>
    /// Work with informations of RAM.
    /// </summary>
    public class Ram
    {
        /// <summary>
        /// Gets all characteristics.
        /// </summary>
        /// <returns>An array of string.</returns>
        public static string[] Characteristics() => Set().Split('/');

        /// <summary>
        /// Sets (gets) values for RAM's characteristics.
        /// </summary>
        public static string Set()
        {
            string toRAM = "";
            ManagementObjectSearcher myRamObject = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
            foreach (ManagementObject obj in myRamObject.Get())
            {
                toRAM += obj["Name"].ToString() + "/";
                toRAM += obj["Capacity"].ToString() + "/";
                toRAM += obj["ConfiguredVoltage"].ToString() + "/";
                toRAM += obj["MaxVoltage"].ToString() + "/";
                toRAM += obj["MemoryType"].ToString() + "/";
                toRAM += obj["MinVoltage"].ToString() + "/";
                toRAM += obj["SerialNumber"].ToString() + "/";
                toRAM += obj["SMBIOSMemoryType"].ToString() + "/";
                toRAM += obj["Speed"].ToString() + "/";
            }
            toRAM += Convert.ToString(new Microsoft.VisualBasic.Devices.ComputerInfo()
                .TotalPhysicalMemory / 1000000) + "/";
            toRAM += Convert.ToString(new Microsoft.VisualBasic.Devices.ComputerInfo()
                .AvailablePhysicalMemory / 1000000) + "/";
            toRAM += Convert.ToString(new Microsoft.VisualBasic.Devices.ComputerInfo()
                .AvailableVirtualMemory / 1000000) + "/";
            toRAM += Convert.ToString((int)new Microsoft.VisualBasic.Devices.ComputerInfo()
                .TotalPhysicalMemory / 1000000) + "/";
            toRAM += Convert.ToString((int)new Microsoft.VisualBasic.Devices.ComputerInfo()
                .TotalPhysicalMemory / 1000000 - (int)new Microsoft.VisualBasic.Devices.ComputerInfo()
                .AvailablePhysicalMemory / 1000000) + "/";
            toRAM += Convert.ToString((int)new Microsoft.VisualBasic.Devices.ComputerInfo()
                .AvailableVirtualMemory / 1000000) + "/";
            return toRAM;
        }
    }
}
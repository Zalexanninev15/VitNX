using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;

using VitNX3.Functions.Win32;

namespace VitNX3.Functions.Information
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
        private static long[] SetCWindowsSize()
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
            return AppsAndProcesses.Processes.Execute("cmd",
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
        private static string Set()
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
            return toRAM;
        }
    }

    /// <summary>
    /// Work with informations of Internet (PC).
    /// </summary>
    public class Internet
    {
        /// <summary>
        /// Gets the host name (name of PC, Windows System).
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetHostName() => Dns.GetHostName();

        /// <summary>
        /// Gets the local IPv6 (obsolete, but work).
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetLocalIPv6() => Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();

        /// <summary>
        /// Gets the local IPv4 (obsolete, but work).
        /// </summary>
        /// <returns>A string.</returns>
        public static string GetLocalIPv4() => Dns.GetHostByName(Dns.GetHostName()).AddressList[1].ToString();

        /// <summary>
        /// Gets the public IP of PC.
        /// </summary>
        public static string GetPublicIP()
        {
            string PublicIP = "localhost";
            using (WebClient client = new WebClient())
            {
                try { PublicIP = client.DownloadString("https://icanhazip.com"); }
                catch { PublicIP = client.DownloadString("https://checkip.amazonaws.com"); }
                if (PublicIP == "" || PublicIP == "0.0.0.0" || PublicIP == "localhost")
                {
                    try { PublicIP = client.DownloadString("https://checkip.amazonaws.com"); } catch { PublicIP = "0.0.0.0"; }
                }
            }
            return PublicIP.Trim();
        }

        /// <summary>
        /// Gets the MAC address.
        /// </summary>
        /// <returns>A string.</returns>
        public string GetMacAddress()
        {
            string macaddr = "";
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics == null || nics.Length < 1)
                return "0";
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    PhysicalAddress address = adapter.GetPhysicalAddress();
                    byte[] bytes = address.GetAddressBytes();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        macaddr += bytes[i].ToString("X2");
                        if (i != bytes.Length - 1)
                            macaddr += "-";
                    }
                }
                break;
            }
            return macaddr;
        }

        /// <summary>
        /// Internet status (int32) for IsHaveInternet function
        /// </summary>
        public enum INTERNET_STATUS
        {
            UNKNOWN_PROBLEM,
            UNCONNECTED,
            CONNECTED
        }

        /// <summary>
        /// Are the have internet.
        /// </summary>
        /// <param name="ipAddressOrDomainForPing">The ip address or domain for ping.</param>
        /// <returns>An INTERNET_STATUS.</returns>
        public static INTERNET_STATUS IsHaveInternet(string ipAddressOrDomainForPing = "google.com")
        {
            try
            {
                Ping ping = new Ping();
                PingReply pingReply = null;
                pingReply = ping.Send(ipAddressOrDomainForPing);
                if (pingReply.Status == IPStatus.Success)
                    return INTERNET_STATUS.CONNECTED;
                else
                    return INTERNET_STATUS.UNCONNECTED;
            }
            catch { return INTERNET_STATUS.UNKNOWN_PROBLEM; }
        }
    }
}
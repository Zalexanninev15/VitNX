﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Reflection;
using System.Windows.Forms;
using VitNX.Functions.Windows.Apps;
using VitNX.Functions.Windows.Win32;

namespace VitNX.Functions.Common.Information
{
    public class Helper
    {
        public static readonly string[] SizeSuffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
    }

    public class Windows
    {
        public static string ComputerName = Environment.MachineName;
        public static string CurrentUsername = Environment.UserName;
        public static DateTime LocalTime = new Microsoft.VisualBasic.Devices.Clock().LocalTime;
        public static bool Is64bit = Environment.Is64BitOperatingSystem;
        public static string WindowsVersion = Convert.ToString(Environment.OSVersion.Version);
        public static double WindowsVersionFromREG = double.Parse((string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentVersion", ""), System.Globalization.CultureInfo.InvariantCulture);
        public static string WindowsEditionIDFromREG = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "EditionID", "");
        public static string WindowsCurrentBuildNumberFromREG = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber", "");
        public static string WindowsProductNameFromREG = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "");
        public static string WindowsDisplayVersionFromREG = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DisplayVersion", "");
        public static string WindowsReleaseIdFromREG = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "");

        public static MemoryStream GetScreen()
        {
            Bitmap BM = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics GH = Graphics.FromImage(BM);
            GH.CopyFromScreen(0, 0, 0, 0, BM.Size);
            MemoryStream memoryStream = new MemoryStream();
            BM.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            memoryStream.Position = 0;
            return memoryStream;
        }

        public static string WriteAllUsersToTempFile()
        {
            string users = Processes.Execute("wmic", "useraccount list full");
            string tempFile = $@"{Path.GetTempPath()}\Users.txt";
            FileSystem.WriteTextToFileUTF8(users, tempFile);
            string[] lines = File.ReadAllLines(tempFile);
            using (StreamWriter writer = new StreamWriter(tempFile))
            {
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        writer.WriteLine(line);
                }
            }
            string[] usersList = File.ReadAllLines(tempFile);
            for (int i = 0; i < usersList.Length; i++)
            {
                if (i == 0)
                    usersList[0] = "Users:\n\n" + usersList[0];
                usersList[i] = usersList[i].Replace("TRUE", "Yes");
                usersList[i] = usersList[i].Replace("FALSE", "No");
                usersList[i] = usersList[i].Replace("FullName", "Full name");
                usersList[i] = usersList[i].Replace("LocalAccount", "Local account");
                usersList[i] = usersList[i].Replace("Degraded", "Degraded\n");
                usersList[i] = usersList[i].Replace("InstallDate", "Creation date");
                usersList[i] = usersList[i].Replace("AccountType", "Account type");
                usersList[i] = usersList[i].Replace("PasswordChangeable", "Password changeable");
                usersList[i] = usersList[i].Replace("PasswordExpires", "Password expires");
                usersList[i] = usersList[i].Replace("PasswordRequired", "Password required");
                usersList[i] = usersList[i].Replace("512", "Plain [512]").Replace("2", "Disconnected [2]").Replace("256", "Temporary duplicate account [256]").Replace("128", "Encrypted password allowed [128]");
                usersList[i] = usersList[i].Replace("OK", "Enabled\n");
            }
            File.WriteAllLines(tempFile, usersList);
            return tempFile;
        }
    }

    public class Cpu
    {
        public static string _Name = "";
        public static string _DeviceID = "";
        public static string _Manufacturer = "";
        public static string _CurrentClockSpeed = "";
        public static string _MaxClockSpeed = "";
        public static string _Caption = "";
        public static string _NumberOfCores = "";
        public static string _NumberOfEnabledCore = "";
        public static string _NumberOfLogicalProcessors = "";
        public static string _Architecture = "";
        public static string _Family = "";
        public static string _ProcessorType = "";
        public static string _Characteristics = "";
        public static string _AddressWidth = "";
        public static string _SerialNumber = "";
        public static string _ThreadCount = "";
        public static string _LoadPercentage = "";
        public static string _CurrentVoltage = "";
        public static int _UsagePercent = 0;

        public static void Set()
        {
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                _Name = obj["Name"].ToString();
                _DeviceID = obj["DeviceID"].ToString();
                _Manufacturer = obj["Manufacturer"].ToString();
                _CurrentClockSpeed = obj["CurrentClockSpeed"].ToString();
                _MaxClockSpeed = obj["MaxClockSpeed"].ToString();
                _Caption = obj["Caption"].ToString();
                _NumberOfCores = obj["NumberOfCores"].ToString();
                _NumberOfEnabledCore = obj["NumberOfEnabledCore"].ToString();
                _NumberOfLogicalProcessors = obj["NumberOfLogicalProcessors"].ToString();
                _Architecture = obj["Architecture"].ToString();
                _Family = obj["Family"].ToString();
                _ProcessorType = obj["ProcessorType"].ToString();
                _Characteristics = obj["Characteristics"].ToString();
                _AddressWidth = obj["AddressWidth"].ToString();
                _SerialNumber = obj["SerialNumber"].ToString();
                _ThreadCount = obj["ThreadCount"].ToString();
                _LoadPercentage = obj["LoadPercentage"].ToString();
                _CurrentVoltage = obj["CurrentVoltage"].ToString();
            }
            _UsagePercent = Convert.ToInt32(new PerformanceCounter("Processor", "% Processor Time", "_Total").NextValue());
        }

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

    public class Gpu
    {
        public static string _Name = "";
        public static string _Status = "";
        public static string _DeviceID = "";
        public static string _AdapterRAM = "";
        public static string _AdapterDACType = "";
        public static string _Monochrome = "";
        public static string _InstalledDisplayDrivers = "";
        public static string _DriverVersion = "";
        public static string _VideoArchitecture = "";
        public static string _VideoMemoryType = "";
        public static string _MaxRefreshRate = "";
        public static string _MinRefreshRate = "";
        public static string _VideoModeDescription = "";

        public static void Set()
        {
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                _Name = obj["Name"].ToString();
                _Status = obj["Status"].ToString();
                _DeviceID = obj["DeviceID"].ToString();
                _AdapterRAM = SizeSuffix((long)Convert.ToDouble(obj["AdapterRAM"]));
                _AdapterDACType = obj["AdapterDACType"].ToString();
                _Monochrome = obj["Monochrome"].ToString();
                _InstalledDisplayDrivers = obj["InstalledDisplayDrivers"].ToString();
                _DriverVersion = obj["DriverVersion"].ToString();
                _VideoArchitecture = obj["VideoArchitecture"].ToString();
                _VideoMemoryType = obj["VideoMemoryType"].ToString();
                _MaxRefreshRate = obj["MaxRefreshRate"].ToString();
                _MinRefreshRate = obj["MinRefreshRate"].ToString();
                _VideoModeDescription = obj["VideoModeDescription"].ToString();
            }
        }

        private static string SizeSuffix(Int64 value)
        {
            if (value < 0) 
                return "-" + SizeSuffix(-value);
            if (value == 0) 
                return "0.0 bytes";
            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));
            return string.Format("{0:n1} {1}", adjustedSize, Helper.SizeSuffixes[mag]);
        }
    }

    public class Disk
    {
        public static long windowsDiskAvailableFreeSpace = 0;
        public static long windowsDiskTotalSize = 0;

        public static List<string> GetAll()
        {
            List<string> drives = new List<string>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                string DriveString = string.Empty;
                DriveString = d.Name + ";";
                if (d.IsReady == true)
                {
                    DriveString +=
                        d.DriveFormat + ";" +
                        d.AvailableFreeSpace + ";" +
                        d.TotalSize;
                    drives.Add(DriveString);
                    DriveString = string.Empty;
                }
            }
            return drives;
        }

        public static long GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                    return drive.AvailableFreeSpace;
            }
            return -1;
        }

        public static long GetTotalSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                    return drive.TotalSize;
            }
            return -1;
        }

        public static void SetCWindowsSize()
        {
            DriveInfo driveInfo = new DriveInfo(@"C:\Windows");
            windowsDiskAvailableFreeSpace = driveInfo.AvailableFreeSpace / 1000000;
            windowsDiskTotalSize = driveInfo.TotalSize / 1000000;
        }
    }

    public class Monitor
    {
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

        public static string GetResolution()
        {
            UInt32 width = 0;
            UInt32 height = 0;
            foreach (var desktopMonitor in new ManagementObjectSearcher("ROOT\\CIMV2", "SELECT * FROM Win32_DesktopMonitor").Get())
            {
                width = (UInt32)desktopMonitor["ScreenWidth"];
                height = (UInt32)desktopMonitor["ScreenHeight"];
            }
            return $"{Convert.ToString(width)}x{Convert.ToString(height)}";
        }

        public List<string> GetAll()
        {
            List<string> screensall = new List<string>();
            Screen[] allScreens = Screen.AllScreens;
            foreach (var screen in allScreens)
            {
                string screenString = string.Empty;
                screenString =
                    screen.DeviceName.Replace("\\", "").Replace(".", "") + ";" +
                    screen.Bounds.Width + "|" + screen.Bounds.Height + ";" +
                    Functions.Windows.NativeControls.Monitor.DeviceFriendlyName(screen) + ";" +
                    screen.Primary.ToString();
                screensall.Add(screenString);
                screenString = string.Empty;
            }
            return screensall;
        }
    }

    public class Motherboard
    {
        public static string GetFirmwareType()
        {
            return Processes.Execute("cmd", "/C echo %firmware_type%");
        }
    }

    public class Ram
    {
        public static string _NameID = "";
        public static string _Capacity = "";
        public static string _ConfiguredVoltage = "";
        public static string _MaxVoltage = "";
        public static string _MemoryType = "";
        public static string _MinVoltage = "";
        public static string _SerialNumber = "";
        public static string _SMBIOSMemoryType = "";
        public static string _Speed = "";
        public static int _MaxMB = 0;
        public static int _UsedMB = 0;
        public static int _IntRamVirtual = 0;
        public static ulong _Max = 0;
        public static ulong _Used = 0;
        public static ulong _RamVirtual = 0;

        public static void Set()
        {
            ManagementObjectSearcher myRamObject = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
            foreach (ManagementObject obj in myRamObject.Get())
            {
                _NameID = obj["Name"].ToString();
                _Capacity = obj["Capacity"].ToString();
                _ConfiguredVoltage = obj["ConfiguredVoltage"].ToString();
                _MaxVoltage = obj["MaxVoltage"].ToString();
                _MemoryType = obj["MemoryType"].ToString();
                _MinVoltage = obj["MinVoltage"].ToString();
                _SerialNumber = obj["SerialNumber"].ToString();
                _SMBIOSMemoryType = obj["SMBIOSMemoryType"].ToString();
                _Speed = obj["Speed"].ToString();
            }
            _Max = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1000000;
            _Used = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory / 1000000;
            _RamVirtual = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailableVirtualMemory / 1000000;
            _MaxMB = (int)_Max;
            _UsedMB = (int)_Max - (int)_Used;
            _IntRamVirtual = (int)_RamVirtual;
        }
    }
}
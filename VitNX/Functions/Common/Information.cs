using Microsoft.Win32;
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
        public static readonly string[] _sizeSuffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
    }

    public class Windows
    {
        public static string _computerName = Environment.MachineName;
        public static string _currentUsername = Environment.UserName;
        public static DateTime _localTime = new Microsoft.VisualBasic.Devices.Clock().LocalTime;
        public static bool _is64bit = Environment.Is64BitOperatingSystem;
        public static string _windowsVersion = Convert.ToString(Environment.OSVersion.Version);
        public static double _windowsVersionFromREG = double.Parse((string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentVersion", ""), System.Globalization.CultureInfo.InvariantCulture);
        public static string _windowsEditionIDFromREG = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "EditionID", "");
        public static string _windowsCurrentBuildNumberFromREG = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber", "");
        public static string _windowsProductNameFromREG = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", "");
        public static string _windowsDisplayVersionFromREG = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DisplayVersion", "");
        public static string _windowsReleaseIdFromREG = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", "");

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

        public static void GetAllUsers()
        {
            string users = Processes.Execute("wmic", "useraccount list full");
            string temp_file = $@"{Path.GetTempPath()}\Пользователи.txt";
            FileSystem.WriteTextToFileUTF8(users, temp_file);
            string[] lines = File.ReadAllLines(temp_file);
            using (StreamWriter writer = new StreamWriter(temp_file))
            {
                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                        writer.WriteLine(line);
                }
            }
            string[] rep = File.ReadAllLines(temp_file);
            for (int i = 0; i < rep.Length; i++)
            {
                if (i == 0)
                {
                    rep[0] = "Пользователи:\n\n" + rep[0];
                }
                rep[i] = rep[i].Replace("TRUE", "Да");
                rep[i] = rep[i].Replace("FALSE", "Нет");
                rep[i] = rep[i].Replace("Description", "Описание");
                rep[i] = rep[i].Replace("Domain", "Домен");
                rep[i] = rep[i].Replace("FullName", "Полное имя");
                rep[i] = rep[i].Replace("Name", "Имя");
                rep[i] = rep[i].Replace("LocalAccount", "Локальная запись");
                rep[i] = rep[i].Replace("Disabled", "Отключена");
                rep[i] = rep[i].Replace("Status", "Статус");
                rep[i] = rep[i].Replace("Lockout", "Блокировка");
                rep[i] = rep[i].Replace("Degraded", "Отключён\n");
                rep[i] = rep[i].Replace("InstallDate", "Дата создания");
                rep[i] = rep[i].Replace("AccountType", "Тип учётной записи");
                rep[i] = rep[i].Replace("PasswordChangeable", "Пароль можно изменить");
                rep[i] = rep[i].Replace("PasswordExpires", "Срок действия пароля истекает");
                rep[i] = rep[i].Replace("PasswordRequired", "Требуется пароль");
                rep[i] = rep[i].Replace("512", "Обычный [512]").Replace("2", "Отключённый [2]").Replace("256", "Временная дублирующая учётная запись [256]").Replace("128", "Разрешён зашифрованный пароль [128]");
                rep[i] = rep[i].Replace("OK", "Включён\n");
            }
            File.WriteAllLines(temp_file, rep);
        }
    }

    public class Cpu
    {
        public static string name = "";
        public static string deviceID = "";
        public static string manufacturer = "";
        public static string currentClockSpeed = "";
        public static string maxClockSpeed = "";
        public static string caption = "";
        public static string numberOfCores = "";
        public static string numberOfEnabledCore = "";
        public static string numberOfLogicalProcessors = "";
        public static string architecture = "";
        public static string family = "";
        public static string processorType = "";
        public static string characteristics = "";
        public static string addressWidth = "";
        public static string serialNumber = "";
        public static string threadCount = "";
        public static string loadPercentage = "";
        public static string currentVoltage = "";
        public static int usagePercent = 0;

        public static void Set()
        {
            ManagementObjectSearcher myProcessorObject = new ManagementObjectSearcher("select * from Win32_Processor");
            foreach (ManagementObject obj in myProcessorObject.Get())
            {
                name = obj["Name"].ToString();
                deviceID = obj["DeviceID"].ToString();
                manufacturer = obj["Manufacturer"].ToString();
                currentClockSpeed = obj["CurrentClockSpeed"].ToString();
                maxClockSpeed = obj["MaxClockSpeed"].ToString();
                caption = obj["Caption"].ToString();
                numberOfCores = obj["NumberOfCores"].ToString();
                numberOfEnabledCore = obj["NumberOfEnabledCore"].ToString();
                numberOfLogicalProcessors = obj["NumberOfLogicalProcessors"].ToString();
                architecture = obj["Architecture"].ToString();
                family = obj["Family"].ToString();
                processorType = obj["ProcessorType"].ToString();
                characteristics = obj["Characteristics"].ToString();
                addressWidth = obj["AddressWidth"].ToString();
                serialNumber = obj["SerialNumber"].ToString();
                threadCount = obj["ThreadCount"].ToString();
                loadPercentage = obj["LoadPercentage"].ToString();
                currentVoltage = obj["CurrentVoltage"].ToString();
            }
            usagePercent = Convert.ToInt32(new PerformanceCounter("Processor", "% Processor Time", "_Total").NextValue());
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
        public static string name = "";
        public static string status = "";
        public static string deviceID = "";
        public static string adapterRAM = "";
        public static string adapterDACType = "";
        public static string monochrome = "";
        public static string installedDisplayDrivers = "";
        public static string driverVersion = "";
        public static string videoArchitecture = "";
        public static string videoMemoryType = "";
        public static string maxRefreshRate = "";
        public static string minRefreshRate = "";
        public static string videoModeDescription = "";

        public static void Set()
        {
            ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
            foreach (ManagementObject obj in myVideoObject.Get())
            {
                name = obj["Name"].ToString();
                status = obj["Status"].ToString();
                deviceID = obj["DeviceID"].ToString();
                adapterRAM = SizeSuffix((long)Convert.ToDouble(obj["AdapterRAM"]));
                adapterDACType = obj["AdapterDACType"].ToString();
                monochrome = obj["Monochrome"].ToString();
                installedDisplayDrivers = obj["InstalledDisplayDrivers"].ToString();
                driverVersion = obj["DriverVersion"].ToString();
                videoArchitecture = obj["VideoArchitecture"].ToString();
                videoMemoryType = obj["VideoMemoryType"].ToString();
                maxRefreshRate = obj["MaxRefreshRate"].ToString();
                minRefreshRate = obj["MinRefreshRate"].ToString();
                videoModeDescription = obj["VideoModeDescription"].ToString();
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
            return string.Format("{0:n1} {1}", adjustedSize, Helper._sizeSuffixes[mag]);
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
        public static string nameID = "";
        public static string capacity = "";
        public static string configuredVoltage = "";
        public static string maxVoltage = "";
        public static string memoryType = "";
        public static string minVoltage = "";
        public static string serialNumber = "";
        public static string SMBIOSMemoryType = "";
        public static string speed = "";
        public static int maxMB = 0;
        public static int usedMB = 0;
        public static int intRamVirtual = 0;
        public static ulong max = 0;
        public static ulong used = 0;
        public static ulong ramVirtual = 0;

        public static void Set()
        {
            ManagementObjectSearcher myRamObject = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");
            foreach (ManagementObject obj in myRamObject.Get())
            {
                nameID = obj["Name"].ToString();
                capacity = obj["Capacity"].ToString();
                configuredVoltage = obj["ConfiguredVoltage"].ToString();
                maxVoltage = obj["MaxVoltage"].ToString();
                memoryType = obj["MemoryType"].ToString();
                minVoltage = obj["MinVoltage"].ToString();
                serialNumber = obj["SerialNumber"].ToString();
                SMBIOSMemoryType = obj["SMBIOSMemoryType"].ToString();
                speed = obj["Speed"].ToString();
            }
            max = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1000000;
            used = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory / 1000000;
            ramVirtual = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailableVirtualMemory / 1000000;
            maxMB = (int)max;
            usedMB = (int)max - (int)used;
            intRamVirtual = (int)ramVirtual;
        }
    }
}
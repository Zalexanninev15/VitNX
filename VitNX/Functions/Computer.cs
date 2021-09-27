using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using VitNX.Win32;

namespace VitNX.Functions.Computer
{
    public class CPU
    {
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
            foreach (var item in new System.Management.ManagementObjectSearcher("select MaxClockSpeed from Win32_Processor").Get())
            {
                var clockSpeedx = (uint)item["MaxClockSpeed"];
                clockSpeed = clockSpeedx.ToString();
            }
            return clockSpeed;
        }
    }

    //public class GPU
    //{

    //}

    public class Disk
    {
        public long GetTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName) { return drive.AvailableFreeSpace; }
            }
            return -1;
        }

        public long GetTotalSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName) { return drive.TotalSize; }
            }
            return -1;
        }
    }

    public class Monitor
    {
        public static string GetMonitorSize()
        {
            string size = string.Empty;
            Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = graphics.GetHdc();
            int monitorHeight = NativeFunctions.GetDeviceCaps(desktop, 6);
            int monitorWidth = NativeFunctions.GetDeviceCaps(desktop, 4);
            size = $"{Math.Sqrt(Math.Pow(monitorHeight, 2) + Math.Pow(monitorWidth, 2)) / 25,4:#,##0.00}";
            return size;
        }
    }

    public class Motherboard
    {
        public static string GetFirmwareType() { return Processes.Run("cmd", "/C echo %firmware_type%", System.Diagnostics.ProcessWindowStyle.Hidden, false, false, true, true, false); }
    }
}
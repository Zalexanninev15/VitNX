using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using VitNX.Functions.Common;
using VitNX.Functions.Windows.Apps;

namespace VitNX.Functions.Windows.Information
{
    public class Windows
    {
        public static bool Is64x()
        { return Environment.Is64BitOperatingSystem; }

        public static string GetVersion()
        { return Convert.ToString(Environment.OSVersion.Version); }

        public static double GetCurrentVersionFromREG()
        { return double.Parse((string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentVersion", ""), System.Globalization.CultureInfo.InvariantCulture); }

        public static string GetEditionIDFromREG()
        { return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "EditionID", ""); }

        public static string GetCurrentBuildNumberFromREG()
        { return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber", ""); }

        public static string GetProductNameFromREG()
        { return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", ""); }

        public static string GetDisplayVersionFromREG()
        { return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DisplayVersion", ""); }

        public static string GetReleaseIdFromREG()
        { return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", ""); }

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
                if (drive.IsReady && drive.Name == driveName)
                    return drive.AvailableFreeSpace;
            }
            return -1;
        }

        public long GetTotalSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                    return drive.TotalSize;
            }
            return -1;
        }
    }

    public class Monitor
    {
        public static string GetSize()
        {
            string size = string.Empty;
            Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = graphics.GetHdc();
            int monitorHeight = Win32.Import.GetDeviceCaps(desktop, 6);
            int monitorWidth = Win32.Import.GetDeviceCaps(desktop, 4);
            size = $"{Math.Sqrt(Math.Pow(monitorHeight, 2) + Math.Pow(monitorWidth, 2)) / 25,4:#,##0.00}";
            return size;
        }
    }

    public class Motherboard
    {
        public static string GetFirmwareType()
        { return Apps.Processes.Execute("cmd", "/C echo %firmware_type%"); }
    }
}
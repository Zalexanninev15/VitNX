using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Security.Principal;

namespace VitNX.Functions.Windows.Apps
{
    public class Processes
    {
        public static string GetList()
        {
            string procList = "Все процессы:";
            foreach (Process process in Process.GetProcesses())
            {
                var procSize = "0x0";
                try
                {
                    var counter = new PerformanceCounter("Process", "Working Set - Private", process.ProcessName);
                    procSize = Convert.ToString(counter.RawValue / 1048576);
                }
                catch { }
                if (procSize != "0x0" && procSize != "0")
                    procList += $"\n\nИмя поцесса: {process.ProcessName}.exe" +
                        $"\nID: {process.Id}" +
                        $"\nПамять: {procSize} МБ" +
                        $"\nTitle & Handle: \"{process.MainWindowTitle}\" & {process.MainWindowHandle}";
            }
            return procList;
        }

        public static void Run(string file,
            string arguments = "")
        {
            var start = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = file,
                    Arguments = arguments,
                    Verb = "open"
                }
            };
            start.Start();
        }

        public static string Execute(string file,
            string arguments)
        {
            var codepage = System.Globalization.CultureInfo.CurrentCulture.TextInfo.OEMCodePage;
            var desiredEncoding = System.Text.Encoding.GetEncoding(codepage);
            var start = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = file,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    Verb = "open",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                    StandardOutputEncoding = desiredEncoding
                }
            };
            start.WaitForExit();
            start.Start();
            string output = start.StandardOutput.ReadToEnd();
            return output;
        }

        public static void Open(string element)
        {
            var ps = new ProcessStartInfo(element)
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        public static Process OnlyOneProcess()
        {
            Process current = Process.GetCurrentProcess();
            Process[] pr = Process.GetProcessesByName(current.ProcessName);
            foreach (Process i in pr)
            {
                if (i.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    { return i; }
                }
            }
            return null;
        }

        public static void KillThis()
        {
            Process.GetCurrentProcess().Kill();
        }

        public static void Kill(string process)
        {
            var start = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = $"/IM \"{process}\" /F /T",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                }
            };
            start.Start();
        }

        public static bool IsAdministrator_YourApp()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    public class Installed
    {
        public static string GetList()
        {
            string toText = "Установленные приложения:";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
            ManagementObjectCollection information = searcher.Get();
            foreach (ManagementObject app in information)
            {
                string toTextText = "";
                try
                {
                    toTextText = GetPath(Convert.ToString(app["Name"]));
                    if (toTextText == "")
                        toTextText = GetPath(Convert.ToString(app["IdentifyingNumber"]));
                }
                catch { }
                toText += ($"\n\nНазвание: {app["Name"]} ({app["Caption"]})" +
                $"\nВерсия: {app["Version"]}" +
                $"\nРазработчик: {app["Vendor"]}" +
                $"\nДата установки: {app["InstallDate"]}" +
                $"\nМестоположение: {toTextText}");
            }
            return toText;
        }

        public static string GetPath(string applicationName)
        {
            var installPath = FindAppPath(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", applicationName);
            if (installPath == "")
                installPath = FindAppPath(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall", applicationName);
            if (installPath == "")
                installPath = FindAppPath(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall", applicationName);
            return installPath;
        }

        public static string FindAppPath(string keyPath, string applicationName)
        {
            var uninstall = Registry.LocalMachine.OpenSubKey(keyPath);
            foreach (var productSubKey in uninstall.GetSubKeyNames())
            {
                var product = uninstall.OpenSubKey(productSubKey);
                var displayName = product.GetValue("DisplayName");
                if (Convert.ToString(displayName) != "" && Convert.ToString(displayName).Contains(applicationName))
                    return product.GetValue("InstallLocation").ToString();
            }
            return null;
        }
    }
}
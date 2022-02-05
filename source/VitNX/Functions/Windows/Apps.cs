using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using System.Security.Principal;
using System.Threading;

namespace VitNX.Functions.Windows.Apps
{
    public class Processes
    {
        public static string GetListWithInformation()
        {
            string procList = "All processes:";
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
                    procList += $"\n\nName: {process.ProcessName}.exe" +
                        $"\nID: {process.Id}" +
                        $"\nUsed memory: {procSize} МБ" +
                        $"\nTitle & Handle: \"{process.MainWindowTitle}\" & {process.MainWindowHandle}";
            }
            return procList;
        }

        public static List<string> GetList()
        {
            List<string> output = new List<string>();
            foreach (Process proc in Process.GetProcesses())
                output.Add(proc.ProcessName.ToUpper());
            return output;
        }

        public static void Run(string targetFile,
            string arguments = "")
        {
            var start = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = targetFile,
                    Arguments = arguments,
                    //Verb = "open"
                }
            };
            start.Start();
        }

        public static string Execute(string targetFile,
            string arguments)
        {
            var codepage = System.Globalization.CultureInfo.CurrentCulture.TextInfo.OEMCodePage;
            var desiredEncoding = System.Text.Encoding.GetEncoding(codepage);
            var start = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = targetFile,
                    Arguments = arguments,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    //Verb = "open",
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

        public static void Open(string targetFile)
        {
            var ps = new ProcessStartInfo(targetFile)
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
                        return i;
                }
            }
            return null;
        }

        public static void KillThis()
        {
            Process.GetCurrentProcess().Kill();
        }

        public static void Kill(string processNameWithExe)
        {
            var start = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "taskkill",
                    Arguments = $"/IM \"{processNameWithExe}\" /F /T",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                }
            };
            start.Start();
        }

        public static bool IsAdministratorYourApp()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static bool IsOneYourApp(string appTitle)
        {
            bool createdNew = false;
            Mutex currentApp = new Mutex(false, appTitle, out createdNew);
            if (!createdNew)
                return false;
            else
                return true;
        }
    }

    public class Installed
    {
        public static string GetList()
        {
            string toText = "Installed apps:";
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
                toText += ($"\nName: {app["Name"]} ({app["Caption"]})" +
                $"\nVersion: {app["Version"]}" +
                $"\nAuthor: {app["Vendor"]}" +
                $"\nInstall date: {app["InstallDate"]}" +
                $"\nInstall path: {toTextText}");
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
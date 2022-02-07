using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Security.Principal;
using System.Threading;

namespace VitNX.Functions.Windows.Apps
{
    /// <summary>
    /// Work with processes.
    /// </summary>
    public class Processes
    {
        /// <summary>
        /// Gets the list of all processes with information.
        /// </summary>
        /// <returns>A string.</returns>
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

        /// <summary>
        /// Gets the list of all processes.
        /// </summary>
        /// <returns>A list of string.</returns>
        public static List<string> GetList()
        {
            List<string> output = new List<string>();
            foreach (Process proc in Process.GetProcesses())
                output.Add(proc.ProcessName.ToUpper());
            return output;
        }

        /// <summary>
        /// Launch a third-party applications.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        /// <param name="arguments">The arguments.</param>
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

        /// <summary>
        /// Opens the link of site.
        /// </summary>
        /// <param name="link">The link.</param>
        /// <returns>A bool.</returns>
        public static bool OpenLink(string link)
        {
            try
            {
                Process.Start(link);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Execute a third-party applications with the result of that application (useful when running console applications).
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>A string.</returns>
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

        /// <summary>
        /// Opens the file/link.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        public static void Open(string targetFile)
        {
            var ps = new ProcessStartInfo(targetFile)
            {
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(ps);
        }

        /// <summary>
        /// Kills the process.
        /// </summary>
        /// <param name="processNameWithExe">The process name with exe.</param>
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

        /// <summary>
        /// Whether your application has administrator rights.
        /// </summary>
        /// <returns>A bool.</returns>
        public static bool IsAdministratorYourApp()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// You only have 1 copy of the application running.
        /// </summary>
        /// <param name="appTitle">The app title.</param>
        /// <returns>A bool.</returns>
        public static bool IsOneYourApp(string appTitle)
        {
            bool createdNew;
            Mutex currentApp = new Mutex(true, appTitle, out createdNew);
            return createdNew;
        }

        /// <summary>
        /// You only have 1 copy of the application running.
        /// </summary>
        /// <param name="currentProcess">The current process.</param>
        /// <param name="currentExeAssemblyLocation">The current exe assembly location.</param>
        /// <returns>A Process.</returns>
        public static Process OnlyOne(Process currentProcess, string currentExeAssemblyLocation)
        {
            currentExeAssemblyLocation = currentExeAssemblyLocation.Replace("/", "\\");
            Process[] pr = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process i in pr)
            {
                if (i.Id != currentProcess.Id)
                {
                    if (currentExeAssemblyLocation == currentProcess.MainModule.FileName)
                        return i;
                }
            }
            return null;
        }
    }

    /// <summary>
    /// Work with installed applications.
    /// </summary>
    public class Installed
    {
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns>A string.</returns>
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

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <param name="applicationName">The application name.</param>
        /// <returns>A string.</returns>
        public static string GetPath(string applicationName)
        {
            var installPath = FindAppPath(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", applicationName);
            if (installPath == "")
                installPath = FindAppPath(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall", applicationName);
            if (installPath == "")
                installPath = FindAppPath(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall", applicationName);
            return installPath;
        }

        /// <summary>
        /// Finds the app path.
        /// </summary>
        /// <param name="regKeyPath">The key path.</param>
        /// <param name="applicationName">The application name.</param>
        /// <returns>A string.</returns>
        public static string FindAppPath(string regKeyPath, string applicationName)
        {
            var uninstall = Registry.LocalMachine.OpenSubKey(regKeyPath);
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
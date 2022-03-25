using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Security.Principal;
using System.Threading;

using VitNX3.Functions.Win32;

namespace VitNX3.Functions.AppsAndProcesses
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
                procList += $"\n\nName: {process.ProcessName}.exe" +
                        $"\nID: {process.Id}" +
                        $"\nTitle: \"{process.MainWindowTitle}\"" +
                        $"\nHandle: {process.MainWindowHandle}";
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
        /// Launch a third-party apps.
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
                }
            };
            start.Start();
        }

        /// <summary>
        /// Launch a third-party apps with options.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="showWindow">Show window of this app</param>
        /// <param name="waitMe">Wait this process/app</param>
        public static void RunAW(string targetFile,
            string arguments = "",
            bool showWindow = true,
            bool waitMe = true)
        {
            ProcessWindowStyle app = ProcessWindowStyle.Normal;
            if (!showWindow)
                app = ProcessWindowStyle.Hidden;
            var start = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = targetFile,
                    Arguments = arguments,
                    CreateNoWindow = !showWindow,
                    WindowStyle = app,
                }
            };
            start.Start();
            if (waitMe)
                start.WaitForExit();
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
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                    StandardOutputEncoding = desiredEncoding
                }
            };
            start.Start();
            start.WaitForExit();
            return start.StandardOutput.ReadToEnd();
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
        /// <param name="processNameWithExe">The process name with .exe.</param>
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
        /// Kills the process (C# native).
        /// </summary>
        /// <param name="processNameWithExe">The process name with .exe.</param>
        public static void KillNative(string processNameWithExe)
        {
            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process process in runningProcesses)
            {
                foreach (ProcessModule module in process.Modules)
                {
                    if (module.FileName.Equals(processNameWithExe))
                        process.Kill();
                }
            }
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
        /// <param name="applicationTitle">The application title.</param>
        /// <returns>A bool.</returns>
        public static bool IsOneYourApp(string applicationTitle)
        {
            bool createdNew;
            Mutex currentApp = new Mutex(true,
                applicationTitle,
                out createdNew);
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

        /// <summary>
        /// Checks the debugger for your app.
        /// </summary>
        /// <param name="currentProcess">The current process.</param>
        /// <returns>A bool.</returns>
        public static bool CheckDebugger(Process currentProcess)
        {
            bool isDebuggerPresent = false;
            try
            {
                Import.CheckRemoteDebuggerPresent(currentProcess.Handle, ref
                    isDebuggerPresent);
                return isDebuggerPresent;
            }
            catch { return isDebuggerPresent; }
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
                $"\nInstall path: {toTextText}\n");
            }
            return toText;
        }

        /// <summary>
        /// Gets the path of application from the Windows Registry.
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
        /// Finds the application path.
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
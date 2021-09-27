using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;
using VitNX.Win32;
using VitNX.Extensions;

namespace VitNX.Functions
{
    public class FileSystem
    {
        public static void CopyFolder(string from, string where) { Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(from, where); }

        public static string GetFileMD5(string file)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(@file))
                {
                    var hashBytes = md5.ComputeHash(stream);
                    var sb = new StringBuilder();
                    foreach (var t in hashBytes) { sb.Append(t.ToString("X2")); }
                    return Convert.ToString(sb);
                }
            }
        }

        public static void CreateFileBackup(string file, string new_file_extension, bool save_old_file)
        {
            string bak_file = file + "." + new_file_extension.Replace(".", "");
            if (File.Exists(bak_file)) { try { File.Delete(bak_file); } catch { } }
            if (save_old_file == false) { try { File.Move(file, bak_file); } catch { } }
            else { File.Copy(file, bak_file); try { File.Delete(file); } catch { } }
        }

        public static void CreateShortcut(string shortcut, string file, string exe_name)
        {
            IWshRuntimeLibrary.WshShell wshShell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut Shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(shortcut);
            Shortcut.TargetPath = file;
            Shortcut.WorkingDirectory = file.Replace(@"\" + exe_name, "").Replace(".exe", "");
            Shortcut.Save();
        }

        public static void CleanRecycleBin() { NativeFunctions.SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOSOUND | RecycleFlags.SHERB_NOCONFIRMATION); }

        public static long GetFolderSize(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (Directory.Exists(path)) { return FolderSize.Result(new DirectoryInfo(dirInfo.FullName)); }
            else { return 0; }
        }
    }

    public class Windows
    {
        public static bool Is64x() { return Environment.Is64BitOperatingSystem; }
        public static string GetVersion() { return Convert.ToString(Environment.OSVersion.Version); }
        public static double GetCurrentVersionFromREG() { return double.Parse((string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentVersion", ""), System.Globalization.CultureInfo.InvariantCulture); }
        public static string GetEditionIDFromREG() { return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "EditionID", ""); }
        public static string GetCurrentBuildNumberFromREG() { return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber", ""); }
        public static string GetProductNameFromREG() { return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName", ""); }
        public static string GetDisplayVersionFromREG() { return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DisplayVersion", ""); }
        public static string GetReleaseIdFromREG() { return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ReleaseId", ""); }
    }

    public class Processes
    {
        public static bool IsAdministrator_YourApp() { return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator); }

        public static string Run(string file, string arguments, ProcessWindowStyle style, bool create_window, bool use_shell_execute, bool get_something, bool get_output, bool get_errors)
        {
            string errors = ""; string output = "";
            Process start_info = new Process();
            start_info.StartInfo.FileName = file;
            start_info.StartInfo.Arguments = arguments;
            start_info.StartInfo.WindowStyle = style;
            start_info.StartInfo.CreateNoWindow = !create_window;
            start_info.StartInfo.UseShellExecute = use_shell_execute;
            start_info.StartInfo.RedirectStandardOutput = get_something;
            start_info.Start();
            if (get_output == true) { output = start_info.StandardOutput.ReadToEnd(); }
            if (get_errors == true) { errors = start_info.StandardError.ReadToEnd(); }
            start_info.WaitForExit();
            return output + " " + errors;
        }
    }
}
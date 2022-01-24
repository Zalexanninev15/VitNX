using System;
using System.Collections.Generic;
using System.IO;

namespace VitNX.Functions.Common
{
    public class FileSystem
    {
        public static List<string> ReturnRecursFList(string path)
        {
            List<string> ls = new List<string>();
            try
            {
                string[] folders = Directory.GetDirectories(path);
                foreach (string folder in folders) ls.Add("Папка: " + folder);
                string[] files = Directory.GetFiles(path);
                foreach (string filename in files) ls.Add("Файл: " + filename);
            }
            catch (Exception e) { ls.Add(e.Message.Trim('\n')); }
            return ls;
        }

        public static long GetDirectorySize(DirectoryInfo path)
        {
            long size = 0;
            FileInfo[] fis = path.GetFiles();
            foreach (FileInfo fi in fis)
                size += fi.Length;
            DirectoryInfo[] dis = path.GetDirectories();
            foreach (DirectoryInfo di in dis)
                size += GetDirectorySize(di);
            return size;
        }

        public static void DeleteFile(string file)
        {
            try { File.Delete(file); }
            catch { }
        }

        public static void DeleteFileInTrash(string file)
        {
            try { File.SetAttributes(file, FileAttributes.Normal); } catch { }
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(file,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }

        public static void DeleteDirectoryToTrash(string path)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(path,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }

        public static string[] WriteTextToFileUTF8(string text, string file)
        {
            string fileName = FileNameGenerator(file, "txt");
            string filePath = $@"{Path.GetTempPath()}\{fileName}";
            using (FileStream fs = File.OpenWrite(filePath))
            {
                var data = text;
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
                fs.Write(bytes, 0, bytes.Length);
            }
            return new string[] { fileName, filePath };
        }

        public static string GetText(string file)
        {
            string text = "Text file not found";
            using (StreamReader sr = File.OpenText(file))
                text = sr.ReadToEnd();
            return text;
        }

        public static string GetMD5(string file)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var Stream = File.OpenRead(@file))
                {
                    var hashBytes = md5.ComputeHash(Stream);
                    var sb = new System.Text.StringBuilder();
                    foreach (var t in hashBytes)
                        sb.Append(t.ToString("X2"));
                    return Convert.ToString(sb);
                }
            }
        }

        public static string FileNameGenerator(string tag, string extension)
        {
            string fileName = string.Format("{0}_{1}_{2}.{3}",
                tag,
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Guid.NewGuid(),
                extension);
            return fileName;
        }

        public static void CleanRecycleBin()
        {
            Windows.Win32.Import.SHEmptyRecycleBin(IntPtr.Zero, null,
Windows.Win32.Enums.SHERB_RECYCLE.SHERB_NOSOUND |
Windows.Win32.Enums.SHERB_RECYCLE.SHERB_NOCONFIRMATION);
        }

        public static void CreateFileBackup(string file, string new_file_extension, bool save_old_file)
        {
            string bak_file = file + "." + new_file_extension.Replace(".", "");
            if (File.Exists(bak_file)) { try { File.Delete(bak_file); } catch { } }
            if (save_old_file == false) { try { File.Move(file, bak_file); } catch { } }
            else
            {
                File.Copy(file, bak_file);
                try { File.Delete(file); } catch { }
            }
        }

        public static void CreateShortcut(string shortcut, string file, string exe_name)
        {
            IWshRuntimeLibrary.WshShell wshShell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut Shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(shortcut);
            Shortcut.TargetPath = file;
            Shortcut.WorkingDirectory = file.Replace(@"\" + exe_name, "").Replace(".exe", "");
            Shortcut.Save();
        }

        public static void CopyFolder(string from, string where)
        { Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(from, where); }
    }
}
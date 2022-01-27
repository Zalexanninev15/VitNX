using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VitNX.Functions.Common
{
    public class FileSystem
    {
        public static List<string> GetItemsListInFolder(string sourceFolder)
        {
            List<string> ls = new List<string>();
            try
            {
                string[] folders = Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders) 
                    ls.Add("Folder: " + folder);
                string[] files = Directory.GetFiles(sourceFolder);
                foreach (string filename in files) 
                    ls.Add("File: " + filename);
            }
            catch (Exception e) { ls.Add(e.Message.Trim('\n')); }
            return ls;
        }

        public static long GetFolderSize(DirectoryInfo sourceFolder)
        {
            long size = 0;
            FileInfo[] fis = sourceFolder.GetFiles();
            foreach (FileInfo fi in fis)
                size += fi.Length;
            DirectoryInfo[] dis = sourceFolder.GetDirectories();
            foreach (DirectoryInfo di in dis)
                size += GetFolderSize(di);
            return size;
        }

        public static void DeleteFileForever(string targetFile)
        {
            try { File.Delete(targetFile); } catch { }
        }

        public static void DeleteFileToTrash(string targetFile)
        {
            try { File.SetAttributes(targetFile, FileAttributes.Normal); } catch { }
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(targetFile,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }

        public static void DeleteFolderToTrash(string targetFolder)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(targetFolder,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }

        public static string[] WriteTextToFileUTF8(string text, string targetFile)
        {
            string fileName = FileNameGenerator(targetFile, "txt");
            string filePath = $@"{Path.GetTempPath()}\{fileName}";
            using (FileStream fs = File.OpenWrite(filePath))
            {
                var data = text;
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                fs.Write(bytes, 0, bytes.Length);
            }
            return new string[] { fileName, filePath };
        }

        public static string GetMD5FromFile(string targetFile)
        {
            string text = "Text file not found";
            using (StreamReader sr = File.OpenText(targetFile))
                text = sr.ReadToEnd();
            return text;
        }

        public static string GetMD5(string targetFile)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var Stream = File.OpenRead(targetFile))
                {
                    var hashBytes = md5.ComputeHash(Stream);
                    var sb = new StringBuilder();
                    foreach (var t in hashBytes)
                        sb.Append(t.ToString("X2"));
                    return Convert.ToString(sb);
                }
            }
        }

        public static string FileNameGenerator(string tag, string fileExtension)
        {
            string fileName = string.Format("{0}_{1}_{2}.{3}",
                tag,
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Guid.NewGuid(),
                fileExtension);
            return fileName;
        }

        public static void CleanRecycleBin()
        {
            Windows.Win32.Import.SHEmptyRecycleBin(IntPtr.Zero, null,
            Windows.Win32.Enums.SHERB_RECYCLE.SHERB_NOSOUND |
            Windows.Win32.Enums.SHERB_RECYCLE.SHERB_NOCONFIRMATION);
        }

        public static void CreateFileBackup(string sourceFolder, string newFileExtension, bool saveOldFile)
        {
            string bak_file = sourceFolder + "." + newFileExtension.Replace(".", "");
            if (File.Exists(bak_file)) { try { File.Delete(bak_file); } catch { } }
            if (saveOldFile == false) { try { File.Move(sourceFolder, bak_file); } catch { } }
            else
            {
                File.Copy(sourceFolder, bak_file);
                try { File.Delete(sourceFolder); } catch { }
            }
        }

        public static void CreateShortcut(string shortcut, string targetFile, string nameExe)
        {
            IWshRuntimeLibrary.WshShell wshShell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut Shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(shortcut);
            Shortcut.TargetPath = targetFile;
            Shortcut.WorkingDirectory = targetFile.Replace(@"\" + nameExe, "").Replace(".exe", "");
            Shortcut.Save();
        }

        public static void CopyFolder(string sourceFolder, string targetFolder)
        { 
            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(sourceFolder, targetFolder); 
        }

        public static bool IsPeExe(string targetFile)
        {
            var twoBytes = new byte[2];
            using (var fileStream = File.Open(targetFile, FileMode.Open))
                fileStream.Read(twoBytes, 0, 2);
            return Encoding.UTF8.GetString(twoBytes) == "MZ";
        }

        public static void ZipFolder(string sourceFolder, string zipFile)
        {
            if (!Directory.Exists(sourceFolder))
                throw new ArgumentException("sourceDirectory");
            byte[] zipHeader = new byte[] { 80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            using (FileStream fs = File.Create(zipFile))
                fs.Write(zipHeader, 0, zipHeader.Length);
            dynamic shellApplication = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
            dynamic source = shellApplication.NameSpace(sourceFolder);
            dynamic destination = shellApplication.NameSpace(zipFile);
            destination.CopyHere(source.Items(), 20);
        }

        public static void UnzipFile(string zipFile, string targetFolder)
        {
            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);
            dynamic shellApplication = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
            dynamic compressedFolderContents = shellApplication.NameSpace(zipFile).Items;
            dynamic destinationFolder = shellApplication.NameSpace(targetFolder);
            destinationFolder.CopyHere(compressedFolderContents);
        }
    }
}

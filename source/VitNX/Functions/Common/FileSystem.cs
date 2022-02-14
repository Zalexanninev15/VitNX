using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace VitNX.Functions.Common
{
    /// <summary>
    /// Work with the file system.
    /// </summary>
    public class FileSystem
    {
        /// <summary>
        /// Gets the items list in folder.
        /// </summary>
        /// <param name="sourceFolder">The source folder.</param>
        /// <returns>A list of string.</returns>
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

        /// <summary>
        /// Gets the folder size.
        /// </summary>
        /// <param name="sourceFolder">The source folder.</param>
        /// <returns>A long.</returns>
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

        /// <summary>
        /// Deletes the file forever.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        public static void DeleteFileForever(string targetFile)
        {
            File.Delete(targetFile);
        }

        /// <summary>
        /// Deletes the file to Recycle Bin.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        public static void DeleteFileToRecycleBin(string targetFile)
        {
            File.SetAttributes(targetFile, FileAttributes.Normal);
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(targetFile,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }

        /// <summary>
        /// Deletes the folder to Recycle Bin.
        /// </summary>
        /// <param name="targetFolder">The target folder.</param>
        public static void DeleteFolderToRecycleBin(string targetFolder)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(targetFolder,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }

        /// <summary>
        /// Writes the text to file UTF-8.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="targetFile">The target file.</param>
        /// <returns>An array of string.</returns>
        public static string[] WriteTextToFileUTF8(string text,
            string targetFile)
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

        /// <summary>
        /// Gets the MD5 from file.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        /// <returns>A string.</returns>
        public static string GetMD5FromFile_Method1(string targetFile)
        {
            string text = "Text file not found";
            using (StreamReader sr = File.OpenText(targetFile))
                text = sr.ReadToEnd();
            return text;
        }

        /// <summary>
        /// Gets the MD5 from file.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        /// <returns>A string.</returns>
        public static string GetMD5FromFile_Method2(string targetFile)
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

        /// <summary>
        /// Files the name generator.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>A string.</returns>
        public static string FileNameGenerator(string tag,
            string fileExtension)
        {
            string fileName = string.Format("{0}_{1}_{2}.{3}",
                tag,
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Guid.NewGuid(),
                fileExtension);
            return fileName;
        }

        /// <summary>
        /// Cleans the Recycle Bin.
        /// </summary>
        public static void CleanRecycleBin()
        {
            Windows.Win32.Import.SHEmptyRecycleBin(IntPtr.Zero, null,
            Windows.Win32.Enums.SHERB_RECYCLE.SHERB_NOSOUND |
            Windows.Win32.Enums.SHERB_RECYCLE.SHERB_NOCONFIRMATION);
        }

        /// <summary>
        /// Creates the file backup.
        /// </summary>
        /// <param name="sourceFolder">The source folder.</param>
        /// <param name="newFileExtension">The new file extension.</param>
        /// <param name="saveOldFile">If true, save old file.</param>
        public static void CreateFileBackup(string sourceFolder
            , string newFileExtension,
            bool saveOldFile)
        {
            string bak_file = sourceFolder + "." + newFileExtension.Replace(".", "");
            if (File.Exists(bak_file))
            {
                try { File.Delete(bak_file); } catch { }
            }
            if (saveOldFile == false)
            {
                try { File.Move(sourceFolder, bak_file); } catch { } 
            }
            else
            {
                File.Copy(sourceFolder, bak_file);
                try { File.Delete(sourceFolder); } catch { }
            }
        }

        /// <summary>
        /// Creates the shortcut.
        /// </summary>
        /// <param name="shortcut">The shortcut.</param>
        /// <param name="targetFile">The target file.</param>
        /// <param name="nameExe">The name exe.</param>
        public static void CreateShortcut(string shortcut,
            string targetFile,
            string nameExe)
        {
            IWshRuntimeLibrary.WshShell wshShell = new IWshRuntimeLibrary.WshShell();
            IWshRuntimeLibrary.IWshShortcut Shortcut = (IWshRuntimeLibrary.IWshShortcut)wshShell.CreateShortcut(shortcut);
            Shortcut.TargetPath = targetFile;
            Shortcut.WorkingDirectory = targetFile.Replace(@"\" + nameExe, "").Replace(".exe", "");
            Shortcut.Save();
        }

        /// <summary>
        /// Copies the folder.
        /// </summary>
        /// <param name="sourceFolder">The source folder.</param>
        /// <param name="targetFolder">The target folder.</param>
        public static void CopyFolder(string sourceFolder,
            string targetFolder)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(sourceFolder, 
                targetFolder);
        }

        /// <summary>
        /// Is this a PE file.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        /// <returns>A bool.</returns>
        public static bool IsPeExe(string targetFile)
        {
            var twoBytes = new byte[2];
            using (var fileStream = File.Open(targetFile, FileMode.Open))
                fileStream.Read(twoBytes, 0, 2);
            return Encoding.UTF8.GetString(twoBytes) == "MZ";
        }

        /// <summary>
        /// Zips the folder.
        /// </summary>
        /// <param name="sourceFolder">The source folder.</param>
        /// <param name="zipFile">The zip file.</param>
        public static void ZipFolder(string sourceFolder, 
            string zipFile)
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

        /// <summary>
        /// Uns the zip file.
        /// </summary>
        /// <param name="zipFile">The zip file.</param>
        /// <param name="targetFolder">The target folder.</param>
        public static void UnZipFile(string zipFile,
            string targetFolder)
        {
            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);
            dynamic shellApplication = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
            dynamic compressedFolderContents = shellApplication.NameSpace(zipFile).Items;
            dynamic destinationFolder = shellApplication.NameSpace(targetFolder);
            destinationFolder.CopyHere(compressedFolderContents);
        }

        /// <summary>
        /// Saves the text dialog.
        /// </summary>
        /// <param name="fileExtension">The file extension.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="text">The text.</param>
        public static void SaveTextDialog(string fileExtension = "*.txt",
            string filter = "Text files|*.txt", 
            string text = "Hi!")
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.DefaultExt = fileExtension;
            saveFile1.Filter = filter;
            if (saveFile1.ShowDialog() == DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                using (StreamWriter sw = new StreamWriter(saveFile1.FileName, true))
                {
                    sw.WriteLine(text);
                    sw.Close();
                }
            }
        }

        /// <summary>
        /// Splits the file.
        /// </summary>
        /// <param name="fileInputPath">The file input path.</param>
        /// <param name="folderOutputPath">The folder output path.</param>
        /// <param name="countOfOutputFiles">The count of output files.</param>
        public static void SplitFile(string fileInputPath, 
            string folderOutputPath, 
            int countOfOutputFiles)
        {
            byte[] byteSource = File.ReadAllBytes(fileInputPath);
            FileInfo fiSource = new FileInfo(fileInputPath);
            int partSize = (int)Math.Ceiling((double)(fiSource.Length / countOfOutputFiles));
            int fileOffset = 0;
            string currPartPath;
            FileStream fsPart;
            int sizeRemaining = (int)fiSource.Length;
            for (int i = 0; i < countOfOutputFiles; i++)
            {
                currPartPath = folderOutputPath + "\\" + fiSource.Name + "." + string.Format(@"{0:D4}", i) + ".part";
                if (!File.Exists(currPartPath))
                {
                    fsPart = new FileStream(currPartPath, FileMode.CreateNew);
                    sizeRemaining = (int)fiSource.Length - (i * partSize);
                    if (sizeRemaining < partSize)
                        partSize = sizeRemaining;
                    fsPart.Write(byteSource,
                        fileOffset,
                        partSize);
                    fsPart.Close();
                    fileOffset += partSize;
                }
            }
        }

        /// <summary>
        /// Joins the files.
        /// </summary>
        /// <param name="folderInputPath">The folder input path.</param>
        /// <param name="fileOutputPath">The file output path.</param>
        public static void JoinFiles(string folderInputPath, string fileOutputPath)
        {
            DirectoryInfo diSource = new DirectoryInfo(folderInputPath);
            FileStream fsSource = new FileStream(fileOutputPath, FileMode.Append);
            foreach (FileInfo fiPart in diSource.GetFiles(@"*.part"))
            {
                byte[] bytePart = File.ReadAllBytes(fiPart.FullName);
                fsSource.Write(bytePart, 0, bytePart.Length);
            }
            fsSource.Close();
        }
    }
}
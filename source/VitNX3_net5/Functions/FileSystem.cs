using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using VitNX3.Functions.Win32;

using Path = System.IO.Path;

namespace VitNX3.Functions.FileSystem
{
    /// <summary>
    /// Works with the folders.
    /// </summary>
    public class Folder
    {
        /// <summary>
        /// Gets the items list in folder.
        /// </summary>
        /// <param name="sourceFolder">The source folder.</param>
        /// <returns>A list of string.</returns>
        public static List<string> GetItemsList(string sourceFolder)
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
        public static long GetSize(DirectoryInfo sourceFolder)
        {
            long size = 0;
            FileInfo[] fis = sourceFolder.GetFiles();
            foreach (FileInfo fi in fis)
                size += fi.Length;
            DirectoryInfo[] dis = sourceFolder.GetDirectories();
            foreach (DirectoryInfo di in dis)
                size += GetSize(di);
            return size;
        }

        /// <summary>
        /// Zips the folder.
        /// </summary>
        /// <param name="sourceFolder">The source folder.</param>
        /// <param name="zipFile">The zip file.</param>
        public static void Zip(string sourceFolder,
            string zipFile)
        {
            if (!Directory.Exists(sourceFolder))
                throw new ArgumentException("sourceDirectory");
            byte[] zipHeader = new byte[] { 80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            using (FileStream fs = System.IO.File.Create(zipFile))
                fs.Write(zipHeader, 0, zipHeader.Length);
            dynamic shellApplication = Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application"));
            dynamic source = shellApplication.NameSpace(sourceFolder);
            dynamic destination = shellApplication.NameSpace(zipFile);
            destination.CopyHere(source.Items(), 20);
        }

        /// <summary>
        /// Copies the folder.
        /// </summary>
        /// <param name="sourceFolder">The source folder.</param>
        /// <param name="targetFolder">The target folder.</param>
        public static void Copy(string sourceFolder,
            string targetFolder)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(sourceFolder,
                targetFolder);
        }

        /// <summary>
        /// Deletes the file forever.
        /// </summary>
        /// <param name="targetFolder">The target folder.</param>
        public static void DeleteForever(string targetFolder)
        {
            Directory.Delete(targetFolder, true);
        }

        /// <summary>
        /// Deletes the file to Recycle Bin.
        /// </summary>
        /// <param name="targetFolder">The target folder.</param>
        public static void DeleteToRecycleBin(string targetFolder)
        {
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(targetFolder,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }
    }

    /// <summary>
    /// Works with the files.
    /// </summary>
    public class File
    {
        /// <summary>
        /// Deletes the file forever.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        public static void DeleteForever(string targetFile)
        {
            System.IO.File.SetAttributes(targetFile, FileAttributes.Normal);
            System.IO.File.Delete(targetFile);
        }

        /// <summary>
        /// Deletes the file to Recycle Bin.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        public static void DeleteToRecycleBin(string targetFile)
        {
            System.IO.File.SetAttributes(targetFile, FileAttributes.Normal);
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(targetFile,
                Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }

        /// <summary>
        /// Writes the text to file as UTF-8 to temp folder.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="targetFile">The target file.</param>
        /// <returns>An array of string (File name and File path).</returns>
        public static string[] WriteTextAsUTF8_ToTemp(string text,
            string targetFile)
        {
            string fileName = NameGenerator(targetFile, "txt");
            string filePath = $@"{Path.GetTempPath()}\{fileName}";
            using (FileStream fs = System.IO.File.OpenWrite(filePath))
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
        public static string GetMD5(string targetFile)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                using (var Stream = System.IO.File.OpenRead(targetFile))
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
        /// Retrieves an <see cref="Icon" /> from an ICO or EXE file.
        /// </summary>
        /// <param name="path">The path to an ICO or EXE file.</param>
        /// <returns>
        /// A new <see cref="Icon" /> object that was loaded from an ICO file or extracted from an EXE file.
        /// If the icon could not be retrieved, <see langword="null" /> is returned.
        /// </returns>
        public static Icon ExtractIconFromFile(string path)
        {
            try
            {
                if (Path.GetExtension(path).Equals(".ico",
                    StringComparison.OrdinalIgnoreCase))
                    return new Icon(path);
                else if (Path.GetExtension(path).Equals(".exe",
                    StringComparison.OrdinalIgnoreCase))
                    return ExtractIconFromExecutable(path);
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Extracts the icon from executable (EXE).
        /// </summary>
        /// <param name="path">The path to an EXE file.</param>
        /// <returns>
        /// A new <see cref="Icon" /> object that was loaded from an ICO file or extracted from an EXE file.
        /// If the icon could not be retrieved, <see langword="null" /> is returned.
        /// </returns>
        public static Icon ExtractIconFromExecutable(string path)
        {
            IntPtr module = IntPtr.Zero;
            try
            {
                module = Import.LoadLibraryEx(path, IntPtr.Zero, 2);
                if (module == IntPtr.Zero) throw new Win32Exception();
                Icon result = null;
                Import.EnumResourceNames(module, (IntPtr)14, (mod, type, name, lParam) =>
                {
                    byte[] groupIcon = Import.GetDataFromResource(module, (IntPtr)14, name);
                    int count = BitConverter.ToUInt16(groupIcon, 4);
                    int size = 6 + count * 16 + Enumerable.Range(0, count).Select(i => BitConverter.ToInt32(groupIcon, 6 + i * 14 + 8)).Sum();
                    using (MemoryStream memoryStream = new MemoryStream(size))
                    {
                        using (BinaryWriter writer = new BinaryWriter(memoryStream))
                        {
                            writer.Write(groupIcon, 0, 6);
                            for (int i = 0, offset = 6 + count * 16; i < count; ++i)
                            {
                                byte[] icon = Import.GetDataFromResource(module,
                                    (IntPtr)3,
                                    (IntPtr)BitConverter.ToUInt16(groupIcon, 6 + i * 14 + 12));
                                writer.Seek(6 + i * 16, SeekOrigin.Begin);
                                writer.Write(groupIcon, 6 + i * 14, 8);
                                writer.Write(icon.Length);
                                writer.Write(offset);
                                writer.Seek(offset, SeekOrigin.Begin);
                                writer.Write(icon, 0, icon.Length);
                                offset += icon.Length;
                            }
                        }
                        using (MemoryStream iconStream = new MemoryStream(memoryStream.ToArray()))
                            result = new Icon(iconStream);
                    }
                    return false;
                }, IntPtr.Zero);
                return result;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (module != IntPtr.Zero)
                    Import.FreeLibrary(module);
            }
        }

        /// <summary>
        /// Files name generator.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="fileExtension">The file extension.</param>
        /// <returns>A string.</returns>
        public static string NameGenerator(string tag,
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
        /// Gets the text from file.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        /// <returns>A string.</returns>
        public static string GetText(string targetFile)
        {
            string text = "Text file not found";
            using (StreamReader sr = System.IO.File.OpenText(targetFile))
                text = sr.ReadToEnd();
            return text;
        }

        /// <summary>
        /// Is this a PE file.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        /// <returns>A bool.</returns>
        public static bool IsPeExe(string targetFile)
        {
            var twoBytes = new byte[2];
            using (var fileStream = System.IO.File.Open(targetFile, FileMode.Open))
                fileStream.Read(twoBytes, 0, 2);
            return Encoding.UTF8.GetString(twoBytes) == "MZ";
        }

        /// <summary>
        /// Unzip the zip file.
        /// </summary>
        /// <param name="zipFile">The zip file.</param>
        /// <param name="targetFolder">The target folder.</param>
        public static void Unzip(string zipFile,
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
        /// Creates the file backup.
        /// </summary>
        /// <param name="sourceFolder">The source folder.</param>
        /// <param name="newFileExtension">The new file extension.</param>
        /// <param name="saveOldFile">If true, save old file.</param>
        public static void CreateBackup(string sourceFolder
            , string newFileExtension,
            bool saveOldFile)
        {
            string bak_file = sourceFolder + "." + newFileExtension.Replace(".", "");
            if (System.IO.File.Exists(bak_file))
            {
                try { System.IO.File.Delete(bak_file); } catch { }
            }
            if (saveOldFile == false)
            {
                try { System.IO.File.Move(sourceFolder, bak_file); } catch { }
            }
            else
            {
                System.IO.File.Copy(sourceFolder, bak_file);
                try { System.IO.File.Delete(sourceFolder); } catch { }
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
            Shortcut.WorkingDirectory = targetFile.Replace(@"\" + nameExe, "").Remove(targetFile.Length - 4, targetFile.Length);
            Shortcut.Save();
        }

        /// <summary>
        /// Prints the file.
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        public static void Print(string sourceFile)
        {
            System.Diagnostics.Process print = new System.Diagnostics.Process();
            print.StartInfo.FileName = sourceFile;
            print.StartInfo.Verb = "Print";
            print.Start();
        }

        /// <summary>
        /// Splits the file as many.
        /// </summary>
        /// <param name="fileInputPath">The file input path.</param>
        /// <param name="folderOutputPath">The folder output path.</param>
        /// <param name="countOfOutputFiles">The count of output files.</param>
        public static void Split(string fileInputPath,
            string folderOutputPath,
            int countOfOutputFiles)
        {
            byte[] byteSource = System.IO.File.ReadAllBytes(fileInputPath);
            FileInfo fiSource = new FileInfo(fileInputPath);
            int partSize = (int)Math.Ceiling((double)(fiSource.Length / countOfOutputFiles));
            int fileOffset = 0;
            string currPartPath;
            FileStream fsPart;
            int sizeRemaining = (int)fiSource.Length;
            for (int i = 0; i < countOfOutputFiles; i++)
            {
                currPartPath = folderOutputPath + "\\" + fiSource.Name + "." + string.Format(@"{0:D4}", i) + ".part";
                if (!System.IO.File.Exists(currPartPath))
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
        /// Joins the files as one.
        /// </summary>
        /// <param name="folderInputPath">The folder input path.</param>
        /// <param name="fileOutputPath">The file output path.</param>
        public static void Join(string folderInputPath, string fileOutputPath)
        {
            DirectoryInfo diSource = new DirectoryInfo(folderInputPath);
            FileStream fsSource = new FileStream(fileOutputPath, FileMode.Append);
            foreach (FileInfo fiPart in diSource.GetFiles(@"*.part"))
            {
                byte[] bytePart = System.IO.File.ReadAllBytes(fiPart.FullName);
                fsSource.Write(bytePart, 0, bytePart.Length);
            }
            fsSource.Close();
        }

        /// <summary>
        /// Loads the custom font from file.
        /// </summary>
        /// <param name="sourceFile">The target file.</param>
        /// <param name="size">The size.</param>
        /// <param name="fontStyle">The font style.</param>
        /// <returns>A Font.</returns>
        public static Font LoadCustomFont(string sourceFile,
            float size = 16,
            FontStyle fontStyle = FontStyle.Regular)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(sourceFile);
            return new Font(pfc.Families[0], size, fontStyle);
        }
    }

    public class Other
    {
        /// <summary>
        /// Cleans the Recycle Bin.
        /// </summary>
        public static void CleanRecycleBin()
        {
            Import.SHEmptyRecycleBin(IntPtr.Zero, null,
            Enums.SHERB_RECYCLE.SHERB_NO_SOUND |
            Enums.SHERB_RECYCLE.SHERB_NO_CONFIRMATION);
        }
    }
}
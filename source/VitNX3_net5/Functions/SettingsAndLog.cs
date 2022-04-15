using System;
using System.IO;
using System.Text;

using VitNX3.Functions.Win32;

namespace VitNX3.Functions.SettingsAndLog
{
    /// <summary>
    /// Work with INI config file, based on Windows System functions.
    /// </summary>
    public class Ini
    {
        private static string path;
        private static string defValue = "TEST_VitNX";

        /// <summary>
        /// Initializing the configuration INI file.
        /// </summary>
        /// <param name="file">The ini file.</param>
        public static void Initialize(string file = null)
        {
            path = new FileInfo(file ?? defValue + ".ini").FullName;
        }

        /// <summary>
        /// Reads value of key from section.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="section">The section.</param>
        /// <returns>A string.</returns>
        public static string Read(string key,
            string section = null)
        {
            var retVal = new StringBuilder(255);
            Import.GetPrivateProfileString(section ?? defValue, key, "", retVal, 255, path);
            return retVal.ToString();
        }

        /// <summary>
        /// Writes the value to key in section.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="section">The section.</param>
        public static void Write(string key,
            string value,
            string section = null)
        {
            Import.WritePrivateProfileString(section ?? defValue, key, value, path);
        }

        /// <summary>
        /// Deletes the key in section.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="section">The section.</param>
        public static void DeleteKey(string key,
            string section = null)
        {
            Write(key, null, section ?? defValue);
        }

        /// <summary>
        /// Deletes the section.
        /// </summary>
        /// <param name="section">The section.</param>
        public static void DeleteSection(string section = null)
        {
            Write(null, null, section ?? defValue);
        }

        /// <summary>
        /// Is key exist.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="section">The section.</param>
        /// <returns>A bool.</returns>
        public static bool KeyExists(string key,
            string section = null)
        {
            return Read(key, section).Length > 0;
        }
    }

    /// <summary>
    /// Write text to log file.
    /// </summary>
    public class Log
    {
        private string logFile = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        public Log(string targetFile)
        {
            logFile = targetFile;
        }

        /// <summary>
        /// Writes log to the log file.
        /// </summary>
        /// <param name="logText">Sets the log text.</param>
        public void Write(string logText)
        {
            DateTime currtime = DateTime.Now;
            using (StreamWriter file = new StreamWriter(logFile, true))
            {
                string tmptxt = string.Format("{0:ddMMyy hh:mm:ss} {1}", currtime, logText);
                file.WriteLine(tmptxt);
                file.Close();
            }
        }
    }
}
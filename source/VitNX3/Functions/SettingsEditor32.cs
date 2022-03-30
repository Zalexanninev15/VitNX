using System.IO;
using System.Text;

using VitNX3.Functions.Win32;

namespace VitNX3.Functions
{
    /// <summary>
    /// Work with INI config files with Windows System functions.
    /// </summary>
    public class IniSettings32
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
}
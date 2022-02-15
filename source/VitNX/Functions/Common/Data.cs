using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Data;

namespace VitNX.Functions.Common.Data
{
    /// <summary>
    /// The numerical text converter.
    /// </summary>
    public class NumericalTextConverter : IMultiValueConverter
    {
        /// <summary>
        /// Converts the value(s).
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="targetType">The target type.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>An object.</returns>
        public object Convert(object[] values,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            string rType = "";
            if (values.Count() == 2)
            {
                try
                {
                    var ValueText = (string)values[0] ?? "";
                    var UnitText = (string)values[1] ?? "";
                    if (ValueText.Trim() != string.Empty)
                        rType = $"{ValueText.Trim()} {UnitText.Trim()}";
                }
                catch { }
            }
            return rType;
        }

        /// <summary>
        /// Converts back the value(s).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetTypes">The target types.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>An array of object.</returns>
        public object[] ConvertBack(
            object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Work with text.
    /// </summary>
    public class Text
    {
        /// <summary>
        /// Possible size suffixes.
        /// </summary>
        public static readonly string[] SizeSuffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        /// <summary>
        /// Contains the only latters.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A bool.</returns>
        public static bool ContainsOnlyLatters(string text) => text.All(char.IsLetter);

        /// <summary>
        /// Contains the numbers latters.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A bool.</returns>
        public static bool ContainsNumbersLatters(string text) => text.All(char.IsLetterOrDigit);

        /// <summary>
        /// Replacers the monster.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="forReplace">The for replace.</param>
        /// <param name="toReplace">The to replace.</param>
        /// <param name="countOfSpacesWith_forReplace">The count of spaces with forReplace. From 0 to 2.</param>
        /// <returns>A string.</returns>
        public static string ReplacerMonster(string text, List<string> forReplace, List<string> toReplace, int countOfSpacesWith_forReplace)
        {
            for (int i = 0; i < forReplace.Count; i++)
            {
                switch (countOfSpacesWith_forReplace)
                {
                    case 0:
                        {
                            if (text.Contains($"{forReplace[i]}"))
                                text = text.Replace(forReplace[i], toReplace[i]);
                            break;
                        }
                    case 1:
                        {
                            if (text.Contains($" {forReplace[i]}"))
                                text = text.Replace(forReplace[i], toReplace[i]);
                            break;
                        }
                    case 2:
                        {
                            if (text.Contains($" {forReplace[i]} "))
                                text = text.Replace(forReplace[i], toReplace[i]);
                            break;
                        }
                }
            }
            return text;
        }

        /// <summary>
        /// Contains the only numbers.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A bool.</returns>
        public static bool ContainsOnlyNumbers(string text) => text.All(char.IsDigit);

        /// <summary>
        /// Contains the symbols.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A bool.</returns>
        public static bool ContainsSymbols(string text) => text.All(char.IsSymbol);

        /// <summary>
        /// Contains the spaces.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A bool.</returns>
        public static bool ContainsSpaces(string text) => text.All(char.IsWhiteSpace);

        /// <summary>
        /// Lists the contains string.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="text">The text.</param>
        /// <returns>A bool.</returns>
        public static bool ListContainsString(List<string> list, string text) => list.Where(CheckString => CheckString.Contains(text)) != null;

        /// <summary>
        /// Lists the add strings to list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="strings">The strings.</param>
        /// <returns>A list of string.</returns>
        public static List<string> ListAddStringsToList(List<string> list, string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
                list.Add(strings[i]);
            return list;
        }

        /// <summary>
        /// Lists the remove strings from list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="strings">The strings.</param>
        /// <returns>A list of string.</returns>
        public static List<string> ListRemoveStringsFromList(List<string> list, string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
                list.Remove(strings[i]);
            return list;
        }

        private static Random randomData = new Random();

        /// <summary>
        /// Generate the GUID.
        /// </summary>
        /// <returns>A string.</returns>
        public static string GenerateGuid = Guid.NewGuid().ToString();

        /// <summary>
        /// Randoms the chars.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>A string.</returns>
        public static string RandomChars(int number)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789%@#()$?*_[]+-";
            return new string(Enumerable.Repeat(chars, number)
              .Select(s => s[randomData.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Randoms the nums.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>A string.</returns>
        public static string RandomNums(int number)
        {
            const string chars = "1234567890";
            return new string(Enumerable.Repeat(chars, number)
              .Select(s => s[randomData.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Fixes the de-encoding.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A string.</returns>
        public static string FixDeEncoding(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Are the valid card number.
        /// </summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns>A bool.</returns>
        public static bool IsValidCardNumber(string cardNumber)
        {
            cardNumber = cardNumber.Replace(" ", "");
            int[] doubledDigits = new int[cardNumber.Length / 2];
            int k = 0;
            for (int i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                int digit = int.Parse(cardNumber[i].ToString());
                doubledDigits[k] = digit * 2;
                k++;
            }
            int total = 0;
            foreach (int i in doubledDigits)
            {
                string number = i.ToString();
                for (int j = 0; j < number.Length; j++)
                    total += int.Parse(number[j].ToString());
            }
            int total2 = 0;
            for (int i = cardNumber.Length - 1; i >= 0; i -= 2)
            {
                int digit = int.Parse(cardNumber[i].ToString());
                total2 += digit;
            }
            int final = total + total2;
            return final % 10 == 0;
        }

        /// <summary>
        /// Gives a suffix to the file size for output.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A string.</returns>
        public static string SizeSuffix(long value)
        {
            if (value < 0)
                return "-" + SizeSuffix(-value);
            if (value == 0)
                return "0.0 bytes";
            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));
            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }
    }

    /// <summary>
    /// Encrypt and decrypt the text or byte[].
    /// </summary>
    public class EncryptAndDecrypt
    {
        /// <summary>
        /// XOR method (encrypt and decrypt).
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="key">The key.</param>
        /// <returns>A string.</returns>
        public static string XOR_Both(string text, int key)
        {
            string newText = string.Empty;
            for (int i = 0; i < text.Length; i++)
            {
                int charValue = Convert.ToInt32(text[i]);
                charValue ^= key;
                newText += char.ConvertFromUtf32(charValue);
            }
            return newText;
        }

        /// <summary>
        /// Encrypt by MD5.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>A string.</returns>
        public static string MD5_Encrypt(string text)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i <= data.Length - 1; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        /// <summary>
        /// Simples the encrypt.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="password">The password.</param>
        /// <returns>A string.</returns>
        public static string SimpleEncrypt(string text, string password)
        {
            byte[] input = Encoding.UTF8.GetBytes(text);
            byte[] output = SimpleEncryptAsByte(input, password);
            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// Simples the decrypt.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="password">The password.</param>
        /// <returns>A string.</returns>
        public static string SimpleDecrypt(string text, string password)
        {
            byte[] input = Convert.FromBase64String(text);
            byte[] output = SimpleDecryptAsByte(input, password);
            return Encoding.UTF8.GetString(output);
        }

        /// <summary>
        /// Simples the encrypt as byte.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="password">The password.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] SimpleEncryptAsByte(byte[] input, string password)
        {
            try
            {
                TripleDESCryptoServiceProvider service = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] key = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                byte[] iv = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                return Transform(input, service.CreateEncryptor(key, iv));
            }
            catch { return new byte[0]; }
        }

        /// <summary>
        /// Simples the decrypt as byte.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="password">The password.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] SimpleDecryptAsByte(byte[] input, string password)
        {
            try
            {
                TripleDESCryptoServiceProvider service = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] key = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                byte[] iv = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
                return Transform(input, service.CreateDecryptor(key, iv));
            }
            catch { return new byte[0]; }
        }

        /// <summary>
        /// Transforms the text.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="CryptoTransform">The crypto transform.</param>
        /// <returns>An array of byte.</returns>
        private static byte[] Transform(byte[] input, ICryptoTransform CryptoTransform)
        {
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(memStream, CryptoTransform, CryptoStreamMode.Write);
            cryptStream.Write(input, 0, input.Length);
            cryptStream.FlushFinalBlock();
            memStream.Position = 0;
            byte[] result = new byte[Convert.ToInt32(memStream.Length)];
            memStream.Read(result, 0, Convert.ToInt32(result.Length));
            memStream.Close();
            cryptStream.Close();
            return result;
        }
    }

    /// <summary>
    /// Encrypter and decrypter for text.
    /// </summary>
    public class EncrypterAndDecrypter
    {
        private static string password = "Hello World!";

        /// <summary>
        /// Sets the password.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A string.</returns>
        public static string SetPassword(string input) => password = input;

        /// <summary>
        /// Decrypts the text.
        /// </summary>
        /// <param name="TextToBeDecrypted">The text to be decrypted.</param>
        /// <returns>A string.</returns>
        public static string Decrypt(string input)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            string DecryptedData = null;
            try
            {
                byte[] EncryptedData = Convert.FromBase64String(input);
                byte[] Salt = Encoding.ASCII.GetBytes(password.Length.ToString());
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(password, Salt);
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32),
                    SecretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    Decryptor,
                    CryptoStreamMode.Read);
                byte[] PlainText = new byte[EncryptedData.Length];
                int DecryptedCount = cryptoStream.Read(PlainText, 0,
                    PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch { DecryptedData = input; }
            return DecryptedData;
        }

        /// <summary>
        /// Encrypts the text.
        /// </summary>
        /// <param name="TextToBeEncrypted">The text to be encrypted.</param>
        /// <returns>A string.</returns>
        public static string Encrypt(string input)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] PlainText = Encoding.Unicode.GetBytes(input);
            byte[] Salt = Encoding.ASCII.GetBytes(password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(password, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32),
                SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                Encryptor,
                CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string EncryptedData = Convert.ToBase64String(CipherBytes);
            return EncryptedData;
        }

        /// <summary>
        /// Encrypts the text with qry param.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A string.</returns>
        public static string EncryptQryParam(string input)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] PlainText = Encoding.Unicode.GetBytes(input);
            byte[] Salt = Encoding.ASCII.GetBytes(password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(password, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32),
                SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                Encryptor,
                CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string EncryptedData = Convert.ToBase64String(CipherBytes);
            return EncryptedData.Replace("+", "~").Replace("/", "^");
        }

        /// <summary>
        /// Decrypts the text with qry param.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>A string.</returns>
        public static string DecryptQryParam(string input)
        {
            input = input.Replace("~", "+").Replace("^", "/");
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            string DecryptedData = null;
            try
            {
                byte[] EncryptedData = Convert.FromBase64String(input);
                byte[] Salt = Encoding.ASCII.GetBytes(password.Length.ToString());
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(password, Salt);
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32),
                    SecretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                    Decryptor,
                    CryptoStreamMode.Read);
                byte[] PlainText = new byte[(EncryptedData.Length)];
                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0,
                    DecryptedCount);
            }
            catch { DecryptedData = input; }
            return DecryptedData;
        }
    }

    /// <summary>
    /// Compress and decompress the text or byte[].
    /// </summary>
    public class CompressAndDecompress
    {
        /* Example:
         *
         * Compress
         * byte[] compressed = CompressAndDecompress.CompressBytes(CompressAndDecompress.GetBytes(input));
         * string output = Encoding.UTF8.GetString(compressed);
         *
         * Decompress
         * string output = CompressAndDecompress.BytesToString(CompressAndDecompress.DecompressBytes(compressed));
         *
         */

        /// <summary>
        /// Gets the bytes.
        /// Example: CompressAndDecompress.GetBytes(input)
        /// </summary>
        /// <param name="str">The str.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] GetBytes(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }

        /// <summary>
        /// Bytes the to string.
        /// Example: string output = CompressAndDecompress.BytesToString(CompressAndDecompress.DecompressBytes(compressed));
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>A string.</returns>
        public static string BytesToString(byte[] input)
        {
            return Encoding.UTF8.GetString(input);
        }

        /// <summary>
        /// Compresses the bytes.
        /// Example: string output = Encoding.UTF8.GetString(CompressAndDecompress.CompressBytes(CompressAndDecompress.GetBytes(input)));
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] CompressBytes(byte[] input)
        {
            MemoryStream output = new MemoryStream();
            GZipStream gzip = new GZipStream(output, CompressionMode.Compress, true);
            gzip.Write(input, 0, input.Length);
            gzip.Close();
            return output.ToArray();
        }

        /// <summary>
        /// Decompresses the bytes.
        /// Example: CompressAndDecompress.DecompressBytes(compressed)
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>An array of byte.</returns>
        public static byte[] DecompressBytes(byte[] input)
        {
            MemoryStream memory = new MemoryStream();
            memory.Write(input, 0, input.Length);
            memory.Position = 0;
            GZipStream gzip = new GZipStream(memory, CompressionMode.Decompress, true);
            MemoryStream output = new MemoryStream();
            byte[] buff = new byte[64];
            int read = gzip.Read(buff, 0, buff.Length);
            while (read > 0)
            {
                output.Write(buff, 0, read);
                read = gzip.Read(buff, 0, buff.Length);
            }
            gzip.Close();
            return output.ToArray();
        }
    }
}
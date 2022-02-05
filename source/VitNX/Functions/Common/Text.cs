using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VitNX.Functions.Common
{
    public class Text
    {
        public static bool TextContainsOnlyLatters(string text) => text.All(Char.IsLetter) ? true : false;

        public static bool TextContainsNumbersLatters(string text) => text.All(Char.IsLetterOrDigit) ? true : false;

        public static bool TextContainsOnlyNumbers(string text) => text.All(Char.IsDigit) ? true : false;

        public static bool TextContainsSymbols(string text) => text.All(Char.IsSymbol) ? true : false;

        public static bool TextContainsSpaces(string text) => text.All(Char.IsWhiteSpace) ? true : false;

        public static bool ListContainsString(List<string> list, string text) => list.Where(CheckString => CheckString.Contains(text)) != null ? true : false;

        public static List<string> ListAddStringsToList(List<string> list, string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
                list.Add(strings[i]);
            return list;
        }

        public static List<string> ListRemoveStringsFromList(List<string> list, string[] strings)
        {
            for (int i = 0; i < strings.Length; i++)
                list.Remove(strings[i]);
            return list;
        }

        private static Random random = new Random();

        public static string RandomChars(int number)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789%@#()$?*_[]+-";
            return new string(Enumerable.Repeat(chars, number)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomNums(int number)
        {
            const string chars = "1234567890";
            return new string(Enumerable.Repeat(chars, number)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string FixText(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
﻿using System.Drawing;

using VitNX3.Functions.CSharp;

namespace VitNX3.UI.Console
{
    /// <summary>
    /// The tools for work with console UI.
    /// </summary>
    public class Text
    {
        /// <summary>
        /// Writeln text with the colors.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="textColor">The text color.</param>
        /// <param name="backgroundColor">The background color.</param>
        public static void Writeln(string text,
            Color textColor,
            Color backgroundColor)
        {
            System.Console.ForegroundColor = Others.ConvertColorToConsoleColor(textColor);
            System.Console.BackgroundColor = Others.ConvertColorToConsoleColor(backgroundColor);
            System.Console.WriteLine(text);
            System.Console.ResetColor();
        }
    }
}
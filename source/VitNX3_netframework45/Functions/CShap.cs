using System;
using System.Drawing;

namespace VitNX3.Functions
{
    /// <summary>
    /// Work with C#.
    /// </summary>
    public class CShap
    {
        /// <summary>
        /// Cleans the memory.
        /// </summary>
        public static void CleanMemory()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch { }
        }

        /// <summary>
        /// Plays the error/successes sound.
        /// </summary>
        public static void PlaySound()
        {
            Console.Beep(3000, 30);
            Console.Beep(1000, 30);
        }

        /// <summary>
        /// Converts the DWord color to RGB.
        /// </summary>
        /// <param name="color">Color as unit.</param>
        /// <returns>A Color.</returns>
        public static Color ConvertDWordColorToRGB(uint color)
        {
            byte redColor = (byte)((0x000000FF & color) >> 0);
            byte greenColor = (byte)((0x0000FF00 & color) >> 8);
            byte blueColor = (byte)((0x00FF0000 & color) >> 16);
            return Color.FromArgb(redColor, greenColor, blueColor);
        }

        /// <summary>
        /// Converts the color to color for console.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public static ConsoleColor ConvertColorToConsoleColor(Color color)
        {
            int index = (color.R > 128 | color.G > 128 | color.B > 128) ? 8 : 0;
            index |= (color.R > 64) ? 4 : 0;
            index |= (color.G > 64) ? 2 : 0;
            index |= (color.B > 64) ? 1 : 0;
            return (ConsoleColor)index;
        }
    }
}
using System;
using System.Drawing;

namespace VitNX.Functions.Common
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
        /// <param name="colorSetEx">The color set ex.</param>
        /// <returns>A Color.</returns>
        public static Color ConvertDWordColorToRGB(uint colorSetEx)
        {
            byte redColor = (byte)((0x000000FF & colorSetEx) >> 0);
            byte greenColor = (byte)((0x0000FF00 & colorSetEx) >> 8);
            byte blueColor = (byte)((0x00FF0000 & colorSetEx) >> 16);
            return Color.FromArgb(redColor, greenColor, blueColor);
        }
    }
}
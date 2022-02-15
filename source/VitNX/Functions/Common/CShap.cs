using System;
using System.Drawing;
using System.Drawing.Text;

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
        /// Loads the custom font from file.
        /// </summary>
        /// <param name="targetFile">The target file.</param>
        /// <param name="size">The size.</param>
        /// <param name="fontStyle">The font style.</param>
        /// <returns>A Font.</returns>
        public static Font LoadCustomFontFromFile(string targetFile,
            float size = 16,
            FontStyle fontStyle = FontStyle.Regular)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(targetFile);
            return new Font(pfc.Families[0], size, fontStyle);
        }
    }
}